document.addEventListener("DOMContentLoaded", function () {

    var alerts = document.querySelectorAll('.alert');
    alerts.forEach(function (alert) {
        setTimeout(function () {
            var bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 4000);
    });

    var addToCartButtons = document.querySelectorAll('form[action*="AddItemCart"] button[type="submit"]');
    addToCartButtons.forEach(function (btn) {
        btn.addEventListener('click', function () {
            this.innerHTML = 'Eklendi!';
            this.classList.replace('btn-success', 'btn-secondary');
        });
    });

});