$(document).ready(function () {
    let container = $('#Users');
    loadDataTable(container);
    container.on('click', '.js-lockunlock', confirmLockUnlock);
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
                data: 'phoneNumber'
            },
            {
                data: 'role'
            },
            {
                data: null,
                render: function (data) {
                    let today = new Date().getTime();
                    let lockoutEnd = new Date(data.lockoutEnd).getTime();

                    let isLocked = lockoutEnd > today;

                    if (!data.lockoutEnabled) {
                        return;
                    }

                    if (isLocked) {
                        return `<button data-toggle="tooltip" title="Click to unlock" class="btn btn-danger js-lockunlock" data-lock="unlock" data-user-id="${data.id}" data-user-name="${data.name}"><i class="bi bi-lock"></i></button>`;
                    }
                    else {
                        return `<button data-toggle="tooltip" title="Click to lock" class="btn btn-primary js-lockunlock" data-lock="lock" data-user-id="${data.id}" data-user-name="${data.name}"><i class="bi bi-unlock"></i></button>`;
                    }
                }
            }
        ]
    });
};

let confirmLockUnlock = function () {
    let button = $(this);

    let name = button.attr('data-user-name');
    let lockUnlock = button.attr('data-lock');
    
    Swal.fire({
        title: `Are you sure you want to ${lockUnlock} the user below`,
        text: `${name}`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: `Yes, ${lockUnlock} the user`
    }).then((result) => {
        if (result.isConfirmed) {
            lockUnlockUser($(this));
        }
    })
};

let lockUnlockUser = function (button) {
    let userId = button.attr('data-user-id');
    $.ajax({
        url: '/admin/api/applicationusers/' + userId,
        method: "POST"
    })
        .done(function (data) {
            table.ajax.reload();
            toastr.success(data);
        })
        .fail(function (data) {
            toastr.error(data.responseText);
        });
}