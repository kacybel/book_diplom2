const userId = document.cookie.split('; ')
    .find(row => row.startsWith('userId='))
    ?.split('=')[1];

if (!userId) {
    window.location.href = "/index.html";
} else {
    fetch(`/api/auth/user/${userId}`)
        .then(response => response.json())
        .then(data => {
            document.getElementById("username").textContent = data.username;
        })
        .catch(error => console.error("Ошибка загрузки имени:", error));

    fetch(`/api/books?userId=${userId}`, {
        headers: {
            'Accept': 'application/json; charset=utf-8'
        }
    })
        .then(response => {
            console.log("Статус ответа:", response.status);
            return response.json();
        })
        .then(books => {
            console.log("Получено:", books);
            const bookList = document.getElementById("bookList");
            bookList.innerHTML = '';
            books.forEach(book => {
                console.log("Обрабатываем книгу:", book);
                const tr = document.createElement("tr");
                const titleCell = document.createElement("td");
                titleCell.textContent = book.title || 'без названия';
                const noteCell = document.createElement("td");
                noteCell.textContent = book.note || 'нет';
                const ratingCell = document.createElement("td");
                ratingCell.textContent = book.ratingValue || 'не указана';
                tr.appendChild(titleCell);
                tr.appendChild(noteCell);
                tr.appendChild(ratingCell);
                bookList.appendChild(tr);
            });
        })
        .catch(error => {
            console.error("Ошибка загрузки книг:", error);
            document.getElementById("message").textContent = "Ошибка загрузки книг: " + error.message;
        });

    document.getElementById("logout").addEventListener("click", () => {
        document.cookie = "userId=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/";
        window.location.href = "/index.html";
    });

    document.getElementById("addBookForm").addEventListener("submit", function (event) {
        event.preventDefault();
        const title = document.getElementById("title").value;
        const note = document.getElementById("note").value;
        if (!title) {
            document.getElementById("message").textContent = "Введите название";
            return;
        }

        console.log("Отправляем:", { title, note, userId: parseInt(userId) });

        fetch("/api/books", {
            method: "POST",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            body: JSON.stringify({
                title: title,
                note: note,
                userId: parseInt(userId)
            })
        })
            .then(response => {
                if (!response.ok) throw new Error("Ошибка добавления книги");
                return response.json();
            })
            .then(data => {
                document.getElementById("message").textContent = data.message;
                closeModal();
                location.reload();
            })
            .catch(error => {
                document.getElementById("message").textContent = "Ошибка: " + error.message;
            });
    });
}

function openModal() {
    document.getElementById("addBookModal").style.display = "block";
}

function closeModal() {
    document.getElementById("addBookModal").style.display = "none";
    document.getElementById("addBookForm").reset();
}