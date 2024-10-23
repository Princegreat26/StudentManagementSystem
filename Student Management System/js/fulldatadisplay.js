document.addEventListener("DOMContentLoaded", async function () {
  const urlParams = new URLSearchParams(window.location.search);
  const id = urlParams.get("id");

  // Retrieve the token from local storage
  const token = localStorage.getItem("authToken");

  if (!token) {
    alert("Please log in to continue.");
    window.location.href = "login.html"; // Redirect to login page if not authenticated
    return;
  }

  if (id) {
    try {
      const res = await fetch(
        `https://localhost:7178/api/StudentManagement/student/${id}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      if (!res.ok) {
        throw new Error("Network response was not ok");
      }

      const data = await res.json();
      displayFullData(data);
    } catch (error) {
      console.error("Error fetching student data:", error);
      alert("An error occurred while fetching the student data.");
    }
  }

  function displayFullData(data) {
    const fullDataTableBody = document.getElementById("fullDataTableBody");
    fullDataTableBody.innerHTML = `
      <tr>
        <td>${data.id}</td>
        <td>${data.firstName}</td>
        <td>${data.middleName}</td>
        <td>${data.lastName}</td>
        <td>${data.emailAddress}</td>
        <td>${data.address}</td>
        <td>${data.phoneNumber}</td>
        <td>${data.department}</td>
        <td>${data.registerationNumber}</td>
        <td>
          <button class="btn btn-primary" onclick="redirectToUpdate('${data.id}')">Update</button>
          <button class="btn btn-danger" onclick="deleteStudent('${data.id}')">Delete</button>
        </td>
      </tr>
    `;
  }
});

function redirectToUpdate(id) {
  window.location.href = `updatestudent.html?id=${id}`;
}

function deleteStudent(id) {
  if (confirm("Are you sure you want to delete this student?")) {
    const spinner = document.createElement("div");
    spinner.className = "spinner";
    document.body.appendChild(spinner);

    fetch(`https://localhost:7178/api/StudentManagement/${id}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`,
        "Content-Type": "application/json",
      },
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data) => {
        document.body.removeChild(spinner);
        alert("Student successfully deleted!");
        window.history.back();
      })
      .catch((error) => {
        document.body.removeChild(spinner);
        alert("Error deleting student: " + error.message);
      });
  }
}

function goBack() {
  window.history.back();
}

// document.addEventListener("DOMContentLoaded", async function () {
//   const urlParams = new URLSearchParams(window.location.search);
//   const id = urlParams.get("id");

//   if (id) {
//     const res = await fetch(
//       `https://localhost:7178/api/StudentManagement/student/${id}`
//     );
//     const data = await res.json();
//     displayFullData(data);
//   }

//   function displayFullData(data) {
//     const fullDataTableBody = document.getElementById("fullDataTableBody");
//     fullDataTableBody.innerHTML = `
//       <tr>
//         <td>${data.id}</td>
//         <td>${data.firstName}</td>
//         <td>${data.middleName}</td>
//         <td>${data.lastName}</td>
//         <td>${data.emailAddress}</td>
//         <td>${data.address}</td>
//         <td>${data.phoneNumber}</td>
//         <td>${data.department}</td>
//         <td>${data.registerationNumber}</td>
//         <td>
//           <button class="btn btn-primary" onclick="redirectToUpdate('${data.id}')">Update</button>
//           <button class="btn btn-danger" onclick="deleteStudent('${data.id}')">Delete</button>
//         </td>
//       </tr>
//     `;
//   }
// });

// function redirectToUpdate(id) {
//   window.location.href = `updatestudent.html?id=${id}`;
// }

// function deleteStudent(id) {
//   if (confirm("Are you sure you want to delete this student?")) {
//     const spinner = document.createElement("div");
//     spinner.className = "spinner";
//     document.body.appendChild(spinner);

//     fetch(`https://localhost:7178/api/StudentManagement/${id}`, {
//       method: "DELETE",
//       Authorization: "bearer ".localStorage.get("authToken", token),
//     })
//       .then((response) => response.json())
//       .then((data) => {
//         document.body.removeChild(spinner);
//         alert("Student successfully deleted!");
//         window.history.back();
//       })
//       .catch((error) => {
//         document.body.removeChild(spinner);
//         alert("Error deleting student!");
//       });
//   }
// }

// function goBack() {
//   window.history.back();
// }
