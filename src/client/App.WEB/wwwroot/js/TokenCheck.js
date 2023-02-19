document.addEventListener("DOMContentLoaded", function () {
    const token = localStorage.getItem('token');

    const xhr = new XMLHttpRequest();
    const url = 'http://localhost:5001/api/Test';
    xhr.open('GET', url, true);
    xhr.setRequestHeader('Authorization', `Bearer ${token}`);

    xhr.onload = function () {
        if (xhr.status === 200) {
            const decoded = jwt_decode(token);
            if (decoded.role == "member") {
                window.location.href = '/Customer/Home/Index';
            }
            else if (decoded.role == "admin") {
                window.location.href = '/Admin/Home/Index';
            }
        } else if (xhr.status === 401) {
            window.location.href = '/Identity/Login/Index';
        }
    };

    xhr.send();
});