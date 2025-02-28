$('#btn_login').click(function (e) {
    e.preventDefault();

    let formData = new FormData(document.getElementById('login_form'));
    const data = Object.fromEntries(formData);

    $.ajax({
        url: loginURL,
        type: 'POST',
        data: data,
        success: function (response) {
            if (response == 1) {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Welcome",
                    showConfirmButton: false,
                    timer: 1000
                });
                setTimeout(function () {
                    window.location.href = homeURL;
                }, 2000);
                
            }
            else {
                Swal.fire({
                    title: "Ups...",
                    text: "Incorrect credentials or user does not exist",
                    icon: "error"
                });
                console.log("It didn't work", response)
            }
        },
        error: function (response) {
            Swal.fire({
                title: "Ups...",
                text: "Something went wrong, try again",
                icon: "error"
            });
        }
    })
})
