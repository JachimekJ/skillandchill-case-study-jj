const API_BASE = "http://localhost:5107/api"; // poprawiony port na backend

document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("loginForm");
  if (form) {
    form.addEventListener("submit", async (e) => {
      e.preventDefault();

      const username = document.getElementById("username").value;
      const password = document.getElementById("password").value;

      try {
        const res = await fetch(`${API_BASE}/auth/login`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ username, password }),
        });

        if (res.ok) {
          const data = await res.json();
          localStorage.setItem("token", data.token);
          localStorage.setItem("username", data.username); // <-- poprawka
          window.location.href = "pages/dashboard.html";
        } else {S
          // tu swagger zwraca pustą odpowiedź 401, więc res.json() wywali błąd
          document.getElementById("errorMessage").innerText =
            "❌ Nieprawidłowe dane logowania";
        }
      } catch (error) {
        console.error("Błąd logowania:", error);
        document.getElementById("errorMessage").innerText =
          "Błąd połączenia z serwerem";
      }
    });
  }
});