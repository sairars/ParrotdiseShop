$(document).ready(function () {

    let container = $('#Orders');
  
    loadDataTable(container);
});

let loadDataTable = function (container) {

    let href = $(location).attr('href');
    let selectedStatus = new URL(href).searchParams.get('status');

    if (selectedStatus == null)
        selectedStatus = 'All';

    table = container.DataTable({
        order: [[1, 'asc']],
        ajax: {
            url: `/admin/api/orders/${selectedStatus}`,
            dataSrc: ''
        },
        columns: [
            {
                data: 'id',
                render: function (data) {
                    return `<a href="/admin/orders/details/${data}">${data}</a>`;
                }
            },
            {
                data: 'name'
            },
            {
                /*if anonymous user: email is pulled from orders, 
                otherwise pulled from user record*/

                data: null,
                render: function (data) {
                    if (data.user == null)
                        return data.email;
                    else
                        return data.user.email;
                }
            },
            {
                data: 'phoneNumber'
            },
            {
                data: 'status'
            },
            {
                data: 'total',
                render: function(data) {
                    return `\$${data}`;
                }
            }
        ]
    });

    $('.list-group-item').each(function () {

        let listItem = $(this);
        listItem.removeClass('active');

        let thisStatus = listItem.attr('data-status');

        if (thisStatus == selectedStatus) {
            listItem.addClass('active');
        }
    });
};