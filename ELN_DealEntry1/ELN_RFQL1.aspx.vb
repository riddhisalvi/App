Imports System.Windows
Imports System.Drawing
Imports System.Web.UI.DataVisualization.Charting
Imports System.Threading
Imports System.ComponentModel
Imports System.Xml
Imports Telerik.Web.UI
Imports System.Net.Mail
Imports System.IO
Imports System.Net

Partial Public Class ELN_RFQL1
    Inherits FinIQAppMain
    Private Const sSelfPath As String = "FinIQWebApp/ELN_DealEntry1/ELN_RFQL1.aspx.vb"
    Dim oWEBADMIN As WEB_ADMINISTRATOR.LSSAdministrator
    Dim oWEBMarketData As Web_FinIQ_MarketData.FINIQ_WEBSRV_MarketData
    Private oWebCustomerProfile As Web_CustomerProfile.CustomerProfile
    Private Const sPoolRedirectionPath As String = "../ELN_OrderPool/ELN_OrderPool.aspx?menustr=EQ%20Sales%20-%20ELN%20Order%20Pool&Mode=&token="
    Dim objELNRFQ As Web_ELNRFQ.ELN_RFQ
    Dim oTRDSS As Web_TRDSS.TRSetup
    Dim ObjCommanData As Web_CommonFunction.CommonFunction '' Added By Nikhil M 08Aug16 For Booking Brach DropDown EQSCB-16
    Dim generateDocument As Web_DocumentGeneration.GenerateDocumentsForWeb
    Private Enum prdTab
        ELN
        FCN
        DRA
        Acc
        Dec
        EQO
    End Enum
#Region "Variables"
    'Private udtStructured_Product_Tranche As Structured_Product_Tranche_ELN
    Dim _DockingLocation As System.Web.UI.DataVisualization.Charting.Docking = DataVisualization.Charting.Docking.Right
    Dim _ChartBackGradientStyle As System.Web.UI.DataVisualization.Charting.GradientStyle = DataVisualization.Charting.GradientStyle.None
    Dim _ChartBorderlineDashStyle As System.Web.UI.DataVisualization.Charting.ChartDashStyle = DataVisualization.Charting.ChartDashStyle.NotSet
    Dim _ChartBorderSkinStyle As System.Web.UI.DataVisualization.Charting.BorderSkinStyle = DataVisualization.Charting.BorderSkinStyle.None
    Dim strBaseCcy As String
    Dim strShare As String
    Dim strBarrier As String
    Dim strStrike As String
    Dim strGetDate As String
    Dim strTenor As String
    'Dim strJPMId As String '<AvinashG. on 27-Jan-2016: >
    Dim strQuantoCcy As String
    Dim strHSBCPrice As String
    Dim strOCBCPrice As String
    Dim strCITIPrice As String
    Dim strCSPrice As String
    Dim strAmount As String
    Dim strXMLNote_RFQ As String
    Dim strEntityId As String
    Dim flag As String
    Public flag1 As Boolean = False
    Dim JPM_ID As String
    Dim UBS_ID As String
    Dim HSBC_ID As String
    Dim BNPP_ID As String  ' GS iS Renamed to BNPP as per BOS Requirement by Mohit on 11-JUN-2015
    Dim BAML_ID As String
    Dim DBIB_ID As String
    Dim OCBC_ID As String
    Dim CITI_ID As String
    Dim LEONTEQ_ID As String
	Dim COMMERZ_ID As String
    Dim strcount As String
    Dim CS_ID As String
    Dim strJPMPrice As String
    Dim StrnoteRFQXML As String
    Dim SchemeName As String
    ''Dim schemeCode As String   ''commented by Kalyan on 13-March-2014
    Dim templateCode As String
    Dim strHardTenor As String
    Dim strPPID As String
    Dim EP_ID As String
    Dim ER_ID As String
    Dim EP_ER_QuoteRequestId As String
    Dim ER_QuoteRequestId As String
    Dim Tenor As Integer = 0
    Dim Quote_ID As String
    Dim PP_CODE As String
    Public Const ChartFont As String = "Arial"
    Public Const ChartTitleSize As Double = 7.0F
    Dim numberdiv As Double = 0
    Public arr(3) As String
    Dim strUPDOWN As String = ""
    Dim tabIndex As Integer
    Dim getAllId As Hashtable
    Dim getPPId As Hashtable
    Dim WebServicePath As String
    Dim sEQC_DealerLoginGroups As String
    ''--

    Dim interval As String '  Added by Chitralekha 12-Sept-16
    Public Structure ChartColors
        Dim JPM As Color
        Dim CS As Color
        Dim UBS As Color
        Dim HSBC As Color
        Dim FINIQ As Color
        Dim BAML As Color
        Dim BNPP As Color  ' GS iS Renamed to BNPP as per BOS Requirement by Mohit on 11-JUN-2015
        Dim DBIB As Color
        Dim OCBC As Color
        Dim CITI As Color
        Dim LEONTEQ As Color
        Dim COMMERZ As Color
    End Structure
    Public Shared structChartColors As ChartColors

    'Public Structure Structured_Product_Tranche_ELN
    '    Public strExchange As String
    '    Public strEntityName As String
    '    Public strTemplateName As String   ''ELN
    '    Public lngTemplateId As String ''ELN/DRA/AQ-DQ
    '    'Dim strExchange As String   ''ELN
    '    Public strTradeDate As String  ''ELN
    '    Public strValueDate As String  ''ELN
    '    Public strFxingdate As String  ''ELN
    '    Public strMaturityDate As String   ''ELN
    '    Public strCurrency As String   ''ELN
    '    Public strAsset As String  ''ELN
    '    Public strELNType As String    ''ELN/DRA/AQ-DQ
    '    Public strTenorType As String  ''ELN
    '    Public dblStrike1 As Double    ''ELN
    '    Public dblStrike2 As Double    ''ELN/DRA/Aq-DQ
    '    Public strPrice As Double  ''ELN/DRA
    '    Public strPrice1 As Double ''ELN/DRA
    '    Public dblBarrier As String    ''ELN
    '    Public strRemark As String ''ELN
    '    Public dblNominalAmount As Double  ''ELN
    '    Public inttenor As Integer ''ELN
    '    Public strIssuer_Date_Offset As String ''variable to store the value of offset days i.e. (T+2,T+7,....) ''ELN
    '    Public strBranchName As String     ''ELN
    '    Public strSecurityDesc As String
    '    ''Public lngOrderQty As Long   ''KBM on 28-April-2014
    '    Public strOrderQty As String    ''KBM on 28-April-2014
    '    Public strSolveFor As String   ''ELN/DRA/AQ-DQ
    '    Public strAssetclass As String ''ELN/DRA/AQ-DQ
    '    '<AvinashG. on 17-Dec-2014: FA-768 	Move RM dropdown from pricer page to order popup >
    '    Public strRFQRMName As String
    '    'Public strRMName As String
    '    '</AvinashG. on 17-Dec-2014: FA-768 	Move RM dropdown from pricer page to order popup >
    '    Public strEmailId As String
    '    Public strBranch As String
    '    Public strDRAFCNTenorType As String    ''DRA
    '    Public strUnderlyingCode As String ''DRA/Aq-DQ
    '    Public strExchangeDRA As String        ''DRA
    '    Public strCurrencyDRA As String    ''DRA
    '    Public inttenorDRA As Integer  ''DRA
    '    Public strGuaranteedDuration As String ''DRA/Aq-DQ
    '    Public strGuaranteedDurationType As String ''DRA/AQ-DQ
    '    Public strKIType As String ''DRA
    '    Public strKILevel As Double    ''DRA
    '    Public strKIObsFreq As String  ''DRA
    '    Public strCoupon As Double ''DRA
    '    Public strCoupon1 As Double    ''DRA
    '    Public strKOType As String ''DRA/Aq-DQ
    '    Public strKOLevel As Double    ''DRA
    '    Public strOTCYN As String  ''DRA
    '    Public dblStrikePrice As Double    ''AQ/DQ
    '    Public strAccumTenorType As String ''AQ/DQ
    '    Public strExchangeAccum As String  ''AQ/DQ
    '    Public strKoPerc As String ''AQ/DQ
    '    Public strUpfront As Double    ''Aq/DQ
    '    Public strUpfront1 As Double    ''Aq/DQ
    '    Public inttenorAccum As Integer    ''AQ/DQ
    '    Public strLeverageratio As String  ''AQ/DQ
    '    Public strFrequency As String  ''AQ/DQ
    '    Public strAQDQCcy As String
    '    Public dblRMMargin As Double ''KBM on 29-April-2014
    'End Structure
    ''--
    Dim hitCount As Integer = 0
    Dim orderValidityTimer As Integer = 0 'In Seconds
#End Region
#Region "Enum"

    Enum grdELNRFQEnum
        RFQ_ID
        CreatePool '<AvinashG. on 09-Apr-2015: Added for BoS>
        RFQ_Details '<Mohit Lalwani on 20-Jan-2015: Added for BoS>
        GenerateDoc
        Solve_For
        Provider_Name
        Price_Per
        Type
        Share
        Strike_Per
        Tenor
        RFQTenor
        Base
        Order_Quantity
        Trade_Date
        Settlement_Date
        Expiry_Date
        Maturity_Date
        Exchange
        Value
        Barrier_Type
        Barrier
        Remark
        External_RFQ_ID
        Quoted_At
        ClubbingRFQId
        created_by
        Upfront
        Quote_Status
        ClientPrice
        ClientYield
        BestPrice_YN
    End Enum
    Enum grdOrderEnum
        ER_QuoteRequestId
        Order_ID
        EP_InternalOrderID
        Order_Details
        GenerateDoc
        ER_RMName
        PP_CODE
        Order_Status
        ELN_Order_Type
        LimitPrice1
        LimitPrice2
        LimitPrice3
        EP_Execution_Price1
        EP_Execution_Price2
        EP_Execution_Price3
        EP_AveragePrice
        Ordered_Qty
        Filled_Qty
        ER_Tenor
        ER_UnderlyingCode
        ER_CashCurrency
        ER_LeverageRatio
        ER_GuaranteedDuration
        EP_StrikePercentage
        EP_KO
        EP_CouponPercentage
        EP_OfferPrice
        EP_RM_Margin
        EP_Upfront
        EP_Client_Price
        EP_Client_Yield
        EP_Notional_Amount1
        EP_Order_Remark1
        EP_ExternalQuoteId
        EP_Deal_Booking_Branch
        ER_TransactionTime
        EP_HedgedFor
        EP_HedgingOrderId
        ExpiryDate
        Barrier
        Barrier_Type
        MaturityDate
        Settlement_Date
        Value
        created_by
        TradeDate
        SolveFor
        EP_OrderComment
    End Enum
    Enum enumPoolDetails
        Exchange
        Share
        SolveFor
        StrikePercentage
        PricePercentage
        Upfront
        PoolMinimum
        PoolMaximum
        MinOrderSize
        MinIncrement
        Tenor
        TenorType
        ELNType
        SettlementWeeks
        SettlementDays
        Currency
        BarrierPercentage
        BarrierType
        TradeDate
        SettlementDate
        ExpiryDate
        MaturityDate
        RecommendedFlag
        Notional
        PPCODE
        RFQID
        PPID
        LockVal
        EOP_OrderPoolCode
        BookingCenter '<RiddhiS. on 10-Nov-2016>
    End Enum
    Enum RedirectOrderDetails
        Exchange
        Share
        SolveFor
        StrikePercentage
        PricePercentage
        Upfront
        Tenor
        TenorType
        ELNType
        SettlementWeeks
        SettlementDays
        Currency
        Notional
        BarrierPercentage
        BarrierType
        TradeDate
        SettlementDate
        ExpiryDate
        MaturityDate
    End Enum

    ''<Rushikesh on 20Sept16>
    Enum grdRMDataEnum
        chkAll
        RM_Name
        Account_Number
        Notional
        OrderId
    End Enum

#End Region
#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtPersonalSettings As DataTable
      
        Dim dtExchange As DataTable
        Try
            Session.Remove("Scheme")
            WebServicePath = String.Empty
            WebServicePath = ConfigurationManager.AppSettings("EQSP_WebServiceLocation").ToString
            WebServicePath = Request.Url.Scheme & Uri.SchemeDelimiter & WebServicePath
            objELNRFQ = New Web_ELNRFQ.ELN_RFQ
            objELNRFQ.Url = WebServicePath & "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx"
            ''Rushikesh 14Jan2016:- For Share load on lazyload
            '<AvinashG. on 21-Oct-2016:EQSCB-79 - URL schema binding on Share search>
            ddlShare.WebServiceSettings.Path = WebServicePath & "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            'ddlShare.WebServiceSettings.Path = "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            '</AvinashG. on 21-Oct-2016:EQSCB-79 - URL schema binding on Share search>
            ddlShare.WebServiceSettings.Method = "GetSharesNames"
            '<AvinashG. on 02-Feb-2016:  FA-1286 - Display User Limit and mail to dealer desk(email id based on config)>
            oWEBADMIN = New WEB_ADMINISTRATOR.LSSAdministrator()
            oWEBADMIN.Url = LoginInfoGV.Login_Info.WebServicePath & "/LSSAdministrator/LSSAdministrator.asmx"


            ''<Email Doc gen Services>
            generateDocument = New Web_DocumentGeneration.GenerateDocumentsForWeb
            generateDocument.Url = LoginInfoGV.Login_Info.WebServicePath & "/DocumentGeneration/DocumentGeneration.asmx"
            generateDocument.Credentials = System.Net.CredentialCache.DefaultCredentials
            ''</Email Doc gen Services>

            oWEBMarketData = New Web_FinIQ_MarketData.FINIQ_WEBSRV_MarketData()
            oWEBMarketData.Url = LoginInfoGV.Login_Info.WebServicePath & "/WebELN_DealEntry/FINIQ_WEBSRV_MarketData.asmx"

            ''<Nikhil M. on 22-Sep-2016: Added>
            ObjCommanData = New Web_CommonFunction.CommonFunction
            ObjCommanData.Url = LoginInfoGV.Login_Info.WebServicePath & "/CommonFunction/CommonFunction.asmx"


            oWebCustomerProfile = New Web_CustomerProfile.CustomerProfile()
            oWebCustomerProfile.Url = LoginInfoGV.Login_Info.WebServicePath & "/Customer_Profile/CustomerProfile.asmx"


            hitCount = CInt(objReadConfig.ReadConfig(dsConfig, "FINIQ_ELN_RFQ", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "30"))
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "pollingTimeJS", "var pollingMilliSec = " + hitCount.ToString + ";", True)
            orderValidityTimer = CInt(objReadConfig.ReadConfig(dsConfig, "EQConnect_Order_Validity_Timer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "30"))
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "orderValidityTimeJS", "var orderValiditySec = " + orderValidityTimer.ToString + ";", True)
            '  For Each Item As DataListItem In grdRMData.Items


            'Mohit Lalwani on 27-Sept-2016
            For Each row As GridViewRow In grdRMData.Rows
                Dim FindCustomer As FinIQ_Fast_Find_Customer
                FindCustomer = CType(row.FindControl("FindCustomer"), FinIQ_Fast_Find_Customer)
                FindCustomer.CA_Filter = Fast_ICustomer_Search.EN_Customer_Or_Account_Filter.Account
                FindCustomer.UnVerified = FinIQApp_Web_Const.EN_ActivationFilter.Exclude
                FindCustomer.Customer_Filter = Fast_ICustomer_Search.EN_Customer_Filter.ENTITY_LRC_SELECTED_RM   'Mohit Lalwani on 22-Oct-2016
                FindCustomer.Entity_ID = LoginInfoGV.Login_Info.EntityID
                FindCustomer.Login_Id = LoginInfoGV.Login_Info.LoginId
                FindCustomer.WebCustomerProfile = oWebCustomerProfile
                FindCustomer.SSICheck = False
                FindCustomer.SetItemIndex = row.RowIndex
                'FindCustomer.setCustName = setCustName(CType(row.FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).GetCustName)
                FindCustomer.RM_ID = (CType(row.Cells(grdRMDataEnum.RM_Name).FindControl("ddlRMName"), RadDropDownList)).SelectedItem().Text

                ''Change done by PriyaB: 05Nov2016
                ''Pass booking center as filter to customer find control
                FindCustomer.CustomerBookingCenter = ddlBookingBranchPopUpValue.SelectedValue
            Next
            '/Mohit Lalwani on 27-Sept-2016




            If Page.IsPostBack = False Then
                Session.Remove("dtELNPreTradeAllocation")
                Session.Remove("dtCustomerCodes")
                'Mohit Lalwani on 7-Sept-2016 for Personal Settings
                dtPersonalSettings = New DataTable("Personal Settings")
                'ObjCommanData = New Web_CommonFunction.CommonFunction
                Select Case ObjCommanData.Web_Get_DefaultPersonalSettingsInfo(LoginInfoGV.Login_Info.EntityID, LoginInfoGV.Login_Info.LoginId, "ELN_DealEntry", dtPersonalSettings)
                    Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                        Session.Add("dtPersonalSettings", dtPersonalSettings)
                    Case Web_CommonFunction.Database_Transaction_Response.Db_No_Data
                        Session.Add("dtPersonalSettings", dtPersonalSettings)
                    Case Web_CommonFunction.Database_Transaction_Response.DB_Unsuccessful
                        Session.Add("dtPersonalSettings", dtPersonalSettings)
                End Select
                '/Mohit Lalwani on 7-Sept-2016 for Personal Settings
                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ColorGrayedOut", " $('#ctl00_MainContent_tabContainer_tabPanelELN_txtTradeDate_txtDate').css('background-color', '#D3D3D3');", True)

                'Mohit Lalwani on 1-July-2016

                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_ShowEQOTab", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                        'Dim sLoginGrp As String
                        'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
                        'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                        If UCase(Request.QueryString("Mode")) = "ALL" Then
                            tabContainer.Tabs(prdTab.EQO).Visible = True
                        Else
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQO_ShowEQOTabToRM", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                    tabContainer.Tabs(prdTab.EQO).Visible = True
                                Case "N", "NO"
                                    tabContainer.Tabs(prdTab.EQO).Visible = False
                            End Select
                        End If
                    Case "N", "NO"
                        tabContainer.Tabs(prdTab.EQO).Visible = False
                End Select

                'Select Case objReadConfig.ReadConfig(dsConfig, "EQO_ShowEQOTab", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                '    Case "Y", "YES"
                '        tabContainer.Tabs(prdTab.EQO).Visible = True
                '    Case "N", "NO"
                '        tabContainer.Tabs(prdTab.EQO).Visible = False
                'End Select
                '/Mohit Lalwani on 7-July-2016

                If ((objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper = "NO" Or _
                     objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper = "N") And _
                     (objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "NO" Or _
                     objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "N") And _
                     (objReadConfig.ReadConfig(dsConfig, "EQC_Show_ELN_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "YES" Or _
                     objReadConfig.ReadConfig(dsConfig, "EQC_Show_ELN_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "Y")) Then
                    rblShareData.SelectedValue = "calc"
                    rowELNCalculator.Visible = True
                    rblShareData.Visible = False
                    tdrblShareData.Visible = False '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
                    rowGraphData.Visible = False
                    rowShareData.Visible = False
                Else
                    Dim count As Integer = 0
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                        Case "Y", "YES"
                            count = count + 1
                        Case "N", "NO"
                            rblShareData.SelectedValue = "GRAPHDATA"
                            rowShareData.Visible = False
                            rowGraphData.Visible = True
                            rblShareData.Items.FindByValue("SHAREDATA").Attributes.Add("style", "display:none")
                    End Select
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                        Case "Y", "YES"
                            count = count + 1
                        Case "N", "NO"
                            rblShareData.SelectedValue = "SHAREDATA"
                            rowGraphData.Visible = False
                            rblShareData.Items.FindByValue("GRAPHDATA").Attributes.Add("style", "display:none")
                    End Select
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Show_ELN_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                        Case "Y", "YES"
                            count = count + 1
                        Case "N", "NO"
                            rowELNCalculator.Visible = False
                            rblShareData.Items.FindByValue("calc").Attributes.Add("style", "display:none")
                    End Select

                    If count = 2 Or count = 3 Then
                        rblShareData.Visible = True
                        tdrblShareData.Visible = True '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
                    Else
                        rblShareData.Visible = False
                        tdrblShareData.Visible = False '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
                    End If

                End If
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        tblRowSelectionExchangeELN.Visible = False
                        tblRowDisplayExchangeELN.Visible = True
                        ddlExchange.Visible = False
                    Case "N", "NO"
                        tblRowSelectionExchangeELN.Visible = True
                        tblRowDisplayExchangeELN.Visible = False
                End Select
                
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowPoolCreation_From_Pricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper()
                    Case "Y", "YES"
                        'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                        'Dim sLoginGrp As String
                        'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
                        'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                        If UCase(Request.QueryString("Mode")) = "ALL" Then
                            grdELNRFQ.Columns(grdELNRFQEnum.CreatePool).Visible = True
                           
                            Else
                            grdELNRFQ.Columns(grdELNRFQEnum.CreatePool).Visible = False
                        End If
                    Case "N", "NO"
                        grdELNRFQ.Columns(grdELNRFQEnum.CreatePool).Visible = False
                End Select
                'Added by Mohit Lalwani on 20-Jan-2015


                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_CaptureOrderComment", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        grdOrder.Columns(grdOrderEnum.EP_OrderComment).Visible = True
                    Case "N", "NO"
                        grdOrder.Columns(grdOrderEnum.EP_OrderComment).Visible = False
                End Select


                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowRFQandOrderDetails", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper()
                    Case "Y", "YES"
                        grdELNRFQ.Columns(grdELNRFQEnum.RFQ_Details).Visible = True
                    Case "N", "NO"
                        grdELNRFQ.Columns(grdELNRFQEnum.RFQ_Details).Visible = False
                End Select

                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowRFQandOrderDetails", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper()
                    Case "Y", "YES"
                        grdOrder.Columns(grdOrderEnum.Order_Details).Visible = True
                    Case "N", "NO"
                        grdOrder.Columns(grdOrderEnum.Order_Details).Visible = False
                End Select
                '/Added by Mohit Lalwani on 20-Jan-2015

                Dim sEQC_DealerRedirection_OnPricer As String = objReadConfig.ReadConfig(dsConfig, "EQC_DealerRedirection_OnPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO")
                Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                    Case "Y", "YES"
                        'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                        'Dim sLoginGrp As String
                        'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup

                        'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                        If UCase(Request.QueryString("Mode")) = "ALL" Then
                            ''USer is Dealer
                            grdOrder.Columns(grdOrderEnum.EP_HedgedFor).Visible = True
                            grdOrder.Columns(grdOrderEnum.EP_HedgingOrderId).Visible = False
                        Else
                            ''User is RM
                            '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
                            div_RM_Limit.Visible = True
                            setRMLimit()
                            '</Added By Mohit Lalwnai on 1-Apr-2016>
                            grdOrder.Columns(grdOrderEnum.EP_HedgedFor).Visible = True
                            grdOrder.Columns(grdOrderEnum.EP_HedgingOrderId).Visible = True
                        End If
                    Case "N", "NO"
                        grdOrder.Columns(grdOrderEnum.EP_HedgedFor).Visible = False
                        grdOrder.Columns(grdOrderEnum.EP_HedgingOrderId).Visible = False
                End Select
                btnCancelReq.Text = objReadConfig.ReadConfig(dsConfig, "EQC_ResetButtonText", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "Reset").Trim
                txtLimitPricePopUpValue.Text = "0"
                txtLimitPricePopUpValue.Enabled = False
                tabPanelELN.Visible = False
                lblMsgPriceProvider.Text = ""
                lblerror.Text = ""
                txttrade.Value = FinIQApp_Date.FinIQDate(Now.ToShortDateString)
                Fill_Entity()
                fill_RMList()
                fill_RFQRMList()
                fill_All_EntityBooks()
                chk_Login_For_PP()
                Get_Price_Provider()
                fill_All_Exchange()
                fill_KO() ''Dilkhush 13May2016 FA1427
		''Rushikesh 14Jan2016:- For Share load on lazyload
                '' fill_All_Shares()
                Fillddl_QuantoCcy()
                setKeyPressValidations()
                setDefaultValues()
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowOrderOnMainPricerPageLoad", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        rbHistory.SelectedValue = "Order History"
                        fill_OrderGrid()
                        '<MohitL. on 04-Nov-2015: FA-1174>
                        ColumnVisibility()
                        '<MohitL. on 04-Nov-2015: FA-1174>
                    Case "N", "NO"
                        rbHistory.SelectedValue = "Quote History"
                        fill_grid()
                End Select
                txtTenor_TextChanged(sender, e)
                txtQuantity_TextChanged(sender, e)



                txtStrike_TextChanged(sender, e)
                ddlSolveFor_SelectedIndexChanged(sender, e)
                SetTemplateDetails(SchemeName)
                SetChartColors()
                btnBNPPDeal.Visible = False
                btnUBSDeal.Visible = False
                btnCSDeal.Visible = False
                btnHSBCDeal.Visible = False
                btnBAMLDeal.Visible = False
                btnJPMDeal.Visible = False
                btnDBIBDeal.Visible = False
		btnOCBCDeal.Visible = False
		btnCITIDeal.Visible = False
                btnLEONTEQDeal.Visible = False
                btnCOMMERZDeal.Visible = False
                lblQuantity.Text = "Notional (<font style=''>" & lblELNBaseCcy.Text & "</font>)"
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        ''getRange()  '<AvinashG. on 04-Sep-2014: Commented as ddlExchnage SelectedIndexChange will get limit for first load.>
                        lblExchangeVal.Text = setExchangeByShare(ddlShare)
                    Case "N", "NO"
                        lblELNBaseCcy.Text = ""
                        lblComentry.Text = ""
                        lblJPMlimit.Text = "N.A."
                        lblBNPPlimit.Text = "N.A."
                        lblUBSlimit.Text = "N.A."
                        lblBNPPlimit.Text = "N.A."
                        lblHSBClimit.Text = "N.A."
                        lblCSLimit.Text = "N.A."
                        lblBAMLlimit.Text = "N.A."
                        lblDBIBLimit.Text = "N.A."
			lblOCBClimit.Text = "N.A."
                        lblCITIlimit.Text = "N.A."
                        lblLEONTEQlimit.Text = "N.A."
                        lblCOMMERZlimit.Text = "N.A."
                        lblJPMlimit.ToolTip = ""
                        lblBNPPlimit.ToolTip = ""
                        lblUBSlimit.ToolTip = ""
                        lblBNPPlimit.ToolTip = ""
                        lblHSBClimit.ToolTip = ""
                        lblCSLimit.ToolTip = ""
                        lblBAMLlimit.ToolTip = ""
                        lblDBIBLimit.ToolTip = ""
			lblOCBClimit.ToolTip = ""
                        lblCITIlimit.ToolTip = ""
                        lblLEONTEQlimit.ToolTip = ""
                        lblCOMMERZlimit.ToolTip = ""
                        pnlReprice.Update()
                        upnlCommentry.Update()
                End Select
                Dim sMailButoonVisibilityJS As StringBuilder = New StringBuilder()

                'AvinashG. Missing config check
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allowed_QuoteMailing", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        ''If ddlShare.SelectedItem Is Nothing Then
                        ''    sMailButoonVisibilityJS.AppendLine("hideEmail();")
                        ''ElseIf ddlShare.SelectedItem.Value = "" Then
                        ''    sMailButoonVisibilityJS.AppendLine("hideEmail();")
                       '' Else
                        ''    sMailButoonVisibilityJS.AppendLine("showEmail();")
                        ''End If
			''Rushikesh 14Jan2016:- For Share load on lazyload
                        If ddlShare.SelectedValue IsNot Nothing Then
                            If ddlShare.SelectedValue = "" Then
                                sMailButoonVisibilityJS.AppendLine("try{ hideEmail(); } catch(e){ }")
                            Else
                                sMailButoonVisibilityJS.AppendLine("try{ showEmail(); } catch(e){ }")
                            End If
                        End If
                    Case "N", "NO"
                        sMailButoonVisibilityJS.AppendLine("try{ hideEmail(); } catch(e){ }")
                End Select
                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showHideMailBtn", sMailButoonVisibilityJS.ToString(), True)
                '/AvinashG. Missing config check

                If Not IsNothing(Request.QueryString("EXTLOD")) Then
                    If UCase(Request.QueryString("EXTLOD")) = "1" Then
                        If IsNothing(Request.QueryString("Prd")) Or Request.QueryString("Prd").ToString.Trim.ToUpper = "ELN" Then
                            tabContainer.ActiveTabIndex = 0
                            tabIndex = tabContainer.ActiveTabIndex

                            ''<Added by rushikesh D. on 21jun16 for HSBC demo>

                            If Not IsNothing(Request.QueryString("SolveFor")) Then
                                Dim strSolveFor As String = Request.QueryString("SolveFor")
                                If strSolveFor = "Price(%)" Then
                                    ddlSolveFor.SelectedValue = "PricePercentage"
                                Else
                                    ddlSolveFor.SelectedValue = "StrikePercentage"
                                End If
                            End If

                            If Not IsNothing(Request.QueryString("OfferPrice")) Then
                                txtELNPrice.Text = Request.QueryString("OfferPrice").ToString
                            Else
                                txtELNPrice.Text = "97.00"
                            End If
                        
                            ''</Added by rushikesh D. on 21jun16 for HSBC demo>

                            If Not IsNothing(Request.QueryString("Strike")) Then
                                txtStrike.Text = Request.QueryString("Strike").ToString
                            Else
                                ' txtStrike.Text = "98.00"    ''<Nikhil M. Chnaged from : "97.00" on 02-Sep-2016: FSD Default 31Aug2016 >
                                txtStrike.Text = getControlPersonalSetting("Strike", "98.00") 'Mohit Lalwani on 8-Sept-2016
                            End If

                            
                            If Not IsNothing(Request.QueryString("Share")) Then
                                Dim strshare As String = Request.QueryString("Share").ToString
                                dtExchange = New DataTable("Dummy")
                                Select Case objELNRFQ.DB_GetExchange(strshare, dtExchange)
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                        setShare(dtExchange.Rows(0)(0).ToString, strshare)          'Mohit Lalwani on 07-Nov-2016
                                        ddlShare_SelectedIndexChanged(Nothing, Nothing)
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

                                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                End Select
                                
                                ' Select objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                '  Case "Y", "YES"
                                '        Dim strshare As String = Request.QueryString("Share").ToString
                                '        ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strshare.Trim))
                                '        ddlShare_SelectedIndexChanged(Nothing, Nothing)
                                '   Case "N", "NO"
                                'dtExchange = New DataTable("Dummy")
                                'Select Case objELNRFQ.DB_GetExchange(Request.QueryString("Share").Trim, dtExchange)
                                '    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                'ddlExchange.SelectedValue = dtExchange.Rows(0)(0).ToString
                                'ddlExchange_SelectedIndexChanged(Nothing, Nothing)
                                '    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

                                '   Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful

                                'End Select
                                '        Dim strshare As String = Request.QueryString("Share").ToString
                                '        ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strshare.Trim))
                                '        ddlShare_SelectedIndexChanged(Nothing, Nothing)
                                'End Select
                            Else
                            End If
                            Dim strTenorType, strTenorVal As String
                            If Not IsNothing(Request.QueryString("Tenor")) Then
                                If Request.QueryString("Tenor").ToString.ToUpper.Contains("M") Then
                                    strTenorType = "MONTH"
                                    strTenorVal = Request.QueryString("Tenor").ToString.ToUpper.Replace("M", "")
                                ElseIf Request.QueryString("Tenor").ToString.ToUpper.Contains("W") Then
                                    strTenorType = "WEEK"
                                    strTenorVal = Request.QueryString("Tenor").ToString.ToUpper.Replace("W", "")
                                ElseIf Request.QueryString("Tenor").ToString.ToUpper.Contains("Y") Then
                                    strTenorType = "YEAR"
                                    strTenorVal = Request.QueryString("Tenor").ToString.ToUpper.Replace("Y", "")
                                Else

                                End If
                            Else
                                strTenorType = "MONTH"
                            End If

                            Dim strSettlWeek As String
                            If Not IsNothing(Request.QueryString("SettlWeek")) Then
                                If Request.QueryString("SettlWeek").ToString.ToUpper.Contains("2W") Then
                                    strSettlWeek = "2W"
                                ElseIf Request.QueryString("SettlWeek").ToString.ToUpper.Contains("1W") Then
                                    strSettlWeek = "1W"
                                Else
                                    strSettlWeek = "2W"
                                End If
                            Else
                                strSettlWeek = "2W"
                            End If

                            ddlTenorTypeELN.SelectedValue = strTenorType
                            txtTenor.Text = strTenorVal

                            ddlSettlementDays.SelectedValue = strSettlWeek
                            ddlSettlementDays_SelectedIndexChanged(Nothing, Nothing)

                            Dim strELN_Upfront As String = "0.5"
                            If Not IsNothing(Request.QueryString("ELN_Upfront")) Then
                                If Request.QueryString("ELN_Upfront").ToString <> "" Then
                                    strELN_Upfront = Request.QueryString("ELN_Upfront").ToString
                                Else
                                    strELN_Upfront = "0.00"
                                End If
                            Else
                                strUpfront = "0.00"
                            End If
                            txtUpfrontELN.Text = strELN_Upfront
                            '<Changed By Mohit Lalwani on 17_Dec-2015>
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SetDefaultNotional", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                    If Not IsNothing(Request.QueryString("Notional")) Then
                                        If Request.QueryString("Notional").ToString <> "" Then
                                            txtQuantity.Text = Request.QueryString("Notional").ToString
                                        Else
                                            txtQuantity.Text = txtQuantity.Text
                                        End If
                                    Else
                                        txtQuantity.Text = txtQuantity.Text
                                    End If

                                Case "N", "NO"
                                    '<AvinashG. on 05-Jan-2016: No point in converting hard-coded CERO to string>
                                    txtQuantity.Text = "0"
                                    'txtQuantity.Text = CStr(0)
                            End Select
                            '</Changed By Mohit Lalwani on 17_Dec-2015>
                            txtQuantity_TextChanged(Nothing, Nothing)

                            If Not IsNothing(Request.QueryString("ELN_ischkBarrier")) Then
                                If UCase(Request.QueryString("ELN_ischkBarrier").ToString) = "FALSE" Then
                                    chkELNType.Checked = False
                                Else
                                    chkELNType.Checked = True
                                End If
                            Else
                                chkELNType.Checked = False
                            End If
                            chkELNType_CheckedChanged(Nothing, Nothing)

                            If Not IsNothing(Request.QueryString("ELN_BarrierLvl")) Then
                                If Request.QueryString("ELN_BarrierLvl").ToString <> "" Then
                                    txtBarrier.Text = Request.QueryString("ELN_BarrierLvl").ToString
                                Else
                                    '   txtBarrier.Text = ""
                                    txtBarrier.Text = "0.00"        'Changed by Mohit Lalwani on 30-Mar-2016 EQBOSDEV-280
                                End If
                            Else
                                '  txtBarrier.Text = ""
                                txtBarrier.Text = "0.00"            'Changed by Mohit Lalwani on 30-Mar-2016 EQBOSDEV-280
                            End If

                            If Not IsNothing(Request.QueryString("ELN_BarrierMode")) Then
                                If Request.QueryString("ELN_BarrierMode").ToString <> "" Then
                                    Dim strBarrierMode As String = Request.QueryString("ELN_BarrierMode").ToString
                                    ''''<Dilkhush 13May2016 FA-1427>
                                    ''ddlBarrier.SelectedValue = strBarrierMode
                                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SHOW_DAILYCLOSE_KO", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                        Case "Y", "YES"
                                            ddlBarrier.SelectedValue = strBarrierMode
                                        Case "N", "NO"
                                            If strBarrierMode.ToUpper <> "DAILY_CLOSE" Then
                                                ddlBarrier.SelectedValue = strBarrierMode
                                            End If
                                    End Select
                                    ''''</Dilkhush 13May2016 FA-1427>
                                Else
                                    ddlBarrier.SelectedIndex = 0
                                End If
                            Else
                                ddlBarrier.SelectedIndex = 0
                            End If

                            ddlBarrier_SelectedIndexChanged(Nothing, Nothing)
                            getCurrency(ddlShare.SelectedValue.ToString)
                            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                            'getRange()
                            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                            ddlQuantoCcy.Items.Clear()
                            ddlQuantoCcy.Items.Add(New DropDownListItem(lblELNBaseCcy.Text, lblELNBaseCcy.Text)) 'Mohit Lalwani on 8-Jul-2016
                            Call ReCalcDate()
                            ResetAll()   'Added by Mohit Lalwani on 18-Apr-2016 FA-1405 - Issuer not disabled when redirected from multi-stock pricer 
                        End If
                    ElseIf UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Then
                        Session.Remove("Share_Value")
                        If Request.QueryString("PRD").Trim = "" Then
                            lblerror.Text = "Error: Failed while trying to load pool data."
                        Else

                            setELNPoolData()
                        End If

                        btnSolveAll.Enabled = False
                    ElseIf UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                        Session.Remove("Share_Value")
                        If Request.QueryString("PRD").Trim = "" Then
                            lblerror.Text = "Error: Failed while trying to load pool data."
                        Else
                            setELNPoolData()
                        End If
                        btnSolveAll.Enabled = False
                    ElseIf Request.QueryString("EXTLOD").ToString.Trim.ToUpper() = "REDIRECTEDHEDGE" Then
                        Session.Remove("Share_Value")
                        If Request.QueryString("PRD").Trim = "" Then
                            lblerror.Text = "Error: Failed while trying to load order data."
                        Else
                            setRedirectedELNOrderData()
                        End If
                    End If
                    ''Dilkhush 01Feb2016 To show email button

                    ''<Start | Nikhil M. on 04-Oct-2016: For Hide/Show allocation>
                    If IsNothing(Request.QueryString("PoolID")) Then
                        grdRMData.Visible = True
                        btnAddAllocation.Visible = True
                    Else
                        If Request.QueryString("PoolID").ToString = "" Then
                            grdRMData.Visible = True
                            btnAddAllocation.Visible = True
                            tblRw1.Visible = True ''<Nikhil M. on 17-Oct-2016: Added for hidning the COntrol on hedge>
                            tblRw2.Visible = True
                            tblRw3.Visible = True
                        Else
                            grdRMData.Visible = False
                            btnAddAllocation.Visible = False
                            tblRw1.Visible = False
                            tblRw2.Visible = False
                            tblRw3.Visible = False ''<Nikhil M. on 17-Oct-2016: Added for hidning the COntrol on hedge>
                            EnableDisableForOrderPoolData(CType(Session("RePricePPName"), String))
                        End If

                    End If
                    ''<End | Nikhil M. on 04-Oct-2016: For Hide/Show>
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowhideELNEmailbtn", "try{ showEmail(); } catch(e){ }", True)
                Else

                    SetPersonalSetting()

                End If



                'Mohit Lalwani on 12-Oct-2016
                If Not IsNothing(Request.QueryString("Mode")) Then
                    If UCase(Request.QueryString("Mode")) = "DEFSETUP" Then
                        'Mohit Lalwani on 26-Oct-2016
                        tdpnlReprice.Visible = False
                        '   pnlReprice.Visible = False
                        '/Mohit Lalwani on 26-Oct-2016
                        upnlGrid.Visible = False
                        '  upnlCommentry.Visible = False
                        ' updShareGraphData.Visible = False
                        tdCommentry.Visible = False
                        tdShareGraphData.Visible = False
                        trSaveSetting.Visible = True
                        txtSettlementDate.Disabled = True
                        txtExpiryDate.Disabled = True
                        txtMaturityDate.Disabled = True
                    Else
                        trSaveSetting.Visible = False
                    End If
                Else
                    trSaveSetting.Visible = False
                End If
                '/Mohit Lalwani on 12-Oct-2016


            End If
            txtTradeDate.Disabled = True
            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Fill_All_Charts()
            Else
                Call manageShareReportShowHide()
            End If
            chkPPmaillist.Visible = False
            hideShowRBLShareData()
            '<shekhar 26-Nov-2015>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "CP_Dragable", "CP_Dragable();", True)
            '</shekhar 26-Nov-2015>
            '<Mohit Lalwani on 26-May-2016>
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "setResolution", "setResolution();", True)
            '</Mohit Lalwani on 26-May-2016>
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "hideLPBoxes", "hideLPBoxes();", True)''<Nikhil M. on 19-Sep-2016: COmmented for visibilty of checkbox >

        Catch ex As Exception
            lblerror.Text = "Error occurred on Page Load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Page_Load", ErrorLevel.High)
        End Try
    End Sub

    'Added by Mohit Lalwani on 16-Sept-2016
    Public Sub SetPersonalSetting()
        Try
            Dim dtExchange As DataTable

            ddlSolveFor.SelectedValue = getControlPersonalSetting("Solve For", "PricePercentage")


            'Mohit Lalwani on 16-Oct-2016
            ' ddlSolveFor_SelectedIndexChanged(Nothing, Nothing)

            If ddlSolveFor.SelectedValue = "PricePercentage" Then
                txtELNPrice.Text = getControlPersonalSetting("IB Price", "0.00")
                txtStrike.Text = getControlPersonalSetting("Strike", "97.00")
            Else
                txtStrike.Text = getControlPersonalSetting("Strike", "0.00")
                txtELNPrice.Text = getControlPersonalSetting("IB Price", "98.00")
            End If
            '/Mohit Lalwani on 16-Oct-2016


            Dim strshare As String = getControlPersonalSetting("Share", "")
            dtExchange = New DataTable("Dummy")

            If strshare <> "" Then
                Select Case objELNRFQ.DB_GetExchange(strshare, dtExchange)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        setShare(dtExchange.Rows(0)(0).ToString, strshare)
                        ' ddlExchange.SelectedValue = dtExchange.Rows(0)(0).ToString
                        ' ddlExchange_SelectedIndexChanged(Nothing, Nothing)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                End Select

            End If
            ddlTenorTypeELN.SelectedValue = getControlPersonalSetting("Tenor Type", "MONTH")
            txtTenor.Text = getControlPersonalSetting("Tenor", "1")

            ddlSettlementDays.SelectedValue = getControlPersonalSetting("Settl. Weeks", "2W")
            ddlSettlementDays_SelectedIndexChanged(Nothing, Nothing)
            txtUpfrontELN.Text = getControlPersonalSetting("Upfront", "0.5")
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SetZeroNotional_MainPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                Case "Y", "YES"
                    txtQuantity.Text = "0"
                Case "N", "NO"
                    txtQuantity.Text = getControlPersonalSetting("Notional", "300,000")                    'Mohit Lalwani on 7-Sept-2016 for Personal Settings
                    ' txtQuantity.Text = "1,000,000"
            End Select
            txtQuantity_TextChanged(Nothing, Nothing)
            chkELNType.Checked = CBool(getControlPersonalSetting("IS KO", "False"))
            chkELNType_CheckedChanged(Nothing, Nothing)
            If chkELNType.Checked Then
                txtBarrier.Text = getControlPersonalSetting("KO", "110.00")
                ddlBarrier.SelectedValue = getControlPersonalSetting("KO Type", "CONTINUOUS")
            End If

            ddlBarrier_SelectedIndexChanged(Nothing, Nothing)
            getCurrency(ddlShare.SelectedValue.ToString)
            ddlQuantoCcy.Items.Clear()
            ddlQuantoCcy.Items.Add(New DropDownListItem(lblELNBaseCcy.Text, lblELNBaseCcy.Text)) 'Mohit Lalwani on 8-Jul-2016
            chkQuantoCcy.Checked = CBool(getControlPersonalSetting("IS Note Ccy", "False"))
            chkQuantoCcy_CheckedChanged(Nothing, Nothing)
            ddlQuantoCcy.SelectedValue = getControlPersonalSetting("Note Ccy", lblELNBaseCcy.Text)
            Call ReCalcDate()
            btnCancelReq_Click(Nothing, Nothing)
        Catch ex As Exception
            lblerror.Text = "Error occurred in binding personal settings.Please contact admin."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "SetPersonalSetting", ErrorLevel.High)
            Throw ex
        End Try
    End Sub


    Public Function Write_PersonalSettings_TOXML(ByRef o_strXMLNote_DefaultValues As String) As Boolean
        Dim strXMLRFQ As StringBuilder
        Dim dtQuote As New DataTable
        Try
            strXMLRFQ = New StringBuilder

            strXMLRFQ.Append("<SettingDetails>")
            strXMLRFQ.Append("<Default>")
            strXMLRFQ.Append("<Solve_For>" & ddlSolveFor.SelectedValue & "</Solve_For>")
            strXMLRFQ.Append("<IB_Price>" & txtELNPrice.Text & "</IB_Price>")
            strXMLRFQ.Append("<Strike>" & txtStrike.Text & "</Strike>")
            strXMLRFQ.Append("<Share>" & ddlShare.SelectedValue.ToString & "</Share>")
            strXMLRFQ.Append("<Tenor_Type>" & ddlTenorTypeELN.SelectedValue & "</Tenor_Type>")
            strXMLRFQ.Append("<Tenor>" & txtTenor.Text & "</Tenor>")
            strXMLRFQ.Append("<Settl_Weeks>" & ddlSettlementDays.SelectedValue & "</Settl_Weeks>")
            strXMLRFQ.Append("<Upfront>" & txtUpfrontELN.Text & "</Upfront>")
            strXMLRFQ.Append("<Notional>" & txtQuantity.Text & "</Notional>")
            strXMLRFQ.Append("<IS_KO>" & chkELNType.Checked & "</IS_KO>")
            strXMLRFQ.Append("<KO>" & txtBarrier.Text & "</KO>")
            strXMLRFQ.Append("<KO_Type>" & ddlBarrier.SelectedValue & "</KO_Type>")
            strXMLRFQ.Append("<IS_Note_Ccy>" & chkQuantoCcy.Checked & "</IS_Note_Ccy>")
            strXMLRFQ.Append("<Note_Ccy>" & ddlQuantoCcy.SelectedValue & "</Note_Ccy>")
            'strXMLRFQ.Append("<Entity_ID>" & LoginInfoGV .Login_Info.EntityID & "</Entity_ID>")
            'strXMLRFQ.Append("<Login_Id>" & LoginInfoGV.Login_Info.LoginId & "</Login_Id>")

            'strXMLRFQ.Append("<Setting_Type>" & ddlQuantoCcy.SelectedValue & "</Setting_Type>")

            'strXMLRFQ.Append("<Default_Value>" & chkQuantoCcy.Checked & "</Default_Value>")
            'strXMLRFQ.Append("<Created_By>" & ddlQuantoCcy.SelectedValue & "</Created_By>")
          
            strXMLRFQ.Append("</Default>")
            strXMLRFQ.Append("</SettingDetails>")

            o_strXMLNote_DefaultValues = strXMLRFQ.ToString
            Write_PersonalSettings_TOXML = True
        Catch ex As Exception
            lblerror.Text = "Write_DefaultSettings_TOXML:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Write_DefaultSettings_TOXML", ErrorLevel.High)

            Throw ex
        End Try
    End Function

    '/Added by Mohit Lalwani on 16-Sept-2016


    Public Sub SetChartColors()
        Try
            structChartColors = New ChartColors
            With Me.structChartColors
                .CS = System.Drawing.Color.FromArgb(140, 174, 212)
                .FINIQ = Color.LightGray
                .BNPP = Color.Aqua
                .HSBC = Color.Red
                .JPM = System.Drawing.Color.FromArgb(37, 53, 131)
                .BAML = Color.Gray
                .UBS = Color.DarkOrange
                .DBIB = Color.Magenta
		.OCBC = Color.BlanchedAlmond
                .CITI = Color.DarkOliveGreen
                .LEONTEQ = Color.Olive
                .COMMERZ = Color.CadetBlue
            End With
        Catch ex As Exception
            lblerror.Text = "SetChartColors:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "SetChartColors", ErrorLevel.High)
            Throw ex
        End Try

    End Sub

    Public Sub setDefaultValues()
        Try
            getCurrency(ddlShare.SelectedValue.ToString)
            If chkELNType.Checked = False Then
                txtBarrier.Text = "0"  ''Type is simple
                txtBarrier.Enabled = False
                ddlBarrier.Enabled = False
                txtBarrier.Visible = False
                ddlBarrier.Visible = False
            Else
                txtBarrier.Enabled = True
                ddlBarrier.Enabled = True
                txtBarrier.Visible = True
                ddlBarrier.Visible = True
            End If

            If chkQuantoCcy.Checked = False Then
                ddlQuantoCcy.DataSource = Nothing
                ddlQuantoCcy.DataBind()
                ddlQuantoCcy.Items.Clear()
                ddlQuantoCcy.Items.Add(New DropDownListItem(lblELNBaseCcy.Text, lblELNBaseCcy.Text)) 'Mohit Lalwani on 8-Jul-2016
                ddlQuantoCcy.Enabled = False
                ddlQuantoCcy.BackColor = Color.FromArgb(242, 242, 243)
            Else
                ddlQuantoCcy.Enabled = True
                ddlQuantoCcy.BackColor = Color.White
            End If
            If ddlSolveFor.SelectedValue = "PricePercentage" Then
                txtELNPrice.Text = ""
                txtELNPrice.Enabled = False
                txtStrike.Enabled = True
            Else
                txtStrike.Text = ""
                txtStrike.Enabled = False
                txtELNPrice.Enabled = True
            End If
            GetCommentary()
        Catch ex As Exception
            lblerror.Text = "setDefaultValues:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setDefaultValues", ErrorLevel.High)
            Throw ex

        End Try
    End Sub

    Public Sub setKeyPressValidations()
        Try
            txtTotalRows.Attributes.Add("onkeypress", "AllowOnlyNumeric()")
            txtQuantity.Attributes.Add("onkeypress", "KeysAllowedForNotional()")
            txtQuantity.Attributes.Add("onkeypress", "KeysAllowedForNotional()")
            txtValueDays.Attributes.Add("onkeypress", "AllowOnlyNumeric()")
            txtUpfrontELN.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtUpfrontELN.ClientID & "');")
            txtTenor.Attributes.Add("onkeypress", "AllowOnlyNumeric()")
            txtStrike.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtStrike.ClientID & "');")
            txtELNPrice.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtELNPrice.ClientID & "');")
            txtBarrier.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtBarrier.ClientID & "');")
            txtUpfrontPopUpValue.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtUpfrontPopUpValue.ClientID & "');")
            btnJPMprice.Attributes.Add("onclick", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlExchange.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtValueDays.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlShare.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlQuantoCcy.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtTenor.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlSettlementDays.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlSolveFor.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtStrike.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtBarrier.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlBarrier.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtQuantity.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtTradeDate.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtExpiryDate.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtSettlementDate.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtMaturityDate.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtELNPrice.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtLimitPricePopUpValue.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtLimitPricePopUpValue.ClientID & "');")
            btnDBIBPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
            btnCSPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
            btnBNPPPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")  ' GS iS Renamed to BNPP as per BOS Requirement by Mohit on 11-JUN-2015
            btnHSBCPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
            btnOCBCPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
            btnCITIprice.Attributes.Add("onclick", "StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
            btnLEONTEQprice.Attributes.Add("onclick", "StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
            btnCOMMERZPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
            btnJPMprice.Attributes.Add("onclick", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            btnUBSPrice.Attributes.Add("onclick", "StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
            btnBAMLPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
	    
	    btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")  ' GS iS Renamed to BNPP as per BOS Requirement by Mohit on 11-JUN-2015
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
	    btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")

            txtClientYieldPopUpValue.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtClientYieldPopUpValue.ClientID & "');") '''DK
        Catch ex As Exception
            lblerror.Text = "setKeyPressValidations:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setKeyPressValidations", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
#Region "ShareReport"

    Public Sub setTDSSData(ByVal strShare As String)
        Dim dtDssData As DataTable
        Try
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    dtDssData = New DataTable("Share_data")
                    If (strShare.Trim <> "") Then
                        Select Case objELNRFQ.Web_GetTDSSData(strShare, dtDssData)
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                lblStock.Text = dtDssData.Rows(0)("RICCode").ToString
                                lblStockDesc.Text = dtDssData.Rows(0)("Description").ToString.ToUpper
                                '<AvinashG. on 01-Feb-2016:  FA-1283 - Capture stock spot price and update Stock Long name(Company Name) >
                                lblSpotDate.Text = If(dtDssData.Rows(0)("SpotLastUpdateDate").ToString.Trim = "", "", CDate(dtDssData.Rows(0)("SpotLastUpdateDate").ToString).ToString("dd-MMM-yy HH:mm"))
                                'lblSpotDate.Text = If(dtDssData.Rows(0)("SpotLastUpdateDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("SpotLastUpdateDate").ToString))
                                '</AvinashG. on 01-Feb-2016:  FA-1283 - Capture stock spot price and update Stock Long name(Company Name) >
                                lblSpotValue.Text = SetNumberFormat(dtDssData.Rows(0)("Spot").ToString, 2)
                                '<AvinashG. on 01-Feb-2016:  FA-1283 - Capture stock spot price and update Stock Long name(Company Name) >
                                lbl52WkHighDate.Text = If(dtDssData.Rows(0)("Last1YearHighDate").ToString.Trim = "", "", CDate(dtDssData.Rows(0)("Last1YearHighDate").ToString).ToString("dd-MMM-yy"))
                                'lbl52WkHighDate.Text = If(dtDssData.Rows(0)("Last1YearHighDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("Last1YearHighDate").ToString))
                                '</AvinashG. on 01-Feb-2016:  FA-1283 - Capture stock spot price and update Stock Long name(Company Name) >

                                lbl52WkHighValue.Text = SetNumberFormat(dtDssData.Rows(0)("Last1YearHighValue").ToString, 2)
                                '<AvinashG. on 01-Feb-2016:  FA-1283 - Capture stock spot price and update Stock Long name(Company Name) >
                                lbl52WkLowDate.Text = If(dtDssData.Rows(0)("SpotLastUpdateDate").ToString.Trim = "", "", CDate(dtDssData.Rows(0)("Last1YearLowDate").ToString).ToString("dd-MMM-yy"))
                                'lbl52WkLowDate.Text = If(dtDssData.Rows(0)("Last1YearLowDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("Last1YearLowDate").ToString))
                                '</AvinashG. on 01-Feb-2016:  FA-1283 - Capture stock spot price and update Stock Long name(Company Name) >
                                lbl52WkLowValue.Text = SetNumberFormat(dtDssData.Rows(0)("Last1YearLowValue").ToString, 2)
                                lblYTDChngValue.Text = SetNumberFormat(dtDssData.Rows(0)("YTDchngperc").ToString, 2)
                                lblMTDChngValue.Text = SetNumberFormat(dtDssData.Rows(0)("MTDchngperc").ToString, 2)
                                lbl1YearChngValue.Text = SetNumberFormat(dtDssData.Rows(0)("1yearchngperc").ToString, 2)
                                lbl20DHistVolCurr.Text = SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaCurr").ToString, 2)
                                lbl60DHistVolCurr.Text = SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaCurr").ToString, 2)
                                lbl250DHistVolCurr.Text = SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaCurr").ToString, 2)
                                lblTrailing12MPEValue.Text = SetNumberFormat(dtDssData.Rows(0)("Trailing12MPE").ToString, 2)
                                lblTrailing12MPBValue.Text = SetNumberFormat(dtDssData.Rows(0)("Trailing12MPB").ToString, 2)
                                lblMarketCapCcy.Text = dtDssData.Rows(0)("MarketCapCcy").ToString
                                lblMarketCapValue.Text = SetNumberFormat(CDbl(CDbl(Val(dtDssData.Rows(0)("MarketCapValue").ToString))) / (1000000), 2) & "M"
                                lblCashEquivCcy.Text = dtDssData.Rows(0)("CashAndEquivCcy").ToString
                                lblCashEquivValue.Text = SetNumberFormat(CDbl(CDbl(Val(dtDssData.Rows(0)("CashAndEquivValue").ToString))) / (1000000), 2) & "M"
                                lblTotalDebtCcy.Text = dtDssData.Rows(0)("TotalDebtCcy").ToString
                                lblTotalDebtValue.Text = SetNumberFormat(CDbl(CDbl(Val(dtDssData.Rows(0)("TotalDebtValue").ToString))) / (1000000), 2) & "M"
                                lbl12TDivValuefreq.Text = If(dtDssData.Rows(0)("DividendYield12MonthFreq").ToString.Trim = "", "", dtDssData.Rows(0)("DividendYield12MonthFreq").ToString.Substring(0, 1))
                                lbl12TDivYieldValue.Text = If(dtDssData.Rows(0)("DividendYield12MonthValue").ToString = "", "", SetNumberFormat(dtDssData.Rows(0)("DividendYield12MonthValue").ToString, 2) & "%")
                                lblNextDivDateValue.Text = If(dtDssData.Rows(0)("NextDividendDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("NextDividendDate").ToString))
                                lblPrevEarnFreq.Text = If(dtDssData.Rows(0)("PreviousEarnFreq").ToString.Trim = "", "", dtDssData.Rows(0)("PreviousEarnFreq").ToString.Substring(0, 1))
                                lblPrevEarnDateValue.Text = If(dtDssData.Rows(0)("PreviousEarnDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("PreviousEarnDate").ToString))
                                lblPrevEPSValue.Text = SetNumberFormat(dtDssData.Rows(0)("PreviousEPS").ToString, 4)
                                lblNextEarnFreq.Text = If(dtDssData.Rows(0)("NextEarnFreq").ToString.Trim = "", "", dtDssData.Rows(0)("NextEarnFreq").ToString.Substring(0, 1))
                                lblNextEarnDateValue.Text = If(dtDssData.Rows(0)("NextEarnDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("NextEarnDate").ToString))
                                '<AvinashG. on 02-Feb-2016: Consistent date format in one area>
                                lblAsOfValue.Text = If(dtDssData.Rows(0)("AsOfDate").ToString.Trim = "", "", CDate(dtDssData.Rows(0)("AsOfDate").ToString).ToString("dd-MMM-yy"))
                                '<AvinashG. on 02-Feb-2016: Consistent date format in one area>
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                                lblStock.Text = strShare
                                Call clearTDSSData()
                            Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                lblStock.Text = strShare
                                Call clearTDSSData()
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                                lblStock.Text = strShare
                                Call clearTDSSData()
                        End Select
                    Else
                        lblStock.Text = strShare
                        Call clearTDSSData()
                    End If
                Case "N", "NO"
            End Select
        Catch ex As Exception
            lblerror.Text = "setTDSSData:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setTDSSData", ErrorLevel.High)
            Throw ex
        End Try
    End Sub


    Private Sub clearTDSSData()
        Try
            lblStockDesc.Text = ""
            lblSpotDate.Text = ""
            lblSpotValue.Text = ""
            lbl52WkHighDate.Text = ""
            lbl52WkHighValue.Text = ""
            lbl52WkLowDate.Text = ""
            lbl52WkLowValue.Text = ""
            lblYTDChngValue.Text = ""
            lblMTDChngValue.Text = ""
            lbl1YearChngValue.Text = ""
            lbl20DHistVolCurr.Text = ""
            lbl60DHistVolCurr.Text = ""
            lbl250DHistVolCurr.Text = ""
            lblTrailing12MPEValue.Text = ""
            lblTrailing12MPBValue.Text = ""
            lblMarketCapCcy.Text = ""
            lblMarketCapValue.Text = ""
            lblCashEquivCcy.Text = ""
            lblCashEquivValue.Text = ""
            lblTotalDebtCcy.Text = ""
            lblTotalDebtValue.Text = ""
            lbl12TDivValuefreq.Text = ""
            lbl12TDivYieldValue.Text = ""
            lblNextDivDateValue.Text = ""
            lblPrevEarnFreq.Text = ""
            lblPrevEarnDateValue.Text = ""
            lblPrevEPSValue.Text = ""
            lblNextEarnFreq.Text = ""
            lblNextEarnDateValue.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

   

   
    Private Sub manageShareReportShowHide()
        Try
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    clearTDSSData()
		''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    If ddlShare.SelectedValue Is Nothing Then
                    'If ddlShare.SelectedItem Is Nothing Then
                        setTDSSData("")
                        Exit Sub
                    Else
                        setTDSSData(ddlShare.SelectedValue.ToString)
                    End If
                Case "N", "NO"
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#End Region
#Region "CheckLoginPP"
    Public Sub chk_PriceProviderStatus(ByVal dtLoginPP As DataTable)
        Dim dr1 As DataRow()
        Try
            dr1 = dtLoginPP.Select("PP_CODE = 'JPM' ")
            If dr1.Length > 0 Then
                btnJPMprice.Visible = True
                tdJPM1.Style.Remove("display")
                lblJPM.Visible = True
                lblJPMPrice.Visible = True
                lblTimer.Visible = True
                TRJPM1.Visible = True
            Else
                btnJPMprice.Visible = False
                btnJPMDeal.Visible = False
                tdJPM1.Style.Remove("display")
                tdJPM1.Style.Add("display", "none !important;")
                lblJPM.Visible = False
                lblJPMPrice.Visible = False
                lblTimer.Visible = False
                TRJPM1.Visible = False
            End If

            dr1 = dtLoginPP.Select("PP_CODE = 'HSBC' ")
            If dr1.Length > 0 Then
                btnHSBCPrice.Style.Add("visibility", "visible")
                tdHSBC1.Style.Remove("display")
                lblHSBC.Visible = True
                lblHSBCPrice.Visible = True
                lblTimerHSBC.Visible = True
                TRHSBC1.Visible = True
            Else
                btnHSBCPrice.Style.Add("visibility", "hidden")
                btnHSBCDeal.Style.Add("visibility", "hidden")
                tdHSBC1.Style.Remove("display")
                tdHSBC1.Style.Add("display", "none !important;")
                lblHSBC.Visible = False
                lblHSBCPrice.Visible = False
                lblTimerHSBC.Visible = False
                TRHSBC1.Visible = False
            End If

 dr1 = dtLoginPP.Select("PP_CODE = 'CITI' ")
            If dr1.Length > 0 Then
                btnCITIPrice.Style.Add("visibility", "visible")
                tdCITI1.Style.Remove("display")
                lblCITI.Visible = True
                lblCITIPrice.Visible = True
                lblTimerCITI.Visible = True
                TRCITI1.Visible = True
            Else
                btnCITIPrice.Style.Add("visibility", "hidden")
                btnCITIDeal.Style.Add("visibility", "hidden")
                tdCITI1.Style.Remove("display")
                tdCITI1.Style.Add("display", "none !important;")
                lblCITI.Visible = False
                lblCITIPrice.Visible = False
                lblTimerCITI.Visible = False
                TRCITI1.Visible = False
            End If
            dr1 = dtLoginPP.Select("PP_CODE = 'LEONTEQ' ")
            If dr1.Length > 0 Then
                btnLEONTEQprice.Style.Add("visibility", "visible")
                tdLEONTEQ1.Style.Remove("display")
                lblLEONTEQ.Visible = True
                lblLEONTEQPrice.Visible = True
                lblTimerLEONTEQ.Visible = True
                TRLEONTEQ1.Visible = True
            Else
                btnLEONTEQprice.Style.Add("visibility", "hidden")
                btnLEONTEQDeal.Style.Add("visibility", "hidden")
                tdLEONTEQ1.Style.Remove("display")
                tdLEONTEQ1.Style.Add("display", "none !important;")
                lblLEONTEQ.Visible = False
                lblLEONTEQPrice.Visible = False
                lblTimerLEONTEQ.Visible = False
                TRLEONTEQ1.Visible = False
            End If
            dr1 = dtLoginPP.Select("PP_CODE = 'COMMERZ' ")
            If dr1.Length > 0 Then
                btnCOMMERZprice.Style.Add("visibility", "visible")
                tdCOMMERZ1.Style.Remove("display")
                lblCOMMERZ.Visible = True
                lblCOMMERZPrice.Visible = True
                lblTimerCOMMERZ.Visible = True
                TRCOMMERZ1.Visible = True
            Else
                btnCOMMERZprice.Style.Add("visibility", "hidden")
                btnCOMMERZDeal.Style.Add("visibility", "hidden")
                tdCOMMERZ1.Style.Remove("display")
                tdCOMMERZ1.Style.Add("display", "none !important;")
                lblCOMMERZ.Visible = False
                lblCOMMERZPrice.Visible = False
                lblTimerCOMMERZ.Visible = False
                TRCOMMERZ1.Visible = False
            End If
dr1 = dtLoginPP.Select("PP_CODE = 'OCBC' ")
            If dr1.Length > 0 Then
                btnOCBCPrice.Style.Add("visibility", "visible")
                tdOCBC1.Style.Remove("display")
                lblOCBC.Visible = True
                lblOCBCPrice.Visible = True
                lblTimerOCBC.Visible = True
                TROCBC1.Visible = True
            Else
                btnOCBCPrice.Style.Add("visibility", "hidden")
                btnOCBCDeal.Style.Add("visibility", "hidden")
                tdOCBC1.Style.Remove("display")
                tdOCBC1.Style.Add("display", "none !important;")
                lblOCBC.Visible = False
                lblOCBCPrice.Visible = False
                lblTimerOCBC.Visible = False
                TROCBC1.Visible = False
            End If
	    
            dr1 = dtLoginPP.Select("PP_CODE ='CS'")
            If dr1.Length > 0 Then
                btnCSPrice.Visible = True
                tdCS1.Style.Remove("display")
                lblCS.Visible = True
                lblCSPrice.Visible = True
                lblTimerCS.Visible = True
                TRCS1.Visible = True
            Else
                btnCSPrice.Visible = False
                btnCSDeal.Visible = False
                tdCS1.Style.Remove("display")
                tdCS1.Style.Add("display", "none !important;")
                lblCS.Visible = False
                lblCSPrice.Visible = False
                lblTimerCS.Visible = False
                TRCS1.Visible = False
            End If

            dr1 = dtLoginPP.Select("PP_CODE ='BNPP'")
            If dr1.Length > 0 Then
                btnBNPPPrice.Visible = True
                tdBNPP1.Style.Remove("display")
                lblBNPP.Visible = True
                lblBNPPPrice.Visible = True
                lblTimerBNPP.Visible = True
                TRBNPP1.Visible = True
            Else
                btnBNPPPrice.Visible = False
                btnBNPPDeal.Visible = False
                tdBNPP1.Style.Remove("display")
                tdBNPP1.Style.Add("display", "none !important;")
                lblBNPP.Visible = False
                lblBNPPPrice.Visible = False
                lblTimerBNPP.Visible = False
                TRBNPP1.Visible = False
            End If
            dr1 = dtLoginPP.Select("PP_CODE ='UBS'")
            If dr1.Length > 0 Then
                btnUBSPrice.Visible = True
                tdUBS1.Style.Remove("display")
                lblUBS.Visible = True
                lblUBSPrice.Visible = True
                lblUBSTimer.Visible = True
                TRUBS1.Visible = True
            Else
                btnUBSPrice.Visible = False
                btnUBSDeal.Visible = False
                tdUBS1.Style.Remove("display")
                tdUBS1.Style.Add("display", "none !important;")
                lblUBS.Visible = False
                lblUBSPrice.Visible = False
                lblUBSTimer.Visible = False
                TRUBS1.Visible = False
            End If
            dr1 = dtLoginPP.Select("PP_CODE ='BAML'")
            If dr1.Length > 0 Then
                btnBAMLPrice.Visible = True
                tdBAML1.Style.Remove("display")
                lblBAML.Visible = True
                lblBAMLPrice.Visible = True
                lblTimerBAML.Visible = True
                TRBAML1.Visible = True
            Else
                btnBAMLPrice.Visible = False
                btnBAMLDeal.Visible = False
                tdBAML1.Style.Remove("display")
                tdBAML1.Style.Add("display", "none !important;")
                lblBAML.Visible = False
                lblBAMLPrice.Visible = False 
                lblTimerBAML.Visible = False
                TRBAML1.Visible = False
            End If
            'dr1 = dtLoginPP.Select("PP_CODE ='DBIB'") ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
            dr1 = dtLoginPP.Select("PP_CODE ='DB'")
            If dr1.Length > 0 Then
                btnDBIBPrice.Visible = True
                tdDBIB.Style.Remove("display")
                lblDBIB.Visible = True
                lblDBIBPrice.Visible = True
                lblTimerDBIB.Visible = True
                TRDBIB.Visible = True
            Else
                btnDBIBPrice.Visible = False
                btnDBIBDeal.Visible = False
                tdDBIB.Style.Remove("display")
                tdDBIB.Style.Add("display", "none !important;")
                lblDBIB.Visible = False
                lblDBIBPrice.Visible = False 
                lblTimerDBIB.Visible = False
                TRDBIB.Visible = False
            End If
        Catch ex As Exception
            lblerror.Text = "chk_PriceProviderStatus:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chk_PriceProviderStatus", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Sub chk_Login_For_PP()
        Dim dtLoginPP As DataTable
        Dim dr As DataRow()
        Dim strLoginName As String = String.Empty

        Try

            strLoginName = LoginInfoGV.Login_Info.LoginId
            dtLoginPP = New DataTable("LoginWisePriceProvider_Mapping")
            tabPanelELN.Visible = True
            SchemeName = "ELN"
            Select Case objELNRFQ.Db_Get_Avail_Login_For_PP(strLoginName, SchemeName, dtLoginPP)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtLoginPP.Rows.Count > 0 Then

                        dr = dtLoginPP.Select("Product_Code='ELN'")
                        If dr.Length > 0 Then
                            chk_PriceProviderStatus(dtLoginPP)
                        End If
                    End If

                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    btnJPMprice.Visible = False
                    btnJPMDeal.Visible = False
                    lblJPM.Visible = False
                    lblJPMPrice.Visible = False
                    lblTimer.Visible = False
                      '<AvinashG. on 27-Jan-2016: Consisitency in used property>
                    'btnHSBCPrice.Style.Add("visibility", "hidden")
                    'btnHSBCDeal.Style.Add("visibility", "hidden")
                    '</AvinashG. on 27-Jan-2016: Consisitency in used property>
                    tdJPM1.Style.Remove("display")
                    tdJPM1.Style.Add("display", "none !important;")
                    tdHSBC1.Style.Remove("display")
                    tdHSBC1.Style.Add("display", "none !important;")
                    tdCS1.Style.Remove("display")
                    tdCS1.Style.Add("display", "none !important;")
                    tdBNPP1.Style.Remove("display")
                    tdBNPP1.Style.Add("display", "none !important;")
                    tdUBS1.Style.Remove("display")
                    tdUBS1.Style.Add("display", "none !important;")
                    tdBAML1.Style.Remove("display")
                    tdBAML1.Style.Add("display", "none !important;")
                    tdDBIB.Style.Remove("display")
                    tdDBIB.Style.Add("display", "none !important;")
		    tdOCBC1.Style.Remove("display")
                    tdOCBC1.Style.Add("display", "none !important;")
		     tdCITI1.Style.Remove("display")
                    tdCITI1.Style.Add("display", "none !important;")
                    tdLEONTEQ1.Style.Remove("display")
                    tdLEONTEQ1.Style.Add("display", "none !important;")
                    tdCOMMERZ1.Style.Remove("display")
                    tdCOMMERZ1.Style.Add("display", "none !important;")
                    lblHSBC.Visible = False
                    lblHSBCPrice.Visible = False
                    lblTimerHSBC.Visible = False
		    lblOCBC.Visible = False
                    lblOCBCPrice.Visible = False
                    lblTimerOCBC.Visible = False
		     lblCITI.Visible = False
                    lblCITIPrice.Visible = False
                    lblTimerCITI.Visible = False
                    lblLEONTEQ.Visible = False
                    lblLEONTEQPrice.Visible = False
                    lblTimerLEONTEQ.Visible = False
                    lblCOMMERZ.Visible = False
                    lblCOMMERZPrice.Visible = False
                    lblTimerCOMMERZ.Visible = False
                    btnCSPrice.Visible = False
                    btnCSDeal.Visible = False
                    lblCS.Visible = False
                    lblCSPrice.Visible = False
                    lblTimerCS.Visible = False
                    btnSolveAll.Visible = False
                    btnBNPPPrice.Visible = False
                    btnBNPPDeal.Visible = False
                    lblBNPP.Visible = False
                    lblBNPPPrice.Visible = False
                    lblTimerBNPP.Visible = False
                    btnUBSPrice.Visible = False
                    btnUBSDeal.Visible = False
                    lblUBS.Visible = False
                    lblUBSPrice.Visible = False
                    lblUBSTimer.Visible = False
                    btnBAMLPrice.Visible = False
                    btnBAMLDeal.Visible = False
                    lblBAML.Visible = False
                    lblBAMLPrice.Visible = False
                    lblTimerBAML.Visible = False
                    btnDBIBPrice.Visible = False
                    btnDBIBDeal.Visible = False
                    lblDBIB.Visible = False
                    lblDBIBPrice.Visible = False
                    lblTimerDBIB.Visible = False
                    TRBAML1.Visible = False
                    TRJPM1.Visible = False
                    TRHSBC1.Visible = False
                    TRCS1.Visible = False
                    TRUBS1.Visible = False
                    TRBNPP1.Visible = False
                    TRDBIB.Visible = False
		    TROCBC1.Visible = False
                    TRCITI1.Visible = False
                    TRLEONTEQ1.Visible = False
                    TRCOMMERZ1.Visible = False
            End Select

           
                    If dtLoginPP.Rows.Count > 0 Then
                        btnSolveAll.Visible = True
                        lblRangeCcy.Visible = True
                    Else
                        btnSolveAll.Visible = False
                        lblRangeCcy.Visible = False
                    End If
               
            Session.Add("PP_Login", dtLoginPP)
            Chk_Server_UpDown()
        Catch ex As Exception
            lblerror.Text = "chk_Login_For_PP:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chk_Login_For_PP", ErrorLevel.High)
            Throw ex

        End Try
    End Sub
    Public Sub chk_LinkUPDownStatus(ByVal dtGetLoginPP As DataTable, ByVal dtChkServer As DataTable)
        Dim dr As DataRow()
        Dim dr1 As DataRow()
        Try
            Dim blnLPIs_Up As Boolean
            blnLPIs_Up = False
            dr1 = dtGetLoginPP.Select("PP_CODE ='JPM'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "JPM" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then
                        blnLPIs_Up = True
                        btnJPMprice.Enabled = True
                        btnJPMprice.CssClass = "btn"
                        lblJPMPrice.Visible = True
                        chkJPM.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else
                        btnJPMprice.Enabled = False
                        btnJPMprice.CssClass = "btnDisabled"
                        lblJPMPrice.Visible = False
                        chkJPM.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnJPMprice.Enabled = False
                    btnJPMprice.CssClass = "btnDisabled"
                    lblJPMPrice.Visible = False
                    chkJPM.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
            dr1 = dtGetLoginPP.Select("PP_CODE ='HSBC'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "HSBC" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then
                        btnHSBCPrice.Enabled = True
                        btnHSBCPrice.CssClass = "btn"
                        lblHSBCPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkHSBC.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else
                        btnHSBCPrice.Enabled = False
                        btnHSBCPrice.CssClass = "btnDisabled"
                        lblHSBCPrice.Visible = False
                        chkHSBC.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnHSBCPrice.Enabled = False
                    btnHSBCPrice.CssClass = "btnDisabled"
                    lblHSBCPrice.Visible = False
                    chkHSBC.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
            dr1 = dtGetLoginPP.Select("PP_CODE ='CS'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "CS" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then

                        btnCSPrice.Enabled = True
                        btnCSPrice.CssClass = "btn"
                        lblCSPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkCS.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit> 
                    Else

                        btnCSPrice.Enabled = False
                        btnCSPrice.CssClass = "btnDisabled"
                        lblCSPrice.Visible = False
                        chkCS.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnCSPrice.Enabled = False
                    btnCSPrice.CssClass = "btnDisabled"
                    lblCSPrice.Visible = False
                    chkCS.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
            dr1 = dtGetLoginPP.Select("PP_CODE ='UBS'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "UBS" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then

                        btnUBSPrice.Enabled = True
                        btnUBSPrice.CssClass = "btn"
                        lblUBSPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkUBS.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else

                        btnUBSPrice.Enabled = False
                        btnUBSPrice.CssClass = "btnDisabled"
                        lblUBSPrice.Visible = False
                        chkUBS.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnUBSPrice.Enabled = False
                    btnUBSPrice.CssClass = "btnDisabled"
                    lblUBSPrice.Visible = False
                    chkUBS.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
            dr1 = dtGetLoginPP.Select("PP_CODE ='BNPP'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "BNPP" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then

                        btnBNPPPrice.Enabled = True
                        btnBNPPPrice.CssClass = "btn"
                        lblBNPPPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkBNPP.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else

                        btnBNPPPrice.Enabled = False
                        btnBNPPPrice.CssClass = "btnDisabled"
                        lblBNPPPrice.Visible = False
                        chkBNPP.Enabled = False  ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnBNPPPrice.Enabled = False
                    btnBNPPPrice.CssClass = "btnDisabled"
                    lblBNPPPrice.Visible = False
                    chkBNPP.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
            dr1 = dtGetLoginPP.Select("PP_CODE ='BAML'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "BAML" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then

                        btnBAMLPrice.Enabled = True
                        btnBAMLPrice.CssClass = "btn"
                        lblBAMLPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkBAML.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else

                        btnBAMLPrice.Enabled = False
                        btnBAMLPrice.CssClass = "btnDisabled"
                        lblBAMLPrice.Visible = False
                        chkBAML.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnBAMLPrice.Enabled = False
                    btnBAMLPrice.CssClass = "btnDisabled"
                    lblBAMLPrice.Visible = False
                    chkBAML.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
            'dr1 = dtGetLoginPP.Select("PP_CODE ='DBIB'") ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
            dr1 = dtGetLoginPP.Select("PP_CODE ='DB'")
            If dr1.Length > 0 Then
                'dr = dtChkServer.Select("Link_Provider_Name ='" & "DBIB" & "' ") ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                dr = dtChkServer.Select("Link_Provider_Name ='" & "DB" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then
                        btnDBIBPrice.Enabled = True
                        btnDBIBPrice.CssClass = "btn"
                        lblDBIBPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkDBIB.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit> 
                    Else
                        btnDBIBPrice.Enabled = False
                        btnDBIBPrice.CssClass = "btnDisabled"
                        lblDBIBPrice.Visible = False
                        chkDBIB.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnDBIBPrice.Enabled = False
                    btnDBIBPrice.CssClass = "btnDisabled"
                    lblDBIBPrice.Visible = False
                    chkDBIB.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
	    dr1 = dtGetLoginPP.Select("PP_CODE ='OCBC'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "OCBC" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then
                        btnOCBCPrice.Enabled = True
                        btnOCBCPrice.CssClass = "btn"
                        lblOCBCPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkOCBC.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else
                        btnOCBCPrice.Enabled = False
                        btnOCBCPrice.CssClass = "btnDisabled"
                        lblOCBCPrice.Visible = False
                        chkOCBC.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnOCBCprice.Enabled = False
                    btnOCBCprice.CssClass = "btnDisabled"
                    lblOCBCPrice.Visible = False
                    chkOCBC.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
	    dr1 = dtGetLoginPP.Select("PP_CODE ='CITI'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "CITI" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then
                        btnCITIPrice.Enabled = True
                        btnCITIPrice.CssClass = "btn"
                        lblCITIPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkCITI.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else
                        btnCITIPrice.Enabled = False
                        btnCITIPrice.CssClass = "btnDisabled"
                        lblCITIPrice.Visible = False
                        chkCITI.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnCITIprice.Enabled = False
                    btnCITIprice.CssClass = "btnDisabled"
                    lblCITIPrice.Visible = False
                    chkCITI.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
            dr1 = dtGetLoginPP.Select("PP_CODE ='LEONTEQ'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "LEONTEQ" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then
                        btnLEONTEQprice.Enabled = True
                        btnLEONTEQprice.CssClass = "btn"
                        lblLEONTEQPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkLEONTEQ.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else
                        btnLEONTEQprice.Enabled = False
                        btnLEONTEQprice.CssClass = "btnDisabled"
                        lblLEONTEQPrice.Visible = False
                        chkLEONTEQ.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnLEONTEQprice.Enabled = False
                    btnLEONTEQprice.CssClass = "btnDisabled"
                    lblLEONTEQPrice.Visible = False
                    chkLEONTEQ.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
            dr1 = dtGetLoginPP.Select("PP_CODE ='COMMERZ'")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "COMMERZ" & "' ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then
                        btnCOMMERZprice.Enabled = True
                        btnCOMMERZprice.CssClass = "btn"
                        lblCOMMERZPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkCOMMERZ.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else
                        btnCOMMERZprice.Enabled = False
                        btnCOMMERZprice.CssClass = "btnDisabled"
                        lblCOMMERZPrice.Visible = False
                        chkCOMMERZ.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnCOMMERZprice.Enabled = False
                    btnCOMMERZprice.CssClass = "btnDisabled"
                    lblCOMMERZPrice.Visible = False
                    chkCOMMERZ.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If
            If blnLPIs_Up = False Then
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
            Else
                btnSolveAll.Enabled = True
                btnSolveAll.CssClass = "btn"
            End If
        Catch ex As Exception
            lblerror.Text = "chk_LinkUPDownStatus:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chk_LinkUPDownStatus", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Function Chk_Server_UpDown() As String
        Dim dtChkServer As DataTable
        Dim strPPID As String = String.Empty
        Dim dtGetLoginPP As DataTable
        Dim dr2 As DataRow()
        Try
            dtChkServer = New DataTable("DUMMY")
            dtGetLoginPP = New DataTable("Login Priceprovider")
            dtGetLoginPP = CType(Session("PP_Login"), DataTable)

            Select Case objELNRFQ.Db_Chk_Server_Response(dtChkServer)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    dr2 = dtGetLoginPP.Select("Product_Code='ELN'")
                    If dr2.Length > 0 Then
                        chk_LinkUPDownStatus(dtGetLoginPP, dtChkServer)
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
            End Select
            Return ""
        Catch ex As Exception
            Return "Chk_Server_UpDown:Error occurred in filling Share."
            lblerror.Text = "Chk_Server_UpDown:Error occurred in filling Share."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Chk_Server_UpDown", ErrorLevel.High)
            Throw ex
        End Try
    End Function
#End Region
#Region "Populate Data"

    Public Sub Fill_Entity()
        Dim dtEntity As DataTable
        Try
            dtEntity = New DataTable("Entity")

            Select Case objELNRFQ.Get_Entity_for_Login(LoginInfoGV.Login_Info.LoginId, dtEntity)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlentity
                        .DataSource = dtEntity
                        .DataTextField = "entity_name"
                        .DataValueField = "entity_id"
                        .DataBind()
                    End With
                    ddlentity.SelectedValue = LoginInfoGV.Login_Info.EntityID.ToString()
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlentity
                        .DataSource = dtEntity
                        .DataBind()
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful

            End Select

        Catch ex As Exception
            lblerror.Text = "Error occurred in filling Entity."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Fill_Entity", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Sub fill_RMList()
        Dim dtRMList As DataTable
        Try
            dtRMList = New DataTable("RM List")
            ''<Dilkhush/AVinash 16Dec2015:BOS Added config to load deptname>
            Dim strDeptNameYN As String = objReadConfig.ReadConfig(dsConfig, "LSS_Capture_DeptName", "LSS", CStr(LoginInfoGV.Login_Info.EntityID), "NO")
            ''Select objELNRFQ.Get_RMList(ddlentity.SelectedValue, LoginInfoGV.Login_Info.LoginId, dtRMList)
            Select Case objELNRFQ.Get_RMList(ddlentity.SelectedValue, LoginInfoGV.Login_Info.LoginId, strDeptNameYN, dtRMList)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlRM
                        .DataSource = dtRMList
                        '<Rushikesh on 20-Oct-2015 to bind login name as text :JIRA-ID:FA-1158>
                        .DataTextField = "Host"
                        '</Rushikesh on 20-Oct-2015 to bind login name as text :JIRA-ID:FA-1158>
                        .DataValueField = "Rel_Manager_Name"
                        .DataBind()
                        If dtRMList.Rows.Count = 1 Then
                        Else
                            .Items.Insert(0, "")
                        End If
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlRM
                        .DataSource = dtRMList
                        .DataBind()
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            End Select
            fill_Email()
        Catch ex As Exception
            lblerror.Text = "Error occurred in filling RM's."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_RMList", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub fill_RFQRMList()
        Dim dtRMList As DataTable
        Try
            dtRMList = New DataTable("RM_List")
            ''<Dilkhush/AVinash 16Dec2015:BOS Added config to load deptname>
            Dim strDeptNameYN As String = objReadConfig.ReadConfig(dsConfig, "LSS_Capture_DeptName", "LSS", CStr(LoginInfoGV.Login_Info.EntityID), "NO")
            ''Select objELNRFQ.Get_RMList(ddlentity.SelectedValue, LoginInfoGV.Login_Info.LoginId, dtRMList)
            Select Case objELNRFQ.Get_RMList(ddlentity.SelectedValue, LoginInfoGV.Login_Info.LoginId, strDeptNameYN, dtRMList)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlRFQRM
                        .DataSource = dtRMList
                        '<Rushikesh on 20-Oct-2015 to bind login name as text :JIRA-ID:FA-1158>
                        .DataTextField = "Host"
                        '</Rushikesh on 20-Oct-2015 to bind login name as text :JIRA-ID:FA-1158>
                        .DataValueField = "Rel_Manager_Name"
                        .DataBind()
                        .Items.Insert(0, "")
                        If dtRMList.Rows.Count = 1 Then
                            .SelectedIndex = 1
                        Else
                            .SelectedIndex = 0
                        End If
                        '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                        Session.Add("ELN_DTRMList", dtRMList)
                        '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlRFQRM
                        .DataSource = dtRMList
                        .DataBind()
                        .Items.Insert(0, "")
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            End Select
            fill_Branch()
        Catch ex As Exception
            lblerror.Text = "Error occurred in filling RM's."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_RFQRMList", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub fill_All_EntityBooks()
        Dim dtBookList As DataTable
        Try
            dtBookList = New DataTable("EnityBooks")
            Select Case objELNRFQ.Get_All_Entity_Books(ddlentity.SelectedValue, dtBookList)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlAccount
                        .DataSource = dtBookList
                        .DataTextField = "BookName"
                        .DataValueField = "BookID"
                        .DataBind()
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlAccount
                        .DataSource = dtBookList
                        .DataBind()
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            End Select
        Catch ex As Exception
            lblerror.Text = "Error occurred in filling RM's."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_All_EntityBooks", ErrorLevel.High)
            Throw ex
        End Try
    End Sub


    Public Sub fill_Email()
        Dim dtBranchEmail As DataTable
        Try
            dtBranchEmail = New DataTable("Email-Branch")
            Select Case objELNRFQ.get_EmailId_Branch(ddlRM.SelectedValue, dtBranchEmail)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    lblEmail.Text = dtBranchEmail.Rows(0).Item("EmailId").ToString
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    lblEmail.Text = ""
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    lblEmail.Text = ""
            End Select
        Catch ex As Exception
            lblerror.Text = "fill_Email:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_Email", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub fill_Branch()
        Dim dtBranchEmail As DataTable
        Try
            dtBranchEmail = New DataTable("Email-Branch")
            Select Case objELNRFQ.get_EmailId_Branch(ddlRFQRM.SelectedValue, dtBranchEmail)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    lblbranch.Text = dtBranchEmail.Rows(0).Item("BookName").ToString
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    lblbranch.Text = ""
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    lblbranch.Text = ""
            End Select
        Catch ex As Exception
            lblerror.Text = "fill_Branch:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_Branch", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub SetTemplateDetails(ByVal SchemeName As String)
        Dim dtTemplate As DataTable = Nothing
        Dim dtexchange As DataTable = Nothing
        Dim TemplateId As String = ""
        strDefaultExchange = ""
        Try
            schemeCode = tabContainer.ActiveTabIndex.ToString
            dtTemplate = New DataTable("Template Code")
            Select Case objELNRFQ.DB_Get_TemplateDetails("ELN", dtTemplate)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtTemplate.Rows.Count > 0 Then
                        strDefaultExchange = dtTemplate.Rows(0).Item("ST_Exchange_Name").ToString
                        TemplateId = dtTemplate.Rows(0).Item("ST_Template_ID").ToString
                        Session.Add("Template_Code", TemplateId)
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
            End Select
        Catch ex As Exception
            lblerror.Text = "SetTemplateDetails:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "SetTemplateDetails", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
    Public Sub setRMLimit()
        Dim dblUserLimit As Double
        Try
            '  If ddlQuantoCcy.SelectedValue.Trim = "" Then
            If ddlShare.Text.Trim = "Please select valid share." Or ddlShare.SelectedValue.Trim = "" Then
                txt_RM_Limit.Text = "Max User Limit: "
                txt_RM_Limit.ToolTip = "Max User Limit: "
            Else
                Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, ddlQuantoCcy.SelectedValue, "ELN", dblUserLimit)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        txt_RM_Limit.Text = "Max User Limit(" + ddlQuantoCcy.SelectedValue + "): " + convertNotionalintoShort((If(dblUserLimit.ToString = "", 0, CDbl(dblUserLimit))), "MAX")
                        txt_RM_Limit.ToolTip = "Max User Limit(" + ddlQuantoCcy.SelectedValue + "): " + FormatNumber(dblUserLimit.ToString, 2).Replace(".00", "")
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                        txt_RM_Limit.Text = "Max User Limit: "
                        txt_RM_Limit.ToolTip = "Max User Limit: "
                        ' lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
                End Select
            End If
        Catch ex As Exception
            txt_RM_Limit.Text = "User Limit: "
            txt_RM_Limit.ToolTip = "User Limit: "
            lblerror.Text = "setRMLimit:Error occurred."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setRMLimit", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    '</Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>


    Public Sub Fillddl_QuantoCcy()
        Dim dtQuantoCcy As DataTable
        Dim I_Marketype As String = String.Empty
        Try
            dtQuantoCcy = New DataTable("DUMMY")
            I_Marketype = "EQ"
            Select Case objELNRFQ.Get_ProdBased_QuantoCcy("ELN", ddlShare.SelectedValue.ToString, dtQuantoCcy)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlQuantoCcy
                        .DataSource = dtQuantoCcy
                        .DataTextField = "CCY"
                        .DataValueField = "CCY"
                        .DataBind()
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlQuantoCcy
                        .DataSource = dtQuantoCcy
                        .DataBind()
                    End With
            End Select
            Session.Add("Quanto", dtQuantoCcy)
        Catch ex As Exception
            lblerror.Text = "Fillddl_QuantoCcy:Error occurred in filling Quanto Ccy."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Fillddl_QuantoCcy", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub fill_All_Exchange()
        Dim dtExchange As DataTable
        Try
            dtExchange = New DataTable("Exchange")
            Select Case objELNRFQ.DB_Fill_Exchange_Combo(dtExchange)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlExchange
                        .DataSource = dtExchange
                        .DataTextField = "Exchange"
                        .DataValueField = "Exchange_Name"
                        .DataBind()
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            Case "Y", "YES"
                                .Items.Insert(0, "ALL")
                                .Visible = False
                            Case "N", "NO"
                        End Select
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlExchange
                        .DataSource = dtExchange
                        .DataBind()
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful

                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error

            End Select
            Session.Add("Exchange_Details", dtExchange)
        Catch ex As Exception
            lblerror.Text = "fill_All_Exchange:Error occurred in filling exchange."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_All_Exchange", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub Fillddl_exchange()
        Dim dtExchange As DataTable
        Try
            dtExchange = New DataTable("Exchange")
            Select Case objELNRFQ.DB_Fill_Exchange_Combo(dtExchange)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlExchange
                        .DataSource = dtExchange
                        .DataTextField = "Exchange"
                        .DataValueField = "Exchange_Name"
                        .DataBind()
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            Case "Y", "YES"
                                .Items.Insert(0, "ALL")
                                .Visible = False
                            Case "N", "NO"
                        End Select
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    lblerror.Text = "No data found."
                    ddlExchange.DataSource = dtExchange
                    ddlExchange.DataBind()
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            End Select
        Catch ex As Exception
            lblerror.Text = "Fillddl_exchange:Error occurred in filling exchange."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Fillddl_exchange", ErrorLevel.High)
        End Try
    End Sub

    Public Sub fill_All_Shares()
        Dim dtShare As DataTable
        Dim I_Marketype As String = String.Empty
        Dim sDRAShareCcy As String
        Try
            dtShare = New DataTable("DUMMY")
            I_Marketype = "EQ"
            If (Not IsNothing(CType(Session("Share_Value"), DataTable)) AndAlso CType(Session("Share_Value"), DataTable).Rows.Count > 0) Then
                dtShare = CType(Session("Share_Value"), DataTable)
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        With ddlShare
                            .Items.Clear()
                            .DataSource = dtShare
                            .DataTextField = "LongName"
                            .DataValueField = "Code"
                            .DataBind()
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                Case "N", "NO"
                                    .Items.Insert(0, New RadComboBoxItem("", ""))
                            End Select
                            .SelectedIndex = 0
                        End With
                    Case "N", "NO"
                        Dim dtExchShares As DataTable
                        dtExchShares = New DataTable("Exchange_Shares")
                        dtExchShares = SelectIntoDataTable("ExchangeCode = '" + ddlExchange.SelectedValue + "'", dtShare)
                        With ddlShare
                            .Items.Clear()
                            .DataSource = dtExchShares
                            .DataTextField = "LongName"
                            .DataValueField = "Code"
                            .DataBind()
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                Case "N", "NO"
                                    .Items.Insert(0, New RadComboBoxItem("", ""))
                            End Select
                            .SelectedIndex = 0
                        End With
                End Select
            Else
                Select Case objELNRFQ.Db_Get_Share_Details(I_Marketype, "ALL", dtShare)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        Session.Add("Share_Value", dtShare)
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            Case "Y", "YES"
                                With ddlShare
                                    .Items.Clear()
                                    .DataSource = dtShare
                                    .DataTextField = "LongName"
                                    .DataValueField = "Code"
                                    .DataBind()
                                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                        Case "Y", "YES"
                                        Case "N", "NO"
                                            .Items.Insert(0, New RadComboBoxItem("", ""))
                                    End Select
                                    .SelectedIndex = 0
                                End With
                            Case "N", "NO"
                                Dim dtExchShares As DataTable
                                dtExchShares = New DataTable("Exchange_Shares")
                                dtExchShares = SelectIntoDataTable("ExchangeCode = '" + ddlExchange.SelectedValue + "'", dtShare)
                                With ddlShare
                                    .Items.Clear()
                                    .DataSource = dtExchShares
                                    .DataTextField = "LongName"
                                    .DataValueField = "Code"
                                    .DataBind()
                                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                        Case "Y", "YES"
                                        Case "N", "NO"
                                            .Items.Insert(0, New RadComboBoxItem("", ""))
                                    End Select
                                    .SelectedIndex = 0
                                End With
                        End Select
                    Case Else
                        Select Case objELNRFQ.Db_Get_Share_Details(I_Marketype, ddlExchange.SelectedValue, dtShare)
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                With ddlShare
                                    .Items.Clear()
                                    .DataSource = dtShare
                                    .DataTextField = "LongName"
                                    .DataValueField = "Code"
                                    .DataBind()
                                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                        Case "Y", "YES"
                                        Case "N", "NO"
                                            .Items.Insert(0, New RadComboBoxItem("", ""))
                                    End Select
                                    .SelectedIndex = 0
                                End With

                            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                                Session.Add("Share_Value", dtShare)
                                With ddlShare
                                    .DataSource = dtShare
                                    .DataBind()
                                End With
                        End Select
                End Select
            End If

        Catch ex As Exception
            lblerror.Text = "Fillddl_Share:Error occurred in filling Share."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_All_Shares", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub Fillddl_Share()

        Dim dtShare As DataTable
        Dim I_Marketype As String = String.Empty
        Try
            dtShare = New DataTable("DUMMY")
            If (Not IsNothing(CType(Session("Share_Value"), DataTable)) AndAlso CType(Session("Share_Value"), DataTable).Rows.Count > 0) Then
                dtShare = CType(Session("Share_Value"), DataTable)
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        With ddlShare
                            .Items.Clear()
                            .DataSource = dtShare
                            .DataTextField = "LongName"
                            .DataValueField = "Code"
                            .DataBind()
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                Case "N", "NO"
                                    .Items.Insert(0, New RadComboBoxItem("", ""))
                            End Select
                            .SelectedIndex = 0
                        End With
                    Case "N", "NO"
                        Dim dtExchShares As DataTable
                        dtExchShares = New DataTable("Exchange_Shares")
                        dtExchShares = SelectIntoDataTable("ExchangeCode = '" + ddlExchange.SelectedValue + "'", dtShare)
                        With ddlShare
                            .Items.Clear()
                            .DataSource = dtExchShares
                            .DataTextField = "LongName"
                            .DataValueField = "Code"
                            .DataBind()
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                Case "N", "NO"
                                    .Items.Insert(0, New RadComboBoxItem("", ""))
                            End Select
                            .SelectedIndex = 0
                        End With
                End Select
            Else
                I_Marketype = "EQ"
                Select Case objELNRFQ.Db_Get_Share_Details(I_Marketype, ddlExchange.SelectedValue, dtShare)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        With ddlShare
                            .Items.Clear()
                            .DataSource = dtShare
                            .DataTextField = "LongName"
                            .DataValueField = "Code"
                            .DataBind()
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                Case "N", "NO"
                                    .Items.Insert(0, New RadComboBoxItem("", ""))
                            End Select
                            .SelectedIndex = 0
                        End With
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                        With ddlShare
                            .DataSource = dtShare
                            .DataBind()
                        End With
                End Select
            End If
            GetCommentary()
        Catch ex As Exception
            lblerror.Text = "Fillddl_Share:Error occurred in filling Share."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Fillddl_Share", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub fill_grid()
        Dim dt As DataTable
        Try
            EnableTimerTick()
            ''''Dilkhush 23sep2016 LoadAll RFQ or Best RFQ
            Dim strExpandAll As String
            If chkExpandAllRFQ.Checked Then
                strExpandAll = "YES"
            Else
                strExpandAll = "NO"
            End If
            ''''Dilkhush 23sep2016 LoadAll RFQ or Best RFQ

            dt = New DataTable("DUMMY")
            '<AvinashG. on 26-Oct-2015: FA-1163 Display of 'Self' , 'Group' and 'All' drop down on order blotter on main pricer page >
            Dim strMode As String = If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "SELF", "SELF", If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "GROUP", "GRP", "ALL"))
            '</AvinashG. on 26-Oct-2015: FA-1163 Display of 'Self' , 'Group' and 'All' drop down on order blotter on main pricer page >
            ''Dilkhush 23Sep2016 added variable load RFQ on condtion basis
            ''Select objELNRFQ.DB_Get_RFQ("ELN", LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode,  dt)
            Select Case objELNRFQ.DB_Get_RFQ("ELN", LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, strExpandAll, dt)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
    
                    If dt.Rows.Count > 0 Then
                        grdELNRFQ.CurrentPageIndex = 0
                        grdELNRFQ.DataSource = dt
                        grdELNRFQ.DataBind()

                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    grdELNRFQ.CurrentPageIndex = 0
                    dt = dt.Clone
                    grdELNRFQ.DataSource = dt
                    grdELNRFQ.DataBind()
            End Select
            Session.Add("ELN_RFQ", dt)
        Catch ex As Exception
            lblerror.Text = "fill_grid:Error occurred in filling grid."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_grid", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub fill_OrderGrid()
        Dim dt As DataTable
        Try
            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            'Mohit Lalwani on 1-Aug-2016
            EnableTimerTick()
            Dim strSchemeName As String = "ELN"
            dt = New DataTable("DUMMY")
            '<AvinashG. on 26-Oct-2015: FA-1163 Display of 'Self' , 'Group' and 'All' drop down on order blotter on main pricer page >
            Dim strMode As String = If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "SELF", "SELF", If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "GROUP", "GRP", "ALL"))
            '</AvinashG. on 26-Oct-2015: FA-1163 Display of 'Self' , 'Group' and 'All' drop down on order blotter on main pricer page >
            Select Case objELNRFQ.DB_Get_Order_History("ELN", strSchemeName, LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, dt)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dt.Rows.Count > 0 Then
                        grdOrder.CurrentPageIndex = 0
                        grdOrder.DataSource = dt
                        grdOrder.DataBind()
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    grdOrder.CurrentPageIndex = 0
                    dt = dt.Clone
                    grdOrder.DataSource = dt
                    grdOrder.DataBind()
            End Select
            Session.Add("Order", dt)
        Catch ex As Exception
            lblerror.Text = "fill_OrderGrid:Error occurred in filling order grid."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_OrderGrid", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub Get_Price_Provider()
        Dim dtPairCode As DataTable
        Try
            dtPairCode = New DataTable("Price Provider")
            Select Case objELNRFQ.DB_Get_All_Of_PriceProvider(PP_CODE, dtPairCode)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    Session.Add("Provide_Id", dtPairCode)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    lblerror.Text = "No record(s) for price provider is found."
            End Select
        Catch ex As Exception
            lblerror.Text = "Get_Price_Provider:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Get_Price_Provider", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
#End Region
#Region "Get Commentry"
    Private Sub GetCommentary()
        Dim strELNCommentary As StringBuilder
        Dim sbComm As StringBuilder
        Dim sbMailComm As StringBuilder
        Dim strTempJS As StringBuilder
        Dim sbMailCommScb As StringBuilder '<Rushikesh on 30-Aug-16 for new email format to scb>
        Dim MRating, SPRating, FRating, bestLP As String
        Dim PriceOrStrikeMail As String
        Dim IssuerMail As String
        Dim IssuePriceMail As String
        Dim lst As ListItem
        Try
            '<AvinashG. on 05-Jan-2016: Initialization should be in Try Block>
            sbComm = New StringBuilder()
            sbMailComm = New StringBuilder()
            strTempJS = New StringBuilder()
            '</AvinashG. on 05-Jan-2016: Initialization should be in Try Block>
            sbMailCommScb = New StringBuilder() '<Rushikesh on 30-Aug-16 for new email format to scb>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allowed_QuoteMailing", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    If tabContainer.ActiveTabIndex = prdTab.ELN Then
                        lblMailComentry.Text = ""
                        If ddlShare.SelectedValue.ToString.Trim <> "" Then
                            sbComm.AppendLine("<TABLE cellpadding='0' style=' margin-top:-2px;'><TR><TD COLSPAN=""1"" style=""font-size:12px;""><span class='fieldInfo'>!</span></TD><TD  COLSPAN=""2"" style=""font-size:12px;"">RFQ Details (ELN)</TD></TR>")


                            Dim tempCounterRFQDetails As Integer
                            tempCounterRFQDetails = 1

                            sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Tenor</TD><TD style='padding-left: 23px;'>&nbsp; " + txtTenor.Text + ddlTenorTypeELN.SelectedItem.Text.Substring(0, 1) + "(TD = " + FinIQApp_Date.FinIQDate(txtTradeDate.Value) + "; VD =  " + _
                                              FinIQApp_Date.FinIQDate(txtSettlementDate.Value) + "; ED = " + FinIQApp_Date.FinIQDate(txtExpiryDate.Value) + "; MD = " + FinIQApp_Date.FinIQDate(txtMaturityDate.Value) + ")</TD></TR>")
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            'sbComm.AppendLine("<TR><TD  >Underlying</TD><TD>&nbsp; " + ddlShare.Text + "</TD></TR>")
                            If ddlShare.SelectedValue IsNot Nothing Then               'Changed by Mohit Lalwani on 7-Apr2-16 Jira:FA-1392 Underlying field is not present in commentry for ELN 
                                sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Underlying</TD><TD style='padding-left: 23px;'>&nbsp; " + ddlShare.Text + "</TD></TR>")        'Changed by Rushikesh on 26-Feb-2016 Jira:FA-1319
                                tempCounterRFQDetails = tempCounterRFQDetails + 1
                            End If
                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Strike</TD><TD style='padding-left: 23px;'>&nbsp; " + If(ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE", FormatNumber(Val(txtStrike.Text.Replace(",", "")), 2), hdnBestIssuer.Value) + "% of Spot" + "</TD></TR>") ''<Nikhil M. on 24-Sep-2016: Removed [] >
                            tempCounterRFQDetails = tempCounterRFQDetails + 1

                            ''<AshwiniP on 05-Oct-2016>
                            If chkELNType.Checked Then
                                sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>KO</TD><TD  style='padding-left: 30px;'> " + FormatNumber(Val(txtBarrier.Text.Replace(",", "")), 2) + "% of Spot (" + ddlBarrier.SelectedItem.Text + ")</TD></TR>")
                                tempCounterRFQDetails = tempCounterRFQDetails + 1
                            End If
                            ''</AshwiniP on 05-Oct-2016>

                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Issue Price</TD><TD  style='padding-left: 23px;'>&nbsp; " + If(ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE", " " + hdnBestIssuer.Value + " ", FormatNumber(Val(txtELNPrice.Text.Replace(",", "")), 2)) + "% of Spot" + "</TD></TR>") ''<Nikhil M. on 20-Sep-2016:Added hdnBestIssuer.value >
                            tempCounterRFQDetails = tempCounterRFQDetails + 1

                            '<Changed by Mohit Lalwani on 30-Nov-2015>
                            'sbComm.AppendLine("<TR><TD  >Upfront</TD><TD>&nbsp; " + FormatNumber(Val(txtUpfrontELN.Text.Replace(",", "")), 2) + "</TD></TR>")
                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Upfront</TD><TD  style='padding-left: 23px;'>&nbsp; " + FormatNumber(Val(txtUpfrontELN.Text.Replace(",", "")), 2) + "%</TD></TR>")
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            '</Changed by Mohit Lalwani on 30-Nov-2015>
                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Client Yield</TD><TD  style='padding-left: 23px;'>&nbsp; " + " " + hdnstrBestClientYeild.Value + " % p.a." + "</TD></TR>") ''<Nikhil M. on 20-Sep-2016: Added  hdnstrBestClientYeild.value>
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            ''sbComm.AppendLine("<TR><TD  >Notional</TD><TD>&nbsp; " + ddlQuantoCcy.SelectedItem.Text + FormatNumber(Val(txtQuantity.Text.Replace(",", "")), 2) + "</TD></TR>")
                            sbComm.AppendLine("<TR><TD  Style='color:#919191' ><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Notional</TD><TD  style='padding-left: 23px;'>&nbsp; " + ddlQuantoCcy.SelectedItem.Text + " " + SetNumberFormat(Val(txtQuantity.Text.Replace(",", "")), 0) + "</TD></TR>")  '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                            sbComm.AppendLine("</TABLE>")

                            ''If ddlShare.SelectedItem IsNot Nothing Then
                            'ddlShare.Text = ddlShare.SelectedItem.Text
                            ''sbMailComm.AppendLine(txtTenor.Text + ddlTenorTypeELN.SelectedItem.Text.Substring(0, 1) + " " + ddlQuantoCcy.SelectedItem.Text + If(chkQuantoCcy.Checked, "(q)", "") + " denominated " + If(chkELNType.Checked, "KO ELN on ", "ELN on ") + ddlShare.SelectedItem.Text + "#")
                            ' '' '' '' ''sbMailComm.AppendLine(txtTenor.Text + ddlTenorTypeELN.SelectedItem.Text.Substring(0, 1) + " " + ddlQuantoCcy.SelectedItem.Text + If(chkQuantoCcy.Checked, "(q)", "") + " denominated " + If(chkELNType.Checked, "KO ELN on ", "ELN on ") + ddlShare.Text + "#")
                            ' '' '' '' '' ''Else
                            ' '' '' '' '' ''End If

                            ' '' '' '' ''sbMailComm.AppendLine("#")
                            ' '' '' '' ''sbMailComm.AppendLine("TD              = " + FinIQApp_Date.FinIQDate(txtTradeDate.Value) + "     ; VD     = " + FinIQApp_Date.FinIQDate(txtSettlementDate.Value) + "#")
                            ' '' '' '' ''sbMailComm.AppendLine("ED              = " + FinIQApp_Date.FinIQDate(txtExpiryDate.Value) + "     ; MD     = " + FinIQApp_Date.FinIQDate(txtMaturityDate.Value) + "#")
                            ' '' '' '' ''sbMailComm.AppendLine("#")
                            ' '' '' '' ''sbMailComm.Append("Strike          = " + If(ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE", FormatNumber(Val(txtStrike.Text.Replace(",", "")), 2), "[  ]") + "% of Spot")
                            ' '' '' '' ''sbMailComm.Append(If(chkELNType.Checked, " ; KO      =" + FormatNumber(Val(txtBarrier.Text.Replace(",", "")), 2) + "% of Spot", "")).AppendLine("#")
                            ' '' '' '' ''sbMailComm.Append("Issue Price  = " + If(ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE", "[  ]", FormatNumber(Val(txtELNPrice.Text.Replace(",", "")), 2)) + "% of Spot" + "#")
                            ' '' '' '' ''sbMailComm.AppendLine("#")
                            ' '' '' '' '' ''                     sbMailComm.AppendLine("Notional     = " + ddlQuantoCcy.SelectedItem.Text + " " + FormatNumber(Val(txtQuantity.Text.Replace(",", "")), 2) + "#")
                            ' '' '' '' ''sbMailComm.AppendLine("Notional     = " + ddlQuantoCcy.SelectedItem.Text + " " + SetNumberFormat(Val(txtQuantity.Text.Replace(",", "")), 0) + "#")  '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                            '' '' '' '' ''lblMailComentry.Text = sbMailComm.ToString()


                            '<Rushikesh on 30-Aug-16 for new email format to scb>

                            ''bestLP = CheckBestPriceForEmail()



                            ''GetIssuerRatingForMail(bestLP, MRating, SPRating, FRating)
                            ''Select Case bestLP
                            ''    Case "JPM"
                            ''        PriceOrStrikeMail = lblJPMClientPrice.Text
                            ''        IssuerMail = "JPM"
                            ''        IssuePriceMail = lblJPMPrice.Text

                            ''    Case "HSBC"
                            ''        PriceOrStrikeMail = lblHSBCClientPrice.Text
                            ''        IssuerMail = "HSBC"
                            ''        IssuePriceMail = lblHSBCPrice.Text

                            ''    Case "CS"
                            ''        PriceOrStrikeMail = lblCSClientPrice.Text
                            ''        IssuerMail = "CS"
                            ''        IssuePriceMail = lblCSPrice.Text

                            ''    Case "UBS"
                            ''        PriceOrStrikeMail = lblUBSClientPrice.Text
                            ''        IssuerMail = "UBS"
                            ''        IssuePriceMail = lblUBSPrice.Text

                            ''    Case "BNPP"
                            ''        PriceOrStrikeMail = lblBNPPClientPrice.Text
                            ''        IssuerMail = "BNPP"
                            ''        IssuePriceMail = lblBNPPPrice.Text

                            ''    Case "BAML"
                            ''        PriceOrStrikeMail = lblBAMLClientPrice.Text
                            ''        IssuerMail = "BAML"
                            ''        IssuePriceMail = lblBAMLPrice.Text

                            ''    Case "DBIB"
                            ''        PriceOrStrikeMail = lblDBIBClientPrice.Text
                            ''        IssuerMail = "DB"
                            ''        IssuePriceMail = lblDBIBPrice.Text

                            ''    Case "OCBC"
                            ''        PriceOrStrikeMail = lblOCBCClientPrice.Text
                            ''        IssuerMail = "OCBC"
                            ''        IssuePriceMail = lblOCBCPrice.Text

                            ''    Case "CITI"
                            ''        PriceOrStrikeMail = lblCITIClientPrice.Text
                            ''        IssuerMail = "CITI"
                            ''        IssuePriceMail = lblCITIPrice.Text

                            ''    Case Nothing, ""
                            ''        PriceOrStrikeMail = ""
                            ''        IssuerMail = ""
                            ''        IssuePriceMail = ""

                            ''End Select

                            ''sbMailCommScb.AppendLine(txtTenor.Text + ddlTenorTypeELN.SelectedItem.Text.Substring(0, 1) + " " + ddlQuantoCcy.SelectedItem.Text + If(chkQuantoCcy.Checked, "(q)", "") + " denominated " + If(chkELNType.Checked, "KO ELN on ", "ELN on ") + ddlShare.Text + "#")
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Product : ELN")
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Reference Stock : " + ddlShare.Text)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Stock Code : " + ddlShare.SelectedValue)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Stock Ccy : " + ddlQuantoCcy.SelectedItem.Text)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Spot : " + lblSpotValue.Text)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Trade Date : " + txtTradeDate.Value)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Settl. Date : " + txtSettlementDate.Value)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Expiry Date : " + txtExpiryDate.Value)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Mat. Date : " + txtMaturityDate.Value)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Tenor : " + txtTenor.Text + ddlTenorTypeELN.SelectedItem.Text.Substring(0, 1))
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Strike : " + If(ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE", FormatNumber(Val(txtStrike.Text.Replace(",", "")), 2), IssuePriceMail))
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Issue Price : " + If(ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE", IssuePriceMail, FormatNumber(Val(txtELNPrice.Text.Replace(",", "")), 2)) + "% of Spot")
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Upfront : " + FormatNumber(Val(txtUpfrontELN.Text.Replace(",", "")), 2))
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Notional : " + ddlQuantoCcy.SelectedItem.Text + SetNumberFormat(Val(txtQuantity.Text.Replace(",", "")), 0))
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.AppendLine("Moody's Rating : " + MRating) ''Moody's Rating
                            ''sbMailCommScb.AppendLine("Standard and Poor's Rating : " + SPRating) ''S&P Rating
                            ''sbMailCommScb.AppendLine("Fitch Rating : " + FRating) ''Fitch Rating
                            ''lblMailComentry.Text = sbMailCommScb.ToString()

                            '</Rushikesh on 30-Aug-16 for new email format to scb>



                            '<RiddhiS. on 06-Sep-2016: for sending mail to selected Price providers>

                            sbMailCommScb.AppendLine(txtTenor.Text + ddlTenorTypeELN.SelectedItem.Text.Substring(0, 1) + " " + ddlQuantoCcy.SelectedItem.Text + If(chkQuantoCcy.Checked, "(q)", "") + " denominated " + If(chkELNType.Checked, "KO ELN on ", "ELN on ") + ddlShare.Text + "#")
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Product : ELN")
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Reference Stock : " + ddlShare.Text)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Stock Code : " + ddlShare.SelectedValue)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Stock Ccy : " + ddlQuantoCcy.SelectedItem.Text)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Spot : " + lblSpotValue.Text)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Trade Date : " + txtTradeDate.Value)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Settl. Date : " + txtSettlementDate.Value)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Expiry Date : " + txtExpiryDate.Value)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Mat. Date : " + txtMaturityDate.Value)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Tenor : " + txtTenor.Text + ddlTenorTypeELN.SelectedItem.Text.Substring(0, 1))
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Upfront : " + FormatNumber(Val(txtUpfrontELN.Text.Replace(",", "")), 2))
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Notional : " + ddlQuantoCcy.SelectedItem.Text + SetNumberFormat(Val(txtQuantity.Text.Replace(",", "")), 0))
                            sbMailCommScb.AppendLine("#")


                            For Each lst In chkPPmaillist.Items
                                If lst.Selected = True Then
                                    Select Case lst.Value
                                        Case "JPM"
                                            PriceOrStrikeMail = lblJPMClientPrice.Text
                                            IssuerMail = "JPM"
                                            IssuePriceMail = lblJPMPrice.Text

                                        Case "HSBC"
                                            PriceOrStrikeMail = lblHSBCClientPrice.Text
                                            IssuerMail = "HSBC"
                                            IssuePriceMail = lblHSBCPrice.Text

                                        Case "CS"
                                            PriceOrStrikeMail = lblCSClientPrice.Text
                                            IssuerMail = "CS"
                                            IssuePriceMail = lblCSPrice.Text

                                        Case "UBS"
                                            PriceOrStrikeMail = lblUBSClientPrice.Text
                                            IssuerMail = "UBS"
                                            IssuePriceMail = lblUBSPrice.Text

                                        Case "BNPP"
                                            PriceOrStrikeMail = lblBNPPClientPrice.Text
                                            IssuerMail = "BNPP"
                                            IssuePriceMail = lblBNPPPrice.Text

                                        Case "BAML"
                                            PriceOrStrikeMail = lblBAMLClientPrice.Text
                                            IssuerMail = "BAML"
                                            IssuePriceMail = lblBAMLPrice.Text

                                        Case "DBIB"
                                            PriceOrStrikeMail = lblDBIBClientPrice.Text
                                            IssuerMail = "DB"
                                            IssuePriceMail = lblDBIBPrice.Text

                                        Case "OCBC"
                                            PriceOrStrikeMail = lblOCBCClientPrice.Text
                                            IssuerMail = "OCBC"
                                            IssuePriceMail = lblOCBCPrice.Text

                                        Case "CITI"
                                            PriceOrStrikeMail = lblCITIClientPrice.Text
                                            IssuerMail = "CITI"
                                            IssuePriceMail = lblCITIPrice.Text

				Case "LEONTEQ"
                                    PriceOrStrikeMail = lblLEONTEQClientPrice.Text
                                    IssuerMail = "LEONTEQ"
                                    IssuePriceMail = lblLEONTEQPrice.Text

                                Case "COMMERZ"
                                    PriceOrStrikeMail = lblCOMMERZClientPrice.Text
                                    IssuerMail = "COMMERZ"
                                    IssuePriceMail = lblCOMMERZPrice.Text
                                        Case Nothing, ""
                                            PriceOrStrikeMail = ""
                                            IssuerMail = ""
                                            IssuePriceMail = ""

                                    End Select
                                    GetIssuerRatingForMail(lst.Value, MRating, SPRating, FRating)
                                    sbMailCommScb.AppendLine("#")
                                    sbMailCommScb.Append("Issuer : " + lst.Text)
                                    sbMailCommScb.AppendLine("#")
                                    sbMailCommScb.Append("Strike : " + If(ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE", FormatNumber(Val(txtStrike.Text.Replace(",", "")), 2), IssuePriceMail))
                                    sbMailCommScb.AppendLine("#")
                                    sbMailCommScb.Append("Issue Price : " + If(ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE", IssuePriceMail, FormatNumber(Val(txtELNPrice.Text.Replace(",", "")), 2)))
                                    sbMailCommScb.AppendLine("#")
                                    sbMailCommScb.Append("Client Price : " + PriceOrStrikeMail)
                                    sbMailCommScb.AppendLine("#")
                                    sbMailCommScb.AppendLine("Moody's Rating : " + MRating) ''Moody's Rating
                                    sbMailCommScb.AppendLine("Standard and Poor's Rating : " + SPRating) ''S&P Rating
                                    sbMailCommScb.AppendLine("Fitch Rating : " + FRating) ''Fitch Rating
                                End If
                            Next
                            lblMailComentry.Text = sbMailCommScb.ToString()
                            '<RiddhiS. on 06-Sep-2016: for sending mail to selected Price providers>
                        Else

                        End If
                    Else

                    End If
                Case "N", "NO"

            End Select
                    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    ''If tabContainer.ActiveTabIndex = prdTab.ELN And ddlShare.SelectedItem Is Nothing Then
                    ''   lblComentry.Text = ""
                    ''    strTempJS.AppendLine("hideEmail();")
                    '' ElseIf tabContainer.ActiveTabIndex = prdTab.ELN And ddlShare.SelectedItem.Value = "" Then
                    If tabContainer.ActiveTabIndex = prdTab.ELN And ddlShare.SelectedValue = "" Then
                        lblComentry.Text = ""
                strTempJS.AppendLine("try{ hideEmail(); } catch(e){ }")			'Mohit Lalwani on 26-Oct-2016
                    Else
                        '<AvinashG. on 24-Aug-2015: FA-1014 - Main Pricer: Quote Mailing Option >
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allowed_QuoteMailing", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            Case "Y", "YES"
                                'btnMail.Visible = True
                                'Avinash/Shekar:-Hide/Show mailing button
                        strTempJS.AppendLine("try{ showEmail(); } catch(e){ }")				'Mohit Lalwani on 26-Oct-2016
                        ''System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "hideEmail12", "try{ showEmail(); } catch(e){ }", True) 		'Mohit Lalwani on 26-Oct-2016
                            Case "N", "NO"
                                'Do nothing'
                        End Select
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Display_MailText_As_Narration", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            Case "Y", "YES"
                                ''lblComentry.Text = If(lblMailComentry.Text.Trim = "", "", _
                                ''                              lblMailComentry.Text.Remove(lblMailComentry.Text.LastIndexOf("#"), 1).Replace("##", "#").Replace("#" + Environment.NewLine, "; ").Replace("; ;", ";")) '<AvinashG. on 15-Sep-2015: Replace unwanted characters from display>
                                lblComentry.Text = sbComm.ToString()
                            Case "N", "NO"
                                strELNCommentary = New StringBuilder()
                                SchemeName = Convert.ToString(Session("Scheme"))

                                If ddlSolveFor.SelectedValue = "PricePercentage" Then

                                    If chkQuantoCcy.Checked = True Then
                                        strELNCommentary.Append("You are buying " & ddlQuantoCcy.SelectedValue.Trim & " " & txtQuantity.Text _
                                        & " quantity of " & SchemeName & " on " & ddlShare.SelectedValue.ToString & " share starting from " & txtSettlementDate.Value & _
                                        " for tenor of " & txtTenor.Text & " " & ddlTenorTypeELN.SelectedValue.Substring(0, 1).ToUpper + ddlTenorTypeELN.SelectedValue.Substring(1).ToLower _
                                        & " with strike of  " & txtStrike.Text & " % of Spot.")

                                    Else
                                        strELNCommentary.Append("You are buying " & lblELNBaseCcy.Text.Trim & " " & txtQuantity.Text _
                                        & " quantity of " & SchemeName & " on " & ddlShare.SelectedValue.ToString & " share starting from " & txtSettlementDate.Value & _
                                        " for tenor of " & txtTenor.Text & " " & ddlTenorTypeELN.SelectedValue.Substring(0, 1).ToUpper + ddlTenorTypeELN.SelectedValue.Substring(1).ToLower _
                                        & " with strike of  " & txtStrike.Text & " % of Spot.")

                                    End If
                                    lblComentry.Text = strELNCommentary.ToString
                                Else
                                    If chkQuantoCcy.Checked = True Then
                                        strELNCommentary.Append("You are buying " & ddlQuantoCcy.SelectedValue.Trim & " " & txtQuantity.Text _
                                       & " quantity of " & SchemeName & " on " & ddlShare.SelectedValue.ToString & " share starting from " & txtSettlementDate.Value & _
                                       " for tenor of " & txtTenor.Text & " " & ddlTenorTypeELN.SelectedValue.Substring(0, 1).ToUpper + ddlTenorTypeELN.SelectedValue.Substring(1).ToLower _
                                        & " with price of  " & txtELNPrice.Text & " % .")

                                    Else
                                        strELNCommentary.Append("You are buying " & lblELNBaseCcy.Text.Trim & " " & txtQuantity.Text _
                                       & " quantity of " & SchemeName & " on " & ddlShare.SelectedValue.ToString & " share starting from " & txtSettlementDate.Value & _
                                       " for tenor of " & txtTenor.Text & " " & ddlTenorTypeELN.SelectedValue.Substring(0, 1).ToUpper + ddlTenorTypeELN.SelectedValue.Substring(1).ToLower _
                                       & " with price of  " & txtELNPrice.Text & " % .")

                                    End If
                                    lblComentry.Text = strELNCommentary.ToString
                                End If



                        End Select

                    End If
                    pnlReprice.Update()
                    upnlCommentry.Update()
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "hideEmail12", strTempJS.ToString, True)
        Catch ex As Exception
            lblerror.Text = "GetCommentary:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "GetCommentary", ErrorLevel.High)

            Throw ex
        End Try
    End Sub
#End Region
#Region "Fill Chart"
    Public Sub Get_RFQ_orderperformanceChart()

        Dim dtOrderP As DataTable
        Chart1.Visible = True
        Chart2.Visible = True
        Dim ProductType As String
        ProductType = "ELN"
        Try
            dtOrderP = New DataTable("Order Performance")
            Select Case objELNRFQ.DB_Get_orderperformance_Details_Chart(ProductType, LoginInfoGV.Login_Info.LoginId, dtOrderP)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtOrderP.Rows.Count > 0 Then
                    Else
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            End Select
        Catch ex As Exception
            lblerror.Text = "Get_RFQ_orderperformanceChart:Error occurred in filling chart."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Get_RFQ_orderperformanceChart", ErrorLevel.High)
        End Try
    End Sub


    Public Sub Get_RFQ_ColumnChart()

        Dim dtOrderPrice As DataTable
        Chart1.Visible = True
        Chart2.Visible = True
        Dim ProductType As String
        ProductType = "ELN"
        Try
            dtOrderPrice = New DataTable("Order To PriceRatio")
            Select Case objELNRFQ.DB_Get_ColumnChart_Details(ProductType, LoginInfoGV.Login_Info.LoginId, dtOrderPrice)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtOrderPrice.Rows.Count > 0 Then
                        DrawColumnChart(dtOrderPrice, "PP_CODE", "OrderToPriceRatio", Chart2, "Default", False, "", SeriesChartType.Bar)
                        Chart2.Visible = True
                    Else
                        Chart2.Visible = False
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    Chart2.Visible = False
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            End Select
        Catch ex As Exception
            lblerror.Text = "Get_RFQ_ColumnChart:Error occurred in filling chart."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Get_RFQ_ColumnChart", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub Get_RFQ_PieChart()
        Dim dtMaxCount As DataTable
        Dim strProductType As String
        strProductType = "ELN"
        Try
            dtMaxCount = New DataTable("Max Count")
            Select Case objELNRFQ.DB_Get_RFQ_Details_Chart(strProductType, LoginInfoGV.Login_Info.LoginId, dtMaxCount)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtMaxCount.Rows.Count > 0 Then
                        DrawPieChart(dtMaxCount, "PP_CODE", "MaxCount", "Default", False, "", Chart1)
                        Chart1.Visible = True
                    Else
                        Chart1.Visible = False
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    Chart1.Visible = False
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            End Select
        Catch ex As Exception
            lblerror.Text = ""
            lblerror.Text = "Get_RFQ_PieChart:Error occurred in filling chart."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Get_RFQ_PieChart", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub DrawColumnChart(ByVal _ChartData As DataTable, _
                         ByVal _AxisXValueMember As String, _
                         ByVal _AxisYValueMember As String, _
                         ByVal Chart As System.Web.UI.DataVisualization.Charting.Chart, _
                         ByVal _SeriesName As String, _
                         ByVal _ShowValuesWithAmountMultiplier As Boolean, _
                         ByVal _ChartTitle As String, ByVal strChartType As System.Web.UI.DataVisualization.Charting.SeriesChartType)
        Dim i As Integer = 0
        Try
            Dim colorBackGround As Color
            colorBackGround = Color.FromArgb(252, 252, 252)
            Chart.Series.Clear()
            Chart.BackColor = colorBackGround
            Chart.BackGradientStyle = _ChartBackGradientStyle
            Chart.BorderlineDashStyle = _ChartBorderlineDashStyle
            Chart.BorderSkin.SkinStyle = _ChartBorderSkinStyle
            Chart.ChartAreas("Default").BackColor = colorBackGround
            Chart.ChartAreas("Default").BackGradientStyle = GradientStyle.None
            Chart.ChartAreas("Default").InnerPlotPosition.X = 12
            Chart.ChartAreas("Default").InnerPlotPosition.Y = 10
            Chart.ChartAreas("Default").AxisX.TitleFont = New Font("arial", 1.0F, FontStyle.Regular)
            Chart.ChartAreas("Default").AxisY.TitleFont = New Font("arial", 1.0F, FontStyle.Regular)
            Chart.ChartAreas("Default").InnerPlotPosition.Height = 80
            Chart.ChartAreas("Default").InnerPlotPosition.Width = 80
            Chart.ChartAreas("Default").InnerPlotPosition.Auto = False
            Chart.Legends("Default").Enabled = True
            Chart.Legends(0).Docking = _DockingLocation
            If _ChartData Is Nothing OrElse _ChartData.Rows.Count = 0 Then
                Throw New Exception("No data to display")
            End If
            If Not _ChartData.Columns.Contains(_AxisXValueMember) Then
                Throw New Exception("Column " & _AxisXValueMember & " not found in datasource.")
            End If
            If Not _ChartData.Columns.Contains(_AxisYValueMember) Then
                Throw New Exception("Column " & _AxisYValueMember & " not found in datasource.")
            End If
            Chart.Series.Add(_SeriesName)
            Chart.Series(0).ChartType = strChartType
            Chart.Series(0).Color = System.Drawing.Color.FromArgb(CInt(CByte(180)), CInt(CByte(65)), CInt(CByte(140)), CInt(CByte(240)))
            Chart.Series(0).Font = New Font("arial", 1.0F, FontStyle.Regular)
            Chart.Legends(0).Enabled = False 'True
            Chart.Legends(0).BackColor = colorBackGround
            Chart.Legends(0).Font = New Font("arial", 1.0F, FontStyle.Regular)
            Chart.Series(0).MarkerSize = 0
            Chart.Series(0).MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.None
            Chart.Series(0).XValueMember = _AxisXValueMember
            Chart.Series(0).YValueMembers = _AxisYValueMember
            Chart.Series(0).Label = "#VALY{N0}"
            Chart.ChartAreas(0).ShadowColor = System.Drawing.Color.Transparent
            Chart.ChartAreas("Default").Area3DStyle.Enable3D = False
            Chart.DataSource = _ChartData
            Chart.DataBind()
            Dim strcount As Integer = Chart.Series(0).Points.Count
            For i = 0 To strcount - 1
                Select Case Chart.Series(0).Points.Item(i).AxisLabel
                    Case "JPM"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.JPM
                    Case "HSBC"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.HSBC
                    Case "CS"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.CS
                    Case "BNPP"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.BNPP
                    Case "UBS"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.UBS
                        'Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                    Case "DB"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.DBIB
		Case "OCBC"
                Chart.Series(0).Points(i).Color = Me.structChartColors.OCBC
		 Case "CITI"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.CITI
                    Case "LEONTEQ"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.LEONTEQ
                    Case "COMMERZ"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.COMMERZ
                    Case "BAML"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.BAML
                End Select
            Next
            Chart.Series(0)("PointWidth") = "0.3"
            If strcount = 1 Then
                Chart.ChartAreas("Default").InnerPlotPosition.Width = 20 ''55
            End If
            upnlChart.Update()
        Catch ex As Exception
            lblerror.Text = "DrawColumnChart:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "DrawColumnChart", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub DrawPieChart(ByVal _ChartData As DataTable, _
                         ByVal _AxisXValueMember As String, _
                         ByVal _AxisYValueMember As String, _
                         ByVal _SeriesName As String, _
                         ByVal _ShowValuesWithAmountMultiplier As Boolean, _
                         ByVal _ChartTitle As String, ByRef Chart As System.Web.UI.DataVisualization.Charting.Chart)
        Dim i As Integer = 0
        Try
            Dim colorBackGround As Color
            colorBackGround = Color.FromArgb(252, 252, 252)

            Chart.Series.Clear()

            ''Set Chart
            Chart.BackColor = colorBackGround

            Chart.BackGradientStyle = _ChartBackGradientStyle
            Chart.BorderlineDashStyle = _ChartBorderlineDashStyle
            Chart.BorderSkin.SkinStyle = _ChartBorderSkinStyle

            ''Set Chart Titles 
            ''   Chart.Titles("ChartTitle").Text = _ChartTitle
            ' Chart.Titles("ChartTitle").Font = New Font(ChartFont, 8.0F, FontStyle.Regular)
            ''  Chart.Titles("ChartTitle").Font = New Font("arial", CType(ddlFont.SelectedValue, System.Drawing.FontStyle), FontStyle.Regular)

            ''    Chart.Titles("ChartTitle").Alignment = ContentAlignment.MiddleCenter
            ' Chart.ChartAreas("Default").AxisX.TitleFont = New Font(ChartFont, 7.0F, FontStyle.Regular)
            ''  Chart.ChartAreas("Default").AxisX.TitleFont = New Font(ChartFont, CType(ddlFont.SelectedValue, System.Drawing.FontStyle), FontStyle.Regular)

            '' Chart.ChartAreas("Default").AxisY.TitleFont = New Font(ChartFont, 7.0F, FontStyle.Regular)
            ' Chart.ChartAreas("Default").AxisY.TitleFont = New Font(ChartFont, CType(ddlFont.SelectedValue, System.Drawing.FontStyle), FontStyle.Regular)

            Chart.ChartAreas("Default").BackColor = colorBackGround
            Chart.ChartAreas("Default").BackGradientStyle = GradientStyle.None

            'setInnerPlotPosition(_ChartPositionProperties)

            Chart.ChartAreas("Default").InnerPlotPosition.X = 30
            Chart.ChartAreas("Default").InnerPlotPosition.Y = 10

            ''---
            Chart.ChartAreas("Default").AxisX.TitleFont = New Font("arial", 7.0F, FontStyle.Regular)
            Chart.ChartAreas("Default").AxisY.TitleFont = New Font("arial", 7.0F, FontStyle.Regular)
            ''--

            Chart.ChartAreas("Default").InnerPlotPosition.Height = 80
            Chart.ChartAreas("Default").InnerPlotPosition.Width = 50 ''55

            Chart.ChartAreas("Default").InnerPlotPosition.Auto = False

            Chart.Legends("Default").Enabled = True
            Chart.Legends(0).Docking = _DockingLocation

            Dim dChartType As System.Web.UI.DataVisualization.Charting.SeriesChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut

            If _ChartData Is Nothing OrElse _ChartData.Rows.Count = 0 Then
                Throw New Exception("No data to display")
            End If

            ''Check if X value member present in datasource.
            If Not _ChartData.Columns.Contains(_AxisXValueMember) Then
                Throw New Exception("Column " & _AxisXValueMember & " not found in datasource.")
            End If

            ''Check if Y value member present in datasource.
            If Not _ChartData.Columns.Contains(_AxisYValueMember) Then
                Throw New Exception("Column " & _AxisYValueMember & " not found in datasource.")
            End If

            '3D setting

            Chart.Series.Add(_SeriesName)

            Chart.Series(0).ChartType = dChartType
                Chart.Series(0)("PieDrawingStyle") = "SoftEdge"
                Chart.Series(0)("PieLabelStyle") = "Outside"
                Chart.Series(0)("DoughnutRadius") = CStr(70)
                Chart.Series(0)("PieSize") = CStr(40)

            Chart.Series(0).Color = System.Drawing.Color.FromArgb(CInt(CByte(180)), CInt(CByte(65)), CInt(CByte(140)), CInt(CByte(240)))
            Chart.Series(0).Font = New System.Drawing.Font("arial", 7.0F, System.Drawing.FontStyle.Regular)

            Chart.Legends(0).Enabled = True
            Chart.Legends(0).BackColor = colorBackGround
            Chart.Legends(0).Font = New Font("arial", 7.0F, FontStyle.Regular)

            Chart.Series(0).MarkerStyle = System.Web.UI.DataVisualization.Charting.MarkerStyle.Circle
            Chart.Series(0).XValueMember = _AxisXValueMember
            Chart.Series(0).YValueMembers = _AxisYValueMember

            Chart.Series(0).Label = "#VALY{N0}"

            Chart.ChartAreas(0).Area3DStyle.IsClustered = True
            Chart.ChartAreas(0).Area3DStyle.IsRightAngleAxes = False
            Chart.ChartAreas(0).Area3DStyle.Perspective = 10
            Chart.ChartAreas(0).Area3DStyle.PointGapDepth = 0
            Chart.ChartAreas(0).Area3DStyle.Rotation = 0
            Chart.ChartAreas(0).Area3DStyle.WallWidth = 20
            Chart.ChartAreas(0).ShadowColor = System.Drawing.Color.Transparent

            Chart.Series(0).LegendText = "#VALX"

            Chart.ChartAreas("Default").Area3DStyle.Enable3D = True
            Chart.ChartAreas("Default").Area3DStyle.Rotation = 45
            Chart.ChartAreas("Default").Area3DStyle.Inclination = 45
            Chart.DataSource = _ChartData
            Chart.DataBind()
            Dim strcount As Integer = Chart.Series(0).Points.Count

            For i = 0 To strcount - 1
                Select Case Chart.Series(0).Points.Item(i).AxisLabel
                    Case "JPM"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.JPM
                    Case "HSBC"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.HSBC        ''System.Drawing.Color.FromArgb(214, 42, 33)
                    Case "CS"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.CS
                    Case "BNPP"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.BNPP      ''System.Drawing.Color.FromArgb(114, 148, 179)
                    Case "UBS"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.UBS
                        'Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                    Case "DB"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.DBIB
			 Case "OCBC"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.OCBC
			Case "CITI"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.CITI
                    Case "LEONTEQ"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.LEONTEQ
                    Case "COMMERZ"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.COMMERZ
                    Case "BAML"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.BAML
                End Select
            Next

            upnlChart.Update()
        Catch ex As Exception
            lblerror.Text = "DrawPieChart:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "DrawPieChart", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
#End Region
#Region "Exchange,Quanto,ValueDays,Strike,OrderQty,Upfront,Barrier,Solve For,Type,Share,LevegareRatio,Duration"
    Public Sub clearCheckBox()
        Try
            chkELNType.Checked = False
            ddlBarrier.Visible = False
            txtBarrier.Visible = False
            chkQuantoCcy.Checked = False
        Catch ex As Exception
            lblerror.Text = "clearCheckBox:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "clearCheckBox", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Sub clearFields()
        Try
            If tabContainer.ActiveTabIndex = 0 Then
                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                    txtELNPrice.Text = "0.00"
                    txtELNPrice.Enabled = False
                    txtStrike.Enabled = True
                Else
                    txtStrike.Text = "0.00"
                    txtStrike.Enabled = False
                    txtELNPrice.Enabled = True
                End If
                upnl1.Update()
            End If
        Catch ex As Exception
            lblerror.Text = "clearFields:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "clearFields", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Sub ddlExchange_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlExchange.SelectedIndexChanged
        Dim dtBaseCCY As DataTable
        Try

            lblerror.Text = ""
            RestoreSolveAll()
            RestoreAll()
            dtBaseCCY = New DataTable("Dummy")
            ''Fillddl_Share() ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value

            Fillddl_QuantoCcy()
            clearFields()
            With ddlShare
                .Items.Clear()
                .Text = ""
            End With
            ''ddlShare.Text = ddlShare.Text ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            If ddlShare.SelectedValue IsNot Nothing And ddlShare.SelectedItem IsNot Nothing Then
                getCurrency(ddlShare.SelectedValue.ToString)
                manageShareReportShowHide()
                GetDates()

                ddlShare.Text = ddlShare.SelectedItem.Text
            End If
            chkQuantoCcy.Checked = False
            ddlQuantoCcy.Visible = True
            ddlQuantoCcy.DataSource = Nothing
            ddlQuantoCcy.DataBind()
            ddlQuantoCcy.Items.Clear()
            ddlQuantoCcy.Items.Add(New DropDownListItem(lblELNBaseCcy.Text, lblELNBaseCcy.Text)) 'Mohit Lalwani on 8-Jul-2016
            ddlQuantoCcy.Enabled = False
            ddlQuantoCcy.BackColor = Color.FromArgb(242, 242, 243)
            ''GetCommentary()
            lblComentry.Text = ""
            pnlReprice.Update()
            upnlCommentry.Update()

            ResetAll()
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>

        Catch ex As Exception
            lblerror.Text = "ddlExchange_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlExchange_SelectedIndexChanged", ErrorLevel.High)
            Throw ex
        End Try

    End Sub
    Public Sub getCurrency(ByVal Share As String, Optional ByRef sCcy As String = "")
        Dim sShareCcy As String
        Try
            Dim dtBaseCCY As DataTable
            sShareCcy = ""
            If Share.Trim = "" Then
                sCcy = ""
            Else
                If (Not IsNothing(CType(Session("Share_Value"), DataTable)) AndAlso CType(Session("Share_Value"), DataTable).Rows.Count > 0) Then
                    sShareCcy = CType(Session("Share_Value"), DataTable).Select("Code = '" + Share.Trim + "'")(0)("Ccy").ToString
                    lblELNBaseCcy.Text = sShareCcy
                    lblQuantity.Text = "Notional (<font style=''>" & lblELNBaseCcy.Text & "</font>)"
                    sCcy = sShareCcy
                Else
                    dtBaseCCY = New DataTable("Dummy")
                    Select Case objELNRFQ.DB_GetBASECCY(Share, dtBaseCCY)
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                            lblELNBaseCcy.Text = dtBaseCCY.Rows(0)(0).ToString
                            lblQuantity.Text = "Notional (<font style=''>" & lblELNBaseCcy.Text & "</font>)"
                            sCcy = dtBaseCCY.Rows(0)(0).ToString
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                        Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    End Select
                End If
            End If


        Catch ex As Exception
            lblerror.Text = "getCurrency:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "getCurrency", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    ''<AshwiniP on 19-Sept-2016>
    Public Sub getPRR(ByVal Share As String, Optional ByRef sPRR As String = "")
        Dim sSharePRR As String
        Try
            Dim dt As DataTable
            sSharePRR = ""
            If Share.Trim = "" Then
                sPRR = ""
            Else
                If (Not IsNothing(CType(Session("Share_Value"), DataTable)) AndAlso CType(Session("Share_Value"), DataTable).Rows.Count > 0) Then
                    sSharePRR = CType(Session("Share_Value"), DataTable).Select("Code = '" + Share.Trim + "'")(0)("PRR").ToString
                    lblPRRVal.Text = sSharePRR
                    sPRR = sSharePRR
                Else
                    dt = New DataTable("Dummy")
                    Select Case objELNRFQ.DB_UnderlyingRiskRatingShare(Share, dt)
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                            lblPRRVal.Text = dt.Rows(0)(0).ToString
                            sPRR = dt.Rows(0)(0).ToString
                            ''If lblPRRVal.Text = "NA" Then
                            ''    lblPRRVal.ForeColor = Color.Red
                            ''Else
                            ''    lblPRRVal.ForeColor = Color.Green
                            ''End If
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                        Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    End Select

                End If
            End If
        Catch ex As Exception
            lblerror.Text = "getPRR:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "getPRR", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Sub ddlQuantoCcy_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlQuantoCcy.SelectedIndexChanged
        Try
            lblerror.Text = ""
            clearFields()
            GetDates()
            lblQuantity.Text = "Notional (<font style=''>" & ddlQuantoCcy.SelectedItem.Text & "</font>)"
            ResetAll()
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'Call getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            GetCommentary()

            '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
            Dim sEQC_DealerRedirection_OnPricer As String = objReadConfig.ReadConfig(dsConfig, "EQC_DealerRedirection_OnPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO")
            Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                Case "Y", "YES"
                    'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                    'Dim sLoginGrp As String
                    'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup

                    'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                    If UCase(Request.QueryString("Mode")) = "ALL" Then

                    Else
                        ''User is RM
                        setRMLimit()
                    End If
                Case "N", "NO"

            End Select

            '</Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
        Catch ex As Exception
            lblerror.Text = "ddlQuantoCcy_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlQuantoCcy_SelectedIndexChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub txtValueDays_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValueDays.TextChanged
        Try
            lblerror.Text = ""
            clearFields()
            lblerror.Text = ""
            If txtValueDays.Text = "" Then
                txtValueDays.Text = "0"
            End If
            If Val(txtValueDays.Text) < 7 Then
                lblerror.Text = "Settlement days should not be less than 7"
                Exit Sub
            Else
            End If
            Dim strSettlementDays As String = CStr(CDbl(txtValueDays.Text))
            GetDates()
            ResetAll()                ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()


        Catch ex As Exception
            lblerror.Text = "txtValueDays_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtValueDays_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub txtStrike_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStrike.TextChanged
        Try
            lblerror.Text = ""
            clearFields()
            txtStrike.Text = SetNumberFormat(txtStrike.Text, 2)
            ResetAll()                ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()

        Catch ex As Exception
            lblerror.Text = "txtStrike_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtStrike_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    Public Sub ddlSolveFor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSolveFor.SelectedIndexChanged
        Try
            lblerror.Text = ""
            clearFields()
            If ddlSolveFor.SelectedValue = "PricePercentage" Then
                txtELNPrice.Text = "0.00"
                txtELNPrice.BackColor = Color.FromArgb(242, 242, 243)
                txtELNPrice.Enabled = False
                txtStrike.Enabled = True
                txtStrike.BackColor = Color.White
            Else
                txtStrike.Text = "0.00"
              
                txtStrike.BackColor = Color.FromArgb(242, 242, 243)
                txtStrike.Enabled = False
                txtELNPrice.Enabled = True
                txtELNPrice.BackColor = Color.White
            End If
            lblSolveForType.Text = ddlSolveFor.SelectedItem.Text
            ResetAll()                  ''Sequence changed by AshwiniP on 04-Oct-2016
            GetCommentary()
            setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice()

        Catch ex As Exception
            lblerror.Text = "ddlSolveFor_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlSolveFor_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    Public Sub ddlBarrier_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlBarrier.SelectedIndexChanged
        Dim strBarrierType As String = String.Empty
        Dim strQuote_Id As String = String.Empty
        Dim PP_ID As String = String.Empty
        Try
            lblerror.Text = ""
            strQuote_Id = Convert.ToString(Session("Quote_ID"))
            strBarrierType = ddlBarrier.SelectedValue
            ResetAll()
            GetCommentary()             ''Added by AshwiniP on 05-Oct-2016
        Catch ex As Exception
            lblerror.Text = "ddlBarrier_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlBarrier_SelectedIndexChanged", ErrorLevel.High)
        End Try

    End Sub
    Private Sub txtQuantity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQuantity.TextChanged
        Dim strcount As Integer = 0
        Dim sQty As Integer = 0
        Try
            lblerror.Text = ""
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            If txtQuantity.Text = "" Then
                txtQuantity.Text = "0"
            End If
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            
            If Qty_Validate(txtQuantity.Text) = False Then
                Exit Sub
            End If
            Try
                txtQuantity.Text = FinIQApp_Number.ConvertFormattedAmountToNumber(txtQuantity.Text).ToString
                txtQuantity.Text = SetNumberFormat(txtQuantity.Text, 0)  '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
            Catch ex As Exception
                lblerror.Text = "Please enter valid Notional"
            End Try
            ResetAll()              ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()

        Catch ex As Exception
            lblerror.Text = "txtQuantity_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtQuantity_TextChanged", ErrorLevel.High)
            Throw ex
        End Try

    End Sub
    Protected Sub ddlSettlementDays_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSettlementDays.SelectedIndexChanged
        Try
            lblerror.Text = ""
            clearFields()
            If ddlSettlementDays.SelectedText = "1W" Then
                txtSettlementDate.Value = FinIQApp_Date.FinIQDate(DateAdd(DateInterval.Day, 7, CDate(txtTradeDate.Value)))
                txtValueDays.Text = "7"
            ElseIf ddlSettlementDays.SelectedText = "2W" Then
                txtSettlementDate.Value = FinIQApp_Date.FinIQDate(DateAdd(DateInterval.Day, 14, CDate(txtTradeDate.Value)))
                txtValueDays.Text = "14"
            End If
            GetDates()
            ResetAll()                ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()
            hdnSettDateManualChangeYN.Value = "N"
        Catch ex As Exception
            lblerror.Text = "ddlSettlementDays_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlSettlementDays_SelectedIndexChanged", ErrorLevel.High)

        End Try
    End Sub
    Protected Sub rbHistory_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbHistory.SelectedIndexChanged
        Try
            RestoreSolveAll()
            RestoreAll()
            If rbHistory.SelectedValue.Trim = "Quote History" Then
                ''<Nikhil M. on 20-Oct-2016: > ''<Nikhil M. on 20-Oct-2016: Added condition  for Hedge >
                If UCase(Request.QueryString("EXTLOD")) <> "HEDGE" Then
                    btnDetailsCancle_ServerClick(Nothing, Nothing)   'Added by Mohit Lalwani on 4-Feb-2016
                End If
                makeThisGridVisible(grdELNRFQ)
                fill_grid()
            ElseIf rbHistory.SelectedValue.Trim = "Order History" Then
                ''<Nikhil M. on 20-Oct-2016: > ''<Nikhil M. on 20-Oct-2016: Added condition  for Hedge >
                If UCase(Request.QueryString("EXTLOD")) <> "HEDGE" Then
                    btnDetailsCancle_ServerClick(Nothing, Nothing)   'Added by Mohit Lalwani on 4-Feb-2016
                End If

                makeThisGridVisible(grdOrder)
                fill_OrderGrid()
                ColumnVisibility()
            End If
            upnlGrid.Update()
            ''<Nikhil M. on 20-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "rbHistory_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "rbHistory_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub txttrade_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttrade.TextChanged
        Try
            clearFields()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "txttrade_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txttrade_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Function CheckMultiple(ByVal Number As Integer, ByVal Multiple As Integer) As Boolean
        Try
            If Number Mod Multiple = 0 Then
                CheckMultiple = True
            Else

                CheckMultiple = False
            End If
        Catch ex As Exception
            lblerror.Text = "CheckMultiple:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "CheckMultiple", ErrorLevel.High)
            Throw ex
        End Try
    End Function
    Protected Sub ddlShare_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShare.SelectedIndexChanged
        Try
	''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            'If ddlShare.SelectedItem Is Nothing Then
            '    clearShareData()
            '    ddlShare.Text = "Please select valid share."
            '    lblExchangeVal.Text = ""
            '    Exit Sub
            ''ElseIf ddlShare.SelectedItem.Value = "" Then
	    If ddlShare.SelectedValue = "" Then
                clearShareData()
                ddlShare.Text = "Please select valid share."
                lblExchangeVal.Text = ""
                Exit Sub
            Else
                lblExchangeVal.Text = setExchangeByShare(ddlShare)
                lblerror.Text = ""
                clearFields()
                getCurrency(ddlShare.SelectedValue.ToString)
                getPRR(ddlShare.SelectedValue.ToString)
                getFlag(ddlShare.SelectedValue.ToString)
                manageShareReportShowHide()
                chkQuantoCcy.Checked = False
                ddlQuantoCcy.Visible = True
                ddlQuantoCcy.DataSource = Nothing
                ddlQuantoCcy.DataBind()
                ddlQuantoCcy.Items.Clear()
                ddlQuantoCcy.Items.Add(New DropDownListItem(lblELNBaseCcy.Text, lblELNBaseCcy.Text)) 'Mohit Lalwani on 8-Jul-2016
                ddlQuantoCcy.Enabled = False
                ddlQuantoCcy.BackColor = Color.FromArgb(242, 242, 243)
                If ddlExchange.SelectedValue.ToString = "ALL" Then
                    GetDates()
                End If
                ResetAll()
                GetCommentary()

               
             
                '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
                Dim sEQC_DealerRedirection_OnPricer As String = objReadConfig.ReadConfig(dsConfig, "EQC_DealerRedirection_OnPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO")
                Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                    Case "Y", "YES"
                        'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                        'Dim sLoginGrp As String
                        'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup

                        'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                        If UCase(Request.QueryString("Mode")) = "ALL" Then
                            ''User is Dealer
                        Else
                            ''User is RM
                            setRMLimit()
                        End If
                    Case "N", "NO"

                End Select
                '</Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                'getRange()
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            End If
        Catch ex As Exception
            lblerror.Text = "ddlShare_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlShare_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub txtELNPrice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtELNPrice.TextChanged
        Try
            lblerror.Text = ""
            txtELNPrice.Text = SetNumberFormat(txtELNPrice.Text, 2)
            If txtELNPrice.Text = "" Or Val(txtELNPrice.Text) < 0 Then
                lblerror.Text = "Please enter valid IB price."
            Else
           
            End If
            ResetAll()                ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()

        Catch ex As Exception
            lblerror.Text = "txtELNPrice_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtELNPrice_TextChanged", ErrorLevel.High)
        End Try
    End Sub
   '<AvinashG. on 27-Jan-2016: Dead Function>
    'Public Sub stop_timer()
    '    Try
    '        lblMsgPriceProvider.Text = ""
    '        lblerror.Text = ""
    '        lblJPMPrice.Text = ""
    '        lblCSPrice.Text = ""
    '        lblHSBCPrice.Text = ""
    '        lblBNPPPrice.Text = ""
    '        lblUBSPrice.Text = ""
    '        lblBAMLPrice.Text = ""
    '        btnJPMprice.Text = "Price"
    '        btnJPMDeal.CssClass = "btnDisabled"
    '        btnCSPrice.Text = "Price"
    '        btnCSDeal.CssClass = "btnDisabled"
    '        btnHSBCPrice.Text = "Price"
    '        btnHSBCDeal.CssClass = "btnDisabled"
    '        btnBNPPPrice.Text = "Price"
    '        btnBNPPDeal.CssClass = "btnDisabled"
    '        btnUBSPrice.Text = "Price"
    '        btnUBSDeal.CssClass = "btnDisabled"
    '        btnBAMLPrice.Text = "Price"
    '        btnBAMLDeal.CssClass = "btnDisabled"
    '        Dim strJavaScriptStopTimer As New StringBuilder
    '        If Val(lblTimerHSBC.Text) > 0 Then
    '            strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
    '            btnHSBCPrice.Enabled = True
    '            btnHSBCPrice.CssClass = "btn"
    '        End If
    '        If Val(lblTimerCS.Text) > 0 Then
    '            strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
    '            btnCSPrice.Enabled = True
    '            btnCSPrice.CssClass = "btn"
    '        End If
    '        If Val(lblUBSTimer.Text) > 0 Then
    '            strJavaScriptStopTimer.AppendLine("StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
    '            btnUBSPrice.Enabled = True
    '            btnUBSPrice.CssClass = "btn"
    '        End If
    '        If Val(lblTimerBNPP.Text) > 0 Then
    '            strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
    '            btnBNPPPrice.Enabled = True
    '            btnBNPPPrice.CssClass = "btn"
    '        End If
    '        If Val(lblTimer.Text) > 0 Then
    '            strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
    '            btnJPMprice.Enabled = True
    '            btnJPMprice.CssClass = "btn"
    '        End If
    '        If Val(lblTimerBAML.Text) > 0 Then
    '            strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
    '            btnBAMLPrice.Enabled = True
    '            btnBAMLPrice.CssClass = "btn"
    '        End If

    '        If strJavaScriptStopTimer.Length > 0 Then
    '            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptStopTimer", strJavaScriptStopTimer.ToString, True)
    '        End If

    '        ResetAll()
    '    Catch ex As Exception
    '        lblerror.Text = "Stop_timer:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "Stop_timer", ErrorLevel.High)
    '        Throw ex
    '    End Try
    'End Sub
    '</AvinashG. on 27-Jan-2016: Dead Function>
    Private Sub Stop_timer_Only()
        Try
            Dim strJavaScriptStopTimerOnly As New StringBuilder
            If Val(lblTimerHSBC.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
                btnHSBCPrice.Enabled = True
                btnHSBCPrice.CssClass = "btn"
            End If

            If Val(lblTimerCS.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
                btnCSPrice.Enabled = True
                btnCSPrice.CssClass = "btn"
            End If

            If Val(lblUBSTimer.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
                btnUBSPrice.Enabled = True
                btnUBSPrice.CssClass = "btn"
            End If

            If Val(lblTimerBNPP.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
                btnBNPPPrice.Enabled = True
                btnBNPPPrice.CssClass = "btn"
            End If

            If Val(lblTimer.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
                btnJPMprice.Enabled = True
                btnJPMprice.CssClass = "btn"
            End If

            If Val(lblTimerBAML.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
                btnBAMLPrice.Enabled = True
                btnBAMLPrice.CssClass = "btn"
            End If
	    If Val(lblTimerOCBC.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
                btnOCBCPrice.Enabled = True
                btnOCBCPrice.CssClass = "btn"
            End If
	     If Val(lblTimerDBIB.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
                btnDBIBPrice.Enabled = True
                btnDBIBPrice.CssClass = "btn"
            End If
	     If Val(lblTimerCITI.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
                btnCITIPrice.Enabled = True
                btnCITIPrice.CssClass = "btn"
            End If
            If Val(lblTimerLEONTEQ.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
                btnLEONTEQprice.Enabled = True
                btnLEONTEQprice.CssClass = "btn"
            End If
            If Val(lblTimerCOMMERZ.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
                btnCOMMERZprice.Enabled = True
                btnCOMMERZprice.CssClass = "btn"
            End If
            If strJavaScriptStopTimerOnly.Length > 0 Then
                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptStopTimerOnly", strJavaScriptStopTimerOnly.ToString, True)
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Stop_timer_Only", ErrorLevel.High)
        End Try
    End Sub
    Public Sub Enable_Disable_Deal_Buttons()
        Try
            btnJPMprice.Text = "Price"
            btnHSBCPrice.Text = "Price"
            btnCSPrice.Text = "Price"
            btnUBSPrice.Text = "Price"
            btnBNPPPrice.Text = "Price"
            btnBAMLPrice.Text = "Price"
	    btnOCBCPrice.Text = "Price"
            btnCITIprice.Text = "Price"
            btnLEONTEQprice.Text = "Price"
            btnCOMMERZPrice.Text = "Price"
	    btnDBIBPrice.Text = "Price"
            btnJPMprice.Enabled = True
            btnHSBCPrice.Enabled = True
            btnCSPrice.Enabled = True
            btnUBSPrice.Enabled = True
            btnBNPPPrice.Enabled = True
            btnBAMLPrice.Enabled = True
	     btnOCBCPrice.Enabled = True
            btnCITIprice.Enabled = True
            btnLEONTEQprice.Enabled = True
            btnCOMMERZPrice.Enabled = True
	    btnDBIBPrice.Enabled = True
            btnJPMprice.CssClass = "btn"
            btnHSBCPrice.CssClass = "btn"
            btnCSPrice.CssClass = "btn"
            btnUBSPrice.CssClass = "btn"
            btnBNPPPrice.CssClass = "btn"
            btnBAMLPrice.CssClass = "btn"
	    btnOCBCPrice.CssClass = "btn"
            btnCITIprice.CssClass = "btn"
            btnLEONTEQprice.CssClass = "btn"
            btnCOMMERZPrice.CssClass = "btn"
	    btnDBIBPrice.CssClass = "btn"
            btnJPMDeal.CssClass = "btnDisabled"
            btnHSBCDeal.CssClass = "btnDisabled"
            btnCSDeal.CssClass = "btnDisabled"
            btnUBSDeal.CssClass = "btnDisabled"
            btnBNPPDeal.CssClass = "btnDisabled"
            btnBAMLDeal.CssClass = "btnDisabled"
	    btnOCBCDeal.CssClass = "btnDisabled"
            btnCITIDeal.CssClass = "btnDisabled"
            btnLEONTEQDeal.CssClass = "btnDisabled"
            btnCOMMERZDeal.CssClass = "btnDisabled"
	    btnDBIBDeal.CssClass = "btnDisabled"
            btnBNPPDeal.Visible = False
            btnBAMLDeal.Visible = False
            btnCSDeal.Visible = False
            btnJPMDeal.Visible = False
            btnHSBCDeal.Visible = False
            btnUBSDeal.Visible = False
	    btnOCBCDeal.Visible = False
            btnCITIDeal.Visible = False
            btnLEONTEQDeal.Visible = False
            btnCOMMERZDeal.Visible = False
            btnDBIBDeal.Visible = False

            btnBAMLDoc.Visible = False
            btnBNPPDoc.Visible = False
            btnCITIDoc.Visible = False
            btnCOMMERZDoc.Visible = False
            btnCSDoc.Visible = False
            btnDBIBDoc.Visible = False
            btnHSBCDoc.Visible = False
            btnJPMDoc.Visible = False
            btnLEONTEQDoc.Visible = False
            btnOCBCDoc.Visible = False
            btnUBSDoc.Visible = False


            txtQuantity.Enabled = True
            DealConfirmPopup.Visible = False
            UPanle11111.Update()
        Catch ex As Exception
            lblerror.Text = "Enable_Disable_Deal_Buttons:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Enable_Disable_Deal_Buttons", ErrorLevel.High)
            Throw ex

        End Try
    End Sub
    Public Sub chkELNType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkELNType.CheckedChanged
        Dim strELNType As String = ""
        Try
            lblerror.Text = ""
            If chkELNType.Checked = False Then
                txtBarrier.Text = "0"
                txtBarrier.Enabled = False
                ddlBarrier.Enabled = False
                txtBarrier.Visible = False
                ddlBarrier.Visible = False
                '<AvinashG. on 30-Aug-2016: SCB specific changes, as per call discussions>
                lblKOWatchDate.Visible = False
                dtpKOWatch.Visible = False
                '</AvinashG. on 30-Aug-2016: SCB specific changes, as per call discussions>
            Else
                txtBarrier.Enabled = True
                'txtBarrier.Text = "110"
                txtBarrier.Text = getControlPersonalSetting("KO", "110.00")
                ddlBarrier.Enabled = True
                txtBarrier.Visible = True
                ddlBarrier.Visible = True
                '<AvinashG. on 30-Aug-2016: SCB specific changes, as per call discussions>
                lblKOWatchDate.Visible = True
                dtpKOWatch.Visible = True
                dtpKOWatch.Value = Convert.ToDateTime(Date.Now()).ToString("dd-MMM-yyyy")
                '</AvinashG. on 30-Aug-2016: SCB specific changes, as per call discussions>
                txtBarrier_TextChanged(sender, e)
            End If
            ResetAll()
            GetCommentary()         ''Added by AshwiniP on 04-oct-2016
        Catch ex As Exception
            lblerror.Text = "chkELNType_CheckedChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chkELNType_CheckedChanged", ErrorLevel.High)

        End Try
    End Sub
    Private Sub txtBarrier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBarrier.TextChanged
        Try
            lblerror.Text = ""
            txtBarrier.Text = SetNumberFormat(txtBarrier.Text, 2)
            ResetAll()                     ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()
        Catch ex As Exception
            lblerror.Text = "txtBarrier_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtBarrier_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub chkQuantoCcy_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkQuantoCcy.CheckedChanged
        Try
            lblerror.Text = ""
            If chkQuantoCcy.Checked = True Then
                lblQuantity.Text = "Notional (<font style=''>" & ddlQuantoCcy.SelectedItem.Text & "</font>)"
                Call Fillddl_QuantoCcy()
                ddlQuantoCcy.Enabled = True
                ddlQuantoCcy.BackColor = Color.White
                ddlQuantoCcy_SelectedIndexChanged(Nothing, Nothing)
            Else
                ddlQuantoCcy.Visible = True
                lblQuantity.Text = "Notional (<font style=''>" & lblELNBaseCcy.Text & "</font>)"
                ddlQuantoCcy.DataSource = Nothing
                ddlQuantoCcy.DataBind()
                ddlQuantoCcy.Items.Clear()
                ddlQuantoCcy.Items.Add(New DropDownListItem(lblELNBaseCcy.Text, lblELNBaseCcy.Text)) 'Mohit Lalwani on 8-Jul-2016
                ddlQuantoCcy.Enabled = False
                ddlQuantoCcy.BackColor = Color.FromArgb(242, 242, 243)
            End If
            GetDates()
            ResetAll()                    ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()

            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>

            '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
            Dim sEQC_DealerRedirection_OnPricer As String = objReadConfig.ReadConfig(dsConfig, "EQC_DealerRedirection_OnPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO")
            Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                Case "Y", "YES"
                    'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                    'Dim sLoginGrp As String
                    'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup

                    'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                    If UCase(Request.QueryString("Mode")) = "ALL" Then
                        ''User is Dealer
                    Else
                        ''User is RM
                        setRMLimit()
                    End If
                Case "N", "NO"

            End Select
            '</Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
        Catch ex As Exception
            lblerror.Text = "chkQuantoCcy_CheckedChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chkQuantoCcy_CheckedChanged", ErrorLevel.High)
        End Try
    End Sub
#End Region
#Region "Populate XML"


    'Public Sub Get_RFQData_TOXML(ByVal PP_ID As String)
    '    Dim dtQuoteCode As DataTable
    '    Dim strQuoteId As String = String.Empty
    '    Dim strYN As String = String.Empty
    '    Try
    '        dtQuoteCode = New DataTable("Quote")
    '        udtStructured_Product_Tranche = New Structured_Product_Tranche_ELN
    '        strEntityName = LoginInfoGV.Login_Info.EntityName
    '        strEntityId = LoginInfoGV.Login_Info.EntityID.ToString
    '        If ddlSolveFor.SelectedValue = "PricePercentage" Then
    '            strYN = "Y"
    '        Else
    '            strYN = "N"
    '        End If


    '        Select Case objELNRFQ.DB_Get_Quote_ID(dtQuoteCode)
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
    '                ER_QuoteRequestId = dtQuoteCode.Rows(0)(0).ToString
    '                Session.Add("Quote_ID", ER_QuoteRequestId)
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
    '                lblerror.Text = "No record(s) for price provider is found."
    '        End Select
    '        Call setUDTValuesFromForm(udtStructured_Product_Tranche)
    '        If Write_RFQData_TOXML(strYN, PP_ID, ER_QuoteRequestId, LoginInfoGV.Login_Info.LoginId, strEntityId, ddlSolveFor.SelectedValue, udtStructured_Product_Tranche, StrnoteRFQXML) = True Then
    '            Select Case objELNRFQ.DB_Insert_IntoELN_RFQ(CStr(StrnoteRFQXML))
    '                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful

    '                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful

    '            End Select
    '        End If
    '    Catch ex As Exception
    '        lblerror.Text = "Get_RFQData_TOXML:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "Get_RFQData_TOXML", ErrorLevel.High)
    '        Throw ex
    '    End Try
    'End Sub

    'Public Function Write_RFQData_TOXML(ByVal strYN As String, ByVal PP_ID As String, _
    '                                    ByVal strQuoteId As String, _
    '                                    ByVal strEntityName As String, _
    '                                    ByVal strEntityId As String, _
    '                                    ByVal strType As String, _
    '                                    ByRef udtStructured_Product_Tranche As Structured_Product_Tranche_ELN, _
    '                                    ByRef o_strXMLNote_RFQ As String) As Boolean

    '    Dim strXMLRFQ As StringBuilder
    '    Dim dtQuote As New DataTable
    '    Try
    '        strXMLRFQ = New StringBuilder
    '        With udtStructured_Product_Tranche

    '            Dim sProductDefinition As String = "<ELN xmlns=""http://www.abcdz.com/StructuredProducts/1.0""><Type>" & udtStructured_Product_Tranche.strELNType & "</Type><SettlementDate>" & udtStructured_Product_Tranche.strValueDate & "</SettlementDate><ExpiryDate>" & udtStructured_Product_Tranche.strFxingdate & "</ExpiryDate><MaturityDate>" & udtStructured_Product_Tranche.strMaturityDate & " </MaturityDate><StrikePercentage>" & udtStructured_Product_Tranche.dblStrike1 & "</StrikePercentage><InterBankPrice Solve=""true"">0.0</InterBankPrice><Underlyings><Underlying><UnderlyingCode Type=""RIC"">" & udtStructured_Product_Tranche.strAsset & "</UnderlyingCode></Underlying></Underlyings></ELN>"

    '            strXMLRFQ.Append("<tradeDetails>")
    '            strXMLRFQ.Append("<quoteDetails>")
    '            strXMLRFQ.Append("<ER_PP_ID>" & PP_ID & "</ER_PP_ID>")
    '            strXMLRFQ.Append("<ER_Type>" & udtStructured_Product_Tranche.strELNType & "</ER_Type>")   'Simple/Barrier
    '            strXMLRFQ.Append("<ER_SettlmentDate>" & udtStructured_Product_Tranche.strValueDate & "</ER_SettlmentDate>")
    '            strXMLRFQ.Append("<ER_ExpiryDate>" & udtStructured_Product_Tranche.strFxingdate & "</ER_ExpiryDate>")
    '            strXMLRFQ.Append("<ER_MaturityDate>" & udtStructured_Product_Tranche.strMaturityDate & "</ER_MaturityDate>")
    '            strXMLRFQ.Append("<ER_StrikePercentage>" & udtStructured_Product_Tranche.dblStrike1 & "</ER_StrikePercentage> ")
    '            strXMLRFQ.Append("<ER_BarrierPercentage>" & udtStructured_Product_Tranche.dblBarrier & "</ER_BarrierPercentage>")
    '            strXMLRFQ.Append("<ER_BarrierMonitoringMode>" & udtStructured_Product_Tranche.strBranchName & "</ER_BarrierMonitoringMode>")
    '            strXMLRFQ.Append("<ER_InterBankPrice>" & udtStructured_Product_Tranche.strPrice & "</ER_InterBankPrice>")
    '            strXMLRFQ.Append("<ER_InterBankPriceSolve_YN>" & strYN & "</ER_InterBankPriceSolve_YN>")
    '            strXMLRFQ.Append("<ER_UnderlyingCode_Type>" & udtStructured_Product_Tranche.strAssetclass & "</ER_UnderlyingCode_Type>")
    '            strXMLRFQ.Append("<ER_UnderlyingCode>" & udtStructured_Product_Tranche.strAsset & " </ER_UnderlyingCode>")
    '            strXMLRFQ.Append("<ER_TenorType>" & udtStructured_Product_Tranche.strTenorType & "</ER_TenorType>")
    '            strXMLRFQ.Append("<ER_Tenor>" & udtStructured_Product_Tranche.inttenor & "</ER_Tenor>")
    '            strXMLRFQ.Append("<ER_TradeDate>" & udtStructured_Product_Tranche.strTradeDate & "</ER_TradeDate>")
    '            strXMLRFQ.Append("<ER_Price>" & udtStructured_Product_Tranche.strPrice & "</ER_Price>")
    '            strXMLRFQ.Append("<ER_QuoteRequestId>" & strQuoteId & "</ER_QuoteRequestId>")
    '            strXMLRFQ.Append("<ER_SecurityDescription>ELN</ER_SecurityDescription>")
    '            strXMLRFQ.Append("<ER_QuoteType>0</ER_QuoteType>")
    '            strXMLRFQ.Append("<ER_BuySell>Buy</ER_BuySell>")
    '            strXMLRFQ.Append("<ER_CashOrderQuantity>" & udtStructured_Product_Tranche.strOrderQty & "</ER_CashOrderQuantity>")
    '            strXMLRFQ.Append("<ER_CashCurrency>" & udtStructured_Product_Tranche.strCurrency & "</ER_CashCurrency>")
    '            strXMLRFQ.Append("<ER_TransactionTime>" & DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff tt") & "</ER_TransactionTime>")
    '            strXMLRFQ.Append("<ER_BidPrice></ER_BidPrice>")
    '            strXMLRFQ.Append("<ER_OfferPrice></ER_OfferPrice>")
    '            strXMLRFQ.Append("<ER_ProductDefinition>" & sProductDefinition & "</ER_ProductDefinition>")
    '            strXMLRFQ.Append("<ER_Text>2012_QR12</ER_Text>")
    '            strXMLRFQ.Append("<ER_Symbol></ER_Symbol>")
    '            strXMLRFQ.Append("<ER_AveragePrice></ER_AveragePrice>")
    '            strXMLRFQ.Append("<ER_Created_By>" & strEntityName & "</ER_Created_By>")
    '            strXMLRFQ.Append("<ER_Remark1></ER_Remark1>")
    '            strXMLRFQ.Append("<ER_Remark2></ER_Remark2>")
    '            strXMLRFQ.Append("<ER_Active_YN>Y</ER_Active_YN>")
    '            strXMLRFQ.Append("<ER_SubScheme>" & udtStructured_Product_Tranche.strTemplateName & "</ER_SubScheme>")
    '            strXMLRFQ.Append("<ER_Exchange>" & udtStructured_Product_Tranche.strExchange & "</ER_Exchange>")
    '            strXMLRFQ.Append("<ER_Quote_Request_YN>Y</ER_Quote_Request_YN>")
    '            strXMLRFQ.Append("<ER_Entity_ID>" & strEntityId & "</ER_Entity_ID>")
    '            strXMLRFQ.Append("<ER_Issuer_Date_Offset>" & udtStructured_Product_Tranche.strIssuer_Date_Offset & "</ER_Issuer_Date_Offset>")
    '            strXMLRFQ.Append("<ER_Template_ID>" & udtStructured_Product_Tranche.lngTemplateId & "</ER_Template_ID>")
    '            strXMLRFQ.Append("<ER_Frequency></ER_Frequency>")
    '            strXMLRFQ.Append("<ER_SolveFor>" & udtStructured_Product_Tranche.strSolveFor & "</ER_SolveFor>")
    '            strXMLRFQ.AppendLine("<ER_UseSoftTenor_YN>N</ER_UseSoftTenor_YN>")
    '            strXMLRFQ.Append("<ER_Nominal_Amount>" & udtStructured_Product_Tranche.dblNominalAmount & "</ER_Nominal_Amount>")
    '            If strType = "PricePercentage" Then
    '                strXMLRFQ.Append("<EP_StrikePercentage>" & udtStructured_Product_Tranche.dblStrike2 & "</EP_StrikePercentage>")
    '            Else
    '                strXMLRFQ.Append("<EP_OfferPrice>" & udtStructured_Product_Tranche.strPrice1 & "</EP_OfferPrice>")
    '            End If
    '            strXMLRFQ.Append("<EP_Remark2>" & udtStructured_Product_Tranche.strRemark & "</EP_Remark2>")
    '            strXMLRFQ.Append("<ER_EntityName>" & udtStructured_Product_Tranche.strEntityName & "</ER_EntityName>")  ''Entity
    '            strXMLRFQ.Append("<ER_RFQ_RMName>" & udtStructured_Product_Tranche.strRFQRMName & "</ER_RFQ_RMName>")  ''RM   
    '            strXMLRFQ.Append("<ER_EmailId>" & udtStructured_Product_Tranche.strEmailId & "</ER_EmailId>")  ''Emailid   
    '            strXMLRFQ.Append("<ER_Branch>" & udtStructured_Product_Tranche.strBranch & "</ER_Branch>")  ''Branch 
    '            strXMLRFQ.Append("<EP_RM_Margin>" & udtStructured_Product_Tranche.dblRMMargin & "</EP_RM_Margin>")  ''Branch 
    '            strXMLRFQ.Append("<ER_Upfront>" & udtStructured_Product_Tranche.strUpfront.ToString & "</ER_Upfront>") '<AvinashG. on 23-Apr-2015: Save upfront at RFQ level as pool creation needs yield and upfront>
    '            strXMLRFQ.Append("<ER_RFQ_Source>MONOTAB_PRICER</ER_RFQ_Source>") '<AvinashG. on 28-Dec-2015: EQBOSDEV-195 - Add RFQ Source to web application>
    '            strXMLRFQ.Append("</quoteDetails>")
    '            strXMLRFQ.Append("</tradeDetails>")
    '        End With

    '        o_strXMLNote_RFQ = strXMLRFQ.ToString
    '        Write_RFQData_TOXML = True

    '    Catch ex As Exception
    '        lblerror.Text = "Write_RFQData_TOXML:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "Write_RFQData_TOXML", ErrorLevel.High)

    '        Throw ex
    '    End Try
    'End Function



    'Private Sub setUDTValuesFromForm(ByRef udtStructured_Product_Tranche As Structured_Product_Tranche_ELN)
    '    Dim strFixingDate As String = String.Empty
    '    Try
    '        udtStructured_Product_Tranche.dblStrike1 = Val(txtStrike.Text)
    '        udtStructured_Product_Tranche.dblBarrier = txtBarrier.Text
    '        udtStructured_Product_Tranche.inttenor = CInt(txtTenor.Text)
    '        udtStructured_Product_Tranche.dblRMMargin = CDbl(txtUpfrontELN.Text)
    '        If chkQuantoCcy.Checked = True Then
    '            udtStructured_Product_Tranche.strCurrency = ddlQuantoCcy.SelectedValue
    '        Else
    '            udtStructured_Product_Tranche.strCurrency = lblELNBaseCcy.Text
    '        End If

    '        udtStructured_Product_Tranche.strAsset = ddlShare.SelectedValue.ToString
    '        udtStructured_Product_Tranche.lngTemplateId = Convert.ToString(Session("Template_Code"))
    '        udtStructured_Product_Tranche.strIssuer_Date_Offset = txtValueDays.Text
    '        If ddlExchange.SelectedValue.ToUpper = "ALL" Then
    '            Dim sTemp As String
    '            udtStructured_Product_Tranche.strExchange = objELNRFQ.GetShareExchange(ddlShare.SelectedValue.ToString, sTemp)
    '        Else
    '            udtStructured_Product_Tranche.strExchange = ddlExchange.SelectedValue
    '        End If
    '        udtStructured_Product_Tranche.strAssetclass = "RIC"
    '        udtStructured_Product_Tranche.strOrderQty = Replace(txtQuantity.Text, ",", "") ''
    '        udtStructured_Product_Tranche.strValueDate = Convert.ToString(Session("Settlementdate"))
    '        udtStructured_Product_Tranche.strFxingdate = Convert.ToString(Session("expiryDAte"))
    '        udtStructured_Product_Tranche.strMaturityDate = Convert.ToString(Session("MaturityDAte"))
    '        udtStructured_Product_Tranche.strTradeDate = Convert.ToString(Session("TradeDAte"))
    '        udtStructured_Product_Tranche.strPrice = Val(txtELNPrice.Text)
    '        udtStructured_Product_Tranche.strTenorType = ddlTenorTypeELN.SelectedValue
    '        udtStructured_Product_Tranche.strSecurityDesc = Convert.ToString(Session("Scheme"))
    '        udtStructured_Product_Tranche.strSolveFor = ddlSolveFor.SelectedValue
    '        If flag = "I" Then
    '            udtStructured_Product_Tranche.strRemark = Convert.ToString(Session("Quote_ID"))
    '        End If
    '        If chkELNType.Checked = True Then
    '            udtStructured_Product_Tranche.strBranchName = ddlBarrier.SelectedValue
    '            udtStructured_Product_Tranche.strELNType = "Barrier"
    '        Else
    '            udtStructured_Product_Tranche.strELNType = "Simple"
    '        End If

    '        If ddlSolveFor.SelectedValue = "PricePercentage" Then
    '            udtStructured_Product_Tranche.dblStrike2 = Val(txtStrike.Text)
    '        Else
    '            udtStructured_Product_Tranche.strPrice1 = Val(txtELNPrice.Text)
    '        End If
    '        udtStructured_Product_Tranche.strUpfront = Val(txtUpfrontELN.Text) * 100

    '        udtStructured_Product_Tranche.strEntityName = ddlentity.SelectedItem.Text
    '        udtStructured_Product_Tranche.strRFQRMName = ddlRFQRM.SelectedItem.Value
    '        Dim strLoginUserEmailID As String
    '        strLoginUserEmailID = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)
    '        udtStructured_Product_Tranche.strEmailId = strLoginUserEmailID
    '        udtStructured_Product_Tranche.strBranch = lblbranch.Text




    '    Catch ex As Exception
    '        lblerror.Text = "setUDTValuesFromForm:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "setUDTValuesFromForm", ErrorLevel.High)
    '        Throw ex
    '    End Try
    'End Sub

#End Region
#Region "Check For Price,Strike,Coupon,Upfront"

    Public Function set_ELN_ClientYield_Price(ByVal dblIssuerPrice As Double, ByVal issuer As String) As Boolean
        Try
            Dim dblClientPrice As Double = 0
            dblClientPrice = dblIssuerPrice + CDbl(txtUpfrontELN.Text)

            Select Case UCase(issuer)
                Case "JPM"
                    lblJPMClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblJPMClientYield.Text = get_ELN_ClientYield(dblClientPrice)
                Case "CS"
                    lblCSClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblCSClientYield.Text = get_ELN_ClientYield(dblClientPrice)
                Case "UBS"
                    lblUBSClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblUBSClientYield.Text = get_ELN_ClientYield(dblClientPrice)
                Case "HSBC"
                    lblHSBCClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblHSBCClientYield.Text = get_ELN_ClientYield(dblClientPrice)
                Case "BAML"
                    lblBAMLClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblBAMLClientYield.Text = get_ELN_ClientYield(dblClientPrice)
                Case "BNPP"
                    lblBNPPClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblBNPPClientYield.Text = get_ELN_ClientYield(dblClientPrice)
                    ''Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                Case "DB"
                    lblDBIBClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblDBIBClientYield.Text = get_ELN_ClientYield(dblClientPrice)
		Case "OCBC"
                    lblOCBCClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblOCBCClientYield.Text = get_ELN_ClientYield(dblClientPrice)
		Case "CITI"
                    lblCITIClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblCITIClientYield.Text = get_ELN_ClientYield(dblClientPrice)
                Case "LEONTEQ"
                    lblLEONTEQClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblLEONTEQClientYield.Text = get_ELN_ClientYield(dblClientPrice)
                Case "COMMERZ"
                    lblCOMMERZClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
                    lblCOMMERZClientYield.Text = get_ELN_ClientYield(dblClientPrice)
            End Select

            Return True
        Catch ex As Exception
            lblerror.Text = "set_ELN_ClientYield_Price:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "set_ELN_ClientYield_Price", ErrorLevel.High)
            Throw ex
        End Try
    End Function
    Public Function set_ELN_AllIssuerPrice_ClientYield_ClientPrice(ByVal dblIssuerPrice As Double) As Boolean

        Try
            Dim dblClientPrice As Double = 0
            dblClientPrice = dblIssuerPrice + CDbl(txtUpfrontELN.Text)

            Dim strClientYield As String = ""
            strClientYield = get_ELN_ClientYield(dblClientPrice)
            lblJPMClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblJPMClientYield.Text = strClientYield
            lblCSClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblCSClientYield.Text = strClientYield
            lblUBSClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblUBSClientYield.Text = strClientYield
            lblHSBCClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblHSBCClientYield.Text = strClientYield
            lblBAMLClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblBAMLClientYield.Text = strClientYield
            lblBNPPClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblBNPPClientYield.Text = strClientYield
            lblDBIBClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblDBIBClientYield.Text = strClientYield
	    lblOCBCClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblOCBCClientYield.Text = strClientYield
	    lblCITIClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblCITIClientYield.Text = strClientYield
            lblLEONTEQClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblLEONTEQClientYield.Text = strClientYield
            lblCOMMERZClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblCOMMERZClientYield.Text = strClientYield
            pnlReprice.Update()
            Return True
        Catch ex As Exception
            lblerror.Text = "set_ELN_AllIssuerPrice_ClientYield_ClientPrice:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "set_ELN_AllIssuerPrice_ClientYield_ClientPrice", ErrorLevel.High)
            Throw ex
        End Try
    End Function


    Public Function setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice(Optional ByVal activeTabIndex As Integer = 0) As Boolean
        Try
            lblJPMPrice.Text = "" ''"0.0"
            lblJPMClientPrice.Text = "" ''"0.0"
            lblJPMClientYield.Text = "" ''"0.0"

            lblCSPrice.Text = "" ''"0.0"
            lblCSClientPrice.Text = "" ''"0.0"
            lblCSClientYield.Text = "" ''"0.0"

            lblUBSPrice.Text = "" ''"0.0"
            lblUBSClientPrice.Text = "" ''"0.0"
            lblUBSClientYield.Text = "" ''"0.0"

            lblHSBCPrice.Text = "" ''"0.0"
            lblHSBCClientPrice.Text = "" ''"0.0"
            lblHSBCClientYield.Text = "" ''"0.0"

            lblBAMLPrice.Text = "" ''"0.0"
            lblBAMLClientPrice.Text = "" ''"0.0"
            lblBAMLClientYield.Text = "" ''"0.0"

            lblBNPPPrice.Text = "" ''"" ''"0.0"
            lblBNPPClientPrice.Text = "" ''"" ''"0.0"
            lblBNPPClientYield.Text = "" ''"" ''"0.0"
            lblDBIBPrice.Text = "" ''"" ''"0.0"
            lblDBIBClientPrice.Text = "" ''"" ''"0.0"
            lblDBIBClientYield.Text = "" ''"" ''"0.0"

            lblOCBCPrice.Text = "" ''"0.0"
            lblOCBCClientPrice.Text = "" ''"0.0"
            lblOCBCClientYield.Text = "" ''"0.0"
	    
	    lblCITIPrice.Text = "" ''"0.0"
            lblCITIClientPrice.Text = "" ''"0.0"
            lblCITIClientYield.Text = "" ''"0.0"

            lblLEONTEQPrice.Text = "" ''"0.0"
            lblLEONTEQClientPrice.Text = "" ''"0.0"
            lblLEONTEQClientYield.Text = "" ''"0.0"

            lblCOMMERZPrice.Text = "" ''"0.0"
            lblCOMMERZClientPrice.Text = "" ''"0.0"
            lblCOMMERZClientYield.Text = "" ''"0.0"
            pnlReprice.Update()

            Return True
        Catch ex As Exception
            lblerror.Text = "setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice", ErrorLevel.High)
            Throw ex
        End Try
    End Function

    Public Function get_ELN_ClientYield(ByVal dblClientPrice As Double) As String
        Dim dblYearBasisValue As Double
        Dim strTypeAsset As String = String.Empty
        Dim strCurrency As String = String.Empty
        Try
            strTypeAsset = "FX"
            If chkQuantoCcy.Checked = True Then
                strCurrency = ddlQuantoCcy.SelectedValue
            Else
                strCurrency = lblELNBaseCcy.Text
            End If
            objELNRFQ.web_GetYearBasisShortTermValues(strTypeAsset, strTypeAsset, strCurrency, dblYearBasisValue)
            Dim lngDayDiff As Long = DateDiff(DateInterval.Day, CDate(txtSettlementDate.Value), CDate(txtMaturityDate.Value))
            If dblClientPrice > 0 Then
                ''Return SetNumberFormat(Math.Round((((100 - dblClientPrice) / dblClientPrice) * 100 * (dblYearBasisValue / lngDayDiff)), 2), 2).ToString'''DK
                Return SetNumberFormat(Math.Round((((100 - dblClientPrice) / dblClientPrice) * 100 * (dblYearBasisValue / lngDayDiff)), 4), 4).ToString
            Else
                Return "0.00"
            End If

        Catch ex As Exception
            lblerror.Text = "get_ELN_ClientYield: Error occurred in processing. "
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Get_ELN_Client_Yield", ErrorLevel.High)

            Return "0.00"
        End Try

    End Function
    '<RiddhiS. 25-Jul-2016 for calculating upfront on yield user input>
    Public Function get_ELN_Upfront(ByVal dblYield As Double, ByVal dblIssuerPrice As Double) As String
        Dim dblYearBasisValue As Double
        Dim strTypeAsset As String = String.Empty
        Dim strCurrency As String = String.Empty
        Try
            strTypeAsset = "FX"
            If chkQuantoCcy.Checked = True Then
                strCurrency = ddlQuantoCcy.SelectedValue
            Else
                strCurrency = lblELNBaseCcy.Text
            End If
            objELNRFQ.web_GetYearBasisShortTermValues(strTypeAsset, strTypeAsset, strCurrency, dblYearBasisValue)
            Dim lngDayDiff As Long
            lngDayDiff = DateDiff(DateInterval.Day, CDate(txtSettlementDate.Value), CDate(txtMaturityDate.Value))

            '''Return SetNumberFormat((((100 * 100 * dblYearBasisValue) / ((100 * dblYearBasisValue) + (lngDayDiff * dblYield))) - dblIssuerPrice), 2).ToString
            Return SetNumberFormat((((100 * 100 * dblYearBasisValue) / ((100 * dblYearBasisValue) + (lngDayDiff * dblYield))) - dblIssuerPrice), 4).ToString '''DK
        Catch ex As Exception
            lblerror.Text = "get_ELN_Upfront: Error occurred in processing. "
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "get_ELN_Upfront", ErrorLevel.High)

            Return "0.00"
        End Try

    End Function

#End Region
#Region "Deal"

    Public Function chk_DealValidations() As Boolean
        Dim dtRangeLimit As DataTable
        Dim dt As DataTable
        Dim sPPCode As String
        Dim sPrd As String
        Dim sRangeCcy As String
        Dim sEQC_DealerRedirection_OnPricer As String
        Dim dblUserLimit As Double
        Dim notBand As String
        Try


            ''<Start | Nikhil M. on 04-Oct-2016: For Hide/Show>
            If IsNothing(Request.QueryString("PoolID")) Then
                'chk_DealValidations = True
            Else
                If Request.QueryString("PoolID").ToString = "" Then
                    'chk_DealValidations = True
                Else
                    chk_DealValidations = True
                    Exit Function
                End If
            End If
            ''<End | Nikhil M. on 04-Oct-2016: For Hide/Show>

            dtRangeLimit = New DataTable("LimitData")
            sEQC_DealerRedirection_OnPricer = objReadConfig.ReadConfig(dsConfig, "EQC_DealerRedirection_OnPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO")
            If ddlRM.Items.Count = 0 OrElse ddlRM.SelectedItem.Text = "" Then
                lblerrorPopUp.Text = "Cannot proceed with order. Please select an RM."
                chk_DealValidations = False
                Exit Function
            Else
                chk_DealValidations = True
            End If

            ''<AshwiniP on 19-Sept-16 START>
            dt = New DataTable("PRRating")
            Select Case objELNRFQ.DB_UnderlyingRiskRatingShare(ddlShare.SelectedValue.ToString, dt)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dt.Rows(0).Item(0).ToString = "NA" Then
                        lblerrorPopUp.ForeColor = Color.Red
                        lblerrorPopUp.Text = "Order cannot be placed as PRR is not available."
                        chk_DealValidations = False
                        Exit Function
                    Else
                        chk_DealValidations = True
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            End Select
            ''<END>

            sPPCode = lblIssuerPopUpValue.Text
            sPrd = "ELN"
            If chkQuantoCcy.Checked Then
                sRangeCcy = ddlQuantoCcy.SelectedValue
            Else
                sRangeCcy = lblELNBaseCcy.Text
            End If
            If ddlOrderTypePopUpValue.SelectedValue.Trim.ToUpper = "LIMIT" Then
                If txtLimitPricePopUpValue.Text = "" OrElse Val(txtLimitPricePopUpValue.Text) = 0 Then
                    lblerrorPopUp.Text = "Please enter limit price."
                    chk_DealValidations = False
                    Exit Function
                Else
                    chk_DealValidations = True
                End If

                If (txtLimitPricePopUpValue.Text.Length - (txtLimitPricePopUpValue.Text.LastIndexOf(".") + 1)) > 4 And CDbl(txtLimitPricePopUpValue.Text) <> Math.Floor(CDbl(txtLimitPricePopUpValue.Text)) Then
                    lblerrorPopUp.Text = "Precision of " + lblLimitPricePopUpCaption.Text + " cannot exceed four digits after decimal point."
                    chk_DealValidations = False
                    Exit Function
                Else
                    chk_DealValidations = True
                End If
            Else
                chk_DealValidations = True
            End If
            If ddlSolveFor.SelectedItem.Value.ToUpper = "STRIKEPERCENTAGE" Then
                Dim dblMaxStrike As Double
                Dim dblELNStrike As Double
                dblMaxStrike = CDbl(objReadConfig.ReadConfig(dsConfig, "EQC_ELN_Allowed_Max_Strike", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "100").Trim.Replace(",", "")) '<AvinashG. on 15-Sep-2015: Change of default 10 100>
                Select Case lblIssuerPopUpValue.Text.Trim.ToUpper
                    Case "JPM"
                        dblELNStrike = CDbl(lblJPMPrice.Text.Replace(",", ""))
                    Case "HSBC"
                        dblELNStrike = CDbl(lblHSBCPrice.Text.Replace(",", ""))
                    Case "UBS"
                        dblELNStrike = CDbl(lblUBSPrice.Text.Replace(",", ""))
                    Case "CS"
                        dblELNStrike = CDbl(lblCSPrice.Text.Replace(",", ""))
                    Case "BAML"
                        dblELNStrike = CDbl(lblBAMLPrice.Text.Replace(",", ""))
                    Case "BNPP"
                        dblELNStrike = CDbl(lblBNPPPrice.Text.Replace(",", ""))
                        ''Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                    Case "DB"
                        dblELNStrike = CDbl(lblDBIBPrice.Text.Replace(",", ""))
                    Case "OCBC"
                        dblELNStrike = CDbl(lblOCBCPrice.Text.Replace(",", ""))
                    Case "CITI"
                        dblELNStrike = CDbl(lblCITIPrice.Text.Replace(",", ""))
                    Case "LEONTEQ"
                        dblELNStrike = CDbl(lblLEONTEQPrice.Text.Replace(",", ""))
                    Case "COMMERZ"
                        dblELNStrike = CDbl(lblCOMMERZPrice.Text.Replace(",", ""))
                End Select
                If dblELNStrike > dblMaxStrike Then
                    chk_DealValidations = False
                    lblerrorPopUp.Text = "Cannot place order. Strike greater than allowed maximum of " + FormatNumber(dblMaxStrike, 2) + "."
                    Exit Function
                Else
                    chk_DealValidations = True

                End If

            End If
            ''<Dilkhush 15Jan2016 : EQBOSDEv:-220>
            '''DK
            If txtClientYieldPopUpValue.Text.Trim = "" Then
                lblerrorPopUp.Text = "Client yield cannot be blank. Please enter correct value."
                chk_DealValidations = False
                Exit Function
            ElseIf Val(txtClientYieldPopUpValue.Text) < 0 Then
                lblerrorPopUp.Text = "Client yield cannot be zero or less than zero. Please enter correct value."
                chk_DealValidations = False
                Exit Function
            ElseIf Val(txtClientYieldPopUpValue.Text) = 0 Then
                lblerrorPopUp.Text = "Client yield cannot be zero or less than zero. Please enter correct value."
                chk_DealValidations = False
                Exit Function
            Else
                chk_DealValidations = True
            End If

            ''<AshwiniP on 22Sept2016>
            Dim TotalSum As Double
            TotalSum = 0
            If grdRMData.Rows.Count > 0 Then
                For Each row As GridViewRow In grdRMData.Rows
                    Dim isChecked As Boolean = row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked

                    ''<Start | Nikhil M. on 15-Oct-2016: Added for Checking RM Name Validation>
                    If isChecked AndAlso (row.Cells(1).Controls.OfType(Of RadDropDownList)().FirstOrDefault().SelectedValue).Trim = "" Or (row.Cells(1).Controls.OfType(Of RadDropDownList)().FirstOrDefault().SelectedValue).Trim = "&nbsp;" Then
                        chkUpfrontOverride.Visible = False
                        lblerrorPopUp.Text = "Please select RM Name."
                        Return False
                    End If
                    ''<End | Nikhil M. on 15-Oct-2016: Added for Checking RM Name Validation>

                    If isChecked AndAlso (row.Cells(2).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault().HiddenCustomerId).Trim = "" Or (row.Cells(2).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault().HiddenCustomerId).Trim = "&nbsp;" Then
                        chkUpfrontOverride.Visible = False
                        lblerrorPopUp.Text = "Please select valid Customer."

                        '<RiddhiS. on 10-Nov-2016: To clear incorrect customer code>
                        Dim FindCustomer As FinIQ_Fast_Find_Customer
                        FindCustomer = CType(row.FindControl("FindCustomer"), FinIQ_Fast_Find_Customer)
                        FindCustomer.setCustName = ""
                        FindCustomer.HiddenCustomerName = ""
                        FindCustomer.HiddenCustomerId = ""
                        FindCustomer.HiddenDocId = ""
                        '</RiddhiS.>


                        Return False
                    End If
                    If isChecked AndAlso Qty_Validate(row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim) = False Then
                        chkUpfrontOverride.Visible = False
                        lblerrorPopUp.Text = "Please enter valid Notional."
                        Return False
                    ElseIf isChecked AndAlso (row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim) = "0" Then ''<Nikhil M. on 17-Oct-2016: Added to check the no zero notional RM  >
                        chkUpfrontOverride.Visible = False
                        lblerrorPopUp.Text = "Notional can not be 0."
                        Return False
                    End If
                    If isChecked AndAlso row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim <> "" Then
                        ''TotalSum = TotalSum + CDbl(row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                        '<Added by AshwiniP on 05-Oct-2016 : For notional validation while order confirm>
                        If Qty_Validate(CStr(TotalSum)) = False Then
                            Exit Function
                        End If
                        Try
                            TotalSum = CDbl(FinIQApp_Number.ConvertFormattedAmountToNumber(CStr(TotalSum)).ToString)
                            TotalSum = CDbl(SetNumberFormat(TotalSum, 0))
                            TotalSum = TotalSum + CDbl(row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                        Catch ex As Exception
                            lblerrorPopUp.Text = "Please enter valid Notional"
                            chk_DealValidations = False
                            Return False
                        End Try
                        '</Added by AshwiniP on 05-Oct-2016>
                    End If
                    If isChecked AndAlso row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim = "" Then
                        chkUpfrontOverride.Visible = False
                        lblerrorPopUp.Text = "Please enter Notional."
                        Return False
                    End If
                Next

                If TotalSum <> CDbl(lblNotionalPopUpValue.Text) Then
                    chkUpfrontOverride.Visible = False
                    lblerrorPopUp.Text = "Sum of Notional does not equal Order Quantity."
                    Return False
                End If
            Else
                chkUpfrontOverride.Visible = False
                lblerrorPopUp.Text = "Please add Allocation."
            End If
            ''</Changed by ashwiniP on 22Sept2016
	    ''<Uncommented by Rushikesh As told by Sanchita on 5Nov16>
            If chkDuplicateRecords() Then    ''Removed by AshwiniP on 01-Oct-2016
            Else
                Return False
            End If
	    ''/<Uncommented by Rushikesh As told by Sanchita on 5Nov16>
            ''</ashwiniP on 22Sept2016
            '''''''Dilkhush Avinash upfront check 
            'EQBOSDEV-435 - Set Min/Max upfront for HK and non HK underlyings for notes and options

            Dim objdeal As WEB_FINIQ_MarketData.QECapture
            objdeal = New WEB_FINIQ_MarketData.QECapture
            objdeal.ProductCode = "ELN"
            objdeal.sStock = lblUnderlyingPopUpValue.Text
            objdeal.sStockCcy = lblELNBaseCcy.Text
            objdeal.sQuantoCcy = lblCurrencyPopUpValue.Text
            If ddlExchange.SelectedValue.ToUpper = "ALL" Then
                Dim sTemp As String
                objdeal.ExchangeName = objELNRFQ.GetShareExchange(lblUnderlyingPopUpValue.Text, sTemp)
            Else
                objdeal.ExchangeName = ddlExchange.SelectedValue
            End If

            objdeal.Notional = CDbl(lblNotionalPopUpValue.Text)
            If lblTenorTypePopUpValue.Text.ToUpper = "MONTH" Then
                objdeal.TenorInMonths = CInt(lblTenorPopUpValue.Text)
            Else
                objdeal.TenorInMonths = CInt(lblTenorPopUpValue.Text) * 12
            End If
            objdeal.DealerCode = LoginInfoGV.Login_Info.LoginId
            objdeal.UserGroup = LoginInfoGV.Login_Info.LoginGroup

            Dim SalesSpread As Double
            Dim MinSpreadPct As Double = 0
            Dim MaxSpreadPct As Double = 0
            Dim MaxDealNotional As Double = 0
            Dim MinDealNotional As Double = 0
            Dim MinSpreadAmt As Double = 0
            Dim MaxSpreadAmt As Double = 0
            Dim sExceptionType As String = ""
            Dim sRemarks As String = ""

            '<AvinashG. on 13-Jul-2016: EQBOSDEV-442 - No message if no setup is found in SpecialSpread>
            Select Case oWEBMarketData.GetSpecialSpreadAndExceptionType(CStr(LoginInfoGV.Login_Info.EntityID), (CDbl(Val(txtUpfrontPopUpValue.Text.Replace(",", ""))) * 100), objdeal, SalesSpread, MinSpreadPct, _
                                                            MaxSpreadPct, MaxDealNotional, MinDealNotional, MinSpreadAmt, MaxSpreadAmt, _
                                                            sExceptionType, sRemarks)
                Case WEB_FINIQ_MarketData.Database_Transaction_Response.Db_Successful

                    'If CDbl(Val(txtUpfrontPopUpValue.Text.Replace(",", ""))) < (MinSpreadPct / 100) Or CDbl(Val(txtUpfrontPopUpValue.Text.Replace(",", ""))) > (MaxSpreadPct / 100) Then
                    Select Case sExceptionType.Trim.ToUpper
                        Case "NORMAL"
                            chk_DealValidations = True
                        Case "WARNING"
                            chkUpfrontOverride.Visible = True
                            lblerrorPopUp.Text = sRemarks + "(" + FormatNumber((MinSpreadPct / 100).ToString, 2) + "% and " + FormatNumber((MaxSpreadPct / 100).ToString, 2) + "%). Please select to continue."

                            If chkUpfrontOverride.Checked Then
                                chk_DealValidations = True
                            Else
                                chk_DealValidations = False
                                Exit Function
                            End If
                        Case "ERROR"
                            lblerrorPopUp.Text = sRemarks
                            chk_DealValidations = False
                            chkUpfrontOverride.Visible = False ''<Nikhil M. on 09-Nov-2016: EQSCB-170 | Hide Checkbox on hard block>
                            Exit Function
                        Case Else ''Considering all other cases as error
                            lblerrorPopUp.Text = "Cannot proceed. Setup issue in upfront validation. "
                            chk_DealValidations = False
                            chkUpfrontOverride.Visible = False ''<Nikhil M. on 09-Nov-2016: EQSCB-170 | Hide Checkbox on hard block>
                            Exit Function
                    End Select

                    'Else
                    'chk_DealValidations = True
                    'End If
                Case Else
                    lblerrorPopUp.Text = "Cannot proceed. Setup issue in upfront validation. "
                    chk_DealValidations = False
                    chkUpfrontOverride.Visible = False ''<Nikhil M. on 09-Nov-2016: EQSCB-170 | Hide Checkbox on hard block>
                    Exit Function
            End Select

            ''<Rushikesh D. on 23-May-2016 JIRAID:EQBOSDEV-371>
            'Dim strMaxUpfront As String
            'strMaxUpfront = objReadConfig.ReadConfig(dsConfig, "EQC_MaxUpfront", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "0").Trim.ToUpper
            'If Val(strMaxUpfront) > 0 Then
            '    If Val(txtUpfrontPopUpValue.Text.Replace(",", "")) > Val(strMaxUpfront) Then
            '        lblerrorPopUp.Text = "Upfront should not be more than " + strMaxUpfront + "%."
            '        chk_DealValidations = False
            '        Exit Function
            '    Else
            '        chk_DealValidations = True
            '    End If
            'End If
            ''</Rushikesh D. on 23-May-2016 JIRAID:EQBOSDEV-371>



            Dim Notional As Double = Convert.ToDouble(txtQuantity.Text)
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>


            ' Dim lmtRow As DataRow
            ' lmtRow = result(0)
            ' Dim dblMin, dblMax As Double
            ' dblMin = CDbl(If(IsDBNull(lmtRow("EQCPPL_Minm")), 0, CDbl(lmtRow("EQCPPL_Minm"))))
            ' dblMax = CDbl(If(IsDBNull(lmtRow("EQCPPL_Maxm")), 0, CDbl(lmtRow("EQCPPL_Maxm"))))
            '<AvinashG. on 16‐Apr‐2015: If redirection is enabled then get user/user group limit>
            '<Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
            Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                Case "Y", "YES"
                    'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                    'Dim sLoginGrp As String
                    'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
                    'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                    If UCase(Request.QueryString("Mode")) = "ALL" Then
                        ''User is Dealer

                        '<AvinashG. on 18‐Sep‐2014: FA‐585 Change of error message for range limit >
                        '<Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
                        'If (Notional < dblMin) Then
                        ' lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
                        ' chk_DealValidations = False
                        'ElseIf (Notional > dblMax) Then
                        ' lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
                        ' chk_DealValidations = False
                        'Else
                        ' chk_DealValidations = True
                        'End If
                        '<\Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
                        'If (Notional >= dblMin) AndAlso (Notional <= dblMax) Then
                        ' chk_DealValidations = True
                        'Else
                        ' lblerrorPopUp.Text = "Notional value out of range."
                        ' chk_DealValidations = False
                        ' Exit Function
                        'End If
                        '<AvinashG. on 18‐Sep‐2014: FA‐585 Change of error message for range limit >
                    Else
                        '<Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
                        'If (Notional < dblMin) Then
                        ' lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
                        ' chk_DealValidations = False
                        ' Exit Function
                        'ElseIf (Notional > dblMax) Then
                        ' lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
                        ' chk_DealValidations = False
                        ' Exit Function
                        'Else
                        '<Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
                        Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, sRangeCcy, sPrd, dblUserLimit)
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                If dblUserLimit > 0 Then
                                    If Notional > dblUserLimit Then ''Condition for notional greater than issuer limit will get checked in previous if
                                        ''Dilkhush/AVinash 03Feb2016 Added limit in message
                                        lblerrorPopUp.Text = "Notional Size is larger than your permitted limit (" + FormatNumber(dblUserLimit.ToString, 0) + " " + sRangeCcy + "). Do you want to redirect this order to dealer?"
                                        btnRedirect.Visible = True
                                        btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                        chk_DealValidations = False
                                    Else
                                        chk_DealValidations = True
                                    End If
                                    '<Added by Mohit Lalwani on 2‐Nov‐2015 for ELN FA‐1169 >
                                Else
                                    ''In‐aligment with the JIRA description allowing redirection if user limit is zero
                                    If dblUserLimit = 0 Then
                                        lblerrorPopUp.Text = "Limit not found or Zero limit found. Do you want to redirect this order to dealer?"
                                        btnRedirect.Visible = True
                                        btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                        chk_DealValidations = False
                                        LogException(LoginInfoGV.Login_Info.LoginId, "User/User Group Limit found 0(Zero) for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                    Else
                                        LogException(LoginInfoGV.Login_Info.LoginId, "Invalid value(" + dblUserLimit.ToString + ") found for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                        chk_DealValidations = False
                                    End If
                                    '</Added by Mohit Lalwani on 2‐Nov‐2015 for ELN FA‐1169 >
                                End If
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
                                chk_DealValidations = False
                        End Select
                        'End If
                        '<Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
                    End If
                Case "N", "NO"
                    '<AvinashG. on 18‐Sep‐2014: FA‐585 Change of error message for range limit >
                    '<Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
                    'If (Notional < dblMin) Then
                    ' lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
                    ' chk_DealValidations = False
                    'ElseIf (Notional > dblMax) Then
                    ' lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
                    ' chk_DealValidations = False
                    'Else
                    ' chk_DealValidations = True
                    'End If
                    '<\Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
                    'If (Notional >= dblMin) AndAlso (Notional <= dblMax) Then
                    ' chk_DealValidations = True
                    'Else
                    ' lblerrorPopUp.Text = "Notional value out of range."
                    ' chk_DealValidations = False
                    ' Exit Function
                    'End If
                    '<AvinashG. on 18‐Sep‐2014: FA‐585 Change of error message for range limit >
            End Select
            '</AvinashG. on 16‐Apr‐2015: If redirection is enabled then get user/user group limit>
            '<Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
            'Else
            ''<AvinashG. on 18‐Sep‐2014: FA‐591 Message if limit not found for a currency >
            'lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            ''lblerrorPopUp.Text = "Range not set for " + sPPCode + "."
            ''</AvinashG. on 18‐Sep‐2014: FA‐591 Message if limit not found for a currency >
            'chk_DealValidations = False
            'Exit Function
            'End If
            'Else
            ''<AvinashG. on 18‐Sep‐2014: FA‐591 Message if limit not found for a currency >
            'lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            ''lblerrorPopUp.Text = "Range not set for " + sPPCode + "."
            ''</AvinashG. on 18‐Sep‐2014: FA‐591 Message if limit not found for a currency >
            'chk_DealValidations = False
            'Exit Function
            ' End If
            ' Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            ' lblerrorPopUp.Text = "Failed to retrieve range limit"
            ' chk_DealValidations = False
            ' Exit Function
            ' Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
            ' '<AvinashG. on 18‐Sep‐2014: FA‐591 Message if limit not found for a currency >
            ' lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            ' 'lblerrorPopUp.Text = "Range not set for " + sPPCode + "."
            ' '</AvinashG. on 18‐Sep‐2014: FA‐591 Message if limit not found for a currency >
            ' chk_DealValidations = False
            ' Exit Function
            ' Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            ' lblerrorPopUp.Text = "Failed to retrieve range limit"
            ' chk_DealValidations = False
            ' Exit Function
            'End Select
            'If (Notional > Convert.ToDouble(Session.Item("MaxLimit"))) Or (Notional < Convert.ToDouble(Session.Item("MinLimit"))) Then
            ' lblerrorPopUp.Text = "Notional value out of range."
            ' chk_DealValidations = False
            ' Exit Function
            'Else
            ' chk_DealValidations = True
            'End If

            'Select Case objELNRFQ.Get_EQCPRD_Limit(sPrd, sRangeCcy, dtRangeLimit)
            '    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            '        If Not (dtRangeLimit Is Nothing) Or dtRangeLimit.Rows.Count < 1 Then
            '            Dim result() As DataRow = dtRangeLimit.Select("EQCPPL_PPCode = '" + sPPCode + "'")
            '            If result.Length > 0 Then
            '                Dim lmtRow As DataRow
            '                lmtRow = result(0)
            '                Dim dblMin, dblMax As Double
            '                dblMin = CDbl(If(IsDBNull(lmtRow("EQCPPL_Minm")), 0, CDbl(lmtRow("EQCPPL_Minm"))))
            '                dblMax = CDbl(If(IsDBNull(lmtRow("EQCPPL_Maxm")), 0, CDbl(lmtRow("EQCPPL_Maxm"))))
            '                Select Case sEQC_DealerRedirection_OnPricer.ToUpper
            '                    Case "Y", "YES"
            '                        sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
            '                        Dim sLoginGrp As String
            '                        sLoginGrp = LoginInfoGV.Login_Info.LoginGroup

            '                        If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
            '                            If (Notional < dblMin) Then
            '                                lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            '                                chk_DealValidations = False
            '                            ElseIf (Notional > dblMax) Then
            '                                lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
            '                                chk_DealValidations = False
            '                            Else
            '                                chk_DealValidations = True
            '                            End If

            '                        Else
            '                            If (Notional < dblMin) Then
            '                                lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            '                                chk_DealValidations = False
            '                                Exit Function
            '                            ElseIf (Notional > dblMax) Then
            '                                lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
            '                                chk_DealValidations = False
            '                                Exit Function
            '                            Else
            '                                Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, sRangeCcy, sPrd, dblUserLimit)
            '                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            '                                        If dblUserLimit > 0 Then
            '                                            If Notional > dblUserLimit Then
            '                                                lblerrorPopUp.Text = "Notional Size is larger than your permitted limit. Do you want to redirect this order to dealer?"
            '                                                btnRedirect.Visible = True
            '                                                chk_DealValidations = False
            '                                            Else
            '                                                chk_DealValidations = True
            '                                            End If
            '                                        Else
            '                                            ''In-aligment with the JIRA description allowing redirection if user limit is zero
            '                                            If dblUserLimit = 0 Then
            '                                                lblerrorPopUp.Text = "Limit not found or Zero limit found. Do you want to redirect this order to dealer?"
            '                                                btnRedirect.Visible = True
            '                                                chk_DealValidations = False
            '                                                LogException(LoginInfoGV.Login_Info.LoginId, "User/User Group Limit found 0(Zero) for  " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
            '                                                                                        sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
            '                                            Else
            '                                                LogException(LoginInfoGV.Login_Info.LoginId, "Invalid value(" + dblUserLimit.ToString + ") found for  " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
            '                                                                                        sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
            '                                                chk_DealValidations = False
            '                                            End If

            '                                        End If
            '                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            '                                        lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
            '                                        chk_DealValidations = False
            '                                End Select
            '                            End If
            '                        End If
            '                    Case "N", "NO"
            '                        If (Notional < dblMin) Then
            '                            lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            '                            chk_DealValidations = False
            '                        ElseIf (Notional > dblMax) Then
            '                            lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
            '                            chk_DealValidations = False
            '                        Else
            '                            chk_DealValidations = True
            '                        End If
            '                End Select
            '            Else
            '                lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            '                chk_DealValidations = False
            '                Exit Function
            '            End If
            '        Else
            '            lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            '            chk_DealValidations = False
            '            Exit Function
            '        End If
            '    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            '        lblerrorPopUp.Text = "Failed to retrieve range limit"
            '        chk_DealValidations = False
            '        Exit Function
            '    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
            '        lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            '        chk_DealValidations = False
            '        Exit Function
            '    Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            '        lblerrorPopUp.Text = "Failed to retrieve range limit"
            '        chk_DealValidations = False
            '        Exit Function
            'End Select

            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            chk_DealValidations = True          ''AshwiniP on 05-oct-2016

         
        Catch ex As Exception
            lblerror.Text = "chk_DealValidations:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chk_DealValidations", ErrorLevel.High)

            Throw ex
        End Try
    End Function

    Public Sub btnJPMDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJPMDeal.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblMsgPriceProvider.Text = ""
            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("JPMQuote"))
            Session.Remove("JPMQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strJPMID As String
                strJPMID = CStr(hashRFQID(hashPPId("JPM")))
                Session.Remove("flag")
                Session("flag") = ""
                DealClicked(strJPMID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerJPMDeal", "StopPPTimerValue('" + btnJPMDeal.ClientID + "');", True)
            btnJPMprice.Text = "Price"
            JpmHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnJPMDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnJPMDeal_Click", ErrorLevel.High)
        End Try
    End Sub

    Public Sub btnCSDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCSDeal.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblMsgPriceProvider.Text = ""
            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("CSQuote"))
            Session.Remove("CSQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Session.Remove("flag")
                Session("flag") = ""
                Dim strCSID As String
                strCSID = CStr(hashRFQID(hashPPId("CS")))
                DealClicked(strCSID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerCSDeal", "StopPPTimerValue('" + btnCSDeal.ClientID + "');", True)
            btnCSPrice.Text = "Price"
            CsHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnCSDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCSDeal_Click", ErrorLevel.High)
        End Try
    End Sub

    Public Sub btnBAMLDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBAMLDeal.Click

        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            RestoreSolveAll()
            RestoreAll()
            hashPPId = New Hashtable
            hashRFQID = New Hashtable

            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""

            lblMsgPriceProvider.Text = ""

            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("BAMLQuote"))
            Session.Remove("BAMLQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Session.Remove("flag")
                Session("flag") = ""
                Dim strBAMLID As String
                strBAMLID = CStr(hashRFQID(hashPPId("BAML")))
                DealClicked(strBAMLID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerBAMLDeal", "StopPPTimerValue('" + btnBAMLDeal.ClientID + "');", True)
            btnBAMLPrice.Text = "Price"
            BAMLHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnBAMLDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnBAMLDeal_Click", ErrorLevel.High)

        End Try
    End Sub

    Public Sub btnBNPPDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBNPPDeal.Click
        Dim getBNPP As String = ""
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            RestoreSolveAll()
            RestoreAll()
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblMsgPriceProvider.Text = ""
            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("BNPPQuote"))
            Session.Remove("BNPPQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strBNPPID As String
                strBNPPID = CStr(hashRFQID(hashPPId("BNPP")))
                Session.Remove("flag")
                Session("flag") = ""
                DealClicked(strBNPPID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerBNPPDeal", "StopPPTimerValue('" + btnBNPPDeal.ClientID + "');", True)
            btnBNPPPrice.Text = "Price"
            BNPPHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnBNPPDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnBNPPDeal_Click", ErrorLevel.High)
        End Try
    End Sub

    Public Sub btnHSBCDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHSBCDeal.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try


            RestoreSolveAll()
            RestoreAll()
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblMsgPriceProvider.Text = ""
            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("HSBCQuote"))
            Session.Remove("HSBCQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strHSBCID As String
                strHSBCID = CStr(hashRFQID(hashPPId("HSBC")))
                Session.Remove("flag")
                Session("flag") = ""
                DealClicked(strHSBCID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerHSBCDeal", "StopPPTimerValue('" + btnHSBCDeal.ClientID + "');", True)
            btnHSBCPrice.Text = "Price"
            HsbcHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnHSBCDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnHSBCDeal_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnCITIDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCITIDeal.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            RestoreSolveAll()
            RestoreAll()
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblMsgPriceProvider.Text = ""
            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("CITIQuote"))
            Session.Remove("CITIQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strCITIID As String
                strCITIID = CStr(hashRFQID(hashPPId("CITI")))
                Session.Remove("flag")
                Session("flag") = ""
                DealClicked(strCITIID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerCITIDeal", "StopPPTimerValue('" + btnCITIDeal.ClientID + "');", True)
            btnCITIprice.Text = "Price"
            CITIHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnCITIDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCITIDeal_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnLEONTEQDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLEONTEQDeal.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            RestoreSolveAll()
            RestoreAll()
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblmsgPriceProvider.Text = ""
            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("LEONTEQQuote"))
            Session.Remove("LEONTEQQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strLEONTEQID As String
                strLEONTEQID = CStr(hashRFQID(hashPPId("LEONTEQ")))
                Session.Remove("flag")
                Session("flag") = ""
                DealClicked(strLEONTEQID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerLEONTEQDeal", "StopPPTimerValue('" + btnLEONTEQDeal.ClientID + "');", True)
            btnLEONTEQPrice.Text = "Price"
            LEONTEQHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnLEONTEQDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnLEONTEQDeal_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnCOMMERZDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCOMMERZDeal.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            RestoreSolveAll()
            RestoreAll()
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblmsgPriceProvider.Text = ""
            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("COMMERZQuote"))
            Session.Remove("COMMERZQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strCOMMERZID As String
                strCOMMERZID = CStr(hashRFQID(hashPPId("COMMERZ")))
                Session.Remove("flag")
                Session("flag") = ""
                DealClicked(strCOMMERZID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerCOMMERZDeal", "StopPPTimerValue('" + btnCOMMERZDeal.ClientID + "');", True)
            btnCOMMERZPrice.Text = "Price"
            COMMERZHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnCOMMERZDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCOMMERZDeal_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnOCBCDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOCBCDeal.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            RestoreSolveAll()
            RestoreAll()
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblMsgPriceProvider.Text = ""
            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("OCBCQuote"))
            Session.Remove("OCBCQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strOCBCID As String
                strOCBCID = CStr(hashRFQID(hashPPId("OCBC")))
                Session.Remove("flag")
                Session("flag") = ""
                DealClicked(strOCBCID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerOCBCDeal", "StopPPTimerValue('" + btnOCBCDeal.ClientID + "');", True)
            btnOCBCPrice.Text = "Price"
            OCBCHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnOCBCDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnOCBCDeal_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnUBSDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUBSDeal.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            RestoreSolveAll()
            RestoreAll()
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblmsgPriceProvider.Text = ""
            If chk_DealValidations() = False Then
                Exit Sub
            End If
            Quote_ID = Convert.ToString(Session("UBSQuote"))
            Session.Remove("UBSQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strUBSID As String
                strUBSID = CStr(hashRFQID(hashPPId("UBS")))
                Session.Remove("flag")
                Session("flag") = ""
                DealClicked(strUBSID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
                ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        btnLoad_Click(sender, e)
                    Case "N", "NO"
                        ''Do Nothing
                End Select
                ''btnLoad_Click(sender, e)
                ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            End If
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerUBSDeal", "StopPPTimerValue('" + btnUBSDeal.ClientID + "');", True)
            btnUBSPrice.Text = "Price"
            UbsHiddenPrice.Value = ";Enable;Disable;Disable;Price"
            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnUBSDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnUBSDeal_Click", ErrorLevel.High)
        End Try
    End Sub

    Public Sub btnDBIBDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDBIBDeal.Click
        Dim getDBIB As String = ""
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Try
            RestoreSolveAll()
            RestoreAll()
            hashPPId = New Hashtable
            hashRFQID = New Hashtable

            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""

            lblMsgPriceProvider.Text = ""

            If chk_DealValidations() = False Then
                Exit Sub
            End If

            ''<Imran 7April:Added for getting pp's latest generated Id>
            Quote_ID = Convert.ToString(Session("DBIBQuote"))
            Session.Remove("DBIBQuote")
            ''</Imran 7April:Added for getting pp's latest generated Id>
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strDBIBID As String
                'strDBIBID = CStr(hashRFQID(hashPPId("DBIB"))) ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                strDBIBID = CStr(hashRFQID(hashPPId("DB")))
                Session.Remove("flag")
                Session("flag") = ""
                DealClicked(strDBIBID)
            Else
                Session.Remove("flag")
                DealClicked(Quote_ID)
            End If

            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>

            '<AvinashG. on 18-Mar-2014: TO CLose timer if deal done>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopTimerDBIBDeal", "StopPPTimerValue('" + btnDBIBDeal.ClientID + "');", True)
            btnDBIBPrice.Text = "Price"
            DBIBHiddenPrice.Value = ";Enable;Disable;Disable;Price"

            upnlChart.Update()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "btnDBIBDeal_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnDBIBDeal_Click", ErrorLevel.High)
        End Try
    End Sub
    Private Sub DealClicked(ByVal strId As String)
        Dim orderQuantity As String = ""
        Dim strType As String = ""
        Dim strLimitPrice1 As String = ""
        Dim strLimitPrice2 As String = ""
        Dim strLimitPrice3 As String = ""
        Dim strMargin As String = ""
        Dim strClientPrice As String = ""
        Dim strClientYield As String = ""
        Dim strBookingBranch As String = ""
        Dim strJavaScriptDealClicked As New StringBuilder
        Dim strRMNameforOrderConfirm As String = ""
        Dim strRMEmailIdforOrderConfirm As String = ""
        Dim strLoginUserEmail As String = ""
        Try
          
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptDealClicked", strJavaScriptDealClicked.ToString, True)
            lblerror.Text = ""
            strMargin = txtUpfrontPopUpValue.Text
            strClientPrice = lblClientPricePopUpValue.Text
            strClientYield = txtClientYieldPopUpValue.Text
            strBookingBranch = ddlBookingBranchPopUpValue.SelectedValue
            ''orderQuantity = lblNotionalPopUpValue.Text
            orderQuantity = SetNumberFormat(lblNotionalPopUpValue.Text, 0) ''EQBOSDEV-228 Added by chaitali removing decimal
            strRMNameforOrderConfirm = ddlRM.SelectedValue
            If ddlOrderTypePopUpValue.SelectedItem.Text.ToUpper.Contains("LIMIT") Then
                strLimitPrice1 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", ""))) '<Avinash G. on 2-May-2014: Val returns wrong value becuase of formatted text value>
                strLimitPrice2 = ""
                strLimitPrice3 = ""
                strType = "Limit"
            Else
                strLimitPrice1 = ""
                strLimitPrice2 = ""
                strLimitPrice3 = ""
                strType = "Market"
            End If




            strLoginUserEmail = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)
            If (lblEmail.Text.Trim <> strLoginUserEmail.Trim) And (lblEmail.Text.Trim <> "") Then
                strRMEmailIdforOrderConfirm = strLoginUserEmail & "," & lblEmail.Text
            Else
                strRMEmailIdforOrderConfirm = strLoginUserEmail
            End If
            Dim count As Integer = 0
            Dim sPoolID As String
            sPoolID = "0"
            If Not IsNothing(Request.QueryString("EXTLOD")) Then
                If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                    If Not IsNothing(Request.QueryString("PoolID")) Then
                        sPoolID = Request.QueryString("PoolID")
                    End If
                End If
            End If
            Dim sRedirectOrderID As String
            sRedirectOrderID = "0"
            If Not IsNothing(Request.QueryString("EXTLOD")) Then
                If UCase(Request.QueryString("EXTLOD")) = "REDIRECTEDHEDGE" Then
                    If Not IsNothing(Request.QueryString("RedirectedOrderId")) Then
                        sRedirectOrderID = Request.QueryString("RedirectedOrderId")
                    End If
                End If
            End If


            ''<Added by Rushikesh on 17-Sep-16 SCB requirement Pretrade Allocation>
            Dim strPreTradeXml As StringBuilder
            strPreTradeXml = savePretradeAllocation(strId)
            ''If savePretradeAllocation(strId) Then
            ''Else
            ''    Exit Sub
            ''End If
            ''<Added by Rushikesh on 17-Sep-16 SCB requirement Pretrade Allocation>



            Dim sOrderComment As String = ""
            sOrderComment = txtOrderCmt.Text.Trim
            ''<Start | Nikhil M. on 16-Sep-2016: Added for Deal confirmation reason>
            Dim strConfirmReason As String = ""
            'strConfirmReason = drpConfirmDeal.SelectedText.ToString
            ''<End | Nikhil M. on 16-Sep-2016: Added for Deal confirmation reason>
            Select Case objELNRFQ.web_Get_orderPlaced_with_Margin_Price_Yield(orderQuantity.Replace(",", ""), strType, strLimitPrice1, strLimitPrice2, strLimitPrice3, _
                                                                              strId, sPoolID, sRedirectOrderID, LoginInfoGV.Login_Info.LoginId, sOrderComment, strMargin, strClientPrice, _
                                                                              strClientYield, strBookingBranch, _
                                                                              strRMNameforOrderConfirm, strRMEmailIdforOrderConfirm, strConfirmReason, strPreTradeXml.ToString) ''<Nikhil M. on 16-Sep-2016: Added "strConfirmReason"  Parameter >
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful

                    lblerror.Text = "Order requested for RFQ " & strId
                    lblerror.ForeColor = Color.Blue
                    ShowHideConfirmationPopup(False)
                    If rbHistory.SelectedValue = "Order History" Then
                        fill_OrderGrid()
                        makeThisGridVisible(grdOrder)
                        ColumnVisibility()
                        upnlGrid.Update()
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
            End Select
            btnJPMprice.Text = "Price"
            btnJPMDeal.CssClass = "btnDisabled"
            btnHSBCPrice.Text = "Price"
            btnHSBCDeal.CssClass = "btnDisabled"
            btnCSPrice.Text = "Price"
            btnCSDeal.CssClass = "btnDisabled"
            btnBNPPPrice.Text = "Price"
            btnBNPPDeal.CssClass = "btnDisabled"
            btnUBSPrice.Text = "Price"
            btnUBSDeal.CssClass = "btnDisabled"
            btnBAMLPrice.Text = "Price"
            btnBAMLDeal.CssClass = "btnDisabled"
            btnDBIBPrice.Text = "Price"
            btnDBIBDeal.CssClass = "btnDisabled"
            btnCITIprice.Text = "Price"
            btnCITIDeal.CssClass = "btnDisabled"
            btnLEONTEQprice.Text = "Price"
            btnLEONTEQDeal.CssClass = "btnDisabled"
            btnCOMMERZprice.Text = "Price"
            btnCOMMERZDeal.CssClass = "btnDisabled"
            btnOCBCprice.Text = "Price"
            btnOCBCprice.CssClass = "btnDisabled"

            btnBNPPDeal.Visible = False
            btnBAMLDeal.Visible = False
            btnCSDeal.Visible = False
            btnJPMDeal.Visible = False
            btnHSBCDeal.Visible = False
            btnUBSDeal.Visible = False
            btnDBIBDeal.Visible = False
            btnCITIDeal.Visible = False
            btnLEONTEQDeal.Visible = False
            btnCOMMERZDeal.Visible = False
            btnOCBCDeal.Visible = False

            txtQuantity.Enabled = True

            '<ashwiniP Start 21Sept16>
            lblTotalAmt.Visible = False
            lblTotalAmtVal.Visible = False
            lblAlloAmt.Visible = False
            lblAlloAmtVal.Visible = False
            lblRemainNotional.Visible = False
            lblRemainNotionalVal.Visible = False
            '<ashwiniP End>

            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If


        Catch ex As Exception
            lblerror.Text = "DealClicked:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "DealClicked", ErrorLevel.High)
            Throw ex
        Finally

        End Try

    End Sub



#End Region
#Region "Tenor & DateCalcutaion"
    Private Sub ReCalcDate()
        Try
            If (CType(Session("IsManualDateEditYN"), String) = "N" Or CType(Session("IsManualDateEditYN"), String) = "") Then
                 GetDates()
            End If
            GetCommentary()
            Enable_Disable_Deal_Buttons()
        Catch ex As Exception
            lblerror.Text = "ReCalcDate:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ReCalcDate", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub txtTenor_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTenor.TextChanged

        Dim TenorInDays As Integer = 0
        Dim strTenorType As String = String.Empty
        Try
            lblerror.Text = ""
            If ValidateTenor() = False Then   ''AshwiniP on 09-Nov-2016
                Exit Sub
            Else
                GetDates()
                ResetAll()                    ''Sequence Changed by AshwiniP on 04-oct-2016
                GetCommentary()
                Enable_Disable_Deal_Buttons()
            End If

        Catch ex As Exception
            lblerror.Text = "txtTenor_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtTenor_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    ''AshwiniP on 09-Nov-2016
    Public Sub Disablebuttons()
        Try
            btnSolveAll.Enabled = False
            btnSolveAll.CssClass = "btnDisabled"
            btnCSPrice.Enabled = False
            btnCSPrice.CssClass = "btnDisabled"
            btnJPMprice.Enabled = False
            btnJPMprice.CssClass = "btnDisabled"
            btnBAMLPrice.Enabled = False
            btnBAMLPrice.CssClass = "btnDisabled"
            btnBNPPPrice.Enabled = False
            btnBNPPPrice.CssClass = "btnDisabled"
            btnUBSPrice.Enabled = False
            btnUBSPrice.CssClass = "btnDisabled"
            btnHSBCPrice.Enabled = False
            btnHSBCPrice.CssClass = "btnDisabled"
            btnDBIBPrice.Enabled = False
            btnDBIBPrice.CssClass = "btnDisabled"
            btnOCBCprice.Enabled = False
            btnOCBCprice.CssClass = "btnDisabled"
            btnCITIprice.Enabled = False
            btnCITIprice.CssClass = "btnDisabled"
            btnLEONTEQprice.Enabled = False
            btnLEONTEQprice.CssClass = "btnDisabled"
            btnCOMMERZprice.Enabled = False
            btnCOMMERZprice.CssClass = "btnDisabled"
        Catch ex As Exception
            lblerror.Text = "Disablebuttons:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtTenor_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    ''AshwiniP on 09-Nov-2016: Config to allow tenor in months <START>
    Public Function ValidateTenor() As Boolean
        Try


            Dim interval1 As String = ddlTenorTypeELN.SelectedValue
            If interval1 = "MONTH" Then
                interval1 = "M"
            ElseIf interval1 = "YEAR" Then
                interval1 = "Y"
            End If
            Dim monthcount As Integer = 0
            If interval1 = "Y" And CDbl(txtTenor.Text) = 1 Then
                monthcount = 12
            ElseIf interval1 = "Y" And CDbl(txtTenor.Text) <> 1 Then
                lblerror.Text = "Please enter valid tenor."
                Disablebuttons()
                Exit Function
            Else
                monthcount = CInt(txtTenor.Text)
            End If
            Dim max_months As Integer = 0
            max_months = CInt(objReadConfig.ReadConfig(dsConfig, "ELN_AllowedTenorInMonths", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "12").Trim.ToUpper())
            If monthcount > max_months Then
                lblerror.Text = "Please enter valid tenor."
                Disablebuttons()
                Exit Function
            End If

            ''<END>
            Return True

        Catch ex As Exception
            lblerror.Text = "ValidateTenor:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ValidateTenor", ErrorLevel.High)
        End Try
    End Function

    Public Sub GetDates()
        Dim Offset As Double = 0.0
        Dim Expiry_date As String = "" '' Fixing Date
        Dim Maturity_Date As String = ""
        Dim Ccy As String = ""
        Dim Ccy1 As String = ""
        Dim SoftTenor As String = ""
        Dim Settlement_Days As String = ""
        Dim Maturity_Days As String = ""
        Dim Value_Date As String = ""
        Dim Fixing_Date As String = ""
        Dim Friday_Preferred_YN As String = ""
        Dim dtExchangeInfo As DataTable
        Try
            Dim Trade_Date As String = Today.Date.ToString("dd-MMM-yyyy")
            Ccy = lblELNBaseCcy.Text
            If chkQuantoCcy.Checked = False Then
                Ccy1 = lblELNBaseCcy.Text
            Else
                Ccy1 = ddlQuantoCcy.SelectedValue
            End If
            Dim interval As String = ddlTenorTypeELN.SelectedValue
            If interval = "DAY" Then
                interval = "D"
            ElseIf interval = "WEEK" Then
                interval = "W"
            ElseIf interval = "MONTH" Then
                interval = "M"
            ElseIf interval = "YEAR" Then
                interval = "Y"
            End If
            SoftTenor = txtTenor.Text & interval
            Settlement_Days = txtValueDays.Text
            Dim dr As DataRow()
            dtExchangeInfo = New DataTable("Dummy")
            dtExchangeInfo = CType(Session("Exchange_Details"), DataTable)
            If ddlExchange.SelectedValue.ToUpper = "ALL" Then
                Dim sTemp As String
                dr = dtExchangeInfo.Select("Exchange_Name = '" & objELNRFQ.GetShareExchange(ddlShare.SelectedValue.ToString, sTemp) & "' ")
            Else
                dr = dtExchangeInfo.Select("Exchange_Name = '" & ddlExchange.SelectedValue.Trim.ToUpper & "' ")
            End If
            If dr.Length > 0 Then
                Maturity_Days = dr(0).Item("SettlementDays").ToString
                If Val(Maturity_Days) = 0 Then
                    Maturity_Days = "2"
                Else
                    Maturity_Days = Maturity_Days
                End If
            End If
            Friday_Preferred_YN = objReadConfig.ReadConfig(dsConfig, "MaturityDate_FridayPreferred_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "")
            Select Case objELNRFQ.getUSDBasedDates(CStr(LoginInfoGV.Login_Info.EntityID), Ccy, Ccy1, Ccy, _
                                                   SoftTenor, Trade_Date, Settlement_Days, _
                                                   Maturity_Days, Friday_Preferred_YN, Value_Date, Fixing_Date, Maturity_Date)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    Session.Add("SettlementDate", Value_Date)
                    Session.Add("ExpiryDate", Fixing_Date)
                    Session.Add("MaturityDate", Maturity_Date)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                    lblerror.Text = "GetDates:Error occurred in processing."
            End Select
            txtTradeDate.Value = FinIQApp_Date.FinIQDate(Trade_Date)
            txtMaturityDate.Value = FinIQApp_Date.FinIQDate(Maturity_Date)
            txtSettlementDate.Value = FinIQApp_Date.FinIQDate(Value_Date)
            txtExpiryDate.Value = FinIQApp_Date.FinIQDate(Fixing_Date)
            lblDaysVal.Text = DateDiff(DateInterval.Day, CDate(Convert.ToString(Session("Settlementdate"))), CDate(Convert.ToString(Session("MaturityDAte")))).ToString '<AvinashG. on 08-Nov-2016:EQSCB-164 Add Number of Days on screen for ELN>
            upnl1.Update()
            Session("IsManualDateEditYN") = "N"
        Catch ex As Exception
            lblerror.Text = "GetDates:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "GetDates", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Function isWeekendDay(ByVal strDate As String) As Boolean
        Try
            If IsDate(strDate) = True Then

                Select Case DatePart(DateInterval.Weekday, CDate(strDate))
                    Case 1, 7
                        isWeekendDay = True
                    Case Else
                        isWeekendDay = False
                End Select

            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "isWeekendDay", ErrorLevel.High)
        End Try
    End Function

    Private Function addDaysToDate(ByVal strDate As String, ByVal intDays As Integer) As String
        Try
            If IsDate(strDate) = True Then
                addDaysToDate = CStr(DateAdd(DateInterval.Day, intDays, CDate(strDate)))
            Else
                addDaysToDate = strDate
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "addDaysToDate", ErrorLevel.High)
        End Try
    End Function


    Private Sub txtExpiryDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExpiryDate.TextChanged
        Try
            lblerror.Text = ""
            Dim strexpiryDAte As String = txtExpiryDate.Value
            If strexpiryDAte <> "" Then
                Session.Add("expiryDAte", strexpiryDAte)
                Dim strExpiryDay As String = CDate(txtExpiryDate.Value).Date.DayOfWeek.ToString
            End If
            ResetAll()                    ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()
            Session.Add("IsManualDateEditYN", "Y")
        Catch ex As Exception
            lblerror.Text = "txtExpiryDate_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtExpiryDate_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub txtMaturityDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMaturityDate.TextChanged
        Try
            lblerror.Text = ""
            Dim strMaturityDAte As String = txtMaturityDate.Value
            If strMaturityDAte <> "" Then
                Session.Add("MaturityDAte", strMaturityDAte)
                Dim strMaturityDay As String = CDate(txtMaturityDate.Value).Date.DayOfWeek.ToString
                Dim strDays As String = CStr(DateDiff(DateInterval.Day, CDate(txtSettlementDate.Value), CDate(txtMaturityDate.Value)))
            End If
            ResetAll()                ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()
            lblDaysVal.Text = DateDiff(DateInterval.Day, CDate(Convert.ToString(Session("Settlementdate"))), CDate(Convert.ToString(Session("MaturityDAte")))).ToString '<AvinashG. on 08-Nov-2016:EQSCB-164 Add Number of Days on screen for ELN>
            Session.Add("IsManualDateEditYN", "Y")
        Catch ex As Exception
            lblerror.Text = "txtMaturityDate_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtMaturityDate_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub txtSettlementDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSettlementDate.TextChanged
        Try
            lblerror.Text = ""
            Dim strSettlementdate As String = txtSettlementDate.Value
            If strSettlementdate <> "" Then
                Session.Add("Settlementdate", strSettlementdate)
                Dim strSettleDay As String = CDate(txtSettlementDate.Value).Date.DayOfWeek.ToString
            End If
            ResetAll()                    ''Sequence Changed by AshwiniP on 04-oct-2016
            GetCommentary()
            lblDaysVal.Text = DateDiff(DateInterval.Day, CDate(Convert.ToString(Session("Settlementdate"))), CDate(Convert.ToString(Session("MaturityDAte")))).ToString '<AvinashG. on 08-Nov-2016:EQSCB-164 Add Number of Days on screen for ELN>
            Session.Add("IsManualDateEditYN", "Y")
            hdnSettDateManualChangeYN.Value = "Y"
        Catch ex As Exception
            lblerror.Text = "txtSettlementDate_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtSettlementDate_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub ddlTenorTypeELN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTenorTypeELN.SelectedIndexChanged
        Try
            lblerror.Text = ""
            If ValidateTenor() = False Then   ''AshwiniP on 09-Nov-2016
                Exit Sub
            Else
                GetDates()
                ResetAll()                          ''Sequence changed by AshwiniP on 04-Oct-2016
                GetCommentary()
                Enable_Disable_Deal_Buttons()
            End If

        Catch ex As Exception
            lblerror.Text = "ddlTenorTypeELN_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlTenorTypeELN_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub




#End Region
#Region "Timer,Button Visibility"
    Public Sub ColumnVisibility()
        Try
            If rbHistory.SelectedValue.Trim = "Quote History" Then
                makeThisGridVisible(grdELNRFQ)
                fill_grid()
            ElseIf rbHistory.SelectedValue.Trim = "Order History" Then
                makeThisGridVisible(grdOrder)
                grdOrder.Columns(grdOrderEnum.EP_OfferPrice).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_Upfront).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_CouponPercentage).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Notional_Amount1).Visible = False
                grdOrder.Columns(grdOrderEnum.LimitPrice2).Visible = False
                grdOrder.Columns(grdOrderEnum.LimitPrice3).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Execution_Price1).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_Execution_Price2).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Execution_Price3).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Client_Price).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_Client_Yield).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_RM_Margin).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_AveragePrice).Visible = True
                grdOrder.Columns(grdOrderEnum.ER_Tenor).Visible = True
                grdOrder.Columns(grdOrderEnum.ER_GuaranteedDuration).Visible = False
                grdOrder.Columns(grdOrderEnum.ER_LeverageRatio).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_StrikePercentage).Visible = True
                ''grdOrder.Columns(grdOrderEnum.EP_HedgingOrderId).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_Order_Remark1).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_KO).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_ExternalQuoteId).Visible = False
                grdOrder.DataBind()
            End If
            upnlGrid.Update()
        Catch ex As Exception
            lblerror.Text = "ColumnVisibility:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ColumnVisibility", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
#End Region
#Region "Grid Data Bound,PageIndex,sort"
    Private Sub grdELNRFQ_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdELNRFQ.ItemCommand
        Dim strNewTenorELN As String = String.Empty
        Dim strNewTenorELNType As String = String.Empty

        Dim dtSelectedShare As DataTable
        Try
            lblmsgPriceProvider.Text = ""
            lblerror.Text = ""
            If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                If e.CommandName.ToUpper = "SELECT" Then
                    ShowHideConfirmationPopup(False)
                    ResetAll()
                    grdELNRFQ.SelectedItemStyle.BackColor = Color.FromArgb(242, 242, 243)
                    Dim strType As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Type).Text
                    If strType = "Simple" Then
                        chkELNType.Checked = False
                        chkELNType_CheckedChanged(Nothing, Nothing)
                        txtBarrier.Enabled = False
                        ddlBarrier.Enabled = False
                        txtBarrier.Visible = False
                        ddlBarrier.Visible = False
                        txtBarrier.Text = "0" '<AvinashG. on 24-Feb-2016: EQBOSDEV-264 KO stickiness issue FA-1322 KO stickiness issue >
                    Else
                        chkELNType.Checked = True
                        chkELNType_CheckedChanged(Nothing, Nothing)
                        txtBarrier.Enabled = True
                        ddlBarrier.Enabled = True
                        txtBarrier.Visible = True
                        ddlBarrier.Visible = True
                        Dim strBarrierType As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Barrier_Type).Text.ToUpper  'Mohit Lalwani on 4-Feb-2016
                        ''''<Dilkhush 13May2016 FA-1427>
                        ''ddlBarrier.SelectedValue = strBarrierType
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SHOW_DAILYCLOSE_KO", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            Case "Y", "YES"
                                ddlBarrier.SelectedValue = strBarrierType
                            Case "N", "NO"
                                If strBarrierType.ToUpper <> "DAILY_CLOSE" Then
                                    ddlBarrier.SelectedValue = strBarrierType
                                End If
                        End Select
                        ''''</Dilkhush 13May2016 FA-1427>
                        Dim strBarrier As String = SetNumberFormat(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Barrier).Text, 2)
                        txtBarrier.Text = strBarrier
                    End If

                    Dim strExchng As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Exchange).Text
                    Dim strShareVal As String = String.Empty
                    strShareVal = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Share).Text
                    setShare(strExchng, strShareVal)


                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                        Case "Y", "YES"
                            ''ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Share).Text))
                            ''ddlShare.Text = ddlShare.SelectedItem.Text
                            If ddlShare.SelectedValue IsNot Nothing Then
                                ddlShare.Text = ddlShare.SelectedItem.Text ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            End If
                            lblExchangeVal.Text = setExchangeByShare(ddlShare)
                        Case "N", "NO"
                            If ddlExchange.SelectedValue = strExchng Then
                                ddlExchange.SelectedValue = strExchng
                                ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Share).Text))
                                'ddlShare.Text = ddlShare.Text
                                If ddlShare.SelectedValue IsNot Nothing Then
                                    ddlShare.Text = ddlShare.SelectedItem.Text ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                End If
                                lblExchangeVal.Text = setExchangeByShare(ddlShare)
                            Else
                                ddlExchange.SelectedValue = strExchng
                                '' Fillddl_Share() ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Share).Text))
                                If ddlShare.SelectedValue IsNot Nothing Then
                                    ddlShare.Text = ddlShare.SelectedItem.Text ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                End If
                            End If
                    End Select
                    getCurrency(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Share).Text)
                    getPRR(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Share).Text) ''AshwiniP 23Sept2016
                    getFlag(ddlShare.SelectedValue.ToString)
                    Dim strQuantoCcy As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Base).Text
                    If lblELNBaseCcy.Text = strQuantoCcy Then
                        chkQuantoCcy.Checked = False
                        ddlQuantoCcy.Enabled = False
                        lblQuantity.Text = "Notional (<font style='font-weight:bold;'>" & lblELNBaseCcy.Text & "</font>)"
                        ddlQuantoCcy.DataSource = Nothing
                        ddlQuantoCcy.DataBind()
                        ddlQuantoCcy.Items.Clear()
                        ddlQuantoCcy.Items.Add(New DropDownListItem(lblELNBaseCcy.Text, lblELNBaseCcy.Text)) 'Mohit Lalwani on 8-Jul-2016
                        ddlQuantoCcy.BackColor = Color.FromArgb(242, 242, 243)
                    Else
                        chkQuantoCcy.Checked = True
                        ddlQuantoCcy.Enabled = True
                        Call Fillddl_QuantoCcy()
                        ddlQuantoCcy.BackColor = Color.White
                        ddlQuantoCcy.SelectedIndex = ddlQuantoCcy.FindItemByText(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Base).Text).Index
                        lblQuantity.Text = "Notional (<font style=''>" & strQuantoCcy & "</font>)"
                    End If

                    Dim strTenor As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.RFQTenor).Text
                    txtTenor.Text = strTenor
                    For i = 0 To strTenor.Length - 1
                        If IsNumeric(strTenor.Substring(i, 1)) = True Then
                            strNewTenorELN = strNewTenorELN + strTenor.Substring(i, 1)
                        End If
                    Next
                    txtTenor.Text = strNewTenorELN
                    For i = 0 To strTenor.Length - 1
                        If IsNumeric(strTenor.Substring(i, 1)) = False Then
                            strNewTenorELNType = strNewTenorELNType + strTenor.Substring(i, 1)
                        End If
                    Next
                    ' ddlTenorTypeELN.SelectedItem.Text = strNewTenorELNType
                    ddlTenorTypeELN.SelectedValue = strNewTenorELNType.ToUpper.Trim   '<Changed by Mohit Lalwani on 11-Dec-2015>
                    Dim strSetDays As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Value).Text
                    Dim strSettleDays As String = CStr(CDbl(strSetDays.Trim.Split(CChar("+"))(1).Trim))
                    If strSettleDays = "7" Then
                        ddlSettlementDays.SelectedValue = "1W"
                    ElseIf strSettleDays = "14" Then
                        ddlSettlementDays.SelectedValue = "2W"
                    Else
                        ddlSettlementDays.SelectedValue = "1W"

                    End If
                    txtValueDays.Text = strSettleDays
                    Dim strTradeDate As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Trade_Date).Text
                    If strTradeDate = Today.ToString("dd-MMM-yy") Then
                        txtTradeDate.Value = Convert.ToDateTime(strTradeDate).ToString("dd-MMM-yyyy")
                        Dim strSettDate As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Settlement_Date).Text
                        txtSettlementDate.Value = Convert.ToDateTime(strSettDate).ToString("dd-MMM-yyyy")
                        Dim strExpDate As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Expiry_Date).Text
                        txtExpiryDate.Value = Convert.ToDateTime(strExpDate).ToString("dd-MMM-yyyy")
                        Dim strMatDate As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Maturity_Date).Text
                        txtMaturityDate.Value = Convert.ToDateTime(strMatDate).ToString("dd-MMM-yyyy")
                    Else
                        GetDates()
                    End If
                    Dim strSolveFor As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Solve_For).Text
                    If strSolveFor = "Price(%)" Then
                        ddlSolveFor.SelectedValue = "PricePercentage"
                        txtELNPrice.Text = "0.00"
                        txtELNPrice.Enabled = False
                        txtELNPrice.BackColor = Color.FromArgb(242, 242, 243)
                        txtStrike.Enabled = True
                        txtStrike.BackColor = Color.White
                        lblSolveForType.Text = "IB Price (%)"
                    Else
                        ddlSolveFor.SelectedValue = "StrikePercentage"
                        txtELNPrice.Enabled = True
                        txtELNPrice.BackColor = Color.White
                        txtStrike.Text = "0.00"
                        txtStrike.Enabled = False
                        txtStrike.BackColor = Color.FromArgb(242, 242, 243)
                        lblSolveForType.Text = "Strike (%)"
                    End If
                    Dim strStrike As String = SetNumberFormat(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Strike_Per).Text, 2)
                    If strStrike = "&nbsp;" Then
                        txtStrike.Text = "0.00"
                    Else
                        txtStrike.Text = strStrike
                    End If
                    Dim strELNPrice As String = SetNumberFormat(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Price_Per).Text, 2)
                    If strELNPrice = "&nbsp;" Then
                        txtELNPrice.Text = "0.00"
                    Else
                        txtELNPrice.Text = strELNPrice
                    End If
                    Dim strOrderqty As String = SetNumberFormat(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Order_Quantity).Text, 0) ''EQBOSDEV-228  Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                    txtQuantity.Text = strOrderqty
                    grdELNRFQ.SelectedItemStyle.BackColor = Color.FromArgb(242, 242, 243)
                ElseIf e.CommandName.ToUpper = "CREATEPOOLELN" Then
                    Dim strTradeDate As String = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Trade_Date).Text
                    If CDate(strTradeDate).ToString("dd-MMM-yy") = Today.ToString("dd-MMM-yy") Then
                        Response.Redirect(sPoolRedirectionPath + "&PRD=ELN&RFQID=" + DirectCast(DirectCast(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.RFQ_ID).Controls(1), System.Web.UI.Control), System.Web.UI.WebControls.LinkButton).Text, False)
                    Else
                        lblerror.Text = "Cannot create pool from quotes other than today. Please use today's quotes only."
                    End If
                    'Added by Mohit lalwani on 14-Mar-2016 
                ElseIf e.CommandName.ToUpper = "GENERATEDOCUMENT" Then
                    ' Generate(DirectCast(DirectCast(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.RFQ_ID).Controls(1), System.Web.UI.Control), System.Web.UI.WebControls.LinkButton).Text)
                    '/Added by Mohit lalwani on 14-Mar-2016 

                    '<RiddhiS. on 09-Oct-2016: To open KYIR on Click>
                    KYIR_Click(Nothing, Nothing)


                ElseIf e.CommandName.ToUpper = "GETRFQDETAILS" Then
                    ShowHideDeatils(True)
                    lblDetails.Text = "RFQ Details"
                    Label24.Text = "Issuer Remark"  'Added by Mohit/Rushi on 02-May-2016 FA-1420
                    pnlDetailsPopup.Visible = True
                    trStatus.Visible = False
                    trOrderType.Visible = False
                    trSpot.Visible = False
                    trExePrc1.Visible = False
                    trAvgExePrc.Visible = False
                    trQuoteStatus.Visible = True
                    trOrderComment.Visible = False

                    '''''''''''''''''''''''''''''''''''
                    lblAlloRFQID.Text = ""
                    lclAlloCP.Text = ""
                    lblAlloClientPrice.Text = ""
                    lblAlloExpiDt.Text = ""
                    lblAlloKO.Text = ""
                    lblAlloMatuDt.Text = ""
                    lblAlloNoteCcy.Text = ""
                    lblAlloOrderSize.Text = ""
                    lblalloOrderType.Text = ""
                    lblAlloPrice.Text = ""
                    lblAlloRemark.Text = ""
                    lblAlloSettDt.Text = ""
                    lblAlloSettWk.Text = ""
                    lblAlloStrike.Text = ""
                    lblAlloSubmitteddBy.Text = ""
                    lblAlloTenor.Text = ""
                    lblAlloTradeDt.Text = ""
                    lblAlloUnderlying.Text = ""
                    lblAlloUpfront.Text = ""
                    lblAlloYield.Text = ""
                    lblAlloOrderStatus.Text = ""
                    lblAlloOrderStatus.Text = ""
                    lblAlloExePrc1.Text = ""
                    lblAlloAvgExePrc.Text = ""
                    lblAlloSpot.Text = ""
                    lblValAlloSolvefor.Text = ""
                    lblValQuoteStatus.Text = ""
                    ''</added by Rushikesh on 29-Dec-15 to flush old value>
                    '  grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Trade_Date)
                    lblAlloRFQID.Text = DirectCast(DirectCast(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.RFQ_ID).Controls(1), System.Web.UI.Control), System.Web.UI.WebControls.LinkButton).Text
                    lclAlloCP.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Provider_Name).Text
                    lblAlloClientPrice.Text = SetNumberFormat(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.ClientPrice).Text, 2)
                    lblAlloExpiDt.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Expiry_Date).Text
                    ' lblAlloYield.Text = SetNumberFormat(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Expiry_Date).Text, 2)
                    'Mohit 28-Dec-2015
                    If grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Barrier).Text.Trim <> "" And grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Barrier).Text.Trim <> "&nbsp;" And grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Barrier).Text.Trim <> "0" Then
                        lblAlloKO.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Barrier).Text + "&nbsp(" + grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Barrier_Type).Text + ")"
                    Else
                    End If
                    lblAlloMatuDt.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Maturity_Date).Text
                    lblAlloNoteCcy.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Base).Text
                    lblAlloOrderSize.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Order_Quantity).Text
                    'lblalloOrderType.Text = item("ELN_Order_Type").Text
                    lblAlloPrice.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Price_Per).Text
                    lblAlloRemark.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Remark).Text


                    lblAlloSettDt.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Settlement_Date).Text
                    lblAlloSettWk.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Value).Text
                    ''lblAlloSpot.Text = item("").Text '' Need to discuss.
                    lblAlloStrike.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Strike_Per).Text
                    lblAlloSubmitteddBy.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.created_by).Text
                    lblAlloTenor.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Tenor).Text + "&nbsp;Month(s)"
                    lblAlloTradeDt.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Trade_Date).Text
                    lblAlloUnderlying.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Share).Text
                    lblValAlloSolvefor.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Solve_For).Text
                    lblAlloUpfront.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Upfront).Text
                    'lblAlloYield.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.).Text

                    'lblAlloOrderStatus.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Price_Per).Text
                    'lblAlloExePrc1.Text = item("Execution_Price1").Text
                    'lblAlloAvgExePrc.Text = item("AvgPrice").Text
                    'If item("ELN_Order_Type").Text.ToUpper = "LIMIT" Then
                    '    lblAlloSpot.Visible = True
                    '    lblAlloSpot.Text = item("LimitPrice1").Text
                    'Else
                    '    lblAlloSpot.Visible = False
                    'End If
                    lblValQuoteStatus.Text = grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Quote_Status).Text
                    lblAlloYield.Text = SetNumberFormat(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.ClientYield).Text, 2)

                    '''''''''''''''''''''''''''''''''''
                    upnlDetails.Update()
                    RestoreAll()
                    RestoreSolveAll()
                    upnlGrid.Update()
                End If
            End If
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            manageShareReportShowHide()
            upnl1.Update()
            GetCommentary()
        Catch ex As Exception
            lblerror.Text = "grdELNRFQ_ItemCommand:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdELNRFQ_ItemCommand", ErrorLevel.High)
        End Try
    End Sub
    Private Sub grdELNRFQ_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdELNRFQ.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.EditItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                If e.Item.Cells(grdELNRFQEnum.Trade_Date).Text <> "&nbsp;" Then
                    e.Item.Cells(grdELNRFQEnum.Trade_Date).Text = Format(CDate(e.Item.Cells(grdELNRFQEnum.Trade_Date).Text), "dd-MMM-yy")
                End If
                If e.Item.Cells(grdELNRFQEnum.Settlement_Date).Text <> "&nbsp;" Then
                    e.Item.Cells(grdELNRFQEnum.Settlement_Date).Text = Format(CDate(e.Item.Cells(grdELNRFQEnum.Settlement_Date).Text), "dd-MMM-yy")
                End If
                If e.Item.Cells(grdELNRFQEnum.Expiry_Date).Text <> "&nbsp;" Then
                    e.Item.Cells(grdELNRFQEnum.Expiry_Date).Text = Format(CDate(e.Item.Cells(grdELNRFQEnum.Expiry_Date).Text), "dd-MMM-yy") 'FinIQApp_Date.FinIQDate(e.Item.Cells(9).Text)
                End If
                If e.Item.Cells(grdELNRFQEnum.Maturity_Date).Text <> "&nbsp;" Then
                    e.Item.Cells(grdELNRFQEnum.Maturity_Date).Text = Format(CDate(e.Item.Cells(grdELNRFQEnum.Maturity_Date).Text), "dd-MMM-yy") 'FinIQApp_Date.FinIQDate(e.Item.Cells(10).Text)
                End If
                If e.Item.Cells(grdELNRFQEnum.Quoted_At).Text <> "&nbsp;" Then
                    e.Item.Cells(grdELNRFQEnum.Quoted_At).Text = Format(CDate(e.Item.Cells(grdELNRFQEnum.Quoted_At).Text), "dd-MMM-yyyy hh:mm:ss tt") 'FinIQApp_Date.FinIQDate(e.Item.Cells(20).Text)
                End If
                If e.Item.Cells(grdELNRFQEnum.Price_Per).Text <> "&nbsp;" Then
                    e.Item.Cells(grdELNRFQEnum.Price_Per).Text = SetNumberFormat(e.Item.Cells(grdELNRFQEnum.Price_Per).Text, 2)
                End If
                If e.Item.Cells(grdELNRFQEnum.Strike_Per).Text <> "&nbsp;" Then
                    e.Item.Cells(grdELNRFQEnum.Strike_Per).Text = SetNumberFormat(e.Item.Cells(grdELNRFQEnum.Strike_Per).Text, 2)
                End If
                If e.Item.Cells(grdELNRFQEnum.Order_Quantity).Text <> "&nbsp;" Then
                    e.Item.Cells(grdELNRFQEnum.Order_Quantity).Text = SetNumberFormat(e.Item.Cells(grdELNRFQEnum.Order_Quantity).Text, 0)    '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F ''reverted
                End If
                If e.Item.Cells(grdELNRFQEnum.Barrier).Text <> "&nbsp;" Then
                    e.Item.Cells(grdELNRFQEnum.Barrier).Text = SetNumberFormat(e.Item.Cells(grdELNRFQEnum.Barrier).Text, 2)
                End If
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowRFQByClubbing_OnPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        If e.Item.ItemIndex = 0 Then
                            e.Item.CssClass = "Grp1"
                        Else
                            If (e.Item.Cells(grdELNRFQEnum.ClubbingRFQId).Text.ToString = "" Or e.Item.Cells(grdELNRFQEnum.ClubbingRFQId).Text.ToString = "&nbsp;") And (grdELNRFQ.Items(e.Item.ItemIndex - 1).Cells(grdELNRFQEnum.ClubbingRFQId).Text.ToString = "" Or grdELNRFQ.Items(e.Item.ItemIndex - 1).Cells(grdELNRFQEnum.ClubbingRFQId).Text.ToString = "&nbsp;") Then
                                e.Item.CssClass = If(grdELNRFQ.Items(e.Item.ItemIndex - 1).CssClass = "Grp2", "Grp1", "Grp2")
                            Else
                                If (e.Item.Cells(grdELNRFQEnum.ClubbingRFQId).Text.ToString = grdELNRFQ.Items(e.Item.ItemIndex - 1).Cells(grdELNRFQEnum.ClubbingRFQId).Text.ToString) Then
                                    e.Item.CssClass = grdELNRFQ.Items(e.Item.ItemIndex - 1).CssClass
                                Else
                                    e.Item.CssClass = If(grdELNRFQ.Items(e.Item.ItemIndex - 1).CssClass = "Grp2", "Grp1", "Grp2")
                                End If
                            End If

                        End If
                    Case "N", "NO"
                End Select
                ''Added by Chitralekha on 1-Oct-16
                If e.Item.Cells(grdELNRFQEnum.BestPrice_YN).Text = "Y" Then
                    e.Item.CssClass = "BestPriceHighlight"

                End If
                ''Ended by Chitralekha on 1-Oct-16
            End If
           
        Catch ex As Exception
            lblerror.Text = "grdELNRFQ_ItemDataBound:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdELNRFQ_ItemDataBound", ErrorLevel.High)
        End Try
    End Sub
    'Mohit Lalwani on 22-Jan-2015
    Private Sub grdOrder_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdOrder.ItemCommand
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                If e.CommandName.ToUpper = "GETORDERDETAILS" Then
                    ShowHideDeatils(True)
                    lblDetails.Text = "Order Details"
                    Label24.Text = "Issuer Order Remark"  'Added by Mohit/Rushi on 02-May-2016 FA-1420
                    pnlDetailsPopup.Visible = True
                    trStatus.Visible = True
                    trOrderType.Visible = True
                    trSpot.Visible = True
                    trExePrc1.Visible = True
                    trAvgExePrc.Visible = True
                    trQuoteStatus.Visible = False
                    '  grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Expiry_Date).Text
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    lblAlloRFQID.Text = ""
                    lclAlloCP.Text = ""
                    lblAlloClientPrice.Text = ""
                    lblAlloExpiDt.Text = ""
                    lblAlloKO.Text = ""
                    lblAlloMatuDt.Text = ""
                    lblAlloNoteCcy.Text = ""
                    lblAlloOrderSize.Text = ""
                    lblalloOrderType.Text = ""
                    lblAlloPrice.Text = ""
                    lblAlloRemark.Text = ""
                    lblAlloSettDt.Text = ""
                    lblAlloSettWk.Text = ""
                    lblAlloStrike.Text = ""
                    lblAlloSubmitteddBy.Text = ""
                    lblAlloTenor.Text = ""
                    lblAlloTradeDt.Text = ""
                    lblAlloUnderlying.Text = ""
                    lblAlloUpfront.Text = ""
                    lblAlloYield.Text = ""
                    lblValQuoteStatus.Text = ""
                    lblAlloOrderStatus.Text = ""
                    lblAlloExePrc1.Text = ""
                    lblAlloAvgExePrc.Text = ""
                    lblAlloSpot.Text = ""
                    lblValAlloSolvefor.Text = ""
                    ''</added by Rushikesh on 29-Dec-15 to flush old value>
                    lblAlloRFQID.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_QuoteRequestId).Text
                    lclAlloCP.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.PP_CODE).Text
                    lblAlloClientPrice.Text = SetNumberFormat(grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_Client_Price).Text, 2)
                    lblAlloExpiDt.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ExpiryDate).Text
                    'Mohit 28-Dec-2015
                    If grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Barrier).Text.Trim <> "" And grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Barrier).Text.Trim <> "&nbsp;" And grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Barrier).Text.Trim <> "0" Then
                        lblAlloKO.Text = SetNumberFormat(grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Barrier).Text, 2) + "&nbsp(" + grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Barrier_Type).Text + ")"
                    Else
                    End If
                    lblAlloMatuDt.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.MaturityDate).Text
                    lblAlloNoteCcy.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_CashCurrency).Text
                    lblAlloOrderSize.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Ordered_Qty).Text
                    lblalloOrderType.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ELN_Order_Type).Text
                    lblAlloPrice.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_OfferPrice).Text
                    lblAlloRemark.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_Order_Remark1).Text

                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_CaptureOrderComment", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                        Case "Y", "YES"
                            trOrderComment.Visible = True
                            lblAlloOrderComment.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_OrderComment).Text
                        Case "N", "NO"
                            trOrderComment.Visible = False
                            lblAlloOrderComment.Text = ""
                    End Select

                    lblAlloSettDt.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Settlement_Date).Text
                    lblAlloSettWk.Text = "T+" + grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Value).Text
                    ''lblAlloSpot.Text = item("").Text '' Need to discuss.
                    lblAlloStrike.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_StrikePercentage).Text
                    lblAlloSubmitteddBy.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.created_by).Text
                    lblAlloTenor.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_Tenor).Text + "&nbsp;Month(s)"
                    lblAlloTradeDt.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.TradeDate).Text
                    lblAlloUnderlying.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_UnderlyingCode).Text
                    lblAlloUpfront.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_Upfront).Text
                    lblAlloYield.Text = SetNumberFormat(grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_Client_Yield).Text, 2)
                    lblAlloOrderStatus.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Order_Status).Text
                    'lblAlloOrderStatus.Text = item("Order_Status").Text
                    lblAlloExePrc1.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_Execution_Price1).Text
                    lblAlloAvgExePrc.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_AveragePrice).Text
                    lblValAlloSolvefor.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.SolveFor).Text
                    If grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ELN_Order_Type).Text.ToUpper = "LIMIT" Then
                        lblAlloSpot.Visible = True
                        lblAlloSpot.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.LimitPrice1).Text
                    Else
                        lblAlloSpot.Visible = False
                    End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    upnlDetails.Update()
                    'Added by Mohit lalwani on 14-Mar-2016 
                ElseIf e.CommandName.ToUpper = "GENERATEDOCUMENT" Then
                    '' Generate_Deal(grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_QuoteRequestId).Text)    
                    '/Added by Mohit lalwani on 14-Mar-2016 



                End If
            End If
        Catch ex As Exception
            lblerror.Text = "grdOrder_ItemCommand:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdOrder_ItemCommand", ErrorLevel.High)
        End Try
    End Sub



    '/Mohit Lalwani on 22-Jan-2015
    Private Sub grdOrder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdOrder.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.EditItem Then
                If e.Item.Cells(grdOrderEnum.Ordered_Qty).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Ordered_Qty).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Ordered_Qty).Text, 0) '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                End If

                If e.Item.Cells(grdOrderEnum.Filled_Qty).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Filled_Qty).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Filled_Qty).Text, 0) ''EQBOSDEV-228 Added by chaitali removing decimalsss 'Changed by Mohit Lalwani on 20-aug-2015                        Case 2
                End If
                If e.Item.Cells(grdOrderEnum.EP_OfferPrice).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.EP_OfferPrice).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_OfferPrice).Text, 2)
                End If
                If e.Item.Cells(grdOrderEnum.EP_StrikePercentage).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.EP_StrikePercentage).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_StrikePercentage).Text, 2)
                End If
                If e.Item.Cells(grdOrderEnum.EP_AveragePrice).Text <> "&nbsp;" Then
                    '<AvinashG. on 11-Mar-2016: FA1363 Roundiing from 4->5 >
                    '' e.Item.Cells(grdOrderEnum.EP_AveragePrice).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_AveragePrice).Text, 4)
                    e.Item.Cells(grdOrderEnum.EP_AveragePrice).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_AveragePrice).Text, 5)
                    '<AvinashG. on 11-Mar-2016: FA1363 Roundiing from 4->5 >
                End If
                If e.Item.Cells(grdOrderEnum.EP_Notional_Amount1).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.EP_Notional_Amount1).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_Notional_Amount1).Text, 0)  ''EQBOSDEV-228  Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                End If

                If e.Item.Cells(grdOrderEnum.LimitPrice1).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.LimitPrice1).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.LimitPrice1).Text, 4) ''16April:Changed from 2 digit to 4 digit decimal,told by Kalyan
                End If

                If e.Item.Cells(grdOrderEnum.EP_Execution_Price1).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.EP_Execution_Price1).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_Execution_Price1).Text, 4) ''16April:Changed from 2 digit to 4 digit decimal,told by Kalyan
                End If
                If e.Item.Cells(grdOrderEnum.EP_Client_Price).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.EP_Client_Price).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_Client_Price).Text, 2)
                End If
                If e.Item.Cells(grdOrderEnum.EP_Client_Yield).Text <> "&nbsp;" Then
                    ''e.Item.Cells(grdOrderEnum.EP_Client_Yield).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_Client_Yield).Text, 2)'''DK
                    e.Item.Cells(grdOrderEnum.EP_Client_Yield).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_Client_Yield).Text, 4)
                End If
                If e.Item.Cells(grdOrderEnum.EP_RM_Margin).Text <> "&nbsp;" Then
                    ''e.Item.Cells(grdOrderEnum.EP_RM_Margin).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_RM_Margin).Text, 2)'''DK
                    e.Item.Cells(grdOrderEnum.EP_RM_Margin).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_RM_Margin).Text, 4)
                End If
            End If

        Catch ex As Exception
            lblerror.Text = "grdOrder_ItemDataBound:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdOrder_ItemDataBound", ErrorLevel.High)

        End Try
    End Sub
    Private Sub grdOrder_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdOrder.PageIndexChanged
        Dim dtGrid As New DataTable("Dummy")
        Try
            grdOrder.EditItemIndex = -1
            grdOrder.CurrentPageIndex = e.NewPageIndex
            dtGrid = CType(Session("Order"), DataTable)
            grdOrder.DataSource = dtGrid
            grdOrder.DataBind()
            grdOrder.Visible = True
        Catch ex As Exception
            lblerror.Text = "grdOrder_PageIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdOrder_PageIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub grdELNRFQ_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdELNRFQ.PageIndexChanged
        Dim dtGrid As New DataTable("Dummy")
        Try
            grdELNRFQ.EditItemIndex = -1
            fill_grid()
            grdELNRFQ.CurrentPageIndex = e.NewPageIndex
            dtGrid = CType(Session("ELN_RFQ"), DataTable)
            grdELNRFQ.DataSource = dtGrid
            grdELNRFQ.DataBind()
        Catch ex As Exception
            lblerror.Text = "grdELNRFQ_PageIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdELNRFQ_PageIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub grdELNRFQ_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdELNRFQ.SortCommand
        Try
            numberdiv = CType(ViewState("NumberForSort_" + e.SortExpression), Int32)
            numberdiv = numberdiv + 1
            ViewState("NumberForSort_" + e.SortExpression) = numberdiv
            If CType(Session("ELN_RFQ"), DataTable) Is Nothing Then Exit Sub
            Dim dtSortRevData As DataTable = CType(Session("ELN_RFQ"), DataTable)
            Dim dvRevData As DataView
            dvRevData = dtSortRevData.DefaultView
            If (numberdiv Mod 2) = 0 Then
                dvRevData.Sort = e.SortExpression & " DESC"
            Else
                dvRevData.Sort = e.SortExpression & " ASC"
            End If

            grdELNRFQ.DataSource = dvRevData
            grdELNRFQ.DataBind()
            upnlGrid.Update()
        Catch ex As Exception
            lblerror.Text = "grdELNRFQ_SortCommand:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdELNRFQ_SortCommand", ErrorLevel.High)

        End Try
    End Sub
    Private Sub grdOrder_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdOrder.SortCommand
        Try
            numberdiv = CType(ViewState("NumberForSort_" + e.SortExpression), Int32)
            numberdiv = numberdiv + 1
            ViewState("NumberForSort_" + e.SortExpression) = numberdiv
            If CType(Session("Order"), DataTable) Is Nothing Then Exit Sub
            Dim dtSortRevData As DataTable = CType(Session("Order"), DataTable)
            Dim dvRevData As DataView
            dvRevData = dtSortRevData.DefaultView
            If (numberdiv Mod 2) = 0 Then
                dvRevData.Sort = e.SortExpression & " DESC"
            Else
                dvRevData.Sort = e.SortExpression & " ASC"
            End If
            grdOrder.DataSource = dvRevData
            grdOrder.DataBind()
            grdOrder.Visible = True
        Catch ex As Exception
            lblerror.Text = "grdOrder_SortCommand:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdOrder_SortCommand", ErrorLevel.High)

        End Try
    End Sub
#End Region
#Region "Refresh Grid,Chart"

    Public Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            RestoreSolveAll()
            RestoreAll()
            CheckBestPrice()
            rbHistory_SelectedIndexChanged(Nothing, Nothing)
            If btnSolveAll.Visible = True Then
                If rblShareData.SelectedValue = "GRAPHDATA" Then
                    Call Fill_All_Charts()
                End If
            End If
            upnlGrid.Update()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnLoad_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnLoad_Click", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
#End Region
#Region "Price"

    Public Function Chk_validation() As Boolean
        Try
            '<AvinashG. on 17-Feb-2016: FA-1273 ELN/DRA/FCN/Acc/Dec: Invalid Tenor Issue >
            If (txtTenor.Text.Contains(".")) Then
                lblerror.ForeColor = Color.Red
                lblerror.Text = "Please enter valid tenor(e.g. 1, 2, 3)."
                Chk_validation = False
                Exit Function
            End If
            '</AvinashG. on 17-Feb-2016: FA-1273 ELN/DRA/FCN/Acc/Dec: Invalid Tenor Issue >
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            'If ddlShare.SelectedItem Is Nothing Then
            '    lblerror.Text = "Please select valid share. "
            '    Chk_validation = False
            '    Exit Function
            ''ElseIf ddlShare.SelectedItem.Value = "" Then
	    If ddlShare.SelectedValue = "" Then
                lblerror.Text = "Please select valid share."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If

            If ddlSolveFor.SelectedValue = "PricePercentage" Then
                If (txtStrike.Text = "" Or Val(txtStrike.Text) = 0 Or Val(txtStrike.Text.Replace(",", "")) > CDbl(objReadConfig.ReadConfig(dsConfig, "EQC_ELN_Allowed_Max_Strike", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "100").Trim.Replace(",", ""))) Then
                    lblerror.Text = "Please enter valid strike. "
                    Chk_validation = False
                    Exit Function
                Else
                    If Val(txtStrike.Text) < 0 Then
                        lblerror.Text = "Strike should be positive. "
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If

                End If
            Else
                If (txtELNPrice.Text = "" Or Val(txtELNPrice.Text) = 0 Or Val(txtELNPrice.Text.Replace(",", "")) > 100) Then
                    lblerror.Text = " Please enter valid IB Price. "
                    Chk_validation = False
                    Exit Function
                Else
                    If Val(txtELNPrice.Text) < 0 Then
                        lblerror.Text = "Price should be positive. "
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If
                End If
            End If
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'If Val(txtQuantity.Text) = 0 Then
            ' lblerror.Text = "Please enter valid notional."
            ' Chk_validation = False
            ' Exit Function
            'Else
            ' ''Accept notional
            ' Chk_validation = True
            'End If
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            ''<Dilkhush/Avinash/Rushi 19May2016 Handle unexpected characters>
            If Qty_Validate(txtQuantity.Text) = False Then
                Chk_validation = False
                Exit Function
            End If
            ''</Dilkhush/Avinash/Rushi 19May2016 Handle unexpected characters>

            If Val(txtValueDays.Text) < 7 Then
                lblerror.Text = "Settlement days should not be less than 7."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If
            If Val(txtTenor.Text) = 0 Then
                lblerror.Text = "Please enter valid tenor."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If
            ''<Nikhil M. on 17-Oct-2016: Added for for removing the COnfig>

            If Val(txtUpfrontELN.Text) > 0 Or Val(txtUpfrontELN.Text) < 100 Then
                Chk_validation = True
            Else
                lblerror.Text = "Please enter a valid Upfront."
                Chk_validation = False
                Exit Function
            End If


            ''<Nikhil M. on 17-Oct-2016: Commnetd for removing the COnfig>
            ' ''<Rushikesh D. on 23-May-2016 JIRAID:EQBOSDEV-371>
            'Dim strMaxUpfront As String
            'strMaxUpfront = objReadConfig.ReadConfig(dsConfig, "EQC_MaxUpfront", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "0").Trim.ToUpper
            'If Val(strMaxUpfront) > 0 Then
            '    If Val(txtUpfrontELN.Text.Replace(",", "")) > Val(strMaxUpfront) Then
            '        lblerror.Text = "Upfront should not be more than " + strMaxUpfront + "%."
            '        Chk_validation = False
            '        Exit Function
            '    Else
            '        Chk_validation = True
            '    End If
            'End If
            ' ''</Rushikesh D. on 23-May-2016 JIRAID:EQBOSDEV-371>

            ''<Nikhil M. on 17-Oct-2016: Added for removing the Config>

            If Val(txtUpfrontELN.Text) = 0 OrElse Val(txtUpfrontELN.Text.Replace(",", "")) >= 100 Then
                lblerror.Text = "Please enter valid upfront."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If

            ''AshwiniP on 09-Nov-2016
            If ValidateTenor() = False Then
                lblerror.Text = "Please enter valid tenor."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If

            
            If chkELNType.Checked = True Then
                If txtBarrier.Text <> "" Then
                    If CDbl(txtBarrier.Text.Replace(",", "")) > 999 Then
                        lblerror.Text = "Please enter valid KO % of Initial."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If


                    If Val(txtBarrier.Text.Replace(",", "")) = 0 Or Val(txtBarrier.Text.Replace(",", "")) <= 100 Then
                        lblerror.Text = "Please enter valid KO % of Initial."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If
                Else
                    lblerror.Text = "Please enter valid KO % of Initial."
                    Chk_validation = False
                    Exit Function
                End If
            Else
                Chk_validation = True
            End If
            If CDate(txtSettlementDate.Value) <= CDate(txtTradeDate.Value) Then
                lblerror.Text = "Please enter valid Settlement date."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If

            If CDate(txtExpiryDate.Value) <= CDate(txtSettlementDate.Value) Then
                lblerror.Text = "Please enter valid Expiry date."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If
            If CDate(txtMaturityDate.Value) <= CDate(txtExpiryDate.Value) Then
                lblerror.Text = "Please enter valid Maturity date."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If

            Dim Trade_Date As String = Today.Date.ToString("dd-MMM-yyyy")
            Dim BaseCcy As String = ""
            Dim QuantoCcy As String = ""
            BaseCcy = lblELNBaseCcy.Text

            If chkQuantoCcy.Checked = False Then
                QuantoCcy = lblELNBaseCcy.Text
            Else
                QuantoCcy = ddlQuantoCcy.SelectedValue
            End If

            If objELNRFQ.Web_CheckHolidays(Trade_Date, txtSettlementDate.Value, BaseCcy, QuantoCcy, BaseCcy, "VALUEDATE") = True Then
                Chk_validation = True
            Else
                lblerror.Text = "Settlement Date is holiday."
                Chk_validation = False
                Exit Function
            End If

            If objELNRFQ.Web_CheckHolidays(Trade_Date, txtExpiryDate.Value, BaseCcy, QuantoCcy, BaseCcy, "EXPIRYDATE") = True Then
                Chk_validation = True
            Else
                lblerror.Text = "Expiry Date is holiday."
                Chk_validation = False
                Exit Function
            End If

            If objELNRFQ.Web_CheckHolidays(Trade_Date, txtMaturityDate.Value, BaseCcy, QuantoCcy, BaseCcy, "MATURITYDATE") = True Then
                Chk_validation = True
            Else
                lblerror.Text = "Maturity Date is holiday."
                Chk_validation = False
                Exit Function
            End If
	''<Added by Rushikesh As told by Sanchita on 5Nov16>
            Dim sMaxSettDtChange As String = objReadConfig.ReadConfig(dsConfig, "EQC_MaxSettDateChange", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "10")

            If hdnSettDateManualChangeYN.Value = "Y" And objELNRFQ.Web_CheckMaxSettDateChange(Trade_Date, txtSettlementDate.Value, QuantoCcy, sMaxSettDtChange).ToUpper = "FALSE" Then
                lblerror.ForeColor = Color.Blue
                lblerror.Text = "Difference between trade date and settlement date should not be greater than " + sMaxSettDtChange + " business days"
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If
	''</Added by Rushikesh As told by Sanchita on 5Nov16>
        Catch ex As Exception
            lblerror.Text = "Chk_validation:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Chk_validation", ErrorLevel.High)
            Throw ex

        End Try
    End Function

    Public Function CheckGUVsTenor(ByVal TenorType As String, ByVal TenorValue As String, ByVal GUType As String, ByVal GUValue As String) As Boolean
        Dim intTenorDays As Long = 0
        Dim intGuranteedDays As Long = 0
        Dim ToDate As Date
        Try
            Select Case TenorType.ToUpper
                Case "MONTH"
                    ToDate = DateAdd(DateInterval.Month, Val(TenorValue), Date.Today)
                Case "WEEK"
                    ToDate = DateAdd(DateInterval.Day, (Val(TenorValue) * 7), Date.Today)
                Case "DAY"
                    ToDate = DateAdd(DateInterval.Day, Val(TenorValue), Date.Today)
                Case "YEAR"
                    ToDate = DateAdd(DateInterval.Year, Val(TenorValue), Date.Today)
            End Select

            intTenorDays = DateDiff(DateInterval.Day, Date.Today, ToDate)

            Select Case GUType.ToUpper
                Case "MONTH"
                    ToDate = DateAdd(DateInterval.Month, Val(GUValue), Date.Today)
                Case "WEEK"
                    ToDate = DateAdd(DateInterval.Day, (Val(GUValue) * 7), Date.Today)
                Case "DAY"
                    ToDate = DateAdd(DateInterval.Day, Val(GUValue), Date.Today)
                Case "YEAR"
                    ToDate = DateAdd(DateInterval.Year, Val(GUValue), Date.Today)
            End Select

            intGuranteedDays = DateDiff(DateInterval.Day, Date.Today, ToDate)
            If intGuranteedDays >= intTenorDays Then
                Return False
            End If
            Return True
        Catch ex As Exception
            lblerror.Text = "CheckGUVsTenor:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "CheckGUVsTenor", ErrorLevel.High)
            Throw ex
        End Try
    End Function

    Public Sub solveRFQ(ByVal PP_CODE As String, ByVal lblTimer As Label, ByVal btnDeal As Button, ByVal btnPrice As Button, ByRef lblPrice As Label)
        Dim PP_ID As String = ""
        Dim dtPriceProvider As DataTable
        Dim dr() As DataRow
        Dim strJavaScripthdnSolveSingleRequest As New StringBuilder
        Try
            dtPriceProvider = New DataTable("Price Provider")
            dtPriceProvider = CType(Session("Provide_Id"), DataTable)
            dr = dtPriceProvider.Select("PP_CODE = '" & PP_CODE & "' ")
            PP_ID = dr(0).Item("PP_ID").ToString
            '<AvinashG. on 16-Feb-2016: Optimization>

            If FillRFQDataTable(PP_ID) = True Then   ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
            Else
                Exit Sub
            End If
            'Get_RFQData_TOXML(PP_ID)
            '<AvinashG. on 16-Feb-2016: Optimization>
            Quote_ID = Convert.ToString(Session("Quote_ID"))
            lblerror.Text = "RFQ " & Quote_ID & "  generated."
            lblerror.ForeColor = Color.Blue
            lblmsgPriceProvider.Text = ""
            If (PP_CODE = "JPM") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('JPMwait').style.visibility = 'visible';")
                Session.Add("JPMQuote", Quote_ID)
            ElseIf (PP_CODE = "HSBC") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('HSBCwait').style.visibility = 'visible';")
                Session.Add("HSBCQuote", Quote_ID)
            ElseIf (PP_CODE = "CS") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('CSwait').style.visibility = 'visible';")
                Session.Add("CSQuote", Quote_ID)
            ElseIf (PP_CODE = "UBS") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('UBSwait').style.visibility = 'visible';")
                Session.Add("UBSQuote", Quote_ID)
            ElseIf (PP_CODE = "BAML") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('BAMLwait').style.visibility = 'visible';")
                Session.Add("BAMLQuote", Quote_ID)
            ElseIf (PP_CODE = "BNPP") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('BNPPwait').style.visibility = 'visible';")
                Session.Add("BNPPQuote", Quote_ID)
                'ElseIf (PP_CODE = "DBIB") Then ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
            ElseIf (PP_CODE = "DB") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('DBIBwait').style.visibility = 'visible';")
                Session.Add("DBIBQuote", Quote_ID)
            ElseIf (PP_CODE = "OCBC") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('OCBCwait').style.visibility = 'visible';")
                Session.Add("OCBCQuote", Quote_ID)
	     ElseIf (PP_CODE = "CITI") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('CITIwait').style.visibility = 'visible';")
                Session.Add("CITIQuote", Quote_ID)
            ElseIf (PP_CODE = "LEONTEQ") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('LEONTEQwait').style.visibility = 'visible';")
                Session.Add("LEONTEQQuote", Quote_ID)
            ElseIf (PP_CODE = "COMMERZ") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('COMMERZwait').style.visibility = 'visible';")
                Session.Add("COMMERZQuote", Quote_ID)
            End If
            If ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE" Then
                strJavaScripthdnSolveSingleRequest.AppendLine("getPrice('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');")
            Else
                strJavaScripthdnSolveSingleRequest.AppendLine("getStrike('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');")
            End If
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScripthdnSolveSingleRequest", "try {" + strJavaScripthdnSolveSingleRequest.ToString + "} catch(e) {}", True)
            grdELNRFQ.SelectedItemStyle.BackColor = Color.White

        Catch ex As Exception
            lblerror.Text = "solveRFQ:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "solveRFQ", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub btnhdnSolveSingleRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnhdnSolveSingleRequest.Click
        Try
            If txtTradeDate.Value = "" Or txtMaturityDate.Value = "" Or txtExpiryDate.Value = "" Or txtSettlementDate.Value = "" Then
                lblerror.Text = "Please enter valid date."
                Session.Remove("hdnPP")
                Exit Sub
            End If
            Session.Remove("flag")
            Session("flag") = ""
            setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice()
            rbHistory.SelectedValue = "Quote History"
            fill_grid()                             '<RiddhiS. on 16-Oct-2016: To shift to Quote History on Single Price>
            makeThisGridVisible(grdELNRFQ)


            Dim strTradeDate As String = txtTradeDate.Value
            Session.Add("TradeDAte", strTradeDate)
            If Chk_validation() = False Then
                Exit Sub
            End If
            If Session("hdnPP").ToString = "JPM" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblJPMPrice.Text = ""
                lblJPMlimit.Text = ""   'Mohit Lalwani on 1-Feb-2016
                lblerror.Text = ""
                JpmHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("JPM", lblTimer, btnJPMDeal, btnJPMprice, lblJPMPrice)
            ElseIf Session("hdnPP").ToString = "HSBC" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblHSBCPrice.Text = ""
                lblHSBClimit.Text = "" 'Mohit Lalwani on 1-Feb-2016
                lblerror.Text = ""
                HsbcHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("HSBC", lblTimerHSBC, btnHSBCDeal, btnHSBCPrice, lblHSBCPrice)
            ElseIf Session("hdnPP").ToString = "CS" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblCSPrice.Text = ""
                lblCSLimit.Text = ""
                lblerror.Text = ""
                CsHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("CS", lblTimerCS, btnCSDeal, btnCSPrice, lblCSPrice)
            ElseIf Session("hdnPP").ToString = "UBS" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblUBSPrice.Text = ""
                lblUBSlimit.Text = ""
                lblerror.Text = ""
                UbsHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("UBS", lblUBSTimer, btnUBSDeal, btnUBSPrice, lblUBSPrice)
            ElseIf Session("hdnPP").ToString = "BNPP" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblBNPPPrice.Text = ""
                lblBNPPlimit.Text = "" 'Mohit Lalwani on 1-Feb-2016
                lblerror.Text = ""
                BNPPHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("BNPP", lblTimerBNPP, btnBNPPDeal, btnBNPPPrice, lblBNPPPrice)
            ElseIf Session("hdnPP").ToString = "BAML" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblerror.Text = ""
                lblBAMLPrice.Text = ""
                lblBAMLlimit.Text = "" 'Mohit Lalwani on 1-Feb-2016
                BAMLHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("BAML", lblTimerBAML, btnBAMLDeal, btnBAMLPrice, lblBAMLPrice)
                'ElseIf Session("hdnPP").ToString = "DBIB" Then ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
            ElseIf Session("hdnPP").ToString = "DB" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblDBIBLimit.Text = "" 'Mohit Lalwani on 1-Feb-2016
                lblerror.Text = ""
                lblDBIBPrice.Text = ""
                DBIBHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                'solveRFQ("DBIB", lblTimerDBIB, btnDBIBDeal, btnDBIBPrice, lblDBIBPrice) ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                solveRFQ("DB", lblTimerDBIB, btnDBIBDeal, btnDBIBPrice, lblDBIBPrice)
	 ElseIf Session("hdnPP").ToString = "OCBC" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblDBIBLimit.Text = "" 'Mohit Lalwani on 1-Feb-2016
                lblOCBCPrice.Text = ""
                lblerror.Text = ""
                OCBCHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("OCBC", lblTimerOCBC, btnOCBCDeal, btnOCBCPrice, lblOCBCPrice)
	 ElseIf Session("hdnPP").ToString = "CITI" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblCITIPrice.Text = ""
                lblCITIlimit.Text = "" 'Mohit Lalwani on 1-Feb-2016
                lblerror.Text = ""
                CITIHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("CITI", lblTimerCITI, btnCITIDeal, btnCITIprice, lblCITIPrice)
            ElseIf Session("hdnPP").ToString = "LEONTEQ" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblLEONTEQPrice.Text = ""
                lblLEONTEQlimit.Text = "" 'Mohit Lalwani on 1-Feb-2016
                lblerror.Text = ""
                LEONTEQHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("LEONTEQ", lblTimerLEONTEQ, btnLEONTEQDeal, btnLEONTEQprice, lblLEONTEQPrice)
            ElseIf Session("hdnPP").ToString = "COMMERZ" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblCOMMERZPrice.Text = ""
                lblCOMMERZlimit.Text = "" 'Mohit Lalwani on 1-Feb-2016
                lblerror.Text = ""
                COMMERZHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("COMMERZ", lblTimerCOMMERZ, btnCOMMERZDeal, btnCOMMERZprice, lblCOMMERZPrice)
            End If
            ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    btnLoad_Click(sender, e)
                Case "N", "NO"
                    ''Do Nothing
            End Select
            ''btnLoad_Click(sender, e)
            ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
            Session.Remove("hdnPP")
            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Fill_All_Charts()
            End If
        Catch ex As Exception
            lblerror.Text = "btnhdnSolveSingleRequest_Click:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnhdnSolveSingleRequest_Click", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub btnJPMprice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJPMprice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnJPMprice.Text <> "Order" Then
                ReCalcDate()
                Session.Add("hdnPP", "JPM")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                'If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("JPM") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("JPM")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                        ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                        GetBestPriceConfirm(JpmHiddenPrice.Value)
                        ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                    Else
                        btnJPMDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                btnJPMprice.Text = "Order"
                End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnJPMprice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnJPMprice_Click", ErrorLevel.High)
        End Try
    End Sub


    Public Sub btnHSBCPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHSBCPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnHSBCPrice.Text <> "Order" Then
                ReCalcDate()
                Session.Add("hdnPP", "HSBC")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                'If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("HSBC") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("HSBC")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                        ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                        GetBestPriceConfirm(HsbcHiddenPrice.Value)
                        ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                    Else
                        btnHSBCDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                btnHSBCPrice.Text = "Order"
                End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnHSBCPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnHSBCPrice_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnOCBCPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOCBCPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnOCBCPrice.Text <> "Order" Then
                ReCalcDate()
                Session.Add("hdnPP", "OCBC")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("OCBC") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("OCBC")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                        ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                        GetBestPriceConfirm(OCBCHiddenPrice.Value)
                        ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                    Else
                        btnOCBCDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                btnOCBCPrice.Text = "Order"
                End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnOCBCPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnOCBCPrice_Click", ErrorLevel.High)
        End Try
    End Sub
   Public Sub btnCITIPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCITIPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnCITIPrice.Text <> "Order" Then
                ReCalcDate()
                Session.Add("hdnPP", "CITI")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check> 
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("CITI") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("CITI")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                        ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                        GetBestPriceConfirm(CITIHiddenPrice.Value)
                        ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                    Else
                        btnCITIDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                btnCITIPrice.Text = "Order"
                End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnCITIPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCITIPrice_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnLEONTEQPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLEONTEQprice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnLEONTEQPrice.Text <> "Order" Then
                ReCalcDate()
                Session.Add("hdnPP", "LEONTEQ")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check> 
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("LEONTEQ") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("LEONTEQ")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                        ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                        GetBestPriceConfirm(LEONTEQHiddenPrice.Value)
                        ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                    Else
                        btnLEONTEQDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                btnLEONTEQPrice.Text = "Order"
            End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnLEONTEQPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnLEONTEQPrice_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnCOMMERZPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCOMMERZprice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnCOMMERZPrice.Text <> "Order" Then
                ReCalcDate()
                Session.Add("hdnPP", "COMMERZ")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check> 
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("COMMERZ") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("COMMERZ")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                        ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                        GetBestPriceConfirm(COMMERZHiddenPrice.Value)
                        ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                    Else
                        btnCOMMERZDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                btnCOMMERZPrice.Text = "Order"
            End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnCOMMERZPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCOMMERZPrice_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnCSPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCSPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnCSPrice.Text <> "Order" Then
                If tabContainer.ActiveTabIndex = 0 Then
                    ReCalcDate()
                End If
                Session.Add("hdnPP", "CS")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                '' If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("CS") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("CS")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                        ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                        GetBestPriceConfirm(CsHiddenPrice.Value)
                        ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                    Else
                        btnCSDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                btnCSPrice.Text = "Order"
                End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnCSPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCSPrice_Click", ErrorLevel.High)
        End Try
    End Sub

    Public Sub btnDBIBPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDBIBPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnDBIBPrice.Text <> "Order" Then
                If tabContainer.ActiveTabIndex = 0 Then
                    ReCalcDate()
                End If
                'Session.Add("hdnPP", "DBIB") ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                Session.Add("hdnPP", "DB")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                ''If checkIssuerLimit("DBIB") Then ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                If checkIssuerLimit("DB") Then
                    If 1 = 1 Then
                        'Set_Order_Pop_Up_Items("DBIB") ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                        Set_Order_Pop_Up_Items("DB")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                        ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                        GetBestPriceConfirm(DBIBHiddenPrice.Value)
                        ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                    Else
                        btnDBIBDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                
                btnDBIBPrice.Text = "Order"
            End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnDBIBPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnDBIBPrice_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnUBSPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUBSPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnUBSPrice.Text <> "Order" Then
                If tabContainer.ActiveTabIndex = 0 Then
                    ReCalcDate()
                End If
                Session.Add("hdnPP", "UBS")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("UBS") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("UBS")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                        ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                        GetBestPriceConfirm(UbsHiddenPrice.Value)
                        ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                    Else
                        btnUBSDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
              
                btnUBSPrice.Text = "Order"
                End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnUBSPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnUBSPrice_Click", ErrorLevel.High)
        End Try
    End Sub

    Public Function setRedirectedConfirmOrderPopUp(ByVal RedirectedOrderId As String) As Boolean

        'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowHongKongForOrderPlacement", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
        '    Case "Y", "YES"
        '        ddlBookingBranchPopUpValue.Enabled = True
        '    Case "N", "NO"
        '        ddlBookingBranchPopUpValue.Enabled = False
        'End Select

        Dim dtROrderDetails As DataTable
        dtROrderDetails = New DataTable("ROrderDetailsOrder")


        If Not IsNothing(Request.QueryString("RedirectedOrderId")) Then
            If RedirectedOrderId.Trim <> "" Then
                lblIssuerPopUpCaption.Text = "Issuer"
                objELNRFQ.Get_ELN_Redirected_Order_Details(RedirectedOrderId, dtROrderDetails)
                ddlBookingBranchPopUpValue.SelectedValue = dtROrderDetails.Rows(0).Item("EP_Deal_Booking_Branch").ToString
                '<Added by Mohit Lalwani on 3-Nov-2015>
                'txtUpfrontPopUpValue.Text = dtROrderDetails.Rows(0).Item("EP_RM_Margin").ToString
                txtUpfrontPopUpValue.Text = txtUpfrontELN.Text
                '</Added by Mohit Lalwani on 3-Nov-2015>
                ddlOrderTypePopUpValue.SelectedValue = dtROrderDetails.Rows(0).Item("EP_OrderType").ToString
                If ddlOrderTypePopUpValue.SelectedValue = "Limit" Then
                    txtLimitPricePopUpValue.Enabled = True
                Else
                    txtLimitPricePopUpValue.Enabled = False
                End If
                txtLimitPricePopUpValue.Text = dtROrderDetails.Rows(0).Item("ER_LimitPrice1").ToString

            End If
        End If



    End Function
    Public Function Set_Order_Pop_Up_Items(ByVal Issuer As String) As Boolean

        Dim notBand As String
        Try
            'AvinashG. on 09-Jul-2016
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False

            ''AshwiniP on 23Sept16 
            Session.Remove("dtELNPreTradeAllocation")
            Dim tempDt As DataTable
            tempDt = New DataTable("tempDt")
            tempDt.Columns.Add("RM_Name", GetType(String))
            tempDt.Columns.Add("Account_Number", GetType(String))
            tempDt.Columns.Add("AlloNotional", GetType(String))
            tempDt.Columns.Add("Cust_ID", GetType(String))
            tempDt.Columns.Add("DocId", GetType(String))
            tempDt.Columns.Add("EPOF_OrderId", GetType(String))
            tempDt.Rows.InsertAt(tempDt.NewRow(), 0)
            Session.Add("dtELNPreTradeAllocation", tempDt)
            grdRMData.DataSource = tempDt
            grdRMData.DataBind()
            For Each row As GridViewRow In grdRMData.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = True
                End If
                OnCheckedChanged(CType(grdRMData.Rows((0)).Cells(0).FindControl("CheckBox1"), CheckBox), Nothing)

            Next
            ''grdRMData.Visible = False


            lblIssuerPopUpValue.Text = Issuer
            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowHongKongForOrderPlacement", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            '    Case "Y", "YES"
            '        ddlBookingBranchPopUpValue.Enabled = True
            '    Case "N", "NO"
            '        ddlBookingBranchPopUpValue.Enabled = False
            'End Select

            ''Start Added By Nikhil M 01Aug16 For Booking Brach DropDown EQSCB-16
            ddlBookingBranchPopUpValue.Items.Clear() ''<Nikhil M. on 20-Sep-2016: Added For Clearing Dropdown>
            Dim DtCommanData As New DataTable("DtCommanData")

            Dim strQuerySelect As String = "entity_id = '" + LoginInfoGV.Login_Info.EntityID.ToString + "'"
            Select Case ObjCommanData.DB_Get_Common_Data("EQC_SCB_BookingCenters", DtCommanData)  '<RiddhiS. on 14-Oct-2016: Config name changed>
                Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                    If DtCommanData.Rows.Count > 0 Then
                        If DtCommanData.Select(strQuerySelect).Length > 0 Then
                            ddlBookingBranchPopUpValue.DataSource = DtCommanData.Select(strQuerySelect, "Data_Value ASC").CopyToDataTable
                            ddlBookingBranchPopUpValue.DataValueField = "Misc1"
                            ddlBookingBranchPopUpValue.DataTextField = "Data_Value"
                            ddlBookingBranchPopUpValue.DataBind()
                        Else
                            SetDllBookingBranch()
                        End If
                    Else
                        SetDllBookingBranch()
                    End If
                Case Web_CommonFunction.Database_Transaction_Response.DB_Unsuccessful
                    SetDllBookingBranch()
            End Select
            '' End Added By Nikhil M 01Aug16 For Booking Brach DropDown EQSCB-16

            lblIssuerPopUpCaption.Text = "Issuer"
            ddlBasketSharesPopValue.Visible = False
            txtLimitPricePopUpValue.Width = New WebControls.Unit(175)
            If chkELNType.Checked Then
                '<AvinashG on 11-Dec-2015  EQBOSDEV-174 - New UI for Order Confirmation Popup >
                'lblProductNamePopUpValue.Text = "(Barrier ELN)"
                lblProductNamePopUpValue.Text = "Barrier Equity Linked Note."
            Else
                'lblProductNamePopUpValue.Text = "(Simple ELN)"
                lblProductNamePopUpValue.Text = "Simple Equity Linked Note."
                '</AvinashG on 11-Dec-2015  EQBOSDEV-174 - New UI for Order Confirmation Popup >
            End If
            lblNotionalPopUpCaption.Text = "Notional"
            lblUpfrontPopUpCaption.Text = "Upfront (%)"
            txtUpfrontPopUpValue.Enabled = True
            lblIssuerPricePopUpCaption.Text = "IB Price (%)"
            lblClientPricePopUpCaption.Text = "Client Price (%)"
            lblClientYieldPopUpCaption.Text = "Client Yield (%)p.a."
            ddlOrderTypePopUpValue.SelectedIndex = 0
            txtLimitPricePopUpValue.Text = "0"
            txtLimitPricePopUpValue.Enabled = False
            lblUnderlyingPopUpValue.Text = ddlShare.SelectedValue.ToString
''            lblNotionalPopUpValue.Text = txtQuantity.Text
            lblNotionalPopUpValue.Text = SetNumberFormat(txtQuantity.Text, 0) ''EQBOSDEV-228 Added by chaitali removing decimal
            lblTotalAmtVal.Text = lblNotionalPopUpValue.Text ''AshwiniP 23Sep2016
            lblRemainNotionalVal.Text = lblTotalAmtVal.Text  ''AshwiniP 26Sep2016
            txtUpfrontPopUpValue.Text = txtUpfrontELN.Text
            If chkELNType.Checked Then
                lblKOPopUpValue.Text = txtBarrier.Text
                lblKOTypePopUpValue.Text = ddlBarrier.SelectedItem.Text
            Else
                lblKOPopUpValue.Text = ""
                lblKOTypePopUpValue.Text = ""
            End If
            tdStrikeCaption.Visible = True
            tdStrikeValue.Visible = True
            tdTenorPopUpCaption.Visible = True
            tdTenorPopUpValue.Visible = True
            lblTenorPopUpValue.Text = txtTenor.Text
            lblTenorTypePopUpValue.Text = ddlTenorTypeELN.SelectedItem.Text
            OptionalTRAccDeccum.Visible = False
            tdKICaption.Visible = False
            tdKIValue.Visible = False




            If ddlSolveFor.SelectedValue.Trim.ToUpper.Contains("STRIKEPERCENTAGE") Then
                Select Case UCase(Issuer)
                    Case "JPM"
                        lblStrikePopUpValue.Text = lblJPMPrice.Text
                    Case "CS"
                        lblStrikePopUpValue.Text = lblCSPrice.Text
                    Case "UBS"
                        lblStrikePopUpValue.Text = lblUBSPrice.Text
                    Case "HSBC"
                        lblStrikePopUpValue.Text = lblHSBCPrice.Text
                    Case "BAML"
                        lblStrikePopUpValue.Text = lblBAMLPrice.Text
                    Case "BNPP"
                        lblStrikePopUpValue.Text = lblBNPPPrice.Text
                        'Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                    Case "DB"
                        lblStrikePopUpValue.Text = lblDBIBPrice.Text
                    Case "OCBC"
                        lblStrikePopUpValue.Text = lblOCBCPrice.Text
		    Case "CITI"
                        lblStrikePopUpValue.Text = lblCITIPrice.Text
                    Case "LEONTEQ"
                        lblStrikePopUpValue.Text = lblLEONTEQPrice.Text
                    Case "COMMERZ"
                        lblStrikePopUpValue.Text = lblCOMMERZPrice.Text
                End Select
            Else
                lblStrikePopUpValue.Text = txtStrike.Text
            End If
            lblCurrencyPopUpValue.Text = "(" + IIf(chkQuantoCcy.Checked, ddlQuantoCcy.SelectedItem.Text, lblELNBaseCcy.Text).ToString + ")"
            Select Case UCase(Issuer)
                Case "JPM"
                    lblIssuerPricePopUpValue.Text = lblJPMPrice.Text
                    lblClientPricePopUpValue.Text = lblJPMClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblJPMClientYield.Text
                Case "CS"
                    lblIssuerPricePopUpValue.Text = lblCSPrice.Text
                    lblClientPricePopUpValue.Text = lblCSClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblCSClientYield.Text
                Case "UBS"
                    lblIssuerPricePopUpValue.Text = lblUBSPrice.Text
                    lblClientPricePopUpValue.Text = lblUBSClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblUBSClientYield.Text
                Case "HSBC"
                    lblIssuerPricePopUpValue.Text = lblHSBCPrice.Text
                    lblClientPricePopUpValue.Text = lblHSBCClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblHSBCClientYield.Text
                Case "BAML"
                    lblIssuerPricePopUpValue.Text = lblBAMLPrice.Text
                    lblClientPricePopUpValue.Text = lblBAMLClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblBAMLClientYield.Text
                Case "BNPP"
                    lblIssuerPricePopUpValue.Text = lblBNPPPrice.Text
                    lblClientPricePopUpValue.Text = lblBNPPClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblBNPPClientYield.Text
                    'Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                Case "DB"
                    lblIssuerPricePopUpValue.Text = lblDBIBPrice.Text
                    lblClientPricePopUpValue.Text = lblDBIBClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblDBIBClientYield.Text
                Case "OCBC"
                    lblIssuerPricePopUpValue.Text = lblOCBCPrice.Text
                    lblClientPricePopUpValue.Text = lblOCBCClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblOCBCClientYield.Text
                Case "CITI"
                    lblIssuerPricePopUpValue.Text = lblCITIPrice.Text
                    lblClientPricePopUpValue.Text = lblCITIClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblCITIClientYield.Text
                Case "LEONTEQ"
                    lblIssuerPricePopUpValue.Text = lblLEONTEQPrice.Text
                    lblClientPricePopUpValue.Text = lblLEONTEQClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblLEONTEQClientYield.Text
                Case "COMMERZ"
                    lblIssuerPricePopUpValue.Text = lblCOMMERZPrice.Text
                    lblClientPricePopUpValue.Text = lblCOMMERZClientPrice.Text
                    txtClientYieldPopUpValue.Text = lblCOMMERZClientYield.Text
            End Select
            If ddlSolveFor.SelectedValue.Trim.ToUpper.Contains("STRIKEPERCENTAGE") Then
                lblIssuerPricePopUpValue.Text = txtELNPrice.Text
            End If



            '<Riddhi S to 01-Aug-2016 for Notional Limit Check>
            'oWEBMarketData.GetNotionalLimitCheck(Convert.ToDouble(txtQuantity.Text), LoginInfoGV.Login_Info.EntityID.ToString, "ELN", ddlQuantoCcy.SelectedValue.ToString, notBand)

            'If notBand.Equals("MEDIUM") Then
            '    lblNtnlSoftBlock.Visible = True
            '    lblNtnlHardBlock.Visible = False
            'ElseIf notBand.Equals("HIGH") Then
            '    lblNtnlHardBlock.Visible = True
            '    lblNtnlSoftBlock.Visible = False
            'Else
            '    lblNtnlSoftBlock.Visible = False
            '    lblNtnlHardBlock.Visible = False
            'End If


            lblerrorPopUp.Text = ""

            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False

            ' ''<Nikhil M. on 20-Oct-2016: Added for Hide/Visible the Allocation fgrid>
            If IsNothing(Request.QueryString("PoolID")) Then
                grdRMData.Visible = True
                btnAddAllocation.Visible = True
                tblRw1.Visible = True
                tblRw2.Visible = True
                tblRw3.Visible = True
                UPanle11111.Update()
            End If
            ''<Nikhil M. on 17-Oct-2016: Added for hidning the COntrol on hedge>
      
            If Not IsNothing(Request.QueryString("RedirectedOrderId")) Then
                Dim sROrderID As String
                sROrderID = Request.QueryString("RedirectedOrderId")
                If sROrderID.Trim <> "" Then

                    setRedirectedConfirmOrderPopUp(sROrderID)

                End If
            End If

            Return True
        Catch ex As Exception
            lblerror.Text = "set_Order_Pop_Up_Items:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "set_Order_Pop_UP_Items", ErrorLevel.High)
            Throw ex
            Return False
        End Try

    End Function
    ''Added By nikhil M on  01Aug16 EQSCB-16
    Public Function SetDllBookingBranch() As Boolean
        ddlBookingBranchPopUpValue.Items.Add(New DropDownListItem("Hong Kong", "HK"))
        ddlBookingBranchPopUpValue.Items.Add(New DropDownListItem("Singapore", "SG"))
        ddlBookingBranchPopUpValue.SelectedIndex = 0 ''Change done by PB: 05Nov2016 - On load default booking center should be blank
    End Function

    Public Sub btnBNPPPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBNPPPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnBNPPPrice.Text <> "Order" Then
                If tabContainer.ActiveTabIndex = 0 Then
                    ReCalcDate()
                End If
                Session.Add("hdnPP", "BNPP")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("BNPP") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("BNPP")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                    Else
                        btnBNPPDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>

                ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                GetBestPriceConfirm(BNPPHiddenPrice.Value)
                ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>

                btnBNPPPrice.Text = "Order"
            End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()

            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If

        Catch ex As Exception
            lblerror.Text = "btnBNPPPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnBNPPPrice_Click", ErrorLevel.High)
        End Try
    End Sub

    Public Sub btnBAMLPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBAMLPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            'chkConfirmDeal.Visible = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            'chkConfirmDeal.Checked = False ''<Nikhil M. on 08-Sep-2016: Added For Deal Conformation >
            ''<Start : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            drpConfirmDeal.Items.Clear()
            drpConfirmDeal.ClearSelection()
            ''<End : Nikhil M. on 16-Sep-2016: Add For Dropdown>
            If btnBAMLPrice.Text <> "Order" Then
                If tabContainer.ActiveTabIndex = 0 Then
                    ReCalcDate()
                End If
                Session.Add("hdnPP", "BAML")
                btnhdnSolveSingleRequest_Click(sender, e)

            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtQuantity.Text = "0.00" Or txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtQuantity.Text = "" Or txtQuantity.Text = "&nbsp;" Or Val(txtQuantity.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Notional Size can not be 0."
                    Exit Sub
                End If
                If checkIssuerLimit("BAML") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("BAML")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
                    Else
                        btnBAMLDeal_Click(sender, e)
                    End If
                End If
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>

                ''<Start | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>
                GetBestPriceConfirm(BAMLHiddenPrice.Value)
                ''<End | Nikhil M. on 20-Sep-2016: Added for confirm Deal Reason>

                btnBAMLPrice.Text = "Order"
                End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnBAMLPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnBAMLPrice_Click", ErrorLevel.High)

        End Try
    End Sub

    Public Sub Solve_All_Requests(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnhdnSolveAllRequests.Click
        Dim all_RFQ_IDs As String = String.Empty
        Dim dtUpdate As DataTable
        Dim strQuoteId1 As String = String.Empty
        Dim dtPriceProvider As DataTable
        Dim strJavaScriptAllRequest As New StringBuilder
        Try
            If Chk_validation() = False Then
                Exit Sub
            End If
            ResetMinMaxNotional() 'Mohit Lalwani on 1-Feb-2016
            strJavaScriptAllRequest.AppendLine("document.getElementById('PriceAllWait').style.visibility = 'visible';")
            getAllId = New Hashtable
            getPPId = New Hashtable
            clearFields()
            flag = "I"
            Session.Add("flag", flag)
            dtUpdate = New DataTable("Dummy")
            rbHistory.SelectedValue = "Quote History"
            Dim strTradeDate As String = txtTradeDate.Value
            Session.Add("TradeDAte", strTradeDate)
            Dim dtLogin As DataTable
            Dim dr As DataRow()
            Dim dr1 As DataRow()
            dtLogin = CType(Session("PP_Login"), DataTable)
            dtPriceProvider = New DataTable("Price Provider")
            dtPriceProvider = CType(Session("Provide_Id"), DataTable)
            If dtLogin.Rows.Count > 0 Then
                dr = dtLogin.Select("PP_CODE = '" & "JPM" & "' ")
                If dr.Length > 0 Then
                    If btnJPMprice.Visible = True Then
                        If btnJPMprice.Enabled = True Then
                            'If chkJPM.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "JPM" & "' ")
                                JPM_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(JPM_ID) = True Then                 ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                                'Get_RFQData_TOXML(JPM_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))

                                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('JPMwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblJPMPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "','" + btnJPMprice.ClientID + "');")
                                    JpmHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('JPMwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblJPMPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "','" + btnJPMprice.ClientID + "');")
                                    JpmHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If

                                strQuoteId1 = all_RFQ_IDs
                                getPPId.Add("JPM", JPM_ID)
                                getAllId.Add(JPM_ID, strQuoteId1)
                            'End If ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                        End If
                    End If
                End If


                dr = dtLogin.Select("PP_CODE = '" & "CS" & "' ")
                If dr.Length > 0 Then
                    If btnCSPrice.Visible = True Then
                        If btnCSPrice.Enabled = True Then
                            'If chkCS.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "CS" & "' ")
                                CS_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(CS_ID) = True Then       ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                                'Get_RFQData_TOXML(CS_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('CSwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCSPrice.ClientID + "','" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "','" + btnCSPrice.ClientID + "');")
                                    CsHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('CSwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCSPrice.ClientID + "','" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "','" + btnCSPrice.ClientID + "');")
                                    CsHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("CS", CS_ID)
                                getAllId.Add(CS_ID, Convert.ToString(Session("Quote_ID")))
                            'End If''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                        End If
                    End If
                End If

                dr = dtLogin.Select("PP_CODE = '" & "HSBC" & "' ")
                If dr.Length > 0 Then
                    If btnHSBCPrice.Visible = True Then
                        If btnHSBCPrice.Enabled = True Then
                            'If chkHSBC.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "HSBC" & "' ")
                                HSBC_ID = dr1(0).Item("PP_ID").ToString

                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(HSBC_ID) = True Then             ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                                'Get_RFQData_TOXML(HSBC_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('HSBCwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblHSBCPrice.ClientID + "','" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "','" + btnHSBCPrice.ClientID + "');")
                                    HsbcHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('HSBCwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblHSBCPrice.ClientID + "','" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "','" + btnHSBCPrice.ClientID + "');")
                                    HsbcHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("HSBC", HSBC_ID)
                                getAllId.Add(HSBC_ID, Convert.ToString(Session("Quote_ID")))
                            'End If
                        End If
                    End If
                End If

                dr = dtLogin.Select("PP_CODE = '" & "BAML" & "' ")
                If dr.Length > 0 Then
                    If btnBAMLPrice.Visible = True Then
                        If btnBAMLPrice.Enabled = True Then
                            'If chkBAML.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "BAML" & "' ")
                                BAML_ID = dr1(0).Item("PP_ID").ToString

                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(BAML_ID) = True Then             ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                                'Get_RFQData_TOXML(BAML_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('BAMLwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblBAMLPrice.ClientID + "','" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "','" + btnBAMLPrice.ClientID + "');")
                                    BAMLHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('BAMLwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblBAMLPrice.ClientID + "','" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "','" + btnBAMLPrice.ClientID + "');")
                                    BAMLHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("BAML", BAML_ID)
                                getAllId.Add(BAML_ID, Convert.ToString(Session("Quote_ID")))
                            'End If
                        End If
                    End If
                End If

                dr = dtLogin.Select("PP_CODE = '" & "BNPP" & "' ")
                If dr.Length > 0 Then
                    If btnBNPPPrice.Visible = True Then
                        If btnBNPPPrice.Enabled = True Then
                            'If chkBNPP.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "BNPP" & "' ")
                                BNPP_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(BNPP_ID) = True Then             ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                                'Get_RFQData_TOXML(BNPP_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('BNPPwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblBNPPPrice.ClientID + "','" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "','" + btnBNPPPrice.ClientID + "');")
                                BNPPHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                            Else
                                strJavaScriptAllRequest.AppendLine("document.getElementById('BNPPwait').style.visibility = 'visible';")
                                strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblBNPPPrice.ClientID + "','" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "','" + btnBNPPPrice.ClientID + "');")
                                BNPPHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                            End If

                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("BNPP", BNPP_ID)
                                getAllId.Add(BNPP_ID, Convert.ToString(Session("Quote_ID")))
                            ' End If
                        End If
                    End If
                End If

                dr = dtLogin.Select("PP_CODE = '" & "UBS" & "' ")
                If dr.Length > 0 Then
                    If btnUBSPrice.Visible = True Then
                        If btnUBSPrice.Enabled = True Then
                            ' If chkUBS.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "UBS" & "' ")
                                UBS_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(UBS_ID) = True Then              ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                            'Get_RFQData_TOXML(UBS_ID)
                            '</AvinashG. on 16-Feb-2016: Optimization>
                            If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                strJavaScriptAllRequest.AppendLine("document.getElementById('UBSwait').style.visibility = 'visible';")
                                strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblUBSPrice.ClientID + "','" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "','" + btnUBSPrice.ClientID + "');")
                                UbsHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                            Else
                                strJavaScriptAllRequest.AppendLine("document.getElementById('UBSwait').style.visibility = 'visible';")
                                strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblUBSPrice.ClientID + "','" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "','" + btnUBSPrice.ClientID + "');")
                                    UbsHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("UBS", UBS_ID)
                                getAllId.Add(UBS_ID, Convert.ToString(Session("Quote_ID")))
                            ' End If
                        End If
                    End If
                End If
               ''dr = dtLogin.Select("PP_CODE = '" & "DBIB" & "' ") ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                dr = dtLogin.Select("PP_CODE = '" & "DB" & "' ")
                If dr.Length > 0 Then
                    If btnDBIBPrice.Visible = True Then
                        If btnDBIBPrice.Enabled = True Then
                            'If chkDBIB.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                 'dr1 = dtPriceProvider.Select("PP_CODE = '" & "DBIB" & "' ") ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                            dr1 = dtPriceProvider.Select("PP_CODE = '" & "DB" & "' ")
                                DBIB_ID = dr1(0).Item("PP_ID").ToString

                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(DBIB_ID) = True Then                 ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                            'Get_RFQData_TOXML(DBIB_ID)
                            '</AvinashG. on 16-Feb-2016: Optimization>
                            If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                strJavaScriptAllRequest.AppendLine("document.getElementById('DBIBwait').style.visibility = 'visible';")
                                strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblDBIBPrice.ClientID + "','" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "','" + btnDBIBPrice.ClientID + "');")
                                DBIBHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                            Else
                                strJavaScriptAllRequest.AppendLine("document.getElementById('DBIBwait').style.visibility = 'visible';")
                                strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblDBIBPrice.ClientID + "','" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "','" + btnDBIBPrice.ClientID + "');")
                                DBIBHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                            End If

                            If all_RFQ_IDs = "" Then
                                all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                strQuoteId1 = all_RFQ_IDs
                            Else
                                all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                            End If
                            ''getPPId.Add("DBIB", DBIB_ID) ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                            getPPId.Add("DB", DBIB_ID)
                            getAllId.Add(DBIB_ID, Convert.ToString(Session("Quote_ID")))
                        End If
                    End If
                End If    'Mohit lalwani on 06-Sept-2016

                dr = dtLogin.Select("PP_CODE = '" & "OCBC" & "' ")
                If dr.Length > 0 Then
                    If btnOCBCprice.Visible = True Then
                        If btnOCBCprice.Enabled = True Then
                            'If chkOCBC.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "OCBC" & "' ")
                                OCBC_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(OCBC_ID) = True Then             ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                                'Get_RFQData_TOXML(OCBC_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('OCBCwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblOCBCPrice.ClientID + "','" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "','" + btnOCBCprice.ClientID + "');")
                                    OCBCHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('OCBCwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblOCBCPrice.ClientID + "','" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "','" + btnOCBCprice.ClientID + "');")
                                    OCBCHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("OCBC", OCBC_ID)
                                getAllId.Add(OCBC_ID, Convert.ToString(Session("Quote_ID")))
                            'End If
                        End If

                    End If
                End If

                dr = dtLogin.Select("PP_CODE = '" & "CITI" & "' ")
                If dr.Length > 0 Then
                    If btnCITIprice.Visible = True Then
                        If btnCITIprice.Enabled = True Then
                            'If chkCITI.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "CITI" & "' ")
                                CITI_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(CITI_ID) = True Then             ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                                'Get_RFQData_TOXML(CITI_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('CITIwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCITIPrice.ClientID + "','" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "','" + btnCITIprice.ClientID + "');")
                                    CITIHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('CITIwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCITIPrice.ClientID + "','" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "','" + btnCITIprice.ClientID + "');")
                                    CITIHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("CITI", CITI_ID)
                                getAllId.Add(CITI_ID, Convert.ToString(Session("Quote_ID")))
                            'End If
                        End If
                    End If
                End If
dr = dtLogin.Select("PP_CODE = '" & "LEONTEQ" & "' ")
                If dr.Length > 0 Then
                    If btnLEONTEQprice.Visible = True Then
                        If btnLEONTEQprice.Enabled = True Then
                            'If chkLEONTEQ.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "LEONTEQ" & "' ")
                                LEONTEQ_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(LEONTEQ_ID) = True Then          ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                                'Get_RFQData_TOXML(LEONTEQ_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('LEONTEQwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblLEONTEQPrice.ClientID + "','" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "','" + btnLEONTEQprice.ClientID + "');")
                                    LEONTEQHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('LEONTEQwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblLEONTEQPrice.ClientID + "','" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "','" + btnLEONTEQprice.ClientID + "');")
                                    LEONTEQHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("LEONTEQ", LEONTEQ_ID)
                                getAllId.Add(LEONTEQ_ID, Convert.ToString(Session("Quote_ID")))
                            'End If
                        End If
                    End If
                End If
                dr = dtLogin.Select("PP_CODE = '" & "COMMERZ" & "' ")
                If dr.Length > 0 Then
                    If btnCOMMERZprice.Visible = True Then
                        If btnCOMMERZprice.Enabled = True Then
                            'If chkCOMMERZ.Checked = True Then''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "COMMERZ" & "' ")
                                COMMERZ_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                            If FillRFQDataTable(COMMERZ_ID) = True Then          ''AshwiniP on 05-Oct-2016 : Notional validation at the time of pricing 
                            Else
                                Exit Sub
                            End If
                                'Get_RFQData_TOXML(COMMERZ_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveFor.SelectedValue = "PricePercentage" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('COMMERZwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPrice('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCOMMERZPrice.ClientID + "','" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "','" + btnCOMMERZprice.ClientID + "');")
                                    COMMERZHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('COMMERZwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrike('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCOMMERZPrice.ClientID + "','" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "','" + btnCOMMERZprice.ClientID + "');")
                                    COMMERZHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("COMMERZ", COMMERZ_ID)
                                getAllId.Add(COMMERZ_ID, Convert.ToString(Session("Quote_ID")))
                            'End If
                        End If
                    End If
                End If
            End If
            Session.Add("All_IDs", all_RFQ_IDs)
            Session.Add("Hash_Values", getAllId)
            Session.Add("PP_IdTable", getPPId)
            lblerror.Text = "RFQs " & all_RFQ_IDs & " are generated."
            lblerror.ForeColor = Color.Blue
            lblMsgPriceProvider.Text = ""

            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptAllRequest", "try {" + strJavaScriptAllRequest.ToString + " } catch(e){}", True) 		'Mohit Lalwani on 26-Oct-2016
            If all_RFQ_IDs <> "" Then
                objELNRFQ.DB_update_quoteID(all_RFQ_IDs, strQuoteId1, dtUpdate)
            End If
            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Call Fill_All_Charts()
            End If
        Catch ex As Exception
            lblerror.Text = "Solve_All_Requests:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Solve_All_Requests", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub btnSolveAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSolveAll.Click

        Try
            ResetAllChkBox() ''<Nikhil M. on 21-Sep-2016: Added>
            If txtTradeDate.Value = "" Or txtMaturityDate.Value = "" Or txtExpiryDate.Value = "" Or txtSettlementDate.Value = "" Then
                lblerror.Text = "Please enter valid date."
                Exit Sub
            End If

            ''< Start Nikhil M. on 17-Sep-2016: COmmented chkBox condition Dependency >
            ' If chkJPM.Checked = True Then
            TRJPM1.Attributes.Remove("class")
            'End If
            'If chkHSBC.Checked = True Then
            TRHSBC1.Attributes.Remove("class")
            'End If
            'If chkUBS.Checked = True Then
            TRUBS1.Attributes.Remove("class")
            'End If
            'If chkCS.Checked = True Then
            TRCS1.Attributes.Remove("class")
            'End If
            'If chkBNPP.Checked = True Then
            TRBNPP1.Attributes.Remove("class")
            'End If
            'If chkBAML.Checked = True Then
            TRBAML1.Attributes.Remove("class")
            'End If
            'If chkDBIB.Checked = True Then
            TRDBIB.Attributes.Remove("class")
            'End If
            'If chkOCBC.Checked = True Then
            TROCBC1.Attributes.Remove("class")
            'End If
            'If chkCITI.Checked = True Then
            TRCITI1.Attributes.Remove("class")
            'End If
            'If chkLEONTEQ.Checked = True Then
            TRLEONTEQ1.Attributes.Remove("class")
            'End If
            'If chkCOMMERZ.Checked = True Then
            TRCOMMERZ1.Attributes.Remove("class")
            'End If
            hideShowRBLShareData()
            Session.Remove("TradeDAte")
            Session.Remove("MaturityDAte")
            Session.Remove("expiryDAte")
            Session.Remove("Settlementdate")
            Session.Add("TradeDAte", txtTradeDate.Value)
            Session.Add("MaturityDAte", txtMaturityDate.Value)
            Session.Add("expiryDAte", txtExpiryDate.Value)
            Session.Add("Settlementdate", txtSettlementDate.Value)
            If (btnJPMprice.Visible = False Or btnJPMprice.Enabled = False) And _
            (btnHSBCPrice.Visible = False Or btnHSBCPrice.Enabled = False) And _
            (btnUBSPrice.Visible = False Or btnUBSPrice.Enabled = False) And _
            (btnCSPrice.Visible = False Or btnCSPrice.Enabled = False) And _
            (btnBAMLPrice.Visible = False Or btnBAMLPrice.Enabled = False) And _
            (btnBNPPPrice.Visible = False Or btnBNPPPrice.Enabled = False) And _
             (btnDBIBPrice.Visible = False Or btnDBIBPrice.Enabled = False) And _
      (btnOCBCprice.Visible = False Or btnOCBCprice.Enabled = False) And _
      (btnCITIprice.Visible = False Or btnCITIprice.Enabled = False) And _
      (btnLEONTEQprice.Visible = False Or btnLEONTEQprice.Enabled = False) And _
      (btnCOMMERZprice.Visible = False Or btnCOMMERZprice.Enabled = False) Then
                lblerror.Text = "All price providers are Off. Please try again later."
                Exit Sub
            End If
            ''<Start | Nikhil M. on 17-Sep-2016: Commented All checked condition>
            '      If (btnJPMprice.Visible = False Or chkJPM.Checked = False) And _
            '      (btnHSBCPrice.Visible = False Or chkHSBC.Checked = False) And _
            '      (btnUBSPrice.Visible = False Or chkUBS.Checked = False) And _
            '      (btnCSPrice.Visible = False Or chkCS.Checked = False) And _
            '      (btnBAMLPrice.Visible = False Or chkBAML.Checked = False) And _
            '      (btnBNPPPrice.Visible = False Or chkBNPP.Checked = False) And _
            '       (btnDBIBPrice.Visible = False Or chkDBIB.Checked = False) And _
            '(btnOCBCprice.Visible = False Or chkOCBC.Checked = False) And _
            '(btnCITIprice.Visible = False Or chkCITI.Checked = False) And _
            '(btnLEONTEQprice.Visible = False Or chkLEONTEQ.Checked = False) And _
            '(btnCOMMERZprice.Visible = False Or chkCOMMERZ.Checked = False) Then
            '          lblerror.Text = "Please check valid price provider."
            '          Exit Sub
            '      End If
            If (btnJPMprice.Visible = False) And _
              (btnHSBCPrice.Visible = False) And _
              (btnUBSPrice.Visible = False) And _
              (btnCSPrice.Visible = False) And _
              (btnBAMLPrice.Visible = False) And _
              (btnBNPPPrice.Visible = False) And _
              (btnDBIBPrice.Visible = False) And _
              (btnOCBCprice.Visible = False) And _
               (btnCITIprice.Visible = False) And _
               (btnLEONTEQprice.Visible = False) And _
               (btnCOMMERZprice.Visible = False) Then
                lblerror.Text = "Please check valid price provider."
                Exit Sub
            End If
            AllHiddenPrice.Value = "Disable;Enable"
            RestoreSolveAll()
            RestoreAll()
            ''<Start | Nikhil M. on 17-Sep-2016: Commented All checked condition>
            'If chkJPM.Checked = True Then
            JpmHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkHSBC.Checked = True Then
            HsbcHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkCS.Checked = True Then
            CsHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkUBS.Checked = True Then
            UbsHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            ' If chkBNPP.Checked = True Then
            BNPPHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkBAML.Checked = True Then
            BAMLHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            ' End If
            ' If chkDBIB.Checked = True Then
            DBIBHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkOCBC.Checked = True Then
            OCBCHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            ' End If
            'If chkCITI.Checked = True Then
            CITIHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkLEONTEQ.Checked = True Then
            LEONTEQHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkCOMMERZ.Checked = True Then
            COMMERZHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            ''<Nikhil M. on 17-Sep-2016: Commented All checked condition>
            lblerror.Text = ""
            Solve_All_Requests(sender, e)
            '<Changed by Mohit Lalwnai on 3-Nov-2015  FA-1174>
            rbHistory.SelectedValue = "Quote History"

            fill_grid()
            makeThisGridVisible(grdELNRFQ) 'FA-1174
            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Call Fill_All_Charts()
            End If
            lblJPMPrice.Text = ""
            lblHSBCPrice.Text = ""
            lblUBSPrice.Text = ""
            lblCSPrice.Text = ""
            lblBNPPPrice.Text = ""
            lblmsgPriceProvider.Text = ""
            lblBAMLPrice.Text = ""
            lblDBIBPrice.Text = ""
	    lblOCBCPrice.Text = ""
	    lblCITIPrice.Text = ""
            btnBAMLPrice.Text = "Price"
            setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice()
            btnCSPrice.Text = "Price"
            btnJPMprice.Text = "Price"
            btnHSBCPrice.Text = "Price"
            btnUBSPrice.Text = "Price"
            btnBNPPPrice.Text = "Price"
            btnDBIBPrice.Text = "Price"
            btnOCBCprice.Text = "Price"
            btnCITIprice.Text = "Price"
            btnLEONTEQPrice.Text = "Price"
            btnCOMMERZprice.Text = "Price"
            btnBNPPDeal.Visible = False
            btnBAMLDeal.Visible = False
            btnCSDeal.Visible = False
            btnJPMDeal.Visible = False
            btnHSBCDeal.Visible = False
            btnUBSDeal.Visible = False
            btnDBIBDeal.Visible = False
            btnBAMLPrice.Enabled = False
            btnOCBCDeal.Visible = False
            btnCITIDeal.Visible = False
 btnLEONTEQDeal.Visible = False
            btnCOMMERZDeal.Visible = False
            ''<Start | Nikhil M. on 17-Sep-2016: Commented Checked condition for removing CHkBox Dependency >
            'If chkBAML.Checked = True Then
            btnBAMLPrice.Enabled = False
            btnBAMLPrice.CssClass = "btnDisabled"
            ' End If
            ' If chkCS.Checked = True Then
            btnCSPrice.Enabled = False
            btnCSPrice.CssClass = "btnDisabled"
            ' End If
            ' If chkJPM.Checked = True Then
            btnJPMprice.Enabled = False
            btnJPMprice.CssClass = "btnDisabled"
            'End If
            'If chkHSBC.Checked = True Then
            btnHSBCPrice.Enabled = False
            btnHSBCPrice.CssClass = "btnDisabled"
            'End If
            'If chkUBS.Checked = True Then
            btnUBSPrice.Enabled = False
            btnUBSPrice.CssClass = "btnDisabled"
            ' End If
            'If chkBNPP.Checked = True Then
            btnBNPPPrice.Enabled = False
            btnBNPPPrice.CssClass = "btnDisabled"
            'End If
            'If chkDBIB.Checked = True Then
            btnDBIBPrice.Enabled = False
            btnDBIBPrice.CssClass = "btnDisabled"
            'End If
            'If chkOCBC.Checked = True Then
            btnOCBCprice.Enabled = False
            btnOCBCprice.CssClass = "btnDisabled"
            'End If
            'If chkCITI.Checked = True Then
            btnCITIprice.Enabled = False
            btnCITIprice.CssClass = "btnDisabled"
            'End If
            'If chkLEONTEQ.Checked = True Then
            btnLEONTEQprice.Enabled = False
            btnLEONTEQprice.CssClass = "btnDisabled"
            'End If
            'If chkCOMMERZ.Checked = True Then
            btnCOMMERZprice.Enabled = False
            btnCOMMERZprice.CssClass = "btnDisabled"
            'End If
            ''<End | Nikhil M. on 17-Sep-2016: Commented for removing CHkBox Dependency >
            btnSolveAll.Enabled = False
            btnSolveAll.CssClass = "btnDisabled"
        Catch ex As Exception
            lblerror.Text = "btnSolveAll_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnSolveAll_Click", ErrorLevel.High)
        End Try
    End Sub
#End Region
#Region "LimitPrice,SetNumberFormat"

    Private Function Qty_Validate(ByVal strQyt As String) As Boolean
        Try

            If strQyt = "" Then

                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                'lblerror.Text = "Please enter valid Notional."
                'Return False
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                Exit Function
            Else
                Dim reg As Regex = New Regex("^[0-9kKmMbBtTlLpP.,]*$")
                If reg.IsMatch(strQyt) = False Then
                    lblerror.Text = "Please enter valid Notional."
                    Return False
                    Exit Function
                End If
            End If

            Return True
        Catch ex As Exception
            lblerror.Text = "Qty_Validate:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Qty_Validate", ErrorLevel.High)
            Throw ex
        End Try
    End Function

#End Region
#Region "Dealer Details"

    Private Sub ddlentity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlentity.SelectedIndexChanged
        Try
            fill_RMList()
            fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
            fill_All_EntityBooks()

        Catch ex As Exception
            lblerror.Text = "ddlentity_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlentity_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub ddlRM_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRM.SelectedIndexChanged
        Try
            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            'Mohit Lalwani on 1-Aug-2016
            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            fill_Email()
            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Call Fill_All_Charts()
            End If
        Catch ex As Exception
            lblerror.Text = "ddlRM_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlRM_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub ddlRFQRM_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRFQRM.SelectedIndexChanged
        Try
            lblerror.Text = ""
            fill_Branch()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "ddlRFQRM_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlRFQRM_SelectedIndexChanged", ErrorLevel.High)
        End Try

    End Sub
#End Region

    Private Sub txtTotalRows_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTotalRows.TextChanged
        Try
            rbHistory_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            lblerror.Text = "txtTotalRows_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtTotalRows_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub Fill_All_Charts()
        Try
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                Case "Y", "YES"
                    Get_RFQ_PieChart()
                    upnlChart.Update()
                    Get_RFQ_ColumnChart()
                    upnlColumnChart.Update()
                    upnlGrid.Update()
                Case "N", "NO"
            End Select
        Catch ex As Exception
            lblerror.Text = "Fill_All_Charts:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
            sSelfPath, "Fill_All_Charts", ErrorLevel.High)
        End Try
    End Sub
    Private Sub txtUpfrontELN_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUpfrontELN.TextChanged
        Try
            lblerror.Text = ""

            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            'Mohit Lalwani on 1-Aug-2016

            ''clearFields() <Nikhil M: Commented for Calucation of Price on Upfront Change 24Aug16 />
            ''<Nikhil M: Added for Calucation of Price on Upfront Change 24Aug16 >
            txtUpfrontELN.Text = SetNumberFormat(txtUpfrontELN.Text, 2)
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ResetPriceOnUpfrontChange_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "Y").Trim.ToUpper
                Case "Y", "YES"
                    SetClientYeild(HsbcHiddenPrice, lblHSBCClientPrice, lblHSBCClientYield, txtUpfrontELN)
                    SetClientYeild(UbsHiddenPrice, lblUBSClientPrice, lblUBSClientYield, txtUpfrontELN)
                    SetClientYeild(JpmHiddenPrice, lblJPMClientPrice, lblJPMClientYield, txtUpfrontELN)
                    SetClientYeild(BNPPHiddenPrice, lblBNPPClientPrice, lblBNPPClientYield, txtUpfrontELN)
                    SetClientYeild(CsHiddenPrice, lblCSClientPrice, lblCSClientYield, txtUpfrontELN)
                    SetClientYeild(CITIHiddenPrice, lblCITIClientPrice, lblCITIClientYield, txtUpfrontELN)
                    SetClientYeild(BAMLHiddenPrice, lblBAMLClientPrice, lblBAMLClientYield, txtUpfrontELN)
                    SetClientYeild(OCBCHiddenPrice, lblOCBCClientPrice, lblOCBCClientYield, txtUpfrontELN)
                    SetClientYeild(DBIBHiddenPrice, lblDBIBClientPrice, lblDBIBClientYield, txtUpfrontELN)
                    SetClientYeild(LEONTEQHiddenPrice, lblLEONTEQClientPrice, lblLEONTEQClientYield, txtUpfrontELN)
                    SetClientYeild(COMMERZHiddenPrice, lblCOMMERZClientPrice, lblCOMMERZClientYield, txtUpfrontELN)

                Case "N", "NO"
                    btnSolveAll_Click(sender, e)
            End Select
            ''</Nikhil M: Added for Calucation of Price on Upfront Change 24Aug16 >
            GetCommentary()
            ''ResetAll()<Nikhil M: Commented for Calucation of Price on Upfront Change 24Aug16 />
        Catch ex As Exception
            lblerror.Text = "txtUpfrontELN_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtUpfrontELN_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub btnDealConfirm_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDealConfirm.ServerClick
        Try
            If txtClientYieldPopUpValue.Text.Trim <> "" Then
                Call txtUpfrontPopUpValue_TextChanged(Nothing, Nothing)
            End If
            Stop_timer_Only()
            ''<Start | Nikhil M. on 20-Sep-2016: Added>
            ''If drpConfirmDeal.Items.Count > 0 And drpConfirmDeal.SelectedText.ToString = "" Then
            ''lblerrorPopUp.Text = "Please select a reason."
            ''Exit Sub
            ''Else
            ''<Start | Nikhil M. on 20-Sep-2016: Added for Reset Commentry>
            hdnstrBestClientYeild.Value = ""
            hdnBestIssuer.Value = ""
            hdnBestProvider.Value = ""
            ResetAllChkBox()
            GetCommentary()
            ''<End | Nikhil M. on 20-Sep-2016: Added for Reset Commentry>
            '' End If
            ''<End | Nikhil M. on 20-Sep-2016: Added>
            If Val(txtUpfrontPopUpValue.Text) > 0 And Val(txtUpfrontPopUpValue.Text.Replace(",", "")) < 100 Then
                Select Case UCase(lblIssuerPopUpValue.Text)
                    '<Start : Nikhil M. on 08-Sep-2016: Commented  >
                    Case "JPM"
                        btnJPMDeal_Click(sender, e)
                    Case "CS"
                        btnCSDeal_Click(sender, e)
                    Case "UBS"
                        btnUBSDeal_Click(sender, e)
                    Case "HSBC"
                        btnHSBCDeal_Click(sender, e)
                    Case "BAML"
                        btnBAMLDeal_Click(sender, e)
                    Case "BNPP"
                        btnBNPPDeal_Click(sender, e)
                        'Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                    Case "DB"
                        btnDBIBDeal_Click(sender, e)
                    Case "OCBC"
                        btnOCBCDeal_Click(sender, e)
                    Case "CITI"
                        btnCITIDeal_Click(sender, e)
                    Case "LEONTEQ"
                        btnLEONTEQDeal_Click(sender, e)
                    Case "COMMERZ"
                        btnCOMMERZDeal_Click(sender, e)
                        'End Select
                        ''<End : Nikhil M. on 08-Sep-2016: COmmented  >

                        ''<Start : Nikhil M. on 08-Sep-2016: Added >
                        'Case "JPM"
                        '    If GetBestPriceConfirm(JpmHiddenPrice.Value, "JPM") Then
                        '        btnJPMDeal_Click(sender, e)
                        '    End If

                        'Case "CS"
                        '    If GetBestPriceConfirm(CsHiddenPrice.Value, "Credit Suisse") Then
                        '        btnCSDeal_Click(sender, e)
                        '    End If

                        'Case "UBS"
                        '    If GetBestPriceConfirm(UbsHiddenPrice.Value, "UBS") Then
                        '        btnUBSDeal_Click(sender, e)
                        '    End If

                        'Case "HSBC"
                        '    If GetBestPriceConfirm(HsbcHiddenPrice.Value, "HSBC") Then
                        '        btnHSBCDeal_Click(sender, e)
                        '    End If

                        'Case "OCBC"
                        '    If GetBestPriceConfirm(OCBCHiddenPrice.Value, "OCBC") Then
                        '        btnOCBCDeal_Click(sender, e)
                        '    End If

                        'Case "CITI"
                        '    If GetBestPriceConfirm(CITIHiddenPrice.Value, "CITI") Then
                        '        btnCITIDeal_Click(sender, e)
                        '    End If

                        'Case "LEONTEQ"
                        '    If GetBestPriceConfirm(LEONTEQHiddenPrice.Value, "LEONTEQ") Then
                        '        btnLEONTEQDeal_Click(sender, e)
                        '    End If

                        'Case "COMMERZ"
                        '    If GetBestPriceConfirm(COMMERZHiddenPrice.Value, "COMMERZ") Then
                        '        btnCOMMERZDeal_Click(sender, e)
                        '    End If

                        'Case "BAML"
                        '    If GetBestPriceConfirm(BAMLHiddenPrice.Value, "BAML") Then
                        '        btnBAMLDeal_Click(sender, e)
                        '    End If

                        'Case "BNPP"
                        '    If GetBestPriceConfirm(BNPPHiddenPrice.Value, "BNPP") Then
                        '        btnBNPPDeal_Click(sender, e)
                        '    End If

                        'Case "DB"
                        '    If GetBestPriceConfirm(DBIBHiddenPrice.Value, "DBIB") Then
                        '        btnDBIBDeal_Click(sender, e)
                        '    End If
                End Select
                ''<End : Nikhil M. on 08-Sep-2016: Added >
                btnLoad_Click(sender, e)
            ElseIf Val(txtUpfrontPopUpValue.Text.Replace(",", "")) >= 100 Then
                lblerrorPopUp.Text = "Upfront should be less than 100."
                ''<Dilkhush 11Jan2016:- Added One condition to show valid msg>
            ElseIf Val(txtUpfrontPopUpValue.Text) = 0 Then
                lblerrorPopUp.Text = "Upfront should be greater than zero."
            Else
                lblerrorPopUp.Text = "Please enter valid Upfront."
            End If
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Or UCase(Request.QueryString("EXTLOD")) = "REDIRECTEDHEDGE" Then
                resetControlsForPool(True)
                tabPanelELN.Enabled = True

                Dim isreadonly As Reflection.PropertyInfo = GetType(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
                ' make collection editable
                isreadonly.SetValue(Me.Request.QueryString, False, Nothing)
                ' remove
                Me.Request.QueryString.Remove("EXTLOD")
                Me.Request.QueryString.Remove("PRD")
                Me.Request.QueryString.Remove("RedirectedOrderId")
                Me.Request.QueryString.Remove("PoolID")

                Exit Sub
            End If
        Catch ex As Exception
            lblerror.Text = "btnDealConfirm_ServerClick:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnDealConfirm_ServerClick", ErrorLevel.High)

        Finally
            UPanle11111.Update()
        End Try

    End Sub
    Private Sub btnDealCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDealCancel.ServerClick
        Try
            Dim strJavaScriptStopTimer As New StringBuilder
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
                ShowHideConfirmationPopup(False)
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
		strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');") '
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "StopAllTimers", strJavaScriptStopTimer.ToString, True)
                lblerror.Text = ""
                panelELN.Enabled = False
                upnl1.Update()
                Exit Sub
            End If
            ShowHideConfirmationPopup(False, "NO")

            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            '/Mohit Lalwani on 1-Aug-2016

            'Changed by Mohit Lalwani on 1-Jul-2016
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
            'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "StopAllTimers", strJavaScriptStopTimer.ToString, True)
            'ResetAll()
            '/Changed by Mohit Lalwani on 1-Jul-2016
            lblerror.Text = ""
            '<ashwiniP Start>
            lblTotalAmt.Visible = False
            lblTotalAmtVal.Visible = False
            lblAlloAmt.Visible = False
            lblAlloAmtVal.Visible = False
            lblRemainNotional.Visible = False
            lblRemainNotionalVal.Visible = False
            '<End>

            If Not IsNothing(Request.QueryString("RedirectedOrderId")) Then
                resetControlsForPool(True)
                tabPanelELN.Enabled = True
                Dim isreadonly As Reflection.PropertyInfo = GetType(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
                isreadonly.SetValue(Me.Request.QueryString, False, Nothing)
                Me.Request.QueryString.Remove("EXTLOD")
                Me.Request.QueryString.Remove("PRD")
                Me.Request.QueryString.Remove("RedirectedOrderId")
                Me.Request.QueryString.Remove("PoolID")
            End If
        Catch ex As Exception
            lblerror.Text = "btnDealCancel_ServerClick:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnDealCancel_ServerClick", ErrorLevel.High)
        Finally
            pnlReprice.Update()
        End Try
    End Sub
    Private Sub ddlOrderTypePopUpValue_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOrderTypePopUpValue.SelectedIndexChanged
        Try
            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            '/Mohit Lalwani on 1-Aug-2016
            txtLimitPricePopUpValue.Text = "0"
            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            If ddlOrderTypePopUpValue.SelectedItem.Text.Contains("Limit") Then
                txtLimitPricePopUpValue.Enabled = True
                ddlBasketSharesPopValue.Visible = False
                txtLimitPricePopUpValue.Style.Add("width", "115px !important")
            Else
                txtLimitPricePopUpValue.Enabled = False
                ddlBasketSharesPopValue.Visible = False
                txtLimitPricePopUpValue.Style.Add("width", "115px !important")
            End If
        Catch ex As Exception
            lblerror.Text = "ddlOrderTypePopUpValue_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlOrderTypePopUpValue_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub txtUpfrontPopUpValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUpfrontPopUpValue.TextChanged
        Try
            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            'Mohit Lalwani on 1-Aug-2016
            lblerrorPopUp.Text = "" '<AvinashG. on 26-Feb-2016: FA-1232 Client yield validation message on order confirmation pop should get clear when upfront changes >
            If Not sender Is Nothing And Not e Is Nothing Then
                chkUpfrontOverride.Checked = False
                chkUpfrontOverride.Visible = False
            End If
            If txtUpfrontPopUpValue.Text.Trim <> "" Then
                lblClientPricePopUpValue.Text = FormatNumber((Val(lblIssuerPricePopUpValue.Text) + Val(txtUpfrontPopUpValue.Text)).ToString, 2)
                '''txtClientYieldPopUpValue.Text = FormatNumber(get_ELN_ClientYield(CDbl(lblClientPricePopUpValue.Text)), 2)'''DK
                txtClientYieldPopUpValue.Text = FormatNumber(get_ELN_ClientYield(CDbl(lblClientPricePopUpValue.Text)), 4)
                '''txtUpfrontPopUpValue.Text = SetNumberFormat(txtUpfrontPopUpValue.Text, 2)'''DK
                txtUpfrontPopUpValue.Text = SetNumberFormat(txtUpfrontPopUpValue.Text, 4)
                UPanle11111.Update()
            Else
                lblerrorPopUp.Text = "Please enter valid upfront."
            End If

        Catch ex As Exception
            lblerror.Text = "txtUpfrontPopUpValue_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtUpfrontPopUpValue_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    '<Riddhi S. on 25-Jul-2016: Client Yield change used to calculate upfront>
    Private Sub txtClientYieldPopUpValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClientYieldPopUpValue.TextChanged
        Try
            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            'Mohit Lalwani on 1-Aug-2016
            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            If txtClientYieldPopUpValue.Text.Trim <> "" Then
                ''txtClientYieldPopUpValue.Text = SetNumberFormat(txtClientYieldPopUpValue.Text, 2)'''DK
                txtClientYieldPopUpValue.Text = SetNumberFormat(txtClientYieldPopUpValue.Text, 4)
                ''' txtUpfrontPopUpValue.Text = FormatNumber(get_ELN_Upfront((CDbl(txtClientYieldPopUpValue.Text)), Val(lblIssuerPricePopUpValue.Text)), 2)'''DK
                txtUpfrontPopUpValue.Text = FormatNumber(get_ELN_Upfront((CDbl(txtClientYieldPopUpValue.Text)), Val(lblIssuerPricePopUpValue.Text)), 4)
                lblClientPricePopUpValue.Text = SetNumberFormat((CDbl(lblIssuerPricePopUpValue.Text) + CDbl(txtUpfrontPopUpValue.Text)), 2)
            Else
                lblerrorPopUp.Text = "Please enter valid yield."
            End If
        Catch ex As Exception
            lblerror.Text = "txtClientYieldPopUpValue_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtClientYieldPopUpValue_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub ResetAll()
        Try
            Enable_Disable_Deal_Buttons()
            btnSolveAll.Enabled = True
            btnSolveAll.CssClass = "btn"
            Chk_Server_UpDown()
            Dim strJavaScript As New StringBuilder
            AllHiddenPrice.Value = "Enable;Disable"
            btnSolveAll.Enabled = True
            strJavaScript.AppendLine("document.getElementById('PriceAllWait').style.visibility = 'hidden';")
            If btnJPMprice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnJPMDeal.ClientID + "');")
            End If
            If btnHSBCPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnHSBCDeal.ClientID + "');")
            End If
            If btnUBSPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnUBSDeal.ClientID + "');")
            End If
            If btnCSPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnCSDeal.ClientID + "');")
            End If
            If btnBAMLPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnBAMLDeal.ClientID + "');")
            End If
            If btnBNPPPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnBNPPDeal.ClientID + "');")
            End If
            If btnDBIBPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnDBIBDeal.ClientID + "');")
            End If
	    If btnOCBCPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnOCBCDeal.ClientID + "');")
            End If
	    If btnCITIPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnCITIDeal.ClientID + "');")
            End If
            If btnLEONTEQprice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnLEONTEQDeal.ClientID + "');")
            End If
            If btnCOMMERZprice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnCOMMERZDeal.ClientID + "');")
            End If
            If btnHSBCPrice.Enabled Then
                lblHSBCClientYield.Text = ""
                lblHSBCClientPrice.Text = ""
                lblHSBCPrice.Text = ""
                lblHSBCPrice.ForeColor = System.Drawing.Color.Green
                HsbcHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnHSBCPrice.Text = "Price"
                btnHSBCPrice.CssClass = "btn"
            Else
                lblHSBCClientYield.Text = ""
                lblHSBCClientPrice.Text = ""
                lblHSBCPrice.Text = ""
                HsbcHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If

            If btnJPMprice.Enabled Then
                lblJPMClientYield.Text = ""
                lblJPMClientPrice.Text = ""
                lblJPMPrice.Text = ""
                lblJPMPrice.ForeColor = System.Drawing.Color.Green
                JpmHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnJPMprice.Text = "Price"
                btnJPMprice.CssClass = "btn"
            Else
                lblJPMClientYield.Text = ""
                lblJPMClientPrice.Text = ""
                lblJPMPrice.Text = ""
                JpmHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If

            If btnCSPrice.Enabled Then
                lblCSClientYield.Text = ""
                lblCSClientPrice.Text = ""
                lblCSPrice.Text = ""
                lblCSPrice.ForeColor = System.Drawing.Color.Green
                CsHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnCSPrice.Text = "Price"
                btnCSPrice.CssClass = "btn"
            Else
                lblCSClientYield.Text = ""
                lblCSClientPrice.Text = ""
                lblCSPrice.Text = ""
                CsHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If

            If btnUBSPrice.Enabled Then
                lblUBSClientYield.Text = ""
                lblUBSClientPrice.Text = ""
                lblUBSPrice.Text = ""
                lblUBSPrice.ForeColor = System.Drawing.Color.Green
                UbsHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnUBSPrice.Text = "Price"
                btnUBSPrice.CssClass = "btn"
            Else
                lblUBSClientYield.Text = ""
                lblUBSClientPrice.Text = ""
                lblUBSPrice.Text = ""
                UbsHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If

            If btnBAMLPrice.Enabled Then
                lblBAMLClientYield.Text = ""
                lblBAMLClientPrice.Text = ""
                lblBAMLPrice.Text = ""
                lblBAMLPrice.ForeColor = System.Drawing.Color.Green
                BAMLHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnBAMLPrice.Text = "Price"
                btnBAMLPrice.CssClass = "btn"
            Else
                lblBAMLClientYield.Text = ""
                lblBAMLClientPrice.Text = ""
                lblBAMLPrice.Text = ""
                BAMLHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If

            If btnBNPPPrice.Enabled Then
                lblBNPPClientYield.Text = ""
                lblBNPPClientPrice.Text = ""
                lblBNPPPrice.Text = ""
                lblBNPPPrice.ForeColor = System.Drawing.Color.Green
                BNPPHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnBNPPPrice.Text = "Price"
                btnBNPPPrice.CssClass = "btn"
            Else
                lblBNPPClientYield.Text = ""
                lblBNPPClientPrice.Text = ""
                lblBNPPPrice.Text = ""
                BNPPHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If
            If btnDBIBPrice.Enabled Then
                lblDBIBClientYield.Text = ""
                lblDBIBClientPrice.Text = ""
                lblDBIBPrice.Text = ""
                lblDBIBPrice.ForeColor = System.Drawing.Color.Green
                DBIBHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnDBIBPrice.Text = "Price"
                btnDBIBPrice.CssClass = "btn"
            Else
                lblDBIBClientYield.Text = ""
                lblDBIBClientPrice.Text = ""
                lblDBIBPrice.Text = ""
                DBIBHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If
	    
	    If btnCITIPrice.Enabled Then
                lblCITIClientYield.Text = ""
                lblCITIClientPrice.Text = ""
                lblCITIPrice.Text = ""
                lblCITIPrice.ForeColor = System.Drawing.Color.Green
                CITIHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnCITIPrice.Text = "Price"
                btnCITIPrice.CssClass = "btn"
            Else
                lblCITIClientYield.Text = ""
                lblCITIClientPrice.Text = ""
                lblCITIPrice.Text = ""
                CITIHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If

            If btnLEONTEQprice.Enabled Then
                lblLEONTEQClientYield.Text = ""
                lblLEONTEQClientPrice.Text = ""
                lblLEONTEQPrice.Text = ""
                lblLEONTEQPrice.ForeColor = System.Drawing.Color.Green
                LEONTEQHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnLEONTEQprice.Text = "Price"
                btnLEONTEQprice.CssClass = "btn"
            Else
                lblLEONTEQClientYield.Text = ""
                lblLEONTEQClientPrice.Text = ""
                lblLEONTEQPrice.Text = ""
                LEONTEQHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If

            If btnCOMMERZprice.Enabled Then
                lblCOMMERZClientYield.Text = ""
                lblCOMMERZClientPrice.Text = ""
                lblCOMMERZPrice.Text = ""
                lblCOMMERZPrice.ForeColor = System.Drawing.Color.Green
                COMMERZHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnCOMMERZprice.Text = "Price"
                btnCOMMERZprice.CssClass = "btn"
            Else
                lblCOMMERZClientYield.Text = ""
                lblCOMMERZClientPrice.Text = ""
                lblCOMMERZPrice.Text = ""
                COMMERZHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If
	    If btnOCBCPrice.Enabled Then
                lblOCBCClientYield.Text = ""
                lblOCBCClientPrice.Text = ""
                lblOCBCPrice.Text = ""
                lblOCBCPrice.ForeColor = System.Drawing.Color.Green
                OCBCHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnOCBCPrice.Text = "Price"
                btnOCBCPrice.CssClass = "btn"
            Else
                lblOCBCClientYield.Text = ""
                lblOCBCClientPrice.Text = ""
                lblOCBCPrice.Text = ""
                OCBCHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If
	    
            strJavaScript.AppendLine("StopPolling();")
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "StopPolling", "try{" + strJavaScript.ToString + "} catch(e){}", True) 		'Mohit Lalwani on 26-Oct-2016
            DealConfirmPopup.Visible = False
            UPanle11111.Update()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
            TRJPM1.Attributes.Remove("class")
            TRHSBC1.Attributes.Remove("class")
            TRUBS1.Attributes.Remove("class")
            TRCS1.Attributes.Remove("class")
            TRBNPP1.Attributes.Remove("class")
            TRBAML1.Attributes.Remove("class") ''Added by imran on 21Aug2015 for bestprice
            TRDBIB.Attributes.Remove("class") ''Added by imran on 21Aug2015 for bestprice
	     TROCBC1.Attributes.Remove("class")
            TRCITI1.Attributes.Remove("class")
            TRLEONTEQ1.Attributes.Remove("class")
            TRCOMMERZ1.Attributes.Remove("class")
            hideShowRBLShareData()
            ResetMinMaxNotional() '<Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
            chkPPmaillist.Visible = False
            '<Added by AshwiniP on 04-oct-2016
            hdnstrBestClientYeild.Value = ""
            hdnBestIssuer.Value = ""
            hdnBestProvider.Value = ""
            ResetAllChkBox()
            ' </Added by AshwiniP on 04-oct-2016
        Catch ex As Exception
            lblerror.Text = "ResetAll:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ResetAll", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    '<Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> 
    Private Sub ResetMinMaxNotional()
        lblJPMlimit.Text = ""
        lblJPMlimit.ToolTip = ""
        lblJPMlimit.Visible = False
        lblHSBClimit.Text = ""
        lblHSBClimit.ToolTip = ""
        lblHSBClimit.Visible = False
        lblCSLimit.Text = ""
        lblCSLimit.ToolTip = ""
        lblCSLimit.Visible = False
        lblUBSlimit.Text = ""
        lblUBSlimit.ToolTip = ""
        lblUBSlimit.Visible = False
        lblBAMLlimit.Text = ""
        lblBAMLlimit.ToolTip = ""
        lblBAMLlimit.Visible = False
        lblBNPPlimit.Text = ""
        lblBNPPlimit.ToolTip = ""
        lblBNPPlimit.Visible = False
	lblDBIBlimit.Text = ""
        lblDBIBlimit.ToolTip = ""
        lblDBIBlimit.Visible = False
	 lblOCBClimit.Text = ""
        lblOCBClimit.ToolTip = ""
        lblOCBClimit.Visible = False
	lblCITIlimit.Text = ""
        lblCITIlimit.ToolTip = ""
        lblCITIlimit.Visible = False
        lblLEONTEQlimit.Text = ""
        lblLEONTEQlimit.ToolTip = ""
        lblLEONTEQlimit.Visible = False
        lblCOMMERZlimit.Text = ""
        lblCOMMERZlimit.ToolTip = ""
        lblCOMMERZlimit.Visible = False
        lblRangeCcy.Text = ""
    End Sub
    '<\Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> 

    Private Sub RestoreAll()
        Try
            Dim iTabIdx As Integer = tabContainer.ActiveTabIndex
            Dim ddlPrdSolveFor As Telerik.Web.UI.RadDropDownList
            ddlPrdSolveFor = ddlSolveFor
            Dim strJavaScriptRestoreAll As New StringBuilder
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            strJavaScriptRestoreAll.AppendLine(Restore(JpmHiddenPrice, lblJPMPrice, lblJPMClientPrice, lblJPMClientYield, iTabIdx, ddlPrdSolveFor, btnJPMprice, btnJPMDoc, JpmHiddenAccDays, lblJPMlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(HsbcHiddenPrice, lblHSBCPrice, lblHSBCClientPrice, lblHSBCClientYield, iTabIdx, ddlPrdSolveFor, btnHSBCPrice, btnHSBCDoc, HsbcHiddenAccDays, lblHSBClimit))
            strJavaScriptRestoreAll.AppendLine(Restore(UbsHiddenPrice, lblUBSPrice, lblUBSClientPrice, lblUBSClientYield, iTabIdx, ddlPrdSolveFor, btnUBSPrice, btnUBSDoc, UbsHiddenAccDays, lblUBSlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(CsHiddenPrice, lblCSPrice, lblCSClientPrice, lblCSClientYield, iTabIdx, ddlPrdSolveFor, btnCSPrice, btnCSDoc, CsHiddenAccDays, lblCSLimit))
            strJavaScriptRestoreAll.AppendLine(Restore(BAMLHiddenPrice, lblBAMLPrice, lblBAMLClientPrice, lblBAMLClientYield, iTabIdx, ddlPrdSolveFor, btnBAMLPrice, btnBAMLDoc, BAMLHiddenAccDays, lblBAMLlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(BNPPHiddenPrice, lblBNPPPrice, lblBNPPClientPrice, lblBNPPClientYield, iTabIdx, ddlPrdSolveFor, btnBNPPPrice, btnBNPPDoc, BNPPHiddenAccDays, lblBNPPlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(DBIBHiddenPrice, lblDBIBPrice, lblDBIBClientPrice, lblDBIBClientYield, iTabIdx, ddlPrdSolveFor, btnDBIBPrice, btnDBIBDoc, DBIBHiddenAccDays, lblDBIBLimit))
            strJavaScriptRestoreAll.AppendLine(Restore(OCBCHiddenPrice, lblOCBCPrice, lblOCBCClientPrice, lblOCBCClientYield, iTabIdx, ddlPrdSolveFor, btnOCBCprice, btnOCBCDoc, OCBCHiddenAccDays, lblOCBClimit))
            strJavaScriptRestoreAll.AppendLine(Restore(CITIHiddenPrice, lblCITIPrice, lblCITIClientPrice, lblCITIClientYield, iTabIdx, ddlPrdSolveFor, btnCITIprice, btnCITIDoc, CITIHiddenAccDays, lblCITIlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(LEONTEQHiddenPrice, lblLEONTEQPrice, lblLEONTEQClientPrice, lblLEONTEQClientYield, iTabIdx, ddlPrdSolveFor, btnLEONTEQprice, btnLEONTEQDoc, LEONTEQHiddenAccDays, lblLEONTEQlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(COMMERZHiddenPrice, lblCOMMERZPrice, lblCOMMERZClientPrice, lblCOMMERZClientYield, iTabIdx, ddlPrdSolveFor, btnCOMMERZprice, btnCOMMERZDoc, COMMERZHiddenAccDays, lblCOMMERZlimit))

            'strJavaScriptRestoreAll.AppendLine(Restore(JpmHiddenPrice, lblJPMPrice, lblJPMClientPrice, lblJPMClientYield, iTabIdx, ddlPrdSolveFor, btnJPMprice, JpmHiddenAccDays))
            'strJavaScriptRestoreAll.AppendLine(Restore(HsbcHiddenPrice, lblHSBCPrice, lblHSBCClientPrice, lblHSBCClientYield, iTabIdx, ddlPrdSolveFor, btnHSBCPrice, HsbcHiddenAccDays))
            'strJavaScriptRestoreAll.AppendLine(Restore(UbsHiddenPrice, lblUBSPrice, lblUBSClientPrice, lblUBSClientYield, iTabIdx, ddlPrdSolveFor, btnUBSPrice, UbsHiddenAccDays))
            'strJavaScriptRestoreAll.AppendLine(Restore(CsHiddenPrice, lblCSPrice, lblCSClientPrice, lblCSClientYield, iTabIdx, ddlPrdSolveFor, btnCSPrice, CsHiddenAccDays))
            'strJavaScriptRestoreAll.AppendLine(Restore(BAMLHiddenPrice, lblBAMLPrice, lblBAMLClientPrice, lblBAMLClientYield, iTabIdx, ddlPrdSolveFor, btnBAMLPrice, BAMLHiddenAccDays))
            'strJavaScriptRestoreAll.AppendLine(Restore(BNPPHiddenPrice, lblBNPPPrice, lblBNPPClientPrice, lblBNPPClientYield, iTabIdx, ddlPrdSolveFor, btnBNPPPrice, BNPPHiddenAccDays))
            'strJavaScriptRestoreAll.AppendLine(Restore(DBIBHiddenPrice, lblDBIBPrice, lblDBIBClientPrice, lblDBIBClientYield, iTabIdx, ddlPrdSolveFor, btnDBIBPrice, DBIBHiddenAccDays))
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptRestoreAll", strJavaScriptRestoreAll.ToString, True)
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "RestoreAll", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Sub btnCancelReq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelReq.Click
        Try
            ResetAll()
            ''<Start | Nikhil M. on 20-Sep-2016: Added for Reset Commentry>
            hdnstrBestClientYeild.Value = ""
            hdnBestIssuer.Value = ""
            hdnBestProvider.Value = ""
            ResetAllChkBox()
            GetCommentary()
            ''<End | Nikhil M. on 20-Sep-2016: Added for Reset Commentry>
            lblerror.Text = ""
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnCancelReq_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCancelReq_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub EnablePage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHdnEnablePage.Click, btnHdnEnablePage2.Click
        Try
            ShowHideConfirmationPopup(False)
        Catch ex As Exception
            lblerror.Text = "EnablePage:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "EnablePage", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub ShowHideDeatils(ByVal blnShowPopup As Boolean)
        Try
            panelELN.Enabled = Not blnShowPopup
            upnl1.Update()
            If TRJPM1.Visible Then
                TRJPM1.Disabled = blnShowPopup
            End If

            If TRHSBC1.Visible Then
                TRHSBC1.Disabled = blnShowPopup
            End If

            If TRUBS1.Visible Then
                TRUBS1.Disabled = blnShowPopup
            End If

            If TRCS1.Visible Then
                TRCS1.Disabled = blnShowPopup
            End If

            If TRBAML1.Visible Then
                TRBAML1.Disabled = blnShowPopup
            End If

            If TRBNPP1.Visible Then
                TRBNPP1.Disabled = blnShowPopup
            End If
            If TRDBIB.Visible Then
                TRDBIB.Disabled = blnShowPopup
            End If
	    If TROCBC1.Visible Then
                TROCBC1.Disabled = blnShowPopup
            End If
            If TRCITI1.Visible Then
                TRCITI1.Disabled = blnShowPopup
            End If
            If TRLEONTEQ1.Visible Then
                TRLEONTEQ1.Disabled = blnShowPopup
            End If
            If TRCOMMERZ1.Visible Then
                TRCOMMERZ1.Disabled = blnShowPopup
            End If
            PanelReprice.Enabled = Not blnShowPopup
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableTabs", " document.getElementById('ctl00_MainContent_tabContainer_header').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            '<Changed by Mohit Lalwani on 1-Feb-2016 for splitting of TAB>

            ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableELNTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelELN').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableDRATab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelDRA').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableAQDQTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelAQDQ').disabled = " + blnShowPopup.ToString.ToLower + ";", True)

            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableELNTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelELN').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableFCNTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelFCN').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableDRATab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelDRA').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableAQTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelAQDQ').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableAQDQTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelDQ').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            '</Changed by Mohit Lalwani on 1-Feb-2016 for splitting of TAB>
        Catch ex As Exception
            lblerror.Text = "ShowHideDeatils:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ShowHideDeatils", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub ShowHideConfirmationPopup(ByVal blnShowPopup As Boolean, Optional ByVal isResetAll As String = "YES")
        Try
            DealConfirmPopup.Visible = blnShowPopup
            txtOrderCmt.Text = ""
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_CaptureOrderComment", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                Case "Y", "YES"
                    rowOrderComment.Visible = True
                Case "N", "NO"
                    rowOrderComment.Visible = False
            End Select
            UPanle11111.Update()
            panelELN.Enabled = Not blnShowPopup
            upnl1.Update()
            If TRJPM1.Visible Then
                TRJPM1.Disabled = blnShowPopup
            End If

            If TRHSBC1.Visible Then
                TRHSBC1.Disabled = blnShowPopup
            End If

            If TRUBS1.Visible Then
                TRUBS1.Disabled = blnShowPopup
            End If

            If TRCS1.Visible Then
                TRCS1.Disabled = blnShowPopup
            End If

            If TRBAML1.Visible Then
                TRBAML1.Disabled = blnShowPopup
            End If

            If TRBNPP1.Visible Then
                TRBNPP1.Disabled = blnShowPopup
            End If
            If TRDBIB.Visible Then
                TRDBIB.Disabled = blnShowPopup
            End If
	     If TROCBC1.Visible Then
                TROCBC1.Disabled = blnShowPopup
            End If
	    
	     If TRCITI1.Visible Then
                TRCITI1.Disabled = blnShowPopup
            End If

            If TRLEONTEQ1.Visible Then
                TRLEONTEQ1.Disabled = blnShowPopup
            End If

            If TRCOMMERZ1.Visible Then
                TRCOMMERZ1.Disabled = blnShowPopup
            End If
            PanelReprice.Enabled = Not blnShowPopup
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableTabs", " document.getElementById('ctl00_MainContent_tabContainer_header').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            '<Changed by Mohit Lalwani on 1-Feb-2016 for splitting of TAB>

            ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableELNTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelELN').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableDRATab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelDRA').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableAQDQTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelAQDQ').disabled = " + blnShowPopup.ToString.ToLower + ";", True)

            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableELNTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelELN').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableFCNTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelFCN').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableDRATab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelDRA').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableAQTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelAQDQ').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableAQDQTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelDQ').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            '</Changed by Mohit Lalwani on 1-Feb-2016 for splitting of TAB>





            'Mohit Lalwani on 1-Aug-2016
            If isResetAll = "YES" Then
                If blnShowPopup = False Then
                    ResetAll()
                End If
            End If
            '/Mohit Lalwani on 1-Aug-2016

            If Not IsNothing(Request.QueryString("EXTLOD")) Then
                If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Then
                    btnDealConfirm.Visible = False
                    btnCapturePoolPrice.Disabled = False
                    btnCapturePoolPrice.Visible = True
                End If
            End If
            ''<Start | Dilkhush/Nikhil M. on 17-Oct-2016: For Hide/Show allocation>
            If IsNothing(Request.QueryString("PoolID")) Then
                grdRMData.Visible = True
                btnAddAllocation.Visible = True
            Else
                If Request.QueryString("PoolID").ToString = "" Then
                    grdRMData.Visible = True
                    btnAddAllocation.Visible = True
                Else
                    grdRMData.Visible = False
                    btnAddAllocation.Visible = False
                End If

            End If
            ''<End | Dilkhush/Nikhil M. on 17-Oct-2016: For Hide/Show>

            btnRedirect.Visible = False
            btnDealConfirm.Visible = True '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
        Catch ex As Exception
            lblerror.Text = "ShowHideConfirmationPopup:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ShowHideConfirmationPopup", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub txtLimitPricePopUpValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLimitPricePopUpValue.TextChanged
        Try
            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            '/Mohit Lalwani on 1-Aug-2016

            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            'If (txtLimitPricePopUpValue.Text.Length - (txtLimitPricePopUpValue.Text.LastIndexOf(".") + 1)) > 4 And CDbl(txtLimitPricePopUpValue.Text) <> Math.Floor(CDbl(txtLimitPricePopUpValue.Text)) Then
            ''Added by Rushikesh D. on 24-Feb-16 JIRAID:EQ-282
            If txtLimitPricePopUpValue.Text.Trim <> "" Then
                If (txtLimitPricePopUpValue.Text.Length - (txtLimitPricePopUpValue.Text.LastIndexOf(".") + 1)) > 4 And CDbl(txtLimitPricePopUpValue.Text) <> Math.Floor(CDbl(txtLimitPricePopUpValue.Text)) Then
                    lblerrorPopUp.Text = "Precision of " + lblLimitPricePopUpCaption.Text + " cannot exceed four digits after decimal point."
                End If
            End If
        Catch ex As Exception
            lblerror.Text = "txtLimitPricePopUpValue_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtLimitPricePopUpValue_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub makeThisGridVisible(ByVal grdToBeShown As DataGrid)
        Try
            grdELNRFQ.Visible = False
            grdOrder.Visible = False
            grdToBeShown.Visible = True
            upnlGrid.Update()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "makeThisGridVisible", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub RestoreSolveAll()
        Try
            Dim PriceStateAll As String
            Dim PriceArrayAll() As String
            PriceStateAll = AllHiddenPrice.Value
            PriceArrayAll = Split(PriceStateAll, ";")
            If (PriceArrayAll.Length > 1) Then
                If (PriceArrayAll(0) = "Enable") Then
                    btnSolveAll.Enabled = True
                    btnSolveAll.CssClass = "btn"
                Else
                    btnSolveAll.Enabled = False
                    btnSolveAll.CssClass = "btnDisabled"
                End If

                If (PriceArrayAll(1) = "Enable") Then
                Else
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopPriceAllWaitLoader", "try{ document.getElementById('PriceAllWait').style.visibility = 'hidden'; } catch(e){}", True) 		'Mohit Lalwani on 26-Oct-2016
                End If
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "RestoreSolveAll", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer, add-on parameter>

    Public Function Restore(ByVal hiddenPriceCsv As HiddenField, ByVal lblIssuerPrice As Label, ByVal lblIssuerClientPrice As Label, _
                        ByVal lblIssuerClientYield As Label, ByVal tabIndex As Integer, ByRef ddlPrdSolveFor As Telerik.Web.UI.RadDropDownList, _
                        ByVal btnPrice As Button, ByVal btnDoc As Button, Optional ByVal HiddenAccDaysCsv As HiddenField = Nothing, Optional ByVal lblMinMaxSize As Label = Nothing) As String
        Try
            Dim PriceArray() As String
            PriceArray = Split(hiddenPriceCsv.Value, ";")
            If (PriceArray.Length > 1) Then
                If (PriceArray(0) = "Rejected") Then
                    lblIssuerPrice.Text = PriceArray(0)
                    '<AvinashG. on 18-May-2016:  FA-1430 - Easily changable color for Rejected and Timeout cases(remove red)>
                    'lblIssuerPrice.ForeColor = System.Drawing.Color.Red
                    lblIssuerPrice.CssClass = lblIssuerPrice.CssClass.Replace("lblPrice", "").Replace("lblRejected", "").Replace("lblTimeout", "") + " lblRejected"
                    '</AvinashG. on 18-May-2016:  FA-1430 - Easily changable color for Rejected and Timeout cases(remove red)>
                ElseIf (PriceArray(0) = "Timeout") Then
                    lblIssuerPrice.Text = PriceArray(0)
                    '<AvinashG. on 18-May-2016:  FA-1430 - Easily changable color for Rejected and Timeout cases(remove red)>
                    'lblIssuerPrice.ForeColor = System.Drawing.Color.Red
                    lblIssuerPrice.CssClass = lblIssuerPrice.CssClass.Replace("lblPrice", "").Replace("lblRejected", "").Replace("lblTimeout", "") + " lblTimeout"
                    '</AvinashG. on 18-May-2016:  FA-1430 - Easily changable color for Rejected and Timeout cases(remove red)>
                Else
                    lblIssuerPrice.Text = SetNumberFormat((Split(PriceArray(0).ToString, ",")(0)), 2) ''<Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    '<AvinashG. on 18-May-2016:  FA-1430 - Easily changable color for Rejected and Timeout cases(remove red)>
                    'lblIssuerPrice.ForeColor = System.Drawing.Color.Green



                    lblIssuerPrice.CssClass = lblIssuerPrice.CssClass.Replace("lblPrice", "").Replace("lblRejected", "").Replace("lblTimeout", "") + " lblPrice"
                    '</AvinashG. on 18-May-2016:  FA-1430 - Easily changable color for Rejected and Timeout cases(remove red)>
                    If (lblIssuerPrice.Text <> "") Then

                        '<AvinashG. on 27-Sep-2016: TEMPORARY----------------------------AS MS WORD IS NOT AVAILABLE ON AZURE CLOUD>
                        If grdELNRFQ.Columns(grdELNRFQEnum.GenerateDoc).Visible = False Then
                            btnDoc.Visible = False '<RiddhiS. on 17-Sep-2016 to show Document link button when price arrives />
                        Else
                            btnDoc.Visible = True '<RiddhiS. on 17-Sep-2016 to show Document link button when price arrives />
                        End If
                        '</AvinashG. on 27-Sep-2016: TEMPORARY---------------------------->

                        If ddlPrdSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE" Then
                            lblIssuerClientPrice.Text = SetNumberFormat((CDbl(lblIssuerPrice.Text) + CDbl(txtUpfrontELN.Text)), 2)
                            '<AvinashG. on 02-Nov-2015: COde Optimization>
                            ''lblIssuerClientYield.Text = SetNumberFormat(Split(HiddenAccDaysCsv.Value, "^")(1), 2) ''Index 1 contains yield ''DK 16sep
                            lblIssuerClientYield.Text = SetNumberFormat(Split(HiddenAccDaysCsv.Value, "^")(1), 4) ''Index 1 contains yield
                            'lblIssuerClientYield.Text = get_ELN_ClientYield(CDbl(lblIssuerClientPrice.Text))
                            '</AvinashG. on 02-Nov-2015: COde Optimization>
                            '<Added By Mohit/Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>  
                            ''Dilkhush/Avinash 22Feb2016 added if condition to handle string to double conversion
                            ''lblMinMaxSize.Text = convertNotionalintoShort(CDbl(Split(PriceArray(0).ToString, ",")(1)), "MIN") + " / " + convertNotionalintoShort(CDbl(Split(PriceArray(0).ToString, ",")(2)), "MAX") '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                            lblMinMaxSize.Text = convertNotionalintoShort((If(Split(PriceArray(0).ToString, ",")(1).ToString = "", 0, CDbl(Split(PriceArray(0).ToString, ",")(1)))), "MIN") + " / " + convertNotionalintoShort((If(Split(PriceArray(0).ToString, ",")(2).ToString = "", 0, CDbl(Split(PriceArray(0).ToString, ",")(2)))), "MAX") '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                            lblMinMaxSize.ToolTip = Split(PriceArray(0).ToString, ",")(1) + " / " + Split(PriceArray(0).ToString, ",")(2) '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                            lblMinMaxSize.Visible = True
                            setMinMaxCurrency()
                            '<\Added By Mohit/Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> 

                        Else
                            lblIssuerClientPrice.Text = SetNumberFormat((CDbl(txtELNPrice.Text) + CDbl(txtUpfrontELN.Text)), 2)
                            '<AvinashG. on 02-Nov-2015: COde Optimization>
                            ''lblIssuerClientYield.Text = SetNumberFormat(Split(HiddenAccDaysCsv.Value, "^")(1), 2) 
                            lblIssuerClientYield.Text = SetNumberFormat(Split(HiddenAccDaysCsv.Value, "^")(1), 4) '''DK
                            '<AvinashG. on 02-Nov-2015: COde Optimization>
                            '<Added By Mohit/Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>       
                            ''Dilkhush/Avinash 22Feb2016 added if condition to handle string to double conversion
                            '' lblMinMaxSize.Text = convertNotionalintoShort(CDbl(Split(PriceArray(0).ToString, ",")(1)), "MIN") + " / " + convertNotionalintoShort(CDbl(Split(PriceArray(0).ToString, ",")(2)), "MAX") '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                            lblMinMaxSize.Text = convertNotionalintoShort((If(Split(PriceArray(0).ToString, ",")(1).ToString = "", 0, CDbl(Split(PriceArray(0).ToString, ",")(1)))), "MIN") + " / " + convertNotionalintoShort((If(Split(PriceArray(0).ToString, ",")(2).ToString = "", 0, CDbl(Split(PriceArray(0).ToString, ",")(2)))), "MAX") '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                            lblMinMaxSize.ToolTip = Split(PriceArray(0).ToString, ",")(1) + " / " + Split(PriceArray(0).ToString, ",")(2) ' + "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                            lblMinMaxSize.Visible = True
                            setMinMaxCurrency()
                            '<\Added By Mohit/Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> 
                        End If
                    Else
                        lblIssuerClientYield.Text = ""
                        lblIssuerClientPrice.Text = ""
                    End If
                End If

                If (PriceArray(1) = "Disable") Then
                    btnPrice.Enabled = False
                    btnPrice.CssClass = "btnDisabled"
                Else
                    btnPrice.Enabled = True
                    btnPrice.CssClass = "btn"
                End If

                Dim waitCtrlId As String = btnPrice.ID.ToUpper.Replace("BTN", "").Replace("PRICE", "") & "wait"
                If btnPrice.Visible Then
                    If (PriceArray(3) = "Disable") Then
                        Restore = "try{ document.getElementById('" & waitCtrlId & "').style.visibility = 'hidden'; }catch(e){}"  		'Mohit Lalwani on 26-Oct-2016
                    Else 
                        Restore = "try{ document.getElementById('" & waitCtrlId & "').style.visibility = 'visible'; }catch(e){}"                		'Mohit Lalwani on 26-Oct-2016
                    End If
                End If
                btnPrice.Text = PriceArray(4)
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Restore", ErrorLevel.High)
            Throw ex
        End Try
    End Function
    '<Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> +-     
    Private Function setMinMaxCurrency() As String
        Dim ExchangeCcy As String
      
        If chkQuantoCcy.Checked Then
            ExchangeCcy = ddlQuantoCcy.SelectedValue
        Else
            ExchangeCcy = lblELNBaseCcy.Text
        End If

        lblRangeCcy.Text = "Min/Max(<B>" + ExchangeCcy + "</B>)"
    End Function
    '<\Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>       
    '<Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>       
    Public Function convertNotionalintoShort(ByVal minDoub As Double, ByVal MinOrMax As String) As String
        If (MinOrMax = "MIN") Then
            ''Avinash 28Dec2015 For leng roundup and roundDown       
            Return If(minDoub >= 1000000, FormatNumber(RoundUp(minDoub / 1000000.0, 1), 1) + "M", FormatNumber(RoundUp(minDoub / 1000.0, 1), 1) + "K")
        Else
            ''Avinash 28Dec2015 For leng roundup and roundDown       
            Return If(minDoub >= 1000000, FormatNumber(RoundDown(minDoub / 1000000.0, 1), 1) + "M", FormatNumber(RoundDown(minDoub / 1000.0, 1), 1) + "K")
        End If
    End Function
    '<\Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> 

    Public Function CheckBestPrice() As Boolean
        Try
            Dim temp As HiddenField
            temp = getBestPrice(JpmHiddenPrice, HsbcHiddenPrice)
            temp = getBestPrice(temp, UbsHiddenPrice)
            temp = getBestPrice(temp, CsHiddenPrice)
            temp = getBestPrice(temp, BNPPHiddenPrice)
            temp = getBestPrice(temp, BAMLHiddenPrice)
            temp = getBestPrice(temp, DBIBHiddenPrice)
            temp = getBestPrice(temp, OCBCHiddenPrice)
            temp = getBestPrice(temp, CITIHiddenPrice)
            temp = getBestPrice(temp, LEONTEQHiddenPrice)
            temp = getBestPrice(temp, COMMERZHiddenPrice)
            If temp.ID Is Nothing Then
            Else
                If (temp.ID.ToUpper.Contains("JPM")) Then
                    TRJPM1.Attributes.Add("class", "lblBestPrice")
                    TRHSBC1.Attributes.Remove("class")
                    TRUBS1.Attributes.Remove("class")
                    TRCS1.Attributes.Remove("class")
                    TRBNPP1.Attributes.Remove("class")
                    TRBAML1.Attributes.Remove("class")
                    TRDBIB.Attributes.Remove("class")
		    TROCBC1.Attributes.Remove("class")
                    TRCITI1.Attributes.Remove("class")
                    TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
                    ''<Nikhil M : 28Sep16 >
                    If Not chkHSBC.Checked And Not chkUBS.Checked And Not chkCS.Checked And _
                    Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And Not chkOCBC.Checked And _
                    Not chkCITI.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblJPMPrice.Text          ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblJPMClientYield.Text ''<Nikhil M. on 17-Sep-2016: >
			                    hdnBestProvider.Value = "JPM"
                    End If
                ElseIf (temp.ID.ToUpper.Contains("HSBC")) Then
                    TRJPM1.Attributes.Remove("class")
                    TRHSBC1.Attributes.Add("class", "lblBestPrice")
                    TRUBS1.Attributes.Remove("class")
                    TRCS1.Attributes.Remove("class")
                    TRBNPP1.Attributes.Remove("class")
                    TRBAML1.Attributes.Remove("class")
                    TRDBIB.Attributes.Remove("class")
		    TROCBC1.Attributes.Remove("class")
                    TRCITI1.Attributes.Remove("class")
                    TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkUBS.Checked And Not chkCS.Checked And _
                   Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And Not chkOCBC.Checked And _
                   Not chkCITI.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblHSBCPrice.Text           ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblHSBCClientYield.Text   ''<Nikhil M. on 17-Sep-2016: >
			                    hdnBestProvider.Value = "HSBC"
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("UBS")) Then
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Add("class", "lblBestPrice")
                        TRCS1.Attributes.Remove("class")
                        TRBNPP1.Attributes.Remove("class")
                        TRBAML1.Attributes.Remove("class")
                        TRDBIB.Attributes.Remove("class")
                        TROCBC1.Attributes.Remove("class")
                        TRCITI1.Attributes.Remove("class")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkCS.Checked And _
                  Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And Not chkOCBC.Checked And _
                  Not chkCITI.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblUBSPrice.Text           ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblUBSClientYield.Text  ''<Nikhil M. on 17-Sep-2016: >
			hdnBestProvider.Value = "UBS"
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("CS")) Then
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Remove("class")
                        TRCS1.Attributes.Add("class", "lblBestPrice")
                        TRBNPP1.Attributes.Remove("class")
                        TRBAML1.Attributes.Remove("class")
                        TRDBIB.Attributes.Remove("class")
                        TROCBC1.Attributes.Remove("class")
                        TRCITI1.Attributes.Remove("class")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
                  Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And Not chkOCBC.Checked And _
                  Not chkCITI.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblCSPrice.Text         ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblCSClientYield.Text ''<Nikhil M. on 17-Sep-2016: >
			hdnBestProvider.Value = "CS"
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("BNPP")) Then
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Remove("class")
                        TRCS1.Attributes.Remove("class")
                        TRBNPP1.Attributes.Add("class", "lblBestPrice")
                        TRBAML1.Attributes.Remove("class")
                        TRDBIB.Attributes.Remove("class")
                        TROCBC1.Attributes.Remove("class")
                        TRCITI1.Attributes.Remove("class")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
                Not chkCS.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And Not chkOCBC.Checked And _
                Not chkCITI.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblBNPPPrice.Text         ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblBNPPClientYield.Text ''<Nikhil M. on 17-Sep-2016: >
			hdnBestProvider.Value = "BNPP"
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("BAML")) Then
                        ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "best", "alert('BAML is Best')", True)
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Remove("class")
                        TRCS1.Attributes.Remove("class")
                        TRBNPP1.Attributes.Remove("class")
                        TRBAML1.Attributes.Add("class", "lblBestPrice")
                        TRDBIB.Attributes.Remove("class")
                        TROCBC1.Attributes.Remove("class")
                        TRCITI1.Attributes.Remove("class")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
               Not chkCS.Checked And Not chkBNPP.Checked And Not chkDBIB.Checked And Not chkOCBC.Checked And _
               Not chkCITI.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblBAMLPrice.Text         ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblBAMLClientYield.Text ''<Nikhil M. on 17-Sep-2016: >
			hdnBestProvider.Value = "BAML"
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("DBIB")) Then ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD> ''Comparing with ID
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Remove("class")
                        TRCS1.Attributes.Remove("class")
                        TRBNPP1.Attributes.Remove("class")
                        TRBAML1.Attributes.Remove("class")
                        TRDBIB.Attributes.Add("class", "lblBestPrice")
                        TROCBC1.Attributes.Remove("class")
                        TRCITI1.Attributes.Remove("class")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
          Not chkCS.Checked And Not chkBNPP.Checked And Not chkBAML.Checked And Not chkOCBC.Checked And _
          Not chkCITI.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblDBIBPrice.Text         ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblDBIBClientYield.Text ''<Nikhil M. on 17-Sep-2016: >
			hdnBestProvider.Value = "DBIB"
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("OCBC")) Then
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Remove("class")
                        TRCS1.Attributes.Remove("class")
                        TRBNPP1.Attributes.Remove("class")
                        TRBAML1.Attributes.Remove("class")
                        TRDBIB.Attributes.Remove("class")
                        TROCBC1.Attributes.Add("class", "lblBestPrice")
                        TRCITI1.Attributes.Remove("class")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
        Not chkCS.Checked And Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And _
        Not chkCITI.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblOCBCPrice.Text         ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblOCBCClientYield.Text ''<Nikhil M. on 17-Sep-2016: >
			hdnBestProvider.Value = "OCBC"
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("CITI")) Then
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Remove("class")
                        TRCS1.Attributes.Remove("class")
                        TRBNPP1.Attributes.Remove("class")
                        TRBAML1.Attributes.Remove("class")
                        TRDBIB.Attributes.Remove("class")
                        TROCBC1.Attributes.Remove("class")
                        TRCITI1.Attributes.Add("class", "lblBestPrice")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
       Not chkCS.Checked And Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And _
       Not chkOCBC.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblCITIPrice.Text         ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblCITIClientYield.Text ''<Nikhil M. on 17-Sep-2016: >
			hdnBestProvider.Value = "CITI"
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("LEONTEQ")) Then
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Remove("class")
                        TRCS1.Attributes.Remove("class")
                        TRBNPP1.Attributes.Remove("class")
                        TRBAML1.Attributes.Remove("class")
                        TRDBIB.Attributes.Remove("class")
                        TROCBC1.Attributes.Remove("class")
                        TRCITI1.Attributes.Remove("class")
                        TRLEONTEQ1.Attributes.Add("class", "lblBestPrice")
                    TRCOMMERZ1.Attributes.Remove("class")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
       Not chkCS.Checked And Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And _
       Not chkOCBC.Checked And Not chkCITI.Checked And Not chkCOMMERZ.Checked Then
                        hdnBestIssuer.Value = lblLEONTEQPrice.Text         ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblLEONTEQClientYield.Text ''<Nikhil M. on 17-Sep-2016: >
			hdnBestProvider.Value = "LEONTEQ"
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("COMMERZ")) Then
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Remove("class")
                        TRCS1.Attributes.Remove("class")
                        TRBNPP1.Attributes.Remove("class")
                        TRBAML1.Attributes.Remove("class")
                        TRDBIB.Attributes.Remove("class")
                        TROCBC1.Attributes.Remove("class")
                        TRCITI1.Attributes.Remove("class")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Add("class", "lblBestPrice")
		     ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
                        Not chkCS.Checked And Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And _
                        Not chkOCBC.Checked And Not chkCITI.Checked And Not chkLEONTEQ.Checked Then
                        hdnBestIssuer.Value = lblCOMMERZPrice.Text         ''<Nikhil M. on 17-Sep-2016: >
                        hdnstrBestClientYeild.Value = lblCOMMERZClientYield.Text ''<Nikhil M. on 17-Sep-2016: >
			hdnBestProvider.Value = "COMMERZ"
                    End If
                    End If
                    GetCommentary() ''<Nikhil M. on 17-Sep-2016:Added >
                    checkDuplicate(temp)
                End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "CheckBestPrice", ErrorLevel.High)
            Throw ex
        End Try
    End Function
    Public Function getBestPrice(ByVal hiddenPriceCsv1 As HiddenField, ByVal hiddenPriceCsv2 As HiddenField) As HiddenField
        'ELN
        'Strike        Lower Strike
        'Price         Less price

        'Accu
        'Strike        Lower Strike
        'Upfront       Higher Upfront

        'Decu
        'Strike        Higher Strike
        'Upfront       Higher Upfront

        'DRA/FCN
        'Price         Less Price
        'Coupon        more Coupon
        'Strike        lower Strike

        Try
            Dim PriceArray1() As String
            Dim PriceArray2() As String
            Dim Value1 As Double
            Dim Value2 As Double
            PriceArray1 = Split(hiddenPriceCsv1.Value, ";")
            If (PriceArray1.Length > 1) Then
                '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                If (Split(PriceArray1(0), ",")(0) = "Rejected") Then
                    Value1 = 0.0
                    '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>	
                ElseIf (Split(PriceArray1(0), ",")(0) = "Timeout") Then
                    Value1 = 0.0
                Else
                    If (Split(PriceArray1(0), ",")(0) <> "") Then
                        Value1 = CDbl(Split(PriceArray1(0), ",")(0).ToString)  '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    Else
                        Value1 = 0.0
                    End If
                End If
            Else
                Value1 = 0.0
            End If
            PriceArray2 = Split(hiddenPriceCsv2.Value, ";")
            If (PriceArray2.Length > 1) Then
                '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                If (Split(PriceArray2(0), ",")(0) = "Rejected") Then
                    Value2 = 0.0
                    '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                ElseIf (Split(PriceArray2(0), ",")(0) = "Timeout") Then
                    Value2 = 0.0
                Else
                    '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    If (Split(PriceArray2(0), ",")(0) <> "") Then
                        Value2 = CDbl(Split(PriceArray2(0), ",")(0).ToString) '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    Else
                        Value2 = 0.0
                    End If
                End If
            Else
                Value2 = 0.0
            End If
            If Value1 = 0.0 And Value2 = 0.0 Then
                Return New HiddenField()
            End If

            If Value1 = 0.0 And Value2 > 0.0 Then
                Return hiddenPriceCsv2
            ElseIf Value2 = 0.0 And Value1 > 0.0 Then
                Return hiddenPriceCsv1
            End If


            If Value2 > Value1 Then
                Return hiddenPriceCsv1
            Else
                Return hiddenPriceCsv2
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "getBestPrice", ErrorLevel.High)
            Throw ex
        End Try

    End Function
    Public Function checkDuplicate(ByVal bestPriceCSV As HiddenField) As String
        Try
            Dim PriceArray1() As String
            Dim Value1 As Double
            PriceArray1 = Split(bestPriceCSV.Value, ";")
            Value1 = CDbl(Split(PriceArray1(0), ",")(0).ToString) '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
            compareDuplicate(Value1, JpmHiddenPrice)
            compareDuplicate(Value1, HsbcHiddenPrice)
            compareDuplicate(Value1, UbsHiddenPrice)
            compareDuplicate(Value1, CsHiddenPrice)
            compareDuplicate(Value1, BNPPHiddenPrice)
            compareDuplicate(Value1, BAMLHiddenPrice)
            compareDuplicate(Value1, DBIBHiddenPrice)
	    compareDuplicate(Value1, OCBCHiddenPrice)
            compareDuplicate(Value1, CITIHiddenPrice)
            compareDuplicate(Value1, LEONTEQHiddenPrice)
            compareDuplicate(Value1, COMMERZHiddenPrice)
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "checkDuplicate", ErrorLevel.High)
            Throw ex
        End Try
    End Function
    Public Function compareDuplicate(ByVal bestValue As Double, ByVal PriceCSV As HiddenField) As String
        Try
            Dim PriceArray2() As String
            Dim Value2 As Double
            PriceArray2 = Split(PriceCSV.Value, ";")
            If (PriceArray2.Length > 1) Then
                '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                If (Split(PriceArray2(0), ",")(0) = "Rejected") Then
                    Value2 = 0.0
                    '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                ElseIf (Split(PriceArray2(0), ",")(0) = "Timeout") Then
                    Value2 = 0.0
                Else
                    '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    If (Split(PriceArray2(0), ",")(0) <> "") Then
                        Value2 = CDbl(Split(PriceArray2(0), ",")(0).ToString) '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    Else
                        Value2 = 0.0
                    End If
                End If
            Else
                Value2 = 0.0
            End If
            If bestValue = Value2 Then
                setDuplicate(PriceCSV)
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "compareDuplicate", ErrorLevel.High)
            Throw ex
        End Try
    End Function
    Public Function setDuplicate(ByVal temp As HiddenField) As Boolean
        If (temp.ID.ToUpper.Contains("JPM")) Then
            TRJPM1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("HSBC")) Then
            TRHSBC1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("UBS")) Then
            TRUBS1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("CS")) Then
            TRCS1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("BNPP")) Then
            TRBNPP1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("BAML")) Then
            TRBAML1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("DBIB")) Then
            TRDBIB.Attributes.Add("class", "lblBestPrice")
	ElseIf (temp.ID.ToUpper.Contains("OCBC")) Then
            TROCBC1.Attributes.Add("class", "lblBestPrice")
	ElseIf (temp.ID.ToUpper.Contains("CITI")) Then
            TRCITI1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("LEONTEQ")) Then
            TRLEONTEQ1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("COMMERZ")) Then
            TRCOMMERZ1.Attributes.Add("class", "lblBestPrice")
        End If
    End Function
    Public Sub getRange()
        Dim Product_name As String
        Dim ExchangeCcy As String
        Dim dtRange As DataTable
        Try
            dtRange = New DataTable("RangeLimit")
            Product_name = "ELN"

            If chkQuantoCcy.Checked Then
                ExchangeCcy = ddlQuantoCcy.SelectedValue
            Else
                ExchangeCcy = lblELNBaseCcy.Text
            End If
            lblRangeCcy.Text = "Min/Max(<B>" + ExchangeCcy + "</B>)"
            Select Case objELNRFQ.Get_EQCPRD_Limit(Product_name, ExchangeCcy, dtRange)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtRange.Rows.Count > 0 Then
                        Dim limit As String
                        Dim minDoub As Double
                        Dim maxDoub As Double
                        getLimit("JPM", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblJPMlimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblJPMlimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                        getLimit("UBS", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblUBSlimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblUBSlimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                        getLimit("CS", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblCSLimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblCSLimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                        getLimit("BNPP", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblBNPPlimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblBNPPlimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                        getLimit("HSBC", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblHSBClimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblHSBClimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                        'getLimit("DBIB", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name) ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                        getLimit("DB", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblDBIBLimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblDBIBLimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                        getLimit("BAML", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblBAMLlimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblBAMLlimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
			 getLimit("OCBC", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblOCBClimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblOCBClimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
			
			                        getLimit("CITI", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblCITIlimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblCITIlimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                        getLimit("LEONTEQ", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblLEONTEQlimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblLEONTEQlimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                        getLimit("COMMERZ", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblCOMMERZlimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblCOMMERZlimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                    Else
                        lblJPMlimit.Text = "N.A."
                        lblBNPPlimit.Text = "N.A."
                        lblUBSlimit.Text = "N.A."
                        lblBNPPlimit.Text = "N.A."
                        lblHSBClimit.Text = "N.A."
                        lblCSLimit.Text = "N.A."
                        lblBAMLlimit.Text = "N.A."
                        lblDBIBLimit.Text = "N.A."
                        lblOCBClimit.Text = "N.A."
                        lblCITIlimit.Text = "N.A."
                        lblLEONTEQlimit.Text = "N.A."
                        lblCOMMERZlimit.Text = "N.A."
                        lblJPMlimit.ToolTip = ""
                        lblBNPPlimit.ToolTip = ""
                        lblUBSlimit.ToolTip = ""
                        lblBNPPlimit.ToolTip = ""
                        lblHSBClimit.ToolTip = ""
                        lblCSLimit.ToolTip = ""
                        lblBAMLlimit.ToolTip = ""
                        lblDBIBLimit.ToolTip = ""
			lblOCBClimit.ToolTip = ""
                        lblCITIlimit.ToolTip = ""
                        lblLEONTEQlimit.ToolTip = ""
                        lblCOMMERZlimit.ToolTip = ""
                        'Session("MinLimit") = 0
                        'Session("MaxLimit") = 0
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    lblJPMlimit.Text = "N.A."
                    lblBNPPlimit.Text = "N.A."
                    lblUBSlimit.Text = "N.A."
                    lblBNPPlimit.Text = "N.A."
                    lblHSBClimit.Text = "N.A."
                    lblCSLimit.Text = "N.A."
                    lblBAMLlimit.Text = "N.A."
                    lblDBIBLimit.Text = "N.A."
		    lblOCBClimit.Text = "N.A."
                    lblCITIlimit.Text = "N.A."
                    lblLEONTEQlimit.Text = "N.A."
                    lblCOMMERZlimit.Text = "N.A."
                    lblJPMlimit.ToolTip = ""
                    lblBNPPlimit.ToolTip = ""
                    lblUBSlimit.ToolTip = ""
                    lblBNPPlimit.ToolTip = ""
                    lblHSBClimit.ToolTip = ""
                    lblCSLimit.ToolTip = ""
                    lblBAMLlimit.ToolTip = ""
                    lblDBIBLimit.ToolTip = ""
		    lblOCBClimit.ToolTip = ""
                    lblCITIlimit.ToolTip = ""
                    lblLEONTEQlimit.ToolTip = ""
                    lblCOMMERZlimit.ToolTip = ""
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                    lblJPMlimit.Text = "N.A."
                    lblBNPPlimit.Text = "N.A."
                    lblUBSlimit.Text = "N.A."
                    lblBNPPlimit.Text = "N.A."
                    lblHSBClimit.Text = "N.A."
                    lblCSLimit.Text = "N.A."
                    lblBAMLlimit.Text = "N.A."
                    lblDBIBLimit.Text = "N.A."
                    lblOCBClimit.Text = "N.A."
                    lblCITIlimit.Text = "N.A."
                    lblLEONTEQlimit.Text = "N.A."
                    lblCOMMERZlimit.Text = "N.A."
                    lblJPMlimit.ToolTip = ""
                    lblBNPPlimit.ToolTip = ""
                    lblUBSlimit.ToolTip = ""
                    lblBNPPlimit.ToolTip = ""
                    lblHSBClimit.ToolTip = ""
                    lblCSLimit.ToolTip = ""
                    lblBAMLlimit.ToolTip = ""
                    lblDBIBLimit.ToolTip = ""
		    lblOCBClimit.ToolTip = ""
                    lblCITIlimit.ToolTip = ""
                    lblLEONTEQlimit.ToolTip = ""
                    lblCOMMERZlimit.ToolTip = ""
            End Select
            clearShareData()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "getRange", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Sub clearShareData()
        'btnMail.Visible = True
	''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
        ''If ddlShare.SelectedItem Is Nothing OrElse ddlShare.SelectedItem.Value = "" Then
        If ddlShare.SelectedValue Is Nothing OrElse ddlShare.SelectedValue = "" Then
            lblELNBaseCcy.Text = ""
            lblComentry.Text = ""
            lblExchangeVal.Text = ""
            lblPRRVal.Text = ""
            lblJPMlimit.Text = "N.A."
            lblBNPPlimit.Text = "N.A."
            lblUBSlimit.Text = "N.A."
            lblBNPPlimit.Text = "N.A."
            lblHSBClimit.Text = "N.A."
            lblCSLimit.Text = "N.A."
            lblBAMLlimit.Text = "N.A."
            lblDBIBLimit.Text = "N.A."
	    lblOCBClimit.Text = "N.A."
            lblCITIlimit.Text = "N.A."
            lblLEONTEQlimit.Text = "N.A."
            lblCOMMERZlimit.Text = "N.A."
            lblJPMlimit.ToolTip = ""
            lblBNPPlimit.ToolTip = ""
            lblUBSlimit.ToolTip = ""
            lblBNPPlimit.ToolTip = ""
            lblHSBClimit.ToolTip = ""
            lblCSLimit.ToolTip = ""
            lblBAMLlimit.ToolTip = ""
            lblDBIBLimit.ToolTip = ""
	     lblOCBClimit.ToolTip = ""
            lblCITIlimit.ToolTip = ""
            lblLEONTEQlimit.ToolTip = ""
            lblCOMMERZlimit.ToolTip = ""
            lblRangeCcy.Text = "Min/Max()"
            lblQuantity.Text = "Notional ()"
            'btnMail.Visible = False
            pnlReprice.Update()
            upnlCommentry.Update()
        End If

    End Sub
    Private Sub getLimit(ByVal sPPCode As String, ByVal dt As DataTable, ByRef dblMinLim As Double, ByRef dblMaxLim As Double, ByVal sCcy As String, ByVal sPrd As String)
        Try
            dblMinLim = 0
            dblMaxLim = 0
            Dim result() As DataRow = dt.Select("EQCPPL_PPCode = '" + sPPCode + "'")
            If result.Length < 1 Then
                dblMinLim = 0
                dblMaxLim = 0
                LogException(LoginInfoGV.Login_Info.LoginId, "No limit setup found for " + sPPCode + " for " + sCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
                                         sSelfPath, "getLimit", ErrorLevel.Medium)
            Else
                Dim lmtRow As DataRow = result(0)
                dblMinLim = CDbl(If(IsDBNull(lmtRow("EQCPPL_Minm")), 0, CDbl(lmtRow("EQCPPL_Minm"))))
                dblMaxLim = CDbl(If(IsDBNull(lmtRow("EQCPPL_Maxm")), 0, CDbl(lmtRow("EQCPPL_Maxm"))))
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "getLimit", ErrorLevel.High)
        End Try
    End Sub
    Private Sub rblShareData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblShareData.SelectedIndexChanged
        If rblShareData.SelectedValue = "SHAREDATA" Then
            rowGraphData.Visible = False
            rowShareData.Visible = True
            rowELNCalculator.Visible = False
            Fill_All_Charts()
        ElseIf rblShareData.SelectedValue = "GRAPHDATA" Then
            rowGraphData.Visible = True
            rowShareData.Visible = False
            rowELNCalculator.Visible = False
        Else
            rowGraphData.Visible = False
            rowShareData.Visible = False
            If ((objReadConfig.ReadConfig(dsConfig, "EQC_Show_ELN_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "YES" Or _
                     objReadConfig.ReadConfig(dsConfig, "EQC_Show_ELN_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "Y") And tabContainer.ActiveTabIndex = 0) Then
                rowELNCalculator.Visible = True
            Else
                rowELNCalculator.Visible = False
            End If
        End If
        hideShowRBLShareData()
    End Sub
    Private Sub hideShowRBLShareData()
        Dim count As Integer = 0
        Dim ELNCalc As String = objReadConfig.ReadConfig(dsConfig, "EQC_Show_ELN_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
        Dim tabIndex As Integer = tabContainer.ActiveTabIndex

        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            Case "Y", "YES"
                count = count + 1
            Case "N", "NO"
                rblShareData.Items.FindByValue("SHAREDATA").Attributes.Add("style", "display:none")
        End Select
        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
            Case "Y", "YES"
                count = count + 1
            Case "N", "NO"
                rblShareData.Items.FindByValue("GRAPHDATA").Attributes.Add("style", "display:none")
        End Select
        If tabIndex = 0 Then
            If (ELNCalc = "YES" Or ELNCalc = "Y") Then
                count = count + 1
                rblShareData.Items.FindByValue("calc").Attributes.Add("style", "display:''")
            Else
                rblShareData.Items.FindByValue("calc").Attributes.Add("style", "display:none")
            End If
        End If

    End Sub
    '<AvinashG. on 09-Apr-2015: BoS related function>
    Private Sub setELNPoolData()
        Dim dtPoolDetails As DataTable
        Dim sPoolID As String
        Dim strNewTenorELN As String = String.Empty
        Dim strNewTenorELNType As String = String.Empty
        Try
            dtPoolDetails = New DataTable("PoolDetails")
            If Not IsNothing(Request.QueryString("PoolID")) Then
                sPoolID = Request.QueryString("PoolID")
                If sPoolID.Trim <> "" Then
                    Select Case objELNRFQ.GetELNPoolRecordData(sPoolID, dtPoolDetails)
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                            Dim strType As String = dtPoolDetails.Rows(0)(enumPoolDetails.ELNType).ToString.Trim
                            If strType = "Simple" Then ''Added on 14Nov
                                chkELNType.Checked = False
                                'lblsimpleBarrier.Text = "Simple"
                                txtBarrier.Enabled = False
                                ddlBarrier.Enabled = False
                                txtBarrier.Visible = False
                                ddlBarrier.Visible = False
                            Else

                                chkELNType.Checked = True
                                ' lblsimpleBarrier.Text = "Barrier"
                                txtBarrier.Enabled = True
                                ddlBarrier.Enabled = True
                                txtBarrier.Visible = True
                                ddlBarrier.Visible = True
                                Dim strBarrierType As String = dtPoolDetails.Rows(0)(enumPoolDetails.BarrierType).ToString.Trim
                                ''''<Dilkhush 13May2016 FA-1427>
                                '' ddlBarrier.SelectedValue = strBarrierType
                                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SHOW_DAILYCLOSE_KO", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                    Case "Y", "YES"
                                        ddlBarrier.SelectedValue = strBarrierType
                                    Case "N", "NO"
                                        If strBarrierType.ToUpper <> "DAILY_CLOSE" Then
                                            ddlBarrier.SelectedValue = strBarrierType
                                        End If
                                End Select
                                ''''</Dilkhush 13May2016 FA-1427>

                                Dim strBarrier As String = dtPoolDetails.Rows(0)(enumPoolDetails.BarrierPercentage).ToString.Trim
                                txtBarrier.Text = strBarrier
                            End If
                            Dim strExchng As String = dtPoolDetails.Rows(0)(enumPoolDetails.Exchange).ToString.Trim
                            Dim strShare As String = dtPoolDetails.Rows(0)(enumPoolDetails.Share).ToString.Trim
                            ''Rushikesh 14Jan2016 to set share from pool data
                            setShare(strExchng, strShare)

                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                    'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                    'If ddlShare.SelectedValue IsNot Nothing Then
                                    '    ddlShare.Text = ddlShare.SelectedItem.Text
                                    'End If
                                    lblExchangeVal.Text = setExchangeByShare(ddlShare)

                                Case "N", "NO"
                                    If ddlExchange.SelectedValue = strExchng Then
                                        ddlExchange.SelectedValue = strExchng
                                        'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                        'If ddlShare.SelectedValue IsNot Nothing Then
                                        '    ddlShare.Text = ddlShare.SelectedItem.Text
                                        'End If
                                        lblExchangeVal.Text = setExchangeByShare(ddlShare)
                                    Else
                                        ddlExchange.SelectedValue = strExchng ''ddlShare.Items.IndexOf(ddlShare.Items.FindByText(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Exchange).Text))
                                        '' Fillddl_Share()
                                        'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                        ''ddlShare.Text = ddlShare.Text
                                        'If ddlShare.SelectedValue IsNot Nothing Then
                                        '    ddlShare.Text = ddlShare.SelectedItem.Text
                                        'End If
                                    End If
                            End Select




                            'If ddlExchange.SelectedValue = strExchng Then
                            '    ddlExchange.SelectedValue = strExchng
                            '    ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(dtPoolDetails.Rows(0)(enumPoolDetails.Share).ToString.Trim))
                            '    lblExchangeVal.Text = setExchangeByShare(ddlShare)
                            'Else
                            '    ddlExchange.SelectedValue = strExchng ''ddlShare.Items.IndexOf(ddlShare.Items.FindByText(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Exchange).Text))
                            '    Fillddl_Share()
                            '    ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(dtPoolDetails.Rows(0)(enumPoolDetails.Share).ToString.Trim))
                            '    lblExchangeVal.Text = setExchangeByShare(ddlShare)
                            'End If



                            getCurrency(dtPoolDetails.Rows(0)(enumPoolDetails.Share).ToString.Trim)

                            Dim strQuantoCcy As String = dtPoolDetails.Rows(0)(enumPoolDetails.Currency).ToString.Trim
                            If lblELNBaseCcy.Text = strQuantoCcy Then
                                chkQuantoCcy.Checked = False
                                ddlQuantoCcy.Enabled = False
                                lblQuantity.Text = "Notional (<font style=''>" & lblELNBaseCcy.Text & "</font>)"
                                ddlQuantoCcy.DataSource = Nothing
                                ddlQuantoCcy.DataBind()
                                ddlQuantoCcy.Items.Clear()
                                ddlQuantoCcy.Items.Add(New DropDownListItem(lblELNBaseCcy.Text, lblELNBaseCcy.Text)) 'Mohit Lalwani on 8-Jul-2016
                                ddlQuantoCcy.BackColor = Color.FromArgb(242, 242, 243)
                            Else
                                chkQuantoCcy.Checked = True
                                ddlQuantoCcy.Enabled = True
                                Call Fillddl_QuantoCcy()
                                ddlQuantoCcy.BackColor = Color.White
                                ddlQuantoCcy.SelectedIndex = ddlQuantoCcy.FindItemByText(dtPoolDetails.Rows(0)(enumPoolDetails.Currency).ToString.Trim).Index
                                ''<Rutuja 25April:added to set Quanto ccy  >
                                lblQuantity.Text = "Notional (<font style=''>" & strQuantoCcy & "</font>)"
                                ''</Rutuja 25April:added to set Quanto ccy  >
                            End If

                            Dim strTenor As String = dtPoolDetails.Rows(0)(enumPoolDetails.Tenor).ToString.Trim
                            txtTenor.Text = strTenor

                            '<Added by Rushikesh D. for upfront redirection from Order pool reprice  23_June2015>
                            Dim strUpfront As String = dtPoolDetails.Rows(0)(enumPoolDetails.Upfront).ToString.Trim
                            If strUpfront.Trim <> "" Or strUpfront.Trim <> "&nbsp;" Then
                                txtUpfrontELN.Text = FormatNumber((CDbl(strUpfront) / 100).ToString, 2)
                                ''txtUpfrontELN.Text = FormatNumber(strUpfront, 2)
                            End If
                            '</Added by Rushikesh D. for upfront redirection from Order pool reprice  23_June2015>

                            'For i = 0 To strTenor.Length - 1
                            '    If IsNumeric(strTenor.Substring(i, 1)) = True Then
                            '        strNewTenorELN = strNewTenorELN + strTenor.Substring(i, 1)
                            '    End If
                            'Next
                            'txtTenor.Text = strNewTenorELN
                            'For i = 0 To strTenor.Length - 1
                            '    If IsNumeric(strTenor.Substring(i, 1)) = False Then
                            '        strNewTenorELNType = strNewTenorELNType + strTenor.Substring(i, 1)
                            '    End If
                            'Next
                            ''ddlTenorTypeELN.SelectedItem.Text = dtPoolDetails.Rows(0)(enumPoolDetails.TenorType).ToString.Trim
                            ddlTenorTypeELN.SelectedValue = dtPoolDetails.Rows(0)(enumPoolDetails.TenorType).ToString.Trim

                            Dim strSetDays As String = dtPoolDetails.Rows(0)(enumPoolDetails.SettlementDays).ToString.Trim
                            'Dim strSettleDays As String = CStr(CDbl(strSetDays.Trim.Split(CChar("+"))(1).Trim))

                            '<RiddhiS. on 17-Oct-2016: Block uncommented as Settlement Weeks was not set as per Pool data>
                            If strSetDays = "7" Then
                                ddlSettlementDays.SelectedValue = "1W"
                            ElseIf strSetDays = "14" Then
                                ddlSettlementDays.SelectedValue = "2W"
                            Else
                                ddlSettlementDays.SelectedValue = "1W"
                            End If
                            '</RiddhiS.>


                            txtValueDays.Text = strSetDays

                            '<AvinashG. on 09-Apr-2015: Set Dates>
                            Dim strTradeDate As String = dtPoolDetails.Rows(0)(enumPoolDetails.TradeDate).ToString.Trim
                            If strTradeDate.ToUpper = Today.ToString("dd-MMM-yyyy").ToUpper Then  '<Rutuja S. on 15-Dec-2014: Added to get dates based on trade date>
                                '<By Rushikesh D. on 2-Feb-2016 for setting Trade date as todays>
                                'txtTradeDate.Value = Convert.ToDateTime(strTradeDate).ToString("dd-MMM-yyyy")
                                'Dim strSettDate As String = dtPoolDetails.Rows(0)(enumPoolDetails.SettlementDate).ToString.Trim
                                'txtSettlementDate.Value = Convert.ToDateTime(strSettDate).ToString("dd-MMM-yyyy")

                                'Dim strExpDate As String = dtPoolDetails.Rows(0)(enumPoolDetails.ExpiryDate).ToString.Trim
                                'txtExpiryDate.Value = Convert.ToDateTime(strExpDate).ToString("dd-MMM-yyyy")

                                'Dim strMatDate As String = dtPoolDetails.Rows(0)(enumPoolDetails.MaturityDate).ToString.Trim
                                'txtMaturityDate.Value = Convert.ToDateTime(strMatDate).ToString("dd-MMM-yyyy")
                                txtTradeDate.Value = Convert.ToDateTime(Date.Now()).ToString("dd-MMM-yyyy")
                                GetDates()
                                '</By Rushikesh D. on 2-Feb-2016 for setting Trade date as todays>
                            Else
                                GetDates() '<RiddhiS. on 17-Oct-2016: To set proper Settlement, Expiry and Maturity date>
                                'lblerror.Text = "Pool ID " + sPoolID + " does not have today's trade date."
                            End If
                            '</AvinashG. on 09-Apr-2015: Set Dates>

                            Dim strSolveFor As String = dtPoolDetails.Rows(0)(enumPoolDetails.SolveFor).ToString.Trim
                            If strSolveFor = "Price(%)" Then
                                ddlSolveFor.SelectedValue = "PricePercentage"
                                ''<Rutuja 25April:Added to enable/disable textbox>
                                txtELNPrice.Text = "0.00"
                                txtELNPrice.Enabled = False
                                txtELNPrice.BackColor = Color.FromArgb(242, 242, 243)
                                txtStrike.Enabled = True
                                txtStrike.BackColor = Color.White
                                ''</Rutuja 25April:Added to enable ,/disable textbox>
                                ''<Dilkhush/Rutuja on 11Aug2014:Added to set solve for type  on grid data change>
                                lblSolveForType.Text = "IB Price (%)"
                                ''</Dilkhush/Rutuja on 11Aug2014:Added to set solve for type  on grid data change>
                            Else
                                ddlSolveFor.SelectedValue = "StrikePercentage"
                                ''<Rutuja 25April:Added to enable/disable textbox>
                                txtELNPrice.Enabled = True
                                txtELNPrice.BackColor = Color.White
                                txtStrike.Text = "0.00"
                                txtStrike.Enabled = False
                                txtStrike.BackColor = Color.FromArgb(242, 242, 243)
                                ''</Rutuja 25April:Added to enable/disable textbox>
                                ''<Dilkhush/Rutuja on 11Aug2014:Added to set solve for type  on grid data change>
                                lblSolveForType.Text = "Strike (%)"
                                ''</Dilkhush/Rutuja on 11Aug2014:Added to set solve for type  on grid data change>
                            End If

                            Dim strStrike As String = SetNumberFormat(dtPoolDetails.Rows(0)(enumPoolDetails.StrikePercentage).ToString.Trim, 2)
                            If strStrike = "&nbsp;" Then
                                txtStrike.Text = "0.00"
                            Else
                                txtStrike.Text = strStrike
                            End If

                            Dim strELNPrice As String = SetNumberFormat(dtPoolDetails.Rows(0)(enumPoolDetails.PricePercentage).ToString.Trim, 2)

                            If strELNPrice = "&nbsp;" Then
                                txtELNPrice.Text = "0.00"
                            Else
                                txtELNPrice.Text = strELNPrice
                            End If

                            Dim strOrderqty As String = SetNumberFormat(dtPoolDetails.Rows(0)(enumPoolDetails.Notional).ToString.Trim, 0) ''EQBOSDEV-228   Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                            ''txtQuantity.Text = FormatNumber(strOrderqty, 2)
			                                txtQuantity.Text = SetNumberFormat(strOrderqty, 0) '' ''EQBOSDEV-228  Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F

                            'panelELN.Enabled = False
                            'upnl1.Update()

                            'tblRFQGridHolder.Disabled = True
                            'upnlGrid.Update()
                            resetControlsForPool(False)
                            btnSolveAll.Enabled = False


                            '<Neha M. on 21-Apr-2015: Commented and put code in method to make it reusable>
                            EnableDisableForOrderPoolData(dtPoolDetails.Rows(0)(enumPoolDetails.PPCODE).ToString.Trim.ToUpper)
                            Session.Add("RePricePPName", dtPoolDetails.Rows(0)(enumPoolDetails.PPCODE).ToString.Trim.ToUpper)
                            '</Neha M. on 21-Apr-2015: Commented and put code in method to make it reusable>
                            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                            'getRange()
                            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                            ''Rushikesh D 16sep2015 to display comentry when redirected from pooling page
                            GetCommentary()

                            ''<Nikhil M. on 04-Oct-2016:Added To Set PPR on Order SUmmary>
                            getPRR(strShare)
                            getFlag(strShare)


                            '<RiddhiS. on 09-Nov-2016: To set selected booking branch>
                            If dtPoolDetails.Rows(0)(enumPoolDetails.BookingCenter).ToString.Trim = "RETAIL" Then
                                For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
                                    If ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
                                        ddlBookingBranchPopUpValue.SelectedIndex = iBKC
                                        Exit For
                                    End If
                                Next
                            Else
                                For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
                                    If Not ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
                                        ddlBookingBranchPopUpValue.SelectedIndex = iBKC
                                        Exit For
                                    End If
                                Next
                            End If

                            ddlBookingBranchPopUpValue.Enabled = False

                        Case Else
                            lblerror.Text = "Received invalid Pool ID."
                    End Select

                Else
                    lblerror.Text = "Missing Pool ID."
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '                    lblSolveForType.Text = "IB Price (%)"
    '                Else
    '                    ddlSolveFor.SelectedValue = "StrikePercentage"
    '                    txtELNPrice.Enabled = True
    '                    txtELNPrice.BackColor = Color.White
    '                    txtStrike.Text = "0.0"
    '                    txtStrike.Enabled = False
    '                    txtStrike.BackColor = Color.FromArgb(242, 242, 243)
    '                    lblSolveForType.Text = "Strike (%)"
    '                End If

    '                Dim strStrike As String = SetNumberFormat(dtPoolDetails.Rows(0)(enumPoolDetails.StrikePercentage).ToString.Trim, 2)
    '                If strStrike = "&nbsp;" Then
    '                    txtStrike.Text = "0.0"
    '                Else
    '                    txtStrike.Text = strStrike
    '                End If

    '                Dim strELNPrice As String = SetNumberFormat(dtPoolDetails.Rows(0)(enumPoolDetails.PricePercentage).ToString.Trim, 2)

    '                If strELNPrice = "&nbsp;" Then
    '                    txtELNPrice.Text = "0.0"
    '                Else
    '                    txtELNPrice.Text = strELNPrice
    '                End If

    '                Dim strOrderqty As String = SetNumberFormat(dtPoolDetails.Rows(0)(enumPoolDetails.Notional).ToString.Trim, 2)
    '                txtQuantity.Text = strOrderqty

    '                panelELN.Enabled = False
    '                upnl1.Update()

    '                tblRFQGridHolder.Disabled = True
    '                upnlGrid.Update()

    '                btnSolveAll.Enabled = False

    '                EnableDisableForOrderPoolData(dtPoolDetails.Rows(0)(enumPoolDetails.PPCODE).ToString.Trim.ToUpper)
    '                Session.Add("RePricePPName", dtPoolDetails.Rows(0)(enumPoolDetails.PPCODE).ToString.Trim.ToUpper)
    '                Call getRange()
    '                GetCommentary()
    '            Else
    '                lblerror.Text = "Received invalid Pool ID."
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    Private Sub btnCapturePoolPrice_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCapturePoolPrice.ServerClick
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim sPoolID As String
        Dim sStrike As String
        Try
            If Not IsNothing(Request.QueryString("PoolID")) Then
                sPoolID = Request.QueryString("PoolID")
                hashPPId = New Hashtable
                hashRFQID = New Hashtable

                hashRFQID = CType(Session("Hash_Values"), Hashtable)
                hashPPId = CType(Session("PP_IdTable"), Hashtable)
                lblerror.Text = ""
                lblerrorPopUp.Text = ""
                chkUpfrontOverride.Checked = False
                chkUpfrontOverride.Visible = False
                lblMsgPriceProvider.Text = ""
                Select Case lblIssuerPopUpValue.Text.ToUpper
                    Case "JPM"
                        sStrike = lblJPMPrice.Text
                        Quote_ID = Convert.ToString(Session("JPMQuote"))
                        Session.Remove("JPMQuote")
                    Case "CS"
                        sStrike = lblCSPrice.Text
                        Quote_ID = Convert.ToString(Session("CSQuote"))
                        Session.Remove("CSQuote")
                    Case "UBS"
                        sStrike = lblUBSPrice.Text
                        Quote_ID = Convert.ToString(Session("UBSQuote"))
                        Session.Remove("UBSQuote")
                    Case "HSBC"
                        sStrike = lblHSBCPrice.Text
                        Quote_ID = Convert.ToString(Session("HSBCQuote"))
                        Session.Remove("HSBCQuote")
                    Case "BAML"
                        sStrike = lblBAMLPrice.Text
                        Quote_ID = Convert.ToString(Session("BAMLQuote"))
                        Session.Remove("BAMLQuote")
                    Case "BNPP"
                        sStrike = lblBNPPPrice.Text
                        Quote_ID = Convert.ToString(Session("BNPPQuote"))
                        Session.Remove("BNPPQuote")
                        'Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                    Case "DB"
                        sStrike = lblDBIBPrice.Text
                        Quote_ID = Convert.ToString(Session("DBIBQuote"))
                        Session.Remove("DBIBQuote")
		    Case "OCBC"
                        sStrike = lblOCBCPrice.Text
                        Quote_ID = Convert.ToString(Session("OCBCQuote"))
                        Session.Remove("OCBCQuote")
		    Case "CITI"
                        sStrike = lblCITIPrice.Text
                        Quote_ID = Convert.ToString(Session("CITIQuote"))
                        Session.Remove("CITIQuote")
                    Case "LEONTEQ"
                        sStrike = lblLEONTEQPrice.Text
                        Quote_ID = Convert.ToString(Session("LEONTEQQuote"))
                        Session.Remove("LEONTEQQuote")
                    Case "COMMERZ"
                        sStrike = lblCOMMERZPrice.Text
                        Quote_ID = Convert.ToString(Session("COMMERZQuote"))
                        Session.Remove("COMMERZQuote")
                End Select

                If sPoolID.Trim <> "" Then
                    Dim sNewPrice As String
                    If ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE" Then
                        sNewPrice = lblIssuerPricePopUpValue.Text
                    ElseIf ddlSolveFor.SelectedValue.ToUpper = "STRIKEPERCENTAGE" Then
                        sNewPrice = sStrike
                    End If
                    Select Case objELNRFQ.SetPoolPrice(sPoolID, sNewPrice, LoginInfoGV.Login_Info.LoginId, Quote_ID)
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                            lblerrorPopUp.Text = "Price captured successfully!"
                            lblerrorPopUp.ForeColor = Color.Blue
                            btnCapturePoolPrice.Disabled = True
                        Case Else
                            lblerrorPopUp.Text = "Error occurred while capturing price"
                            lblerrorPopUp.ForeColor = Color.Red
                    End Select
                End If
            End If
        Catch ex As Exception
            lblerrorPopUp.Text = "Error occurred while capturing the price."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCapturePoolPrice_ServerClick", ErrorLevel.High)
        End Try
    End Sub
    Public Sub resetControlsForPool(ByVal flag As Boolean)
        Try



            panelELN.Enabled = flag
            upnl1.Update()


            tblRFQGridHolder.Disabled = Not (flag)
            upnlGrid.Update()
            grdELNRFQ.Enabled = flag
            grdOrder.Enabled = flag

            btnSolveAll.Enabled = flag
            EnableDisableForOrderPoolData("")
        Catch ex As Exception
            lblerrorPopUp.Text = "Error occurred while capturing the price."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "resetControlsAfterRepeice_ServerClick", ErrorLevel.High)
        End Try
    End Sub
    Private Sub btnRedirect_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRedirect.ServerClick
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim orderQuantity As String = ""
        Dim strType As String = ""
        Dim strLimitPrice1 As String = ""
        Dim strLimitPrice2 As String = ""
        Dim strLimitPrice3 As String = ""
        Dim strMargin As String = ""
        Dim strClientPrice As String = ""
        Dim strClientYield As String = ""
        Dim strBookingBranch As String = ""
        Dim strJavaScriptDealClicked As New StringBuilder
        Dim strRMNameforOrderConfirm As String = ""
        Dim strRMEmailIdforOrderConfirm As String = ""
        Dim strLoginUserEmail As String = ""
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)
            lblerror.Text = ""
            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            lblMsgPriceProvider.Text = ""
            Select Case lblIssuerPopUpValue.Text.ToUpper
                Case "JPM"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("JPM")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("JPMQuote"))
                        Session.Remove("JPMQuote")
                    End If
                Case "CS"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("CS")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("CSQuote"))
                        Session.Remove("CSQuote")
                    End If
                Case "UBS"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("UBS")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("UBSQuote"))
                        Session.Remove("UBSQuote")
                    End If
                Case "HSBC"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("HSBC")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("HSBCQuote"))
                        Session.Remove("HSBCQuote")
                    End If
                Case "BAML"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("BAML")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("BAMLQuote"))
                        Session.Remove("BAMLQuote")
                    End If
                Case "BNPP"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("BNPP")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("BNPPQuote"))
                        Session.Remove("BNPPQuote")
                    End If
                    'Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                Case "DB"
                    If Convert.ToString(Session("flag")) = "I" Then
                        'Quote_ID = CStr(hashRFQID(hashPPId("DBIB"))) ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                        Quote_ID = CStr(hashRFQID(hashPPId("DB")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("DBIBQuote"))
                        Session.Remove("DBIBQuote")
                    End If
		Case "OCBC"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("OCBC")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("OCBCQuote"))
                        Session.Remove("OCBCQuote")
                    End If
		 Case "CITI"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("CITI")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("CITIQuote"))
                        Session.Remove("CITIQuote")
                    End If
                Case "LEONTEQ"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("LEONTEQ")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("LEONTEQQuote"))
                        Session.Remove("LEONTEQQuote")
                    End If
                Case "COMMERZ"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("COMMERZ")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("COMMERZQuote"))
                        Session.Remove("COMMERZQuote")
                    End If
            End Select
            Select Case tabContainer.ActiveTabIndex
                Case 0
                    strMargin = txtUpfrontPopUpValue.Text
                    strClientPrice = lblClientPricePopUpValue.Text
                    strClientYield = txtClientYieldPopUpValue.Text
                    strBookingBranch = ddlBookingBranchPopUpValue.SelectedValue
''                    orderQuantity = lblNotionalPopUpValue.Text
orderQuantity = SetNumberFormat(lblNotionalPopUpValue.Text, 0)''EQBOSDEV-228 Added by chaitali removing decimal
                    strRMNameforOrderConfirm = ddlRM.SelectedValue
                    If ddlOrderTypePopUpValue.SelectedItem.Text.ToUpper.Contains("LIMIT") Then
                        strLimitPrice1 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", "")))
                        strLimitPrice2 = ""
                        strLimitPrice3 = ""
                        strType = "Limit"
                    Else
                        strLimitPrice1 = ""
                        strLimitPrice2 = ""
                        strLimitPrice3 = ""
                        strType = "Market"
                    End If
                Case 1
                    strMargin = txtUpfrontPopUpValue.Text.Replace(",", "")
                    strClientPrice = lblClientPricePopUpValue.Text
                    strClientYield = ""
                    strBookingBranch = ddlBookingBranchPopUpValue.SelectedValue
''                    orderQuantity = lblNotionalPopUpValue.Text
                    orderQuantity = SetNumberFormat(lblNotionalPopUpValue.Text, 0) ''EQBOSDEV-228 Added by chaitali removing decimal

                    strRMNameforOrderConfirm = ddlRM.SelectedValue
                    If ddlOrderTypePopUpValue.SelectedItem.Text.ToUpper.Contains("LIMIT") Then
                        ddlBasketSharesPopValue.Visible = True
                        txtLimitPricePopUpValue.Width = New WebControls.Unit(75)
                        ddlBasketSharesPopValue.Width = New WebControls.Unit(100)
                        Select Case ddlBasketSharesPopValue.SelectedIndex
                            Case 0
                                strLimitPrice1 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", "")))
                                strLimitPrice2 = ""
                                strLimitPrice3 = ""
                            Case 1
                                strLimitPrice1 = ""
                                strLimitPrice2 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", "")))
                                strLimitPrice3 = ""
                            Case 2
                                strLimitPrice1 = ""
                                strLimitPrice2 = ""
                                strLimitPrice3 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", "")))
                        End Select
                        strType = "Limit"
                    Else
                        ddlBasketSharesPopValue.Visible = False
                        txtLimitPricePopUpValue.Width = New WebControls.Unit(175)
                        strLimitPrice1 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", "")))
                        strLimitPrice2 = ""
                        strLimitPrice3 = ""
                        strType = "Market"
                    End If
                Case 2
                    strMargin = ""
                    strClientPrice = ""
                    strClientYield = ""
                    strBookingBranch = ddlBookingBranchPopUpValue.SelectedValue
                    ''orderQuantity = lblNotionalPopUpValue.Text
                     orderQuantity = SetNumberFormat(lblNotionalPopUpValue.Text, 0) ''EQBOSDEV-228 Added by chaitali removing decimal
                    strRMNameforOrderConfirm = ddlRM.SelectedValue
                    If ddlOrderTypePopUpValue.SelectedItem.Text.ToUpper.Contains("LIMIT") Then
                        strLimitPrice1 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", "")))
                        strLimitPrice2 = ""
                        strLimitPrice3 = ""
                        strType = "Limit"
                    Else
                        strLimitPrice1 = ""
                        strLimitPrice2 = ""
                        strLimitPrice3 = ""
                        strType = "Market"
                    End If
            End Select
            Dim count As Integer = 0
            txtUpfrontPopUpValue_TextChanged(Nothing, Nothing)
            Select Case objELNRFQ.web_update_RedirectedOrders_with_Margin_Price_Yield(orderQuantity.Replace(",", ""), strType, strLimitPrice1, strLimitPrice2, strLimitPrice3, _
                                                                              Quote_ID, LoginInfoGV.Login_Info.LoginId, strMargin, strClientPrice, _
                                                                              strClientYield, strBookingBranch, _
                                                                              strRMNameforOrderConfirm, strRMEmailIdforOrderConfirm)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    '<AvinashG. on 02-Feb-2016: FA-1286 - Display User Limit and mail to dealer desk(email id based on config) >
                    Dim sLSS_DealerNotificationGroupEmailID As String
                    sLSS_DealerNotificationGroupEmailID = objReadConfig.ReadConfig(dsConfig, "LSS_DealerNotificationGroupEmailID", "LSS", CStr(LoginInfoGV.Login_Info.EntityID), "NOVALIDID").Trim.ToUpper
                    Select Case sLSS_DealerNotificationGroupEmailID
                        Case "NOVALIDID"
                            LogException(LoginInfoGV.Login_Info.LoginId, "Cannot notify by mail! Invalid config value (" + sLSS_DealerNotificationGroupEmailID + ") found for 'LSS_DealerNotificationGroupEmailID' ", LogType.FnqInfo, Nothing, _
sSelfPath, "btnRedirect_ServerClick", ErrorLevel.Medium)
                        Case Else

                            Dim sbNotifyMail As StringBuilder
                            Dim errmailnotify As String = ""
                            
                            sbNotifyMail = New StringBuilder()
                            sbNotifyMail.Append("<div   style=""border: solid 1px #CCC;""><table cellpadding='2px' cellspacing='4px' width='490px'><tr><td style=""background-color: #DBE5F1;"">" + _
                            lblIssuerPopUpCaption.Text + "</td><td>" + lblIssuerPopUpValue.Text + "</td><td   style=""background-color: #DBE5F1;"">" + _
    lblUnderlyingPopUpCaption.Text + "</td><td>" + lblUnderlyingPopUpValue.Text + "</td></tr>" + _
   "<tr><td  style=""background-color: #DBE5F1;"">" + lblBookingBranchPopUpCaption.Text + "</td><td >" + ddlBookingBranchPopUpValue.SelectedItem.Text + "</td><td style=""background-color: #DBE5F1;"">" + _
    lblNotionalPopUpCaption.Text + lblCurrencyPopUpValue.Text + "</td><td>" + lblNotionalPopUpValue.Text + "</td></tr>" + _
    "<tr><td  style=""background-color: #DBE5F1;"">" + lblIssuerPricePopUpCaption.Text + "</td><td>" + lblIssuerPricePopUpValue.Text + "</td><td  style=""background-color: #DBE5F1;"">" + _
    lblClientPricePopUpCaption.Text + "</td><td>" + lblClientPricePopUpValue.Text + "</td></tr>" + _
    "<tr><td  style=""background-color: #DBE5F1;"">" + lblStrikePopUpCaption.Text + "</td><td>" + lblStrikePopUpValue.Text + "</td><td  style=""background-color: #DBE5F1;"">" + _
    lblTenorPopUpCaption.Text + "</td><td>" + lblTenorPopUpValue.Text + " " + lblTenorTypePopUpValue.Text + "</td></tr>" + _
    "<tr><td  style=""background-color: #DBE5F1;"">" + lblKOPopUpCaption.Text + "</td><td>" + lblKOPopUpValue.Text + " " + lblKOTypePopUpValue.Text + "</td><td>" + _
    "</td><td>" + "</td></tr>" + _
    "<tr><td  style=""background-color: #DBE5F1;"">" + lblUpfrontPopUpCaption.Text + "</td><td>" + txtUpfrontPopUpValue.Text + "</td><td  style=""background-color: #DBE5F1;"">" + _
    lblClientYieldPopUpCaption.Text + "</td><td>" + txtClientYieldPopUpValue.Text + "</td></tr>" + _
    "<tr><td  style=""background-color: #DBE5F1;"">" + lblOrderTypePopUpCaption.Text + "</td><td>" + ddlOrderTypePopUpValue.SelectedItem.Text + "</td><td  style=""background-color: #DBE5F1;"">" + _
    lblLimitPricePopUpCaption.Text + "</td><td>" + txtLimitPricePopUpValue.Text + "</td></tr></table></div>")
                            '<Changed by Mohit Lalwani on 3-Feb-2016 FA-1339>
                            Dim emailSubject As String = ""
                            emailSubject = objReadConfig.ReadConfig(dsConfig, "EQC_DealerRedirection_Subject", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "EQ Connect Order Redirection for ELN &#8211; &lt;RFQID&gt;").ToString
                            emailSubject = emailSubject.Trim.Replace("&lt;RFQID&gt;", Quote_ID).Replace("&lt;Product&gt;", "ELN").Replace("&#8211;", "-")

                            'oWEBADMIN.Notify_ToDealerDeskGroupEmailID(LoginInfoGV.Login_Info.EntityID.ToString(), _
                            '                                                             LoginInfoGV.Login_Info.LoginId, sLSS_DealerNotificationGroupEmailID, LoginInfoGV.Login_Info.LoginName + " redirected an order.  RFQ Id  for redirected order is: " + Quote_ID + _
                            '                                                             sbNotifyMail.ToString, "EQ Connect Order Redirection for ELN - " + Quote_ID, errmailnotify)
                            oWEBADMIN.Notify_ToDealerDeskGroupEmailID(LoginInfoGV.Login_Info.EntityID.ToString(), _
                                                                                         LoginInfoGV.Login_Info.LoginId, sLSS_DealerNotificationGroupEmailID, LoginInfoGV.Login_Info.LoginName + " redirected an order.  RFQ Id  for redirected order is: " + Quote_ID + _
                                                                                         sbNotifyMail.ToString, emailSubject, errmailnotify)
                            '</Changed by Mohit Lalwani on 3-Feb-2016 FA-1339>
                    End Select
                    '</AvinashG. on 02-Feb-2016: FA-1286 - Display User Limit and mail to dealer desk(email id based on config) >
                    lblerror.Text = "Order redirected for RFQ " & Quote_ID
                    lblerror.ForeColor = Color.Blue
                    ShowHideConfirmationPopup(False)
                    Stop_timer_Only()
                    '<MohitL. on 03-Nov-2015: FA-1174>
                    rbHistory.SelectedValue = "Order History"
                    fill_OrderGrid()
                    makeThisGridVisible(grdOrder)
                    ColumnVisibility()
                    upnlGrid.Update()
                    '</MohitL. on 03-Nov-2015: FA-1174>

                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    lblerrorPopUp.Text = "Error occurred while redirecting the order."
            End Select
        Catch ex As Exception
            lblerrorPopUp.Text = "Error occurred while redirecting the order."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnRedirect_ServerClick", ErrorLevel.High)
        End Try
    End Sub


    Private Sub EnableDisableForOrderPoolData(ByVal ppname As String)
        Try
            btnSolveAll.Enabled = False
            btnSolveAll.CssClass = "btnDisabled"
            If ppname.Trim.ToUpper = "HSBC" Then
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = False
                TRDBIB.Disabled = True
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = True
                btnHSBCPrice.CssClass = "btn"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"

		TROCBC1.Disabled = True
		btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

		TRCITI1.Disabled = True
		btnCITIPrice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "JPM" Then
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = True
                btnJPMprice.CssClass = "btn"
                TRJPM1.Disabled = False
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True

		TROCBC1.Disabled = True
		btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"


		TRCITI1.Disabled = True
		btnCITIPrice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"
                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "UBS" Then
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = True
                btnUBSPrice.CssClass = "btn"
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = False
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
				TROCBC1.Disabled = True
		btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"


		TRCITI1.Disabled = True
		btnCITIPrice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "BNPP" Then
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                btnBNPPPrice.Enabled = True
                btnBNPPPrice.CssClass = "btn"
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = False
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
				TROCBC1.Disabled = True
		btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

		TRCITI1.Disabled = True
		btnCITIPrice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "CS" Then
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                btnCSPrice.Enabled = True
                btnCSPrice.CssClass = "btn"
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = False
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
				TROCBC1.Disabled = True
		btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

		TRCITI1.Disabled = True
		btnCITIPrice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "BAML" Then
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = True
                btnBAMLPrice.CssClass = "btn"
                TRJPM1.Disabled = True
                TRBAML1.Disabled = False
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
				TROCBC1.Disabled = True
		btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

		TRCITI1.Disabled = True
		btnCITIPrice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"
                'ElseIf ppname.Trim.ToUpper = "DBIB" Then ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
            ElseIf ppname.Trim.ToUpper = "DB" Then
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = True
                btnDBIBPrice.CssClass = "btn"
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRDBIB.Disabled = False
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                
		TROCBC1.Disabled = True
		btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

                TRCITI1.Disabled = True
                btnCITIprice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "OCBC" Then
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
		TROCBC1.Disabled = False
		
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                btnOCBCprice.Enabled = True
                btnOCBCprice.CssClass = "btn"

                TRCITI1.Disabled = True
                btnCITIprice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"
	    ElseIf ppname.Trim.ToUpper = "CITI" Then
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
                TROCBC1.Disabled = True
                btnOCBCprice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

                TRCITI1.Disabled = False
                btnCITIprice.Enabled = True
                btnCITIprice.CssClass = "btn"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"

            ElseIf ppname.Trim.ToUpper = "LEONTEQ" Then
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
                TROCBC1.Disabled = True
                btnOCBCprice.Enabled = False
                btnOCBCprice.CssClass = "btnDisabled"

                TRCITI1.Disabled = True
                btnCITIprice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"

                TRLEONTEQ1.Disabled = False
                btnLEONTEQprice.Enabled = True
                btnLEONTEQprice.CssClass = "btn"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZprice.Enabled = False
                btnCOMMERZprice.CssClass = "btnDisabled"

            ElseIf ppname.Trim.ToUpper = "COMMERZ" Then
                btnBNPPPrice.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnJPMprice.Enabled = False
                btnJPMprice.CssClass = "btnDisabled"
                btnBAMLPrice.Enabled = False
                btnBAMLPrice.CssClass = "btnDisabled"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
                TROCBC1.Disabled = True
                btnOCBCprice.Enabled = False
                btnOCBCprice.CssClass = "btnDisabled"

                TRCITI1.Disabled = True
                btnCITIprice.Enabled = False
                btnCITIprice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = False
                btnCOMMERZprice.Enabled = True
                btnCOMMERZprice.CssClass = "btn"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQprice.Enabled = False
                btnLEONTEQprice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "" Then
                'enable all field 
                btnBNPPPrice.Enabled = True
                btnBNPPPrice.CssClass = "btn"
                btnHSBCPrice.Enabled = True
                btnHSBCPrice.CssClass = "btn"
                btnCSPrice.Enabled = True
                btnCSPrice.CssClass = "btn"
                btnUBSPrice.Enabled = True
                btnUBSPrice.CssClass = "btn"
                btnJPMprice.Enabled = True
                btnJPMprice.CssClass = "btn"
                btnBAMLPrice.Enabled = True
                btnBAMLPrice.CssClass = "btn"
                btnDBIBPrice.Enabled = True
                btnDBIBPrice.CssClass = "btn"
                TRJPM1.Disabled = False
                TRBAML1.Disabled = False
                TRCS1.Disabled = False
                TRBNPP1.Disabled = False
                TRUBS1.Disabled = False
                TRHSBC1.Disabled = False
                TRDBIB.Disabled = False
                TROCBC1.Disabled = False
                btnOCBCprice.Enabled = True
                btnOCBCprice.CssClass = "btn"
                TRCITI1.Disabled = False
                btnCITIprice.Enabled = True
                btnCITIprice.CssClass = "btn"

                TRLEONTEQ1.Disabled = False
                btnLEONTEQprice.Enabled = True
                btnLEONTEQprice.CssClass = "btn"

                TRCOMMERZ1.Disabled = False
                btnCOMMERZprice.Enabled = True
                btnCOMMERZprice.CssClass = "btn"

                btnSolveAll.Enabled = True
                btnSolveAll.CssClass = "btn"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '<AvinashG. on 09-Apr-2015: BoS related function>
    Private Sub setRedirectedELNOrderData()
        Dim dtROrderDetails As DataTable
        Dim sROrderID As String
        Dim strNewTenorELN As String = String.Empty
        Dim strNewTenorELNType As String = String.Empty
        Try
            dtROrderDetails = New DataTable("ROrderDetails")
            If Not IsNothing(Request.QueryString("RedirectedOrderId")) Then
                sROrderID = Request.QueryString("RedirectedOrderId")
                If sROrderID.Trim <> "" Then
                    objELNRFQ.Get_ELN_Redirected_Order_Details(sROrderID, dtROrderDetails)
                    Dim strType As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.ELNType).ToString.Trim
                    If strType = "Simple" Then ''Added on 14Nov
                        chkELNType.Checked = False
                        'lblsimpleBarrier.Text = "Simple"
                        txtBarrier.Enabled = False
                        ddlBarrier.Enabled = False
                        txtBarrier.Visible = False
                        ddlBarrier.Visible = False
                    Else

                        chkELNType.Checked = True
                        ' lblsimpleBarrier.Text = "Barrier"
                        txtBarrier.Enabled = True
                        ddlBarrier.Enabled = True
                        txtBarrier.Visible = True
                        ddlBarrier.Visible = True
                        Dim strBarrierType As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.BarrierType).ToString.Trim
                        ''''<Dilkhush 13May2016 FA-1427>
                        ''ddlBarrier.SelectedValue = strBarrierType
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SHOW_DAILYCLOSE_KO", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            Case "Y", "YES"
                                ddlBarrier.SelectedValue = strBarrierType
                            Case "N", "NO"
                                If strBarrierType.ToUpper <> "DAILY_CLOSE" Then
                                    ddlBarrier.SelectedValue = strBarrierType
                                End If
                        End Select
                        ''''</Dilkhush 13May2016 FA-1427>
                        Dim strBarrier As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.BarrierPercentage).ToString.Trim
                        txtBarrier.Text = strBarrier
                    End If


                    Dim strExchng As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.Exchange).ToString.Trim
                    Dim strShare As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.Share).ToString.Trim
                    ''Rushikesh 14Jan2016 to set share from pool data
                    setShare(strExchng, strShare)
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                        Case "Y", "YES"
                            ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                            'ddlShare.Text = ddlShare.Text
                            'If ddlShare.SelectedValue IsNot Nothing Then
                            '    ddlShare.Text = ddlShare.SelectedItem.Text
                            'End If
                            lblExchangeVal.Text = setExchangeByShare(ddlShare)
                        Case "N", "NO"
                            If ddlExchange.SelectedValue = strExchng Then
                                ddlExchange.SelectedValue = strExchng
                                'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                ''ddlShare.Text = ddlShare.Text
                                'If ddlShare.SelectedValue IsNot Nothing Then
                                '    ddlShare.Text = ddlShare.SelectedItem.Text
                                'End If
                                lblExchangeVal.Text = setExchangeByShare(ddlShare)
                            Else
                                ddlExchange.SelectedValue = strExchng ''ddlShare.Items.IndexOf(ddlShare.Items.FindByText(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Exchange).Text))
                                '' Fillddl_Share()
                                'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                'If ddlShare.SelectedValue IsNot Nothing Then
                                '    ddlShare.Text = ddlShare.SelectedItem.Text
                                'End If

                            End If
                    End Select


                    getCurrency(dtROrderDetails.Rows(0)(RedirectOrderDetails.Share).ToString.Trim)

                    Dim strQuantoCcy As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.Currency).ToString.Trim
                    If lblELNBaseCcy.Text = strQuantoCcy Then
                        chkQuantoCcy.Checked = False
                        ddlQuantoCcy.Enabled = False
                        lblQuantity.Text = "Notional (<font style=''>" & lblELNBaseCcy.Text & "</font>)"
                        ddlQuantoCcy.DataSource = Nothing
                        ddlQuantoCcy.DataBind()
                        ddlQuantoCcy.Items.Clear()
                        ddlQuantoCcy.Items.Add(New DropDownListItem(lblELNBaseCcy.Text, lblELNBaseCcy.Text)) 'Mohit Lalwani on 8-Jul-2016
                        ddlQuantoCcy.BackColor = Color.FromArgb(242, 242, 243)
                    Else
                        chkQuantoCcy.Checked = True
                        ddlQuantoCcy.Enabled = True
                        Call Fillddl_QuantoCcy()
                        ddlQuantoCcy.BackColor = Color.White
                        ''ddlQuantoCcy.SelectedIndex = ddlQuantoCcy.Items.IndexOf(ddlQuantoCcy.Items.FindByText(dtROrderDetails.Rows(0)(RedirectOrderDetails.Currency).ToString.Trim))
                        ddlQuantoCcy.SelectedValue = dtROrderDetails.Rows(0)(RedirectOrderDetails.Currency).ToString.Trim
                        ''<Rutuja 25April:added to set Quanto ccy  >
                        lblQuantity.Text = "Notional (<font style=''>" & strQuantoCcy & "</font>)"
                        ''</Rutuja 25April:added to set Quanto ccy  >
                    End If

                    Dim strTenor As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.Tenor).ToString.Trim
                    txtTenor.Text = strTenor
                    'For i = 0 To strTenor.Length - 1
                    '    If IsNumeric(strTenor.Substring(i, 1)) = True Then
                    '        strNewTenorELN = strNewTenorELN + strTenor.Substring(i, 1)
                    '    End If
                    'Next
                    'txtTenor.Text = strNewTenorELN
                    'For i = 0 To strTenor.Length - 1
                    '    If IsNumeric(strTenor.Substring(i, 1)) = False Then
                    '        strNewTenorELNType = strNewTenorELNType + strTenor.Substring(i, 1)
                    '    End If
                    'Next
                    '<Changed by Mohit Lalwani on  13-Oct-2015>
                    'ddlTenorTypeELN.SelectedItem.Text = dtROrderDetails.Rows(0)(RedirectOrderDetails.TenorType).ToString.Trim
                    ddlTenorTypeELN.SelectedValue = dtROrderDetails.Rows(0)(RedirectOrderDetails.TenorType).ToString.Trim.ToUpper
                    '</Changed by Mohit Lalwani on  13-Oct-2015>
                    Dim strUpfront As String = dtROrderDetails.Rows(0).Item("Upfront").ToString.Trim
                    If strUpfront.Trim <> "" Or strUpfront.Trim <> "&nbsp;" Then
                        txtUpfrontELN.Text = FormatNumber((CDbl(strUpfront) / 100).ToString, 2)
                        '' txtUpfrontELN.Text = FormatNumber(strUpfront, 2)
                    End If

                    Dim strSetDays As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.SettlementDays).ToString.Trim
                    'Dim strSettleDays As String = CStr(CDbl(strSetDays.Trim.Split(CChar("+"))(1).Trim))
                    'If strSettleDays = "7" Then
                    '    ddlSettlementDays.SelectedValue = "1W"
                    'ElseIf strSettleDays = "14" Then
                    '    ddlSettlementDays.SelectedValue = "2W"
                    'Else
                    '    ddlSettlementDays.SelectedValue = "1W"
                    'End If
                    txtValueDays.Text = strSetDays

                    '<AvinashG. on 09-Apr-2015: Set Dates>
                    Dim strTradeDate As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.TradeDate).ToString.Trim
                    If strTradeDate.ToUpper = Today.ToString("dd-MMM-yyyy").ToUpper Then  '<Rutuja S. on 15-Dec-2014: Added to get dates based on trade date>
                        txtTradeDate.Value = Convert.ToDateTime(strTradeDate).ToString("dd-MMM-yyyy")

                        Dim strSettDate As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.SettlementDate).ToString.Trim
                        txtSettlementDate.Value = Convert.ToDateTime(strSettDate).ToString("dd-MMM-yyyy")

                        Dim strExpDate As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.ExpiryDate).ToString.Trim
                        txtExpiryDate.Value = Convert.ToDateTime(strExpDate).ToString("dd-MMM-yyyy")

                        Dim strMatDate As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.MaturityDate).ToString.Trim
                        txtMaturityDate.Value = Convert.ToDateTime(strMatDate).ToString("dd-MMM-yyyy")
                    Else
                        lblerror.Text = "Pool ID " + sROrderID + " not having todays' trade date."
                    End If
                    '</AvinashG. on 09-Apr-2015: Set Dates>

                    Dim strSolveFor As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.SolveFor).ToString.Trim
                    If strSolveFor = "PricePercentage" Then
                        ddlSolveFor.SelectedValue = "PricePercentage"
                        ''<Rutuja 25April:Added to enable/disable textbox>
                        txtELNPrice.Text = "0.00"
                        txtELNPrice.Enabled = False
                        txtELNPrice.BackColor = Color.FromArgb(242, 242, 243)
                        txtStrike.Enabled = True
                        txtStrike.BackColor = Color.White
                        ''</Rutuja 25April:Added to enable ,/disable textbox>
                        ''<Dilkhush/Rutuja on 11Aug2014:Added to set solve for type  on grid data change>
                        lblSolveForType.Text = "IB Price (%)"
                        ''</Dilkhush/Rutuja on 11Aug2014:Added to set solve for type  on grid data change>
                    Else
                        ddlSolveFor.SelectedValue = "StrikePercentage"
                        ''<Rutuja 25April:Added to enable/disable textbox>
                        txtELNPrice.Enabled = True
                        txtELNPrice.BackColor = Color.White
                        txtStrike.Text = "0.00"
                        txtStrike.Enabled = False
                        txtStrike.BackColor = Color.FromArgb(242, 242, 243)
                        ''</Rutuja 25April:Added to enable/disable textbox>
                        ''<Dilkhush/Rutuja on 11Aug2014:Added to set solve for type  on grid data change>
                        lblSolveForType.Text = "Strike (%)"
                        ''</Dilkhush/Rutuja on 11Aug2014:Added to set solve for type  on grid data change>
                    End If

                    Dim strStrike As String = SetNumberFormat(dtROrderDetails.Rows(0)(RedirectOrderDetails.StrikePercentage).ToString.Trim, 2)
                    If strStrike = "" Or strStrike = "&nbsp;" Then
                        txtStrike.Text = "0.00"
                    Else
                        txtStrike.Text = strStrike
                    End If

                    Dim strELNPrice As String = SetNumberFormat(dtROrderDetails.Rows(0)(RedirectOrderDetails.PricePercentage).ToString.Trim, 2)

                    If strELNPrice = "" Or strELNPrice = "&nbsp;" Then
                        txtELNPrice.Text = "0.00"
                    Else
                        txtELNPrice.Text = strELNPrice
                    End If

                    Dim strOrderqty As String = SetNumberFormat(dtROrderDetails.Rows(0)(RedirectOrderDetails.Notional).ToString.Trim, 0)  '' Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
 ''                   txtQuantity.Text = FormatNumber(strOrderqty, 2)
 txtQuantity.Text = SetNumberFormat(strOrderqty, 0)  '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                    '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                    'getRange()
                    '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                    GetCommentary()

                    resetControlsForPool(False)

                    ''btnSolveAll.Enabled = False

                Else
                    lblerror.Text = "Received invalid Order ID."
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Sets Exchange for selected Share, this is as per the new requirement of client, JIRA FA-692
    ''' </summary>
    ''' <param name="ddlShare">Control of the share being used for setting exchange.</param>
    ''' <param name="ddlExchange">Exchange Control that needs to be setup.</param>
    ''' <returns></returns>
    ''' <remarks>Revision History: 1) on 23-Aug-2015, Dead function so changing it to required function</remarks>
    Private Function setExchangeByShare(ByVal _ddlShare As RadComboBox) As String
        Dim sExchangeName As String
        Try
            sExchangeName = ""
	    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            If _ddlShare.SelectedValue IsNot Nothing Then
            ''If _ddlShare.SelectedItem.Value.Trim = "" Then
                If _ddlShare.SelectedValue.Trim = "" Then
                    sExchangeName = ""
                Else
                    If (Not IsNothing(CType(Session("Share_Value"), DataTable)) AndAlso CType(Session("Share_Value"), DataTable).Rows.Count > 0) Then
                        Dim drShareData As DataRow
                        drShareData = CType(Session("Share_Value"), DataTable).Select("Code = '" + _ddlShare.SelectedValue.ToString + "'")(0)
                        sExchangeName = drShareData.Item("ExchangeCode").ToString + " - " + drShareData.Item("ExchangeName").ToString
                    Else
                        objELNRFQ.GetShareExchange(_ddlShare.SelectedValue.ToString, sExchangeName)
                    End If
                End If

            End If

            Return sExchangeName
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function SelectIntoDataTable(ByVal selectFilter As String, ByVal sourceDataTable As DataTable) As DataTable
        Try
            Dim newDataTable As DataTable = sourceDataTable.Clone
            Dim dataRows As DataRow() = sourceDataTable.Select(selectFilter)
            Dim typeDataRow As DataRow

            For Each typeDataRow In dataRows
                newDataTable.ImportRow(typeDataRow)
            Next
            Return newDataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub tabContainer_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabContainer.ActiveTabChanged
        Select Case tabContainer.ActiveTabIndex
            Case prdTab.FCN
                Response.Redirect("../ELN_DealEntry1/DRAFCNRFQ1.aspx?menustr=EQ%20Sales%20-%20FCN%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=&PrdToLoad=FCN", False)
            Case prdTab.DRA
                Response.Redirect("../ELN_DealEntry1/DRAFCNRFQ1.aspx?menustr=EQ%20Sales%20-%20DRA%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=&PrdToLoad=DRA", False)
            Case prdTab.Acc
                Response.Redirect("../ELN_DealEntry1/AccDecRFQ1.aspx?menustr=EQ%20Sales%20-%20Acc%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=&PrdToLoad=ACCUMULATOR", False) '<AvinashG. on 24-Jan-2016: >
            Case prdTab.Dec
                Response.Redirect("../ELN_DealEntry1/AccDecRFQ1.aspx?menustr=EQ%20Sales%20-%20Dec%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=&PrdToLoad=DECUMULATOR", False) '<AvinashG. on 24-Jan-2016: >
            Case prdTab.EQO
                Response.Redirect("../ELN_DealEntry1/EQORFQ1.aspx?menustr=EQ%20Sales%20-%EQO%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=", False) '<AvinashG. on 24-Jan-2016: >
        End Select
    End Sub



    Public Function CheckBestPriceForEmail() As String
        Try
            Dim temp As HiddenField
            temp = getBestPrice(JpmHiddenPrice, HsbcHiddenPrice)
            temp = getBestPrice(temp, UbsHiddenPrice)
            temp = getBestPrice(temp, CsHiddenPrice)
            temp = getBestPrice(temp, BNPPHiddenPrice)
            temp = getBestPrice(temp, BAMLHiddenPrice)
	    temp = getBestPrice(temp, DBIBHiddenPrice)
	    temp = getBestPrice(temp, OCBCHiddenPrice)
            temp = getBestPrice(temp, CITIHiddenPrice)
            temp = getBestPrice(temp, LEONTEQHiddenPrice)
            temp = getBestPrice(temp, COMMERZHiddenPrice)
            If temp.ID Is Nothing Then
                '   System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "best", "alert('No best Price available')", True)
            Else
                Return temp.ID.Replace("HiddenPrice", "").ToUpper
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "CheckBestPrice", ErrorLevel.High)
            Throw ex
        End Try
    End Function

#Region "functions For Quote MailHTML String"

    Private Sub btnEMLMailTrial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEMLMailTrial.Click
        Dim sMailBodyTemplate As String
        Dim EmailtemplateFilePath As String = ""
        Dim sUnderlyingTicker, sUnderlyingTicker2, sUnderlyingTicker3 As String
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim bestPP As String
        Dim RFQID As String
        Dim Issuer As String
        Dim Yield As String
        Dim PriceOrStrike As String
        Dim mailFileName As String
        Dim sDayCount As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            GetCommentary()

            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_MailText_Narration_AS", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "TEXT").Trim.ToUpper
                Case "TEXT"
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "hideEmail13", "mailtoMail();", True)
                Case "HTML"

                    hashRFQID = CType(Session("Hash_Values"), Hashtable)
                    hashPPId = CType(Session("PP_IdTable"), Hashtable)
                    bestPP = CheckBestPriceForEmail()


                    EmailtemplateFilePath = System.Web.HttpContext.Current.Server.MapPath("BasicMailTemplate.eml")    ''mangesh wakode 26 nov 2015  Relative path  
                    '' sMailBodyTemplate = System.IO.File.ReadAllText("C:\inetpub\wwwroot\FinIQWebApp\ELN_DealEntry_Layout10\BasicMailTemplate.eml")
                    sMailBodyTemplate = System.IO.File.ReadAllText(EmailtemplateFilePath)
                    ''mangesh wakode 25 nov 2015 
                    Dim strMailHTML As String = ""


                    Dim mailSubject As New StringBuilder     ''mangesh wakode 26 nov 2015  for mail subject
                    ''mangesh wakode 26 nov 2015 for quote mail narration tab wise 
                    Select Case tabContainer.ActiveTabIndex
                        Case prdTab.ELN
                            Select Case bestPP
                                Case "JPM"
                                    PriceOrStrike = lblJPMPrice.Text
                                    Issuer = "JPM"
                                    Yield = lblJPMClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("JPM")))
                                    Else
                                        RFQID = Convert.ToString(Session("JPMQuote"))
                                    End If

                                    ' RFQID = Session("JPMQuote").ToString
                                Case "HSBC"
                                    PriceOrStrike = lblHSBCPrice.Text
                                    Issuer = "HSBC"
                                    Yield = lblHSBCClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("HSBC")))
                                    Else
                                        RFQID = Convert.ToString(Session("HSBCQuote"))
                                    End If
                                    'RFQID = Convert.ToString(Session("HSBCQuote"))
                                Case "CS"
                                    PriceOrStrike = lblCSPrice.Text
                                    Issuer = "CS"
                                    Yield = lblCSClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("CS")))
                                    Else
                                        RFQID = Convert.ToString(Session("CSQuote"))
                                    End If
                                    ' RFQID = Convert.ToString(Session("CSQuote"))
                                Case "UBS"
                                    PriceOrStrike = lblUBSPrice.Text
                                    Issuer = "UBS"
                                    Yield = lblUBSClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("UBS")))
                                    Else
                                        RFQID = Convert.ToString(Session("UBSQuote"))
                                    End If
                                    'RFQID = Convert.ToString(Session("UBSQuote"))
                                Case "BNPP"
                                    PriceOrStrike = lblBNPPPrice.Text
                                    Issuer = "BNPP"
                                    Yield = lblBNPPClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("BNPP")))
                                    Else
                                        RFQID = Convert.ToString(Session("BNPPQuote"))
                                    End If
                                    'RFQID = Convert.ToString(Session("BNPPQuote"))
                                Case "BAML"
                                    PriceOrStrike = lblBAMLPrice.Text
                                    Issuer = "BAML"
                                    Yield = lblBAMLClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("BAML")))
                                    Else
                                        RFQID = Convert.ToString(Session("BAMLQuote"))
                                    End If
                                    'RFQID = Convert.ToString(Session("BAMLQuote"))
                                Case "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD> ''Comparing ID
                                    ''Case "DB"
                                    PriceOrStrike = lblDBIBPrice.Text
                                    'Issuer = "DBIB" ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                                    Issuer = "DB"
                                    Yield = lblDBIBClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        'RFQID = CStr(hashRFQID(hashPPId("DBIB"))) ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
                                        RFQID = CStr(hashRFQID(hashPPId("DB")))
                                    Else
                                        RFQID = Convert.ToString(Session("DBIBQuote"))
                                    End If
				Case "OCBC"
                                    PriceOrStrike = lblOCBCPrice.Text
                                    Issuer = "OCBC"
                                    Yield = lblOCBCClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("OCBC")))
                                    Else
                                        RFQID = Convert.ToString(Session("OCBCQuote"))
                                    End If
                                    'RFQID = Convert.ToString(Session("OCBCQuote"))
                                 Case "CITI"
                                    PriceOrStrike = lblCITIPrice.Text
                                    Issuer = "CITI"
                                    Yield = lblCITIClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("CITI")))
                                    Else
                                        RFQID = Convert.ToString(Session("CITIQuote"))
                                    End If
                                    'RFQID = Convert.ToString(Session("CITIQuote"))
                                Case "LEONTEQ"
                                    PriceOrStrike = lblLEONTEQPrice.Text
                                    Issuer = "LEONTEQ"
                                    Yield = lblLEONTEQClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("LEONTEQ")))
                                    Else
                                        RFQID = Convert.ToString(Session("LEONTEQQuote"))
                                    End If
                                    'RFQID = Convert.ToString(Session("LEONTEQQuote"))
                                Case "COMMERZ"
                                    PriceOrStrike = lblCOMMERZPrice.Text
                                    Issuer = "COMMERZ"
                                    Yield = lblCOMMERZClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("COMMERZ")))
                                    Else
                                        RFQID = Convert.ToString(Session("COMMERZQuote"))
                                    End If
                                    'RFQID = Convert.ToString(Session("COMMERZQuote"))
				Case Nothing, ""
                                    PriceOrStrike = ""
                                    Issuer = ""
                                    Yield = ""

                            End Select

                            If RFQID Is Nothing Or RFQID = "" Then
                                lblerror.ForeColor = Color.Red
                                lblerror.Text = "No price available for mailing!"
                                Exit Sub
                            End If
                            ''sUnderlyingTicker = getUnderlyingTicker(ddlShare.SelectedItem.Text)
			    sUnderlyingTicker = getUnderlyingTicker(ddlShare.Text) ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value



                            strMailHTML = getQuoteMailHTMLString_ELN(sUnderlyingTicker, PriceOrStrike, bestPP, Issuer, Yield, RFQID)   ''for ELN
                            'mailSubject.Append(" " + RFQID)   ''Mangesh wakode <8 dec 2015>RFQ Removed from mail subject as told by Avinash G.
                            '<AvinashG. on 06-Jan-2016: Config based bank name>
                            mailSubject.Append(objReadConfig.ReadConfig(dsConfig, "EQC_QuoteMailSubjectBankName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), " <EQ_Connect RFQ>").Replace("&lt;", "<").Replace("&gt;", ">"))
                            'mailSubject.Append(" <EQ_Connect RFQ>")   ''Mangesh wakode <8 dec 2015>
                            '<AvinashG. on 06-Jan-2016: Config based bank name>
                            '<AvinashG. on 24-Dec-2015: Tenor not notional >
                            mailSubject.Append(" " + txtTenor.Text.ToString.Trim + " " + ddlTenorTypeELN.SelectedItem.Text)
                            'mailSubject.Append(" " + txtQuantity.Text.ToString.Trim)      ''append notional to subject
                            '</AvinashG. on 24-Dec-2015: Tenor not notional >
                            mailSubject.Append(" " + lblELNBaseCcy.Text.ToString.Trim)           ''append CCy to subject
                            mailSubject.Append(" ELN on ")                                    ''append product to subject
                            mailSubject.Append(sUnderlyingTicker)       ''append Underlying to subject

                            mailFileName = "EQ_Connect_ELN_" + txtTenor.Text + "_" + ddlTenorTypeELN.SelectedItem.Text + "_" + sUnderlyingTicker + ".eml"

                    End Select
                    Dim sMailBody As String = sMailBodyTemplate.Replace("[Mail_subject]", mailSubject.ToString).Replace("[Mail_Body]", strMailHTML)

                    HttpContext.Current.Response.Clear()
                    ' HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=EQConnect_RFQ_Details_ELN_" + txtTenor.Text + "_" + ddlTenorTypeELN.SelectedItem.Text + If(ddlShare.SelectedItem.Value <> "", ddlShare.SelectedItem.Value, "") + ".eml")
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + mailFileName)
                    HttpContext.Current.Response.Write(sMailBody)

                    HttpContext.Current.Response.Charset = ""
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    HttpContext.Current.Response.ContentType = "application/octet-stream"
                    HttpContext.Current.Response.End()
                    HttpContext.Current.ApplicationInstance.CompleteRequest()



                Case "DOCGEN"
                    hashRFQID = CType(Session("Hash_Values"), Hashtable)
                    hashPPId = CType(Session("PP_IdTable"), Hashtable)
                    bestPP = CheckBestPriceForEmail()
                    Dim strMailHTML As String = ""
                    Dim strNewClientYield As String = ""


                    Dim mailSubject As New StringBuilder
                    Select Case tabContainer.ActiveTabIndex
                        Case prdTab.ELN
                            Select Case hdnBestProvider.Value
                                Case "JPM"
                                    Issuer = "JPM"
                                    strNewClientYield = lblJPMClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("JPM")))
                                    Else
                                        RFQID = Convert.ToString(Session("JPMQuote"))
                                    End If
                                Case "HSBC"
                                    Issuer = "HSBC"
                                    strNewClientYield = lblHSBCClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("HSBC")))
                                    Else
                                        RFQID = Convert.ToString(Session("HSBCQuote"))
                                    End If
                                Case "CS"
                                    Issuer = "CS"
                                    strNewClientYield = lblCSClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("CS")))
                                    Else
                                        RFQID = Convert.ToString(Session("CSQuote"))
                                    End If
                                Case "UBS"
                                    Issuer = "UBS"
                                    strNewClientYield = lblUBSClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("UBS")))
                                    Else
                                        RFQID = Convert.ToString(Session("UBSQuote"))
                                    End If
                                Case "BNPP"
                                    Issuer = "BNPP"
                                    strNewClientYield = lblBNPPClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("BNPP")))
                                    Else
                                        RFQID = Convert.ToString(Session("BNPPQuote"))
                                    End If
                                Case "BAML"
                                    Issuer = "BAML"
                                    strNewClientYield = lblBAMLClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("BAML")))
                                    Else
                                        RFQID = Convert.ToString(Session("BAMLQuote"))
                                    End If
                                Case "DBIB"
                                    Issuer = "DB"
                                    strNewClientYield = lblDBIBClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("DB")))
                                    Else
                                        RFQID = Convert.ToString(Session("DBIBQuote"))
                                    End If
                                Case "OCBC"
                                    Issuer = "OCBC"
                                    strNewClientYield = lblOCBCClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("OCBC")))
                                    Else
                                        RFQID = Convert.ToString(Session("OCBCQuote"))
                                    End If
                                Case "CITI"
                                    Issuer = "CITI"
                                    strNewClientYield = lblCITIClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("CITI")))
                                    Else
                                        RFQID = Convert.ToString(Session("CITIQuote"))
                                    End If
                                Case "LEONTEQ"
                                    Issuer = "LEONTEQ"
                                    strNewClientYield = lblLEONTEQClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("LEONTEQ")))
                                    Else
                                        RFQID = Convert.ToString(Session("LEONTEQQuote"))
                                    End If
                                Case "COMMERZ"
                                    Issuer = "COMMERZ"
                                    strNewClientYield = lblCOMMERZClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("COMMERZ")))
                                    Else
                                        RFQID = Convert.ToString(Session("COMMERZQuote"))
                                    End If
                                Case Nothing, ""
                                    Issuer = ""

                            End Select

                            If RFQID Is Nothing Or RFQID = "" Or strNewClientYield = "" Then
                                lblerror.ForeColor = Color.Red
                                lblerror.Text = "No price available for mailing!"
                                Exit Sub
                            End If
                            sUnderlyingTicker = getUnderlyingTicker(ddlShare.Text) ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value

                            Dim strFileName As String
                            Dim strDocGenVirtualPath As String
                            strFileName = generateDocument.StartDocumentGeneration(LoginInfoGV.Login_Info.LoginId, "ELN", "SEND_QUOTE_EMAIL", RFQID, "ELN_DEAL", LoginInfoGV.Login_Info.EntityID.ToString, LoginInfoGV.Login_Info.GlobalServerDateTime, 1)
                            Dim FileText As String = File.ReadAllText(strFileName)
                            replaceClassToStyle(FileText)


                            mailSubject.Append(objReadConfig.ReadConfig(dsConfig, "EQC_QuoteMailSubjectBankName", If(chkELNType.Checked, "KOELN", "ELN"), CStr(LoginInfoGV.Login_Info.EntityID), " <EQ_Connect RFQ>").Replace("&lt;", "<").Replace("&gt;", ">"))
                            mailSubject.Append(" " + txtTenor.Text.ToString.Trim + " " + ddlTenorTypeELN.SelectedItem.Text)
                            mailSubject.Append(" (" + lblELNBaseCcy.Text.ToString.Trim + ")")           ''append CCy to subject
                            mailSubject.Append(" " + If(chkELNType.Checked, "KOELN", "ELN") + " on ")                                    ''append product to subject
                            mailSubject.Append(sUnderlyingTicker)       ''append Underlying to subject

                            mailFileName = "EQ_Connect_ELN_" + txtTenor.Text + "_" + ddlTenorTypeELN.SelectedItem.Text + "_" + sUnderlyingTicker + ".eml"


                            Dim dtImageDetails As DataTable
                            dtImageDetails = New DataTable("dtImageDetails")
                            dtImageDetails.Columns.Add("imageID", GetType(String))
                            dtImageDetails.Columns.Add("imagePath", GetType(String))
                            dtImageDetails.Rows.InsertAt(dtImageDetails.NewRow(), 0)
                            dtImageDetails.Rows(0).Item(0) = "Image"
                            If chkELNType.Checked Then
                                dtImageDetails.Rows(0).Item(1) = System.Web.HttpContext.Current.Server.MapPath("..\..\FinIQWebApp\ELN_DealEntry1\Images\EmailPrdHeaders\KOELN_Header.png")
                            Else
                                dtImageDetails.Rows(0).Item(1) = System.Web.HttpContext.Current.Server.MapPath("..\..\FinIQWebApp\ELN_DealEntry1\Images\EmailPrdHeaders\ELN_Header.png")
                            End If

                            ''<Sample image commented for using actual image>
                            'dtImageDetails.Rows.InsertAt(dtImageDetails.NewRow(), 1)
                            'dtImageDetails.Rows(1).Item(0) = "GraphSample"
                            'dtImageDetails.Rows(1).Item(1) = System.Web.HttpContext.Current.Server.MapPath("..\..\FinIQWebApp\ELN_DealEntry1\Images\EmailPrdHeaders\StockGraphSample.png")
                            ''</Sample image commented for using actual image>


                            ''<TRDS Email Code>
                            'URLAuth = "https://" & ConfigurationManager.AppSettings("ReutersPath").ToString() & "/Authentication/RequestToken"

                            'Dim postString As String = "{""Credentials"" : {""Username"":""" & strCredentials(0) & """,""Password"":""" & strCredentials(1) & """}}"

                            'Dim contentType As String = "application/json"

                            'Dim webRequest As HttpWebRequest = CType(webRequest.Create(URLAuth), HttpWebRequest)

                            'webRequest.Method = "POST"
                            'webRequest.ContentType = "image"

                            'Using requestWriter As StreamWriter = New StreamWriter(webRequest.GetRequestStream())

                            '    requestWriter.Write(postString)
                            '    requestWriter.Flush()
                            '    requestWriter.Close()

                            '    Dim httpResponse = DirectCast(webRequest.GetResponse(), HttpWebResponse)

                            '    Using streamReader As StreamReader = New StreamReader(httpResponse.GetResponseStream())
                            '        Dim authResult = streamReader.ReadToEnd()
                            '        dt = ConvertJSONToDataTable(authResult)
                            '        authtoken = dt.Rows(0).Item(1).ToString
                            '        getReutersAuthentication = authtoken
                            '    End Using
                            'End Using



                            Dim sGraphImagePath As String = ""
                            ''sGraphImagePath = "E:\MailingHeaders\FromTRDS\graph_" + ddlShare.SelectedValue + "_" + RFQID + ".gif"
                            sGraphImagePath = objReadConfig.ReadConfig(dsConfig, "EQC_QuoteEmailGraphImagePath", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "C:\FinIQ\Product_Info\GraphImagePath\")
                            sGraphImagePath = sGraphImagePath + ddlShare.SelectedValue + "_" + RFQID + ".gif"
                            objReadConfig.ReadConfig(dsConfig, "EQC_ProductKYIRPath", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "C:\FinIQ\Product_Info\")
                            Dim tImage As Image = getEmailGraphImage(RFQID, "TRDS1")
                            If tImage Is Nothing Then
                                dtImageDetails.Rows.InsertAt(dtImageDetails.NewRow(), 1)
                                dtImageDetails.Rows(1).Item(0) = "GraphSample"
                                dtImageDetails.Rows(1).Item(1) = System.Web.HttpContext.Current.Server.MapPath("..\..\FinIQWebApp\ELN_DealEntry1\Images\EmailPrdHeaders\GraphNotFound.PNG")
                            Else
                                tImage.Save(sGraphImagePath, System.Drawing.Imaging.ImageFormat.Gif)
                                dtImageDetails.Rows.InsertAt(dtImageDetails.NewRow(), 1)
                                dtImageDetails.Rows(1).Item(0) = "GraphSample"
                                dtImageDetails.Rows(1).Item(1) = sGraphImagePath
                            End If


                            ''</TRDS Email Code>
                            Dim strLoginUserEmail As String = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)


                            Dim filePath As String
                            Dim fileName As String
                            filePath = objReadConfig.ReadConfig(dsConfig, "EQC_ProductKYIRPath", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "C:\FinIQ\Product_Info\")
                            If chkELNType.Checked Then
                                fileName = "KYIR - KOELN.pdf"
                            Else
                                fileName = "KYIR - ELN.pdf"
                            End If
                            filePath = filePath + fileName

                            Dim strNewUpfront As String = txtUpfrontELN.Text

                            Select Case objELNRFQ.Web_InsertNewUpfrontDetails(RFQID, strNewUpfront, strNewClientYield)
                                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful

                                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                                    lblerror.Text = "Error occurred while saving new upfront."
                                    Exit Sub
                            End Select


                            If oWEBADMIN.Notify_ToDealerDeskGroupEmailID_imageContent(LoginInfoGV.Login_Info.EntityID.ToString(), _
                                                                                               LoginInfoGV.Login_Info.LoginId, strLoginUserEmail, FileText, mailSubject.ToString(), filePath, dtImageDetails, "") Then
                                lblerror.Text = "Email sent successfully."
                            Else
                                lblerror.Text = "Email sending failed."
                            End If

                            If File.Exists(strFileName) Then
                                File.Delete(strFileName)
                            End If
                    End Select

            End Select
            RestoreSolveAll()
            RestoreAll()
            upnlGrid.Update()

        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "btnEMLMailTrial_Click", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Function getEmailGraphImage(ByVal sRFQID As String, ByVal sMode As String) As Image
        Try
            ''Bitmap.FromStream(New MemoryStream(tClient.DownloadData("http://t1.trkd-asia.com/finiq/?sym=6837.HK&style=1&int=4&per=8&w=570&h=200&l=12.6,409090,1%7C11.9,404090,1%7C11.3,409040,1&token=2B157F5064B581A671634B2403F9EAAAAC90B9744204D3EA4AAFB995952A182571D71AB3E961D80ABFE91B504277C109C8DEFF69F9A70119F5EA52C9F2FDA9CD940A8E95A6620F94E61539EE1CAEF720")))
            Dim tClient As WebClient = New WebClient
            Dim strImageURL As String
            Dim dtEmailDetails As DataTable
            dtEmailDetails = Nothing
            Dim strTokenCon As String = ""
            strImageURL = ConfigurationManager.AppSettings("quoteEmail_imageUrl")
            If strImageURL <> "" Then
                strImageURL = strImageURL.Replace("[QE_Share]", ddlShare.SelectedValue)
                strImageURL = strImageURL.Replace("[QE_Style]", "1")
                strImageURL = strImageURL.Replace("[QE_Int]", "4")
                strImageURL = strImageURL.Replace("[QE_Per]", "11")
                strImageURL = strImageURL.Replace("[QE_Width]", "540")
                strImageURL = strImageURL.Replace("[QE_Height]", "200")
                getQuoteEmailDetails(sRFQID, sMode, dtEmailDetails)
                If dtEmailDetails.Rows.Count > 0 Then
                    strImageURL = strImageURL.Replace("[QE_Param]", dtEmailDetails.Rows(0)("ABSStrike").ToString() + ",404090,1")
                End If
                strTokenCon = objReadConfig.ReadConfig(dsConfig, "EQC_QuoteEmailToken", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "token=2B157F5064B581A671634B2403F9EAAAAC90B9744204D3EA4AAFB995952A182571D71AB3E961D80ABFE91B504277C109C8DEFF69F9A70119F5EA52C9F2FDA9CD940A8E95A6620F94E61539EE1CAEF720")
                strImageURL = strImageURL.Replace("token=[QE_Token]", strTokenCon)
            Else
                LogException(LoginInfoGV.Login_Info.LoginId, "Exception:Graph image url generation failed.", LogType.FnqError, Nothing, _
                        sSelfPath, "getEmailGraphImage", ErrorLevel.High)
                Dim blankImage As Image
                Return blankImage
            End If



            'strImageURL = New StringBuilder()
            'strImageURL.Append("http://t1.trkd-asia.com/finiq/?")
            'strImageURL.Append("sym=" + ddlShare.SelectedValue) ''Stock code
            'strImageURL.Append("&style=1") ''Graph Style
            ''int: (Interval)
            ''    4 - Daily Chart
            ''    5 - Weekly Chart
            ''    6 - Monthly Chart
            ''    7 - Quarterly Chart
            'strImageURL.Append("&int=4")
            ''per: (Period)
            ''    7 - 3 Months
            ''    8 - 6 Months
            ''    11 - 1 Year
            ''    12 - 2 Years
            ''getQuoteEmailDetails(sRFQID, sMode, dtEmailDetails)
            'strImageURL.Append("&per=11")
            'strImageURL.Append("&w=540") ''Width
            'strImageURL.Append("&h=200") ''Height
            'If dtEmailDetails.Rows.Count > 0 Then
            '    strImageURL.Append("&l=" + dtEmailDetails.Rows(0)("ABSStrike").ToString() + ",404090,1") ''AbsStrike,lineColor,LineStyle
            'End If
            ''strImageURL.Append("&token=2B157F5064B581A671634B2403F9EAAAAC90B9744204D3EA4AAFB995952A182571D71AB3E961D80ABFE91B504277C109C8DEFF69F9A70119F5EA52C9F2FDA9CD940A8E95A6620F94E61539EE1CAEF720")
            'strTokenCon = objReadConfig.ReadConfig(dsConfig, "EQC_QuoteEmailToken", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "token=2B157F5064B581A671634B2403F9EAAAAC90B9744204D3EA4AAFB995952A182571D71AB3E961D80ABFE91B504277C109C8DEFF69F9A70119F5EA52C9F2FDA9CD940A8E95A6620F94E61539EE1CAEF720")
            'strImageURL.Append("&" & strTokenCon)

            ' ''NEW
            'oTRDSS = New Web_TRDSS.TRSetup
            'strImageURL.Append("&token=" + oTRDSS.getReutersAuthentication())
            ' ''END NEW


            Return Bitmap.FromStream(New MemoryStream(tClient.DownloadData(strImageURL.ToString)))
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "getEmailGraphImage", ErrorLevel.High)
            Dim blankImage As Image
            Return blankImage
        End Try
    End Function

    Sub getQuoteEmailDetails(ByVal sRFQID As String, ByVal sMode As String, ByRef dtEmailDetails As DataTable)
        Select Case objELNRFQ.Web_getQuoteEmailDetails(sRFQID, sMode, dtEmailDetails)
            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                ''no data
        End Select
    End Sub
    Sub replaceClassToStyle(ByRef strHtmlText As String)

        Dim strblueHeader As String = "background-color :#003466;color :White ;text-align :left ;height:20px !important;"
        Dim strfontCss As String = "font-family :verdana,tahoma,helvetica;font-size :10pt;color:#03316C;"
        Dim strouterTR As String = "outline: 2px solid #41ABD1;"
        Dim strtrKO As String = ""
        strtrKO = If(chkELNType.Checked, "", "display:none;")
        Dim strtrSpot As String = If(chkELNType.Checked = False, "", "display:none;")

        Dim strtableizertable As String = "font-size: 12px;border: 1px solid #CCC;font-family: Arial, Helvetica, sans-serif;"
        Dim strtableizer_table_th As String = "background-color: #104E8B;color: #FFF;font-weight: bold;"
        Dim strredClass As String = "color: red;font-weight:bold;"
        Dim strblueClass As String = "color: #004d99;font-weight:bold;"
        Dim strgraybackClass As String = "background-color: #C0C0C0;width:110;"

        strHtmlText = strHtmlText.Replace("blueHeader", strblueHeader)
        strHtmlText = strHtmlText.Replace("fontCss", strfontCss)
        strHtmlText = strHtmlText.Replace("outerTR", strouterTR)

        strHtmlText = strHtmlText.Replace("tableizer-table", strtableizertable)
        strHtmlText = strHtmlText.Replace("tableizer-table_th", strtableizer_table_th)
        strHtmlText = strHtmlText.Replace("redClass", strredClass)
        strHtmlText = strHtmlText.Replace("trKO", strtrKO)
        strHtmlText = strHtmlText.Replace("trSpot", strtrSpot)
        strHtmlText = strHtmlText.Replace("blueClass", strblueClass)
        strHtmlText = strHtmlText.Replace("graybackClass", strgraybackClass)

    End Sub

    '23-Nov-2015 Added by Imran/Mangesh for Email best issuer


    ''Mangesh wakode 25 nov 2015 added function for quote mail format(ELN)
    Public Function getQuoteMailHTMLString_ELN(ByVal sUnderlyingTicker As String, ByVal PriceOrStrike As String, ByVal bestPP As String, ByVal Issuer As String, _
                                               ByVal Yield As String, ByRef RFQID As String) As String
        ''23 nov 2015 Mangesh / Imran <START>

        ''Ashwini P 1-Aug-2016
        Dim BestPPMoodys As String
        Dim MoodysRating As String
        Dim SnPRating As String
        Dim FitchRating As String

        Try



            ''23 nov 2015 Mangesh / Imran <END>
            ''25 nov 2015 Mangesh wakode padding added to header
            Dim sbELNMailColumnHeader As StringBuilder
            sbELNMailColumnHeader = New StringBuilder()
            sbELNMailColumnHeader.Append("<br/>")
            sbELNMailColumnHeader.Append("<HTML><HEAD></HEAD>")
            sbELNMailColumnHeader.Append("<BODY dir=3Dltr>")
            sbELNMailColumnHeader.Append("<DIV dir=3Dltr>")
            sbELNMailColumnHeader.Append("<DIV style=3D""FONT-SIZE: 12pt; FONT-FAMILY: 'Calibri'; COLOR: #000000"">")
            sbELNMailColumnHeader.Append("<TABLE style=3D""BORDER-COLLAPSE: collapse; COLOR: #000000; ""=20")
            sbELNMailColumnHeader.Append("cellSpacing=3D0 cellPadding=3D0  border=3D0>")
            sbELNMailColumnHeader.Append("  <TBODY>")
            '<AvinashG. on 07-Jan-2016: Optimization Reduction in size of download file>
            sbELNMailColumnHeader.Append(" <TR style=3D""HEIGHT: 25pt; BACKGROUND: #336699; color:#ffffff"">")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>RFQ ID</TD>")  ''Mangesh wakode 16 dec 2015
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Product</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Issue Date</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Valuation Date</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Maturity Date</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Tenor (d)</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Underlying</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Currency</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Strike</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>KO %</TD>")      ''Added as per new requirement 25 nov 2015
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Price</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Yield</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Issuer</TD>")
            ''Dilkhush/Avinash 09Dec2015: Commented from  Russell mail
            'sbELNMailColumnHeader.Append("    <TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom>")
            'sbELNMailColumnHeader.Append("      <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Upfront</FONT></SPAN></P></TD>")
            sbELNMailColumnHeader.Append(" </TR><TR > ")
            'sbELNMailColumnHeader.Append("  <TR style=3D""HEIGHT: 15pt; BACKGROUND: #336699;"">")
            'sbELNMailColumnHeader.Append("    <TD style=3D""BORDER: windowtext 1pt solid; "" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("      <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D"" FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>RFQ ID</FONT></SPAN></P></TD>")  ''Mangesh wakode 16 dec 2015
            'sbELNMailColumnHeader.Append("    <TD style=3D""BORDER: windowtext 1pt solid; "" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("      <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D"" FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Product</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D"" FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Issue Date</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Valuation Date</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Maturity Date</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Tenor (d)</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Underlying</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;""height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Currency</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Strike</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>KO %</FONT></SPAN></P></TD>")      ''Added as per new requirement 25 nov 2015
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Price</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Yield</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Issuer</FONT></SPAN></P></TD>")
            ' ''Dilkhush/Avinash 09Dec2015: Commented from  Russell mail
            ''sbELNMailColumnHeader.Append("    <TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom>")
            ''sbELNMailColumnHeader.Append("      <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Upfront</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append(" </TR><TR > ")
            '</AvinashG. on 07-Jan-2016: Optimization Reduction in size of download file>
            sbELNMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + RFQID + "</TD>")  ''mangesh wakode 16 dec 2015
            sbELNMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">ELN</TD>")
            ''<Dilkhush:31Dec2015 Date mismatched>
            'sbELNMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + CDate(txtTradeDate.Value.ToString).ToString("dd-MMM-yy") + _
            '                        "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" _
            '                        + CDate(txtSettlementDate.Value.ToString).ToString("dd-MMM-yy") + "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + CDate(txtMaturityDate.Value.ToString).ToString("dd-MMM-yy") + "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
            sbELNMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + CDate(txtSettlementDate.Value.ToString).ToString("dd-MMM-yy") + _
                                    "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" _
                                    + CDate(txtExpiryDate.Value.ToString).ToString("dd-MMM-yy") + "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + CDate(txtMaturityDate.Value.ToString).ToString("dd-MMM-yy") + "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
            ''</Dilkhush:31Dec2015 Date mismatched>
            ''Mangesh wakode 10 dec 2015 sett date - mat.date  for tenor
            sbELNMailColumnHeader.Append(DateDiff(DateInterval.Day, CDate(txtSettlementDate.Value), CDate(txtMaturityDate.Value)).ToString + "</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + sUnderlyingTicker + "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + ddlQuantoCcy.SelectedItem.Text + "</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center; BACKGROUND: #f5fb99;"">")
            If ddlSolveFor.SelectedItem.Value = "PricePercentage" Then
                sbELNMailColumnHeader.Append(txtStrike.Text.Trim + "%")
            Else
                If Not PriceOrStrike Is Nothing Then
                    If PriceOrStrike.Trim <> "" Then
                        sbELNMailColumnHeader.Append(PriceOrStrike + "%")
                    Else
                        sbELNMailColumnHeader.Append("")
                    End If
                Else
                    sbELNMailColumnHeader.Append("")
                End If

            End If
            sbELNMailColumnHeader.Append(" </TD>")
            If chkELNType.Checked = True Then
                sbELNMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + txtBarrier.Text.ToString.Trim + "</TD>")
            Else
                sbELNMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + "N.A." + "</TD>")

            End If
            sbELNMailColumnHeader.Append("</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
            If ddlSolveFor.SelectedItem.Value = "StrikePercentage" Then
                sbELNMailColumnHeader.Append((Double.Parse(txtELNPrice.Text.Trim) + Double.Parse(txtUpfrontELN.Text.Trim)).ToString + "%")
            Else
                If Not PriceOrStrike Is Nothing Then
                    If PriceOrStrike.Trim <> "" Then
                        sbELNMailColumnHeader.Append((Double.Parse(PriceOrStrike) + Double.Parse(txtUpfrontELN.Text.Trim)).ToString + "%")
                    Else
                        sbELNMailColumnHeader.Append("")
                    End If
                Else
                    sbELNMailColumnHeader.Append("")
                End If
            End If

            sbELNMailColumnHeader.Append("</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
            If Not Yield Is Nothing Then
                If Yield.Trim <> "" Then
                    sbELNMailColumnHeader.Append(Yield + "%")
                Else
                    sbELNMailColumnHeader.Append("")
                End If
            Else
                sbELNMailColumnHeader.Append("")
            End If
            sbELNMailColumnHeader.Append("</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
            sbELNMailColumnHeader.Append(Issuer)
            sbELNMailColumnHeader.Append("</TD>")
            ''Dilkhush/Avinash 09Dec2015: Commented from  Russell mail
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + txtUpfrontELN.Text + "%</TD>")

            '' <Ashwini P> 01-Aug-2016 START
            BestPPMoodys = CheckBestPriceForEmail()
            GetIssuerRatingForMail(BestPPMoodys, MoodysRating, SnPRating, FitchRating)
            sbELNMailColumnHeader.Append("<TR><TD>* Issuer Rating: Moody's Rating:  " + MoodysRating + ", S&P Rating:  " + SnPRating + ", Fitch Rating:  " + FitchRating + "</TD></TR>")
            ''</Ashwini P> END


            sbELNMailColumnHeader.Append("</TR></TBODY></TABLE></DIV></DIV></BODY>")
            sbELNMailColumnHeader.Append("</HTML>")
            '</mangesh wakode> 23 Nov 2015 

            Return sbELNMailColumnHeader.ToString
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                  sSelfPath, "GetQuoteMailHTMLString_ELN", ErrorLevel.High)
            Throw ex
        End Try
    End Function

    Private Function getUnderlyingTicker(ByVal sUnderlying As String) As String
        Dim sUnderlyingTicker As String
        Try
            Dim iBBGSeparatorIdx = sUnderlying.IndexOf(CChar("/"))
            Dim iDescSeparatorIdx = sUnderlying.IndexOf(CChar("-"))
            Dim sBBGTicker As String

            If iBBGSeparatorIdx <> -1 Or iDescSeparatorIdx <> -1 Then
                sBBGTicker = sUnderlying.Substring(iBBGSeparatorIdx + 1, iDescSeparatorIdx - iBBGSeparatorIdx - 1)
            Else
                sBBGTicker = ""
            End If
            sUnderlyingTicker = If(sBBGTicker <> "", sBBGTicker, sUnderlying.Substring(sUnderlying.IndexOf(CChar("(")) + 1, sUnderlying.IndexOf(CChar(")")) - 1))
            Return sUnderlyingTicker
        Catch ex As Exception
            Return ""
            Throw ex
        End Try
    End Function

    ''Ashwini P on 01-Aug-2016
    Private Sub GetIssuerRatingForMail(ByVal BestPPMoodys As String, ByRef MoodysRating As String, ByRef SnPRating As String, ByRef FitchRating As String)
        Try
            Dim dtMoodysRating As DataTable
            dtMoodysRating = Nothing
            BestPPMoodys = CheckBestPriceForEmail()
            Select Case objELNRFQ.DB_GetMoodysRatingForMail(BestPPMoodys, dtMoodysRating)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    MoodysRating = dtMoodysRating.Rows(0).Item(0).ToString()
                    SnPRating = dtMoodysRating.Rows(0).Item(1).ToString()
                    FitchRating = dtMoodysRating.Rows(0).Item(2).ToString()
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    ''no data
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
    Private Function checkIssuerLimit(ByVal PPName As String) As Boolean
        Dim min As Double
        Dim max As Double
        Select Case PPName.ToUpper
            Case "JPM"
                min = CDbl(Split(lblJPMlimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblJPMlimit.ToolTip, " / ")(1).Trim)
            Case "HSBC"
                '  lblHSBClimit       
                min = CDbl(Split(lblHSBClimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblHSBClimit.ToolTip, " / ")(1).Trim)
            Case "UBS"
                ' lblUBSlimit       
                min = CDbl(Split(lblUBSlimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblUBSlimit.ToolTip, " / ")(1).Trim)
            Case "CS"
                ' lblCSLimit       
                min = CDbl(Split(lblCSLimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblCSLimit.ToolTip, " / ")(1).Trim)
            Case "BNPP"
                ' lblBNPPlimit       
                min = CDbl(Split(lblBNPPlimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblBNPPlimit.ToolTip, " / ")(1).Trim)
            Case "BAML"
                ' lblBAMLlimit       
                min = CDbl(Split(lblBAMLlimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblBAMLlimit.ToolTip, " / ")(1).Trim)
                'Case "DBIB"  ''<EQBOSDEV-430:Rename DBIB entity to DB on PROD>
            Case "DB"
                '  lblDBIBlimit       
                min = CDbl(Split(lblDBIBlimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblDBIBlimit.ToolTip, " / ")(1).Trim)
	    Case "OCBC"
                '  lblOCBClimit       
                min = CDbl(Split(lblOCBClimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblOCBClimit.ToolTip, " / ")(1).Trim)
	     Case "CITI"
                '  lblCITIlimit       
                min = CDbl(Split(lblCITIlimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblCITIlimit.ToolTip, " / ")(1).Trim)
            Case "LEONTEQ"
                '  lblLEONTEQlimit       
                min = CDbl(Split(lblLEONTEQlimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblLEONTEQlimit.ToolTip, " / ")(1).Trim)
            Case "COMMERZ"
                '  lblCOMMERZlimit       
                min = CDbl(Split(lblCOMMERZlimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblCOMMERZlimit.ToolTip, " / ")(1).Trim)
        End Select

        Dim Notional As Double = Convert.ToDouble(txtQuantity.Text)
        If (Notional < min) Then
            lblerror.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            Return False
        ElseIf (Notional > max) Then
            lblerror.Text = "Can not place order. Notional size is larger than the maximum permitted."
            Return False
        Else
            Return True
        End If




    End Function
    ''14Jan2016
    ''Rushikesh to set share value for grid selection and for other operations
    Private Sub setShare(ByVal strExchng As String, ByVal strShareVal As String)
        Try
            Dim dtSelectedShare As DataTable
            dtSelectedShare = Nothing
            Select Case objELNRFQ.Db_Get_Selected_Share("EQ", strExchng, "", strShareVal, dtSelectedShare)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlShare
                        .Items.Clear()
                        .DataSource = dtSelectedShare
                        .DataTextField = "LongName"
                        .DataValueField = "Code"
                        .DataBind()
                        .SelectedIndex = 0
                    End With
                    ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShareVal))
                    ddlShare.Text = ddlShare.SelectedItem.Text                'Mohit Lalwani on 15-Apr-2016 FA-1392
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlShare
                        .DataSource = dtShare
                        .DataBind()
                    End With
            End Select
        Catch ex As Exception

        End Try
    End Sub
    ''Avinash 28Dec2015 For leng roundup and roundDown
    '   Private Delegate Function RoundingFunction(ByVal value As Double) As Double

    '   Private Enum RoundingDirection
    '       Up
    '       Down
    '   End Enum

    '   Public Function RoundUp(ByVal value As Double, ByVal precision As Integer) As Double
    '       Return Round(value, precision, RoundingDirection.Up)
    '   End Function

    '   Public Function RoundDown(ByVal value As Double, ByVal precision As Integer) _
    'As Double
    '       Return Round(value, precision, RoundingDirection.Down)
    '   End Function

    '   Private Function Round(ByVal value As Double, ByVal precision As Integer, _
    'ByVal roundingDirection As RoundingDirection) As Double
    '       Dim roundingFunction As RoundingFunction
    '       If roundingDirection = roundingDirection.Up Then
    '           roundingFunction = AddressOf Math.Ceiling
    '       Else
    '           roundingFunction = AddressOf Math.Floor
    '       End If
    '       value = value * Math.Pow(10, precision)
    '       value = roundingFunction(value)
    '       Return value * Math.Pow(10, -1 * precision)
    '   End Function
    'Mohit Lalwani on 20-Jan-2015
    Private Sub btnDetailsCancle_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailsCancle.ServerClick
        pnlDetailsPopup.Visible = False
        upnlDetails.Update()
        ShowHideDeatils(False)
    End Sub
    '/Mohit Lalwani on 20-Jan-2015
    Private Function FillRFQDataTable(ByVal sPP_ID As String) As Boolean
        Dim sRFQId As String
        Try
            Dim dtRFQInsertData As DataTable

            Dim sProductDefinition As String = ""

            objELNRFQ.GetUDTSchema("udtELNRFQ", dtRFQInsertData)

            dtRFQInsertData.Rows.Add()
            dtRFQInsertData.Rows(0)("ER_PP_ID") = sPP_ID
            If chkELNType.Checked = True Then
                dtRFQInsertData.Rows(0)("ER_BarrierMonitoringMode") = ddlBarrier.SelectedValue
                dtRFQInsertData.Rows(0)("ER_Type") = "Barrier"
                sProductDefinition += "Barrier"

            Else
                dtRFQInsertData.Rows(0)("ER_BarrierMonitoringMode") = ""
                dtRFQInsertData.Rows(0)("ER_Type") = "Simple"
                sProductDefinition += "Simple"
            End If
            dtRFQInsertData.Rows(0)("ER_SettlmentDate") = Convert.ToString(Session("Settlementdate"))
            sProductDefinition += Convert.ToString(Session("Settlementdate"))

            dtRFQInsertData.Rows(0)("ER_ExpiryDate") = Convert.ToString(Session("expiryDAte"))
            sProductDefinition += Convert.ToString(Session("expiryDAte"))

            dtRFQInsertData.Rows(0)("ER_MaturityDate") = Convert.ToString(Session("MaturityDAte"))
            sProductDefinition += Convert.ToString(Session("MaturityDAte"))

            dtRFQInsertData.Rows(0)("ER_StrikePercentage") = Val(txtStrike.Text)
            sProductDefinition += Val(txtStrike.Text).ToString + "0.0"

            dtRFQInsertData.Rows(0)("ER_BarrierPercentage") = txtBarrier.Text
            dtRFQInsertData.Rows(0)("ER_InterBankPrice") = Val(txtELNPrice.Text)
            If ddlSolveFor.SelectedValue.ToUpper = "PRICEPERCENTAGE" Then
                dtRFQInsertData.Rows(0)("ER_InterBankPriceSolve_YN") = "Y"
            Else
                dtRFQInsertData.Rows(0)("ER_InterBankPriceSolve_YN") = "N"
            End If
            dtRFQInsertData.Rows(0)("ER_UnderlyingCode_Type") = "RIC"
            dtRFQInsertData.Rows(0)("ER_UnderlyingCode") = ddlShare.SelectedValue.ToString
            sProductDefinition += ddlShare.SelectedValue.ToString
            dtRFQInsertData.Rows(0)("ER_TenorType") = ddlTenorTypeELN.SelectedValue
            dtRFQInsertData.Rows(0)("ER_Tenor") = CInt(txtTenor.Text)
            dtRFQInsertData.Rows(0)("ER_TradeDate") = Convert.ToString(Session("TradeDAte"))
            dtRFQInsertData.Rows(0)("ER_Price") = Val(txtELNPrice.Text)
            'dtRFQInsertData.Rows(0)("ER_QuoteRequestId") = '''' To be returned after insertion
            dtRFQInsertData.Rows(0)("ER_SecurityDescription") = "ELN"
            dtRFQInsertData.Rows(0)("ER_QuoteType") = "0"
            dtRFQInsertData.Rows(0)("ER_BuySell") = "Buy"
            '<Added by AshwiniP on 05-Oct-2016 : for notional Validation>
            If Qty_Validate(txtQuantity.Text) = False Then
                Exit Function
            End If
            Try
                txtQuantity.Text = FinIQApp_Number.ConvertFormattedAmountToNumber(txtQuantity.Text).ToString
                txtQuantity.Text = SetNumberFormat(txtQuantity.Text, 0)
                dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(txtQuantity.Text, ",", "")
            Catch ex As Exception
                lblerror.Text = "Please enter valid Notional"
                Exit Function
            End Try
            '</Added by AshwiniP on 05-Oct-2016>
            '' dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(txtQuantity.Text, ",", "")
            If chkQuantoCcy.Checked = True Then
                dtRFQInsertData.Rows(0)("ER_CashCurrency") = ddlQuantoCcy.SelectedValue
            Else
                dtRFQInsertData.Rows(0)("ER_CashCurrency") = lblELNBaseCcy.Text
            End If
            dtRFQInsertData.Rows(0)("ER_TransactionTime") = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss.fff")
            dtRFQInsertData.Rows(0)("ER_BidPrice") = 0
            ''  Confirm by checking insertion
            dtRFQInsertData.Rows(0)("ER_ProductDefinition") = sProductDefinition
            dtRFQInsertData.Rows(0)("ER_Text") = "2012_QR12"
            dtRFQInsertData.Rows(0)("ER_Symbol") = ""
            dtRFQInsertData.Rows(0)("ER_AveragePrice") = 0
            dtRFQInsertData.Rows(0)("ER_Created_By") = LoginInfoGV.Login_Info.LoginId
            dtRFQInsertData.Rows(0)("ER_Remark1") = ""
            dtRFQInsertData.Rows(0)("ER_Remark2") = ""
            'dtRFQInsertData.Rows(0)("ER_Misc1") = ""
            'dtRFQInsertData.Rows(0)("ER_Misc2") =
            dtRFQInsertData.Rows(0)("ER_Active_YN") = "Y"
            dtRFQInsertData.Rows(0)("ER_SubScheme") = ""
            If ddlExchange.SelectedValue.ToUpper = "ALL" Then
                Dim sTemp As String
                dtRFQInsertData.Rows(0)("ER_Exchange") = objELNRFQ.GetShareExchange(ddlShare.SelectedValue.ToString, sTemp)
            Else

                dtRFQInsertData.Rows(0)("ER_Exchange") = ddlExchange.SelectedValue

            End If

            dtRFQInsertData.Rows(0)("ER_Quote_Request_YN") = "Y"
            dtRFQInsertData.Rows(0)("ER_Entity_ID") = LoginInfoGV.Login_Info.EntityID.ToString
            dtRFQInsertData.Rows(0)("ER_Issuer_Date_Offset") = txtValueDays.Text
            dtRFQInsertData.Rows(0)("ER_Template_ID") = Convert.ToString(Session("Template_Code"))
            dtRFQInsertData.Rows(0)("ER_Frequency") = ""
            dtRFQInsertData.Rows(0)("ER_Nominal_Amount") = "0"
            dtRFQInsertData.Rows(0)("ER_Upfront") = Val(txtUpfrontELN.Text) * 100
            dtRFQInsertData.Rows(0)("ER_SolveFor") = ddlSolveFor.SelectedValue
            dtRFQInsertData.Rows(0)("ER_EntityName") = ddlentity.SelectedItem.Text
            Dim strLoginUserEmailID As String
            strLoginUserEmailID = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)
            dtRFQInsertData.Rows(0)("ER_EmailId") = strLoginUserEmailID
            dtRFQInsertData.Rows(0)("ER_Branch") = lblbranch.Text
            dtRFQInsertData.Rows(0)("ER_RFQ_RMName") = ddlRFQRM.SelectedItem.Value
            If ddlSolveFor.SelectedValue = "PricePercentage" Then
                dtRFQInsertData.Rows(0)("EP_StrikePercentage") = Val(txtStrike.Text)
            Else
                dtRFQInsertData.Rows(0)("EP_OfferPrice") = Val(txtELNPrice.Text)
            End If
            dtRFQInsertData.Rows(0)("ER_RFQ_Source") = "MONOTAB_PRICER"
            '<AvinashG. on 24-Sep-2016: To add ELN long days>
            dtRFQInsertData.Rows(0)("ER_LongDays") = DateDiff(DateInterval.Day, CDate(Convert.ToString(Session("Settlementdate"))), CDate(Convert.ToString(Session("MaturityDAte"))))
            '</AvinashG. on 24-Sep-2016: To add ELN long days>
            If flag = "I" Then
                '' To decide logic for Individual or Group RFQ in type
                ''dtRFQInsertData.Rows(0)("EP_Remark2") = Convert.ToString(Session("Quote_ID"))
            End If
            Select Case objELNRFQ.Insert_Dt_ELN_RFQ(dtRFQInsertData, sRFQId)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    Session.Add("Quote_ID", sRFQId)
            End Select
            FillRFQDataTable = True           ''Added by AshwiniP on 05-Oct-2016

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''Dilkhush 13May2016 FA1427
    Public Sub fill_KO()
        Try
            With ddlBarrier
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SHOW_DAILYCLOSE_KO", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        .Items.Add(New DropDownListItem("Intraday", "CONTINUOUS"))
                        .Items.Add(New DropDownListItem("Day close", "DAILY_CLOSE"))
                    Case "N", "NO"
                        .Items.Add(New DropDownListItem("Intraday", "CONTINUOUS"))
                End Select
            End With
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                       sSelfPath, "fill_KO", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    'Mohit Lalwani on 1-Aug-2016
    Private Sub ddlBookingBranchPopUpValue_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBookingBranchPopUpValue.SelectedIndexChanged
        RestoreSolveAll()
        RestoreAll()

        ''Change done by PriyaB: 05Nov2016
        ''On change of book center .. reset customer grid.
        Session.Remove("dtELNPreTradeAllocation")
        Dim tempDt As DataTable
        tempDt = New DataTable("tempDt")
        tempDt.Columns.Add("RM_Name", GetType(String))
        tempDt.Columns.Add("Account_Number", GetType(String))
        tempDt.Columns.Add("AlloNotional", GetType(String))
        tempDt.Columns.Add("Cust_ID", GetType(String))
        tempDt.Columns.Add("DocId", GetType(String))
        tempDt.Columns.Add("EPOF_OrderId", GetType(String))
        tempDt.Rows.InsertAt(tempDt.NewRow(), 0)
        Session.Add("dtELNPreTradeAllocation", tempDt)
        grdRMData.DataSource = tempDt
        grdRMData.DataBind()
        For Each row As GridViewRow In grdRMData.Rows
            If row.RowType = DataControlRowType.DataRow Then
                row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = True
            End If
            OnCheckedChanged(CType(grdRMData.Rows((0)).Cells(0).FindControl("CheckBox1"), CheckBox), Nothing)
        Next
    End Sub
    '/Mohit Lalwani on 1-Aug-2016

    'Mohit Lalwani on 1-Aug-2016
    Private Sub txtOrderCmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderCmt.TextChanged
        RestoreSolveAll()
        RestoreAll()
    End Sub
    '/Mohit Lalwani on 1-Aug-2016
Private Sub Generate(ByVal DealNO As String)
        Try
            Dim objDocGen As Web_DocGen.DocGen
            objDocGen = New Web_DocGen.DocGen
            objDocGen.Url = LoginInfoGV.Login_Info.WebServicePath & "/DocumentGeneration/DocGen.asmx"
            Dim O_Outputfile() As String
            Dim strDocGenVirtualPath As String = String.Empty
            strDocGenVirtualPath = objReadConfig.ReadConfig(New DataSet, "WebDocGen_VirtualPath", "DocGen", CStr(LoginInfoGV.Login_Info.EntityID), "")
            objDocGen.Generate_EQC(DealNO, LoginInfoGV.Login_Info.LoginId, LoginInfoGV.Login_Info.EntityID, "PUBLISH_TERMSHEET", "ELN_RFQ", O_Outputfile)
            If Not IsNothing(O_Outputfile) Then
                If O_Outputfile.Length > 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSgetDescription5446", "window.open('" & LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/" & O_Outputfile(O_Outputfile.Length - 1).ToString & "','CUSTOMER_PROFILE','scrollbars=yes,resizable=yes,menubar=0,status=0,width=1280,height=650,top=0,left=0');", True)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSSpineer", "setSpineer();", True)
                Else
                    lblError.Text = "Document generation failed !"
                End If
            Else
            End If

        Catch ex As Exception

        End Try
    End Sub


    ''<Nikhil M: Commented for Calucation of Price on Upfront Change 24Aug16>

    Public Sub SetClientYeild(ByVal hiddenPrice As HiddenField, ByVal lblClientPrice As Label, _
                        ByVal lblClientYield As Label, ByVal upfront As TextBox)
        Try
            If Val(hiddenPrice.Value) <> 0.0 Then
                lblClientPrice.Text = FormatNumber((Val(hiddenPrice.Value) + Val(upfront.Text)).ToString, 2)
                '''lblClientYield.Text = FormatNumber(get_ELN_ClientYield(CDbl(lblClientPrice.Text)), 2)
                lblClientYield.Text = FormatNumber(get_ELN_ClientYield(CDbl(lblClientPrice.Text)), 4) '''DK
            End If

        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                       sSelfPath, "SetClientYeild", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    ''</Nikhil M: Commented for Calucation of Price on Upfront Change 24Aug16>
     Private Sub Generate_Deal(ByVal DealNO As String)
        Try
            Dim objDocGen As Web_DocGen.DocGen
            objDocGen = New Web_DocGen.DocGen
            objDocGen.Url = LoginInfoGV.Login_Info.WebServicePath & "/DocumentGeneration/DocGen.asmx"
            Dim O_Outputfile() As String
            Dim strDocGenVirtualPath As String = String.Empty
            strDocGenVirtualPath = objReadConfig.ReadConfig(New DataSet, "WebDocGen_VirtualPath", "DocGen", CStr(LoginInfoGV.Login_Info.EntityID), "")
            objDocGen.Generate_EQC(DealNO, LoginInfoGV.Login_Info.LoginId, LoginInfoGV.Login_Info.EntityID, "PUBLISH_TERMSHEET", "RM_ORDER", O_Outputfile)
            If Not IsNothing(O_Outputfile) Then
                If O_Outputfile.Length > 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSgetDescription5446", "window.open('" & LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/" & O_Outputfile(O_Outputfile.Length - 1).ToString & "','CUSTOMER_PROFILE','scrollbars=yes,resizable=yes,menubar=0,status=0,width=1280,height=650,top=0,left=0');", True)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSSpineer", "setSpineer();", True)
                Else
                    lblerror.Text = "Document generation failed !"
                End If
            Else
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub addPPimg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles addPPimg.Click
        Try
            chkPPmaillist.Visible = True
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                       sSelfPath, "addPPimg_Click", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Function GetBestPriceConfirm(ByVal BasePrice As String) As Boolean ''<Nikhil M. on 20-Sep-2016: Removed Parmetter , ByVal IssureName As String>
        Try

            Dim temp As HiddenField
            temp = getBestPrice(JpmHiddenPrice, HsbcHiddenPrice)
            temp = getBestPrice(temp, UbsHiddenPrice)
            temp = getBestPrice(temp, CsHiddenPrice)
            temp = getBestPrice(temp, BNPPHiddenPrice)
            temp = getBestPrice(temp, BAMLHiddenPrice)
            temp = getBestPrice(temp, DBIBHiddenPrice)
            temp = getBestPrice(temp, OCBCHiddenPrice)
            temp = getBestPrice(temp, CITIHiddenPrice)
            temp = getBestPrice(temp, LEONTEQHiddenPrice)
            temp = getBestPrice(temp, COMMERZHiddenPrice)

            If temp.Value.Split(CChar(","))(0) = BasePrice.Split(CChar(","))(0) Then

            Else
                displayReason()
            End If
        Catch ex As Exception
            lblerror.Text = "GetBestPriceConfirm: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                     sSelfPath, "GetBestPriceConfirm", ErrorLevel.High)
        End Try
    End Function



    ''< Start | Nikhil M. on 16-Sep-2016: Added>
    Private Function displayReason() As Boolean
        Try
            Dim DtReason As DataTable
            DtReason = New DataTable("Dummy")
            Select Case WebCommonFunction.DB_Get_Common_Data("EQC_Dealconfirmation_reason", DtReason)
                Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                    drpConfirmDeal.DataTextField = "Data_Value"
                    drpConfirmDeal.DataValueField = "Misc1"
                    drpConfirmDeal.DataSource = DtReason
                    drpConfirmDeal.DataBind()
                    drpConfirmDeal.Items.Insert(0, New DropDownListItem("", ""))
                    drpConfirmDeal.SelectedIndex = 0
                Case Web_CommonFunction.Database_Transaction_Response.DB_Unsuccessful, Web_CommonFunction.Database_Transaction_Response.Db_No_Data
                    drpConfirmDeal.DataSource = DtReason
                    drpConfirmDeal.DataBind()
            End Select
        Catch ex As Exception
            lblerror.Text = "displayReason: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                      sSelfPath, "displayReason", ErrorLevel.High)
        End Try
    End Function

    ''< Start | Nikhil M. on 16-Sep-2016: Added>
   'Added by Chitralekha on 12-Sept-16
    Protected Sub EnableTimerTick()
        Try
            ChkGridRefreshRate()
            ' fillGrid()
        Catch ex As Exception
		Throw ex
        End Try
    End Sub

    Private Sub ChkGridRefreshRate()
        Try
            interval = objReadConfig.ReadConfig(dsConfig, "EQC_Pricer_RefreshTimeInSec", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "60")
            If String.Compare(interval, "0") <> 0 Then
                TmRefresh.Interval = CInt(interval) * 1000
                TmRefresh.Enabled = True
            End If
        Catch ex As Exception
		Throw ex
        End Try
    End Sub

    Private Sub TmRefresh_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TmRefresh.Tick
        Try
            interval = objReadConfig.ReadConfig(dsConfig, "EQC_Pricer_RefreshTimeInSec", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "60")
            If String.Compare(interval, "0") <> 0 Then
                If rbHistory.SelectedValue.Trim = "Order History" Then
                fill_OrderGrid() ' changed by Chitralekha on 12-sep-16
                End If
            End If

        Catch ex As Exception

            lblerror.Text = "Error occurred in Processing."

            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "TmRefresh_Tick", ErrorLevel.High)
        End Try
    End Sub
    'Ended by Chitralekha on 12-sept-16
    ''<Start  | Nikhil M. on 17-Sep-2016: Added>
    Private Sub chkBNPP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBNPP.CheckedChanged
        hdnBestIssuer.Value = lblBNPPPrice.Text
        hdnBestProvider.Value = "BNPP"
        hdnstrBestClientYeild.Value = lblBNPPClientYield.Text
        CheckUncheck(chkBNPP)
        ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
        If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
            EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
        End If
    End Sub

    Private Sub chkCITI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCITI.CheckedChanged
        hdnBestIssuer.Value = lblCITIPrice.Text
        hdnBestProvider.Value = "CITI"
        hdnstrBestClientYeild.Value = lblCITIClientYield.Text
        CheckUncheck(chkCITI)
        ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
        If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
            EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
        End If
    End Sub

    Private Sub chkCS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCS.CheckedChanged
        hdnBestIssuer.Value = lblCSPrice.Text
        hdnBestProvider.Value = "CS"
        hdnstrBestClientYeild.Value = lblCSClientYield.Text
        CheckUncheck(chkCS)
        ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
        If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
            EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
        End If
    End Sub

    Private Sub chkDBIB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDBIB.CheckedChanged
        hdnBestIssuer.Value = lblDBIBPrice.Text
        hdnBestProvider.Value = "DBIB"
        hdnstrBestClientYeild.Value = lblDBIBClientYield.Text
        CheckUncheck(chkDBIB)
        ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
        If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
            EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
        End If
    End Sub

    Private Sub chkHSBC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHSBC.CheckedChanged
        hdnBestIssuer.Value = lblHSBCPrice.Text
        hdnBestProvider.Value = "HSBC"
        hdnstrBestClientYeild.Value = lblHSBCClientYield.Text
        CheckUncheck(chkHSBC)
        ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
        If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
            EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
        End If
    End Sub

    Private Sub chkUBS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUBS.CheckedChanged
        hdnBestIssuer.Value = lblUBSPrice.Text
        hdnBestProvider.Value = "UBS"
        hdnstrBestClientYeild.Value = lblUBSClientYield.Text
        CheckUncheck(chkUBS)
        ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
        If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
            EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
        End If
    End Sub

    Private Sub chkJPM_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkJPM.CheckedChanged
        hdnBestIssuer.Value = lblJPMPrice.Text
        hdnBestProvider.Value = "JPM"
        hdnstrBestClientYeild.Value = lblJPMClientYield.Text
        CheckUncheck(chkJPM)
        ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
        If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
            EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
        End If
    End Sub

  

    Public Function CheckUncheck(ByVal tempCheckBox As CheckBox) As CheckBox
        Try
            Dim flagSHowHide As Boolean
            If tempCheckBox.Checked = True Then
                flagSHowHide = False
            Else
                flagSHowHide = True
            End If
            chkCITI.Checked = False
            chkJPM.Checked = False
            chkHSBC.Checked = False
            chkBNPP.Checked = False
            chkBAML.Checked = False
            chkUBS.Checked = False
            chkCS.Checked = False
            chkOCBC.Checked = False
            chkDBIB.Checked = False
            chkLEONTEQ.Checked = False
            chkCOMMERZ.Checked = False
            If flagSHowHide = True Then
                CheckBestPrice()
                tempCheckBox.Checked = False
            Else
                tempCheckBox.Checked = True
            End If
            GetCommentary()
            RestoreSolveAll()
            RestoreAll()
        Catch ex As Exception
            lblerror.Text = "CheckUncheck : Error occurred in Processing."

            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "CheckUncheck", ErrorLevel.High)
        End Try
   
    End Function

    ''<End  | Nikhil M. on 17-Sep-2016: Added>
    ''' <remarks>'Mohit lalwani on 7-Sept-2016 for Personal Settings</remarks>
    Private Function getControlPersonalSetting(ByVal ControlName As String, ByVal DefaultValue As String) As String
        Dim dtGetLoginPP As DataTable
        Dim dtPersonalSetting As DataTable

        Dim dr1 As DataRow()
        Dim dr2 As DataRow()
        Try
            dtPersonalSetting = New DataTable("Personal Setting")
            dtPersonalSetting = CType(Session("dtPersonalSettings"), DataTable)
            If dtPersonalSetting.Rows.Count > 0 Then
                dr1 = dtPersonalSetting.Select("Control_Name ='" + ControlName.ToUpper + "' and Setting_Type ='USER' and Login_Id ='" + LoginInfoGV.Login_Info.LoginId + "'")
                dr2 = dtPersonalSetting.Select("Control_Name ='" + ControlName.ToUpper + "'  and Setting_Type ='ENTITY'")
                If dr1.Length > 0 Then
                    getControlPersonalSetting = dr1(0).Item("Default_Value").ToString
                ElseIf dr2.Length > 0 Then
                    getControlPersonalSetting = dr2(0).Item("Default_Value").ToString
                Else
                    getControlPersonalSetting = DefaultValue
                End If
            Else
                getControlPersonalSetting = DefaultValue
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
     '/Mohit lalwani on 7-Sept-2016 for Personal Settings

    '<Rushikesh Start>
      Private Sub funAdd()
        Try
           
            Dim tempDt As DataTable
            tempDt = New DataTable("tempDt")
            If CType(Session("dtELNPreTradeAllocation"), DataTable) Is Nothing Then
            Else
                tempDt = CType(Session("dtELNPreTradeAllocation"), DataTable)
            End If


            Dim MaxRM_Allocation As Integer = 0
            MaxRM_Allocation = CInt(objReadConfig.ReadConfig(dsConfig, "EQC_Allocation_MaxAllowedRecords", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "3").Trim.ToUpper())
            If tempDt IsNot Nothing Then
                If tempDt.Rows.Count >= MaxRM_Allocation Then
                    lblerrorPopUp.Text = "Can not allocate order to more than " + MaxRM_Allocation.ToString + " records."
                    Exit Sub
                End If
            End If

            If tempDt.Rows.Count = 0 Then
                tempDt.Columns.Add("RM_Name", GetType(String))
                tempDt.Columns.Add("Account_Number", GetType(String))
                tempDt.Columns.Add("AlloNotional", GetType(String))
                tempDt.Columns.Add("Cust_ID", GetType(String))
                tempDt.Columns.Add("DocId", GetType(String))
                tempDt.Columns.Add("EPOF_OrderId", GetType(String))
                tempDt.Rows.InsertAt(tempDt.NewRow(), 0)
            Else
                tempDt.Rows.InsertAt(tempDt.NewRow(), tempDt.Rows.Count)
            End If

            Session.Add("dtELNPreTradeAllocation", tempDt)

            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            
            If tempDt IsNot Nothing Then
                Dim chkedRows(MaxRM_Allocation + 1) As Integer
                Dim i As Integer = 0
                Dim iDr As Integer = 0
                For Each row As GridViewRow In grdRMData.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim isChecked As Boolean = row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked
                        If isChecked Then
                            chkedRows(i) = iDr
                            i = i + 1
                        End If
                    End If
                    iDr = iDr + 1
                Next
                grdRMData.DataSource = tempDt
                grdRMData.DataBind()

                Dim k As Integer
                ''Dim count As Integer = CType(tempDt.Rows.Count, Integer)
                For k = 0 To tempDt.Rows.Count - 1

                    CType(grdRMData.Rows((k)).Cells(0).FindControl("CheckBox1"), CheckBox).Checked = True
                    OnCheckedChanged(CType(grdRMData.Rows((k)).Cells(0).FindControl("CheckBox1"), CheckBox), Nothing)
                    CType(grdRMData.Rows((k)).Cells(0).FindControl("ddlRMName"), RadDropDownList).SelectedText = tempDt.Rows(k).Item("RM_Name").ToString

                    ddl_OnSelectedIndexChanged(CType(grdRMData.Rows(k).Cells(0).FindControl("ddlRMName"), RadDropDownList), Nothing) 'Mohit Lalwani on 04-Nov-2016

                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).setCustName = tempDt.Rows(k).Item("Account_Number").ToString
                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCustomerName = tempDt.Rows(k).Item("Account_Number").ToString
                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCustomerId = tempDt.Rows(k).Item("Cust_ID").ToString
                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenDocId = tempDt.Rows(k).Item("DocId").ToString
                    'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAllocationRMName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper  ''Commented by AshwiniP on 13-Oct-2016
                    '    Case "Y", "YES"

                    'End Select
                Next

            End If
            'fill_RMListAllocation()
        Catch ex As Exception
            ''lblError.Text = "Error occurred while allocating the notional."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                          sSelfPath, "funAdd", ErrorLevel.High)
            Throw ex
        End Try

    End Sub

    Protected Sub OnCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim isUpdateVisible As Boolean = False
        Dim chk As CheckBox = TryCast(sender, CheckBox)
        Dim tempDt As DataTable
        Try
            ''Dim maxAcclength As String = objReadConfig.ReadConfig(dsConfig, "EQC_PostTrade_AccountNumLength", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "10").Trim.ToUpper   ''Commented by AshwiniP on 13-Oct-2016
            tempDt = CType(Session("dtELNPreTradeAllocation"), DataTable)
            If chk.ID = "chkAll" Then
                For Each row As GridViewRow In grdRMData.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = chk.Checked
                    End If
                Next
            End If
            Dim chkAll As CheckBox = TryCast(grdRMData.HeaderRow.FindControl("chkAll"), CheckBox)
            chkAll.Checked = True
            Dim k As Integer = 0
            For Each row As GridViewRow In grdRMData.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    If row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = True Then
                        tempDt.Rows(k)("EPOF_OrderId") = "Check"
                    Else
                        tempDt.Rows(k)("EPOF_OrderId") = "Uncheck"
                    End If
                End If
                k += 1
            Next
            For Each row As GridViewRow In grdRMData.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim isChecked As Boolean = row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked

                    For i As Integer = 1 To row.Cells.Count - 1

                        row.Cells(i).Controls.OfType(Of Label)().FirstOrDefault().Visible = Not isChecked
                        If row.Cells(i).Controls.OfType(Of TextBox)().ToList().Count > 0 Then
                            row.Cells(i).Controls.OfType(Of TextBox)().FirstOrDefault().Visible = isChecked
                           row.Cells(i).Controls.OfType(Of TextBox)().FirstOrDefault().Attributes.Add("onkeypress", "KeysAllowedForNotional()")

                            ''<Nikhil M. on 24-Sep-2016: Commented>
                            'If row.Cells(i).Controls.OfType(Of RadDropDownList)().FirstOrDefault().ID = "txtAccNo" Then
                            '    row.Cells(i).Controls.OfType(Of RadDropDownList)().FirstOrDefault().Attributes.Add("MaxLength", maxAcclength) ''29dec2015
                            'End If
                        End If

                        If i = 1 Then
                            If row.Cells(i).Controls.OfType(Of RadDropDownList)().ToList().Count > 0 Then
                                row.Cells(i).Controls.OfType(Of RadDropDownList)().FirstOrDefault().Visible = isChecked
                                If row.Cells(i).Controls.OfType(Of Label)().FirstOrDefault().Text.Replace(" ", "") = "" Then
                                    row.Cells(i).Controls.OfType(Of RadDropDownList)().FirstOrDefault().SelectedIndex = 0
                                Else
                                    ''row.Cells(i).Controls.OfType(Of DropDownList)().FirstOrDefault().SelectedIndex = row.Cells(i).Controls.OfType(Of DropDownList)().FirstOrDefault().Items.IndexOf(row.Cells(i).Controls.OfType(Of DropDownList)().FirstOrDefault().Items.FindByText(row.Cells(i).Controls.OfType(Of Label)().FirstOrDefault().Text))
                                    row.Cells(i).Controls.OfType(Of RadDropDownList)().FirstOrDefault().SelectedIndex = row.Cells(i).Controls.OfType(Of RadDropDownList)().FirstOrDefault().FindItemByText(row.Cells(i).Controls.OfType(Of Label)().FirstOrDefault().Text).Index

                                End If
                            End If
                        End If

                        ''''<Nikhil M. on 26-Sep-2016: For CustPan>
                        If i = 2 Then
                            If row.Cells(i).Controls.OfType(Of FinIQ_Fast_Find_Customer)().ToList().Count > 0 Then
                                row.Cells(i).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault().Visible = isChecked
                                If row.Cells(i).Controls.OfType(Of Label)().FirstOrDefault().Text.Replace(" ", "") = "" Then
                                    row.Cells(i).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault().setCustName = ""
                                    row.Cells(i).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault().HiddenCustomerId = ""
                                    row.Cells(i).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault().HiddenDocId = ""


                                Else
                                    row.Cells(i).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault().setCustName = row.Cells(i).Controls.OfType(Of Label)().FirstOrDefault().Text
                                    row.Cells(i).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault().HiddenCustomerId = tempDt.Rows(row.RowIndex).Item("Cust_ID").ToString
                                    row.Cells(i).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault().HiddenDocId = tempDt.Rows(row.RowIndex).Item("DocId").ToString
                                End If
                            End If
                        End If

                        If isChecked AndAlso Not isUpdateVisible Then
                            isUpdateVisible = True
                        End If
                        If Not isChecked Then
                            chkAll.Checked = False
                        End If
                    Next
                End If
            Next
            GetTotalRemainAlloLables()
            lblerrorPopUp.Text = ""
        Catch ex As Exception
            lblError.Text = "Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                          sSelfPath, "OnCheckedChanged", ErrorLevel.High)
        End Try
    End Sub


    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim tempDt As DataTable
                tempDt = CType(Session("dtELNPreTradeAllocation"), DataTable)




                Dim rowDataBoundIndex As Integer = e.Row.RowIndex

                Dim ddlRM_Name As RadDropDownList = TryCast(e.Row.FindControl("ddlRMName"), RadDropDownList)

                Dim lblRM_Name As Label
                If Not IsNothing(TryCast(e.Row.FindControl("txtRM_Name"), Label)) Then
                    lblRM_Name = TryCast(e.Row.FindControl("txtRM_Name"), Label)
                Else
                    lblRM_Name.Text = ""
                End If
                With ddlRM_Name
                    .DataSource = Session("ELN_DTRMList")
                    .DataTextField = "Host"
                    .DataValueField = "Rel_Manager_Name"
                    .DataBind()
                    .Items.Insert(0, "")
                End With
		 ''<Nikhil M : 28Sep16 >
                If Not ddlRM_Name Is Nothing Then
                    ddlRM_Name.FindItemByText(lblRM_Name.Text).Selected = True
                End If

                ''<Nikhil M. on 26-Sep-2016: Added >
                Dim ddlCIFPAN As FinIQ_Fast_Find_Customer = TryCast(e.Row.FindControl("FindCustomer"), FinIQ_Fast_Find_Customer)

                Dim lblCIFPAN As Label
                If Not IsNothing(TryCast(e.Row.FindControl("lblCIFPAN"), Label)) Then
                    lblCIFPAN = TryCast(e.Row.FindControl("lblCIFPAN"), Label)
                Else
                    lblCIFPAN.Text = ""
                End If

 		''<Nikhil M : 28Sep16 >
         '       If lblRM_Name.Text <> "" Then
       '             Dim dtCIFPANTemp As DataTable
       '             dtCIFPANTemp = New DataTable("dtCIFPANTemp")
        '            SetDrpCIFPAN(LoginInfoGV.Login_Info.EntityID.ToString, ddlRM_Name.SelectedValue, dtCIFPANTemp)
        '            With ddlCIFPAN
         '               .DataTextField = "Name"
          '              .DataValueField = "CIF_PANId"
           '             .DataSource = dtCIFPANTemp
            '            .DataBind()
             '           .Items.Insert(0, "")
      '              End With
       '         Else
        '            With ddlCIFPAN
         '               .DataBind()
          '              .Items.Insert(0, "")
           '         End With
	   
	   
	     If lblCIFPAN.Text <> "" Then
                    ddlCIFPAN.setCustName = lblCIFPAN.Text
                    ddlCIFPAN.HiddenCustomerId = tempDt.Rows(rowDataBoundIndex).Item("Cust_ID").ToString
                    ddlCIFPAN.HiddenDocId = tempDt.Rows(rowDataBoundIndex).Item("DocId").ToString
                End If
                '  End If
       '         If Not ddlCIFPAN Is Nothing Then
       '             ddlCIFPAN.FindItemByText(lblCIFPAN.Text).Selected = True
       '         End If
            End If
        Catch ex As Exception
            ''lblError.Text = "Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                          sSelfPath, "OnRowDataBound", ErrorLevel.High)
        End Try
    End Sub

    Protected Sub ddl_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim I As Integer
            Dim dtCIFPANTemp As DataTable
            I = DirectCast(CType(CType(CType(sender, RadDropDownList).Parent, DataControlFieldCell).Parent, GridViewRow), System.Web.UI.WebControls.GridViewRow).DataItemIndex

            Dim tempDt As DataTable
            tempDt = CType(Session("dtELNPreTradeAllocation"), DataTable)
            dtCIFPANTemp = New DataTable("dtCIFPANTemp")
            If CType(sender, RadDropDownList).ID = "ddlRMName" Then
                tempDt.Rows(I).Item("RM_Name") = CType(sender, RadDropDownList).SelectedItem.Text
                grdRMData.Rows(I).Cells(grdRMDataEnum.RM_Name).Controls.OfType(Of Label)().FirstOrDefault().Text = CType(sender, RadDropDownList).SelectedItem.Text
                Dim FindCustomer As FinIQ_Fast_Find_Customer
                FindCustomer = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer)

                'Mohit Lalwani on 04-Nov-2016
                FindCustomer.setCustName = ""
                FindCustomer.HiddenCustomerName = ""
                FindCustomer.HiddenCustomerId = ""
                FindCustomer.HiddenDocId = ""
                '/Mohit Lalwani on 04-Nov-2016
            End If
            Session.Add("dtELNPreTradeAllocation", tempDt)
            Session.Add("dtCustomerCodes", dtCIFPANTemp)  '<RiddhiS. on 03-Oct-2016: To get Customer Segment>
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
              sSelfPath, "ddl_OnSelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Protected Sub ddlCIFPAN_onTextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim I, cntCodes As Integer
        Dim tempDt As DataTable
        Dim dtCustomerCodes As DataTable
        tempDt = CType(Session("dtELNPreTradeAllocation"), DataTable)
        dtCustomerCodes = CType(Session("dtCustomerCodes"), DataTable)
        lblerrorPopUp.Text = ""
        Try
            ''<Start | Nikhil M. on 23-Sep-2016:Changed >
            If Not TypeOf sender Is TextBox Then
                I = DirectCast(CType(CType(CType(sender, RadDropDownList).Parent, DataControlFieldCell).Parent, GridViewRow), System.Web.UI.WebControls.GridViewRow).DataItemIndex

                If CType(sender, RadDropDownList).ID = "ddlCIFPAN" Then
                    If CType(sender, RadDropDownList).SelectedText.Trim = "" Then
                        lblerrorPopUp.Text = "Please Select code."
                        Exit Sub
                    End If
                    tempDt.Rows(I).Item("Account_Number") = CType(sender, RadDropDownList).SelectedItem.Text
                    grdRMData.Rows(I).Cells(grdRMDataEnum.Account_Number).Controls.OfType(Of Label)().FirstOrDefault().Text = CType(sender, RadDropDownList).SelectedItem.Text


                    ''Commented by PriyaB: 05Nov2016
                    ''After discussion with Milind K, Sanchita: Booking center dropdown is made editable and based on the selection customer's are filtered.
                    ' ''<RiddhiS. on 03-Oct-2016: To change Booking Branch based on CIF of first row in Allocation grid>
                    '' ''Check first record and if it is a Retail customer then set Retail as booking center
                    ''If I = 0 Then               ''To Change only according to first row
                    ''    Dim drCustSegment As DataRow = dtCustomerCodes.Select("CIF_PANId = '" + CType(sender, RadDropDownList).SelectedItem.Text + "'")(0)
                    ''    Dim sFirstCustSegment As String = drCustSegment.Item("Segment").ToString

                    ''    If sFirstCustSegment.ToUpper = "RETAIL" Then
                    ''        Dim sAvailableBKC As String()
                    ''        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
                    ''            If ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
                    ''                ddlBookingBranchPopUpValue.SelectedIndex = iBKC
                    ''                Exit For
                    ''            End If
                    ''        Next
                    ''    ElseIf sFirstCustSegment.ToUpper = "PVB" Then
                    ''        Dim sAvailableBKC As String()
                    ''        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
                    ''            If Not ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
                    ''                ddlBookingBranchPopUpValue.SelectedIndex = iBKC
                    ''                Exit For
                    ''            End If
                    ''        Next
                    ''    End If


                    ''    'For cntCodes = 0 To dtCustomerCodes.Rows.Count - 1
                    ''    '    If dtCustomerCodes.Rows(cntCodes).Item("CIF_PANId").ToString = CType(sender, RadDropDownList).SelectedItem.Text Then
                    ''    '        If dtCustomerCodes.Rows(cntCodes).Item("Segment").ToString.ToUpper = "RETAIL" Then
                    ''    '            ddlBookingBranchPopUpValue.SelectedValue = "Retail"
                    ''    '            ddlBookingBranchPopUpValue.SelectedText = "Singapore Retail"
                    ''    '            Exit For
                    ''    '        Else
                    ''    '            ddlBookingBranchPopUpValue.SelectedValue = "SG"
                    ''    '            ddlBookingBranchPopUpValue.SelectedText = "Singapore"
                    ''    '        End If
                    ''    '    End If
                    ''    'Next
                    ''End If
                    ' ''</RiddhiS. on 03-Oct-2016>

                End If
                ''<End | Nikhil M. on 23-Sep-2016:Changed >
            ElseIf CType(sender, TextBox).ID = "txtAlloNotional" Then
                I = DirectCast(CType(CType(CType(sender, TextBox).Parent, DataControlFieldCell).Parent, GridViewRow), System.Web.UI.WebControls.GridViewRow).DataItemIndex ''<Nikhil M. on 23-Sep-2016: added>

                If Qty_Validate(CType(sender, TextBox).Text) = False Then
                    Exit Sub
                End If

                CType(sender, TextBox).Text = SetNumberFormat(FinIQApp_Number.ConvertFormattedAmountToNumber(CType(sender, TextBox).Text).ToString, 0)
                
                tempDt.Rows(I).Item(2) = CType(sender, TextBox).Text
                grdRMData.Rows(I).Cells(grdRMDataEnum.Notional).Controls.OfType(Of Label)().FirstOrDefault().Text = CType(sender, TextBox).Text
            End If
            Session.Add("dtELNPreTradeAllocation", tempDt)
           
            GetTotalRemainAlloLables()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
              sSelfPath, "ddlCIFPAN_onTextChanged", ErrorLevel.High)
        End Try
    End Sub
    ''<ashwiniP on 21Sept16>
    Private Sub GetTotalRemainAlloLables()
        Try
            Dim TotalSum As Double
            TotalSum = 0
            For Each row As GridViewRow In grdRMData.Rows
                If row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim <> "" _
                And row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked Then
                    TotalSum = TotalSum + CDbl(row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                End If
            Next
            lblTotalAmt.Visible = True
            lblTotalAmtVal.Visible = True
            lblTotalAmtVal.Text = lblNotionalPopUpValue.Text
            lblAlloAmt.Visible = True
            lblAlloAmtVal.Visible = True
            lblAlloAmtVal.Text = SetNumberFormat(FinIQApp_Number.ConvertFormattedAmountToNumber(CStr(TotalSum)).ToString, 0)
            lblRemainNotional.Visible = True
            lblRemainNotionalVal.Visible = True
            lblRemainNotionalVal.Text = SetNumberFormat(FinIQApp_Number.ConvertFormattedAmountToNumber(CStr(CDbl(lblTotalAmtVal.Text) - CDbl(lblAlloAmtVal.Text))).ToString, 0)
            lblRemainNotional.ForeColor = Color.Green
            lblRemainNotionalVal.ForeColor = Color.Green
            If CDbl(lblAlloAmtVal.Text) > CDbl(lblTotalAmtVal.Text) Then
                lblAlloAmt.ForeColor = Color.Red
                lblAlloAmtVal.ForeColor = Color.Red
            Else
                lblAlloAmt.ForeColor = Color.Black
                lblAlloAmtVal.ForeColor = Color.Black
            End If
            If ((CDbl(lblRemainNotionalVal.Text) > CDbl(lblTotalAmtVal.Text)) Or CDbl(lblRemainNotionalVal.Text) < 0) Then
                lblRemainNotional.ForeColor = Color.Red
                lblRemainNotionalVal.ForeColor = Color.Red
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
             sSelfPath, "GetTotalRemainAlloLables", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnAddAllocation_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAllocation.ServerClick
        Try
            grdRMData.Visible = True
            funAdd()

        Catch ex As Exception
            lblerror.Text = "Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                          sSelfPath, "btnAddAllocation_ServerClick", ErrorLevel.High)
        End Try
    End Sub

    Public Function savePretradeAllocation(ByVal sRFQ As String) As StringBuilder
        Try
            Dim Xml As New StringBuilder

            If sRFQ = "" Or sRFQ = "&nbsp;" Or Val(sRFQ) < 0 Then
                lblerror.Text = "RFQ not valid."
                Return Xml.Append("")
            End If


            'Dim Xml_Update As New StringBuilder
            Xml.Append("<AllocationDetails>")
            'Xml_Update.Append("<AllocationDetails>")

            For Each row As GridViewRow In grdRMData.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim isChecked As Boolean = row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked
                    If isChecked And row.Cells(3).Controls.OfType(Of Label)().FirstOrDefault().Text.Trim <> "" Then
                        Xml.Append("<OrderDetails>")
                        ''Xml.Append("<EPOF_OrderPoolId>" & sRFQ + "1" & "</EPOF_OrderPoolId>")
                        Xml.Append("<EPOF_OrderPoolId>orderID</EPOF_OrderPoolId>")
                        Xml.Append("<EPOF_Created_By>" & LoginInfoGV.Login_Info.LoginId & "</EPOF_Created_By>")
                        Xml.Append("<EPOF_CashOrderQuantity>" & (row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Replace(",", "")) & "</EPOF_CashOrderQuantity>")
                        ' Xml.Append("<EPOF_OrderInfo>" & CType((row.Cells(2).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault()), FinIQ_Fast_Find_Customer).HiddenCIFNo & "</EPOF_OrderInfo>") ''<Nikhil M. on 24-Sep-2016: Changed>
                        Xml.Append("<EPOF_OrderInfo>" & CType((row.Cells(2).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault()), FinIQ_Fast_Find_Customer).HiddenCustomerId & "</EPOF_OrderInfo>") ''<Nikhil M. on 24-Sep-2016: Changed>
                        Xml.Append("<EPOF_Allocation_RM>" & (row.Cells(1).Controls.OfType(Of RadDropDownList)().FirstOrDefault().SelectedValue) & "</EPOF_Allocation_RM>") ''<Nikhil M. on 26-Sep-2016: Changed>
                        Xml.Append("</OrderDetails>")
                    End If
                End If
            Next

            Xml.Append("</AllocationDetails>")
            'Xml_Update.Append("</AllocationDetails>")

            '' ''<Dilkhush 28Sep2016:- Commnted and return String from function to insert action after order placed i.e. in order placed sp>
            ''Select Case objELNRFQ.Web_Insert_PostOrderAllocation(Xml.ToString, Xml_Update.ToString, "A")
            ''    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            ''        Return True
            ''    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            ''        Return False
            ''End Select
            '' ''<Dilkhush 28Sep2016:- Commnted and return String from function to insert action after order placed i.e. in order placed sp>
            Return Xml
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                          sSelfPath, "savePretradeAllocation", ErrorLevel.High)
            Throw ex
        End Try

    End Function

    Function chkDuplicateRecords() As Boolean ''Add this validation to end of function only
        Try
            Dim tempDt As DataTable
            tempDt = New DataTable("tempDt")
            Dim sDuplicateRec As String = ""
            Dim sDuplicateRec1 As String = ""
            Dim j As Integer = 0
            Dim x As Integer = 0
            tempDt = CType(Session("dtELNPreTradeAllocation"), DataTable)
            If Not tempDt Is Nothing Then
                If tempDt.Rows.Count = 0 Then
                    lblerrorPopUp.Text = "No records to insert."
                    Return False
                End If
            End If
            Dim duplicatesNo As IEnumerable(Of String)
            Dim duplicatesName As IEnumerable(Of String)

            '' chkUpfrontOverride.Visible = False
                For Each row As GridViewRow In grdRMData.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                    If row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked And tempDt.Rows(j)("EPOF_OrderId").ToString().Trim().ToUpper() = "CHECK" Then

                        Dim RM_Name As String = ""
                        Dim RM_AccountNumber As String = ""

                        Dim cnt As Integer = 0

                        RM_Name = tempDt.Rows(j)("RM_Name").ToString().ToUpper()
                        RM_AccountNumber = tempDt.Rows(j)("Account_Number").ToString().ToUpper()
                        For i As Integer = 0 To tempDt.Rows.Count - 1
                            If RM_Name = tempDt.Rows(i)("RM_Name").ToString().ToUpper() And _
                                RM_AccountNumber = tempDt.Rows(i)("Account_Number").ToString().ToUpper() And _
                                tempDt.Rows(i)("EPOF_OrderId").ToString().ToUpper() = "CHECK" Then
                                cnt += 1
                    End If

                            '' ''If cnt > 1 Then
                            '' ''    tempDt.Rows(i)("EPOF_OrderId") = "REMOVED"
                            '' ''End If

                        Next

                        If cnt > 1 Then
                            '' Duplicate Data
                            chkUpfrontOverride.Visible = False
                            chkUpfrontOverride.Checked = False
                            lblerrorPopUp.Text = "Above records contain duplicate entries."
                            Return False

                        End If


                        '' ''    tempDt.Rows(j)(3) = "Removed"
                        ' ''Else
                        ' ''    tempDt.Rows(j)(3) = ""
                    End If


                    '' '' ''If Not row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked And tempDt.Rows(j)(3).ToString().Trim().ToUpper() = "" Then
                    '' '' ''    tempDt.Rows(j)(3) = "Removed"
                    '' '' ''Else

                    '' '' ''    tempDt.Rows(j)(3) = ""
                    '' '' ''End If
                End If
                j += 1
                x += 1
            Next

            Return True





            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAllocationAccountNumber", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
            '    Case "Y", "YES"
            '        duplicatesNo = tempDt.AsEnumerable().GroupBy(Function(i) i.Field(Of String)("Account_Number")).Where(Function(g) g.Count() > 1).Select(Function(g) g.Key)
            'End Select

            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAllocationRMName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
            '    Case "Y", "YES"
            '        duplicatesName = tempDt.AsEnumerable().GroupBy(Function(i) i.Field(Of String)("RM_Name")).Where(Function(g) g.Count() > 1).Select(Function(g) g.Key)
            'End Select


            'For Each dup In duplicatesNo
            '    sDuplicateRec1 = dup + "," + sDuplicateRec
            'Next
            'For Each dup In duplicatesName
            '    sDuplicateRec = dup + "," + sDuplicateRec
            'Next
            'If sDuplicateRec.Replace(",", "") <> "" And sDuplicateRec1.Replace(",", "") <> "" Then
            '    sDuplicateRec = sDuplicateRec.Substring(0, sDuplicateRec.Length - 1)
            '    chkUpfrontOverride.Visible = False
            '    chkUpfrontOverride.Checked = False
            '    lblerrorPopUp.Text = "Above records contain duplicate entries."
            '    Return False
            'Else
            '    Return True
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '<Rushikesh End>
    
    		    ''<Nikhil M. on 20-Sep-2016: Added to reset all cheakbox>
    Public Function ResetAllChkBox() As Boolean
        Try
            chkCITI.Checked = False
            chkJPM.Checked = False
            chkHSBC.Checked = False
            chkBNPP.Checked = False
            chkBAML.Checked = False
            chkUBS.Checked = False
            chkCS.Checked = False
            chkOCBC.Checked = False
	    chkDBIB.Checked = False
	    chkLEONTEQ.Checked = False
	    chkCOMMERZ.Checked = False
        Catch ex As Exception
				Throw ex
        End Try

    End Function

    Private Sub chkBAML_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBAML.CheckedChanged
        Try
            hdnBestIssuer.Value = lblBAMLPrice.Text
            hdnBestProvider.Value = "BAML"
            hdnstrBestClientYeild.Value = lblBAMLClientYield.Text
            CheckUncheck(chkBAML)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkOCBC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOCBC.CheckedChanged
        Try
            hdnBestIssuer.Value = lblOCBCPrice.Text
            hdnBestProvider.Value = "OCBC"
            hdnstrBestClientYeild.Value = lblOCBCClientYield.Text
            CheckUncheck(chkOCBC)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkLEONTEQ_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLEONTEQ.CheckedChanged
        Try
            hdnBestIssuer.Value = lblLEONTEQPrice.Text
            hdnBestProvider.Value = "LEONTEQ"
            hdnstrBestClientYeild.Value = lblLEONTEQClientYield.Text
            CheckUncheck(chkLEONTEQ)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkCOMMERZ_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCOMMERZ.CheckedChanged
        Try
            hdnBestIssuer.Value = lblCOMMERZPrice.Text
            hdnBestProvider.Value = "COMMERZ"
            hdnstrBestClientYeild.Value = lblCOMMERZClientYield.Text
            CheckUncheck(chkCOMMERZ)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''Dilkhush 23sep2016 Added for filter best price RFQ else load all RFQ
    Private Sub chkExpandAllRFQ_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkExpandAllRFQ.CheckedChanged
        Try
            fill_grid()
            RestoreSolveAll()
            RestoreAll()
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chkExpandAllRFQ_CheckedChanged", ErrorLevel.High)
        End Try
    End Sub
Private Sub KYIR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles KYIR.Click
        Dim filePath As String
        Dim fileName As String
        Try
            filePath = objReadConfig.ReadConfig(dsConfig, "EQC_ProductKYIRPath", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "C:\FinIQ\Product_Info\")
            If chkELNType.Checked Then
                fileName = "KYIR - KOELN.pdf"
            Else
                fileName = "KYIR - ELN.pdf"
            End If
            filePath = filePath + fileName
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + fileName))
            Response.TransmitFile(filePath)
            Response.End()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "KYIR_Click", ErrorLevel.High)
        End Try
    End Sub
    ''End | Chitralekha M on 26-Sep-16>

    
      ''<Nikhil M. on 26-Sep-2016: Added>
    Public Function SetDrpCIFPAN(ByVal UserEntityId As String, ByVal RMName As String, ByRef dtCIFPANTemp As DataTable) As Boolean
        Dim dtPANCIF As DataTable
        Try
            dtPANCIF = New DataTable("Dummy")
            Select Case objELNRFQ.DB_Get_CIFPAN(UserEntityId, "", RMName, dtPANCIF)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    dtCIFPANTemp = dtPANCIF
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    dtCIFPANTemp = dtPANCIF
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    dtCIFPANTemp = dtPANCIF
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Function
 '<RiddhiS. on 17-Sep-2016: For Document Generation>
    
    Private Sub btnBAMLDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBAMLDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strBAMLID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strBAMLID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strBAMLID = CStr(hashRFQID(hashPPId("BAML")))
            End If

            'Generate(strBAMLID)     '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnBAMLDoc_Click", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub btnBNPPDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBNPPDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strBNPPID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strBNPPID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strBNPPID = CStr(hashRFQID(hashPPId("BNPP")))
            End If

            'Generate(strBNPPID)  '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnBNPPDoc_Click", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnCITIDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCITIDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strCITIID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strCITIID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strCITIID = CStr(hashRFQID(hashPPId("CITI")))
            End If

            'Generate(strCITIID)  '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCITIDoc_Click", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnCOMMERZDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCOMMERZDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strCOMMERZID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strCOMMERZID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strCOMMERZID = CStr(hashRFQID(hashPPId("COMMERZ")))
            End If

            'Generate(strCOMMERZID)  '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCOMMERZDoc_Click", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnCSDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCSDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strCSID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strCSID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strCSID = CStr(hashRFQID(hashPPId("CS")))
            End If

            'Generate(strCSID)   '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCSDoc_Click", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnDBIBDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDBIBDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strDBIBID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strDBIBID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strDBIBID = CStr(hashRFQID(hashPPId("DBIB")))
            End If

            'Generate(strDBIBID)     '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnDBIBDoc_Click", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnHSBCDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHSBCDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strHSBCID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strHSBCID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strHSBCID = CStr(hashRFQID(hashPPId("HSBC")))
            End If

            'Generate(strHSBCID)     '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnHSBCDoc_Click", ErrorLevel.High)
        End Try
    End Sub
    
    Private Sub btnJPMDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJPMDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strJPMID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strJPMID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strJPMID = CStr(hashRFQID(hashPPId("JPM")))
            End If

            'Generate(strJPMID)  '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnJPMDoc_Click", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnLEONTEQDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLEONTEQDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strLEONTEQID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strLEONTEQID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strLEONTEQID = CStr(hashRFQID(hashPPId("LEONTEQ")))
            End If

            'Generate(strLEONTEQID)  '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnLEONTEQDoc_Click", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnOCBCDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOCBCDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strOCBCID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strOCBCID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strOCBCID = CStr(hashRFQID(hashPPId("OCBC")))
            End If

            'Generate(strOCBCID)     '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnOCBCDoc_Click", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnUBSDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUBSDoc.Click
        Dim hashRFQID As Hashtable
        Dim hashPPId As Hashtable
        Dim strUBSID As String
        Try
            hashPPId = New Hashtable
            hashRFQID = New Hashtable
            hashRFQID = CType(Session("Hash_Values"), Hashtable)
            hashPPId = CType(Session("PP_IdTable"), Hashtable)

            If hashRFQID Is Nothing Then                                 ''SinglePricing
                strUBSID = Convert.ToString(Session("Quote_ID"))
            Else                                                         ''PriceAll
                strUBSID = CStr(hashRFQID(hashPPId("UBS")))
            End If

            'Generate(strUBSID)  '<RiddhiS. on 09-Oct-2016: To open KYIR on button click>
            KYIR_Click(sender, e)

        Catch ex As Exception
            lblerror.Text = "Error occurred on Document load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnUBSDoc_Click", ErrorLevel.High)
        End Try
    End Sub
    
    ''<Chitralekha M on 28-Sept-2016>
    Public Sub getFlag(ByVal Share As String)

        Try
            Dim dt As DataTable

            If (Not IsNothing(CType(Session("Share_Value"), DataTable)) AndAlso CType(Session("Share_Value"), DataTable).Rows.Count > 0) Then
                lblAdvisoryFlagVal.Text = ""
            Else
                dt = New DataTable("Dummy")
                Select Case objELNRFQ.DB_UnderlyingRiskRatingShare(Share, dt)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful

                        sFlag = dt.Rows(0)(1).ToString
                        If dt.Rows(0)(1).ToString = "Y" Then
                            lblAdvisoryFlagVal.ForeColor = Color.Green
                            lblAdvisoryFlagVal.Text = "Yes"

                        Else
                            lblAdvisoryFlagVal.ForeColor = Color.Red
                            lblAdvisoryFlagVal.Text = "No"
                        End If
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                End Select

            End If

        Catch ex As Exception
            lblerror.Text = "getFlag:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "getFlag", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    ''</Chitralekha M on 28-Sept-2016>



'Added by Mohit Lalwani on 30-sept-2016 for customer control

    Public Sub CustomerSelected(ByVal Customer_Info As Fast_Customer_Info)
        Dim I As Integer
        Dim tempDt As DataTable
        tempDt = CType(Session("dtELNPreTradeAllocation"), DataTable)
        lblerrorPopUp.Text = ""
        Try
            I = CInt(Customer_Info.ItemIndex)
            tempDt.Rows(I).Item("Account_Number") = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).GetCustName
            tempDt.Rows(I).Item("Cust_ID") = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCustomerId  'Customer_Info.CustomerCIFNo
            tempDt.Rows(I).Item("DocId") = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenDocId
            grdRMData.Rows(I).Cells(grdRMDataEnum.Account_Number).Controls.OfType(Of Label)().FirstOrDefault().Text = Customer_Info.CustomerName      'Customer_Info.CustomerName

            Session.Add("dtELNPreTradeAllocation", tempDt)

            ''Commented by PriyaB: 05Nov2016
            ''After discussion with Milind K, Sanchita: Booking center dropdown is made editable and based on the selection customer's are filtered.
            '<AvinashG on 09-Oct-2016: Removing temporary drop down and using customer control itself>
            ''If I = 0 Then               ''To Change only according to first row
            ''    Dim sFirstCustName As String

            ''    sFirstCustName = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).GetCustName

            ''    If sFirstCustName.ToUpper.Contains("TSO") Then
            ''        Dim sAvailableBKC As String()
            ''        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
            ''            If ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
            ''                ddlBookingBranchPopUpValue.SelectedValue = ddlBookingBranchPopUpValue.Items.Item(iBKC).Value
            ''                Exit For
            ''            End If
            ''        Next
            ''    Else
            ''        Dim sAvailableBKC As String()
            ''        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
            ''            If Not ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
            ''                ddlBookingBranchPopUpValue.SelectedValue = ddlBookingBranchPopUpValue.Items.Item(iBKC).Value
            ''                Exit For
            ''            End If
            ''        Next
            ''    End If
            ''End If

            '' '' '' ''<RiddhiS. on 03-Oct-2016: To change Booking Branch based on CIF of first row in Allocation grid>
            ' '' '' '' ''Check first record and if it is a Retail customer then set Retail as booking center
            ' '' '' ''If I = 0 Then               ''To Change only according to first row
            ' '' '' ''    ' Dim drCustSegment As DataRow = dtCustomerCodes.Select("CIF_PANId = '" + CType(sender, RadDropDownList).SelectedItem.Text + "'")(0)
            ' '' '' ''    Dim drCustSegment As DataRow = dtCustomerCodes.Select("CIF_PANId = '" + CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCIFNo + "'")(0)

            ' '' '' ''    Dim sFirstCustSegment As String = drCustSegment.Item("Segment").ToString

            ' '' '' ''    If sFirstCustSegment.ToUpper = "RETAIL" Then
            ' '' '' ''        Dim sAvailableBKC As String()
            ' '' '' ''        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
            ' '' '' ''            If ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
            ' '' '' ''                ddlBookingBranchPopUpValue.SelectedIndex = iBKC
            ' '' '' ''                Exit For
            ' '' '' ''            End If
            ' '' '' ''        Next
            ' '' '' ''    ElseIf sFirstCustSegment.ToUpper = "PVB" Then
            ' '' '' ''        Dim sAvailableBKC As String()
            ' '' '' ''        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
            ' '' '' ''            If Not ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
            ' '' '' ''                ddlBookingBranchPopUpValue.SelectedIndex = iBKC
            ' '' '' ''                Exit For
            ' '' '' ''            End If
            ' '' '' ''        Next
            ' '' '' ''    End If


            ' '' '' ''    'For cntCodes = 0 To dtCustomerCodes.Rows.Count - 1
            ' '' '' ''    '    If dtCustomerCodes.Rows(cntCodes).Item("CIF_PANId").ToString = CType(sender, RadDropDownList).SelectedItem.Text Then
            ' '' '' ''    '        If dtCustomerCodes.Rows(cntCodes).Item("Segment").ToString.ToUpper = "RETAIL" Then
            ' '' '' ''    '            ddlBookingBranchPopUpValue.SelectedValue = "Retail"
            ' '' '' ''    '            ddlBookingBranchPopUpValue.SelectedText = "Singapore Retail"
            ' '' '' ''    '            Exit For
            ' '' '' ''    '        Else
            ' '' '' ''    '            ddlBookingBranchPopUpValue.SelectedValue = "SG"
            ' '' '' ''    '            ddlBookingBranchPopUpValue.SelectedText = "Singapore"
            ' '' '' ''    '        End If
            ' '' '' ''    '    End If
            ' '' '' ''    'Next
            ' '' '' ''End If
            '' '' '' ''</RiddhiS. on 03-Oct-2016>


            GetTotalRemainAlloLables()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
              sSelfPath, "CustomerSelected", ErrorLevel.High)
        End Try

    End Sub

    '/Added by Mohit Lalwani on 30-sept-2016 for customer control


    Public Function setCustName(ByVal custId As String) As String
        Dim sFirstCustName As String
        Try
            Dim I As Integer
            sFirstCustName = ""
            I = 0

            If custId <> "" And IsNumeric(custId) Then
                Select Case objELNRFQ.Web_getCustomerNameFromID(custId, sFirstCustName)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful

                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                End Select
            End If

            Return sFirstCustName
        Catch ex As Exception
            'LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
            '           sSelfPath, "setCustName", ErrorLevel.High)
            Return sFirstCustName
        End Try
    End Function

    Private Sub btnSaveSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveSettings.Click
        Dim o_strXMLNote_DefaultValues As String
        Dim DefaultSetting_Level As String

        Try
            If Write_PersonalSettings_TOXML(o_strXMLNote_DefaultValues) = True Then
                Select Case objELNRFQ.Web_Insert_ELN_DefaultSettings(CStr(o_strXMLNote_DefaultValues), CStr(LoginInfoGV.Login_Info.EntityID), LoginInfoGV.Login_Info.LoginId, "ELN_DealEntry", objReadConfig.ReadConfig(dsConfig, "EQC_DefaultSetting_Level", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "ENTITY").Trim.ToUpper, LoginInfoGV.Login_Info.LoginId)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        lblError_DefaultSettings.Text = "Personal settings saved successfully!"
                        lblError_DefaultSettings.ForeColor = Color.Blue
                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                        lblError_DefaultSettings.Text = "Error occured in saving personal settings."
                        lblError_DefaultSettings.ForeColor = Color.Red
                End Select
            End If
        Catch ex As Exception
            lblError_DefaultSettings.Text = "Error occured in saving personal settings."
            lblError_DefaultSettings.ForeColor = Color.Red
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnSaveSettings_Click", ErrorLevel.High)
        End Try

    End Sub
End Class
