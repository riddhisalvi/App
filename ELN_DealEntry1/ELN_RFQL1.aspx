<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ELN_RFQL1.aspx.vb" MasterPageFile="~/FiniqAppMasterPage.Master"
    EnableEventValidation="false" Inherits="FinIQWebApp.ELN_RFQL1" Title="ELN RFQ and Orders" %>
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

    <script type="text/javascript" src="../FinIQJS/Jquery/jquery.min.js"></script>

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
            if (document.getElementById("<%=chkLEONTEQ.ClientID%>")) { document.getElementById("<%=chkLEONTEQ.ClientID%>").style.visibility = 'visible'; }
            if (document.getElementById("<%=chkCOMMERZ.ClientID%>")) { document.getElementById("<%=chkCOMMERZ.ClientID%>").style.visibility = 'visible'; }
        }

        function hideLPBoxes()
        {
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
                }  }
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
            else if (btnDeal.indexOf("DBIB") == 21) { //Did not replaced DBIB with DB since we are checking client ID
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
                else  if (btnDeal.indexOf("OCBC") == 21) {
                    intervalCopyOCBC = interval;
                    processingOCBC = true;
                    if ('Enable' == document.getElementById("<%=OCBCHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                else  if (btnDeal.indexOf("CITI") == 21) {
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
                        url: "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx/Web_CheckForAsyncPriceResponsewithMailForSolo",
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
                                        JpmHiddenAccDays = document.getElementById("<%=JpmHiddenAccDays.ClientID %>");
                                        JpmHiddenAccDays.value = '';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        HsbcHiddenAccDays = document.getElementById("<%=HsbcHiddenAccDays.ClientID %>");
                                        HsbcHiddenAccDays.value = '';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        CsHiddenAccDays = document.getElementById("<%=CsHiddenAccDays.ClientID %>");
                                        CsHiddenAccDays.value = '';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        UbsHiddenAccDays = document.getElementById("<%=UbsHiddenAccDays.ClientID %>");
                                        UbsHiddenAccDays.value = '';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        BAMLHiddenAccDays = document.getElementById("<%=BAMLHiddenAccDays.ClientID %>");
                                        BAMLHiddenAccDays.value = '';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenAccDays = document.getElementById("<%=BNPPHiddenAccDays.ClientID %>");
                                        BNPPHiddenAccDays.value = '';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        DBIBHiddenAccDays = document.getElementById("<%=DBIBHiddenAccDays.ClientID %>");
                                        DBIBHiddenAccDays.value = '';
                                        processingDBIB = false;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        OCBCHiddenAccDays = document.getElementById("<%=OCBCHiddenAccDays.ClientID %>");
                                        OCBCHiddenAccDays.value = '';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        CITIHiddenAccDays = document.getElementById("<%=CITIHiddenAccDays.ClientID %>");
                                        CITIHiddenAccDays.value = '';
                                        processingCITI = false;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        LEONTEQHiddenAccDays = document.getElementById("<%=LEONTEQHiddenAccDays.ClientID %>");
                                        LEONTEQHiddenAccDays.value = '';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        COMMERZHiddenAccDays = document.getElementById("<%=COMMERZHiddenAccDays.ClientID %>");
                                        COMMERZHiddenAccDays.value = '';
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
                                        JpmHiddenAccDays = document.getElementById("<%=JpmHiddenAccDays.ClientID %>");
                                        JpmHiddenAccDays.value = '';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        HsbcHiddenAccDays = document.getElementById("<%=HsbcHiddenAccDays.ClientID %>");
                                        HsbcHiddenAccDays.value = '';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        CsHiddenAccDays = document.getElementById("<%=CsHiddenAccDays.ClientID %>");
                                        CsHiddenAccDays.value = '';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        UbsHiddenAccDays = document.getElementById("<%=UbsHiddenAccDays.ClientID %>");
                                        UbsHiddenAccDays.value = '';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        BAMLHiddenAccDays = document.getElementById("<%=BAMLHiddenAccDays.ClientID %>");
                                        BAMLHiddenAccDays.value = '';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenAccDays = document.getElementById("<%=BNPPHiddenAccDays.ClientID %>");
                                        BNPPHiddenAccDays.value = '';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        DBIBHiddenAccDays = document.getElementById("<%=DBIBHiddenAccDays.ClientID %>");
                                        DBIBHiddenAccDays.value = '';
                                        processingDBIB = false;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        OCBCHiddenAccDays = document.getElementById("<%=OCBCHiddenAccDays.ClientID %>");
                                        OCBCHiddenAccDays.value = '';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        CITIHiddenAccDays = document.getElementById("<%=CITIHiddenAccDays.ClientID %>");
                                        CITIHiddenAccDays.value = '';
                                        processingCITI = false;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        LEONTEQHiddenAccDays = document.getElementById("<%=LEONTEQHiddenAccDays.ClientID %>");
                                        LEONTEQHiddenAccDays.value = '';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        COMMERZHiddenAccDays = document.getElementById("<%=COMMERZHiddenAccDays.ClientID %>");
                                        COMMERZHiddenAccDays.value = '';
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
                                        JpmHiddenAccDays = document.getElementById("<%=JpmHiddenAccDays.ClientID %>");
                                        JpmHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = true;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        HsbcHiddenAccDays = document.getElementById("<%=HsbcHiddenAccDays.ClientID %>");
                                        HsbcHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        processingHSBC = true;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        CsHiddenAccDays = document.getElementById("<%=CsHiddenAccDays.ClientID %>");
                                        CsHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        processingCS = true;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        UbsHiddenAccDays = document.getElementById("<%=UbsHiddenAccDays.ClientID %>");
                                        UbsHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        processingUBS = true;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        BAMLHiddenAccDays = document.getElementById("<%=BAMLHiddenAccDays.ClientID %>");
                                        BAMLHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        processingBAML = true;

                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenAccDays = document.getElementById("<%=BNPPHiddenAccDays.ClientID %>");
                                        BNPPHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        processingBNPP = true;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        DBIBHiddenAccDays = document.getElementById("<%=DBIBHiddenAccDays.ClientID %>");
                                        DBIBHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        processingDBIB = true;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        OCBCHiddenAccDays = document.getElementById("<%=OCBCHiddenAccDays.ClientID %>");
                                        OCBCHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = true;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        CITIHiddenAccDays = document.getElementById("<%=CITIHiddenAccDays.ClientID %>");
                                        CITIHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = true;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        LEONTEQHiddenAccDays = document.getElementById("<%=LEONTEQHiddenAccDays.ClientID %>");
                                        LEONTEQHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = true;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        COMMERZHiddenAccDays = document.getElementById("<%=COMMERZHiddenAccDays.ClientID %>");
                                        COMMERZHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
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
        function getStrike(RFQId, lblPrice, lblTimer, btnDeal, btnPrice1) {//Added By Imran 12-March-14
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
                        url: "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx/Web_CheckForAsyncStrikeResponsewithMailForSolo",
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
                                        JpmHiddenAccDays = document.getElementById("<%=JpmHiddenAccDays.ClientID %>");
                                        JpmHiddenAccDays.value = '';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        HsbcHiddenAccDays = document.getElementById("<%=HsbcHiddenAccDays.ClientID %>");
                                        HsbcHiddenAccDays.value = '';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        CsHiddenAccDays = document.getElementById("<%=CsHiddenAccDays.ClientID %>");
                                        CsHiddenAccDays.value = '';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        UbsHiddenAccDays = document.getElementById("<%=UbsHiddenAccDays.ClientID %>");
                                        UbsHiddenAccDays.value = '';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        BAMLHiddenAccDays = document.getElementById("<%=BAMLHiddenAccDays.ClientID %>");
                                        BAMLHiddenAccDays.value = '';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenAccDays = document.getElementById("<%=BNPPHiddenAccDays.ClientID %>");
                                        BNPPHiddenAccDays.value = '';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        DBIBHiddenAccDays = document.getElementById("<%=DBIBHiddenAccDays.ClientID %>");
                                        DBIBHiddenAccDays.value = '';
                                        processingDBIB = false;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        OCBCHiddenAccDays = document.getElementById("<%=OCBCHiddenAccDays.ClientID %>");
                                        OCBCHiddenAccDays.value = '';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        CITIHiddenAccDays = document.getElementById("<%=CITIHiddenAccDays.ClientID %>");
                                        CITIHiddenAccDays.value = '';
                                        processingCITI = false;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        LEONTEQHiddenAccDays = document.getElementById("<%=LEONTEQHiddenAccDays.ClientID %>");
                                        LEONTEQHiddenAccDays.value = '';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        COMMERZHiddenAccDays = document.getElementById("<%=COMMERZHiddenAccDays.ClientID %>");
                                        COMMERZHiddenAccDays.value = '';
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
                                        JpmHiddenAccDays = document.getElementById("<%=JpmHiddenAccDays.ClientID %>");
                                        JpmHiddenAccDays.value = '';
                                        processingJPM = false;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        HsbcHiddenAccDays = document.getElementById("<%=HsbcHiddenAccDays.ClientID %>");
                                        HsbcHiddenAccDays.value = '';
                                        processingHSBC = false;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        CsHiddenAccDays = document.getElementById("<%=CsHiddenAccDays.ClientID %>");
                                        CsHiddenAccDays.value = '';
                                        processingCS = false;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        UbsHiddenAccDays = document.getElementById("<%=UbsHiddenAccDays.ClientID %>");
                                        UbsHiddenAccDays.value = '';
                                        processingUBS = false;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        BAMLHiddenAccDays = document.getElementById("<%=BAMLHiddenAccDays.ClientID %>");
                                        BAMLHiddenAccDays.value = '';
                                        processingBAML = false;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenAccDays = document.getElementById("<%=BNPPHiddenAccDays.ClientID %>");
                                        BNPPHiddenAccDays.value = '';
                                        processingBNPP = false;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        DBIBHiddenAccDays = document.getElementById("<%=DBIBHiddenAccDays.ClientID %>");
                                        DBIBHiddenAccDays.value = '';
                                        processingDBIB = false;
                                    }
                                    else  if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        OCBCHiddenAccDays = document.getElementById("<%=OCBCHiddenAccDays.ClientID %>");
                                        OCBCHiddenAccDays.value = '';
                                        processingOCBC = false;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        CITIHiddenAccDays = document.getElementById("<%=CITIHiddenAccDays.ClientID %>");
                                        CITIHiddenAccDays.value = '';
                                        processingCITI = false;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        LEONTEQHiddenAccDays = document.getElementById("<%=LEONTEQHiddenAccDays.ClientID %>");
                                        LEONTEQHiddenAccDays.value = '';
                                        processingLEONTEQ = false;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#COMMERZwait").hide();
                                        document.getElementById('COMMERZwait').style.visibility = 'hidden';
                                        COMMERZHiddenAccDays = document.getElementById("<%=COMMERZHiddenAccDays.ClientID %>");
                                        COMMERZHiddenAccDays.value = '';
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
                                        JpmHiddenAccDays = document.getElementById("<%=JpmHiddenAccDays.ClientID %>");
                                        JpmHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#JPMwait").hide();
                                        document.getElementById('JPMwait').style.visibility = 'hidden';
                                        processingJPM = true;
                                    }
                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                        HsbcHiddenPrice = document.getElementById("<%=HsbcHiddenPrice.ClientID %>");
                                        HsbcHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        HsbcHiddenAccDays = document.getElementById("<%=HsbcHiddenAccDays.ClientID %>");
                                        HsbcHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#HSBCwait").hide();
                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                        processingHSBC = true;
                                    }
                                    else if (btnDeal.indexOf("CS") == 21) {
                                        CsHiddenPrice = document.getElementById("<%=CsHiddenPrice.ClientID %>");
                                        CsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        CsHiddenAccDays = document.getElementById("<%=CsHiddenAccDays.ClientID %>");
                                        CsHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#CSwait").hide();
                                        document.getElementById('CSwait').style.visibility = 'hidden';
                                        processingCS = true;
                                    }
                                    else if (btnDeal.indexOf("UBS") == 21) {
                                        UbsHiddenPrice = document.getElementById("<%=UbsHiddenPrice.ClientID %>");
                                        UbsHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        UbsHiddenAccDays = document.getElementById("<%=UbsHiddenAccDays.ClientID %>");
                                        UbsHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#UBSwait").hide();
                                        document.getElementById('UBSwait').style.visibility = 'hidden';
                                        processingUBS = true;
                                    }
                                    else if (btnDeal.indexOf("BAML") == 21) {
                                        BAMLHiddenPrice = document.getElementById("<%=BAMLHiddenPrice.ClientID %>");
                                        BAMLHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        BAMLHiddenAccDays = document.getElementById("<%=BAMLHiddenAccDays.ClientID %>");
                                        BAMLHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#BAMLwait").hide();
                                        document.getElementById('BAMLwait').style.visibility = 'hidden';
                                        processingBAML = true;
                                    }
                                    else if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        BNPPHiddenAccDays = document.getElementById("<%=BNPPHiddenAccDays.ClientID %>");
                                        BNPPHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        processingBNPP = true;
                                    }
                                    else if (btnDeal.indexOf("DBIB") == 21) {
                                        DBIBHiddenPrice = document.getElementById("<%=DBIBHiddenPrice.ClientID %>");
                                        DBIBHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        $("#DBIBwait").hide();
                                        document.getElementById('DBIBwait').style.visibility = 'hidden';
                                        DBIBHiddenAccDays = document.getElementById("<%=DBIBHiddenAccDays.ClientID %>");
                                        DBIBHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        processingDBIB = true;
                                    }
                                    else if (btnDeal.indexOf("OCBC") == 21) {
                                        OCBCHiddenPrice = document.getElementById("<%=OCBCHiddenPrice.ClientID %>");
                                        OCBCHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        OCBCHiddenAccDays = document.getElementById("<%=OCBCHiddenAccDays.ClientID %>");
                                        OCBCHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#OCBCwait").hide();
                                        document.getElementById('OCBCwait').style.visibility = 'hidden';
                                        processingOCBC = true;
                                    }
                                    else if (btnDeal.indexOf("CITI") == 21) {
                                        CITIHiddenPrice = document.getElementById("<%=CITIHiddenPrice.ClientID %>");
                                        CITIHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        CITIHiddenAccDays = document.getElementById("<%=CITIHiddenAccDays.ClientID %>");
                                        CITIHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#CITIwait").hide();
                                        document.getElementById('CITIwait').style.visibility = 'hidden';
                                        processingCITI = true;
                                    }
                                    else if (btnDeal.indexOf("LEONTEQ") == 21) {
                                        LEONTEQHiddenPrice = document.getElementById("<%=LEONTEQHiddenPrice.ClientID %>");
                                        LEONTEQHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        LEONTEQHiddenAccDays = document.getElementById("<%=LEONTEQHiddenAccDays.ClientID %>");
                                        LEONTEQHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
                                        $("#LEONTEQwait").hide();
                                        document.getElementById('LEONTEQwait').style.visibility = 'hidden';
                                        processingLEONTEQ = true;
                                    }
                                    else if (btnDeal.indexOf("COMMERZ") == 21) {
                                        COMMERZHiddenPrice = document.getElementById("<%=COMMERZHiddenPrice.ClientID %>");
                                        COMMERZHiddenPrice.value = msg.d + ';Enable;Enable;Disable;Order';
                                        COMMERZHiddenAccDays = document.getElementById("<%=COMMERZHiddenAccDays.ClientID %>");
                                        COMMERZHiddenAccDays.value = msg.d.split("^")[0] + '^' + msg.d.split("^")[1];
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


        //Added By Imran to stop all timer 12-March-14.
        //Currently not in use.
        function StopAllTimers() {
            clearTimeout(tmrJPM);
            document.getElementById("<%=lblTimerHSBC.ClientID %>").innerHTML = "";
            clearTimeout(tmrHSBC);
            document.getElementById("<%=lblTimer.ClientID %>").innerHTML = "";
            clearTimeout(tmrCS);
            document.getElementById("<%=lblUBSTimer.ClientID %>").innerHTML = "";
            clearTimeout(tmrUBS);
            document.getElementById("<%=lblTimerCS.ClientID %>").innerHTML = "";
            clearTimeout(tmrBAML);
            document.getElementById("<%=lblTimerBAML.ClientID %>").innerHTML = "";
            clearTimeout(tmrBNPP);
            document.getElementById("<%=lblTimerBNPP.ClientID %>").innerHTML = "";
			clearTimeout(tmrDBIB);
            document.getElementById("<%=lblTimerDBIB.ClientID %>").innerHTML = "";
            clearTimeout(tmrOCBC);
            document.getElementById("<%=lblTimerOCBC.ClientID %>").innerHTML = "";
            clearTimeout(tmrCITI);
            document.getElementById("<%=lblTimerCITI.ClientID %>").innerHTML = "";
             clearTimeout(tmrLEONTEQ);
             document.getElementById("<%=lblTimerLEONTEQ.ClientID %>").innerHTML = "";
             clearTimeout(tmrCOMMERZ);
             document.getElementById("<%=lblTimerCOMMERZ.ClientID %>").innerHTML = "";
            
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

        //Added By Imran to stop specific timer 12-March-14.
        //Currently not in use.
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

        //Added By Imran to stop specific timer based on label,button 12-March-14.
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

        //Added By Imran to start specific timer based on label,button 12-March-14.
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
                if ($("#ctl00_MainContent_DealConfirmPopup").is(':visible'))        //FIre Server trip only if pop-up is visible
                {
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
                    //if ($("#ctl00_MainContent_lblIssuerPopUpValue").text().toUpperCase() == "DBIB") { //<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text().toUpperCase() == "DB") {
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
                else  if (btnDeal.indexOf("OCBC") == 21) {
                    tmrOCBC = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                else  if (btnDeal.indexOf("CITI") == 21) {
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
            __doPostBack('<%= upnl1.ClientID %>', '');
        }


        function OnHover(val) {
            val.style.backgroundColor = "#FFF";
        }
        function OnOut(val) {
            val.style.backgroundColor = "#F2F2F3";
        }
        setResolution();
        $(document).ready(function() {
            //hideLPBoxes(); ''<Nikhil M. on 17-Sep-2016: Commented>
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

        //Added by Imran P 22-sept-2015 to calculate Strike Price
        function calStrikePriceForELN() {
            var UPrice = replaceAll(document.getElementById('<%=txtUnderlyingPrice.ClientID%>').value, ",", "");
            var StrikePer = document.getElementById('<%=txtStrikePer.ClientID%>').value;
            if (isNaN(UPrice) || isNaN(StrikePer) || UPrice == '' || StrikePer == '') {
                document.getElementById('<%=txtStrikePrice.ClientID%>').value = "";
            }
            else {
                document.getElementById('<%=txtStrikePrice.ClientID%>').value = FormatAmount(((UPrice) * (StrikePer / 100)).toFixed(2));
                var NoAmount = replaceAll(document.getElementById('<%=txtNotionalAmount.ClientID%>').value, ",", "");
                var NoShares = replaceAll(document.getElementById('<%=txtNoShares.ClientID%>').value, ",", "");
                if (!isNaN(NoAmount) && NoAmount != '') {
                    calNoOfShareELN();
                }
                else if (!isNaN(NoShares) && NoShares != '') {
                    calNotionalAmountELN();
                }
                else {
                    //  document.getElementById('<%=txtNotionalAmount.ClientID%>').value = "";
                }
            }
        }
        //Formating with comma
        function formatNum() {
            document.getElementById('<%=txtNotionalAmount.ClientID%>').value = FormatAmount(document.getElementById('<%=txtNotionalAmount.ClientID%>').value);
            document.getElementById('<%=txtNoShares.ClientID%>').value = FormatAmount(document.getElementById('<%=txtNoShares.ClientID%>').value);
        }
        //Allow decimal and numeric
        function allowDecimal() {
            AllowNumericWithdecimal('<%=txtStrikePer.ClientID%>')
        }
        //Added by Imran P 22-sept-2015 to calculate no of shares
        function calNoOfShareELN() {
            var SPrice = replaceAll(document.getElementById('<%=txtStrikePrice.ClientID%>').value, ",", "");
            var NoAmount = replaceAll(document.getElementById('<%=txtNotionalAmount.ClientID%>').value, ",", "");
            if (isNaN(SPrice) || isNaN(NoAmount) || SPrice == '' || NoAmount == '') {
                document.getElementById('<%=txtNoShares.ClientID%>').value = "";
            }
            else {
                document.getElementById('<%=txtNoShares.ClientID%>').value = FormatAmount((NoAmount / SPrice).toFixed(2));
            }
        }



        //Added by Imran P 22-sept-2015 to calculate notional amount
        function calNotionalAmountELN() {
            var SPrice = replaceAll(document.getElementById('<%=txtStrikePrice.ClientID%>').value, ",", "");
            var NoShares = replaceAll(document.getElementById('<%=txtNoShares.ClientID%>').value, ",", "");
            if (isNaN(SPrice) || isNaN(NoShares) || SPrice == '' || NoShares == '') {
                document.getElementById('<%=txtNotionalAmount.ClientID%>').value = "";
            }
            else {
                //EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                document.getElementById('<%=txtNotionalAmount.ClientID%>').value = FormatAmount((NoShares * SPrice).toFixed(0));     // Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F

            }
        }

        //Added by Imran P 22-sept-2015 to clear text box of ELN calculator
        function clearELNCalData() {
            document.getElementById('<%=txtUnderlyingPrice.ClientID%>').value = "";
            document.getElementById('<%=txtStrikePer.ClientID%>').value = "";
            document.getElementById('<%=txtStrikePrice.ClientID%>').value = "";
            document.getElementById('<%=txtNotionalAmount.ClientID%>').value = "";
            document.getElementById('<%=txtNoShares.ClientID%>').value = "";

        }

        //Added by Imran P 22-sept-2015 to calculate Strike Price
        function calAllForELN() {
            calStrikePriceForELN();
            var NoAmount = replaceAll(document.getElementById('<%=txtNotionalAmount.ClientID%>').value, ",", "");
            var NoShares = replaceAll(document.getElementById('<%=txtNoShares.ClientID%>').value, ",", "");
            if (!isNaN(NoAmount) && NoAmount != '') {
                calNoOfShareELN();
            }
            else if (isNaN(NoShares) && NoShares == '') {
                calNotionalAmountELN();
            }
            else {

            }
        }

        function ConvertFormattedAmountToNumber(control) {
            var param = "";
            var amountportion = 0;
            var result = 0;
            var charcountcheck = "";
            var Amount = document.getElementById(control).value;


            if (Amount.length = 0) {
                return 0;
            }
            else {
            }


            if (!isNaN(Amount) && Amount != '') {
                document.getElementById(control).value = parseFloat(Amount);
                return 0;
            }
            else {
            }

            if (Amount.charAt(0) == ".") {
                Amount = "0" + Amount;
            }
            else {
            }

            charcountcheck = Amount.substring(0, Amount.length - 1)
            if (!isNaN(charcountcheck) && charcountcheck != '') {
                amountportion = parseFloat(charcountcheck)
                param = Amount.charAt(Amount.length - 1)
            }
            else {
                // alert("Please enter valid amount, as '1M' or '3B'");
            }

            if (isNaN(amountportion) && amountportion == '') {
                //alert("Please enter valid amount, as '1M' or '3B'");
            }
            else {
                if (param == "K" || param == "k") {
                    document.getElementById(control).value = FormatAmount(amountportion * 1000);
                }
                else if (param == "M" || param == "m") {
                    document.getElementById(control).value = FormatAmount(amountportion * 1000000);
                }
                else if (param == "B" || param == "b") {
                    document.getElementById(control).value = FormatAmount(amountportion * 1000000000);
                }
                else if (param == "T" || param == "t") {
                    document.getElementById(control).value = FormatAmount(amountportion * 1000);
                }
                else if (param == "L" || param == "l") {
                    document.getElementById(control).value = FormatAmount(amountportion * 100000);
                }
                else if (param == "P" || param == "p") {
                    document.getElementById(control).value = amountportion / 100;
                }
                else {
                    return 0;
                }
            }
        }



        function StopTimer(lblid, btnDeal) {
            try {
                document.getElementById(lblid).innerHTML = "";
            }
            catch (Error) {

            }


        }
        //Mohit on 15-Jan-2015
        function OnClientItemsRequestFailedHandler(sender, eventArgs) {
            eventArgs.set_cancel(true);
        }
    </script>

    <script type="text/javascript">
        //Rushikesh/Mohit/Dilkhush 14Jan2016 for share search
        function OnClientRequesting(sender, args) {
            var context = args.get_context()
            context["iMarketype"] = "EQ";
            context["iShareVAl"] = "";

            if ("<%=ddlExchange.Visible%>" == "True") {
                var seXchange = document.getElementById("<%=ddlExchange.clientID%>").value;
                context["sExchange"] = seXchange;
            }
            else {
                context["sExchange"] = "All";
            }
        }
    </script>

    <style type="text/css">
  #ctl00_MainContent_tabContainer_tabPanelELN_chkQuantoCcy
        {
            margin-left: 0px !important;
            }
        
             #ctl00_MainContent_tabContainer_tabPanelELN_chkELNType
        {
            margin-left: 0px !important;
            }   
        
        
        .txt[disabled='disabled'].txt
        {
            height:16px;
            }
        
        
        .ddl
        {
            border:none;
            }
        
        
        
         .RadioBtn label
        {
        	vertical-align:3px;
        }
        .ajax_tab_xp .ajax__tab_body
{

    border: 1px solid #d5d5d5 !important;
}

        #ctl00_MainContent_upnRedirect
        {width: 150px;
         display: inline;
        }
        #ctl00_MainContent_UpdateProgress1
        {
        	width:400px;
        	display: block;
        	position:absolute;
        	float:right;
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
            padding-left:29px;
            text-align:left;
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
        #ctl00_MainContent_tabContainer_body
        {
            border:1px solid #d5d5d5;
            }
 .grayBorder{
            border-color : #D5D5D5 !important;
            height:30px !important;
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
    <table cellspacing="0" cellpadding="1em" width="98%">
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" width="100%"  style="">
                    <tr>
                        <td style="height: 150px; width: 720px;padding-top: 0px;" align="left" valign="top">
                            <ajax:TabContainer ID="tabContainer" runat="server" ActiveTabIndex="0"
                                Style="vertical-align: top" AutoPostBack="false" OnClientActiveTabChanged="function(){UpdateTab();}">
                                <ajax:TabPanel ID="tabPanelELN" runat="server" TabIndex="0" Height="180px" BackColor="AliceBlue">
                                    <HeaderTemplate>
                                        &nbsp;ELN&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="upnl1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Panel ID="panelELN" runat="server">
                                                    <table cellspacing="1px" cellpadding="1px" style="width: 720px; height: 180px;">
                                                        <tr runat="server" id="tblRowSelectionExchangeELN" visible="false">
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="Exchange"></asp:Label>
                                                            </td>
                                                            <td colspan="4">
                                                                <asp:DropDownList ID="ddlExchange" runat="server" AutoPostBack="True"  
                                                                    Height="20px" Width="390px" TabIndex="1">
                                                                </asp:DropDownList>
                                                                <%--added by Mohit Lalwani on 15-Jan-2015--%>
                                                                <asp:HiddenField ID="hdnConfig_EQC_Allow_ALL_AsExchangeOption" Value="" runat="server" />
                                                                <%--/added by Mohit Lalwani on 15-Jan-2015--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td width="95px;">
                                                                <asp:Label ID="lblShareName" runat="server" CssClass="lbl" Text="Share"></asp:Label>
                                                            </td>
                                                            <td align="left" valign="top" colspan="5">

                                                                <script type="text/javascript">
                                                                    function ShowDropDownFunction() {
                                                                        var combo = $find("<%= ddlShare.ClientID %>");
                                                                        if (combo.get_text().length.toString() == '3')
                                                                            combo.showDropDown();
                                                                    }
                                                                    function HideDropDownFunction() {
                                                                        var combo = $find("<%= ddlShare.ClientID %>");
                                                                        combo.hideDropDown();
                                                                    }
                                                                
                                                                </script>

                                                                <%--   <telerik:RadComboBox ID="ddlShare" runat="server" Filter="Contains" MarkFirstMatch="true"
                                                                    ShowDropDownOnTextboxClick="true" EnableViewState="true" Style="width: 390px;
                                                                    height: 20px !important;" AutoPostBack="true" OnSelectedIndexChanged="ddlShare_SelectedIndexChanged"
                                                                    MinFilterLength="3" ForeColor="Black" ShowToggleImage="true" ToolTip="Search by inputing share name or symbol. Press Enter to select."
                                                                    EmptyMessage="" LoadingMessage="Loading matching shares..." TabIndex="1" OnClientKeyPressing="ShowDropDownFunction"
                                                                    MaxHeight="180" EnableLoadOnDemand="false" Font-Names="Arial">
                                                                    <CollapseAnimation Type="None" />
                                                                    <ExpandAnimation Type="None" />
                                                                </telerik:RadComboBox>--%>
                                                                <telerik:RadComboBox ID="ddlShare" runat="server" Width="250" Height="150" OnClientItemsRequesting="OnClientRequesting"
                                                                    EmptyMessage="" EnableLoadOnDemand="true" ShowMoreResultsBox="false" EnableVirtualScrolling="false"
                                                                    OnSelectedIndexChanged="ddlShare_SelectedIndexChanged" AutoPostBack="true" MarkFirstMatch="true"
                                                                    ShowDropDownOnTextboxClick="true" Style="width: 395px; height: 20px !important;"
                                                                    MinFilterLength="3" ForeColor="Black" ShowToggleImage="true" ToolTip="Search by inputing share name or symbol. Press Enter to select."
                                                                    LoadingMessage="Loading matching shares..." TabIndex="1" OnClientItemsRequestFailed="OnClientItemsRequestFailedHandler">
                                                                </telerik:RadComboBox>
                                                                <asp:Label ID="lblShare" runat="server" CssClass="lbl" Text="Share" Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="white-space: nowrap; width: 90px">
                                                                <asp:Label ID="Label6" runat="server" CssClass="lbl" Style="vertical-align: baseline;"
                                                                    Text="Ccy"></asp:Label>
                                                                    <asp:Label ID="lblELNBaseCcy" runat="server" CssClass="lbl" Width="30px" Style="vertical-align: baseline;"
                                                                   ></asp:Label>
                                                            </td>
                                                            <td>
                                                                 <asp:Label ID="lblPRR" runat="server" CssClass="lbl" Text="PRR"></asp:Label>
                                                                <asp:Label ID="lblPRRVal" runat="server" CssClass="lbl" Width="30px" ></asp:Label>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr style="height: 20px;" runat="server" id="tblRowDisplayExchangeELN">
                                                            <td>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblExchange" runat="server" CssClass="lbl" Text="Exchange"></asp:Label>
                                                            </td>
                                                            <td align="left" colspan="4">
                                                                <asp:Label ID="lblExchangeVal" runat="server" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <td align="left" valign="top">
                                                            </td>
                                                            <td style="width: 20px">
                                                             <asp:Label ID="lblAdvisoryFlag" runat="server" Text="Advisory Flag " Width="80px"
                                                        CssClass="lbl" ></asp:Label>
                                                            
                                                            </td>
                                                            <td>
                                                            <asp:Label ID="lblAdvisoryFlagVal" runat="server" Text=""  CssClass="lbl"></asp:Label>
                                                            
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSolveFor" runat="server" CssClass="lbl" Text="Solve For"></asp:Label>
                                                            </td>
                                                            <td>
                                                              <telerik:RadDropDownList ID="ddlSolveFor" runat="server" AutoPostBack="True"  
                                                                    Width="125px" TabIndex="3" >
                                                                    <Items>
                                                                    <telerik:DropDownListItem Text="IB Price (%)" Value="PricePercentage"/>
                                                                    <telerik:DropDownListItem Text="Strike (%)" Value="StrikePercentage"/>
                                                                    </Items>
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td style="width: 30px;">
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTenor" runat="server" CssClass="lbl" Text="Tenor"></asp:Label>
                                                            </td>
                                                            <td style="white-space: nowrap">
                                                                <asp:TextBox ID="txtTenor" runat="server" CssClass="txt" AutoPostBack="True" AutoCompleteType="BusinessStreetAddress"
                                                                    Width="52px" Style="text-align: right;" MaxLength="3" TabIndex="8">1</asp:TextBox>&nbsp;
                                                                      <telerik:RadDropDownList 
                                                                        ID="ddlTenorTypeELN" runat="server"   Width="82px" AutoPostBack="true"
                                                                        TabIndex="8"    >
                                                                       <Items>
                                                                    <telerik:DropDownListItem 
                                                                        Value="MONTH" Text="Month" Selected="True"/>
                                                                       <telerik:DropDownListItem  Value="YEAR" Text="Year"/></Items>
                                                                                      </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTradeDate" runat="server" CssClass="lbl" Text="Trade Date"></asp:Label>
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <uc1:DateControl ID="txtTradeDate" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                                    DoPostBack="true" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStrike" runat="server" CssClass="lbl" Text="Strike (%)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtStrike" runat="server" AutoPostBack="True" CssClass="txt" Style="text-align: right"
                                                                    Width="120px" TabIndex="4">98</asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblValueDays" runat="server" CssClass="lbl" Text="Settl. Weeks"></asp:Label>
                                                            </td>
                                                            <td style="white-space: nowrap">
                                                              <telerik:RadDropDownList ID="ddlSettlementDays" runat="server"   AutoPostBack="True"
                                                                    Width="54px" TabIndex="8"   >
                                                                    <Items>
                                                                    <telerik:DropDownListItem Text="1W" Value="1W"/>
                                                                    <telerik:DropDownListItem Text="2W"  Value="2W" Selected="True"/>
                                                                    </Items>
                                                                 </telerik:RadDropDownList>

                                                                <asp:TextBox ID="txtValueDays" runat="server" CssClass="txt" Width="45px" AutoPostBack="True" Enabled="False"
                                                                    Style="text-align: right; margin-left: 5px;" TabIndex="8" MaxLength="3">14</asp:TextBox><asp:Label ID="lblValDays"
                                                                        runat="server" CssClass="lbl" Font-Size="XX-Small" Text="Days" Width="43px" Style="padding: 2px;"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSettlementDate" runat="server" Text="Settl. Date" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc1:DateControl ID="txtSettlementDate" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                                    DoPostBack="true" style="position: relative" width="90px" TabIndex="11" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblELNPrice" runat="server" CssClass="lbl" Text="IB Price (%)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtELNPrice" runat="server" AutoPostBack="true" Enabled="false"  CssClass="txt" Style="text-align: right"
                                                                    Width="120px" TabIndex="5"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style='margin-left:0px !important;'>
                                                                <asp:CheckBox ID="chkQuantoCcy" runat="server" AutoPostBack="true" Checked="false"
                                                                    CssClass="lbl" Text="Note Ccy" TabIndex="10" style="margin-left:0px !important;" />
                                                            </td>
                                                            <td>
                                                                  <telerik:RadDropDownList ID="ddlQuantoCcy" runat="server" AutoPostBack="True"  
                                                                     Width="54px"   >
                                                              </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblExpiryDate" runat="server" Text="Expiry Date" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc1:DateControl ID="txtExpiryDate" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                                    DoPostBack="true" TabIndex="12" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <asp:Label ID="lblQuantity" runat="server" CssClass="lbl" Text="Notional"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtQuantity" runat="server" AutoPostBack="true" CssClass="txt" Style="text-align: right"
                                                                    Width="120px" TabIndex="6" MaxLength="14">300,000</asp:TextBox> <%--<Nikhil M. Changed on 02-Sep-2016: FSD Default > 1,000,000</Nikhil>--%>
                                                                <%--EQBOSDEV-228 Remove .00 by chaitali on 21-Jan-16--%>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="white-space: nowrap">
                                                                <asp:CheckBox ID="chkELNType" runat="server" AutoPostBack="true" Checked="false"
                                                                    CssClass="lbl" Text="KO % Of Initial" TabIndex="10" style='margin-left:0px;' />
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <asp:TextBox ID="txtBarrier" runat="server" AutoPostBack="True" CssClass="txt" Style="text-align: right;padding-left:0px;padding-right:0px;"
                                                                    Width="50px" MaxLength="6">110.00</asp:TextBox>
                                                                <telerik:RadDropDownList ID="ddlBarrier" runat="server" AutoPostBack="True"  
                                                                    TabIndex="11" Width="82px" style="margin-left:5px;">
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMaturityDate" runat="server" Text="Mat. Date" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc1:DateControl ID="txtMaturityDate" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                                    DoPostBack="true" TabIndex="13" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="Upfront (%)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUpfrontELN" runat="server" AutoPostBack="true" CssClass="txt"
                                                                    Style="text-align: right" Width="120px" TabIndex="7" MaxLength="6">0.50</asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblKOWatchDate" runat="server" CssClass="lbl" Text="KO Watch Date" Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <uc1:DateControl ID="dtpKOWatch" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                                    DoPostBack="true" DataFormatString="{0:dd-MMM-yyyy}" Disabled = "true" Visible="false"/>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <asp:Label ID="lblDays" runat="server" CssClass="lbl" Text="No. of Days"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDaysVal" runat="server" CssClass="lbl" Text=""></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="lblClientYieldToIB" runat="server" Visible="false"  CssClass="lbl" Text="Client Yield(%)"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtClientYieldToIB" runat="server"  Visible="false"  Style="text-align: right" Width="120px" AutoPostBack="true" CssClass="txt" ></asp:TextBox>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelFCN" runat="server" TabIndex="1" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;FCN&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="text-align: center; margin-top: 40px; height: 125px;">
                                            <img src="../App_Resources/ajax-loader7.gif" id="Img1" width="50px" height="50px"
                                                alt="x" />
                                            <div style="text-align: center;">
                                                Loading...</div>
                                        </div>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelDRA" runat="server" TabIndex="2" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;DRA&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="text-align: center; margin-top: 40px; height: 125px;">
                                            <img src="../App_Resources/ajax-loader7.gif" id="Img3" width="50px" height="50px"
                                                alt="x" />
                                            <div style="text-align: center;">
                                                Loading...</div>
                                        </div>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelAQDQ" runat="server" TabIndex="3" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;Accu&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="text-align: center; margin-top: 40px; height: 125px;">
                                            <img src="../App_Resources/ajax-loader7.gif" id="Img2" width="50px" height="50px"
                                                alt="x" />
                                            <div style="text-align: center;">
                                                Loading...</div>
                                        </div>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelDQ" runat="server" TabIndex="4" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;Decu&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="text-align: center; margin-top: 40px; height: 125px;">
                                            <img src="../App_Resources/ajax-loader7.gif" id="Img2" width="50px" height="50px"
                                                alt="x" />
                                            <div style="text-align: center;">
                                                Loading...</div>
                                        </div>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelEQO" runat="server" TabIndex="5" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;Options&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="text-align: center; margin-top: 40px; height: 125px;">
                                            <img src="../App_Resources/ajax-loader7.gif" id="Img4" width="50px" height="50px"
                                                alt="x" />
                                            <div style="text-align: center;">
                                                Loading...</div>
                                        </div>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                            </ajax:TabContainer>
                        </td>
                        <td colspan="3" align="left" valign="top" class="Filter" style="border-top-width: 0px;
                            border-left-width: 0px; border-bottom-width: 1px;border-right-width:1px; " runat="server" id="tdShareGraphData">
                            <asp:UpdatePanel ID="updShareGraphData" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table cellpadding="0px" cellspacing="0px" style="margin-left: 0px; width: 100%">
                                        <tr>
                                            <td style="vertical-align: top;padding-top:0px;" align="left" valign="top">
                                                <div class="ajax__tab_header"  >
                                                    <table cellpadding="0px" cellspacing="0px" style="border: none; float: left">
                                                        <tr>
                                                            <td>
                                                                <div id="div_RM_Limit" runat="server" visible="false" style="background-color: #336699;
                                                                    color: White; padding: 5px; width: 147px; text-align: center">
                                                                    <asp:Label runat="server" ID="txt_RM_Limit" Text="Max User Limit: " ToolTip="User Limit-"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td id="tdrblShareData" runat="server" style="white-space: nowrap">
                                                                <asp:RadioButtonList ID="rblShareData" runat="server" RepeatDirection="Horizontal"
                                                                    AutoPostBack="true"  CssClass="RadioBtn">
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
                                                                <telerik:RadDropDownList  ID="ddlRFQRM" runat="server"   Width="200" Height="20px"
                                                                    AutoPostBack="true"     ></telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEntity" runat="server" Text="Entity" Height="22px" CssClass="lbl"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlentity" runat="server"   Width="120px" Height="20px"
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
                                                                <telerik:RadDropDownList ID="ddlAccount" runat="server"   Width="80px" Height="20px"
                                                                    AutoPostBack="false" Visible="false">
                                                                    <Items>
                                                                    <telerik:DropDownListItem Text="SG A/C" Value="1" />
                                                                    <telerik:DropDownListItem  Text="HK A/C" Value="2" />
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
                                        <tr>
                                            <td class="Filter" style="border:none;border-top: 1px solid rgb(213, 213, 213);border-right: 0px solid rgb(213, 213, 213); border-image: none;" align="left" valign="top">
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
                                                                                                <asp:Label ID="lblStockDesc" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                                   "></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr title="Refreshed frequently as compared to rest of data.">
                                                                                            <td style="width: 100px;background-color:#F5FAFD">
                                                                                                <span class='fieldInfo' style="background-color:#FFF" onclick='showFieldInfo(ctl00_MainContent_tabCntrShareRpt_tabShare1_lblSpot,spotInfo);'>
                                                                                                    !</span> <span style='visibility: hidden; width: 0px; float: left; display: none;'
                                                                                                        id='spotInfo'><b>Value: </b>Instrument's last price for the trading day.<br />
                                                                                                        <b>Date: </b>Date of the instrument's last updated close price.</span>
                                                                                                <asp:Label ID="lblSpot" runat="server" CssClass="lbl" Style="float: left;color:#919191;" Text="Spot(d)"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 90px;background-color:#F5FAFD">
                                                                                                <asp:Label ID="lblSpotDate" runat="server" CssClass="lbl"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 80px; text-align: right;background-color:#F5FAFD ">
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
                                                                                                <asp:Label ID="lbl12TDivYield" runat="server" CssClass="lbl ColorLightGray" Text="12T Div Yield Indicative"></asp:Label>
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
                                                                                                            runat="server" CssClass="lbl ColorLightGray" Text="52 Wk High"></asp:Label>
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
                                                                                                <asp:Label ID="lblPrevEarnDate" runat="server" CssClass="lbl ColorLightGray" Text="Prev. Earn Date"></asp:Label>
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
                                                                                                            runat="server" CssClass="lbl ColorLightGray" Text="52 Wk Low"></asp:Label>
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
                                                                                                <asp:Label ID="lblPrevEPS" runat="server" CssClass="lbl ColorLightGray" Text="Prev. EPS"></asp:Label>
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
                                                                                                <asp:Label ID="lblYTDChng" runat="server" CssClass="lbl ColorLightGray" Text="YTD Chng %"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td style="text-align: right">
                                                                                                <asp:Label ID="lblYTDChngValue" runat="server" CssClass="lbl"></asp:Label>
                                                                                            </td>
                                                                                            <td rowspan="4" colspan="3">
                                                                                                <table style="width: 100%;" cellpadding="0">
                                                                                                    <tr>
                                                                                                        <td colspan="2" style="border-bottom: solid 1px #d5d5d5; text-align: center;
                                                                                                            height: 12px !important">
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
                                                                                                            <asp:Label ID="lbl20DHistVol" runat="server" CssClass="lbl ColorLightGray" Text="20D"></asp:Label>
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
                                                                                                            <asp:Label ID="lbl60DHistVol" runat="server" CssClass="lbl ColorLightGray" Text="60D"></asp:Label>
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
                                                                                                            <asp:Label ID="lbl250DHistVol" runat="server" CssClass="lbl ColorLightGray" Text="250D"></asp:Label>
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
                                                                                                <asp:Label ID="lblMTDChng" runat="server" CssClass="lbl ColorLightGray" Text="MTD Chng %"></asp:Label>
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
                                                                                                <asp:Label ID="lbl1YearChng" runat="server" CssClass="lbl ColorLightGray" Text="1 Year Chng %"></asp:Label>
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
                                                                                                <asp:Label ID="lblTrailing12MPE" runat="server" CssClass="lbl ColorLightGray" Text="Trailing 12M P/E"></asp:Label>
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
                                                                                            <td colspan="8" style="border-top: solid 1px #d5d5d5; width: 100%;">
                                                                                                <asp:Label ID="lblAsOfCaption" runat="server" CssClass="lbl" Style="font-size: 10px !important;
                                                                                                    color: #00ADEF; float: left;Padding-left:13px;">*As of&nbsp;</asp:Label>
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
                                                                    </ajax:TabContainer>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="rowELNCalculator" visible="false">
                                                        <td>
                                                            <asp:UpdatePanel ID="upnlELNCal" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div style="width: 530px; border-bottom: solid 1px #d5d5d5; padding: 3px 0px 1px 2px">
                                                                        <asp:Label ID="lblCal" runat="server" Text="CALCULATOR : ELN"></asp:Label></div>
                                                                    <table style="width: 350px;" cellspacing="3" cellpadding="2">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblUnderlyingPrice" runat="server" Text="Underlying Price"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <input id="txtUnderlyingPrice" type='text' runat="server" text="" style="text-align: right"
                                                                                    maxlength="6" onkeypress="Javascript:AllowNumericWithdecimal(this.id);" onchange="calStrikePriceForELN()"
                                                                                    class="txt" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblStrikePer" runat="server" Text="Strike (%)"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <input id="txtStrikePer" type='text' runat="server" text="" style="text-align: right"
                                                                                    maxlength="6" onkeypress="Javascript:AllowNumericWithdecimal(this.id);" onchange="calStrikePriceForELN()"
                                                                                    class="txt" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblStrikePrice" runat="server" Text="Strike Price"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <input id="txtStrikePrice" type='text' runat="server" text="" style="text-align: right"
                                                                                    disabled class="txt" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblNotionalAmount" runat="server" Text="Notional Amount"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <input id="txtNotionalAmount" type='text' runat="server" text="" style="text-align: right"
                                                                                    onkeypress="Javascript:KeysAllowedForNotional();" onchange="ConvertFormattedAmountToNumber(this.id);calNoOfShareELN()"
                                                                                    class="txt" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblNoShares" runat="server" Text="Number Of Shares"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <input id="txtNoShares" type='text' runat="server" text="" style="text-align: right"
                                                                                    onkeypress="AllowOnlyNumeric();" onchange="ConvertFormattedAmountToNumber(this.id);calNotionalAmountELN()"
                                                                                    class="txt" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnCalculate" type="submit" runat="server" value="Calculate" class="btn" />
                                                                                &nbsp;&nbsp;<input id="btnClear" type="submit" runat="server" value="Clear" onclick="clearELNCalData()"
                                                                                    class="btn" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
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
                    <tr runat="server" id="trSaveSetting">
                    <td >
                     <asp:Button ID="btnSaveSettings" runat="server"  Text="Save Settings" CssClass="btn" onmouseover="JavaScript:alert:this.focus();" /><asp:Label ID="lblError_DefaultSettings" runat="server" CssClass="lbl" ForeColor="red" Style="width: auto;"></asp:Label>
                    </td>
                    <td colspan="3">
                    
                    </td>
                    </tr>
                    <tr>
                        <td class="Filter" align="left" valign="top" style="width: 720px; border-top-width: 0px;" runat="server" id="tdpnlReprice" >
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
                                                                    <td style="height: 48px; line-height: 48px;">
                                                                        <asp:Button ID="btnCancelReq" runat="server" Width="100%" Text="Reset" CssClass="btn"
                                                                            onmouseover="JavaScript:alert:this.focus();" />
                                                                    </td>
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
                                                                       <%-- <asp:Label ID="lblTimerAll" runat="server" Text="" CssClass="lblBOLD" Style="vertical-align: middle;
                                                                            text-align: center;"> </asp:Label>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr><td></td></tr>
                                                                <tr>
                                                                    <td valign="top" align="center" style=" width: 105px !important;">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="PriceAllWait" width="20px" height="20px"
                                                                                style="visibility: hidden;" alt="x" />
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
                                                                <tr> <%--''<Nikhil M. on 17-Sep-2016: remove onmouseout ="hideLPBoxes()" ></Nikhil>--%> 
                                                                    <td valign="top" align="center" style="height: 48px; line-height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkHSBC" runat="server" AutoPostBack="true"   /> <%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
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
                                                                        <asp:Button ID="btnHSBCDoc" Text="Document" runat="server" CssClass="cssDocLink" Visible="false" />
                                                                      <%-- <asp:ImageButton ID="btnHSBCDocI" ImageUrl="~/App_Themes/images/doc.png" Visible="true" Width="50px" Height="50px" />--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="HSBCwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" alt="x" />
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
                                                                <tr ><%--''<Nikhil M. on 17-Sep-2016: remove onmouseout ="hideLPBoxes()"></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                         <asp:CheckBox ID="chkUBS" runat="server" AutoPostBack="true"  /> <%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
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
                                                                    <td valign="middle" align="center" style="height: 25px" >
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
                                                                        <asp:Button ID="btnUBSDoc" Text="Document" runat="server" CssClass="cssDocLink" Visible="false"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="UBSwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" alt="x" />
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
                                                                    <td valign="bottom" align="center" >
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
                                                                <tr ><%--''<Nikhil M. on 17-Sep-2016: Remove "onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;"  >
                                                                      <asp:CheckBox ID="chkJPM" runat="server" AutoPostBack="true"   /> <%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblJPM" runat="server" CssClass="lbl BestLP" Text="JPM" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="height: 25px;" valign="middle" >
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
                                                                        <asp:Button ID="btnJPMDoc" Text="Document" runat="server" CssClass="cssDocLink" Visible="false"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="JPMwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" alt="x" />
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
                                                                <tr><%--''<Nikhil M. on 17-Sep-2016: Remove "onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;"  >
                                                                    <asp:CheckBox ID="chkBNPP" runat="server" AutoPostBack="true"  /> <%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblBNPP" runat="server" CssClass="lbl BestLP" Text="BNPP" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" align="center" valign="middle" >
                                                                        <asp:Label ID="lblBNPPPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblBNPPClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblBNPPClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblTimerBNPP" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnBNPPDoc" Text="Document" runat="server" CssClass="cssDocLink" Visible="false"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center" >
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
                                                                    <td valign="bottom" align="center" >
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
                                                                <tr><%--''<Nikhil M. on 17-Sep-2016: Remove "onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle !important;">
                                                                    
                                                                    <asp:CheckBox ID="chkCS"  runat="server" AutoPostBack="true"  Style="" /> <%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
                                                                    
                                                                        <asp:Label ID="lblCS" runat="server" CssClass="lbl BestLP" Text="CS" Style="font-size: 14px !important;"></asp:Label> <%-- Changed by AshwiniP on 05-Oct-2016 (Credit Suisse to CS)--%>
                                                                    
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" valign="middle" >
                                                                        <asp:Label ID="lblCSPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;" >
                                                                        <asp:Label ID="lblCSClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;" >
                                                                        <asp:Label ID="lblCSClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;" >
                                                                        <asp:Label ID="lblTimerCS" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnCSDoc" Text="Document" runat="server" CssClass="cssDocLink" Visible="false"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center" >
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
                                                                    <td valign="bottom" align="center" >
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
                                                                <tr><%--''<Nikhil M. on 17-Sep-2016: Remove "onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                    <asp:CheckBox ID="chkBAML"  runat="server" AutoPostBack="true"  /><%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblBAML" runat="server" CssClass="lbl BestLP" Text="BAML" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr >
                                                                    <td style="height: 25px;" valign="middle" >
                                                                        <asp:Label ID="lblBAMLPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" > 
                                                                        <asp:Label ID="lblBAMLClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblBAMLClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr> 
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblTimerBAML" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnBAMLDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center" >
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
                                                                    <td valign="bottom" align="center"  >
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
                                                            <table id="TRDBIB" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr><%--''<Nikhil M. on 17-Sep-2016: Remove "onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle !important;">
                                                                      <asp:CheckBox ID="chkDBIB"  runat="server" AutoPostBack="true"    /><%--''<Nikhil M. on 17-Sep-2016: ></Nikhil>--%>
                                                                        <asp:Label ID="lblDBIB" runat="server" CssClass="lbl BestLP" Text="DB" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;" valign="middle" >
                                                                        <asp:Label ID="lblDBIBPrice" runat="server" Font-Bold="true" CssClass="lbl LPPrice"
                                                                            Text="" ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;" >
                                                                        <asp:Label ID="lblDBIBClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;" >
                                                                        <asp:Label ID="lblDBIBClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px;" >
                                                                        <asp:Label ID="lblTimerDBIB" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnDBIBDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center" >
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="DBIBwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" />
                                                                        </div>
                                                                        <asp:Button ID="btnDBIBPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnDBIBPrice_Click" CssClass="btn" Width="45px" />
                                                                        <asp:Button ID="btnDBIBDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" Enabled="false" OnClick="btnDBIBDeal_Click" CssClass="btn" ToolTip="Deal"
                                                                            Width="45px" Visible="false" />
                                                                        <asp:HiddenField ID="DBIBHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="DBIBHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center" >
                                                                        <div style="width: 100%; height: 20px; z-index: 1000px; margin-top: 5px">
                                                                            <asp:Label ID="lblDBIBLimit" Text="" runat="server">
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
                                                                <tr><%--''<Nikhil M. on 17-Sep-2016: Remove "onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                    <asp:CheckBox ID="chkOCBC"  runat="server" AutoPostBack="true"    /><%--''<Nikhil M. on 17-Sep-2016:Commented ></Nikhil>--%>
                                                                        <asp:Label ID="lblOCBC" runat="server" CssClass="lbl BestLP" Text="OCBC" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="height: 25px;" valign="middle" >
                                                                        <asp:Label ID="lblOCBCPrice" runat="server" CssClass="lbl LPPrice" Text="" Font-Bold="true"
                                                                            ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblOCBCClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblOCBCClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblTimerOCBC" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                    <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnOCBCDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center" >
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="OCBCwait" width="20px" height="20px"
                                                                                style="visibility: hidden;"  alt="x" />
                                                                        </div>
                                                                        <asp:Button ID="btnOCBCprice" name="btnOCBCprice" onmouseover="JavaScript:alert:this.focus();"
                                                                            runat="server" CssClass="btn" Text="Price" Width="45px" OnClick="btnOCBCprice_Click" />
                                                                        <asp:Button ID="btnOCBCDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            CssClass="btn" Text="Order" OnClick="btnOCBCDeal_Click" Enabled="false" ToolTip="Deal"
                                                                            Width="45px" Visible="false" />
                                                                        <asp:HiddenField ID="OCBCHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="OCBCHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center" >
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
                                                                <tr><%--''<Nikhil M. on 17-Sep-2016: Remove "onmouseout ="hideLPBoxes()"" ></Nikhil>--%>
                                                                    <td valign="top" align="center" style="height: 48px; vertical-align: middle;">
                                                                        <asp:CheckBox ID="chkCITI" runat="server" AutoPostBack="true" /> <%--''<Nikhil M. on 17-Sep-2016: Remopve "checked" ></Nikhil>--%>
                                                                        <asp:Label ID="lblCITI" runat="server" CssClass="lbl BestLP" Text="CITI" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="height: 25px;" valign="middle" >
                                                                        <asp:Label ID="lblCITIPrice" runat="server" CssClass="lbl LPPrice" Text="" Font-Bold="true"
                                                                            ForeColor="Green"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblCITIClientPrice" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblCITIClientYield" runat="server" Text="0.0"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="middle" align="center" style="height: 25px" >
                                                                        <asp:Label ID="lblTimerCITI" runat="server" CssClass="lblBOLD"></asp:Label>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                    <tr>
                                                                    <td valign="middle" align="center">
                                                                        <asp:Button ID="btnCITIDoc" Text="Document" Visible="false" runat="server" CssClass="cssDocLink" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center" >
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="CITIwait" width="20px" height="20px"
                                                                                style="visibility: hidden;"  alt="x" />
                                                                        </div>
                                                                        <asp:Button ID="btnCITIprice" name="btnCITIprice" onmouseover="JavaScript:alert:this.focus();"
                                                                            runat="server" CssClass="btn" Text="Price" Width="45px"/>
                                                                        <asp:Button ID="btnCITIDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            CssClass="btn" Text="Order" Enabled="false" ToolTip="Deal"
                                                                            Width="45px" Visible="false" />
                                                                        <asp:HiddenField ID="CITIHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="CITIHiddenAccDays" Value="" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="bottom" align="center" >
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
                                                                    <asp:CheckBox ID="chkLEONTEQ"  AutoPostBack="true" runat="server"/>
                                                                        <asp:Label ID="lblLEONTEQ" runat="server" CssClass="lbl BestLP" Text="Leonteq" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="height: 25px;" valign="middle">
                                                                        <asp:Label ID="lblLEONTEQPrice" runat="server" CssClass="lbl LPPrice" Text="" Font-Bold="true"
                                                                            ForeColor="Green"></asp:Label>
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
                                                                        &nbsp;
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
                                                                                style="visibility: hidden;"  alt="x" />
                                                                        </div>
                                                                        <asp:Button ID="btnLEONTEQprice" name="btnLEONTEQprice" onmouseover="JavaScript:alert:this.focus();"
                                                                            runat="server" CssClass="btn" Text="Price" Width="45px"/>
                                                                        <asp:Button ID="btnLEONTEQDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            CssClass="btn" Text="Order" Enabled="false" ToolTip="Deal"
                                                                            Width="45px" Visible="false" />
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
                                                                    <asp:CheckBox ID="chkCOMMERZ"  AutoPostBack="true" runat="server"/>
                                                                        <asp:Label ID="lblCOMMERZ" runat="server" CssClass="lbl BestLP" Text="Commerz" Style="font-size: 14px !important;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="height: 25px;" valign="middle">
                                                                        <asp:Label ID="lblCOMMERZPrice" runat="server" CssClass="lbl LPPrice" Text="" Font-Bold="true"
                                                                            ForeColor="Green"></asp:Label>
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
                                                                        &nbsp;
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
                                                                                style="visibility: hidden;"  alt="x" />
                                                                        </div>
                                                                        <asp:Button ID="btnCOMMERZprice" name="btnCOMMERZprice" onmouseover="JavaScript:alert:this.focus();"
                                                                            runat="server" CssClass="btn" Text="Price" Width="45px"/>
                                                                        <asp:Button ID="btnCOMMERZDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            CssClass="btn" Text="Order" Enabled="false" ToolTip="Deal"
                                                                            Width="45px" Visible="false" />
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
                                      <asp:HiddenField ID="hdnBestIssuer" Value="" runat="server" />
                                     <asp:HiddenField ID ="hdnBestProvider" Value ="" runat ="server" />
                                      <asp:HiddenField ID="hdnstrBestClientYeild" Value="" runat="server" /> 
                                      <asp:HiddenField ID="hdnSettDateManualChangeYN" Value="" runat="server" /> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" valign="top" class="Filter" style="border-top-width: 0px; border-left-width: 0px;padding-left:0px;" runat="server"  id="tdCommentry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="0" align="left" style="text-align: left; margin-top: -2px;">
                                        <asp:UpdatePanel runat="server" ID="upnlCommentry" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="left" style="text-align: left;">
                                                            <asp:Label ID="lblComentry" runat="server" Text="" CssClass="lbl"></asp:Label>
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
                                   <asp:ImageButton ID="addPPimg" ImageUrl="../App_Themes/images/add.png" runat="server" Height="17px" style="float:left; margin-left:5px;" Visible="false" />
                                       <div id="maillistDiv"  >
                                            <asp:CheckBoxList id="chkPPmaillist" runat="server" RepeatDirection="Horizontal"  visible="false">
                                                <asp:ListItem Text="HSBC" Value="HSBC" />
                                                <asp:ListItem Text="UBS" Value="UBS" />
                                                <asp:ListItem Text="JPM" Value="JPM" />
                                                <asp:ListItem Text="BNPP" Value="BNPP" />
                                                <asp:ListItem Text="Credit Suisse" Value="CS" />
                                                <asp:ListItem Text="CITI" Value="CITI" />
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                        <asp:UpdatePanel ID="upnQTEmail" runat="server">
                                            <ContentTemplate>
                                            <table >
                                            <tr><td>
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
                                            </td></tr>
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
                                    <td colspan="2" class="lbl" align="left" style="text-align: left; padding: 2px 0px 0px 26px !important">
                                        <div style="vertical-align: top; width: auto; float: left; text-align: left; vertical-align: bottom;">
                                            <asp:UpdatePanel ID="upnlMsg" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblerror" runat="server" CssClass="lbl" ForeColor="red" Style="width: auto;"></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="lblMsgPriceProvider" runat="server" CssClass="lbl" ForeColor="Red"
                                                        Style="width: auto;padding-left:3px;"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 13px;">
                                    </td>
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
                        <table style="width: 100%" cellpadding="0" cellspacing="0" runat="server"
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
                                                 <telerik:RadDropDownList ID="ddlSelfGrp" runat="server"   Width="100px">
                                                          <Items>
                                                    <telerik:DropDownListItem Text="Self" Value="Self" Selected="True"/>
                                                    <telerik:DropDownListItem Text="Group" Value="Group"/>
                                                    <telerik:DropDownListItem Text="All" Value="All"/>
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
                                            <td >
                                                <asp:RadioButtonList ID="rbHistory" runat="server" RepeatDirection="horizontal" CssClass="RadioBtn"
                                                    AutoPostBack="True" style="display:inline !important;vertical-align:super">
                                                    <asp:ListItem Value="Quote History" Selected="True">Quote History</asp:ListItem>
                                                    <asp:ListItem Value="Order History">Order History</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td >
                                                <asp:CheckBox ID="chkExpandAllRFQ" Text ="Expand All Quote RFQ"  CssClass="mobilesubtitle"  runat="server" AutoPostBack="true" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td >
                                                <asp:Label ID="lblTotalRows" runat="server" CssClass="lbl" Text="Max Records To Fetch: " style="vertical-align :1px"></asp:Label>
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
                                            CssClass="cssbtnEMLMail" Style="visibility:hidden" Visible="true" />        <%--<RiddhiS. on 09-Oct-2016: Visible set to false as KYIR is opened on Document open link >   --%>
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
                                        <asp:DataGrid ID="grdELNRFQ" runat="server" CssClass="Grid draggable" PageSize="10"
                                            AllowSorting="true" AutoGenerateColumns="False" AllowPaging="true" GridLines="None">
                                            <FooterStyle CssClass="GridFooter "></FooterStyle>
                                            <ItemStyle CssClass="GridItem  " />
                                            <SelectedItemStyle CssClass="GridItemSelect" />
                                           <%-- <AlternatingItemStyle CssClass="AlternatItemStyle "   />--%>
                                            <HeaderStyle CssClass="GridHeaderTitle " />
                                            <PagerStyle CssClass="GridPager " />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="RFQ ID" SortExpression="ER_QuoteRequestId" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkRFQID" runat="server" CommandName="Select" Text='<%# Bind("ER_QuoteRequestId") %>'
                                                            CausesValidation="False" Font-Underline="true" ForeColor="#1580b2">ER_QuoteRequestId</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Create Pool" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="GridRightBorder" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCreatePool" runat="server" Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #cccccc" CommandName="CREATEPOOLELN" Text='Create Pool' CausesValidation="False"
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            ToolTip="Click to create pool for this RFQ."  CssClass="grdPushBtn" ></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="RFQ Details" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="GridRightBorder" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnRFQ_Details" runat="server" Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #cccccc"  CssClass="grdPushBtn" CommandName="GetRFQDetails" Text='RFQ Details' CausesValidation="False"
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            ToolTip="Click to view RFQ Details."></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Generate Document" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="upnlPrdGridDoc" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                            <input type="button" ID="btnGenerateDoc" style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C !important; border-width: 1px !important; font-size: 12px; border-style: solid; 
                                                            border-color: #cccccc"  value='Generate Document' 
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            title="Click to generate document." class="grdPushBtn" onclick="document.getElementById('ctl00_MainContent_KYIR').click();" />
                                            <%--<asp:Button ID="btnGenerateDoc" runat="server" Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C !important; border-width: 1px !important; font-size: 12px; border-style: solid; 
                                                            border-color: #cccccc" CommandName="GENERATEDOCUMENT" Text='Generate Document' CausesValidation="False"
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            ToolTip="Click to generate document." CssClass="grdPushBtn" OnClientClick="document.getElementById('ctl00_MainContent_KYIR').click(); return false;"></asp:Button>--%>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn HeaderText="Solve For" DataField="ER_SolveFor" SortExpression="ER_SolveFor"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Provider" DataField="PP_CODE" SortExpression="PP_CODE"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Price(%)" DataField="EP_OfferPrice" SortExpression="EP_OfferPrice"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Type" DataField="ER_Type" SortExpression="ER_Type" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Share" DataField="ER_UnderlyingCode" SortExpression="ER_UnderlyingCode"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Strike(%)" DataField="EP_StrikePercentage" SortExpression="EP_StrikePercentage"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Tenor (mths)" DataField="ER_Tenor" SortExpression="ER_Tenor"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="RFQ Tenor" DataField="ER_RFQTenor" SortExpression="ER_RFQTenor"
                                                    Visible="false" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ccy" DataField="ER_CashCurrency" SortExpression="ER_CashCurrency"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order Qty" DataField="ER_CashOrderQuantity" SortExpression="ER_CashOrderQuantity"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Trade Date" DataField="TradeDate" SortExpression="TradeDate"
                                                    DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Settl. Date" DataField="SettlmentDate" SortExpression="SettlmentDate"
                                                    DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Expiry Date" DataField="ExpiryDate" SortExpression="ExpiryDate"
                                                    DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Mat. Date" DataField="MaturityDate" SortExpression="MaturityDate"
                                                    DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Exchange" DataField="ER_Exchange" SortExpression="ER_Exchange"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Value" DataField="ER_Issuer_Date_Offset" SortExpression="ER_Issuer_Date_Offset"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Barr.Type" DataField="ER_BarrierMonitoringMode" SortExpression="ER_BarrierMonitoringMode"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KO % of init." DataField="ER_BarrierPercentage" SortExpression="ER_BarrierPercentage"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Issuer Remark" DataField="EP_Quote_Request_Rejection_Reason"
                                                    SortExpression="EP_Quote_Request_Rejection_Reason" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ext. RFQ ID" DataField="EP_ExternalQuoteId" SortExpression="EP_ExternalQuoteId"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Quote Requested At" DataField="ER_TransactionTime" SortExpression="ER_TransactionTime"
                                                    DataFormatString="{0:dd-MMM-yyyy hh:mm:ss tt}" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Common RFQ Id" DataField="ClubbingRFQId" SortExpression="ClubbingRFQId"
                                                    HeaderStyle-ForeColor="Black" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Created By" DataField="ER_Created_By" SortExpression="created_by"
                                                    HeaderStyle-ForeColor="Black" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Upfront" DataField="ER_Upfront" SortExpression="ER_Upfront"
                                                    HeaderStyle-ForeColor="Black" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Quote Status" DataField="Quote_Status" SortExpression="Quote_Status"
                                                    HeaderStyle-ForeColor="Black" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Client Price" DataField="EP_Client_Price" SortExpression="EP_Client_Price"
                                                    HeaderStyle-ForeColor="Black" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Client Yield" DataField="EP_Client_Yield" SortExpression="EP_Client_Yield"
                                                    HeaderStyle-ForeColor="Black" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
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
                                        <asp:DataGrid ID="grdOrder" runat="server" CssClass="Grid draggable" PageSize="10"
                                            AllowSorting="true" AutoGenerateColumns="false" AllowPaging="true" GridLines="None">
                                            <ItemStyle CssClass="GridItem  " />
                                            <SelectedItemStyle CssClass="GridItemSelect" />
                                            <AlternatingItemStyle CssClass="AlternatItemStyle " />
                                            <HeaderStyle CssClass="GridHeaderTitle " />
                                            <PagerStyle CssClass="GridPager " />
                                            <Columns>
                                                <asp:BoundColumn HeaderText="RFQ ID" DataField="ER_QuoteRequestId" SortExpression="ER_QuoteRequestId"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ext. Order ID" DataField="Order_ID" SortExpression="Order_ID"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order ID" DataField="EP_InternalOrderID" SortExpression="EP_InternalOrderID"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Order Details" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  CssClass="GridRightBorder" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnOrder_Details"  CssClass="grdPushBtn" runat="server" Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #cccccc" CommandName="GetOrderDetails" Text='Order Details' CausesValidation="False"
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            ToolTip="Click to view Order Details."></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Generate Document" HeaderStyle-ForeColor="White" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGenerateOrderDoc" runat="server" Style="background-image: none; background-color: white !important;
                                                            color: black !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #ddd" CommandName="GENERATEDOCUMENT" Text='Generate Document' CausesValidation="False"
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            ToolTip="Click to generate document."></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn HeaderText="RM Name" DataField="ER_RMName" SortExpression="ER_RMName"
                                                    HeaderStyle-ForeColor="Black">                                                      <%--Short Expression Changed By Mohit Lalwani from EP_RMName to ER_RMName on 1-Apr-2016 Jira:EQBOSDEV-309 --%>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Provider" DataField="PP_CODE" SortExpression="PP_CODE"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <%--<asp:BoundColumn HeaderText="Order Status" DataField="Order_Status" SortExpression="Order_Status"
                                                    HeaderStyle-ForeColor="Black" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>--%>
                                                <asp:BoundColumn HeaderText="Order Status" DataField="Field_DisplayAliasName" SortExpression="Order_Status"
                                                    HeaderStyle-ForeColor="Black" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order Type" DataField="ELN_Order_Type" SortExpression="ELN_Order_Type"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Limit Prc1" DataField="LimitPrice1" SortExpression="LimitPrice1"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Limit Prc2" DataField="LimitPrice2" SortExpression="LimitPrice2"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Limit Prc3" DataField="LimitPrice3" SortExpression="LimitPrice3"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Exec. Prc1" DataField="EP_Execution_Price1" SortExpression="EP_Execution_Price1"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Exec. Prc2" DataField="EP_Execution_Price2" SortExpression="EP_Execution_Price2"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Exec. Prc3" DataField="EP_Execution_Price3" SortExpression="EP_Execution_Price3"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Avg Exec. Prc" DataField="EP_AveragePrice" SortExpression="EP_AveragePrice"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ordered Qty" DataField="Ordered_Qty" SortExpression="Ordered_Qty"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Filled Qty" DataField="Filled_Qty" SortExpression="Filled_Qty"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Tenor (mths)" DataField="ER_Tenor" SortExpression="ER_Tenor"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Share" DataField="ER_UnderlyingCode" SortExpression="ER_UnderlyingCode"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ccy" DataField="ER_CashCurrency" SortExpression="ER_CashCurrency"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Leverage" DataField="ER_LeverageRatio" SortExpression="ER_LeverageRatio">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Guarantee" DataField="ER_GuaranteedDuration" SortExpression="ER_GuaranteedDuration"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Strike(%)" DataField="EP_StrikePercentage" SortExpression="EP_StrikePercentage"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KO(%)" DataField="EP_KO" SortExpression="EP_KO" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Coupon(%)" DataField="EP_CouponPercentage" SortExpression="EP_CouponPercentage"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Price(%)" DataField="EP_OfferPrice" SortExpression="EP_OfferPrice"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Upfront(%)" DataField="EP_RM_Margin" SortExpression="EP_RM_Margin">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Upfront(%)" DataField="EP_RM_Margin" SortExpression="EP_Upfront"
                                                    HeaderStyle-ForeColor="Black">
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
                                                <asp:BoundColumn HeaderText="Notional Amount" DataField="EP_Notional_Amount1" SortExpression="EP_Notional_Amount1"
                                                    HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Issuer Order Remark" DataField="EP_Order_Remark1" SortExpression="EP_Order_Remark1"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Ext. RFQ ID" DataField="EP_ExternalQuoteId" SortExpression="EP_ExternalQuoteId"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Booking Branch" DataField="EP_Deal_Booking_Branch" SortExpression="EP_Deal_Booking_Branch"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order Requested At" DataField="ER_TransactionTime" SortExpression="ER_TransactionTime"
                                                    DataFormatString="{0:dd-MMM-yyyy hh:mm:ss tt}" HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="RM OrderId" DataField="EP_HedgedFor" SortExpression="EP_HedgedFor"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Booking OrderId" DataField="EP_HedgingOrderId" SortExpression="EP_HedgingOrderId"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <%-- Mohit on 22-Jan--%>
                                                <asp:BoundColumn HeaderText="Expiry Date" DataField="ExpiryDate" SortExpression="ExpiryDate"
                                                    DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="KO % of init." DataField="EP_KO" SortExpression="BarrierPercentage"
                                                    HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Barr.Type" DataField="BarrierMonitoringMode" SortExpression="ER_BarrierMonitoringMode"
                                                    HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Mat. Date" DataField="Maturity_Date" SortExpression="MaturityDate"
                                                    DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Settl. Date" DataField="SettlmentDate" SortExpression="SettlmentDate"
                                                    DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Value" DataField="SettlementDays" SortExpression="ER_Issuer_Date_Offset"
                                                    HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Created By" DataField="ER_Created_By" SortExpression="created_by"
                                                    HeaderStyle-ForeColor="Black" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Trade Date" DataField="ER_TradeDate" SortExpression="TradeDate"
                                                    DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Solve For" DataField="ER_SolveFor" SortExpression="ER_SolveFor"
                                                    HeaderStyle-ForeColor="Black" Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Order Comment" DataField="EP_OrderComment" SortExpression="EP_OrderComment"
                                                    HeaderStyle-ForeColor="Black">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle Wrap="true" Width="250px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <PagerStyle Mode="NumericPages" CssClass="GridPager " />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grdELNRFQ" EventName="SortCommand" />
                        <asp:AsyncPostBackTrigger ControlID="grdOrder" EventName="SortCommand" />
                        <Asp:PostBackTrigger ControlID="KYIR" />        <%-- Changed by Chitralekha M on 26-Sep-16>--%>
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
                <asp:Button ID="btnhdnEnableDisableDealButtons" runat="server" OnClick="Enable_Disable_Deal_Buttons"
                    Style="visibility: hidden; display: none" onmouseover="JavaScript:alert:this.focus();" />
                <asp:Button ID="btnHdnEnablePage2" runat="server" OnClick="EnablePage" Style="visibility: hidden;
                    display: none" onmouseover="JavaScript:alert:this.focus();" />
                <asp:Button ID="btnhdnSolveAllRequests" runat="server" OnClick="Solve_All_Requests"
                    Style="visibility: hidden; display: none" onmouseover="JavaScript:alert:this.focus();" />
                <asp:Button ID="btnhdnSolveSingleRequest" runat="server" Style="visibility: hidden;
                    display: none" onmouseover="JavaScript:alert:this.focus();" />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UPanle11111" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="DealConfirmPopup" runat="server" CssClass="ConfirmationPopup ui-widget-content ui-draggable"
                Visible="false">
                <div id="Div1" class="msgbody" runat="server">
                    <div class="icon-confirmed">
                        <img src="Images/confirmed.png" width="20px" height="20px" alt="" />
                    </div>
                    <div style="width: 490px !important; cursor: move">
                        <h1>
                            You are placing an order for
                            <asp:Label ID="lblProductNamePopUpValue" runat="server" Text="Equity Linked Note" 
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
                            <td class="lbl">
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
                                <telerik:RadDropDownList ID="ddlRM" runat="server"   style="width:100% !important" 
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
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblBookingBranchPopUpCaption" runat="server" Text="Booking Branch"
                                    CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control">
                                <telerik:RadDropDownList ID="ddlBookingBranchPopUpValue" runat="server" Width="100%" Enabled="true" >
                                   <%--  <Items>
                                    <telerik:DropDownListItem Text="Hong Kong" Value="HK"/>
                                    <telerik:DropDownListItem Text="Singapore" Value="SG" Selected="True"/>
                                    </Items>--%><%-- <COmmented By nikhil M For Dyanamically Adding Element 08Aug2016  EQSCB-16 >--%>
                                    
                               </telerik:RadDropDownList>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblNotionalPopUpCaption" runat="server" Text="Notional" CssClass="lbl"></asp:Label>
                                <asp:Label ID="lblCurrencyPopUpValue" runat="server" Text="HKD" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="right" class="control">
                                <asp:Label ID="lblNotionalPopUpValue" runat="server" Text="0" CssClass="lblBOLD"
                                    Style="text-align: right"></asp:Label>
                                <%--EQBOSDEV-228 removed decimal by chaitali on 21-Jan-16--%>
                            </td>
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
                                     MaxLength="6" style="width:97% !important"></asp:TextBox>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblClientYieldPopUpCaption" runat="server" Text="Client Yield %p.a."
                                    CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:TextBox ID="txtClientYieldPopUpValue" runat="server" Text="0.0%a.p." MaxLength="5" AutoPostBack="true" CssClass="txt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="OptionalTRAccDeccum" visible="false">
                            <td class="lbl" nowrap="nowrap">
                                <asp:Label ID="lblGuaranteePopUpCaption" runat="server" Text="Guarantee" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" nowrap="nowrap" align="right">
                                <asp:Label ID="lblGuaranteePopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>&nbsp;Settlement
                                Periods
                            </td>
                            <td class="lbl" runat="server" id="td1">
                                <asp:Label ID="lblGearingPopUpCaption" runat="server" Text="Is 2x Gearing Leverage"
                                    CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblGearingPopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblOrderTypePopUpCaption" runat="server" Text="Order Type" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control">
                                <telerik:RadDropDownList ID="ddlOrderTypePopUpValue" runat="server" AutoPostBack="true"
                                    Width="100%"  >
                                    <Items>
                                    <telerik:DropDownListItem Text="Market" Value="Market" Selected/>
                                    <telerik:DropDownListItem Text="Limit" Value="Limit"/>
                                    </Items>
                                </telerik:RadDropDownList>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblLimitPricePopUpCaption" runat="server" Text="Limit Level" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="left" style="white-space: nowrap">
                               <telerik:RadDropDownList ID="ddlBasketSharesPopValue" runat="server"   Width="75px">
                               </telerik:RadDropDownList> 
                                <asp:TextBox ID="txtLimitPricePopUpValue" runat="server" Text="84" CssClass="txt"
                                    Style="text-align: right" AutoPostBack="true" MaxLength="12" onkeypress="Javascript:KeysAllowedForNotional();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="rowOrderComment" runat="server" visible="false">
                            <td class="lbl">
                                <asp:Label ID="lblOrderComment" runat="server" Text="Comment: " CssClass="lbl"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtOrderCmt" runat="server" CssClass="txt" AutoPostBack="true" MaxLength="512"
                                    Style="width: 99% !important;"></asp:TextBox>
                            </td>
                        </tr>
                        
                      <%--  <tr>
                            <td colspan="4">
                                <asp:Label ID="lblNtnlSoftBlock" runat="server" Text="Notional Soft Block" ForeColor="Orange" Visible="false"></asp:Label><br />
                                <asp:Label ID="lblNtnlHardBlock" runat="server" Text="Notional Hard Block" ForeColor="Red" Visible="false"></asp:Label>
                            </td>
                            
                        </tr>--%>
                        
                               <%--''<Nikhil M. on 16-Sep-2016:Added For Deal Conformation >--%>
                           <tr id="TblDealReason" runat="server" >
                                            <td>Non-Best Price Reason</td>
                                            <td colspan="3">
                                           <%-- <asp:DropDownList ID="drpConfirmDeal" runat="server"></asp:DropDownList>--%>
                                            <telerik:RadDropDownList CssClass="RadDropDownList RadDropDownList_Default"  ID="drpConfirmDeal" runat="server" Width="360px">  <%--AshwiniP on 11-Nov-2016:To keep order popup width constant--%>
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
                                <asp:GridView ID="grdRMData" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound"  CssClass ="grayBorder" 
                                    DataKeyNames="RM_Name" BorderColor ="#D5D5D5" RowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign ="Center" >
                                    <Columns>
                                        <asp:TemplateField  ItemStyle-CssClass ="grayBorder" HeaderStyle-CssClass ="grayBorder">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RM Name" ItemStyle-Width="150" ItemStyle-BorderColor ="#D5D5D5" ItemStyle-CssClass ="grayBorder" HeaderStyle-CssClass ="grayBorder">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRM_Name" runat="server" Text='<%# Eval("RM_Name") %>'></asp:Label>
                                                <%--<asp:DropDownList ID="ddlRMName" runat="server" Visible="false" Width="92%" CssClass="ddl"
                                                    OnSelectedIndexChanged="ddl_OnSelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>--%>
                                                
                                               <telerik:RadDropDownList ID="ddlRMName" runat="server" Visible="false" Width="92%" AutoPostBack =true  OnSelectedIndexChanged ="ddl_OnSelectedIndexChanged">
                                                </telerik:RadDropDownList>
                                                
                                                <%-- <telerik:RadDropDownList ID="ddlRMName" runat="server" Visible="false" Width="92%"  OnSelectedIndexChanged ="ddl_OnSelectedIndexChanged">
                                                </telerik:RadDropDownList>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CIF/PAN" ItemStyle-Width="100" ItemStyle-CssClass ="grayBorder" HeaderStyle-CssClass ="grayBorder">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCIFPAN" runat="server" Text='<%# Eval("Account_Number") %>'></asp:Label>
                                               <%-- <asp:TextBox ID="txtAccNo" runat="server" Text='<%# Eval("Account_Number") %>' Visible="false"
                                                    CssClass="txt" Width="92%" OnTextChanged="txtBox_onTextChanged" AutoPostBack="True"></asp:TextBox>--%>
                                                   
                                                     <uc2:FinIQ_Fast_Find_Customer ID="FindCustomer" runat="server" DoPostBack="true" OnCustomer_Selected="CustomerSelected"
                                                                                                    EnableTheming="true"  SetWidth="50" />
                                                    
                                                 
                                                <%--
                                                      <telerik:RadDropDownList ID="ddlCIFPAN" runat="server" Width="92%" AutoPostBack =true  OnSelectedIndexChanged="ddlCIFPAN_onTextChanged" >
                                                      </telerik:RadDropDownList>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Notional" ItemStyle-Width="150" ItemStyle-CssClass ="grayBorder" HeaderStyle-CssClass ="grayBorder">
                                            <ItemStyle HorizontalAlign="Center" ></ItemStyle>
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
                        <tr  runat="server"  id="tblRw1">
                         <td class="lbl">
                            <asp:Label ID="lblTotalAmt" runat="server" Text="Total Notional" CssClass="lbl" Visible="True" ></asp:Label>
                            </td>
                            <td>
                            <asp:Label ID="lblTotalAmtVal" runat="server" Text=" " CssClass="lbl" Visible="True"  ></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                               <input type="button" id="btnAddAllocation" runat="server" value="Add Allocation" visible="true"
                                                style="width: 120px" class="btn" onmouseover="JavaScript:alert:this.focus();" />
                            </td>
                        </tr>
                        <tr runat="server" id="tblRw2">
                         <td class="lbl">
                            <asp:Label ID="lblAlloAmt" runat="server" Text="Allocated Notional" CssClass="lbl" Visible="True" style="display:inline-flex;" ></asp:Label>
                            </td>
                            <td>
                            <asp:Label ID="lblAlloAmtVal" runat="server" Text=" " CssClass="lbl" Visible="True"   ></asp:Label>
                            </td>
                        </tr>
                         <tr runat="server" id="tblRw3">
                         <td class="lbl">
                            <asp:Label ID="lblRemainNotional" runat="server" Text="Remaining Notional" CssClass="lbl" Visible="True" style="display:inline-flex;" ></asp:Label>
                            </td>
                            <td>
                            <asp:Label ID="lblRemainNotionalVal" runat="server" Text=" " CssClass="lbl" Visible="True"   ></asp:Label>
                            </td>
                        </tr>
                        <%--</ashwiniP on 20Sept16>--%>
                        <tr>
                            <td colspan="4">
                            <div>
                                <asp:CheckBox ID="chkUpfrontOverride" runat="server" Visible="false"/>
                            <asp:Label ID="lblerrorPopUp" runat="server" ForeColor="Red"></asp:Label>
                           <%-- <asp:CheckBox ID="chkConfirmDeal" runat="server" Visible="false" Text="Do you want to proceed?"/> --%><%--''<Nikhil M. on 08-Sep-2016: ></Nikhil>--%>
                                    
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
                            <td colspan="4" style="height:10px !important;">
                                
                                
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
                Visible="false" Style="width: top: 250px; left: 550px;">
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
                            <td align="right" width="5%">
                                <div >
                                    <input id="btnDetailsCancle" style="background-color:#79B7E3 !important" runat="server" class="btn" onmouseover="JavaScript:alert:this.focus();"
                                        type="button" value="X" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table id="tblAlloDetails" class="TFtable" style="white-space: nowrap !important;
                        width: 100%;">
                        <tr id="trStatus" runat="server">
                            <td>
                                <asp:Label ID="lblsttus" runat="server" CssClass="lbl" Text="Order Status" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloOrderStatus" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="RFQ ID" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloRFQID" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="Counterparty" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lclAlloCP" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr visible="false" runat="server">
                            <td>
                                <asp:Label ID="lblAlloSolvefor" runat="server" CssClass="lbl" Text="Solve For" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblValAlloSolvefor" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="Trade Date" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloTradeDt" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" CssClass="lbl" Text="Settle Date" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloSettDt" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" CssClass="lbl" Text="Expiry Date" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloExpiDt" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" CssClass="lbl" Text="Maturity Date" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloMatuDt" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" CssClass="lbl" Text="Underlying Name(s)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloUnderlying" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="Note Ccy." Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloNoteCcy" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" CssClass="lbl" Text="Strike (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloStrike" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" CssClass="lbl" Text="KO (%) of Initial" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloKO" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label15" runat="server" CssClass="lbl" Text="Settlement Days" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloSettWk" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label16" runat="server" CssClass="lbl" Text="Tenor" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloTenor" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label17" runat="server" CssClass="lbl" Text="Price (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloPrice" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label18" runat="server" CssClass="lbl" Text="Upfront (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloUpfront" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label19" runat="server" CssClass="lbl" Text="Client Price (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloClientPrice" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label20" runat="server" CssClass="lbl" Text="Client Yield (%) p.a."
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloYield" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label21" runat="server" CssClass="lbl" Text="Order Size" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloOrderSize" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id='trQuoteStatus' runat="server">
                            <td>
                                <asp:Label ID="lblQuoteStatus" runat="server" CssClass="lbl" Text="Quote Status"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblValQuoteStatus" runat="server" Text=""  style="white-space:pre-wrap;"/>
                            </td>
                        </tr>
                        <tr id='trOrderType' runat="server">
                            <td>
                                <asp:Label ID="Label22" runat="server" CssClass="lbl" Text="Order Type" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblalloOrderType" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id='trSpot' runat="server">
                            <td>
                                <asp:Label ID="Label23" runat="server" CssClass="lbl" Text="Ref. Spot" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloSpot" runat="server" Text="" Visible="false" />
                            </td>
                        </tr>
                        <tr id='trExePrc1' runat="server">
                            <td>
                                <asp:Label ID="Label45" runat="server" CssClass="lbl" Text="Exec. Price" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloExePrc1" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr id='trAvgExePrc' runat="server">
                            <td>
                                <asp:Label ID="Label73" runat="server" CssClass="lbl" Text="Avg. Exec. Price" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloAvgExePrc" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label24" runat="server" CssClass="lbl" Text="Issuer Remark" Style="font-weight: bold;" />
                            </td>
                            <td style="white-space: normal;">
                                <asp:Label ID="lblAlloRemark" runat="server" Text=""  style="white-space:pre-wrap;"/>
                            </td>
                        </tr>
                        <tr id="trOrderComment" runat="Server" visible="false">
                            <td>
                                <asp:Label ID="Label80" runat="server" CssClass="lbl" Text="Order Comment" Style="font-weight: bold;
                                    background-color: Transparent;" />
                            </td>
                            <td >
                                <asp:Label ID="lblAlloOrderComment" runat="server" Text="" style="white-space:pre-wrap;"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label25" runat="server" CssClass="lbl" Text="Submitted by" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloSubmitteddBy" runat="server" Text="" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
<asp:UpdatePanel  ID="upPTimerRefresh" runat="server" UpdateMode="Conditional" >
 <ContentTemplate >
 <asp:Timer ID="TmRefresh" runat="server"  OnTick="EnableTimerTick"></asp:Timer>
 </ContentTemplate>
 </asp:UpdatePanel>
    <script type="text/javascript">

        function CP_Dragable() {
            $(".ConfirmationPopup").draggable({ containment: "window" }); //Mohit Lalwani on 1-Feb-2016
        }         
    </script>

</asp:Content>
