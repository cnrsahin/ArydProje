$(document).ready(function () {
    // OrderLine Güncelleme Ekranı MODAL
    $(function () {
        const placeModal = $('#placeModal');
        const url = '/OrderLine/Update/';
        $(document).on('click', '#orderLineUpdate', function (evet) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { id: id }).done(function (data) {
                placeModal.html(data);
                placeModal.find(".modal").modal('show');
            }).fail(function () {
                alert("Bir hata oluştu.");
            });
        });
    });

    // OrderHeader Güncelleme Ekranı MODAL
    $(function () {
        const placeModal = $('#placeModal');
        const url = '/OrderHeader/Update/';
        $(document).on('click', '#headerUpdate', function (evet) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { id: id }).done(function (data) {
                placeModal.html(data);
                placeModal.find(".modal").modal('show');
            }).fail(function () {
                alert("Bir hata oluştu.");
            });
        });
    });

    // OrderLine Ekleme Ekranı MODAL
    $(function () {
        const placeModal = $('#placeModal');
        const url = '/OrderLine/Add/';
        $(document).on('click', '#lineAdd', function (evet) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { orderHeaderId: id }).done(function (data) {
                placeModal.html(data);
                placeModal.find(".modal").modal('show');
            }).fail(function () {
                alert("Bir hata oluştu.");
            });
        });
    });
});