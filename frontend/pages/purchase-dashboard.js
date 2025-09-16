// purchase-dashboard.js
document.addEventListener("DOMContentLoaded", () => {
  const token = localStorage.getItem("token");
  if (!token) return;

  async function loadChart() {
    try {
      const res = await fetch("http://localhost:5107/api/purchase?distributorId=1", {
        headers: { "Authorization": "Bearer " + token }
      });

      if (!res.ok) {
        console.error("❌ Błąd pobierania danych do wykresu");
        return;
      }

      const data = await res.json();

      // Przygotowanie danych do wykresu
      const labels = data.map(r => r.quarter);
      const actualSales = data.map(r => r.actualSales);
      const budget = data.map(r => r.budget);

      // Tworzenie wykresu
      const ctx = document.getElementById("purchaseChart").getContext("2d");
      new Chart(ctx, {
        type: "bar",
        data: {
          labels: labels,
          datasets: [
            {
              label: "Actual Sales",
              data: actualSales,
              backgroundColor: "rgba(54, 162, 235, 0.7)"
            },
            {
              label: "Budget",
              data: budget,
              backgroundColor: "rgba(255, 99, 132, 0.7)"
            }
          ]
        },
        options: {
          responsive: true,
          plugins: {
            title: {
              display: true,
              text: "Porównanie: Actual Sales vs Budget"
            }
          },
          scales: {
            y: {
              beginAtZero: true
            }
          }
        }
      });
    } catch (err) {
      console.error("❌ Błąd w loadChart:", err);
    }
  }

  loadChart();
});
