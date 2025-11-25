# İyzico Ödeme Entegrasyonu - Backend Kullanım Kılavuzu

## 🚀 Kurulum Adımları

### 1. NuGet Paketlerini Yükleyin

```bash
cd Business
dotnet add package Iyzipay

cd ../API
dotnet add package Iyzipay
```

### 2. Veritabanı Migration'ını Çalıştırın

```bash
# Migration oluştur
dotnet ef migrations add AddPaymentTable --project .\DataAccessLayer\DataAccessLayer.csproj --startup-project .\API\API.csproj

# Veritabanını güncelle
dotnet ef database update --project .\DataAccessLayer\DataAccessLayer.csproj --startup-project .\API\API.csproj
```

### 3. appsettings.json Dosyasını Güncelleyin

`appsettings.json` dosyasında İyzico test anahtarlarınızı ekleyin:

```json
{
  "Iyzico": {
    "ApiKey": "sandbox-SIZIN_API_KEYINIZ",
    "SecretKey": "sandbox-SIZIN_SECRET_KEYINIZ",
    "BaseUrl": "https://sandbox-api.iyzipay.com"
  }
}
```

**Not:** Test anahtarlarınızı https://sandbox-merchant.iyzipay.com adresinden alabilirsiniz.

---

## 📡 API Endpoint'leri

### 1. Ödeme Başlatma

**Endpoint:** `POST /api/payment/initiate`

**Açıklama:** Ödeme işlemini başlatır ve 3D Secure HTML içeriğini döner.

**Request Body:**
```json
{
  "price": 100.00,
  "paidPrice": 100.00,
  "currency": "TRY",
  "basketId": 1,
  "callbackUrl": "https://yourapp.com/api/payment/callback",
  
  "buyerId": "123",
  "buyerName": "Ahmet",
  "buyerSurname": "Yılmaz",
  "buyerEmail": "ahmet@example.com",
  "buyerIdentityNumber": "12345678901",
  "buyerRegistrationAddress": "Atatürk Cad. No:1",
  "buyerCity": "Istanbul",
  "buyerCountry": "Turkey",
  "buyerZipCode": "34000",
  "buyerPhone": "+905551234567",
  
  "shippingContactName": "Ahmet Yılmaz",
  "shippingCity": "Istanbul",
  "shippingCountry": "Turkey",
  "shippingAddress": "Atatürk Cad. No:1",
  "shippingZipCode": "34000",
  
  "billingContactName": "Ahmet Yılmaz",
  "billingCity": "Istanbul",
  "billingCountry": "Turkey",
  "billingAddress": "Atatürk Cad. No:1",
  "billingZipCode": "34000",
  
  "basketItems": [
    {
      "id": "1",
      "name": "Ürün 1",
      "category1": "Elektronik",
      "itemType": "PHYSICAL",
      "price": 50.00
    },
    {
      "id": "2",
      "name": "Ürün 2",
      "category1": "Giyim",
      "itemType": "PHYSICAL",
      "price": 50.00
    }
  ]
}
```

**Response (Success):**
```json
{
  "success": true,
  "message": "Ödeme sayfası oluşturuldu",
  "threeDSHtmlContent": "<html>...</html>",
  "paymentId": "token-123",
  "conversationId": "conv-456"
}
```

---

### 2. Ödeme Callback (İyzico'dan döner)

**Endpoint:** `POST /api/payment/callback`

**Açıklama:** İyzico, ödeme tamamlandığında bu endpoint'e callback yapar.

**Request Body (Form Data):**
```
status: success
paymentId: 12345
conversationId: conv-456
token: token-123
```

**Response (Success):**
```json
{
  "success": true,
  "message": "Ödeme başarılı",
  "status": "SUCCESS",
  "paidPrice": 100.00,
  "currency": "TRY",
  "paymentId": "12345",
  "conversationId": "conv-456",
  "cardFamily": "Bonus",
  "cardType": "CREDIT_CARD",
  "paymentDate": "2025-11-15T10:30:00Z"
}
```

---

### 3. Conversation ID ile Ödeme Sorgulama

**Endpoint:** `GET /api/payment/conversation/{conversationId}`

**Açıklama:** Conversation ID ile ödeme bilgisini getirir.

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "orderId": 10,
    "paymentId": "12345",
    "conversationId": "conv-456",
    "amount": 100.00,
    "paidPrice": 100.00,
    "currency": "TRY",
    "status": "Success",
    "cardFamily": "Bonus",
    "cardType": "CREDIT_CARD"
  }
}
```

---

### 4. Payment ID ile Ödeme Sorgulama

**Endpoint:** `GET /api/payment/payment/{paymentId}`

---

### 5. Order ID'ye Göre Ödemeleri Getirme

**Endpoint:** `GET /api/payment/order/{orderId}`

---

## 🔄 Flutter ile Entegrasyon Akışı

### Adım 1: Flutter'dan Backend'e İstek

```dart
final response = await http.post(
  Uri.parse('https://your-backend.com/api/payment/initiate'),
  headers: {'Content-Type': 'application/json'},
  body: jsonEncode({
    'price': 100.00,
    'paidPrice': 100.00,
    'basketId': 1,
    'callbackUrl': 'https://your-backend.com/api/payment/callback',
    // ... diğer bilgiler
  }),
);

final data = jsonDecode(response.body);
String htmlContent = data['threeDSHtmlContent'];
```

### Adım 2: WebView'da 3D Secure Sayfasını Göster

```dart
import 'package:webview_flutter/webview_flutter.dart';

WebView(
  initialData: InAppWebViewInitialData(
    data: htmlContent,
    mimeType: 'text/html',
    encoding: 'utf-8',
  ),
  onLoadStop: (controller, url) async {
    // Callback URL'ine yönlendirme kontrolü
    if (url.toString().contains('/payment/callback')) {
      // Ödeme tamamlandı
      Navigator.pop(context);
    }
  },
)
```

---

## 🧪 Test Kartları (Sandbox)

İyzico Sandbox ortamında test için kullanabileceğiniz kartlar:

| Kart Numarası        | Son Kullanma | CVC | Sonuç           |
|----------------------|--------------|-----|-----------------|
| 5528790000000008     | 12/30        | 123 | Başarılı        |
| 4603450000000000     | 12/30        | 123 | Başarılı        |
| 5311570000000005     | 12/30        | 123 | 3DS ile Başarılı|

---

## 🔒 Güvenlik Notları

1. **Secret Key'i asla Flutter'da saklamayın!** Tüm ödeme işlemleri backend üzerinden yapılmalıdır.
2. Callback URL'inizin HTTPS olduğundan emin olun.
3. Production'a geçerken `appsettings.json` içindeki `BaseUrl`'i `https://api.iyzipay.com` olarak değiştirin.
4. Production API Key'lerini environment variables'dan okuyun.

---

## 📊 Veritabanı Yapısı

### Payment Tablosu

| Sütun              | Tip       | Açıklama                     |
|--------------------|-----------|------------------------------|
| Id                 | int       | Primary Key                  |
| OrderId            | int       | Foreign Key (Order tablosu)  |
| PaymentId          | string    | İyzico Payment ID            |
| ConversationId     | string    | Benzersiz işlem takip ID'si |
| Amount             | decimal   | Ödeme tutarı                 |
| PaidPrice          | decimal   | Ödenen tutar                 |
| Currency           | string    | Para birimi (TRY, USD)       |
| Status             | string    | Pending, Success, Failed     |
| CardFamily         | string    | Kart ailesi (Bonus, Axess)   |
| CardType           | string    | CREDIT_CARD, DEBIT_CARD      |
| PaymentDate        | DateTime  | Ödeme tarihi                 |

---

## 🛠️ Sorun Giderme

### Hata: "The type or namespace name 'Iyzipay' could not be found"

**Çözüm:** Business projesine Iyzipay paketini ekleyin:
```bash
cd Business
dotnet add package Iyzipay
```

### Hata: "Invalid conversation id"

**Çözüm:** Her ödeme için yeni bir ConversationId oluşturulduğundan emin olun (GUID kullanılıyor).

### Hata: "Payment not found"

**Çözüm:** Veritabanı migration'ının çalıştırıldığından emin olun:
```bash
dotnet ef database update --project .\DataAccessLayer\DataAccessLayer.csproj --startup-project .\API\API.csproj
```

---

## 📞 Destek

İyzico Sandbox Dokümantasyonu: https://dev.iyzipay.com/tr

---

**Not:** Bu entegrasyon, backend'de güvenli bir şekilde ödeme işlemlerini yönetir. Flutter uygulamanız sadece API endpoint'lerini kullanarak ödeme işlemlerini başlatır ve WebView içinde 3D Secure doğrulamasını gösterir.
