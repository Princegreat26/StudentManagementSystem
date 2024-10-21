document.addEventListener("DOMContentLoaded", function () {
  const registrationForm = document.getElementById("registrationForm");
  const departmentDropdown = document.getElementById("department");

  // Fetch departments when the dropdown is clicked
  departmentDropdown.addEventListener("click", function () {
    if (departmentDropdown.options.length === 1) {
      fetch("https://localhost:7178/api/StudentManagement/department")
        .then((response) => response.json())
        .then((data) => {
          // Clear existing options except the first one
          while (departmentDropdown.options.length > 1) {
            departmentDropdown.remove(1);
          }

          // Populate the dropdown with the retrieved departments from the backend
          data.forEach((department) => {
            const option = document.createElement("option");
            option.value = department.name;
            option.text = department.name;
            departmentDropdown.add(option);
          });
        })
        .catch((error) => {
          console.error("Error fetching departments:", error);
          alert("An error occurred while fetching the departments.");
        });
    }
  });

  registrationForm.addEventListener("submit", function (event) {
    event.preventDefault();

    const firstName = document.getElementById("firstName");
    const middleName = document.getElementById("middleName");
    const lastName = document.getElementById("lastName");
    const phoneNumber = document.getElementById("phoneNumber");
    const email = document.getElementById("emailAddress");
    const address = document.getElementById("address");
    const department = document.getElementById("department");

    // Debugging: Check if elements are found
    console.log("firstName:", firstName);
    console.log("middleName:", middleName);
    console.log("lastName:", lastName);
    console.log("phoneNumber:", phoneNumber);
    console.log("emailAddress:", email);
    console.log("address:", address);
    console.log("department", department);

    if (
      firstName &&
      middleName &&
      lastName &&
      phoneNumber &&
      email &&
      address &&
      department
    ) {
      const studentData = {
        firstName: firstName.value,
        middleName: middleName.value,
        lastName: lastName.value,
        phoneNumber: phoneNumber.value,
        emailAddress: email.value,
        address: address.value,
        department: department.value,
      };

      fetch("https://localhost:7178/api/StudentManagement", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `bearer ${localStorage.getItem("authToken")}`,
        },
        body: JSON.stringify(studentData),
      })
        .then((response) => response.json())
        .then((data) => {
          console.log("Success:", data);
          alert("Student registered successfully!");
        })
        .catch((error) => {
          console.error("Error:", error);
          alert("An error occurred while registering the student.");
        });
    } else {
      alert("One or more form elements are missing.");
    }
  });
});
