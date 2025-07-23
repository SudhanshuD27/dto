$(document).ready(function () {
    FetchManagerDetails();
});

$("#modalbtn").click(function () {
    $("#exampleModal").modal('show');
    $('#savebtn').show();
    $('#updbtn').hide();
    $('#id_div').hide();
    $('#mgrform')[0].reset();
});

function FetchManagerDetails() {
    $.ajax({
        url: '/ManagerHandler/FetchManagers',
        type: 'GET',
        success: function (res) {
            let ob = '';
            $.each(res, function (i, item) {
                ob += '<tr>';
                ob += `<td>${item.managerId}</td>`;
                ob += `<td>${item.mname}</td>`;
                ob += `<td>
                    <button class="btn btn-sm btn-primary" onclick="EditManager(${item.managerId})">Edit</button>
                    <button class="btn btn-sm btn-danger ms-2" onclick="DeleteManager(${item.managerId})">Delete</button>
                </td>`;
                ob += '</tr>';
            });
            $('#mgrdata').html(ob);
        }
    });
}

$("#savebtn").click(function () {
    var obj = $("#mgrform").serialize();
    $.ajax({
        url: '/ManagerHandler/AddManager',
        type: 'POST',
        data: obj,
        success: function () {
            $('#mgrform')[0].reset();
            $("#exampleModal").modal('hide');
            FetchManagerDetails();
        },
        error: function () {
            alert('Error while adding manager');
        }
    });
});

function EditManager(id) {
    $.ajax({
        url: '/ManagerHandler/GetManagerById?id=' + id,
        type: 'GET',
        success: function (res) {
            $('#exampleModal').modal('show');
            $('#savebtn').hide();
            $('#updbtn').show();
            $('#id_div').show();

            $('#ManagerId').val(res.managerId);
            $('#Mname').val(res.mname);
        }
    });
}

$("#updbtn").click(function () {
    var obj = $("#mgrform").serialize();
    $.ajax({
        url: '/ManagerHandler/UpdateManager',
        type: 'POST',
        data: obj,
        success: function () {
            $('#mgrform')[0].reset();
            $("#exampleModal").modal('hide');
            FetchManagerDetails();
        }
    });
});

function DeleteManager(id) {
    if (confirm("Are you sure?")) {
        $.ajax({
            url: '/ManagerHandler/DeleteManager?id=' + id,
            type: 'POST',
            success: function (res) {
                alert(res);
                FetchManagerDetails();
            },
            error: function () {
                alert("Delete failed. Error logged.");
            }
        });
    }
}

$("#closemodal").click(function () {
    $('#mgrform')[0].reset();
});
