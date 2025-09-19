# Backend – Portal B2B dla dystrybutorów

## 1. Technologie
- ASP.NET Core Web API (.NET 9, Minimal API).
- Entity Framework Core (ORM, migracje, SQLite w MVP).
- JWT (JSON Web Token) do autoryzacji.
- Swagger (tylko w środowisku developerskim).

2. Struktura projektu (Minimal API)

```text
BackendApp/
├── Endpoints/        -> (opcjonalnie) definicje mapowań endpointów w osobnych plikach
├── Models/           -> Klasy domenowe (User, Sales, Purchase, Media, Log)
├── DTOs/             -> Data Transfer Objects (UserLoginDto, SalesDto, PurchaseDto, MediaDto)
├── Data/             -> DbContext (AppDbContext), konfiguracje EF Core
├── Services/         -> Logika biznesowa (AuthService, SalesService, PurchaseService, MediaService, AdminService)
├── Program.cs        -> Konfiguracja DI, middleware, MapGet/MapPost (Minimal API)
├── appsettings.json  -> Konfiguracja (np. connection string do SQLite)

## 3. Modele danych (Database Models)
### Users
- Id (PK)  
- Username  
- Email  
- PasswordHash  
- RoleId (FK → Roles.Id)  
- IsLocked (bool)  
- CreatedAt (datetime)  
- UpdatedAt (datetime)  

### Roles
- Id (PK)  
- Name — ["Employee", "Distributor", "ExportManager", "Admin", "SuperAdmin"]  
- Description  

### LoginAttempts
- Id (PK)  
- UserId (FK → Users.Id)  
- AttemptTime (datetime)  
- IsSuccessful (bool)  

### Distributors
- Id (PK)  
- UserId (FK → Users.Id)  
- CompanyName  
- Country  
- ContactPerson  
- Phone  

### SalesReports
- Id (PK)  
- DistributorId (FK → Distributors.Id)  
- Quarter (string, np. "Q1-2025")  
- ProfessionalSales  
- PharmacySales  
- EcommerceB2C  
- EcommerceB2B  
- ThirdParty  
- Other  
- TotalSales (calculated)  
- NewClients  
- TotalSalesEUR (calculated)  
- CreatedAt (datetime)  

### PurchaseReports
- Id (PK)  
- DistributorId (FK → Distributors.Id)  
- Quarter (string)  
- LastYearSales  
- Purchases  
- Budget  
- ActualSales (from SalesReports.TotalSales)  
- TotalVsLastYear (calculated)  
- TotalVsBudget (calculated)  
- TotalPOS  
- NewOpenings  
- NewOpeningsTarget  
- CreatedAt (datetime)  

### MediaFiles
- Id (PK)  
- FileName  
- FilePath  
- FileType  
- FileSize  
- SKU  
- Category (PRODUCTS / MARKETING)  
- UploadedAt (datetime)  
- UploadedBy (FK → Users.Id)  

### ActivityLogs
- Id (PK)  
- UserId (FK → Users.Id)  
- Action (string)  
- Timestamp (datetime)  
- IPAddress  
- Details (json / text)  

---

### ERD (simplified ASCII diagram)

+-------------+        +-----------+
|   Roles     |1------<|   Users   |
|-------------|        |-----------|
| Id (PK)     |        | Id (PK)   |
| Name        |        | Username  |
| Description |        | Email     |
+-------------+        | Password  |
                       | RoleId FK |
                       | IsLocked  |
                       +-----------+
                             |
                             |1
                             | 
                             v
                       +-----------+
                       |LoginAttempts|
                       |-------------|
                       | Id (PK)     |
                       | UserId FK   |
                       | AttemptTime |
                       | IsSuccessful|
                       +-------------+

+----------------+        +-------------------+
|  Distributors  |1------<|   SalesReports    |
|----------------|        |-------------------|
| Id (PK)        |        | Id (PK)           |
| UserId (FK)    |        | DistributorId (FK)|
| CompanyName    |        | Quarter           |
| Country        |        | Sales fields...   |
+----------------+        +-------------------+
       |
       |1
       v
+-------------------+
| PurchaseReports   |
|-------------------|
| Id (PK)           |
| DistributorId (FK)|
| Quarter           |
| Purchases fields… |
+-------------------+

+------------+        +-------------+
|   Users    |1------<| MediaFiles  |
|------------|        |-------------|
| Id (PK)    |        | Id (PK)     |
| ...        |        | FileName    |
+------------+        | FilePath    |
                      | SKU         |
                      | UploadedByFK|
                      +-------------+

+------------+        +----------------+
|   Users    |1------<|  ActivityLogs  |
|------------|        |----------------|
| Id (PK)    |        | Id (PK)        |
| ...        |        | UserId (FK)    |
+------------+        | Action         |
                      | Timestamp      |
                      +----------------+

---

## 4. DTOs (przykłady)
- UserLoginDto: { Username, Password }
- UserResponseDto: { Id, Username, Role }
- SalesReportDto: { pola formularza sprzedaży + Total + EUR_* }
- PurchaseReportDto: { pola formularza zakupów + metryki porównawcze }
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

GET  /api/purchase
POST /api/purchase
GET  /api/purchase/export

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

