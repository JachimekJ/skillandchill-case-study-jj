const API_BASE = "http://localhost:5107/api";

document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("purchaseForm");
  const tableBody = document.querySelector("#purchaseTable tbody");
  const message = document.getElementById("purchaseMessage");

  const token = localStorage.getItem("token");
  if (!token) {
    alert("Brak tokena. Zaloguj się ponownie.");
    window.location.href = "../index.html";
    return;
  }

  async function loadReports() {
    try {
      const res = await fetch(`${API_BASE}/purchase?distributorId=1`, {
        headers: { "Authorization": "Bearer " + token }
      });
      if (res.ok) {
        const data = await res.json();
        tableBody.innerHTML = "";
        data.forEach(row => {
          const tr = `
            <tr>
              <td>${row.quarter}</td>
              <td>${row.lastYearSales}</td>
              <td>${row.purchases}</td>
              <td>${row.budget}</td>
              <td>${row.actualSales}</td>
              <td>${row.yearVsLastYear}</td>
              <td>${row.yearVsBudget}</td>
              <td>${row.totalPOS}</td>
              <td>${row.newOpenings}</td>
              <td>${row.openingsTarget}</td>
            </tr>
          `;
          tableBody.insertAdjacentHTML("beforeend", tr);
        });
      }
    } catch (err) {
      console.error("Błąd pobierania purchase:", err);
    }
  }

  form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const payload = {
      distributorId: 1,
      quarter: document.getElementById("quarter").value,
      lastYearSales: parseFloat(document.getElementById("lastYearSales").value),
      purchases: parseFloat(document.getElementById("purchases").value),
      budget: parseFloat(document.getElementById("budget").value),
      actualSales: parseFloat(document.getElementById("actualSales").value),
      totalPOS: parseInt(document.getElementById("totalPOS").value),
      newOpenings: parseInt(document.getElementById("newOpenings").value),
      openingsTarget: parseInt(document.getElementById("openingsTarget").value)
    };

    try {
      const res = await fetch(`${API_BASE}/purchase`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer " + token
        },
        body: JSON.stringify(payload)
      });

      if (res.ok) {
        message.innerText = "✅ Zapisano raport zakupowy!";
        form.reset();
        loadReports();
      } else {
        message.innerText = "❌ Błąd przy zapisie raportu.";
      }
    } catch (err) {
      console.error("Błąd zapisu:", err);
      message.innerText = "❌ Błąd połączenia z serwerem.";
    }
  });

  loadReports();
});
