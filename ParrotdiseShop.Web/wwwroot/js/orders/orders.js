$(document).ready(function () {
    let container = $('#Orders');
    loadDataTable(container);
});

let loadDataTable = function (container) {
    table = container.DataTable({
        order: [[1, 'asc']],
        ajax: {
            url: '/admin/api/orders',
            dataSrc: ''
        },
        columns: [
            {
                data: 'id',
                render: function (data) {
                    return `<a href="/admin/orders/edit/${data}">${data}</a>`;
                }
            },
            {
                data: 'name'
            },
            {
                data: 'user.email'
            },
            {
                data: 'phoneNumber'
            },
            {
                data: 'status'
            },
            {
                data: 'total'
            }
        ]
    });
};