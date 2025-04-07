document.getElementById("loginForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Отменяем стандартную отправку формы

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    fetch("https://localhost:7207/api/auth/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            username: username,
            userPassword: password
        })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Неверный логин или пароль");
            }
            return response.json();
        })
        .then(data => {
            document.getElementById("message").innerText = data.message + " (ID: " + data.userId + ")";
        })
        .catch(error => {
            document.getElementById("message").innerText = error.message;
        });
});