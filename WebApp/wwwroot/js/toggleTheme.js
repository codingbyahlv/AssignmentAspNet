const toggleBtn = document.querySelector("#toggle-btn");
const toggleBtnMobile = document.querySelector("#toggle-btn-mobile");
const themeStylesheet = document.querySelector("#theme-stylesheet");

function toggleTheme() {
    if (themeStylesheet.href.includes("light")) { 
        themeStylesheet.href = "/css/site-dark.min.css";
    } else {
        themeStylesheet.href = "/css/site-light.min.css";
    }
}

window.addEventListener("load", function () {
    toggleBtn.addEventListener("click", toggleTheme);
    toggleBtnMobile.addEventListener("click", toggleTheme);
});