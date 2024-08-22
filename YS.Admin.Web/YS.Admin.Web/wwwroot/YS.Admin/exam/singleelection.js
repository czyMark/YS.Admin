var arLetter = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"];
$(function () {
    $(".ExamaAdd").click(function () {
        var value = $("#txt_timu").val().trim();
        if (value == "") {
            return false;
        }
        add_timu(value)
    })

    $(".ExamRemov").click(function () {
        remove_timu()
    })
})
function remove_timu() {
    $("#fields tr:last").remove()
}
function add_timu(value) {
    var index = $("#fields tr").length;
    var st = arLetter[index] + " . " + value;
    appeddata(index, st);

    $("#txt_timu").val("")
}
function selection(value) {
    console.log(value)
    $("#answer").val(value);
}

function appeddata(index, st) {
    var value = $("#answer").val();
    var checkstr = '';
    if (value.toLowerCase().indexOf(arLetter[index].toLowerCase()) != -1) {
        checkstr = 'checked="checked"';
    }
    $("#fields").append('<tr><td>&nbsp;<div class="exam-radio"><input onclick="selection(\'' + arLetter[index] + '\')" type="radio" name="radio-p-1" id="f' + index + '" ' + checkstr + ' ><label for="f' + index + '"  class="cr">' + st + '</label><input type="hidden" name="timulist" value="' + st + '"></div></td></tr>');
}
function mrData(datas) {
    console.log(datas)
    if (datas.trim().length > 0) {
        var strdata = datas.replaceAll(',em,', '▲');
        var arrdata = strdata.split('▲');
        for (var i = 0; i < arrdata.length; i++) {
            appeddata(i, arrdata[i]);
        }
    }
}