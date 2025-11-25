# E-Commerce API

Bu proje, .NET 8 kullanılarak geliştirilmiş, N-Katmanlı mimariye (N-Layer Architecture) sahip kapsamlı bir E-Ticaret RESTful API projesidir. İçerisinde ürün yönetimi, sepet işlemleri, sipariş takibi, kullanıcı yetkilendirme (JWT) ve Iyzico ödeme entegrasyonu bulunmaktadır.

## 🚀 Teknolojiler ve Araçlar

*   **.NET 8.0** - Ana Framework
*   **Entity Framework Core** - ORM Aracı
*   **PostgreSQL** - Veritabanı (Npgsql provider ile)
*   **JWT (JSON Web Token)** - Kimlik Doğrulama ve Yetkilendirme
*   **Iyzico** - Ödeme Sistemi Entegrasyonu
*   **Swagger/OpenAPI** - API Dokümantasyonu
*   **Docker** - Konteynerizasyon

## ⚙️ Kurulum ve Çalıştırma

### Gereksinimler
*   .NET 8.0 SDK
*   PostgreSQL Veritabanı
*   Docker (Opsiyonel)

### 1. Projeyi Klonlayın
```bash
git clone https://github.com/kullaniciadi/proje-adi.git
cd ECommerce/API
```

### 2. Yapılandırma (appsettings.json)
`API/appsettings.json` dosyasındaki veritabanı bağlantı dizesini (Connection String) kendi ortamınıza göre düzenleyin:

```json
"ConnectionStrings": {
  "RenderConnection": "Host=localhost;Database=ECommerceDb;Username=postgres;Password=sifreniz"
}
```

### 3. Veritabanını Oluşturma
Migration'ları uygulayarak veritabanını oluşturun:
```bash
dotnet ef database update --project ../DataAccessLayer --startup-project .
```

### 4. Uygulamayı Başlatma
```bash
dotnet run
```
Uygulama varsayılan olarak `http://localhost:5000` veya `https://localhost:5001` adresinde çalışacaktır. Swagger arayüzüne `/swagger` adresinden erişebilirsiniz.

---

## 📡 API Endpoint'leri

Aşağıda API tarafından sunulan temel servisler listelenmiştir.

### 🔐 Kimlik Doğrulama (Auth)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| POST | `/api/Auth/GetToken` | Kullanıcı girişi yapar ve JWT Token döner. |
| POST | `/api/Auth/Logout` | (Authorize) Çıkış işlemi yapar. |

### 📦 Ürünler (Product)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Product` | Tüm ürünleri listeler. |
| GET | `/api/Product/{id}` | ID'ye göre tek bir ürün getirir. |
| POST | `/api/Product` | Yeni ürün ekler. |
| PUT | `/api/Product/{id}` | Mevcut ürünü günceller. |
| DELETE | `/api/Product/{id}` | Ürünü siler. |

### 📂 Kategoriler (Category)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Category` | Tüm kategorileri listeler. |
| GET | `/api/Category/{id}` | ID'ye göre kategori getirir. |
| POST | `/api/Category` | Yeni kategori ekler. |
| PUT | `/api/Category/{id}` | Kategoriyi günceller. |
| DELETE | `/api/Category/{id}` | Kategoriyi siler. |

### 🛒 Sepet İşlemleri (Basket)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Basket` | Tüm sepetleri listeler (Admin). |
| GET | `/api/Basket/{id}` | ID'ye göre sepet detayını getirir. |
| POST | `/api/Basket` | Sepete ürün ekler/yeni sepet oluşturur. |
| PUT | `/api/Basket/{id}` | Sepeti günceller. |
| PUT | `/api/Basket/user/{kullaniciId}` | Kullanıcı ID'sine göre sepeti günceller. |
| DELETE | `/api/Basket/{id}` | Sepeti siler. |

### 📦 Siparişler (Order)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Order` | Tüm siparişleri listeler. |
| GET | `/api/Order/{id}` | ID'ye göre sipariş detayını getirir. |
| GET | `/api/Order/user/{kullaniciId}` | Belirli bir kullanıcının siparişlerini getirir. |

### 💳 Ödeme (Payment - Iyzico)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| POST | `/api/Payment/initiate` | Ödeme işlemini başlatır ve 3D Secure HTML içeriğini döner. |
| POST | `/api/Payment/callback` | Iyzico'dan dönen ödeme sonucunu işler. |
| GET | `/api/Payment/conversation/{id}` | Conversation ID ile ödeme durumunu sorgular. |

### 👤 Kullanıcılar (Kullanici)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Kullanici` | Tüm kullanıcıları listeler. |
| GET | `/api/Kullanici/{id}` | ID'ye göre kullanıcı getirir. |
| POST | `/api/Kullanici` | Yeni kullanıcı oluşturur. |
| PUT | `/api/Kullanici/{id}` | Kullanıcı bilgilerini günceller. |
| DELETE | `/api/Kullanici/{id}` | Kullanıcıyı siler. |

---

## 🛡️ Güvenlik ve Roller

API, **JWT (JSON Web Token)** tabanlı kimlik doğrulama kullanır. `Program.cs` içerisinde tanımlanan şu politikalar (Policies) mevcuttur:

*   **EmployeePolicy**: `Employee` rolüne sahip kullanıcılar.
*   **CourierPolicy**: `Courier` rolüne sahip kullanıcılar.
*   **CustomerPolicy**: `Customer` rolüne sahip kullanıcılar.

Korumalı endpoint'lere istek atarken Header kısmına `Authorization: Bearer <TOKEN>` eklenmelidir.

## 🐳 Docker ile Çalıştırma

Proje kök dizininde `Dockerfile` bulunmaktadır. Aşağıdaki komutlarla konteyner ayağa kaldırılabilir:

```bash
# Image oluşturma
docker build -t ecommerce-api .

# Konteyneri çalıştırma
docker run -d -p # E-Commerce API

Bu proje, .NET 8 kullanılarak geliştirilmiş, N-Katmanlı mimariye (N-Layer Architecture) sahip kapsamlı bir E-Ticaret RESTful API projesidir. İçerisinde ürün yönetimi, sepet işlemleri, sipariş takibi, kullanıcı yetkilendirme (JWT) ve Iyzico ödeme entegrasyonu bulunmaktadır.

## 🚀 Teknolojiler ve Araçlar

*   **.NET 8.0** - Ana Framework
*   **Entity Framework Core** - ORM Aracı
*   **PostgreSQL** - Veritabanı (Npgsql provider ile)
*   **JWT (JSON Web Token)** - Kimlik Doğrulama ve Yetkilendirme
*   **Iyzico** - Ödeme Sistemi Entegrasyonu
*   **Swagger/OpenAPI** - API Dokümantasyonu
*   **Docker** - Konteynerizasyon

## ⚙️ Kurulum ve Çalıştırma

### Gereksinimler
*   .NET 8.0 SDK
*   PostgreSQL Veritabanı
*   Docker (Opsiyonel)

### 1. Projeyi Klonlayın
```bash
git clone https://github.com/kullaniciadi/proje-adi.git
cd ECommerce/API
```

### 2. Yapılandırma (appsettings.json)
`API/appsettings.json` dosyasındaki veritabanı bağlantı dizesini (Connection String) kendi ortamınıza göre düzenleyin:

```json
"ConnectionStrings": {
  "RenderConnection": "Host=localhost;Database=ECommerceDb;Username=postgres;Password=sifreniz"
}
```

### 3. Veritabanını Oluşturma
Migration'ları uygulayarak veritabanını oluşturun:
```bash
dotnet ef database update --project ../DataAccessLayer --startup-project .
```

### 4. Uygulamayı Başlatma
```bash
dotnet run
```
Uygulama varsayılan olarak `http://localhost:5000` veya `https://localhost:5001` adresinde çalışacaktır. Swagger arayüzüne `/swagger` adresinden erişebilirsiniz.

---

## 📡 API Endpoint'leri

Aşağıda API tarafından sunulan temel servisler listelenmiştir.

### 🔐 Kimlik Doğrulama (Auth)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| POST | `/api/Auth/GetToken` | Kullanıcı girişi yapar ve JWT Token döner. |
| POST | `/api/Auth/Logout` | (Authorize) Çıkış işlemi yapar. |

### 📦 Ürünler (Product)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Product` | Tüm ürünleri listeler. |
| GET | `/api/Product/{id}` | ID'ye göre tek bir ürün getirir. |
| POST | `/api/Product` | Yeni ürün ekler. |
| PUT | `/api/Product/{id}` | Mevcut ürünü günceller. |
| DELETE | `/api/Product/{id}` | Ürünü siler. |

### 📂 Kategoriler (Category)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Category` | Tüm kategorileri listeler. |
| GET | `/api/Category/{id}` | ID'ye göre kategori getirir. |
| POST | `/api/Category` | Yeni kategori ekler. |
| PUT | `/api/Category/{id}` | Kategoriyi günceller. |
| DELETE | `/api/Category/{id}` | Kategoriyi siler. |

### 🛒 Sepet İşlemleri (Basket)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Basket` | Tüm sepetleri listeler (Admin). |
| GET | `/api/Basket/{id}` | ID'ye göre sepet detayını getirir. |
| POST | `/api/Basket` | Sepete ürün ekler/yeni sepet oluşturur. |
| PUT | `/api/Basket/{id}` | Sepeti günceller. |
| PUT | `/api/Basket/user/{kullaniciId}` | Kullanıcı ID'sine göre sepeti günceller. |
| DELETE | `/api/Basket/{id}` | Sepeti siler. |

### 📦 Siparişler (Order)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Order` | Tüm siparişleri listeler. |
| GET | `/api/Order/{id}` | ID'ye göre sipariş detayını getirir. |
| GET | `/api/Order/user/{kullaniciId}` | Belirli bir kullanıcının siparişlerini getirir. |

### 💳 Ödeme (Payment - Iyzico)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| POST | `/api/Payment/initiate` | Ödeme işlemini başlatır ve 3D Secure HTML içeriğini döner. |
| POST | `/api/Payment/callback` | Iyzico'dan dönen ödeme sonucunu işler. |
| GET | `/api/Payment/conversation/{id}` | Conversation ID ile ödeme durumunu sorgular. |

### 👤 Kullanıcılar (Kullanici)
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Kullanici` | Tüm kullanıcıları listeler. |
| GET | `/api/Kullanici/{id}` | ID'ye göre kullanıcı getirir. |
| POST | `/api/Kullanici` | Yeni kullanıcı oluşturur. |
| PUT | `/api/Kullanici/{id}` | Kullanıcı bilgilerini günceller. |
| DELETE | `/api/Kullanici/{id}` | Kullanıcıyı siler. |

---

## 🛡️ Güvenlik ve Roller

API, **JWT (JSON Web Token)** tabanlı kimlik doğrulama kullanır. `Program.cs` içerisinde tanımlanan şu politikalar (Policies) mevcuttur:

*   **EmployeePolicy**: `Employee` rolüne sahip kullanıcılar.
*   **CourierPolicy**: `Courier` rolüne sahip kullanıcılar.
*   **CustomerPolicy**: `Customer` rolüne sahip kullanıcılar.

Korumalı endpoint'lere istek atarken Header kısmına `Authorization: Bearer <TOKEN>` eklenmelidir.

## 🐳 Docker ile Çalıştırma

Proje kök dizininde `Dockerfile` bulunmaktadır. Aşağıdaki komutlarla konteyner ayağa kaldırılabilir:

```bash
# Image oluşturma
docker build -t ecommerce-api .

# Konteyneri çalıştırma
docker run -d -p 
