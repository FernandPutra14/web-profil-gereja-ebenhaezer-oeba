//Slider Pendeta Section
$('.fernand').owlCarousel({
    loop: true,
    autoplay: false,
    autoplayTimeout: 8000,
    margin: 10,
    nav: true,
    navText: [
        "<i class='ri-arrow-left-s-line'></i>",
        "<i class='ri-arrow-right-s-line'></i>"
    ],
    responsive: {
        0: {
            items: 1
        },
        690: {
            items: 2
        },
        1040: {
            items: 3
        },
        1370: {
            items: 4
        }
    }
});
//Akhir Slider Pendeta Section


//Slider Warta dan Pengumuman Section
$(document).ready(function () {
    function setCarousel() {
        if ($(window).width() >= 1200) {
            $('.fajar').owlCarousel({
                loop: false,
                autoplay: false,
                margin: 10,
                nav: false,
                items: 3,
                mouseDrag: false,
                touchDrag: false
            });
        } else {
            $('.fajar').owlCarousel({
                loop: true,
                autoplay: false,
                autoplayTimeout: 8000,
                margin: 10,
                nav: true,
                navText: [
                    "<i class='ri-arrow-left-s-line'></i>",
                    "<i class='ri-arrow-right-s-line'></i>"
                ],
                responsive: {
                    0: {
                        items: 1
                    },
                    750: {
                        items: 2
                    },
                    1200: {
                        items: 2
                    },
                }
            });
        }
    }

    setCarousel();

    $(window).resize(function () {
        $('.fajar').trigger('destroy.owl.carousel');
        $('.fajar').find('.owl-stage-outer').children().unwrap();
        $('.fajar').removeClass("owl-center owl-loaded owl-text-select-on");

        setCarousel();
    });
});
//Akhir Slider Warta dan Pengumuman Section