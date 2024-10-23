document.addEventListener("DOMContentLoaded", async function () {
  const urlParams = new URLSearchParams(window.location.search);
  const id = urlParams.get("id");

  if (id) {
    const res = await fetch(
      `https://localhost:7178/api/StudentManagement/student/${id}`
    );
    const data = await res.json();
    populateForm(data);
  }

  document
    .getElementById("updateForm")
    .addEventListener("submit", async function (event) {
      event.preventDefault();

      const formData = new FormData(event.target);
      const updatedData = Object.fromEntries(formData.entries());

      const spinner = document.createElement("div");
      spinner.className = "spinner";
      document.body.appendChild(spinner);

      const res = await fetch(
        `https://localhost:7178/api/StudentManagement/${id}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
            Authorization: "bearer ".localStorage.get("authToken", token),
          },
          body: JSON.stringify(updatedData),
        }
      );

      if (res.ok) {
        document.body.removeChild(spinner);
        alert("Student successfully updated!");
        window.history.back();
      } else {
        document.body.removeChild(spinner);
        alert("Error updating student!");
      }
    });

  function populateForm(data) {
    document.getElementById("studentId").value = data.id;
    document.getElementById("firstName").value = data.firstName;
    document.getElementById("middleName").value = data.middleName;
    document.getElementById("lastName").value = data.lastName;
    document.getElementById("emailAddress").value = data.emailAddress;
    document.getElementById("address").value = data.address;
    document.getElementById("phoneNumber").value = data.phoneNumber;
    document.getElementById("department").value = data.department;
  }
});

function goBack() {
  window.history.back();
}
