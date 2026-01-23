<<<<<<< HEAD
=======
//index.html javascript
>>>>>>> 298fc1d403c607f6e7304ac73331ae4bfb6f65a1
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

<<<<<<< HEAD

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
=======
<<<<<<< Updated upstream


document.addEventListener("DOMContentLoaded", function () {

    const searchBtn = document.getElementById("searchBtn");
    const searchInput = document.getElementById("searchInput");
    const pharmacies = document.querySelectorAll(".pharmacy-card");
    const message = document.getElementById("noResultMsg");

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
        if (!found) {
            message.style.display = "block";
        } else {
            message.style.display = "none";
        }
    });

});








document.addEventListener("DOMContentLoaded", function () {

    const grid = document.getElementById("productGrid");
    if (!grid) return; // Only runs on browse.html

    // Read pharmacy from URL
    const params = new URLSearchParams(window.location.search);
    const pharmacy = params.get("pharmacy");

    console.log("Selected pharmacy:", pharmacy); // DEBUG

    const pharmacyData = {

        city: [
            { name: "Paracetamol", info: "500mg Tablets", price: "$4.99", img: "assets/paracetamol.jpg" },
            { name: "Ibuprofen", info: "200mg Capsules", price: "$5.99", img: "assets/ibuprofen.jpg" },
            { name: "Tachipirina", info: "Syrup", price: "$11.99", img: "assets/tachipirina.jpg" }
        ],

        healthplus: [
            { name: "Insulin", info: "Injection", price: "$19.99", img: "assets/insulin.jpg" },
            { name: "Amoxicillin", info: "500mg Capsules", price: "$8.99", img: "assets/amoxicillin.jpg" },
            { name: "Rapidol S", info: "500mg Capsules", price: "$6.99", img: "assets/rapidolS.jpg" }
        ],

        vita: [
            { name: "Paracetamol", info: "500mg Tablets", price: "$5.49", img: "assets/paracetamol.jpg" },
            { name: "Cough Syrup", info: "Cold & Flu", price: "$7.99", img: "assets/tachipirina.jpg" }
        ],

        greencross: [
            { name: "Ibuprofen", info: "400mg Tablets", price: "$6.49", img: "assets/ibuprofen.jpg" },
            { name: "Amoxicillin", info: "250mg Capsules", price: "$7.99", img: "assets/amoxicillin.jpg" }
        ],

        medicare: [
            { name: "Insulin", info: "Injection", price: "$18.99", img: "assets/insulin.jpg" },
            { name: "Vitamin C", info: "1000mg Tablets", price: "$4.50", img: "assets/paracetamol.jpg" }
        ],

        family: [
            { name: "Rapidol S", info: "500mg Capsules", price: "$6.99", img: "assets/rapidolS.jpg" },
            { name: "Tachipirina", info: "Syrup", price: "$12.99", img: "assets/tachipirina.jpg" }
        ],

        careplus: [
            { name: "Amoxicillin", info: "500mg Capsules", price: "$9.49", img: "assets/amoxicillin.jpg" },
            { name: "Paracetamol", info: "500mg Tablets", price: "$4.99", img: "assets/paracetamol.jpg" }
        ],

        rapid: [
            { name: "Ibuprofen", info: "200mg Capsules", price: "$5.99", img: "assets/ibuprofen.jpg" },
            { name: "Insulin", info: "Injection", price: "$20.99", img: "assets/insulin.jpg" }
        ]
    };

    const meds = pharmacyData[pharmacy];

    if (!meds) {
        grid.innerHTML = "<p style='font-weight:600;color:#a11d2d;'>No medications available for this pharmacy.</p>";
        return;
    }

    meds.forEach(med => {
        const card = document.createElement("div");
        card.className = "card";

        card.innerHTML = `
            <img src="${med.img}" alt="${med.name}">
            <h3>${med.name}</h3>
            <small>${med.info}</small>
            <div class="stars">★★★★★</div>
            <div class="price">${med.price}</div>
            <button>Add to Cart</button>
        `;

        grid.appendChild(card);
    });

});
=======
//request.html javascript

const step1 = document.getElementById("step1");
const step2 = document.getElementById("step2");
const step3 = document.getElementById("step3");

const ok1 = document.getElementById("ok1");
const err1 = document.getElementById("err1");

const pharmacies = [
  { name: "Pharmacy Vita", distance: "1.2 km", time: "10 minutes" },
  { name: "City Pharmacy", distance: "2.0 km", time: "15 minutes" },
  { name: "Health Plus Pharmacy", distance: "2.5 km", time: "18 minutes" }
];

document.getElementById("btnSendRequest").addEventListener("click", () => {
  const medicine = document.getElementById("medicine").value.trim();
  const quantity = document.getElementById("quantity").value;
  const address = document.getElementById("address").value.trim();

  if (!medicine || !quantity || !address) {
    err1.style.display = "block";
    err1.textContent = "Please fill in all required fields.";
    return;
  }

  err1.style.display = "none";
  ok1.style.display = "block";
  ok1.textContent = "Request sent successfully. Searching for pharmacy...";

  setTimeout(() => {
    step1.style.display = "none";
    step2.style.display = "block";
  }, 800);

  setTimeout(() => {
    const selected = pharmacies[Math.floor(Math.random() * pharmacies.length)];

    document.getElementById("pharmacyName").textContent = selected.name;
    document.getElementById("pharmacyDistance").textContent = selected.distance;
    document.getElementById("pharmacyTime").textContent = selected.time;

    step2.style.display = "none";
    step3.style.display = "block";
  }, 2500);
});

>>>>>>> Stashed changes
>>>>>>> 298fc1d403c607f6e7304ac73331ae4bfb6f65a1
