# Backend – Portal B2B dla dystrybutorów

## 1. Technologie
- ASP.NET Core Web API (.NET 9, Minimal API).
- Entity Framework Core (ORM, migracje, SQLite w MVP).
- JWT (JSON Web Token) do autoryzacji.
- Swagger (tylko w środowisku developerskim).

## 2. Struktura projektu (Minimal API)
BackendApp/
├── Endpoints/         -> (opcjonalnie) definicje mapowań endpointów w osobnych plikach
├── Models/            -> Klasy domenowe (User, Sales, Purchase, Media, Log)
├── DTOs/              -> Data Transfer Objects (UserLoginDto, SalesDto, PurchaseDto, MediaDto)
├── Data/              -> DbContext (AppDbContext), konfiguracje EF Core
├── Services/          -> Logika biznesowa (AuthService, SalesService, PurchaseService, MediaService, AdminService)
├── Program.cs         -> Konfiguracja DI, middleware, MapGet/MapPost (Minimal API)
├── appsettings.json   -> Konfiguracja (np. connection string do SQLite)

## 3. Modele danych (wysoki poziom)
User
- Id, Username, PasswordHash, Role, IsLocked, FailedAttempts, MustChangePassword

Sales
- Id, DistributorId, Quarter, Currency,
- Professional, Pharmacy, EcommerceB2C, EcommerceB2B, ThirdParty, Other, Total, NewClients,
- EUR_* (pola przeliczone do EUR wg średniego kursu NBP)

Purchases
- Id, DistributorId, Quarter,
- LastYearSales, Purchases, Budget, ActualSales, YoY, VsBudget,
- TotalPOS, NewOpenings, NewOpeningsTarget

Media
- Id, Path, FileName, Type, Size, UploadedAt, UploadedBy, RoleAccess, SKU (opcjonalnie)

Log
- Id, UserId, Action, Timestamp, Metadata

## 4. DTOs (przykłady)
- UserLoginDto: { Username, Password }
- UserResponseDto: { Id, Username, Role }
- SalesDto: { pola formularza sprzedaży + Total + EUR_* }
- PurchaseDto: { pola formularza zakupów + metryki porównawcze }
- MediaDto: { Id, FileName, Path, Size, UploadedAt }

## 5. Serwisy (zarys)
AuthService
- Login(), ChangePassword(), LockAccount(), UnlockAccount()

SalesService
- AddSales(), GetSalesByDistributor(), ExportSalesCsv()

PurchaseService
- AddPurchase(), GetPurchaseReport(), ExportPurchaseCsv()

MediaService
- GetFiles(), SearchFilesBySku(), DownloadFile()

AdminService
- GetAllUsers(), ManageRoles(), ViewLogs()

## 6. Endpointy (Minimal API – ścieżki)
POST /api/auth/login
POST /api/auth/change-password
POST /api/auth/unlock/{userId}

GET  /api/sales
POST /api/sales
GET  /api/sales/export

GET  /api/purchases
POST /api/purchases
GET  /api/purchases/export

GET  /api/media
GET  /api/media/{id}/download
GET  /api/media/search?sku={sku}

GET  /api/admin/users
POST /api/admin/users
PUT  /api/admin/users/{id}/role
GET  /api/admin/logs

## 7. Baza danych
- ORM: Entity Framework Core
- Provider: SQLite (plik app.db w MVP)
- Migracje: `dotnet ef migrations add InitialCreate`
- Aktualizacja bazy: `dotnet ef database update`

## 8. Bezpieczeństwo
- Hasła: hashowanie (np. bcrypt/argon2).
- JWT: podpisany token, ustawione wygasanie.
- Blokada konta po 3 nieudanych logowaniach.
- Hierarchiczne odblokowanie (rola wyższa → odblokowuje niższą).
- Logi aktywności w tabeli Logs (logowanie, eksporty, akcje admin).

## 9. Priorytety MVP (Backend)
1) Logowanie + role (AuthService, User model, JWT)  
2) Sales (dodawanie/odczyt + eksport CSV)  
3) Purchase Report (dodawanie/odczyt + eksport CSV)  
4) Media (lista + pobieranie; wyszukiwarka po SKU)  
5) Admin (lista userów, zmiana ról, logi)
