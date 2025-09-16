const API_BASE = "http://localhost:5107/api";

document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("mediaSearchForm");
  const tableBody = document.querySelector("#mediaTable tbody");
  const message = document.getElementById("mediaMessage");

  const token = localStorage.getItem("token");
  if (!token) {
    alert("Brak tokena. Zaloguj się ponownie.");
    window.location.href = "../index.html";
    return;
  }

  async function loadMedia(sku = "") {
    let url = `${API_BASE}/media`;
    if (sku) {
      url = `${API_BASE}/media/search?sku=${encodeURIComponent(sku)}`;
    }

    try {
      const res = await fetch(url, {
        headers: { "Authorization": "Bearer " + token }
      });

      if (res.ok) {
        const data = await res.json();
        tableBody.innerHTML = "";

        if (data.length === 0) {
          message.innerText = "❌ Brak plików dla podanego SKU.";
          return;
        }

        message.innerText = "";
        data.forEach(file => {
          const tr = `
            <tr>
              <td>${file.name}</td>
              <td>${file.type}</td>
              <td>${file.size}</td>
              <td>${new Date(file.date).toLocaleDateString()}</td>
              <td><a href="${file.url}" target="_blank">Pobierz</a></td>
            </tr>
          `;
          tableBody.insertAdjacentHTML("beforeend", tr);
        });
      } else {
        message.innerText = "❌ Błąd pobierania plików.";
      }
    } catch (err) {
      console.error("Błąd media:", err);
      message.innerText = "❌ Błąd połączenia z serwerem.";
    }
  }

  // obsługa formularza
  form.addEventListener("submit", (e) => {
    e.preventDefault();
    const sku = document.getElementById("sku").value.trim();
    loadMedia(sku);
  });

  // ładowanie wszystkich plików przy starcie
  loadMedia();
});
