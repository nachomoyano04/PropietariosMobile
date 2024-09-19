//Filtrado por dni

let tablaInquilinos = document.querySelector("#tablaInquilinos");
let filtrarPorDni = document.querySelector("#filtrarPorDni");
let buscarInquilinoDNI = document.querySelector("#buscarInquilinoDNI");
buscarInquilinoDNI.addEventListener("input", (e) => {
    if(e.target.value === ""){
        rellenarTablaInquilinos();
    }
})
buscarInquilinoDNI.addEventListener("focus",(e) => {
    if(e.target.value === ""){
        buscarInquilinoApellido.value = "";
        buscarInquilinoEmail.value = "";
        rellenarTablaInquilinos();
    }
})
filtrarPorDni.addEventListener("click", (e) => {
    let dni = buscarInquilinoDNI.value;
    axios.get("http://localhost:5203/Inquilino/GetInquilinosPorDni", {params:{dni: dni}})
    .then(res => {
        let inquilinos = res.data;
        llenarTabla(tablaInquilinos, inquilinos);
    })
    .catch(err => console.log(err));
})



//Filtrado por apellido
let buscarInquilinoApellido = document.querySelector("#buscarInquilinoApellido");
buscarInquilinoApellido.addEventListener("input", (e) => {
    if(e.target.value === ""){
        rellenarTablaInquilinos();
    }
})
buscarInquilinoApellido.addEventListener("focus", (e) => {
    if(e.target.value === ""){
        buscarInquilinoDNI.value = "";
        buscarInquilinoEmail.value = "";
        rellenarTablaInquilinos();
    }
})


let filtrarPorApellido = document.querySelector("#filtrarPorApellido");
filtrarPorApellido.addEventListener("click", () => {
    let Apellido = buscarInquilinoApellido.value;
    axios("http://localhost:5203/Inquilino/GetInquilinosPorApellido", {params: {Apellido: Apellido}})
    .then(res => {
        let tablaInquilinos = document.querySelector("#tablaInquilinos");
        let elementos = res.data;
        llenarTabla(tablaInquilinos, elementos);
    })
    .catch(err => console.log(err));
}) 

//Filtrado por email
let buscarInquilinoEmail = document.querySelector("#buscarInquilinoEmail");
buscarInquilinoEmail.addEventListener("input", (e) => {
    if(e.target.value === ""){
        rellenarTablaInquilinos();
    }
})
buscarInquilinoEmail.addEventListener("focus",(e) => {
    if(e.target.value === ""){
        buscarInquilinoDNI.value = "";
        buscarInquilinoApellido.value = "";
        rellenarTablaInquilinos();
    }
})
let filtrarPorEmail = document.querySelector("#filtrarPorEmail");
filtrarPorEmail.addEventListener("click", () => {
    let Email = buscarInquilinoEmail.value;
    axios("http://localhost:5203/Inquilino/GetInquilinosPorEmail", {params: {Email: Email}})
    .then(res => {
        let tablaInquilinos = document.querySelector("#tablaInquilinos");
        let elementos = res.data;
        llenarTabla(tablaInquilinos, elementos);
    })
    .catch(err => console.log(err));
}) 

const rellenarTablaInquilinos = () => {
    axios("http://localhost:5203/Inquilino/GetInquilinos")
    .then(res => {
        llenarTabla(tablaInquilinos, res.data);
    })
    .catch(err => console.log(err));
}


const llenarTabla = (tabla, elementos) => {
    tabla.innerHTML = "";
    let maqueta = ""; 
    for(let i of elementos){
        maqueta += `<tr>
            <td>${i.dni}</td>
            <td>${i.apellido}</td>
            <td>${i.nombre}</td>
            <td>${i.telefono}</td>
            <td>${i.correo}</td>
            <td class="d-flex align-items-center w-auto">
                <a href="/Inquilino/Editar/${i.idInquilino}" class="btn btn-editbt mx-1">
                    <i class="fa-solid fa-pen-to-square"></i>
                </a>
                <form asp-action="Borrar" class="mx-1" method="post" style="display:inline;">
                    <input type="hidden" name="id" value="${i.idInquilino}" />
                    <button type="submit" class="btn btn-borrarbt" title="Borrar">
                        <i class="fa-solid fa-trash"></i>
                    </button>
                </form>
                <a class="btn btn-detallebt mx-1" href="/Inquilino/Detalle/${i.idInquilino}">
                    <i class="fa-solid fa-list"></i>
                </a>
            </td>
        </tr>`
    }
    tabla.innerHTML = maqueta;
}