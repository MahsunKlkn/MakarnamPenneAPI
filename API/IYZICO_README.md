# İyzico Payment Integration - Quick Start

## ⚡ Hızlı Başlangıç

### 1. NuGet Paketini Yükleyin
```bash
dotnet add .\Business\Business.csproj package Iyzipay
```

### 2. appsettings.json'ı Düzenleyin
```json
"Iyzico": {
  "ApiKey": "sandbox-rmmRNQqs82pDe7Wk1diKhHr96JZXcYBq",
  "SecretKey": "sandbox-r5mU1RudySfgruykpfhrgInH099dYeUs",
  "BaseUrl": "https://sandbox-api.iyzipay.com"
}
```

**Test anahtarlarınızı alın:** https://sandbox-merchant.iyzipay.com

### 3. Database Migration Çalıştırın
```bash
dotnet ef migrations add AddPaymentTable --project .\DataAccessLayer\DataAccessLayer.csproj --startup-project .\API\API.csproj

dotnet ef database update --project .\DataAccessLayer\DataAccessLayer.csproj --startup-project .\API\API.csproj
```

### 4. Projeyi Çalıştırın
```bash
dotnet run --project .\API\API.csproj
```

---

## 📋 Oluşturulan Dosyalar

### **Entities Layer**
- ✅ `Entities/Concrete/Payment.cs` - Payment entity
- ✅ `Entities/Dto/PaymentRequestDto.cs` - Ödeme isteği DTO
- ✅ `Entities/Dto/PaymentResponseDto.cs` - Ödeme yanıt DTO'ları

### **Data Access Layer**
- ✅ `DataAccessLayer/Abstract/IPaymentDal.cs` - Payment repository interface
- ✅ `DataAccessLayer/Concrete/EntityRepository/EfPaymentRepository.cs` - Payment repository implementation
- ✅ `DataAccessLayer/Concrete/EntityRepository/Context/Context.cs` - DbContext'e Payment eklendi

### **Business Layer**
- ✅ `Business/Abstract/IPaymentService.cs` - Payment service interface
- ✅ `Business/Concrete/PaymentManager.cs` - Payment service implementation

### **API Layer**
- ✅ `API/Controllers/PaymentController.cs` - Payment API endpoints
- ✅ `API/Program.cs` - DI Container'a payment servisleri eklendi
- ✅ `API/appsettings.json` - İyzico konfigürasyonu eklendi

### **Dokümantasyon**
- ✅ `IYZICO_KULLANIM_KILAVUZU.md` - Detaylı kullanım kılavuzu

---

## 🔌 API Endpoint'leri

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| POST | `/api/payment/initiate` | Ödeme başlatır, 3D Secure HTML döner |
| POST | `/api/payment/callback` | İyzico callback'i (otomatik) |
| GET | `/api/payment/conversation/{conversationId}` | Ödeme sorgula |
| GET | `/api/payment/payment/{paymentId}` | Ödeme sorgula |
| GET | `/api/payment/order/{orderId}` | Siparişin ödemelerini listele |

---

## 🧪 Test Kartları (Sandbox)

| Kart No | SKT | CVC | Sonuç |
|---------|-----|-----|-------|
| 5528790000000008 | 12/30 | 123 | ✅ Başarılı |
| 5311570000000005 | 12/30 | 123 | ✅ 3DS Başarılı |
| 4603450000000000 | 12/30 | 123 | ✅ Başarılı |

---

## 📱 Flutter Entegrasyon Örneği

### 1. Backend'e İstek Gönder
```dart
final response = await http.post(
  Uri.parse('http://localhost:5000/api/payment/initiate'),
  body: jsonEncode(paymentRequest),
  headers: {'Content-Type': 'application/json'},
);

final data = jsonDecode(response.body);
String htmlContent = data['threeDSHtmlContent'];
```

### 2. WebView'da 3D Secure Göster
```dart
WebView(
  initialData: InAppWebViewInitialData(data: htmlContent),
  onLoadStop: (controller, url) {
    if (url.contains('/payment/callback')) {
      // Ödeme tamamlandı!
    }
  },
)
```

---

## ⚠️ ÖNEMLİ NOTLAR

1. **Secret Key'i asla Flutter'da saklamayın!** Tüm ödeme işlemleri backend üzerinden yapılır.

2. **Callback URL:** Flutter uygulamanızdan gelen callback URL'i backend'inizin URL'i olmalıdır:
   ```
   callbackUrl: "https://your-backend.com/api/payment/callback"
   ```

3. **Production'a Geçiş:**
   - `BaseUrl`'i değiştirin: `https://api.iyzipay.com`
   - Production API Key'lerini kullanın
   - Environment variables kullanın

4. **IP Adresi:** `PaymentManager.cs` içinde hardcoded IP var:
   ```csharp
   Ip = "85.34.78.112" // Bunu gerçek kullanıcı IP'sinden alın
   ```

---

## 🔧 Gerekli Paketler

```xml
<PackageReference Include="Iyzipay" Version="2.1.38" />
```

---

## 📚 Daha Fazla Bilgi

Detaylı kullanım kılavuzu için: `IYZICO_KULLANIM_KILAVUZU.md`

İyzico Dokümantasyonu: https://dev.iyzipay.com/tr

---

## ✅ Yapılması Gerekenler

- [ ] NuGet paketini yükleyin (`dotnet add package Iyzipay`)
- [ ] appsettings.json'da API Key'leri güncelleyin
- [ ] Migration çalıştırın
- [ ] Test kartlarıyla test edin
- [ ] Flutter uygulamasından entegre edin
- [ ] Production'a geçmeden önce IP adresini dinamikleştirin

**Backend hazır! Artık Flutter uygulamanızdan ödeme işlemlerini başlatabilirsiniz.** 🚀
