var ServerData = null;

$(document).ready(function () {

    ServerData = $("#customInput").data("value");

    //$("#ddlJobs").append($('<option default selected disabled value="1" > one </option>').html('select the job'));

    ServerData.forEach(elm => {
        $('#ddlJobs').append($('<option><?option>').html(elm))

        console.log(elm)
    })

    //$('.mdb-select').materialSelect();
    $('select').selectpicker();
   

});


function selected() {
    
    console.log(document.getElementsByClassName("selected"))
}




console.log(ServerData)

