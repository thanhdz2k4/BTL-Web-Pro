// Lưu dữ liệu vào localStorage
function saveData() {
    const status = document.querySelector("select").value;
    const description = document.querySelector('input[value="Nội dung tổng quan"]').value;
    const teacherName = document.querySelector('input[value="Lê Hữu Dũng"]').value;

    // Lấy danh sách file từ localStorage
    const files = JSON.parse(localStorage.getItem("uploadedFiles")) || [];

    // Lấy thời gian hiện tại
    const currentDate = new Date();
    const dueDate = currentDate.toLocaleString('vi-VN', { 
        hour: '2-digit', 
        minute: '2-digit', 
        hour12: false 
    }) + ' | ' + currentDate.toLocaleDateString('vi-VN');

    const data = {
        status,
        description,
        teacherName,
        dueDate,
        files
    };

    localStorage.setItem("classData", JSON.stringify(data));
    alert("Đã lưu thay đổi!");
    displayDueDate(dueDate);
}

// Hiển thị Due Date lên trang
function displayDueDate(dueDate) {
    const dueDateElement = document.querySelector(".due-date");
    if (dueDateElement) {
        dueDateElement.textContent = `Due Date: ${dueDate}`;
    }
}

// Tải dữ liệu từ localStorage khi trang được mở
function loadData() {
    const savedData = localStorage.getItem("classData");

    if (savedData) {
        const { status, description, teacherName, dueDate, files } = JSON.parse(savedData);

        document.querySelector("select").value = status;
        document.querySelector('input[value="Nội dung tổng quan"]').value = description;
        document.querySelector('input[value="Lê Hữu Dũng"]').value = teacherName;

        if (dueDate) {
            displayDueDate(dueDate);
        }

        if (files && files.length > 0) {
            displayFiles(files);
        }
    }
}

// Hiển thị danh sách file
function displayFiles(files) {
    const container = document.querySelector(".document-buttons");
    container.innerHTML = ""; // Xóa các file cũ trước khi hiển thị

    files.forEach((file, index) => {
        const fileButton = document.createElement("button");
        fileButton.className = "btn btn-outline-secondary m-1";
        fileButton.textContent = file.name;
        fileButton.onclick = () => downloadFile(file);
        container.appendChild(fileButton);
    });
}

// Lưu file vào localStorage
function saveFiles(files) {
    const fileList = Array.from(files).map(file => ({ name: file.name, content: URL.createObjectURL(file) }));
    localStorage.setItem("uploadedFiles", JSON.stringify(fileList));
    displayFiles(fileList);
}

// Tải file xuống khi bấm vào nút
function downloadFile(file) {
    const a = document.createElement("a");
    a.href = file.content;
    a.download = file.name;
    a.click();
}

// Xử lý khi người dùng tải file lên
document.querySelector("#fileInput").addEventListener("change", (event) => {
    saveFiles(event.target.files);
});

// Thêm sự kiện khi nhấn nút Save
document.addEventListener("DOMContentLoaded", () => {
    loadData();
    const saveButton = document.querySelector(".btn-primary");
    saveButton.addEventListener("click", saveData);
});
