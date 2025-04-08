// Получаем userId из куки
const userId = document.cookie.split('; ')
    .find(row => row.startsWith('userId='))
    ?.split('=')[1];

if (!userId) {
    window.location.href = "/index.html"; // Если нет куки, возвращаем на логин
} else {
    fetch(`/api/auth/user/${userId}`, {
        method: "GET"
    })
        .then(response => response.json())
        .then(data => {
            document.getElementById("username").innerText = data.username;
        })
        .catch(() => {
            window.location.href = "/index.html";
        });

    document.getElementById("logout").addEventListener("click", () => {
        document.cookie = "userId=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/";
        window.location.href = "/index.html";
    });
}