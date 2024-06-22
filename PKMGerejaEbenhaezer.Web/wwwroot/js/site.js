//Toggle Bang
var menuBtn = document.querySelector('.main-navbar .menu-btn');
var menuList = document.querySelector('.main-navbar .nav-list');
var menuListItems = document.querySelectorAll('.nav-list li a');

menuBtn.addEventListener('click', function () {
    menuBtn.classList.toggle('active');
    menuList.classList.toggle('active');
});

for (var i = 0; 1 < menuListItems.length; i++) {
    menuListItems[i].addEventListener('click', menuItemClicked)
}

function menuItemClicked() {
    menuBtn.classList.remove('active');
    menuList.classList.remove('active');
}