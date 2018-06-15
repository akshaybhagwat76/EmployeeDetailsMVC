$(document).ready(function () {
    if (employee > 0) {
        $(".btn-block").html("Update");
        getUserProfile(employee);
    }
    getCountries();
});

function getUserProfile(id) {
    $.ajax({
        type: "POST",
        url: "/Employee/getUserProfile",
        data: { id: id },
        dataType: "json",
        success: function (data) {

            var array = data.mealpreference.split(",");

            $("#mealPreferences").find(":checkbox[name^=chk]").filter(function (index, val) {
                if (array.indexOf(this.value) >= 0)
                    return this.checked = true;
            });

            var cnt = String(data.CountryId);
            var ct = String(data.CityId);
            var st = String(data.StateId);

            $('#firstName').val(data.fullname);
            $('#email').val(data.UserName);
            $('#password').val(data.Password);
            $('#birthDate').val(data.birthdate);
            $("#drp1").val(data.CountryId);
            Setregions(data.StateId, data.CountryId, data.CityId);
            $("input:radio[name=rdbGender][value=" + data.gender + "]").attr("checked", true);
        }
    })
}

function getCountries() {
    $.ajax({
        type: "GET",
        url: "/Employee/getCountries",
        dataType: "json",
        success: function (data) {
            (data || []).unshift({ Text: "SELECT", Value: "" });
            let optionHtml = data.map(x => `<option value='${x.Value}'>${x.Text}</option>`);
            $('#drp1').html(optionHtml);
        }
    })
}


function Setregions(selectableId, countryId, City_Id) {
    var cityddl = $('#drp3');
    $('#drp3').empty();
    $("#drp1").val(countryId);
    cityddl.append($('<option></option>').text('SELECT'));
    var id = $("#drp1 option:selected").val();
    $.ajax({
        type: "POST",
        url: "/Employee/getStates",
        data: { id: countryId },
        dataType: "json",
        success: function (data) {
            var cityddl = $('#drp2');
            $("#drp1").val(countryId);
            $('#drp2').empty();
            var Stateshtml = data.map(v=> `<option value='${v.StateId}'>${v.StateName}</option>`)
            cityddl.html(Stateshtml);
            $("#drp2").val(selectableId);
            $.ajax({
                type: "POST",
                url: "/Employee/GetCities",
                data: { id: selectableId },
                dataType: "json",
                success: function (data) {
                    var cityddl = $('#drp3');
                    $('#drp3').empty();
                    cityddl.append($('<option></option>').text('SELECT'));
                    (data || []).unshift({ CityName: "SELECT", CityId: "" });
                    var ctHtml = data.map(h=>`<option value='${h.CityId}'>${h.CityName}</option>`);
                    cityddl.html(ctHtml);
                    $("#drp3").val(City_Id);
                    $("#drp2").val(selectableId);
                    $("#drp1").val(countryId);
                    alert(countryId);
                }
            })
        }
    })
}

$('.btn-block').on("click", function () {

    var ItemIdArray = [];
    $("#mealPreferences").find("input[type=checkbox]:checked").each(function () {
        ItemIdArray.push($(this).val());
    });
    var isValid = true;

    $('#myForm .validate').each(function () {
        let $this = $(this), val = $(this).val();
        if (!val) {
            $this.addClass('error');
            isValid = false;
        }
        else {
            $this.removeClass('error');
        }
    });

    if ($("#mealPreferences").find("input[type=checkbox]:checked").length < 1) {
        $('#lblchkError').addClass('error').text('Select atleast one preference');
        isValid = false;
    } else { $('#lblchkError').removeClass('error').text('') }

    if ($('input[name="rdbGender"]:checked').length == 0) {
        $('#lblrdbError').addClass('error').text('Must select gender');
        isValid = false;
    } else { $('#lblrdbError').removeClass('error').text('') };


    var data = {
        UserId: employee,
        fullname: $('#firstName').val(),
        UserName: $('#email').val(),
        Password: $('#password').val(),
        birthdate: $('#birthDate').val(),
        CountryId: $('#drp1 option:selected').val(),
        StateId: $('#drp2 option:selected').val(),
        CityId: $('#drp3 option:selected').val(),
        gender: $("input[type=radio]:checked").val(),
        mealarr: ItemIdArray
    }

    //isValid = false;

    if (isValid) {
        $.ajax({
            type: "POST",
            url: "/Employee/SaveEmployee",
            data: { userVM: data },
            dataType: "json",
            success: function (response) {
                if (response == "ok") {
                    $('#lblError').addClass('success').text('Profile updated successfully');
                    if (employee == 0) {
                        $('#myForm')[0].reset();
                    }
                }
            }
        })
    }
});

function getStates() {
    var cityddl = $("#drp3");
    $("#drp3").empty();
    cityddl.append($('<option></option>').text('SELECT'));
    debugger;
    var id = $("#drp1 option:selected").val();
    $.ajax({
        type: "POST",
        url: "/Employee/getStates",
        data: { id: id },
        dataType: "json",
        success: function (data) {
            debugger;
            var cityddl = $("#drp2");
            $("#drp2").empty();
            var Stateshtml = data.map(v=> `<option value='${v.StateId}'>${v.StateName}</option>`)
            cityddl.html(Stateshtml);
            cityddl.prepend($('<option selected></option>').text('Please select'));
        }
    })
}

function getCities() {

    var id = $("#drp2 option:selected").val();
    $.ajax({
        type: "POST",
        url: "/Employee/GetCities",
        data: { id: id },
        dataType: "json",
        success: function (data) {
            debugger
            var cityddl = $("#drp3");
            $("#drp3").empty();
            cityddl.append($('<option></option>').text('SELECT'));
            (data || []).unshift({ CityName: "SELECT", CityId: "" });
            var ctHtml = data.map(h=>`<option value='${h.CityId}'>${h.CityName}</option>`);
            cityddl.html(ctHtml);
        }
    })
}