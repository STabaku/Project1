const API_BASE = "http://localhost:5183/api";
const AUTH_API = API_BASE + "/auth";

const TOKEN_KEY = "token";
const ROLE_KEY = "userRole";

const qs = id => document.getElementById(id);
const qsa = sel => document.querySelectorAll(sel);

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

const mask = v =>
  v.includes("@")
    ? v.slice(0, 2) + "***@" + v.split("@")[1]
    : "***" + v.slice(-3);

const otpValue = () =>
  [...qsa("#otpGrid input")].map(i => i.value).join("");

/* ================= STEP 1 – REQUEST OTP ================= */
async function sendOtp() {
  hide(qs("err1"));
  hide(qs("ok1"));

  const identifier = qs("identifier")?.value.trim();
  if (!identifier) {
    show(qs("err1"), "Email or phone is required");
    return;
  }

  const res = await fetch(AUTH_API + "/login-request", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ emailOrNumber: identifier })
  });

  if (!res.ok) {
    show(qs("err1"), "User not found or not verified");
    return;
  }

  show(qs("ok1"), "OTP sent");
  qs("maskTarget").textContent = mask(identifier);

  qs("step1").style.display = "none";
  qs("step2").style.display = "block";

  qsa("#otpGrid input")[0]?.focus();
}

/* ================= STEP 2 – VERIFY OTP & LOGIN ================= */
async function verifyOtp() {
  hide(qs("err2"));
  hide(qs("ok2"));

  const otp = otpValue();
  if (otp.length !== 6) {
    show(qs("err2"), "Enter 6-digit OTP");
    return;
  }

  const identifier = qs("identifier").value.trim();

  const res = await fetch(AUTH_API + "/login", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      emailOrNumber: identifier,
      otp: otp
    })
  });

  const data = await res.json();

  if (!res.ok) {
    show(qs("err2"), data || "Invalid or expired OTP");
    return;
  }

  localStorage.setItem(TOKEN_KEY, data.token);
  localStorage.setItem(ROLE_KEY, data.role);

  show(qs("ok2"), "Login successful");

  setTimeout(() => {
    if (data.role === "PharmacyAdmin")
      window.location.href = "pharmacy-dashboard.html";
    else if (data.role === "SuperAdmin")
      window.location.href = "dashboard.html";
    else
      window.location.href = "profile.html";
  }, 600);
}

/* ================= NAVBAR ================= */
function updateNavbar() {
  const token = localStorage.getItem(TOKEN_KEY);
  const nav = document.querySelector(".nav-links");
  if (!nav) return;

  nav.innerHTML = `
    <a href="index.html">Home</a>
    ${
      token
        ? `
          <a href="profile.html">My Profile</a>
          <a href="#" onclick="logout()">Logout</a>
        `
        : `
          <a href="login.html">Login</a>
          <a href="register.html">Register</a>
        `
    }
  `;
}

/* ================= LOGOUT ================= */
function logout() {
  localStorage.removeItem(TOKEN_KEY);
  localStorage.removeItem(ROLE_KEY);
  window.location.href = "login.html";
}

/* ================= OTP INPUT UX ================= */
qsa("#otpGrid input").forEach((i, idx, arr) => {
  i.addEventListener("input", () => {
    i.value = i.value.replace(/\D/g, "").slice(0, 1);
    if (i.value && idx < arr.length - 1) arr[idx + 1].focus();
  });
  i.addEventListener("keydown", e => {
    if (e.key === "Backspace" && !i.value && idx > 0)
      arr[idx - 1].focus();
  });
});

/* ================= EVENTS ================= */
qs("btnSendOtp")?.addEventListener("click", sendOtp);
qs("btnResendOtp")?.addEventListener("click", sendOtp);
qs("btnVerifyOtp")?.addEventListener("click", verifyOtp);

document.addEventListener("DOMContentLoaded", updateNavbar);
