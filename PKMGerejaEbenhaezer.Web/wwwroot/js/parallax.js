function adjustBackgroundPosition() {
    var scrollPosition = window.pageYOffset;
    var elements = ['hero'];

    elements.forEach(function (id) {
        var element = document.getElementById(id);
        if (element) {
            element.style.backgroundPositionY = -(scrollPosition * 0.5) + 'px';
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    adjustBackgroundPosition();
});

document.addEventListener('scroll', function () {
    adjustBackgroundPosition();
});
