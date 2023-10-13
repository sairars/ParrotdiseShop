$(document).ready(function () {
    let container = $('#Users');
    loadDataTable(container);
    container.on("click", ".js-delete", confirmDeleteUser);
});

let loadDataTable = function (container) {
    table = container.DataTable({
        order: [[1, 'asc']],
        ajax: {
            url: '/admin/api/applicationusers',
            dataSrc: ''
        },
        columns: [
            {
                data: 'name'
            },
            {
                data: 'email'
            },
            {
                data: "phoneNumber"
            },
            {
                data: "role"
            }
        ]
    });
};

let confirmDeleteUser = function () {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            deleteUser($(this));
        }
    })
};

let deleteUser = function (button) {
    let categoryId = button.attr("data-user-id");
    $.ajax({
        url: "/admin/api/applicationusers/" + userId,
        method: "DELETE"
    })
        .done(function (data) {
            table.ajax.reload();
            toastr.success(data);
        })
        .fail(function (data) {
            toastr.error(data);
        });
}