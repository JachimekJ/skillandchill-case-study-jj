# Frontend – Portal B2B dla dystrybutorów

## 1. Technologie
- HTML5 – struktura strony.
- CSS3 (Flexbox, Grid) – layout, responsywność (desktop-first).
- JavaScript (ES6+) – logika, walidacje, komunikacja z backendem (fetch).
- (Opcjonalnie) framework CSS: Bootstrap/Tailwind.

## 2. Struktura projektu
frontend/
├── index.html          -> Strona główna / logowanie
├── style.css           -> Style globalne
├── app.js              -> Logika aplikacji, komunikacja API
├── assets/             -> Zasoby (grafiki/ikony)
│   └── .gitkeep
├── components/         -> Komponenty HTML
│   └── login-form.html
├── pages/              -> Podstrony
│   ├── dashboard.html
│   └── admin.html

## 3. Główne widoki
- index.html – ekran logowania.
- dashboard.html – dashboard użytkownika (Sales, Purchase, Media).
- admin.html – panel administratora (użytkownicy, logi).

## 4. Komponenty
login-form.html
- Formularz logowania (username, password).
- Walidacja (JS).
- Komunikaty: błąd logowania / konto zablokowane.

Dashboard (dashboard.html)
- Sekcja Sales – formularz, tabela, eksport CSV.
- Sekcja Purchases – formularz, tabela, eksport CSV.
- Sekcja Media – lista plików, wyszukiwarka po SKU, pobieranie.

Admin Panel (admin.html)
- Lista użytkowników.
- Zmiana roli.
- Podgląd logów.

## 5. Komunikacja z API
- fetch() do backendu (REST, JSON).
- JWT w localStorage / sessionStorage.
- Nagłówek Authorization: `Bearer <token>`.

## 6. Responsywność
- Desktop-first (grid + flex).
- Mobile: uproszczony widok jednokolumnowy (menu jako burger).

## 7. Priorytety MVP (Frontend)
1) Logowanie (formularz, JWT, przekierowanie do dashboardu)  
2) Dashboard – sekcje Sales i Purchase (formularze + listy)  
3) Media – lista i pobieranie (wyszukiwarka po SKU)  
4) Admin – minimalny widok listy użytkowników
