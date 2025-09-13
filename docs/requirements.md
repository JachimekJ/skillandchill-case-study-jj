# Requirements – Portal B2B dla dystrybutorów

## 1. Cel projektu
Wdrożenie portalu internetowego dla dystrybutorów firmy kosmetycznej.  
Portal umożliwi:
- logowanie i autoryzację użytkowników,
- raportowanie wyników sprzedaży,
- monitorowanie zakupów i punktów sprzedaży,
- dostęp do materiałów marketingowych i produktowych,
- zarządzanie użytkownikami oraz logami aktywności.

---

## 2. Zakres prac
- Analiza wymagań biznesowych i technicznych.
- Projekt i wdrożenie portalu B2B.
- Przygotowanie modeli danych i architektury systemu.
- Implementacja backendu (ASP.NET Core Web API).
- Implementacja frontend (HTML5, CSS3, JS).
- Integracja z bazą danych (SQLite – MVP).
- Dokumentacja techniczna.

---

## 3. Wymagania funkcjonalne
1. Moduł logowania i autoryzacji z różnymi poziomami dostępu.
2. Raportowanie sprzedaży (dashboard + eksport danych).
3. Repozytorium plików (materiały marketingowe, dokumenty).
4. System uprawnień (role i dostęp do modułów).
5. Panel administracyjny z zarządzaniem użytkownikami i logami.

---

## 4. Wymagania niefunkcjonalne
- System dla ~80 użytkowników (35 dystrybutorów + osoby dodatkowe + zespół firmy).
- Typowe obciążenie: 1–2 jednoczesnych użytkowników.
- Responsywny design (desktop-first, wsparcie mobile).
- Kompatybilność z Chrome, Firefox, Safari, Edge.
- Szyfrowanie danych (HTTPS).
- Bezpieczne przechowywanie haseł (hashing, np. bcrypt).
- Logowanie aktywności użytkowników.
- Możliwość eksportu danych do CSV (UTF-8).
- Intuicyjny dashboard i prosty UX.

---

## 5. Role użytkowników i poziomy uprawnień
1. **Pracownicy dystrybutora** – ograniczony dostęp, brak wglądu w umowy i szczegóły sprzedaży.  
2. **Dystrybutorzy** – dostęp tylko do własnych danych i formularzy.  
3. **Export Managerowie** – dostęp do przypisanych dystrybutorów, filtrowanie danych, ustalanie celów.  
4. **Administratorzy** – pełny dostęp do danych wszystkich dystrybutorów.  
5. **Super-administrator** – wszystkie uprawnienia + zarządzanie kontami i logami.

---

## 6. Funkcje bezpieczeństwa
- Początkowe dane logowania dostarczane mailem, wymuszona zmiana hasła przy pierwszym logowaniu.
- Hasło: min. 8 znaków, 1 znak specjalny, 1 cyfra.
- Automatyczne blokowanie konta po 3 nieudanych próbach logowania.
- Hierarchiczny system odblokowywania kont (wyższy poziom → niższy).
- HTTPS dla wszystkich połączeń.
- Logi aktywności użytkowników (audyt).

---

## 7. Moduły priorytetowe (Faza I)
1. **SALES CHANNELS** – raportowanie wyników sprzedaży.  
2. **PURCHASE REPORT** – raportowanie zakupów i punktów sprzedaży.  
3. **MEDIA** – repozytorium plików marketingowych.  
+ **Logowanie i autoryzacja** jako moduł bazowy.

---

## 8. Format danych i eksport
- CSV (UTF-8) dla raportów sprzedaży i zakupów.
- Kolumny zgodnie ze specyfikacją (np. Sales_Channels_Professional_Sales, Purchase_Report_Budget).
- Konwencje nazw plików w module MEDIA:
  - `SKU123_main_1.jpg`, `SKU123_manual.pdf`.
  - `campaignName_SKU123_SKU222.jpg`.
  - `SKU123_ingredients.pdf`.

---

## 9. Zakres MVP
- Logowanie + autoryzacja (JWT).
- Formularz sprzedaży (Sales).
- Formularz zakupów (Purchase).
- Podstawowe repozytorium plików (Media).
- Prosty panel admina (zarządzanie użytkownikami).
- Eksport CSV.
