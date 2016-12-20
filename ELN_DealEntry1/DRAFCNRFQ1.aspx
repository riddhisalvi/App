<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DRAFCNRFQ1.aspx.vb" MasterPageFile="~/FiniqAppMasterPage.Master"
    EnableEventValidation="false" Inherits="FinIQWebApp.DRAFCNRFQ1" Title="DRA/FCN RFQ and Orders" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.DataVisualization" Namespace="System.Web.UI.DataVisualization.Charting"
    TagPrefix="asp" %>
<%@ Register Src="../FinIQ_User_Controls/DateControl.ascx" TagName="DateControl"
    TagPrefix="uc1" %>
<%@ Register Src="../FinIQ_User_Controls/Fast_Find_Customer_Control/FinIQ_Fast_Find_Customer.ascx"
    TagName="FinIQ_Fast_Find_Customer" TagPrefix="uc2" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy21" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/FinIQJS/FixFocus.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <link href="../App_Themes/confirmationBoxSolo.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/ELNStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../FinIQJS/Jquery/jquery-ui.css" />

    <script src="../FinIQJS/jquery-1.9.1.js" type="text/javascript"></script>

    <script src="../FinIQJS/jquery-color.js" type="text/javascript"></script>

    <script class="include" type="text/javascript" src="../FinIQJS/Jquery/jquery.min.js"></script>

    <script src="../FinIQJS/Jquery/jquery.js" type="text/javascript"></script>

    <script src="../FinIQJS/Jquery/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function showLPBoxes() {
            if (document.getElementById("<%=chkHSBC.ClientID%>")) {
                document.getElementById("<%=chkHSBC.ClientID%>").style.visibility = 'visible';
            }
            if (document.getElementById("<%=chkUBS.ClientID%>")) {
                document.getElementById("<%=chkUBS.ClientID%>").style.visibility = 'visible';
            }
            if (document.getElementById("<%=chkJPM.ClientID%>")) { document.getElementById("<%=chkJPM.ClientID%>").style.visibility = 'visible'; }
            if (document.getElementById("<%=chkBNPP.ClientID%>")) { document.getElementById("<%=chkBNPP.ClientID%>").style.visibility = 'visible'; }
            if (document.getElementById("<%=chkCS.ClientID%>")) { document.getElementById("<%=chkCS.ClientID%>").style.visibility = 'visible'; }
            if (document.getElementById("<%=chkBAML.ClientID%>")) {
                document.getElementById("<%=chkBAML.ClientID%>").style.visibility = 'visible';
            }
            if (document.getElementById("<%=chkDBIB.ClientID%>")) { document.getElementById("<%=chkDBIB.ClientID%>").style.visibility = 'visible'; }
            if (document.getElementById("<%=chkOCBC.ClientID%>")) { document.getElementById("<%=chkOCBC.ClientID%>").style.visibility = 'visible'; }
            if (document.getElementById("<%=chkCITI.ClientID%>")) { document.getElementById("<%=chkCITI.ClientID%>").style.visibility = 'visible'; }
        }

        function hideLPBoxes() {
            if (document.getElementById("<%=chkHSBC.ClientID%>")) {

                if (!document.getElementById("<%=chkHSBC.ClientID%>").checked) {
                    document.getElementById("<%=chkHSBC.ClientID%>").style.visibility = 'hidden';
                }

            }
            if (document.getElementById("<%=chkUBS.ClientID%>")) {
                if (!document.getElementById("<%=chkUBS.ClientID%>").checked) {
                    document.getElementById("<%=chkUBS.ClientID%>").style.visibility = 'hidden';
                }
            }
            if (document.getElementById("<%=chkJPM.ClientID%>")) {
                if (!document.getElementById("<%=chkJPM.ClientID%>").checked) {
                    document.getElementById("<%=chkJPM.ClientID%>").style.visibility = 'hidden';
                }
            }
            if (document.getElementById("<%=chkBNPP.ClientID%>")) {
                if (!document.getElementById("<%=chkBNPP.ClientID%>").checked) {
                    document.getElementById("<%=chkBNPP.ClientID%>").style.visibility = 'hidden';
                }
            }
            if (document.getElementById("<%=chkCS.ClientID%>")) {
                if (!document.getElementById("<%=chkCS.ClientID%>").checked) {
                    document.getElementById("<%=chkCS.ClientID%>").style.visibility = 'hidden';
                } 
            }
            if (document.getElementById("<%=chkBAML.ClientID%>")) {
                if (!document.getElementById("<%=chkBAML.ClientID%>").checked) {
                    document.getElementById("<%=chkBAML.ClientID%>").style.visibility = 'hidden';
                }
            }
            if (document.getElementById("<%=chkDBIB.ClientID%>")) {
                if (!document.getElementById("<%=chkDBIB.ClientID%>").checked) { document.getElementById("<%=chkDBIB.ClientID%>").style.visibility = 'hidden'; }
            }
            if (document.getElementById("<%=chkOCBC.ClientID%>")) {
                if (!document.getElementById("<%=chkOCBC.ClientID%>").checked) { document.getElementById("<%=chkOCBC.ClientID%>").style.visibility = 'hidden'; }
            }
            if (document.getElementById("<%=chkCITI.ClientID%>")) {
                if (!document.getElementById("<%=chkCITI.ClientID%>").checked) { document.getElementById("<%=chkCITI.ClientID%>").style.visibility = 'hidden'; }
            }
            if (document.getElementById("<%=chkLEONTEQ.ClientID%>")) {
                if (!document.getElementById("<%=chkLEONTEQ.ClientID%>").checked) { document.getElementById("<%=chkLEONTEQ.ClientID%>").style.visibility = 'hidden'; }
            }
            if (document.getElementById("<%=chkCOMMERZ.ClientID%>")) {
                if (!document.getElementById("<%=chkCOMMERZ.ClientID%>").checked) { document.getElementById("<%=chkCOMMERZ.ClientID%>").style.visibility = 'hidden'; }
            }

        }
    
    
    
    
    
    
        // <Added by Mohit Lalwani for adding ToolTip>

        $(function() {
            $(".ajax__tab_tab").eq(0).attr("title", "Equity Linked Note (ELN)");
            $(".ajax__tab_tab").eq(1).attr("title", "Fixed Coupon Note (FCN)");
            $(".ajax__tab_tab").eq(2).attr("title", "Daily Range Accrual (DRA)");
            $(".ajax__tab_tab").eq(3).attr("title", "Accumulator (KODA)");
            $(".ajax__tab_tab").eq(4).attr("title", "Decumulator (DeKODA)");
            $(".ajax__tab_tab").eq(5).attr("title", "Equity Options (EQO)");

        })

        // </Added by Mohit Lalwani for adding ToolTip>
        function showFieldInfo(titleElem, contentElem) {
            $("#closeFieldInfo").css('visibility', 'visible');
            $("#divShareFieldInfo").css('top', $(titleElem).position().top);
            $("#divShareFieldInfo").css('left', $(titleElem).position().left);
            $('#lblFieldCaption').html($(titleElem).html().toUpperCase());
            $('#contentShareFieldInfo').html($(contentElem).html());
            $("#divShareFieldInfo").css('visibility', 'visible');
            $("#divShareFieldInfo").slideDown("slow");
            $("#closeFieldInfo").click(function() {
                $("#divShareFieldInfo").slideUp("slow");
            });
        }

        var tmr, tmr1, tmr2, tmr3;
        var updateProgress = null;
        var i = 0;
        var intervalCopyJPM, intervalCopyHSBC, intervalCopyUBS, intervalCopyCS, intervalCopyBAML, intervalCopyBNPP, intervalCopyDBIB, intervalCopyOCBC, intervalCopyCITI, intervalCopyLEONTEQ, intervalCopyCOMMERZ;
        var JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
        var HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
        var UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
        var CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
        var BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
        var BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
        var DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");  //mangesh wakode
        var OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
        var CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
        var LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
        var COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
        
        var AllHiddenPrice = document.getElementById("<%=AllHiddenPrice.ClientID %>");
        var JpmHiddenAccDays = document.getElementById("<%=JpmHiddenAccDays.ClientID %>");
        var HsbcHiddenAccDays = document.getElementById("<%=HsbcHiddenAccDays.ClientID %>");
        var UbsHiddenAccDays = document.getElementById("<%=UbsHiddenAccDays.ClientID %>");
        var CsHiddenAccDays = document.getElementById("<%=CsHiddenAccDays.ClientID %>");
        var BAMLHiddenAccDays = document.getElementById("<%=BAMLHiddenAccDays.ClientID %>");
        var BNPPHiddenAccDays = document.getElementById("<%=BNPPHiddenAccDays.ClientID %>");
        var DBIBHiddenAccDays = document.getElementById("<%=DBIBHiddenAccDays.ClientID %>"); //mangesh wakode
        var OCBCHiddenAccDays = document.getElementById("<%=OCBCHiddenAccDays.ClientID %>");
        var CITIHiddenAccDays = document.getElementById("<%=CITIHiddenAccDays.ClientID %>");
        var LEONTEQHiddenAccDays = document.getElementById("<%=LEONTEQHiddenAccDays.ClientID %>");
        var COMMERZHiddenAccDays = document.getElementById("<%=COMMERZHiddenAccDays.ClientID %>");
        //Added by Imran to start different timer for different provider.
        var tmrJPM, tmrHSBC, tmrUBS, tmrCS, tmrBAML, tmrBNPP, tmrDBIB, tmrOCBC, tmrCITI, tmrLEONTEQ, tmrCOMMERZ;
        var processingJPM = false, processingHSBC = false, processingUBS = false, processingCS = false, processingBAML = false, processingBNPP = false, processingDBIB = false, processingOCBC = false, processingCITI = false, processingLEONTEQ = false, processingCOMMERZ = false;
        //---Added for search on share
        var ddlText, ddlValue, ddl, txt;
        function setPriceAllEnableDisable() {
            AllHiddenPrice = document.getElementById("<%=AllHiddenPrice.ClientID %>");
            var solveAllFlag = false;
            var btnSA = document.getElementById("<%=btnSolveAll.ClientID %>");
            if (processingJPM == false && processingHSBC == false && processingUBS == false && processingCS == false && processingBAML == false && processingBNPP == false && processingDBIB == false && processingOCBC == false && processingCITI == false && processingLEONTEQ == false && processingCOMMERZ == false) {
                solveAllFlag = true;
            }
            else {
                solveAllFlag = false;
            }
            if (solveAllFlag == false) {
                AllHiddenPrice.value = 'Disable;' + AllHiddenPrice.value.split(";")[1];
                document.getElementById("<%=btnSolveAll.ClientID %>").disabled = true;
                btnSA.disabled = true;
                $("#ctl00_MainContent_btnSolveAll").removeClass("btn").addClass("btnDisabled");
            }
            else {
                AllHiddenPrice.value = 'Enable;Disable';
                document.getElementById("<%=btnSolveAll.ClientID %>").disabled = false;
                btnSA.disabled = false;
                $("#ctl00_MainContent_btnSolveAll").removeClass("btnDisabled").addClass("btn");
                document.getElementById('PriceAllWait').style.visibility = 'hidden';
                document.getElementById("ctl00_MainContent_btnLoad").click();
            }

        }

        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        function StopPolling() {
            try {
                clearInterval(intervalCopyJPM);
                clearInterval(intervalCopyHSBC);
                clearInterval(intervalCopyUBS);
                clearInterval(intervalCopyCS);
                clearInterval(intervalCopyBAML);
                clearInterval(intervalCopyBNPP);
                clearInterval(intervalCopyDBIB);
                clearInterval(intervalCopyOCBC);
                clearInterval(intervalCopyCITI);
                clearInterval(intervalCopyLEONTEQ);
                clearInterval(intervalCopyCOMMERZ);
                processingJPM = false;
                processingHSBC = false;
                processingUBS = false;
                processingCS = false;
                processingBAML = false;
                processingBNPP = false;
                processingDBIB = false;
                processingOCBC = false;
                processingCITI = false;
                processingLEONTEQ = false;
                processingCOMMERZ = false;
            }
            catch (err) {

            }
        }

        function getPrice(RFQId, lblPrice, lblTimer, btnDeal, btnPrice1) {
            var checkResetflag = false;
            var time = pollingMilliSec;
            var startTime = new Date().getTime();
            if (btnDeal.indexOf("JPM") == 21) {
                processingJPM = true;
            }
            else if (btnDeal.indexOf("HSBC") == 21) {
                processingHSBC = true;
            }
            else if (btnDeal.indexOf("CS") == 21) {
                processingCS = true;
            }
            else if (btnDeal.indexOf("UBS") == 21) {
                processingUBS = true;
            }
            else if (btnDeal.indexOf("BAML") == 21) {
                processingBAML = true;
            }
            else if (btnDeal.indexOf("BNPP") == 21) {
                processingBNPP = true;
            }
            else if (btnDeal.indexOf("DBIB") == 21) {
                processingDBIB = true;
            }
            else if (btnDeal.indexOf("OCBC") == 21) {
                processingOCBC = true;
            }
            else if (btnDeal.indexOf("CITI") == 21) {
                processingCITI = true;
            }
            else if (btnDeal.indexOf("LEONTEQ") == 21) {
                processingLEONTEQ = true;
            }
            else if (btnDeal.indexOf("COMMERZ") == 21) {
                processingCOMMERZ = true;
            }
            var interval = setInterval(function() {
                if (btnDeal.indexOf("JPM") == 21) {
                    intervalCopyJPM = interval;
                    processingJPM = true;
                    if ('Enable' == document.getElementById("<%=JpmHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("HSBC") == 21) {
                    intervalCopyHSBC = interval;
                    processingHSBC = true;
                    if ('Enable' == document.getElementById("<%=HsbcHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("CS") == 21) {
                    intervalCopyCS = interval;
                    processingCS = true;
                    if ('Enable' == document.getElementById("<%=CsHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("UBS") == 21) {
                    intervalCopyUBS = interval;
                    processingUBS = true;
                    if ('Enable' == document.getElementById("<%=UbsHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("BAML") == 21) {
                    intervalCopyBAML = interval;
                    processingBAML = true;
                    if ('Enable' == document.getElementById("<%=BAMLHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("BNPP") == 21) {
                    intervalCopyBNPP = interval;
                    processingBNPP = true;
                    if ('Enable' == document.getElementById("<%=BNPPHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("DBIB") == 21) {
                    intervalCopyDBIB = interval;
                    processingDBIB = true;
                    if ('Enable' == document.getElementById("<%=DBIBHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("OCBC") == 21) {
                    intervalCopyOCBC = interval;
                    processingOCBC = true;
                    if ('Enable' == document.getElementById("<%=OCBCHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("CITI") == 21) {
                    intervalCopyCITI = interval;
                    processingCITI = true;
                    if ('Enable' == document.getElementById("<%=CITIHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                 else if (btnDeal.indexOf("LEONTEQ") == 21) {
                    intervalCopyLEONTEQ = interval;
                    processingLEONTEQ = true;
                    if ('Enable' == document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("COMMERZ") == 21) {
                    intervalCopyCOMMERZ = interval;
                    processingCOMMERZ = true;
                    if ('Enable' == document.getElementById("<%=COMMERZHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                if (checkResetflag == false) {
                    $.ajax({
                        type: "POST",
                        url: "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx/Web_CheckForAsyncPriceResponsewithMail_Rewrite",
                        data: '{"strRFQID":"' + RFQId + '","strTimeout":"' + time + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        cache: false,
                        success: function(msg) {
                            if (msg.d) {
                                setPriceAllEnableDisable()
                                if ($.trim(msg.d) == 'Error') {

                                }
                                else if ($.trim(msg.d) == 'Rejected') {
                                    $("#" + lblPrice).text(msg.d);
                                    setPriceAllEnableDisable();
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = false;

                                    //document.getElementById(lblPrice).style.color = "red";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblRejected");
                                    $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
                                    if (btnDeal.indexOf("JPM") == 21) {
                                        JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                                        JpmHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        processingDBIB = false;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = false;
                                    }
                                      else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        processingCOMMERZ = false;
                                    }
                                    clearInterval(interval);
                                    setTimeout(setPriceAllEnableDisable, 1000);
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                }
                                else if ($.trim(msg.d) == 'Timeout') {
                                    $("#" + lblPrice).text('Timeout');
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = false;
                                    //document.getElementById(lblPrice).style.color = "red";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblTimeout");
                                    $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
                                    clearInterval(interval);
                                    if (btnDeal.indexOf("JPM") == 21) {
                                        JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                                        JpmHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        processingDBIB = false;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = false;
                                    }
                                   else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        processingCOMMERZ = false;
                                    } 
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                    setTimeout(setPriceAllEnableDisable, 500);
                                }

                                else {
                                    AllHiddenPrice = 'Disable';
                                    $("#" + lblPrice).text(parseFloat(msg.d).toFixed(2));
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = true;
                                    //document.getElementById(lblPrice).style.color = "green";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblPrice");
                                    $("#" + btnPrice1).removeClass("btn").addClass("btnDisabled");
                                    $("#" + lblPrice).text(parseFloat(msg.d).toFixed(2));
                                    $("#" + btnPrice1).val('Order');
                                    if (btnDeal.indexOf("JPM") == 21) {
                                        JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                                        JpmHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = true;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = true;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = true;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = true;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = true;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = true;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        processingDBIB = true;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = true;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = true;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = true;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        processingCOMMERZ = true;
                                    }
                                    clearInterval(interval);
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                    InitializeTimer(lblTimer, orderValiditySec, btnDeal, btnPrice1);
                                    setTimeout(setPriceAllEnableDisable, 1500);
                                }
                            }
                            else {


                            }
                        },
                        error: function(jqXHR, exception, t) {
                            setPriceAllEnableDisable();
                            if (new Date().getTime() - startTime > pollingMilliSec) {
                                clearInterval(interval);
                                setTimeout(setPriceAllEnableDisable, 500);
                                return;
                            }
                            alert(jqXHR.responseText);

                        }
                    });
                }
                else {
                    clearInterval(interval);
                }
            }, 5000);

        }


    
        function getStrike(RFQId, lblPrice, lblTimer, btnDeal, btnPrice1) {
            var checkResetflag = false;
            var time = pollingMilliSec;
            var startTime = new Date().getTime();
            if (btnDeal.indexOf("JPM") == 21) {
                processingJPM = true;
            }
            else if (btnDeal.indexOf("HSBC") == 21) {
                processingHSBC = true;
            }
            else if (btnDeal.indexOf("CS") == 21) {
                processingCS = true;
            }
            else if (btnDeal.indexOf("UBS") == 21) {
                processingUBS = true;
            }
            else if (btnDeal.indexOf("BAML") == 21) {
                processingBAML = true;
            }
            else if (btnDeal.indexOf("BNPP") == 21) {
                processingBNPP = true;
            }
           else if (btnDeal.indexOf("DBIB") == 21) {
                processingDBIB = true;
            }
            else if (btnDeal.indexOf("OCBC") == 21) {
                processingOCBC = true;
            }
            else if (btnDeal.indexOf("CITI") == 21) {
                processingCITI = true;
            }
            else if (btnDeal.indexOf("LEONTEQ") == 21) {
                processingLEONTEQ = true;
            }
            else if (btnDeal.indexOf("COMMERZ") == 21) {
                processingCOMMERZ = true;
            }
            var interval = setInterval(function() {
                if (btnDeal.indexOf("JPM") == 21) {
                    intervalCopyJPM = interval;
                    processingJPM = true;
                    if ('Enable' == document.getElementById("<%=JpmHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("HSBC") == 21) {
                    intervalCopyHSBC = interval;
                    processingHSBC = true;
                    if ('Enable' == document.getElementById("<%=HsbcHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("CS") == 21) {
                    intervalCopyCS = interval;
                    processingCS = true;
                    if ('Enable' == document.getElementById("<%=CsHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("UBS") == 21) {
                    intervalCopyUBS = interval;
                    processingUBS = true;
                    if ('Enable' == document.getElementById("<%=UbsHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("BAML") == 21) {
                    intervalCopyBAML = interval;
                    processingBAML = true;
                    if ('Enable' == document.getElementById("<%=BAMLHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("BNPP") == 21) {
                    intervalCopyBNPP = interval;
                    processingBNPP = true;
                    if ('Enable' == document.getElementById("<%=BNPPHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("DBIB") == 21) {
                    intervalCopyDBIB = interval;
                    processingDBIB = true;
                    if ('Enable' == document.getElementById("<%=DBIBHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("OCBC") == 21) {
                    intervalCopyOCBC = interval;
                    processingOCBC = true;
                    if ('Enable' == document.getElementById("<%=OCBCHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("CITI") == 21) {
                    intervalCopyCITI = interval;
                    processingCITI = true;
                    if ('Enable' == document.getElementById("<%=CITIHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("LEONTEQ") == 21) {
                    intervalCopyLEONTEQ = interval;
                    processingLEONTEQ = true;
                    if ('Enable' == document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("COMMERZ") == 21) {
                    intervalCopyCOMMERZ = interval;
                    processingCOMMERZ = true;
                    if ('Enable' == document.getElementById("<%=COMMERZHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                if (checkResetflag == false) {
                    $.ajax({
                        type: "POST",
                        url: "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx/Web_CheckForAsyncStrikeResponsewithMail_Rewrite",
                        data: '{"strRFQID":"' + RFQId + '","strTimeout":"' + time + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        cache: false,
                        success: function(msg) {
                            if (msg.d) {

                                setPriceAllEnableDisable()
                                if ($.trim(msg.d) == 'Error') {

                                }
                                else if ($.trim(msg.d) == 'Rejected') {
                                    $("#" + lblPrice).text(msg.d);
                                    setPriceAllEnableDisable();
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = false;

                                    //document.getElementById(lblPrice).style.color = "red";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblRejected");
                                    $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
                                    if (btnDeal.indexOf("JPM") == 21) {
                                        JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                                        JpmHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        processingDBIB = false;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = false;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        processingCOMMERZ = false;
                                    }
                                    clearInterval(interval);
                                    setTimeout(setPriceAllEnableDisable, 1000);
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                }
                                else if ($.trim(msg.d) == 'Timeout') {
                                    $("#" + lblPrice).text('Timeout');
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = false;

                                    //document.getElementById(lblPrice).style.color = "red";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblTimeout");
                                    $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
                                    clearInterval(interval);
                                    if (btnDeal.indexOf("JPM") == 21) {
                                        JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                                        JpmHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        processingDBIB = false;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = false;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        processingCOMMERZ = false;
                                    }
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                    setTimeout(setPriceAllEnableDisable, 500);

                                }
                                else {
                                    AllHiddenPrice = 'Disable';
                                    $("#" + lblPrice).text(parseFloat(msg.d).toFixed(2));
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = true;

                                    //document.getElementById(lblPrice).style.color = "green";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblPrice");
                                    $("#" + btnPrice1).removeClass("btn").addClass("btnDisabled");
                                    $("#" + lblPrice).text(parseFloat(msg.d).toFixed(2));
                                    $("#" + btnPrice1).val('Order');
                                    if (btnDeal.indexOf("JPM") == 21) {
                                        JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                                        JpmHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = true;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = true;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = true;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = true;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = true;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = true;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        processingDBIB = true;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = true;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = true;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = true;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        processingCOMMERZ = true;
                                    }
                                    clearInterval(interval);
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                    InitializeTimer(lblTimer, orderValiditySec, btnDeal, btnPrice1);
                                    setTimeout(setPriceAllEnableDisable, 1000);

                                }
                            }
                            else {

                            }
                        },
                        error: function(jqXHR, exception, t) {
                            setPriceAllEnableDisable();
                            if (new Date().getTime() - startTime > pollingMilliSec) {
                                clearInterval(interval);
                                setTimeout(setPriceAllEnableDisable, 500);
                                return;
                            }
                            alert(jqXHR.responseText);

                        }
                    });
                }
                else {
                    clearInterval(interval);
                }
            }, 5000);

        }



        function getCoupon(RFQId, lblPrice, lblTimer, btnDeal, btnPrice1) {
            var checkResetflag = false;
            var time = pollingMilliSec;
            var startTime = new Date().getTime();
            if (btnDeal.indexOf("JPM") == 21) {
                processingJPM = true;
            }
            else if (btnDeal.indexOf("HSBC") == 21) {
                processingHSBC = true;
            }
            else if (btnDeal.indexOf("CS") == 21) {
                processingCS = true;
            }
            else if (btnDeal.indexOf("UBS") == 21) {
                processingUBS = true;
            }
            else if (btnDeal.indexOf("BAML") == 21) {
                processingBAML = true;
            }
            else if (btnDeal.indexOf("BNPP") == 21) {
                processingBNPP = true;
            }
            else if (btnDeal.indexOf("DBIB") == 21) {
                processingDBIB = true;
            }
            else if (btnDeal.indexOf("OCBC") == 21) {
                processingOCBC = true;
            }
            else if (btnDeal.indexOf("CITI") == 21) {
                processingCITI = true;
            }
             else if (btnDeal.indexOf("LEONTEQ") == 21) {
                processingLEONTEQ = true;
            }
            else if (btnDeal.indexOf("COMMERZ") == 21) {
                processingCOMMERZ = true;
            }
            var interval = setInterval(function() {

                if (btnDeal.indexOf("JPM") == 21) {
                    intervalCopyJPM = interval;
                    processingJPM = true;
                    if ('Enable' == document.getElementById("<%=JpmHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("HSBC") == 21) {
                    intervalCopyHSBC = interval;
                    processingHSBC = true;
                    if ('Enable' == document.getElementById("<%=HsbcHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("CS") == 21) {
                    intervalCopyCS = interval;
                    processingCS = true;
                    if ('Enable' == document.getElementById("<%=CsHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("UBS") == 21) {
                    intervalCopyUBS = interval;
                    processingUBS = true;
                    if ('Enable' == document.getElementById("<%=UbsHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("BAML") == 21) {
                    intervalCopyBAML = interval;
                    processingBAML = true;
                    if ('Enable' == document.getElementById("<%=BAMLHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("BNPP") == 21) {
                    intervalCopyBNPP = interval;
                    processingBNPP = true;
                    if ('Enable' == document.getElementById("<%=BNPPHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("DBIB") == 21) {
                    intervalCopyDBIB = interval;
                    processingDBIB = true;
                    if ('Enable' == document.getElementById("<%=DBIBHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("OCBC") == 21) {
                    intervalCopyOCBC = interval;
                    processingOCBC = true;
                    if ('Enable' == document.getElementById("<%=OCBCHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("CITI") == 21) {
                    intervalCopyCITI = interval;
                    processingCITI = true;
                    if ('Enable' == document.getElementById("<%=CITIHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("LEONTEQ") == 21) {
                    intervalCopyLEONTEQ = interval;
                    processingLEONTEQ = true;
                    if ('Enable' == document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else if (btnDeal.indexOf("COMMERZ") == 21) {
                    intervalCopyCOMMERZ = interval;
                    processingCOMMERZ = true;
                    if ('Enable' == document.getElementById("<%=COMMERZHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                if (checkResetflag == false) {
                    $.ajax({
                        type: "POST",
                        url: "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx/Web_CheckForAsyncCouponResponsewithMail_Rewrite",
                        data: '{"strRFQID":"' + RFQId + '","strTimeout":"' + time + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        cache: false,
                        success: function(msg) {
                            if (msg.d) {

                                setPriceAllEnableDisable()
                                if ($.trim(msg.d) == 'Error') {

                                }
                                else if ($.trim(msg.d) == 'Rejected') {
                                    $("#" + lblPrice).text(msg.d);
                                    setPriceAllEnableDisable();

                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = false;

                                    //document.getElementById(lblPrice).style.color = "red";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblRejected");
                                    $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
                                    if (btnDeal.indexOf("JPM") == 21) {
                                        JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                                        JpmHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        processingDBIB = false;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = false;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        processingCOMMERZ = false;
                                    }
                                    clearInterval(interval);
                                    setTimeout(setPriceAllEnableDisable, 1000);
                                    document.getElementById("<%=btnLoad.ClientID %>").click(); //'<AvinashG. on 28-Apr-2014: Update grid after Rejection comes up>
                                }
                                else if ($.trim(msg.d) == 'Timeout') {
                                    $("#" + lblPrice).text('Timeout');
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = false;
                                    //document.getElementById(lblPrice).style.color = "red";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblTimeout");
                                    $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
                                    clearInterval(interval);
                                    if (btnDeal.indexOf("JPM") == 21) {
                                        JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                                        JpmHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        processingDBIB = false;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = false;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        processingCOMMERZ = false;
                                    }
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                    setTimeout(setPriceAllEnableDisable, 500);

                                }

                                else {
                                    AllHiddenPrice = 'Disable';
                                    $("#" + lblPrice).text(parseFloat(msg.d).toFixed(2));
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = true;
                                    //document.getElementById(lblPrice).style.color = "green";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblPrice");
                                    $("#" + btnPrice1).removeClass("btn").addClass("btnDisabled");
                                    $("#" + lblPrice).text(parseFloat(msg.d).toFixed(2));
                                    $("#" + btnPrice1).val('Order');
                                    if (btnDeal.indexOf("JPM") == 21) {
                                        JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                                        JpmHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = true;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = true;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = true;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = true;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = true;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = true;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        processingDBIB = true;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = true;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = true;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = true;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        processingCOMMERZ = true;
                                    }
                                    clearInterval(interval);
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                    InitializeTimer(lblTimer, orderValiditySec, btnDeal, btnPrice1);
                                    setTimeout(setPriceAllEnableDisable, 1000);
                                }
                            }
                            else {

                            }
                        },
                        error: function(jqXHR, exception, t) {
                            setPriceAllEnableDisable();
                            if (new Date().getTime() - startTime > pollingMilliSec) {
                                clearInterval(interval);
                                setTimeout(setPriceAllEnableDisable, 500);
                                return;
                            }
                            alert(jqXHR.responseText);

                        }
                    });
                }
                else {
                    clearInterval(interval);
                }
            }, 5000);

        }


        function StopAllTimers() {
            document.getElementById("<%=lblTimer.ClientID %>").innerHTML = "";
            clearTimeout(tmrJPM);
            
            document.getElementById("<%=lblTimerHSBC.ClientID %>").innerHTML = "";
            clearTimeout(tmrHSBC);

            document.getElementById("<%=lblTimerCS.ClientID %>").innerHTML = "";
            clearTimeout(tmrCS);
            
            document.getElementById("<%=lblUBSTimer.ClientID %>").innerHTML = "";
            clearTimeout(tmrUBS);
            
            document.getElementById("<%=lblTimerBAML.ClientID %>").innerHTML = "";
            clearTimeout(tmrBAML);

            document.getElementById("<%=lblTimerBNPP.ClientID %>").innerHTML = "";
            clearTimeout(tmrBNPP);

            document.getElementById("<%=lblTimerDBIB.ClientID %>").innerHTML = "";
            clearTimeout(tmrDBIB);
            
            document.getElementById("<%=lblTimerOCBC.ClientID %>").innerHTML = "";
            clearTimeout(tmrOCBC);
            
            document.getElementById("<%=lblTimerCITI.ClientID %>").innerHTML = "";
            clearTimeout(tmrCITI);
            
            document.getElementById("<%=lblTimerLEONTEQ.ClientID %>").innerHTML = "";
            clearTimeout(tmrLEONTEQ);
                        
            document.getElementById("<%=lblTimerCOMMERZ.ClientID %>").innerHTML = "";
            clearTimeout(tmrCOMMERZ);
            
            JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
            JpmHiddenPrice.value = ';Disable;Disable;Disable;Price';
            HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
            HsbcHiddenPrice.value = ';Disable;Disable;Disable;Price';
            CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
            CsHiddenPrice.value = ';Disable;Disable;Disable;Price';
            UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
            UbsHiddenPrice.value = ';Disable;Disable;Disable;Price';
            BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
            BAMLHiddenPrice.value = ';Disable;Disable;Disable;Price';
            BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
            BNPPHiddenPrice.value = ';Disable;Disable;Disable;Price';
            DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
            DBIBHiddenPrice.value = ';Disable;Disable;Disable;Price';
            OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
            OCBCHiddenPrice.value = ';Disable;Disable;Disable;Price';
            CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
            CITIHiddenPrice.value = ';Disable;Disable;Disable;Price';
            LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
            LEONTEQHiddenPrice.value = ';Disable;Disable;Disable;Price';
            COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
            COMMERZHiddenPrice.value = ';Disable;Disable;Disable;Price';
        }

        function StopPPTimerValue(btnDeal) {
            if (btnDeal.indexOf("JPM") == 21) {
                clearTimeout(tmrJPM);
                $("#JPMwait").hide();
                if (document.getElementById('JPMwait') != null) {
                    document.getElementById('JPMwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("HSBC") == 21) {
                clearTimeout(tmrHSBC);
                $("#HSBCwait").hide();
                if (document.getElementById('HSBCwait') != null) {
                    document.getElementById('HSBCwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("CS") == 21) {
                clearTimeout(tmrCS);
                $("#CSwait").hide();
                if (document.getElementById('CSwait') != null) {
                    document.getElementById('CSwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("UBS") == 21) {
                clearTimeout(tmrUBS);
                $("#UBSwait").hide();
                if (document.getElementById('UBSwait') != null) {
                    document.getElementById('UBSwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("BAML") == 21) {
                clearTimeout(tmrBAML);
                $("#BAMLwait").hide();
                if (document.getElementById('BAMLwait') != null) {
                    document.getElementById('BAMLwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("BNPP") == 21) {
                clearTimeout(tmrBNPP);
                $("#BNPPwait").hide();
                if (document.getElementById('BNPPwait') != null) {
                    document.getElementById('BNPPwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("DBIB") == 21) {
                clearTimeout(tmrDBIB);
                $("#DBIBwait").hide();
                if (document.getElementById('DBIBwait') != null) {
                    document.getElementById('DBIBwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("OCBC") == 21) {
                clearTimeout(tmrOCBC);
                $("#OCBCwait").hide();
                if (document.getElementById('OCBCwait') != null) {
                    document.getElementById('OCBCwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("CITI") == 21) {
                clearTimeout(tmrCITI);
                $("#CITIwait").hide();
                if (document.getElementById('CITIwait') != null) {
                    document.getElementById('CITIwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("LEONTEQ") == 21) {
                clearTimeout(tmrLEONTEQ);
                $("#LEONTEQwait").hide();
                if (document.getElementById('LEONTEQwait') != null) {
                    document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                }
            }
            else if (btnDeal.indexOf("COMMERZ") == 21) {
                clearTimeout(tmrCOMMERZ);
                $("#COMMERZwait").hide();
                if (document.getElementById('COMMERZwait') != null) {
                    document.getElementById('COMMERZwait').style.visibility = 'hidden';
                }
            }
        }

        function StopPPTimer(lblPrice, btnDeal, btnPrice1) {
            setPriceAllEnableDisable();
            $("#" + lblPrice).text("");
            document.getElementById(btnPrice1).disabled = false;
            document.getElementById(btnDeal).disabled = true;
            $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
            $("#" + btnDeal).removeClass("btn").addClass("btnDisabled");
            if (btnDeal.indexOf("JPM") == 21) {
                clearTimeout(tmrJPM);
                JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                JpmHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#JPMwait").hide();
            }
            else if (btnDeal.indexOf("HSBC") == 21) {
                clearTimeout(tmrHSBC);
                HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                HsbcHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#HSBCwait").hide();
            }
            else if (btnDeal.indexOf("CS") == 21) {
                clearTimeout(tmrCS);
                CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                CsHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#CSwait").hide();
            }
            else if (btnDeal.indexOf("UBS") == 21) {
                clearTimeout(tmrUBS);
                UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                UbsHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#UBSwait").hide();
            }
            else if (btnDeal.indexOf("BAML") == 21) {
                clearTimeout(tmrBAML);
                BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                BAMLHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#BAMLwait").hide();
            }
            else if (btnDeal.indexOf("BNPP") == 21) {
                clearTimeout(tmrBNPP);
                BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                BNPPHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#BNPPwait").hide();
            }
            else if (btnDeal.indexOf("DBIB") == 21) {
                clearTimeout(tmrDBIB);
                DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                DBIBHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#DBIBwait").hide();
            }
            else if (btnDeal.indexOf("OCBC") == 21) {
                clearTimeout(tmrOCBC);
                OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                OCBCHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#OCBCwait").hide();
            }
            else if (btnDeal.indexOf("CITI") == 21) {
                clearTimeout(tmrCITI);
                CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                CITIHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#CITIwait").hide();
            }
            else if (btnDeal.indexOf("LEONTEQ") == 21) {
                clearTimeout(tmrLEONTEQ);
                LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                LEONTEQHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#LEONTEQwait").hide();
            }
            else if (btnDeal.indexOf("COMMERZ") == 21) {
                clearTimeout(tmrCOMMERZ);
                COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                COMMERZHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#COMMERZwait").hide();
            }
        }

        function InitializeTimer(lblid, ValidityTime, btnDeal, btnPrice) {
            setTimeout(setPriceAllEnableDisable, 500);
            if (ValidityTime == "") ValidityTime = orderValiditySec;
            document.getElementById(lblid).innerHTML = Pad(ValidityTime);
            if (ValidityTime < 20) { document.getElementById(lblid).style.color = "red"; }
            ValidityTime = ValidityTime - 1;
            if (ValidityTime <= 0) {
                if (btnDeal.indexOf("JPM") == 21) {
                    JpmHiddenPrice = document.getElementById("<%=JpmHiddenPrice.ClientID %>");
                    JpmHiddenPrice.value = JpmHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrJPM);
                    processingJPM = false;
                }
                else if (btnDeal.indexOf("HSBC") == 21) {
                    HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                    HsbcHiddenPrice.value = HsbcHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrHSBC);
                    processingHSBC = false;
                }
                else if (btnDeal.indexOf("CS") == 21) {
                    CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                    CsHiddenPrice.value = CsHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrCS);
                    processingCS = false;
                }
                else if (btnDeal.indexOf("UBS") == 21) {
                    UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                    UbsHiddenPrice.value = UbsHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrUBS);
                    processingUBS = false;
                }
                else if (btnDeal.indexOf("BAML") == 21) {
                    BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                    BAMLHiddenPrice.value = BAMLHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrBAML);
                    processingBAML = false;
                }
                else if (btnDeal.indexOf("BNPP") == 21) {
                    BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                    BNPPHiddenPrice.value = BNPPHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrBNPP);
                    processingBNPP = false;
                }
                else if (btnDeal.indexOf("DBIB") == 21) {
                    DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                    DBIBHiddenPrice.value = DBIBHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrDBIB);
                    processingDBIB = false;
                }
                else if (btnDeal.indexOf("OCBC") == 21) {
                    OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                    OCBCHiddenPrice.value = OCBCHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrOCBC);
                    processingOCBC = false;
                }
                else if (btnDeal.indexOf("CITI") == 21) {
                    CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                    CITIHiddenPrice.value = CITIHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrCITI);
                    processingCITI = false;
                }
                else if (btnDeal.indexOf("LEONTEQ") == 21) {
                    LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                    LEONTEQHiddenPrice.value = LEONTEQHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrLEONTEQ);
                    processingLEONTEQ = false;
                }
                else if (btnDeal.indexOf("COMMERZ") == 21) {
                    COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                    COMMERZHiddenPrice.value = COMMERZHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrCOMMERZ);
                    processingCOMMERZ = false;
                }
                document.getElementById(lblid).innerHTML = "";
                if ($("#ctl00_MainContent_DealConfirmPopup").is(':visible')) {
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "BNPP") {
                        if (processingBNPP == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "CS") {
                        if (processingCS == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "HSBC") {
                        if (processingHSBC == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "JPM") {
                        if (processingJPM == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text().toUpperCase() == "UBS") {
                        if (processingUBS == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text().toUpperCase() == "BAML") {
                        if (processingBAML == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
//                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "DBIB") {
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "DB") {
                        if (processingDBIB == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "OCBC") {
                        if (processingOCBC == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "CITI") {
                        if (processingCITI == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "LEONTEQ") {
                        if (processingLEONTEQ == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "COMMERZ") {
                        if (processingCOMMERZ == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }
                }
                if (document.getElementById(btnPrice) != null) {
                    document.getElementById(btnPrice).disabled = false;
                    $("#" + btnPrice).removeClass("btnDisabled").addClass("btn");
                    $("#" + btnPrice).val('Price');
                }
                document.getElementById("ctl00_MainContent_btnLoad").click(); 
            }
            else {
                if (btnDeal.indexOf("JPM") == 21) {
                    tmrJPM = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("HSBC") == 21) {
                    tmrHSBC = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("CS") == 21) {
                    tmrCS = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("UBS") == 21) {
                    tmrUBS = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("BAML") == 21) {
                    tmrBAML = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("BNPP") == 21) {
                    tmrBNPP = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("DBIB") == 21) {
                    tmrDBIB = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("OCBC") == 21) {
                    tmrOCBC = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("CITI") == 21) {
                    tmrCITI = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("LEONTEQ") == 21) {
                    tmrLEONTEQ = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else if (btnDeal.indexOf("COMMERZ") == 21) {
                    tmrCOMMERZ = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
            }
        }

      

        function OnFocusSelectText(id) {
            document.getElementById(id).focus();
            document.getElementById(id).select();
        }
   
        function postbackButtonClick() {
            return true;


        }

        function StopTimer1(btnhdnSolveAllRequests) {

        }

        function StopTimer2(btnhdnSolveSingleRequest) {

        }

        function StopTimer(lblid, btnDeal) {
            try {
                document.getElementById(lblid).innerHTML = "";
            }
            catch (Error) {

            }


        }


        function InitializeTimer1(lblid1, ValidityTime1, btnhdnEnableDisableDealButtons) {
            if (ValidityTime1 == "") ValidityTime1 = orderValiditySec;

            try {
                document.getElementById(lblid1).innerHTML = Pad(ValidityTime1);
            }
            catch (Error)
            { prompt("err0"); }



            if (ValidityTime1 < 20) {
                try {
                    document.getElementById(lblid1).style.color = "red";
                }
                catch (Error)
            { prompt("err1"); }

            }
            ValidityTime1 = ValidityTime1 - 1;
            if (ValidityTime1 <= 0) {
                clearTimeout(tmr1);
                document.getElementById(lblid1).innerHTML = "";
                $("#ctl00_MainContent_DealConfirmPopup").css('visibility', 'hidden');

                try {
                    updateProgress.set_visible(false);
                }
                catch (Error)
                { }


            }
            else {
                tmr1 = self.setTimeout("InitializeTimer1('" + lblid1 + "','" + ValidityTime1 + "','" + btnhdnEnableDisableDealButtons + "');", 1000);
            }

        }
        function Pad(number) {
            if (number < 10) number = 0 + "" + number; return number;
        }

        function SpecialCharacterNotAllowed(tbControl) {
            var ControlText = document.getElementById(tbControl).value
            var C, d;
            CheckStartSpace(tbControl)
            if (ControlText.length > 0) {
                for (i = 0; i < ControlText.length; i++) {
                    d = ControlText.charAt(i)

                    if (((d >= 'a') && (d <= 'z')) || ((d >= 'A') && (d <= 'Z')) || ((d <= '') && (d >= ' ')) || ((d >= '0') && (d <= '9')) || (d == '-') || (d == ' ')) {
                    }
                    else {
                        C = ControlText.substring(0, i)
                        document.getElementById(tbControl).value = C
                        document.getElementById(tbControl).focus();

                        return false
                    }
                }
            }

        }
        function CheckStartSpace(tbControl) {
            var ControlText = document.getElementById(tbControl).value;
            var C = ControlText.substring(0, ControlText.length);

            if (ControlText.charAt(0) == ' ') {
                var pos = 0;
                for (var i = 0; i < ControlText.length; i++) {
                    if (ControlText.charAt(i) == ' ')
                        pos = pos + 1;
                    if (ControlText.charAt(i) != '')
                        break;
                }
                document.getElementById(tbControl).value = ControlText.substring(pos, ControlText.length);
            }

        }

        function UpdateTab() {
            $('#ctl00_MainContent_upnl2').hide();
            $("#tdDRAFCN").css("width", 720);
            //if (sender.get_activeTabIndex().toString() == 1 || sender.get_activeTabIndex().toString() == 2) {
                $("#tdDRAFCN").append("<div style='text-align:center; margin-top:40px; height:125px;'><img src='../App_Resources/ajax-loader7.gif' id='Img2' width='50px' height='50px' alt='x' /><div style='text-align:center;'>Loading...</div></div>");
            //}
              
                         __doPostBack('<%= upnl2.ClientID %>', '');
               }

     
        function OnHover(val) {
            val.style.backgroundColor = "#FFF";
        }
        function OnOut(val) {
            val.style.backgroundColor = "#DDD";
        }
        setResolution();
        $(document).ready(function() {
        // hideLPBoxes(); ''<Nikhil M. on 17-Sep-2016: Commented>
            setResolution();

        });
        $(window).resize(function() {
            setResolution();
        });

        function setResolution() {
            var viewportWidth;
            if (document.compatMode === 'BackCompat') {
                viewportWidth = document.body.clientWidth;
            } else {
                viewportWidth = document.documentElement.clientWidth;
            }

            $(".gridScroll").width((Number(viewportWidth) - 20).toString() + "px");
        }
        function mailtoMail() {
            var formattedBody;
            var mailSubject;
            mailSubject = $('#ctl00_MainContent_lblMailComentry').text().substring(0, $('#ctl00_MainContent_lblMailComentry').text().indexOf("#"));
            if (isIE() && isIE() < 9) {

                formattedBody = $('#ctl00_MainContent_lblMailComentry').text().replace(/#/g, "\n");

            } else {
                formattedBody = document.getElementById('ctl00_MainContent_lblMailComentry').innerHTML.replace(/#/g, "");
            }

            var mailToLink = "mailto:?subject=" + mailSubject + "&body=" + encodeURIComponent(formattedBody);
            window.location.href = mailToLink;
        }
        //Mohit on 15-Jan-2015
        function OnClientItemsRequestFailedHandler(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
     
    </script>

    <style type="text/css">
        #ctl00_MainContent_chkAddShare2
        {
            margin-left: 0px;
        }
        #ctl00_MainContent_chkAddShare1
        {
            margin-left: 0px;
        }
        #ctl00_MainContent_chkAddShare3
        {
            margin-left: 0px;
        }
        .ddl
        {
            border: none;
        }
        .RadioBtn label
        {
            vertical-align: 3px;
        }
        #ctl00_MainContent_tabContainer_body
        {
            padding-top: 0px;
            padding-bottom: 0px;
        }
        #ctl00_MainContent_tabContainer_body
        {
            border: none;
        }
        #ctl00_MainContent_tabContainer .ajax__tab_body
        {
            border: none;
        }
        #ctl00_MainContent_upnRedirect
        {
            width: 150px;
            display: inline;
        }
        #ctl00_MainContent_UpdateProgress1
        {
            width: 400px;
            display: block;
            position: absolute;
            float: right;
        }
        .cssbtnEMLMail
        {
            /*background-image: url('../App_Resources/email.png'); */
            width: 200px;
            height: 20px;
            float: left;
            border: 0px;
            color: #00ADEF;
            background-color: white;
            text-decoration: underline;
            text-transform: uppercase;
            margin-left: -3px;
            padding-left: 30px;
            text-align: left;
        }
        .show
        {
            display: block !important;
        }
        .hide
        {
            display: none !important;
        }
        .gridScroll
        {
            /*width: 100% !important;*/
        }
        .Grp1
        {
            /*background:#ffdddd; /*light pink*/ /*background:#f4fbf7;*/ /*background-color:#f7f6f3 !important;*/
            background-color: #d6e0ea !important;
        }
        .Grp2
        {
            /*background:#fec4c4;*/ /*background:#dde8ef ; Blue*/
            background-color: #FFFFFF !important; /* White*/
        }
        .fieldInfoNA
        {
            background-color: #FFF;
            font-size: 12px !important;
            color: White;
            width: 14px !important;
            font-weight: bold;
            text-align: center;
            float: left;
            margin-right: 2px;
            cursor: default;
            float: left;
        }
        #ctl00_MainContent_rowAccDecCalculator table tr, ctl00_MainContent_rowELNCalculator table tr
        {
            padding-top: 2px;
        }
        #ctl00_MainContent_rowAccDecCalculator table tr td table tr, ctl00_MainContent_rowELNCalculator table tr td table tr
        {
            padding-top: 2px !important;
        }
        #ctl00_MainContent_upnlCommentry
        {
            float: left;
        }
        .grayBorder
        {
            border-color: #D5D5D5 !important;
            height: 30px !important;
        }
        .BestPriceHighlight
        {
            background-color: #F5FAFD !important;
        }
        
          /* AshwiniP on 11-Nov-2016 */ 
          .RadDropDownList_Default .rddlItem
             {
             	 margin: 0 1px;
                 padding: 5px 6px;
                 white-space: pre;
             }
    </style>

    <script type="text/javascript">
        //Rushikesh/Mohit/Dilkhush 14Jan2016 for share search
        function OnClientRequesting(sender, args) {
            var context = args.get_context()
            context["iMarketype"] = "EQ";
            context["iShareVAl"] = "";
            if (document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "NO" || document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "N") {
                // if ("<%=ddlExchangeDRAFCN.Visible%>" == "True") {
                var seXchange = document.getElementById("<%=ddlExchangeDRAFCN.clientID%>").value;
                context["sExchange"] = seXchange;
            }
            else {
                context["sExchange"] = "All";
            }
        }
        function OnClientRequesting2(sender, args) {
            var context = args.get_context()
            context["iMarketype"] = "EQ";
            context["iShareVAl"] = "";
            if (document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "NO" || document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "N") {
                //  if ("<%=ddlExchangeDRAFCN2.Visible%>" == "True") {
                var seXchange = document.getElementById("<%=ddlExchangeDRAFCN2.clientID%>").value;
                context["sExchange"] = seXchange;
            }
            else {
                context["sExchange"] = "All";
            }
        }
        function OnClientRequesting3(sender, args) {
            var context = args.get_context()
            context["iMarketype"] = "EQ";
            context["iShareVAl"] = "";
            if (document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "NO" || document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "N") {
                //   if ("<%=ddlExchangeDRAFCN3.Visible%>" == "True") {
                var seXchange = document.getElementById("<%=ddlExchangeDRAFCN3.clientID%>").value;
                context["sExchange"] = seXchange;
            }
            else {
                context["sExchange"] = "All";
            }
     }
    </script>

    <table cellspacing="0" cellpadding="1em" width="98%">
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td align="left" valign="top">
                            <ajax:TabContainer ID="tabContainer" runat="server" ActiveTabIndex="1" Style="vertical-align: top"
                                AutoPostBack="false" OnClientActiveTabChanged="UpdateTab">
                                <ajax:TabPanel ID="tabPanelELN" runat="server" TabIndex="0" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;ELN&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelFCN" runat="server" TabIndex="1" Height="160px">
                                    <HeaderTemplate>
                                        &nbsp;FCN&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelDRA" runat="server" TabIndex="2" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;DRA&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelAQDQ" runat="server" TabIndex="3" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;Accu&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelDQ" runat="server" TabIndex="4" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;Decu&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelEQO" runat="server" TabIndex="5" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;Options&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                            </ajax:TabContainer>
                        </td>
                        <td colspan="3" align="left" valign="top" class="Filter" style="border-top-width: 0px;
                            border-left-width: 0px; border-bottom-width: 0px;" runat="server" id="tdShareGraphData">
                            <asp:UpdatePanel ID="updShareGraphData" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table cellpadding="0px" cellspacing="0px" style="margin-left: 0px; width: 100%">
                                        <tr>
                                            <td style="vertical-align: top;" align="left" valign="top">
                                                <div class="ajax__tab_header">
                                                    <table cellpadding="0px" cellspacing="0px" style="border: none; float: left">
                                                        <tr>
                                                            <td>
                                                                <div id="div_RM_Limit" runat="server" visible="false" style="background-color: #336699;
                                                                    color: White; padding: 5px; width: 147px; text-align: center">
                                                                    <asp:Label runat="server" ID="txt_RM_Limit" Text="Max User Limit: " ToolTip="User Limit-"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td id="tdrblShareData" runat="server" style="padding-left: 0px; white-space: nowrap">
                                                                <asp:RadioButtonList ID="rblShareData" runat="server" RepeatDirection="Horizontal"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Text="Share Info." Enabled="true" Value="SHAREDATA" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Quote/Order Graph" Enabled="true" Value="GRAPHDATA"></asp:ListItem>
                                                                    <asp:ListItem Text="Show Calculator" Enabled="true" Value="calc"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <img src="../App_Resources/user_suit.png" style="border: 0px; width: 20px; height: 20px;"
                                                                    alt="RFQ RM" visible="false" title="RFQ RM" />
                                                            </td>
                                                            <td>
                                                                <%--<asp:DropDownList ID="ddlRFQRM" runat="server" CssClass="styled-select" Width="200"
                                                                    Height="20px" AutoPostBack="true">
                                                                </asp:DropDownList>--%>
                                                                <telerik:RadDropDownList ID="ddlRFQRM" runat="server" AutoPostBack="true">
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEntity" runat="server" Text="Entity" Height="22px" CssClass="lbl"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlentity" runat="server" Width="120px" Height="20px"
                                                                    AutoPostBack="true" Style="display: none;">
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <img src="../App_Resources/account.png" style="border: 0px; width: 0px; height: 0px;"
                                                                    alt="Dealing Branch" visible="false" />
                                                                <asp:Label ID="lblAccount" runat="server" Text="Account" Height="22px" CssClass="lbl"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlAccount" runat="server" Width="80px" Height="20px"
                                                                    AutoPostBack="false" Visible="false">
                                                                    <Items>
                                                                        <telerik:DropDownListItem Text="SG A/C" Value="1" />
                                                                        <telerik:DropDownListItem Text="HK A/C" Value="2" />
                                                                    </Items>
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlBranch" runat="server" Width="120px" Height="20px"
                                                                    Visible="false">
                                                                </telerik:RadDropDownList>
                                                                <asp:Label ID="lblbranch" runat="server" Text="" CssClass="lbl" Height="22px" ForeColor="blue"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rblShareData" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <!--Separate Deal Controls-->
                    <tr>
                        <td id="tdDRAFCN" style="width: 760px; padding: 8px; border: 1px solid #d5d5d5;"
                            align="left" valign="top">
                            <asp:UpdatePanel ID="upnl2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="panelDRAFCN" runat="server">
                                        <table cellpadding="1px" cellspacing="1px" style="width: 760px; height: 180px;">
                                            <tr style="display: none">
                                                <td>
                                                    <asp:Label ID="lblDRAType" runat="server" Text="Type" CssClass="lbl" Width="80px"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDropDownList ID="ddlDRAType" runat="server" Width="105px" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="DRA" Text="DRA" />
                                                            <telerik:DropDownListItem Value="FCN" Text="FCN" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </td>
                                                <td style="padding-left: 45px;">
                                                    <asp:Label ID="lblAddedshares" CssClass="lbl" runat="server" Text="Underlying/s"
                                                        Visible="false"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr style="padding-bottom: 2px;">
                                                <td style="padding-top: 0px;" colspan="6" rowspan="2">
                                                    <table cellspacing="0" cellpadding="2" id="tblDRABasket">
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSelectionExchangeDRAFCNHeader" runat="server" CssClass="lbl" Text="Exchange"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblShares" runat="server" CssClass="lbl" Text="Basket Underlying(s)"></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;(<asp:Label ID="txtBasketShares" runat="server" CssClass="lbl"
                                                                    Text=" "></asp:Label>)
                                                            </td>
                                                            <td style="width: 30px">
                                                                <asp:Label ID="lblDRACcy" runat="server" CssClass="lbl" Text="Ccy"></asp:Label>
                                                            </td>
                                                            <td style="margin-left: 10px; width: 400px;">
                                                                <asp:Label ID="lblDisplayExchangeDRAFCNHeader" runat="server" Text="Exchange" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPRR" runat="server" Text="PRR " Width="25px" CssClass="lbl" Style="text-align: left"></asp:Label>
                                                            </td>
                                                            <%--Added by Chitralekha on 29-Sep-16--%>
                                                            <td>
                                                                <asp:Label ID="lblAdvisoryFlag" runat="server" Text="Advisory Flag " Width="60px"
                                                                    CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <%--Ended by Chitralekha on 29-Sep-16--%>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-left: 0px;">
                                                                <asp:CheckBox ID="chkAddShare1" runat="server" CssClass="lbl" AutoPostBack="true"
                                                                    Checked="true" Enabled="false" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlExchangeDRAFCN" runat="server" Width="226px" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>

                                                                <script type="text/javascript">
                                                                                function ShowDropDownFunctionDRA() {
                                                                                    var combo1 = $find("<%= ddlShareDRA.ClientID %>");
                                                                                    if (combo1.get_text().length.toString() == '3')
                                                                                        combo1.showDropDown();
                                                                                }
                                                                                function HideDropDownFunctionDRA() {
                                                                                    var combo1 = $find("<%= ddlShareDRA.ClientID %>");
                                                                                    combo1.hideDropDown();
                                                                                }


                                                                                function ShowDropDownFunctionDRA2() {
                                                                                    var combo2 = $find("<%= ddlShareDRA2.ClientID %>");
                                                                                    if (combo2.get_text().length.toString() == '3')
                                                                                        combo2.showDropDown();
                                                                                }
                                                                                function HideDropDownFunctionDRA2() {
                                                                                    var combo2 = $find("<%= ddlShareDRA2.ClientID %>");
                                                                                    combo2.hideDropDown();
                                                                                }
                                                                                function ShowDropDownFunctionDRA3() {
                                                                                    var combo3 = $find("<%= ddlShareDRA3.ClientID %>");
                                                                                    if (combo3.get_text().length.toString() == '3')
                                                                                        combo3.showDropDown();
                                                                                }
                                                                                function HideDropDownFunctionDRA3() {
                                                                                    var combo3 = $find("<%= ddlShareDRA3.ClientID %>");
                                                                                    combo3.hideDropDown();
                                                                                }
                                                                </script>

                                                                <%-- <telerik:RadComboBox ID="ddlShareDRA" runat="server" Filter="Contains" MarkFirstMatch="true"
                                                                                ShowDropDownOnTextboxClick="true" EnableViewState="true" Style="width: 300px;" 
                                                                                AutoPostBack="true" MinFilterLength="3" ForeColor="Black" ShowToggleImage="true" 
                                                                                ToolTip="Search by inputing share name or symbol. Press Enter to select."
                                                                                EmptyMessage="" LoadingMessage="Loading matching shares..."  Font-Names="Arial"
                                                                                OnClientKeyPressing="ShowDropDownFunctionDRA" EnableLoadOnDemand="false" MaxHeight="180" Height="20">
                                                                                  <CollapseAnimation Type="None" />
                                                                                  <ExpandAnimation Type="None" />
                                                                            </telerik:RadComboBox>--%>
                                                                <telerik:RadComboBox ID="ddlShareDRA" runat="server" Height="150" OnClientItemsRequesting="OnClientRequesting"
                                                                    EmptyMessage="" EnableLoadOnDemand="true" ShowMoreResultsBox="false" EnableVirtualScrolling="false"
                                                                    AutoPostBack="true" MarkFirstMatch="true" ShowDropDownOnTextboxClick="true" Style="width: 300px;
                                                                    height: 20px !important;" MinFilterLength="3" ForeColor="Black" ShowToggleImage="true"
                                                                    ToolTip="Search by inputing share name or symbol. Press Enter to select." LoadingMessage="Loading matching shares..."
                                                                    TabIndex="1" OnClientItemsRequestFailed="OnClientItemsRequestFailedHandler">
                                                                </telerik:RadComboBox>
                                                                <%--added by Mohit Lalwani on 15-Jan-2015--%>
                                                                <asp:HiddenField ID="hdnConfig_EQC_Allow_ALL_AsExchangeOption" Value="" runat="server" />
                                                                <%--/added by Mohit Lalwani on 15-Jan-2015--%>
                                                            </td>
                                                            <td style="padding-left: 2px">
                                                                <asp:Label ID="lblDRABaseCcy" runat="server" CssClass="lbl" Width="26px" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblExchangeDRAFCN" runat="server" CssClass="lbl" Text="" Style="display: inline-flex;"></asp:Label>
                                                                <input type="hidden" id="Hdnbasketaddedshares" name="Hdnbasketaddedshares" runat="server"
                                                                    onmouseover="JavaScript:alert:this.focus();" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPRRVal" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <%--Added by Chitralekha on 29-Sep-16--%>
                                                            <td>
                                                                <asp:Label ID="lblAdvisoryFlagVal" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <%--Ended by Chitralekha on 29-Sep-16--%>
                                                        </tr>
                                                        <tr runat="server" id="tblShareDRA_2">
                                                            <td style="padding-left: 0px;">
                                                                <asp:CheckBox ID="chkAddShare2" runat="server" CssClass="lbl" AutoPostBack="true" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlExchangeDRAFCN2" runat="server" Width="226px" AutoPostBack="true"
                                                                    Enabled="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <%--<telerik:RadComboBox ID="ddlShareDRA2" runat="server" Filter="Contains" MarkFirstMatch="true"
                                                                                ShowDropDownOnTextboxClick="true" EnableViewState="true" Style="width: 300px;" 
                                                                                AutoPostBack="true" MinFilterLength="3" ForeColor="Black" ShowToggleImage="true" 
                                                                                ToolTip="Search by inputing share name or symbol. Press Enter to select."
                                                                                EmptyMessage="" LoadingMessage="Loading matching shares..."  Font-Names="Arial"
                                                                                OnClientKeyPressing="ShowDropDownFunctionDRA2" EnableLoadOnDemand="false"
                                                                                MaxHeight="180" Enabled ="false" >
                                                                                  <CollapseAnimation Type="None" />
                                                                                  <ExpandAnimation Type="None" />
                                                                            </telerik:RadComboBox>--%>
                                                                <telerik:RadComboBox ID="ddlShareDRA2" runat="server" Height="150" OnClientItemsRequesting="OnClientRequesting2"
                                                                    EmptyMessage="" EnableLoadOnDemand="true" ShowMoreResultsBox="false" EnableVirtualScrolling="false"
                                                                    AutoPostBack="true" MarkFirstMatch="true" ShowDropDownOnTextboxClick="true" Style="width: 300px;
                                                                    height: 20px !important;" Enabled="false" MinFilterLength="3" ForeColor="Black"
                                                                    ShowToggleImage="true" ToolTip="Search by inputing share name or symbol. Press Enter to select."
                                                                    LoadingMessage="Loading matching shares..." TabIndex="1" OnClientItemsRequestFailed="OnClientItemsRequestFailedHandler">
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td style="padding-left: 2px">
                                                                <asp:Label ID="lblBaseCurrency2" runat="server" CssClass="lbl" Width="26px" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblExchangeDRAFCN2" runat="server" CssClass="lbl" Text="" Style="display: inline-flex;"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="Share" Visible="false"
                                                                    Style="width: 0px; display: none;"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPRRVal2" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <%--Added by Chitralekha on 29-Sep-16--%>
                                                            <td>
                                                                <asp:Label ID="lblAdvisoryFlagVal2" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <%--Ended by Chitralekha on 29-Sep-16--%>
                                                        </tr>
                                                        <tr runat="server" id="tblShareDRA_3" visible="false">
                                                            <td style="padding-left: 0px;">
                                                                <asp:CheckBox ID="chkAddShare3" runat="server" CssClass="lbl" AutoPostBack="true"
                                                                    Enabled="false" />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlExchangeDRAFCN3" runat="server" Width="226px" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <%-- <telerik:RadComboBox ID="ddlShareDRA3" runat="server" Filter="Contains" MarkFirstMatch="true"
                                                                    ShowDropDownOnTextboxClick="true" EnableViewState="true" Style="width: 300px;" 
                                                                    AutoPostBack="true" MinFilterLength="3"
                                                                    ForeColor="Black" ShowToggleImage="true" ToolTip="Search by inputing share name or symbol. Press Enter to select."
                                                                    EmptyMessage="" LoadingMessage="Loading matching shares..."  Font-Names="Arial"
                                                                    OnClientKeyPressing="ShowDropDownFunctionDRA3" EnableLoadOnDemand="false"
                                                                    MaxHeight="180">
                                                                      <CollapseAnimation Type="None" />
                                                                        <ExpandAnimation Type="None" />
                                                                </telerik:RadComboBox>--%>
                                                                <telerik:RadComboBox ID="ddlShareDRA3" runat="server" Height="150" EmptyMessage=""
                                                                    OnClientItemsRequesting="OnClientRequesting3" EnableLoadOnDemand="true" ShowMoreResultsBox="false"
                                                                    EnableVirtualScrolling="false" AutoPostBack="true" MarkFirstMatch="true" ShowDropDownOnTextboxClick="true"
                                                                    Style="width: 300px; height: 20px !important;" MinFilterLength="3" ForeColor="Black"
                                                                    ShowToggleImage="true" ToolTip="Search by inputing share name or symbol. Press Enter to select."
                                                                    LoadingMessage="Loading matching shares..." TabIndex="1" OnClientItemsRequestFailed="OnClientItemsRequestFailedHandler">
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td style="padding-left: 2px">
                                                                <asp:Label ID="lblBaseCurrency3" runat="server" CssClass="lbl" Width="26px" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblExchangeDRAFCN3" runat="server" CssClass="lbl" Text="" Style="display: inline-flex;"></asp:Label>
                                                                <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="Share" Visible="false"
                                                                    Style="display: none;"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPRRVal3" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <%--Added by Chitralekha on 29-Sep-16--%>
                                                            <td>
                                                                <asp:Label ID="lblAdvisoryFlagVal3" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <%--Ended by Chitralekha on 29-Sep-16--%>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: bottom; white-space: nowrap;">
                                                    <table cellspacing="0" cellpadding="0">
                                                        <tr>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSolveForDRA" runat="server" Text="Solve For " CssClass="lbl" Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDropDownList ID="ddlSolveForDRA" runat="server" Width="120px" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="CONVERSION_STRIKE" Text="Strike / Acc (%)" />
                                                            <telerik:DropDownListItem Value="COUPON" Text="Coupon (%)" />
                                                            <telerik:DropDownListItem Value="PRICE" Text="IB Price (%)" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTenorDRA" runat="server" Text="Tenor" CssClass="lbl"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTenorDRA" runat="server" CssClass="txt" Width="20px" AutoPostBack="True"
                                                        Style="text-align: right" MaxLength="3">4</asp:TextBox>&nbsp;
                                                    <%--<Nikhil M. Changed from "3" on 02-Sep-2016: FSD Default >--%>
                                                    <telerik:RadDropDownList ID="ddlTenorTypeDRA" runat="server" Width="105px" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="MONTH" Text="Month" Selected="True" />
                                                            <telerik:DropDownListItem Value="YEAR" Text="Year" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </td>
                                                <td nowrap="nowrap">
                                                    <asp:CheckBox ID="chkKI" runat="server" CssClass="lbl" AutoPostBack="true" />
                                                    <asp:Label ID="lblKIType" runat="server" Text="KI % of Initial" CssClass="lbl "></asp:Label>
                                                </td>
                                                <td style="white-space: nowrap">
                                                    <asp:TextBox ID="txtKILevel" runat="server" CssClass="txt" Style="text-align: right;
                                                        padding-left: 3px;" Enabled="false" BackColor="#F2F2F3" Width="60px" AutoPostBack="true"
                                                        MaxLength="6"></asp:TextBox>
                                                    <telerik:RadDropDownList ID="ddlKIType" runat="server" Width="91px" AutoPostBack="true"
                                                        BackColor="#F2F2F3" Enabled="false">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="AMERICAN" Text="Day Close" />
                                                            <telerik:DropDownListItem Value="EUROPEAN" Text="European" />
                                                            <telerik:DropDownListItem Value="CONTINUOUS" Text="Continuous" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblStrikeDRAFCN" runat="server" Text="Strike (%)" CssClass="lbl" Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStrikeDRAFCN" runat="server" CssClass="txt" Style="text-align: right"
                                                        Width="115px" AutoPostBack="true">92.00</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPrice" runat="server" Text="IB Price (%)" CssClass="lbl" Width="80px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDRAPrice" runat="server" CssClass="txt" Style="text-align: right"
                                                        Width="133px" AutoPostBack="true">99.00</asp:TextBox>
                                                    <%--<Nikhil M. Changed from 99.50 on 02-Sep-2016: FSD Default ></Nikhil>--%>
                                                </td>
                                                <td nowrap="nowrap">
                                                    <asp:CheckBox ID="chkKO" runat="server" CssClass="lbl" AutoPostBack="true" Enabled="false" />
                                                    <%--Rutuja Added enable=false on 18Aug2014,Jira FA-486--%>
                                                    <asp:Label ID="lblKOType" runat="server" Text="KO % of Initial" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtKOLevel" runat="server" CssClass="txt" Width="60px" Style="text-align: right;
                                                        padding-left: 3px;" AutoPostBack="true" MaxLength="6"></asp:TextBox><asp:Label ID="lblKOLevelSameAsCoupon"
                                                            runat="server" Visible="false" Text="" CssClass="lbl" Style="padding-left: 3px;"></asp:Label>
                                                    <telerik:RadDropDownList ID="ddlKOType" runat="server" Width="91px" AutoPostBack="true"
                                                        BackColor="#F2F2F3" Enabled="true">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="AMERICAN" Text="Daily Close" />
                                                            <telerik:DropDownListItem Value="EUROPEAN" Text="Period End" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <%--'<AvinashG. on 30-Aug-2016:Hiding lblKOLevelSameAsCoupon and showing dropdown 
                                                            as per SCB requirement, TBD with counterparties if they
                                                            are supporting it>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <asp:Label ID="lblCoupon" runat="server" CssClass="lbl " Text="Coupon (%)p.a." Width="85px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCoupon" runat="server" CssClass="txt" Style="text-align: right"
                                                        Width="115px" AutoPostBack="true">10.00</asp:TextBox>
                                                </td>
                                                <td style="width: 60px">
                                                    <asp:Label ID="lblAcc" runat="server" Text="Acc (%)" CssClass="lbl" Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAccSameasstrike" runat="server" Text="Same as strike (%)" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNote" runat="server" Text="&nbsp;&nbsp;Note Ccy" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDropDownList ID="ddlNoteCcy" runat="server" Width="67px" AutoPostBack="true">
                                                    </telerik:RadDropDownList>
                                                    <asp:Label ID="lblQuantoFlag" runat="server" CssClass="lbl" Text="Quanto Flag "></asp:Label>
                                                    <asp:Label ID="lblQuantoYNFlag" runat="server" CssClass="lbl" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space: nowrap;">
                                                    <asp:Label ID="lblKIFrequency" runat="server" Text="Coupon Frq." CssClass="lbl" Width="60px"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadDropDownList ID="ddlKIFrequency" runat="server" Width="122px" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="MONTHLY" Text="Monthly" Selected="True" />
                                                            <telerik:DropDownListItem Value="BIMONTHLY" Text="Two-Monthly" />
                                                            <telerik:DropDownListItem Value="QUARTERLY" Text="Quarterly" />
                                                            <telerik:DropDownListItem Value="SEMIANNUALLY" Text="Semiannually" />
                                                            <telerik:DropDownListItem Value="ANNUALLY" Text="Annually" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGUPeriod" runat="server" Text="Guarantee" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td nowrap="nowrap">
                                                    <asp:TextBox ID="txtGuPeriod" runat="server" CssClass="txt" Width="40px" Style="text-align: right"
                                                        AutoPostBack="true" MaxLength="3">1</asp:TextBox>
                                                    <asp:Label ID="lblGUType" runat="server" CssClass="lbl" Text="Coupon Periods"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblOrderDRAFCN" runat="server" CssClass="lbl" Text="&nbsp;&nbsp;Notional"></asp:Label>
                                                    <asp:Label ID="lblNoteCcy" runat="server" CssClass="lbl" Text=""></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtOrderDRAFCN" runat="server" CssClass="txt" Style="text-align: right"
                                                        Width="150px" AutoPostBack="true" MaxLength="14">300,000</asp:TextBox>
                                                    <%--<Nikhil M. Changed from "1,000,000" on 02-Sep-2016: FSD Default ></Nikhil>--%>
                                                    <%--'' EQBOSDEV-228  Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblUpfrontDRA" runat="server" CssClass="lbl" Text="Upfront (%)"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUpfrontDRA" runat="server" AutoPostBack="true" CssClass="txt"
                                                        Style="text-align: right" Width="115px" TabIndex="7" MaxLength="6"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="Filter" style="border: 1px solid #d5d5d5; border-left: none;" align="left"
                            valign="top" runat="server" id="tdgrphShareData">
                            <table id="grphShareData" cellpadding="1px" style="float: left; margin-top: -3px;">
                                <tr runat="server" id="rowGraphData" visible="false">
                                    <td style="text-align: center; font-weight: bold;">
                                        <asp:UpdatePanel ID="upnlChart" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Chart ID="Chart1" runat="server" Width="303px" Height="170px" BackGradientStyle="None"
                                                    ImageStorageMode="UseHttpHandler">
                                                    <Legends>
                                                        <asp:Legend Name="Default">
                                                        </asp:Legend>
                                                    </Legends>
                                                    <Titles>
                                                        <asp:Title Name="ChartTitle">
                                                        </asp:Title>
                                                    </Titles>
                                                    <Series>
                                                        <asp:Series ChartType="Doughnut" Name="Series1" ChartArea="Default" Legend="Default">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="Default">
                                                            <AxisY LineColor="64, 64, 64, 64" LabelAutoFitMinFontSize="6" LabelAutoFitMaxFontSize="6">
                                                                <MajorGrid Enabled="false"></MajorGrid>
                                                                <MajorTickMark Enabled="false" />
                                                            </AxisY>
                                                            <AxisX LineColor="64, 64, 64, 64" IsStartedFromZero="true" LabelAutoFitMinFontSize="6"
                                                                LabelAutoFitMaxFontSize="6">
                                                                <MajorGrid Enabled="false"></MajorGrid>
                                                                <MajorTickMark Enabled="false" />
                                                            </AxisX>
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        Best Quotes
                                    </td>
                                    <td style="text-align: center; font-weight: bold;">
                                        <asp:UpdatePanel ID="upnlColumnChart" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Chart ID="Chart2" runat="server" Width="303px" Height="170px" BackGradientStyle="None"
                                                    ImageStorageMode="UseHttpHandler" BorderSkin-BackHatchStyle="None" IsSoftShadows="True">
                                                    <Legends>
                                                        <asp:Legend Name="Default" Enabled="False">
                                                        </asp:Legend>
                                                    </Legends>
                                                    <Titles>
                                                        <asp:Title Name="ChartTitle">
                                                        </asp:Title>
                                                    </Titles>
                                                    <Series>
                                                        <asp:Series Name="Series1" ChartArea="Default" Legend="Default" ChartType="Column"
                                                            IsXValueIndexed="True" LabelForeColor="White" Font="9px">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="Default" AlignmentOrientation="Vertical" IsSameFontSizeForAllAxes="True">
                                                            <AxisY LineColor="64, 64, 64, 64" LabelAutoFitStyle="DecreaseFont" TextOrientation="Stacked"
                                                                TitleAlignment="Center">
                                                                <MajorGrid Enabled="false"></MajorGrid>
                                                                <MajorTickMark Enabled="false" />
                                                            </AxisY>
                                                            <AxisX LineColor="64, 64, 64, 64" IsStartedFromZero="true" LabelAutoFitStyle="DecreaseFont"
                                                                TextOrientation="Stacked">
                                                                <MajorGrid Enabled="false"></MajorGrid>
                                                                <MajorTickMark Enabled="false" />
                                                            </AxisX>
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        Order/RFQ (%)
                                    </td>
                                </tr>
                                <tr runat="server" id="rowShareData">
                                    <td colspan="2">
                                        <asp:UpdatePanel ID="upnlShareRpt" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <div id="divShareFieldInfo" style="max-width: 317px; min-height: 85px; position: absolute;
                                                    background-color: #DEE4EB; border: solid 1px 5D7B9D; visibility: hidden;">
                                                    <div id="titleShareFieldInfo" style="height: 22px; width: 99%; background-color: #5D7B9D;
                                                        padding-left: 3px; padding-top: 3px;">
                                                        <span style="background-color: #2288D3; font-size: 14px !important; color: #FFF;
                                                            font-weight: bold; margin-right: 6px; width: 0px; visibility: hidden;">!</span>
                                                        <span id="lblFieldCaption" style="width: 300px !important; color: #FFF;">Share Field
                                                            Info</span> <span id="closeFieldInfo" style="background-color: Red; border: solid 1px #000;
                                                                float: right; visibility: hidden; font-weight: bold; text-align: center; color: #FFF;
                                                                width: 12px; margin-right: 2px; overflow: hidden; cursor: default">x</span>
                                                    </div>
                                                    <div id='contentShareFieldInfo' style="padding: 3px 2px 3px 2px; white-space: normal;">
                                                    </div>
                                                </div>
                                                <ajax:TabContainer ID="tabCntrShareRpt" runat="server" ActiveTabIndex="0">
                                                    <ajax:TabPanel ID="tabShare1" runat="server" TabIndex="0" BackColor="AliceBlue">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblStock" runat="server" CssClass="lblBOLD" Style="background-color: Transparent"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <asp:Panel ID="pnlDSSData" runat="server">
                                                                <table cellspacing="0">
                                                                    <tr>
                                                                        <td colspan="7" style="border-bottom: solid 1px #d5d5d5; width: 100%; height: 16px;
                                                                            padding-top: 0px; padding-bottom: 0px">
                                                                            <asp:Label ID="lblStockDesc" runat="server" CssClass="lbl" Style="font-size: 10px !important;"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr title="Refreshed frequently as compared to rest of data.">
                                                                        <td style="width: 100px; background-color: #F5FAFD;">
                                                                            <span class='fieldInfo' style="background-color: #FFF" onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lblSpot,spotInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='spotInfo'><b>Value: </b>Instrument's last price for the trading day.<br />
                                                                                    <b>Date: </b>Date of the instrument's last updated close price.</span>
                                                                            <asp:Label ID="lblSpot" runat="server" CssClass="lbl" Style="float: left; color: #919191;"
                                                                                Text="Spot(d)"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 90px; background-color: #F5FAFD;">
                                                                            <asp:Label ID="lblSpotDate" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 80px; text-align: right; background-color: #F5FAFD;">
                                                                            <asp:Label ID="lblSpotValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td rowspan="7" style="background-color: #d5d5d5">
                                                                        </td>
                                                                        <td style="width: 110px; display: none; visibility: hidden;">
                                                                            <asp:Label ID="lblMarketCap" runat="server" CssClass="lbl" Text="Market Cap"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 30px; display: none; visibility: hidden;">
                                                                            <asp:Label ID="lblMarketCapCcy" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 95px; text-align: right; display: none; visibility: hidden;">
                                                                            <asp:Label ID="lblMarketCapValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 110px; display: none; visibility: hidden">
                                                                            <asp:Label ID="lblCashEquiv" runat="server" CssClass="lbl" Text="Cash & Equiv."></asp:Label>
                                                                        </td>
                                                                        <td style="width: 30px; display: none; visibility: hidden">
                                                                            <asp:Label ID="lblCashEquivCcy" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 95px; text-align: right; display: none; visibility: hidden">
                                                                            <asp:Label ID="lblCashEquivValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 140px;" colspan="2">
                                                                            <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lbl12TDivYield,f12TDivYieldInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='f12TDivYieldInfo'>Ratio of the annualized dividends to the price of a stock.
                                                                                    Dividends are adjusted to account for any stock splits during the 12-month period.
                                                                                    Gross dividends (dividends before taxes) are used to calculate dividend yield.</span>
                                                                            <asp:Label ID="lbl12TDivYield" Style="color: #919191;" runat="server" CssClass="lbl"
                                                                                Text="12T Div Yield Indicative"></asp:Label>
                                                                            <asp:Label ID="lbl12TDivValuefreq" runat="server" CssClass="lbl" Style="padding-left: 5px;"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 95px; text-align: right">
                                                                            <asp:Label ID="lbl12TDivYieldValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lbl52WkHigh,f52WkHighInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='f52WkHighInfo'><b>Value: </b>Highest of the daily high prices during the past
                                                                                    365 calendar days.<br />
                                                                                    <b>Date: </b>Date of Close Price - 52 Week High.</span><asp:Label ID="lbl52WkHigh"
                                                                                        runat="server" CssClass="lbl" Style="color: #919191;" Text="52 Wk High"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl52WkHighDate" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lbl52WkHighValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="display: none; visibility: hidden">
                                                                            <asp:Label ID="lblTotalDebt" runat="server" CssClass="lbl" Text="Total Debt"></asp:Label>
                                                                        </td>
                                                                        <td style="display: none; visibility: hidden">
                                                                            <asp:Label ID="lblTotalDebtCcy" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: right; display: none; visibility: hidden">
                                                                            <asp:Label ID="lblTotalDebtValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lblPrevEarnDate,lblPrevEarnDateInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='lblPrevEarnDateInfo'>The date on which EPS was last announced for the reporting
                                                                                    period.</span>
                                                                            <asp:Label ID="lblPrevEarnDate" Style="color: #919191;" runat="server" CssClass="lbl"
                                                                                Text="Prev. Earn Date"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPrevEarnFreq" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lblPrevEarnDateValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lbl52WkLow,f52WkLowInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='f52WkLowInfo'><b>Value: </b>Lowest of the daily low prices during the past 365
                                                                                    calendar days.<br />
                                                                                    <b>Date: </b>Date of Close Price - 52 Week Low.</span><asp:Label ID="lbl52WkLow"
                                                                                        runat="server" CssClass="lbl" Style="color: #919191;" Text="52 Wk Low"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl52WkLowDate" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lbl52WkLowValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lblPrevEPS,lblPrevEPSInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='lblPrevEPSInfo'>The last announced EPS for the reporting period.</span>
                                                                            <asp:Label ID="lblPrevEPS" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                Text="Prev. EPS"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lblPrevEPSValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lblYTDChng,YTDChangeInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='YTDChangeInfo'>Percent change between the closing price of the latest completed
                                                                                    tradable day and the closing price for the last tradable day of the prior year.</span>
                                                                            <asp:Label ID="lblYTDChng" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                Text="YTD Chng %"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lblYTDChngValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td rowspan="4" colspan="3">
                                                                            <table style="width: 100%;" cellpadding="0">
                                                                                <tr>
                                                                                    <td colspan="2" style="border-bottom: solid 1px #d5d5d5; text-align: center; height: 12px !important">
                                                                                        <asp:Label ID="lblCurr" runat="server" Style="white-space: normal; text-align: center;"
                                                                                            CssClass="lbl" Text="Current Vol. Annualized"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lbl20DHistVol,f20DHistVolInfo);'>
                                                                                            !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                                id='f20DHistVolInfo'>Volatility of returns for stock price movements for 20 days.
                                                                                                The volatility calculation uses a standard method for volatility, assuming 250 trading
                                                                                                days per year.</span>
                                                                                        <asp:Label ID="lbl20DHistVol" Style="color: #919191;" runat="server" CssClass="lbl"
                                                                                            Text="20D"></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: right">
                                                                                        <asp:Label ID="lbl20DHistVolCurr" runat="server" CssClass="lbl"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lbl60DHistVol,f60DHistVolInfo);'>
                                                                                            !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                                id='f60DHistVolInfo'>Volatility of returns for stock price movements for 60 days.
                                                                                                The volatility calculation uses a standard method for volatility, assuming 250 trading
                                                                                                days per year.</span>
                                                                                        <asp:Label ID="lbl60DHistVol" Style="color: #919191;" runat="server" CssClass="lbl"
                                                                                            Text="60D"></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: right">
                                                                                        <asp:Label ID="lbl60DHistVolCurr" runat="server" CssClass="lbl"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lbl250DHistVol,f250DHistVolInfo);'>
                                                                                            !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                                id='f250DHistVolInfo'>Volatility of returns for stock price movements for 250 days.
                                                                                                The volatility calculation uses a standard method for volatility, assuming 250 trading
                                                                                                days per year.</span>
                                                                                        <asp:Label ID="lbl250DHistVol" Style="color: #919191;" runat="server" CssClass="lbl"
                                                                                            Text="250D"></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: right">
                                                                                        <asp:Label ID="lbl250DHistVolCurr" runat="server" CssClass="lbl"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lblNextDivDate" runat="server" CssClass="lbl" Text="Next Div Date"
                                                                                Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblNextDiv" runat="server" CssClass="lbl" Text="A/F" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblNextDivDateValue" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lblMTDChng,MTDChangeInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='MTDChangeInfo'>Percent change between the closing price of the latest completed
                                                                                    tradable day and the closing price for the last tradable day of the prior month.</span>
                                                                            <asp:Label ID="lblMTDChng" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                Text="MTD Chng %"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lblMTDChngValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lbl1YearChng,f1YrChngInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='f1YrChngInfo'>Percentage difference between current price and price 1 year ago.</span>
                                                                            <asp:Label ID="lbl1YearChng" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                Text="1 Year Chng %"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lbl1YearChngValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lblTrailing12MPE,Trailing12MPEInfo);'>
                                                                                !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                    id='Trailing12MPEInfo'>Ratio of stock price to earnings per share.</span>
                                                                            <asp:Label ID="lblTrailing12MPE" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                Text="Trailing 12M P/E"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lblTrailing12MPEValue" runat="server" CssClass="lbl"></asp:Label>
                                                                            <asp:Label ID="lblNextEarnDate" runat="server" CssClass="lbl" Text="Next Earn Date"
                                                                                Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblNextEarnFreq" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblNextEarnDateValue" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none; visibility: hidden">
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblTrailing12MPB" runat="server" CssClass="lbl" Text="Trailing 12M P/B"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lblTrailing12MPBValue" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="8" style="border-top: solid 1px #d5d5d5; width: 100%; padding-left: 13px;">
                                                                            <asp:Label ID="lblAsOfCaption" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                color: #00ADEF; float: left">*As of&nbsp;</asp:Label>
                                                                            <asp:Label ID="lblAsOfValue" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                color: #00ADEF; float: left"></asp:Label>
                                                                            <asp:Label ID="lblTRDSSDisclaimer" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                color: #00ADEF; float: right; text-align: right;">&nbsp;*sources and definitions as per Thomson Reuters DataScope Select</asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </ajax:TabPanel>
                                                    <ajax:TabPanel ID="tabShare2" runat="server" TabIndex="1" Visible="false">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblStock2" runat="server" CssClass="lblBOLD" Style="background-color: Transparent"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="upnlCntrShareRpt2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="pnlDSSData2" runat="server">
                                                                        <table style="" cellspacing="0">
                                                                            <tr>
                                                                                <td colspan="7" style="border-bottom: solid 1px #919191; width: 100%; height: 16px;
                                                                                    padding-top: 0px; padding-bottom: 0px">
                                                                                    <asp:Label ID="lblStockDesc2" runat="server" CssClass="lbl" Style="font-size: 10px !important;"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr title="Refreshed frequently as compared to rest of data.">
                                                                                <td style="width: 100px; background-color: #F5FAFD;">
                                                                                    <span class='fieldInfo' style="background-color: #FFF" onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lblSpot2,spotInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblSpot2" runat="server" CssClass="lbl" Text="Spot(d)" Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 90px; background-color: #F5FAFD;">
                                                                                    <asp:Label ID="lblSpotDate2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 80px; text-align: right; background-color: #F5FAFD;">
                                                                                    <asp:Label ID="lblSpotValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td rowspan="7" style="background-color: #d5d5d5">
                                                                                </td>
                                                                                <td style="width: 110px; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblMarketCap2" runat="server" CssClass="lbl" Text="Market Cap"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 30px; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblMarketCapCcy2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 95px; text-align: right; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblMarketCapValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 110px; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblCashEquiv2" runat="server" CssClass="lbl" Text="Cash & Equiv."></asp:Label>
                                                                                </td>
                                                                                <td style="width: 30px; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblCashEquivCcy2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 95px; text-align: right; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblCashEquivValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 140px;" colspan="2">
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lbl12TDivYield2,f12TDivYieldInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lbl12TDivYield2" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                        Text="12T Div Yield Indicative"></asp:Label>
                                                                                    <asp:Label ID="lbl12TDivValuefreq2" runat="server" CssClass="lbl" Style="padding-left: 5px;"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 95px; text-align: right">
                                                                                    <asp:Label ID="lbl12TDivYieldValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lbl52WkHigh2,f52WkHighInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lbl52WkHigh2" runat="server" CssClass="lbl" Text="52 Wk High" Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl52WkHighDate2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lbl52WkHighValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="display: none; visibility: hidden">
                                                                                    <asp:Label ID="lblTotalDebt2" runat="server" CssClass="lbl" Text="Total Debt"></asp:Label>
                                                                                </td>
                                                                                <td style="display: none; visibility: hidden">
                                                                                    <asp:Label ID="lblTotalDebtCcy2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right; display: none; visibility: hidden">
                                                                                    <asp:Label ID="lblTotalDebtValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lblPrevEarnDate2,lblPrevEarnDateInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblPrevEarnDate2" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                        Text="Prev. Earn Date"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblPrevEarnFreq2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblPrevEarnDateValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lbl52WkLow2,f52WkLowInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lbl52WkLow2" runat="server" CssClass="lbl" Text="52 Wk Low" Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl52WkLowDate2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lbl52WkLowValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lblPrevEPS2,lblPrevEPSInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblPrevEPS2" runat="server" CssClass="lbl" Text="Prev. EPS" Style="color: #919191"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblPrevEPSValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lblYTDChng2,YTDChangeInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblYTDChng2" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                        Text="YTD Chng %"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblYTDChngValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td rowspan="4" colspan="3">
                                                                                    <table style="width: 100%;" cellpadding="0">
                                                                                        <tr>
                                                                                            <td colspan="2" style="border-bottom: solid 1px #d5d5d5; text-align: center;">
                                                                                                <asp:Label ID="lblCurr2" runat="server" Style="white-space: normal; text-align: center;"
                                                                                                    CssClass="lbl" Text="Current Vol. Annualized"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lbl20DHistVol2,f20DHistVolInfo);'>
                                                                                                    !</span>
                                                                                                <asp:Label ID="lbl20DHistVol2" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                                    Text="20D"></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: right">
                                                                                                <asp:Label ID="lbl20DHistVolCurr2" runat="server" CssClass="lbl"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lbl60DHistVol2,f60DHistVolInfo);'>
                                                                                                    !</span>
                                                                                                <asp:Label ID="lbl60DHistVol2" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                                    Text="60D"></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: right">
                                                                                                <asp:Label ID="lbl60DHistVolCurr2" runat="server" CssClass="lbl"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lbl250DHistVol2,f250DHistVolInfo);'>
                                                                                                    !</span>
                                                                                                <asp:Label ID="lbl250DHistVol2" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                                    Text="250D"></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: right">
                                                                                                <asp:Label ID="lbl250DHistVolCurr2" runat="server" CssClass="lbl"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblNextDivDate2" runat="server" CssClass="lbl" Text="Next Div Date"
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblNextDiv2" runat="server" CssClass="lbl" Text="A/F" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblNextDivDateValue2" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lblMTDChng2,MTDChangeInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblMTDChng2" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                        Text="MTD Chng %"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblMTDChngValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lbl1YearChng2,f1YrChngInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lbl1YearChng2" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                        Text="1 Year Chng %"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lbl1YearChngValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare2_lblTrailing12MPE2,Trailing12MPEInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblTrailing12MPE2" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                        Text="Trailing 12M P/E"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblTrailing12MPEValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                    <asp:Label ID="lblNextEarnDate2" runat="server" CssClass="lbl" Text="Next Earn Date"
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblNextEarnFreq2" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblNextEarnDateValue2" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="display: none; visibility: hidden">
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="lblTrailing12MPB2" runat="server" CssClass="lbl" Text="Trailing 12M P/B"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblTrailing12MPBValue2" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="7" style="border-top: solid 1px #919191; width: 100%;">
                                                                                    <asp:Label ID="lblAsOfCaption2" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                        color: #00ADEF; float: left; padding-left: 13px;">* As of&nbsp;</asp:Label>
                                                                                    <asp:Label ID="lblAsOfValue2" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                        color: #00ADEF; float: left"></asp:Label>
                                                                                    <asp:Label ID="lblTRDSSDisclaimer2" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                        color: #00ADEF; float: right; text-align: right;">* sources and definitions as per Thomson Reuters DataScope Select</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                    </ajax:TabPanel>
                                                    <ajax:TabPanel ID="tabShare3" runat="server" TabIndex="1" Visible="false">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblStock3" runat="server" CssClass="lblBOLD" Style="background-color: Transparent"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="upnlCntrShareRpt3" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="pnlDSSData3" runat="server">
                                                                        <table style="" cellspacing="0">
                                                                            <tr>
                                                                                <td colspan="7" style="border-bottom: solid 1px #919191; width: 100%; height: 16px;
                                                                                    padding-top: 0px; padding-bottom: 0px">
                                                                                    <asp:Label ID="lblStockDesc3" runat="server" CssClass="lbl" Style="font-size: 10px !important;"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr title="Refreshed frequently as compared to rest of data.">
                                                                                <td style="width: 100px; background-color: #F5FAFD;">
                                                                                    <span class='fieldInfo' style="background-color: #FFF" onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lblSpot3,spotInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblSpot3" runat="server" CssClass="lbl" Text="Spot(d)" Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 90px; background-color: #F5FAFD;">
                                                                                    <asp:Label ID="lblSpotDate3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 80px; text-align: right; background-color: #F5FAFD;">
                                                                                    <asp:Label ID="lblSpotValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td rowspan="7" style="background-color: #d5d5d5">
                                                                                </td>
                                                                                <td style="width: 110px; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblMarketCap3" runat="server" CssClass="lbl" Text="Market Cap"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 30px; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblMarketCapCcy3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 95px; text-align: right; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblMarketCapValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 110px; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblCashEquiv3" runat="server" CssClass="lbl" Text="Cash & Equiv."></asp:Label>
                                                                                </td>
                                                                                <td style="width: 30px; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblCashEquivCcy3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 95px; text-align: right; display: none; visibility: hidden;">
                                                                                    <asp:Label ID="lblCashEquivValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 140px;" colspan="2">
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lbl12TDivYield3,f12TDivYieldInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lbl12TDivYield3" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                        Text="12T Div Yield Indicative"></asp:Label>
                                                                                    <asp:Label ID="lbl12TDivValuefreq3" runat="server" CssClass="lbl" Style="padding-left: 5px;"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 95px; text-align: right">
                                                                                    <asp:Label ID="lbl12TDivYieldValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lbl52WkHigh3,f52WkHighInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lbl52WkHigh3" runat="server" CssClass="lbl" Text="52 Wk High" Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl52WkHighDate3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lbl52WkHighValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="display: none; visibility: hidden">
                                                                                    <asp:Label ID="lblTotalDebt3" runat="server" CssClass="lbl" Text="Total Debt"></asp:Label>
                                                                                </td>
                                                                                <td style="display: none; visibility: hidden">
                                                                                    <asp:Label ID="lblTotalDebtCcy3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right; display: none; visibility: hidden">
                                                                                    <asp:Label ID="lblTotalDebtValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lblPrevEarnDate3,lblPrevEarnDateInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblPrevEarnDate3" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                        Text="Prev. Earn Date"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblPrevEarnFreq3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblPrevEarnDateValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lbl52WkLow3,f52WkLowInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lbl52WkLow3" runat="server" CssClass="lbl" Text="52 Wk Low" Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl52WkLowDate3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lbl52WkLowValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lblPrevEPS3,lblPrevEPSInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblPrevEPS3" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                        Text="Prev. EPS"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblPrevEPSValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lblYTDChng3,YTDChangeInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblYTDChng3" runat="server" CssClass="lbl" Text="YTD Chng %" Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblYTDChngValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                                <td rowspan="4" colspan="3">
                                                                                    <table style="width: 100%;" cellpadding="0">
                                                                                        <tr>
                                                                                            <td colspan="3" style="border-bottom: solid 1px #d5d5d5; text-align: center;">
                                                                                                <asp:Label ID="lblCurr3" runat="server" Style="white-space: normal; text-align: center;"
                                                                                                    CssClass="lbl" Text="Current Vol. Annualized"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lbl20DHistVol3,f20DHistVolInfo);'>
                                                                                                    !</span>
                                                                                                <asp:Label ID="lbl20DHistVol3" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                                    Text="20D"></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: right">
                                                                                                <asp:Label ID="lbl20DHistVolCurr3" runat="server" CssClass="lbl"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lbl60DHistVol3,f60DHistVolInfo);'>
                                                                                                    !</span>
                                                                                                <asp:Label ID="lbl60DHistVol3" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                                    Text="60D"></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: right">
                                                                                                <asp:Label ID="lbl60DHistVolCurr3" runat="server" CssClass="lbl"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lbl250DHistVol3,f250DHistVolInfo);'>
                                                                                                    !</span>
                                                                                                <asp:Label ID="lbl250DHistVol3" runat="server" CssClass="lbl" Style="color: #919191;"
                                                                                                    Text="250D"></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: right">
                                                                                                <asp:Label ID="lbl250DHistVolCurr3" runat="server" CssClass="lbl"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblNextDivDate3" runat="server" CssClass="lbl" Text="Next Div Date"
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblNextDiv3" runat="server" CssClass="lbl" Text="A/F" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblNextDivDateValue3" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lblMTDChng3,MTDChangeInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblMTDChng3" runat="server" CssClass="lbl" Text="MTD Chng %" Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblMTDChngValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lbl1YearChng3,f1YrChngInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lbl1YearChng3" runat="server" CssClass="lbl" Text="1 Year Chng %"
                                                                                        Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lbl1YearChngValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <span class='fieldInfo' onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare3_lblTrailing12MPE3,Trailing12MPEInfo);'>
                                                                                        !</span>
                                                                                    <asp:Label ID="lblTrailing12MPE3" runat="server" CssClass="lbl" Text="Trailing 12M P/E"
                                                                                        Style="color: #919191;"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblTrailing12MPEValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                    <asp:Label ID="lblNextEarnDate3" runat="server" CssClass="lbl" Text="Next Earn Date"
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblNextEarnFreq3" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblNextEarnDateValue3" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="display: none; visibility: hidden">
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="lblTrailing12MPB3" runat="server" CssClass="lbl" Text="Trailing 12M P/B"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: right">
                                                                                    <asp:Label ID="lblTrailing12MPBValue3" runat="server" CssClass="lbl"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="7" style="border-top: solid 1px #919191; width: 100%;">
                                                                                    <asp:Label ID="lblAsOfCaption3" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                        color: #00ADEF; float: left; padding-left: 13px;">* As of&nbsp;</asp:Label>
                                                                                    <asp:Label ID="lblAsOfValue3" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                        color: #00ADEF; float: left"></asp:Label>
                                                                                    <asp:Label ID="lblTRDSSDisclaimer3" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                        color: #00ADEF; float: right; text-align: right;">* sources and definitions as per Thomson Reuters DataScope Select</asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                    </ajax:TabPanel>
                                                </ajax:TabContainer>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="trSaveSetting">
                        <td>
                            <asp:Button ID="btnSaveSettings" runat="server" Text="Save Settings" CssClass="btn"
                                onmouseover="JavaScript:alert:this.focus();" /><asp:Label ID="lblError_DefaultSettings"
                                    runat="server" CssClass="lbl" ForeColor="red" Style="width: auto;"></asp:Label>
                        </td>
                        <td colspan="3">
                        </td>
                    </tr>
                    <!--Separate Deal Controls-->
                    <tr>
                        <td class="Filter" align="left" valign="top" style="width: 720px; border-top-width: 0px;"  runat="server" id="tdpnlReprice">
                            <asp:UpdatePanel runat="server" ID="pnlReprice" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="PanelReprice" runat="server">
                                        <div style="width: 100%">
                                        </div>
                                        <table class="ELN3" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td valign="top">
                                                    <div>
                                                        <asp:Label ID="lblPricerate" runat="server" CssClass="lbl" Text=""></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr style="vertical-align: top">
                                                <td valign="top" align="left">
                                                    <asp:UpdatePanel ID="upnlSolveAll" runat="server">
                                                        <ContentTemplate>
                                                            <table style="margin-left: 12px;" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td rowspan="2" style="height: 48px; line-height: 48px;">
                                                                        <asp:Button ID="btnCancelReq" runat="server" Width="100%" Text="Reset" CssClass="btn"
                                                                            onmouseover="JavaScript:alert:this.focus();" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="text-align: left; height: 25px;">
                                                                        <asp:Label ID="lblSolveForType" runat="server" Text="IB Price %" CssClass="lbl" Style="white-space: nowrap;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="text-align: left; height: 25px !important;">
                                                                        <asp:Label ID="lblClientPriceCaption" runat="server" CssClass="lbl" Text="Client Price (%)"
                                                                            Style="text-align: left !important; white-space: nowrap;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="text-align: left; height: 25px !important;">
                                                                        <asp:Label ID="lblClientYieldCaption" runat="server" CssClass="lbl" Text="Client Yield (%)p.a."
                                                                            Style="white-space: nowrap;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" style="height: 25px !important;">
                                                                        <%--<asp:Label ID="lblTimerAll" runat="server" Text="" CssClass="lblBOLD" Style="vertical-align: middle;
                                                                            text-align: center;"> </asp:Label>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center" style="height: 20px; width: 105px !important;">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="PriceAllWait" width="20px" height="20px"
                                                                                style="visibility: hidden;" alt="X" />
                                                                        </div>
                                                                        <asp:Button ID="btnSolveAll" name="btnSolveAll" onmouseover="JavaScript:alert:this.focus();"
                                                                            runat="server" CssClass="btn" Text="Price All" OnClick="btnSolveAll_Click" Style="width: 100%;" />
                                                                        <asp:HiddenField ID="AllHiddenPrice" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblRangeCcy" CssClass="lbl" runat="server"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdHSBC1" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlHSBC" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRHSBC1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <%--''<Nikhil M. on 17-Sep-2016: Remove "  onmouseover="showLPBoxes()"  onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; line-height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkHSBC" CssClass="chkBoxCheck" runat="server" AutoPostBack="true" /><%--''<Nikhil M. on 17-Sep-2016: Remove "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblHSBC" runat="server" CssClass="lbl BestLP" Text="HSBC" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px" valign="middle">
                                                                        <asp:Label ID="lblHSBCPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblHSBCClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblHSBCClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblTimerHSBC" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnHSBCDoc" Text="Document" runat="server" CssClass="cssDocLink"
                                                                            Visible="false" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="HSBCwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" />
                                                                        </div>
                                                                        <asp:Button ID="btnHSBCPrice" name="btnHSBCPrice" onmouseover="JavaScript:alert:this.focus();"
                                                                            runat="server" CssClass="btn" Text="Price" OnClick="btnHSBCPrice_Click" Width="45px" />
                                                                        <asp:Button ID="btnHSBCDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" OnClick="btnHSBCDeal_Click" CssClass="btn" Enabled="false" ToolTip="Deal"
                                                                            Width="45px" />
                                                                        <asp:HiddenField ID="HsbcHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="HsbcHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000; margin-top: 5px">
                                                                            <asp:Label ID="lblHSBClimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdUBS1" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlUBS" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRUBS1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <%--''<Nikhil M. on 17-Sep-2016: Remove " onmouseover="showLPBoxes()"  onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkUBS" CssClass="chkBoxCheck" runat="server" AutoPostBack="true" /><%--''<Nikhil M. on 17-Sep-2016: Remove "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblUBS" runat="server" CssClass="lbl BestLP" Text="UBS" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px" valign="middle">
                                                                        <asp:Label ID="lblUBSPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblUBSClientPrice" runat="server" Text="0.0" Style="height: 25px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblUBSClientYield" runat="server" Text="0.0" Style="height: 25px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblUBSTimer" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnUBSDoc" Text="Document" runat="server" CssClass="cssDocLink" Visible="false" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="UBSwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" alt="X" />
                                                                        </div>
                                                                        <asp:Button ID="btnUBSPrice" name="btnUBSPrice" onmouseover="JavaScript:alert:this.focus();"
                                                                            runat="server" Text="Price" ToolTip=" Price" OnClick="btnUBSPrice_Click" CssClass="btn"
                                                                            Width="45px" />
                                                                        <asp:Button ID="btnUBSDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" Enabled="false" ToolTip="Deal" CssClass="btn" OnClick="btnUBSDeal_Click"
                                                                            Width="45px" Visible="false" />
                                                                        <asp:HiddenField ID="UbsHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="UbsHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblUBSlimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdJPM1" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlJPM" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRJPM1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <%--''<Nikhil M. on 17-Sep-2016: Remove " onmouseover="showLPBoxes()" onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkJPM" CssClass="chkBoxCheck" runat="server" AutoPostBack="true" /><%--''<Nikhil M. on 17-Sep-2016: Remove "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblJPM" runat="server" CssClass="lbl BestLP" Text="JPM" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="height: 25px;" valign="middle">
                                                                        <asp:Label ID="lblJPMPrice" runat="server" CssClass="lbl LPPrice" Text="" Font-Bold="true"
                                                                            ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblJPMClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblJPMClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblTimer" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnJPMDoc" Text="Document" runat="server" CssClass="cssDocLink" Visible="false" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="JPMwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" />
                                                                        </div>
                                                                        <asp:Button ID="btnJPMprice" name="btnJPMprice" onmouseover="JavaScript:alert:this.focus();"
                                                                            runat="server" CssClass="btn" Text="Price" Width="45px" OnClick="btnJPMprice_Click" />
                                                                        <asp:Button ID="btnJPMDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            CssClass="btn" Text="Order" OnClick="btnJPMDeal_Click" Enabled="false" ToolTip="Deal"
                                                                            Width="45px" Visible="false" />
                                                                        <asp:HiddenField ID="JpmHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="JpmHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblJPMlimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdBNPP1" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlBNPP" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRBNPP1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <%--''<Nikhil M. on 17-Sep-2016: Remove " onmouseover="showLPBoxes()"  onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkBNPP" CssClass="chkBoxCheck" runat="server" AutoPostBack="true" /><%--''<Nikhil M. on 17-Sep-2016: Remove "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblBNPP" runat="server" CssClass="lbl BestLP" Text="BNPP" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" align="center" valign="middle">
                                                                        <asp:Label ID="lblBNPPPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblBNPPClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblBNPPClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblTimerBNPP" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnBNPPDoc" Text="Document" runat="server" CssClass="cssDocLink"
                                                                            Visible="false" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="BNPPwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" />
                                                                        </div>
                                                                        <asp:Button ID="btnBNPPPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnBNPPPrice_Click" Width="45px" CssClass="btn" />
                                                                        <asp:Button ID="btnBNPPDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" OnClick="btnBNPPDeal_Click" Enabled="false" ToolTip="Deal" Width="45px"
                                                                            CssClass="btn" Visible="false" />
                                                                        <asp:HiddenField ID="BNPPHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="BNPPHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblBNPPlimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdCS1" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlCS" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRCS1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <%--''<Nikhil M. on 17-Sep-2016: Remove " onmouseover="showLPBoxes()" onmouseover="showLPBoxes()"  onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle !important;">
                                                                        <asp:CheckBox ID="chkCS" CssClass="chkBoxCheck" runat="server" AutoPostBack="true"
                                                                            Style="" /><%--''<Nikhil M. on 17-Sep-2016: Remove "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblCS" runat="server" CssClass="lbl BestLP" Text="CS" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" valign="middle">
                                                                        <asp:Label ID="lblCSPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;">
                                                                        <asp:Label ID="lblCSClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;">
                                                                        <asp:Label ID="lblCSClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;">
                                                                        <asp:Label ID="lblTimerCS" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnCSDoc" Text="Document" runat="server" CssClass="cssDocLink" Visible="false" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="CSwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" alt="x" />
                                                                        </div>
                                                                        <asp:Button ID="btnCSPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnCSPrice_Click" CssClass="btn" Width="45px" />
                                                                        <asp:Button ID="btnCSDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" Enabled="false" OnClick="btnCSDeal_Click" CssClass="btn" ToolTip="Deal"
                                                                            Width="45px" Visible="false" />
                                                                        <asp:HiddenField ID="CsHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="CsHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblCSLimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdBAML1" align="left" valign="top">
                                                    <!--SIXTH LP-->
                                                    <asp:UpdatePanel ID="upnlBAML" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRBAML1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <%--''<Nikhil M. on 17-Sep-2016: Remove " onmouseover="showLPBoxes()" onmouseover="showLPBoxes()"  onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkBAML" CssClass="chkBoxCheck" runat="server" AutoPostBack="true" />
                                                                        <%--''<Nikhil M. on 17-Sep-2016: Remove "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblBAML" runat="server" CssClass="lbl BestLP" Text="BAML" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" valign="middle">
                                                                        <asp:Label ID="lblBAMLPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblBAMLClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblBAMLClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblTimerBAML" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnBAMLDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="BAMLwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" alt="x" />
                                                                        </div>
                                                                        <asp:Button ID="btnBAMLPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnBAMLPrice_Click" Width="45px" CssClass="btn" />
                                                                        <asp:Button ID="btnBAMLDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" OnClick="btnBAMLDeal_Click" Enabled="false" ToolTip="Deal" Width="45px"
                                                                            CssClass="btn" Visible="false" />
                                                                        <asp:HiddenField ID="BAMLHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="BAMLHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblBAMLlimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdDBIB" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlDBIB" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRDBIB1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <%--''<Nikhil M. on 17-Sep-2016: Remove " onmouseover="showLPBoxes()"  onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle !important;">
                                                                        <asp:CheckBox ID="chkDBIB" CssClass="chkBoxCheck" runat="server" AutoPostBack="true" />
                                                                        <%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblDBIB" runat="server" CssClass="lbl BestLP" Text="DBIB" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" align="center" valign="middle">
                                                                        <asp:Label ID="lblDBIBPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;">
                                                                        <asp:Label ID="lblDBIBClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;">
                                                                        <asp:Label ID="lblDBIBClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;">
                                                                        <asp:Label ID="lblTimerDBIB" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnDBIBDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="DBIBwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" />
                                                                        </div>
                                                                        <asp:Button ID="btnDBIBPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnDBIBPrice_Click" CssClass="btn" Width="45px" />
                                                                        <asp:Button ID="btnDBIBDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" OnClick="btnDBIBDeal_Click" Enabled="false" ToolTip="Deal" Width="45px"
                                                                            CssClass="btn" Visible="false" />
                                                                        <asp:HiddenField ID="DBIBHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="DBIBHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblDBIBlimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdOCBC1" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlOCBC" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TROCBC1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <%--''<Nikhil M. on 17-Sep-2016: Remove " onmouseover="showLPBoxes()" onmouseover="showLPBoxes()"  onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkOCBC" CssClass="chkBoxCheck" runat="server" AutoPostBack="true" />
                                                                        <%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblOCBC" runat="server" CssClass="lbl BestLP" Text="OCBC" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" align="center" valign="middle">
                                                                        <asp:Label ID="lblOCBCPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblOCBCClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblOCBCClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblTimerOCBC" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnOCBCDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="OCBCwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" />
                                                                        </div>
                                                                        <asp:Button ID="btnOCBCPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnOCBCPrice_Click" Width="45px" CssClass="btn" />
                                                                        <asp:Button ID="btnOCBCDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" OnClick="btnOCBCDeal_Click" Enabled="false" ToolTip="Deal" Width="45px"
                                                                            CssClass="btn" Visible="false" />
                                                                        <asp:HiddenField ID="OCBCHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="OCBCHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblOCBClimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdCITI1" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlCITI" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRCITI1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <%--''<Nikhil M. on 17-Sep-2016: Remove " onmouseover="showLPBoxes()" onmouseover="showLPBoxes()"  onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkCITI" CssClass="chkBoxCheck" runat="server" AutoPostBack="true" />
                                                                        <%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblCITI" runat="server" CssClass="lbl BestLP" Text="CITI" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" align="center" valign="middle">
                                                                        <asp:Label ID="lblCITIPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblCITIClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblCITIClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblTimerCITI" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnCITIDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="CITIwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" />
                                                                        </div>
                                                                        <asp:Button ID="btnCITIPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnCITIPrice_Click" Width="45px" CssClass="btn" />
                                                                        <asp:Button ID="btnCITIDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" OnClick="btnCITIDeal_Click" Enabled="false" ToolTip="Deal" Width="45px"
                                                                            CssClass="btn" Visible="false" />
                                                                        <asp:HiddenField ID="CITIHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="CITIHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblCITIlimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdLEONTEQ1" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlLEONTEQ" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRLEONTEQ1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkLEONTEQ" AutoPostBack="true" runat="server" />
                                                                        <asp:Label ID="lblLEONTEQ" runat="server" CssClass="lbl BestLP" Text="Leonteq" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" align="center" valign="middle">
                                                                        <asp:Label ID="lblLEONTEQPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblLEONTEQClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblLEONTEQClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblTimerLEONTEQ" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnLEONTEQDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="LEONTEQwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" />
                                                                        </div>
                                                                        <asp:Button ID="btnLEONTEQPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnLEONTEQPrice_Click" Width="45px" CssClass="btn" />
                                                                        <asp:Button ID="btnLEONTEQDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" OnClick="btnLEONTEQDeal_Click" Enabled="false" ToolTip="Deal" Width="45px"
                                                                            CssClass="btn" Visible="false" />
                                                                        <asp:HiddenField ID="LEONTEQHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="LEONTEQHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblLEONTEQlimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td runat="server" id="tdCOMMERZ1" align="left" valign="top">
                                                    <asp:UpdatePanel ID="upnlCOMMERZ" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRCOMMERZ1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkCOMMERZ" AutoPostBack="true" runat="server" />
                                                                        <asp:Label ID="lblCOMMERZ" runat="server" CssClass="lbl BestLP" Text="Commerz" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" align="center" valign="middle">
                                                                        <asp:Label ID="lblCOMMERZPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblCOMMERZClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblCOMMERZClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px">
                                                                        <asp:Label ID="lblTimerCOMMERZ" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnCOMMERZDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="COMMERZwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" />
                                                                        </div>
                                                                        <asp:Button ID="btnCOMMERZPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnCOMMERZPrice_Click" Width="45px" CssClass="btn" />
                                                                        <asp:Button ID="btnCOMMERZDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" OnClick="btnCOMMERZDeal_Click" Enabled="false" ToolTip="Deal" Width="45px"
                                                                            CssClass="btn" Visible="false" />
                                                                        <asp:HiddenField ID="COMMERZHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="COMMERZHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center">
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblCOMMERZlimit" Text="" runat="server">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:HiddenField ID="hdnBestStrike" Value="" runat="server" />
                                    <asp:HiddenField ID="hdnBestProvider" Value="" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" valign="top" class="Filter" style="border-top-width: 0px; border-left-width: 0px;"
                            runat="server" id="tdCommentry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="0" align="left" style="text-align: left; margin-top: -2px;">
                                        <asp:UpdatePanel runat="server" ID="upnlCommentry" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="left" style="text-align: left;">
                                                            <asp:Label ID="lblComentry1" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                        </td>
                                                        <td style="width: 20px !important">
                                                            <asp:Label ID="lblMailComentry" runat="server" Text="" CssClass="lbl" Style="visibility: hidden;
                                                                user-select: none; font-size: 1px !important; height: 1px !important; float: left"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="addPPimg" ImageUrl="../App_Themes/images/add.png" runat="server"
                                            Height="17px" Style="float: left; margin-left: 5px;" Visible="false" />
                                        <div id="maillistDiv">
                                            <asp:CheckBoxList ID="chkPPmaillist" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="HSBC" Value="HSBC" />
                                                <asp:ListItem Text="UBS" Value="UBS" />
                                                <asp:ListItem Text="JPM" Value="JPM" />
                                                <asp:ListItem Text="BNPP" Value="BNPP" />
                                                <asp:ListItem Text="CS" Value="CS" />
                                                <%-- Changed by AshwiniP on 05-Oct-2016 (Credit Suisse to CS)--%>
                                                <asp:ListItem Text="CITI" Value="CITI" />
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upnQTEmail" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upnQTEmail"
                                                                DisplayAfter="0">
                                                                <ProgressTemplate>
                                                                    <div style="background-color: #FFFFFF;">
                                                                        <img alt="Loading" src="../App_Resources/loading.gif" />
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnEMLMailTrial" Text="Click to mail this Quote" runat="server" title="Mail this Quote"
                                                                CssClass="cssbtnEMLMail" Style="" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <script type="text/javascript">
                                            function showEmail() {
                                                $("#ctl00_MainContent_btnEMLMailTrial").css("display", "block");
                                            }
                                            function hideEmail() {
                                                $("#ctl00_MainContent_btnEMLMailTrial").css("display", "none");
                                            }
                                        </script>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="lbl" align="left" style="text-align: left; padding: 2px 0px 0px 2px !important">
                                        <div style="vertical-align: top; width: auto; float: left; text-align: left; vertical-align: bottom;">
                                            <asp:UpdatePanel ID="upnlMsg" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblerror" runat="server" CssClass="lbl" ForeColor="red" Style="width: auto;
                                                        padding-left: 25px;"></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="lblMsgPriceProvider" runat="server" CssClass="lbl" ForeColor="Red"
                                                        Style="width: auto; padding-left: 3px;"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 13px;">
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upnlGrid" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%" class="Filter" cellpadding="0" cellspacing="0" runat="server"
                            id="tblRFQGridHolder">
                            <tr style="width: 100%">
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" CssClass="lbl">Owner</asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDropDownList ID="ddlSelfGrp" runat="server" Width="100px">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Self" Value="Self" Selected="True" />
                                                        <telerik:DropDownListItem Text="Group" Value="Group" />
                                                        <telerik:DropDownListItem Text="All" Value="All" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 25px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTrade" runat="server" CssClass="lbl" Text="Trade Date"></asp:Label>
                                            </td>
                                            <td style="white-space: nowrap;">
                                                <uc1:DateControl ID="txttrade" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                    DoPostBack="true" DataFormatString="{0:dd-MMM-yyyy}" />
                                            </td>
                                            <td style="width: 25px">
                                            </td>
                                            <td>
                                                <asp:Button ID="btnLoad" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                    CssClass="btnRefresh" Text="" ToolTip="Refresh" />
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td valign="top">
                                                <asp:RadioButtonList ID="rbHistory" runat="server" RepeatDirection="horizontal" CssClass="RadioBtn"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="Quote History" Selected="True">Quote History</asp:ListItem>
                                                    <asp:ListItem Value="Order History">Order History</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkExpandAllRFQ" CssClass="mobilesubtitle" Text="Expand All Quote RFQ" runat="server" AutoPostBack="true"
                                                    Style="vertical-align: 1px" />
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalRows" runat="server" CssClass="lbl" Text="Max Records To Fetch: "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTotalRows" runat="server" CssClass="txt" Width="30px" Text="10"
                                                    AutoPostBack="true" MaxLength="4"></asp:TextBox>
                                            </td>
                                            <%-- Start | Chitralekha M on 26-Sep-16>--%>
                                            <td>
                                                <asp:UpdatePanel ID="upnlPrdKYIR" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <asp:Button ID="KYIR" Text="Product KYIR" runat="server" title="KYIR" Width="120"
                                                            CssClass="cssbtnEMLMail" Style="visibility: hidden" Visible="true" />
                                                        <%--<RiddhiS. on 09-Oct-2016: Visible set to false as KYIR is opened on Document open link >   --%>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <%-- End | Chitralekha M on 26-Sep-16>--%>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="overflow: auto; min-height: 150px !important;" class="gridScroll">
                                        <asp:DataGrid ID="grdOrder" runat="server" CssClass="Grid draggable" PageSize="10"
                                            AllowSorting="true" AutoGenerateColumns="false" AllowPaging="true" GridLines="None">
                                            <ItemStyle CssClass="GridItem  " />
                                            <SelectedItemStyle CssClass="GridItemSelect" />
                                            <AlternatingItemStyle CssClass="AlternatItemStyle " />
                                            <HeaderStyle CssClass="GridHeaderTitle " />
                                            <PagerStyle CssClass="GridPager " />
                                            <Columns>
                                                <asp:BoundColumn HeaderText="RFQ ID" DataField="ER_QuoteRequestId" SortExpression="ER_QuoteRequestId"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ext. Order ID" DataField="Order_ID" SortExpression="Order_ID"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order ID" DataField="EP_InternalOrderID" SortExpression="EP_InternalOrderID"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Order Details" HeaderStyle-ForeColor="BLack" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRightBorder" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnOrder_Details" runat="server" Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #cccccc" CommandName="GetOrderDetails" Text='Order Details' CausesValidation="False"
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            ToolTip="Click to view Order Details." class="grdPushBtn"></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Generate Document" HeaderStyle-ForeColor="White"
                                                    Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGenerateOrderDoc" runat="server" Style="background-image: none;
                                                            background-color: #DDD !important; color: #369 !important; border-width: 1px !important;
                                                            font-size: 12px; border-style: solid; border-color: #369" CommandName="GENERATEDOCUMENT"
                                                            Text='Generate Document' CausesValidation="False" onmouseover="OnHover(this);"
                                                            onmouseout="OnOut(this);" onblur="OnOut(this);" ToolTip="Click to generate document.">
                                                        </asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn HeaderText="RM Name" DataField="ER_RMName" SortExpression="ER_RMName"
                                                    HeaderStyle-ForeColor="White">
                                                    <%--Short Expression Changed By Mohit Lalwani from EP_RMName to ER_RMName on 1-Apr-2016 Jira:EQBOSDEV-309 --%>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Provider" DataField="PP_CODE" SortExpression="PP_CODE"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order Status" DataField="Field_DisplayAliasName" SortExpression="Order_Status"
                                                    HeaderStyle-ForeColor="White" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order Type" DataField="ELN_Order_Type" SortExpression="ELN_Order_Type"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Limit Prc1" DataField="LimitPrice1" SortExpression="LimitPrice1"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Limit Prc2" DataField="LimitPrice2" SortExpression="LimitPrice2"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Limit Prc3" DataField="LimitPrice3" SortExpression="LimitPrice3"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Exec. Prc1" DataField="EP_Execution_Price1" SortExpression="EP_Execution_Price1"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Exec. Prc2" DataField="EP_Execution_Price2" SortExpression="EP_Execution_Price2"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Exec. Prc3" DataField="EP_Execution_Price3" SortExpression="EP_Execution_Price3"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Avg Exec. Prc" DataField="EP_AveragePrice" SortExpression="EP_AveragePrice"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ordered Qty" DataField="Ordered_Qty" SortExpression="Ordered_Qty"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Filled Qty" DataField="Filled_Qty" SortExpression="Filled_Qty"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Tenor (mths)" DataField="ER_Tenor" SortExpression="ER_Tenor"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Share" DataField="ER_UnderlyingCode" SortExpression="ER_UnderlyingCode"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ccy" DataField="ER_CashCurrency" SortExpression="ER_CashCurrency"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Leverage" DataField="ER_LeverageRatio" SortExpression="ER_LeverageRatio">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Guarantee" DataField="ER_GuaranteedDuration" SortExpression="ER_GuaranteedDuration"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Strike(%)" DataField="EP_StrikePercentage" SortExpression="EP_StrikePercentage"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KO(%)" DataField="EP_KO" SortExpression="EP_KO" HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Coupon(%)" DataField="EP_CouponPercentage" SortExpression="EP_CouponPercentage"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Price(%)" DataField="EP_OfferPrice" SortExpression="EP_OfferPrice"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Upfront(%)" DataField="EP_RM_Margin" SortExpression="EP_RM_Margin">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Upfront(%)" DataField="EP_Upfront" SortExpression="EP_Upfront"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <%--Added client price,yield,upfront 11April --%>
                                                <asp:BoundColumn HeaderText="Client Price(%)" DataField="EP_Client_Price" SortExpression="EP_Client_Price">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Client Yield(%)p.a" DataField="EP_Client_Yield" SortExpression="EP_Client_Yield">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <%--/Added client price,yield,upfront 11April --%>
                                                <asp:BoundColumn HeaderText="Notional Amount" DataField="EP_Notional_Amount1" SortExpression="EP_Notional_Amount1"
                                                    HeaderStyle-ForeColor="White" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Issuer Order Remark" DataField="EP_Order_Remark1" SortExpression="EP_Order_Remark1"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ext. RFQ ID" DataField="EP_ExternalQuoteId" SortExpression="EP_ExternalQuoteId"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Booking Branch" DataField="EP_Deal_Booking_Branch" SortExpression="EP_Deal_Booking_Branch"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order Requested At" DataField="ER_TransactionTime" SortExpression="ER_TransactionTime"
                                                    DataFormatString="{0:dd-MMM-yyyy hh:mm:ss tt}" HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="RM OrderId" DataField="EP_HedgedFor" SortExpression="EP_HedgedFor"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Booking OrderId" DataField="EP_HedgingOrderId" SortExpression="EP_HedgingOrderId"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Type" DataField="ER_Type" SortExpression="ER_Type" HeaderStyle-ForeColor="White"
                                                    Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KI Type" DataField="ER_KI_Type" SortExpression="ER_KI_Type"
                                                    HeaderStyle-ForeColor="White" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KI % of init." DataField="ER_KI_Level" SortExpression="ER_KI_Level"
                                                    HeaderStyle-ForeColor="White" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Coupon Freq" DataField="ER_KI_ObservationFrequency"
                                                    SortExpression="ER_KI_ObservationFrequency" HeaderStyle-ForeColor="White" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Created By" DataField="ER_Created_By" SortExpression="created_by"
                                                    HeaderStyle-ForeColor="White" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KO Type" DataField="ER_KO_Type" SortExpression="ER_KO_Type"
                                                    HeaderStyle-ForeColor="White" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order Comment" DataField="EP_OrderComment" SortExpression="EP_OrderComment"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="true" Width="250px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <PagerStyle Mode="NumericPages" CssClass="GridPager " />
                                        </asp:DataGrid>
                                        <asp:DataGrid ID="grdDRAFCN" runat="server" CssClass="Grid draggable" PageSize="10"
                                            AllowSorting="true" AutoGenerateColumns="false" Width="100%" AllowPaging="true"
                                            GridLines="None">
                                            <ItemStyle CssClass="GridItem  " />
                                            <SelectedItemStyle CssClass="GridItemSelect" />
                                            <HeaderStyle CssClass="GridHeaderTitle" />
                                            <PagerStyle CssClass="GridPager " />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="RFQ ID" SortExpression="ER_QuoteRequestId" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDRARFQID" runat="server" CommandName="Select" Text='<%# Bind("ER_QuoteRequestId") %>'
                                                            CausesValidation="False" Font-Underline="true" ForeColor="#1580b2">ER_QuoteRequestId</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Create Pool" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRightBorder" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCreatePool_DRAFCN" runat="server" Style="background-image: none;
                                                            background-color: #F2F2F3 !important; color: #4D4C4C !important; border-width: 1px !important;
                                                            font-size: 12px; border-style: solid; border-color: #CCCCCC" CommandName="CREATEPOOLDRAFCN"
                                                            Text='Create Pool' CausesValidation="False" onmouseover="OnHover(this);" onmouseout="OnOut(this);"
                                                            onblur="OnOut(this);" ToolTip="Click to create pool for this RFQ." class="grdPushBtn">
                                                        </asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="RFQ Details" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRightBorder" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnRFQ_Details" runat="server" Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C  !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #cccccc" CommandName="GetRFQDetails" Text='RFQ Details' CausesValidation="False"
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            ToolTip="Click to view RFQ Details." class="grdPushBtn"></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Generate Document" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="upnlPrdGridDoc" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <input type="button" id="Button1" style="background-image: none; background-color: #F2F2F3 !important;
                                                                    color: #4D4C4C !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                                    border-color: #cccccc" value='Generate Document' onmouseover="OnHover(this);"
                                                                    onmouseout="OnOut(this);" onblur="OnOut(this);" title="Click to generate document."
                                                                    class="grdPushBtn" onclick="document.getElementById('ctl00_MainContent_KYIR').click();" />
                                                                <%--<asp:Button ID="btnGenerateDoc" runat="server" Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C  !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #cccccc" CommandName="GENERATEDOCUMENT" Text='Generate Document' CausesValidation="False"
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            ToolTip="Click to generate document." class="grdPushBtn"></asp:Button>--%>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn HeaderText="Solve For" DataField="ER_SolveFor" SortExpression="ER_SolveFor"
                                                    HeaderStyle-ForeColor="BLack">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Provider" DataField="PP_CODE" SortExpression="PP_CODE"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Price(%)" DataField="EP_OfferPrice" SortExpression="EP_OfferPrice"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Type" DataField="ER_Type" SortExpression="ER_Type" HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Share" DataField="ER_UnderlyingCode" SortExpression="ER_UnderlyingCode"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Strike(%)" DataField="EP_StrikePercentage" SortExpression="EP_StrikePercentage"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Coupon(%)" DataField="EP_CouponPercentage" SortExpression="EP_CouponPercentage"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Tenor (mths)" DataField="ER_Tenor" SortExpression="ER_Tenor"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="RFQ Tenor" DataField="ER_RFQTenor" SortExpression="ER_RFQTenor"
                                                    Visible="false" HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ccy" DataField="ER_CashCurrency" SortExpression="ER_CashCurrency"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order Qty" DataField="ER_CashOrderQuantity" SortExpression="ER_CashOrderQuantity"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KI Type" DataField="ER_KI_Type" SortExpression="ER_KI_Type"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KI % of init." DataField="ER_KI_Level" SortExpression="ER_KI_Level"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Guarantee" DataField="ER_GuaranteedDuration" SortExpression="ER_GuaranteedDuration"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Coupon Freq" DataField="ER_KI_ObservationFrequency"
                                                    SortExpression="ER_KI_ObservationFrequency" HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KO Type" DataField="ER_KO_Type" SortExpression="ER_KO_Type"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KO % of init." DataField="ER_KO_Level" SortExpression="ER_KO_Level"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Exchange" DataField="ER_Exchange" SortExpression="ER_Exchange"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="OTC" DataField="ER_OTC_YN" SortExpression="ER_OTC_YN"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Issuer Remark" DataField="EP_Quote_Request_Rejection_Reason"
                                                    SortExpression="EP_Quote_Request_Rejection_Reason" HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ext. RFQ ID" DataField="EP_ExternalQuoteId" SortExpression="EP_ExternalQuoteId"
                                                    HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Quote Requested At" DataField="ER_TransactionTime" SortExpression="ER_TransactionTime"
                                                    DataFormatString="{0:dd-MMM-yy hh:mm:ss tt}" HeaderStyle-ForeColor="White">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <%--Rushikesh. on 01-July-2015: Required to identify clubbed/PriceAll RFQ JIRA FA-925--%>
                                                <asp:BoundColumn HeaderText="Common RFQ Id" DataField="ClubbingRFQId" SortExpression="ClubbingRFQId"
                                                    HeaderStyle-ForeColor="White" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <%--End Rushikesh. on 01-July-2015: Required to identify clubbed/PriceAll RFQ JIRA FA-925--%>
                                                <asp:BoundColumn HeaderText="Quote Status" DataField="Quote_Status" SortExpression="Quote_Status"
                                                    HeaderStyle-ForeColor="White" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Upfront" DataField="ER_Upfront" SortExpression="ER_Upfront"
                                                    HeaderStyle-ForeColor="White" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Client Price(%)" DataField="EP_Client_Price" SortExpression="EP_Client_Price"
                                                    Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Client Yield(%)p.a" DataField="EP_Client_Yield" SortExpression="EP_Client_Yield"
                                                    Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Created By" DataField="ER_Created_By" SortExpression="created_by"
                                                    HeaderStyle-ForeColor="White" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <%--                                                <asp:BoundColumn HeaderText="Type" DataField="ER_Type" SortExpression="ER_Type" HeaderStyle-ForeColor="White"
                                                    Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>--%>
                                                <%--added by Chitralekha on 1-Oct-16--%>
                                                <asp:BoundColumn HeaderText="Best Price Y/N" DataField="EP_BestPrice_YN" SortExpression="EP_BestPrice_YN"
                                                    HeaderStyle-ForeColor="Black" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <%--ended by Chitralekha on 1-Oct-16--%>
                                            </Columns>
                                            <PagerStyle Mode="NumericPages" CssClass="GridPager " />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grdOrder" EventName="SortCommand" />
                        <asp:AsyncPostBackTrigger ControlID="grdDRAFCN" EventName="SortCommand" />
                        <asp:PostBackTrigger ControlID="KYIR" />
                        <%-- Changed by Chitralekha M on 26-Sep-16>--%>
                        <%-- <RiddhiS. on 09-Oct-2016: Opening KYIR on document btn click> --%>
                        <asp:PostBackTrigger ControlID="btnBAMLDoc" />
                        <asp:PostBackTrigger ControlID="btnBNPPDoc" />
                        <asp:PostBackTrigger ControlID="btnCITIDoc" />
                        <asp:PostBackTrigger ControlID="btnCOMMERZDoc" />
                        <asp:PostBackTrigger ControlID="btnCSDoc" />
                        <asp:PostBackTrigger ControlID="btnDBIBDoc" />
                        <asp:PostBackTrigger ControlID="btnHSBCDoc" />
                        <asp:PostBackTrigger ControlID="btnJPMDoc" />
                        <asp:PostBackTrigger ControlID="btnLEONTEQDoc" />
                        <asp:PostBackTrigger ControlID="btnOCBCDoc" />
                        <asp:PostBackTrigger ControlID="btnUBSDoc" />
                        <%-- </RiddhiS.> --%>
                    </Triggers>
                </asp:UpdatePanel>
                <%--<asp:UpdatePanel ID="upPTimerRefresh" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Timer ID="TmRefresh" runat="server">
                                    </asp:Timer>
                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                <asp:Button ID="btnhdnEnableDisableDealButtons" runat="server" OnClick="Enable_Disable_Deal_Buttons"
                    Style="visibility: hidden; display: none" onmouseover="JavaScript:alert:this.focus();" />
                <asp:Button ID="btnHdnEnablePage2" runat="server" OnClick="EnablePage" Style="visibility: hidden;
                    display: none" onmouseover="JavaScript:alert:this.focus();" />
                <asp:Button ID="btnhdnSolveAllRequests" runat="server" OnClick="Solve_All_Requests"
                    Style="visibility: hidden; display: none" onmouseover="JavaScript:alert:this.focus();" />
                <asp:Button ID="btnhdnSolveSingleRequest" runat="server" Style="visibility: hidden;
                    display: none" onmouseover="JavaScript:alert:this.focus();" />
                <asp:Button ID="btnHdnUpdateDRABasket" runat="server" Style="visibility: hidden;
                    display: none" OnClick="UpdateDRABasket" onmouseover="JavaScript:alert:this.focus();" />
            </td>
        </tr>
    </table>
    <%--</asp:Panel></ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:UpdatePanel ID="UPanle11111" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="DealConfirmPopup" runat="server" CssClass="ConfirmationPopup ui-widget-content ui-draggable"
                Visible="false">
                <%--<div id="" runat="server" class="" style="visibility: visible;">--%>
                <div id="Div1" class="msgbody" runat="server">
                    <div class="icon-confirmed">
                        <img src="Images/confirmed.png" width="20px" height="20px" alt="" />
                    </div>
                    <div style="width: 490px !important; cursor: move">
                        <h1>
                            You are placing an order for
                            <asp:Label ID="lblProductNamePopUpValue" runat="server" Text="Accumulator" CssClass="lbl"
                                Style="background-color: Transparent; color: White; font-size: larger; text-decoration: underline;"></asp:Label></h1>
                    </div>
                    <table cellpadding="2px" cellspacing="4px" width="490px" class="Filter">
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblIssuerPopUpCaption" runat="server" Text="Issuer" CssClass="lbl"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIssuerPopUpValue" runat="server" Text="JPM" CssClass="lblBOLD"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnderlyingPopUpCaption" runat="server" Text="Underlying" CssClass="lbl"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnderlyingPopUpValue" runat="server" Text="2628.HK" CssClass="lblBOLD"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblProductNamePopUpCaption" runat="server" Text="Product" CssClass="lbl"
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblRM" runat="server" Text="RM" Height="22px" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" colspan="3">
                                <telerik:RadDropDownList ID="ddlRM" runat="server" Style="width: 100% !important"
                                    AutoPostBack="true">
                                </telerik:RadDropDownList>
                                <img src="../App_Resources/email.png" style="border: 0px; width: 18px; height: 18px;
                                    display: none; visibility: hidden;" alt="RM EmailId" visible="false" />
                                <asp:Label ID="lblEmailId" runat="server" Text="Email&nbsp;ID" Height="22px" CssClass="lbl"
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblEmail" runat="server" CssClass="lbl " Height="22px" Text="" ForeColor="blue"
                                    Visible="false"></asp:Label>
                                <img src="../App_Resources/building.png" style="border: 0px; width: 18px; height: 18px;
                                    visibility: hidden; display: none;" alt="RM Branch" visible="false" />
                                <asp:Label ID="lblBran" runat="server" Text="Branch" CssClass="lbl" Height="22px"
                                    Visible="false"></asp:Label>
                            </td>
                            <%--<td class="lbl">
                                <asp:Label ID="lblIssuerPricePopUpCaption" runat="server" Text="Issuer Price" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblIssuerPricePopUpValue" runat="server" Text="98" CssClass="lblBOLD"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblBookingBranchPopUpCaption" runat="server" Text="Booking Branch"
                                    CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control">
                                <telerik:RadDropDownList ID="ddlBookingBranchPopUpValue" runat="server" Width="100%"
                                    Enabled="true">
                                    <%--  <Items>
                                    <telerik:DropDownListItem Text="Hong Kong" Value="HK"/>
                                    <telerik:DropDownListItem Text="Singapore" Value="SG" Selected="True"/>
                                </Items> --%>
                                </telerik:RadDropDownList>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblNotionalPopUpCaption" runat="server" Text="Notional" CssClass="lbl"></asp:Label>
                                <asp:Label ID="lblCurrencyPopUpValue" runat="server" Text="HKD" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="right" class="control">
                                <asp:Label ID="lblNotionalPopUpValue" runat="server" Text="0" CssClass="lblBOLD"
                                    Style="text-align: right"></asp:Label>
                                <%--EQBOSDEV-228 Removing 0.00 by 0 by chaitali  on21-Jan16 --%>
                            </td>
                            <%--<td class="lbl">
                                <asp:Label ID="lblClientPricePopUpCaption" runat="server" Text="Client Price" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblClientPricePopUpValue" runat="server" Text="99" CssClass="lblBOLD"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblIssuerPricePopUpCaption" runat="server" Text="IB Price %" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblIssuerPricePopUpValue" runat="server" Text="98" CssClass="lblBOLD"></asp:Label>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblClientPricePopUpCaption" runat="server" Text="Client Price %" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblClientPricePopUpValue" runat="server" Text="99" CssClass="lblBOLD"></asp:Label>
                            </td>
                            <%--<td class="lbl">
                                <asp:Label ID="lblClientYieldPopUpCaption" runat="server" Text="Client Yield" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblClientYieldPopUpValue" runat="server" Text="0.0%a.p." CssClass="lblBOLD"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="lbl" runat="server" id="tdStrikeCaption" visible="false">
                                <asp:Label ID="lblStrikePopUpCaption" runat="server" Text="Strike (%)" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" runat="server" id="tdStrikeValue" visible="false" align="right">
                                <asp:Label ID="lblStrikePopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>
                            </td>
                            <td class="lbl" id="tdTenorPopUpCaption" runat="server" visible="false" nowrap="nowrap">
                                <asp:Label ID="lblTenorPopUpCaption" runat="server" Text="Tenor" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" nowrap="nowrap" align="right" runat="server" id="tdTenorPopUpValue"
                                visible="false">
                                <asp:Label ID="lblTenorPopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>&nbsp;<asp:Label
                                    ID="lblTenorTypePopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl" nowrap="nowrap">
                                <asp:Label ID="lblKOPopUpCaption" runat="server" Text="KO Level(%)" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" nowrap="nowrap" align="right">
                                <asp:Label ID="lblKOPopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>&nbsp;<asp:Label
                                    ID="lblKOTypePopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>
                            </td>
                            <td class="lbl" nowrap="nowrap" runat="server" id="tdKICaption" visible="false">
                                <asp:Label ID="lblKIPopUpCaption" runat="server" Text="KI Level(%)" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" nowrap="nowrap" align="right" runat="server" id="tdKIValue" visible="false">
                                <asp:Label ID="lblKIPopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>&nbsp;<asp:Label
                                    ID="lblKITypePopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblUpfrontPopUpCaption" runat="server" Text="Upfront %" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control">
                                <asp:TextBox ID="txtUpfrontPopUpValue" runat="server" AutoPostBack="true" CssClass="txt"
                                    MaxLength="6" Style="width: 97% !important;"></asp:TextBox>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblClientYieldPopUpCaption" runat="server" Text="Client Yield %p.a."
                                    CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblClientYieldPopUpValue" runat="server" Text="0.0%a.p." CssClass="lblBOLD"></asp:Label>
                            </td>
                            <%--<td class="control">
                                <asp:Label ID="lblNotionalPopUpCaption" runat="server" Text="Notional" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNotionalPopUpValue" runat="server" Text="0.00" CssClass="lblBOLD"
                                    Style="text-align: right"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblOrderTypePopUpCaption" runat="server" Text="Order Type" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control">
                                <telerik:RadDropDownList ID="ddlOrderTypePopUpValue" runat="server" AutoPostBack="true"
                                    Width="100%">
                                    <Items>
                                        <telerik:DropDownListItem Text="Market" Value="Market" Selected />
                                        <telerik:DropDownListItem Text="Limit" Value="Limit" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblLimitPricePopUpCaption" runat="server" Text="Limit Level" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="left" style="white-space: nowrap">
                                <telerik:RadDropDownList ID="ddlBasketSharesPopValue" runat="server" Width="100%">
                                </telerik:RadDropDownList>
                                <%--Width of txtLimitPricePopUpValue removed for JIRA ID:FA-923--%>
                                <asp:TextBox ID="txtLimitPricePopUpValue" runat="server" Text="84" CssClass="txt"
                                    Style="text-align: right" AutoPostBack="true" MaxLength="12" onkeypress="Javascript:KeysAllowedForNotional();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="rowOrderComment" runat="server" visible="false">
                            <td>
                                <asp:Label ID="lblOrderComment" runat="server" Text="Comment: " CssClass="lbl"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtOrderCmt" runat="server" CssClass="txt" AutoPostBack="true" MaxLength="512"
                                    Style="width: 99% !important;"></asp:TextBox>
                            </td>
                        </tr>
                        <%--''<Nikhil M. on 16-Sep-2016:Added For Deal Conformation >--%>
                        <tr id="TblDealReason" runat="server" >
                            <td>
                                Non-Best Price Reason
                            </td>
                            <td colspan="3">
                                <%-- <asp:DropDownList ID="drpConfirmDeal" runat="server"></asp:DropDownList>--%>
                                <telerik:RadDropDownList CssClass="RadDropDownList RadDropDownList_Default" ID="drpConfirmDeal"
                                    runat="server" Width="360px">                <%--AshwiniP on 11-Nov-2016:To keep order popup width constant--%>
                                </telerik:RadDropDownList>
                            </td>
                        </tr>
                        <%--</Nikhil>--%>
                         <%--''<Rushi on 09-Nov-2016:Added For Advisory reason Conformation >--%>
                           <tr id="trAdvisoryReason" runat="server" visible="true">
                                            <td>Advisory Reason</td>
                                            <td colspan="3">
                                            <telerik:RadDropDownList CssClass="RadDropDownList RadDropDownList_Default"  ID="ddlAdvisoryReason" runat="server" Width="360px">  <%--AshwiniP on 11-Nov-2016:To keep order popup width constant--%>
                                            </telerik:RadDropDownList>
                                            </td>
                           </tr>
                           <%--</Rushi>--%>
                           
                        <tr>
                            <td colspan="4" align="center">
                                <asp:GridView ID="grdRMData" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound"
                                    CssClass="grayBorder" DataKeyNames="RM_Name" BorderColor="#D5D5D5" RowStyle-VerticalAlign="Middle"
                                    RowStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="grayBorder" HeaderStyle-CssClass="grayBorder">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RM Name" ItemStyle-Width="150" ItemStyle-BorderColor="#D5D5D5"
                                            ItemStyle-CssClass="grayBorder" HeaderStyle-CssClass="grayBorder">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRM_Name" runat="server" Text='<%# Eval("RM_Name") %>'></asp:Label>
                                                <%--<asp:DropDownList ID="ddlRMName" runat="server" Visible="false" Width="92%" CssClass="ddl"
                                                    OnSelectedIndexChanged="ddl_OnSelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>--%>
                                                <telerik:RadDropDownList ID="ddlRMName" runat="server" Visible="false" Width="92%"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_OnSelectedIndexChanged">
                                                </telerik:RadDropDownList>
                                                <%-- <telerik:RadDropDownList ID="ddlRMName" runat="server" Visible="false" Width="92%"  OnSelectedIndexChanged ="ddl_OnSelectedIndexChanged">
                                                </telerik:RadDropDownList>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CIF/PAN" ItemStyle-Width="150" ItemStyle-CssClass="grayBorder"
                                            HeaderStyle-CssClass="grayBorder">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCIFPAN" runat="server" Text='<%# Eval("Account_Number") %>'></asp:Label>
                                                <%-- <asp:TextBox ID="txtAccNo" runat="server" Text='<%# Eval("Account_Number") %>' Visible="false"
                                                    CssClass="txt" Width="92%" OnTextChanged="txtBox_onTextChanged" AutoPostBack="True"></asp:TextBox>--%>
                                                <%--       <telerik:RadDropDownList ID="ddlCIFPAN" runat="server" Width="92%" AutoPostBack =true  OnSelectedIndexChanged="ddlCIFPAN_onTextChanged" >                                        
                                                      </telerik:RadDropDownList>--%>
                                                <uc2:FinIQ_Fast_Find_Customer ID="FindCustomer" runat="server" DoPostBack="true"
                                                    OnCustomer_Selected="CustomerSelected" EnableTheming="true" SetWidth="98" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Notional" ItemStyle-Width="150" ItemStyle-CssClass="grayBorder"
                                            HeaderStyle-CssClass="grayBorder">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label Style="text-align: right;" ID="LabelAN" runat="server" Text='<%# Eval("AlloNotional") %>'></asp:Label>
                                                <asp:TextBox ID="txtAlloNotional" Style="text-align: right;" runat="server" Text='<%# Eval("AlloNotional") %>'
                                                    CssClass="txt" Visible="false" Width="92%" OnTextChanged="ddlCIFPAN_onTextChanged"
                                                    AutoPostBack="True" MaxLength="15"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UniqueID" ItemStyle-Width="150" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUniqueID" runat="server" Text='<%# Eval("EPOF_OrderId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <%-- <ashwiniP on 20Sept16>--%>
                        <tr  runat="server"  id="tblRw1" >
                            <td class="lbl"   >
                                <asp:Label ID="lblTotalAmt" runat="server" Text="Total Notional" CssClass="lbl" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotalAmtVal" runat="server" Text=" " CssClass="lbl" Visible="false"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                                <input type="button" id="btnAddAllocation" runat="server" value="Add Allocation"
                                    visible="true" style="width: 120px" class="btn" onmouseover="JavaScript:alert:this.focus();" />
                            </td>
                        </tr>
                        <tr  runat="server"  id="tblRw2" >
                            <td class="lbl">
                                <asp:Label ID="lblAlloAmt" runat="server" Text="Allocated Notional" CssClass="lbl"
                                    Visible="false" Style="display: inline-flex;"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAlloAmtVal" runat="server" Text=" " CssClass="lbl" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr  runat="server"  id="tblRw3">
                            <td class="lbl">
                                <asp:Label ID="lblRemainNotional" runat="server" Text="Remaining Notional" CssClass="lbl"
                                    Visible="false" Style="display: inline-flex;"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRemainNotionalVal" runat="server" Text=" " CssClass="lbl" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <%-- </ashwiniP on 20Sept16>--%>
                        <tr>
                            <td colspan="4">
                                <div>
                                    <asp:CheckBox ID="chkUpfrontOverride" runat="server" Visible="false" />
                                    <asp:Label ID="lblerrorPopUp" runat="server" ForeColor="Red"></asp:Label>
                                    <%--<asp:CheckBox ID="chkConfirmDeal" runat="server" Visible="false" Text="Do you want to proceed?"/>--%>
                                    <%--''<Nikhil M. on 08-Sep-2016: ></Nikhil>--%>
                                </div>
                                <div class="clsButton" style="text-align: center; line-height: 25px;">
                                    <input type="button" id="btnDealConfirm" runat="server" value="Confirm" class="btn"
                                        onmouseover="JavaScript:alert:this.focus();" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnRedirect"
                                        DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div style="background-color: #FFFFFF;">
                                                <img alt="Loading" src="../App_Resources/loading.gif" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdatePanel ID="upnRedirect" runat="server">
                                        <ContentTemplate>
                                            <input type="button" id="btnRedirect" runat="server" value="Redirect" visible="false"
                                                style="width: 100px" class="btn" onmouseover="JavaScript:alert:this.focus();" />
                                            <input type="button" id="btnCapturePoolPrice" runat="server" value="Capture Price"
                                                visible="false" style="width: 100px" class="btn" onmouseover="JavaScript:alert:this.focus();" />
                                            <input type="button" id="btnDealCancel" runat="server" value="Cancel" class="btn"
                                                onmouseover="JavaScript:alert:this.focus();" />
                                            <asp:Button ID="btnHdnEnablePage" runat="server" OnClick="EnablePage" Style="visibility: hidden;
                                                display: none" onmouseover="JavaScript:alert:this.focus();" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 10px !important;">
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upnlDetails" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlDetailsPopup" runat="server" CssClass="ConfirmationPopup ui-widget-content ui-draggable"
                Visible="false" Style="width: top:100px; left: 550px;">
                <div id="Div2" runat="server">
                    <table width="100%" style="background-color: rgb(93, 123, 157);">
                        <tr>
                            <td width="95%">
                                <div class="icon-confirmed">
                                    <img src="Images/confirmed.png" width="20px" height="20px" alt="" />
                                </div>
                                <div style="width: 100% !important; cursor: move">
                                    <h1>
                                        <asp:Label ID="lblDetails" runat="server" Text="Order Details" /></h1>
                                </div>
                            </td>
                            <td align="right" width="5%" background-color="#5D7B9D">
                                <div>
                                    <input id="btnDetailsCancle" runat="server" class="btn" onmouseover="JavaScript:alert:this.focus();"
                                        type="button" value="X" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table id="Table2" class="TFtable" style="white-space: nowrap !important; width: 100%;">
                        <tr id="trDraOrderStatus" runat="server">
                            <td>
                                <asp:Label ID="lblAlloDraOrderStatustitle" runat="server" CssClass="lbl" Text="Order Status"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraOrderStatus" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label26" runat="server" CssClass="lbl" Text="Type" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraType" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label27" runat="server" CssClass="lbl" Text="RFQ ID" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraRFQID" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label28" runat="server" CssClass="lbl" Text="Counterparty" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraCp" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label29" runat="server" CssClass="lbl" Text="Underlying Name(s)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraUnderlying" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label30" runat="server" CssClass="lbl" Text="Note Ccy." Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraNoteCcy" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label31" runat="server" CssClass="lbl" Text="Strike (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraStrike" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label32" runat="server" CssClass="lbl" Text="KI (%) of Initial" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraKIper" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label33" runat="server" CssClass="lbl" Text="KO (%) of Initial" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraKOper" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label34" runat="server" CssClass="lbl" Text="Tenor" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraTenor" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label35" runat="server" CssClass="lbl" Text="Price (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraPrice" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label36" runat="server" CssClass="lbl" Text="Upfront (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraUpfront" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label37" runat="server" CssClass="lbl" Text="Client price (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraClientprice" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label38" runat="server" CssClass="lbl" Text="Coupon (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraCoupon" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label39" runat="server" CssClass="lbl" Text="Coupon Frequency" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraCouponFrequency" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id="AlloDraGuarantee" runat="server">
                            <td>
                                <asp:Label ID="Label40" runat="server" CssClass="lbl" Text="Guarantee" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraGuarantee" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label41" runat="server" CssClass="lbl" Text="Order Size" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraOrderSize" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id='trDRAQuoteStatus' runat="server">
                            <td>
                                <asp:Label ID="Label77" runat="server" CssClass="lbl" Text="Quote Status" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblDRAQuoteStatus" runat="server" Text="" Style="white-space: pre-wrap;" />
                            </td>
                        </tr>
                        <tr id="trDraOrderType" runat="Server">
                            <td>
                                <asp:Label ID="Label42" runat="server" CssClass="lbl" Text="Order Type" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraOrderType" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id="trDraLimitPrice" runat="Server" visible="false">
                            <td>
                                <asp:Label ID="Label43" runat="server" CssClass="lbl" Text="Limit Price" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraLimitPrice" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id="trDraSpot1" runat="Server">
                            <td>
                                <asp:Label ID="Label44" runat="server" CssClass="lbl" Text="Ref. Spot 1" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraSpot1" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id="trDraExePrc1" runat="Server">
                            <td>
                                <asp:Label ID="Label46" runat="server" CssClass="lbl" Text="Exec. Price1" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraExePrc1" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id="trDraExePrc2" runat="Server">
                            <td>
                                <asp:Label ID="Label51" runat="server" CssClass="lbl" Text="Exec. Price2" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraExePrc2" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id="trDraExePrc3" runat="Server">
                            <td>
                                <asp:Label ID="Label72" runat="server" CssClass="lbl" Text="Exec. Price3" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraExePrc3" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id="trDraAvgExePrc" runat="Server">
                            <td>
                                <asp:Label ID="Label75" runat="server" CssClass="lbl" Text="Avg. Exec. Price" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraAvgExePrc" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label47" runat="server" CssClass="lbl" Text="Issuer Remark" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraRemarks" runat="server" Text="" Style="white-space: pre-wrap;" />
                            </td>
                        </tr>
                        <tr id="trDraOrderComment" runat="Server" visible="false">
                            <td>
                                <asp:Label ID="Label80" runat="server" CssClass="lbl" Text="Order Comment" Style="font-weight: bold;
                                    background-color: Transparent;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraOrderComment" runat="server" Text="" Style="white-space: pre-wrap;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label48" runat="server" CssClass="lbl" Text="Submitted by" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloDraSubmittedby" runat="server" Text="" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upPTimerRefresh" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Timer ID="TmRefresh" runat="server" OnTick="EnableTimerTick">
            </asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function CP_Dragable() {
            $(".ConfirmationPopup").draggable({ containment: "window" });  //Mohit Lalwani on 1-Feb-2016
        }
    </script>

</asp:Content>
