import { Pacijent } from "./pacijent.js";
import {Soba} from "./soba.js"

export class Klinika{

    constructor(id, naziv, brSoba, sobe){
        this.id = id;
        this.naziv = naziv;
        this.brSoba = brSoba;
        this.kontejner = null;
        this.sobe = sobe;
    }

    dodajSobu(sob){
        this.sobe.push(sob);
    }

    crtajKliniku(host){

        if(!host)
            throw new Exception("Roditeljski element ne postoji");
        
        this.kontejner = document.createElement("div");
        this.kontejner.classList.add("kontejner");
        host.appendChild(this.kontejner);

        this.crtajFormu(this.kontejner);
        this.crtajSobe(this.kontejner);

    }

    crtajFormu(host){

        const kontForma = document.createElement("div");
        kontForma.className = "kontForma";
        host.appendChild(kontForma);

        const kontLabele = document.createElement("div");
        kontLabele.className = "kontLabele";
        kontForma.appendChild(kontLabele);

        var elLabela = document.createElement("h3");
        elLabela.innerHTML = "Ime klinike: " + this.naziv;
        kontLabele.appendChild(elLabela);

        elLabela = document.createElement("h3");
        elLabela.innerHTML = "Broj soba: " + this.brSoba;
        kontLabele.appendChild(elLabela);

        const kontDugmici = document.createElement("div");
        kontDugmici.className = "kontDugmici";
        kontForma.appendChild(kontDugmici);

        var elDugme = document.createElement("button");
        elDugme.innerHTML = "Dodaj sobu";
        elDugme.value = this.id;
        elDugme.className = "dugmeSoba"
        kontDugmici.appendChild(elDugme);

        var modal = document.getElementById("myModal");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        var sub = document.getElementsByName("submit")[0];

        sub.onclick = function(event) {
            var brs = document.getElementsByName("brs")[0].value;
            var brk = document.getElementsByName("brk")[0].value;

            console.log(brs + " " + brk);
            var div = document.getElementsByClassName("kontSobe")[sub.value - 2];

            console.log(sub.value)
            fetch("https://localhost:5001/Klinika/KreirajSobu/" + sub.value, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    brSobe: brs,
                    brKreveta: brk
                })
            }).then(p => {
                if (p.ok) {
                    p.json().then(q => {
                        var novaSoba = new Soba(q.id, q.brSobe, q.brKreveta, []);
                        //this.sobe.push(novaSoba);
                        novaSoba.crtajSobu(div);    
                    });
                }
                else if (p.status == 400){
                    alert("Klinika je zauzeta.");
                }
            }).catch(p => {
                alert("Greška prilikom upisa.");
            });

            sub.value = null;
            modal.style.display = "none";
        }

        span.onclick = function() {
            modal.style.display = "none";
          }
          
          // When the user clicks anywhere outside of the modal, close it
        window.onclick = function(event) {
            if (event.target == modal) {
              modal.style.display = "none";
            }
        }

        elDugme.onclick = (ev) => {
            document.getElementsByName("brs")[0].value = "";
            document.getElementsByName("brk")[0].value = "";
            //console.log(elDugme.value);
            sub.value = elDugme.value;
            modal.style.display = "block";
        }

    }

    crtajSobe(host){

        const kontSobe = document.createElement("div");
        kontSobe.className = "kontSobe";
        host.appendChild(kontSobe);
        var sobaObj = null;
        var nizPac = [];
        this.sobe.forEach((s, i) => {

            console.log(s.id)

            fetch("https://localhost:5001/Klinika/PreuzmiPacijente/" + s.id).then(p => {
                    p.json().then(data => {
                            if(data.length != 0){
                            console.log(data);

                            nizPac = [];

                            data.forEach(pacijent => {
                                var pac1 = new Pacijent(pacijent.id, pacijent.ime, pacijent.prezime, pacijent.jmbg, pacijent.bolest, pacijent.stanje);
                                nizPac.push(pac1);
                            });

                            sobaObj = new Soba(s.id, s.brSobe, s.brKreveta, nizPac);
                            sobaObj.crtajSobu(kontSobe);
                        }
                        else{
                        sobaObj = new Soba(s.id, s.brSobe, s.brKreveta, []);
                        sobaObj.crtajSobu(kontSobe);
                        }
                    });
            });
        });
    }

}