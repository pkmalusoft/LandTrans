var g_js_tabrowloader = 'Loading...';
var g_js_loader = "";
g_js_loader += "<div class=\"g_loader\" style=\"padding: 20px;text-align: center;width: 100%;\">";
g_js_loader += "<div class=\"preloader-wrapper active\">";
g_js_loader += "  <div class=\"spinner-layer spinner-blue-only\" style=\"border-color: #808282;\">";
g_js_loader += "    <div class=\"circle-clipper left\">";
g_js_loader += "      <div class=\"circle\"><\/div>";
g_js_loader += "    <\/div>";
g_js_loader += "    <div class=\"gap-patch\">";
g_js_loader += "      <div class=\"circle\"><\/div>";
g_js_loader += "    <\/div>";
g_js_loader += "    <div class=\"circle-clipper right\">";
g_js_loader += "      <div class=\"circle\"><\/div>";
g_js_loader += "    <\/div>";
g_js_loader += "  <\/div>";
g_js_loader += "<\/div>";
g_js_loader += "<\/div>";

function g_js_getRandomNumber() {
    return Math.floor((Math.random() * 199990) + 1);
}
function g_js_inputfilter(ctrls, mode = 'number') {
    // Allow only alph_nume like : unique profile id
    if (mode == 'alphanum') {
        $(ctrls).on('keypress', function (event) {
            var regex = new RegExp("^[a-zA-Z0-9\_ ]+$");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(ctrls).on('input', function () {
            $(ctrls).val($.trim($(ctrls).val()).replace(/[^a-z0-9\s]/gi, '').replace(/[_\s]/g, ' '));
        });
    }
    else {
        $(ctrls).on('keypress', function (event) {
            var regex = new RegExp("^[0-9\_ ]+$");
            var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        });
        $(ctrls).on('input', function () {
            $(ctrls).val($.trim($(ctrls).val()).replace(/[^0-9\s]/gi, '').replace(/[_\s]/g, ' '));
        });
    }
}
function g_js_pgfocus(ctrl, padding_top = 150, md = '') {
    $('html,body').animate({
        scrollTop: $(ctrl).offset().top - padding_top
    }, 'slow', function () {
        $(ctrl).focus();
    });
    if (md == 'cl') {
        $(ctrl).css('background-color', '#ffeb3b4f');
        setTimeout(function () { $(ctrl).removeAttr('style') }, 2000);
    }
}
function g_js_autocom(url, mode, txtCtrl, MinimumChar, termkey, keyname, valname, dataVal = '', paranameval = '', dataVal2 = '', sel_callBack_op = undefined) {
    $(txtCtrl).autocomplete({
        minLength: MinimumChar,
        // delay: 100,
        autoFocus: false,
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: url,
                data: "{o: { " + termkey + ": '" + $(txtCtrl).val() + "', Mode : '" + mode + "'" + paranameval + " }}",
                dataType: "json",
                success: function (d) {
                    // debugger;
                    if (d.success) {
                        if (d.status == 200) {
                            var ll = [];
                            dataVal = dataVal == '' ? valname : dataVal;
                            $.each(d.data, function (i, o) {
                                ll.push({ 'label': o[keyname], 'value': o[valname], 'did': o[dataVal], 'value2': o[dataVal2] });
                            });
                            response(ll);
                        }
                    }
                },
                error: function (result) {
                }
            });
        },
        select: function (event, ui) {
            if (dataVal != valname) {
                event.preventDefault();
                var vv = ui.item.value;
                var label = ui.item.label;
                $(txtCtrl).attr('data-id', ui.item.did);
                $(txtCtrl).val(label);
                if (sel_callBack_op != undefined) {
                    sel_callBack_op(ui.item.value2);
                }
            }
        }
    });

}
function g_js_bindCountry(ctrl, async_mode=!0) {
    $(ctrl).empty().append('<option selected="selected" disabled  value="">Loading...</option>');
    $.ajax({
        type: "POST",
        url: "/Admin/GetDDLCountry",
        data: '{}',
        async: async_mode,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $(ctrl).empty().append('<option selected="selected" disabled  value="">Select Country</option>');
            $.each(r, function (i, v) {
                $(ctrl).append($("<option></option>").val(v.Value).html(v.Text));
            });
        },
        error: function (data) {
            //alert('Connection Error...');
        }
    });
}
function g_js_bindCityOnChange(ctrl,countryCtrl, async_mode=!0) {
    $(countryCtrl).change(function () {
        var selectedValue = $(countryCtrl).find("option:selected").val();
        $(ctrl).empty().append('<option selected="selected" disabled  value="">Loading...</option>');
        $.ajax({
            type: "POST",
            url: "/Admin/GetDDLCity",
            data: "{SID:'" + selectedValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (r) {
                $(ctrl).empty().append('<option selected="selected"  disabled  value="">Select</option>');
                $.each(r, function (i, v) {
                    $(ctrl).append($("<option></option>").val(v.Value).html(v.Text));
                });

            }
        });
    });
}
function g_js_bindLocationOnChange(ctrl,cityCtrl, async_mode=!0) {
    $(cityCtrl).change(function () {
        var selectedValue = $(cityCtrl).find("option:selected").val();
        $(ctrl).empty().append('<option selected="selected" disabled  value="">Loading...</option>');
        $.ajax({
            type: "POST",
            url: "/Admin/GetDDLLocation",
            data: "{LID:'" + selectedValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (r) {
                $(ctrl).empty().append('<option selected="selected"  disabled  value="">Select</option>');
                $.each(r, function (i, v) {
                    $(ctrl).append($("<option></option>").val(v.Value).html(v.Text));
                });

            }
        });
    });
}
function g_js_bindDesignation(ctrl, async_mode = !0) {
    $(ctrl).empty().append('<option selected="selected" disabled  value="">Loading...</option>');
    $.ajax({
        type: "POST",
        url: "/Admin/GetDDLDesignation",
        data: '{}',
        async: async_mode,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $(ctrl).empty().append('<option selected="selected" disabled  value="">Select</option>');
            $.each(r, function (i, v) {
                $(ctrl).append($("<option></option>").val(v.Value).html(v.Text));
            });
        },
        error: function (data) {
            //alert('Connection Error...');
        }
    });
}
function g_js_bindBranch(ctrl, async_mode = !0) {
    $(ctrl).empty().append('<option selected="selected" disabled  value="">Loading...</option>');
    $.ajax({
        type: "POST",
        url: "/Master/GetDDLBranchMaster",
        data: '{}',
        async: async_mode,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $(ctrl).empty().append('<option selected="selected" disabled  value="">Select</option>');
            $.each(r, function (i, v) {
                $(ctrl).append($("<option></option>").val(v.Value).html(v.Text));
            });
        },
        error: function (data) {
            //alert('Connection Error...');
        }
    });
}
function g_js_bindPackages(ctrl, async_mode = !0) {
    $(ctrl).empty().append('<option selected="selected" disabled  value="">Loading...</option>');
    $.ajax({
        type: "POST",
        url: "/Admin/GetPackages",
        data: '{}',
        async: async_mode,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $(ctrl).empty().append('<option selected="selected" disabled  value="">Select</option>');
            $.each(r, function (i, v) {
                $(ctrl).append($("<option></option>").val(v.PackageID).html(v.PackageType));
            });
        },
        error: function (data) {
            //alert('Connection Error...');
        }
    });
} 
function g_js_bindDepot(ctrl, async_mode = !0) {
    $(ctrl).empty().append('<option selected="selected" disabled  value="">Loading...</option>');
    $.ajax({
        type: "POST",
        url: "/Route/CityMasterSelectDepot",
        data: '{}',
        async: async_mode,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $(ctrl).empty().append('<option selected="selected" disabled  value="">Select Depot</option>');
            $.each(r, function (i, v) {
                $(ctrl).append($("<option></option>").val(v.Value).html(v.Text));
            });
        },
        error: function (data) {
            //alert('Connection Error...');
        }
    });
} 
function g_js_bindCourierStatusHold(ctrl, async_mode = !0) {
    $(ctrl).empty().append('<option selected="selected" disabled  value="">Loading...</option>');
    $.ajax({
        type: "POST",
        url: "/Admin/GetCourierStatusHold",
        data: '{}',
        async: async_mode,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $(ctrl).empty().append('<option selected="selected" disabled  value="">Select</option>');
            $.each(r, function (i, v) {
                $(ctrl).append($("<option></option>").val(v.CourierStatusID).html(v.CourierStatus));
            });
        },
        error: function (data) {
            //alert('Connection Error...');
        }
    });
}
function g_js_GetManufacturerForVehicle(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append("<option value=\"\">Loading...</option>");
    }
    $.ajax({
        type: "Post",
        url: "/Master/GetManufacturerForVehicle",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == 'ddl' || md == 'ddld') {
                        $(ctrl).empty();
                        var m = (md == 'ddl' ? "" : "<option value=\"\">" + bo.Text + "</option>"); $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.VehicleDescription + "\">" + o.VehicleDescription + "</option>";
                        });
                        $(ctrl).append(m);
                    }
                }
                else if (d.status == 203) {
                    if (md == "ddl" || md == 'ddld') {
                        $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    }
                }
                else if (d.status == 401) {
                    $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    window.location.href = '/';
                }
            }
            else {
                $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            }
        },
        error: function (data) {
            $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            console.log('error : g_js_GetManufacturerForVehicle');
        }
    });
}
function g_js_GetInsCompanyForVehicle(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append("<option value=\"\">Loading...</option>");
    }
    $.ajax({
        type: "Post",
        url: "/Admin/GetInsCompanyForVehicle",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == 'ddl' || md == 'ddld') {
                        $(ctrl).empty();
                        var m = (md == 'ddl' ? "" : "<option value=\"\">" + bo.Text + "</option>"); $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.InsuranceCompName + "\">" + o.InsuranceCompName + "</option>";
                        });
                        $(ctrl).append(m);
                    }
                }
                else if (d.status == 203) {
                    if (md == "ddl" || md == 'ddld') {
                        $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    }
                }
                else if (d.status == 401) {
                    $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    window.location.href = '/';
                }
            }
            else {
                $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            }
        },
        error: function (data) {
            $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            console.log('error : g_js_GetInsCompanyForVehicle');
        }
    });
}
function g_js_GetModelForVehicle(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append("<option value=\"\">Loading...</option>");
    }
    $.ajax({
        type: "Post",
        url: "/Master/GetModelForVehicle",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == 'ddl' || md == 'ddld') {
                        $(ctrl).empty();
                        var m = (md == 'ddl' ? "" : "<option value=\"\">" + bo.Text + "</option>"); $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.Model + "\">" + o.Model + "</option>";
                        });
                        $(ctrl).append(m);
                    }
                }
                else if (d.status == 203) {
                    if (md == "ddl" || md == 'ddld') {
                        $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    }
                }
                else if (d.status == 401) {
                    $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    window.location.href = '/';
                }
            }
            else {
                $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            }
        },
        error: function (data) {
            $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            console.log('error : g_js_GetModelForVehicle');
        }
    });
}
function g_js_GetVehicleTypes(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append("<option value=\"\">Loading...</option>");
    }
    $.ajax({
        type: "Post",
        url: "/Master/GetVehicleTypes",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == 'ddl' || md == 'ddld') {
                        $(ctrl).empty();
                        var m = (md == 'ddl' ? "" : "<option value=\"\">" + bo.Text + "</option>"); $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.VehicleTypeID + "\">" + o.VehicleType + "</option>";
                        });
                        $(ctrl).append(m);
                    }
                }
                else if (d.status == 203) {
                    if (md == "ddl" || md == 'ddld') {
                        $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    }
                }
                else if (d.status == 401) {
                    $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    window.location.href = '/';
                }
            }
            else {
                $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            }
        },
        error: function (data) {
            $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            console.log('error : g_js_GetVehicleTypes');
        }
    });
}
function g_js_GetRegisteredUnderForVehicle(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append("<option value=\"\">Loading...</option>");
    }
    $.ajax({
        type: "Post",
        url: "/Master/GetRegisteredUnderForVehicle",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == 'ddl' || md == 'ddld') {
                        $(ctrl).empty();
                        var m = (md == 'ddl' ? "" : "<option value=\"\">" + bo.Text + "</option>"); $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.RegisteredUnder + "\">" + o.RegisteredUnder + "</option>";
                        });
                        $(ctrl).append(m);
                    }
                }
                else if (d.status == 203) {
                    if (md == "ddl" || md == 'ddld') {
                        $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    }
                }
                else if (d.status == 401) {
                    $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
                    window.location.href = '/';
                }
            }
            else {
                $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            }
        },
        error: function (data) {
            $(ctrl).empty().append("<option value=\"\">" + d.status_message + "</option>");
            console.log('error : g_js_GetRegisteredUnderForVehicle');
        }
    });
}

function g_js_GetEmployeeMaster(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append(g_js_tabrowloader);
    }
    $.ajax({
        type: "Post",
        url: "/Admin/GetEmployeeMaster",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == "tb") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th>Employee Name</th>";
                        th += "   <th>Employee Code</th>";
                        th += "   <th>Designation</th>";
                        th += "   <th>Passport Expiry</th>";
                        th += "   <th>VisaExpiry Date</th>";
                        th += "   <th>Action</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            // var stuline = (o.IsActive == "True" ? "<i class=\"fa fa-check-circle text-success pgdeact-EmployeeMaster\" data-id=\"" + o.ID + "\" style=\"font-size: 20px;cursor:pointer;\" title=\"Set deactive\"></i>" : "<i class=\"fa fa-ban text-danger pgact-EmployeeMaster\"  data-id=\"" + o.ID + "\"  style=\"font-size: 20px;cursor:pointer;\"  title=\"Set active\"></i>");
                            var ediline = "<i class=\"fa fa-pencil pgedit-EmployeeMaster\" data-id=\"" + o.EmployeeID + "\" style=\"font-size: 20px;color:#0ad251;cursor:pointer;margin-left: 10px;\" title=\"Edit\"><textarea style=\"display:none;\">" + JSON.stringify(o) + "</textarea></i>  ";
                            var delline = "<i class=\"fa fa-times text-danger pgdelete-EmployeeMaster\" data-id=\"" + o.EmployeeID + "\" style=\"font-size: 20px;cursor:pointer;margin-left: 10px;\"  title=\"Delete\"></i>";
                            m += "<tr class=\"c-row\" >";
                            m += "   <td>" + o.EmployeeName + "</td>";
                            m += "   <td>" + o.EmployeeCode + "</td>";
                            m += "   <td>" + o.Designation + "</td>";
                            m += "   <td>" + o.PassportExpiryDate + "</td>";
                            m += "   <td>" + o.VisaExpiryDate + "</td>";
                            m += "   <td>" + ediline + delline+"</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-EmployeeMaster " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb)
                        if (md == "tb") {
                            $('.' + g_tid).DataTable();
                        }
                    }
                }
                else if (d.status == 203) {
                    if (md == "tb" || md == "dt") {
                        $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
                    }
                }
                else if (d.status == 401) {
                    swal(d.status_message);
                    window.location.href = '/';
                }
            }
            else {
                swal(d.status_message);
                $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
            }
        },
        error: function (data) {
            $(ctrl).empty().append('<div class="card-body">Connection error</div>');
            console.log('error : g_js_GetEmployeeMaster');
        }
    });
}
function g_js_ManageEmployeeMaster(btn, async_mode, cobj, who, g_btn, md, g_ctrl) {
    if (btn != null) {
        if ($(btn).hasClass('disabled')) {
            return;
        }
        $(btn).addClass('disabled');
    }
    $.ajax({
        type: "POST",
        url: "/Admin/ManageEmployeeMaster",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            // console.log(d);
            // Alert('Ajax success')
            if (d.success) {
                if (d.status == 200) {
                    if (!isNaN(d.id) && d.id > 0) {
                        if (cobj.Mode == "AddNew") {
                            swal("", "Saved Successfully", "success");
                            $('#modal-EmployeeMaster').modal('hide');
                            g_js_GetEmployeeMaster('#dataholder', !0, { Mode: 'AdminAll' }, who, 'tb', !0, '');
                        }
                        else if (cobj.Mode == "DeleteByID") {
                            $(g_btn).closest('tr').fadeOut();
                            setTimeout(function () { $(g_btn).closest('tr').remove(); }, 500);
                            swal("", "Deleted Successfully", "success");
                        }
                        else if (cobj.Mode == "UpdateByID") {
                            swal("", "Updated Successfully", "success");
                            $('#modal-EmployeeMaster').modal('hide');
                            g_js_GetEmployeeMaster('#dataholder', !0, { Mode: 'AdminAll' }, who, 'tb', !0, '');
                        }
                        else if (cobj.Mode == "DeactiveByID") {
                            swal("", "Saved Successfully", "success");
                            g_js_GetEmployeeMaster('#dataholder', !0, { Mode: 'AdminAll' }, who, 'tb', !0, '');
                        }
                        else if (cobj.Mode == "ActiveByID") {
                            swal("", "Saved Successfully", "success");
                            g_js_GetEmployeeMaster('#dataholder', !0, { Mode: 'AdminAll' }, who, 'tb', !0, '');
                        }

                    }
                }
                else if (d.status == 203) {
                    swal(d.status_message);
                }
            }
            else if (d.status == 401) {
                swal(d.status_message);
                window.location.href = '/';
            }
            else {
                swal(d.status_message);
            }
            if (btn != null) { $(btn).removeClass('disabled'); }
        },
        error: function () {
            swal('Connection error');
            if (btn != null) { $(btn).removeClass('disabled'); }
        }
    });

}
function g_js_GetVehicleMaster(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append(g_js_tabrowloader);
    }
    $.ajax({
        type: "Post",
        url: "/Vehicle/GetVehicleMaster",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == "tb") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th>Category</th>";
                        th += "   <th>Registration No</th>";
                        th += "   <th>Model</th>";
                        th += "   <th>Registered Under</th>";
                        th += "   <th>Reg Expiry date</th>";
                        th += "   <th>Insurance Exp Date</th>";
                        th += "   <th>Vehicle Type</th>";
                        th += "   <th style=\"min-width: 71px;\">Action</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            // var stuline = (o.IsActive == "True" ? "<i class=\"fa fa-check-circle text-success pgdeact-VehicleMaster\" data-id=\"" + o.ID + "\" style=\"font-size: 20px;cursor:pointer;\" title=\"Set deactive\"></i>" : "<i class=\"fa fa-ban text-danger pgact-VehicleMaster\"  data-id=\"" + o.ID + "\"  style=\"font-size: 20px;cursor:pointer;\"  title=\"Set active\"></i>");
                            var ediline = "<i class=\"fa fa-pencil pgedit-VehicleMaster\" data-id=\"" + o.VehicleID + "\" style=\"font-size: 20px;color:#0ad251;cursor:pointer;margin-left: 10px;\" title=\"Edit\"><textarea style=\"display:none;\">" + JSON.stringify(o) + "</textarea></i>  ";
                            var delline = "<i class=\"fa fa-times text-danger pgdelete-VehicleMaster\" data-id=\"" + o.VehicleID + "\" style=\"font-size: 20px;cursor:pointer;margin-left: 10px;\"  title=\"Delete\"></i>";
                            m += "<tr class=\"c-row\" >";
                            m += "   <td>" + o.Category + "</td>";
                            m += "   <td>" + o.RegistrationNo + "</td>";
                            m += "   <td>" + o.Model + "</td>";
                            m += "   <td>" + o.RegisteredUnder + "</td>";
                            m += "   <td>" + o.RegExpirydate + "</td>";
                            m += "   <td>" + o.InsuranceExpDate + "</td>";
                            m += "   <td>" + o.VehicleType + "</td>";
                            m += "   <td>" + ediline + delline + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-VehicleMaster " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb)
                        if (md == "tb") {
                            $('.' + g_tid).DataTable();
                        }
                    }
                    else if (md == 'ddl' || md == 'ddld') {
                        $(ctrl).empty();
                        var m = (md == 'ddl' ? "" : "<option value=\"\">" + bo.Text + "</option>");
                        $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.TruckDetailID + "\">" + o.RegistrationNo + " - " + o.DriverName + "</option>";
                        });
                        $(ctrl).empty().append(m);
                    }
                    else if (md == 'ddld2') {
                        $(ctrl).empty();
                        var m = "<option value=\"\">" + bo.Text + "</option>";
                        $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.TruckDetailID + "\">" + o.ReceiptNo + " : " + o.DriverName + " : " + o.RegNo + " : " + o.OriginCity + " : " + o.DestinationCity + "</option>";
                        });
                        $(ctrl).empty().append(m);
                    }
                    else if (md == 'ddld3') {
                        $(ctrl).empty();
                        var m = "<option value=\"\">" + bo.Text + "</option>";
                        $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.VehicleID + "\">" + o.RegistrationNo + "</option>";
                        });
                        $(ctrl).empty().append(m);
                    }
                    else if (md == 'ddld4') {
                        $(ctrl).empty();
                        var m = (md == 'ddl' ? "" : "<option value=\"\">" + bo.Text + "</option>");
                        $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.TruckDetailID + "\">" + o.ReceiptNo + " - " + o.DriverName + "</option>";
                        });
                        $(ctrl).empty().append(m);
                    }
                }
                else if (d.status == 203) {
                    if (md == "tb" || md == "dt") {
                        $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
                    }
                    else if (md == "ddl" || md == 'ddld' || md == 'ddld2' || md == 'ddld4') {
                        $(ctrl).empty().append('<option value=\"\">' + d.status_message + '</option>');
                    }
                }
                else if (d.status == 401) {
                    swal(d.status_message);
                    window.location.href = '/';
                }
            }
            else {
                if (md == "ddl" || md == 'ddld' || md == 'ddld2' || md == 'ddld4') {
                    $(ctrl).empty().append('<option value=\"\">' + d.status_message + '</option>');
                }
                else
                swal(d.status_message);
                $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
            }
        },
        error: function (data) {
            $(ctrl).empty().append('<div class="card-body">Connection error</div>');
            console.log('error : g_js_GetVehicleMaster');
        }
    });
}
function g_js_ManageVehicleMaster(btn, async_mode, cobj, who, g_btn, md, g_ctrl) {
    if (btn != null) {
        if ($(btn).hasClass('disabled')) {
            return;
        }
        $(btn).addClass('disabled');
    }
    $.ajax({
        type: "POST",
        url: "/Vehicle/ManageVehicleMaster",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (!isNaN(d.id) && d.id > 0) {
                        if (cobj.eMode == "AddNew") {
                            swal("", "Saved Successfully", "success");
                            $('#modal-VehicleMaster').modal('hide');
                            g_js_GetVehicleMaster('#dataholder', !0, { eMode: 'AdminAll' }, who, 'tb', !1);
                        }
                        else if (cobj.eMode == "DeleteByID") {
                            $(g_btn).closest('tr').fadeOut();
                            setTimeout(function () { $(g_btn).closest('tr').remove(); }, 500);
                            swal("", "Deleted Successfully", "success");
                        }
                        else if (cobj.eMode == "UpdateByID") {
                            swal("", "Updated Successfully", "success");
                            $('#modal-VehicleMaster').modal('hide');
                            g_js_GetVehicleMaster('#dataholder', !0, { eMode: 'AdminAll' }, who, 'tb', !1);
                        }
                    }
                }
                else if (d.status == 203) {
                    swal(d.status_message);
                }
            }
            else if (d.status == 401) {
                swal(d.status_message);
                window.location.href = '/';
            }
            else {
                swal(d.status_message);
            }
            if (btn != null) { $(btn).removeClass('disabled'); }
        },
        error: function () {
            swal('Connection error');
            if (btn != null) { $(btn).removeClass('disabled'); }
        }
    });

}
function g_js_GetCustomerEnquiry(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append(g_js_tabrowloader);
    }
    $.ajax({
        type: "Post",
        url: "/Admin/GetCustomerEnquiry",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == "tb") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th>Branch Name</th>";
                        th += "   <th>Enquiry No</th>";
                        th += "   <th>Enquiry Date</th>";
                        th += "   <th>Shipper</th>";
                        th += "   <th>Country</th>";
                        th += "   <th>Consignee</th>";
                        th += "   <th>Country</th>";
                        th += "   <th style=\"min-width: 71px;\">Action</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            // var stuline = (o.IsActive == "True" ? "<i class=\"fa fa-check-circle text-success pgdeact-CustomerEnquiry\" data-id=\"" + o.ID + "\" style=\"font-size: 20px;cursor:pointer;\" title=\"Set deactive\"></i>" : "<i class=\"fa fa-ban text-danger pgact-CustomerEnquiry\"  data-id=\"" + o.ID + "\"  style=\"font-size: 20px;cursor:pointer;\"  title=\"Set active\"></i>");
                            var ediline = "<i class=\"fa fa-pencil pgedit-CustomerEnquiry\" data-id=\"" + o.EnquiryID + "\" style=\"font-size: 20px;color:#0ad251;cursor:pointer;margin-left: 10px;\" title=\"Edit\"><textarea style=\"display:none;\">" + JSON.stringify(o) + "</textarea></i>  ";
                            var delline = "<i class=\"fa fa-times text-danger pgdelete-CustomerEnquiry\" data-id=\"" + o.EnquiryID + "\" style=\"font-size: 20px;cursor:pointer;margin-left: 10px;\"  title=\"Delete\"></i>";
                            m += "<tr class=\"c-row\" >";
                            m += "   <td>" + o.BranchName + "</td>";
                            m += "   <td>" + o.EnquiryNo + "</td>";
                            m += "   <td>" + o.EnquiryDate + "</td>";
                            m += "   <td>" + o.Shipper + "</td>";
                            m += "   <td>" + o.ShipperCountry + "</td>";
                            m += "   <td>" + o.Consignee + "</td>";
                            m += "   <td>" + o.ConsigneeCountry + "</td>";
                            m += "   <td>" + ediline + delline +"</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-CustomerEnquiry " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb)
                        if (md == "tb") {
                            $('.' + g_tid).DataTable();
                        }
                    }
                }
                else if (d.status == 203) {
                    if (md == "tb" || md == "dt") {
                        $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
                    }
                }
                else if (d.status == 401) {
                    swal(d.status_message);
                    window.location.href = '/';
                }
            }
            else {
                swal(d.status_message);
                $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
            }
        },
        error: function (data) {
            $(ctrl).empty().append('<div class="card-body">Connection error</div>');
            console.log('error : g_js_GetCustomerEnquiry');
        }
    });
}
function g_js_ManageCustomerEnquiry(btn, async_mode, cobj, who, g_btn, md, g_ctrl) {
    if (btn != null) {
        if ($(btn).hasClass('disabled')) {
            return;
        }
        $(btn).addClass('disabled');
    }
    $.ajax({
        type: "POST",
        url: "/Admin/ManageCustomerEnquiry",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (!isNaN(d.id) && d.id > 0) {
                        if (cobj.Mode == "AddNew") {
                            swal("", "Saved Successfully", "success");
                            $('#modal-CustomerEnquiry').modal('hide');
                            g_js_GetCustomerEnquiry('#dataholder', !0, { Mode: 'AdminAll' }, who, 'tb', !0, '');
                        }
                        else if (cobj.Mode == "DeleteByID") {
                            $(g_btn).closest('tr').fadeOut();
                            setTimeout(function () { $(g_btn).closest('tr').remove(); }, 500);
                            swal("", "Deleted Successfully", "success");
                        }
                        else if (cobj.Mode == "UpdateByID") {
                            swal("", "Updated Successfully", "success");
                            $('#modal-CustomerEnquiry').modal('hide');
                            g_js_GetCustomerEnquiry('#dataholder', !0, { Mode: 'AdminAll' }, who, 'tb', !0, '');
                        }
                    }
                }
                else if (d.status == 203) {
                    swal(d.status_message);
                }
            }
            else if (d.status == 401) {
                swal(d.status_message);
                window.location.href = '/';
            }
            else {
                swal(d.status_message);
            }
            if (btn != null) { $(btn).removeClass('disabled'); }
        },
        error: function () {
            swal('Connection error');
            if (btn != null) { $(btn).removeClass('disabled'); }
        }
    });

}
function g_js_GetCustomerMaster(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append(g_js_loader);
    }
    $.ajax({
        type: "Post",
        url: "/Admin/GetCustomerMaster",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == 'obj') {
                        bo(d.data[0]);
                    }
                }
                else if (d.status == 203) {
                    if (md == "obj") {
                        bo(undefined);
                    }
                }
                else if (d.status == 401) {
                    swal(d.status_message);
                    window.location.href = '/';
                }
            }
            else {
                if (md == "obj") {
                    bo(undefined);
                }
            }
        },
        error: function (data) {
            if (md == "obj") {
                bo(undefined);
            }
            console.log('error : g_js_GetCustomerMaster');
        }
    });
}
function g_js_GetLabelPrinting(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append(g_js_tabrowloader);
    }
    $.ajax({
        type: "Post",
        url: "/Admin/GetLabelPrinting",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == "tb") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th>AWBNo</th>";
                        th += "   <th>InScan Date</th>";
                        th += "   <th>Enquiry No</th>";
                        th += "   <th>Shipper</th>";
                        th += "   <th>Consignee</th>";
                        th += "   <th style=\"min-width: 71px;\">Action</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            // var stuline = (o.IsActive == "True" ? "<i class=\"fa fa-check-circle text-success pgdeact-LabelPrinting\" data-id=\"" + o.ID + "\" style=\"font-size: 20px;cursor:pointer;\" title=\"Set deactive\"></i>" : "<i class=\"fa fa-ban text-danger pgact-LabelPrinting\"  data-id=\"" + o.ID + "\"  style=\"font-size: 20px;cursor:pointer;\"  title=\"Set active\"></i>");
                            var ediline = "<i class=\"fa fa-pencil pgedit-LabelPrinting\" data-id=\"" + o.InScanID + "\" style=\"font-size: 20px;color:#0ad251;cursor:pointer;margin-left: 10px;\" title=\"Edit\"><textarea style=\"display:none;\">" + JSON.stringify(o) + "</textarea></i>  ";
                            var delline = "<i class=\"fa fa-times text-danger pgdelete-LabelPrinting\" data-id=\"" + o.InScanID + "\" style=\"font-size: 20px;cursor:pointer;margin-left: 10px;\"  title=\"Delete\"></i>";
                            m += "<tr class=\"c-row\" >";
                            m += "   <td>" + o.AWBNo + "</td>";
                            m += "   <td>" + o.InScanDate + "</td>";
                            m += "   <td>" + o.EnquiryNo + "</td>";
                            m += "   <td>" + o.Shipper + "</td>";
                            m += "   <td>" + o.Consignee + "</td>";
                            m += "   <td>" + ediline + delline + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-LabelPrinting " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb)
                        if (md == "tb") {
                            $('.' + g_tid).DataTable();
                        }
                    }
                    else if (md == "tb2") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th>AWB No</th>";
                        th += "   <th>InScan Date</th>";
                        th += "   <th>Shipper</th>";
                        th += "   <th>Consignee</th>";
                        th += "   <th>Origin</th>";
                        th += "   <th>Destination</th>";
                        th += "   <th>Item Name</th>";
                        th += "   <th>Weight</th>";
                        th += "   <th>Pieces</th>";
                        th += "   <th>Courier Status</th>";
                        th += "   <th style=\"min-width: 71px;\">Action</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            var delline = "<i class=\"fa fa-times text-danger pgdelete-HoldShipment\" data-id=\"" + o.InScanID + "\" style=\"font-size: 20px;cursor:pointer;margin-left: 10px;\"  title=\"Delete\"></i>";
                            m += "<tr class=\"c-row\" >";
                            m += "   <td>" + o.AWBNo + "</td>";
                            m += "   <td>" + o.InScanDate + "</td>";
                            m += "   <td>" + o.Shipper + "</td>";
                            m += "   <td>" + o.Consignee + "</td>";
                            m += "   <td>" + o.Origin + "</td>";
                            m += "   <td>" + o.Destination + "</td>";
                            m += "   <td>" + o.ItemName + "</td>";
                            m += "   <td>" + o.StatedWeight + "</td>";
                            m += "   <td>" + o.Pieces + "</td>";
                            m += "   <td>" + o.CourierStatus + "</td>";
                            m += "   <td>" + delline + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-LabelPrinting " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb)
                        if (md == "tb" || md == "tb2") {
                            $('.' + g_tid).DataTable();
                        }
                    }
                    else if (md == "tb3") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th>DRSNo</th>";
                        th += "   <th>DRS Date</th>";
                        th += "   <th>DeliveryBy</th>";
                        th += "   <th>CheckedBy</th>";
                        th += "   <th>Vehicle</th>";
                        th += "   <th style=\"min-width: 71px;\">Action</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            var delline = "<i class=\"fa fa-times text-danger pgdelete-DRS\" data-id=\"" + o.DRSID + "\" style=\"font-size: 20px;cursor:pointer;margin-left: 10px;\"  title=\"Delete\"></i>";
                            var ediline = "<i class=\"fa fa-pencil pgedit-DRS\" data-id=\"" + o.DRSID + "\" style=\"font-size: 20px;color:#0ad251;cursor:pointer;margin-left: 10px;\" title=\"Edit\"><textarea style=\"display:none;\">" + JSON.stringify(o) + "</textarea></i>  ";
                            m += "<tr class=\"c-row\" >";
                            m += "   <td>" + o.DRSNo + "</td>";
                            m += "   <td>" + o.DRSDate + "</td>";
                            m += "   <td>" + o.DeliveryBy + "</td>";
                            m += "   <td>" + o.CheckedBy + "</td>";
                            m += "   <td>" + o.RegistrationNo + "</td>";
                            m += "   <td>" + ediline + delline + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-DRS " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb)
                        $('.' + g_tid).DataTable({ "order": [[0, 'desc']]});
                    }
                    else if (md == "tb4") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th  class=\"bg-grey text-white\">AWB No</th>";
                        th += "   <th  class=\"bg-grey text-white\">Shipper</th>";
                        th += "   <th  class=\"bg-grey text-white\">Consignee</th>";
                        th += "   <th  class=\"bg-grey text-white\">Item Name</th>";
                        th += "   <th  class=\"bg-grey text-white\">Weight</th>";
                        th += "   <th  class=\"bg-grey text-white\">Pieces</th>";
                        th += "   <th  class=\"bg-grey text-white\">NCND</th>";
                        th += "   <th  class=\"bg-grey text-white\">COD</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            m += "<tr class=\"pgrec r" + o.AWBNo + "\" data-id=\"" + o.InScanID + "~" + o.MaterialCost + "~" + o.COD + "\" data-id2=\"" + o.InScanID +"\" >";
                            m += "   <td>" + o.AWBNo + "</td>";
                            m += "   <td>" + o.Shipper + "</td>";
                            m += "   <td>" + o.Consignee + "</td>";
                            m += "   <td>" + o.ItemName + "</td>";
                            m += "   <td>" + o.StatedWeight + "</td>";
                            m += "   <td>" + o.Pieces + "</td>";
                            m += "   <td>" + o.MaterialCost + "</td>";
                            m += "   <td>" + o.COD + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb);
                        bo();
                    }
                    else if (md == "tb5") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th  class=\"bg-grey text-white\">AWB No</th>";
                        th += "   <th  class=\"bg-grey text-white\">InScan Date</th>";
                        th += "   <th  class=\"bg-grey text-white\">Consignee</th>";
                        th += "   <th  class=\"bg-grey text-white\">Cons. Depot</th>";
                        th += "   <th  class=\"bg-grey text-white\">Item Name</th>";
                        th += "   <th  class=\"bg-grey text-white\">Pieces</th>";
                        th += "   <th  class=\"bg-grey text-white\">Transhipment</th>";
                        th += "   <th  class=\"bg-grey text-white\">Holding</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            m += "<tr class=\"pgrec r" + o.AWBNo + "\" data-id=\"" + o.InScanID + "\" >";
                            m += "   <td>" + o.AWBNo + "</td>";
                            m += "   <td>" + o.InScanDate + "</td>";
                            m += "   <td>" + o.Consignee + "</td>";
                            m += "   <td>" + o.ConsigneeDepot + "</td>";
                            m += "   <td>" + o.ItemName + "</td>";
                            m += "   <td>" + o.Pieces + "</td>";
                            m += "   <td>No</td>";
                            m += "   <td></td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb);
                        bo();
                    }
                    else if (md == "tb6") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th><input type=\"checkbox\" class=\"pgcheckall\"/></th>";
                        th += "   <th>AWB No</th>";
                        th += "   <th>Shipper</th>";
                        th += "   <th>Consignee</th>";
                        th += "   <th>Origin</th>";
                        th += "   <th>Destination</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            m += "<tr class=\"c-row\" >";
                            m += "   <td><input type=\"checkbox\" class=\"pgcheck\" data-id=\"" + o.InScanID + "\" /></td>";
                            m += "   <td>" + o.AWBNo + "</td>";
                            m += "   <td>" + o.Shipper + "</td>";
                            m += "   <td>" + o.Consignee + "</td>";
                            m += "   <td>" + o.Origin + "</td>";
                            m += "   <td>" + o.Destination + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-LabelPrinting " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb);
                    }
                    else if (md == "tb7") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th  class=\"bg-grey text-white\">AWB No</th>";
                        th += "   <th  class=\"bg-grey text-white\">InScan Date</th>";
                        th += "   <th  class=\"bg-grey text-white\">Enquiry No</th>";
                        th += "   <th  class=\"bg-grey text-white\">Item Name</th>";
                        th += "   <th  class=\"bg-grey text-white\">No of Packages</th>";
                        th += "   <th  class=\"bg-grey text-white\">Package Type</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            m += "<tr class=\"pgrec r" + o.AWBNo + "\" data-id=\"" + o.InScanID + "\" >";
                            m += "   <td>" + o.AWBNo + "</td>";
                            m += "   <td>" + o.InScanDate + "</td>";
                            m += "   <td>" + o.EnquiryNo + "</td>";
                            m += "   <td>" + o.ItemName + "</td>";
                            m += "   <td>" + o.Pieces + "</td>";
                            m += "   <td>" + o.PackageType + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb);
                        bo();
                    }
                    else if (md == 'obj') {
                        bo();
                    }
                }
                else if (d.status == 203) {
                    if (md == "tb" || md == "tb2" || md == "tb3" || md == "tb4" || md == "tb5" || md == "tb6" || md == "tb7") {
                        $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
                    }
                    else if (md == "obj") {
                        bo(undefined);
                    }
                }
                else if (d.status == 401) {
                    swal(d.status_message);
                    window.location.href = '/';
                }
            }
            else {
                if (md == "obj") {
                    bo(undefined);
                }
                else {
                    swal(d.status_message);
                    $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
                }
            }
        },
        error: function (data) {
            if (md == "obj") {
                bo(undefined);
            }
            else {
                $(ctrl).empty().append('<div class="card-body">Connection error</div>');
            }
            console.log('error : g_js_GetLabelPrinting');
        }
    });
}
function g_js_ManageLabelPrinting(btn, async_mode, cobj, who, g_btn, md, g_ctrl,bo) {
    if (btn != null) {
        if ($(btn).hasClass('disabled')) {
            return;
        }
        $(btn).addClass('disabled');
    }
    $.ajax({
        type: "POST",
        url: "/Admin/ManageLabelPrinting",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (!isNaN(d.id) && d.id > 0) {
                        if (cobj.Mode == "AddNew") {
                            swal("", "Saved Successfully", "success");
                            $('#modal-LabelPrinting').modal('hide');
                            $('.btn-search-LabelPrinting').trigger('click');
                        }
                        else if (cobj.Mode == "DeleteByID") {
                            $(g_btn).closest('tr').fadeOut();
                            setTimeout(function () { $(g_btn).closest('tr').remove(); }, 500);
                            swal("", "Deleted Successfully", "success");
                        }
                        else if (cobj.Mode == "ReleaseByID") {
                            $(g_btn).closest('tr').fadeOut();
                            swal("", "Released Successfully", "success");
                            $('.btn-search-HoldShipment').trigger('click');
                        }
                        else if (cobj.Mode == "HoldByID") {
                            $(g_btn).closest('tr').fadeOut();
                            $('#modal-HoldShipment').modal('hide');
                            swal("", "Added Successfully", "success");
                            $('.btn-search-HoldShipment').trigger('click');
                        }
                        else if (cobj.Mode == "UpdateByID") {
                            swal("", "Updated Successfully", "success");
                            $('#modal-LabelPrinting').modal('hide');
                            $('.btn-search-LabelPrinting').trigger('click');
                        }
                        else if (cobj.Mode == "CheckDRS") {
                            bo(d.id);
                        }
                        else if (cobj.Mode == "CheckTruckAssign") {
                            bo(d.id);
                        }
                        else if (cobj.Mode == "InScanSelectAllForVerify") {
                            bo(d.id);
                        }
                        else if (cobj.Mode == "DRSInsert") {
                            $('#modal-DRS').modal('hide');
                            swal("", "Added Successfully", "success");
                            $('.btn-search-DRS').trigger('click');
                        }
                        else if (cobj.Mode == "DRSUpdate") {
                            $('#modal-DRS').modal('hide');
                            swal("", "Updated Successfully", "success");
                            $('.btn-search-DRS').trigger('click');
                        }
                        else if (cobj.Mode == "InScanUpdateForTruckAssign") {
                            swal("", "Updated Successfully", "success");
                            $('#dataholder').empty().append('<h4>Added Consignment will show here</h4>');
                            $('#txt-AWBNo').val('');
                        }
                        else if (cobj.Mode == "TransferShipment") {
                            swal("", "Details Updated Successfully", "success").then((value) => {
                                window.location.reload();
                            });
                        }
                        else if (cobj.Mode == "VerifyInboundInsert") {
                            swal("", "Details Updated Successfully", "success").then((value) => {
                                window.location.reload();
                            });
                        }
                        else if (cobj.Mode == "DRSDelete") {
                            $(g_btn).closest('tr').fadeOut();
                            setTimeout(function () { $(g_btn).closest('tr').remove(); }, 500);
                            swal("", "Deleted Successfully", "success");
                        } 
                    }
                }
                else if (d.status == 203) {
                    if (cobj.Mode == "CheckDRS" || cobj.Mode == "CheckTruckAssign" || cobj.Mode == "InScanSelectAllForVerify") {
                        $('#txt-AWBNo').after('<small style="color:red" class="pgerror">' + d.status_message + '</small>');
                    }
                    else {
                        swal(d.status_message);
                    }
                }
            }
            else if (d.status == 401) {
                swal(d.status_message);
                window.location.href = '/';
            }
            else {
                if (cobj.Mode == "CheckDRS" || cobj.Mode == "CheckTruckAssign" || cobj.Mode == "InScanSelectAllForVerify") {
                    $('#txt-AWBNo').after('<small style="color:red" class="pgerror">' + d.status_message+'</small>');
                }
                else {
                    swal(d.status_message);
                }
            }
            if (btn != null) { $(btn).removeClass('disabled'); }
        },
        error: function () {
            swal('Connection error');
            if (btn != null) { $(btn).removeClass('disabled'); }
        }
    });

}
function g_js_GetAWBStatus(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append(g_js_loader);
    }
    $.ajax({
        type: "Post",
        url: "/Admin/GetAWBStatus",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == "tb") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th class=\"bg-grey text-white\" colspan=\"3\">Status</th>";
                        th += "</tr>";
                        th += "<tr>";
                        th += "   <th>Date</th>";
                        th += "   <th>Status</th>";
                        th += "   <th>Remarks</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            m += "<tr class=\"c-row\" >";
                            m += "   <td>" + o.StatusDate + "</td>";
                            m += "   <td>" + o.CourierStatus + "</td>";
                            m += "   <td>" + o.Remarks + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-AWBStatus " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb);
                    }
                }
                else if (d.status == 203) {
                    if (md == "tb" || md == "dt") {
                        $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
                    }
                }
                else if (d.status == 401) {
                    swal(d.status_message);
                    window.location.href = '/';
                }
            }
            else {
                swal(d.status_message);
                $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
            }
        },
        error: function (data) {
            $(ctrl).empty().append('<div class="card-body">Connection error</div>');
            console.log('error : g_js_GetAWBStatus');
        }
    });
}
function g_js_GetRoutes(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append(g_js_loader);
    }
    $.ajax({
        type: "Post",
        url: "/Route/GetRoutes",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == "tb") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th>Route Code</th>";
                        th += "   <th>Route Name</th>";
                        th += "   <th style=\"min-width: 71px;\">Action</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            // var stuline = (o.IsActive == "True" ? "<i class=\"fa fa-check-circle text-success pgdeact-Routes\" data-id=\"" + o.ID + "\" style=\"font-size: 20px;cursor:pointer;\" title=\"Set deactive\"></i>" : "<i class=\"fa fa-ban text-danger pgact-Routes\"  data-id=\"" + o.ID + "\"  style=\"font-size: 20px;cursor:pointer;\"  title=\"Set active\"></i>");
                            
                            var ediline = "<i class=\"fa fa-pencil pgedit-Route\" data-id=\"" + o.RouteID + "\" style=\"font-size: 20px;color:#0ad251;cursor:pointer;margin-left: 10px;\" title=\"Edit\"><textarea style=\"display:none;\">" + JSON.stringify(o) + "</textarea></i>  ";
                            var delline = "<i class=\"fa fa-times pgdelete-Route\" data-id=\"" + o.RouteID + "\" style=\"font-size: 20px;cursor:pointer;margin-left: 10px;\"  title=\"Delete\"></i>";
                            m += "<tr class=\"c-row\" >";
                            m += "   <td>" + o.RouteCode + "</td>";
                            m += "   <td>" + o.RouteName + "</td>";
                            m += "   <td>" + ediline + delline + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-Routes " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb)
                        if (md == "tb") {
                            $('.' + g_tid).DataTable({
                                "order": [[0, "asc"]]
                            });
                        }
                    }
                    else if (md == "tb2") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th>Depot</th>";
                        th += "   <th style=\"width: 100px;\">Stop Order</th>";
                        th += "   <th style=\"min-width: 71px;\">Action</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            var delline = "<i class=\"fa fa-times text-danger pgdelete-Route2\" data-id=\"" + o.RouteID + "\" style=\"font-size: 20px;cursor:pointer;margin-left: 10px;\"  title=\"Delete\"></i>";
                            m += "<tr class=\"pgrec\"  data-id2=\"" + o.DepotID +"\"  >";
                            m += "   <td>" + o.Depot + "</td>";
                            m += "   <td><input type=\"number\" stylr=\"padding-left: 5px;\" min=\"0\" value=\"" + o.Order + "\" /></td>";
                            m += "   <td>" + delline + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-Routes " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb);
                        if (cobj.Mode == 'RouteOrderSelectByRouteID') {
                            bo();
                        }
                    }
                    else if (md == 'ddl' || md == 'ddld') {
                        $(ctrl).empty();
                        var m = (md == 'ddl' ? "" : "<option value=\"\">" + bo.Text + "</option>");
                        $.each(d.data, function (i, o) {
                            m += "<option value=\"" + o.RouteID + "\">" + o.RouteCode + " : " + o.RouteName + "</option>";
                        });
                        $(ctrl).empty().append(m);
                    }
                }
                else if (d.status == 203) {
                    if (md == "tb" || md == "dt") {
                        $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
                    }
                    if (md == "ddl" || md == 'ddld') {
                        $(ctrl).empty().append('<option value=\"\">' + d.status_message + '</option>');
                    }
                }
                else if (d.status == 401) {
                    swal(d.status_message);
                    window.location.href = '/';
                }
            }
            else {
                if (md == "ddl" || md == 'ddld') {
                    $(ctrl).empty().append('<option value=\"\">' + d.status_message + '</option>');
                }
                else
                swal(d.status_message);
                $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
            }
        },
        error: function (data) {
            $(ctrl).empty().append('<div class="card-body">Connection error</div>');
            console.log('error : g_js_GetRoutes');
        }
    });
}
function g_js_ManageRoutes(btn, async_mode, cobj, who, g_btn, md, g_ctrl) {
    if (btn != null) {
        if ($(btn).hasClass('disabled')) {
            return;
        }
        $(btn).addClass('disabled');
    }
    $.ajax({
        type: "POST",
        url: "/Route/ManageRoutes",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (!isNaN(d.id) && d.id > 0) {
                        if (cobj.Mode == "AddNew") {
                            swal("", "Saved Successfully", "success");
                            $('#modal-Route').modal('hide');
                            g_js_GetRoutes('#dataholder', !0, { Mode: 'AdminAll' }, who, 'tb', !0, '');
                        }
                        else if (cobj.Mode == "DeleteByID") {
                            $(g_btn).closest('tr').fadeOut();
                            setTimeout(function () { $(g_btn).closest('tr').remove(); }, 500);
                            swal("", "Deleted Successfully", "success");
                        }
                        else if (cobj.Mode == "UpdateByID") {
                            swal("", "Updated Successfully", "success");
                            $('#modal-Route').modal('hide');
                            g_js_GetRoutes('#dataholder', !0, { Mode: 'AdminAll' }, who, 'tb', !0, '');
                        }
                    }
                }
                else if (d.status == 203) {
                    swal(d.status_message);
                }
            }
            else if (d.status == 401) {
                swal(d.status_message);
                window.location.href = '/';
            }
            else {
                swal(d.status_message);
            }
            if (btn != null) { $(btn).removeClass('disabled'); }
        },
        error: function () {
            swal('Connection error');
            if (btn != null) { $(btn).removeClass('disabled'); }
        }
    });

}
function g_js_GetJobs(ctrl, async_mode, cobj, who, md, isloader, bo) {
    if (isloader) {
        $(ctrl).empty().append(g_js_tabrowloader);
    }
    $.ajax({
        type: "Post",
        url: "/Admin/GetJobs",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (md == "tb") {
                        $(ctrl).empty();
                        var th = "";
                        th += "<thead>";
                        th += "<tr>";
                        th += "   <th>JobNo</th>";
                        th += "   <th>Job Date</th>";
                        th += "   <th>From Date</th>";
                        th += "   <th>To Date</th>";
                        th += "   <th style=\"min-width: 71px;\">Action</th>";
                        th += "</tr>";
                        th += "</thead>";
                        th += "<tbody>";
                        var m = "";
                        $.each(d.data, function (i, o) {
                            var delline = "<i class=\"fa fa-times text-danger pgdelete-Jobs\" data-id=\"" + o.JobID + "\" style=\"font-size: 20px;cursor:pointer;margin-left: 10px;\"  title=\"Delete\"></i>";
                            var ediline = "<i class=\"fa fa-pencil pgedit-Jobs\" data-id=\"" + o.JobID + "\" style=\"font-size: 20px;color:#0ad251;cursor:pointer;margin-left: 10px;\" title=\"Edit\"><textarea style=\"display:none;\">" + JSON.stringify(o) + "</textarea></i>  ";
                            m += "<tr class=\"c-row\" >";
                            m += "   <td>" + o.JobNo + "</td>";
                            m += "   <td>" + o.JobDate + "</td>";
                            m += "   <td>" + o.FromDate + "</td>";
                            m += "   <td>" + o.ToDate + "</td>";
                            m += "   <td>" + ediline + delline + "</td>";
                            m += "<\/tr>";
                        });
                        var g_tid = "datatables-" + g_js_getRandomNumber();
                        var ttb = "<table id=\"" + g_tid + "\" class=\"table table-sm table-hover pgtable-edithover pgtbl-Jobs " + g_tid + "\">";
                        ttb += th + m + ' <\/tbody><\/table>';
                        $(ctrl).empty().append(ttb);
                    }
                }
                else if (d.status == 203) {
                    if (md == "tb" || md == "dt") {
                        $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
                    }
                }
                else if (d.status == 401) {
                    swal(d.status_message);
                    window.location.href = '/';
                }
            }
            else {
                swal(d.status_message);
                $(ctrl).empty().append('<div class="card-body">' + d.status_message + '</div>');
            }
        },
        error: function (data) {
            $(ctrl).empty().append('<div class="card-body">Connection error</div>');
            console.log('error : g_js_GetJobs');
        }
    });
}
function g_js_ManageJobs(btn, async_mode, cobj, who, g_btn, md, g_ctrl) {
    if (btn != null) {
        if ($(btn).hasClass('disabled')) {
            return;
        }
        $(btn).addClass('disabled');
    }
    $.ajax({
        type: "POST",
        url: "/Admin/ManageJobs",
        data: "{o: " + JSON.stringify(cobj) + ",Who : '" + who + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: async_mode,
        success: function (d) {
            if (d.success) {
                if (d.status == 200) {
                    if (!isNaN(d.id) && d.id > 0) {
                        if (cobj.Mode == "AddNew") {
                            swal("", "Saved Successfully", "success");
                            $('#modal-Job').modal('hide');
                            $('.btn-search-Job').trigger('click');
                        }
                        else if (cobj.Mode == "DeleteByID") {
                            $(g_btn).closest('tr').fadeOut();
                            setTimeout(function () { $(g_btn).closest('tr').remove(); }, 500);
                            swal("", "Deleted Successfully", "success");
                        }
                        else if (cobj.Mode == "UpdateByID") {
                            swal("", "Updated Successfully", "success");
                            $('#modal-Job').modal('hide');
                            $('.btn-search-Job').trigger('click');
                        }
                    }
                }
                else if (d.status == 203) {
                    swal(d.status_message);
                }
            }
            else if (d.status == 401) {
                swal(d.status_message);
                window.location.href = '/';
            }
            else {
                swal(d.status_message);
            }
            if (btn != null) { $(btn).removeClass('disabled'); }
        },
        error: function () {
            swal('Connection error');
            if (btn != null) { $(btn).removeClass('disabled'); }
        }
    });

}

