using Business.Abstract;
using DataAccessLayer.Abstarct;
using Entities.Dto;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentEntity = Entities.Concrete.Payment;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly IPaymentDal _paymentDal;
        private readonly IBasketDal _basketDal;
        private readonly IConfiguration _configuration;
        private readonly Options _iyzicoOptions;

        public PaymentManager(IPaymentDal paymentDal, IBasketDal basketDal, IConfiguration configuration)
        {
            _paymentDal = paymentDal;
            _basketDal = basketDal;
            _configuration = configuration;
            
            _iyzicoOptions = new Options
            {
                ApiKey = _configuration["Iyzico:ApiKey"] ?? throw new ArgumentNullException("Iyzico:ApiKey"),
                SecretKey = _configuration["Iyzico:SecretKey"] ?? throw new ArgumentNullException("Iyzico:SecretKey"),
                BaseUrl = _configuration["Iyzico:BaseUrl"] ?? "https://sandbox-api.iyzipay.com"
            };
        }

        

public async Task<PaymentResponseDto> InitiatePayment(PaymentRequestDto request)
{
    try
    {
        string conversationId = Guid.NewGuid().ToString();
        
        // ✅ CheckoutFormInitialize kullanın - Kullanıcı kartını WebView'da girecek
        CreateCheckoutFormInitializeRequest iyzicoRequest = new CreateCheckoutFormInitializeRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = conversationId,
            Price = request.Price.ToString("F2", CultureInfo.InvariantCulture),
            PaidPrice = request.PaidPrice.ToString("F2", CultureInfo.InvariantCulture),
            Currency = Currency.TRY.ToString(),
            BasketId = request.BasketId.ToString(),
            PaymentGroup = PaymentGroup.PRODUCT.ToString(),
            CallbackUrl = request.CallbackUrl,
            EnabledInstallments = new List<int> { 1, 2, 3, 6, 9 },
            
            Buyer = new Buyer
            {
                Id = request.BuyerId,
                Name = request.BuyerName,
                Surname = request.BuyerSurname,
                Email = request.BuyerEmail,
                IdentityNumber = request.BuyerIdentityNumber,
                RegistrationAddress = request.BuyerRegistrationAddress,
                City = request.BuyerCity,
                Country = request.BuyerCountry,
                ZipCode = request.BuyerZipCode,
                Ip = "85.34.78.112" // Gerçek IP'yi HttpContext'ten alabilirsiniz
            },
            
            ShippingAddress = new Iyzipay.Model.Address
            {
                ContactName = request.ShippingContactName,
                City = request.ShippingCity,
                Country = request.ShippingCountry,
                Description = request.ShippingAddress,
                ZipCode = request.ShippingZipCode
            },
            
            BillingAddress = new Iyzipay.Model.Address
            {
                ContactName = request.BillingContactName,
                City = request.BillingCity,
                Country = request.BillingCountry,
                Description = request.BillingAddress,
                ZipCode = request.BillingZipCode
            },
            
            BasketItems = request.BasketItems.Select(item => new BasketItem
            {
                Id = item.Id,
                Name = item.Name,
                Category1 = item.Category1,
                ItemType = BasketItemType.PHYSICAL.ToString(),
                Price = item.Price.ToString("F2", CultureInfo.InvariantCulture)
            }).ToList()
        };

        // CheckoutForm oluştur
        CheckoutFormInitialize checkoutFormInitialize = await Task.Run(() => 
            CheckoutFormInitialize.Create(iyzicoRequest, _iyzicoOptions));

        if (checkoutFormInitialize.Status != "success")
        {
            Console.WriteLine($"❌ İyzico Hatası: {checkoutFormInitialize.ErrorMessage}");
            Console.WriteLine($"❌ Hata Kodu: {checkoutFormInitialize.ErrorCode}");
            
            return new PaymentResponseDto
            {
                Success = false,
                Message = checkoutFormInitialize.ErrorMessage ?? "Ödeme başlatılamadı",
                ErrorCode = checkoutFormInitialize.ErrorCode,
                ErrorMessage = checkoutFormInitialize.ErrorMessage
            };
        }

        // Veritabanına kaydet
        var payment = new PaymentEntity
        {
            ConversationId = conversationId,
            Amount = request.Price,
            PaidPrice = request.PaidPrice,
            Currency = request.Currency,
            Status = "Pending",
            PaymentStatus = checkoutFormInitialize.Status,
            PaymentId = checkoutFormInitialize.Token,
            DateCreated = DateTime.UtcNow,
            DateUpdated = DateTime.UtcNow
        };

        _paymentDal.Insert(payment);

        Console.WriteLine($"✅ İyzico CheckoutForm başarılı - Token: {checkoutFormInitialize.Token}");
        Console.WriteLine($"✅ ConversationId: {conversationId}");

        return new PaymentResponseDto
        {
            Success = true,
            Message = "Ödeme sayfası oluşturuldu",
            ThreeDSHtmlContent = checkoutFormInitialize.CheckoutFormContent,
            PaymentId = checkoutFormInitialize.Token,
            ConversationId = conversationId,
            Status = "success"
        };
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Exception: {ex.Message}");
        Console.WriteLine($"❌ StackTrace: {ex.StackTrace}");
        
        return new PaymentResponseDto
        {
            Success = false,
            Message = $"Ödeme başlatılamadı: {ex.Message}",
            ErrorMessage = ex.Message
        };
    }
}

        public async Task<PaymentResultDto> HandleCallback(PaymentCallbackDto callback)
        {
            try
            {
                var request = new RetrieveCheckoutFormRequest
                {
                    Token = callback.Token
                };

                var checkoutForm = await Task.Run(() => 
                    CheckoutForm.Retrieve(request, _iyzicoOptions));

                var payment = _paymentDal.GetByConversationId(callback.ConversationId);
                
                if (payment != null)
                {
                    payment.Status = checkoutForm.PaymentStatus == "SUCCESS" ? "Success" : "Failed";
                    payment.PaymentStatus = checkoutForm.PaymentStatus;
                    payment.PaymentId = checkoutForm.PaymentId?.ToString();
                    payment.CardFamily = checkoutForm.CardFamily;
                    payment.CardType = checkoutForm.CardType;
                    payment.CardAssociation = checkoutForm.CardAssociation;
                    payment.LastFourDigits = checkoutForm.LastFourDigits;
                    payment.PaymentDate = DateTime.UtcNow;
                    payment.DateUpdated = DateTime.UtcNow;
                    
                    if (checkoutForm.PaymentStatus != "SUCCESS")
                    {
                        payment.ErrorCode = checkoutForm.ErrorCode;
                        payment.ErrorMessage = checkoutForm.ErrorMessage;
                        payment.ErrorGroup = checkoutForm.ErrorGroup;
                    }

                    _paymentDal.Update(payment);

                    return new PaymentResultDto
                    {
                        Success = checkoutForm.PaymentStatus == "SUCCESS",
                        Message = checkoutForm.PaymentStatus == "SUCCESS" ? "Ödeme başarılı" : checkoutForm.ErrorMessage ?? "Ödeme başarısız",
                        Status = checkoutForm.PaymentStatus ?? "UNKNOWN",
                        PaidPrice = payment.PaidPrice,
                        Currency = payment.Currency,
                        PaymentId = payment.PaymentId,
                        ConversationId = payment.ConversationId,
                        CardFamily = payment.CardFamily,
                        CardType = payment.CardType,
                        PaymentDate = payment.PaymentDate
                    };
                }

                return new PaymentResultDto
                {
                    Success = false,
                    Message = "Ödeme kaydı bulunamadı",
                    Status = "NOT_FOUND"
                };
            }
            catch (Exception ex)
            {
                return new PaymentResultDto
                {
                    Success = false,
                    Message = $"Ödeme kontrolü yapılamadı: {ex.Message}",
                    Status = "ERROR"
                };
            }
        }

        public PaymentEntity? GetByConversationId(string conversationId)
        {
            return _paymentDal.GetByConversationId(conversationId);
        }

        public PaymentEntity? GetByPaymentId(string paymentId)
        {
            return _paymentDal.GetByPaymentId(paymentId);
        }

        public List<PaymentEntity> GetByOrderId(int orderId)
        {
            return _paymentDal.GetByOrderId(orderId);
        }
    }
}
