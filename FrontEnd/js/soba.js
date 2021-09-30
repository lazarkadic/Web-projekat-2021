import { Pacijent } from "./pacijent.js";


export class Soba{

    constructor(id, brSobe, brKreveta, pacijenti){
        this.id = id;
        this.brSobe = brSobe;
        this.brKreveta = brKreveta;
        if(pacijenti.lenght != 0){
            this.pacijenti = pacijenti;
        }
        else {
            this.pacijenti = [];
        }
        this.kontejnerSoba = null;
    }

    crtajSobu(host){
        this.kontejnerSoba = document.createElement("div");
        this.kontejnerSoba.className = "kontejnerSoba";
        host.appendChild(this.kontejnerSoba);

        var kontSoba = this.kontejnerSoba;

        var kontFormaSoba = document.createElement("div");
        kontFormaSoba.className = "kontFormaSoba";
        kontSoba.appendChild(kontFormaSoba);

        var kontFormaSobaLabele = document.createElement("div");
        kontFormaSobaLabele.className = "kontFormaSobaLabele";
        kontFormaSoba.appendChild(kontFormaSobaLabele);

        var kontFormaSobaDugmici = document.createElement("div");
        kontFormaSobaDugmici.className = "kontFormaSobaDugmici";
        kontFormaSoba.appendChild(kontFormaSobaDugmici);

        var sobaLab = document.createElement("label");
        sobaLab.innerHTML = "Soba broj: " + this.brSobe;
        sobaLab.className = "sobaLab";
        kontFormaSobaLabele.appendChild(sobaLab);
        sobaLab = document.createElement("label");
        sobaLab.innerHTML = "Broj kreveta: " + this.brKreveta;
        sobaLab.className = "sobaLab";
        kontFormaSobaLabele.appendChild(sobaLab);

        var dugmePac = document.createElement("button");
        dugmePac.innerHTML = "Dodaj pacijenta";
        dugmePac.value = this.id;
        kontFormaSobaDugmici.appendChild(dugmePac);
        var dugmeSob = document.createElement("button");
        dugmeSob.innerHTML = "Obrisi sobu";
        dugmeSob.value = this.id;
        kontFormaSobaDugmici.appendChild(dugmeSob);


        var kontKreveti = document.createElement("div");
        kontKreveti.className = "kontKreveti";
        kontKreveti.outerHTML = this.id;
        //kontKreveti.innerHTML = ".";
        kontSoba.appendChild(kontKreveti);

        var modal = document.getElementById("myModal1");

        var span = document.getElementsByClassName("close1")[0];

        var sub = document.getElementsByName("submit1")[0];

        var div = null;

        span.onclick = function() {
            modal.style.display = "none";
          }
          
          // When the user clicks anywhere outside of the modal, close it
        window.onclick = function(event) {
            if (event.target == modal) {
              modal.style.display = "none";
            }
        }

        dugmePac.onclick = (ev) => {
            sub.value = dugmePac.value;
            div = dugmePac.parentElement.parentElement.parentElement.lastChild;
            document.getElementsByName("im")[0].value = "";
            document.getElementsByName("pr")[0].value = "";
            document.getElementsByName("jm")[0].value = "";
            document.getElementsByName("bo")[0].value = "";
            document.getElementsByName("st")[0].value = "";
            modal.style.display = "block";

            sub.onclick = (ev) => {
                //alert(sub.value)
                
                var i = document.getElementsByName("im")[0].value;
                var p = document.getElementsByName("pr")[0].value;
                var j = document.getElementsByName("jm")[0].value;
                var b = document.getElementsByName("bo")[0].value;
                var s = document.getElementsByName("st")[0].value;
    
                fetch("https://localhost:5001/Klinika/KreirajPacijenta/" + sub.value, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        ime: i,
                        prezime: p,
                        jmbg: j,
                        bolest: b,
                        stanje: s
                    })
                }).then(p => {
                    if (p.ok) {
                        p.json().then(q => {
                            var novPacijent = new Pacijent(q.id, q.ime, q.prezime, q.jmbg, q.bolest, q.stanje);
                            this.pacijenti.push(novPacijent);
                            novPacijent.crtajPacijenta(div);    
                        });
                    }
                    else if (p.status == 400){
                        alert("Soba je zauzeta.");
                    }
                }).catch(p => {
                    alert("Greška prilikom upisa.");
                });
    
                modal.style.display = "none";
    
            }
        }

        dugmeSob.onclick = (ev) => {
            //alert(dugmeSob.value)

            if(this.pacijenti.length != 0){
                // console.log(this.pacijenti.length)
                    for(let i = 0; i < this.pacijenti.length; i++){
                    fetch("https://localhost:5001/Klinika/IzbrisiPacijenta/" + this.pacijenti[i].id, {
                        method: "POST"
                    }).then(p => {
                        if (p.ok) {
                            console.log("Uspesno obrisan pacijent " + i);

                        }
                        else if (p.status == 400){
                            alert("Bad request.");
                        }
                    }).catch(p => {
                        alert("Greška prilikom upisa.");
                    });
                }
            }
            fetch("https://localhost:5001/Klinika/IzbrisiSobu/" + dugmeSob.value, {
                        method: "POST"
                    }).then(p => {
                        if (p.ok) {
                            console.log("Uspesno obrisana soba " + dugmeSob.value);
                            dugmeSob.parentElement.parentElement.parentElement.remove();
                        }
                        else if (p.status == 400){
                            alert("Bad request.");
                        }
                    }).catch(p => {
                        alert("Greška prilikom upisa.");
                    });

        }


        this.crtajPacijente(kontKreveti);

    }

    crtajPacijente(host){
        if(this.pacijenti.lenght != 0){
            
            this.pacijenti.forEach((element, i) => {
                //console.log("crtam u sobi: "+ this.id + " pacijenta br: " + i + " sa imenom: " + element.ime) 
                element.crtajPacijenta(host);
            });
        }
    }

}