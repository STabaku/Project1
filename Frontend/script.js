
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
