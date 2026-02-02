function login(e) {
  e.preventDefault();

  const user = document.getElementById("username").value;
  const pass = document.getElementById("password").value;

  if (user === "admin" && pass === "1234") {
    localStorage.setItem("pharmacyAuth", "true");
    window.location.href = "dashboard.html";
  } else {
    document.getElementById("error").innerText = " Invalid credentials";
  }
}


