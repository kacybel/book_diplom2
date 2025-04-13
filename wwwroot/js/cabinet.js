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
        .catch(error => console.error("������ �������� �����:", error));

    fetch(`/api/books?userId=${userId}`, {
        headers: {
            'Accept': 'application/json; charset=utf-8'
        }
    })
        .then(response => {
            console.log("������ ������:", response.status);
            return response.json();
        })
        .then(books => {
            console.log("��������:", books);
            const bookList = document.getElementById("bookList");
            bookList.innerHTML = '';
            books.forEach(book => {
                console.log("������������ �����:", book);
                const tr = document.createElement("tr");
                const titleCell = document.createElement("td");
                titleCell.textContent = book.title || '��� ��������';
                const noteCell = document.createElement("td");
                noteCell.textContent = book.note || '���';
                const ratingCell = document.createElement("td");
                ratingCell.textContent = book.ratingValue || '�� �������';
                tr.appendChild(titleCell);
                tr.appendChild(noteCell);
                tr.appendChild(ratingCell);
                bookList.appendChild(tr);
            });
        })
        .catch(error => {
            console.error("������ �������� ����:", error);
            document.getElementById("message").textContent = "������ �������� ����: " + error.message;
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
            document.getElementById("message").textContent = "������� ��������";
            return;
        }

        console.log("����������:", { title, note, userId: parseInt(userId) });

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
                if (!response.ok) throw new Error("������ ���������� �����");
                return response.json();
            })
            .then(data => {
                document.getElementById("message").textContent = data.message;
                closeModal();
                location.reload();
            })
            .catch(error => {
                document.getElementById("message").textContent = "������: " + error.message;
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