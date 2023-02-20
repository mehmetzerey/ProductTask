const form = document.getElementById('register-form');

form.addEventListener('submit', (event) => {
    event.preventDefault();

    const formData = new FormData(form);
    const data = {};

    for (let [key, value] of formData.entries()) {
        data[key] = value;
    }

    const url = 'http://localhost:5001/api/auth/Register';
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
                window.location.href = '/Identity/Login/Index';
            } else {
                throw new Error('Bir hata oluştu.');
            }
        })
        .catch(error => {
            alert(error.message);
        });
});