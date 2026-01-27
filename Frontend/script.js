document.addEventListener("DOMContentLoaded", () => {
  const isLoggedIn = localStorage.getItem("isLoggedIn");
  const nav = document.querySelector(".nav-links");

  if (!nav) return; // For pages without main navbar (like login) do nothing

  if (isLoggedIn === "true") {

    // Remove Login link if exists
    const loginLink = [...document.querySelectorAll("a")].find(a =>
      a.getAttribute("href") && a.getAttribute("href").includes("login")
    );
    if (loginLink) loginLink.remove();

    // Add Profile link if not already present
    if (!document.querySelector('a[href="profile.html"]')) {
      const profileLink = document.createElement("a");
      profileLink.href = "profile.html";
      profileLink.className = "nav-link";
      profileLink.innerHTML = `<i class="fa-solid fa-user"></i> Profile`;
      nav.appendChild(profileLink);
    }

    // Add Logout link if not already present
    if (!document.getElementById("logoutBtn")) {
      const logoutLink = document.createElement("a");
      logoutLink.href = "#";
      logoutLink.id = "logoutBtn";
      logoutLink.className = "nav-link";
      logoutLink.innerHTML = `<i class="fa-solid fa-right-from-bracket"></i> Logout`;
      logoutLink.addEventListener("click", logoutUser);
      nav.appendChild(logoutLink);
    }

  } else {
    // Redirect from protected pages if not logged in
    const protectedPages = ["request.html", "pharmacies.html", "profile.html","browse.html"];
    const currentPage = window.location.pathname.split("/").pop();

    if (protectedPages.includes(currentPage)) {
      window.location.href = "login.html";
    }
  }
});


const searchBtn = document.getElementById("searchBtn");
const searchInput = document.getElementById("searchInput");
const pharmacies = document.querySelectorAll(".pharmacy-card");
const message = document.getElementById("noResultMsg");

if (searchBtn && searchInput && pharmacies.length > 0) {
  searchBtn.addEventListener("click", function () {
    const input = searchInput.value.toLowerCase().trim();
    let found = false;

    pharmacies.forEach(card => {
      const name = card.querySelector("h3").textContent.toLowerCase();
      const location = card.querySelector("small").textContent.toLowerCase();

      if (name.includes(input) || location.includes(input)) {
        card.style.display = "";
        found = true;
      } else {
        card.style.display = "none";
      }
    });

    // Show or hide message
    if (message) {
      message.style.display = found ? "none" : "block";
    }
  });
}








// ===============================
// CONFIG
// ===============================
const BACKEND_URL = "http://localhost:5183/api/auth";

// ===============================
// ELEMENT REFERENCES
// ===============================
const step1 = document.getElementById("step1");
const step2 = document.getElementById("step2");
const identifierEl = document.getElementById("identifier");
const maskTarget = document.getElementById("maskTarget");

const ok1 = document.getElementById("ok1");
const err1 = document.getElementById("err1");
const ok2 = document.getElementById("ok2");
const err2 = document.getElementById("err2");





// ===============================
// HELPERS
// ===============================
function show(el, msg){
  el.textContent = msg;
  el.style.display = "block";
}
function hide(el){
  el.textContent = "";
  el.style.display = "none";
}
function maskIdentifier(v){
  v = v.trim();
  if (v.includes("@")){
    const [u, d] = v.split("@");
    const uMasked = u.length <= 2 ? u[0] + "*" : u.slice(0,2) + "***";
    return `${uMasked}@${d}`;
  }
  const last = v.slice(-3);
  return v.slice(0, Math.max(0, v.length-3)).replace(/\d/g,"*") + last;
}
function getOtpValue(){
  const inputs = [...document.querySelectorAll("#otpGrid input")];
  return inputs.map(i => i.value.replace(/\D/g,"")).join("");
}





// ===============================
// SEND OTP
// ===============================
async function sendOtp() {
  hide(ok1); hide(err1); hide(ok2); hide(err2);

  const identifier = identifierEl.value.trim();
  if (!identifier) {
    show(err1, "Please enter your email or phone number.");
    return;
  }

  // <-- Add the log here
  console.log("Sending login OTP for:", identifier);

  try {
    const res = await fetch(`${BACKEND_URL}/login`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ EmailOrNumber: identifier })
    });

    const data = await res.json();
    if (!res.ok) {
      show(err1, data);
      return;
    }

    show(ok1, "OTP sent successfully.");
    maskTarget.textContent = maskIdentifier(identifier);

    step1.style.display = "none";
    step2.style.display = "block";

    const inputs = [...document.querySelectorAll("#otpGrid input")];
    inputs[0].focus();

    // AUTO-FILL OTP in DEV MODE
    if (data.otp) {
      const otpDigits = data.otp.split("");
      inputs.forEach((inp, i) => { inp.value = otpDigits[i] || ""; });
    }

  } catch(e) {
    show(err1, "Cannot connect to server. Check backend URL.");
  }
}


// ===============================
// VERIFY OTP
// ===============================
async function verifyLoginOtp() {
  hide(ok2); hide(err2);

  const otp = getOtpValue(); // gets digits from grid
  if (otp.length !== 6) {
    show(err2, "Please enter the 6-digit OTP code.");
    return;
  }

  const identifier = identifierEl.value.trim();

 const payload = {
    EmailOrNumber: identifierEl.value.trim(),  // make sure this always has value
    OTP: getOtpValue()                          // ensure 6-digit string
};
console.log("Sending verify-login payload:", payload);  // debug


  try {
    const res = await fetch("http://localhost:5183/api/auth/verify-login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    const data = await res.json();

    if (!res.ok) {
      show(err2, data.message || JSON.stringify(data));
      return;
    }

    // Successful login
    localStorage.setItem("isLoggedIn", "true")
    localStorage.setItem("userIdentifier", identifier);
   

    localStorage.setItem("userRole", data.role || "User");
    localStorage.setItem("userId", data.id);

    show(ok2, "Login successful. Redirecting...");

    setTimeout(() => {
      window.location.href = "profile.html";
    }, 1000);

  } catch(e) {
    show(err2, "Server error. Try again later.");
  }
}


// ===============================
// OTP GRID UX
// ===============================
document.querySelectorAll("#otpGrid input").forEach((inp, idx, arr) => {
  inp.addEventListener("input", () => {
    inp.value = inp.value.replace(/\D/g,"").slice(0,1);
    if (inp.value && idx < arr.length - 1) arr[idx+1].focus();
  });
  inp.addEventListener("keydown", (e) => {
    if (e.key === "Backspace" && !inp.value && idx > 0) arr[idx-1].focus();
  });
});

// ===============================
// BUTTON EVENTS
// ===============================
const btnSendOtp = document.getElementById("btnSendOtp");
if (btnSendOtp) btnSendOtp.addEventListener("click", sendOtp);

const btnResendOtp = document.getElementById("btnResendOtp");
if (btnResendOtp) btnResendOtp.addEventListener("click", sendOtp);

const btnVerifyOtp = document.getElementById("btnVerifyOtp");
if (btnVerifyOtp) btnVerifyOtp.addEventListener("click", verifyLoginOtp);



// ===============================
// LOGOUT FUNCTION (used elsewhere in project)
// ===============================
function logoutUser() {
  localStorage.removeItem("isLoggedIn");
  localStorage.removeItem("userRole");
  localStorage.removeItem("userIdentifier");
  window.location.href = "login.html";
}






   
//request.html javascript

document.addEventListener("DOMContentLoaded", function() {
  // Only visible on request.html
  const step1 = document.getElementById("step1");
  const step2 = document.getElementById("step2");
  const step3 = document.getElementById("step3");

  const ok1 = document.getElementById("ok1");
  const err1 = document.getElementById("err1");

  // your existing request.html JS code here...
});






/* ================= FUNCTIONS ================= */

function requireLogin(targetPage) {
  const isLoggedIn = localStorage.getItem("isLoggedIn");

  if (isLoggedIn !== "true") {
    window.location.href = "login.html";
  } else {
    window.location.href = targetPage;
  }
}

function logoutUser() {
  localStorage.removeItem("isLoggedIn");
  localStorage.removeItem("userRole");
  localStorage.removeItem("userIdentifier");
 window.location.href = "login.html";
}











