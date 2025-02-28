var table;
var u_table;
$(document).ready(function () {
    u_table = $('#t_users').DataTable({
        ajax: {
            url: usersURL,
            type: 'GET',
            datatype: 'json'
        },
        ordering: false,
        info: false,
        resoponsive: true,
        columns: [
            {
                'data': "Id"
            },
            {
                'data': "Name"
            },
            {
                'data': "Lastname"
            },
            {
                'data': "Email"
            },
            {
                'data': "Office"
            },
            {
                'data': 'null',
                'deafultContent': ''
            }
        ],
        columnDefs: [{
            'targets': 5,
            'render': function (data, type, row, meta) {
                return `
                    <a onclick="displayUserForm('${row.Id}')" data-bs-toggle="modal" data-bs-target="#updateModal"><i class="bi bi-pencil-fill"></i></a>
                    <a onclick="deleteUserAlert('${row.Id}', '${row.Name}')"><i class="bi bi-trash3-fill" style="color: #e74c3c;"></i></a>
                `;
            } //End of render section
        }]
    });
});

function displayUserForm(id) {
    let completeURL = `${userDetails}/${id}`;
    $.ajax({
        type: 'GET',
        url: completeURL,
        datatype: 'json',
        success: function (data) {
            //Convert the object data to json
            let jsonData = JSON.stringify(data);
            console.log(data);

            data.data.forEach(item => {
                document.getElementById('ID').value = item.Id;
                document.getElementById('u_name').value = item.Name;
                document.getElementById('u_lastname').value = item.Lastname;
                document.getElementById('u_email').value = item.Email;
                document.getElementById('u_office').value = item.Office;
                
            });
        },
        error: function (xhr, status, error) {
            console.log('The data could not be displayed', error, 'Status:', status)
        }
    })
}

$('#btn_update_user').click(function (e) {
    e.preventDefault();

    let formData = new FormData(document.getElementById('update_form'));

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

                // Reloads the table
                u_table.ajax.reload();
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
                    title: "Ups...!",
                    text: "Please fill all the required spaces",
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
});

$(document).ready(function () {
    table = $('#t_devices').DataTable({
        ajax: {
            url: url,
            type: 'GET',
            datatype: 'json'
        },
        ordering: false,
        info: false,
        resoponsive: true,
        columns: [
            {
                'data': "Id"
            },
            {
                'data': 'AssetType'
            },
            {
                'data': 'ProductCode'
            },
            {
                'data': 'Category'
            },
            {
                'data': 'Make'
            },
            {
                'data': 'SerialNumber'
            },
            {
                'data': 'Taggable'
            },
            {
                'data': 'AllocatedTo'
            },
            {
                'data': 'Po_No'
            },
            {
                'data': 'null',
                'deafultContent': ''
            }
        ],
        columnDefs: [{
            'targets': 9,
            'render': function (data, type, row, meta) {
                return `
                    <a onclick="displayAsset('${row.Id}', '${row.SerialNumber}')" data-bs-toggle="modal" data-bs-target="#update_asset_modal"><i class="bi bi-pencil-fill"></i></a>
                    <a onclick="deleteAssetAlert('${row.Id}', '${row.ProductCode}')"><i class="bi bi-trash3-fill" style="color: #e74c3c;"></i></a>
                `;
            } //End of render section
        }]
    });
});

//Display asset info in the modal
function displayAsset(id, sn) {
    //let url_one = `${assetURL}?param1=${encodeURIComponent(id)}&param2=${encodeURIComponent(sn)}`;
    const url_two = `/Admin/GetAsset?param1=${encodeURIComponent(id)}`;
    $.ajax({
        type: 'GET',
        url: url_two,
        datatype: 'json',
        success: function (data) {
            //Convert the object data to json
            let jsonData = JSON.stringify(data);
            console.log(data);

            data.data.forEach(item => {
                document.getElementById('u_id').value = item.Id;
                document.getElementById('u_type').value = item.AssetType;
                document.getElementById('u_p_code').value = item.ProductCode;
                document.getElementById('u_category').value = item.Category;
                document.getElementById('u_make').value = item.Make;
                document.getElementById('u_sn').value = item.SerialNumber;
                document.getElementById('u_asset_no').value = item.AssetNumber;
                document.getElementById('u_taggable').value = item.Taggable;
                document.getElementById('u_allocated').value = item.AllocatedTo;
                document.getElementById('u_email').value = item.EmailId;
                document.getElementById('u_po_no').value = item.Po_No;
                document.getElementById('u_eol').value = item.EolEos;
                document.getElementById('u_eol_date').value = item.EolEosDate;
            });
        },
        error: function (xhr, status, error) {
            console.log('The data could not be displayed', error, 'Status:', status)
        }
    })
}

// Update asset
$('#btn_update_asset').click(function (e) {
    e.preventDefault();

    let formData = new FormData(document.getElementById('update_asset_form'));
    const data = Object.fromEntries(formData);

    console.log(data);
    $.ajax({
        url: updateAssetURL,
        type: 'POST',
        data: data,
        success: function (response) {
            if (response == 1) {
                console.log("It works");
                Swal.fire({
                    title: "Good job!",
                    text: "Asset updated!",
                    icon: "success"
                });

                // Reloads the table
                table.ajax.reload();
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
                    title: "Ups...!",
                    text: "Please fill all the required spaces",
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
});

function deleteAssetAlert(id, product) {
    Swal.fire({
        title: `Are you sure you want to delete ${product}?`,
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            deleteAsset(id);
        }
    });
}

function deleteAsset(id) {
    $.ajax({
        url: deleteURL,
        type: 'POST',
        data: {
            //email corresponds to the value of the collection the controller will receive
            item: id
        },
        success: function (response) {
            //Here I use the server response
            if (response == 1) {
                Swal.fire({
                    title: "Deleted!",
                    text: "Item deleted!",
                    icon: "success"
                });
                table.ajax.reload();
            }
            else if (response == 2) {
                alert("ERROR")
            }
            console.log(response)
        },
        error: function (error) {
            console.log(error)
        }
    });
}

// Insert new asset
$('#btn_insert').click(function (e) {
    e.preventDefault();

    let formData = new FormData(document.getElementById('insert_form'));

    const data = Object.fromEntries(formData);

    $.ajax({
        url: insertURL,
        type: 'POST',
        data: data,
        success: function (response) {
            if (response == 1) {
                console.log("It works");
                Swal.fire({
                    title: "Good job!",
                    text: "Asset created!",
                    icon: "success"
                });

                // Reset serial number input
                document.getElementById('sn').value = "";

                // Reloads the table
                table.ajax.reload();
            }
            else if (response == 2) {
                console.log("It didn't work")
            }
            else {
                Swal.fire({
                    title: "Ups...!",
                    text: "Please fill all the required spaces",
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
    })
});

$('#btn_reset').click(function (e) {
    e.preventDefault();
    document.getElementById('type').value = "";
    document.getElementById('p_code').value = "";
    document.getElementById('make').value = "";
    document.getElementById('sn').value = "";
    document.getElementById('asset_no').value = 0;
    document.getElementById('allocated').value = "";
    document.getElementById('email').value = "";
    document.getElementById('po_no').value = 0;
    document.getElementById('eol_date').value = "";
});

function deleteUserAlert(id, name) {
    Swal.fire({
        title: `Are you sure you want to delete ${name}'s profile?`,
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            console.log("THE ID IS", id)
            deleteUser(id);
        }
    });
}

function deleteUser(id) {
    $.ajax({
        url: deleteUserURL,
        type: 'POST',
        data: {
            //email corresponds to the value of the collection the controller will receive
            ID: id
        },
        success: function (response) {
            //Here I use the server response
            if (response == 1) {
                Swal.fire({
                    title: "Deleted!",
                    text: "User deleted!",
                    icon: "success"
                });
                u_table.ajax.reload();
            }
            else if (response == 2) {
                alert("ERROR")
            }
            console.log(response)
        },
        error: function (error) {
            console.log(error)
        }
    });
}

$('#btn_add_user').click(function (e) {
    e.preventDefault();

    let formData = new FormData(document.getElementById('new_user_form'));

    const data = Object.fromEntries(formData);

    $.ajax({
        url: addUserURL,
        type: 'POST',
        data: data,
        success: function (response) {
            if (response == 1) {
                console.log("It works");
                Swal.fire({
                    title: "Good job!",
                    text: "User created!",
                    icon: "success"
                });

                // Reset serial number input
                document.getElementById('sn').value = "";

                // Reloads the table
                u_table.ajax.reload();
            }
            else if (response == 2) {
                console.log("It didn't work")
            }
            else {
                Swal.fire({
                    title: "Ups...!",
                    text: "Please fill all the required spaces",
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
    })
});