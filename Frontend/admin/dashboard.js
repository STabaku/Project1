// protect page
if (localStorage.getItem("adminAuth") !== "true") {
  window.location.href = "login.html";
}

const BASE_URL = "http://localhost:5183";

/* NAVIGATION */
function showSection(id) {
  document.querySelectorAll(".section").forEach(s => s.classList.add("hidden"));
  document.getElementById(id).classList.remove("hidden");

  document.querySelectorAll(".sidebar a").forEach(a => a.classList.remove("active"));
  event.target.classList.add("active");
}

function logout() {
  localStorage.removeItem("adminAuth");
  window.location.href = "login.html";
}

/* LOAD USERS */
async function loadUsers() {
  const res = await fetch(`${BASE_URL}/api/users`);
  const data = await res.json();

  const table = document.getElementById("usersTable");
  table.innerHTML = "";

  data.forEach(u => {
    table.innerHTML += `
      <tr>
        <td>${u.id}</td>
        <td>${u.email}</td>
      </tr>
    `;
  });
}

/* LOAD REQUESTS */
async function loadRequests() {
  const res = await fetch(`${BASE_URL}/api/requests`);
  const data = await res.json();

  const table = document.getElementById("requestsTable");
  table.innerHTML = "";

  data.forEach(r => {
    table.innerHTML += `
      <tr>
        <td>${r.id}</td>
        <td>${r.userId}</td>
        <td>${r.medicineName}</td>
        <td>${r.quantity}</td>
        <td>${r.address}</td>
        <td>${r.status}</td>
        <td>
  <button onclick="accept(${r.id})" ${r.status !== 'Pending' ? 'disabled' : ''}>Accept</button>
  <button onclick="deliver(${r.id})" ${r.status !== 'Accepted' ? 'disabled' : ''}>Deliver</button>
</td>

      </tr>
    `;
  });
}

loadUsers();
loadRequests();


// ================= ADD USER =================
async function addUser() {
  const name = document.getElementById("newUserName").value.trim();
  const email = document.getElementById("newUserEmail").value.trim();
  const gender = document.getElementById("newUserGender").value;

  if (!name || !email) {
    document.getElementById("addUserMsg").innerText = "Name and Email are required";
    return;
  }

  try {
    const res = await fetch(`${BASE_URL}/api/users`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ Name: name, Email: email, Gender: gender })
    });

    if (!res.ok) {
      const data = await res.json();
      document.getElementById("addUserMsg").innerText = data.message || "Failed to add user";
      return;
    }

    document.getElementById("addUserMsg").innerText = "User added successfully!";
    document.getElementById("newUserName").value = "";
    document.getElementById("newUserEmail").value = "";
    document.getElementById("newUserGender").value = "";

    loadUsers(); // refresh table
  } catch (err) {
    document.getElementById("addUserMsg").innerText = "Server error. Try again.";
  }
}


// Accept a request
async function accept(id) {
  try {
    const res = await fetch(`${BASE_URL}/api/requests/accept/${id}`, { method: "POST" });
    if (!res.ok) throw new Error("Failed to accept request");
    loadRequests(); // refresh table
  } catch (err) {
    alert(err.message);
  }
}

// Deliver a request
async function deliver(id) {
  try {
    const res = await fetch(`${BASE_URL}/api/requests/deliver/${id}`, { method: "POST" });
    if (!res.ok) throw new Error("Failed to deliver request");
    loadRequests(); // refresh table
  } catch (err) {
    alert(err.message);
  }
}
