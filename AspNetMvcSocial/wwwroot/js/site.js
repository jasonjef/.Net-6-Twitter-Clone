const dropdown = document.querySelector('.dropdown')
const lside = document.querySelector('.lsidepp')


lside.addEventListener('click', () => {
    if (dropdown.style.display == "none") {
        dropdown.style.transition = "all 0.5s ease"
        dropdown.style.display = "flex"
    } else {
        dropdown.style.display = "none"
    }
});