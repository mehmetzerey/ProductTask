const form = document.getElementById('register-form');

form.addEventListener('submit', (event) => {
    event.preventDefault();

    const formData = new FormData(form);
    const data = {};

    for (let [key, value] of formData.entries()) {
        data[key] = value;
    }

    const url = 'http://localhost:5001/api/auth/RegisterWithRole?role=1';
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    fetch(url, options)
        .then(response => {
            if (response.ok) {
                alert("Eklendi")
            } else {
                alert("Hata")
            }
        })
        .catch(error => {
            alert(error.message);
        });
});