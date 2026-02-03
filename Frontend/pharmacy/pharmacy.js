

let requests = [
  {id:1, medicine:"Paracetamol", status:"Pending"},
  {id:2, medicine:"Aspirin", status:"Accepted"},
  {id:3, medicine:"Vitamin C", status:"Delivered"},
  {id:4, medicine:"Ibuprofen", status:"Pending"}
];


const MED_KEY = "pharmacy_medicines";

function loadMedicines(){
  try{
    return JSON.parse(localStorage.getItem(MED_KEY)) || [];
  }catch(e){
    return [];
  }
}
function saveMedicines(list){
  localStorage.setItem(MED_KEY, JSON.stringify(list));
}

let medicines = loadMedicines();


function showSection(id){
  document.querySelectorAll(".section").forEach(s=>s.classList.remove("active"));
  document.getElementById(id).classList.add("active");
}


function render(){
  let newT="", activeT="", historyT="";

  requests.forEach(r=>{
    if(r.status==="Pending"){
      newT+=`<tr><td>${r.id}</td><td>${r.medicine}</td>
      <td><button class="btn btn-accept" onclick="accept(${r.id})">Accept</button></td></tr>`;
    }
    if(r.status==="Accepted"){
      activeT+=`<tr><td>${r.id}</td><td>${r.medicine}</td>
      <td>Accepted</td>
      <td><button class="btn btn-done" onclick="deliver(${r.id})">Done</button></td></tr>`;
    }
    if(r.status==="Delivered"){
      historyT+=`<tr><td>${r.id}</td><td>${r.medicine}</td>
      <td>Delivered</td></tr>`;
    }
  });

  document.getElementById("newTable").innerHTML=newT;
  document.getElementById("activeTable").innerHTML=activeT;
  document.getElementById("historyTable").innerHTML=historyT;

  document.getElementById("totalReq").innerText=requests.length;
  document.getElementById("pendingReq").innerText=requests.filter(r=>r.status==="Pending").length;
  document.getElementById("activeReq").innerText=requests.filter(r=>r.status==="Accepted").length;
  document.getElementById("doneReq").innerText=requests.filter(r=>r.status==="Delivered").length;
}

function accept(id){
  requests.find(r=>r.id===id).status="Accepted";
  render();
}

function deliver(id){
  requests.find(r=>r.id===id).status="Delivered";
  render();
}


function addMedicine(){
  let name = document.getElementById("medicineName").value.trim();
  let category = document.getElementById("medicineCategory").value.trim();
  let description = document.getElementById("medicineDescription").value.trim();
  let price = document.getElementById("medicinePrice").value.trim();
  let imageInput = document.getElementById("medicineImage");

  if(name==="" || category==="" || description==="" || price===""){
    alert("Fill all fields");
    return;
  }

  let imageURL = "";
  if(imageInput.files.length > 0){

    imageURL = URL.createObjectURL(imageInput.files[0]);
  }

  let newId = medicines.length ? medicines[medicines.length - 1].id + 1 : 1;

  medicines.push({
    id:newId,
    name:name,
    category:category,
    description:description,
    price:Number(price),
    image:imageURL,
    createdAt:new Date().toISOString()
  });

  saveMedicines(medicines);

  // clear form
  document.getElementById("medicineName").value="";
  document.getElementById("medicineCategory").value="";
  document.getElementById("medicineDescription").value="";
  document.getElementById("medicinePrice").value="";
  document.getElementById("medicineImage").value="";

  alert("Medicine Added Successfully (Saved to catalog)");

  showSection("overview");
  render();
}

render();
function logout() {
  localStorage.removeItem("user");

  window.location.href = "pharmacyLogin.html";
}

