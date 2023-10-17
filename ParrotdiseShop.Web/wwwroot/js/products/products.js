$(document).ready(function () {
    let container = $('#Products');
    loadDataTable(container);
    container.on("click", ".js-delete", confirmDeleteProduct);
});

let loadDataTable = function (container) {
    table = container.DataTable({
        order: [[1, 'asc']],
        ajax: {
            url: '/admin/api/products',
            dataSrc: ''
        },
        columns: [
            {
                data: 'name',
                render: function (data, type, product) {
                    return `<a href="/admin/products/edit/${product.id}">${data}</a>`;
                }
            },
            {
                data: 'sku'
            },
            {
                data: 'unitPrice'
            },
            {
                data: 'unitsInStock'
            },
            {
                data: 'category.name'
            },
            {
                data: "id",
                render: function (data) {
                    return `<button class="btn btn-danger js-delete" data-product-id="${data}"><i class="bi bi-trash me-2"></i></button>`;
                }
            }
        ]
    });
};

let confirmDeleteProduct = function () {
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
            deleteProduct($(this));
        }
    })
};

let deleteProduct = function (button) {
    let productId = button.attr("data-product-id");
    $.ajax({
        url: "/admin/api/products/" + productId,
        method: "DELETE"
    })
        .done(function (data) {
            table.ajax.reload();
            toastr.success(data);
        })
        .fail(function (data) {
            toastr.error(data.responseText);
        });
}