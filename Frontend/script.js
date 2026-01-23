document.addEventListener("DOMContentLoaded", () => {

  const isLoggedIn = localStorage.getItem("isLoggedIn");
  const nav = document.querySelector(".nav-links");

  if (!nav) return; // në faqet pa navbar kryesor (si login) mos bëj asgjë

  /* ================= NAVBAR CONTROL ================= */

  if (isLoggedIn === "true") {

    // Fshij Login (më fleksibël – gjen çdo link që përmban 'login')
    const loginLink = [...document.querySelectorAll("a")].find(a =>
      a.getAttribute("href") && a.getAttribute("href").includes("login")
    );

    if (loginLink) loginLink.remove();

    // Shto Profile
    if (!document.querySelector('a[href="profile.html"]')) {
      const profileLink = document.createElement("a");
      profileLink.href = "profile.html";
      profileLink.className = "nav-link";
      profileLink.innerHTML = `<i class="fa-solid fa-user"></i> Profile`;
      nav.appendChild(profileLink);
    }

    // Shto Logout
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

    // Redirect nga faqet e mbrojtura
    const protectedPages = ["request.html", "pharmacies.html", "profile.html"];
    const currentPage = window.location.pathname.split("/").pop();

    if (protectedPages.includes(currentPage)) {
      window.location.href = "login.html";
    }
  }

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
