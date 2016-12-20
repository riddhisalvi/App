<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EQORFQ1.aspx.vb" MasterPageFile="~/FiniqAppMasterPage.Master"
    EnableEventValidation="false" Inherits="FinIQWebApp.EQORFQ1" Title="EQO RFQ and Orders" %>

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

    <script type="text/javascript">

        function showLPBoxes() {
           if (document.getElementById("<%=chkBNPP.ClientID%>")) { document.getElementById("<%=chkBNPP.ClientID%>").style.visibility = 'visible'; }
          }

        function hideLPBoxes() {
          
            if (document.getElementById("<%=chkBNPP.ClientID%>")) {
                if (!document.getElementById("<%=chkBNPP.ClientID%>").checked) {
                    document.getElementById("<%=chkBNPP.ClientID%>").style.visibility = 'hidden';
                }
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

        //Rushikesh/Mohit/Dilkhush 14Jan2016 for share search
        function OnClientRequesting(sender, args) {
            var context = args.get_context();
            context["iMarketype"] = "EQ";
            context["iShareVAl"] = "";
            if (document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "NO" || document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "N") {
                // if ("<%=ddlExchangeEQO.Visible%>" == "True") {
                var seXchange = document.getElementById("<%=ddlExchangeEQO.clientID%>").value;
                context["sExchange"] = seXchange;
            }
            else {
                context["sExchange"] = "All";
            }
        }
        function OnClientRequesting2(sender, args) {
            var context = args.get_context();
            context["iMarketype"] = "EQ";
            context["iShareVAl"] = "";
            if (document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "NO" || document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "N") {
                //  if ("<%=ddlExchangeEQO2.Visible%>" == "True") {
                var seXchange = document.getElementById("<%=ddlExchangeEQO2.clientID%>").value;
                context["sExchange"] = seXchange;
            }
            else {
                context["sExchange"] = "All";
            }
        }
        function OnClientRequesting3(sender, args) {
            var context = args.get_context();
            context["iMarketype"] = "EQ";
            context["iShareVAl"] = "";
            if (document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "NO" || document.getElementById("<%=hdnConfig_EQC_Allow_ALL_AsExchangeOption.ClientID %>").value == "N") {
                //   if ("<%=ddlExchangeEQO3.Visible%>" == "True") {
                var seXchange = document.getElementById("<%=ddlExchangeEQO3.clientID%>").value;
                context["sExchange"] = seXchange;
            }
            else {
                context["sExchange"] = "All";
            }

        }
        function OnClientItemsRequestFailedHandler(sender, eventArgs) {
            eventArgs.set_cancel(true);
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


    </script>

    <script type="text/javascript" language="javascript">
        setResolution();
        $(document).ready(function() {
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
                //viewportHeight = document.documentElement.clientHeight;
                viewportWidth = document.documentElement.clientWidth;
            }

            $(".gridScroll").width((Number(viewportWidth) - 20).toString() + "px");


        }

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
        //Added by Imran to take current thread in temp variable and stop Jquery manually(19-March-14).
        var intervalCopyBNPP;
        //Added by Imran to store client side state into hidden variable.
        var BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
        var AllHiddenPrice = document.getElementById("<%=AllHiddenPrice.ClientID %>");
        var BNPPHiddenMatDate = document.getElementById("<%=BNPPHiddenMatDate.ClientID %>");
        //Added by Imran to start different timer for different provider.
        var tmrBNPP;
        var processingBNPP = false;
        //---Added for search on share
        var ddlText, ddlValue, ddl, txt;
        var tabIndex;
        //Added By Imran for solve all button enable and disable (12-March-14)
        function setPriceAllEnableDisable() {
            try {
                AllHiddenPrice = document.getElementById("<%=AllHiddenPrice.ClientID %>");
                var solveAllFlag = false;
                var btnSA = document.getElementById("<%=btnSolveAll.ClientID %>");
                if (processingBNPP == false) {
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
            } catch (err) { }
        }

        //Added By Imran to check timer value is numeric(12-March-14)
        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }



        //Added By Imran to stop polling (19-March-14)
        function StopPolling() {
            try {
                clearInterval(intervalCopyBNPP);
                processingBNPP = false;
            }
            catch (err) {

            }
        }


        //Added By Imran to poll web service for price after 5 sec (12-March-14)
        //need to change poll time (else config based poll time in java script)
        function getPremium(RFQId, lblPrice, lblTimer, btnDeal, btnPrice1) {
            var checkResetflag = false;

            var time = pollingMilliSec;
            var startTime = new Date().getTime();
            if (btnDeal.indexOf("BNPP") == 21) {
                processingBNPP = true;
            }
            //            else if (btnDeal.indexOf("HSBC") == 21) {
            //                processingHSBC = true;
            //            }

            var interval = setInterval(function() {
                //Added By Imran to take current timer into temp variable(19-March-14)
                if (btnDeal.indexOf("BNPP") == 21) {
                    intervalCopyBNPP = interval;
                    processingBNPP = true;
                    if ('Enable' == document.getElementById("<%=BNPPHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                //                else if (btnDeal.indexOf("HSBC") == 21) {
                //                    intervalCopyHSBC = interval;
                //                    processingHSBC = true;
                //                    if ('Enable' == document.getElementById("HSBCHiddenId").value.split(";")[1]) {
                //                        checkResetflag = true;
                //                    }
                //                    else {
                //                        checkResetflag = false;
                //                    }
                //                }

                if (checkResetflag == false) {
                    $.ajax({
                        type: "POST",
                        url: "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx/Web_CheckForAsyncPremiumResponsewithMail",
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
                                    if (btnDeal.indexOf("BNPP") == 21) {

                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenLimit = document.getElementById("<%=BNPPHiddenLimit.ClientID %>");
                                        BNPPHiddenLimit.value = '';
                                        BNPPHiddenMatDate = document.getElementById("<%=BNPPHiddenMatDate.ClientID %>");
                                        BNPPHiddenMatDate.value = '';
                                        processingBNPP = false;
                                    }
                                    //                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                    //                                        HsbcHiddenPrice = document.getElementById("HSBCHiddenId");
                                    //                                        HsbcHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                    //                                        $("#HSBCwait").hide();
                                    //                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                    //                                        processingHSBC = false;
                                    //                                    }
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
                                    if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenLimit = document.getElementById("<%=BNPPHiddenLimit.ClientID %>");
                                        BNPPHiddenLimit.value = '';
                                        BNPPHiddenMatDate = document.getElementById("<%=BNPPHiddenMatDate.ClientID %>");
                                        BNPPHiddenMatDate.value = '';
                                        processingBNPP = false;
                                    }
                                    //                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                    //                                        HsbcHiddenPrice = document.getElementById("HSBCHiddenId");
                                    //                                        HsbcHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                    //                                        $("#HSBCwait").hide();
                                    //                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                    //                                        processingHSBC = false;
                                    //                                    }
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                    setTimeout(setPriceAllEnableDisable, 500);
                                }

                                else {
                                    //PRICE APPEARED
                                    AllHiddenPrice = 'Disable';
                                    $("#" + lblPrice).text((parseFloat(msg.d)).toFixed(4));
                                    //$("#" + lblPrice).text((parseFloat(msg.d)).toFixed(5));
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = true;
                                    //document.getElementById(lblPrice).style.color = "green";
                                    $('#' + lblPrice).removeClass("lblPrice").removeClass("lblTimeout").removeClass("lblRejected").addClass("lblPrice");
                                    $("#" + btnPrice1).removeClass("btn").addClass("btnDisabled");
                                    $("#" + lblPrice).text((parseFloat(msg.d)).toFixed(4));
                                    $("#" + btnPrice1).val('Order');
                                    if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = (parseFloat(msg.d.split(";")[0])).toFixed(4) + ';Enable;Enable;Disable;Order';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenLimit = document.getElementById("<%=BNPPHiddenLimit.ClientID %>");
                                        BNPPHiddenLimit.value = msg.d.split(";")[1].split("^")[0];
                                        BNPPHiddenMatDate = document.getElementById("<%=BNPPHiddenMatDate.ClientID %>");
                                        BNPPHiddenMatDate.value = msg.d.split("^")[1];
                                        processingBNPP = true;
                                    }
                                    //                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                    //                                        HsbcHiddenPrice = document.getElementById("HSBCHiddenId");
                                    //                                        HsbcHiddenPrice.value = parseFloat(msg.d).toFixed(2) + ';Enable;Enable;Disable;Order';
                                    //                                        $("#HSBCwait").hide();
                                    //                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                    //                                        processingHSBC = true;
                                    //                                    }

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




        //Added By Imran to poll web service for price after 5 sec (12-March-14)
        //need to change poll time (else config based poll time in java script)
        function getBarrier(RFQId, lblPrice, lblTimer, btnDeal, btnPrice1) {
            var checkResetflag = false;
            var time = pollingMilliSec;
            var startTime = new Date().getTime();
            if (btnDeal.indexOf("BNPP") == 21) {
                processingBNPP = true;
            }
            //            else if (btnDeal.indexOf("HSBC") == 21) {
            //                processingHSBC = true;
            //            }

            var interval = setInterval(function() {
                //Added By Imran to take current timer into temp variable(19-March-14)
                if (btnDeal.indexOf("BNPP") == 21) {
                    intervalCopyBNPP = interval;
                    processingBNPP = true;
                    if ('Enable' == document.getElementById("<%=BNPPHiddenPrice.ClientID %>").value.split(";")[1]) {
                        checkResetflag = true;
                    }
                    else {
                        checkResetflag = false;
                    }
                }
                //                else if (btnDeal.indexOf("HSBC") == 21) {
                //                    intervalCopyHSBC = interval;
                //                    processingHSBC = true;
                //                    if ('Enable' == document.getElementById("HSBCHiddenId").value.split(";")[1]) {
                //                        checkResetflag = true;
                //                    }
                //                    else {
                //                        checkResetflag = false;
                //                    }
                //                }

                if (checkResetflag == false) {
                    $.ajax({
                        type: "POST",
                        url: "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx/Web_CheckForAsyncBarrierResponsewithMail",
                        //data: "{'strRFQID': " + RFQId + "&'strTimeout':30}",
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

                                    document.getElementById(lblPrice).style.color = "red";
                                    $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
                                    if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenLimit = document.getElementById("<%=BNPPHiddenLimit.ClientID %>");
                                        BNPPHiddenLimit.value = msg.d.split(";")[1].split("^")[0];
                                        BNPPHiddenMatDate = document.getElementById("<%=BNPPHiddenMatDate.ClientID %>");
                                        BNPPHiddenMatDate.value = msg.d.split("^")[1];
                                        processingBNPP = false;
                                    }
                                    //                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                    //                                        HsbcHiddenPrice = document.getElementById("HSBCHiddenId");
                                    //                                        HsbcHiddenPrice.value = msg.d + ';Enable;Disable;Disable;Price';
                                    //                                        $("#HSBCwait").hide();
                                    //                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                    //                                        processingHSBC = false;
                                    //                                    }

                                    clearInterval(interval);

                                    setTimeout(setPriceAllEnableDisable, 1000);
                                    //Added by Imran for best price logic 14-Apr-14
                                    document.getElementById("<%=btnLoad.ClientID %>").click(); //'<AvinashG. on 28-Apr-2014: Update grid after Rejection comes up>
                                }
                                else if ($.trim(msg.d) == 'Timeout') {
                                    $("#" + lblPrice).text('Timeout');

                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = false;
                                    document.getElementById(lblPrice).style.color = "red";
                                    $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
                                    clearInterval(interval);
                                    if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenLimit = document.getElementById("<%=BNPPHiddenLimit.ClientID %>");
                                        BNPPHiddenLimit.value = msg.d.split(";")[1].split("^")[0];
                                        BNPPHiddenMatDate = document.getElementById("<%=BNPPHiddenMatDate.ClientID %>");
                                        BNPPHiddenMatDate.value = msg.d.split("^")[1];
                                        processingBNPP = false;
                                    }
                                    //                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                    //                                        HsbcHiddenPrice = document.getElementById("HSBCHiddenId");
                                    //                                        HsbcHiddenPrice.value = 'Timeout;Enable;Disable;Disable;Price';
                                    //                                        $("#HSBCwait").hide();
                                    //                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                    //                                        processingHSBC = false;
                                    //                                    }
                                    document.getElementById("<%=btnLoad.ClientID %>").click();
                                    setTimeout(setPriceAllEnableDisable, 500);
                                }

                                else {
                                    //PRICE APPEARED
                                    AllHiddenPrice = 'Disable';
                                    $("#" + lblPrice).text((parseFloat(msg.d)).toFixed(4));
                                    if (document.getElementById(btnPrice1) != null)
                                        document.getElementById(btnPrice1).disabled = true;
                                    document.getElementById(lblPrice).style.color = "green";
                                    $("#" + btnPrice1).removeClass("btn").addClass("btnDisabled");
                                    $("#" + lblPrice).text((parseFloat(msg.d)).toFixed(2));
                                    $("#" + btnPrice1).val('Order');
                                    if (btnDeal.indexOf("BNPP") == 21) {
                                        BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                                        BNPPHiddenPrice.value = (parseFloat(msg.d)).toFixed(4) + ';Enable;Enable;Disable;Order';
                                        $("#BNPPwait").hide();
                                        document.getElementById('BNPPwait').style.visibility = 'hidden';
                                        BNPPHiddenLimit = document.getElementById("<%=BNPPHiddenLimit.ClientID %>");
                                        BNPPHiddenLimit.value = msg.d.split(";")[1].split("^")[0];
                                        BNPPHiddenMatDate = document.getElementById("<%=BNPPHiddenMatDate.ClientID %>");
                                        BNPPHiddenMatDate.value = msg.d.split("^")[1];
                                        processingBNPP = true;
                                    }
                                    //                                    else if (btnDeal.indexOf("HSBC") == 21) {
                                    //                                        HsbcHiddenPrice = document.getElementById("HSBCHiddenId");
                                    //                                        HsbcHiddenPrice.value = parseFloat(msg.d).toFixed(2) + ';Enable;Enable;Disable;Order';
                                    //                                        $("#HSBCwait").hide();
                                    //                                        document.getElementById('HSBCwait').style.visibility = 'hidden';
                                    //                                        processingHSBC = true;
                                    //                                    }

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

        //Added By Imran to stop all timer 12-March-14.
        //Currently not in use.
        function StopAllTimers() {
            clearTimeout(tmrBNPP);
            document.getElementById("<%=lblTimerBNPP.ClientID %>").innerHTML = ""
            BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
            BNPPHiddenPrice.value = ';Disable;Disable;Disable;Price';

        }

        //Added By Imran to stop specific timer 12-March-14.
        //Currently not in use.
        function StopPPTimerValue(btnDeal) {
            if (btnDeal.indexOf("BNPP") == 21) {
                clearTimeout(tmrBNPP);
                $("#BNPPwait").hide();
                if (document.getElementById('BNPPwait') != null) {
                    document.getElementById('BNPPwait').style.visibility = 'hidden';
                }
            }
            //            else if (btnDeal.indexOf("HSBC") == 21) {
            //                clearTimeout(tmrHSBC);
            //                $("#HSBCwait").hide();
            //                if (document.getElementById('HSBCwait') != null) {
            //                    document.getElementById('HSBCwait').style.visibility = 'hidden';
            //                }
            //            }

        }

        //Added By Imran to stop specific timer based on label,button 12-March-14.
        function StopPPTimer(lblPrice, btnDeal, btnPrice1) {
            setPriceAllEnableDisable();
            $("#" + lblPrice).text("");
            document.getElementById(btnPrice1).disabled = false;
            document.getElementById(btnDeal).disabled = true;
            $("#" + btnPrice1).removeClass("btnDisabled").addClass("btn");
            $("#" + btnDeal).removeClass("btn").addClass("btnDisabled");
            if (btnDeal.indexOf("BNPP") == 21) {
                clearTimeout(tmrBNPP);
                BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                BNPPHiddenPrice.value = ';Enable;Disable;Disable;Price';
                $("#BNPPwait").hide();
            }
            //            else if (btnDeal.indexOf("HSBC") == 21) {
            //                clearTimeout(tmrHSBC);
            //                HsbcHiddenPrice = document.getElementById("HSBCHiddenId");
            //                HsbcHiddenPrice.value = ';Enable;Disable;Disable;Price';
            //                $("#HSBCwait").hide();
            //            }

        }

        //Added By Imran to start specific timer based on label,button 12-March-14.
        function InitializeTimer(lblid, ValidityTime, btnDeal, btnPrice) {
            setTimeout(setPriceAllEnableDisable, 500);
            if (ValidityTime == "") ValidityTime = orderValiditySec;
            document.getElementById(lblid).innerHTML = Pad(ValidityTime);
            if (ValidityTime < 20) { document.getElementById(lblid).style.color = "red"; }
            ValidityTime = ValidityTime - 1;
            if (ValidityTime <= 0) {
                if (btnDeal.indexOf("BNPP") == 21) {
                    BNPPHiddenPrice = document.getElementById("<%=BNPPHiddenPrice.ClientID %>");
                    BNPPHiddenPrice.value = BNPPHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                    clearTimeout(tmrBNPP);
                    processingBNPP = false;
                }
                //                else if (btnDeal.indexOf("HSBC") == 21) {
                //                    HsbcHiddenPrice = document.getElementById("HSBCHiddenId");
                //                    HsbcHiddenPrice.value = HsbcHiddenPrice.value.split(";")[0] + ';Enable;Disable;Disable;Price';
                //                    clearTimeout(tmrHSBC);
                //                    processingHSBC = false;
                //                }

                document.getElementById(lblid).innerHTML = "";
                if ($("#ctl00_MainContent_DealConfirmPopup").is(':visible'))        //FIre Server trip only if pop-up is visible
                {

                    if ($("#ctl00_MainContent_lblIssuerPopUpValue").text() == "BNPP") {
                        if (processingBNPP == false)
                            document.getElementById("<%=btnHdnEnablePage2.ClientID %>").click();
                    }

                }
                //'</AvinashG. on 26-Aug-2014: FA-526 	Order pop up disappers even before the completing 60 sec time >
                //document.getElementById(btnDeal).disabled = true;
                if (document.getElementById(btnPrice) != null) {
                    document.getElementById(btnPrice).disabled = false;
                    $("#" + btnPrice).removeClass("btnDisabled").addClass("btn");
                    $("#" + btnPrice).val('Price');
                }
                document.getElementById("ctl00_MainContent_btnLoad").click();  //KBM on 21-April-2014
            }
            else {
                if (btnDeal.indexOf("BNPP") == 21) {
                    tmrBNPP = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                }
                //                else if (btnDeal.indexOf("HSBC") == 21) {
                //                    tmrHSBC = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnPrice + "');", 1000);
                //                }


            }
        }



        function OnFocusSelectText(id) {
            document.getElementById(id).focus();
            document.getElementById(id).select();
        }
        function postbackButtonClick() {
            return true;
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
            var tab = $find('<%= tabContainer.ClientID %>');
            __doPostBack('<%= upnl4.ClientID %>', '');
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
        function OnHover(val) {
            val.style.backgroundColor = "#FFF";
        }
        function OnOut(val) {
            val.style.backgroundColor = "#F2F2F3";
        }
    </script>

    <style type="text/css">
        .settlDateCntrl table
        {
            padding-left: 3px;
            float: left;
            margin-top: -2px;
        }
        #ctl00_MainContent_tabContainer_body
        {
            border: 1px solid #d5d5d5;
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
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td style="height: 150px; width: 720px;" align="left" valign="top">
                            <%-- <asp:UpdatePanel ID="updTab" runat="server" UpdateMode="Always" >
                        <ContentTemplate>--%>
                            <ajax:TabContainer ID="tabContainer" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_xp ajax__tab_container ajax__tab_default"
                                Style="vertical-align: top" AutoPostBack="false" OnClientActiveTabChanged="function(){UpdateTab();}">
                                <ajax:TabPanel ID="tabPanelELN" runat="server" TabIndex="0" Height="180px">
                                    <HeaderTemplate>
                                        &nbsp;ELN&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="upnl1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div style="text-align: center; margin-top: 40px; height: 125px;">
                                                    <img src="../App_Resources/ajax-loader7.gif" id="ImgELN" width="50px" height="50px"
                                                        alt="x" />
                                                    <div style="text-align: center;">
                                                        Loading...</div>
                                                </div>
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
                                            <img src="../App_Resources/ajax-loader7.gif" id="ImgDRA" width="50px" height="50px"
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
                                            <img src="../App_Resources/ajax-loader7.gif" id="ImgAcc" width="50px" height="50px"
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
                                            <img src="../App_Resources/ajax-loader7.gif" id="ImgDec" width="50px" height="50px"
                                                alt="x" />
                                            <div style="text-align: center;">
                                                Loading...</div>
                                        </div>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                                <ajax:TabPanel ID="tabPanelEQO" runat="server" TabIndex="5">
                                    <HeaderTemplate>
                                        &nbsp;Options&nbsp;
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="upnl4" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Panel ID="panelEQO" runat="server">
                                                    <table cellspacing="1px" cellpadding="1px" style="width: 720px; height: 180px;">
                                                        <tr runat="server" id="trUType" visible="false">
                                                            <td>
                                                                <asp:Label ID="lblUType" runat="server" CssClass="lbl" Text="Underlying Type"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlProductEQO" runat="server" CssClass="ddl" Width="124px"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Value="Equity" Text="Equity"></asp:ListItem>
                                                                    <asp:ListItem Value="Index" Text="Index"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="7">
                                                                <table cellspacing="0" cellpadding="1" id="tblEQOBasket" style="margin-left: -2px">
                                                                    <tr>
                                                                        <td runat="server" id="tdHeadAddShare1" visible="false">
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Label ID="lblSelectionExchangeEQOHeader" runat="server" CssClass="lbl" Text="Exchange"
                                                                                Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblShares" runat="server" CssClass="lbl" Text="Underlying(s)"></asp:Label>
                                                                            (<asp:Label ID="txtBasketShares" runat="server" CssClass="lbl" Text=" "></asp:Label>)
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEQOCcy" runat="server" CssClass="lbl" Text="Ccy" style="padding-left:25px;"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                            <asp:Label ID="lblDisplayExchangeEQOHeader" runat="server" Text="Exchange" CssClass="lbl" style="padding-left:25px;"></asp:Label></td><td>
                                                                             <%--added by AshwiniP on 19-Sept-2015--%>
                                                                             <asp:Label ID="lblPRR" runat="server" Text="PRR " Width="5px" CssClass="lbl" Style="padding-left:25px;"></asp:Label>
                                                                           
                                                                        </td>
                                                                        <td>  <asp:Label ID="lblAdvisoryFlag" runat="server" Text="Advisory " CssClass="lbl" style =" padding-left:50px;  "></asp:Label></td><td>Flag</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td runat="server" id="tdAddShare1" visible="false">
                                                                            <asp:CheckBox ID="chkAddShare1" runat="server" CssClass="lbl" AutoPostBack="true"
                                                                                Checked="true" Enabled="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlExchangeEQO" runat="server" CssClass="ddl" Width="226px"
                                                                                AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>

                                                                            <script type="text/javascript">
                                                                                function ShowDropDownFunctionEQO() {
                                                                                    var combo1 = $find("<%= ddlShareEQO.ClientID %>");
                                                                                    if (combo1.get_text().length.toString() == '3')
                                                                                        combo1.showDropDown();
                                                                                }
                                                                                function HideDropDownFunctionEQO() {
                                                                                    var combo1 = $find("<%= ddlShareEQO.ClientID %>");
                                                                                    combo1.hideDropDown();
                                                                                }
                                                                                function ShowDropDownFunctionEQO2() {
                                                                                    var combo2 = $find("<%= ddlShareEQO2.ClientID %>");
                                                                                    if (combo2.get_text().length.toString() == '3')
                                                                                        combo2.showDropDown();
                                                                                }
                                                                                function HideDropDownFunctionEQO2() {
                                                                                    var combo2 = $find("<%= ddlShareEQO2.ClientID %>");
                                                                                    combo2.hideDropDown();
                                                                                }
                                                                                function ShowDropDownFunctionEQO3() {
                                                                                    var combo3 = $find("<%= ddlShareEQO3.ClientID %>");
                                                                                    if (combo3.get_text().length.toString() == '3')
                                                                                        combo3.showDropDown();
                                                                                }
                                                                                function HideDropDownFunctionEQO3() {
                                                                                    var combo3 = $find("<%= ddlShareEQO3.ClientID %>");
                                                                                    combo3.hideDropDown();
                                                                                }
                                                                            </script>

                                                                            <telerik:RadComboBox ID="ddlShareEQO" runat="server" Height="150" OnClientItemsRequesting="OnClientRequesting"
                                                                                EmptyMessage="" EnableLoadOnDemand="true" ShowMoreResultsBox="false" EnableVirtualScrolling="false"
                                                                                AutoPostBack="true" MarkFirstMatch="true" ShowDropDownOnTextboxClick="true" Style="width: 300px;
                                                                                height: 20px !important;" MinFilterLength="3" ForeColor="Black" ShowToggleImage="true"
                                                                                ToolTip="Search by inputing share name or symbol. Press Enter to select." LoadingMessage="Loading matching shares..."
                                                                                TabIndex="1" OnClientItemsRequestFailed="OnClientItemsRequestFailedHandler">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                     
                                                                        <td>
                                                                            <asp:Label ID="lblEQOBaseCcy" runat="server" CssClass="lbl" Width="26px" Text="" style =" padding-left: 15px;"></asp:Label>
                                                                         </td>
                                                                            <td>
                                                                            <asp:Label ID="lblExchangeEQO" runat="server" CssClass="lbl" Text="" style="white-space:nowrap; padding-left:15px;"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                             <%--added by AshwiniP on 19-Sept-2015--%>
                                                                            <asp:Label ID="lblPRRVal" runat="server" Text=""  CssClass="lbl" Style="padding-left:15px;"></asp:Label></td>
                                                                            <td>
                                                                            <%--added by Mohit Lalwani on 15-Jan-2015--%>
                                                                            <asp:Label ID="lblAdvisoryFlagVal" runat="server" Text=""  CssClass="lbl" Style="Padding-left: 60px;"></asp:Label>
                                                                            <%--Added by Chitralekha on 28-Sep-16--%>
                                                                            </td>
                                                                            <td>
                                                    
                                                                            <asp:HiddenField ID="hdnConfig_EQC_Allow_ALL_AsExchangeOption" Value="" runat="server" />
                                                                            <%--/added by Mohit Lalwani on 15-Jan-2015--%>
                                                                            <input type="hidden" id="Hdnbasketaddedshares" name="Hdnbasketaddedshares" runat="server"
                                                                                onmouseover="JavaScript:alert:this.focus();" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="tblShareEQO_2" visible="false">
                                                                        <td>
                                                                            <asp:CheckBox ID="chkAddShare2" runat="server" CssClass="lbl" AutoPostBack="true" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlExchangeEQO2" runat="server" CssClass="ddl" Width="226px"
                                                                                AutoPostBack="true" Enabled="false">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="ddlShareEQO2" runat="server" Height="150" OnClientItemsRequesting="OnClientRequesting2"
                                                                                EmptyMessage="" EnableLoadOnDemand="true" ShowMoreResultsBox="false" EnableVirtualScrolling="false"
                                                                                AutoPostBack="true" MarkFirstMatch="true" ShowDropDownOnTextboxClick="true" Style="width: 300px;
                                                                                height: 20px !important;" Enabled="false" MinFilterLength="3" ForeColor="Black"
                                                                                ShowToggleImage="true" ToolTip="Search by inputing share name or symbol. Press Enter to select."
                                                                                LoadingMessage="Loading matching shares..." TabIndex="1" OnClientItemsRequestFailed="OnClientItemsRequestFailedHandler">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblBaseCurrency2" runat="server" CssClass="lbl" Width="26px" Text=""></asp:Label>
                                                                            <asp:Label ID="lblExchangeEQO2" runat="server" CssClass="lbl" Text=""></asp:Label>
                                                                             <asp:Label ID="lblPRRVal2" runat="server" CssClass="lbl" Text="" Style="padding-left: 25px;"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" id="tblShareEQO_3" visible="false">
                                                                        <td>
                                                                            <asp:CheckBox ID="chkAddShare3" runat="server" CssClass="lbl" AutoPostBack="true"
                                                                                Enabled="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlExchangeEQO3" runat="server" CssClass="ddl" Width="226px"
                                                                                AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="ddlShareEQO3" runat="server" Height="150" EmptyMessage=""
                                                                                OnClientItemsRequesting="OnClientRequesting3" EnableLoadOnDemand="true" ShowMoreResultsBox="false"
                                                                                EnableVirtualScrolling="false" AutoPostBack="true" MarkFirstMatch="true" ShowDropDownOnTextboxClick="true"
                                                                                Style="width: 300px; height: 20px !important;" MinFilterLength="3" ForeColor="Black"
                                                                                ShowToggleImage="true" ToolTip="Search by inputing share name or symbol. Press Enter to select."
                                                                                LoadingMessage="Loading matching shares..." TabIndex="1" OnClientItemsRequestFailed="OnClientItemsRequestFailedHandler">
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblBaseCurrency3" runat="server" CssClass="lbl" Width="26px" Text=""></asp:Label>
                                                                            <asp:Label ID="lblExchangeEQO3" runat="server" CssClass="lbl" Text=""></asp:Label>
                                                                            <asp:Label ID="lblPRRVal3" runat="server" CssClass="lbl" Text="" Style="padding-left: 25px;"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <%-- <td colspan="2" style="vertical-align: top">
                                                                <fieldset id="fsEstimates" style="width: 100px; height: 30px; padding: 0px;" runat="server">
                                                                    <legend>Estimated Notional:</legend>
                                                                    <div style="white-space: nowrap; padding-top: 2px">
                                                                        
                                                                    </div>
                                                                </fieldset>
                                                            </td>--%>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSideEQO" runat="server" CssClass="lbl" Text="Client side"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlSideEQO" runat="server" Width="130px"
                                                                    AutoPostBack="true">
                                                                    <Items>
                                                                        <telerik:DropDownListItem Value="Buy" Text="Buy" />
                                                                        <telerik:DropDownListItem Value="Sell" Text="Sell" Selected="True" />
                                                                    </Items>
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSubSecurityType" runat="server" CssClass="lbl" Text="Option Type"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlOptionType" runat="server" Width="131px"
                                                                    AutoPostBack="true">
                                                                    <%--<asp:ListItem Value="European Call" Text="European Call"></asp:ListItem>
                                                                    <asp:ListItem Value="European Put" Text="European Put"></asp:ListItem>
                                                                    <asp:ListItem Value="American Call" Text="American Call"></asp:ListItem>
                                                                    <asp:ListItem Value="American Put" Text="American Put"></asp:ListItem>
                                                                    <asp:ListItem Value="KnockIn Put" Text="KnockIn Put"></asp:ListItem>--%>
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTenorEQO" runat="server" CssClass="lbl" Text="Tenor"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTenorEQO" runat="server" CssClass="txt" AutoPostBack="True" AutoCompleteType="BusinessStreetAddress"
                                                                    Width="30px" Style="text-align: right" MaxLength="3">3</asp:TextBox>
                                                                <telerik:RadDropDownList ID="ddlTenorEQO" runat="server" Width="87px"
                                                                    AutoPostBack="true">
                                                                    <%--need to discuss tenor types--%>
                                                                    <Items>
                                                                        <telerik:DropDownListItem Value="MONTH" Text="Month" Selected="True" />
                                                                        <telerik:DropDownListItem Value="YEAR" Text="Year" />
                                                                    </Items>
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td >
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblstrikeEQO" runat="server" CssClass="lbl" Text="Strike" Visible="false"></asp:Label>
                                                                <%--Add % if checkbox checked (%) else keep blank--%>
                                                                <telerik:RadDropDownList ID="ddlStrikeTypeEQO" runat="server" AutoPostBack="true" Width="100px">
                                                                    <Items>
                                                                        <telerik:DropDownListItem Value="Percentage" Text="Strike(%)" />
                                                                        <telerik:DropDownListItem Value="Absolute" Text="Strike" />
                                                                    </Items>
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                                <%--<asp:CheckBox ID="chkStrikePriceType" runat="server" CssClass="lbl" /> --%>
                                                                <asp:TextBox ID="txtStrikeEQO" runat="server" CssClass="txt" Width="125px" AutoPostBack="true"
                                                                    MaxLength="9" Style="text-align: right">100.00</asp:TextBox>
                                                            </td>
                                                            <%--<td >
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblSolveforEQO" runat="server" CssClass="lbl" Text="Solve For"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlSolveforEQO" runat="server" Width="131px"
                                                                    AutoPostBack="true">
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="white-space: nowrap;" align="left">
                                                                <asp:Label ID="lblTradeDateEQO" runat="server" CssClass="lbl" Text="Trade Date"></asp:Label>
                                                            </td>
                                                            <td style="white-space: nowrap;">
                                                                <uc1:DateControl ID="txtTradeDateEQO" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                                    DoPostBack="true" Disabled="true" TextBoxSize="105" />
                                                            </td>
                                                            <%-- <td>
                                                                <asp:Label ID="lblPriceEQO" runat="server" CssClass="lbl" Text="Price"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPriceEQO" runat="server" CssClass="txt" Width="121px"></asp:TextBox>
                                                            </td>--%>
                                                            <td >
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPremiumEQO" runat="server" CssClass="lbl" Text="Premium (%)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPremium" runat="server" CssClass="txt" Width="125px" AutoPostBack="true"
                                                                    Style="text-align: right">80.00</asp:TextBox>
                                                            </td>
                                                            <%--<td ></td>--%>
                                                            <td>
                                                                <asp:Label ID="lblsettlCcyEQO" runat="server" CssClass="lbl" Text="Settlement"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlsettlmethod" runat="server" Width="74px"
                                                                    AutoPostBack="true">
                                                                    <Items>
                                                                        <telerik:DropDownListItem Value="Physical" Text="Physical" />
                                                                        <telerik:DropDownListItem Value="Cash" Text="Cash" />
                                                                    </Items>
                                                                </telerik:RadDropDownList>
                                                                <telerik:RadDropDownList ID="ddlSettlCcyEQO" runat="server" Width="53px"
                                                                    AutoPostBack="true">
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSettlDateEQO" runat="server" Text="Settl. Date" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <td class="settlDateCntrl">
                                                                <asp:TextBox ID="txtSettlDays" runat="server" CssClass="txt" Width="30px" AutoPostBack="true"
                                                                    Style="text-align: right; float: left;" MaxLength="2" Enabled="False">2</asp:TextBox>
                                                                <uc1:DateControl ID="txtSettlDateEQO" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                                    DoPostBack="true" style="position: relative" TextBoxSize="68" />
                                                            </td>
                                                            <td >
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="tdTitleNumberOfShares">
                                                                <asp:RadioButton ID="rdbQuantity" runat="server" GroupName="grprdbQuantity" Checked="true"
                                                                    AutoPostBack="true" />
                                                                <asp:Label ID="lblOrderqtyEQO" runat="server" CssClass="lbl" Text="No. of shares"></asp:Label>
                                                            </td>
                                                            <td runat="server" id="tdControlNumberOfShares">
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtOrderqtyEQO" runat="server" CssClass="txt" Width="70px" AutoPostBack="true"
                                                                            Style="text-align: right" MaxLength="10">2,000</asp:TextBox>
                                                                        <telerik:RadDropDownList ID="ddlInvestCcy" runat="server" CssClass="ddl" AutoPostBack="true"
                                                                            Visible="true" Width="52px">
                                                                        </telerik:RadDropDownList>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <%--        <td></td>--%>
                                                            <td runat="server" id="tdTitleNotional">
                                                                <asp:RadioButton ID="rdbNotional" runat="server" GroupName="grprdbQuantity" AutoPostBack="true" />
                                                                <asp:Label ID="lblTitleNotional" runat="server" Text="Notional" CssClass="lbl"></asp:Label>
                                                                <asp:Label ID="lblNotionalWithCcy1" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td runat="server" id="tdControlNotional">
                                                                <div>
                                                                    <asp:UpdatePanel ID="upanlNotional" runat="server" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtNotional" runat="server" AutoPostBack="true" OnTextChanged="txtNotional_TextChanged"
                                                                                CssClass="txt" Style="text-align: right" Width="116px" MaxLength="14" Enabled="false"
                                                                                BackColor="LightGray"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblExpiryDateEQO" runat="server" Text="Expiry Date" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc1:DateControl ID="txtExpiryDateEQO" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                                    DoPostBack="true" TextBoxSize="105" />
                                                            </td>
                                                            <td >
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblUpfrontEQO" runat="server" CssClass="lbl" Text="Upfront (%)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUpfrontEQO" runat="server" CssClass="txt" Width="125px" AutoPostBack="true"
                                                                    Style="text-align: right" MaxLength="6">0.5</asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="fsEstimates" runat="server" CssClass="lbl" Text="Esti. Notional"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEstimatedNoOfDays" runat="server" Text=" " CssClass="lbl" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblEstimatedNotional" runat="server" Text=" " CssClass="lbl"></asp:Label>
                                                                <asp:Label ID="lblNotionalWithCcy" runat="server" Text="" Width="75px"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMaturityDateEQO" runat="server" Text="Mat. Date" CssClass="lbl"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <uc1:DateControl ID="txtMaturityDateEQO" runat="server" CalenderCss="btn1" TextBoxCss="txt"
                                                                    DoPostBack="true" TextBoxSize="105" />
                                                            </td>
                                                            <td >
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBarrierLevelEQO" runat="server" CssClass="lbl" Text="Barrier Level"
                                                                    Visible="false"></asp:Label>
                                                                <asp:DropDownList ID="ddlBarrierEQO" runat="server" CssClass="ddl" AutoPostBack="true"
                                                                    Width="82px" Visible="false">
                                                                    <asp:ListItem Value="Percentage" Text="Barrier(%)"></asp:ListItem>
                                                                    <asp:ListItem Value="Absolute" Text="Barrier"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBarrierLevelEQO" runat="server" CssClass="txt" Width="125px"
                                                                    AutoPostBack="true" Visible="false" Style="text-align: right">90.00</asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblBarrierMonitoringType" runat="server" CssClass="lbl" Text="Barrier Type"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlBarrierMonitoringType" runat="server" CssClass="ddl" Width="120px"
                                                                    AutoPostBack="true" Visible="false">
                                                                    <asp:ListItem Value="Continuous" Text="Continuous"></asp:ListItem>
                                                                    <asp:ListItem Value="At Close" Text="At Close"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td >
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </ajax:TabPanel>
                            </ajax:TabContainer>
                            <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        </td>
                        <td colspan="3" align="left" valign="top" class="Filter" style="border-top-width: 0px;
                            border-left-width: 0px;" id="tdShareGraphData" runat="server">
                            <asp:UpdatePanel ID="updShareGraphData" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table cellpadding="0px" cellspacing="0px" style="margin-left: 0px; width: 100%">
                                        <tr>
                                            <td style="vertical-align: top; padding-top: 2px;" align="left" valign="top">
                                                <div class="ajax__tab_header">
                                                    <table cellpadding="0px" cellspacing="0px" style="margin-left: 0px; width: 100%">
                                                        <tr>
                                                            <td id="tdrblShareData" runat="server" style="padding-left: 0px; white-space: nowrap">
                                                                <asp:RadioButtonList ID="rblShareData" runat="server" RepeatDirection="Horizontal"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Text="Share Info." Enabled="true" Value="SHAREDATA" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Quote/Order Graph" Enabled="true" Value="GRAPHDATA"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td style="width: 20px;">
                                                                <img src="../App_Resources/user_suit.png" style="border: 0px; width: 20px; height: 20px;"
                                                                    alt="RFQ RM" visible="false" title="RFQ RM" />
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlRFQRM" runat="server" CssClass="ddl" Width="200"
                                                                    Height="20px" AutoPostBack="true">
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEntity" runat="server" Text="Entity" Height="22px" CssClass="lbl"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlentity" runat="server" CssClass="ddl" Width="120px" Height="20px"
                                                                    AutoPostBack="true" Style="display: none;">
                                                                </asp:DropDownList>
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
                                                                <telerik:RadDropDownList ID="ddlAccount" runat="server" CssClass="ddl" Height="20px"
                                                                    AutoPostBack="false" Visible="false">
                                                                    <Items>
                                                                        <telerik:DropDownListItem Text="SG A/C" Value="1" />
                                                                        <telerik:DropDownListItem Text="HK A/C" Value="2" />
                                                                    </Items>
                                                                </telerik:RadDropDownList>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDropDownList ID="ddlBranch" runat="server" CssClass="ddl" Width="120px"
                                                                    Height="20px" Visible="false">
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
                                            <td class="Filter" style="border: 1px solid #d5d5d5; border-left: none;border-bottom:none;" align="left"
                                                valign="top" runat="Server" id="tdgrphShareData">
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
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rblShareData" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                     </tr>
                    <!--Separate Deal Controls-->
                                        <tr runat="server" id="trSaveSetting">
                    <td >
                     <asp:Button ID="btnSaveSettings" runat="server"  Text="Save Settings" CssClass="btn" onmouseover="JavaScript:alert:this.focus();" /><asp:Label ID="lblError_DefaultSettings" runat="server" CssClass="lbl" ForeColor="red" Style="width: auto;"></asp:Label>
                    </td>
                    <td colspan="3">
                    
                    </td>
                    </tr>
                    <tr>
                        <td class="Filter" align="left" valign="top" style="width: 720px; border-top-width: 0px;"  runat="server" id="tdpnlReprice">
                            <asp:UpdatePanel runat="server" ID="pnlReprice" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="PanelReprice" runat="server">
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
                                                                        <asp:Label ID="lblClientPriceCaption" runat="server" CssClass="lbl" Text="Client Premium (%)"
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
                                                                        <asp:Label ID="lblTimerAll" runat="server" Text="" CssClass="lblBOLD" Style="vertical-align: middle;
                                                                            text-align: center;"> </asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" align="center" style="width: 105px !important;">
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
                                                <td runat="server" id="tdBNPP1" align="left" valign="top">
                                                    <!--SIXTH LP-->
                                                   <asp:UpdatePanel ID="upnlBNPP" runat="server">
                                                        <ContentTemplate>
                                                            <table id="TRBNPP1" runat="server" cellpadding="0" cellspacing="0" class="cptyTbl">
                                                                <tr  onmouseover="showLPBoxes()" >
                                                                    <td valign="middle"  align="center" style="height: 48px; vertical-align: middle;"  >
                                                                    <table cellpadding ="0" cellspacing ="0" >
                                                                    <tr><td style ="height:70% !important;" align="center"> <asp:CheckBox ID="chkBNPP" runat="server" AutoPostBack="true"  /><asp:Label ID="Label2" runat="server" CssClass="lbl BestLP" Text="SCB" Style="font-size: 14px !important;"></asp:Label></td></tr>
                                                                    <tr><td style ="height:30% !important;" align="center">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBNPP" runat="server" CssClass="lbl BestLP" Text="BNPP" Style="font-size: 10px !important;"></asp:Label></td></tr>
                                                                    </table>
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
                                                                    <td valign="top" align="center" >
                                                                        <div style="width: 20px; height: 20px; position: absolute; z-index: 1000px">
                                                                            <img src="../App_Resources/ajax-loader7.gif" id="BNPPwait" width="20px" height="20px"
                                                                                style="visibility: hidden;" alt="x" />
                                                                        </div>
                                                                        <asp:Button ID="btnBNPPPrice" onmouseover="JavaScript:alert:this.focus();" runat="server"
                                                                            Text="Price" OnClick="btnBNPPPrice_Click" Width="45px" CssClass="btn" />
                                                                        <asp:Button ID="btnBNPPDeal" runat="server" onmouseover="JavaScript:alert:this.focus();"
                                                                            Text="Order" OnClick="btnBNPPDeal_Click" Enabled="false" ToolTip="Deal" Width="45px"
                                                                            CssClass="btn" Visible="false" />
                                                                        <asp:HiddenField ID="BNPPHiddenPrice" Value="" runat="server" />
                                                                        <asp:HiddenField ID="BNPPHiddenMatDate" Value="" runat="server" />
                                                                        <asp:HiddenField ID="BNPPHiddenLimit" Value="" runat="server" />
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
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:HiddenField ID="hdnBestPremium" Value="" runat="server" />
                                    <asp:HiddenField ID ="hdnBestProvider" Value ="" runat ="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--Commented on 12March,not required for Asyn.pricing
                            <asp:UpdateProgress AssociatedUpdatePanelID="pnlReprice" runat="server" ID="prgReprice">
                                <ProgressTemplate>
                                    <div class="block" style="position: absolute; top: 390; left: 120; width: 550px;
                                        height: 80px;" id="progress">
                                        <asp:UpdatePanel ID="upnlImg" runat="server">
                                            <ContentTemplate>
                                                <img alt="Loading" src="Images/ajax-loader7.gif" style="width: 50px; height: 35px;" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                        </td>
                        <td align="left" valign="top" class="Filter" style="border-top-width: 0px; border-left-width: 0px;" runat=server id='tdCommentry'>
                            <table>
                                <tr>
                                    <td colspan="2" align="left" style="text-align: left;">
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
                                                    <asp:ListItem Text="BNPP" Value="BNPP" />
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
                                    <td colspan="2" class="lbl" align="left" style="text-align: left; padding: 2px 0px 0px 2px !important">
                                        <div style="vertical-align: top; width: auto; float: left; text-align: left; vertical-align: bottom;">
                                            <asp:UpdatePanel ID="upnlMsg" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblerror" runat="server" CssClass="lbl" ForeColor="red" Style="width: auto;padding-left:22px;"></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="lblMsgPriceProvider" runat="server" CssClass="lbl" ForeColor="Red"
                                                        Style="width: auto;"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
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
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left">
                            <asp:UpdatePanel ID="upnlGrid" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
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
                                                            <telerik:RadDropDownList ID="ddlSelfGrp" runat="server" CssClass="ddl" Width="100px">
                                                                <Items>
                                                                    <telerik:DropDownListItem Text="Self" Value="Self" Selected="True" />
                                                                    <telerik:DropDownListItem Text="Group" Value="Group" />
                                                                    <telerik:DropDownListItem Text="All" Value="All" />
                                                                </Items>
                                                            </telerik:RadDropDownList>
                                                        </td>
                                                        <td style="width: 10px">
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
                                                        <td style="width: 10px">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFont" runat="server" CssClass="lbl" Text="Font-Size" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFont" runat="server" CssClass="ddl" AutoPostBack="true"
                                                                Width="50px" Visible="false">
                                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="9" Value="9" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTotalRows" runat="server" CssClass="lbl" Text="Max Records To Fetch: "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTotalRows" runat="server" CssClass="txt" Width="30px" Text="10"
                                                                AutoPostBack="true" MaxLength="4"></asp:TextBox>
                                                        </td>
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
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Ext. Order ID" DataField="Order_ID" SortExpression="Order_ID"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%-- <asp:BoundColumn HeaderText="Ext. RFQ ID" DataField="EP_ExternalQuoteId" SortExpression="EP_ExternalQuoteId"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>--%>
                                                            <asp:BoundColumn HeaderText="Order ID" DataField="EP_InternalOrderID" SortExpression="EP_InternalOrderID"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="Order Details" HeaderStyle-ForeColor="Black" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRightBorder"/>
                                                                <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnOrder_Details"  class="grdPushBtn"  runat="server" Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C  !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #cccccc" CommandName="GetOrderDetails" Text='Order Details' CausesValidation="False"
                                                                        onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                                        ToolTip="Click to view Order Details."></asp:Button>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn HeaderText="RM Name" DataField="ER_RMName1" SortExpression="ER_RMName1"
                                                                HeaderStyle-ForeColor="White">
                                                                <%--Short Expression Changed By Mohit Lalwani from EP_RMName to ER_RMName on 1-Apr-2016 Jira:EQBOSDEV-309 --%>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Provider" DataField="PP_CODE" SortExpression="PP_CODE"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Type" DataField="ER_Type" SortExpression="ER_Type">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Input Type" DataField="ER_EQO_Quantity_Type" SortExpression="ER_EQO_Quantity_Type"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Order Status" DataField="Field_DisplayAliasName" SortExpression="Order_Status"
                                                                HeaderStyle-ForeColor="White" Visible="true">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Order Type" DataField="ELN_Order_Type" SortExpression="ELN_Order_Type"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Limit Prc1" DataField="LimitPrice1" SortExpression="LimitPrice1"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Limit Prc2" DataField="LimitPrice2" SortExpression="LimitPrice2"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Limit Prc3" DataField="LimitPrice3" SortExpression="LimitPrice3"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Exec. Prc1" DataField="EP_Execution_Price1" SortExpression="EP_Execution_Price1"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Exec. Prc2" DataField="EP_Execution_Price2" SortExpression="EP_Execution_Price2"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Exec. Prc3" DataField="EP_Execution_Price3" SortExpression="EP_Execution_Price3"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Avg Exec. Prc" DataField="EP_AveragePrice" SortExpression="EP_AveragePrice"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%--Ordered Qty--%>
                                                            <asp:BoundColumn HeaderText="Ordered Qty" DataField="Ordered_Qty" SortExpression="Ordered_Qty"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%--Filled Qty--%>
                                                            <asp:BoundColumn HeaderText="Filled Qty" DataField="Filled_Qty" SortExpression="Filled_Qty"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Tenor (mths)" DataField="ER_Tenormths" SortExpression="ER_Tenormths"
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
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Settl. Ccy" DataField="ER_Quanto_Currency" SortExpression="ER_Quanto_Currency"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Settl. Type" DataField="ER_SettlementType" SortExpression="ER_SettlementType"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Fixing Type" DataField="ER_ExerciseType" SortExpression="ER_ExerciseType"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%-- <asp:BoundColumn HeaderText="Order Status" DataField="Order_Status" SortExpression="Order_Status"
                                                                HeaderStyle-ForeColor="White" Visible="true">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>--%>
                                                            <asp:BoundColumn HeaderText="Premium (%)" DataField="EP_OfferPrice" SortExpression="EP_OfferPrice"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Barr. level" DataField="ER_BarrierPercentage" SortExpression="ER_BarrierPercentage"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Barr.Type" DataField="ER_BarrierMonitoringMode" SortExpression="ER_BarrierMonitoringMode"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Option Type" DataField="ER_SecuritySubType" SortExpression="ER_SecuritySubType"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Strike(%)" DataField="EP_StrikePercentage" SortExpression="EP_StrikePercentage"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Upfront(%)" DataField="EP_Upfront" SortExpression="EP_Upfront"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%--<asp:BoundColumn HeaderText="Coupon(%)" DataField="EP_CouponPercentage" SortExpression="EP_CouponPercentage"
                                                            HeaderStyle-ForeColor="White">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:BoundColumn>--%>
                                                            <%--Added client price,yield,upfront 11April --%>
                                                            <asp:BoundColumn HeaderText="Client Premium(%)" DataField="EP_Client_Price" SortExpression="EP_Client_Price">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%--<asp:BoundColumn HeaderText="Client Yield(%)p.a" DataField="EP_Client_Yield" SortExpression="EP_Client_Yield">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:BoundColumn>--%>
                                                            <asp:BoundColumn HeaderText="Upfront(%)" DataField="EP_RM_Margin" SortExpression="EP_RM_Margin">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%--/Added client price,yield,upfront 11April --%>
                                                            <asp:BoundColumn HeaderText="Notional Amount" DataField="EP_Notional_Amount1" SortExpression="EP_Notional_Amount1"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Issuer Order Remark" DataField="EP_Order_Remark1" SortExpression="EP_Order_Remark1"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Booking Branch" DataField="EP_Deal_Booking_Branch" SortExpression="EP_Deal_Booking_Branch"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Order Requested At" DataField="ER_TransactionTime" SortExpression="ER_TransactionTime"
                                                                DataFormatString="{0:dd-MMM-yyyy, HH:mm:ss tt}" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Trade Date" DataField="ER_TradeDate" SortExpression="ER_TradeDate"
                                                                DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Settl. Date" DataField="SettlmentDate" SortExpression="SettlmentDate"
                                                                DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Expiry Date" DataField="ExpiryDate" SortExpression="ExpiryDate"
                                                                DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Mat. Date" DataField="MaturityDate" SortExpression="MaturityDate"
                                                                DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Tenor" DataField="ER_Tenor" SortExpression="ER_Tenor"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Solve For" DataField="ER_SolveFor" SortExpression="ER_SolveFor"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Value" DataField="ER_Issuer_Date_Offset" SortExpression="ER_Issuer_Date_Offset"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Created By" DataField="ER_Created_By" SortExpression="created_by"
                                                                HeaderStyle-ForeColor="White" Visible="False">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Client side" DataField="ER_BuySell" SortExpression="ER_BuySell"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Strike Type" DataField="ER_StrikeType" SortExpression="ER_StrikeType"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Order Comment" DataField="EP_OrderComment" SortExpression="EP_OrderComment"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="true" Width="250px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                        <PagerStyle Mode="NumericPages" CssClass="GridPager " />
                                                    </asp:DataGrid>
                                                    <asp:DataGrid ID="grdEQORFQ" runat="server" CssClass="Grid" PageSize="10" AllowSorting="true"
                                                        AutoGenerateColumns="False" AllowPaging="true" GridLines="None" Width="100%">
                                                        <FooterStyle CssClass="GridFooter "></FooterStyle>
                                                        <ItemStyle CssClass="GridItem  " />
                                                        <SelectedItemStyle CssClass="GridItemSelect" />
                                                        <AlternatingItemStyle CssClass="AlternatItemStyle " />
                                                        <HeaderStyle CssClass="GridHeaderTitle " />
                                                        <PagerStyle CssClass="GridPager " />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="RFQ ID" SortExpression="ER_QuoteRequestId" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkRFQID" runat="server" CommandName="Select" Text='<%# Bind("ER_QuoteRequestId") %>'
                                                                        CausesValidation="False" Font-Underline="true" ForeColor="Blue">ER_QuoteRequestId</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="RFQ Details" HeaderStyle-ForeColor="Black" Visible="false" >
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRightBorder"/>
                                                                <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnRFQ_Details" runat="server" class="grdPushBtn"  Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C  !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #cccccc" CommandName="GetRFQDetails" Text='RFQ Details' CausesValidation="False"
                                                                        onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                                        ToolTip="Click to view RFQ Details."></asp:Button>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                           <asp:TemplateColumn HeaderText="Generate Document" Visible="true">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="GridRightBorder" />
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGenerateDoc" runat="server" Style="background-image: none; background-color: #F2F2F3 !important;
                                                            color: #4D4C4C  !important; border-width: 1px !important; font-size: 12px; border-style: solid;
                                                            border-color: #cccccc" CommandName="GENERATEDOCUMENT" Text='Generate Document' CausesValidation="False"
                                                            onmouseover="OnHover(this);" onmouseout="OnOut(this);" onblur="OnOut(this);"
                                                            ToolTip="Click to generate document." class="grdPushBtn"></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                            <asp:BoundColumn HeaderText="Solve For" DataField="ER_SolveFor" SortExpression="ER_SolveFor"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Client side" DataField="ER_BuySell" SortExpression="ER_BuySell"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Provider" DataField="PP_CODE" SortExpression="PP_CODE"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%--Commented by Mohit L. on 12-May-2015--%>
                                                            <%--<asp:BoundColumn HeaderText="Price(%)" DataField="EP_OfferPrice" SortExpression="EP_OfferPrice"
                                                            HeaderStyle-ForeColor="White">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        </asp:BoundColumn>--%>
                                                            <asp:BoundColumn HeaderText="Type" DataField="ER_Type" SortExpression="ER_Type" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Share" DataField="ER_UnderlyingCode" SortExpression="ER_UnderlyingCode"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Product" DataField="ER_UnderlyingProduct" SortExpression="ER_UnderlyingProduct"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Strike Type" DataField="ER_StrikeType" SortExpression="ER_StrikeType"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Strike" DataField="ER_StrikePercentage" SortExpression="ER_StrikePercentage"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Upfront (%)" DataField="ER_Upfront" SortExpression="ER_Upfront"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%--ER_Premium--%>
                                                            <asp:BoundColumn HeaderText="Premium (%)" DataField="EP_OfferPrice" SortExpression="EP_OfferPrice"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Barrier level" DataField="ER_BarrierPercentage" SortExpression="ER_BarrierPercentage"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Barr.Type" DataField="ER_BarrierMonitoringMode" SortExpression="ER_BarrierMonitoringMode"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Tenor (mths)" DataField="ER_Tenormths" SortExpression="ER_Tenormths"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Tenor" DataField="ER_Tenor" SortExpression="ER_Tenor"
                                                                HeaderStyle-ForeColor="White" Visible="false">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Notional Ccy" DataField="ER_CashCurrency" SortExpression="ER_CashCurrency"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Underlying Ccy" DataField="ER_UnderlyingCcy" SortExpression="ER_UnderlyingCcy"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Settl. Ccy" DataField="ER_Quanto_Currency" SortExpression="ER_Quanto_Currency"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Settl. Type" DataField="ER_SettlementType" SortExpression="ER_SettlementType"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Fixing Type" DataField="ER_ExerciseType" SortExpression="ER_ExerciseType"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Option Type" DataField="ER_SecuritySubType" SortExpression="ER_SecuritySubType"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <%--<asp:BoundColumn HeaderText="No. of shares" DataField="ER_Nominal_Amount" SortExpression="ER_Nominal_Amount"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>--%>
                                                            <asp:BoundColumn HeaderText="Order Qty." DataField="ER_CashOrderQuantity" SortExpression="ER_CashOrderQuantity"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Input Type" DataField="ER_EQO_Quantity_Type" SortExpression="ER_EQO_Quantity_Type"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Trade Date" DataField="TradeDate" SortExpression="TradeDate"
                                                                DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Settl. Date" DataField="SettlmentDate" SortExpression="SettlmentDate"
                                                                DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Expiry Date" DataField="ExpiryDate" SortExpression="ExpiryDate"
                                                                DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Mat. Date" DataField="MaturityDate" SortExpression="MaturityDate"
                                                                DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Exchange" DataField="ER_Exchange" SortExpression="ER_Exchange"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Value" DataField="ER_Issuer_Date_Offset" SortExpression="ER_Issuer_Date_Offset"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Issuer Remark" DataField="EP_Quote_Request_Rejection_Reason"
                                                                SortExpression="EP_Quote_Request_Rejection_Reason" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Ext. RFQ ID" DataField="EP_ExternalQuoteId" SortExpression="EP_ExternalQuoteId"
                                                                HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Quote Requested At" DataField="ER_TransactionTime" SortExpression="ER_TransactionTime"
                                                                DataFormatString="{0:dd-MMM-yyyy hh:mm:ss tt}" HeaderStyle-ForeColor="White">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Common RFQ Id" DataField="ClubbingRFQId" SortExpression="ClubbingRFQId"
                                                                HeaderStyle-ForeColor="White" Visible="False">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Created By" DataField="ER_Created_By" SortExpression="ER_Created_By"
                                                                HeaderStyle-ForeColor="White" Visible="False">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Wrap="false" HorizontalAlign="Center" VerticalAlign="Middle" />
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
                                    <%--                                    <asp:AsyncPostBackTrigger ControlID="grdELNRFQ" EventName="SortCommand" />--%>
                                    <asp:AsyncPostBackTrigger ControlID="grdEQORFQ" EventName="SortCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="grdOrder" EventName="SortCommand" />
                                    <%--                                   <asp:AsyncPostBackTrigger ControlID="grdDRAFCN" EventName="SortCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="grdAccumDecum" EventName="SortCommand" />--%>
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
                            <asp:Label ID="lblProductNamePopUpValue" runat="server" Text="Equity Options" Style="background-color: Transparent;
                                color: White; font-size: larger; text-decoration: underline;"></asp:Label></h1>
                    </div>
                    <table cellpadding="2px" cellspacing="4px" width="490px" class="Filter">
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblIssuerPopUpCaption" runat="server" Text="Issuer" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="left">
                                <asp:Label ID="lblPopupSCBPPValue" runat="server" Text="SCB " CssClass="lblBOLD" Visible ="false" ></asp:Label>
                                <asp:Label ID="lblIssuerPopUpValue" runat="server" Text="BNPP" CssClass="lblBOLD"></asp:Label>
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
                                <telerik:RadDropDownList ID="ddlRM" runat="server" CssClass="ddl" Style="width: 100% !important"
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
                                <telerik:RadDropDownList ID="ddlBookingBranchPopUpValue" runat="server" Width="100px" Enabled="true"
                                    CssClass="ddl">
                                    <%--<asp:ListItem Text="Hong Kong" Value="HK"></asp:ListItem>
                                    <asp:ListItem Text="Singapore" Value="SG" Selected="True"></asp:ListItem>--%>
                                    <%--Commented By Nikhil M For Dyanic Adding Element 08Aug2016  EQSCB-16--%>
                                </telerik:RadDropDownList>
                            </td>
                            <td class="lbl" id="tdPopUpShares" runat="server">
                                <asp:Label ID="lblNoOfSharePopUpCaption" runat="server" Text="No. of Shares" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right" id="tdPopUpSharesValue" runat="server">
                                <asp:Label ID="lblNoOfSharePopUpValue" runat="server" Text="0" CssClass="lblBOLD"></asp:Label>
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
                                <asp:Label ID="lblStrikePopUpCaption" runat="server" Text="Strike" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblStrikePopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>
                            </td>
                            <td class="lbl" nowrap="nowrap">
                                <asp:Label ID="lblTenorPopUpCaption" runat="server" Text="Tenor" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblTenorPopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>&nbsp;<asp:Label
                                    ID="lblTenorTypePopUpValue" runat="server" CssClass="lblBOLD"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblUpfrontPopUpCaption" runat="server" Text="Upfront %" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control">
                                <asp:TextBox ID="txtUpfrontPopUpValue" runat="server" AutoPostBack="true" CssClass="txt"
                                    Style="width: 115px; text-align: right;" MaxLength="6"></asp:TextBox>
                            </td>
                            <td class="lbl" id="tdPopUpNotional" runat="server">
                                <asp:Label ID="lblNotionalPopUpCaption" runat="server" Text="Notional" CssClass="lbl"></asp:Label>
                                <asp:Label ID="lblCurrencyPopUpValue" runat="server" Text="HKD" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="right" class="control" id="tdPopUpNotionalValue" runat="server">
                                <asp:Label ID="lblNotionalPopUpValue" runat="server" Text="0.00" CssClass="lblBOLD"
                                    Style="text-align: right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblIssuerPricePopUpCaption" runat="server" Text="Premium (%)" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblIssuerPricePopUpValue" runat="server" Text="" CssClass="lblBOLD"></asp:Label>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblClientPricePopUpCaption" runat="server" Text="Client Premium (%)"
                                    CssClass="lbl" Style="white-space: nowrap"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblClientPricePopUpValue" runat="server" Text="" CssClass="lblBOLD"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblOrderTypePopUpCaption" runat="server" Text="Order Type" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control">
                                <telerik:RadDropDownList ID="ddlOrderTypePopUpValue" runat="server" AutoPostBack="true"
                                    Width="100px" CssClass="ddl">
                                    <Items>
                                    <telerik:DropDownListItem Text="Market" Value="Market" Selected="true" />
                                    <telerik:DropDownListItem Text="Limit" Value="Limit" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblLimitPricePopUpCaption" runat="server" Text="Limit Level" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="left" style="white-space: nowrap">
                                <telerik:RadDropDownList ID="ddlBasketSharesPopValue" runat="server" CssClass="ddl"
                                    Width="75px">
                                </telerik:RadDropDownList>
                                <asp:TextBox ID="txtLimitPricePopUpValue" runat="server" Text="84" CssClass="txt"
                                    Style="text-align: right" Width="175" AutoPostBack="true" MaxLength="12"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lbl">
                                <asp:Label ID="lblSettlementPopupShow" runat="server" Text="Settlement" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblSettlementPopup" runat="server" Text="" CssClass="lblBOLD"></asp:Label>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="Label1" runat="server" Text="Option Type" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="right">
                                <asp:Label ID="lblOptionPopUpValue" runat="server" Text="" CssClass="lblBOLD"></asp:Label>
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
                        <%--''<Nikhil M. on 28-Sep-2016: >--%>
                        
                        <%--''<AshwiniP on 10-nov-2016:Added For Deal Conformation >--%>
                           <tr id="TblDealReason" runat="server" >
                                            <td>Non-Best Price Reason</td>
                                            <td colspan="3">
                                           <%-- <asp:DropDownList ID="drpConfirmDeal" runat="server"></asp:DropDownList>--%>
                                            <telerik:RadDropDownList CssClass="RadDropDownList RadDropDownList_Default"  ID="drpConfirmDeal" runat="server" Width="360px">  <%--AshwiniP on 11-Nov-2016:To keep order popup width constant--%>
                                            </telerik:RadDropDownList>
                                            </td>
                           </tr>
                           <%--</AshwiniP>--%>
                        
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
                                        <asp:TemplateField HeaderText="CIF/PAN" ItemStyle-Width="150" ItemStyle-CssClass ="grayBorder" HeaderStyle-CssClass ="grayBorder">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCIFPAN" runat="server" Text='<%# Eval("Account_Number") %>'></asp:Label>
                                               <%-- <asp:TextBox ID="txtAccNo" runat="server" Text='<%# Eval("Account_Number") %>' Visible="false"
                                                    CssClass="txt" Width="92%" OnTextChanged="txtBox_onTextChanged" AutoPostBack="True"></asp:TextBox>--%>
                                                  <%--    <telerik:RadDropDownList ID="ddlCIFPAN" runat="server" Width="92%" AutoPostBack =true  OnSelectedIndexChanged="ddlCIFPAN_onTextChanged" >
                                                      </telerik:RadDropDownList>--%>
                                                                                                       <uc2:FinIQ_Fast_Find_Customer ID="FindCustomer" runat="server" DoPostBack="true" OnCustomer_Selected="CustomerSelected"
                                                                                                    EnableTheming="true"  SetWidth="98" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of shares/Notional" ItemStyle-Width="150" ItemStyle-CssClass ="grayBorder" HeaderStyle-CssClass ="grayBorder">
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
                        <tr>
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
                        <tr>
                         <td class="lbl">
                            <asp:Label ID="lblAlloAmt" runat="server" Text="Allocated Notional" CssClass="lbl" Visible="True" style="display:inline-flex;" ></asp:Label>
                            </td>
                            <td>
                            <asp:Label ID="lblAlloAmtVal" runat="server" Text=" " CssClass="lbl" Visible="True"   ></asp:Label>
                            </td>
                        </tr>
                         <tr>
                         <td class="lbl">
                            <asp:Label ID="lblRemainNotional" runat="server" Text="Remaining Notional" CssClass="lbl" Visible="True" style="display:inline-flex;" ></asp:Label>
                            </td>
                            <td>
                            <asp:Label ID="lblRemainNotionalVal" runat="server" Text=" " CssClass="lbl" Visible="True"   ></asp:Label>
                            </td>
                        </tr>
                        <%--</ashwiniP on 20Sept16>--%>
                        <tr style="visibility: hidden; display: none;">
                            <td class="lbl">
                                <asp:Label ID="lblRFQDateCaption" runat="server" Text="FinIQ Maturity" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control">
                                <asp:Label ID="lblRFQDateValue" runat="server" Text="" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="lbl">
                                <asp:Label ID="lblOrderDateCaption" runat="server" Text="LP Maturity" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="control" align="left" style="white-space: nowrap">
                                <asp:Label ID="lblOrderDateValue" runat="server" Text="" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblRFQOrderDateMismatchMsg" runat="server" Text="" CssClass="lbl"
                                    Style="color: Red; background-color: Transparent !important;" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div>
                                    <asp:CheckBox ID="chkUpfrontOverride" runat="server" Visible="false" />
                                    <asp:Label ID="lblerrorPopUp" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:CheckBox ID="chkConfirmDeal" runat="server" Visible="false" Text="Do you want to proceed?"/> <%--''<Nikhil M. on 09-Sep-2016: ></Nikhil>--%>
                                                                      
                                    </div>
                                <div class="clsButton" style="text-align: center; line-height: 25px;">
                                    <input type="button" id="btnDealConfirm" runat="server" value="Confirm" class="btn"
                                        onmouseover="JavaScript:alert:this.focus();" />
                                    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnRedirect"
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
                                        visible="false" style="width: 100px" class="btn" onmouseover="JavaScript:alert:this.focus();" />--%>
                                    <input type="button" id="btnDealCancel" runat="server" value="Cancel" class="btn"
                                        onmouseover="JavaScript:alert:this.focus();" />
                                    <asp:Button ID="btnHdnEnablePage" runat="server" OnClick="EnablePage" Style="visibility: hidden;
                                        display: none" onmouseover="JavaScript:alert:this.focus();" />
                                    <%-- </ContentTemplate>
                                    </asp:UpdatePanel>--%>
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

    <script type="text/javascript">

        function CP_Dragable() {
            $(".ConfirmationPopup").draggable({ containment: "body" }); //Mohit Lalwani on 1-Feb-2016
        }         
    </script>

    <asp:UpdatePanel ID="upnlDetails" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlDetailsPopup" runat="server" CssClass="ConfirmationPopup ui-widget-content ui-draggable"
                Visible="false" Style="top: 250px; left: 550px;">
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
                                <div>
                                    <input id="btnDetailsCancle" style="background-color: #79B7E3 !important" runat="server"
                                        class="btn" onmouseover="JavaScript:alert:this.focus();" type="button" value="X" />
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
                                <asp:Label ID="lblHdrRFQID" runat="server" CssClass="lbl" Text="RFQ ID" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloRFQID" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHdrCounterparty" runat="server" CssClass="lbl" Text="Counterparty"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lclAlloCP" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHdrFixingType" runat="server" CssClass="lbl" Text="Fixing Type"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblFixingType" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHdrOptionType" runat="server" CssClass="lbl" Text="Option Type"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblOptionType" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHdrClientSide" runat="server" CssClass="lbl" Text="Client Side"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblClientSide" runat="server" Text="" />
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
                        <tr id="Tr1" runat="server">
                            <td>
                                <asp:Label ID="lblAlloSolvefor" runat="server" CssClass="lbl" Text="Solve For" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblValAlloSolvefor" runat="server" Text="" />
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
                                <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="Trade Date" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloTradeDt" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="Settle Date" Style="font-weight: bold;" />
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
                                <asp:Label ID="Label15" runat="server" CssClass="lbl" Text="Settlement Days" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloSettWk" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHdrSettlementMethod" runat="server" CssClass="lbl" Text="Settl. Type"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblSettlementMethod" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHdrAlloSettlCcy" runat="server" CssClass="lbl" Text="Settl. Ccy"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloSettlCcy" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="Notional Ccy" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloNoteCcy" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label21" runat="server" CssClass="lbl" Text="No. of Shares" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloOrderSize" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHdrAlloStrikeType" runat="server" CssClass="lbl" Text="Strike Type"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloStrikeType" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" CssClass="lbl" Text="Strike" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloStrike" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHdrAlloKOType" runat="server" CssClass="lbl" Text="Barrier Type"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloKOType" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" CssClass="lbl" Text="Barrier Level" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloKO" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label17" runat="server" CssClass="lbl" Text="Premium (%)" Style="font-weight: bold;" />
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
                        <tr runat="server" id="trClientPremium" visible="false">
                            <td>
                                <asp:Label ID="Label19" runat="server" CssClass="lbl" Text="Client Premium (%)" Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloClientPrice" runat="server" Text="" />
                            </td>
                        </tr>
                        <%--                        <tr>
                            <td>
                                <asp:Label ID="Label20" runat="server" CssClass="lbl" Text="Client Yield (%) p.a."
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblAlloYield" runat="server" Text="" />
                            </td>
                        </tr>--%>
                        <tr id='trQuoteStatus' runat="server">
                            <td>
                                <asp:Label ID="lblQuoteStatus" runat="server" CssClass="lbl" Text="Quote Status"
                                    Style="font-weight: bold;" />
                            </td>
                            <td>
                                <asp:Label ID="lblValQuoteStatus" runat="server" Text="" />
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
                                <asp:Label ID="lblAlloRemark" runat="server" Text="" />
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
