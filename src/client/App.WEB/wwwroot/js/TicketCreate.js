const form = document.getElementById('ticket-create-form');
const token = localStorage.getItem('token');

form.addEventListener('submit', (event) => {
    event.preventDefault();

    const formData = new FormData(form);
    const data = {};

    for (let [key, value] of formData.entries()) {
        data[key] = value;
    }

    const url = 'http://localhost:5001/api/Ticket';
    const options = {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token
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