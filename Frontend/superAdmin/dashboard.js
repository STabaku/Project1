/* ================= AUTH CHECK ================= */

if (localStorage.getItem("pharmacyAuth") !== "true") {
  window.location.href = "login.html";
}

const BASE_URL = "http://localhost:5183";


/* ================= NAVIGATION ================= */

function showSection(sectionId, el) {

  // hide all sections
  document.querySelectorAll('.section').forEach(section => {
    section.classList.add('hidden');
    section.classList.remove('active-section');
  });

  // show selected
  const selected = document.getElementById(sectionId);
  if (selected) {
    selected.classList.remove('hidden');
    selected.classList.add('active-section');
  }

  // remove active from sidebar
  document.querySelectorAll('.sidebar a').forEach(link => {
    link.classList.remove('active');
  });

  // activate clicked link
  if (el) el.classList.add('active');
}


/* ================= LOGOUT ================= */

function logout() {
  localStorage.removeItem("pharmacyAuth");
  window.location.href = "login.html";
}


/* ================= DASHBOARD DATA ================= */

/* ---- USERS ---- */

function loadUsers() {

  const users = [
    { id:1, name:"Eliada Eliada", email:"eliada12@gmail.com" },
    { id:2, name:"Silva Tabaku", email:"silvatabaku@gmail.com" },
    { id:3, name:"Nadja Brari", email:"nadiaB@gmail.com" }
  ];

  const table = document.getElementById("usersTable");
  if (!table) return;

  table.innerHTML = users.map(u => `
    <tr>
      <td>${u.id}</td>
      <td>${u.name}</td>
      <td>${u.email}</td>
    </tr>
  `).join("");

  document.getElementById("totalUsers").innerText = users.length;
}


/* ---- PHARMACIES ---- */

function loadPharmacies() {

  const pharmacies = [
    { id:1, name:"City Pharmacy", city:"Tirana" },
    { id:2, name:" Green Cross Pharmacy", city:"Durres" }
  ];

  const table = document.getElementById("pharmaciesTable");
  if (!table) return;

  table.innerHTML = pharmacies.map(p => `
    <tr>
      <td>${p.id}</td>
      <td>${p.name}</td>
      <td>${p.city}</td>
    </tr>
  `).join("");

  document.getElementById("totalPharmacies").innerText = pharmacies.length;
}


/* ---- REQUESTS ---- */

function loadRequests() {

  const requests = [
    { id:1, medicine:"Insulin", status:"Pending" },
    { id:2, medicine:"Paracetamol", status:"Accepted" },
    { id:3, medicine:"Amoxicillin", status:"Delivered" }
  ];

  const table = document.getElementById("requestsTable");
  if (!table) return;

  table.innerHTML = requests.map(r => `
    <tr>
      <td>${r.id}</td>
      <td>${r.medicine}</td>
      <td>${r.status}</td>
    </tr>
  `).join("");

  document.getElementById("totalRequests").innerText = requests.length;
}


/* ---- HISTORY ---- */

function loadHistory() {

  const history = [
    { id:1, action:"User registered", date:"2024-01-12" },
    { id:2, action:"Pharmacy approved", date:"2024-01-15" }
  ];

  const table = document.getElementById("historyTable");
  if (!table) return;

  table.innerHTML = history.map(h => `
    <tr>
      <td>${h.id}</td>
      <td>${h.action}</td>
      <td>${h.date}</td>
    </tr>
  `).join("");
}


/* ================= INIT ================= */

document.addEventListener("DOMContentLoaded", () => {
  loadUsers();
  loadPharmacies();
  loadRequests();
  loadHistory();
});
