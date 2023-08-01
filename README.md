# HotelGuide Projesi

Bu proje, OtelService ve RaporService adında iki mikroservisi içeren bir örnektir. OtelService, otelleri yönetmek ve rapor taleplerini oluşturmak için kullanılan HTTP API'lerini sunar. RaporService ise raporları yönetmek ve rapor durumlarını takip etmek için kullanılır. Projede ASP.NET Core, Entity Framework Core, RabbitMQ gibi teknolojiler kullanılmıştır.Code first yaklaşımı ile database oluşturulmuş olup PostgreSQL db kullanılmıştır.

## Proje Yapısı

Projedeki dosya ve klasör organizasyonu şu şekildedir:

OtelService/
|-- Controllers/
| |-- OtelController.cs
|-- Data/
| |-- Entity/
| | |-- Otel.cs
| | |-- Rapor.cs
| |-- OtelDbContext.cs
|-- Services/

RaporService/
|-- Controllers/
| |-- RaporController.cs
|-- Data/
| |-- Entity/
| | |-- Rapor.cs
| |-- OtelDbContext.cs
|-- Services/


## Gereksinimler

- .NET 5.0 veya üstü sürümü
- RabbitMQ (Distributed Messaging için)

## Kurulum ve Çalıştırma

1. Mikroservisleri klonlayın:
git clone https://github.com/ahmetagdas/mikroservis-projesi.git

İlgili mikroservis klasörüne gidin:
## cd OtelService
Bağımlılıkları yükleyin:
## dotnet restore
OtelService'yi çalıştırın:
## dotnet run
RaporService için de aynı adımları takip edin.

## API Dökümantasyonu
OtelService API
Otelleri Listele
GET /api/otel      -Tüm otelleri listeler.

Otel Detayı
GET /api/otel/{otelId}     -Bir otelin detaylarını gösterir.

Otel Oluştur
POST /api/otel             -Yeni bir otel oluşturur ve bir rapor talebi gönderir.

Otel Güncelle
PUT /api/otel/{otelId}     -Bir otelin bilgilerini günceller.

Otel Sil
DELETE /api/otel/{otelId}  -Bir oteli siler.

Otel Yetkililerini Listele
GET /api/otel/yetkililer   -Otel yetkililerini listeler.

Otel Detayı ve Yetkilileri
GET /api/otel/{otelId}/detay  -Bir otelin detaylarını ve yetkililerini gösterir.

RaporService API
Raporları Listele
GET /api/raporlar                    -Tüm raporları listeler.

Rapor Detayı
GET /api/raporlar/{raporId}          -Bir raporun detaylarını gösterir.

Rapor Oluştur
POST /api/raporlar                   -Yeni bir rapor oluşturur.

Testler
Mikroservislerin testlerini çalıştırmak için:
## cd OtelService.Tests
## dotnet test

## cd RaporService.Tests
## dotnet test

İletişim
E-posta: ahmetagdas.1967@gmail.com


