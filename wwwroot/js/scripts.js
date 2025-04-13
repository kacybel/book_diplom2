document.getElementById("loginForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Отменяем стандартную отправку формы

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    fetch("/api/auth/login", {
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
            document.cookie = `userId=${data.userId}; path=/; SameSite=None; Secure`;
            window.location.href = "/cabinet.html";
        })
        .catch(error => {
            document.getElementById("message").innerText = error.message;
        });
});

// Добавляем Enter для поля пароля
document.getElementById("password").addEventListener("keydown", function (event) {
    if (event.key === "Enter") {
        event.preventDefault(); // Предотвращаем лишние действия
        document.getElementById("loginForm").dispatchEvent(new Event("submit")); // Запускаем submit
    }
});