document.addEventListener("DOMContentLoaded", function () {
  const searchInput = document.getElementById("searchInput");
  const searchButton = document.getElementById("searchButton");
  const resultsContainer = document.getElementById("resultsContainer");
  const resultsTableBody = resultsContainer.querySelector("tbody");

  searchButton.addEventListener("click", async function () {
    const searchQuery = searchInput.value.trim();
    if (searchQuery === "") {
      alert("Please enter a registration number to search.");
      return;
    }

    console.log("Search query:", searchQuery);

    // Retrieve the token from local storage
    const token = localStorage.getItem("authToken");

    if (!token) {
      alert("Please log in to continue.");
      window.location.href = "login.html"; // Redirect to login page if not authenticated
      return;
    }

    try {
      const res = await fetch(
        `https://localhost:7178/api/StudentManagement/${searchQuery}`,
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
      console.log(data);
      displayResults(data);
    } catch (error) {
      console.error("Error fetching student data:", error);
      alert("An error occurred while fetching the student data.");
    }
  });

  function displayResults(data) {
    resultsTableBody.innerHTML = ``;
    searchInput.value = "";
    resultsTableBody.innerHTML = `
      <tr>
        <td>${data.firstName}</td>
        <td>${data.lastName}</td>
        <td>${data.department}</td>
        <td>${data.registerationNumber}</td>
        <td>
          <button class="btn btn-primary" onclick="redirectToFullData('${data.id}')">
            View Details
          </button>
        </td>
      </tr>
    `;
  }

  function redirectToFullData(id) {
    window.location.href = `fulldatadisplay.html?id=${id}`;
  }
});
