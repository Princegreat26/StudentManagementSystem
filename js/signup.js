document.addEventListener("DOMContentLoaded", function () {
  document
    .getElementById("signupForm")
    .addEventListener("submit", function (event) {
      event.preventDefault();

      const name = document.getElementById("name").value;
      const email = document.getElementById("email").value;
      const userName = document.getElementById("userName").value;
      const password = document.getElementById("password").value;
      const confirmPassword = document.getElementById("confirmPassword").value;

      if (password !== confirmPassword) {
        alert("Passwords do not match!");
        return;
      }

      const userData = {
        name: name,
        userName: userName,
        email: email,
        password: password,
      };

      // Create and display the spinner
      const spinner = document.createElement("div");
      spinner.className = "spinner";
      document.body.appendChild(spinner);

      fetch("https://localhost:7178/api/Authentication/signup", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(userData),
      })
        .then((response) => response.json())
        .then((data) => {
          // Remove the spinner
          document.body.removeChild(spinner);
          alert("You have been successfully registered!");
          window.location.href = "login.html";
        })
        .catch((error) => {
          // Remove the spinner
          document.body.removeChild(spinner);
          console.error("Error:", error);
          alert("An error occurred while registering.");
        });
    });
});
