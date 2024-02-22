/* global bootstrap: false */

var rowChoose;

(() => {
    'use strict'
    const tooltipTriggerList = Array.from(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    tooltipTriggerList.forEach(tooltipTriggerEl => {
        new bootstrap.Tooltip(tooltipTriggerEl)
    })
})()

$("#btnEditJobPost").on("click", function () {
    window.history.back();
});

$(document).ready(function () {

    if ($("#checkDefine").is(":checked")) {
        $('#btnCreatePost').attr("class", "btn btn-success font-semibold text-sm m-2");
        $('#btnCreatePost').attr("href", "/NguoiTuyenDung/TaoTinTuyenDungDetail?handler=CreatePost");
    } else {
        $('#btnCreatePost').attr("class", "btn btn-secondary font-semibold text-sm m-2");
        $('#btnCreatePost').removeAttr("href");
    }
    console.log("disabled btn create");
});


$("#checkDefine").on("click", function () {
    debugger

    if ($("#checkDefine").is(":checked")) {
        $('#btnCreatePost').attr("class", "btn btn-success font-semibold text-sm m-2");
        $('#btnCreatePost').attr("href", "/NguoiTuyenDung/TaoTinTuyenDungDetail?handler=CreatePost");
    } else {
        $('#btnCreatePost').attr("class", "btn btn-secondary font-semibold text-sm m-2");
        $('#btnCreatePost').removeAttr("href");
    }
});


$(document).ready(function () {
    $("#idSelectJobNames").on("change", function () {
        var value = $(this).val().toLowerCase();
        var jobName = $('#idSelectJobNames').val();
        console.log('jobName = ' + jobName);

        $('#idDowloadReport').attr('href', '/NguoiTuyenDung/DanhSachTinDang?handle=DowloadReport&JobName=' + jobName);
        $("#tableJobPosts tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        
        });
    });
});


$('#idDowloadReport').on('click', function () {
    $('#idFormFilter').submit();
});

$('.cEditTime').on('click', function () {
    var row = $(this).closest('tr');
    var idJob = row.find('.cJobName .cId').val().trim();
    var jobName = row.find('.cJobName').text().trim();
    var createDate = row.find('.cCreateDate').text().trim();
    var deadline = row.find('.cDeadline').val().trim();
    debugger
    var createDateFormat = formatDate(new Date(createDate));


    $('#iJobName').text(jobName);
    $('#idDateCreate').val(new Date(createDate).toISOString().split('T')[0]);
    var newDeadline = new Date(deadline);
    const year = newDeadline.getUTCFullYear();
    const month = String(newDeadline.getUTCMonth() + 1).padStart(2, '0');
    const day = String(newDeadline.getUTCDate()).padStart(2, '0');
    const formattedDate = `${year}-${month}-${day}`;

    $('#idDateSubmit').val(formattedDate);
    $('#iIdJobPost').val(idJob);
});

function formatDate(date) {
    return date.getDate() + "/" + (parseInt(date.getMonth()) + 1) + "/" + date.getFullYear();
}

$('#iGiaHan').on('click', function () {
    var idJob = $('#iIdJobPost').val();
    var dealine = $('#idDateSubmit').val();

    $('#fUpdateDeadline').submit();


    //var data = {
    //    id: idJob,
       
    //};


    //$.ajax({
    //    url: "http://localhost:5000/api/JobPosts/updateDealineJobPost",
    //    contentType: "application/json; charset=utf-8",
    //    type: "post",
    //    dataType: "json",
    //    data: { id: idJob },
    //    success: function (result, status, xhr) {
    //        console.log('OKI');
    //    },
    //    error: function (xhr, status, error) {
    //        console.log(xhr);
    //        console.log('LOI RUI');
    //    }
    //});
});


$('.cDeleteJobPost').on('click', function () {
    var row = $(this).closest('tr');
    var id = row.find('.cId').val();
    $('#iIdDeleteJobPost').val(id);
});

$('#iDeleteJob').on('click', function () {
    var id = $('#iIdDeleteJobPost').val();
    var row;

    $('#iTableJobPost tr').each(function () {
        if ($(this).find('.cId').val() === id) {
            row = $(this);
        }
    });
    row.remove();
    $('#exampleModalDelete').modal('toggle');

    $.ajax({
        url: "http://localhost:5000/api/JobPosts/deleteJobPost/" + id,
        contentType: "application/json",
        type: "delete",
        success: function (result, status, xhr) {
            console.log('OKI');
        },
        error: function (xhr, status, error) {
            console.log(xhr);
            console.log('LOI RUI');
        }
    });
});


$('.cEditStatus').on('click', function () {
    debugger
    var row = $(this).closest('tr');
    var status = row.find('.cStatus').val();
    var id = row.find('.cId').val();

    $('#iIdStatusJobPost').val(id);
    $('#iStatusJobPost').val(status);

    if (status) {
        $('#iEditStatusContent').text("Bạn có muốn (ẨN) tin tuyển dụng này không?");
    } else {
        $('#iEditStatusContent').text("Bạn có muốn (HIỂN THỊ) tin tuyển dụng này không?");
    }
});


$('#iChangeStatus').on('click', function () {
    debugger
    var status = $('#iStatusJobPost').val();
    var id = $('#iIdStatusJobPost').val();


    $.ajax({
        url: "http://localhost:5000/api/JobPosts/changeStatus/" + id,
        contentType: "application/json",
        type: "put",
        success: function (result, status, xhr) {
            $('#exampleModalEditStatus').modal('toggle');
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log(xhr);
            console.log('LOI RUI');
        }
    });

});


//$("#idSelectJobNamesUngTuyen").on("change", function () {
//    var value = $(this).val().toLowerCase();

//    //$('#idDowloadReport').attr('href', '/NguoiTuyenDung/DanhSachTinDang?handle=DowloadReport&JobName=' + jobName);
//    $("#tableJobPostsUngTuyen tr").filter(function () {
//        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)

//    });
//});



$('.cEditStatusHsUngTuyen').on('click', function () {
    debugger
    var row = $(this).closest('tr');
    var status = row.find('.cStatus').val();
    var id = row.find('.cId').val();

    $('#iIdStatusHsUngTuyen').val(id);
    $('#iStatusHsUngTuyen').val(status);

    if (status) {
        $('#iEditStatusContent').text("Bạn có muốn thay đổi trạng thái của hồ sơ này thành CHƯA PHÊ DUYỆT không?");
    } else {
        $('#iEditStatusContent').text("Bạn có muốn thay đổi trạng thái của hồ sơ này thành ĐÃ PHÊ DUYỆT không?");
    }
});


$('#iChangeStatusHsUngTuyen').on('click', function () {
    debugger
    var id = $('#iIdStatusHsUngTuyen').val();


    $.ajax({
        url: "http://localhost:5000/api/JobApplicant/changeStatus/" + id,
        contentType: "application/json",
        type: "put",
        success: function (result, status, xhr) {
            $('#editStatusHoSoUngTuyen').modal('toggle');
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log(xhr);
            console.log('LOI RUI');
        }
    });

});





$('.cDeleteHsUngTuyen').on('click', function () {
    var row = $(this).closest('tr');
    var id = row.find('.cId').val();
    $('#iIdDeleteHsUngTuyen').val(id);
});

$('#iDeleteHsUngTuyen').on('click', function () {
    var id = $('#iIdDeleteHsUngTuyen').val();
    var row;

    $('#iTableHsUngTuyen tr').each(function () {
        if ($(this).find('.cId').val() === id) {
            row = $(this);
        }
    });


    $.ajax({
        url: "http://localhost:5000/api/JobApplicant/deleteJobApplication/" + id,
        contentType: "application/json",
        type: "delete",
        success: function (result, status, xhr) {
            row.remove();
            $('#modalDeleteHsUngTuyen').modal('toggle');
        },
        error: function (xhr, status, error) {
            console.log(xhr);
            console.log('LOI RUI');
        }
    });
});






    