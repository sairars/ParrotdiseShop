$(document).ready(function () {
    let container = $('#Categories');
    loadDataTable(container);
    container.on("click", ".js-delete", confirmDeleteCategory);
});

let loadDataTable = function (container) {
    table = container.DataTable({
        order: [[1, 'asc']],
        ajax: {
            url: '/api/categories',
            dataSrc: ''
        },
        columns: [
            {
                data: 'name',
                render: function (data, type, category) {
                    return `<a href="/Categories/Edit/${category.id}">${data}</a>`;
                }
            },
            {
                data: 'displayOrder'
            },
            {
                data: "id",
                render: function (data) {
                    return `<button class="btn btn-danger js-delete" data-category-id="${data}"><i class="bi bi-trash me-2"></i></button>`;
                }
            }
        ]
    });
};

let confirmDeleteCategory = function () {
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
            deleteCategory($(this));
        }
    })
};

let deleteCategory = function (button) {
    let categoryId = button.attr("data-category-id");
    $.ajax({
        url: "/api/categories/" + categoryId,
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