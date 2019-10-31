
var lista = [];
var a1

function Topic(title, description, timeestimate, timespent, source, startdate, end_date, inprogress) {
    this.Title = title;
    this.Description = description;
    this.TimeEstimate = timeestimate;
    this.TimeSpent = timespent;
    this.Source = source;
    this.StartLearningDate = startdate;
    this.CompletionDate = end_date;
    this.InProgress = inprogress; 
}
function luo() {
    let title = $("#name").val();
    let description = $("#desc").val();
    let timeestimate = $("#time_est").val();
    let timespent = $("#time_spent").val();
    let source = $("#source").val();
    let startdate = $("#start").val();
    let end_date = $("#end").val();
    let inprogress = $("#status").val();

    a1 = new Topic(title, description, timeestimate, timespent, source, startdate, end_date, inprogress);       
    lista.push(a1);
    console.dir(lista);
    tulosta(lista);
}

function tallenna() {
    let loclist = JSON.stringify(lista);
    console.log(loclist);
    localStorage.listsave = loclist;     
    }    

function lataa() {
    let temp1 = JSON.parse(localStorage.listsave);
    console.dir(temp1);
    for (var i = 0; i < temp1.length; i++) {
        t1 = new Topic(temp1[i].Title, temp1[i].Description, temp1[i].TimeEstimate, temp1[i].TimeSpent, temp1[i].Source, temp1[i].StartLearningDate, temp1[i].CompletionDate, temp1[i].InProgress);
        lista.push(t1);
    }  
    tulosta(lista);
}
function tulosta() {
    document.getElementById("lista").innerHTML = "";
    for (var i = 0; i < lista.length; i++) {
        var topic = lista[i];
        document.getElementById("lista").innerHTML +=
            "<tr>"
            + "<td>" + topic.Title + "</td>"
            + "<td>" + topic.Description + "</td>"
            + "<td>" + topic.TimeEstimate + "</td>"
            + "<td>" + topic.Source + "</td>"
            + "<td>" + topic.StartLearningDate + "</td>"
            + "<td>" + topic.CompletionDate + "</td>"
            + "<td>" + topic.InProgress + "</td>"
            + "</tr>";
    }
}
function lisaavakio() {  
    document.getElementById("name").value = "Testiotsikko";
    document.getElementById("desc").value = "Kuvaus tahan";
    document.getElementById("time_est").value = "01:00";
    document.getElementById("time_spent").value = "01:00";
    document.getElementById("source").value = "jokulahde.com";
    document.getElementById("start").value = "2019-09-29";
    document.getElementById("end").value = "2019-09-29";
    document.getElementById("status").value = true;
}