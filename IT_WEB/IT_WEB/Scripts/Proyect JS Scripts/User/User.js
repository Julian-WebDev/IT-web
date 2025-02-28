function update() {
    let formData = new FormData(document.getElementById('user_form'));
    const data = Object.fromEntries(formData);

    console.log(data);
    $.ajax({
        url: updateURL,
        type: 'POST',
        data: data,
        success: function (response) {
            if (response == 1) {
                console.log("It works");
                Swal.fire({
                    title: "Good job!",
                    text: "User updated!",
                    icon: "success"
                });
            }
            else if (response == 2) {
                Swal.fire({
                    title: "Ups...",
                    text: "Something went wrong, try again",
                    icon: "error"
                });
            }
            else {
                Swal.fire({
                    title: "Ups...",
                    text: "Please fill all the spaces",
                    icon: "info"
                });
            }
        },
        error: function (response) {
            Swal.fire({
                title: "Ups...",
                text: "Something went wrong, try again",
                icon: "error"
            });
        }
    });
}

const checkbox = document.getElementById('check_password');
const input = document.getElementById('password');

// Add an event listener to the checkbox
checkbox.addEventListener('change', function () {
    // Check if the checkbox is checked
    if (checkbox.checked) {
        input.type = 'text'; // Set initial value or any specific value
    } else {
        input.type = 'password';
        // Change the input type to 'text' when checkbox is unchecked
    }
});