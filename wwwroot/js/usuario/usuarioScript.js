let editarAvatar = document.querySelector("#editarAvatar");
let archivo = document.querySelector("#archivo");
let imagenAvatar = document.querySelector("#imagenAvatar");

editarAvatar.addEventListener("click", () => {
    archivo.click();
});

archivo.addEventListener("change", (event) => {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
            imagenAvatar.src = e.target.result;
        };
        reader.readAsDataURL(file);
    }
});

let borrarAvatar = document.querySelector("#borrarAvatar");
let borrarAvatarInput = document.querySelector("#borrarAvatarInput");
borrarAvatar.addEventListener("click", () => {
    imagenAvatar.src = "/img/Avatar/default.jpg";
    borrarAvatar.disabled = true;
    borrarAvatar.textContent = "borrado";
    borrarAvatarInput.value = "true";
})
