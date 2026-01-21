const apiUrl = "http://localhost:5183/api/auth";

function showLogin() { document.getElementById("loginPopup").style.display = "block"; }
function showSignup() { document.getElementById("signupPopup").style.display = "block"; }
function closePopup(id) { document.getElementById(id).style.display = "none"; }

async function signup() {
const data = {
    Name: document.getElementById("signupName").value,
    Location: document.getElementById("signupLocation").value,
    Number: document.getElementById("signupNumber").value,
    Gender: document.getElementById("signupGender").value,
    Age: parseInt(document.getElementById("signupAge").value),
    Email: document.getElementById("signupEmail").value
};

    const res = await fetch(apiUrl + "/signup", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });
    alert(await res.text());
}

async function verifySignup() {
    const data = {
        EmailOrNumber: document.getElementById("signupEmail").value,
        OTP: document.getElementById("signupOTP").value
    };
    const res = await fetch(apiUrl + "/verify", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });
    const result = await res.json();
    alert(result.message + " Role: " + result.role);
}

async function login() {
    const data = { EmailOrNumber: document.getElementById("loginEmail").value };
    const res = await fetch(apiUrl + "/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });
    alert(await res.text());
}

async function verifyLogin() {
    const data = {
        EmailOrNumber: document.getElementById("loginEmail").value,
        OTP: document.getElementById("loginOTP").value
    };
    const res = await fetch(apiUrl + "/verify", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
    });
    const result = await res.json();
    alert(result.message + " Role: " + result.role);
}
