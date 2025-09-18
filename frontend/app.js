const API_BASE = "http://localhost:5107/api";



document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("loginForm");

  form.addEventListener("submit", async (e) => {
    e.preventDefault(); // 🚫 BLOKUJE RELOAD strony

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    try {
      const res = await fetch(`${API_BASE}/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
      });

      console.log("STATUS:", res.status); // 👈 Debug do DevTools

      if (res.ok) {
        const data = await res.json();
        localStorage.setItem("token", data.token);
        localStorage.setItem("username", data.username || data.Username);
        localStorage.setItem("role", data.role || data.Role);

        window.location.href = "pages/dashboard.html";
      } else {
        document.getElementById("errorMessage").innerText =
          "❌ Nieprawidłowe dane logowania";
      }
    } catch (error) {
      console.error("Błąd logowania:", error);
      document.getElementById("errorMessage").innerText =
        "❌ Błąd połączenia z serwerem";
    }
  });
});
