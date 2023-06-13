$(document).ready(function () {
    loadDataTable();
});

let loadDataTable = function () {
    table = $('#Categories').DataTable({
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
            }
        ]
    });
};