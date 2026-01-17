function sendRequest() {
  fetch("http://localhost:5000/api/requests", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      medication: "Insulin",
      urgency: "High"
    })
  });

  alert("Request sent asynchronously");
}
