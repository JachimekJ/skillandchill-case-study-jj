# Projekt systemu – Portal B2B dla dystrybutorów

## 1. Architektura ogólna
System działa w modelu klient–serwer:
- Frontend (przeglądarka): HTML5, CSS3, JavaScript (desktop-first, RWD).
- Backend (API): ASP.NET Core Web API (.NET 9, Minimal API).
- Baza danych: SQLite w MVP (docelowo łatwa migracja do MS SQL / PostgreSQL).
- Repozytorium plików: przechowywanie materiałów (MEDIA) po stronie serwera, dostęp kontrolowany rolami.
- Komunikacja: REST/JSON przez HTTPS.

## 2. Moduły systemu
1. Logowanie i autoryzacja
   - 5 poziomów ról: Pracownik dystrybutora, Dystrybutor, Export Manager, Administrator, Super-administrator.
   - Zmiana hasła przy pierwszym logowaniu, blokady po 3 nieudanych próbach, logi aktywności, hierarchiczne odblokowanie.

2. SALES CHANNELS (sprzedaż)
   - Formularz kwartalny z kanałami sprzedaży, automatyczne sumy, przeliczenia na EUR (średni kurs NBP), eksport CSV.

3. PURCHASE REPORT (zakupy / POS)
   - Formularz kwartalny, dane historyczne i bieżące, automatyczne porównania (YoY, vs budżet), dane POS, eksport CSV.

4. MEDIA (repozytorium plików)
   - Struktura katalogów: /PRODUCTS/<SKU>/..., /MARKETING/<YYYY_MM>/...
   - Wyszukiwanie po SKU, sortowanie, pobieranie pojedyncze i wielokrotne (zgodnie z uprawnieniami).

5. Funkcje administracyjne
   - Zarządzanie użytkownikami/rolami, filtry (kraj, dystrybutor), zastępstwa Export Managerów, logi, eksport danych.

## 3. Technologie
- Backend: ASP.NET Core Web API (.NET 9, Minimal API), Entity Framework Core, AutoMapper (opcjonalnie), Swagger (dev).
- Frontend: HTML5, CSS3 (Grid/Flex), JS (ES6+). (Opcjonalnie: Bootstrap/Tailwind w przyszłości).
- Baza: SQLite (MVP), migracje EF Core; tabele: Users, Sales, Purchases, Media, Logs.
- Narzędzia: Git/GitHub, Postman, AI asystenci (ChatGPT/Copilot) do przyspieszenia implementacji (bezpieczne użycie).

## 4. Diagram architektury
[ Przeglądarka (Frontend) ]
            |
         HTTPS / JSON
            |
[ ASP.NET Core Web API (Backend) ]
       |                  |
       |                  +--> [ Media Storage (pliki) ]
       |
       +----------------------> [ SQLite (EF Core) ]

## 5. Diagram modułów (logiczny)
+------------------------+
|  Logowanie & Role      |
|  (Auth, Audit Logs)    |
+-----------+------------+
            |
            v
  +---------+----------+------------------+
  |                    |                  |
  v                    v                  v
[Sales Channels]   [Purchase Report]   [Media]
  |                    |                  |
  +------ eksport CSV  +------ eksport CSV +--- pobieranie plików
  |
  +--> Dashboard (widok zbiorczy)

## 6. Bezpieczeństwo
- HTTPS w komunikacji; hasła hashowane (MVP: można zacząć od uproszczenia z jasnym opisem w docs).
- 5 ról, kontrola dostępu per endpoint i per widok (RBA – Role-Based Access).
- Blokada po 3 nieudanych logowaniach; hierarchiczne odblokowanie (wyższa rola odblokowuje niższą).
- Logi aktywności (logowanie, próby, eksporty, akcje administracyjne).

## 7. Wymagania niefunkcjonalne
- Użytkownicy: ~80 kont na start (35 dystrybutorów + osoby towarzyszące + zespół firmy).
- Obciążenie: typowo 1–2 użytkowników jednocześnie → brak potrzeby zaawansowanego skalowania w MVP.
- Przeglądarki: Chrome, Firefox, Edge, Safari (najnowsze wersje).
- RWD: desktop-first (mobile wspierany w podstawowym zakresie).
- Migracje: możliwość przejścia z SQLite do MS SQL/PostgreSQL bez zmiany modelu domenowego (EF Core).

## 8. Kluczowe encje (wysoki poziom)
Users:      Id, Username, PasswordHash, Role, IsLocked, FailedAttempts, MustChangePassword
Sales:      Id, DistributorId, Quarter, Currency, Professional, Pharmacy, EcommerceB2C, EcommerceB2B, ThirdParty, Other, Total, NewClients, EUR_*
Purchases:  Id, DistributorId, Quarter, LastYearSales, Purchases, Budget, ActualSales, YoY, VsBudget, TotalPOS, NewOpenings, NewOpeningsTarget
Media:      Id, Path, FileName, Type, Size, UploadedAt, UploadedBy, RoleAccess, SKU (opcjonalnie)
Logs:       Id, UserId, Action, Timestamp, Metadata

## 9. Plan realizacji (zgodnie z case study)
Faza I – Analiza i planowanie
- Analiza wymagań (na bazie dokumentu), projekt architektury (ten plik), przygotowanie modeli danych.

Faza II – Rozwój (MVP)
1) Logowanie i system uprawnień (5 ról, blokady, zmiana hasła przy 1. logowaniu, logi).  
2) Sales Channels (formularz, sumy, EUR NBP, eksport CSV).  
3) Purchase Report (formularz, porównania YoY i vs budżet, eksport CSV).  
4) Media (lista, wyszukiwarka po SKU, sortowanie, pobieranie; upload jako „next step”, jeśli zabraknie czasu).  
5) Integracja komponentów i dashboard.

Faza III – Testy
- Testy modułowe i integracyjne (front–back), weryfikacja ról i dostępu, poprawki UX.

Faza IV – Dokumentacja i prezentacja
- Uzupełnienie docs/, finalny README (uruchomienie), zrzuty ekranu z MVP.
