
document.addEventListener("DOMContentLoaded", () => {
  const isLoggedIn = localStorage.getItem("isLoggedIn");

  const loginLink = document.querySelector('a[href="login.html"]');
  const registerLink = document.querySelector('a[href="register.html"]');

  if (isLoggedIn === "true") {

   
    if (loginLink) loginLink.style.display = "none";
    if (registerLink) registerLink.style.display = "none";

    // Add Profile
    const nav = document.querySelector(".nav-links");
    const profileLink = document.createElement("a");
    profileLink.href = "profile.html";
    profileLink.textContent = "Profile";

    nav.appendChild(profileLink);
  }
});

