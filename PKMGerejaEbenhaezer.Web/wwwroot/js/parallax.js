function adjustBackgroundPosition() {
    var scrollPosition = window.pageYOffset;
    var elements = ['hero', 'wartaus', 'peng-us', 'ibadahus', 'tentangus', 'kontak-us'];

    if (window.matchMedia('(min-width: 969px)').matches) {
        elements.forEach(function (id) {
            var element = document.getElementById(id);
            if (element) {
                element.style.backgroundPositionY = -(scrollPosition * 0.5) + 'px';
            }
        });
    }
}


function saveScrollPosition() {
    sessionStorage.setItem('scrollPosition', window.pageYOffset);
}

//function restoreScrollPosition() {
//    var scrollPosition = sessionStorage.getItem('scrollPosition');
//    if (scrollPosition !== null) {
//        window.scrollTo(0, parseInt(scrollPosition));
//    }
//}

document.addEventListener('DOMContentLoaded', function () {
    if (performance.navigation.type === performance.navigation.TYPE_RELOAD) {
        restoreScrollPosition();
        // Tambahkan event listener untuk memastikan penyesuaian latar belakang setelah posisi scroll dipulihkan
        window.addEventListener('scroll', adjustBackgroundPosition, { once: true });
    } else {
        adjustBackgroundPosition();
    }
});

document.addEventListener('scroll', function () {
    adjustBackgroundPosition();
});

window.addEventListener('beforeunload', function () {
    saveScrollPosition();
});
