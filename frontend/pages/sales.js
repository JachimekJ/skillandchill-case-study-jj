const API_BASE = "http://localhost:5107/api"; // Twój backend

document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("salesForm");
  const tableBody = document.querySelector("#salesTable tbody");
  const message = document.getElementById("salesMessage");

  const token = localStorage.getItem("token");
  if (!token) {
    alert("Brak tokena. Zaloguj się ponownie.");
    window.location.href = "../index.html";
    return;
  }

  // Funkcja do odświeżania tabeli
  async function loadSales() {
    try {
      const res = await fetch(`${API_BASE}/sales?distributorId=1`, {
        headers: { "Authorization": "Bearer " + token }
      });
      if (res.ok) {
        const data = await res.json();
        tableBody.innerHTML = "";
        data.forEach(row => {
          const tr = `
            <tr>
              <td>${row.quarter}</td>
              <td>${row.currency}</td>
              <td>${row.professional}</td>
              <td>${row.pharmacy}</td>
              <td>${row.ecommerceB2C}</td>
              <td>${row.ecommerceB2B}</td>
              <td>${row.thirdParty}</td>
              <td>${row.other}</td>
              <td>${row.newClients}</td>
              <td>${row.total}</td>
              <td>${row.eurTotal}</td>
            </tr>
          `;
          tableBody.insertAdjacentHTML("beforeend", tr);
        });
      }
    } catch (err) {
      console.error("Błąd pobierania sales:", err);
    }
  }

  // Obsługa formularza
  form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const payload = {
      distributorId: 1,
      quarter: document.getElementById("quarter").value,
      currency: document.getElementById("currency").value,
      professional: parseFloat(document.getElementById("professional").value),
      pharmacy: parseFloat(document.getElementById("pharmacy").value),
      ecommerceB2C: parseFloat(document.getElementById("ecommerceB2C").value),
      ecommerceB2B: parseFloat(document.getElementById("ecommerceB2B").value),
      thirdParty: parseFloat(document.getElementById("thirdParty").value),
      other: parseFloat(document.getElementById("other").value),
      newClients: parseInt(document.getElementById("newClients").value)
    };

    try {
      const res = await fetch(`${API_BASE}/sales`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer " + token
        },
        body: JSON.stringify(payload)
      });

      if (res.ok) {
        message.innerText = "✅ Zapisano dane sprzedaży!";
        form.reset();
        loadSales();
      } else {
        message.innerText = "❌ Błąd przy zapisie danych.";
      }
    } catch (err) {
      console.error("Błąd zapisu:", err);
      message.innerText = "❌ Błąd połączenia z serwerem.";
    }
  });

  loadSales();
});
