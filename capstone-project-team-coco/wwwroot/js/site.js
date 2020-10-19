// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let programTitle = document.getElementsByClassName("programTitle");
let i;

for (i = 0; i < programTitle.length; i++) {
    programTitle[i].addEventListener("click", function () {
        this.classList.toggle("active");

        let content = this.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = content.scrollHeight + "px";
        }
    });
}

let addNewProgramTitle = document.getElementsByClassName("addNewProgramTitle");
let p;

for (p = 0; p < addNewProgramTitle.length; p++) {
    addNewProgramTitle[p].addEventListener("click", function () {
        this.classList.toggle("active");

        let content = this.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = content.scrollHeight + "px";
        }
    });
}