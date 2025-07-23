$(document).ready(function () {
    FetchEmp();
});

$("#modalbtn").click(function () {
    $("#exampleModal").modal('show');
});

$("#savebtn").click(function (e) {
    e.preventDefault();
    var obj = $("#submitform").serialize();

    console.log(obj);
    $.ajax({
        url: '/AjaxNewEmp/AddEmployee', 
        method: 'POST',
        dataType: 'json',
        data: obj,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8',
        success: function () {
            alert('Emp Added Successfully');
            $("#exampleModal").modal('hide');
            FetchEmp();
        },
        error: function () {
            alert('Not loaded');
        }
    });
});

function FetchEmp() {
    $.ajax({
        url: '/AjaxNewEmp/FetchEmp', 
        dataType: 'json',
        type: 'GET',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            var obj = '';
            $.each(response, function (index, item) {
                obj += '<tr>';
                obj += `<td>${item.eid}</td>`;
                obj += `<td>${item.ename}</td>`;
                obj += `<td>${item.email}</td>`;
                obj += `<td>${item.esalary}</td>`;
                obj += `<td><a class="btn btn-sm btn-danger" onclick="DeleteEmp(${item.eid})">Delete</a></td>`;
                obj += '</tr>';
            });

            $("#mydata").html(obj);
        },
        error: function () {
            alert('Fetch failed');
        }
    });
}

function DeleteEmp(id) {
    if (confirm('Are you sure?')) {
        $.ajax({
            url: '/AjaxNewEmp/DelEmp?empid=' + id, 
            dataType: 'json',
            success: function () {
                alert('Emp Deleted Successfully');
                FetchEmp();
            },
            error: function () {
                alert('Delete failed');
            }
        });
    } else {
        alert('Action cancelled');
    }
}

$("#txt").keyup(function () {
    var data = $("#txt").val();
    $.ajax({
        url: '/AjaxNewEmp/SearchEmployeeData?mydata=' + data, 
        dataType: 'json',
        type: 'GET',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            var obj = '';
            $.each(response, function (index, item) {
                obj += '<tr>';
                obj += `<td>${item.eid}</td>`;
                obj += `<td>${item.ename}</td>`;
                obj += `<td>${item.email}</td>`;
                obj += `<td>${item.esalary}</td>`;
                obj += `<td><a class="btn btn-sm btn-danger" onclick="DeleteEmp(${item.eid})">Delete</a></td>`;
                obj += '</tr>';
            });

            $("#mydata").html(obj);
        },
        error: function () {
            alert('Search failed');
        }
    });
});
