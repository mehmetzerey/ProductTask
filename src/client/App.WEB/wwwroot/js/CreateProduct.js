const form = document.getElementById('product-create-form');
const token = localStorage.getItem('token');

form.addEventListener('submit', (event) => {
    event.preventDefault();

    const name = document.getElementById('Name').value;
    const code = document.getElementById('Code').value;
    const price = document.getElementById('Price').value;
    const file = document.getElementById('files').files[0]; // files arrayinden ilk dosyayı alıyoruz

    const formData = new FormData();
    formData.append('Name', name);
    formData.append('Code', code);
    formData.append('Price', price);
    formData.append('FileLink', file);

    const url = 'http://localhost:5001/api/Product';
    const options = {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token
        },
        body: formData
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
