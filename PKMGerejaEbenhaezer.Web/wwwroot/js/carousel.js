//Slider (menggunakan template owl-carousel)
$('.fernand').owlCarousel({
    loop: true,
    autoplay: true,
    autoplayTimeout: 8000,
    margin: 10,
    nav: true,
    navText: [
        "<i class='ri-arrow-left-s-fill'></i>",
        "<i class='ri-arrow-right-s-fill'></i>"
    ],
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1000: {
            items: 4
        }
    }
});

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
                nav: false,
                navText: [
                    "<i class='ri-arrow-left-s-fill'></i>",
                    "<i class='ri-arrow-right-s-fill'></i>"
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


