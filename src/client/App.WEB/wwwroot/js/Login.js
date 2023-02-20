const loginForm = document.getElementById('login-form');

// Form submit işlemi yakalama
loginForm.addEventListener('submit', function (event) {
    // Formun varsayılan submit işlemini engelleme
    event.preventDefault();

    // Kullanıcı adı ve şifreyi al
    const email = loginForm.elements['email'].value;
    const password = loginForm.elements['password'].value;

    // AJAX isteği gönder
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5001/api/Auth/Login',
        data: JSON.stringify({ email: email, password: password }),
        contentType: 'application/json',
        success: function (response) {
            if (response.isSuccess == true) {
                localStorage.setItem("token", response.data.token);
                const decoded = jwt_decode(response.data.token);
                debugger;
                if (decoded.role == "member") {
                    window.location.href = '/Customer/Home/Index';
                }
                else if (decoded.role == "admin") {
                    window.location.href = '/Admin/Home/Index';
                }
            } else {
                alert('Sorun ile karşılaşıldı.');
            }

        },
        error: function (xhr, status, error) {
            // Login hatası, kullanıcıyı bilgilendir
            alert('Kullanıcı adı veya şifre yanlış');
        }
    });
});