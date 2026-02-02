const API_BASE = "http://localhost:5183/api";
const AUTH_API = API_BASE + "/auth";

const qs = id => document.getElementById(id);

const show = (el, msg) => {
  if (!el) return;
  el.textContent = msg;
  el.style.display = "block";
};

const hide = el => {
  if (!el) return;
  el.textContent = "";
  el.style.display = "none";
};

// ================= LOGIN =================
async function login() {
  hide(qs("err1"));
  hide(qs("ok1"));

  const email = qs("email")?.value.trim();
  const password = qs("password")?.value;

  if (!email || !password) {
    show(qs("err1"), "Email and password are required");
    return;
  }

  try {
    const res = await fetch(AUTH_API + "/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        emailOrNumber: email,
        password: password
      })
    });

    const data = await res.json();

    if (!res.ok) {
      show(qs("err1"), data || "Invalid credentials");
      return;
    }

    localStorage.setItem("token", data.token);
    localStorage.setItem("userRole", data.role);

const payload = JSON.parse(atob(data.token.split('.')[1]));
console.log("JWT payload:", payload);

const userId =
  payload.nameid ||
  payload.sub ||
  payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

localStorage.setItem("userId", userId);


    show(qs("ok1"), "Login successful");

    setTimeout(() => {
      if (data.role === "PharmacyAdmin")
        window.location.href = "pharmacy-dashboard.html";
      else if (data.role === "SuperAdmin")
        window.location.href = "dashboard.html";
      else
        window.location.href = "index.html";
    }, 500);

  } catch {
    show(qs("err1"), "Server error. Try again.");
  }
}

// ================= NAVBAR =================
function updateNavbar() {
  const token = localStorage.getItem("token");
  const authLink = document.getElementById("auth-link");
  if (!authLink) return;

  if (token) {
    authLink.innerHTML = `
      <a class="nav-link" href="profile.html">
        <i class="fa fa-user"></i> My Profile
      </a>
      <a class="nav-link" href="#" id="logoutBtn">
        <i class="fa fa-right-from-bracket"></i> Logout
      </a>
    `;

    document
      .getElementById("logoutBtn")
      ?.addEventListener("click", logout);

  } else {
    authLink.innerHTML = `
      <a class="nav-link" href="login.html">
        <i class="fa fa-right-to-bracket"></i> Login
      </a>
      <a class="nav-link" href="register.html">
        <i class="fa fa-user-plus"></i> Register
      </a>
    `;
  }
}

// ================= EVENTS =================
qs("btnLogin")?.addEventListener("click", login);
document.addEventListener("DOMContentLoaded", updateNavbar);
