function login(e) {
  e.preventDefault();

  const user = document.getElementById("username").value;
  const pass = document.getElementById("password").value;

  // simple demo admin login
  if (user === "admin" && pass === "admin123") {
    localStorage.setItem("adminAuth", "true");
    window.location.href = "dashboard.html";
  } else {
    document.getElementById("error").innerText = "Invalid admin credentials";
  }
}

