$(document).ready(function () {
    loadDataTable();
});

let loadDataTable = function () {
    table = container.DataTable({
        ajax: {
            url: '/api/categories',
            dataSrc: ''
        },
        columns: [
            { data: 'name' },
            { data: 'displayorder' }
        ]
    });
};