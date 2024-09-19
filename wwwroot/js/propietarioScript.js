
//Filtrado por dni

let tablaPropietarios = document.querySelector("#tablaPropietarios");
let filtrarPorDni = document.querySelector("#filtrarPorDni");
let buscarPropietarioDNI = document.querySelector("#buscarPropietarioDNI");
buscarPropietarioDNI.addEventListener("input", (e) => {
    if(e.target.value === ""){
        rellenarTablaPropietarios();
    }
})
buscarPropietarioDNI.addEventListener("focus",(e) => {
    if(e.target.value === ""){
        buscarPropietarioApellido.value = "";
        buscarPropietarioEmail.value = "";
        rellenarTablaPropietarios();
    }
})
filtrarPorDni.addEventListener("click", (e) => {
    let dni = buscarPropietarioDNI.value;
    axios.get("http://localhost:5203/Propietario/GetPropietariosPorDni", {params:{dni: dni}})
    .then(res => {
        let Propietarios = res.data;
        llenarTabla(tablaPropietarios, Propietarios);
    })
    .catch(err => console.log(err));
})



//Filtrado por apellido
let buscarPropietarioApellido = document.querySelector("#buscarPropietarioApellido");
buscarPropietarioApellido.addEventListener("input", (e) => {
    if(e.target.value === ""){
        rellenarTablaPropietarios();
    }
})
buscarPropietarioApellido.addEventListener("focus", (e) => {
    if(e.target.value === ""){
        buscarPropietarioDNI.value = "";
        buscarPropietarioEmail.value = "";
        rellenarTablaPropietarios();
    }
})


let filtrarPorApellido = document.querySelector("#filtrarPorApellido");
filtrarPorApellido.addEventListener("click", () => {
    let Apellido = buscarPropietarioApellido.value;
    axios("http://localhost:5203/Propietario/GetPropietariosPorApellido", {params: {Apellido: Apellido}})
    .then(res => {
        let elementos = res.data;
        llenarTabla(tablaPropietarios, elementos);
    })
    .catch(err => console.log(err));
}) 

//Filtrado por email
let buscarPropietarioEmail = document.querySelector("#buscarPropietarioEmail");
buscarPropietarioEmail.addEventListener("input", (e) => {
    if(e.target.value === ""){
        rellenarTablaPropietarios();
    }
})
buscarPropietarioEmail.addEventListener("focus",(e) => {
    if(e.target.value === ""){
        buscarPropietarioDNI.value = "";
        buscarPropietarioApellido.value = "";
        rellenarTablaPropietarios();
    }
})
let filtrarPorEmail = document.querySelector("#filtrarPorEmail");
filtrarPorEmail.addEventListener("click", () => {
    let Email = buscarPropietarioEmail.value;
    axios("http://localhost:5203/Propietario/GetPropietariosPorEmail", {params: {Email: Email}})
    .then(res => {
        let elementos = res.data;
        llenarTabla(tablaPropietarios, elementos);
    })
    .catch(err => console.log(err));
}) 

const rellenarTablaPropietarios = () => {
    axios("http://localhost:5203/Propietario/GetPropietarios")
    .then(res => {
        llenarTabla(tablaPropietarios, res.data);
    })
    .catch(err => console.log(err));
}

const llenarTabla = (tabla, elementos) => {
    tabla.innerHTML = "";
    let maqueta = "";
    for(let e of elementos){
        maqueta += `<tr>
                <td>${e.dni}</td>
                <td>${e.nombre}</td>
                <td>${e.apellido}</td>
                <td>${e.telefono}</td>
                <td>${e.correo}</td>
                <td class="d-flex align-items-center w-auto">
                    <a class="btn btn-editbt mx-1" href="/Propietario/Editar/${e.idPropietario}">
                        <i class="fa-solid fa-pen-to-square"></i>
                    </a>
                    <form action="/Propietario/Borrar" class="mx-1" method="post">
                        <input type="hidden" name="id" value="${e.idPropietario}" />
                        <button type="submit" class="btn btn-borrarbt" title="Borrar">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </form> 
                    <a class="btn btn-detallebt mx-1" href="/Propietario/Detalle/${e.idPropietario}">
                        <i class="fa-solid fa-list"></i>
                    </a>
                </td>
            </tr>`
    }
    tabla.innerHTML = maqueta;
}