document.addEventListener("DOMContentLoaded", async function () {
  const allStudentsTableBody = document.getElementById("allStudentsTableBody");

  // Retrieve the token from local storage
  const token = localStorage.getItem("authToken");

  if (!token) {
    alert("Please log in to continue.");
    window.location.href = "login.html"; // Redirect to login page if not authenticated
    return;
  }

  try {
    const res = await fetch("https://localhost:7178/api/StudentManagement/", {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });

    if (!res.ok) {
      throw new Error("Network response was not ok");
    }

    const data = await res.json();
    console.log(data);
    displayAllStudents(data);
  } catch (error) {
    console.error("Error fetching student data:", error);
    alert("An error occurred while fetching the student data.");
  }

  function displayAllStudents(students) {
    allStudentsTableBody.innerHTML = "";
    students.forEach((student) => {
      const row = document.createElement("tr");
      row.innerHTML = `
        <td>${student.firstName}</td>
        <td>${student.lastName}</td>
        <td>${student.department}</td>
        <td>${student.registerationNumber}</td>
        <td>
          <button class="btn btn-primary" onclick="redirectToFullData('${student.id}')">
            View Details
          </button>
        </td>
      `;
      allStudentsTableBody.appendChild(row);
    });
  }
});

function redirectToFullData(id) {
  window.location.href = `fulldatadisplay.html?id=${id}`;
}
