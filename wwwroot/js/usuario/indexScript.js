let usuariosDadosDeBaja = document.querySelector("#usuariosDadosDeBaja");
usuariosDadosDeBaja.addEventListener("click", () => {
    if(usuariosDadosDeBaja.classList.contains("btn-danger")){
            axios("http://localhost:5203/Usuario/DadosDeBaja")
            .then(res => {
                llenarTablaUsuarios(res.data);
            })
            .catch(err => console.log(err));
            usuariosDadosDeBaja.classList.remove("btn-danger");
            usuariosDadosDeBaja.classList.add("btn-success");
            usuariosDadosDeBaja.textContent = "Dados de alta";
    }else{
        axios("http://localhost:5203/Usuario/DadosDeAlta")
        .then(res => {
            llenarTablaUsuarios(res.data);
        })
        .catch(err => console.log(err));
        usuariosDadosDeBaja.classList.remove("btn-success");
        usuariosDadosDeBaja.classList.add("btn-danger");
        usuariosDadosDeBaja.textContent = "Dados de baja";
    }
})

const llenarTablaUsuarios = (usuarios) => {
    let tablaUsuarios = document.querySelector("#tablaUsuarios");
    tablaUsuarios.innerHTML = "";
    let maqueta = "";
    console.log(usuarios);
    for(let u of usuarios){
        maqueta += 
        `<tr>
            <td>${u.nombre}</td>
            <td>${u.apellido}</td>
            <td>${u.email}</td>
            <td>${u.rol}</td>
            <td><img src="${u.avatar}" class="img-fluid" alt="avatar"></td>
            <td class="align-items-center w-auto">
                    ${!u.estado?`
                    <form action="/Usuario/Alta" class="mx-1" method="post" style="display:inline;">
                        <input type="hidden" name="idUsuario" value="${u.idUsuario}" />
                        <button type="submit" class="btn btn-exitobt" title="Alta">
                            <i class="fa-solid fa-arrow-up" style="color: #44ff00;"></i>
                        </button>
                    </form>`:
                    `
                    <a href="/Usuario/CambiarPassword/${u.idUsuario}" class="btn btn-detallebt mx-1">
                        Cambiar password
                    </a>
                    <a href="/Usuario/Editar/${u.idUsuario}" class="btn btn-editbt mx-1">
                        <i class="fa-solid fa-pen-to-square"></i>
                    </a>
                    <form action="/Usuario/Borrar" class="mx-1" method="post" style="display:inline;">
                        <input type="hidden" name="idUsuario" value="${u.idUsuario}" />
                        <button type="submit" class="btn btn-borrarbt" title="Borrar">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </form>
                    <a class="btn btn-detallebt mx-1" href="/Usuario/Detalle/${u.idUsuario}">
                        <i class="fa-solid fa-list"></i>
                    </a>`}

            </td>
        </tr>`;
    }
    tablaUsuarios.innerHTML = maqueta;
}