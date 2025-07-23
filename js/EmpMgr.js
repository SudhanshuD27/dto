$(document).ready(function () {
    FetchEmpDetails();
});

$("#modalbtn").click(function () {
    $("#exampleModal").modal('show');
    $('#savebtn').show();
    $('#updbtn').hide();
    $('#id_div').hide();
});

$("#savebtn").click(function () {
    var obj = $("#empdets").serialize();

    $.ajax({
        url: '/EmployeeHandler/AddEmployees',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded;charset=utf-8',
        data: obj,
        success: function () {
            alert('Added Successfully');
            $('#empdets')[0].reset();
            $("#exampleModal").modal('hide');
            FetchEmpDetails();
        },
        error: function () {
            alert('Error while adding');
        }
    });
});

function FetchEmpDetails() {
    $.ajax({
        url: '/EmployeeHandler/FetchEmps',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            var ob = '';
            $.each(res, function (index, item) {
                ob += '<tr>';
                ob += `<td>${item.eid}</td>`;
                ob += `<td>${item.ename}</td>`;
                ob += `<td>${item.email}</td>`;
                ob += `<td>${item.esalary}</td>`;
                ob += `<td>${item.managerName}</td>`;
                ob += `<td><a class="btn btn-sm btn-primary editbtn" onClick="EditEmp(${item.eid})">Edit</a></td>`;
                ob += '</tr>';
            });
            $("#empdata").html(ob);
        },
        error: function () {
            alert('Error while fetching employee data');
        }
    });
}

function EditEmp(id) {
    $.ajax({
        url: '/EmployeeHandler/GetEmpById?id=' + id,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (res) {
            $('#exampleModal').modal('show');
            $('#savebtn').hide();
            $('#updbtn').show();
            $('#id_div').show();

            $('#eid').val(res.eid);
            $('#ename').val(res.ename);
            $('#email').val(res.email);
            $('#esalary').val(res.esalary);
            $('#ManagerId').val(res.managerId);
        },
        error: function () {
            alert('Error while fetching employee');
        }
    });
}

$('#updbtn').click(function () {
    var obj = $('#empdets').serialize();
    $.ajax({
        url: '/EmployeeHandler/UpdateEmployee',
        type: 'POST',
        data: obj,
        success: function () {
            $('#exampleModal').modal('hide');
            FetchEmpDetails();
            $('#empdets')[0].reset();
        },
        error: function () {
            alert('Error while updating');
        }
    });
});

$("#closemodal").click(function () {
    $('#empdets')[0].reset();
});



