const toggleProfile = document.querySelector('.toggle-sidebar-hp');
const showsidebar = document.querySelector('#sidebar');

toggleProfile.addEventListener('click', function () {
    showsidebar.classList.toggle('show');
});

// Menutup sidebar jika klik di luar sidebar
document.addEventListener('click', function (event) {
    // Cek jika sidebar terbuka dan klik di luar sidebar dan tombol toggle
    if (showsidebar.classList.contains('show') && !showsidebar.contains(event.target) && !toggleProfile.contains(event.target)) {
        showsidebar.classList.remove('show');
    }
});

