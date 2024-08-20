// Counter Chart Jemaat Section
const counters = document.querySelectorAll(".counters span");
const container = document.querySelector(".counters");

let activated = false;

window.addEventListener("scroll", () => {
    const containerTop = container.getBoundingClientRect().top + window.scrollY;
    const triggerPoint = containerTop - window.innerHeight + 100;

    if (window.scrollY > triggerPoint && !activated) {
        counters.forEach(counter => {
            counter.innerText = '0';
            const target = parseInt(counter.dataset.count);
            const duration = 2000;
            const increment = target / (duration / 16.67);

            let count = 0;
            const startTime = performance.now();

            function updateCount(timestamp) {
                const elapsedTime = timestamp - startTime;

                if (elapsedTime < duration) {
                    count = Math.min(increment * (elapsedTime / 16.67), target);
                    counter.innerText = Math.ceil(count);
                    requestAnimationFrame(updateCount);
                } else {
                    counter.innerText = target;
                }
            }

            requestAnimationFrame(updateCount);
            activated = true;
        });
    } else if (
        window.scrollY < triggerPoint - 500 || window.scrollY === 0
    ) {
        if (activated) {
            counters.forEach(counter => {
                counter.innerText = '0';
            });
            activated = false;
        }
    }
});