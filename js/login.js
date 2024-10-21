document.addEventListener("DOMContentLoaded", function () {
  document
    .getElementById("loginForm")
    .addEventListener("submit", function (event) {
      event.preventDefault();

      const username = document.getElementById("username").value;
      const password = document.getElementById("password").value;

      const userData = {
        username: username,
        password: password,
      };

      // Create and display the spinner
      const spinner = document.createElement("div");
      spinner.className = "spinner";
      document.body.appendChild(spinner);

      fetch("https://localhost:7178/api/Authentication/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          //Authorization: "bearer ".localStorage.get("authToken", token),
        },
        body: JSON.stringify(userData),
      })
        .then((response) => response.text())
        .then((token) => {
          // Remove the spinner
          localStorage.setItem("authToken", token);
          document.body.removeChild(spinner);
          if (token) {
            alert("Login successful!");

            // Get the redirect URL from the query parameter
            const urlParams = new URLSearchParams(window.location.search);
            const redirectUrl = urlParams.get("redirect");

            // Redirect to the intended page or default to the home page
            window.location.href = redirectUrl || "index.html";
          } else {
            alert("Invalid username or password.");
          }
        })
        .catch((error) => {
          // Remove the spinner
          document.body.removeChild(spinner);
          console.error("Error:", error);
          alert("An error occurred while logging in.");
        });
    });
});
