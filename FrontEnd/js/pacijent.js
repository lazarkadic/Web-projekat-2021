

export class Pacijent{

    constructor(id, i, p, jmbg, b, s){
        this.id = id;
        this.ime = i;
        this.prezime = p;
        this.jmbg = jmbg;
        this.bolest = b;
        this.stanje = s;
        this.kontejnerPacijent = null;
    }

    crtajPacijenta(host){
        this.kontejnerPacijent = document.createElement("div");
        this.kontejnerPacijent.className = "kontejnerPacijent";
        host.appendChild(this.kontejnerPacijent); 
        
        var pomDiv = document.createElement("div");
        pomDiv.className = "pomDiv";
        pomDiv.innerHTML = `${this.ime} ${this.prezime} ${this.bolest} ${this.stanje}`; 
        this.kontejnerPacijent.appendChild(pomDiv);

        var dugmeDiv = document.createElement("div");
        dugmeDiv.className = "dugmeDiv";
        this.kontejnerPacijent.appendChild(dugmeDiv); 

        var dugmeIzmeni = document.createElement("button");
        dugmeIzmeni.innerHTML = "Izmeni";
        dugmeIzmeni.value = this.id;
        dugmeDiv.appendChild(dugmeIzmeni);

        var dugmeObrisi = document.createElement("button");
        dugmeObrisi.innerHTML = "Obrisi";
        dugmeObrisi.value = this.id;
        dugmeDiv.appendChild(dugmeObrisi);


        dugmeObrisi.onclick = (ev) =>{

            fetch("https://localhost:5001/Klinika/IzbrisiPacijenta/" + dugmeObrisi.value, {
                    method: "POST"
                }).then(p => {
                    if (p.ok) {
                        //alert("Uspesno obrisan pacijent.")
                        dugmeObrisi.parentElement.parentElement.remove();


                    }
                    else if (p.status == 400){
                        alert("Soba je zauzeta.");
                    }
                }).catch(p => {
                    alert("Greška prilikom upisa.");
                });
            
        }

        var modal = document.getElementById("myModal1");

        var span = document.getElementsByClassName("close1")[0];

        var sub = document.getElementsByName("submit1")[0];

        span.onclick = function() {
            modal.style.display = "none";
        }
        
        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function(event) {
            if (event.target == modal) {
            modal.style.display = "none";
            }
        }

        dugmeIzmeni.onclick = (ev) => {

            document.getElementsByName("im")[0].value = this.ime;
            document.getElementsByName("pr")[0].value = this.prezime;
            document.getElementsByName("jm")[0].value = this.jmbg;
            document.getElementsByName("bo")[0].value = this.bolest;
            document.getElementsByName("st")[0].value = this.stanje;
            modal.style.display = "block";

            var div = dugmeIzmeni.parentElement.previousSibling;

            sub.onclick = (ev) => {
                //alert(sub.value)
                
                this.ime = document.getElementsByName("im")[0].value;
                this.prezime = document.getElementsByName("pr")[0].value;
                this.jmbg = document.getElementsByName("jm")[0].value;
                this.bolest = document.getElementsByName("bo")[0].value;
                this.stanje = document.getElementsByName("st")[0].value;
    
                fetch("https://localhost:5001/Klinika/IzmeniPacijenta", {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        id: this.id,
                        ime: this.ime,
                        prezime: this.prezime,
                        jmbg: this.jmbg,
                        bolest: this.bolest,
                        stanje: this.stanje
                    })
                }).then(p => {
                    if (p.ok) {
                        div.innerHTML = `${this.ime} ${this.prezime} ${this.bolest} ${this.stanje}`;
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

    }
}