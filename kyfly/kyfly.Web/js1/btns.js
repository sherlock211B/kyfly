$(function () {
    var url = window.location.href;
    var str = url.split('/');
    var name = str[str.length - 1];
    $.getJSON('../../ashx/GetButton.ashx?pageName=' + name, function (msg) {
 
        //var str = msg.split(',');
        if($("#add") != null)
            $("#add").hide();
        if ($("#edit") != null)
            $("#edit").hide();
        if ($("#del") != null)
            $("#del").hide();
        if ($("#search") != null)
            $("#search").hide();
    //    alert(msg);
        
        for (var i = 0; i < msg.length; i++) {
         //   str += '&nbsp;&nbsp;<a href="javascript:void(0)" onclick="' + msg[i].BtnCode + '()"><span class="' + msg[i].Icon + '">&nbsp;</span>' + msg[i].ButtonName + '</a>';
            if (msg[i].BtnCode == "add" && $("#add") != null)
                $("#add").show();
            else if (msg[i].BtnCode == "edit" && $("#edit") != null)
                $("#edit").show();
            else if (msg[i].BtnCode == "del" && $("#del") != null)
                $("#del").show();
            else if (msg[i].BtnCode == "search" && $("#search") != null)
                $("#search").show();
        //    alert(msg[i].BtnCode);
        }
 
    })
});


function GetRequest() {

    var url = location.search; //获取url中"?"符后的字串
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);
        }
    }
    return theRequest;
}

function opendd28() {
    //alert("1");
    document.getElementById("uploadwin").src = "/Common/upload_deal/uploadmodel.aspx?d=1";
    //alert("2");
    $('#dd28').dialog('open');
}
function closedd28() {
    $('#dd28').dialog('close');
}

function printDiv(myDiv) {

    //var newstr = document.all.item(myDiv).innerHTML; 
    var newstr = document.getElementById(myDiv).innerHTML;
    //alert(newstr);
    var oldstr = document.body.innerHTML;
    document.body.innerHTML = newstr;
    window.print();
    document.body.innerHTML = oldstr;
    return false;
}
