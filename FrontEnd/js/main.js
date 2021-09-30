import {Klinika} from "./klinika.js"

// const klinika1 = new Klinika("Pedijatrija", 5);
// klinika1.crtajKliniku(document.body);

// const klinika2 = new Klinika("Ortopedija", 10);
// klinika2.crtajKliniku(document.body);

fetch("https://localhost:5001/Klinika/PreuzmiKlinike").then(p => {
    p.json().then(data => {
        data.forEach(klinika => {
            const kl1 = new Klinika(klinika.id, klinika.naziv, klinika.brojSoba, klinika.sobe);
            kl1.crtajKliniku(document.body);
        });
    });
});
