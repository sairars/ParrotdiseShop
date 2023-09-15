$(function () {
    $("#orderDetails").on("click", ".js-shipping", updateShippingInfo);
    $("#orderDetails").on("click", ".js-cancel", confirmCancelOrder);
});

let updateShippingInfo = function () {
    let button = $(this);

    Swal.fire({
        title: 'Enter Shipping Details',
        html: `<input type="text" id="carrier" class="swal2-input" placeholder="Carrier">
               <input type="text" id="trackingNumber" class="swal2-input" placeholder="Tracking Number">`,
        confirmButtonText: 'Ship Order',
        focusConfirm: false,
        preConfirm: () => {
            let carrier = Swal.getPopup().querySelector('#carrier').value
            let trackingNumber = Swal.getPopup().querySelector('#trackingNumber').value

            if (!carrier || !trackingNumber) {
                Swal.showValidationMessage(`Please enter Carrier and Tracking Number`)
            }
            return { carrier: carrier, trackingNumber: trackingNumber }
        }
    }).then((result) => {
        let carrier = result.value.carrier;
        let trackingNumber = result.value.trackingNumber;

        let orderId = button.attr("data-order-id");

        let order = {
            id: orderId,
            carrier: carrier,
            trackingNumber: trackingNumber
        };

        let vm = {
            order: order
        };

        $.ajax({
            url: "/admin/orders/ship",
            method: "POST",
            data: vm
        })
            .done(function () {
                location.reload();
            })
            .fail(function () {
                alert("An error occured");
            });
    });
}

let confirmCancelOrder = function () {
    Swal.fire({
        title: 'Are you sure you wish to cancel this order?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, cancel it!'
    }).then((result) => {
        if (result.isConfirmed) {
            cancelOrder($(this));
        }
    });
};

let cancelOrder = function (button) {
    let id = button.attr("data-order-id");

    $.ajax({
        url: "/admin/orders/cancel/" + id,
        method: "POST"
    })
        .done(function () {
            location.reload();
        })
        .fail(function () {
            alert("An error occured");
        });
}


