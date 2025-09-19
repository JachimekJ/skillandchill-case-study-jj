# skillandchill-case-study-jj
# Portal B2B dla Dystrybutorów – Case Study SKILL & CHILL

## 📌 Cel projektu
Celem projektu było stworzenie portalu B2B dla dystrybutorów firmy kosmetycznej. Portal umożliwia:
- logowanie i autoryzację użytkowników,
- raportowanie sprzedaży,
- monitorowanie zakupów i punktów sprzedaży,
- dostęp do materiałów marketingowych i produktowych,
- (docelowo) zarządzanie użytkownikami i logami.

Projekt został wykonany jako **MVP** w ramach case study rekrutacyjnego.

---

## 📖 Dokumentacja
Pełna dokumentacja znajduje się w katalogu **/docs**:
- `requirements.md` – wymagania biznesowe i techniczne
- `design.md` – projekt architektury i modułów
- `backend.md` – opis backendu
- `frontend.md` – opis frontend
- `api.md` – specyfikacja API

---

## 🏗️ Architektura
- **Backend:** ASP.NET Core (.NET 9, Minimal API), Entity Framework Core, SQLite (MVP), JWT.
- **Frontend:** HTML5, CSS3 (RWD, desktop-first), JavaScript (fetch API, Chart.js).
- **Komunikacja:** REST/JSON, HTTPS (docelowo).

---

## ✅ Funkcje zrealizowane w MVP

### 🔐 Logowanie i autoryzacja
- JWT + role (`Employee`, `Distributor`, `ExportManager`, `Admin`, `SuperAdmin`)
- Blokada po 3 nieudanych próbach
- Wymuszona zmiana hasła przy pierwszym logowaniu
- Hierarchiczne odblokowywanie kont

### 📊 Sales Channels
- Formularz kwartalny (Professional, Pharmacy, B2C, B2B, Third Party, Other)
- Auto-sumowanie i wyliczenia (Total, EUR Total – uproszczony kurs)
- Historia sprzedaży w tabeli
- API: `GET /api/sales`, `POST /api/sales`

### 🛒 Purchase Report
- Formularz kwartalny (Last Year, Purchases, Budget, POS, Openings)
- Auto-powiązanie z Sales (Actual Sales)
- Automatyczne obliczenia (vs Last Year, vs Budget)
- Historia raportów + dashboard z wykresem
- API: `GET /api/purchase`, `POST /api/purchase`

### 📂 Media
- Lista plików z repozytorium
- Wyszukiwarka po SKU
- Pobieranie plików
- API: `GET /api/media`, `GET /api/media/search`

---

## 🚧 Elementy do rozwinięcia (Next Steps)

- **Eksport CSV** (Sales, Purchase)  
- **Panel Admina** – zarządzanie użytkownikami, rolami, logami  
- **Sortowanie i multi-download w Media**  
- **Raportowanie sprzedaży po SKU**  
- **Integracja z API NBP** dla kursów walut zamiast stałego mnożnika  
- **Import danych do formularzy (Sales)**  
- **Filtrowanie danych wg kraju/dystrybutora (Admin/Export Manager)**  
- **Obsługa zastępstw Export Managerów**  

---

## 🧪 Spójność dokumentacji z kodem
- Dokumentacja opisuje pełen system zgodny z wymaganiami case study.  
- MVP implementuje najważniejsze moduły, ale część funkcjonalności pozostaje w fazie planów.  
- Repozytorium jest spójne: docs prezentuje **pełną wizję**, a kod – **działający szkielet systemu**.

---

## 🎯 Ocena realizacji case study
- **Case study**: zawiera 3 kluczowe moduły (Sales, Purchase, Media) + logowanie i role:contentReference[oaicite:1]{index=1}.  
- **Repozytorium**: zrealizowało wszystkie trzy moduły + logowanie.  
- **Braki**: eksport CSV, zaawansowany admin panel, część funkcji dodatkowych (sortowanie, import, NBP API).  

📌 **Wniosek:** MVP spełnia wymagania pierwszej fazy projektu, a dokumentacja jasno wskazuje kierunki dalszego rozwoju. Projekt jest spójny i profesjonalnie przygotowany.
