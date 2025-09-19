# Frontend – Portal B2B dla dystrybutorów

## 1. Technologie
- **HTML5** – struktura strony.
- **CSS3 (Flexbox, Grid)** – layout, responsywność (desktop-first).
- **JavaScript (ES6+)** – logika, walidacje, komunikacja z backendem (fetch API).
- **(Opcjonalnie)** framework CSS: Bootstrap / Tailwind dla szybszego prototypowania.

---

## 2. Struktura projektu

```text
frontend/
├── index.html            -> Strona logowania
├── style.css             -> Style globalne
├── app.js                -> Logika aplikacji, komunikacja z API
├── assets/               -> Zasoby (grafiki/ikony)
│   └── .gitkeep
├── components/           -> Komponenty HTML (formularze, listy)
│   ├── login-form.html
│   ├── sales-form.html
│   ├── purchase-form.html
│   └── media-list.html
├── pages/                -> Podstrony
│   ├── dashboard.html
│   ├── sales.html
│   ├── purchase.html
│   ├── media.html
│   └── admin.html
```

---

## 3. Główne widoki
- **index.html** – ekran logowania.
- **dashboard.html** – dashboard użytkownika po zalogowaniu (przekierowanie do modułów).
- **sales.html** – moduł Sales Reports (Sales Channels) – formularz, tabela, eksport.
- **purchase.html** – moduł Purchase Reports – formularz, tabela, dashboard porównań.
- **media.html** – repozytorium plików (lista, wyszukiwarka, pobieranie).
- **admin.html** – panel administratora (użytkownicy, role, logi).

---

## 4. Komponenty
**login-form.html**
- Formularz logowania (username, password).
- Walidacja w JS.
- Obsługa komunikatów: błąd logowania / konto zablokowane.

**sales-form.html**
- Formularz kwartalny (kanały sprzedaży: Professional, Pharmacy, Ecommerce B2C/B2B, Third Party, Other).
- Auto-sumowanie (Total).
- Przeliczenia na EUR wg kursu NBP.
- Pole: New Clients.

**purchase-form.html**
- Formularz kwartalny (Last Year Sales, Purchases, Budget, Actual Sales).
- Auto-wyliczenia: YoY, vs Budget.
- Sekcja POS (Total POS, New openings, New openings target).

**media-list.html**
- Lista plików (Products, Marketing).
- Wyszukiwarka po SKU.
- Sortowanie (data, typ, rozmiar).
- Pobieranie plików.

**admin panel (admin.html)**
- Lista użytkowników.
- Zmiana ról.
- Filtry (np. kraj, dystrybutor).
- Podgląd logów aktywności.

---

## 5. Komunikacja z API
- `fetch()` (REST, JSON).
- JWT przechowywane w `localStorage` lub `sessionStorage`.
- Autoryzacja: nagłówek `Authorization: Bearer <token>`.
- Obsługa błędów (np. brak dostępu → redirect do logowania).

---

## 6. Responsywność
- Desktop-first (Grid + Flexbox).
- Mobile: widok jednokolumnowy, menu typu burger.
- Większość użytkowników → desktop, mobile pomocniczo.

---

## 7. Priorytety MVP (Frontend)
1. **Logowanie** – formularz + walidacja, JWT, redirect do dashboardu.  
2. **Sales** – formularz i tabela z kanałami sprzedaży.  
3. **Purchase Report** – formularz i dashboard porównań.  
4. **Media** – lista plików, wyszukiwarka po SKU, pobieranie.  
5. **Admin** – minimalny widok użytkowników + logi.  