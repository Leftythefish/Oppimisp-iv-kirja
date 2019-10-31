// JavaScript source code
var a1
function Auto(rekisterinumero) {
    this.Rekisterinumero = rekisterinumero;
    this.Nopeus = 0;
    this.toString = function () {
        return `Auton tiedot: ${this.Rekisterinumero} ja ${this.Nopeus}`
    }
    this.Kiihdyta = function () {
        this.Nopeus++;
    }
}
function luo() {
    let rekkari = $("#rkr").val();
    a1 = new Auto(rekkari);
    console.log("Auto luotu: " + a1.toString());
    console.dir(a1);
}
function kiihdyta() {
    a1.Kiihdyta();
    console.log("Autoa kiihdetty: " + a1.toString());
    console.dir(a1);
}
function tallenna() {
    let a1json = JSON.stringify(a1);
    console.log(a1json);
    localStorage.munauto = a1json;
}
function lueauto() {
    let tempa1 = JSON.parse(localStorage.munauto);
    console.dir(tempa1);
    a1 = new Auto(tempa1.Rekisterinumero);
    a1.Nopeus = tempa1.Nopeus;
    console.dir(a1);
}