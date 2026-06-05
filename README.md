# MakinaTeknik — Porto MVC Oto Servis Sitesi

ASP.NET Core MVC ile geliştirilmiş, Porto HTML şablonu üzerine kurulu profesyonel oto servis web sitesi.

## Teknolojiler

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core (Code First)
- SQL Server LocalDB
- Porto HTML Şablonu
- AdminLTE Admin Paneli
- MailKit (E-posta)
- Cookie Authentication

## Özellikler

- **Ön Yüz:** Anasayfa, Hakkımızda, Ürünler, İletişim, Randevu sayfaları
- **Admin Panel:** Ürün ve Kategori CRUD, Yönetici yönetimi
- **Randevu Sistemi:** E-posta bildirimi ile randevu formu
- **Dinamik İçerik:** Admin'den eklenen ürünler ön yüzde görünür

## Kurulum

### 1. Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (Visual Studio ile gelir)

### 2. Projeyi Klonla
```bash
git clone https://github.com/Tunahan918/mekaniktamir.git
cd mekaniktamir
```

### 3. appsettings.json Oluştur
`appsettings.example.json` dosyasını kopyalayıp `appsettings.json` olarak kaydet:
```bash
copy appsettings.example.json appsettings.json
```

### 4. Veritabanını Oluştur
```bash
dotnet ef database update
```

### 5. Projeyi Çalıştır
```bash
dotnet run
```

Tarayıcıda `https://localhost:56769` adresine git.

### 6. Admin Paneline Giriş
- URL: `/admin/login`
- Kullanıcı: `admin`
- Şifre: `123456`

## Proje Yapısı

```
├── Areas/Admin/          # Admin panel (controllers + views)
├── Controllers/          # Ana site controller'ları
├── Data/                 # Entity Framework modelleri
├── Views/                # Razor view'ları
├── wwwroot/              # Statik dosyalar (CSS, JS, görseller)
└── Migrations/           # EF Core migration'ları
```
