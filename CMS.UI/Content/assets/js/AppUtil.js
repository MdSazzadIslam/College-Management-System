var AppUtil = {
    ShowErrors: function (message, id, pos) {
        alert(message + " " + id + " " + pos);
        //$("#login-pinno").notify("Please enter pin no", { position: "top center" });
        //$("#" + id).notify("asasdf", { position: pos });
        $("#login-pinno").notify("idoot", { position: "top center" });
        //$("#nid").notify("Please enter National ID", { position: "top center" });
    },

    MakeAjaxCall: function (url, type, data, SuccessCall, ErrorCall) {

        $.ajax({
            type: type,
            //async: false,
            url: url,
            dataType: "json",
            cache: false,
            //headers: header,
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: SuccessCall,
            error: ErrorCall

        });


    },
    MakeAjaxCallJSONAntifergery: function (url, type, data, header, SuccessCall, ErrorCall) {

        $.ajax({
            type: type,
            //async: false,
            url: url,
            dataType: "json",
            cache: false,
            headers: header,
            contentType: 'application/json; charset=utf-8',
            data: data,
            success: SuccessCall,
            error: ErrorCall

        });


    },
    MakeAjaxCallsForAntiForgery: function (url, type, data, SuccessCall, ErrorCall) {

        $.ajax({
            async: false,
            type: type,
            //headers:headers,
            url: url,
            dataType: "json",
            //contentType: "'application/x-www-form-urlencoded; charset=utf-8'",
            cache: false,
            data: data,
            success: SuccessCall,
            error: ErrorCall

        });
    },


    GetIdValue: function (id) {
        //
        if (($("#" + id)).value == "") {
            return "";
        }
        else
            return $("#" + id).val();

    },

    getDateTime: function (id) {
        return $('#' + id).datepicker('getDate');
    },
    ParseDateTime: function (dateString) {

        var myDate = moment(dateString);
        return myDate.format("DD/MM/YYYY hh:mm A");
    },
    ParseDate: function (dateString) {

        var myDate = moment(dateString);
        return myDate.format("DD/MM/YYYY");
    },
    ParseDateINMMDDYYYY: function (dateString) {

        var myDate = moment(dateString);
        return myDate.format("MM/DD/YYYY");
    },
    ShowSuccess: function (message) {

        $.notify(message, "success");
    },

    ShowError: function (message) {
        $.notify(message, "error");
    },
    ShowErrorOnControl: function (message, controlID, position) {


        $("#" + controlID).notify(message, { position: position });
    },
    ShowLoadingModal: function () {
        $("#loadingModal").modal({ backdrop: 'static' });
    },

    HideLoadingModal: function () {
        $("#loadingModal").modal('hide');
    },

    ShowWaitingDialog: function (message) {
        waitingDialogManager.show(message, { dialogSize: 'sm', progressType: 'success' });
        //waitingDialogManager.show();
    },

    HideWaitingDialog: function () {
        waitingDialogManager.hide()
    },
    ///type: Success,Info,Warning,Error
    ShowNotification: function (type, message) {

        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        Command: toastr[type](message)
    },
    GenerateLoadingTable: function (cols, rows) {
        let html = '<table class="table table-bordered">';
        for (let i = 0; i < cols; i++) {
            html += '<tr>'
            //print the row
            for (let r = 0; r < rows; r++) {
                html += '<th scope="col"><span class="content-placeholder" >&nbsp;</span></th>'
            }
            html += '</tr>'
        }
        html += '</table>'
        return html;
    },
    //hide loading row by selecting loading_row.hide()
    GenerateLoadingRow: function (cols, rows) {
        let html = '';
        for (let i = 0; i < cols; i++) {
            html += '<tr class="loading_row">'
            //print the row
            for (let r = 0; r < rows; r++) {
                html += '<th scope="col"><span class="content-placeholder" >&nbsp;</span></th>'
            }
            html += '</tr>'
        }
        return html;
    }

}