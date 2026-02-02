if (localStorage.getItem("pharmacyAuth") !== "true") {
  window.location.href = "login.html";
}

const BASE_URL = "http://localhost:5183";

/* NAVIGATION */
function showSection(sectionId) {
  // hide all sections
  document.querySelectorAll('.section').forEach(section => {
    section.classList.add('hidden');
  });

  // show selected section
  document.getElementById(sectionId).classList.remove('hidden');

  // remove active from all sidebar links
  document.querySelectorAll('.sidebar a').forEach(link => {
    link.classList.remove('active');
  });

  // add active to clicked link
  event.target.classList.add('active');
}

function logout() {
  localStorage.removeItem("pharmacyAuth");
  window.location.href = "./login.html";
}

/* LOAD REQUESTS */
async function loadRequests() {
  const res = await fetch(`${BASE_URL}/api/requests`);
  const data = await res.json();

  const newT = document.getElementById("newRequests");
  const activeT = document.getElementById("activeOrders");
  const historyT = document.getElementById("historyOrders");

  newT.innerHTML = "";
  activeT.innerHTML = "";
  historyT.innerHTML = "";

  let pending = 0, active = 0, done = 0;

  data.forEach(r => {
    if (r.status === "Pending") {
      pending++;
      newT.innerHTML += `
        <tr>
          <td>${r.id}</td>
          <td>${r.medicineName}</td>
          <td>${r.quantity}</td>
          <td>${r.address}</td>
          <td>${new Date(r.createdAt).toLocaleString()}</td>
          <td><button class="btn btn-accept" onclick="accept(${r.id})">Accept</button></td>
        </tr>`;
    }

    if (r.status === "Accepted") {
      active++;
      activeT.innerHTML += `
        <tr>
          <td>${r.id}</td>
          <td>${r.medicineName}</td>
          <td>${r.quantity}</td>
          <td>${r.address}</td>
          <td class="status-accepted">Accepted</td>
          <td><button class="btn btn-done" onclick="deliver(${r.id})">Delivered</button></td>
        </tr>`;
    }

    if (r.status === "Delivered") {
      done++;
      historyT.innerHTML += `
        <tr>
          <td>${r.id}</td>
          <td>${r.medicineName}</td>
          <td>${r.quantity}</td>
          <td>${r.address}</td>
          <td class="status-delivered">Delivered</td>
          <td>${new Date(r.createdAt).toLocaleString()}</td>
        </tr>`;
    }
  });

  document.getElementById("totalReq").innerText = data.length;
  document.getElementById("pendingReq").innerText = pending;
  document.getElementById("activeReq").innerText = active;
  document.getElementById("doneReq").innerText = done;
}

async function accept(id) {
  await fetch(`${BASE_URL}/api/requests/accept/${id}`, { method: "POST" });
  loadRequests();
}

async function deliver(id) {
  await fetch(`${BASE_URL}/api/requests/deliver/${id}`, { method: "POST" });
  loadRequests();
}

setInterval(loadRequests, 3000);
loadRequests();


