const nav_header = document.querySelector(".head");
const section_hero = document.querySelector("#hero");


const observer = new IntersectionObserver(
    (entries) => {
        const ent = entries[0];
        console.log(ent);
        ent.isIntersecting == false ? nav_header.classList.add("stick") :
            nav_header.classList.remove("stick");
    }, {
    root: null,
    rootMargin: "",
    threshold: "",
});

observer.observe(section_hero);