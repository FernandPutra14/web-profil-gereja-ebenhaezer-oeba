$(document).ready(function () {
    $.fancybox.defaults.buttons = [
        'slideShow',
        'share',
        'zoom',
        'fullScreen',
        'download',
        'thumbs',
        'close',
        'delete'
    ];

    $.fancybox.defaults.btnTpl.delete = '<button data-fancybox-delete class="fancybox-button fancybox-button--delete" title="Delete"><i class="fa fa-trash"></i></button>';

    // CLick tombol Delete
    $(document).on('click', '[data-fancybox-delete]', function () {
        var instance = $.fancybox.getInstance();
        var current = instance.current;
        var fotoId = $(current.opts.$orig).data('id');

        //Memperlihatkan konfirmasi modal
        $('#deleteModal').modal('show');
    });

    //Inisialisasi Fancybox dengan custom tombol hapus
    $('[data-fancybox="gallery"]').fancybox();
});