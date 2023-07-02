$(document).ready(function () {
    $("#productForm").on("click", ".js-validate-upload", validateFileUpload);
});


let validateFileUpload = function () {
    let uploadBox = $("#uploadBox");

    if (uploadBox.val() !== "") {
        return true;
    }

    Swal.fire({
        icon: 'error',
        text: 'Please upload an image!'
    });

    return false;
}

tinymce.init({
    selector: 'textarea',
    plugins: 'anchor autolink charmap codesample emoticons lists visualblocks wordcount',
    toolbar: 'undo redo | blocks fontfamily fontsize | bold italic underline strikethrough | align lineheight | numlist bullist indent outdent',
    statusbar: false
});