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


Partial Public Class AccDecRFQ1
    Inherits FinIQAppMain
    Private Const sSelfPath As String = "FinIQWebApp/ELN_DealEntry1/AccDecRFQ1.aspx.vb"
    Dim oWEBADMIN As WEB_ADMINISTRATOR.LSSAdministrator
    Dim oWEBMarketData As WEB_FINIQ_MarketData.FINIQ_WEBSRV_MarketData
    Dim ObjCommanData As Web_CommonFunction.CommonFunction ''Added By Nikhil M On 08Aug2016 EQSCB-16
    ' Private Const sPoolRedirectionPath As String = "../ELN_OrderPool/ELN_OrderPool.aspx?menustr=EQ%20Sales%20-%20ELN%20Order%20Pool&Mode=&token="
    Private Const sPoolRedirectionPath As String = "../ELN_OrderPool/AccuDecu_OrderPool.aspx?menustr=EQ%20Sales%20-%20Accu/Decu%20Order%20Pool&Mode=&token="
    Private oWebCustomerProfile As Web_CustomerProfile.CustomerProfile
    Dim objELNRFQ As Web_ELNRFQ.ELN_RFQ
    Dim generateDocument As Web_DocumentGeneration.GenerateDocumentsForWeb
    Dim objDocGen As Web_DocGenOpenXML.DocumentGenerationOpenXml
                    
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
    '<AvinashG. on 09-Aug-2016: Unused variables>
    'Dim strBaseCcy As String
    'Dim strShare As String
    'Dim strBarrier As String
    'Dim strStrike As String
    'Dim strGetDate As String
    'Dim strTenor As String
    'Dim strJPMId As String
    'Dim strQuantoCcy As String
    'Dim strHSBCPrice As String
    ''Mohit Lalwani on 29-Jan-2016
    'Dim strOCBCPrice As String
    'Dim strCITIPrice As String
    'Dim strLEONTEQPrice As String
    'Dim strDBIBPrice As String
    ''/Mohit Lalwani on 29-Jan-2016
    'Dim strCSPrice As String
    'Dim strAmount As String
    'Dim strXMLNote_RFQ As String
    'Dim strEntityId As String
    'Public flag1 As Boolean = False
    'Dim strcount As String
    'Dim strJPMPrice As String
    'Dim StrnoteRFQXML As String
    'Dim templateCode As String
    'Dim strHardTenor As String
    'Dim strPPID As String
    'Dim EP_ID As String
    'Dim ER_ID As String
    'Dim EP_ER_QuoteRequestId As String
    'Dim ER_QuoteRequestId As String
    'Dim Tenor As Integer = 0
    'Public arr(3) As String
    'Dim strUPDOWN As String = ""
    '</AvinashG. on 09-Aug-2016: Unused variables>

    Dim interval As String '  Added by Chitralekha 12-Sept-16

    Dim flag As String

    Dim JPM_ID As String
    Dim UBS_ID As String
    Dim HSBC_ID As String
    'Mohit Lalwani on 29-Jan-2016
    Dim OCBC_ID As String
    Dim CITI_ID As String
    Dim LEONTEQ_ID As String
    Dim COMMERZ_ID As String
    '/Mohit Lalwani on 29-Jan-2016
    Dim BNPP_ID As String
    Dim BAML_ID As String
    Dim DBIB_ID As String
    Dim CS_ID As String

    Dim SchemeName As String
   
    Dim Quote_ID As String
    Dim PP_CODE As String
    Public Const ChartFont As String = "Arial"
    Public Const ChartTitleSize As Double = 7.0F
    Dim numberdiv As Double = 0


    Dim tabIndex As Integer
    Dim getAllId As Hashtable
    Dim getPPId As Hashtable
    Dim WebServicePath As String
    Dim sEQC_DealerLoginGroups As String
    ''--
    Public Structure ChartColors
        Dim JPM As Color
        Dim CS As Color
        Dim UBS As Color
        Dim HSBC As Color
        Dim FINIQ As Color
        Dim BAML As Color
        Dim BNPP As Color
        Dim DBIB As Color
        Dim OCBC As Color    'Mohit Lalwani on 29-Jan-2016
        Dim CITI As Color    'Mohit Lalwani on 29-Jan-2016
        Dim LEONTEQ As Color    
        Dim COMMERZ As Color
    End Structure
    Public Shared structChartColors As ChartColors

    'Public Structure Structured_Product_Tranche_ELN
    '    Public strExchange As String
    '    Public strEntityName As String
    '    Public strTemplateName As String
    '    Public lngTemplateId As String
    '    Public strTradeDate As String
    '    Public strValueDate As String
    '    Public strFxingdate As String
    '    Public strMaturityDate As String
    '    Public strCurrency As String
    '    Public strAsset As String
    '    Public strELNType As String
    '    Public strTenorType As String
    '    Public dblStrike1 As Double
    '    Public dblStrike2 As Double
    '    Public strPrice As Double
    '    Public strPrice1 As Double
    '    Public dblBarrier As String
    '    Public strRemark As String
    '    Public dblNominalAmount As Double
    '    Public inttenor As Integer
    '    Public strIssuer_Date_Offset As String
    '    Public strBranchName As String
    '    Public strSecurityDesc As String
    '    Public strOrderQty As String
    '    Public strSolveFor As String
    '    Public strAssetclass As String
    '    Public strRFQRMName As String
    '    Public strEmailId As String
    '    Public strBranch As String
    '    Public strDRAFCNTenorType As String
    '    Public strUnderlyingCode As String
    '    Public strExchangeDRA As String
    '    Public strCurrencyDRA As String
    '    Public inttenorDRA As Integer
    '    Public strGuaranteedDuration As String
    '    Public strGuaranteedDurationType As String
    '    Public strKIType As String
    '    Public strKILevel As Double
    '    Public strKIObsFreq As String
    '    Public strCoupon As Double
    '    Public strCoupon1 As Double
    '    Public strKOType As String
    '    Public strKOLevel As Double
    '    Public strOTCYN As String
    '    Public dblStrikePrice As Double
    '    Public strAccumTenorType As String
    '    Public strExchangeAccum As String
    '    Public strKoPerc As String
    '    Public strUpfront As Double
    '    Public strUpfront1 As Double
    '    Public inttenorAccum As Integer
    '    Public strLeverageratio As String
    '    Public strFrequency As String
    '    Public strAQDQCcy As String
    '    Public dblRMMargin As Double
    'End Structure

    Dim hitCount As Integer = 0
    Dim orderValidityTimer As Integer = 0
#End Region

#Region "Enum"

    Enum grdELNRFQEnum
        RFQ_ID
        CreatePool
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
        BestPrice_YN
    End Enum
    Enum grdDRAFCNEnum
        RFQ_ID
        CreatePool '<MohitL. on 06-Oct-2015: Added for BoS>
        Solve_For
        Provider_Name
        Price_Per
        Type
        Share
        Strike_Per
        Coupon_Per
        Tenor
        RFQTenor
        Currency
        Order_Quantity
        KI_Type
        KI_Level
        GU_Period
        KI_Frequency
        KO_Type
        KO_Level
        Exchange
        OTC
        Remark
        External_RFQ_ID
        Quoted_At
        ClubbingRFQId
    End Enum
    Enum grdAccumDeccumEnum
        RFQ_ID
        CreatePool
        RFQ_Details '<Mohit Lalwani on 20-Jan-2015: Added for BoS>
        GenerateDoc '<Mohit Lalwani>
        Solve_For
        Provider_Name
        Upfront
        Type
        Share
        Strike_Per
        Tenor
        RFQTenor
        Order_Quantity
        EP_NumberOfDaysAccrual
        TotalShares
        GU_Duration
        KO_Per
        KO_Settlement
        Frequency
        Exchange
        Leverage_Ratio
        Remark
        External_RFQ_ID
        Quoted_At
        ClubbingRFQId
        Quote_Status
        CashCurrency
        created_by
        BestPrice_YN
    End Enum
    '<AvinashG. on 27-Nov-2015, FA-1190>
    Enum grdOrderEnum
        ER_QuoteRequestId
        Order_ID
        EP_InternalOrderID
        Order_Details   'Mohit
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
        Type
        ER_KOPercentage
        Frequency
        ER_CashOrderQuantity
        EP_NumberOfDaysAccrual
        ER_Created_By
        EP_OrderComment
    End Enum

    Enum grdRMDataEnum
        chkAll
        RM_Name
        Account_Number
        Notional
        OrderId
    End Enum
    'Enum grdOrderEnum
    '    ER_QuoteRequestId
    '    EP_ExternalQuoteId
    '    EP_InternalOrderID
    '    Order_ID
    '    PP_CODE
    '    Ordered_Qty
    '    Filled_Qty
    '    ER_GuaranteedDuration
    '    ER_UnderlyingCode
    '    ER_CashCurrency
    '    Order_Status
    '    EP_OfferPrice
    '    EP_StrikePercentage
    '    EP_Upfront
    '    EP_CouponPercentage
    '    EP_Client_Price
    '    EP_Client_Yield
    '    EP_RM_Margin
    '    EP_Notional_Amount1
    '    ELN_Order_Type
    '    LimitPrice1
    '    LimitPrice2
    '    LimitPrice3
    '    EP_Execution_Price1
    '    EP_Execution_Price2
    '    EP_Execution_Price3
    '    ER_LeverageRatio
    '    ''Rutuja 15April:Added new column
    '    EP_AveragePrice
    '    ''Rutuja 15April:Added new column
    '    ER_TransactionTime
    '    EP_Deal_Booking_Branch
    '    EP_HedgedFor
    '    EP_HedgingOrderId
    '    EP_Order_Remark1
    'End Enum
    '</AvinashG. on 27-Nov-2015, FA-1190>
    Enum enumPoolDetails
        er_Type
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
        EOP_LeverageRatio
        EOP_Frequency
        KOPercentage
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

#End Region

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtPersonalSettings As DataTable
        Dim blnShowEstimatedNotional As Boolean
        Dim dtExchange As DataTable
        Dim dtExchange2 As DataTable
        Dim dtExchange3 As DataTable

        Try

            
            '</RiddhiS.>
            WebServicePath = String.Empty
            WebServicePath = ConfigurationManager.AppSettings("EQSP_WebServiceLocation").ToString
            WebServicePath = Request.Url.Scheme & Uri.SchemeDelimiter & WebServicePath
            objELNRFQ = New Web_ELNRFQ.ELN_RFQ
            objELNRFQ.Url = WebServicePath & "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx"
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            '</AvinashG. on 21-Oct-2016:EQSCB-79 - URL schema binding on Share search>
            ddlShareAccumDecum.WebServiceSettings.Path = WebServicePath & "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            'ddlShareAccumDecum.WebServiceSettings.Path = "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            '<AvinashG. on 21-Oct-2016:EQSCB-79 - URL schema binding on Share search>
            ddlShareAccumDecum.WebServiceSettings.Method = "GetSharesNames"
            '<AvinashG. on 02-Feb-2016:  FA-1286 - Display User Limit and mail to dealer desk(email id based on config)>
            oWEBADMIN = New WEB_ADMINISTRATOR.LSSAdministrator()
            oWEBADMIN.Url = LoginInfoGV.Login_Info.WebServicePath & "/LSSAdministrator/LSSAdministrator.asmx"

            generateDocument = New Web_DocumentGeneration.GenerateDocumentsForWeb
            generateDocument.Url = LoginInfoGV.Login_Info.WebServicePath & "/DocumentGeneration/DocumentGeneration.asmx"
            generateDocument.Credentials = System.Net.CredentialCache.DefaultCredentials

            oWEBMarketData = New Web_FinIQ_MarketData.FINIQ_WEBSRV_MarketData()
            oWEBMarketData.Url = LoginInfoGV.Login_Info.WebServicePath & "/WebELN_DealEntry/FINIQ_WEBSRV_MarketData.asmx"

            ''<Nikhil M. on 22-Sep-2016: Added>
            ObjCommanData = New Web_CommonFunction.CommonFunction
            ObjCommanData.Url = LoginInfoGV.Login_Info.WebServicePath & "/CommonFunction/CommonFunction.asmx"


            oWebCustomerProfile = New Web_CustomerProfile.CustomerProfile()
            oWebCustomerProfile.Url = LoginInfoGV.Login_Info.WebServicePath & "/Customer_Profile/CustomerProfile.asmx"

            objDocGen = New Web_DocGenOpenXML.DocumentGenerationOpenXml
            objDocGen.Url = LoginInfoGV.Login_Info.WebServicePath & "/DocumentGenerationOpenXml/DocumentGenerationOpenXml.asmx"

            hitCount = CInt(objReadConfig.ReadConfig(dsConfig, "FINIQ_AccumDeccum_RFQ", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "30"))
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "pollingTimeJS", "var pollingMilliSec = " + hitCount.ToString + ";", True)
            orderValidityTimer = CInt(objReadConfig.ReadConfig(dsConfig, "EQConnect_Order_Validity_Timer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "30"))
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "orderValidityTimeJS", "var orderValiditySec = " + orderValidityTimer.ToString + ";", True)


            'Mohit Lalwani on 27-Sept-2016
            For Each row As GridViewRow In grdRMData.Rows
                Dim FindCustomer As FinIQ_Fast_Find_Customer
                FindCustomer = CType(row.FindControl("FindCustomer"), FinIQ_Fast_Find_Customer)
                FindCustomer.CA_Filter = Fast_ICustomer_Search.EN_Customer_Or_Account_Filter.Account
                FindCustomer.UnVerified = FinIQApp_Web_Const.EN_ActivationFilter.Exclude
                FindCustomer.Customer_Filter = Fast_ICustomer_Search.EN_Customer_Filter.ENTITY_LRC_SELECTED_RM          'Mohit Lalwani on 22-Oct-2016
                FindCustomer.Entity_ID = LoginInfoGV.Login_Info.EntityID
                FindCustomer.Login_Id = LoginInfoGV.Login_Info.LoginId
                FindCustomer.WebCustomerProfile = oWebCustomerProfile
                FindCustomer.SSICheck = False
                FindCustomer.SetItemIndex = row.RowIndex

                ''FindCustomer.RM_ID = row.Cells(grdRMDataEnum.RM_Name).Controls.OfType(Of Label)().FirstOrDefault().Text
                FindCustomer.RM_ID = (CType(row.Cells(grdRMDataEnum.RM_Name).FindControl("ddlRMName"), RadDropDownList)).SelectedItem().Text

                '<RiddhiS. on 10-Nov-2016: Pass booking center as filter to customer find control>
                FindCustomer.CustomerBookingCenter = ddlBookingBranchPopUpValue.SelectedValue

            Next
            '/Mohit Lalwani on 27-Sept-2016




            If Page.IsPostBack = False AndAlso Request.HttpMethod = "GET" Then
                '<RiddhiS. on 28-Oct-2016: Clear session>
                Session.Remove("Scheme_AQDQ")
                Session.Remove("Template_Code_AQDQ")

                Session.Remove("dtACCDECPreTradeAllocation")
                Session.Remove("dtCustomerCodes")
                'Mohit Lalwani on 7-Sept-2016 for Personal Settings
                Dim ApplicationID As String
                If Not IsNothing(Request.QueryString("PrdToLoad")) Then
                    If UCase(Request.QueryString("PrdToLoad")) = "ACCUMULATOR" Then
                        ApplicationID = "ACC_DealEntry"
                    Else
                        ApplicationID = "DEC_DealEntry"
                    End If
                Else
                    ApplicationID = "ACC_DealEntry"
                End If
                dtPersonalSettings = New DataTable("Personal Settings")
                'ObjCommanData = New Web_CommonFunction.CommonFunction
                Select Case ObjCommanData.Web_Get_DefaultPersonalSettingsInfo(LoginInfoGV.Login_Info.EntityID, LoginInfoGV.Login_Info.LoginId, ApplicationID, dtPersonalSettings)
                    Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                        Session.Add("dtPersonalSettings", dtPersonalSettings)
                    Case Web_CommonFunction.Database_Transaction_Response.Db_No_Data
                        Session.Add("dtPersonalSettings", dtPersonalSettings)
                    Case Web_CommonFunction.Database_Transaction_Response.DB_Unsuccessful
                        Session.Add("dtPersonalSettings", dtPersonalSettings)
                End Select
                '/Mohit Lalwani on 7-Sept-2016 for Personal Settings


                'Added by Mohit Lalwani on 15-Jan-2015
                hdnConfig_EQC_Allow_ALL_AsExchangeOption.Value = objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                '/Added by Mohit Lalwani on 15-Jan-2015

                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ColorGrayedOut", " $('#ctl00_MainContent_tabContainer_tabPanelELN_txtTradeDate_txtDate').css('background-color', '#D3D3D3');", True)


                'Mohit Lalwani on 1-July-2016

                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_ShowEQOTab", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                        'Dim sLoginGrp As String
                        'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
                        'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                        If UCase(Request.QueryString("Mode")) = "ALL" Then
                            ''User is Dealer
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

                '/Mohit Lalwani on 1-July-2016
                '<MohitL. on 15-Apr-2016:Set Default notional size and daily no. of shares to 0 on main pricer screen JIRA ID: EQBOSDEV-321>
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SetZeroNotional_MainPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        txtAccumOrderqty.Text = "0"
                    Case "N", "NO"
                        ' txtAccumOrderqty.Text = "2,000"
                End Select
                '</MohitL. on 15-Apr-2016:Set Default notional size and daily no. of shares to 0 on main pricer screen JIRA ID: EQBOSDEV-321>
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allowed_QuoteMailing", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                    Case "N", "NO"
                        ''  btnMail.Visible = False
                End Select
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAQDQ_Estimated_Notional", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        fsEstimates.Visible = True
                    Case "N", "NO"
                        fsEstimates.Visible = False
                End Select
                If ((objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper = "NO" Or _
       objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper = "N") And _
       (objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "NO" Or _
       objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "N") And _
       (objReadConfig.ReadConfig(dsConfig, "EQC_Show_AccDec_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "YES" Or _
       objReadConfig.ReadConfig(dsConfig, "EQC_Show_AccDec_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "Y")) Then
                    rblShareData.SelectedValue = "calc"
                    rowAccDecCalculator.Visible = True
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
                            rowAccDecCalculator.Visible = False
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

                        lblDisplayExchangeAccumDecumVal.Visible = True
                        lblDisplayExchangeAccumDecumHeader.Visible = True
                        lblSelectionExchangeAccDecHeader.Visible = False

                    Case "N", "NO"

                        lblDisplayExchangeAccumDecumVal.Visible = False
                        lblDisplayExchangeAccumDecumHeader.Visible = False
                        lblSelectionExchangeAccDecHeader.Visible = True
                End Select

                '<Added by Mohit Lalwani on 4-Dec-2015>
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowPoolCreation_From_Pricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper()
                    Case "Y", "YES"
                        'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                        'Dim sLoginGrp As String
                        'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
                        'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                        If UCase(Request.QueryString("Mode")) = "ALL" Then
                            ''User is Dealer
                            grdAccumDecum.Columns(grdAccumDeccumEnum.CreatePool).Visible = True
                        Else
                            grdAccumDecum.Columns(grdAccumDeccumEnum.CreatePool).Visible = False
                        End If
                    Case "N", "NO"
                        grdAccumDecum.Columns(grdAccumDeccumEnum.CreatePool).Visible = False
                End Select
                '</Added by Mohit Lalwani on 4-Dec-2015>

                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowRFQandOrderDetails", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper()
                    Case "Y", "YES"
                        grdAccumDecum.Columns(grdAccumDeccumEnum.RFQ_Details).Visible = True
                    Case "N", "NO"
                        grdAccumDecum.Columns(grdAccumDeccumEnum.RFQ_Details).Visible = False
                End Select

                ''<Added by Rushikesh D. on 26-May-16 JIRAID:EQBOSDEV-385>
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_CaptureOrderComment", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        grdOrder.Columns(grdOrderEnum.EP_OrderComment).Visible = True
                    Case "N", "NO"
                        grdOrder.Columns(grdOrderEnum.EP_OrderComment).Visible = False
                End Select
                ''</Added by Rushikesh D. on 26-May-16 JIRAID:EQBOSDEV-385>


                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowRFQandOrderDetails", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper()
                    Case "Y", "YES"
                        grdOrder.Columns(grdOrderEnum.Order_Details).Visible = True
                    Case "N", "NO"
                        grdOrder.Columns(grdOrderEnum.Order_Details).Visible = False
                End Select
                btnCancelReq.Text = objReadConfig.ReadConfig(dsConfig, "EQC_ResetButtonText", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "Reset").Trim
                txtLimitPricePopUpValue.Text = "0"
                txtLimitPricePopUpValue.Enabled = False
                tabPanelAQDQ.Visible = True
                lblMsgPriceProvider.Text = ""
                lblerror.Text = ""
                lblJPMPrice.Text = ""
                txttrade.Value = FinIQApp_Date.FinIQDate(Now.ToShortDateString) ''Dilkhush 02Feb2016
                Fill_Entity()
                fill_RMList()
                fill_RFQRMList()
                fill_All_EntityBooks()
                chk_Login_For_PP()
                Get_Price_Provider()
                fill_All_Exchange()
                ''fill_All_Shares() ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                getCurrency(ddlShareAccumDecum.SelectedValue.ToString)
                setKeyPressValidations()
                makeThisGridVisible(grdAccumDecum)
                GetCommentary_Accum()
                lblComentry2.Visible = True
                setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice()
                SetTemplateDetails(SchemeName)
                SetChartColors()
                txtStrikeaccum.Enabled = False
                btnBNPPDeal.Visible = False
                btnUBSDeal.Visible = False
                btnCSDeal.Visible = False
                btnHSBCDeal.Visible = False
                btnBAMLDeal.Visible = False
                btnJPMDeal.Visible = False
                btnDBIBDeal.Visible = False

                btnOCBCDeal.Visible = False    'Mohit Lalwani on 29-Jan-2016
                btnCITIDeal.Visible = False    'Mohit Lalwani on 29-Jan-2016
                btnLEONTEQDeal.Visible = False    'Mohit Lalwani on 29-Jan-2016
                btnCOMMERZDeal.Visible = False
                If ddlShareAccumDecum.SelectedValue <> "" Then          '<RiddhiS. on 03-Oct-2016: To avoid error msg "Cannot calculate estimated notional. Rate for selected share not found." on Page load>
                    txtAccumOrderqty_TextChanged(Nothing, Nothing)
                End If
                fill_Accum_Decum_Grid()
                ''Dilkhush 02Feb2016 Move position to down 
                'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowOrderOnMainPricerPageLoad", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                '    Case "Y", "YES"
                '        rbHistory.SelectedValue = "Order History"
                '        fill_OrderGrid()
                '        ColumnVisibility()
                '    Case "N", "NO"
                '        rbHistory.SelectedValue = "Quote History"
                '        fill_Accum_Decum_Grid()
                'End Select
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Default_First_Share_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                        'getRange()
                        '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>

                        lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                    Case "N", "NO"

                        lblComentry2.Text = ""
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
                ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                'If ddlShareAccumDecum.SelectedItem Is Nothing Then
                If ddlShareAccumDecum.SelectedValue Is Nothing Then
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "hideEmail11", "try{ hideEmail(); } catch(e){ }", True) 'Added by Mohit Lalwani on 17-Oct-2016
                ElseIf ddlShareAccumDecum.SelectedValue = "" Then
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "hideEmail111", "try{ hideEmail(); } catch(e){ }", True) 'Added by Mohit Lalwani on 17-Oct-2016
                Else
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "showEmail2", "try{ showEmail(); } catch(e){ }", True) 'Added by Mohit Lalwani on 17-Oct-2016
                End If

                '<Added by Mohit Lalwnai on 25-Nov-2015 EQBOSDEV-138>
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AccDcc_DefaultGearing", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        chkLeverageRatio.Checked = True
                    Case "N", "NO"
                        chkLeverageRatio.Checked = False
                End Select


                Dim isContainAccumGUDuration As DropDownListItem = ddlAccumGUDuration.FindItemByValue(objReadConfig.ReadConfig(dsConfig, "EQC_AccDcc_DefaultGuarantee", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "0").ToString())


                If Not ddlAccumGUDuration.FindItemByValue(objReadConfig.ReadConfig(dsConfig, "EQC_AccDcc_DefaultGuarantee", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "0").ToString()) Is Nothing Then
                    ddlAccumGUDuration.SelectedValue = objReadConfig.ReadConfig(dsConfig, "EQC_AccDcc_DefaultGuarantee", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "0").ToString
                Else
                    ddlAccumGUDuration.SelectedIndex = 0
                End If


                '</Added by Mohit Lalwnai on 25-Nov-2015 >
                '<AvinashG. on 24-Jan-2016: >
                If Not IsNothing(Request.QueryString("PrdToLoad")) Then
                    If UCase(Request.QueryString("PrdToLoad")) = "ACCUMULATOR" Then
                        tabContainer.ActiveTabIndex = prdTab.Acc
                        ddlAccumType.SelectedValue = "ACCUMULATOR"
                        ddlAccumType_SelectedIndexChanged(Nothing, Nothing)
                        Accumulator.Checked = True
                    Else
                        tabContainer.ActiveTabIndex = prdTab.Dec
                        ddlAccumType.SelectedValue = "DECUMULATOR"
                        ddlAccumType_SelectedIndexChanged(Nothing, Nothing)
                        Decumulator.Checked = True
                    End If
                Else
                    ddlAccumType.SelectedValue = "ACCUMULATOR"
                    ddlAccumType_SelectedIndexChanged(Nothing, Nothing)
                    Accumulator.Checked = True
                End If
                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setCalc", "AccDecCalcType();", True)

                ''Dilkhush 02Feb2016 Move position to down 
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowOrderOnMainPricerPageLoad", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        rbHistory.SelectedValue = "Order History"
                        fill_OrderGrid()
                        ColumnVisibility()
                    Case "N", "NO"
                        rbHistory.SelectedValue = "Quote History"
                        fill_Accum_Decum_Grid()
                End Select

                '</AvinashG. on 24-Jan-2016: >
                If Not IsNothing(Request.QueryString("EXTLOD")) Then
                    If UCase(Request.QueryString("EXTLOD")) = "1" Then
                        If Request.QueryString("Prd").ToString.Trim.ToUpper = "ACC" Or Request.QueryString("Prd").ToString.Trim.ToUpper = "DEC" Then

                            If Request.QueryString("Prd").ToString.Trim.ToUpper = "ACC" Then
                                ddlAccumType.SelectedValue = "ACCUMULATOR"
                                tabContainer.ActiveTabIndex = prdTab.Acc
                            Else
                                ddlAccumType.SelectedValue = "DECUMULATOR"
                                tabContainer.ActiveTabIndex = prdTab.Dec
                            End If
                            tabIndex = tabContainer.ActiveTabIndex
                            If Not IsNothing(Request.QueryString("Frequency")) Then
                                ddlFrequencyAccumDecum.SelectedValue = Request.QueryString("Frequency")
                                ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                            End If
                            If Not IsNothing(Request.QueryString("Gu")) Then
                                ddlAccumGUDuration.SelectedValue = Request.QueryString("Gu")
                            Else
                                ddlAccumGUDuration.SelectedValue = "0"
                            End If
                            If Not IsNothing(Request.QueryString("2xLeverage")) And Request.QueryString("2xLeverage").Trim.ToUpper = "YES" Then
                                chkLeverageRatio.Checked = True
                            Else
                                chkLeverageRatio.Checked = False
                            End If
                            chkLeverageRatio_CheckedChanged(Nothing, Nothing)
                            If Not IsNothing(Request.QueryString("KOLevel")) Then
                                txtKO.Text = Request.QueryString("KOLevel")
                            Else
                                If Request.QueryString("Prd").ToString.Trim.ToUpper = "ACC" Then
                                    txtKO.Text = "105"
                                Else
                                    txtKO.Text = "95"
                                End If
                            End If
                            If Request.QueryString("SolveFor").ToString.Trim.ToUpper = "STRIKE" Then
                                ddlSolveForAccumDecum.SelectedValue = "STRIKE"
                                txtUpfront.Text = Request.QueryString("Upfront").ToString
                            Else
                                ddlSolveForAccumDecum.SelectedValue = "UPFRONT"
                                txtStrikeaccum.Text = Request.QueryString("Strike").ToString
                            End If
                            Dim strTenorTypeAQDQ, strTenorValAQDQ As String
                            If Not IsNothing(Request.QueryString("Tenor")) Then
                                If Request.QueryString("Tenor").ToString.ToUpper.Contains("M") Then
                                    strTenorTypeAQDQ = "MONTH"
                                    strTenorValAQDQ = Request.QueryString("Tenor").ToString.ToUpper.Replace("M", "")
                                ElseIf Request.QueryString("Tenor").ToString.ToUpper.Contains("W") Then
                                    strTenorTypeAQDQ = "WEEK"
                                    strTenorValAQDQ = Request.QueryString("Tenor").ToString.ToUpper.Replace("W", "")
                                ElseIf Request.QueryString("Tenor").ToString.ToUpper.Contains("Y") Then
                                    strTenorTypeAQDQ = "YEAR"
                                    strTenorValAQDQ = Request.QueryString("Tenor").ToString.ToUpper.Replace("Y", "")
                                Else
                                End If
                            Else
                                strTenorTypeAQDQ = "MONTH"
                            End If
                            ddlTenorTypeAccum.SelectedValue = strTenorTypeAQDQ
                            txtTenorAccumDecum.Text = strTenorValAQDQ
                            '<Changed By Mohit Lalwani on 17_Dec-2015>
                            ''If Not IsNothing(Request.QueryString("NoOfShare")) Then
                            ''    txtAccumOrderqty.Text = Request.QueryString("NoOfShare")
                            ''Else
                            ''    txtAccumOrderqty.Text = "0"
                            ''End If
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SetDefaultNotional", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                    If Not IsNothing(Request.QueryString("NoOfShare")) Then
                                        If Request.QueryString("NoOfShare").ToString <> "" Then
                                            txtAccumOrderqty.Text = Request.QueryString("NoOfShare").ToString
                                        Else
                                            txtAccumOrderqty.Text = "0"
                                        End If
                                    Else
                                        txtAccumOrderqty.Text = "0"
                                    End If

                                Case "N", "NO"
                                    txtAccumOrderqty.Text = "0"
                            End Select
                            txtAccumOrderqty_TextChanged(Nothing, Nothing)
                            '</Changed By Mohit Lalwani on 17_Dec-2015>
                            If Not IsNothing(Request.QueryString("Share")) Then
                                Dim strshare As String = Request.QueryString("Share").ToString
                                dtExchange = New DataTable("Dummy")
                                Select Case objELNRFQ.DB_GetExchange(strshare, dtExchange)
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                        '     ddlExchangeAccumDecum.SelectedValue = dtExchange.Rows(0)(0).ToString
                                        '      ddlExchangeAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                        setShare(ddlExchangeAccumDecum.SelectedValue.ToString, strshare)
                                        ddlShareAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

                                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                End Select
                              
                                'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                '    Case "Y", "YES"
                                '        Dim strshare As String = Request.QueryString("Share").ToString
                                '        ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(strshare.Trim))
                                '        ddlShareAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                '    Case "N", "NO"
                                '        dtExchange = New DataTable("Dummy")
                                '        Select Case objELNRFQ.DB_GetExchange(Request.QueryString("Share").Trim, dtExchange)
                                '            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                '                ddlExchangeAccumDecum.SelectedValue = dtExchange.Rows(0)(0).ToString
                                '                ddlExchangeAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                '            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                                '            Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                '        End Select
                                '        Dim strshare As String = Request.QueryString("Share").ToString
                                '        ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(strshare.Trim))
                                '        ddlShareAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                'End Select
                            Else
                            End If
                            Call getCurrency(ddlShareAccumDecum.SelectedValue.ToString)
                            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                            'Call getRange()
                            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                            '    Call DisplayEstimatedNotional()  '<RiddhiS. on 26-Sep-2016: No share is selected on page load, so notional cannot be calculated>
                            ResetAll()   'Added by Mohit Lalwani on 18-Apr-2016 FA-1405 - Issuer not disabled when redirected from multi-stock pricer 
                        End If
                        chk_Login_For_PP()

                    ElseIf UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Then
                        Session.Remove("Share_Value")
                        If Request.QueryString("PRD").Trim = "" Then
                            lblerror.Text = "Error: Failed while trying to load pool data."
                        Else
                            setACCDECPoolData()
                        End If
                        btnSolveAll.Enabled = False
                    ElseIf UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                        Session.Remove("Share_Value")
                        If Request.QueryString("PRD").Trim = "" Then
                            lblerror.Text = "Error: Failed while trying to load pool data."
                        Else
                            setACCDECPoolData()
                        End If
                        btnSolveAll.Enabled = False
                    ElseIf Request.QueryString("EXTLOD").ToString.Trim.ToUpper() = "REDIRECTEDHEDGE" Then
                        Session.Remove("Share_Value")
                        If Request.QueryString("PRD").Trim = "" Then
                            lblerror.Text = "Error: Failed while trying to load order data."
                        Else
                            setRedirectedACCDECPoolData()
                        End If

                    Else

                    End If
                    ''Dilkhush 01Feb2016 To show email button
                    ''<Start | Nikhil M. on 04-Oct-2016: For Hide/Show allocation>
                    If IsNothing(Request.QueryString("PoolID")) Then
                        grdRMData.Visible = True
                        btnAddAllocation.Visible = True
                        tblRw1.Visible = True ''<Nikhil M. on 17-Oct-2016: Added for hidning the COntrol on hedge>
                        tblRw2.Visible = True
                        tblRw3.Visible = True
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

                        End If
                    End If
                    ''<End | Nikhil M. on 04-Oct-2016: For Hide/Show>
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ShowhideACCDECEmailbtn", "try{ showEmail(); } catch(e){ }", True)         'Added by Mohit Lalwani on 17-Oct-2016
                Else
                    SetPersonalSetting()            'Mohit Lalwani on 23-Sept-2016
                End If


                'Mohit Lalwani on 12-Oct-2016
                If Not IsNothing(Request.QueryString("Mode")) Then
                    If UCase(Request.QueryString("Mode")) = "DEFSETUP" Then
                        'Mohit Lalwani on 26-Oct-2016
                        tdpnlReprice.Visible = False
                        '   pnlReprice.Visible = False
                        '/Mohit Lalwani on 26-Oct-2016
                        upnlGrid.Visible = False

                        tdCommentry.Visible = False
                        '      tdgrphShareData.Visible = False
                        tdShareGraphData.Visible = False
                        trSaveSetting.Visible = True
                        tdShareGraphData1.Visible = False
                    Else
                        trSaveSetting.Visible = False
                    End If
                Else
                    trSaveSetting.Visible = False
                End If
                '/Mohit Lalwani on 12-Oct-2016

            End If
            tabIndex = tabContainer.ActiveTabIndex

            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Fill_All_Charts()
            Else
                Call manageShareReportShowHide()
            End If
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAccDec_WeeklyFrequency", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    For Each item As DropDownListItem In ddlFrequencyAccumDecum.Items
                        If item.Value.ToString.ToUpper.Trim = "WEEKLY" Then
                            item.Enabled = True
                            Exit For
                        End If
                    Next
                Case "N", "NO"
                    For Each item As DropDownListItem In ddlFrequencyAccumDecum.Items
                        If item.Value.ToString.ToUpper.Trim = "WEEKLY" Then
                            item.Enabled = False
                            Exit For
                        End If
                    Next
            End Select
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAccDec_MonthlyFrequency", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    For Each item As DropDownListItem In ddlFrequencyAccumDecum.Items
                        If item.Value.ToString.ToUpper.Trim = "MONTHLY" Then
                            item.Enabled = True
                            Exit For
                        End If
                    Next
                Case "N", "NO"
                    For Each item As DropDownListItem In ddlFrequencyAccumDecum.Items
                        If item.Value.ToString.ToUpper.Trim = "MONTHLY" Then
                            item.Enabled = False
                            Exit For
                        End If
                    Next
            End Select

            ''<Dilkhush\Avinash (10Dec2015) : Added to handle on config base>
            Dim sEQC_DealerRedirection_OnPricer As String = objReadConfig.ReadConfig(dsConfig, "EQC_DealerRedirection_OnPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO")
            Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                Case "Y", "YES"
                    'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                    'Dim sLoginGrp As String
                    'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup

                    'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                    If UCase(Request.QueryString("Mode")) = "ALL" Then
                        ''User is Dealer
                        grdOrder.Columns(grdOrderEnum.EP_HedgedFor).Visible = True
                        grdOrder.Columns(grdOrderEnum.EP_HedgingOrderId).Visible = False
                    Else
                        ''USer is RM
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
            ''</Dilkhush\Avinash (10Dec2015) : Added to handle on config base>


            chkPPmaillist.Visible = False

            ''ddlShareAccumDecum.Localization.NoMatches = "No matches"
            hideShowRBLShareData()
            '<shekhar 26-Nov-2015>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "CP_Dragable", "CP_Dragable();", True)
            '</shekhar 26-Nov-2015>
            '<Mohit Lalwani on 26-May-2016>
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "setResolution", "setResolution();", True)
            '</Mohit Lalwani on 26-May-2016>

            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setAccCalc1", "clearACCDECCalDataONLoad();", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setCalc", "AccDecCalcType();", True)


            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "hideLPBoxes", "hideLPBoxes();", True)''<Nikhil M. on 19-Sep-2016: Commented>

        Catch ex As Exception
            lblerror.Text = "Error occurred on Page Load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Page_Load", ErrorLevel.High)
        End Try
    End Sub

    'Added by Mohit Lalwani on 16-Sept-2016
    Public Sub SetPersonalSetting()
        Dim dtExchange As DataTable
             Try
            ddlFrequencyAccumDecum.SelectedValue = getControlPersonalSetting("Frequency", "Monthly")
            ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
            ddlAccumGUDuration.SelectedValue = getControlPersonalSetting("Guarantee", "0")
            chkLeverageRatio.Checked = CBool(getControlPersonalSetting("Is 2x Gearing Leverage", "FALSE"))
            chkLeverageRatio_CheckedChanged(Nothing, Nothing)

            If ddlAccumType.SelectedValue = "ACCUMULATOR" Then
                txtKO.Text = getControlPersonalSetting("KO", "105.00")
            Else
                txtKO.Text = getControlPersonalSetting("KO", "95.00")
            End If

            '      If Request.QueryString("SolveFor").ToString.Trim.ToUpper = "STRIKE" Then
            ddlSolveForAccumDecum.SelectedValue = getControlPersonalSetting("Solve For", "STRIKE")

            ''<Added by Dilkhush on 05-Oct-2016>
            If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                txtStrikeaccum.Text = ""
                txtStrikeaccum.Enabled = False
                txtUpfront.Enabled = True
                lblSolveForType.Text = "Strike (%)"
		'Added by Mohit Lalwani on 17-Oct-2016
                txtUpfront.Text = getControlPersonalSetting("Upfront", "0.5")
                txtStrikeaccum.Text = "0.00"
		'/Added by Mohit Lalwani on 17-Oct-2016
            Else
                txtUpfront.Text = ""
                txtUpfront.Enabled = False
                txtStrikeaccum.Enabled = True
                lblSolveForType.Text = "Upfront (%)"
		'Added by Mohit Lalwani on 17-Oct-2016
                txtUpfront.Text = "0.00"
                txtStrikeaccum.Text = getControlPersonalSetting("Strike", "")
                txtUpfront.Text = ""
		'/Added by Mohit Lalwani on 17-Oct-2016
            End If
            ''</Added by Dilkhush on 05-Oct-2016>


            Dim strTenorTypeAQDQ, strTenorValAQDQ As String
            'If Not IsNothing(Request.QueryString("Tenor")) Then
            '    If Request.QueryString("Tenor").ToString.ToUpper.Contains("M") Then
            strTenorTypeAQDQ = getControlPersonalSetting("Tenor Type", "MONTH")
            strTenorValAQDQ = getControlPersonalSetting("Tenor", "12")
            '    ElseIf Request.QueryString("Tenor").ToString.ToUpper.Contains("W") Then
            '        strTenorTypeAQDQ = "WEEK"
            '        strTenorValAQDQ = Request.QueryString("Tenor").ToString.ToUpper.Replace("W", "")
            '    ElseIf Request.QueryString("Tenor").ToString.ToUpper.Contains("Y") Then
            '        strTenorTypeAQDQ = "YEAR"
            '        strTenorValAQDQ = Request.QueryString("Tenor").ToString.ToUpper.Replace("Y", "")
            '    Else
            '    End If
            'Else
            '    strTenorTypeAQDQ = "MONTH"
            'End If
            ddlTenorTypeAccum.SelectedValue = strTenorTypeAQDQ
            txtTenorAccumDecum.Text = strTenorValAQDQ
            'Changed by Mohit Lalwani on 21-Oct-2016 
            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SetDefaultNotional", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SetZeroNotional_MainPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                '/Changed by Mohit Lalwani on 21-Oct-2016
                Case "Y", "YES"
                    txtAccumOrderqty.Text = "0"
                Case "N", "NO"
                    txtAccumOrderqty.Text = getControlPersonalSetting("Daily no. of Shares", "")

            End Select
            ''02Nov2016 Mohit/Dilkhush : Added if condition to avoid error msg if we are not setting anything in orderquantity
            If txtAccumOrderqty.Text <> "" Then
                txtAccumOrderqty_TextChanged(Nothing, Nothing)
            End If
            '</Changed By Mohit Lalwani on 17_Dec-2015>
            '  If Not IsNothing(Request.QueryString("Share")) Then
            Dim strshare As String = getControlPersonalSetting("Share", "")
            dtExchange = New DataTable("Dummy")
            If strshare <> "" Then
                Select Case objELNRFQ.DB_GetExchange(strshare, dtExchange)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        'ddlExchangeAccumDecum.SelectedValue = dtExchange.Rows(0)(0).ToString
                        'ddlExchangeAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                        setShare(dtExchange.Rows(0)(0).ToString, strshare)    'Mohit Lalwani on 07-Nov-2016
                        ddlShareAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                End Select
               
            End If
            'Else
            'End If
            Call getCurrency(ddlShareAccumDecum.SelectedValue.ToString)
            ' Call DisplayEstimatedNotional() '<RiddhiS. on 26-Sep-2016: No share is selected in personal settings.>
            '  ResetAll()   'Added by Mohit Lalwani on 18-Apr-2016 FA-1405 - Issuer not disabled when redirected from multi-stock pricer 
        Catch ex As Exception
            lblerror.Text = "Error occurred in binding personal settings.Please contact admin."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "SetPersonalSetting", ErrorLevel.High)
            Throw ex
        End Try
    End Sub



'Added by Mohit Lalwani on 17-Oct-2016
    Public Function Write_PersonalSettings_TOXML(ByRef o_strXMLNote_DefaultValues As String) As Boolean
        Dim strXMLRFQ As StringBuilder
        Dim dtQuote As New DataTable
        Try
            strXMLRFQ = New StringBuilder

            strXMLRFQ.Append("<SettingDetails>")
            strXMLRFQ.Append("<Default>")
            strXMLRFQ.Append("<Frequency>" & ddlFrequencyAccumDecum.SelectedValue & "</Frequency>")
            strXMLRFQ.Append("<Guarantee>" & ddlAccumGUDuration.SelectedValue & "</Guarantee>")
            strXMLRFQ.Append("<Is_2x_Gearing_Leverage>" & chkLeverageRatio.Checked & "</Is_2x_Gearing_Leverage>")
            strXMLRFQ.Append("<KO>" & txtKO.Text & "</KO>")
            strXMLRFQ.Append("<Solve_For>" & ddlSolveForAccumDecum.SelectedValue & "</Solve_For>")
            strXMLRFQ.Append("<Upfront>" & txtUpfront.Text & "</Upfront>")
            strXMLRFQ.Append("<Strike>" & txtStrikeaccum.Text & "</Strike>")
            strXMLRFQ.Append("<Tenor_Type>" & ddlTenorTypeAccum.SelectedValue & "</Tenor_Type>")
            strXMLRFQ.Append("<Tenor>" & txtTenorAccumDecum.Text & "</Tenor>")
            strXMLRFQ.Append("<Daily_no_of_Shares>" & txtAccumOrderqty.Text & "</Daily_no_of_Shares>")
            strXMLRFQ.Append("<Share>" & ddlShareAccumDecum.SelectedValue.ToString & "</Share>")
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
'/Added by Mohit Lalwani on 17-Oct-2016


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
                .DBIB = Color.Blue
                .OCBC = Color.GreenYellow        'Mohit Lalwani on 29-Jan-2016
                .CITI = Color.Brown        'Mohit Lalwani on 29-Jan-2016
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



    Public Sub setKeyPressValidations()
        Try
            txtTotalRows.Attributes.Add("onkeypress", "AllowOnlyNumeric()")
            txtTenorAccumDecum.Attributes.Add("onkeypress", "AllowOnlyNumeric()")
            txtUpfront.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtUpfront.ClientID & "');")
            txtDuration.Attributes.Add("onkeypress", "AllowOnlyNumeric()")
            txtStrikeaccum.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtStrikeaccum.ClientID & "');")
            txtKO.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtKO.ClientID & "');")
            txtAccumOrderqty.Attributes.Add("onkeypress", "AllowOnlyNumeric()")
            txtUpfrontPopUpValue.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtUpfrontPopUpValue.ClientID & "');")
            btnJPMprice.Attributes.Add("onclick", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlAccumType.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtTenorAccumDecum.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlExchangeAccumDecum.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlShareAccumDecum.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlFrequencyAccumDecum.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtDuration.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlKOSettlementType.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtUpfront.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtAccumOrderqty.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            ddlSolveForAccumDecum.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtKO.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            chkLeverageRatio.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtStrikeaccum.Attributes.Add("onchange", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            txtLimitPricePopUpValue.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtLimitPricePopUpValue.ClientID & "');")
            btnDBIBPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
            btnCSPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
            btnBNPPPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            btnHSBCPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
            btnOCBCPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
            btnCITIPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
            btnLEONTEQPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
            btnCOMMERZPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
            btnJPMprice.Attributes.Add("onclick", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            btnUBSPrice.Attributes.Add("onclick", "StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
            btnBAMLPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
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
                                '</AvinashG. on 02-Feb-2016: Consistent date format in one area>
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
                    'If ddlShareAccumDecum.SelectedItem Is Nothing Then
                    If ddlShareAccumDecum.SelectedValue Is Nothing Then
                        setTDSSData("")
                        Exit Sub
                    Else
                        setTDSSData(ddlShareAccumDecum.SelectedValue.ToString)
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
            'Mohit Lalwani on 29-Jan-2016
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
                btnLEONTEQPrice.Style.Add("visibility", "visible")
                tdLEONTEQ1.Style.Remove("display")
                lblLEONTEQ.Visible = True
                lblLEONTEQPrice.Visible = True
                lblTimerLEONTEQ.Visible = True
                TRLEONTEQ1.Visible = True
            Else
                btnLEONTEQPrice.Style.Add("visibility", "hidden")
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
                btnCOMMERZPrice.Style.Add("visibility", "visible")
                tdCOMMERZ1.Style.Remove("display")
                lblCOMMERZ.Visible = True
                lblCOMMERZPrice.Visible = True
                lblTimerCOMMERZ.Visible = True
                TRCOMMERZ1.Visible = True
            Else
                btnCOMMERZPrice.Style.Add("visibility", "hidden")
                btnCOMMERZDeal.Style.Add("visibility", "hidden")
                tdCOMMERZ1.Style.Remove("display")
                tdCOMMERZ1.Style.Add("display", "none !important;")
                lblCOMMERZ.Visible = False
                lblCOMMERZPrice.Visible = False
                lblTimerCOMMERZ.Visible = False
                TRCOMMERZ1.Visible = False
            End If
            '/Mohit Lalwani on 29-Jan-2016
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
                lblBAMLPrice.Visible = False  ''<Rutuja S. on 14-Nov-2014: Changed lblTimerBAML to lblBAMLPrice >
                lblTimerBAML.Visible = False
                TRBAML1.Visible = False
            End If
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
                btnDBIBDeal.Visible = False  'Urgent
                tdDBIB.Style.Remove("display")
                tdDBIB.Style.Add("display", "none !important;")
                lblDBIB.Visible = False
                lblDBIBPrice.Visible = False  ''<Rutuja S. on 14-Nov-2014: Changed lblTimerBAML to lblBAMLPrice >
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
            tabPanelAQDQ.Visible = True
            '<AvinashG. on 29-Jan-2016: FA-1282 - Make Buyser Issuer Mapping product specific on DRA,FCN and Accum,Decum>
            Select Case ddlAccumType.SelectedValue.ToUpper
                Case "DECUMULATOR"
                    SchemeName = "DAC"
                Case "ACCUMULATOR"
                    SchemeName = "ACC"
            End Select
            Session.Add("Scheme_AQDQ", SchemeName) '<RiddhiS. on 28-Oct-2016: Different session name for eahch product>
            'SchemeName = "DAC"
            '</AvinashG. on 29-Jan-2016: FA-1282 - Make Buyser Issuer Mapping product specific on DRA,FCN and Accum,Decum>

            Select Case objELNRFQ.Db_Get_Avail_Login_For_PP(strLoginName, SchemeName, dtLoginPP)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtLoginPP.Rows.Count > 0 Then
                        '<AvinashG. on 29-Jan-2016: FA-1282 - Make Buyser Issuer Mapping product specific on DRA,FCN and Accum,Decum>
                        Select Case ddlAccumType.SelectedValue.ToUpper
                            Case "DECUMULATOR"
                                dr = dtLoginPP.Select("Product_Code='DAC'")
                            Case "ACCUMULATOR"
                                dr = dtLoginPP.Select("Product_Code='ACC'")
                        End Select
                        'dr = dtLoginPP.Select("Product_Code='DAC'")
                        '</AvinashG. on 29-Jan-2016: FA-1282 - Make Buyser Issuer Mapping product specific on DRA,FCN and Accum,Decum>
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
                    btnHSBCPrice.Style.Add("visibility", "hidden")
                    btnHSBCDeal.Style.Add("visibility", "hidden")
                    'Mohit Lalwani on 29-Jan-2016
                    'btnDBIBPrice.Style.Add("visibility", "hidden")             //Commented by Mohit
                    'btnDBIBDeal.Style.Add("visibility", "hidden")              //Commented by Mohit
                    btnOCBCPrice.Style.Add("visibility", "hidden")
                    btnOCBCDeal.Style.Add("visibility", "hidden")
                    btnCITIPrice.Style.Add("visibility", "hidden")
                    btnCITIDeal.Style.Add("visibility", "hidden")
                    btnLEONTEQPrice.Style.Add("visibility", "hidden")
                    btnLEONTEQDeal.Style.Add("visibility", "hidden")
                    btnCOMMERZPrice.Style.Add("visibility", "hidden")
                    btnCOMMERZDeal.Style.Add("visibility", "hidden")
                    '/Mohit Lalwani on 29-Jan-2016
                    tdJPM1.Style.Remove("display")
                    tdJPM1.Style.Add("display", "none !important;")
                    tdHSBC1.Style.Remove("display")
                    tdHSBC1.Style.Add("display", "none !important;")
                    'Mohit Lalwani on 29-Jan-2016
                    tdOCBC1.Style.Remove("display")
                    tdOCBC1.Style.Add("display", "none !important;")
                    tdCITI1.Style.Remove("display")
                    tdCITI1.Style.Add("display", "none !important;")
                    tdLEONTEQ1.Style.Remove("display")
                    tdLEONTEQ1.Style.Add("display", "none !important;")
                    tdCOMMERZ1.Style.Remove("display")
                    tdCOMMERZ1.Style.Add("display", "none !important;")
                    '/Mohit Lalwani on 29-Jan-2016
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
                    lblHSBC.Visible = False
                    lblHSBCPrice.Visible = False
                    lblTimerHSBC.Visible = False
                    'Added by Mohit on 29-Jan-2016
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

                    lblDBIBPrice.Visible = False 'Urgent
                    lblTimerDBIB.Visible = False    'Urgent
                    '/Added by Mohit on 29-Jan-2016
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
                    TROCBC1.Visible = False   'Mohit Lalwani on 29-Jan-2016 
                    TRCITI1.Visible = False 'Mohit Lalwani on 29-Jan-2016 
                    TRLEONTEQ1.Visible = False 
                    TRCOMMERZ1.Visible = False
                    TRCS1.Visible = False
                    TRUBS1.Visible = False
                    TRBNPP1.Visible = False
                    TRDBIB.Visible = False
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
                        chkHSBC.Enabled = False  ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
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
                    btnOCBCPrice.Enabled = False
                    btnOCBCPrice.CssClass = "btnDisabled"
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
                    btnCITIPrice.Enabled = False
                    btnCITIPrice.CssClass = "btnDisabled"
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
                        btnLEONTEQPrice.Enabled = True
                        btnLEONTEQPrice.CssClass = "btn"
                        lblLEONTEQPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkLEONTEQ.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else
                        btnLEONTEQPrice.Enabled = False
                        btnLEONTEQPrice.CssClass = "btnDisabled"
                        lblLEONTEQPrice.Visible = False
                        chkLEONTEQ.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnLEONTEQPrice.Enabled = False
                    btnLEONTEQPrice.CssClass = "btnDisabled"
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
                        btnCOMMERZPrice.Enabled = True
                        btnCOMMERZPrice.CssClass = "btn"
                        lblCOMMERZPrice.Visible = True
                        If blnLPIs_Up = False Then
                            blnLPIs_Up = True
                        End If
                        chkCOMMERZ.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else
                        btnCOMMERZPrice.Enabled = False
                        btnCOMMERZPrice.CssClass = "btnDisabled"
                        lblCOMMERZPrice.Visible = False
                        chkCOMMERZ.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnCOMMERZPrice.Enabled = False
                    btnCOMMERZPrice.CssClass = "btnDisabled"
                    lblCOMMERZPrice.Visible = False
                    chkCOMMERZ.Enabled = False
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
                        chkBNPP.Enabled = False ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
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
            dr1 = dtGetLoginPP.Select("PP_CODE ='DB'")
            If dr1.Length > 0 Then
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
                    '<AvinashG. on 10-Mar-2016: FA-1365 JPM ELN API Notification: Error >
                    Select Case tabContainer.ActiveTabIndex
                        Case prdTab.Acc
                            dr2 = dtGetLoginPP.Select("Product_Code='ACC'")
                        Case prdTab.Dec
                            dr2 = dtGetLoginPP.Select("Product_Code='DAC'")
                    End Select
                    '</AvinashG. on 10-Mar-2016: FA-1365 JPM ELN API Notification: Error >

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
                        .DataTextField = "Host"
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
                        .DataTextField = "Host"
                        .DataValueField = "Rel_Manager_Name"
                        .DataBind()
                        .Items.Insert(0, "")
                        If dtRMList.Rows.Count = 1 Then
                            .SelectedIndex = 1
                        Else
                            .SelectedIndex = 0
                        End If
                        '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                        Session.Add("ACCDEC_DTRMList", dtRMList)
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
            ' SchemeName = "Accum/Decum"     '<RiddhiS./Dilkhush on 28-Oct-2016: already passed as method parameter>
            'Session.Add("Scheme", SchemeName)
            '' Session.Add("Scheme_AQDQ", SchemeName) '<RiddhiS. on 28-Oct-2016: Scheme session name different for each product>
            'Select Case objELNRFQ.DB_Get_TemplateDetails("ELN", dtTemplate)
            Select Case ddlAccumType.SelectedValue.ToUpper
                Case "DECUMULATOR"
                    SchemeName = "DAC"
                Case "ACCUMULATOR"
                    SchemeName = "ACC"
            End Select
            Select Case objELNRFQ.DB_Get_TemplateDetails(SchemeName, dtTemplate) '<RiddhiS. on 28-Oct-2016:  product name passed>
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtTemplate.Rows.Count > 0 Then
                        strDefaultExchange = dtTemplate.Rows(0).Item("ST_Exchange_Name").ToString
                        TemplateId = dtTemplate.Rows(0).Item("ST_Template_ID").ToString
                        'Session.Add("Template_Code", TemplateId)
                        Session.Add("Template_Code_AQDQ", TemplateId)  '<RiddhiS. on 28-Oct-2016: Different session name for each product>
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
        Dim Sprd As String
        Try
            '  If ddlQuantoCcy.SelectedValue.Trim = "" Then
            If ddlShareAccumDecum.Text.Trim = "Please select valid share." Or ddlShareAccumDecum.SelectedValue.Trim = "" Then
                txt_RM_Limit.Text = "Max User Limit: "
                txt_RM_Limit.ToolTip = "Max User Limit: "
            Else
                If ddlAccumType.SelectedValue.ToUpper = "ACCUMULATOR" Then
                    Sprd = "Accumulator"
                Else
                    Sprd = "Decumulator"
                End If

                Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, lblAQDQBaseCcy.Text, Sprd, dblUserLimit)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        txt_RM_Limit.Text = "Max User Limit(" + lblAQDQBaseCcy.Text + "): " + convertNotionalintoShort((If(dblUserLimit.ToString = "", 0, CDbl(dblUserLimit))), "MAX")
                        txt_RM_Limit.ToolTip = "Max User Limit(" + lblAQDQBaseCcy.Text + "): " + FormatNumber(dblUserLimit.ToString, 2).Replace(".00", "")
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                        txt_RM_Limit.Text = "Max User Limit: "
                        txt_RM_Limit.ToolTip = "Max User Limit: "
                        ' lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
                End Select
            End If
        Catch ex As Exception
            txt_RM_Limit.Text = "Max User Limit: "
            txt_RM_Limit.ToolTip = "Max User Limit: "
            lblerror.Text = "setRMLimit:Error occurred."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setRMLimit", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    '</Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>

    Public Sub Fill_Accum_ddl_Share()
        Dim dtShare As DataTable
        Dim I_Marketype As String = String.Empty
        Try
            dtShare = New DataTable("DUMMY")
            If (Not IsNothing(CType(Session("Share_Value"), DataTable)) AndAlso CType(Session("Share_Value"), DataTable).Rows.Count > 0) Then
                dtShare = CType(Session("Share_Value"), DataTable)
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        With ddlShareAccumDecum
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
                        dtExchShares = SelectIntoDataTable("ExchangeCode = '" + ddlExchangeAccumDecum.SelectedValue + "'", dtShare)
                        With ddlShareAccumDecum
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
                Select Case objELNRFQ.Db_Get_Share_Details(I_Marketype, ddlExchangeAccumDecum.SelectedValue, dtShare)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        With ddlShareAccumDecum
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
                        With ddlShareAccumDecum
                            .DataSource = dtShare
                            .DataBind()
                        End With
                End Select
            End If
            GetCommentary_Accum()
        Catch ex As Exception
            lblerror.Text = "Fill_Accum_ddl_Share:Error occurred in filling Share."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Fill_Accum_ddl_Share", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub Fill_Accum_exchange()
        Dim dtExchange As DataTable
        Try
            dtExchange = New DataTable("Exchange")
            Select Case objELNRFQ.DB_Fill_Exchange_Combo(dtExchange)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlExchangeAccumDecum
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
                    ddlExchangeAccumDecum.DataSource = dtExchange
                    ddlExchangeAccumDecum.DataBind()
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            End Select
            GetCommentary_Accum()
        Catch ex As Exception
            lblerror.Text = "Fill_Accum_exchange:Error occurred in filling exchange."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Fill_Accum_exchange", ErrorLevel.High)
        End Try
    End Sub


    Public Sub fill_All_Exchange()
        Dim dtExchange As DataTable
        Try
            dtExchange = New DataTable("Exchange")
            Select Case objELNRFQ.DB_Fill_Exchange_Combo(dtExchange)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlExchangeAccumDecum
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
                    With ddlExchangeAccumDecum
                        .DataSource = dtExchange
                        .DataBind()
                    End With
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
                        With ddlShareAccumDecum
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
                        dtExchShares = SelectIntoDataTable("ExchangeCode = '" + ddlExchangeAccumDecum.SelectedValue + "'", dtShare)
                        With ddlShareAccumDecum
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
                                With ddlShareAccumDecum
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
                                dtExchShares = SelectIntoDataTable("ExchangeCode = '" + ddlExchangeAccumDecum.SelectedValue + "'", dtShare)
                                With ddlShareAccumDecum
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
                        Select Case objELNRFQ.Db_Get_Share_Details(I_Marketype, ddlExchangeAccumDecum.SelectedValue, dtShare)
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                With ddlShareAccumDecum
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
                                With ddlShareAccumDecum
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

    Public Sub fill_Accum_Decum_Grid()
        Dim dtAccum As DataTable
        Dim strPrdCode As String '<AvinashG. on 25-Jan-2016: EQBOSDEV-233 ELN/DRA/FCN/ACCU/DECU in separate tabs >
        Try
            ''''Dilkhush 23sep2016 LoadAll RFQ or Best RFQ
            Dim strExpandAll As String
            If chkExpandAllRFQ.Checked Then
                strExpandAll = "YES"
            Else
                strExpandAll = "NO"
            End If
            ''''Dilkhush 23sep2016 LoadAll RFQ or Best RFQ

            dtAccum = New DataTable("DUMMY")
            strPrdCode = ddlAccumType.SelectedItem.Value
            '<RiddhiS. on 28-Oct-2016: for passing template code>
            Dim strSchemeName As String = ""
            Select Case ddlAccumType.SelectedValue.ToUpper
                Case "DECUMULATOR"
                    strSchemeName = "DAC"
                Case "ACCUMULATOR"
                    strSchemeName = "ACC"
            End Select
            '</RiddhiS.>

            Dim strMode As String = If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "SELF", "SELF", If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "GROUP", "GRP", "ALL"))
            ''''Dilkhush 23sep2016 Added variable to LoadAll RFQ or Best RFQ
            '<AvinashG. on 25-Jan-2016: EQBOSDEV-233 ELN/DRA/FCN/ACCU/DECU in separate tabs >
            ''Select objELNRFQ.Get_ProductBased_Accum_Decum_RFQ("ELN", LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, strPrdCode, dtAccum)

            '<RiddhiS. on 28-Oct-2016: Passed Scheme name>
            '  Select Case objELNRFQ.Get_ProductBased_Accum_Decum_RFQ("ELN", LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, strPrdCode, strExpandAll, dtAccum)
            Select Case objELNRFQ.Get_ProductBased_Accum_Decum_RFQ(strSchemeName, LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, strPrdCode, strExpandAll, dtAccum)
                '</RiddhiS.>
                'Select Case objELNRFQ.DB_Get_Accum_Decum("ELN", LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, dtAccum)
                '</AvinashG. on 25-Jan-2016: EQBOSDEV-233 ELN/DRA/FCN/ACCU/DECU in separate tabs >
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtAccum.Rows.Count > 0 Then
                        grdAccumDecum.CurrentPageIndex = 0
                        grdAccumDecum.DataSource = dtAccum
                        grdAccumDecum.DataBind()
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    grdAccumDecum.CurrentPageIndex = 0
                    dtAccum = dtAccum.Clone
                    grdAccumDecum.DataSource = dtAccum
                    grdAccumDecum.DataBind()
            End Select
            Session.Add("Accum_Decum", dtAccum)
        Catch ex As Exception
            lblerror.Text = "fill_Accum_Decum_Grid:Error occurred in filling grid."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_Accum_Decum_Grid", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub fill_OrderGrid()
        Dim dt As DataTable
        Dim strPrdCode As String '<AvinashG. on 25-Jan-2016: EQBOSDEV-233 ELN/DRA/FCN/ACCU/DECU in separate tabs >
        Try
            'Mohit Lalwani on 22-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            'Mohit Lalwani on 22-Aug-2016
            EnableTimerTick()
            Dim strSchemeName As String = ""
            strSchemeName = "Accum/Decum"
            dt = New DataTable("DUMMY")
            strPrdCode = ddlAccumType.SelectedItem.Value
            Dim strMode As String = If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "SELF", "SELF", If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "GROUP", "GRP", "ALL"))
            '<AvinashG. on 25-Jan-2016: EQBOSDEV-233 ELN/DRA/FCN/ACCU/DECU in separate tabs >
            Select Case objELNRFQ.Get_ProductBased_Order_History("ELN", strSchemeName, LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, strPrdCode, dt)
                'Select Case objELNRFQ.DB_Get_Order_History("ELN", strSchemeName, LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, dt)
                '</AvinashG. on 25-Jan-2016: EQBOSDEV-233 ELN/DRA/FCN/ACCU/DECU in separate tabs >
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

    Private Sub GetCommentary_Accum()
        Dim strAccumCommentary As StringBuilder = New StringBuilder()
        Dim sLeverage As String
        Dim sbMailComm As StringBuilder = New StringBuilder()
        Dim sbComm As StringBuilder
        sbComm = New StringBuilder()
        Dim strTempJS As StringBuilder
        strTempJS = New StringBuilder()

        Dim SCBText As StringBuilder '<RiddhiS. on 31-Aug-2016: SCB Format email>
        SCBText = New StringBuilder()
        Dim bestLP As String
        Dim PriceOrStrikeMail As String
        Dim IssuerMail As String
        Dim IssuePriceMail As String
        Dim MRating, SPRating, FRating As String
        Dim lst As ListItem

        Try
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allowed_QuoteMailing", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"

                    sbMailComm = New StringBuilder()
                    lblMailComentry.Text = ""
		     ''Rushikesh 14Jan2016:- For Share load on lazyload change item to TEXT and value
                    If ddlShareAccumDecum.SelectedValue.ToString.Trim <> "" Then
                        sbComm.AppendLine("<TABLE><TR><TD COLSPAN=""1""><span class='fieldInfo'>!</span></TD><TD  COLSPAN=""2"" style=""font-size:12px;"">RFQ Details (" + ddlAccumType.SelectedItem.Text + ")</TD></TR>")




                        Dim tempCounterRFQDetails As Integer
                        tempCounterRFQDetails = 1


                        sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Tenor</TD><TD style='padding-left: 19px;'> " + txtTenorAccumDecum.Text + ddlTenorTypeAccum.SelectedItem.Text.Substring(0, 1) + "</TD></TR>")
                        tempCounterRFQDetails = tempCounterRFQDetails + 1

                        sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Underlying</TD><TD style='padding-left: 19px;'> " + ddlShareAccumDecum.Text + "</TD></TR>")
                        tempCounterRFQDetails = tempCounterRFQDetails + 1

                        sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Strike</TD><TD style='padding-left: 19px;'> " + If(ddlSolveForAccumDecum.SelectedValue.ToUpper = "STRIKE", " " + hdnBestStrike.Value + " ", FormatNumber(Val(txtStrikeaccum.Text.Replace(",", "")), 2)) + "% of Spot" + "</TD></TR>") ''<Nikhil M. on 20-Sep-2016: Added>
                        tempCounterRFQDetails = tempCounterRFQDetails + 1

                        sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>KO</TD><TD style='padding-left: 19px;'> " + FormatNumber(Val(txtKO.Text.Replace(",", "")), 2) + "% of Spot" + "</TD></TR>")
                        tempCounterRFQDetails = tempCounterRFQDetails + 1

                        sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Upfront</TD><TD style='padding-left: 19px;'> " + If(ddlSolveForAccumDecum.SelectedValue.ToUpper = "UPFRONT", hdnBestStrike.Value, FormatNumber(Val(txtUpfront.Text.Replace(",", "")), 2)) + "%" + "</TD></TR>") ''<Nikhil M. on 23-Sep-2016: Changed>
                        tempCounterRFQDetails = tempCounterRFQDetails + 1

                        ''< Start : Nikhil M. on 02-Sep-2016: FOR FSD Changes>
                        If txtAccumOrderqty.Text <> "" Then
                            sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Daily Shares</TD><TD style='padding-left: 19px;'> " + If(txtAccumOrderqty.Text.Replace(",", "") = "", "0", txtAccumOrderqty.Text) + " shares / day" + "</TD></TR>")
                        Else
                            sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Daily Shares</TD><TD style='padding-left: 19px;'> " + txtAccumOrderqty.Text + " shares / day" + "</TD></TR>")
                        End If
                        ''< End : Nikhil M. on 02-Sep-2016: FOR FSD Changes>

                        sbComm.AppendLine("</TABLE>")
                        ''sbMailComm.Append(txtTenorAccumDecum.Text + ddlTenorTypeAccum.SelectedItem.Text.Substring(0, 1) + ", ")
                        ''sbMailComm.Append(ddlFrequencyAccumDecum.SelectedItem.Text + " Sttld, " + If(chkLeverageRatio.Checked, "2x Geared ", "Normal "))
                        ''sbMailComm.Append(ddlAccumType.SelectedItem.Text + " with " + If(ddlAccumGUDuration.SelectedValue.ToString = "0", "NO", ddlAccumGUDuration.SelectedValue.ToString) _
                        ''                  + " Guarantee on " + ddlShareAccumDecum.Text).AppendLine("#")  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                        ''sbMailComm.AppendLine("#")
                        ''sbMailComm.Append("Strike           = " + If(ddlSolveForAccumDecum.SelectedValue.ToUpper = "STRIKE", "[  ]", FormatNumber(Val(txtStrikeaccum.Text.Replace(",", "")), 2)) + "% of Spot")
                        ''sbMailComm.Append(" ; KO     = " + FormatNumber(Val(txtKO.Text.Replace(",", "")), 2) + "% of Spot").AppendLine()
                        ''sbMailComm.AppendLine("Upfront        = " + If(ddlSolveForAccumDecum.SelectedValue.ToUpper = "UPFRONT", "[  ]", FormatNumber(Val(txtUpfront.Text.Replace(",", "")), 2)) + "%" + "#")
                        ''sbMailComm.AppendLine("#")
                        ''sbMailComm.AppendLine("Daily Shares = " + If(CInt(txtAccumOrderqty.Text.Replace(",", "")) = 0, "0", txtAccumOrderqty.Text) + " shares / day" + "#")

                        ''lblMailComentry.Text = sbMailComm.ToString

                        ' ''<RiddhiS. on 31-Aug-2016: SCB temp email format till confirmation arrives>

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



                        ''SCBText.Append(txtTenorAccumDecum.Text + ddlTenorTypeAccum.SelectedItem.Text.Substring(0, 1) + ", ")
                        ''SCBText.Append(ddlFrequencyAccumDecum.SelectedItem.Text + " Sttld, " + If(chkLeverageRatio.Checked, "2x Geared ", "Normal "))
                        ''SCBText.Append(ddlAccumType.SelectedItem.Text + " with " + If(ddlAccumGUDuration.SelectedValue.ToString = "0", "NO", ddlAccumGUDuration.SelectedValue.ToString) _
                        ''                  + " Guarantee on " + ddlShareAccumDecum.Text).AppendLine("#")  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                        ''SCBText.AppendLine("#")

                        ''SCBText.AppendLine("Reference Stock : " + ddlShareAccumDecum.Text) ''Reference Stock
                        ''SCBText.AppendLine("Stock Code : " + ddlShareAccumDecum.SelectedValue) ''Stock Code
                        ''SCBText.AppendLine("Stock Currency : " + lblAQDQBaseCcy.Text) ''Stock Ccy
                        ''SCBText.Append("Tenor : " + txtTenorAccumDecum.Text + ddlTenorTypeAccum.SelectedItem.Text.Substring(0, 1)) ''Tenor
                        ''SCBText.AppendLine("Spot : " + lblSpotValue.Text) ''Spot
                        ''SCBText.Append("Strike : " + If(ddlSolveForAccumDecum.SelectedValue.ToUpper = "STRIKE", IssuePriceMail, FormatNumber(Val(txtStrikeaccum.Text.Replace(",", "")), 2))).AppendLine("#") ''Strike
                        ''SCBText.Append("KO (%) : " + FormatNumber(Val(txtKO.Text.Replace(",", "")), 2)).AppendLine("#") ''KO (%)
                        ''SCBText.Append("KO Type : " + Label1.Text).AppendLine("#") ''KO Type
                        ''SCBText.AppendLine("Frequency : " + ddlFrequencyAccumDecum.SelectedItem.Text) ''Frequency
                        ''SCBText.AppendLine("Guarantee : " + ddlAccumGUDuration.SelectedValue.ToString) ''Guarantee
                        ''SCBText.AppendLine("Upfront : " + If(ddlSolveForAccumDecum.SelectedValue.ToUpper = "UPFRONT", IssuePriceMail, FormatNumber(Val(txtUpfront.Text.Replace(",", "")), 2)) + "%") ''Upfront
                        ''SCBText.AppendLine("Is Gearing Leverage : " + If(chkLeverageRatio.Checked, "YES", "NO ")) ''Gearing Leverage
                        ''SCBText.AppendLine("Daily Shares : " + If(CInt(txtAccumOrderqty.Text.Replace(",", "")) = 0, "0", txtAccumOrderqty.Text) + " shares / day" + "#") ''Daily Shares
                        ''SCBText.AppendLine("No. of Days : " + lblEstimatedNoOfDays.Text) ''No. of Days
                        ''SCBText.AppendLine("Notional : " + lblEstimatedNotional.Text) ''Notional
                        ''SCBText.AppendLine("Issuer :  " + IssuerMail) ''Issuer
                        ''SCBText.AppendLine("Issuer Price :  " + IssuePriceMail) ''Issuer Price
                        ''SCBText.AppendLine("Client Price :  " + PriceOrStrikeMail) ''Client Price
                        ''SCBText.AppendLine("Moody's Rating : " + MRating) ''Moody's Rating
                        ''SCBText.AppendLine("Standard and Poor's Rating : " + SPRating) ''S&P Rating
                        ''SCBText.AppendLine("Fitch Rating : " + FRating) ''Fitch Rating

                        ''lblMailComentry.Text = SCBText.ToString
                        '</RiddhiS.>

                        '<RiddhiS. on 06-Sep-2016: for sending mail to selected Price providers>
                        SCBText.Append(txtTenorAccumDecum.Text + ddlTenorTypeAccum.SelectedItem.Text.Substring(0, 1) + ", ")
                        SCBText.Append(ddlFrequencyAccumDecum.SelectedItem.Text + " Sttld, " + If(chkLeverageRatio.Checked, "2x Geared ", "Normal "))
                        SCBText.Append(ddlAccumType.SelectedItem.Text + " with " + If(ddlAccumGUDuration.SelectedValue.ToString = "0", "NO", ddlAccumGUDuration.SelectedValue.ToString) _
                                          + " Guarantee on " + ddlShareAccumDecum.Text).AppendLine("#")  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                        SCBText.AppendLine("#")

                        SCBText.AppendLine("Reference Stock : " + ddlShareAccumDecum.Text) ''Reference Stock
                        SCBText.AppendLine("Stock Code : " + ddlShareAccumDecum.SelectedValue) ''Stock Code
                        SCBText.AppendLine("Stock Currency : " + lblAQDQBaseCcy.Text) ''Stock Ccy
                        SCBText.Append("Tenor : " + txtTenorAccumDecum.Text + ddlTenorTypeAccum.SelectedItem.Text.Substring(0, 1)) ''Tenor
                        SCBText.AppendLine("Spot : " + lblSpotValue.Text) ''Spot
                        SCBText.Append("KO (%) : " + FormatNumber(Val(txtKO.Text.Replace(",", "")), 2)).AppendLine("#") ''KO (%)
                        SCBText.Append("KO Type : " + Label1.Text).AppendLine("#") ''KO Type
                        SCBText.AppendLine("Frequency : " + ddlFrequencyAccumDecum.SelectedItem.Text) ''Frequency
                        SCBText.AppendLine("Guarantee : " + ddlAccumGUDuration.SelectedValue.ToString) ''Guarantee
                        SCBText.AppendLine("Is Gearing Leverage : " + If(chkLeverageRatio.Checked, "YES", "NO ")) ''Gearing Leverage
                        SCBText.AppendLine("Daily Shares : " + txtAccumOrderqty.Text + " shares / day" + "#") ''Daily Shares
                        SCBText.AppendLine("No. of Days : " + lblEstimatedNoOfDays.Text) ''No. of Days
                        SCBText.AppendLine("Notional : " + lblEstimatedNotional.Text) ''Notional

                        
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
                                SCBText.AppendLine("Issuer :  " + IssuerMail) ''Issuer
                                SCBText.AppendLine("#")
                                SCBText.Append("Strike : " + If(ddlSolveForAccumDecum.SelectedValue.ToUpper = "STRIKE", IssuePriceMail, FormatNumber(Val(txtStrikeaccum.Text.Replace(",", "")), 2))).AppendLine("#") ''Strike
                                SCBText.AppendLine("Upfront : " + If(ddlSolveForAccumDecum.SelectedValue.ToUpper = "UPFRONT", IssuePriceMail, FormatNumber(Val(txtUpfront.Text.Replace(",", "")), 2)) + "%") ''Upfront
                       ' ''< Start : Nikhil M. on 02-Sep-2016: FOR FSD Changes>
                       ' If txtAccumOrderqty.Text <> "" Then
                       '     SCBText.AppendLine("Daily Shares : " + If(CInt(txtAccumOrderqty.Text.Replace(",", "")) = 0, "0", txtAccumOrderqty.Text) + " shares / day" + "#") ''Daily Shares
                       ' Else
                       '     SCBText.AppendLine("Daily Shares : " + txtAccumOrderqty.Text + " shares / day" + "#") ''Daily Shares
                       ' End If
                       ' ''< Start : Nikhil M. on 02-Sep-2016: FOR FSD Changes>
                                SCBText.AppendLine("Issuer Price :  " + IssuePriceMail) ''Issuer Price
                                SCBText.AppendLine("Client Price :  " + PriceOrStrikeMail) ''Client Price
                                SCBText.AppendLine("Moody's Rating : " + MRating) ''Moody's Rating
                                SCBText.AppendLine("Standard and Poor's Rating : " + SPRating) ''S&P Rating
                                SCBText.AppendLine("Fitch Rating : " + FRating) ''Fitch Rating

                            End If
                        Next

                        lblMailComentry.Text = SCBText.ToString
                        '</RiddhiS. on 06-Sep-2016>

                    Else
                    End If
                Case "N", "NO"
            End Select
	     ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            If ddlShareAccumDecum.SelectedValue Is Nothing Then
                lblComentry2.Text = ""
                ''btnMail.Visible = False
                strTempJS.AppendLine("try{ hideEmail(); } catch(e){ }")
            ElseIf (tabContainer.ActiveTabIndex = prdTab.Acc And ddlShareAccumDecum.SelectedValue = "") Or (tabContainer.ActiveTabIndex = prdTab.Dec And ddlShareAccumDecum.SelectedValue = "") Then
                lblComentry2.Text = ""
                ''btnMail.Visible = False
                strTempJS.AppendLine("try{ hideEmail(); } catch(e){ }")
            Else
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allowed_QuoteMailing", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"

                        'Avinash/Shekar:-Hide/Show mailing button
                        strTempJS.AppendLine("try{ showEmail(); } catch(e){ }")
                    Case "N", "NO"
                        'Do nothing'
                End Select
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Display_MailText_As_Narration", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        ''<Avinash 26Nov2015:- display in vertical format>
                        ''lblComentry2.Text = If(lblMailComentry.Text.Trim = "", "", _
                        ''                              lblMailComentry.Text.Remove(lblMailComentry.Text.LastIndexOf("#"), 1).Replace("##", "#").Replace("#" + Environment.NewLine, "; ").Replace("; ;", ";")) '<AvinashG. on 15-Sep-2015: Replace unwanted characters from display>
                        lblComentry2.Text = sbComm.ToString()
                    Case "N", "NO"
                        SchemeName = Convert.ToString(Session("Scheme"))
                        strAccumCommentary = New StringBuilder
                        If chkLeverageRatio.Checked Then
                            sLeverage = " with 2x leverage "
                        Else
                            sLeverage = " with NO leverage "
                        End If
                        Select Case ddlFrequencyAccumDecum.SelectedItem.Text.ToUpper.Trim
                            Case "WEEKLY"
                                ddldurationAccum.SelectedValue = "WEEK"
                                txtDuration.Text = ddlAccumGUDuration.SelectedValue
                            Case "BI-WEEKLY"
                                ddldurationAccum.SelectedValue = "WEEK"
                                txtDuration.Text = CStr(2 * Val(ddlAccumGUDuration.SelectedValue))
                            Case "MONTHLY"
                                ddldurationAccum.SelectedValue = "MONTH"
                                txtDuration.Text = ddlAccumGUDuration.SelectedValue
                        End Select
                        If ddlAccumType.SelectedValue = "ACCUMULATOR" Then
                            If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                strAccumCommentary.Append("You are buying " & txtAccumOrderqty.Text & " quantity of " & ddlShareAccumDecum.SelectedValue & sLeverage & _
                             " with " & ddlFrequencyAccumDecum.SelectedItem.Text.Substring(0, 1).ToUpper + ddlFrequencyAccumDecum.SelectedItem.Text.Substring(1).ToLower & _
                             " frequency for tenor of " & txtTenorAccumDecum.Text & " " & ddlTenorTypeAccum.SelectedValue.Substring(0, 1).ToUpper + ddlTenorTypeAccum.SelectedValue.Substring(1).ToLower & _
                             " and  Guarantee as " & txtDuration.Text & " " & ddldurationAccum.SelectedValue.Substring(0, 1).ToUpper + ddldurationAccum.SelectedValue.Substring(1).ToLower & _
                          " , upfront with " & txtUpfront.Text & " % and KO as " & txtKO.Text & " %.")

                            Else
                                strAccumCommentary.Append("You are buying " & txtAccumOrderqty.Text & " quantity of " & ddlShareAccumDecum.SelectedValue & sLeverage & _
                             " with " & ddlFrequencyAccumDecum.SelectedItem.Text.Substring(0, 1).ToUpper + ddlFrequencyAccumDecum.SelectedItem.Text.Substring(1).ToLower & _
                            " frequency for tenor of " & txtTenorAccumDecum.Text & " " & ddlTenorTypeAccum.SelectedValue.Substring(0, 1).ToUpper + ddlTenorTypeAccum.SelectedValue.Substring(1).ToLower & _
                            " and  Guarantee as " & txtDuration.Text & " " & ddldurationAccum.SelectedValue.Substring(0, 1).ToUpper + ddldurationAccum.SelectedValue.Substring(1).ToLower & _
                        " , strike with " & txtStrikeaccum.Text & " % and KO as " & txtKO.Text & " %.")

                            End If
                            lblComentry2.Text = strAccumCommentary.ToString
                        Else
                            If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                strAccumCommentary.Append("You are selling " & txtAccumOrderqty.Text & " quantity of " & ddlShareAccumDecum.SelectedValue & sLeverage & _
                             " with " & ddlFrequencyAccumDecum.SelectedItem.Text.Substring(0, 1).ToUpper + ddlFrequencyAccumDecum.SelectedItem.Text.Substring(1).ToLower & _
                             " frequency for tenor of " & txtTenorAccumDecum.Text & " " & ddlTenorTypeAccum.SelectedValue.Substring(0, 1).ToUpper + ddlTenorTypeAccum.SelectedValue.Substring(1).ToLower & _
                             " and  Guarantee as " & txtDuration.Text & " " & ddldurationAccum.SelectedValue.Substring(0, 1).ToUpper + ddldurationAccum.SelectedValue.Substring(1).ToLower & _
                            " , upfront with " & txtUpfront.Text & " % and KO as " & txtKO.Text & " %.")    ''<Rutuja 22April:Changes bps to %,told by Sarun %>
                            Else
                                strAccumCommentary.Append("You are selling " & txtAccumOrderqty.Text & " quantity of " & ddlShareAccumDecum.SelectedValue & sLeverage & _
                            " with " & ddlFrequencyAccumDecum.SelectedItem.Text.Substring(0, 1).ToUpper + ddlFrequencyAccumDecum.SelectedItem.Text.Substring(1).ToLower & _
                            " frequency for tenor of " & txtTenorAccumDecum.Text & " " & ddlTenorTypeAccum.SelectedValue.Substring(0, 1).ToUpper + ddlTenorTypeAccum.SelectedValue.Substring(1).ToLower & _
                            " and  Guarantee as " & txtDuration.Text & " " & ddldurationAccum.SelectedValue.Substring(0, 1).ToUpper + ddldurationAccum.SelectedValue.Substring(1).ToLower & _
                       " , strike with " & txtStrikeaccum.Text & " % and KO as " & txtKO.Text & " %.")

                            End If
                            lblComentry2.Text = strAccumCommentary.ToString
                        End If
                End Select
            End If
            pnlReprice.Update()
            upnlCommentry.Update()
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Accdecc1", strTempJS.ToString, True)
        Catch ex As Exception
            lblerror.Text = "GetCommentary_Accum:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "GetCommentary_Accum", ErrorLevel.High)

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
        ProductType = "Accum/Decum"
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
        ProductType = "Accum/Decum"
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
        Chart1.Visible = True
        Dim strProductType As String
        strProductType = "Accum/Decum"
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
            Chart1.Visible = True
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
            Chart.Legends(0).Enabled = False
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
                    Case "OCBC"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.OCBC
                    Case "CITI"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.CITI
                    Case "LEONTEQ"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.LEONTEQ
                    Case "COMMERZ"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.COMMERZ
                    Case "CS"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.CS
                    Case "BNPP"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.BNPP
                    Case "UBS"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.UBS
                    Case "DB"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.DBIB
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
            Chart.BackColor = colorBackGround
            Chart.BackGradientStyle = _ChartBackGradientStyle
            Chart.BorderlineDashStyle = _ChartBorderlineDashStyle
            Chart.BorderSkin.SkinStyle = _ChartBorderSkinStyle
            Chart.ChartAreas("Default").BackColor = colorBackGround
            Chart.ChartAreas("Default").BackGradientStyle = GradientStyle.None
            Chart.ChartAreas("Default").InnerPlotPosition.X = 30
            Chart.ChartAreas("Default").InnerPlotPosition.Y = 10
            Chart.ChartAreas("Default").AxisX.TitleFont = New Font("arial", 7.0F, FontStyle.Regular)
            Chart.ChartAreas("Default").AxisY.TitleFont = New Font("arial", 7.0F, FontStyle.Regular)
            Chart.ChartAreas("Default").InnerPlotPosition.Height = 80
            Chart.ChartAreas("Default").InnerPlotPosition.Width = 50 ''55
            Chart.ChartAreas("Default").InnerPlotPosition.Auto = False
            Chart.Legends("Default").Enabled = True
            Chart.Legends(0).Docking = _DockingLocation
            Dim dChartType As System.Web.UI.DataVisualization.Charting.SeriesChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut
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
                        Chart.Series(0).Points(i).Color = Me.structChartColors.HSBC
                    Case "CITI"                                                                             'Mohit Lalwani on 29-Jan-2016
                        Chart.Series(0).Points(i).Color = Me.structChartColors.CITI
                    Case "LEONTEQ"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.LEONTEQ
                    Case "COMMERZ"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.COMMERZ
                    Case "OCBC"                                                                                                 'Mohit Lalwani on 29-Jan-2016
                        Chart.Series(0).Points(i).Color = Me.structChartColors.OCBC
                    Case "CS"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.CS
                    Case "BNPP"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.BNPP
                    Case "UBS"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.UBS
                    Case "DB"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.DBIB
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

    Public Sub clearFields()
        Try
            If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                txtStrikeaccum.Text = "0.00"
                txtStrikeaccum.Enabled = False
                txtUpfront.Enabled = True
            Else
                txtUpfront.Text = "0.00"
                txtUpfront.Enabled = False
                txtStrikeaccum.Enabled = True
            End If
            upnl3.Update()
        Catch ex As Exception
            lblerror.Text = "clearFields:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "clearFields", ErrorLevel.High)
            Throw ex
        End Try
    End Sub


    Public Sub ddlExchangeAccumDecum_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExchangeAccumDecum.SelectedIndexChanged
        Dim dtBaseCCY As DataTable
        Try
            dtBaseCCY = New DataTable("Dummy")
            lblerror.Text = ""
            clearFields()

            With ddlShareAccumDecum
                .Items.Clear()
                .Text = ""
            End With


            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            If ddlShareAccumDecum.SelectedValue IsNot Nothing And ddlShareAccumDecum.SelectedItem IsNot Nothing Then
                ''Fill_Accum_ddl_Share()  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                getCurrency(ddlShareAccumDecum.SelectedValue.ToString)
                manageShareReportShowHide()

                ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                DisplayEstimatedNotional()
            End If
            ''GetCommentary_Accum()
            lblComentry2.Text = ""
            pnlReprice.Update()
            upnlCommentry.Update()
            ResetAll()
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>


        Catch ex As Exception
            lblerror.Text = "ddlExchangeAccumDecum_SelectedIndexChanged:Error occured in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlExchangeAccumDecum_SelectedIndexChanged", ErrorLevel.High)
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
                    lblAQDQBaseCcy.Text = sShareCcy
                    sCcy = sShareCcy
                Else
                    dtBaseCCY = New DataTable("Dummy")
                    Select Case objELNRFQ.DB_GetBASECCY(Share, dtBaseCCY)
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                            lblAQDQBaseCcy.Text = dtBaseCCY.Rows(0)(0).ToString
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
    Private Sub txtUpfront_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUpfront.TextChanged
        Try
            lblerror.Text = ""
            clearFields()
            txtUpfront.Text = SetNumberFormat(txtUpfront.Text, 2)
            '  GetCommentary_Accum()
            ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtUpfront_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtUpfront_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub DisplayEstimatedNotional()
        Try
            Dim EqPair As String = ddlShareAccumDecum.SelectedValue.ToString & " - " & lblAQDQBaseCcy.Text
            Dim BidRate As Double = objELNRFQ.GetShareRate(EqPair, BidRate)
            Dim iAccDays As Integer
            If BidRate = 0 Or BidRate = Nothing Then
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAQDQ_Estimated_Notional", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        lblerror.Text = "Cannot calculate estimated notional. Rate for selected share not found."
                        lblEstimatedNotional.Text = ""
                        lblEstimatedNoOfDays.Text = ""
                        lblEstimatedGearedShares.Text = ""      'Added by ChitralekhaM on 07-Sep-16
                        lblEstimatedUngearedShares.Text = ""    'Added by ChitralekhaM on 07-Sep-16
                        lblNotionalWithCcy.Text = "Notional:"
                    Case "N", "NO"
                End Select
            Else
                ' Added Geared and Ungeared Shares by Chitralekha on 8-Spt-2016
                Dim EstimatedNotional As Double
                Dim NoOfDays As Long
                Dim sExchange As String = ""
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        sExchange = lblDisplayExchangeAccumDecumVal.Text.Split(CChar("-"))(0).Trim
                    Case "N", "NO"
                        sExchange = ddlExchangeAccumDecum.SelectedItem.Value
                End Select
                Select Case objELNRFQ.DB_Get_NoOfDaysAccrual(sExchange, CInt(txtTenorAccumDecum.Text), CChar(ddlTenorTypeAccum.SelectedValue), NoOfDays)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        If NoOfDays < 0 Then
                            lblEstimatedNotional.Text = ""
                            lblEstimatedNoOfDays.Text = ""
                            lblEstimatedGearedShares.Text = ""
                            lblEstimatedUngearedShares.Text = ""
                        Else
                            EstimatedNotional = NoOfDays * CLng(txtAccumOrderqty.Text) * BidRate
                            lblEstimatedNoOfDays.Text = CStr(NoOfDays)
                            lblEstimatedNotional.Text = SetNumberFormat(CStr(EstimatedNotional), 0) ''EQBOSDEV-228  Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                           lblEstimatedUngearedShares.Text = SetNumberFormat(CStr(CLng(txtAccumOrderqty.Text) * NoOfDays), 0)  'Added by ChitralekhaM on 07-Sep-16''<Nikhil M. on 30-Sep-2016: Changed>

                            ''<Start |Nikhil M. on 30-Sep-2016: Added to checking Gearing checked or not>
                            If chkLeverageRatio.Checked Then
                                lblEstimatedGearedShares.Text = SetNumberFormat(CStr(CLng(txtAccumOrderqty.Text) * NoOfDays * 2), 0) 'Added by ChitralekhaM on 07-Sep-16 ''<Nikhil M. on 30-Sep-2016: Changed>
                            Else
                                lblEstimatedGearedShares.Text = "-"
                            End If
                            ''<Start |Nikhil M. on 30-Sep-2016: Added to check Gearing checked or not>
                        End If
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                        lblEstimatedNotional.Text = ""
                        lblEstimatedNoOfDays.Text = ""
                        lblEstimatedGearedShares.Text = ""
                        lblEstimatedGearedShares.Text = ""
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                        lblEstimatedNotional.Text = ""
                        lblEstimatedNoOfDays.Text = ""
                        lblEstimatedGearedShares.Text = ""
                        lblEstimatedUngearedShares.Text = ""
                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                        lblEstimatedNotional.Text = ""
                        lblEstimatedNoOfDays.Text = ""
                        lblEstimatedGearedShares.Text = ""
                        lblEstimatedUngearedShares.Text = ""
                End Select
                lblNotionalWithCcy.Text = "Notional (<font style='font-weight:bold;'>" & lblAQDQBaseCcy.Text & "</font>):"
                lblerror.Text = ""
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         "FinIQWebApp/ELN_DealEntry/ELN_RFQ.aspx.vb", "DisplayEstimatedNotional", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txtAccumOrderqty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAccumOrderqty.TextChanged
        Dim strcount As Integer = 0
        Dim sQty As Integer = 0
        Try
            lblerror.Text = ""
            clearFields()
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            ''<Start: Nikhil M. on 02-Sep-2016: FSD DefaultValue Changes >
            ' If txtAccumOrderqty.Text = "" Then
            'txtAccumOrderqty.Text = "0"
            ' End If
            ''<End: Nikhil M. on 02-Sep-2016: FSD DefaultValue Changes >
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>

            ''<Start | Nikhil M. on 02-Sep-2016: >
            If txtAccumOrderqty.Text = "" Then
                clearShareData()
                lblComentry2.Text = ""
                lblerror.Text = "Please enter valid Daily no.of shares."
                upnlCommentry.Update()
                lblEstimatedNotional.Text = ""          'Added by ChitralekhaM on 12-Oct-16
                lblEstimatedNoOfDays.Text = ""          'Added by ChitralekhaM on 12-Oct-16
                lblEstimatedGearedShares.Text = ""      'Added by ChitralekhaM on 12-Oct-16
                lblEstimatedUngearedShares.Text = ""    'Added by ChitralekhaM on 12-Oct-16
                Exit Sub
            End If
            ''<ENd | Nikhil M. on 02-Sep-2016: >
            If txtTenorAccumDecum.Text = "0" Or txtTenorAccumDecum.Text = "" Or Val(txtAccumOrderqty.Text) = 0 Or txtAccumOrderqty.Text = "" Then ''<Nikhil M. Added Condition "Or txtAccumOrderqty.Text = "" "  on 02-Sep-2016: FSD Default Value Changes >
                lblEstimatedNotional.Text = ""
                lblEstimatedNoOfDays.Text = ""
                lblEstimatedGearedShares.Text = ""      'Added by ChitralekhaM on 07-Sep-16
                lblEstimatedUngearedShares.Text = ""    'Added by ChitralekhaM on 07-Sep-16
            Else
                'getCurrency(ddlShareAccumDecum.SelectedValue) ''<Nikhil M. on 02-Sep-2016: >
                DisplayEstimatedNotional()
            End If
            If Qty_Validate(txtAccumOrderqty.Text) = False Then
                Exit Sub
            End If
            Try
                txtAccumOrderqty.Text = FinIQApp_Number.ConvertFormattedAmountToNumber(txtAccumOrderqty.Text).ToString
                txtAccumOrderqty.Text = SetNumberFormat(txtAccumOrderqty.Text, 0)  ''EQBOSDEV-228  Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
            Catch ex As Exception
                lblerror.Text = "Please enter valid Daily no.of shares."
            End Try
            ' GetCommentary_Accum()
            ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtAccumOrderqty_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtAccumOrderqty_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub ddlSolveForAccumDecum_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSolveForAccumDecum.SelectedIndexChanged
        Try
            lblerror.Text = ""
            clearFields()
            If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                txtStrikeaccum.Text = ""
                txtUpfront.Text = ""
                txtStrikeaccum.Enabled = False
                txtUpfront.Enabled = True
            Else
                txtUpfront.Text = ""
                txtStrikeaccum.Text = ""
                txtUpfront.Enabled = False
                txtStrikeaccum.Enabled = True

            End If
            If rbHistory.SelectedValue = "Order History" Then
                rbHistory.SelectedValue = "Quote History"
                fill_Accum_Decum_Grid()
                upnl3.Update()
            End If
            lblSolveForType.Text = ddlSolveForAccumDecum.SelectedItem.Text
            ' GetCommentary_Accum()
            ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
            ResetAll()
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
        Catch ex As Exception
            lblerror.Text = "ddlSolveForAccumDecum_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlSolveForAccumDecum_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub



    Private Sub ddlShareAccumDecum_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShareAccumDecum.SelectedIndexChanged
        Try
	 ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            If ddlShareAccumDecum.SelectedValue Is Nothing Then
                clearShareData()
                ddlShareAccumDecum.Text = "Please select valid share."
                lblDisplayExchangeAccumDecumVal.Text = ""
                Exit Sub
            ElseIf ddlShareAccumDecum.SelectedValue = "" Then
                clearShareData()
                ddlShareAccumDecum.Text = "Please select valid share."
                lblDisplayExchangeAccumDecumVal.Text = ""
                Exit Sub
           '     ''<Start : Nikhil M. on 02-Sep-2016: FSD Changes >
           ' ElseIf txtAccumOrderqty.Text = "" Then
           '    clearShareData()
           '     lblerror.Text = "Invalid No. of Share"
           '     Exit Sub
           '     ''<End  : Nikhil M. on 02-Sep-2016: FSD Changes >
            Else
                lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                lblerror.Text = ""
                clearFields()
                manageShareReportShowHide()
                getCurrency(ddlShareAccumDecum.SelectedValue)
                getPRR(ddlShareAccumDecum.SelectedValue.ToString)
                getFlag(ddlShareAccumDecum.SelectedValue.ToString)
                '  GetCommentary_Accum()
                ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
                ResetAll()
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                'getRange()
                '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                '<AvinashG. on 05-Jan-2016: Conditional call to function>
                If txtTenorAccumDecum.Text = "0" Or txtTenorAccumDecum.Text = "" Or Val(txtAccumOrderqty.Text.Replace(",", "")) = 0 Then
                    lblEstimatedNotional.Text = ""
                    lblEstimatedNoOfDays.Text = ""
                    lblEstimatedGearedShares.Text = ""      'Added by ChitralekhaM on 07-Sep-16
                    lblEstimatedUngearedShares.Text = ""    'Added by ChitralekhaM on 07-Sep-16
                Else
                    DisplayEstimatedNotional()
                End If
                'DisplayEstimatedNotional()
                '<AvinashG. on 05-Jan-2016: Conditional call to function>
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
            End If
        Catch ex As Exception
            lblerror.Text = "ddlShareAccumDecum_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlShareAccumDecum_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Function chkMultiple(ByVal Number As Integer) As Boolean
        Try
            If Number Mod 3 = 0 Then
                chkMultiple = True
            Else
                lblerror.Text = "Guarantee should be multiple of 3,6.."
                chkMultiple = False
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chkMultiple", ErrorLevel.High)
        End Try
    End Function
    Function OddOrEven(ByVal TheNumber As Integer) As Boolean
        Try
            If TheNumber Mod 2 = 0 Then
                OddOrEven = True
            Else
                lblerror.Text = "Guarantee should be even numbers only."
                OddOrEven = False
            End If
        Catch ex As Exception
            lblerror.Text = "OddOrEven:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "OddOrEven", ErrorLevel.High)
        End Try
    End Function

    Private Sub txtDuration_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDuration.TextChanged
        Try
            clearFields()
            GetCommentary_Accum()
            ResetAll()
            If ddldurationAccum.SelectedValue = "FORTNIGHTLY" Then
                OddOrEven(CInt(txtDuration.Text))
            End If
        Catch ex As Exception
            lblerror.Text = "txtDuration_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "txtDuration_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub chkLeverageRatio_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLeverageRatio.CheckedChanged
        Dim intLeverageRatio As Integer = 0
        Try
            lblerror.Text = ""
            If chkLeverageRatio.Checked = False Then
                lblEstimatedGearedShares.Text = "-" ''<Nikhil M. on 30-Sep-2016: Added for Estimate changes >
                intLeverageRatio = 1
            Else
                intLeverageRatio = 2
                DisplayEstimatedNotional()
                ''<Nikhil M. on 30-Sep-2016: Added for Estimate changes >
            End If
            Session.Add("Leverage_Ratio", intLeverageRatio)
            '  GetCommentary_Accum()
            ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "chkLeverageRatio_CheckedChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chkLeverageRatio_CheckedChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub


    Private Sub txtKO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKO.TextChanged
        Try
            lblerror.Text = ""
            clearFields()
            txtKO.Text = SetNumberFormat(txtKO.Text, 2)
            ' GetCommentary_Accum()
            ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtKO_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtKO_TextChanged", ErrorLevel.High)
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
                makeThisGridVisible(grdAccumDecum)
                fill_Accum_Decum_Grid()
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



    Private Sub ddlAccumType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAccumType.SelectedIndexChanged
        Try
            lblerror.Text = ""
            clearFields()
            GetCommentary_Accum()
            ResetAll()
            Select Case ddlAccumType.SelectedValue.ToUpper
                Case "ACCUMULATOR"
                    txtKO.Text = "105.00"
                Case "DECUMULATOR"
                    txtKO.Text = "95.00"
            End Select
            ''getRange()
        Catch ex As Exception
            lblerror.Text = "ddlAccumType_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlAccumType_SelectedIndexChanged", ErrorLevel.High)
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

    Private Function SetFrequencytype(ByVal sTenorType As String, ByVal sTenorValue As String, ByVal SFrequencyType As String) As Boolean
        Try
            Select Case sTenorType.Trim.ToUpper
                Case "MONTH"
                    Select Case SFrequencyType.Trim.ToUpper
                        Case "QUARTERLY"
                            If CheckMultiple(CInt(Val(sTenorValue)), 3) Then
                                Return True
                            Else
                                Return False
                            End If

                        Case "SEMIANNUALLY"
                            If CheckMultiple(CInt(Val(sTenorValue)), 6) Then
                                Return True
                            Else
                                Return False
                            End If
                        Case "ANNUALLY"
                            If CheckMultiple(CInt(Val(sTenorValue)), 12) Then
                                Return True
                            Else
                                Return False
                            End If

                        Case Else
                            Return True
                    End Select
                Case "WEEK"

                    Select Case SFrequencyType.Trim.ToUpper
                        Case "MONTHLY"
                            If CheckMultiple(CInt(Val(sTenorValue)), 4) Then
                                Return True
                            Else
                                Return False
                            End If

                        Case "QUARTERLY"
                            If CheckMultiple(CInt(Val(sTenorValue)), 12) Then
                                Return True
                            Else
                                Return False
                            End If

                        Case "SEMIANNUALLY"
                            If CheckMultiple(CInt(Val(sTenorValue)), 24) Then
                                Return True
                            Else
                                Return False
                            End If
                        Case "ANNUALLY"
                            If CheckMultiple(CInt(Val(sTenorValue)), 48) Then
                                Return True
                            Else
                                Return False
                            End If

                        Case "FORTNIGHTLY"
                            If CheckMultiple(CInt(Val(sTenorValue)), 2) Then
                                Return True
                            Else
                                Return False
                            End If
                        Case Else
                            Return True
                    End Select
                Case "YEAR"
                    Return True
                Case Else
                    Return False
            End Select
        Catch ex As Exception
            lblerror.Text = "SetFrequencytype:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "SetFrequencyType", ErrorLevel.High)
            Throw ex
        End Try
    End Function

    Private Sub ddlFrequencyAccumDecum_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFrequencyAccumDecum.SelectedIndexChanged
        Dim dtSupportedGuPeriods As DataTable
        Try
            dtSupportedGuPeriods = New DataTable("GuPeriods")
            lblerror.Text = ""
            clearFields()
            ddlAccumGUDuration.Items.Clear()
            Select Case ddlFrequencyAccumDecum.SelectedItem.Text.ToUpper.Trim
                Case "WEEKLY"
                    Select Case WebCommonFunction.DB_Get_Common_Data("EQC_AccDec_WeeklyGuPeriods", dtSupportedGuPeriods)
                        Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                            For Each dr As DataRow In dtSupportedGuPeriods.Select("", "Data_Value ASC")
                                ddlAccumGUDuration.Items.Add(New DropDownListItem(dr("Data_Value").ToString, dr("Data_Value").ToString)) 'Mohit Lalwani on 8-Aug-2016
                            Next

                        Case Else
                            With ddlAccumGUDuration
                                .Items.Add(New DropDownListItem("0", "0"))
                                .Items.Add(New DropDownListItem("1", "1"))
                                .Items.Add(New DropDownListItem("2", "2"))
                                .Items.Add(New DropDownListItem("3", "3"))
                                .Items.Add(New DropDownListItem("4", "4"))
                                .SelectedValue = "1"
                            End With
                    End Select
                Case "BI-WEEKLY"
                    Select Case WebCommonFunction.DB_Get_Common_Data("EQC_AccDec_BiWeeklyGuPeriods", dtSupportedGuPeriods)
                        Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                            For Each dr As DataRow In dtSupportedGuPeriods.Select("", "Data_Value ASC")
                                ddlAccumGUDuration.Items.Add(New DropDownListItem(dr("Data_Value").ToString, dr("Data_Value").ToString)) 'Mohit Lalwani on 8-Aug-2016
                            Next
                        Case Else
                            With ddlAccumGUDuration
                                .Items.Add(New DropDownListItem("0", "0"))
                                .Items.Add(New DropDownListItem("1", "1"))
                                .Items.Add(New DropDownListItem("2", "2"))
                                .SelectedValue = "0"
                            End With
                    End Select

                Case "MONTHLY"
                    Select Case WebCommonFunction.DB_Get_Common_Data("EQC_AccDec_MonthlyGuPeriods", dtSupportedGuPeriods)
                        Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                            For Each dr As DataRow In dtSupportedGuPeriods.Select("", "Data_Value ASC")
                                ddlAccumGUDuration.Items.Add(New DropDownListItem(dr("Data_Value").ToString, dr("Data_Value").ToString)) 'Mohit Lalwani on 8-Aug-2016
                            Next
                        Case Else
                            With ddlAccumGUDuration
                                .Items.Add(New DropDownListItem("0", "0"))
                                .Items.Add(New DropDownListItem("1", "1"))
                                .SelectedValue = "1"
                            End With

                    End Select
            End Select

            If SetFrequencytype(ddlTenorTypeAccum.SelectedValue.Trim.ToUpper, txtTenorAccumDecum.Text.Trim, ddlFrequencyAccumDecum.SelectedValue.Trim) Then
            Else

                lblerror.Text = "Frequency is not valid."
            End If

            '  GetCommentary_Accum()
            ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "ddlFrequencyAccumDecum_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlFrequencyAccumDecum_SelectedIndexChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub txtStrikeaccum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStrikeaccum.TextChanged
        Try
            lblerror.Text = ""
            clearFields()
            txtStrikeaccum.Text = SetNumberFormat(txtStrikeaccum.Text, 2)
            '   GetCommentary_Accum()
            ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtStrikeaccum_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtStrikeaccum_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub



    Private Sub ddldurationAccum_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddldurationAccum.SelectedIndexChanged
        Try
            clearFields()
            GetCommentary_Accum()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "ddldurationAccum_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddldurationAccum_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    'Public Sub stop_timer()
    '    Try
    '        lblMsgPriceProvider.Text = ""
    '        lblerror.Text = ""
    '        lblJPMPrice.Text = ""
    '        lblCSPrice.Text = ""
    '        lblHSBCPrice.Text = ""
    '        lblBNPPPrice.Text = ""
    '        lblUBSPrice.Text = ""
    '        lblMSPrice.Text = ""
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
    '        btnMSPrice.Text = "Price"
    '        btnMSDeal.CssClass = "btnDisabled"
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
    '        If Val(lblTimerMS.Text) > 0 Then
    '            strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerMS.ClientID + "','" + btnMSDeal.ClientID + "');")
    '            btnMSPrice.Enabled = True
    '            btnMSPrice.CssClass = "btn"
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

    Private Sub Stop_timer_Only()
        Try
            Dim strJavaScriptStopTimerOnly As New StringBuilder
            If Val(lblTimerHSBC.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
                btnHSBCPrice.Enabled = True
                btnHSBCPrice.CssClass = "btn"
            End If

            If Val(lblTimerOCBC.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
                btnOCBCPrice.Enabled = True
                btnOCBCPrice.CssClass = "btn"
            End If
            If Val(lblTimerCITI.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
                btnCITIPrice.Enabled = True
                btnCITIPrice.CssClass = "btn"
            End If
            If Val(lblTimerLEONTEQ.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
                btnLEONTEQPrice.Enabled = True
                btnLEONTEQPrice.CssClass = "btn"
            End If
            If Val(lblTimerCOMMERZ.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
                btnCOMMERZPrice.Enabled = True
                btnCOMMERZPrice.CssClass = "btn"
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
            If Val(lblTimerDBIB.Text) > 0 Then
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
                btnDBIBPrice.Enabled = True
                btnDBIBPrice.CssClass = "btn"
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
            btnOCBCPrice.Text = "Price"                    'Mohit Lalwani on 29-Jan-2016
            btnCITIPrice.Text = "Price"                    'Mohit Lalwani on 29-Jan-2016
            btnLEONTEQPrice.Text = "Price"
            btnCOMMERZPrice.Text = "Price"
            btnCSPrice.Text = "Price"
            btnUBSPrice.Text = "Price"
            btnBNPPPrice.Text = "Price"
            btnBAMLPrice.Text = "Price"
            btnDBIBPrice.Text = "Price"
            btnJPMprice.Enabled = True
            btnHSBCPrice.Enabled = True
            btnOCBCPrice.Enabled = True
            btnCITIPrice.Enabled = True
            btnLEONTEQPrice.Enabled = True
            btnCOMMERZPrice.Enabled = True
            btnCSPrice.Enabled = True
            btnUBSPrice.Enabled = True
            btnBNPPPrice.Enabled = True
            btnBAMLPrice.Enabled = True
            btnDBIBPrice.Enabled = True
            btnJPMprice.CssClass = "btn"
            btnHSBCPrice.CssClass = "btn"
            btnOCBCPrice.CssClass = "btn"
            btnCITIPrice.CssClass = "btn"
            btnLEONTEQPrice.CssClass = "btn"
            btnCOMMERZPrice.CssClass = "btn"
            btnCSPrice.CssClass = "btn"
            btnUBSPrice.CssClass = "btn"
            btnBNPPPrice.CssClass = "btn"
            btnBAMLPrice.CssClass = "btn"
            btnDBIBPrice.CssClass = "btn"
            btnJPMDeal.CssClass = "btnDisabled"
            btnHSBCDeal.CssClass = "btnDisabled"
            btnOCBCDeal.CssClass = "btnDisabled"
            btnCITIDeal.CssClass = "btnDisabled"
            btnLEONTEQDeal.CssClass = "btnDisabled"
            btnCOMMERZDeal.CssClass = "btnDisabled"
            btnCSDeal.CssClass = "btnDisabled"
            btnUBSDeal.CssClass = "btnDisabled"
            btnBNPPDeal.CssClass = "btnDisabled"
            btnBAMLDeal.CssClass = "btnDisabled"
            btnDBIBDeal.CssClass = "btnDisabled"
            btnBNPPDeal.Visible = False
            btnBAMLDeal.Visible = False
            btnCSDeal.Visible = False
            btnJPMDeal.Visible = False
            btnHSBCDeal.Visible = False
            btnOCBCDeal.Visible = False                    'Mohit Lalwani on 29-Jan-2016
            btnCITIDeal.Visible = False                    'Mohit Lalwani on 29-Jan-2016
            btnLEONTEQDeal.Visible = False
            btnCOMMERZDeal.Visible = False
            btnUBSDeal.Visible = False
            btnDBIBDeal.Visible = False
            txtAccumOrderqty.Enabled = True
            DealConfirmPopup.Visible = False

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

            UPanle11111.Update()
        Catch ex As Exception
            lblerror.Text = "Enable_Disable_Deal_Buttons:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Enable_Disable_Deal_Buttons", ErrorLevel.High)
            Throw ex

        End Try
    End Sub


#End Region

#Region "Populate XML"

    'Public Sub Get_AccumRFQData_TOXML(ByVal PP_ID As String)
    '    Dim dtQuoteCode As DataTable
    '    Dim strQuoteId As String = String.Empty
    '    Try
    '        dtQuoteCode = New DataTable("Quote")
    '        udtStructured_Product_Tranche = New Structured_Product_Tranche_ELN
    '        strEntityName = LoginInfoGV.Login_Info.EntityName
    '        strEntityId = LoginInfoGV.Login_Info.EntityID.ToString

    '        Select Case objELNRFQ.DB_Get_Quote_ID(dtQuoteCode)
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
    '                ER_QuoteRequestId = dtQuoteCode.Rows(0)(0).ToString
    '                Session.Add("Quote_ID", ER_QuoteRequestId)
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
    '                lblerror.Text = "No record(s) for price provider is found."
    '        End Select

    '        Call setUDTValuesFromForm(udtStructured_Product_Tranche)

    '        If Write_AccumDecum_RFQData_TOXML(PP_ID, ER_QuoteRequestId, LoginInfoGV.Login_Info.LoginId, strEntityId, ddlSolveForAccumDecum.SelectedValue, udtStructured_Product_Tranche, StrnoteRFQXML) = True Then
    '            Select Case objELNRFQ.DB_Insert_IntoAccumDecum_RFQ(CStr(StrnoteRFQXML))
    '                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
    '                    Session.Add("ER_ID", ER_QuoteRequestId)

    '                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful

    '            End Select
    '        End If
    '    Catch ex As Exception
    '        lblerror.Text = "Get_AccumRFQData_TOXML:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "Get_AccumRFQData_TOXML", ErrorLevel.High)
    '        Throw ex
    '    End Try
    'End Sub


    'Public Function Write_AccumDecum_RFQData_TOXML(ByVal PP_ID As String, _
    '                                    ByVal strQuoteId As String, _
    '                                    ByVal strEntityName As String, _
    '                                    ByVal strEntityId As String, _
    '                                    ByVal strAccumType As String, _
    '                                    ByRef udtStructured_Product_Tranche As Structured_Product_Tranche_ELN, _
    '                                    ByRef o_strXMLNote_RFQ As String) As Boolean

    '    Dim strAccumDecumXMLRFQ As StringBuilder
    '    Dim dtQuote As New DataTable
    '    Try
    '        strAccumDecumXMLRFQ = New StringBuilder

    '        With udtStructured_Product_Tranche
    '            Dim sProductDefinition As String = "<AccumDecum xmlns=""http://www.abcdz.com/StructuredProducts/1.0""><Type>" & udtStructured_Product_Tranche.strELNType & "</Type><StrikePercentage>" & udtStructured_Product_Tranche.dblStrike1 & "</StrikePercentage><InterBankPrice Solve=""true"">0.0</InterBankPrice><Underlyings><Underlying><UnderlyingCode Type=""RIC"">" & udtStructured_Product_Tranche.strUnderlyingCode & "</UnderlyingCode></Underlying></Underlyings></AccumDecum>"
    '            strAccumDecumXMLRFQ.Append("<tradeDetails>")
    '            strAccumDecumXMLRFQ.Append("<quoteDetails>")
    '            strAccumDecumXMLRFQ.Append("<ER_PP_ID>" & PP_ID & "</ER_PP_ID>")
    '            strAccumDecumXMLRFQ.Append("<ER_Type>" & udtStructured_Product_Tranche.strELNType & "</ER_Type>")
    '            strAccumDecumXMLRFQ.Append("<ER_StrikePercentage>" & udtStructured_Product_Tranche.dblStrikePrice & "</ER_StrikePercentage> ")
    '            strAccumDecumXMLRFQ.Append("<ER_UnderlyingCode_Type>" & udtStructured_Product_Tranche.strAssetclass & "</ER_UnderlyingCode_Type>")
    '            strAccumDecumXMLRFQ.Append("<ER_UnderlyingCode>" & udtStructured_Product_Tranche.strUnderlyingCode & " </ER_UnderlyingCode>")
    '            strAccumDecumXMLRFQ.Append("<ER_TenorType>" & udtStructured_Product_Tranche.strAccumTenorType & "</ER_TenorType>")
    '            strAccumDecumXMLRFQ.Append("<ER_Tenor>" & udtStructured_Product_Tranche.inttenorAccum & "</ER_Tenor>")
    '            strAccumDecumXMLRFQ.Append("<ER_QuoteRequestId>" & strQuoteId & "</ER_QuoteRequestId>")
    '            strAccumDecumXMLRFQ.Append("<ER_SecurityDescription>Accum/Decum</ER_SecurityDescription>")
    '            strAccumDecumXMLRFQ.Append("<ER_QuoteType>0</ER_QuoteType>")
    '            strAccumDecumXMLRFQ.Append("<ER_BuySell>Buy</ER_BuySell>")
    '            strAccumDecumXMLRFQ.Append("<ER_CashOrderQuantity>" & udtStructured_Product_Tranche.strOrderQty & "</ER_CashOrderQuantity>")
    '            strAccumDecumXMLRFQ.Append("<ER_CashCurrency>" & udtStructured_Product_Tranche.strAQDQCcy & "</ER_CashCurrency>")
    '            strAccumDecumXMLRFQ.Append("<ER_TransactionTime>" & DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff tt") & "</ER_TransactionTime>")
    '            strAccumDecumXMLRFQ.Append("<ER_ProductDefinition>" & sProductDefinition & "</ER_ProductDefinition>")
    '            strAccumDecumXMLRFQ.Append("<ER_Text>2012_QR12</ER_Text>")
    '            strAccumDecumXMLRFQ.Append("<ER_Symbol></ER_Symbol>")
    '            strAccumDecumXMLRFQ.Append("<ER_Created_By>" & strEntityName & "</ER_Created_By>")
    '            strAccumDecumXMLRFQ.Append("<ER_Exchange>" & udtStructured_Product_Tranche.strExchangeAccum & "</ER_Exchange>")
    '            strAccumDecumXMLRFQ.Append("<ER_Quote_Request_YN>Y</ER_Quote_Request_YN>")
    '            strAccumDecumXMLRFQ.Append("<ER_Entity_ID>" & strEntityId & "</ER_Entity_ID>")
    '            strAccumDecumXMLRFQ.Append("<ER_GuaranteedDuration>" & udtStructured_Product_Tranche.strGuaranteedDuration & "</ER_GuaranteedDuration>")
    '            strAccumDecumXMLRFQ.Append("<ER_GuaranteedDurationType>" & udtStructured_Product_Tranche.strGuaranteedDurationType & "</ER_GuaranteedDurationType>")
    '            strAccumDecumXMLRFQ.Append("<ER_KOPercentage>" & Replace(udtStructured_Product_Tranche.strKoPerc, ",", "") & "</ER_KOPercentage>")
    '            strAccumDecumXMLRFQ.Append("<ER_LeverageRatio>" & udtStructured_Product_Tranche.strLeverageratio & "</ER_LeverageRatio>")
    '            strAccumDecumXMLRFQ.Append("<ER_KOSettlement>" & udtStructured_Product_Tranche.strKOType & "</ER_KOSettlement>")
    '            strAccumDecumXMLRFQ.Append("<ER_SolveFor>" & udtStructured_Product_Tranche.strSolveFor & "</ER_SolveFor>")
    '            strAccumDecumXMLRFQ.Append("<ER_Frequency>" & udtStructured_Product_Tranche.strFrequency & "</ER_Frequency>")
    '            strAccumDecumXMLRFQ.Append("<ER_Upfront>" & udtStructured_Product_Tranche.strUpfront & "</ER_Upfront>")
    '            strAccumDecumXMLRFQ.Append("<ER_Template_ID>" & udtStructured_Product_Tranche.lngTemplateId & "</ER_Template_ID>")
    '            If strAccumType = "STRIKE" Then
    '                strAccumDecumXMLRFQ.Append("<EP_Upfront>" & udtStructured_Product_Tranche.strUpfront1 & "</EP_Upfront>")
    '            Else
    '                strAccumDecumXMLRFQ.Append("<EP_StrikePercentage>" & udtStructured_Product_Tranche.dblStrike2 & "</EP_StrikePercentage>")
    '            End If
    '            strAccumDecumXMLRFQ.Append("<ER_EntityName>" & udtStructured_Product_Tranche.strEntityName & "</ER_EntityName>")  ''Entity
    '            strAccumDecumXMLRFQ.Append("<ER_RFQ_RMName>" & udtStructured_Product_Tranche.strRFQRMName & "</ER_RFQ_RMName>")  ''RM   
    '            strAccumDecumXMLRFQ.Append("<ER_EmailId>" & udtStructured_Product_Tranche.strEmailId & "</ER_EmailId>")  ''Emailid   
    '            strAccumDecumXMLRFQ.Append("<ER_Branch>" & udtStructured_Product_Tranche.strBranch & "</ER_Branch>")  ''Branch 
    '            strAccumDecumXMLRFQ.Append("<ER_RFQ_Source>MONOTAB_PRICER</ER_RFQ_Source>") '<AvinashG. on 28-Dec-2015: EQBOSDEV-195 - Add RFQ Source to web application>
    '            strAccumDecumXMLRFQ.Append("</quoteDetails>")
    '            strAccumDecumXMLRFQ.Append("</tradeDetails>")
    '        End With
    '        o_strXMLNote_RFQ = strAccumDecumXMLRFQ.ToString
    '        Write_AccumDecum_RFQData_TOXML = True
    '    Catch ex As Exception
    '        lblerror.Text = "Write_AccumDeccum_RFQData_TOXML:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "Write_AccumDeccum_RFQData_TOXML", ErrorLevel.High)

    '        Throw ex
    '    End Try
    'End Function

    'Private Sub setUDTValuesFromForm(ByRef udtStructured_Product_Tranche As Structured_Product_Tranche_ELN)
    '    Dim strFixingDate As String = String.Empty
    '    Try
    '        udtStructured_Product_Tranche.strELNType = ddlAccumType.SelectedValue
    '        udtStructured_Product_Tranche.strAssetclass = "RIC"
    '        udtStructured_Product_Tranche.strAccumTenorType = ddlTenorTypeAccum.SelectedValue
    '        udtStructured_Product_Tranche.inttenorAccum = CInt(Val(txtTenorAccumDecum.Text))
    '        If ddlExchangeAccumDecum.SelectedValue.ToUpper = "ALL" Then
    '            Dim sTemp As String
    '            udtStructured_Product_Tranche.strExchangeAccum = objELNRFQ.GetShareExchange(ddlShareAccumDecum.SelectedValue.ToString, sTemp)
    '        Else
    '            udtStructured_Product_Tranche.strExchangeAccum = ddlExchangeAccumDecum.SelectedValue
    '        End If
    '        Select Case ddlFrequencyAccumDecum.SelectedItem.Text.ToUpper.Trim
    '            Case "WEEKLY"
    '                udtStructured_Product_Tranche.strGuaranteedDuration = CStr(Val(ddlAccumGUDuration.SelectedValue))
    '                udtStructured_Product_Tranche.strGuaranteedDurationType = "WEEK"
    '            Case "BI-WEEKLY"
    '                udtStructured_Product_Tranche.strGuaranteedDuration = CStr(2 * CInt(CStr(Val(ddlAccumGUDuration.SelectedValue))))
    '                udtStructured_Product_Tranche.strGuaranteedDurationType = "WEEK"
    '            Case "MONTHLY"
    '                udtStructured_Product_Tranche.strGuaranteedDuration = CStr(Val(ddlAccumGUDuration.SelectedValue))
    '                udtStructured_Product_Tranche.strGuaranteedDurationType = "MONTH"
    '        End Select
    '        udtStructured_Product_Tranche.strKoPerc = txtKO.Text
    '        Dim strLeverage As String
    '        strLeverage = If(chkLeverageRatio.Checked = False, "1", "2")
    '        udtStructured_Product_Tranche.strLeverageratio = If(strLeverage = Nothing, "1", strLeverage).ToString
    '        udtStructured_Product_Tranche.strKOType = ddlKOSettlementType.SelectedValue
    '        udtStructured_Product_Tranche.strSolveFor = ddlSolveForAccumDecum.SelectedValue
    '        udtStructured_Product_Tranche.strFrequency = ddlFrequencyAccumDecum.SelectedValue
    '        udtStructured_Product_Tranche.strUpfront = Val(txtUpfront.Text) * 100
    '        udtStructured_Product_Tranche.lngTemplateId = Convert.ToString(Session("Template_Code"))
    '        udtStructured_Product_Tranche.strUnderlyingCode = ddlShareAccumDecum.SelectedValue.ToString
    '        udtStructured_Product_Tranche.strOrderQty = Replace(txtAccumOrderqty.Text, ",", "") ''
    '        udtStructured_Product_Tranche.dblStrikePrice = Val(txtStrikeaccum.Text)
    '        udtStructured_Product_Tranche.strAQDQCcy = lblAQDQBaseCcy.Text
    '        If flag = "I" Then
    '            udtStructured_Product_Tranche.strRemark = Convert.ToString(Session("Quote_ID"))
    '        End If
    '        If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
    '            udtStructured_Product_Tranche.strUpfront1 = Val(txtUpfront.Text) * 100
    '            udtStructured_Product_Tranche.dblStrike2 = Val("")
    '        Else
    '            udtStructured_Product_Tranche.strUpfront1 = Val("")
    '            udtStructured_Product_Tranche.dblStrike2 = Val(txtStrikeaccum.Text)
    '        End If
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

    Public Function set_AccuDecu_TotalShares_NumberOfAccrualDays(ByVal intNumberOfAccrualDays As Integer, ByVal issuer As String) As Boolean
        Try
            Dim dblTotalShares As Double = 0
            Dim strTotalShares As String = ""
            Dim strNumberOfAccrualDays As String = ""

            If intNumberOfAccrualDays <= 0 Then
                dblTotalShares = 0
                strTotalShares = ""
                strNumberOfAccrualDays = ""
            Else
                strNumberOfAccrualDays = intNumberOfAccrualDays.ToString()
                dblTotalShares = intNumberOfAccrualDays * Val(Replace(txtAccumOrderqty.Text, ",", ""))
                strTotalShares = FormatNumber(dblTotalShares, 0)
            End If


            Select Case UCase(issuer)
                Case "JPM"
                    lblJPMClientPrice.Text = strNumberOfAccrualDays
                    lblJPMClientYield.Text = strTotalShares
                Case "CS"
                    lblCSClientPrice.Text = strNumberOfAccrualDays
                    lblCSClientYield.Text = strTotalShares
                Case "UBS"
                    lblUBSClientPrice.Text = strNumberOfAccrualDays
                    lblUBSClientYield.Text = strTotalShares
                Case "HSBC"
                    lblHSBCClientPrice.Text = strNumberOfAccrualDays
                    lblHSBCClientYield.Text = strTotalShares
                    'Mohit Lalwani on 29-Jan-2016
                Case "OCBC"
                    lblOCBCClientPrice.Text = strNumberOfAccrualDays
                    lblOCBCClientYield.Text = strTotalShares
                Case "CITI"
                    lblCITIClientPrice.Text = strNumberOfAccrualDays
                    lblCITIClientYield.Text = strTotalShares
                    '/Mohit Lalwani on 29-Jan-2016
                Case "LEONTEQ"
                    lblLEONTEQClientPrice.Text = strNumberOfAccrualDays
                    lblLEONTEQClientYield.Text = strTotalShares
                Case "COMMERZ"
                    lblCOMMERZClientPrice.Text = strNumberOfAccrualDays
                    lblCOMMERZClientYield.Text = strTotalShares
                Case "BAML"
                    lblBAMLClientPrice.Text = strNumberOfAccrualDays
                    lblBAMLClientYield.Text = strTotalShares
                Case "BNPP"
                    lblBNPPClientPrice.Text = strNumberOfAccrualDays
                    lblBNPPClientYield.Text = strTotalShares
                Case "DB"
                    lblDBIBClientPrice.Text = strNumberOfAccrualDays
                    lblDBIBClientYield.Text = strTotalShares
            End Select

            Return True
        Catch ex As Exception
            lblerror.Text = "set_AccuDecu_TotalShares_NumberOfAccrualDays:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "set_AccuDecu_TotalShares_NumberOfAccrualDays", ErrorLevel.High)
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

            lblBAMLPrice.Text = "" ''"0.0"
            lblBAMLClientPrice.Text = "" ''"0.0"
            lblBAMLClientYield.Text = "" ''"0.0"

            lblBNPPPrice.Text = "" ''"" ''"0.0"
            lblBNPPClientPrice.Text = "" ''"" ''"0.0"
            lblBNPPClientYield.Text = "" ''"" ''"0.0"
            lblDBIBPrice.Text = "" ''"" ''"0.0"
            lblDBIBClientPrice.Text = "" ''"" ''"0.0"
            lblDBIBClientYield.Text = "" ''"" ''"0.0"

            pnlReprice.Update()

            Return True
        Catch ex As Exception
            lblerror.Text = "setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice", ErrorLevel.High)
            Throw ex
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

            ''<AshwiniP on 16-Sept-2016 START>
            dt = New DataTable("PRRating")
            Select Case objELNRFQ.DB_UnderlyingRiskRatingShare(ddlShareAccumDecum.SelectedValue.ToString, dt)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dt.Rows(0).Item(0).ToString = "NA" Then
                        lblerror.ForeColor = Color.Red
                        lblerrorPopUp.Text = "Order cannot be placed as PRR is not available."
                        '' chkConfirmDeal.Visible = False
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
            If ddlAccumType.SelectedValue.ToUpper = "ACCUMULATOR" Then
                sPrd = "Accumulator"
            Else
                sPrd = "Decumulator"
            End If
            objELNRFQ.GetShareQuoteCcy(ddlShareAccumDecum.SelectedValue.ToString, sRangeCcy)

            If ddlOrderTypePopUpValue.SelectedValue.Trim.ToUpper = "LIMIT" Then
                If txtLimitPricePopUpValue.Text = "" OrElse Val(txtLimitPricePopUpValue.Text) = 0 Then
                    lblerrorPopUp.Text = "Please enter limit price."
                    chk_DealValidations = False
                    Exit Function
                Else
                    chk_DealValidations = True
                End If
                ' If (txtLimitPricePopUpValue.Text.Length - (txtLimitPricePopUpValue.Text.LastIndexOf(".") + 1)) > 4 Then
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

            '            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            '            If Val(lblIssuerPricePopUpValue.Text) <= 0 Then
            '                lblerrorPopUp.Text = "Invalid strike(%).. Cannot proceed."
            '                chk_DealValidations = False
            '                Exit Function
            '            Else
            '                chk_DealValidations = True
            '            End If
            '            ''</Rutuja 5May:‐ve Strike validation>
            '            ''<Dilkhush 29Dec2015 :Code take out from select case>
            '            Dim EqPair As String = ddlShareAccumDecum.SelectedValue.ToString & " ‐ " & lblAQDQBaseCcy.Text
            '            ' TO get the Quantity of Shares and Value from Stored Proc
            '            Dim BidRate As Double = objELNRFQ.GetShareRate(EqPair, BidRate)
            '            Dim Notional As Double
            '            Dim iAccDays As Integer
            '            ''</Dilkhush 29Dec2015 :Code take out from select case>
            '            '<AvinashG. on 01‐Jul‐2015: FA‐929 EQ RFQ and Order: Disable Issuer Limit Check for Acc/Dec|Moving logic for Limit check in Yes part>
            '            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowIssuerLimitCheckForAccDec", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            '                Case "Y", "YES"
            '                    If BidRate = 0 Or BidRate = Nothing Then
            '                        lblerrorPopUp.Text = "Cannot proceed. Share rate not specified."
            '                        chk_DealValidations = False
            '                    Else
            '                        ' Notional Validations
            '                        Select Case lblIssuerPopUpValue.Text.Trim.ToUpper
            '                            Case "JPM"
            '                                iAccDays = CInt(lblJPMClientPrice.Text.Replace(",", ""))
            '                            Case "HSBC"
            '                                iAccDays = CInt(lblHSBCClientPrice.Text.Replace(",", ""))
            '                            Case "UBS"
            '                                iAccDays = CInt(lblUBSClientPrice.Text.Replace(",", ""))
            '                            Case "CS"
            '                                iAccDays = CInt(lblCSClientPrice.Text.Replace(",", ""))
            '                            Case "BAML"
            '                                iAccDays = CInt(lblBAMLClientPrice.Text.Replace(",", ""))
            '                            Case "BNPP"
            '                                iAccDays = CInt(lblBNPPClientPrice.Text.Replace(",", ""))
            '                        End Select
            '                        Notional = (Convert.ToDouble(txtAccumOrderqty.Text) * iAccDays * BidRate)
            '                    End If
            '                    ''<Dilkhush 24Dec2015:ADded for redirection>
            '                    Select Case objELNRFQ.Get_EQCPRD_Limit(sPrd, sRangeCcy, dtRangeLimit)
            '                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            '                            If Not (dtRangeLimit Is Nothing) Or dtRangeLimit.Rows.Count < 1 Then
            '                                Dim result() As DataRow = dtRangeLimit.Select("EQCPPL_PPCode = '" + sPPCode + "'")
            '                                If result.Length > 0 Then
            '                                    Dim lmtRow As DataRow
            '                                    lmtRow = result(0)
            '                                    Dim dblMin, dblMax As Double
            '                                    dblMin = CDbl(If(IsDBNull(lmtRow("EQCPPL_Minm")), 0, CDbl(lmtRow("EQCPPL_Minm"))))
            '                                    dblMax = CDbl(If(IsDBNull(lmtRow("EQCPPL_Maxm")), 0, CDbl(lmtRow("EQCPPL_Maxm"))))
            '                                    Select Case sEQC_DealerRedirection_OnPricer.ToUpper
            '                                        Case "Y", "YES"
            '                                            sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
            '                                            Dim sLoginGrp As String
            '                                            sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
            '                                            If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
            '                                                If (Notional < dblMin) Then
            '                                                    lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            '                                                    chk_DealValidations = False
            '                                                ElseIf (Notional > dblMax) Then
            '                                                    lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
            '                                                    chk_DealValidations = False
            '                                                Else
            '                                                    chk_DealValidations = True
            '                                                End If
            '                                                'Mohit
            '                                            Else
            '                                                If (Notional < dblMin) Then
            '                                                    lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            '                                                    chk_DealValidations = False
            '                                                    Exit Function
            '                                                ElseIf (Notional > dblMax) Then
            '                                                    lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
            '                                                    chk_DealValidations = False
            '                                                    Exit Function
            '                                                Else
            '                                                    Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, sRangeCcy, sPrd, dblUserLimit)
            '                                                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            '                                                            If dblUserLimit > 0 Then
            '                                                                If Notional > dblUserLimit Then
            '                                                                    lblerrorPopUp.Text = "Notional Size is larger than your permitted limit. Do you want to redirect this order to dealer?"
            '                                                                    btnRedirect.Visible = True
            '                                                                    chk_DealValidations = False
            '                                                                Else
            '                                                                    chk_DealValidations = True
            '                                                                End If
            '                                                            Else
            '                                                                If dblUserLimit = 0 Then
            '                                                                    lblerrorPopUp.Text = "Limit not found or Zero limit found. Do you want to redirect this order to dealer?"
            '                                                                    btnRedirect.Visible = True
            '                                                                    chk_DealValidations = False
            '                                                                    LogException(LoginInfoGV.Login_Info.LoginId, "User/User Group Limit found 0(Zero) for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
            'sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
            '                                                                Else
            '                                                                    LogException(LoginInfoGV.Login_Info.LoginId, "Invalid value(" + dblUserLimit.ToString + ") found for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
            'sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
            '                                                                    chk_DealValidations = False
            '                                                                End If
            '                                                            End If
            '                                                        Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            '                                                            lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
            '                                                            chk_DealValidations = False
            '                                                    End Select
            '                                                End If
            '                                            End If
            '                                        Case "N", "NO"
            '                                            If (Notional < dblMin) Then
            '                                                lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            '                                                chk_DealValidations = False
            '                                            ElseIf (Notional > dblMax) Then
            '                                                lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
            '                                                chk_DealValidations = False
            '                                            Else
            '                                                chk_DealValidations = True
            '                                            End If
            '                                    End Select
            '                                    '/Mohit
            '                                Else
            '                                    lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            '                                    chk_DealValidations = False
            '                                    Exit Function
            '                                End If
            '                            Else
            '                                lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            '                                chk_DealValidations = False
            '                                Exit Function
            '                            End If
            '                        Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            '                            lblerrorPopUp.Text = "Failed to retrieve range limit"
            '                            chk_DealValidations = False
            '                            Exit Function
            '                        Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
            '                            lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            '                            chk_DealValidations = False
            '                            Exit Function
            '                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            '                            lblerrorPopUp.Text = "Failed to retrieve range limit"
            '                            chk_DealValidations = False
            '                            Exit Function
            '                    End Select
            '                    'Mohit
            '                    ' ''End If ''Dilkhush 29Dec2015 :commented
            '                    '</AvinashG. on 08‐Aug‐2014: Notional Range Validation>
            '                    '<\Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
            '                Case "N", "NO"
            '                    ''Dilkhush 29Dec2015: For REdirection
            '                    '' chk_DealValidations = True
            '                    Select Case sEQC_DealerRedirection_OnPricer.ToUpper
            '                        Case "Y", "YES"
            '                            If BidRate = 0 Or BidRate = Nothing Then
            '                                lblerrorPopUp.Text = "Cannot proceed. Share rate not specified."
            '                                chk_DealValidations = False
            '                            Else
            '                                ' Notional Validations
            '                                Select Case lblIssuerPopUpValue.Text.Trim.ToUpper
            '                                    Case "JPM"
            '                                        iAccDays = CInt(lblJPMClientPrice.Text.Replace(",", ""))
            '                                    Case "HSBC"
            '                                        iAccDays = CInt(lblHSBCClientPrice.Text.Replace(",", ""))
            '                                    Case "UBS"
            '                                        iAccDays = CInt(lblUBSClientPrice.Text.Replace(",", ""))
            '                                    Case "CS"
            '                                        iAccDays = CInt(lblCSClientPrice.Text.Replace(",", ""))
            '                                    Case "BAML"
            '                                        iAccDays = CInt(lblBAMLClientPrice.Text.Replace(",", ""))
            '                                    Case "BNPP"
            '                                        iAccDays = CInt(lblBNPPClientPrice.Text.Replace(",", ""))
            '                                End Select
            '                                Notional = (Convert.ToDouble(txtAccumOrderqty.Text) * iAccDays * BidRate)
            '                            End If
            '                            sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
            '                            Dim sLoginGrp As String
            '                            sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
            '                            If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
            '                            Else
            '                                Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, sRangeCcy, sPrd, dblUserLimit)
            '                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            '                                        If dblUserLimit > 0 Then
            '                                            If Notional > dblUserLimit Then ''Condition for notional greater than issuer limit will get checked in previous if
            '                                                lblerrorPopUp.Text = "Notional Size is larger than your permitted limit. Do you want to redirect this order to dealer?"
            '                                                btnRedirect.Visible = True
            '                                                chk_DealValidations = False
            '                                            Else
            '                                                chk_DealValidations = True
            '                                            End If
            '                                        Else
            '                                            If dblUserLimit = 0 Then
            '                                                lblerrorPopUp.Text = "Limit not found or Zero limit found. Do you want to redirect this order to dealer?"
            '                                                btnRedirect.Visible = True
            '                                                chk_DealValidations = False
            '                                                LogException(LoginInfoGV.Login_Info.LoginId, "User/User Group Limit found 0(Zero) for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
            '                                                              sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
            '                                            Else
            '                                                LogException(LoginInfoGV.Login_Info.LoginId, "Invalid value(" + dblUserLimit.ToString + ") found for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
            'sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
            '                                                chk_DealValidations = False
            '                                            End If
            '                                        End If
            '                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            '                                        lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
            '                                        chk_DealValidations = False
            '                                End Select
            '                            End If
            '                        Case "N", "NO"
            '                            chk_DealValidations = True
            '                    End Select
            ''<Rutuja 5May:-ve Strike validation>
            If Val(lblIssuerPricePopUpValue.Text) <= 0 Then
                lblerrorPopUp.Text = "Invalid strike(%).. Cannot proceed."
                chk_DealValidations = False
                Exit Function
            Else
                chk_DealValidations = True
            End If
            ''</Rutuja 5May:-ve Strike validation>

            ''<Changed by Chitralekha on 22Sept2016
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
                        lblerrorPopUp.Text = "Please enter valid Number of Shares."
                        Return False
                    ElseIf isChecked AndAlso (row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim) = "0" Then ''<Nikhil M. on 17-Oct-2016: Added to check the no zero notional RM  >
                        chkUpfrontOverride.Visible = False
                        lblerrorPopUp.Text = "Shares can not be 0."
                        Return False
                    End If
                    If isChecked AndAlso row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim <> "" Then
                        TotalSum = TotalSum + CDbl(row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text)

                    End If
                    If isChecked AndAlso row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim = "" Then
                        chkUpfrontOverride.Visible = False
                        lblerrorPopUp.Text = "Please Enter Number of Shares."
                        Return False
                    End If
                Next
                If TotalSum <> CDbl(lblNotionalPopUpValue.Text) Then
                    chkUpfrontOverride.Visible = False
                    lblerrorPopUp.Text = "Sum of Number of Shares is not equals Order Quantity."
                    Return False
                End If
            Else
                chkUpfrontOverride.Visible = False
                lblerrorPopUp.Text = "Please add Allocation."
            End If
	    ''<Uncommented by Rushikesh As told by Sanchita on 5Nov16>
            If chkDuplicateRecords() Then    ''Removed by AshwiniP on 01-Oct-2016
            Else
                Return False
            End If
	    ''</Uncommented by Rushikesh As told by Sanchita on 5Nov16>
            ''</Changed by Chitralekha on 22Sept2016
            '''''''Dilkhush Avinash upfront check 
            'EQBOSDEV-435 - Set Min/Max upfront for HK and non HK underlyings for notes and options

            Dim objdeal As Web_FinIQ_MarketData.QECapture
            objdeal = New Web_FinIQ_MarketData.QECapture
            objdeal.ProductCode = ddlAccumType.SelectedValue
            objdeal.sStock = lblUnderlyingPopUpValue.Text
            objdeal.sStockCcy = lblAQDQBaseCcy.Text
            objdeal.sQuantoCcy = lblAQDQBaseCcy.Text        ''As AQDQ don't have quanto, passing same ccy 
            If ddlExchangeAccumDecum.SelectedValue.ToUpper = "ALL" Then
                Dim sTemp As String
                objdeal.ExchangeName = objELNRFQ.GetShareExchange(lblUnderlyingPopUpValue.Text, sTemp)
            Else
                objdeal.ExchangeName = ddlExchangeAccumDecum.SelectedValue
            End If

            objdeal.Notional = CDbl(lblNotionalPopUpValue.Text)
            Dim sTenorType, sTenorVal As String
            sTenorVal = lblClientPricePopUpValue.Text.Split(CChar(" "))(0)
            sTenorType = lblClientPricePopUpValue.Text.Split(CChar(" "))(1)

            If sTenorType.ToUpper = "MONTH" Then
                objdeal.TenorInMonths = CInt(sTenorVal)
            Else
                objdeal.TenorInMonths = CInt(sTenorVal) * 12
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
                    lblerrorPopUp.Text = "Cannot proceed. Setup issue in upfront validation."
                    chk_DealValidations = False
                    chkUpfrontOverride.Visible = False ''<Nikhil M. on 09-Nov-2016: EQSCB-170 | Hide Checkbox on hard block>
                    Exit Function
            End Select

            ''<Dilkhush 29Dec2015 :Code take out from select case>
            Dim EqPair As String = ddlShareAccumDecum.SelectedValue.ToString & " - " & lblAQDQBaseCcy.Text
            ' TO get the Quantity of Shares and Value from Stored Proc
            Dim BidRate As Double = objELNRFQ.GetShareRate(EqPair, BidRate)
            Dim Notional As Double
            Dim iAccDays As Integer

            ''</Dilkhush 29Dec2015 :Code take out from select case>

            '<AvinashG. on 01-Jul-2015:     FA-929 EQ RFQ and Order: Disable Issuer Limit Check for Acc/Dec|Moving logic for Limit check in Yes part>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowIssuerLimitCheckForAccDec", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"

                    If BidRate = 0 Or BidRate = Nothing Then
                        lblerrorPopUp.Text = "Cannot proceed. Share rate not specified."
                        chk_DealValidations = False
                        Exit Function ''Added by Rushikesh
                    Else
                        ' Notional Validations
                        Select Case lblIssuerPopUpValue.Text.Trim.ToUpper
                            Case "JPM"
                                iAccDays = CInt(lblJPMClientPrice.Text.Replace(",", ""))
                            Case "HSBC"
                                iAccDays = CInt(lblHSBCClientPrice.Text.Replace(",", ""))
                            Case "OCBC"                    'Mohit Lalwani on 29-Jan-2016
                                iAccDays = CInt(lblOCBCClientPrice.Text.Replace(",", ""))
                            Case "CITI"                    'Mohit Lalwani on 29-Jan-2016
                                iAccDays = CInt(lblCITIClientPrice.Text.Replace(",", ""))
                            Case "LEONTEQ"
                                iAccDays = CInt(lblLEONTEQClientPrice.Text.Replace(",", ""))
                            Case "COMMERZ"
                                iAccDays = CInt(lblCOMMERZClientPrice.Text.Replace(",", ""))
                            Case "UBS"
                                iAccDays = CInt(lblUBSClientPrice.Text.Replace(",", ""))
                            Case "CS"
                                iAccDays = CInt(lblCSClientPrice.Text.Replace(",", ""))
                            Case "BAML"
                                iAccDays = CInt(lblBAMLClientPrice.Text.Replace(",", ""))
                            Case "BNPP"
                                iAccDays = CInt(lblBNPPClientPrice.Text.Replace(",", ""))
                            Case "DB"
                                iAccDays = CInt(lblDBIBClientPrice.Text.Replace(",", "")) 'Urgent       FA-1401 - btnClick_Button Error:when price is clicked 
                        End Select
                        Notional = (Convert.ToDouble(txtAccumOrderqty.Text) * iAccDays * BidRate)
                    End If

                    ''<Dilkhush 24Dec2015:ADded for redirection>
                    '<Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    '' ''Select Case objELNRFQ.Get_EQCPRD_Limit(sPrd, sRangeCcy, dtRangeLimit)
                    '' ''    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    '' ''If Not (dtRangeLimit Is Nothing) Or dtRangeLimit.Rows.Count < 1 Then
                    '' ''Dim result() As DataRow = dtRangeLimit.Select("EQCPPL_PPCode = '" + sPPCode + "'")
                    '' ''If result.Length > 0 Then
                    '' ''Dim lmtRow As DataRow
                    '' ''lmtRow = result(0)
                    '' ''Dim dblMin, dblMax As Double
                    '' ''dblMin = CDbl(If(IsDBNull(lmtRow("EQCPPL_Minm")), 0, CDbl(lmtRow("EQCPPL_Minm"))))
                    '' ''dblMax = CDbl(If(IsDBNull(lmtRow("EQCPPL_Maxm")), 0, CDbl(lmtRow("EQCPPL_Maxm"))))
                    '</Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                        Case "Y", "YES"
                            'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                            'Dim sLoginGrp As String
                            'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup

                            'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                            If UCase(Request.QueryString("Mode")) = "ALL" Then
                                ''User is Dealer
                                '<Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                                '' ''If (Notional < dblMin) Then
                                '' ''    lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
                                '' ''    chk_DealValidations = False
                                '' ''ElseIf (Notional > dblMax) Then
                                '' ''    lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
                                '' ''    chk_DealValidations = False
                                '' ''Else
                                '' ''    chk_DealValidations = True
                                '' ''End If
                                ' '' ''Mohit
                                '</Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                            Else
                                '<Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                                '' ''If (Notional < dblMin) Then
                                '' ''    lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
                                '' ''    chk_DealValidations = False
                                '' ''    Exit Function
                                '' ''ElseIf (Notional > dblMax) Then
                                '' ''    lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
                                '' ''    chk_DealValidations = False
                                '' ''    Exit Function
                                '' ''Else
                                '</Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                                Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, sRangeCcy, sPrd, dblUserLimit)
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                        If dblUserLimit > 0 Then
                                            If Notional > dblUserLimit Then
                                                ''Dilkhush/AVinash 03Feb2016 Added limit in message
                                                lblerrorPopUp.Text = "Notional Size is larger than your permitted limit (" + FormatNumber(dblUserLimit.ToString, 0) + " " + sRangeCcy + "). Do you want to redirect this order to dealer?"
                                                ''lblerrorPopUp.Text = "Notional Size is larger than your permitted limit. Do you want to redirect this order to dealer?"
                                                btnRedirect.Visible = True
                                                btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                                chk_DealValidations = False
                                            Else
                                                chk_DealValidations = True
                                            End If

                                        Else

                                            If dblUserLimit = 0 Then
                                                lblerrorPopUp.Text = "Limit not found or Zero limit found. Do you want to redirect this order to dealer?"
                                                btnRedirect.Visible = True
                                                btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                                chk_DealValidations = False
                                                LogException(LoginInfoGV.Login_Info.LoginId, "User/User Group Limit found 0(Zero) for  " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
                                                                                        sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                            Else
                                                LogException(LoginInfoGV.Login_Info.LoginId, "Invalid value(" + dblUserLimit.ToString + ") found for  " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
                                                                                        sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                                chk_DealValidations = False
                                            End If
                                        End If
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                        lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
                                        chk_DealValidations = False
                                End Select
                                '' ''End If
                            End If
                        Case "N", "NO"
                            '<Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                            '' ''If (Notional < dblMin) Then
                            '' ''    lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
                            '' ''    chk_DealValidations = False
                            '' ''ElseIf (Notional > dblMax) Then
                            '' ''    lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
                            '' ''    chk_DealValidations = False
                            '' ''Else
                            '' ''chk_DealValidations = True
                            '' ''End If
                            '</Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    End Select
                    '<Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                    '/Mohit
                    '' ''Else
                    '' ''    lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
                    '' ''    chk_DealValidations = False
                    '' ''    Exit Function
                    '' ''End If
                    '' ''Else
                    '' ''    lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
                    '' ''    chk_DealValidations = False
                    '' ''    Exit Function
                    '' ''End If
                    '' ''    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    '' ''        lblerrorPopUp.Text = "Failed to retrieve range limit"
                    '' ''        chk_DealValidations = False
                    '' ''        Exit Function
                    '' ''    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    '' ''        lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
                    '' ''        chk_DealValidations = False
                    '' ''        Exit Function
                    '' ''    Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                    '' ''        lblerrorPopUp.Text = "Failed to retrieve range limit"
                    '' ''        chk_DealValidations = False
                    '' ''        Exit Function
                    '' ''End Select
                    'Mohit	
                    ' ''End If ''Dilkhush 29Dec2015 :commented
                    '</AvinashG. on 08-Aug-2014: Notional Range Validation>
                    '<\Commented By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
                Case "N", "NO"
                    ''Dilkhush 29Dec2015: For REdirection
                    '' chk_DealValidations = True
                    Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                        Case "Y", "YES"

                            'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                            'Dim sLoginGrp As String
                            'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
                            'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                            If UCase(Request.QueryString("Mode")) = "ALL" Then
                                ''User is Dealer
                                chk_DealValidations = True
                            Else
                                If BidRate = 0 Or BidRate = Nothing Then
                                    lblerrorPopUp.Text = "Cannot proceed. Share rate not specified."
                                    chk_DealValidations = False
                                    Exit Function ''Added by Rushikesh
                                Else
                                    ' Notional Validations
                                    Select Case lblIssuerPopUpValue.Text.Trim.ToUpper
                                        Case "JPM"
                                            iAccDays = CInt(lblJPMClientPrice.Text.Replace(",", ""))
                                        Case "HSBC"
                                            iAccDays = CInt(lblHSBCClientPrice.Text.Replace(",", ""))
                                        Case "OCBC"
                                            iAccDays = CInt(lblOCBCClientPrice.Text.Replace(",", ""))
                                        Case "CITI"
                                            iAccDays = CInt(lblCITIClientPrice.Text.Replace(",", ""))
                                        Case "LEONTEQ"
                                            iAccDays = CInt(lblLEONTEQClientPrice.Text.Replace(",", ""))
                                        Case "COMMERZ"
                                            iAccDays = CInt(lblCOMMERZClientPrice.Text.Replace(",", ""))
                                        Case "UBS"
                                            iAccDays = CInt(lblUBSClientPrice.Text.Replace(",", ""))
                                        Case "CS"
                                            iAccDays = CInt(lblCSClientPrice.Text.Replace(",", ""))
                                        Case "BAML"
                                            iAccDays = CInt(lblBAMLClientPrice.Text.Replace(",", ""))
                                        Case "BNPP"
                                            iAccDays = CInt(lblBNPPClientPrice.Text.Replace(",", ""))
                                        Case "DB"
                                            iAccDays = CInt(lblDBIBClientPrice.Text.Replace(",", "")) 'Urgent           
                                    End Select
                                    Notional = (Convert.ToDouble(txtAccumOrderqty.Text) * iAccDays * BidRate)
                                End If


                                Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, sRangeCcy, sPrd, dblUserLimit)
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                        If dblUserLimit > 0 Then
                                            If Notional > dblUserLimit Then ''Condition for notional greater than issuer limit will get checked in previous if
                                                lblerrorPopUp.Text = "Notional Size is larger than your permitted limit (" + FormatNumber(dblUserLimit.ToString, 0) + " " + sRangeCcy + "). Do you want to redirect this order to dealer?"
                                                btnRedirect.Visible = True
                                                btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                                chk_DealValidations = False
                                            Else
                                                chk_DealValidations = True
                                            End If
                                        Else
                                            If dblUserLimit = 0 Then
                                                lblerrorPopUp.Text = "Limit not found or Zero limit found. Do you want to redirect this order to dealer?"
                                                btnRedirect.Visible = True
                                                btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                                chk_DealValidations = False
                                                LogException(LoginInfoGV.Login_Info.LoginId, "User/User Group Limit found 0(Zero) for  " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
                                                          sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                            Else
                                                LogException(LoginInfoGV.Login_Info.LoginId, "Invalid value(" + dblUserLimit.ToString + ") found for  " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
                                                          sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                                chk_DealValidations = False
                                            End If

                                        End If
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                        lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
                                        chk_DealValidations = False
                                End Select
                            End If
                        Case "N", "NO"
                            chk_DealValidations = True
                    End Select
                    ''Dilkhush 29Dec2015 : Redirection
            End Select
            '<AvinashG. on 01‐Jul‐2015: FA‐929 EQ RFQ and Order: Disable Issuer Limit Check for Acc/Dec|Moving logic for Limit check in Yes part>


            'If Val(lblIssuerPricePopUpValue.Text) <= 0 Then
            '    lblerrorPopUp.Text = "Invalid strike(%).. Cannot proceed."
            '    chk_DealValidations = False
            '    Exit Function
            'Else
            '    chk_DealValidations = True
            'End If
            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowIssuerLimitCheckForAccDec", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            '    Case "Y", "YES"
            '        Dim EqPair As String = ddlShareAccumDecum.SelectedValue.ToString & " - " & lblAQDQBaseCcy.Text
            '        Dim BidRate As Double = objELNRFQ.GetShareRate(EqPair, BidRate)

            '        Dim iAccDays As Integer
            '        If BidRate = 0 Or BidRate = Nothing Then
            '            lblerrorPopUp.Text = "Cannot proceed. Share rate not specified."
            '            chk_DealValidations = False
            '        Else
            '            Select Case lblIssuerPopUpValue.Text.Trim.ToUpper
            '                Case "JPM"
            '                    iAccDays = CInt(lblJPMClientPrice.Text.Replace(",", ""))
            '                Case "HSBC"
            '                    iAccDays = CInt(lblHSBCClientPrice.Text.Replace(",", ""))
            '                Case "UBS"
            '                    iAccDays = CInt(lblUBSClientPrice.Text.Replace(",", ""))
            '                Case "CS"
            '                    iAccDays = CInt(lblCSClientPrice.Text.Replace(",", ""))
            '                Case "BAML"
            '                    iAccDays = CInt(lblBAMLClientPrice.Text.Replace(",", ""))
            '                Case "BNPP"
            '                    iAccDays = CInt(lblBNPPClientPrice.Text.Replace(",", ""))
            '                Case "DBIB"
            '                    iAccDays = CInt(lblDBIBClientPrice.Text.Replace(",", ""))
            '            End Select

            '            Dim Notional As Double = (Convert.ToDouble(txtAccumOrderqty.Text) * iAccDays * BidRate)
            '            Select Case objELNRFQ.Get_EQCPRD_Limit(sPrd, sRangeCcy, dtRangeLimit)
            '                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            '                    If Not (dtRangeLimit Is Nothing) Or dtRangeLimit.Rows.Count < 1 Then
            '                        Dim result() As DataRow = dtRangeLimit.Select("EQCPPL_PPCode = '" + sPPCode + "'")
            '                        If result.Length > 0 Then
            '                            Dim lmtRow As DataRow
            '                            lmtRow = result(0)
            '                            Dim dblMin, dblMax As Double
            '                            dblMin = CDbl(If(IsDBNull(lmtRow("EQCPPL_Minm")), 0, CDbl(lmtRow("EQCPPL_Minm"))))
            '                            dblMax = CDbl(If(IsDBNull(lmtRow("EQCPPL_Maxm")), 0, CDbl(lmtRow("EQCPPL_Maxm"))))
            '                            Select Case sEQC_DealerRedirection_OnPricer.ToUpper
            '                                Case "Y", "YES"
            '                                    sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
            '                                    Dim sLoginGrp As String
            '                                    sLoginGrp = LoginInfoGV.Login_Info.LoginGroup

            '                                    If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
            '                                        If (Notional < dblMin) Then
            '                                            lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            '                                            chk_DealValidations = False
            '                                        ElseIf (Notional > dblMax) Then
            '                                            lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
            '                                            chk_DealValidations = False
            '                                        Else
            '                                            chk_DealValidations = True
            '                                        End If
            '                                        'Mohit
            '                                    Else
            '                                        If (Notional < dblMin) Then
            '                                            lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            '                                            chk_DealValidations = False
            '                                            Exit Function
            '                                        ElseIf (Notional > dblMax) Then
            '                                            lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
            '                                            chk_DealValidations = False
            '                                            Exit Function
            '                                        Else
            '                                            Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, sRangeCcy, sPrd, dblUserLimit)
            '                                                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            '                                                    If dblUserLimit > 0 Then
            '                                                        If Notional > dblUserLimit Then
            '                                                            lblerrorPopUp.Text = "Notional Size is larger than your permitted limit. Do you want to redirect this order to dealer?"
            '                                                            btnRedirect.Visible = True
            '                                                            chk_DealValidations = False
            '                                                        Else
            '                                                            chk_DealValidations = True
            '                                                        End If

            '                                                    Else

            '                                                        If dblUserLimit = 0 Then
            '                                                            lblerrorPopUp.Text = "Limit not found or Zero limit found. Do you want to redirect this order to dealer?"
            '                                                            btnRedirect.Visible = True
            '                                                            chk_DealValidations = False
            '                                                            LogException(LoginInfoGV.Login_Info.LoginId, "User/User Group Limit found 0(Zero) for  " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
            '                                                                                                    sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
            '                                                        Else
            '                                                            LogException(LoginInfoGV.Login_Info.LoginId, "Invalid value(" + dblUserLimit.ToString + ") found for  " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
            '                                                                                                    sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
            '                                                            chk_DealValidations = False
            '                                                        End If
            '                                                    End If
            '                                                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            '                                                    lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
            '                                                    chk_DealValidations = False
            '                                            End Select
            '                                        End If
            '                                    End If
            '                                Case "N", "NO"
            '                                    If (Notional < dblMin) Then
            '                                        lblerrorPopUp.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
            '                                        chk_DealValidations = False
            '                                    ElseIf (Notional > dblMax) Then
            '                                        lblerrorPopUp.Text = "Can not place order. Notional size is larger than the maximum permitted."
            '                                        chk_DealValidations = False
            '                                    Else
            '                                        chk_DealValidations = True
            '                                    End If

            '                            End Select
            '                            '/Mohit
            '                        Else
            '                            lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            '                            chk_DealValidations = False
            '                            Exit Function
            '                        End If
            '                    Else
            '                        lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            '                        chk_DealValidations = False
            '                        Exit Function
            '                    End If
            '                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            '                    lblerrorPopUp.Text = "Failed to retrieve range limit"
            '                    chk_DealValidations = False
            '                    Exit Function
            '                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
            '                    lblerrorPopUp.Text = "Range not set for " + sRangeCcy + " for " + sPPCode + "."
            '                    chk_DealValidations = False
            '                    Exit Function
            '                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            '                    lblerrorPopUp.Text = "Failed to retrieve range limit"
            '                    chk_DealValidations = False
            '                    Exit Function
            '            End Select
            '            'Mohit			
            '        End If
            '    Case "N", "NO"
            '        chk_DealValidations = True
            'End Select
            ''/
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>

           
            ''</Changed by Chitralekha on 22Sept2016
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
            btnCITIPrice.Text = "Price"
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
            lblMsgPriceProvider.Text = ""
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
            lblMsgPriceProvider.Text = ""
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
            lblMsgPriceProvider.Text = ""
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
                ''<Dilkhush:22Dec2015 config based GridAuto Refresh>
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_RealTime_Quote_History", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        btnLoad_Click(sender, e)
                    Case "N", "NO"
                        ''Do Nothing
                End Select
                ''btnLoad_Click(sender, e)
                ''</Dilkhush:22Dec2015 config based GridAuto Refresh>
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
            Quote_ID = Convert.ToString(Session("DBIBQuote"))
            Session.Remove("DBIBQuote")
            rbHistory.SelectedValue = "Order History"
            If Convert.ToString(Session("flag")) = "I" Then
                Dim strDBIBID As String
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
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');")
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptDealClicked", strJavaScriptDealClicked.ToString, True)
            lblerror.Text = ""
            strMargin = ""
            strClientPrice = ""
            strClientYield = ""
            strBookingBranch = ddlBookingBranchPopUpValue.SelectedValue
            orderQuantity = lblNotionalPopUpValue.Text
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
            Select Case objELNRFQ.web_Get_orderPlaced_with_Margin_Price_Yield(orderQuantity.Replace(",", ""), strType, strLimitPrice1, strLimitPrice2, strLimitPrice3, _
                                                                              strId, sPoolID, sRedirectOrderID, LoginInfoGV.Login_Info.LoginId, sOrderComment, strMargin, strClientPrice, _
                                                                              strClientYield, strBookingBranch, _
                                                                              strRMNameforOrderConfirm, strRMEmailIdforOrderConfirm, "", strPreTradeXml.ToString) ''<Nikhil M. on 16-Sep-2016:Added Comment for Deal Confirmation Reason  >
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

            btnOCBCPrice.Text = "Price"
            btnOCBCDeal.CssClass = "btnDisabled"
            btnCITIPrice.Text = "Price"
            btnCITIDeal.CssClass = "btnDisabled"

            btnLEONTEQPrice.Text = "Price"
            btnLEONTEQDeal.CssClass = "btnDisabled"
            btnCOMMERZPrice.Text = "Price"
            btnCOMMERZDeal.CssClass = "btnDisabled"
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
            btnBNPPDeal.Visible = False
            btnBAMLDeal.Visible = False
            btnCSDeal.Visible = False
            btnJPMDeal.Visible = False
            btnHSBCDeal.Visible = False
            btnOCBCDeal.Visible = False
            btnCITIDeal.Visible = False
            btnLEONTEQDeal.Visible = False
            btnCOMMERZDeal.Visible = False
            btnUBSDeal.Visible = False
            btnDBIBDeal.Visible = False
            txtAccumOrderqty.Enabled = True
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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


    Private Sub txtTenorAccumDecum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTenorAccumDecum.TextChanged
        Dim TenorAccumInDays As Integer = 0
        Dim strTenorAccumType As String = String.Empty
        Dim TenorAccum As Integer = 0
        Try
            lblerror.Text = ""
            ' GetCommentary_Accum()
            If ValidateTenor() = False Then   ''AshwiniP on 09-Nov-2016
                Exit Sub
            Else
                ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
                Enable_Disable_Deal_Buttons()
                If SetFrequencytype(ddlTenorTypeAccum.SelectedValue.Trim.ToUpper, txtTenorAccumDecum.Text.Trim, ddlFrequencyAccumDecum.SelectedValue.Trim) Then
                Else
                    lblerror.Text = "Frequency is not valid."
                End If
                ResetAll()
                If txtTenorAccumDecum.Text = "0" Or txtTenorAccumDecum.Text = "" Then
                    lblEstimatedNotional.Text = ""
                    lblEstimatedNoOfDays.Text = ""
                    lblEstimatedGearedShares.Text = ""      'Added by ChitralekhaM on 07-Sep-16
                    lblEstimatedUngearedShares.Text = ""    'Added by ChitralekhaM on 07-Sep-16
                Else
                    DisplayEstimatedNotional()
                End If
            End If
        Catch ex As Exception
            lblerror.Text = "txtTenorAccumDecum_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtTenorAccumDecum_TextChanged", ErrorLevel.High)
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


            Dim interval1 As String = ddlTenorTypeAccum.SelectedValue
            If interval1 = "MONTH" Then
                interval1 = "M"
            ElseIf interval1 = "YEAR" Then
                interval1 = "Y"
            End If
            Dim monthcount As Integer = 0
            If interval1 = "Y" And CDbl(txtTenorAccumDecum.Text) = 1 Then
                monthcount = 12
            ElseIf interval1 = "Y" And CDbl(txtTenorAccumDecum.Text) <> 1 Then
                lblerror.Text = "Please enter valid tenor."
                Disablebuttons()
                Exit Function
            Else
                monthcount = CInt(txtTenorAccumDecum.Text)
            End If
            Dim max_months As Integer = 0

            If ddlAccumType.SelectedValue.ToUpper = "ACCUMULATOR" Then
                max_months = CInt(objReadConfig.ReadConfig(dsConfig, "ACC_AllowedTenorInMonths", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "12").Trim.ToUpper())
            ElseIf ddlAccumType.SelectedValue.ToUpper = "DECUMULATOR" Then
                max_months = CInt(objReadConfig.ReadConfig(dsConfig, "DAC_AllowedTenorInMonths", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "12").Trim.ToUpper())
            End If

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



    Private Sub ddlTenorTypeAccum_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTenorTypeAccum.SelectedIndexChanged
        Try
            lblerror.Text = ""
            ' GetCommentary_Accum()
            If ValidateTenor() = False Then   ''AshwiniP on 09-Nov-2016
                Exit Sub
            Else
                ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
                Enable_Disable_Deal_Buttons()
                If SetFrequencytype(ddlTenorTypeAccum.SelectedValue.Trim.ToUpper, txtTenorAccumDecum.Text.Trim, ddlFrequencyAccumDecum.SelectedValue.Trim) Then
                Else
                    lblerror.Text = "Frequency is not valid."
                End If
                ResetAll()
                If txtTenorAccumDecum.Text = "0" Or txtTenorAccumDecum.Text = "" Then
                    lblEstimatedNotional.Text = ""
                    lblEstimatedNoOfDays.Text = ""
                    lblEstimatedGearedShares.Text = ""      'Added by ChitralekhaM on 07-Sep-16
                    lblEstimatedUngearedShares.Text = ""    'Added by ChitralekhaM on 07-Sep-16
                Else
                    DisplayEstimatedNotional()
                End If
            End If
        Catch ex As Exception
            lblerror.Text = "ddlTenorTypeAccum_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlTenorTypeAccum_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub


    Private Sub ddlKOSettlementType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlKOSettlementType.SelectedIndexChanged
        Try
            ResetAll()
        Catch ex As Exception
            lblerror.Text = Exception(ex)
            lblerror.Text = "ddlKOSettlementType_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlKOSettlementType_SelectedIndexChanged", ErrorLevel.High)

        End Try
    End Sub
#End Region

#Region "Timer,Button Visibility"

    Public Sub ColumnVisibility()
        Try

            If rbHistory.SelectedValue.Trim = "Quote History" Then
                makeThisGridVisible(grdAccumDecum)
                fill_Accum_Decum_Grid()
            ElseIf rbHistory.SelectedValue.Trim = "Order History" Then
                makeThisGridVisible(grdOrder)
                grdOrder.Columns(grdOrderEnum.EP_OfferPrice).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Upfront).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_CouponPercentage).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Notional_Amount1).Visible = False
                grdOrder.Columns(grdOrderEnum.LimitPrice2).Visible = False
                grdOrder.Columns(grdOrderEnum.LimitPrice3).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Execution_Price1).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_Execution_Price2).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Execution_Price3).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Client_Price).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_Client_Yield).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_RM_Margin).Visible = False
                grdOrder.Columns(grdOrderEnum.EP_AveragePrice).Visible = True
                grdOrder.Columns(grdOrderEnum.ER_GuaranteedDuration).Visible = True
                grdOrder.Columns(grdOrderEnum.ER_LeverageRatio).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_KO).Visible = True
                grdOrder.Columns(grdOrderEnum.EP_ExternalQuoteId).Visible = True
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
    'Mohit Lalwani on 22-Jan-2015
    Private Sub grdOrder_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdOrder.ItemCommand
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                If e.CommandName.ToUpper = "GETORDERDETAILS" Then
                    ShowHideDeatils(True)
                    lblDetails.Text = "Order Details"
                    Label68.Text = "Issuer Order Remark"  'Added by Mohit/Rushi on 02-May-2016 FA-1420
                    pnlDetailsPopup.Visible = True
                    trAcDcStatusBak.Visible = True
                    trAcDcOrderType.Visible = True
                    trAcDcRefSpot.Visible = True
                    trAcDcExePrc1.Visible = True
                    trAcDcAvgExePrc.Visible = True
                    trAcDCQuoteStatus.Visible = False
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    lblAlloAcDcOrderStatus.Text = ""
                    lblAlloAcDcType.Text = ""
                    lblAlloAcDcRFQID.Text = ""
                    lblAlloAcDccp.Text = ""
                    lblAlloAcDcUnderlying.Text = ""
                    lblAlloAcDccurrency.Text = ""
                    lblAlloAcDcTenor.Text = ""
                    lblAlloAcDcKOper.Text = ""
                    lblAlloAcDcstrike.Text = ""
                    lblAlloAcDcupfront.Text = ""
                    lblAlloAcDcExePrc1.Text = ""
                    lblAlloAcDcAvgExePrc.Text = ""
                    lblAlloAcDcgearing.Text = ""
                    lblAlloAcDcfrequency.Text = ""
                    lblAlloAcDcGuarantee.Text = ""
                    lblAlloAcDcDNS.Text = ""
                    lblAlloAcDcAccDays.Text = ""
                    lblAlloAcDcTotalshares.Text = ""
                    lblAlloAcDcOrderType.Text = ""
                    lblAlloAcDcLimitPrice.Text = ""
                    lblAlloAcDcRemark.Text = ""
                    lblAlloAcDcSubmittedBy.Text = ""
                    lblAlloAcDcRefSpot.Text = ""
                    ' lblValQuoteStatus.Text = ""
                    ''</added by Rushikesh on 29-Dec-15 to flush old value>
                    lblAlloAcDcOrderStatus.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Order_Status).Text
                    '<Changed by Mohit Lalwani on 4-Feb-2016>
                    Select Case grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Type).Text.ToUpper
                        Case "ACCUMULATOR"
                            lblAlloAcDcType.Text = "Accumulator"
                        Case "DECUMULATOR"
                            lblAlloAcDcType.Text = "Decumulator"
                    End Select
                    '</Changed by Mohit Lalwani on 4-Feb-2016>
                    lblAlloAcDcRFQID.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_QuoteRequestId).Text
                    lblAlloAcDccp.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.PP_CODE).Text
                    lblAlloAcDcUnderlying.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_UnderlyingCode).Text
                    lblAlloAcDccurrency.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_CashCurrency).Text
                    lblAlloAcDcTenor.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_Tenor).Text + "&nbsp;Month(s)"
                    lblAlloAcDcKOper.Text = SetNumberFormat(grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_KOPercentage).Text, 2)           'Changed By Mohit Lalwani on 2-Feb-2016
                    lblAlloAcDcstrike.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_StrikePercentage).Text
                    lblAlloAcDcupfront.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_Upfront).Text
                    ''lblAlloAcDcgearing.Text = item("LeverageRatio").Text
                    lblAlloAcDcExePrc1.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_Execution_Price1).Text
                    lblAlloAcDcAvgExePrc.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_AveragePrice).Text
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Show_AQDQ_Leverage_As_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                        Case "Y", "YES"
                            If (grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_LeverageRatio).Text.ToUpper = "NO") Then
                                lblAlloAcDcgearing.Text = "No"
                            Else
                                lblAlloAcDcgearing.Text = "Yes"
                            End If
                        Case "N", "NO"
                            If (grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_LeverageRatio).Text = "1") Then
                                lblAlloAcDcgearing.Text = "No"
                            Else
                                lblAlloAcDcgearing.Text = "Yes"
                            End If
                    End Select
                    lblAlloAcDcfrequency.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Frequency).Text
                    lblAlloAcDcGuarantee.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_GuaranteedDuration).Text
                    lblAlloAcDcDNS.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Ordered_Qty).Text
                    lblAlloAcDcAccDays.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_NumberOfDaysAccrual).Text
                    lblAlloAcDcTotalshares.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_CashOrderQuantity).Text
                    lblAlloAcDcOrderType.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ELN_Order_Type).Text
                    lblAlloAcDcLimitPrice.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.LimitPrice1).Text
                    ''lblAlloAcDcRefSpot.Text = item("Quote_Request_ID").Text
                    lblAlloAcDcRemark.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_Order_Remark1).Text
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_CaptureOrderComment", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                        Case "Y", "YES"
                            tracdcOrderComment.Visible = True
                            lblAlloacdcOrderComment.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_OrderComment).Text
                        Case "N", "NO"
                            tracdcOrderComment.Visible = False
                            lblAlloacdcOrderComment.Text = ""
                    End Select

                    lblAlloAcDcSubmittedBy.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_Created_By).Text

                    'If item("ELN_Order_Type").Text.ToUpper = "LIMIT" Then
                    '    lblAlloAcDcRefSpot.Visible = True
                    '    lblAlloAcDcRefSpot.Text = item("LimitPrice1").Text
                    'Else
                    '    lblAlloAcDcRefSpot.Visible = False
                    'End If

                    ' lblValQuoteStatus.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP).Text

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                    upnlDetails.Update()
                ElseIf e.CommandName.ToUpper = "GENERATEDOCUMENT" Then
                    '''Deal DOcgen not required by SCB
                    'Generate_Deal(grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_QuoteRequestId).Text)
                    '/Added by Mohit lalwani on 14-Mar-2016 
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    '/Mohit Lalwani on 22-Jan-2015


    Private Sub grdOrder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdOrder.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.EditItem Then
                If e.Item.Cells(grdOrderEnum.Ordered_Qty).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Ordered_Qty).Text = SetNumberFormatZeroDecim(e.Item.Cells(grdOrderEnum.Ordered_Qty).Text, 0)
                End If
                If e.Item.Cells(grdOrderEnum.Filled_Qty).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Filled_Qty).Text = SetNumberFormatZeroDecim(e.Item.Cells(grdOrderEnum.Filled_Qty).Text, 0)
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

                If e.Item.Cells(grdOrderEnum.EP_Upfront).Text <> "&nbsp;" Then
                    '  e.Item.Cells(grdOrderEnum.EP_Upfront).Text = SetNumberFormat((Val(e.Item.Cells(grdOrderEnum.EP_Upfront).Text) / 100).ToString, 2)  ''Rutuja 14April changed fro 4 to 2 decimal,told by Kalyan M ''SetNumberFormat(e.Item.Cells(12).Text, 2)
                    e.Item.Cells(grdOrderEnum.EP_Upfront).Text = (Val(e.Item.Cells(grdOrderEnum.EP_Upfront).Text) / 100).ToString  'Mohit Lalwani on 3-Feb-2016
                End If
                If e.Item.Cells(grdOrderEnum.EP_Notional_Amount1).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.EP_Notional_Amount1).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_Notional_Amount1).Text, 2)
                End If

                If e.Item.Cells(grdOrderEnum.LimitPrice1).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.LimitPrice1).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.LimitPrice1).Text, 4) ''16April:Changed from 2 digit to 4 digit decimal,told by Kalyan
                End If
                If e.Item.Cells(grdOrderEnum.EP_Execution_Price1).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.EP_Execution_Price1).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_Execution_Price1).Text, 4) ''16April:Changed from 2 digit to 4 digit decimal,told by Kalyan
                End If

                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Show_AQDQ_Leverage_As_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"

                        If (e.Item.Cells(grdOrderEnum.ER_LeverageRatio).Text.ToString = "1") Then
                            e.Item.Cells(grdOrderEnum.ER_LeverageRatio).Text = "No"
                        Else
                            e.Item.Cells(grdOrderEnum.ER_LeverageRatio).Text = "Yes"
                        End If

                    Case "N", "NO"
                End Select


                If e.Item.Cells(grdOrderEnum.EP_KO).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.EP_KO).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_KO).Text, 2)
                End If

            End If
            'grdOrder.Font.Size = CType(ddlFont.SelectedValue, FontUnit)

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

    Private Sub grdAccumDecum_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdAccumDecum.ItemCommand
        Dim strNewAccumTenor As String = String.Empty
        Dim strNewAccumTenorType As String = String.Empty
        Dim strNewAccumGu As String = String.Empty
        Dim strNewAccumGuType As String = String.Empty
        Try
            lblMsgPriceProvider.Text = ""
            lblerror.Text = ""
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                If e.CommandName.ToUpper = "SELECT" Then
                    ShowHideConfirmationPopup(False)
                    ResetAll()
                    grdAccumDecum.SelectedItemStyle.BackColor = Color.FromArgb(242, 242, 243)

                    Dim strAccumSolveFor As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Solve_For).Text

                    If strAccumSolveFor = "Strike(%)" Then
                        ddlSolveForAccumDecum.SelectedValue = "STRIKE"
                        txtStrikeaccum.Text = ""
                        txtStrikeaccum.Enabled = False
                        txtUpfront.Enabled = True
                        lblSolveForType.Text = "Strike (%)"
                    Else
                        ddlSolveForAccumDecum.SelectedValue = "UPFRONT"
                        txtUpfront.Text = ""
                        txtUpfront.Enabled = False
                        txtStrikeaccum.Enabled = True
                        lblSolveForType.Text = "Upfront (%)"
                    End If

                    Dim strAccumExchg As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Exchange).Text





                    Dim strExchng As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Exchange).Text
                    Dim strShareVal As String = String.Empty
                    strShareVal = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text
                    ''Rushikesh 14Jan2016 to set share from pool data
                    setShare(strExchng, strShareVal)

                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                        Case "Y", "YES"
                            'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                            If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                                ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                            End If
                            lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                        Case "N", "NO"
                            If ddlExchangeAccumDecum.SelectedValue = strExchng Then
                                ddlExchangeAccumDecum.SelectedValue = strExchng
                                ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text))
                                'ddlShare.Text = ddlShare.Text
                                If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                                    ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                End If
                                lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                            Else
                                ddlExchangeAccumDecum.SelectedValue = strExchng
                                '' Fillddl_Share() ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text))
                                If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                                    ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                End If
                            End If
                    End Select





                    'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    '    Case "Y", "YES"
                    '        ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text))
                    '        'ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                    '        If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                    '            ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                    '        End If
                    '        lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                    '    Case "N", "NO"
                    '        If ddlExchangeAccumDecum.SelectedValue = strAccumExchg Then
                    '            ddlExchangeAccumDecum.SelectedValue = strAccumExchg
                    '            ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text))
                    '            'ddlShareAccumDecum.Text = ddlShareAccumDecum.Text
                    '            If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                    '                ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                    '            End If
                    '        Else
                    '            ddlExchangeAccumDecum.SelectedValue = strAccumExchg
                    '            ''Fill_Accum_ddl_Share()
                    '            ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text))
                    '            'ddlShareAccumDecum.Text = ddlShareAccumDecum.Text
                    '            If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                    '                ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                    '            End If

                    '        End If
                    '        lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                    'End Select
                    getCurrency(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text)
                    ''AshwiniP on 23Sept2016
                    getPRR(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text)
                    getFlag(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text)

                    Dim strAccumTenor As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.RFQTenor).Text

                    For i = 0 To strAccumTenor.Length - 1
                        If IsNumeric(strAccumTenor.Substring(i, 1)) = True Then
                            strNewAccumTenor = strNewAccumTenor + strAccumTenor.Substring(i, 1)
                        End If
                    Next
                    txtTenorAccumDecum.Text = strNewAccumTenor

                    For i = 0 To strAccumTenor.Length - 1
                        If IsNumeric(strAccumTenor.Substring(i, 1)) = False Then
                            strNewAccumTenorType = (strNewAccumTenorType + strAccumTenor.Substring(i, 1)).Trim
                        End If
                    Next


                    ddlTenorTypeAccum.SelectedIndex = ddlTenorTypeAccum.FindItemByText(strNewAccumTenorType).Index

                    Dim strAccumGu As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.GU_Duration).Text

                    For i = 0 To strAccumGu.Length - 1
                        If IsNumeric(strAccumGu.Substring(i, 1)) = True Then
                            strNewAccumGu = strNewAccumGu + strAccumGu.Substring(i, 1)
                        End If
                    Next
                    txtDuration.Text = strNewAccumGu
                    Dim straccumFrequency As String
                    straccumFrequency = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Frequency).Text.ToUpper.Trim

                    Select Case grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Frequency).Text.ToUpper.Trim
                        Case "WEEKLY"
                            ddlFrequencyAccumDecum.SelectedIndex = 0
                            ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                            ddlAccumGUDuration.SelectedValue = strNewAccumGu
                        Case "FORTNIGHTLY", "BI-WEEKLY"
                            ddlFrequencyAccumDecum.SelectedIndex = ddlFrequencyAccumDecum.FindItemByText(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Frequency).Text).Index
                            ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                            ddlAccumGUDuration.SelectedValue = CStr(Val(strNewAccumGu) / 2)
                        Case "MONTHLY"
                            ddlFrequencyAccumDecum.SelectedIndex = ddlFrequencyAccumDecum.FindItemByText(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Frequency).Text).Index
                            ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                            ddlAccumGUDuration.SelectedValue = strNewAccumGu

                    End Select
                    Dim strAccumStrike As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Strike_Per).Text
                    If strAccumStrike = "&nbsp;" Then
                        txtStrikeaccum.Text = "0.00"
                    Else
                        txtStrikeaccum.Text = strAccumStrike
                    End If
                    Dim strAccumUpfront As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Upfront).Text

                    If strAccumUpfront = "&nbsp;" Then
                        txtUpfront.Text = "0.00"
                    Else
                        txtUpfront.Text = strAccumUpfront
                    End If


                    If txtDuration.Text = "" Then
                        ddlAccumGUDuration.SelectedValue = "0"
                    End If

                    For i = 0 To strAccumGu.Length - 1
                        If IsNumeric(strAccumGu.Substring(i, 1)) = False Then
                            strNewAccumGuType = (strNewAccumGuType + strAccumGu.Substring(i, 1)).Trim
                        End If
                    Next
                    ddldurationAccum.SelectedIndex = ddldurationAccum.Items.IndexOf(ddldurationAccum.Items.FindByText(strNewAccumGuType))

                    Dim strAccumKO As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.KO_Per).Text
                    txtKO.Text = strAccumKO

                    Dim strAccumKOSettl As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.KO_Settlement).Text
                    ddlKOSettlementType.SelectedValue = strAccumKOSettl

                    Dim strAccumType As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Type).Text
                    ddlAccumType.SelectedIndex = ddlAccumType.Items.IndexOf(ddlAccumType.Items.FindByValue(strAccumType.ToUpper))

                    Dim strAccumOrderqty As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Order_Quantity).Text
                    txtAccumOrderqty.Text = strAccumOrderqty

                    Dim strLevereageRatio As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Leverage_Ratio).Text

                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Show_AQDQ_Leverage_As_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                        Case "Y", "YES"
                            If strLevereageRatio = "No" Then
                                chkLeverageRatio.Checked = False
                            Else
                                chkLeverageRatio.Checked = True
                            End If
                        Case "N", "NO"
                            If strLevereageRatio = "1" Then
                                chkLeverageRatio.Checked = False
                            Else
                                chkLeverageRatio.Checked = True
                            End If
                    End Select
                    chkLeverageRatio_CheckedChanged(Nothing, Nothing)
                    grdAccumDecum.SelectedItemStyle.BackColor = Color.FromArgb(242, 242, 243)

                    DisplayEstimatedNotional()

                    '<Added by Mohit Lalwani on 4-Dec-2015>
                ElseIf e.CommandName.ToUpper = "CREATEPOOLACCDCC" Then
                    Dim strTradeDate As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Quoted_At).Text
                    If CDate(strTradeDate).ToString("dd-MMM-yy") = Today.ToString("dd-MMM-yy") Then
                        Dim strAccType As String = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Type).Text
                        If strAccType.ToUpper.Trim = "ACCUMULATOR" Then
                            Response.Redirect(sPoolRedirectionPath + "&PRD=ACCU&RFQID=" + DirectCast(DirectCast(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.RFQ_ID).Controls(1), System.Web.UI.Control), System.Web.UI.WebControls.LinkButton).Text, False)
                        ElseIf strAccType.ToUpper.Trim = "DECUMULATOR" Then
                            Response.Redirect(sPoolRedirectionPath + "&PRD=DECU&RFQID=" + DirectCast(DirectCast(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.RFQ_ID).Controls(1), System.Web.UI.Control), System.Web.UI.WebControls.LinkButton).Text, False)
                        End If
                    Else
                        lblerror.Text = "Cannot create pool from quotes other than today. Please use today's quotes only."
                    End If
                    '</Added by Mohit Lalwani on 4-Dec-2015>


                    '<RiddhiS. on 09-Oct-2016: To generate Pre-trade termsheet + KYIR on grid click>
                ElseIf e.CommandName.ToUpper = "GENERATEDOCUMENT" Then
                    Dim RFQID As String = DirectCast(DirectCast(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.RFQ_ID).Controls(1), System.Web.UI.Control), System.Web.UI.WebControls.LinkButton).Text
                    Generate_ACCDEC(RFQID)

                ElseIf e.CommandName.ToUpper = "GETRFQDETAILS" Then
                    ShowHideDeatils(True)
                    lblDetails.Text = "RFQ Details"
                    Label68.Text = "Issuer Remark"  'Added by Mohit/Rushi on 02-May-2016 FA-1420

                    pnlDetailsPopup.Visible = True

                    trAcDcStatusBak.Visible = False
                    trAcDcOrderType.Visible = False
                    trAcDcRefSpot.Visible = False
                    trAcDcExePrc1.Visible = False
                    trAcDcAvgExePrc.Visible = False
                    trAcDCQuoteStatus.Visible = True
                    tracdcOrderComment.Visible = False
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    lblAlloAcDcOrderStatus.Text = ""
                    lblAlloAcDcType.Text = ""
                    lblAlloAcDcRFQID.Text = ""
                    lblAlloAcDccp.Text = ""
                    lblAlloAcDcUnderlying.Text = ""
                    lblAlloAcDccurrency.Text = ""
                    lblAlloAcDcTenor.Text = ""
                    lblAlloAcDcKOper.Text = ""
                    lblAlloAcDcstrike.Text = ""
                    lblAlloAcDcupfront.Text = ""
                    lblAlloAcDcExePrc1.Text = ""
                    lblAlloAcDcAvgExePrc.Text = ""
                    lblAlloAcDcgearing.Text = ""
                    lblAlloAcDcfrequency.Text = ""
                    lblAlloAcDcGuarantee.Text = ""
                    lblAlloAcDcDNS.Text = ""
                    lblAlloAcDcAccDays.Text = ""
                    lblAlloAcDcTotalshares.Text = ""
                    lblAlloAcDcOrderType.Text = ""
                    lblAlloAcDcLimitPrice.Text = ""
                    lblAlloAcDcRemark.Text = ""
                    lblAlloAcDcSubmittedBy.Text = ""
                    lblAlloAcDcRefSpot.Text = ""
                    lblACDCValQuoteStatus.Text = ""
                    ''</added by Rushikesh on 29-Dec-15 to flush old value>
                    lblACDCValQuoteStatus.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Quote_Status).Text  'Mohit Lalwani on 19-Jan-2016
                    '  lblAlloAcDcOrderStatus.Text = item("Order_Status").Text
                    lblAlloAcDcType.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Type).Text  'Mohit Lalwani on 19-Jan-2016
                    lblAlloAcDcRFQID.Text = DirectCast(DirectCast(grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.RFQ_ID).Controls(1), System.Web.UI.Control), System.Web.UI.WebControls.LinkButton).Text
                    lblAlloAcDccp.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Provider_Name).Text
                    lblAlloAcDcUnderlying.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Share).Text
                    lblAlloAcDccurrency.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.CashCurrency).Text
                    lblAlloAcDcTenor.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Tenor).Text + "&nbsp;Month(s)"
                    lblAlloAcDcKOper.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.KO_Per).Text
                    lblAlloAcDcstrike.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Strike_Per).Text
                    lblAlloAcDcupfront.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Upfront).Text
                    ''lblAlloAcDcgearing.Text = item("LeverageRatio").Text
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Show_AQDQ_Leverage_As_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                        Case "Y", "YES"
                            If (grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Leverage_Ratio).Text.ToUpper = "NO") Then
                                lblAlloAcDcgearing.Text = "No"
                            Else
                                lblAlloAcDcgearing.Text = "Yes"
                            End If
                        Case "N", "NO"
                            If (grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Leverage_Ratio).Text = "1") Then
                                lblAlloAcDcgearing.Text = "No"
                            Else
                                lblAlloAcDcgearing.Text = "Yes"
                            End If
                    End Select
                    lblAlloAcDcfrequency.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Frequency).Text
                    lblAlloAcDcGuarantee.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.GU_Duration).Text
                    lblAlloAcDcDNS.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Order_Quantity).Text
                    lblAlloAcDcAccDays.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.EP_NumberOfDaysAccrual).Text
                    lblAlloAcDcTotalshares.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.TotalShares).Text
                    ' lblAlloAcDcOrderType.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.).Text
                    ' lblAlloAcDcLimitPrice.Text = item("LimitPrice1").Text
                    ''lblAlloAcDcRefSpot.Text = item("Quote_Request_ID").Text
                    lblAlloAcDcRemark.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.Remark).Text
                    lblAlloAcDcSubmittedBy.Text = grdAccumDecum.Items(e.Item.ItemIndex).Cells(grdAccumDeccumEnum.created_by).Text

                    'If item("ELN_Order_Type").Text.ToUpper = "LIMIT" Then
                    '    lblAlloAcDcRefSpot.Visible = True
                    '    lblAlloAcDcRefSpot.Text = item("LimitPrice1").Text
                    'Else
                    '    lblAlloAcDcRefSpot.Visible = False
                    'End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    upnlDetails.Update()
                    RestoreAll()
                    RestoreSolveAll()
                    upnlGrid.Update()
                    DisplayEstimatedNotional()
                Else
                    grdAccumDecum.SelectedItemStyle.BackColor = Color.White
                End If

            End If
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>

            manageShareReportShowHide()

            upnl3.Update()
            GetCommentary_Accum()
            'DisplayEstimatedNotional()   '<RiddhiS. on 17-Oct-2016: Function called only for SELECT and RFQDetails button click>
        Catch ex As Exception
            lblerror.Text = "grdAccumDecum_ItemCommand:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdAccumDecum_ItemCommand", ErrorLevel.High)

        End Try
    End Sub


    ''14Jan2016
    ''Rushikesh to set share value for grid selection and for other operations
    Private Sub setShare(ByVal strExchng As String, ByVal strShareVal As String)
        Try
            Dim dtSelectedShare As DataTable
            dtSelectedShare = Nothing
            Select Case objELNRFQ.Db_Get_Selected_Share("EQ", strExchng, "", strShareVal, dtSelectedShare)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlShareAccumDecum
                        .Items.Clear()
                        .DataSource = dtSelectedShare
                        .DataTextField = "LongName"
                        .DataValueField = "Code"
                        .DataBind()
                        .SelectedIndex = 0
                    End With
                    ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(strShareVal))
                    ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text      'Mohit Lalwani on 15-Apr-2016 FA-1392
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlShareAccumDecum
                        .DataSource = dtShare
                        .DataBind()
                    End With
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdAccumDecum_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdAccumDecum.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.EditItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                If e.Item.Cells(grdAccumDeccumEnum.Order_Quantity).Text <> "&nbsp;" Then
                    e.Item.Cells(grdAccumDeccumEnum.Order_Quantity).Text = SetNumberFormatZeroDecim(e.Item.Cells(grdAccumDeccumEnum.Order_Quantity).Text, 0) 'Added by Mohit Lalwani on 20-Aug-2015
                End If

                If e.Item.Cells(grdAccumDeccumEnum.Upfront).Text <> "&nbsp;" Then
                    e.Item.Cells(grdAccumDeccumEnum.Upfront).Text = (Val(e.Item.Cells(grdAccumDeccumEnum.Upfront).Text) / 100).ToString
                End If

                If e.Item.Cells(grdAccumDeccumEnum.Strike_Per).Text <> "&nbsp;" Then
                    e.Item.Cells(grdAccumDeccumEnum.Strike_Per).Text = SetNumberFormat(e.Item.Cells(grdAccumDeccumEnum.Strike_Per).Text, 2)
                End If
                If e.Item.Cells(grdAccumDeccumEnum.KO_Per).Text <> "&nbsp;" Then
                    e.Item.Cells(grdAccumDeccumEnum.KO_Per).Text = SetNumberFormat(e.Item.Cells(grdAccumDeccumEnum.KO_Per).Text, 2)
                End If

                If e.Item.Cells(grdAccumDeccumEnum.TotalShares).Text <> "&nbsp;" Then
                    e.Item.Cells(grdAccumDeccumEnum.TotalShares).Text = SetNumberFormatZeroDecim(e.Item.Cells(grdAccumDeccumEnum.TotalShares).Text, 0)   'Added by Mohit Lalwani on 20-Aug-2015
                End If

                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowRFQByClubbing_OnPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        If e.Item.ItemIndex = 0 Then
                            e.Item.CssClass = "Grp1"
                        Else
                            If (e.Item.Cells(grdAccumDeccumEnum.ClubbingRFQId).Text.ToString = "" Or e.Item.Cells(grdAccumDeccumEnum.ClubbingRFQId).Text.ToString = "&nbsp;") And (grdAccumDecum.Items(e.Item.ItemIndex - 1).Cells(grdAccumDeccumEnum.ClubbingRFQId).Text.ToString = "" Or grdAccumDecum.Items(e.Item.ItemIndex - 1).Cells(grdAccumDeccumEnum.ClubbingRFQId).Text.ToString = "&nbsp;") Then
                                e.Item.CssClass = If(grdAccumDecum.Items(e.Item.ItemIndex - 1).CssClass = "Grp2", "Grp1", "Grp2")
                            Else
                                If (e.Item.Cells(grdAccumDeccumEnum.ClubbingRFQId).Text.ToString = grdAccumDecum.Items(e.Item.ItemIndex - 1).Cells(grdAccumDeccumEnum.ClubbingRFQId).Text.ToString) Then
                                    e.Item.CssClass = grdAccumDecum.Items(e.Item.ItemIndex - 1).CssClass
                                Else
                                    e.Item.CssClass = If(grdAccumDecum.Items(e.Item.ItemIndex - 1).CssClass = "Grp2", "Grp1", "Grp2")
                                End If
                            End If

                        End If
                    Case "N", "NO"

                End Select

                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Show_AQDQ_Leverage_As_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"

                        If (e.Item.Cells(grdAccumDeccumEnum.Leverage_Ratio).Text.ToString = "1") Then
                            e.Item.Cells(grdAccumDeccumEnum.Leverage_Ratio).Text = "No"
                        Else
                            e.Item.Cells(grdAccumDeccumEnum.Leverage_Ratio).Text = "Yes"
                        End If

                    Case "N", "NO"

                End Select
                ''Added by Chitralekha on 1-Oct-16
                If e.Item.Cells(grdAccumDeccumEnum.BestPrice_YN).Text = "Y" Then
                    e.Item.CssClass = "BestPriceHighlight"

                End If
                ''Ended by Chitralekha on 1-Oct-16
            End If
            ' grdAccumDecum.Font.Size = CType(ddlFont.SelectedValue, FontUnit)

        Catch ex As Exception
            lblerror.Text = "grdAccumDecum_ItemDataBound:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdAccumDecum_ItemDataBound", ErrorLevel.High)

        End Try
    End Sub

    Private Sub grdAccumDecum_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdAccumDecum.PageIndexChanged
        Dim dtGrid As New DataTable("Dummy")
        Try
            grdAccumDecum.EditItemIndex = -1
            fill_Accum_Decum_Grid()
            grdAccumDecum.CurrentPageIndex = e.NewPageIndex
            dtGrid = CType(Session("Accum_Decum"), DataTable)
            grdAccumDecum.DataSource = dtGrid
            grdAccumDecum.DataBind()
        Catch ex As Exception
            lblerror.Text = "grdAccumDecum_PageIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdAccumDecum_PageIndexChanged", ErrorLevel.High)
        End Try
    End Sub



    Private Sub grdAccumDecum_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdAccumDecum.SortCommand
        Try
            numberdiv = CType(ViewState("NumberForSort_" + e.SortExpression), Int32)
            numberdiv = numberdiv + 1
            ViewState("NumberForSort_" + e.SortExpression) = numberdiv

            If CType(Session("Accum_Decum"), DataTable) Is Nothing Then Exit Sub

            Dim dtSortRevData As DataTable = CType(Session("Accum_Decum"), DataTable)
            Dim dvRevData As DataView
            dvRevData = dtSortRevData.DefaultView

            If (numberdiv Mod 2) = 0 Then
                dvRevData.Sort = e.SortExpression & " DESC"
            Else
                dvRevData.Sort = e.SortExpression & " ASC"
            End If

            grdAccumDecum.DataSource = dvRevData
            grdAccumDecum.DataBind()

        Catch ex As Exception
            lblerror.Text = "grdAccumDecum_SortCommand:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdAccumDecum_SortCommand", ErrorLevel.High)

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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
            If (txtTenorAccumDecum.Text.Contains(".")) Then
                lblerror.ForeColor = Color.Red
                lblerror.Text = "Please enter valid tenor(e.g. 1, 2, 3)."
                Chk_validation = False
                Exit Function
            End If
            '</AvinashG. on 17-Feb-2016: FA-1273 ELN/DRA/FCN/Acc/Dec: Invalid Tenor Issue >
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            If ddlShareAccumDecum.SelectedValue Is Nothing Then
                lblerror.Text = "Please select valid share. "
                Chk_validation = False
                Exit Function
            ElseIf ddlShareAccumDecum.SelectedValue = "" Then
                lblerror.Text = "Please select valid share."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If

            '<RiddhiS. on 16-Sep-2016: Validation to handle blank daily number of shares>
            If txtAccumOrderqty.Text = "" Then
                lblerror.ForeColor = Color.Red
                lblerror.Text = "Please enter valid No. Of Shares."
                Chk_validation = False
                Exit Function
                ''''Dilkhush 17Oct2016 to avoid -ve share from price
            Else
                If Val(txtAccumOrderqty.Text) < 0 Then
                    lblerror.Text = "Please enter valid No. Of Shares."
                    Chk_validation = False
                    Exit Function
                Else
                    Chk_validation = True
                End If
            End If

            If ddlSolveForAccumDecum.SelectedValue.Trim.ToUpper = "STRIKE" Then
                If (txtUpfront.Text = "") OrElse Val(txtUpfront.Text) = 0 OrElse Val(txtUpfront.Text.Replace(",", "")) >= 100 Then
                    lblerror.Text = " Please enter valid upfront. "
                    Chk_validation = False
                    Exit Function
                Else
                    Chk_validation = True
                End If
            Else
                If Not Val(txtStrikeaccum.Text) = 0 Then
                    Chk_validation = True
                Else
                    lblerror.Text = "Please enter valid strike %."
                    Chk_validation = False
                    Exit Function
                End If
                If ddlAccumType.SelectedValue.ToUpper = "ACCUMULATOR" Then
                    If Val(txtStrikeaccum.Text.Replace(",", "")) < 100 Then
                        Chk_validation = True
                    Else
                        lblerror.Text = "Strike % should be less than 100."
                        Chk_validation = False
                        Exit Function
                    End If
                ElseIf ddlAccumType.SelectedValue.ToUpper = "DECUMULATOR" Then
                    If Val(txtStrikeaccum.Text.Replace(",", "")) > 100 Then
                        Chk_validation = True
                    Else
                        lblerror.Text = "Strike % should be greater than 100."
                        Chk_validation = False
                        Exit Function
                    End If
                End If
                If (txtStrikeaccum.Text = "" OrElse Val(txtStrikeaccum.Text) = 0) Then
                    lblerror.Text = " Please enter valid strike. "
                    Chk_validation = False
                    Exit Function
                Else
                    Chk_validation = True
                End If
            End If

            If SetFrequencytype(ddlTenorTypeAccum.SelectedValue.Trim.ToUpper, txtTenorAccumDecum.Text.Trim, ddlFrequencyAccumDecum.SelectedValue.Trim) Then
                Chk_validation = True
            Else
                lblerror.Text = "Frequency is not valid."
                Chk_validation = False
                Exit Function
            End If


            If ddlAccumType.SelectedValue.ToUpper = "ACCUMULATOR" Then
                If Val(txtKO.Text.Replace(",", "")) > 100 Then
                    Chk_validation = True
                Else
                    lblerror.Text = "KO % should be greater than 100."
                    Chk_validation = False
                    Exit Function
                End If
            ElseIf ddlAccumType.SelectedValue.ToUpper = "DECUMULATOR" Then
                If txtKO.Text <> "" AndAlso (Val(txtKO.Text.Replace(",", "")) < 100 And Not Val(txtKO.Text) < 0) Then
                    Chk_validation = True
                Else
                    lblerror.Text = "KO % should be less than 100."
                    Chk_validation = False
                    Exit Function
                End If
            End If

            ''AshwiniP on 09-Nov-2016
            If ValidateTenor() = False Then
                lblerror.Text = "Please enter valid tenor."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If

            If ddlTenorTypeAccum.SelectedValue.Trim.ToUpper = "MONTH" Then
                If CheckMultiple(CInt(Val(txtTenorAccumDecum.Text)), 3) And CInt(Val(txtTenorAccumDecum.Text)) <= 24 Then
                    Chk_validation = True

                Else
                    lblerror.Text = "Tenor in months should be in multiples of 3 and less than or equal to 24."
                    Chk_validation = False
                    Exit Function
                End If
            End If
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'If Val(txtAccumOrderqty.Text) = 0 Then
            ' lblerror.Text = "Please enter valid Daily no. of shares."
            ' Chk_validation = False
            ' Exit Function
            'Else
            ' ''Accept notional
            ' Chk_validation = True
            'End If
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            If Val(txtTenorAccumDecum.Text) = 0 Then
                lblerror.Text = "Please enter valid tenor."
                Chk_validation = False
                Exit Function
            Else
                Chk_validation = True
            End If

            If ddlTenorTypeAccum.SelectedValue.Trim.ToUpper = "YEAR" Then
                If Val(txtTenorAccumDecum.Text.Trim) > 0 And Val(txtTenorAccumDecum.Text.Trim) <= 2 Then
                    Chk_validation = True
                Else
                    lblerror.Text = "Tenor in years should not be greater than 2."
                    Chk_validation = False
                    Exit Function
                End If
            End If
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
            FillRFQDataTable(PP_ID)
            'Get_AccumRFQData_TOXML(PP_ID)
            '</AvinashG. on 16-Feb-2016: Optimization>
            Quote_ID = Convert.ToString(Session("Quote_ID"))
            lblerror.Text = "RFQ " & Quote_ID & "  generated."
            lblerror.ForeColor = Color.Blue
            lblMsgPriceProvider.Text = ""
            If (PP_CODE = "JPM") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('JPMwait').style.visibility = 'visible';")
                Session.Add("JPMQuote", Quote_ID)
            ElseIf (PP_CODE = "HSBC") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('HSBCwait').style.visibility = 'visible';")
                Session.Add("HSBCQuote", Quote_ID)
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
            ElseIf (PP_CODE = "DB") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('DBIBwait').style.visibility = 'visible';")
                Session.Add("DBIBQuote", Quote_ID)
            End If

            If ddlSolveForAccumDecum.SelectedValue.ToUpper = "STRIKE" Then
                strJavaScripthdnSolveSingleRequest.AppendLine("getStrikeForAccum('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');")
            Else
                strJavaScripthdnSolveSingleRequest.AppendLine("getUpfrontForAccum('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');")
            End If
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScripthdnSolveSingleRequest", strJavaScripthdnSolveSingleRequest.ToString, True)
            grdAccumDecum.SelectedItemStyle.BackColor = Color.White
        Catch ex As Exception
            lblerror.Text = "solveRFQ:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "solveRFQ", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub btnhdnSolveSingleRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnhdnSolveSingleRequest.Click
        Try

            Session.Remove("flag")
            Session("flag") = ""
            setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice()


            '<RiddhiS. on 29-Sep-2016: To show Quote History Blotter on Price>
            rbHistory.SelectedValue = "Quote History"
            fill_Accum_Decum_Grid()
            makeThisGridVisible(grdAccumDecum)
            '</RiddhiS.>


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
                lblJPMlimit.Text = ""
                lblerror.Text = ""
                JpmHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("JPM", lblTimer, btnJPMDeal, btnJPMprice, lblJPMPrice)
            ElseIf Session("hdnPP").ToString = "HSBC" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnHSBCPrice.Enabled = False
                btnHSBCPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblHSBClimit.Text = ""
                lblHSBCPrice.Text = ""
                lblerror.Text = ""
                HsbcHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("HSBC", lblTimerHSBC, btnHSBCDeal, btnHSBCPrice, lblHSBCPrice)
            ElseIf Session("hdnPP").ToString = "OCBC" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblOCBClimit.Text = ""
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
                lblCITIlimit.Text = ""
                lblerror.Text = ""
                CITIHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("CITI", lblTimerCITI, btnCITIDeal, btnCITIPrice, lblCITIPrice)
            ElseIf Session("hdnPP").ToString = "LEONTEQ" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblLEONTEQPrice.Text = ""
                lblLEONTEQlimit.Text = ""
                lblerror.Text = ""
                LEONTEQHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("LEONTEQ", lblTimerLEONTEQ, btnLEONTEQDeal, btnLEONTEQPrice, lblLEONTEQPrice)
            ElseIf Session("hdnPP").ToString = "COMMERZ" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblCOMMERZPrice.Text = ""
                lblCOMMERZlimit.Text = ""
                lblerror.Text = ""
                COMMERZHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("COMMERZ", lblTimerCOMMERZ, btnCOMMERZDeal, btnCOMMERZPrice, lblCOMMERZPrice)
            ElseIf Session("hdnPP").ToString = "CS" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnCSPrice.Enabled = False
                btnCSPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblCSLimit.Text = ""
                lblCSPrice.Text = ""
                lblerror.Text = ""
                CsHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("CS", lblTimerCS, btnCSDeal, btnCSPrice, lblCSPrice)
            ElseIf Session("hdnPP").ToString = "UBS" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnUBSPrice.Enabled = False
                btnUBSPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblUBSlimit.Text = ""
                lblUBSPrice.Text = ""
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
                lblBNPPlimit.Text = ""
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
                lblBAMLlimit.Text = ""
                BAMLHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("BAML", lblTimerBAML, btnBAMLDeal, btnBAMLPrice, lblBAMLPrice)
            ElseIf Session("hdnPP").ToString = "DB" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnDBIBPrice.Enabled = False
                btnDBIBPrice.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblDBIBLimit.Text = ""
                lblerror.Text = ""
                lblDBIBPrice.Text = ""
                DBIBHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                solveRFQ("DB", lblTimerDBIB, btnDBIBDeal, btnDBIBPrice, lblDBIBPrice)
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
            ' Session.Remove("hdnPP") '<RiddhiS. on 27-Sep-2016: Session needed for DocGen>
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
            If btnJPMprice.Text <> "Order" Then
                Session.Add("hdnPP", "JPM")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                '' If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016  EQBOSDEV-314
                    Exit Sub
                End If
                If checkIssuerLimit("JPM") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("JPM")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
            If btnHSBCPrice.Text <> "Order" Then
                Session.Add("hdnPP", "HSBC")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check> 
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016  EQBOSDEV-314
                    Exit Sub
                End If
                If checkIssuerLimit("HSBC") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("HSBC")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
            If btnOCBCPrice.Text <> "Order" Then
                Session.Add("hdnPP", "OCBC")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                '' If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016 EQBOSDEV-314
                    Exit Sub
                End If
                If checkIssuerLimit("OCBC") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("OCBC")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
            If btnCITIPrice.Text <> "Order" Then
                Session.Add("hdnPP", "CITI")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016  EQBOSDEV-314
                    Exit Sub
                End If
                If checkIssuerLimit("CITI") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("CITI")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnCITIPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCITIPrice_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnLEONTEQPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLEONTEQPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            If btnLEONTEQPrice.Text <> "Order" Then
                Session.Add("hdnPP", "LEONTEQ")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016  EQBOSDEV-314
                    Exit Sub
                End If
                If checkIssuerLimit("LEONTEQ") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("LEONTEQ")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnLEONTEQPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnLEONTEQPrice_Click", ErrorLevel.High)
        End Try
    End Sub
    Public Sub btnCOMMERZPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCOMMERZPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            If btnCOMMERZPrice.Text <> "Order" Then
                Session.Add("hdnPP", "COMMERZ")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016  EQBOSDEV-314
                    Exit Sub
                End If
                If checkIssuerLimit("COMMERZ") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("COMMERZ")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
            If btnCSPrice.Text <> "Order" Then
                Session.Add("hdnPP", "CS")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016 EQBOSDEV-314
                    Exit Sub
                End If
                If checkIssuerLimit("CS") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("CS")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
            If btnDBIBPrice.Text <> "Order" Then
                Session.Add("hdnPP", "DB")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                ''If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016   EQBOSDEV-314
                    Exit Sub
                End If
                If checkIssuerLimit("DB") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("DB")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
            If btnUBSPrice.Text <> "Order" Then
                Session.Add("hdnPP", "UBS")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                'If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016   EQBOSDEV-314
                    Exit Sub
                End If
                If checkIssuerLimit("UBS") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("UBS")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                        fill_RFQRMList()    '''''Dilkhush 28Oct2016 Added to load RM on demand
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "btnUBSPrice_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnUBSPrice_Click", ErrorLevel.High)
        End Try
    End Sub

    Public Function Set_Order_Pop_Up_Items(ByVal Issuer As String) As Boolean
        Try
            'AvinashG. on 09-Jul-2016
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            lblPopupSCBPPValue.Visible = True ''<Added by Rushikesh on 16-Sep-16 for SCB JPM issuere>
            'Changed by Chitralekha on 22-Sep-16
            Session.Remove("dtACCDECPreTradeAllocation")
            Dim tempDt As DataTable
            tempDt = New DataTable("tempDt")
            tempDt.Columns.Add("RM_Name", GetType(String))
            tempDt.Columns.Add("Account_Number", GetType(String))
            tempDt.Columns.Add("AlloNotional", GetType(String))
            tempDt.Columns.Add("Cust_ID", GetType(String))
            tempDt.Columns.Add("DocId", GetType(String))
            tempDt.Columns.Add("EPOF_OrderId", GetType(String))
            tempDt.Rows.InsertAt(tempDt.NewRow(), 0)
            Session.Add("dtACCDECPreTradeAllocation", tempDt)
            grdRMData.DataSource = tempDt
            grdRMData.DataBind()
            For Each row As GridViewRow In grdRMData.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = True
                End If
                OnCheckedChanged(CType(grdRMData.Rows((0)).Cells(0).FindControl("CheckBox1"), CheckBox), Nothing)
            Next


            '/Changed by Chitralekha on 22-Sep-16
            lblIssuerPopUpValue.Text = Issuer
            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowHongKongForOrderPlacement", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            '    Case "Y", "YES"
            '        ddlBookingBranchPopUpValue.Enabled = True
            '    Case "N", "NO"
            '        ddlBookingBranchPopUpValue.Enabled = False
            'End Select
            ddlBasketSharesPopValue.Visible = False
            txtLimitPricePopUpValue.Width = New WebControls.Unit(175)
            ddlOrderTypePopUpValue.SelectedIndex = 0
            txtLimitPricePopUpValue.Text = "0"
            txtLimitPricePopUpValue.Enabled = False
            lblIssuerPopUpCaption.Text = "Counterparty"
            tdStrikeCaption.Visible = False
            tdStrikeValue.Visible = False
            tdTenorPopUpCaption.Visible = False
            tdTenorPopUpValue.Visible = False
            OptionalTRAccDeccum.Visible = True
            lblGuaranteePopUpValue.Text = ddlAccumGUDuration.SelectedItem.Text
            If chkLeverageRatio.Checked Then
                lblGearingPopUpValue.Text = "Yes"
            Else
                lblGearingPopUpValue.Text = "No"
            End If
            '</Added by Mohit Lalwani on 19-Nov-2015>
            ddlBasketSharesPopValue.Visible = False
            '<AvinashG. on 29-Jan-2015: FA-827, handle long RM Names >
            txtLimitPricePopUpValue.Width = New WebControls.Unit(175)
            'txtLimitPricePopUpValue.Width = New WebControls.Unit(115)
            '</AvinashG. on 29-Jan-2015: FA-827, handle long RM Names >
            ''</Rutuja S.20March:disable basket share dropdown for Accum/Decum>


            ddlOrderTypePopUpValue.SelectedIndex = 0 ''set market by default
            txtLimitPricePopUpValue.Text = "0"
            txtLimitPricePopUpValue.Enabled = False '<AvinashG. on 27-Mar-2014: >
            lblUnderlyingPopUpValue.Text = ddlShareAccumDecum.SelectedValue.ToString
            '<AvinashG on 11-Dec-2015  EQBOSDEV-174 - New UI for Order Confirmation Popup >
            lblProductNamePopUpValue.Text = If(ddlAccumType.SelectedItem.Text.ToUpper = "ACCUMULATOR", "Accumulator", "Decumulator")
            'lblProductNamePopUpValue.Text = "(" & ddlAccumType.SelectedItem.Text & ")"
            '</AvinashG on 11-Dec-2015  EQBOSDEV-174 - New UI for Order Confirmation Popup >
            lblNotionalPopUpCaption.Text = "Total Daily Shares"
            lblUpfrontPopUpCaption.Text = "Upfront (%)"
            lblIssuerPricePopUpCaption.Text = "Strike (%)"
            lblClientPricePopUpCaption.Text = "Tenor"
            lblClientYieldPopUpCaption.Text = "Frequency"
            txtUpfrontPopUpValue.Enabled = False
            lblNotionalPopUpValue.Text = txtAccumOrderqty.Text
            'Changed by Chitrlekha on 23-Sep-16
            lbltxtTotalShares.Text = lblNotionalPopUpValue.Text
            lbltxtAllocatedShares.Text = "0"
            lbltxtRemainingShares.Text = "0"
            ' lbltxtAllocatedShares.Text = CStr(TotalSum)
            lbltxtRemainingShares.Text = CStr(CDbl(lbltxtTotalShares.Text) - CDbl(lbltxtAllocatedShares.Text))
            '/Changed by Chitralekha on 23-Sep-16
            txtUpfrontPopUpValue.Text = txtUpfront.Text
            '<Added by Mohit Lalwani on 19-Nov-2015 EQBOSDEV-132 - The order confirmation box do not contain critical information like KO level and strike for FCN>
            lblKOPopUpValue.Text = txtKO.Text
            lblKOTypePopUpValue.Text = Label1.Text
            tdKICaption.Visible = False
            tdKIValue.Visible = False
            '</Added by Mohit Lalwani on 19-Nov-2015 EQBOSDEV-132 - The order confirmation box do not contain critical information like KO level and strike for FCN>
            ' lblCurrencyPopUpValue.Text = lblAQDQBaseCcy.Text       <%--Changed By Mohit On 23-Dec-2015 --%>
            lblClientPricePopUpValue.Text = txtTenorAccumDecum.Text & " " & ddlTenorTypeAccum.SelectedItem.Text
            lblClientYieldPopUpValue.Text = ddlFrequencyAccumDecum.SelectedItem.Text


            If ddlSolveForAccumDecum.SelectedItem.Text.Contains("Strike (%)") Then
                txtUpfrontPopUpValue.Text = txtUpfront.Text
                Select Case UCase(Issuer)
                    Case "JPM"
                        lblIssuerPricePopUpValue.Text = lblJPMPrice.Text
                    Case "CS"
                        lblIssuerPricePopUpValue.Text = lblCSPrice.Text

                    Case "UBS"
                        lblIssuerPricePopUpValue.Text = lblUBSPrice.Text

                    Case "HSBC"
                        lblIssuerPricePopUpValue.Text = lblHSBCPrice.Text

                    Case "OCBC"
                        lblIssuerPricePopUpValue.Text = lblOCBCPrice.Text
                    Case "CITI"
                        lblIssuerPricePopUpValue.Text = lblCITIPrice.Text
                    Case "LEONTEQ"
                        lblIssuerPricePopUpValue.Text = lblLEONTEQPrice.Text
                    Case "COMMERZ"
                        lblIssuerPricePopUpValue.Text = lblCOMMERZPrice.Text
                    Case "BAML"
                        lblIssuerPricePopUpValue.Text = lblBAMLPrice.Text

                    Case "BNPP"
                        lblIssuerPricePopUpValue.Text = lblBNPPPrice.Text

                    Case "DB"
                        lblIssuerPricePopUpValue.Text = lblDBIBPrice.Text
                End Select
            Else
                lblIssuerPricePopUpValue.Text = txtStrikeaccum.Text
                Select Case UCase(Issuer)
                    Case "JPM"
                        txtUpfrontPopUpValue.Text = lblJPMPrice.Text
                    Case "CS"
                        txtUpfrontPopUpValue.Text = lblCSPrice.Text

                    Case "UBS"
                        txtUpfrontPopUpValue.Text = lblUBSPrice.Text

                    Case "HSBC"
                        txtUpfrontPopUpValue.Text = lblHSBCPrice.Text
                    Case "OCBC"
                        txtUpfrontPopUpValue.Text = lblOCBCPrice.Text
                    Case "CITI"
                        txtUpfrontPopUpValue.Text = lblCITIPrice.Text
                    Case "LEONTEQ"
                        txtUpfrontPopUpValue.Text = lblLEONTEQPrice.Text
                    Case "COMMERZ"
                        txtUpfrontPopUpValue.Text = lblCOMMERZPrice.Text
                    Case "BAML"
                        txtUpfrontPopUpValue.Text = lblBAMLPrice.Text

                    Case "BNPP"
                        txtUpfrontPopUpValue.Text = lblBNPPPrice.Text
                    Case "DB"
                        txtUpfrontPopUpValue.Text = lblDBIBPrice.Text
                End Select
            End If

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

                    ' setRedirectedConfirmOrderPopUp(sROrderID)

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


    Public Sub btnBNPPPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBNPPPrice.Click
        Try
            ''<IMRAN/Dilkhush:30Dec2015: Commented to restore view state on last>
            ''RestoreSolveAll()
            ''RestoreAll()
            If btnBNPPPrice.Text <> "Order" Then
                Session.Add("hdnPP", "BNPP")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                'If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check> 
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016  EQBOSDEV-314
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
                btnBNPPPrice.Text = "Order"
            End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
            If btnBAMLPrice.Text <> "Order" Then
                Session.Add("hdnPP", "BAML")
                btnhdnSolveSingleRequest_Click(sender, e)
            Else
                '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
                'If txtAccumOrderqty.Text = "0" Or txtAccumOrderqty.Text = "0.00" Or txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Then ''<RushikeshD on 2-Feb-16 for not zero notional check>
                If txtAccumOrderqty.Text = "" Or txtAccumOrderqty.Text = "&nbsp;" Or Val(txtAccumOrderqty.Text) = 0 Then
                    lblerror.ForeColor = Color.Red
                    lblerror.Text = "Can not place Order. Daily No. of Shares cannot be 0." 'Notional is Changed By Mohit Lalwani to Daily No. of Shares on 1-Apr-2016   EQBOSDEV-314
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
                btnBAMLPrice.Text = "Order"
            End If
            ''<IMRAN/Dilkhush 30Dec2015: TO restore button view state>
            RestoreSolveAll()
            RestoreAll()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
                            ' If chkJPM.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "JPM" & "' ")
                                JPM_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(JPM_ID)
                                'Get_AccumRFQData_TOXML(JPM_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>

                                all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('JPMwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblJPMPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "','" + btnJPMprice.ClientID + "');")
                                    JpmHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('JPMwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblJPMPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "','" + btnJPMprice.ClientID + "');")
                                    JpmHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                strQuoteId1 = all_RFQ_IDs
                                getPPId.Add("JPM", JPM_ID)
                                getAllId.Add(JPM_ID, strQuoteId1)
                            'End If
                        End If
                    End If

                End If
                dr = dtLogin.Select("PP_CODE = '" & "CS" & "' ")
                If dr.Length > 0 Then
                    If btnCSPrice.Visible = True Then
                        If btnCSPrice.Enabled = True Then
                            'If chkCS.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "CS" & "' ")
                                CS_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(CS_ID)
                                'Get_AccumRFQData_TOXML(CS_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>

                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('CSwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCSPrice.ClientID + "','" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "','" + btnCSPrice.ClientID + "');")
                                    CsHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('CSwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCSPrice.ClientID + "','" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "','" + btnCSPrice.ClientID + "');")
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
                            ' End If
                        End If
                    End If
                End If
                dr = dtLogin.Select("PP_CODE = '" & "HSBC" & "' ")
                If dr.Length > 0 Then
                    If btnHSBCPrice.Visible = True Then
                        If btnHSBCPrice.Enabled = True Then
                            'If chkHSBC.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "HSBC" & "' ")
                                HSBC_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(HSBC_ID)
                                'Get_AccumRFQData_TOXML(HSBC_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('HSBCwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblHSBCPrice.ClientID + "','" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "','" + btnHSBCPrice.ClientID + "');")
                                    HsbcHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('HSBCwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblHSBCPrice.ClientID + "','" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "','" + btnHSBCPrice.ClientID + "');")
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
                dr = dtLogin.Select("PP_CODE = '" & "CITI" & "' ")
                If dr.Length > 0 Then
                    If btnCITIPrice.Visible = True Then
                        If btnCITIPrice.Enabled = True Then
                            'If chkCITI.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "CITI" & "' ")
                                CITI_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(CITI_ID)
                                'Get_AccumRFQData_TOXML(CITI_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('CITIwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCITIPrice.ClientID + "','" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "','" + btnCITIPrice.ClientID + "');")
                                    CITIHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('CITIwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCITIPrice.ClientID + "','" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "','" + btnCITIPrice.ClientID + "');")
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
                    If btnLEONTEQPrice.Visible = True Then
                        If btnLEONTEQPrice.Enabled = True Then
                            'If chkLEONTEQ.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "LEONTEQ" & "' ")
                                LEONTEQ_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(LEONTEQ_ID)
                                'Get_AccumRFQData_TOXML(LEONTEQ_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('LEONTEQwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblLEONTEQPrice.ClientID + "','" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "','" + btnLEONTEQPrice.ClientID + "');")
                                    LEONTEQHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('LEONTEQwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblLEONTEQPrice.ClientID + "','" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "','" + btnLEONTEQPrice.ClientID + "');")
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
                    If btnCOMMERZPrice.Visible = True Then
                        If btnCOMMERZPrice.Enabled = True Then
                            'If chkCOMMERZ.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "COMMERZ" & "' ")
                                COMMERZ_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(COMMERZ_ID)
                                'Get_AccumRFQData_TOXML(COMMERZ_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('COMMERZwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCOMMERZPrice.ClientID + "','" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "','" + btnCOMMERZPrice.ClientID + "');")
                                    COMMERZHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('COMMERZwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblCOMMERZPrice.ClientID + "','" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "','" + btnCOMMERZPrice.ClientID + "');")
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
                            ' End If
                        End If
                    End If
                End If
                dr = dtLogin.Select("PP_CODE = '" & "OCBC" & "' ")
                If dr.Length > 0 Then
                    If btnOCBCPrice.Visible = True Then
                        If btnOCBCPrice.Enabled = True Then
                            'If chkOCBC.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "OCBC" & "' ")
                                OCBC_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(OCBC_ID)
                                'Get_AccumRFQData_TOXML(OCBC_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('OCBCwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblOCBCPrice.ClientID + "','" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "','" + btnOCBCPrice.ClientID + "');")
                                    OCBCHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('OCBCwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblOCBCPrice.ClientID + "','" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "','" + btnOCBCPrice.ClientID + "');")
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
                dr = dtLogin.Select("PP_CODE = '" & "BAML" & "' ")
                If dr.Length > 0 Then
                    If btnBAMLPrice.Visible = True Then
                        If btnBAMLPrice.Enabled = True Then
                            'If chkBAML.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "BAML" & "' ")
                                BAML_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(BAML_ID)
                                'Get_AccumRFQData_TOXML(BAML_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('BAMLwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblBAMLPrice.ClientID + "','" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "','" + btnBAMLPrice.ClientID + "');")
                                    BAMLHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('BAMLwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblBAMLPrice.ClientID + "','" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "','" + btnBAMLPrice.ClientID + "');")
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
                            'If chkBNPP.Checked = True Then  ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "BNPP" & "' ")
                                BNPP_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(BNPP_ID)
                                'Get_AccumRFQData_TOXML(BNPP_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('BNPPwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblBNPPPrice.ClientID + "','" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "','" + btnBNPPPrice.ClientID + "');")
                                    BNPPHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('BNPPwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblBNPPPrice.ClientID + "','" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "','" + btnBNPPPrice.ClientID + "');")
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
                            'End If
                        End If
                    End If
                End If
                dr = dtLogin.Select("PP_CODE = '" & "UBS" & "' ")
                If dr.Length > 0 Then
                    If btnUBSPrice.Visible = True Then
                        If btnUBSPrice.Enabled = True Then
                            'If chkUBS.Checked = True Then  ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "UBS" & "' ")
                                UBS_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(UBS_ID)
                                'Get_AccumRFQData_TOXML(UBS_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('UBSwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblUBSPrice.ClientID + "','" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "','" + btnUBSPrice.ClientID + "');")
                                    UbsHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('UBSwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblUBSPrice.ClientID + "','" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "','" + btnUBSPrice.ClientID + "');")
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
                            'End If
                        End If
                    End If
                End If
                dr = dtLogin.Select("PP_CODE = '" & "DB" & "' ")
                If dr.Length > 0 Then
                    If btnDBIBPrice.Visible = True Then
                        If btnDBIBPrice.Enabled = True Then
                            ' If chkDBIB.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Remove chkBox Dependency >
                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "DB" & "' ")
                                DBIB_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(DBIB_ID)
                                'Get_AccumRFQData_TOXML(DBIB_ID)
                                '</AvinashG. on 16-Feb-2016: Optimization>
                                If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('DBIBwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getStrikeForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblDBIBPrice.ClientID + "','" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "','" + btnDBIBPrice.ClientID + "');")
                                    DBIBHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('DBIBwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getUpfrontForAccum('" + Convert.ToString(Session("Quote_ID")) + "','" + lblDBIBPrice.ClientID + "','" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "','" + btnDBIBPrice.ClientID + "');")
                                    DBIBHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                End If
                                If all_RFQ_IDs = "" Then
                                    all_RFQ_IDs = Convert.ToString(Session("Quote_ID"))
                                    strQuoteId1 = all_RFQ_IDs
                                Else
                                    all_RFQ_IDs = all_RFQ_IDs & "," & Convert.ToString(Session("Quote_ID"))
                                End If
                                getPPId.Add("DB", DBIB_ID)
                                getAllId.Add(DBIB_ID, Convert.ToString(Session("Quote_ID")))
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
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptAllRequest", "try{" + strJavaScriptAllRequest.ToString + "} catch(e){}", True)			'Added by Mohit Lalwani on 17-Oct-2016
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
            If txtAccumOrderqty.Text.Trim = "" Then   ''<Start | Nikhil M. on 17-Sep-2016: Added>
                lblerror.Text = "Invalid number of shares"
                Exit Sub
            End If
            ''<Start | Nikhil M. on 17-Sep-2016: Commented For Removing Chkbox dependency>
            'If chkJPM.Checked = True Then
            TRJPM1.Attributes.Remove("class")
            'End If
            'If chkHSBC.Checked = True Then
            TRHSBC1.Attributes.Remove("class")
            ' End If
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
            ''<End | Nikhil M. on 17-Sep-2016: Commented For Removing Chkbox dependency>
            'TRJPM1.Attributes.Remove("class")
            'TRHSBC1.Attributes.Remove("class")
            'TROCBC1.Attributes.Remove("class")
            'TRCITI1.Attributes.Remove("class")
            'TRLEONTEQ1.Attributes.Remove("class")
            'TRCOMMERZ1.Attributes.Remove("class")
            'TRUBS1.Attributes.Remove("class")
            'TRCS1.Attributes.Remove("class")
            'TRBNPP1.Attributes.Remove("class")
            'TRBAML1.Attributes.Remove("class")                        'Added by Imran 21-Aug-2015 for best price
            'TRDBIB.Attributes.Remove("class")                        'Added by Imran 21-Aug-2015 for best price
            hideShowRBLShareData()
            If (btnJPMprice.Visible = False Or btnJPMprice.Enabled = False) And _
            (btnHSBCPrice.Visible = False Or btnHSBCPrice.Enabled = False) And _
            (btnOCBCPrice.Visible = False Or btnOCBCPrice.Enabled = False) And _
            (btnCITIPrice.Visible = False Or btnCITIPrice.Enabled = False) And _
            (btnLEONTEQPrice.Visible = False Or btnLEONTEQPrice.Enabled = False) And _
            (btnCOMMERZPrice.Visible = False Or btnCOMMERZPrice.Enabled = False) And _
            (btnUBSPrice.Visible = False Or btnUBSPrice.Enabled = False) And _
            (btnCSPrice.Visible = False Or btnCSPrice.Enabled = False) And _
            (btnBAMLPrice.Visible = False Or btnBAMLPrice.Enabled = False) And _
            (btnBNPPPrice.Visible = False Or btnBNPPPrice.Enabled = False) And _
             (btnDBIBPrice.Visible = False Or btnDBIBPrice.Enabled = False) Then
                lblerror.Text = "All price providers are Off. Please try again later."
                Exit Sub
            End If

            ''<Start | Nikhil M. on 17-Sep-2016: Commented For Removing Chkbox dependency>
            ''      If (btnJPMprice.Visible = False Or chkJPM.Checked = False) And _
            ''      (btnHSBCPrice.Visible = False Or chkHSBC.Checked = False) And _
            ''      (btnUBSPrice.Visible = False Or chkUBS.Checked = False) And _
            ''      (btnCSPrice.Visible = False Or chkCS.Checked = False) And _
            ''      (btnBAMLPrice.Visible = False Or chkBAML.Checked = False) And _
            ''      (btnBNPPPrice.Visible = False Or chkBNPP.Checked = False) And _
            ''       (btnDBIBPrice.Visible = False Or chkDBIB.Checked = False) And _
            ''(btnOCBCPrice.Visible = False Or chkOCBC.Checked = False) And _
            ''(btnCITIPrice.Visible = False Or chkCITI.Checked = False) And _
            ''(btnLEONTEQPrice.Visible = False Or chkLEONTEQ.Checked = False) And _
            ''(btnCOMMERZPrice.Visible = False Or chkCOMMERZ.Checked = False) Then
            ''          lblerror.Text = "Please check valid price provider."
            ''          Exit Sub
            ''      End If
            If (btnJPMprice.Visible = False) And _
                 (btnHSBCPrice.Visible = False) And _
                 (btnUBSPrice.Visible = False) And _
                 (btnCSPrice.Visible = False) And _
                 (btnBAMLPrice.Visible = False) And _
                 (btnBNPPPrice.Visible = False) And _
                  (btnDBIBPrice.Visible = False) And _
                    (btnOCBCPrice.Visible = False) And _
                    (btnCITIPrice.Visible = False) And _
                    (btnLEONTEQPrice.Visible = False) And _
                    (btnCOMMERZPrice.Visible = False) Then
                lblerror.Text = "Please check valid price provider."
                Exit Sub
            End If
            AllHiddenPrice.Value = "Disable;Enable"
            RestoreSolveAll()
            RestoreAll()

            ''<Start | Nikhil M. on 17-Sep-2016: Commented For Removing Chkbox dependency>
            'If chkJPM.Checked = True Then
            JpmHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkHSBC.Checked = True Then
            HsbcHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkOCBC.Checked = True Then
            OCBCHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If

            'If chkCITI.Checked = True Then
            CITIHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkCS.Checked = True Then
            CsHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkUBS.Checked = True Then
            UbsHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkBNPP.Checked = True Then
            BNPPHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkBAML.Checked = True Then
            BAMLHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkDBIB.Checked = True Then
            DBIBHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkLEONTEQ.Checked = True Then
            LEONTEQHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If
            'If chkCOMMERZ.Checked = True Then
            COMMERZHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            ' End If
            ''<End | Nikhil M. on 17-Sep-2016: Commented For Removing Chkbox dependency>
            lblerror.Text = ""
            Solve_All_Requests(sender, e)
            fill_Accum_Decum_Grid()
            makeThisGridVisible(grdAccumDecum)
            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Call Fill_All_Charts()
            End If
            lblJPMPrice.Text = ""
            lblHSBCPrice.Text = ""
            lblOCBCPrice.Text = ""
            lblCITIPrice.Text = ""
            lblLEONTEQPrice.Text = ""
            lblCOMMERZPrice.Text = ""
            lblUBSPrice.Text = ""
            lblCSPrice.Text = ""
            lblBNPPPrice.Text = ""
            lblMsgPriceProvider.Text = ""
            lblBAMLPrice.Text = ""
            lblDBIBPrice.Text = ""
            btnBAMLPrice.Text = "Price"
            setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice()
            btnCSPrice.Text = "Price"
            btnJPMprice.Text = "Price"
            btnHSBCPrice.Text = "Price"
            btnOCBCPrice.Text = "Price"
            btnCITIPrice.Text = "Price"
            btnLEONTEQPrice.Text = "Price"
            btnCOMMERZPrice.Text = "Price"
            btnUBSPrice.Text = "Price"
            btnBNPPPrice.Text = "Price"
            btnDBIBPrice.Text = "Price"
            btnBNPPDeal.Visible = False
            btnBAMLDeal.Visible = False
            btnCSDeal.Visible = False
            btnJPMDeal.Visible = False
            btnHSBCDeal.Visible = False
            btnOCBCDeal.Visible = False
            btnCITIDeal.Visible = False
            btnLEONTEQDeal.Visible = False
            btnCOMMERZDeal.Visible = False
            btnUBSDeal.Visible = False
            btnDBIBDeal.Visible = False

            ''<Start | Nikhil M. on 17-Sep-2016: Commented For Removing Chkbox dependency>
            'If chkBAML.Checked = True Then
            btnBAMLPrice.Enabled = False
            btnBAMLPrice.CssClass = "btnDisabled"
            'End If

            'If chkCS.Checked = True Then
            btnCSPrice.Enabled = False
            btnCSPrice.CssClass = "btnDisabled"
            ' End If
            'If chkJPM.Checked = True Then
            btnJPMprice.Enabled = False
            btnJPMprice.CssClass = "btnDisabled"
            'End If
            'If chkHSBC.Checked = True Then
            btnHSBCPrice.Enabled = False
            btnHSBCPrice.CssClass = "btnDisabled"
            'End If
            'If chkOCBC.Checked = True Then
            btnOCBCPrice.Enabled = False
            btnOCBCPrice.CssClass = "btnDisabled"
            'End If
            'If chkCITI.Checked = True Then
            btnCITIPrice.Enabled = False
            btnCITIPrice.CssClass = "btnDisabled"
            'End If
            'If chkUBS.Checked = True Then
            btnUBSPrice.Enabled = False
            btnUBSPrice.CssClass = "btnDisabled"
            'End If
            'If chkBNPP.Checked = True Then
            btnBNPPPrice.Enabled = False
            btnBNPPPrice.CssClass = "btnDisabled"
            'End If
            'If chkDBIB.Checked = True Then
            btnDBIBPrice.Enabled = False
            btnDBIBPrice.CssClass = "btnDisabled"
            'End If
            'If chkLEONTEQ.Checked = True Then
            btnLEONTEQPrice.Enabled = False
            btnLEONTEQPrice.CssClass = "btnDisabled"
            'End If
            'If chkCOMMERZ.Checked = True Then
            btnCOMMERZPrice.Enabled = False
            btnCOMMERZPrice.CssClass = "btnDisabled"
            'End If
            ''<End | Nikhil M. on 17-Sep-2016: Commented For Removing Chkbox dependency>

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
            'Added by Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            '/Added by Mohit Lalwani on 1-Aug-2016
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

    Private Sub btnDealConfirm_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDealConfirm.ServerClick
        Try
            Stop_timer_Only()
            If Val(txtUpfrontPopUpValue.Text) > 0 And Val(txtUpfrontPopUpValue.Text.Replace(",", "")) < 100 Then
                Select Case UCase(lblIssuerPopUpValue.Text)
                    'Case "JPM"
                    '    btnJPMDeal_Click(sender, e)
                    'Case "CS"
                    '    btnCSDeal_Click(sender, e)
                    'Case "UBS"
                    '    btnUBSDeal_Click(sender, e)
                    'Case "HSBC"
                    '    btnHSBCDeal_Click(sender, e)
                    'Case "OCBC"
                    '    btnOCBCDeal_Click(sender, e)
                    'Case "CITI"
                    '    btnCITIDeal_Click(sender, e)
                    'Case "LEONTEQ"
                    '    btnLEONTEQDeal_Click(sender, e)
                    'Case "COMMERZ"
                    '    btnCOMMERZDeal_Click(sender, e)
                    'Case "BAML"
                    '    btnBAMLDeal_Click(sender, e)
                    'Case "BNPP"
                    '    btnBNPPDeal_Click(sender, e)
                    'Case "DB"
                    '    btnDBIBDeal_Click(sender, e)


                    Case "JPM"
                        If GetBestPriceConfirm(JpmHiddenPrice.Value, "JPM") Then
                            btnJPMDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "CS"
                        If GetBestPriceConfirm(CsHiddenPrice.Value, "Credit Suisse") Then
                            btnCSDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "UBS"
                        If GetBestPriceConfirm(UbsHiddenPrice.Value, "UBS") Then
                            btnUBSDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "HSBC"
                        If GetBestPriceConfirm(HsbcHiddenPrice.Value, "HSBC") Then
                            btnHSBCDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "OCBC"
                        If GetBestPriceConfirm(OCBCHiddenPrice.Value, "OCBC") Then
                            btnOCBCDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "CITI"
                        If GetBestPriceConfirm(CITIHiddenPrice.Value, "CITI") Then
                            btnCITIDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "LEONTEQ"
                        If GetBestPriceConfirm(LEONTEQHiddenPrice.Value, "LEONTEQ") Then
                            btnLEONTEQDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "COMMERZ"
                        If GetBestPriceConfirm(COMMERZHiddenPrice.Value, "COMMERZ") Then
                            btnCOMMERZDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "BAML"
                        If GetBestPriceConfirm(BAMLHiddenPrice.Value, "BAML") Then
                            btnBAMLDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "BNPP"
                        If GetBestPriceConfirm(BNPPHiddenPrice.Value, "BNPP") Then
                            btnBNPPDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                    Case "DB"
                        If GetBestPriceConfirm(DBIBHiddenPrice.Value, "DBIB") Then
                            btnDBIBDeal_Click(sender, e)
                            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                        End If

                End Select
                btnLoad_Click(sender, e)
            ElseIf Val(txtUpfrontPopUpValue.Text.Replace(",", "")) >= 100 Then
                lblerrorPopUp.Text = "Upfront should be less than 100."
                ''<Dilkhush 11Jan2016:- Added One condition to show valid msg>
            ElseIf Val(txtUpfrontPopUpValue.Text) = 0 Then
                lblerrorPopUp.Text = "Upfront should be greater than zero."
            Else
                lblerrorPopUp.Text = "Please enter valid Upfront."
            End If
            '<Changed By Mohit on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>

            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Or UCase(Request.QueryString("EXTLOD")) = "REDIRECTEDHEDGE" Then
                resetControlsForPool(True)
                tabPanelAQDQ.Enabled = True
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
            '</Changed By Mohit on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
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
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
                ShowHideConfirmationPopup(False)
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerLEONTEQ.ClientID + "','" + btnLEONTEQDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCOMMERZ.ClientID + "','" + btnCOMMERZDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');") 'Imran Need to check if macq gives error due to JS 6-June-14
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');") '
                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "StopAllTimers", strJavaScriptStopTimer.ToString, True)
                lblerror.Text = ""
                Exit Sub
            End If


            ShowHideConfirmationPopup(False, "NO")


            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()

            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimer.ClientID + "','" + btnJPMDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerHSBC.ClientID + "','" + btnHSBCDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerOCBC.ClientID + "','" + btnOCBCDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCITI.ClientID + "','" + btnCITIDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerCS.ClientID + "','" + btnCSDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblUBSTimer.ClientID + "','" + btnUBSDeal.ClientID + "');")
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBAML.ClientID + "','" + btnBAMLDeal.ClientID + "');") 'Imran Need to check if macq gives error due to JS 6-June-14
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerDBIB.ClientID + "','" + btnDBIBDeal.ClientID + "');")
            'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "StopAllTimers", strJavaScriptStopTimer.ToString, True)
            'ResetAll()
            '/Mohit Lalwani on 1-Aug-2016
            lblerror.Text = ""
            '<Changed By Mohit on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
            If Not IsNothing(Request.QueryString("RedirectedOrderId")) Then
                resetControlsForPool(True)
                tabPanelAQDQ.Enabled = True
                Dim isreadonly As Reflection.PropertyInfo = GetType(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
                ' make collection editable
                isreadonly.SetValue(Me.Request.QueryString, False, Nothing)
                ' remove
                Me.Request.QueryString.Remove("EXTLOD")
                Me.Request.QueryString.Remove("PRD")
                Me.Request.QueryString.Remove("RedirectedOrderId")
                Me.Request.QueryString.Remove("PoolID")
                'Request.QueryString.Remove("EXTLOD")
                'Request.QueryString.Remove("PRD")
                'Request.QueryString.Remove("RedirectedOrderId")
            End If
            '</Changed By Mohit on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
            'Changed by Chitralekha M on 21-Sep-16
            lblTotalShares.Visible = False
            lbltxtTotalShares.Visible = False
            lblAllocatedShares.Visible = False

            lbltxtAllocatedShares.Visible = False
            lblRemainingShares.Visible = False
            lbltxtRemainingShares.Visible = False
            'Changed by Chitralekha M on 21-Sep-16
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
            'Added by Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            '/Added by Mohit Lalwani on 1-Aug-2016

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


    Private Sub ResetAll()
        Try
            '<RiddhiS. Blotter does not need to be reset>
            '<RiddhiS. on 29-Sep-2016: To show Order History Blotter on Reset>
            'rbHistory.SelectedValue = "Order History"
            'fill_OrderGrid()
            'makeThisGridVisible(grdOrder)
            '</RiddhiS.>
            '</RiddhiS.>

            Enable_Disable_Deal_Buttons()
            btnSolveAll.Enabled = True
            btnSolveAll.CssClass = "btn"
            chk_Login_For_PP()
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
            If btnOCBCPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnOCBCDeal.ClientID + "');")
            End If
            If btnCITIPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnCITIDeal.ClientID + "');")
            End If
            If btnLEONTEQPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnLEONTEQDeal.ClientID + "');")
            End If
            If btnCOMMERZPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnCOMMERZDeal.ClientID + "');")
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
            If btnLEONTEQPrice.Enabled Then
                lblLEONTEQClientYield.Text = ""
                lblLEONTEQClientPrice.Text = ""
                lblLEONTEQPrice.Text = ""
                lblLEONTEQPrice.ForeColor = System.Drawing.Color.Green
                LEONTEQHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnLEONTEQPrice.Text = "Price"
                btnLEONTEQPrice.CssClass = "btn"
            Else
                lblLEONTEQClientYield.Text = ""
                lblLEONTEQClientPrice.Text = ""
                lblLEONTEQPrice.Text = ""
                LEONTEQHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            End If
            If btnCOMMERZPrice.Enabled Then
                lblCOMMERZClientYield.Text = ""
                lblCOMMERZClientPrice.Text = ""
                lblCOMMERZPrice.Text = ""
                lblCOMMERZPrice.ForeColor = System.Drawing.Color.Green
                COMMERZHiddenPrice.Value = ";Enable;Disable;Disable;Price"
                btnCOMMERZPrice.Text = "Price"
                btnCOMMERZPrice.CssClass = "btn"
            Else
                lblCOMMERZClientYield.Text = ""
                lblCOMMERZClientPrice.Text = ""
                lblCOMMERZPrice.Text = ""
                COMMERZHiddenPrice.Value = ";Disable;Disable;Disable;Price"
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
            strJavaScript.AppendLine("StopPolling();")
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "StopPolling", "try{" + strJavaScript.ToString + "}catch(e){}", True) 'Added by Mohit Lalwani on 17-Oct-2016
            DealConfirmPopup.Visible = False
            UPanle11111.Update()
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
            End If
            TRJPM1.Attributes.Remove("class")
            TRHSBC1.Attributes.Remove("class")
            TROCBC1.Attributes.Remove("class")
            TRCITI1.Attributes.Remove("class")
            TRLEONTEQ1.Attributes.Remove("class")
            TRCOMMERZ1.Attributes.Remove("class")
            TRUBS1.Attributes.Remove("class")
            TRCS1.Attributes.Remove("class")
            TRBNPP1.Attributes.Remove("class")
            TRBAML1.Attributes.Remove("class") ''Added by imran on 21Aug2015 for bestprice
            TRDBIB.Attributes.Remove("class") ''Added by imran on 21Aug2015 for bestprice
            hideShowRBLShareData()
            ResetMinMaxNotional() '<Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
        Catch ex As Exception
            lblerror.Text = "ResetAll:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ResetAll", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    '<Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> 
    Private Sub ResetMinMaxNotional()
        lblDBIBLimit.Text = ""
        lblDBIBLimit.ToolTip = ""
        lblDBIBLimit.Visible = False
        lblJPMlimit.Text = ""
        lblJPMlimit.ToolTip = ""
        lblJPMlimit.Visible = False
        lblHSBClimit.Text = ""
        lblHSBClimit.ToolTip = ""
        lblHSBClimit.Visible = False

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
        lblRangeCcy.Text = ""
    End Sub
    '<\Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> 

    Private Sub RestoreAll()
        Try
            Dim iTabIdx As Integer = tabContainer.ActiveTabIndex
            Dim ddlPrdSolveFor As Telerik.Web.UI.RadDropDownList
            ddlPrdSolveFor = ddlSolveForAccumDecum
            Dim strJavaScriptRestoreAll As New StringBuilder
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            strJavaScriptRestoreAll.AppendLine(Restore(JpmHiddenPrice, lblJPMPrice, lblJPMClientPrice, lblJPMClientYield, iTabIdx, ddlPrdSolveFor, btnJPMprice, btnJPMDoc, JpmHiddenAccDays, lblJPMlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(HsbcHiddenPrice, lblHSBCPrice, lblHSBCClientPrice, lblHSBCClientYield, iTabIdx, ddlPrdSolveFor, btnHSBCPrice, btnHSBCDoc, HsbcHiddenAccDays, lblHSBClimit))
            strJavaScriptRestoreAll.AppendLine(Restore(OCBCHiddenPrice, lblOCBCPrice, lblOCBCClientPrice, lblOCBCClientYield, iTabIdx, ddlPrdSolveFor, btnOCBCPrice, btnOCBCDoc, OCBCHiddenAccDays, lblOCBClimit))
            strJavaScriptRestoreAll.AppendLine(Restore(CITIHiddenPrice, lblCITIPrice, lblCITIClientPrice, lblCITIClientYield, iTabIdx, ddlPrdSolveFor, btnCITIPrice, btnCITIDoc, CITIHiddenAccDays, lblCITIlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(LEONTEQHiddenPrice, lblLEONTEQPrice, lblLEONTEQClientPrice, lblLEONTEQClientYield, iTabIdx, ddlPrdSolveFor, btnLEONTEQPrice, btnLEONTEQDoc, LEONTEQHiddenAccDays, lblLEONTEQlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(COMMERZHiddenPrice, lblCOMMERZPrice, lblCOMMERZClientPrice, lblCOMMERZClientYield, iTabIdx, ddlPrdSolveFor, btnCOMMERZPrice, btnCOMMERZDoc, COMMERZHiddenAccDays, lblCOMMERZlimit))

            strJavaScriptRestoreAll.AppendLine(Restore(UbsHiddenPrice, lblUBSPrice, lblUBSClientPrice, lblUBSClientYield, iTabIdx, ddlPrdSolveFor, btnUBSPrice, btnUBSDoc, UbsHiddenAccDays, lblUBSlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(CsHiddenPrice, lblCSPrice, lblCSClientPrice, lblCSClientYield, iTabIdx, ddlPrdSolveFor, btnCSPrice, btnCSDoc, CsHiddenAccDays, lblCSLimit))
            strJavaScriptRestoreAll.AppendLine(Restore(BAMLHiddenPrice, lblBAMLPrice, lblBAMLClientPrice, lblBAMLClientYield, iTabIdx, ddlPrdSolveFor, btnBAMLPrice, btnBAMLDoc, BAMLHiddenAccDays, lblBAMLlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(BNPPHiddenPrice, lblBNPPPrice, lblBNPPClientPrice, lblBNPPClientYield, iTabIdx, ddlPrdSolveFor, btnBNPPPrice, btnBNPPDoc, BNPPHiddenAccDays, lblBNPPlimit))
            strJavaScriptRestoreAll.AppendLine(Restore(DBIBHiddenPrice, lblDBIBPrice, lblDBIBClientPrice, lblDBIBClientYield, iTabIdx, ddlPrdSolveFor, btnDBIBPrice, btnDBIBDoc, DBIBHiddenAccDays, lblDBIBLimit))

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
            ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
            lblerror.Text = ""
            If UCase(Request.QueryString("EXTLOD")) = "REPRICEPOOL" Or UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("ACCDECRePricePPName")))
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
'Mohit Lalwnai 
    Private Sub ShowHideDeatils(ByVal blnShowPopup As Boolean)
        Try
            panelAQDQ.Enabled = Not blnShowPopup
            upnl3.Update()
            If TRJPM1.Visible Then
                TRJPM1.Disabled = blnShowPopup
            End If

            If TRHSBC1.Visible Then
                TRHSBC1.Disabled = blnShowPopup
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
            ''Start Added By Nikhil M 08Aug16 For Booking Brach DropDown EQSCB-16
            Dim DtCommanData As New DataTable("DtCommanData")
            ddlBookingBranchPopUpValue.Items.Clear() ''<Nikhil M. on 20-Sep-2016: Added For Clearing Dropdown>

            Dim strQuerySelect As String = "entity_id = '" + LoginInfoGV.Login_Info.EntityID.ToString + "'"
            Select Case ObjCommanData.DB_Get_Common_Data("EQC_SCB_BookingCenters", DtCommanData)  '<RiddhiS. on 14-Oct-2016: Config name changed>
                Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                    If DtCommanData.Rows.Count > 0 Then
                        If DtCommanData.Select(strQuerySelect).Length > 0 Then
                            ddlBookingBranchPopUpValue.DataSource = DtCommanData.Select(strQuerySelect).CopyToDataTable
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
            '' End Added By Nikhil M 08Aug16 For Booking Brach DropDown EQSCB-16

            UPanle11111.Update()
            panelAQDQ.Enabled = Not blnShowPopup
            upnl3.Update()
            If TRJPM1.Visible Then
                TRJPM1.Disabled = blnShowPopup
            End If

            If TRHSBC1.Visible Then
                TRHSBC1.Disabled = blnShowPopup
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

    Private Sub ddlAccumGUDuration_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAccumGUDuration.SelectedIndexChanged
        Try
            lblerror.Text = ""
            txtDuration.Text = ddlAccumGUDuration.SelectedValue
            'GetCommentary_Accum()
            ResetCommetryElement() '<RiddhiS. on 30-Sep-2016: To avoid change in Strike/Upfront in commentary when control value is changed>
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "ddlAccumGUDuration_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlAccumGUDuration_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txtLimitPricePopUpValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLimitPricePopUpValue.TextChanged
        Try
            'Added by Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            '/Added by Mohit Lalwani on 1-Aug-2016
            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            'If (txtLimitPricePopUpValue.Text.Length - (txtLimitPricePopUpValue.Text.LastIndexOf(".") + 1)) > 4  Then
            ''If (txtLimitPricePopUpValue.Text.Length - (txtLimitPricePopUpValue.Text.LastIndexOf(".") + 1)) > 4 And CDbl(txtLimitPricePopUpValue.Text) <> Math.Floor(CDbl(txtLimitPricePopUpValue.Text)) Then
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
            grdAccumDecum.Visible = False
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
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopPriceAllWaitLoader", "try{document.getElementById('PriceAllWait').style.visibility = 'hidden';}catch(e){}", True)   'Added by Mohit Lalwani on 17-Oct-2016
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
                        ByVal lblIssuerClientYield As Label, ByVal tabIndex As Integer, ByRef ddlPrdSolveFor As RadDropDownList, _
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
                        If grdAccumDecum.Columns(grdAccumDeccumEnum.GenerateDoc).Visible = False Then
                            btnDoc.Visible = False '<RiddhiS. on 17-Sep-2016 to show Document link button when price arrives />
                        Else
                            btnDoc.Visible = True '<RiddhiS. on 17-Sep-2016 to show Document link button when price arrives />
                        End If
                        '</AvinashG. on 27-Sep-2016: TEMPORARY---------------------------->
                        If (Not String.IsNullOrEmpty(HiddenAccDaysCsv.Value)) Then
                            If ddlPrdSolveFor.SelectedValue.ToUpper = "UPFRONT" Then
                                lblIssuerPrice.Text = (Val(Split(HiddenAccDaysCsv.Value, ";")(0)) / 100).ToString
                                '<Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>       
                                ''Dilkhush/Avinash 22Feb2016 added if condition to handle string to double conversion
                                '' lblMinMaxSize.Text = convertNotionalintoShort(CDbl(Split(PriceArray(0).ToString, ",")(1)), "MIN") + " / " + convertNotionalintoShort(CDbl(Split(PriceArray(0).ToString, ",")(2)), "MAX") '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                                lblMinMaxSize.Text = convertNotionalintoShort((If(Split(PriceArray(0).ToString, ",")(1).ToString = "", 0, CDbl(Split(PriceArray(0).ToString, ",")(1)))), "MIN") + " / " + convertNotionalintoShort((If(Split(PriceArray(0).ToString, ",")(2).ToString = "", 0, CDbl(Split(PriceArray(0).ToString, ",")(2)))), "MAX") '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                                lblMinMaxSize.ToolTip = Split(PriceArray(0).ToString, ",")(1) + " / " + Split(PriceArray(0).ToString, ",")(2) '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                                lblMinMaxSize.Visible = True
                                setMinMaxCurrency()
                                '<\Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> 

                            Else
                                lblIssuerPrice.Text = Split(HiddenAccDaysCsv.Value, ";")(0)
                                '<Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>       
                                ''Dilkhush/Avinash 22Feb2016 added if condition to handle string to double conversion
                                '' lblMinMaxSize.Text = convertNotionalintoShort(CDbl(Split(PriceArray(0).ToString, ",")(1)), "MIN") + " / " + convertNotionalintoShort(CDbl(Split(PriceArray(0).ToString, ",")(2)), "MAX") ' + "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                                lblMinMaxSize.Text = convertNotionalintoShort((If(Split(PriceArray(0).ToString, ",")(1).ToString = "", 0, CDbl(Split(PriceArray(0).ToString, ",")(1)))), "MIN") + " / " + convertNotionalintoShort((If(Split(PriceArray(0).ToString, ",")(2).ToString = "", 0, CDbl(Split(PriceArray(0).ToString, ",")(2)))), "MAX") '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                                lblMinMaxSize.ToolTip = Split(PriceArray(0).ToString, ",")(1) + " / " + Split(PriceArray(0).ToString, ",")(2) '+ "(" + Split(PriceArray(0).ToString, ",")(3) + ")"       
                                lblMinMaxSize.Visible = True
                                setMinMaxCurrency()
                                '<\Added By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional> 

                            End If
                            Try
                                lblIssuerClientPrice.Text = Split(HiddenAccDaysCsv.Value, ";")(1)
                                lblIssuerClientYield.Text = FormatNumber(CDbl(Split(HiddenAccDaysCsv.Value, ";")(1)) * Val(Replace(txtAccumOrderqty.Text, ",", "")), 0)
                            Catch ex As Exception
                                lblerror.Text = "Unable to get NumberOfAccrualDays from issuer"
                            End Try
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
                        Restore = "try{document.getElementById('" & waitCtrlId & "').style.visibility = 'hidden';}catch(e){}"   'Added by Mohit Lalwani on 17-Oct-2016
                    Else
                        Restore = "try{document.getElementById('" & waitCtrlId & "').style.visibility = 'visible';}catch(e){}"    ' Added by Mohit Lalwani on 17-Oct-2016
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
       
        objELNRFQ.GetShareQuoteCcy(ddlShareAccumDecum.SelectedValue.ToString, ExchangeCcy)
        lblRangeCcy.Text = "Min/Max(<B>" + ExchangeCcy + "</B>)" '<AvinashG. on 18-Apr-2016: EQBOSDEV-328  Min/Max(CCY) label is not displayed for Accum Decum  >
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
		                        hdnBestProvider.Value = "JPM"
                        hdnBestStrike.Value = lblJPMPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
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
hdnBestProvider.Value = "HSBC"
                        hdnBestStrike.Value = lblHSBCPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
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
hdnBestProvider.Value = "UBS"
                        hdnBestStrike.Value = lblUBSPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
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
hdnBestProvider.Value = "CS"
                        hdnBestStrike.Value = lblCSPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
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
hdnBestProvider.Value = "BNPP"
                        hdnBestStrike.Value = lblBNPPPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("BAML")) Then
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
hdnBestProvider.Value = "BAML"
                        hdnBestStrike.Value = lblBAMLPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
                    End If
                    ElseIf (temp.ID.ToUpper.Contains("DBIB")) Then ''<Do not replace comparing ID>
                        TRJPM1.Attributes.Remove("class")
                        TRHSBC1.Attributes.Remove("class")
                        TRUBS1.Attributes.Remove("class")
                        TRCS1.Attributes.Remove("class")
                        TRBNPP1.Attributes.Remove("class")
                        TRDBIB.Attributes.Add("class", "lblBestPrice")
                        TROCBC1.Attributes.Remove("class")
                        TRCITI1.Attributes.Remove("class")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCOMMERZ1.Attributes.Remove("class")
                    ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
          Not chkCS.Checked And Not chkBNPP.Checked And Not chkBAML.Checked And Not chkOCBC.Checked And _
          Not chkCITI.Checked And Not chkLEONTEQ.Checked And Not chkCOMMERZ.Checked Then
hdnBestProvider.Value = "DBIB"
                        hdnBestStrike.Value = lblDBIBPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
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
hdnBestProvider.Value = "OCBC"
                        hdnBestStrike.Value = lblOCBCPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
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
hdnBestProvider.Value = "CITI"
                        hdnBestStrike.Value = lblCITIPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
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
                        TRLEONTEQ1.Attributes.Add("class", "lblBestPrice")
                        TRCOMMERZ1.Attributes.Remove("class")
                    TRCITI1.Attributes.Remove("class")
                    ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
       Not chkCS.Checked And Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And _
       Not chkOCBC.Checked And Not chkCITI.Checked And Not chkCOMMERZ.Checked Then
hdnBestProvider.Value = "LEONTEQ"
                        hdnBestStrike.Value = lblLEONTEQPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
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
                        TRCOMMERZ1.Attributes.Add("class", "lblBestPrice")
                        TRLEONTEQ1.Attributes.Remove("class")
                    TRCITI1.Attributes.Remove("class")
                    ''<Nikhil M : 28Sep16 >
                    If Not chkJPM.Checked And Not chkHSBC.Checked And Not chkUBS.Checked And _
                        Not chkCS.Checked And Not chkBNPP.Checked And Not chkBAML.Checked And Not chkDBIB.Checked And _
                        Not chkOCBC.Checked And Not chkCITI.Checked And Not chkLEONTEQ.Checked Then
hdnBestProvider.Value = "COMMERZ"
                        hdnBestStrike.Value = lblCOMMERZPrice.Text ''<Nikhil M. on 17-Sep-2016: added for Commetry>
                    End If
                    End If
                    GetCommentary_Accum()
                    checkDuplicate(temp)
                End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "CheckBestPrice", ErrorLevel.High)
            Throw ex
        End Try
    End Function
    '23-Nov-2015 Added by Imran/Mangesh for Email best issuer

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
    'Added by Imran Patel 19-Aug-2015 BOS Req
    Public Function getBestPrice(ByVal hiddenPriceCsv1 As HiddenField, ByVal hiddenPriceCsv2 As HiddenField) As HiddenField
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
            If ddlSolveForAccumDecum.SelectedValue.ToUpper = "UPFRONT" Then
                If Value1 = 0.0 And Value2 = 0.0 Then
                    Return New HiddenField()
                End If

                If Value1 = 0.0 And Value2 > 0.0 Then
                    Return hiddenPriceCsv2
                ElseIf Value2 = 0.0 And Value1 > 0.0 Then
                    Return hiddenPriceCsv1
                End If

                If Value2 > Value1 Then
                    Return hiddenPriceCsv2
                Else
                    Return hiddenPriceCsv1
                End If
            Else
                If Value1 = 0.0 And Value2 = 0.0 Then
                    Return New HiddenField()
                End If

                If Value1 = 0.0 And Value2 > 0.0 Then
                    Return hiddenPriceCsv2
                ElseIf Value2 = 0.0 And Value1 > 0.0 Then
                    Return hiddenPriceCsv1
                End If

                If ddlAccumType.SelectedValue.ToUpper = "ACCUMULATOR" Then
                    If Value2 > Value1 Then
                        Return hiddenPriceCsv1
                    Else
                        Return hiddenPriceCsv2
                    End If
                Else
                    If Value2 > Value1 Then
                        Return hiddenPriceCsv2
                    Else
                        Return hiddenPriceCsv1
                    End If
                End If
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
        ElseIf (temp.ID.ToUpper.Contains("OCBC")) Then
            TROCBC1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("CITI")) Then
            TRCITI1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("LEONTEQ")) Then
            TRLEONTEQ1.Attributes.Add("class", "lblBestPrice")
        ElseIf (temp.ID.ToUpper.Contains("COMMERZ")) Then
            TRCOMMERZ1.Attributes.Add("class", "lblBestPrice")
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
        End If
    End Function

    Public Sub getRange()
        Dim Product_name As String
        Dim ExchangeCcy As String
        Dim dtRange As DataTable
        Try
            dtRange = New DataTable("RangeLimit")
            Product_name = ddlAccumType.SelectedValue

            If Product_name.ToUpper = "ACCUMULATOR" Then
                Product_name = "Accumulator"
            Else
                Product_name = "Decumulator"
            End If
            objELNRFQ.GetShareQuoteCcy(ddlShareAccumDecum.SelectedValue.ToString, ExchangeCcy)
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
                        getLimit("DB", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblDBIBLimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblDBIBLimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")
                        getLimit("BAML", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblBAMLlimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblBAMLlimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")

                    Else
                        lblJPMlimit.Text = "N.A."
                        lblBNPPlimit.Text = "N.A."
                        lblUBSlimit.Text = "N.A."
                        lblBNPPlimit.Text = "N.A."
                        lblHSBClimit.Text = "N.A."

                        lblOCBClimit.Text = "N.A."
                        lblCITIlimit.Text = "N.A."
                        lblLEONTEQlimit.Text = "N.A."
                        lblCOMMERZlimit.Text = "N.A."

                        lblCSLimit.Text = "N.A."
                        lblBAMLlimit.Text = "N.A."
                        lblDBIBLimit.Text = "N.A."
                        lblJPMlimit.ToolTip = ""
                        lblBNPPlimit.ToolTip = ""
                        lblUBSlimit.ToolTip = ""
                        lblBNPPlimit.ToolTip = ""
                        lblHSBClimit.ToolTip = ""
                        lblOCBClimit.ToolTip = ""
                        lblCITIlimit.ToolTip = ""
                        lblLEONTEQlimit.ToolTip = ""
                        lblCOMMERZlimit.ToolTip = ""
                        lblCSLimit.ToolTip = ""
                        lblBAMLlimit.ToolTip = ""
                        lblDBIBLimit.ToolTip = ""
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    lblJPMlimit.Text = "N.A."
                    lblBNPPlimit.Text = "N.A."
                    lblUBSlimit.Text = "N.A."
                    lblBNPPlimit.Text = "N.A."
                    lblHSBClimit.Text = "N.A."
                    lblOCBClimit.Text = "N.A."
                    lblCITIlimit.Text = "N.A."
                    lblLEONTEQlimit.Text = "N.A."
                    lblCOMMERZlimit.Text = "N.A."
                    lblCSLimit.Text = "N.A."
                    lblBAMLlimit.Text = "N.A."
                    lblDBIBLimit.Text = "N.A."
                    lblJPMlimit.ToolTip = ""
                    lblBNPPlimit.ToolTip = ""
                    lblUBSlimit.ToolTip = ""
                    lblBNPPlimit.ToolTip = ""
                    lblHSBClimit.ToolTip = ""
                    lblOCBClimit.ToolTip = ""
                    lblCITIlimit.ToolTip = ""
                    lblLEONTEQlimit.ToolTip = ""
                    lblCOMMERZlimit.ToolTip = ""
                    lblCSLimit.ToolTip = ""
                    lblBAMLlimit.ToolTip = ""
                    lblDBIBLimit.ToolTip = ""
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                    lblJPMlimit.Text = "N.A."
                    lblBNPPlimit.Text = "N.A."
                    lblUBSlimit.Text = "N.A."
                    lblBNPPlimit.Text = "N.A."
                    lblHSBClimit.Text = "N.A."
                    lblOCBClimit.Text = "N.A."
                    lblCITIlimit.Text = "N.A."
                    lblLEONTEQlimit.Text = "N.A."
                    lblCOMMERZlimit.Text = "N.A."
                    lblCSLimit.Text = "N.A."
                    lblBAMLlimit.Text = "N.A."
                    lblDBIBLimit.Text = "N.A."
                    lblJPMlimit.ToolTip = ""
                    lblBNPPlimit.ToolTip = ""
                    lblUBSlimit.ToolTip = ""
                    lblBNPPlimit.ToolTip = ""
                    lblHSBClimit.ToolTip = ""
                    lblOCBClimit.ToolTip = ""
                    lblCITIlimit.ToolTip = ""
                    lblLEONTEQlimit.ToolTip = ""
                    lblCOMMERZlimit.ToolTip = ""
                    lblCSLimit.ToolTip = ""
                    lblBAMLlimit.ToolTip = ""
                    lblDBIBLimit.ToolTip = ""
            End Select
            clearShareData()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "getRange", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub clearShareData()
        ''btnMail.Visible = True
 ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
        If ddlShareAccumDecum.SelectedValue Is Nothing OrElse ddlShareAccumDecum.SelectedValue = "" Then
            lblAQDQBaseCcy.Text = ""
            lblComentry2.Text = ""
            lblDisplayExchangeAccumDecumVal.Text = ""
            lblPRRVal.Text = ""
            lblJPMlimit.Text = "N.A."
            lblBNPPlimit.Text = "N.A."
            lblUBSlimit.Text = "N.A."
            lblBNPPlimit.Text = "N.A."
            lblHSBClimit.Text = "N.A."
            lblOCBClimit.Text = "N.A."
            lblCITIlimit.Text = "N.A."
            lblLEONTEQlimit.Text = "N.A."
            lblCOMMERZlimit.Text = "N.A."
            lblCSLimit.Text = "N.A."
            lblBAMLlimit.Text = "N.A."
            lblDBIBLimit.Text = "N.A."
            lblJPMlimit.ToolTip = ""
            lblBNPPlimit.ToolTip = ""
            lblUBSlimit.ToolTip = ""
            lblBNPPlimit.ToolTip = ""
            lblHSBClimit.ToolTip = ""
            lblOCBClimit.ToolTip = ""
            lblCITIlimit.ToolTip = ""
            lblLEONTEQlimit.ToolTip = ""
            lblCOMMERZlimit.ToolTip = ""
            lblCSLimit.ToolTip = ""
            lblBAMLlimit.ToolTip = ""
            lblDBIBLimit.ToolTip = ""
            lblRangeCcy.Text = "Min/Max()"
            ''btnMail.Visible = False
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
            rowAccDecCalculator.Visible = False
            Fill_All_Charts()
        ElseIf rblShareData.SelectedValue = "GRAPHDATA" Then
            rowGraphData.Visible = True
            rowShareData.Visible = False
            rowAccDecCalculator.Visible = False
        Else
            rowGraphData.Visible = False
            rowShareData.Visible = False
            If ((objReadConfig.ReadConfig(dsConfig, "EQC_Show_ELN_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "YES" Or _
                     objReadConfig.ReadConfig(dsConfig, "EQC_Show_ELN_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "Y") And tabContainer.ActiveTabIndex = 0) Then
                rowAccDecCalculator.Visible = False
            ElseIf ((objReadConfig.ReadConfig(dsConfig, "EQC_Show_AccDec_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "YES" Or _
                    objReadConfig.ReadConfig(dsConfig, "EQC_Show_AccDec_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "Y") And (tabContainer.ActiveTabIndex = 3 Or tabContainer.ActiveTabIndex = 4)) Then
                ''Dilkhush 02Feb2016 Changed tab index 2 to 3 & 4
                rowAccDecCalculator.Visible = True
                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setAccCalc12", "AccDecCalcType();", True)
            Else
                rowAccDecCalculator.Visible = False
            End If

        End If
        hideShowRBLShareData()
        System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setAccCalc", "clearACCDECCalData();", True)
    End Sub

    Private Sub hideShowRBLShareData()
        Dim count As Integer = 0
        Dim ELNCalc As String = objReadConfig.ReadConfig(dsConfig, "EQC_Show_ELN_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
        Dim AccDecCalc As String = objReadConfig.ReadConfig(dsConfig, "EQC_Show_AccDec_Calculator", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
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

        If (AccDecCalc = "YES" Or AccDecCalc = "Y") Then
            count = count + 1
            rblShareData.Items.FindByValue("calc").Attributes.Add("style", "display:''")
        Else
            rblShareData.Items.FindByValue("calc").Attributes.Add("style", "display:none")
        End If


        If count >= 2 Then
            rblShareData.Visible = True
            ''' tdrblShareData.Visible = True '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>

        Else
            rblShareData.Visible = False
            ''tdrblShareData.Visible = False '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>

        End If
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
                Case "DB"
                    If Convert.ToString(Session("flag")) = "I" Then
                        Quote_ID = CStr(hashRFQID(hashPPId("DB")))
                        Session.Remove("flag")
                        Session("flag") = ""
                    Else
                        Quote_ID = Convert.ToString(Session("DBIBQuote"))
                        Session.Remove("DBIBQuote")
                    End If
            End Select
            strMargin = ""
            strClientPrice = ""
            strClientYield = ""
            strBookingBranch = ddlBookingBranchPopUpValue.SelectedValue
            orderQuantity = lblNotionalPopUpValue.Text
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

            Dim count As Integer = 0

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
    lblNotionalPopUpCaption.Text + "</td><td>" + lblNotionalPopUpValue.Text + "</td></tr>" + _
    "<tr><td  style=""background-color: #DBE5F1;"">" + lblStrikePopUpCaption.Text + "</td><td>" + lblStrikePopUpValue.Text + "</td><td  style=""background-color: #DBE5F1;"">" + _
    lblTenorPopUpCaption.Text + "</td><td>" + lblTenorPopUpValue.Text + " " + lblTenorTypePopUpValue.Text + "</td></tr>" + _
    "<tr><td  style=""background-color: #DBE5F1;"">" + lblKOPopUpCaption.Text + "</td><td>" + lblKOPopUpValue.Text + " " + lblKOTypePopUpValue.Text + "</td><td>" + _
    "</td><td>" + "</td></tr>" + _
    "<tr><td  style=""background-color: #DBE5F1;"">" + lblUpfrontPopUpCaption.Text + "</td><td>" + txtUpfrontPopUpValue.Text + "</td><td  style=""background-color: #DBE5F1;"">" + _
    lblClientYieldPopUpCaption.Text + "</td><td>" + lblClientYieldPopUpValue.Text + "</td></tr>" + _
     "<tr><td  style=""background-color: #DBE5F1;"">" + lblGuaranteePopUpCaption.Text + "</td><td>" + lblGuaranteePopUpValue.Text + "</td><td  style=""background-color: #DBE5F1;"">" + _
    lblGearingPopUpCaption.Text + "</td><td>" + lblGearingPopUpValue.Text + "</td></tr>" + _
    "<tr><td  style=""background-color: #DBE5F1;"">" + lblOrderTypePopUpCaption.Text + "</td><td>" + ddlOrderTypePopUpValue.SelectedItem.Text + "</td><td  style=""background-color: #DBE5F1;"">" + _
    lblLimitPricePopUpCaption.Text + "</td><td>" + txtLimitPricePopUpValue.Text + "</td></tr></table></div>")

                            '<Changed by Mohit Lalwani on 3-Feb-2016 FA-1339>
                            Dim emailSubject As String = ""
                            emailSubject = objReadConfig.ReadConfig(dsConfig, "EQC_DealerRedirection_Subject", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "EQ Connect Order Redirection for " + ddlAccumType.SelectedItem.Text.ToString + " &#8211; &lt;RFQID&gt;").ToString
                            If ddlAccumType.SelectedValue.ToUpper = "ACCUMULATOR" Then
                                emailSubject = emailSubject.Trim.Replace("&lt;RFQID&gt;", Quote_ID).Replace("&lt;Product&gt;", "ACCU").Replace("&#8211;", "-")
                            Else
                                emailSubject = emailSubject.Trim.Replace("&lt;RFQID&gt;", Quote_ID).Replace("&lt;Product&gt;", "DECU").Replace("&#8211;", "-")
                            End If
                            'oWEBADMIN.Notify_ToDealerDeskGroupEmailID(LoginInfoGV.Login_Info.EntityID.ToString(), _
                            '                                                             LoginInfoGV.Login_Info.LoginId, sLSS_DealerNotificationGroupEmailID, LoginInfoGV.Login_Info.LoginName + " redirected an order.  RFQ Id  for redirected order is: " + Quote_ID + _
                            '                                                             sbNotifyMail.ToString, "EQ Connect Order Redirection for " + ddlAccumType.SelectedItem.Text.ToString + " - " + Quote_ID, errmailnotify)
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

    Private Sub btnPrdInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrdInfo.Click
        Dim Product_name As String = ""
        Dim strWebPrdInfo_DirPath As String
        Try
            Product_name = ddlAccumType.SelectedValue
            strWebPrdInfo_DirPath = objReadConfig.ReadConfig(New DataSet, "WebPrdInfo_DirPath", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "")
            Dim str As String = String.Empty
            str = str & "window.open('" & LoginInfoGV.Login_Info.WebServicePath & "/../" & strWebPrdInfo_DirPath & "/" & Product_name & ".pdf" & "','ProductTermsheet','scrollbars=yes,resizable=yes,menubar=0,status=0,width=1280,height=650,top=0,left=0');"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "clientstrTypeList", str, True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnInfo1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInfo1.Click
        Try
            If pnlInfo1.Visible Then
                pnlInfo1.Visible = False
            Else
                pnlInfo1.Visible = True
                lblInfo1Header.Text = btnInfo1.Text
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnInfo2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInfo2.Click
        Try
            If pnlInfo2.Visible Then
                pnlInfo2.Visible = False
            Else
                pnlInfo2.Visible = True
                lblInfo2Header.Text = btnInfo2.Text
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnInfo3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInfo3.Click
        Try
            If pnlInfo3.Visible Then
                pnlInfo3.Visible = False
            Else
                pnlInfo3.Visible = True
                lblInfo3Header.Text = btnInfo3.Text
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnInfo4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInfo4.Click
        Try
            If pnlInfo4.Visible Then
                pnlInfo4.Visible = False
            Else
                pnlInfo4.Visible = True
                lblInfo4Header.Text = btnInfo4.Text
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnInfo1Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInfo1Close.Click
        pnlInfo1.Visible = False
    End Sub
    Private Sub btnInfo2Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInfo2Close.Click
        pnlInfo2.Visible = False
    End Sub
    Private Sub btnInfo3Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInfo3Close.Click
        pnlInfo3.Visible = False
    End Sub
    Private Sub btnInfo4Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInfo4Close.Click
        pnlInfo4.Visible = False
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
                TROCBC1.Disabled = True
                TRCITI1.Disabled = True
                TRLEONTEQ1.Disabled = True
                TRCOMMERZ1.Disabled = True
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


                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"

                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "OCBC" Then
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
                TROCBC1.Disabled = False
                TRCITI1.Disabled = True
                TRLEONTEQ1.Disabled = True
                TRCOMMERZ1.Disabled = True

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
                btnOCBCPrice.Enabled = True
                btnOCBCPrice.CssClass = "btn"

                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "CITI" Then
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
                TROCBC1.Disabled = True
                TRCITI1.Disabled = False
                TRLEONTEQ1.Disabled = True
                TRCOMMERZ1.Disabled = True
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
                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
                btnCITIPrice.Enabled = True
                btnCITIPrice.CssClass = "btn"
            ElseIf ppname.Trim.ToUpper = "LEONTEQ" Then
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
                TROCBC1.Disabled = True
                TRLEONTEQ1.Disabled = False
                TRCITI1.Disabled = True
                TRCOMMERZ1.Disabled = True
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
                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"
                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"
                btnLEONTEQPrice.Enabled = True
                btnLEONTEQPrice.CssClass = "btn"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
            ElseIf ppname.Trim.ToUpper = "COMMERZ" Then
                TRJPM1.Disabled = True
                TRBAML1.Disabled = True
                TRCS1.Disabled = True
                TRBNPP1.Disabled = True
                TRUBS1.Disabled = True
                TRHSBC1.Disabled = True
                TRDBIB.Disabled = True
                TROCBC1.Disabled = True
                TRCOMMERZ1.Disabled = False
                TRCITI1.Disabled = True
                TRLEONTEQ1.Disabled = True
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
                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"
                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"
                btnCOMMERZPrice.Enabled = True
                btnCOMMERZPrice.CssClass = "btn"
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
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
                TRCITI1.Disabled = True
                TRLEONTEQ1.Disabled = True
                TRCOMMERZ1.Disabled = True
                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
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
                TRCITI1.Disabled = True
                TRLEONTEQ1.Disabled = True
                TRCOMMERZ1.Disabled = True
                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
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
                TRCITI1.Disabled = True
                TRLEONTEQ1.Disabled = True
                TRCOMMERZ1.Disabled = True
                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
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
                TRCITI1.Disabled = True
                TRLEONTEQ1.Disabled = True
                TRCOMMERZ1.Disabled = True

                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
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
                TRCITI1.Disabled = True
                TRLEONTEQ1.Disabled = True
                TRCOMMERZ1.Disabled = True

                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
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
                TRCITI1.Disabled = True

                btnOCBCPrice.Enabled = False
                btnOCBCPrice.CssClass = "btnDisabled"

                btnCITIPrice.Enabled = False
                btnCITIPrice.CssClass = "btnDisabled"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btnDisabled"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btnDisabled"
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
                TROCBC1.Disabled = False
                TRCITI1.Disabled = False
                TRDBIB.Disabled = False
                btnSolveAll.Enabled = True
                btnSolveAll.CssClass = "btn"

                btnOCBCPrice.Enabled = True
                btnOCBCPrice.CssClass = "btn"

                btnCITIPrice.Enabled = True
                btnCITIPrice.CssClass = "btn"

                TRLEONTEQ1.Disabled = True
                btnLEONTEQPrice.Enabled = False
                btnLEONTEQPrice.CssClass = "btn"

                TRCOMMERZ1.Disabled = True
                btnCOMMERZPrice.Enabled = False
                btnCOMMERZPrice.CssClass = "btn"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Private Function setExchangeByShare(ByVal _ddlShare As RadComboBox) As String
        Dim sExchangeName As String
        Try
            sExchangeName = ""
            'If _ddlShare.SelectedItem.Value.Trim = "" Then
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
            Case prdTab.ELN
                Response.Redirect("../ELN_DealEntry1/ELN_RFQL1.aspx?menustr=EQ%20Sales%20-%20EQ%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=", False)
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
            GetCommentary_Accum()
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_MailText_Narration_AS", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "TEXT").Trim.ToUpper
                Case "DOCGEN"

                    hashRFQID = CType(Session("Hash_Values"), Hashtable)
                    hashPPId = CType(Session("PP_IdTable"), Hashtable)
                    'bestPP = CheckBestPriceForEmail()

                    Select Case hdnBestProvider.Value
                        Case "JPM"
                            PriceOrStrike = lblJPMPrice.Text
                            sDayCount = lblJPMClientPrice.Text
                            Issuer = "JPM"
                            Yield = lblJPMClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("JPM")))
                            Else
                                RFQID = Convert.ToString(Session("JPMQuote"))
                            End If

                        Case "HSBC"
                            PriceOrStrike = lblHSBCPrice.Text
                            sDayCount = lblHSBCClientPrice.Text
                            Issuer = "HSBC"
                            Yield = lblHSBCClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("HSBC")))
                            Else
                                RFQID = Convert.ToString(Session("HSBCQuote"))
                            End If

                        Case "OCBC"
                            PriceOrStrike = lblOCBCPrice.Text
                            sDayCount = lblOCBCClientPrice.Text
                            Issuer = "OCBC"
                            Yield = lblOCBCClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("OCBC")))
                            Else
                                RFQID = Convert.ToString(Session("OCBCQuote"))
                            End If

                        Case "CITI"
                            PriceOrStrike = lblCITIPrice.Text
                            sDayCount = lblCITIClientPrice.Text
                            Issuer = "CITI"
                            Yield = lblCITIClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("CITI")))
                            Else
                                RFQID = Convert.ToString(Session("CITIQuote"))
                            End If

                        Case "LEONTEQ"
                            PriceOrStrike = lblLEONTEQPrice.Text
                            sDayCount = lblLEONTEQClientPrice.Text
                            Issuer = "LEONTEQ"
                            Yield = lblLEONTEQClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("LEONTEQ")))
                            Else
                                RFQID = Convert.ToString(Session("LEONTEQQuote"))
                            End If

                        Case "COMMERZ"
                            PriceOrStrike = lblCOMMERZPrice.Text
                            sDayCount = lblCOMMERZClientPrice.Text
                            Issuer = "COMMERZ"
                            Yield = lblCOMMERZClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("COMMERZ")))
                            Else
                                RFQID = Convert.ToString(Session("COMMERZQuote"))
                            End If

                        Case "CS"
                            PriceOrStrike = lblCSPrice.Text
                            sDayCount = lblCSClientPrice.Text
                            Issuer = "CS"
                            Yield = lblCSClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("CS")))
                            Else
                                RFQID = Convert.ToString(Session("CSQuote"))
                            End If

                        Case "UBS"
                            PriceOrStrike = lblUBSPrice.Text
                            sDayCount = lblUBSClientPrice.Text
                            Issuer = "UBS"
                            Yield = lblUBSClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("UBS")))
                            Else
                                RFQID = Convert.ToString(Session("UBSQuote"))
                            End If

                        Case "BNPP"
                            PriceOrStrike = lblBNPPPrice.Text
                            sDayCount = lblBNPPClientPrice.Text
                            Issuer = "BNPP"
                            Yield = lblBNPPClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("BNPP")))
                            Else
                                RFQID = Convert.ToString(Session("BNPPQuote"))
                            End If

                        Case "BAML"
                            PriceOrStrike = lblBAMLPrice.Text
                            sDayCount = lblBAMLClientPrice.Text
                            Issuer = "BAML"
                            Yield = lblBAMLClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("BAML")))
                            Else
                                RFQID = Convert.ToString(Session("BAMLQuote"))
                            End If

                        Case "DBIB" ''<Do not replace DBIB to DB ,here comparing ID>
                            PriceOrStrike = lblDBIBPrice.Text
                            sDayCount = lblDBIBClientPrice.Text
                            Issuer = "DB"
                            Yield = lblDBIBClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("DB")))
                            Else
                                RFQID = Convert.ToString(Session("DBIBQuote"))
                            End If
                        Case Nothing, ""
                            sDayCount = ""
                            PriceOrStrike = ""
                            Issuer = ""
                            Yield = ""

                    End Select

                    If RFQID Is Nothing Or RFQID = "" Or PriceOrStrike = "" Or PriceOrStrike.ToUpper = "REJECTED" Then
                        lblerror.ForeColor = Color.Red
                        lblerror.Text = "No price available for mailing!"
                        Exit Sub
                    End If
                    Dim strFileName As String
                    Dim strDocGenVirtualPath As String
                    If ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "ACCUMULATOR" Then
                        strFileName = generateDocument.StartDocumentGeneration(LoginInfoGV.Login_Info.LoginId, "ACC", "SEND_QUOTE_EMAIL", RFQID, "ELN_DEAL", LoginInfoGV.Login_Info.EntityID.ToString, LoginInfoGV.Login_Info.GlobalServerDateTime, 1)
                    ElseIf ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "DECUMULATOR" Then
                        strFileName = generateDocument.StartDocumentGeneration(LoginInfoGV.Login_Info.LoginId, "DAC", "SEND_QUOTE_EMAIL", RFQID, "ELN_DEAL", LoginInfoGV.Login_Info.EntityID.ToString, LoginInfoGV.Login_Info.GlobalServerDateTime, 1)
                    End If


                    'Dim strTempPath As String
                    'strTempPath = "C:/FinIQ/dat/Templates/DCD/MY/Termsheet.html"

                    'strFileName = Path.GetFileName(strFileName)
                    'strDocGenVirtualPath = objReadConfig.ReadConfig(New DataSet, "WebDocGen_VirtualPath", "DocGen", CStr(LoginInfoGV.Login_Info.EntityID), "")

                    'Dim str As String = String.Empty
                    'str = LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/" & strFileName
                    'Dim FileText As String = File.ReadAllText(str)
                    Dim FileText As String = File.ReadAllText(strFileName)
                    ''Dim FileText As String = File.ReadAllText(strTempPath)
                    'str = str & "window.open('" & LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/" & strFileName & "','Confirmation','scrollbars=yes,resizable=yes,menubar=0,status=0,width=1500,height=300');"
                    'Page.ClientScript.RegisterStartupScript(Me.GetType, "Confirmation", str, True)

                    replaceClassToStyle(FileText)

                    Dim mailSubject As New StringBuilder
                    If ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "ACCUMULATOR" Then
                        mailSubject.Append(objReadConfig.ReadConfig(dsConfig, "EQC_QuoteMailSubjectBankName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), " <EQ_Connect RFQ>").Replace("&lt;", "<").Replace("&gt;", ">"))
                        mailSubject.Append(" " + txtTenorAccumDecum.Text.ToString.Trim + " " + ddlTenorTypeAccum.SelectedItem.Text)
                        mailSubject.Append(" " + lblAQDQBaseCcy.Text.ToString.Trim)
                        mailSubject.Append(" ACCUMULATOR on ")
                    Else
                        mailSubject.Append(objReadConfig.ReadConfig(dsConfig, "EQC_QuoteMailSubjectBankName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), " <EQ_Connect RFQ>").Replace("&lt;", "<").Replace("&gt;", ">"))
                        mailSubject.Append(" " + txtTenorAccumDecum.Text.ToString.Trim + " " + ddlTenorTypeAccum.SelectedItem.Text)
                        mailSubject.Append(" " + lblAQDQBaseCcy.Text.ToString.Trim)
                        mailSubject.Append(" DECUMULATOR on ")
                    End If

                    Dim strUnderlyingTicker As String = getUnderlyingTicker(ddlShareAccumDecum.Text)
                    'Dim strDRAUnderlying2 As String = If(chkAddShare2.Checked, If(ddlShareAccumDecum.SelectedValue Is Nothing Or ddlShareDRA2.SelectedValue = "", "", ddlShareDRA2.Text), "")
                    'strUnderlyingTicker2 = If(strDRAUnderlying2 <> "", getUnderlyingTicker(strDRAUnderlying2), "")
                    'Dim strDRAUnderlying3 As String = If(chkAddShare3.Checked, If(ddlShareDRA3.SelectedValue Is Nothing Or ddlShareDRA3.SelectedValue = "", "", ddlShareDRA3.Text), "")
                    'strUnderlyingTicker3 = If(strDRAUnderlying3 <> "", getUnderlyingTicker(strDRAUnderlying3), "")

                    Dim sSubjectBasket As String = strUnderlyingTicker
                    ''If sUnderlyingTicker2 <> "" Then
                    ''    sSubjectBasket += ", " + strDRAUnderlying2
                    ''    If sUnderlyingTicker3 <> "" Then
                    ''        sSubjectBasket += ", " + strDRAUnderlying3
                    ''    End If
                    ''End If
                    mailSubject.Append(If(sSubjectBasket.EndsWith(", "), sSubjectBasket.Substring(0, sSubjectBasket.LastIndexOf(", ")), sSubjectBasket))

                    Dim strLoginUserEmail As String = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)


                    Dim dtImageDetails As DataTable
                    dtImageDetails = New DataTable("dtImageDetails")
                    dtImageDetails.Columns.Add("imageID", GetType(String))
                    dtImageDetails.Columns.Add("imagePath", GetType(String))
                    dtImageDetails.Rows.InsertAt(dtImageDetails.NewRow(), 0)
                    dtImageDetails.Rows(0).Item(0) = "Image"
                    If ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "ACCUMULATOR" Then
                        dtImageDetails.Rows(0).Item(1) = System.Web.HttpContext.Current.Server.MapPath("..\..\FinIQWebApp\ELN_DealEntry1\Images\EmailPrdHeaders\Accumulator_Header.png")
                    Else
                        dtImageDetails.Rows(0).Item(1) = System.Web.HttpContext.Current.Server.MapPath("..\..\FinIQWebApp\ELN_DealEntry1\Images\EmailPrdHeaders\Decumulator_Header.png")
                    End If

                    ''<TRDS Email Code>
                    ''<Sample image commented for using actual image>
                    'dtImageDetails.Rows.InsertAt(dtImageDetails.NewRow(), 1)
                    'dtImageDetails.Rows(1).Item(0) = "GraphSample"
                    'dtImageDetails.Rows(1).Item(1) = System.Web.HttpContext.Current.Server.MapPath("..\..\FinIQWebApp\ELN_DealEntry1\Images\EmailPrdHeaders\StockGraphSample.png")
                    ''</Sample image commented for using actual image>

                    Dim sGraphImagePath As String = ""
                    'sGraphImagePath = "E:\MailingHeaders\FromTRDS\graph_" + ddlShareAccumDecum.SelectedValue + "_" + RFQID + ".gif"

                    sGraphImagePath = objReadConfig.ReadConfig(dsConfig, "EQC_QuoteEmailGraphImagePath", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "C:\FinIQ\Product_Info\GraphImagePath\")
                    sGraphImagePath = sGraphImagePath + ddlShareAccumDecum.SelectedValue + "_" + RFQID + ".gif"

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

                    Dim filePath As String
                    Dim fileStaticKYRPath As String
                    Dim fileName As String
                    fileStaticKYRPath = objReadConfig.ReadConfig(dsConfig, "EQC_ProductKYIRPath", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "C:\FinIQ\Product_Info\")
                    If ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "ACCUMULATOR" Then
                        fileName = "KYIR - ACC.pdf"
                    Else
                        fileName = "KYIR - DEC.pdf"
                    End If
                    fileStaticKYRPath = fileStaticKYRPath + fileName
                    If File.Exists(fileStaticKYRPath) Then
                        filePath = fileStaticKYRPath
                    Else
                        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:File Not found:" + filePath, Nothing, Nothing, "btnEMLMailTrial_Click", "High")
                        filePath = ""
                    End If

                    ''<TitmSheet Attachment>
                    Dim titmSheetPath As String = ""
                    Dim titmSheetName As String = ""
                    titmSheetPath = objReadConfig.ReadConfig(dsConfig, "EQC_ProductTitmSheetPathAccDec", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "E:\Product_Info\TermSheet\AccDec\")

                    ''TermSheet_Accumulator_Leverage_Guarantee.pdf
                    ''TermSheet_Decumulator_Leverage_NoGuarantee.pdf
                    titmSheetName = "TermSheet"
                    If ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "ACCUMULATOR" Then
                        titmSheetName = titmSheetName + "_Accumulator"
                    Else
                        titmSheetName = titmSheetName + "_Decumulator"
                    End If

                    If chkLeverageRatio.Checked = True Then
                        titmSheetName = titmSheetName + "_Leverage"
                    Else
                        titmSheetName = titmSheetName + "_NoLeverage"
                    End If

                    If ddlAccumGUDuration.SelectedValue = "0" Then
                        titmSheetName = titmSheetName + "_NoGuarantee"
                    Else
                        titmSheetName = titmSheetName + "_Guarantee"
                    End If
                    titmSheetName = titmSheetName + ".PDF"
                    titmSheetPath = titmSheetPath + titmSheetName

                    If File.Exists(titmSheetPath) Then
                        filePath = filePath + ";" + titmSheetPath
                    Else
                        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:File Not found:" + titmSheetPath, Nothing, Nothing, "btnEMLMailTrial_Click", "High")
                        titmSheetPath = ""
                    End If
                    ''</TitmSheet Attachment>

                    ''<Attach KYIR>

                    Dim strKYIFileName As String
                    If ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "ACCUMULATOR" Then
                        strKYIFileName = objDocGen.StartDocumentGeneration(LoginInfoGV.Login_Info.LoginId, "ACC", "PUBLISH_TERMSHEET", RFQID, "ELN_DEAL", LoginInfoGV.Login_Info.EntityID.ToString, LoginInfoGV.Login_Info.GlobalServerDateTime, 1)
                    ElseIf ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "DECUMULATOR" Then
                        strKYIFileName = objDocGen.StartDocumentGeneration(LoginInfoGV.Login_Info.LoginId, "DAC", "PUBLISH_TERMSHEET", RFQID, "ELN_DEAL", LoginInfoGV.Login_Info.EntityID.ToString, LoginInfoGV.Login_Info.GlobalServerDateTime, 1)
                    End If
                    If File.Exists(strKYIFileName) Then
                        filePath = filePath + ";" + strKYIFileName
                    End If
                    ''</Attach KYIR>

                    If oWEBADMIN.Notify_ToDealerDeskGroupEmailID_imageContent(LoginInfoGV.Login_Info.EntityID.ToString(), _
                                                                                       LoginInfoGV.Login_Info.LoginId, strLoginUserEmail, FileText, mailSubject.ToString(), filePath, dtImageDetails, "") Then
                        lblerror.ForeColor = Color.Blue
                        lblerror.Text = "Email sent successfully."
                    Else
                        lblerror.ForeColor = Color.Blue
                        lblerror.Text = "Email sending failed."
                    End If

                    If File.Exists(strFileName) Then
                        File.Delete(strFileName)
                    End If
                    'If File.Exists(strKYIFileName) Then
                    '    File.Delete(strKYIFileName)
                    'End If

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

                    Select Case bestPP
                        Case "JPM"
                            PriceOrStrike = lblJPMPrice.Text
                            sDayCount = lblJPMClientPrice.Text
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
                            sDayCount = lblHSBCClientPrice.Text
                            Issuer = "HSBC"
                            Yield = lblHSBCClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("HSBC")))
                            Else
                                RFQID = Convert.ToString(Session("HSBCQuote"))
                            End If
                            'RFQID = Convert.ToString(Session("HSBCQuote"))
                        Case "OCBC"
                            PriceOrStrike = lblOCBCPrice.Text
                            sDayCount = lblOCBCClientPrice.Text
                            Issuer = "OCBC"
                            Yield = lblOCBCClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("OCBC")))
                            Else
                                RFQID = Convert.ToString(Session("OCBCQuote"))
                            End If
                            'RFQID = Convert.ToString(Session("HSBCQuote"))
                        Case "CITI"
                            PriceOrStrike = lblCITIPrice.Text
                            sDayCount = lblCITIClientPrice.Text
                            Issuer = "CITI"
                            Yield = lblCITIClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("CITI")))
                            Else
                                RFQID = Convert.ToString(Session("CITIQuote"))
                            End If
                            'RFQID = Convert.ToString(Session("HSBCQuote"))
                        Case "LEONTEQ"
                            PriceOrStrike = lblLEONTEQPrice.Text
                            sDayCount = lblLEONTEQClientPrice.Text
                            Issuer = "LEONTEQ"
                            Yield = lblLEONTEQClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("LEONTEQ")))
                            Else
                                RFQID = Convert.ToString(Session("LEONTEQQuote"))
                            End If
                            'RFQID = Convert.ToString(Session("HSBCQuote"))
                        Case "COMMERZ"
                            PriceOrStrike = lblCOMMERZPrice.Text
                            sDayCount = lblCOMMERZClientPrice.Text
                            Issuer = "COMMERZ"
                            Yield = lblCOMMERZClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("COMMERZ")))
                            Else
                                RFQID = Convert.ToString(Session("COMMERZQuote"))
                            End If
                            'RFQID = Convert.ToString(Session("HSBCQuote"))
                        Case "CS"
                            PriceOrStrike = lblCSPrice.Text
                            sDayCount = lblCSClientPrice.Text
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
                            sDayCount = lblUBSClientPrice.Text
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
                            sDayCount = lblBNPPClientPrice.Text
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
                            sDayCount = lblBAMLClientPrice.Text
                            Issuer = "BAML"
                            Yield = lblBAMLClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("BAML")))
                            Else
                                RFQID = Convert.ToString(Session("BAMLQuote"))
                            End If
                            'RFQID = Convert.ToString(Session("BAMLQuote"))
                        Case "DBIB" ''<Do not replace DBIB to DB ,here comparing ID>
                            PriceOrStrike = lblDBIBPrice.Text
                            sDayCount = lblDBIBClientPrice.Text
                            Issuer = "DB"
                            Yield = lblDBIBClientYield.Text
                            If Convert.ToString(Session("flag")) = "I" Then
                                RFQID = CStr(hashRFQID(hashPPId("DB")))
                            Else
                                RFQID = Convert.ToString(Session("DBIBQuote"))
                            End If
                        Case Nothing, ""
                            sDayCount = ""
                            PriceOrStrike = ""
                            Issuer = ""
                            Yield = ""

                    End Select

                    If RFQID Is Nothing Or RFQID = "" Then
                        lblerror.ForeColor = Color.Red
                        lblerror.Text = "No price available for mailing!"
                        Exit Sub
                    End If
                    sUnderlyingTicker = getUnderlyingTicker(ddlShareAccumDecum.Text)  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value

                    strMailHTML = getQuoteMailHTMLString_AccDecc(sUnderlyingTicker, PriceOrStrike, bestPP, Issuer, Yield, sDayCount, RFQID)   ''for ACCDECC
                    'mailSubject.Append(" " + RFQID)                                 ''Mangesh wakode <8 dec 2015>RFQ Removed from mail subject as told by Avinash G.
                    '<AvinashG. on 06-Jan-2016: Config based bank name>
                    mailSubject.Append(objReadConfig.ReadConfig(dsConfig, "EQC_QuoteMailSubjectBankName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), " <EQ_Connect RFQ>").Replace("&lt;", "<").Replace("&gt;", ">"))
                    'mailSubject.Append(" <EQ_Connect RFQ>")   ''Mangesh wakode <8 dec 2015>
                    '<AvinashG. on 06-Jan-2016: Config based bank name>
                    '</AvinashG. on 24-Dec-2015: Tenor not notional >
                    mailSubject.Append(" " + txtTenorAccumDecum.Text.ToString.Trim + " " + ddlTenorTypeAccum.SelectedItem.Text)
                    'mailSubject.Append(" " + txtAccumOrderqty.Text.ToString.Trim + " shares/day")    ''append notional to subject
                    '</AvinashG. on 24-Dec-2015: Tenor not notional >
                    mailSubject.Append(" " + lblAQDQBaseCcy.Text.ToString.Trim)          ''append CCy to subject
                    mailSubject.Append(" " + ddlAccumType.SelectedValue.ToString.Trim + " on ") ''append product to subject
                    mailSubject.Append(" " + sUnderlyingTicker) ''append Underlying to subject
                    If ddlAccumType.SelectedValue.ToUpper = "ACCUMULATOR" Then
                        mailFileName = "EQ_Connect_Accu_" + ddlAccumType.SelectedItem.Text + txtTenorAccumDecum.Text + "_" + If(ddlTenorTypeAccum.SelectedValue.ToString.ToUpper = "MONTH", "m", "y") + "_" + _
                        sUnderlyingTicker + ".eml"
                    Else
                        mailFileName = "EQ_Connect_Decu_" + ddlAccumType.SelectedItem.Text + txtTenorAccumDecum.Text + "_" + If(ddlTenorTypeAccum.SelectedValue.ToString.ToUpper = "MONTH", "m", "y") + "_" + _
                        sUnderlyingTicker + ".eml"
                    End If

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
        ''Bitmap.FromStream(New MemoryStream(tClient.DownloadData("http://t1.trkd-asia.com/finiq/?sym=6837.HK&style=1&int=4&per=8&w=570&h=200&l=12.6,409090,1%7C11.9,404090,1%7C11.3,409040,1&token=2B157F5064B581A671634B2403F9EAAAAC90B9744204D3EA4AAFB995952A182571D71AB3E961D80ABFE91B504277C109C8DEFF69F9A70119F5EA52C9F2FDA9CD940A8E95A6620F94E61539EE1CAEF720")))
        Try
            Dim tClient As WebClient = New WebClient
            Dim strImageURL As String
            Dim dtEmailDetails As DataTable
            dtEmailDetails = Nothing
            Dim strTokenCon As String = ""

            strImageURL = ConfigurationManager.AppSettings("quoteEmail_imageUrl")
            If strImageURL <> "" Then
                strImageURL = strImageURL.Replace("[QE_Share]", ddlShareAccumDecum.SelectedValue)
                strImageURL = strImageURL.Replace("[QE_Style]", "1")
                strImageURL = strImageURL.Replace("[QE_Int]", "4")
                strImageURL = strImageURL.Replace("[QE_Per]", "11")
                strImageURL = strImageURL.Replace("[QE_Width]", "540")
                strImageURL = strImageURL.Replace("[QE_Height]", "200")
                getQuoteEmailDetails(sRFQID, sMode, dtEmailDetails)
                If dtEmailDetails.Rows.Count > 0 Then
                    Dim strParam As String
                    strParam = dtEmailDetails.Rows(0)("ABSKO_ACDC").ToString() + ",31CFEF,1" ''AbsKO,lineColor,LineStyle
                    strParam = strParam + "%7C" + dtEmailDetails.Rows(0)("ABSStrike").ToString() + ",428EDE,1" ''AbsStrike,lineColor,LineStyle
                    strImageURL = strImageURL.Replace("[QE_Param]", strParam)
                End If
                strTokenCon = objReadConfig.ReadConfig(dsConfig, "EQC_QuoteEmailToken", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "token=2B157F5064B581A671634B2403F9EAAAAC90B9744204D3EA4AAFB995952A182571D71AB3E961D80ABFE91B504277C109C8DEFF69F9A70119F5EA52C9F2FDA9CD940A8E95A6620F94E61539EE1CAEF720")
                strImageURL = strImageURL.Replace("token=[QE_Token]", strTokenCon)
            Else
                LogException(LoginInfoGV.Login_Info.LoginId, "Exception:Graph image url generation failed.", LogType.FnqError, Nothing, _
                        sSelfPath, "getEmailGraphImage", ErrorLevel.High)
                Dim blankImage As Image
                Return blankImage
            End If

            '<Commented by rushikesh on 8-Nov-16>
            'strImageURL = New StringBuilder()
            'strImageURL.Append("http://t1.trkd-asia.com/finiq/?")
            'strImageURL.Append("sym=" + ddlShareAccumDecum.SelectedValue) ''Stock code
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
            'getQuoteEmailDetails(sRFQID, sMode, dtEmailDetails)
            'strImageURL.Append("&per=11")
            'strImageURL.Append("&w=540") ''Width
            'strImageURL.Append("&h=200") ''Height
            'If dtEmailDetails.Rows.Count > 0 Then
            '    strImageURL.Append("&l=" + dtEmailDetails.Rows(0)("ABSKO_ACDC").ToString() + ",31CFEF,1") ''AbsKO,lineColor,LineStyle
            '    strImageURL.Append("%7C" + dtEmailDetails.Rows(0)("ABSStrike").ToString() + ",428EDE,1") ''AbsStrike,lineColor,LineStyle
            'End If
            ''strImageURL.Append("&token=2B157F5064B581A671634B2403F9EAAAAC90B9744204D3EA4AAFB995952A182571D71AB3E961D80ABFE91B504277C109C8DEFF69F9A70119F5EA52C9F2FDA9CD940A8E95A6620F94E61539EE1CAEF720")

            'strTokenCon = objReadConfig.ReadConfig(dsConfig, "EQC_QuoteEmailToken", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "token=2B157F5064B581A671634B2403F9EAAAAC90B9744204D3EA4AAFB995952A182571D71AB3E961D80ABFE91B504277C109C8DEFF69F9A70119F5EA52C9F2FDA9CD940A8E95A6620F94E61539EE1CAEF720")
            'strImageURL.Append("&" & strTokenCon)
            'strImageURL.Append("&token=" + oTRDSS.getReutersAuthentication())
            'oTRDSS = New Web_TRDSS.TRSetup
            ''servise url not added for web refrence
            'strImageURL.Append("&token=" + oTRDSS.getReutersAuthentication())
            '</Commented by rushikesh on 8-Nov-16>
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
        Dim strouterTR As String = "outline: 2px solid #41ABD1;border-radius:25px;"

        Dim strtableizertable As String = "font-size: 12px;border: 1px solid #CCC;font-family: Arial, Helvetica, sans-serif;"
        Dim strtableizer_table_th As String = "background-color: #104E8B;color: #FFF;font-weight: bold;"
        Dim strredClass As String = "color: red;font-weight:bold;"
        Dim strblueClass As String = "color: #004d99;font-weight:bold;"
        Dim strblueClassRight As String = "color: #004d99;font-weight:bold;text-align:right;"
        Dim strgraybackClass As String = "background-color: #C0C0C0;width:110px;"

        strHtmlText = strHtmlText.Replace("graybackClass", strgraybackClass)
        strHtmlText = strHtmlText.Replace("blueHeader", strblueHeader)
        strHtmlText = strHtmlText.Replace("fontCss", strfontCss)
        strHtmlText = strHtmlText.Replace("outerTR", strouterTR)

        strHtmlText = strHtmlText.Replace("tableizer-table", strtableizertable)
        strHtmlText = strHtmlText.Replace("tableizer-table_th", strtableizer_table_th)
        strHtmlText = strHtmlText.Replace("redClass", strredClass)
        strHtmlText = strHtmlText.Replace("blueClass", strblueClass)
        strHtmlText = strHtmlText.Replace("blueClassRight", strblueClassRight)

    End Sub
    '<Added by Mohit Lalwani on 15-Dec-2015>

    
    Public Sub resetControlsForPool(ByVal flag As Boolean)
        Try
            panelAQDQ.Enabled = flag
            upnl3 .Update()


            tblRFQGridHolder.Disabled = Not (flag)
            upnlGrid.Update()
            grdOrder.Enabled = flag
            grdAccumDecum.Enabled = flag
            btnSolveAll.Enabled = flag
            EnableDisableForOrderPoolData("")
        Catch ex As Exception
            lblerrorPopUp.Text = "Error occurred while capturing the price."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "resetControlsAfterRepeice_ServerClick", ErrorLevel.High)
        End Try
    End Sub
    
#Region "functions For Quote MailHTML String"
    ''Mangesh wakode 25 nov 2015 added function for quote mail format(AccDecc)
    Public Function getQuoteMailHTMLString_AccDecc(ByVal sUnderlyingTicker As String, ByVal PriceOrStrike As String, ByVal bestPP As String, ByVal Issuer As String, _
                                               ByVal Yield As String, ByVal sDayCount As String, ByRef RFQID As String) As String
        ''23 nov 2015 Mangesh / Imran <START>
        Try

            ''Ashwini P 1-Aug-2016
            Dim BestPPMoodys As String
            Dim MoodysRating As String
            Dim SnPRating As String
            Dim FitchRating As String

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
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>RFQ ID</TD>")   ''Mangesh wakode 16 dec 2015
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Product</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Normal/Levered</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Guaranteed</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Underlying</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>KO Barrier</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Tenor</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Set Freq</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Strike</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>CP</TD>")
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Daycount</TD>")

            'sbELNMailColumnHeader.Append("  <TR style=3D""HEIGHT: 15pt; BACKGROUND: #336699;"">")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; "" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D"" FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>RFQ ID</FONT></SPAN></P></TD>")   ''Mangesh wakode 16 dec 2015
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; "" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D"" FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Product</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; "" height=3D20 vAlign=3Dbottom >")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D"" FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Normal/Levered</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Guaranteed</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Underlying</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>KO Barrier</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Tenor</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Set Freq</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Strike</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom noWrap>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>CP</FONT></SPAN></P></TD>")
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Daycount</FONT></SPAN></P></TD>")
            ''Dilkhush/Avinash 09Dec2015: Commented from  Russell mail
            'sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid;"" height=3D20 vAlign=3Dbottom>")
            'sbELNMailColumnHeader.Append("  <P style=3D""MARGIN: 0in 0in 10pt; LINE-HEIGHT: 13pt"" align=3Dcenter><SPAN lang=3DEN-SG style=3D""COLOR: ; ""><FONT style=3D""FONT-SIZE: 11pt; padding:4pt;"" color=3D#ffffff>Upfront</FONT></SPAN></P></TD>")
            '</AvinashG. on 07-Jan-2016: Optimization Reduction in size of download file>
            sbELNMailColumnHeader.Append(" </TR><TR >")


            ''data
            sbELNMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + RFQID + "</TD>")                     ''RFQID   'mangesh wakode 16 dec 2015
            sbELNMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + If(ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "ACCUMULATOR", "Accum", "Decum") + "</TD>")                     ''Product

            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
            If chkLeverageRatio.Checked = True Then
                sbELNMailColumnHeader.Append("Leveraged")
            Else
                sbELNMailColumnHeader.Append("Normal")
            End If
            sbELNMailColumnHeader.Append(" </TD>") ''Normal/Levered    
            If ddlFrequencyAccumDecum.SelectedValue.ToUpper = "WEEKLY" Then
                sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + ddlAccumGUDuration.SelectedValue.ToString + "&nbsp;" + "w" + " </TD>")           ''Guaranteed
            ElseIf ddlFrequencyAccumDecum.SelectedValue.ToUpper = "FORTNIGHTLY" Then
                sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + (CDbl(ddlAccumGUDuration.SelectedValue.ToString.Trim) * 2).ToString + "&nbsp;" + "w" + " </TD>")           ''Guaranteed
            ElseIf ddlFrequencyAccumDecum.SelectedValue.ToUpper = "MONTHLY" Then
                sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + ddlAccumGUDuration.SelectedValue.ToString.Trim + "&nbsp;" + "m" + " </TD>")           ''Guaranteed
            End If
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + sUnderlyingTicker + " </TD>")           ''Underlying   
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + FormatNumber(txtKO.Text.Trim, 2) + "%</TD>")           ''KO Barrier             
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + txtTenorAccumDecum.Text + If(ddlTenorTypeAccum.SelectedValue.ToString.ToUpper = "MONTH", "m", "y") + " </TD>")           ''Tenor 
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + ddlFrequencyAccumDecum.SelectedValue.ToString + " </TD>")           ''set freq 

            If ddlSolveForAccumDecum.SelectedValue.ToString.Trim.ToString = "STRIKE" Then
                sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center; BACKGROUND: #f5fb99;"">" + PriceOrStrike + "%</TD>")           ''strike 
            Else
                sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center; BACKGROUND: #f5fb99;"">" + txtStrikeaccum.Text.Trim + "%</TD>")           ''strike 

            End If

            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + bestPP + " </TD>")                ''CP    ''hardcode    
            sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + sDayCount + " </TD>")                ''Daycount  ''hardcode

            ''Dilkhush/Avinash 09Dec2015: Commented from  Russell mail
            'If ddlSolveForAccumDecum.SelectedValue.ToString.Trim.ToString = "UPFRONT" Then
            '    sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + PriceOrStrike + "%</TD>")           ''upfront 
            'Else
            '    sbELNMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + FormatNumber(txtUpfront.Text.Trim, 2) + "%</TD>")           ''upfront 
            'End If

            '' <Ashwini P> 01-Aug-2016 START
            BestPPMoodys = CheckBestPriceForEmail()
            GetIssuerRatingForMail(BestPPMoodys, MoodysRating, SnPRating, FitchRating)
            sbELNMailColumnHeader.Append("<TR><TD>* Issuer Rating: Moody's Rating:  " + MoodysRating + ", S&P Rating:  " + SnPRating + ", Fitch Rating:  " + FitchRating + "</TD></TR>")
            ''</Ashwini P> END

            sbELNMailColumnHeader.Append("</TR></TBODY></TABLE></DIV></DIV></BODY>")
            sbELNMailColumnHeader.Append("</HTML>")

            Return sbELNMailColumnHeader.ToString

        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                  sSelfPath, "GetQuoteMailHTMLString_AccDecc", ErrorLevel.High)
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
                    Case "BAML"
                        sStrike = lblBAMLPrice.Text
                        Quote_ID = Convert.ToString(Session("BAMLQuote"))
                        Session.Remove("BAMLQuote")
                    Case "BNPP"
                        sStrike = lblBNPPPrice.Text
                        Quote_ID = Convert.ToString(Session("BNPPQuote"))
                        Session.Remove("BNPPQuote")
                    Case "DB"
                        sStrike = lblDBIBPrice.Text
                        Quote_ID = Convert.ToString(Session("DBIBQuote"))
                        Session.Remove("DBIBQuote")
                End Select

                If sPoolID.Trim <> "" Then
                    Dim sNewPrice As String
                    If ddlSolveForAccumDecum.SelectedValue.ToUpper = "UPFRONT" Then
                        sNewPrice = txtUpfrontPopUpValue.Text
                    ElseIf ddlSolveForAccumDecum.SelectedValue.ToUpper = "STRIKE" Then
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
            Case "DB"
                ' lblBAMLlimit       
                min = CDbl(Split(lblDBIBLimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblDBIBLimit.ToolTip, " / ")(1).Trim)
        End Select

        
        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowIssuerLimitCheckForAccDec", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            Case "Y", "YES"
                Dim EqPair As String = ddlShareAccumDecum.SelectedValue.ToString & " - " & lblAQDQBaseCcy.Text
                Dim BidRate As Double = objELNRFQ.GetShareRate(EqPair, BidRate)
                Dim iAccDays As Integer
                If BidRate = 0 Or BidRate = Nothing Then
                    lblerrorPopUp.Text = "Cannot proceed. Share rate not specified."
                    Return False
                Else
                    Select Case PPName.ToUpper
                        Case "JPM"
                            iAccDays = CInt(lblJPMClientPrice.Text.Replace(",", ""))
                        Case "HSBC"
                            iAccDays = CInt(lblHSBCClientPrice.Text.Replace(",", ""))
                        Case "CITI"
                            iAccDays = CInt(lblCITIClientPrice.Text.Replace(",", ""))
                        Case "LEONTEQ"
                            iAccDays = CInt(lblLEONTEQClientPrice.Text.Replace(",", ""))
                        Case "COMMERZ"
                            iAccDays = CInt(lblCOMMERZClientPrice.Text.Replace(",", ""))
                        Case "OCBC"
                            iAccDays = CInt(lblOCBCClientPrice.Text.Replace(",", ""))
                        Case "UBS"
                            iAccDays = CInt(lblUBSClientPrice.Text.Replace(",", ""))
                        Case "CS"
                            iAccDays = CInt(lblCSClientPrice.Text.Replace(",", ""))
                        Case "BAML"
                            iAccDays = CInt(lblBAMLClientPrice.Text.Replace(",", ""))
                        Case "BNPP"
                            iAccDays = CInt(lblBNPPClientPrice.Text.Replace(",", ""))
                        Case "DB"
                            iAccDays = CInt(lblDBIBClientPrice.Text.Replace(",", ""))
                    End Select

                    Dim Notional As Double = (Convert.ToDouble(txtAccumOrderqty.Text) * iAccDays * BidRate)

                    If (Notional < min) Then
                        lblerror.Text = "Can not place Order. Notional Size is smaller than the minimum permitted."
                        Return False
                    ElseIf (Notional > max) Then
                        lblerror.Text = "Can not place order. Notional size is larger than the maximum permitted."
                        Return False
                    Else
                        Return True
                    End If
                End If
            Case "N", "NO"
                Return True
        End Select

    End Function
    ''<Dilkhush/AVinash 22Dec2015:Set pool data > <> 20327   
    Private Sub setACCDECPoolData()
        Dim dtPoolDetails As DataTable
        Dim sPoolID As String
        Dim strNewAccumTenor As String = String.Empty
        Dim strNewAccumTenorType As String = String.Empty
        Dim strNewAccumGu As String = String.Empty
        Dim strNewAccumGuType As String = String.Empty
        Try
            tabContainer.ActiveTabIndex = prdTab.Acc
            tabIndex = prdTab.Acc
            'tabContainer_ActiveTabChanged(Nothing, Nothing)  '<Added by RushikeshD. on 29-Jan-2015 Jira:EQBOSDEV-233 >

            dtPoolDetails = New DataTable("PoolDetails")
            If Not IsNothing(Request.QueryString("PoolID")) Then
                sPoolID = Request.QueryString("PoolID")
                If sPoolID.Trim <> "" Then
                    Select Case objELNRFQ.GetACCDECPoolRecordData(sPoolID, dtPoolDetails)
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                            'dtPoolDetails.Rows(0).Item("CouponPercentage").ToString       
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''       
                            ShowHideConfirmationPopup(False)
                            ResetAll()
                            grdAccumDecum.SelectedItemStyle.BackColor = Color.FromArgb(242, 242, 243)

                            Dim strAccumType As String = dtPoolDetails.Rows(0).Item("ACCDECType").ToString
                            ddlAccumType.SelectedIndex = ddlAccumType.Items.IndexOf(ddlAccumType.Items.FindByValue(strAccumType))
                            '<Added by RushikeshD. on 29-Jan-2015 Jira:EQBOSDEV-233 >
                            If strAccumType.ToUpper = "ACCUMULATOR" Then
                                tabContainer.ActiveTabIndex = prdTab.Acc
                            ElseIf strAccumType.ToUpper = "DECUMULATOR" Then
                                tabContainer.ActiveTabIndex = prdTab.Dec
                            End If
                            '<Added by RushikeshD. on 29-Jan-2015 Jira:EQBOSDEV-233 >

                            Dim strAccumSolveFor As String = dtPoolDetails.Rows(0).Item("SolveFor").ToString

                            If strAccumSolveFor = "Strike(%)" Then
                                ddlSolveForAccumDecum.SelectedValue = "STRIKE"
                                txtStrikeaccum.Text = ""
                                txtStrikeaccum.Enabled = False
                                txtUpfront.Enabled = True
                                lblSolveForType.Text = "Strike (%)"
                            Else
                                ddlSolveForAccumDecum.SelectedValue = "UPFRONT"
                                txtUpfront.Text = ""
                                txtUpfront.Enabled = False
                                txtStrikeaccum.Enabled = True
                                lblSolveForType.Text = "Upfront (%)"
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
                                    lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                                Case "N", "NO"
                                    If ddlExchangeAccumDecum.SelectedValue = strExchng Then
                                        ddlExchangeAccumDecum.SelectedValue = strExchng
                                        'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                        'If ddlShare.SelectedValue IsNot Nothing Then
                                        '    ddlShare.Text = ddlShare.SelectedItem.Text
                                        'End If
                                        lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                                    Else
                                        ddlExchangeAccumDecum.SelectedValue = strExchng ''ddlShare.Items.IndexOf(ddlShare.Items.FindByText(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Exchange).Text))
                                        '' Fillddl_Share()
                                        'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                        ''ddlShare.Text = ddlShare.Text
                                        'If ddlShare.SelectedValue IsNot Nothing Then
                                        '    ddlShare.Text = ddlShare.SelectedItem.Text
                                        'End If
                                    End If
                            End Select
                            If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                                ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                            End If
                            'Dim strAccumExchg As String = dtPoolDetails.Rows(0).Item("Exchange").ToString


                            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            '    Case "Y", "YES"
                            '        ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(dtPoolDetails.Rows(0).Item("Share").ToString))
                            '        'ddlShareAccumDecum.Text = ddlShareAccumDecum.Text
                            '        If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                            '            ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                            '        End If
                            '        lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                            '    Case "N", "NO"
                            '        If ddlExchangeAccumDecum.SelectedValue = strAccumExchg Then
                            '            ddlExchangeAccumDecum.SelectedValue = strAccumExchg
                            '            ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(dtPoolDetails.Rows(0).Item("Share").ToString))
                            '            'ddlShareAccumDecum.Text = ddlShareAccumDecum.Text
                            '            If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                            '                ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                            '            End If
                            '        Else
                            '            ddlExchangeAccumDecum.SelectedValue = strAccumExchg
                            '            ''Fill_Accum_ddl_Share()
                            '            ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(dtPoolDetails.Rows(0).Item("Share").ToString))
                            '            'ddlShareAccumDecum.Text = ddlShareAccumDecum.Text
                            '            If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                            '                ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                            '            End If
                            '        End If
                            '        lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                            'End Select
                            getCurrency(dtPoolDetails.Rows(0).Item("Share").ToString)

                            Dim strAccumTenor As String = dtPoolDetails.Rows(0).Item("Tenor").ToString

                            For i = 0 To strAccumTenor.Length - 1
                                If IsNumeric(strAccumTenor.Substring(i, 1)) = True Then
                                    strNewAccumTenor = strNewAccumTenor + strAccumTenor.Substring(i, 1)
                                End If
                            Next
                            txtTenorAccumDecum.Text = strNewAccumTenor

                            For i = 0 To strAccumTenor.Length - 1
                                If IsNumeric(strAccumTenor.Substring(i, 1)) = False Then
                                    strNewAccumTenorType = (strNewAccumTenorType + strAccumTenor.Substring(i, 1)).Trim
                                End If
                            Next

                            ddlTenorTypeAccum.SelectedValue = dtPoolDetails.Rows(0).Item("TenorType").ToString.ToUpper.Trim   ''mangesh wakode 17 dec 2015       

                            Dim strAccumGu As String = dtPoolDetails.Rows(0).Item("GuDuration").ToString

                            For i = 0 To strAccumGu.Length - 1
                                If IsNumeric(strAccumGu.Substring(i, 1)) = True Then
                                    strNewAccumGu = strNewAccumGu + strAccumGu.Substring(i, 1)
                                End If
                            Next
                            txtDuration.Text = strNewAccumGu
                            Dim straccumFrequency As String
                            straccumFrequency = dtPoolDetails.Rows(0).Item("KO_Type").ToString.ToUpper

                            Select Case dtPoolDetails.Rows(0).Item("EOP_Frequency").ToString.ToUpper
                                Case "WEEKLY"


                                    ddlFrequencyAccumDecum.SelectedIndex = ddlFrequencyAccumDecum.FindItemByValue("WEEKLY").Index
                                    ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                    ddlAccumGUDuration.SelectedValue = strNewAccumGu
                                Case "FORTNIGHTLY", "BI-WEEKLY"
                                    ddlFrequencyAccumDecum.SelectedIndex = ddlFrequencyAccumDecum.FindItemByValue("Fortnightly").Index
                                    ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                    ddlAccumGUDuration.SelectedValue = CStr(Val(strNewAccumGu) / 2)
                                Case "MONTHLY"
                                    ddlFrequencyAccumDecum.SelectedIndex = ddlFrequencyAccumDecum.FindItemByValue("MONTHLY").Index
                                    ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                    ddlAccumGUDuration.SelectedValue = strNewAccumGu
                            End Select
                            Dim strAccumStrike As String = dtPoolDetails.Rows(0).Item("StrikePercentage").ToString
                            If strAccumStrike = "&nbsp;" Then
                                txtStrikeaccum.Text = "0.00"
                            Else
                                txtStrikeaccum.Text = strAccumStrike
                            End If
                            Dim strAccumUpfront As String = dtPoolDetails.Rows(0).Item("Upfront").ToString

                            If strAccumUpfront = "&nbsp;" Then
                                txtUpfront.Text = "0.00"
                            Else
                                txtUpfront.Text = CStr(CDbl(strAccumUpfront) / 100)

                            End If


                            If txtDuration.Text = "" Then
                                ddlAccumGUDuration.SelectedValue = "0"
                            End If

                            For i = 0 To strAccumGu.Length - 1
                                If IsNumeric(strAccumGu.Substring(i, 1)) = False Then
                                    strNewAccumGuType = (strNewAccumGuType + strAccumGu.Substring(i, 1)).Trim
                                End If
                            Next
                            ddldurationAccum.SelectedIndex = ddldurationAccum.Items.IndexOf(ddldurationAccum.Items.FindByText(strNewAccumGuType))

                            Dim strAccumKO As String = dtPoolDetails.Rows(0).Item("KOPercentage").ToString
                            txtKO.Text = strAccumKO

                            'Dim strAccumKOSettl As String = dtPoolDetails.Rows(0).Item("KO_Level").ToString       
                            'ddlKOSettlementType.SelectedValue = strAccumKOSettl       

                           

                            Dim strAccumOrderqty As String = dtPoolDetails.Rows(0).Item("Notional").ToString ''Mangesh wakode 17 dec 2015       
                            txtAccumOrderqty.Text = strAccumOrderqty
                            txtAccumOrderqty_TextChanged(Nothing, Nothing)    ''Mangesh wakode 17 dec 2015       

                            Dim strLevereageRatio As String = dtPoolDetails.Rows(0).Item("EOP_LeverageRatio").ToString

                            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Show_AQDQ_Leverage_As_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                            '    Case "Y", "YES"
                            '        If strLevereageRatio = "No" Then
                            'chkLeverageRatio.Checked = False
                            '        Else
                            'chkLeverageRatio.Checked = True
                            '        End If
                            '    Case "N", "NO"
                            If strLevereageRatio = "1" Then
                                chkLeverageRatio.Checked = False
                            Else
                                chkLeverageRatio.Checked = True
                            End If
                            'End Select
                            chkLeverageRatio_CheckedChanged(Nothing, Nothing)
                            grdAccumDecum.SelectedItemStyle.BackColor = Color.FromArgb(242, 242, 243)
                            '<Added by Mohit Lalwani on 4-Dec-2015>       




                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''       
                            resetControlsForPool(False)
                            btnSolveAll.Enabled = False
                            EnableDisableForOrderPoolData(dtPoolDetails.Rows(0).Item("PPCODE").ToString.Trim.ToUpper)
                            Session.Add("ACCDECRePricePPName", dtPoolDetails.Rows(0).Item("PPCODE").ToString.Trim.ToUpper)
                            ''getRange()

                            GetCommentary_Accum()
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
                            '</RiddhiS.>


                        Case Else
                            lblerror.Text = "Error occured while getting Pool Data."
                    End Select

                Else
                    lblerror.Text = "Received invalid Pool ID."
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''<Dilkhush/AVinash 22Dec2015:Set pool data > +-     
    Private Sub setRedirectedACCDECPoolData()
        Dim dtROrderDetails As DataTable
        Dim sROrderID As String
        Dim strNewAccumTenor As String = String.Empty
        Dim strNewAccumTenorType As String = String.Empty
        Dim strNewAccumGu As String = String.Empty
        Dim strNewAccumGuType As String = String.Empty
        Try
            tabContainer.ActiveTabIndex = prdTab.Acc
            tabIndex = prdTab.Acc
            ' tabContainer_ActiveTabChanged(Nothing, Nothing) '<Added by RushikeshD. on 29-Jan-2015 Jira:EQBOSDEV-233 >

            dtROrderDetails = New DataTable("OrderDetails")
            If Not IsNothing(Request.QueryString("RedirectedOrderId")) Then
                sROrderID = Request.QueryString("RedirectedOrderId")
                If sROrderID.Trim <> "" Then
                    Select Case objELNRFQ.Get_ACCDEC_Redirected_Order_Details(sROrderID, dtROrderDetails)
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful



                            Dim strAccumSolveFor As String = dtROrderDetails.Rows(0).Item("SolveFor").ToString
                            If strAccumSolveFor = "Strike(%)" Then
                                ddlSolveForAccumDecum.SelectedValue = "STRIKE"
                                txtStrikeaccum.Text = ""
                                txtStrikeaccum.Enabled = False
                                txtUpfront.Enabled = True
                                lblSolveForType.Text = "Strike (%)"
                            Else
                                ddlSolveForAccumDecum.SelectedValue = "UPFRONT"
                                txtUpfront.Text = ""
                                txtUpfront.Enabled = False
                                txtStrikeaccum.Enabled = True
                                lblSolveForType.Text = "Upfront (%)"
                            End If

                            ''Rushikesh 01feb2016 to set share from pool data

                            Dim strExchng As String = dtROrderDetails.Rows(0).Item("Exchange").ToString.Trim
                            Dim strShare As String = dtROrderDetails.Rows(0).Item("Underlying").ToString.Trim
                            setShare(strExchng, strShare)

                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                    'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                    'If ddlShare.SelectedValue IsNot Nothing Then
                                    '    ddlShare.Text = ddlShare.SelectedItem.Text
                                    'End If
                                    lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                                Case "N", "NO"
                                    If ddlExchangeAccumDecum.SelectedValue = strExchng Then
                                        ddlExchangeAccumDecum.SelectedValue = strExchng
                                        'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                        'If ddlShare.SelectedValue IsNot Nothing Then
                                        '    ddlShare.Text = ddlShare.SelectedItem.Text
                                        'End If
                                        lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                                    Else
                                        ddlExchangeAccumDecum.SelectedValue = strExchng ''ddlShare.Items.IndexOf(ddlShare.Items.FindByText(grdELNRFQ.Items(e.Item.ItemIndex).Cells(grdELNRFQEnum.Exchange).Text))
                                        '' Fillddl_Share()
                                        'ddlShare.SelectedIndex = ddlShare.Items.IndexOf(ddlShare.Items.FindItemByValue(strShare))
                                        ''ddlShare.Text = ddlShare.Text
                                        'If ddlShare.SelectedValue IsNot Nothing Then
                                        '    ddlShare.Text = ddlShare.SelectedItem.Text
                                        'End If
                                    End If
                            End Select
                            If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                                ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                            End If

                            ''                    Dim strAccumExchg As String = dtROrderDetails.Rows(0).Item("Exchange").ToString


                            ''                    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            ''                        Case "Y", "YES"
                            ''                            ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(dtROrderDetails.Rows(0).Item("Underlying").ToString))
                            ''                             ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            ' '' ddlShareAccumDecum.Text = ddlShareAccumDecum.Text
                            ''                            If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                            ''                                ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                            ''                            End If
                            ''                            lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                            ''                        Case "N", "NO"
                            ''                            If ddlExchangeAccumDecum.SelectedValue = strAccumExchg Then
                            ''                                ddlExchangeAccumDecum.SelectedValue = strAccumExchg
                            ''                                ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(dtROrderDetails.Rows(0).Item("Underlying").ToString))
                            ''                                ''ddlShareAccumDecum.Text = ddlShareAccumDecum.Text
                            ''                                If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                            ''                                    ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                            ''                                End If
                            ''                            Else
                            ''                                ddlExchangeAccumDecum.SelectedValue = strAccumExchg
                            ''                                ''Fill_Accum_ddl_Share()  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            ''                                ddlShareAccumDecum.SelectedIndex = ddlShareAccumDecum.Items.IndexOf(ddlShareAccumDecum.Items.FindItemByValue(dtROrderDetails.Rows(0).Item("Underlying").ToString))
                            ''                                If ddlShareAccumDecum.SelectedValue IsNot Nothing Then
                            ''                                    ddlShareAccumDecum.Text = ddlShareAccumDecum.SelectedItem.Text
                            ''                                End If
                            ''                            End If
                            ''                            lblDisplayExchangeAccumDecumVal.Text = setExchangeByShare(ddlShareAccumDecum)
                            ''                    End Select
                            ''Rushikesh 01feb2016 to set share from pool data
                            getCurrency(dtROrderDetails.Rows(0).Item("Underlying").ToString)

                            Dim strAccumTenor As String = dtROrderDetails.Rows(0).Item("Tenor").ToString

                            For i = 0 To strAccumTenor.Length - 1
                                If IsNumeric(strAccumTenor.Substring(i, 1)) = True Then
                                    strNewAccumTenor = strNewAccumTenor + strAccumTenor.Substring(i, 1)
                                End If
                            Next
                            txtTenorAccumDecum.Text = strNewAccumTenor

                            For i = 0 To strAccumTenor.Length - 1
                                If IsNumeric(strAccumTenor.Substring(i, 1)) = False Then
                                    strNewAccumTenorType = (strNewAccumTenorType + strAccumTenor.Substring(i, 1)).Trim
                                End If
                            Next

                            ddlTenorTypeAccum.SelectedValue = dtROrderDetails.Rows(0).Item("TenorType").ToString.ToUpper.Trim   ''mangesh wakode 17 dec 2015       

                            Dim strAccumGu As String = dtROrderDetails.Rows(0).Item("GuaranteedDuration").ToString

                            For i = 0 To strAccumGu.Length - 1
                                If IsNumeric(strAccumGu.Substring(i, 1)) = True Then
                                    strNewAccumGu = strNewAccumGu + strAccumGu.Substring(i, 1)
                                End If
                            Next
                            txtDuration.Text = strNewAccumGu
                            Dim straccumFrequency As String
                            straccumFrequency = dtROrderDetails.Rows(0).Item("ER_Frequency").ToString.ToUpper

                            Select Case dtROrderDetails.Rows(0).Item("ER_Frequency").ToString.ToUpper
                                Case "WEEKLY"

                                    ddlFrequencyAccumDecum.SelectedIndex = ddlFrequencyAccumDecum.FindItemByValue("WEEKLY").Index
                                    ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                    ddlAccumGUDuration.SelectedValue = strNewAccumGu
                                Case "FORTNIGHTLY", "BI-WEEKLY"
                                    ddlFrequencyAccumDecum.SelectedIndex = ddlFrequencyAccumDecum.FindItemByValue("Fortnightly").Index
                                    ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                    ddlAccumGUDuration.SelectedValue = CStr(Val(strNewAccumGu) / 2)
                                Case "MONTHLY"
                                    ddlFrequencyAccumDecum.SelectedIndex = ddlFrequencyAccumDecum.FindItemByValue("MONTHLY").Index
                                    ddlFrequencyAccumDecum_SelectedIndexChanged(Nothing, Nothing)
                                    ddlAccumGUDuration.SelectedValue = strNewAccumGu
                            End Select
                            Dim strAccumStrike As String = dtROrderDetails.Rows(0).Item("ConversionStrikePercentage").ToString
                            If strAccumStrike = "&nbsp;" Then
                                txtStrikeaccum.Text = "0.00"
                            Else
                                txtStrikeaccum.Text = strAccumStrike
                            End If
                            Dim strAccumUpfront As String = dtROrderDetails.Rows(0).Item("Upfront").ToString

                            If strAccumUpfront = "&nbsp;" Then
                                txtUpfront.Text = "0.00"
                            Else
                                txtUpfront.Text = CStr(CDbl(strAccumUpfront) / 100)
                            End If


                            If txtDuration.Text = "" Then
                                ddlAccumGUDuration.SelectedValue = "0"
                            End If

                            For i = 0 To strAccumGu.Length - 1
                                If IsNumeric(strAccumGu.Substring(i, 1)) = False Then
                                    strNewAccumGuType = (strNewAccumGuType + strAccumGu.Substring(i, 1)).Trim
                                End If
                            Next
                            ddldurationAccum.SelectedIndex = ddldurationAccum.Items.IndexOf(ddldurationAccum.Items.FindByText(strNewAccumGuType))

                            Dim strAccumKO As String = dtROrderDetails.Rows(0).Item("KOPercentage").ToString
                            txtKO.Text = strAccumKO

                            Dim strAccumKOSettl As String = dtROrderDetails.Rows(0).Item("KOSettlement").ToString
                            ddlKOSettlementType.SelectedValue = strAccumKOSettl

                            Dim strAccumType As String = dtROrderDetails.Rows(0).Item("Type").ToString
                            ddlAccumType.SelectedIndex = ddlAccumType.Items.IndexOf(ddlAccumType.Items.FindByValue(strAccumType))

                            '<Added by RushikeshD. on 29-Jan-2015 Jira:EQBOSDEV-233 >
                            If strAccumType.ToUpper = "ACCUMULATOR" Then
                                tabContainer.ActiveTabIndex = prdTab.Acc
                            ElseIf strAccumType.ToUpper = "DECUMULATOR" Then
                                tabContainer.ActiveTabIndex = prdTab.Dec
                            End If
                            '<Added by RushikeshD. on 29-Jan-2015 Jira:EQBOSDEV-233 >

                            Dim strAccumOrderqty As String = dtROrderDetails.Rows(0).Item("ER_CashOrderQuantity").ToString
                            txtAccumOrderqty.Text = strAccumOrderqty
                            txtAccumOrderqty_TextChanged(Nothing, Nothing)
                            Dim strLevereageRatio As String = dtROrderDetails.Rows(0).Item("LeverageRatio").ToString

                            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Show_AQDQ_Leverage_As_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                            '    Case "Y", "YES"
                            '        If strLevereageRatio = "No" Then
                            '            chkLeverageRatio.Checked = False
                            '        Else
                            '            chkLeverageRatio.Checked = True
                            '        End If
                            '    Case "N", "NO"
                            If strLevereageRatio = "1" Then
                                chkLeverageRatio.Checked = False
                            Else
                                chkLeverageRatio.Checked = True
                            End If
                            'End Select
                            chkLeverageRatio_CheckedChanged(Nothing, Nothing)
                            grdAccumDecum.SelectedItemStyle.BackColor = Color.FromArgb(242, 242, 243)
                            '<Added by Mohit Lalwani on 4-Dec-2015>       




                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''       
                            'resetControlsForPool(False)       
                            'btnSolveAll.Enabled = False       
                            EnableDisableForOrderPoolData(dtROrderDetails.Rows(0).Item("PPCODE").ToString.Trim.ToUpper)
                            Session.Add("ACCDECRePricePPName", dtROrderDetails.Rows(0).Item("PPCODE").ToString.Trim.ToUpper)
                            ''getRange()
                            GetCommentary_Accum()
                            'ddlNoteCcy.SelectedValue = dtROrderDetails.Rows(0).Item("CashCurrency").ToString       
                            'lblNoteCcy.Text = "( " & dtROrderDetails.Rows(0).Item("CashCurrency").ToString & " )"       
                            resetControlsForPool(False)
                            btnSolveAll.CssClass = "btn"
                        Case Else
                            lblerror.Text = "Error occured while getting Pool Data."
                    End Select

                Else
                    lblerror.Text = "Received invalid Pool ID."
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''</Dilkhush/AVinash 22Dec2015:Set pool data > 
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

            objELNRFQ.GetUDTSchema("udtAccDecRFQ", dtRFQInsertData)
            dtRFQInsertData.Rows.Add()

            dtRFQInsertData.Rows(0)("ER_PP_ID") = sPP_ID
            dtRFQInsertData.Rows(0)("ER_Type") = ddlAccumType.SelectedValue
            sProductDefinition += ddlAccumType.SelectedValue

            dtRFQInsertData.Rows(0)("ER_StrikePercentage") = Val(txtStrikeaccum.Text)
            If ddlSolveForAccumDecum.SelectedValue.ToString.ToUpper = "STRIKE" Then
                sProductDefinition += Val(txtStrikeaccum.Text).ToString + "0.0"
            Else
                sProductDefinition += "00.0"
            End If

            dtRFQInsertData.Rows(0)("ER_UnderlyingCode_Type") = "RIC"
            dtRFQInsertData.Rows(0)("ER_UnderlyingCode") = ddlShareAccumDecum.SelectedValue.ToString
            sProductDefinition += ddlShareAccumDecum.SelectedValue.ToString
            dtRFQInsertData.Rows(0)("ER_TenorType") = ddlTenorTypeAccum.SelectedValue
            dtRFQInsertData.Rows(0)("ER_Tenor") = CInt(Val(txtTenorAccumDecum.Text))
            dtRFQInsertData.Rows(0)("ER_SecurityDescription") = "Accum/Decum"
            dtRFQInsertData.Rows(0)("ER_QuoteType") = "0"
            dtRFQInsertData.Rows(0)("ER_BuySell") = "Buy"
            dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(txtAccumOrderqty.Text, ",", "")
            dtRFQInsertData.Rows(0)("ER_CashCurrency") = lblAQDQBaseCcy.Text
            dtRFQInsertData.Rows(0)("ER_TransactionTime") = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss.fff")
            dtRFQInsertData.Rows(0)("ER_ProductDefinition") = sProductDefinition
            dtRFQInsertData.Rows(0)("ER_Text") = "2012_QR12"
            dtRFQInsertData.Rows(0)("ER_Symbol") = ""
            dtRFQInsertData.Rows(0)("ER_Created_By") = LoginInfoGV.Login_Info.LoginId

            If ddlExchangeAccumDecum.SelectedValue.ToUpper = "ALL" Then
                Dim sTemp As String
                dtRFQInsertData.Rows(0)("ER_Exchange") = objELNRFQ.GetShareExchange(ddlShareAccumDecum.SelectedValue.ToString, sTemp)
            Else
                dtRFQInsertData.Rows(0)("ER_Exchange") = ddlExchangeAccumDecum.SelectedValue
            End If


            dtRFQInsertData.Rows(0)("ER_Quote_Request_YN") = "Y"
            dtRFQInsertData.Rows(0)("ER_Entity_ID") = LoginInfoGV.Login_Info.EntityID.ToString
            SetTemplateDetails(SchemeName) ''Dilkhush
            dtRFQInsertData.Rows(0)("ER_Template_ID") = Convert.ToString(Session("Template_Code_AQDQ"))
            dtRFQInsertData.Rows(0)("ER_Frequency") = ddlFrequencyAccumDecum.SelectedValue
            Dim strLeverage As String
            strLeverage = If(chkLeverageRatio.Checked = False, "1", "2")
            dtRFQInsertData.Rows(0)("ER_LeverageRatio") = If(strLeverage = Nothing, "1", strLeverage).ToString
            dtRFQInsertData.Rows(0)("ER_Upfront") = Val(txtUpfront.Text) * 100
            Select Case ddlFrequencyAccumDecum.SelectedItem.Text.ToUpper.Trim
                Case "WEEKLY"
                    dtRFQInsertData.Rows(0)("ER_GuaranteedDuration") = CStr(Val(ddlAccumGUDuration.SelectedValue))
                    dtRFQInsertData.Rows(0)("ER_GuaranteedDurationType") = "WEEK"
                Case "BI-WEEKLY"
                    dtRFQInsertData.Rows(0)("ER_GuaranteedDuration") = CStr(2 * CInt(CStr(Val(ddlAccumGUDuration.SelectedValue))))
                    dtRFQInsertData.Rows(0)("ER_GuaranteedDurationType") = "WEEK"
                Case "MONTHLY"
                    dtRFQInsertData.Rows(0)("ER_GuaranteedDuration") = CStr(Val(ddlAccumGUDuration.SelectedValue))
                    dtRFQInsertData.Rows(0)("ER_GuaranteedDurationType") = "MONTH"
            End Select


            dtRFQInsertData.Rows(0)("ER_SolveFor") = ddlSolveForAccumDecum.SelectedValue
            dtRFQInsertData.Rows(0)("ER_KOSettlement") = ddlKOSettlementType.SelectedValue

            dtRFQInsertData.Rows(0)("ER_KOPercentage") = Replace(txtKO.Text, ",", "")
            dtRFQInsertData.Rows(0)("ER_EntityName") = ddlentity.SelectedItem.Text
            Dim strLoginUserEmailID As String
            strLoginUserEmailID = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)
            dtRFQInsertData.Rows(0)("ER_EmailId") = strLoginUserEmailID
            dtRFQInsertData.Rows(0)("ER_Branch") = lblbranch.Text
            dtRFQInsertData.Rows(0)("ER_RFQ_RMName") = ddlRFQRM.SelectedItem.Value
            dtRFQInsertData.Rows(0)("ER_RFQ_Source") = "MONOTAB_PRICER"

            If ddlSolveForAccumDecum.SelectedValue = "STRIKE" Then
                dtRFQInsertData.Rows(0)("EP_Upfront") = (Val(txtUpfront.Text) * 100).ToString
            Else
                dtRFQInsertData.Rows(0)("EP_StrikePercentage") = Val(txtStrikeaccum.Text)
            End If


            Select Case objELNRFQ.Insert_Dt_AccDec_RFQ(dtRFQInsertData, sRFQId)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    Session.Add("Quote_ID", sRFQId)
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'Added by Mohit Lalwani on 1-Aug-2016
    Private Sub ddlBookingBranchPopUpValue_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBookingBranchPopUpValue.SelectedIndexChanged
        RestoreSolveAll()
        RestoreAll()

        '<RiddhiS. on 10-Nov-2016: On change of book center, reset customer grid>
        Session.Remove("dtACCDECPreTradeAllocation")
        Dim tempDt As DataTable
        tempDt = New DataTable("tempDt")
        tempDt.Columns.Add("RM_Name", GetType(String))
        tempDt.Columns.Add("Account_Number", GetType(String))
        tempDt.Columns.Add("AlloNotional", GetType(String))
        tempDt.Columns.Add("Cust_ID", GetType(String))
        tempDt.Columns.Add("DocId", GetType(String))
        tempDt.Columns.Add("EPOF_OrderId", GetType(String))
        tempDt.Rows.InsertAt(tempDt.NewRow(), 0)
        Session.Add("dtACCDECPreTradeAllocation", tempDt)
        grdRMData.DataSource = tempDt
        grdRMData.DataBind()
        For Each row As GridViewRow In grdRMData.Rows
            If row.RowType = DataControlRowType.DataRow Then
                row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = True
            End If
            OnCheckedChanged(CType(grdRMData.Rows((0)).Cells(0).FindControl("CheckBox1"), CheckBox), Nothing)
        Next

    End Sub
    '/Added by Mohit Lalwani on 1-Aug-2016

    'Added by Mohit Lalwani on 1-Aug-2016
    Private Sub txtOrderCmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderCmt.TextChanged
        RestoreSolveAll()
        RestoreAll()
    End Sub
    '/Added by Mohit Lalwani on 1-Aug-2016

    ''Added By nikhil M on  08Aug16 EQSCB-16
    Public Function SetDllBookingBranch() As Boolean
        ddlBookingBranchPopUpValue.Items.Add(New DropDownListItem("Hong Kong", "HK"))
        ddlBookingBranchPopUpValue.Items.Add(New DropDownListItem("Singapore", "SG"))
        ddlBookingBranchPopUpValue.SelectedIndex = 1
    End Function
    Private Sub Generate_ACCDEC(ByVal DealNO As String)
   
	 Try
            Dim objDocGen As Web_DocGenOpenXML.DocumentGenerationOpenXml
            objDocGen = New Web_DocGenOpenXML.DocumentGenerationOpenXml
            objDocGen.Url = LoginInfoGV.Login_Info.WebServicePath & "/DocumentGenerationOpenXml/DocumentGenerationOpenXml.asmx"
            Dim O_Outputfile As String
            Dim strDocGenVirtualPath As String = String.Empty
            Dim strProduct As String = String.Empty
            Dim strURL As String = String.Empty


            'tabContainer.ActiveTabIndex = prdTab.Acc
            ' ddlAccumType.SelectedValue = "ACCUMULATOR"
            If tabContainer.ActiveTabIndex = prdTab.Acc Then
                strProduct = "ACC"
            Else
                strProduct = "DAC"
            End If

            strDocGenVirtualPath = objReadConfig.ReadConfig(New DataSet, "WebDocGen_VirtualPath", "DocGen", CStr(LoginInfoGV.Login_Info.EntityID), "")
            O_Outputfile = objDocGen.StartDocumentGeneration(LoginInfoGV.Login_Info.LoginId, strProduct, "PUBLISH_TERMSHEET", DealNO, "ELN_RFQ", LoginInfoGV.Login_Info.EntityID.ToString, LoginInfoGV.Login_Info.GlobalServerDateTime, 1)

            '<RiddhiS. on 08-Nov-2016: For creating proper URL>
            strURL = LoginInfoGV.Login_Info.WebServicePath.ToString.Substring(0, LoginInfoGV.Login_Info.WebServicePath.ToString.IndexOf("FinIQWeb_WebService"))



            'objDocGen.Generate_ACCDEC(DealNO, LoginInfoGV.Login_Info.LoginId, LoginInfoGV.Login_Info.EntityID, "PUBLISH_TERMSHEET", "ELN_RFQ", O_Outputfile)
            If Not IsNothing(O_Outputfile) Then
                If O_Outputfile.Length > 0 Then

                    '<RiddhiS. on 17-Oct-2016: Termsheet Attachment>
                    Dim termSheetName As String = ""
                    termSheetName = "TermSheet"
                    If ddlAccumType.SelectedValue.ToString.Trim.ToUpper = "ACCUMULATOR" Then
                        termSheetName = termSheetName + "_Accumulator"
                    Else
                        termSheetName = termSheetName + "_Decumulator"
                    End If

                    If chkLeverageRatio.Checked = True Then
                        termSheetName = termSheetName + "_Leverage"
                    Else
                        termSheetName = termSheetName + "_NoLeverage"
                    End If

                    If ddlAccumGUDuration.SelectedValue = "0" Then
                        termSheetName = termSheetName + "_NoGuarantee"
                    Else
                        termSheetName = termSheetName + "_Guarantee"
                    End If
                    termSheetName = termSheetName + ".PDF"
                



                    If strProduct = "ACC" Then
                        'hdnKYIRFile.Value = LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/" & "KYIR_AQ.pdf"
                        hdnKYIRFile.Value = strURL & "/../" & strDocGenVirtualPath & "/" & "KYIR_AQ.pdf"
                    Else
                        ' hdnKYIRFile.Value = LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/" & "KYIR_DQ.pdf"
                        hdnKYIRFile.Value = strURL & "/../" & strDocGenVirtualPath & "/" & "KYIR_DQ.pdf"
                    End If
                    'hdnTermsheetFile.Value = LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/Termsheet/" & termSheetName
                    hdnTermsheetFile.Value = strURL & "/../" & strDocGenVirtualPath & "/Termsheet/" & termSheetName

                    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSgetDescription5446", " window.open('" & LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/" & System.IO.Path.GetFileName(O_Outputfile) & "','CUSTOMER_PROFILE','scrollbars=yes,resizable=yes,menubar=0,status=0,width=1280,height=650,top=0,left=0'); openPDF();", True)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSgetDescription5446", " window.open('" & strURL & "/../" & strDocGenVirtualPath & "/" & System.IO.Path.GetFileName(O_Outputfile) & "','CUSTOMER_PROFILE','scrollbars=yes,resizable=yes,menubar=0,status=0,width=1280,height=650,top=0,left=0'); openPDF();", True)

                    '  ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSSpineer", "setSpineer();", True)
                Else
                    lblerror.Text = "Document generation failed !"
                End If
            Else
            End If
        Catch ex As Exception
            lblerror.Text = "Generate_ACCDEC: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "Generate_ACCDEC", ErrorLevel.High)
        End Try
    End Sub
    'Private Sub Generate_Deal(ByVal DealNO As String)
    '    Try
    '        Dim objDocGen As Web_DocGen.DocGen
    '        objDocGen = New Web_DocGen.DocGen
    '        objDocGen.Url = LoginInfoGV.Login_Info.WebServicePath & "/DocumentGeneration/DocGen.asmx"
    '        Dim O_Outputfile() As String
    '        Dim strDocGenVirtualPath As String = String.Empty
    '        strDocGenVirtualPath = objReadConfig.ReadConfig(New DataSet, "WebDocGen_VirtualPath", "DocGen", CStr(LoginInfoGV.Login_Info.EntityID), "")
    '        objDocGen.Generate_ACCDEC(DealNO, LoginInfoGV.Login_Info.LoginId, LoginInfoGV.Login_Info.EntityID, "PUBLISH_TERMSHEET", "RM_ORDER", O_Outputfile)
    '        If Not IsNothing(O_Outputfile) Then
    '            If O_Outputfile.Length > 0 Then
    '                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSgetDescription5446", "window.open('" & LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/" & O_Outputfile(O_Outputfile.Length - 1).ToString & "','CUSTOMER_PROFILE','scrollbars=yes,resizable=yes,menubar=0,status=0,width=1280,height=650,top=0,left=0');", True)
    '                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSSpineer", "setSpineer();", True)
    '            Else
    '                lblerror.Text = "Document generation failed !"
    '            End If
    '        Else
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub addPPimg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles addPPimg.Click
        chkPPmaillist.Visible = True
    End Sub


    Private Function GetBestPriceConfirm(ByVal BasePrice As String, ByVal IssureName As String) As Boolean
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

            'If temp.Value.Split(CChar(","))(0) = BasePrice.Split(CChar(","))(0) Then
            '    Return True
            'Else
            '    lblerrorPopUp.Text = IssureName & " is not best price. Cannot proceed with this issuer"
            '    Return False
            'End If
            Return True
        Catch ex As Exception

        End Try
    End Function
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

            lblError.Text = "TmRefresh_Tick: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "TmRefresh_Tick", ErrorLevel.High)
        End Try
    End Sub
    'Ended by Chitralekha on 12-sept-16


    ''<Start | Nikhil M. on 17-Sep-2016: Commented For Removing Chkbox dependency>


    Private Sub chkBNPP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBNPP.CheckedChanged
        Try
            hdnBestStrike.Value = lblBNPPPrice.Text
            hdnBestProvider.Value = "BNPP"
            CheckUncheck(chkBNPP)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "chkBNPP_CheckedChanged: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "chkBNPP_CheckedChanged", ErrorLevel.High)
        End Try

    End Sub

    Private Sub chkCITI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCITI.CheckedChanged
        Try
            hdnBestStrike.Value = lblCITIPrice.Text
            hdnBestProvider.Value = "CITI"
            CheckUncheck(chkCITI)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "chkCITI_CheckedChanged: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "chkCITI_CheckedChanged", ErrorLevel.High)
        End Try

    End Sub

    Private Sub chkCS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCS.CheckedChanged
        Try
            hdnBestStrike.Value = lblCSPrice.Text
            hdnBestProvider.Value = "CS"
            CheckUncheck(chkCS)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "chkCS_CheckedChanged: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "chkCS_CheckedChanged", ErrorLevel.High)
        End Try

    End Sub

    Private Sub chkDBIB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDBIB.CheckedChanged
        Try
            hdnBestStrike.Value = lblDBIBPrice.Text
            hdnBestProvider.Value = "DBIB"
            CheckUncheck(chkDBIB)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "chkDBIB_CheckedChanged: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "chkDBIB_CheckedChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub chkHSBC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHSBC.CheckedChanged
        Try
            hdnBestStrike.Value = lblHSBCPrice.Text
            hdnBestProvider.Value = "HSBC"
            CheckUncheck(chkHSBC)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "chkHSBC_CheckedChanged: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "chkHSBC_CheckedChanged", ErrorLevel.High)
        End Try

    End Sub

    Private Sub chkJPM_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkJPM.CheckedChanged
        Try
            hdnBestStrike.Value = lblJPMPrice.Text
            hdnBestProvider.Value = "JPM"
            CheckUncheck(chkJPM)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "chkJPM_CheckedChanged: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "chkJPM_CheckedChanged", ErrorLevel.High)
        End Try

    End Sub

    Private Sub chkUBS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUBS.CheckedChanged
        Try
            hdnBestStrike.Value = lblUBSPrice.Text
            hdnBestProvider.Value = "UBS"
            CheckUncheck(chkUBS)
            ''<Nikhil M. on 22-Oct-2016: Added for Enabling the conrol for Hedge>
            If UCase(Request.QueryString("EXTLOD")) = "HEDGE" Then
                EnableDisableForOrderPoolData(Convert.ToString(Session("RePricePPName")))
            End If
        Catch ex As Exception
            lblerror.Text = "chkUBS_CheckedChanged: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "chkUBS_CheckedChanged", ErrorLevel.High)
        End Try

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
            tempCheckBox.Checked = True
            If flagSHowHide = True Then
                CheckBestPrice()
                tempCheckBox.Checked = False
            Else
                tempCheckBox.Checked = True
            End If
            GetCommentary_Accum()
            RestoreSolveAll()
            RestoreAll()
        Catch ex As Exception
            lblerror.Text = "CheckUncheck: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "CheckUncheck", ErrorLevel.High)
        End Try

    End Function
    ''<End | Nikhil M. on 17-Sep-2016: Commented For Removing Chkbox dependency>
      ''' <summary>
    ''' Get the setup value from the session saved datatable as per user or entity or else return passed default value
    ''' </summary>
    ''' <param name="ControlName">Control display label or referential present on screen.</param>
    ''' <param name="DefaultValue">Default value to be returned if not value is found.</param>
    ''' <returns>String value for calling control name.</returns>
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
     Private Sub btnAddAllocation_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAllocation.ServerClick
        Try
            '' chkDuplicateRecords()
            grdRMData.Visible = True
            funAdd()

        Catch ex As Exception
            lblerror.Text = "Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                          sSelfPath, "btnAddAllocation_ServerClick", ErrorLevel.High)
        End Try
    End Sub
    ''Ended by Chitralekha M on 20-Sep-16
Private Sub funAdd()
        Try

            Dim tempDt As DataTable
            tempDt = New DataTable("tempDt")
            If CType(Session("dtACCDECPreTradeAllocation"), DataTable) Is Nothing Then
            Else
                tempDt = CType(Session("dtACCDECPreTradeAllocation"), DataTable)
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

            Session.Add("dtACCDECPreTradeAllocation", tempDt)
            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Visible = False  'Changed by Chitralekha om 21-Sep-16
            chkUpfrontOverride.Checked = False      'Changed by Chitralekha om 21-Sep-16
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

                    ddl_OnSelectedIndexChanged(CType(grdRMData.Rows(k).Cells(0).FindControl("ddlRMName"), RadDropDownList), Nothing)  'Mohit Lalwani on 04-Nov-2016

                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).setCustName = tempDt.Rows(k).Item("Account_Number").ToString
                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCustomerName = tempDt.Rows(k).Item("Account_Number").ToString
                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCustomerId = tempDt.Rows(k).Item("Cust_ID").ToString
                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenDocId = tempDt.Rows(k).Item("DocId").ToString
                    'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAllocationRMName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper    ''Commented by AshwiniP on 13-Oct-2016
                    '    Case "Y", "YES"
                    ''End Select
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
            '  Dim maxAcclength As String = objReadConfig.ReadConfig(dsConfig, "EQC_PostTrade_AccountNumLength", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "10").Trim.ToUpper   ''Commented by AshwiniP on 13-Oct-2016
            tempDt = CType(Session("dtACCDECPreTradeAllocation"), DataTable)
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
                            row.Cells(i).Controls.OfType(Of TextBox)().FirstOrDefault().Attributes.Add("onkeypress", "AllowOnlyNumeric()")      'Changed by Chitralekha on 29-Sep-16

                            'If row.Cells(i).Controls.OfType(Of TextBox)().FirstOrDefault().ID = "txtAccNo" Then
                            '    row.Cells(i).Controls.OfType(Of TextBox)().FirstOrDefault().Attributes.Add("MaxLength", maxAcclength) ''29dec2015
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

                        ''''<Nikhil M. on 26-Sep-2016:For CustPan Added >
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
            chkNoShares()
            lblerrorPopUp.Text = ""
        Catch ex As Exception
            lblError.Text = "Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                          sSelfPath, "OnCheckedChanged", ErrorLevel.High)
        End Try
    End Sub


    Protected Sub ddl_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim I As Integer
            ''<Nikhil M. on 26-Sep-2016: added>
            Dim dtCIFPANTemp As DataTable
            dtCIFPANTemp = New DataTable("dtCIFPANTemp")
            I = DirectCast(CType(CType(CType(sender, RadDropDownList).Parent, DataControlFieldCell).Parent, GridViewRow), System.Web.UI.WebControls.GridViewRow).DataItemIndex

            Dim tempDt As DataTable
            tempDt = CType(Session("dtACCDECPreTradeAllocation"), DataTable)
            dtCIFPANTemp = New DataTable("dtCIFPANTemp")
            If CType(sender, RadDropDownList).ID = "ddlRMName" Then
                tempDt.Rows(I).Item("RM_Name") = CType(sender, RadDropDownList).SelectedItem.Text
                grdRMData.Rows(I).Cells(grdRMDataEnum.RM_Name).Controls.OfType(Of Label)().FirstOrDefault().Text = CType(sender, RadDropDownList).SelectedItem.Text
                'Added by Mohit Lalwani on 22-Oct-2016
                Dim FindCustomer As FinIQ_Fast_Find_Customer
                FindCustomer = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer)
                'Mohit Lalwani on 04-Nov-2016
                FindCustomer.setCustName = ""
                FindCustomer.HiddenCustomerName = ""
                FindCustomer.HiddenCustomerId = ""
                FindCustomer.HiddenDocId = ""
                '/Mohit Lalwani on 04-Nov-2016
                '/Added by Mohit Lalwani on 22-Oct-2016
            End If
            Session.Add("dtACCDECPreTradeAllocation", tempDt)
            Session.Add("dtCustomerCodes", dtCIFPANTemp)  '<RiddhiS. on 03-Oct-2016: To get Customer Segment>
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
              sSelfPath, "ddl_OnSelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    
    
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
    ''<Nikhil M. on 20-Sep-2016: Added to reset COmmetry element>
    Public Function ResetCommetryElement() As Boolean
        Try
            hdnBestStrike.Value = ""
            hdnBestProvider.Value = ""
            ResetAllChkBox()
            GetCommentary_Accum()
        Catch ex As Exception
				Throw ex
        End Try

    End Function
Protected Sub chkNoShares()
	

        Dim tempDt As DataTable
        Try
	
	tempDt = New DataTable("tempDt")
        Dim sDuplicateRec As String = ""
        Dim j As Integer = 0
        Dim x As Integer = 0
        tempDt = CType(Session("dtACCDECPreTradeAllocation"), DataTable)
        Dim duplicatesNo As IEnumerable(Of String)
        Dim duplicatesName As IEnumerable(Of String)

        Dim TotalSum As Double
        TotalSum = 0

        For Each row As GridViewRow In grdRMData.Rows
                If row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim <> "" _
                And row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked Then
                TotalSum = TotalSum + CDbl(row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
            End If
        Next
        lblTotalShares.Visible = True
        lbltxtTotalShares.Visible = True
        lblAllocatedShares.Visible = True
        lbltxtTotalShares.Text = lblNotionalPopUpValue.Text

        lbltxtAllocatedShares.Visible = True
        lbltxtAllocatedShares.Text = CStr(TotalSum)
        lblRemainingShares.Visible = True
        lbltxtRemainingShares.Visible = True

        lbltxtRemainingShares.Text = CStr(CDbl(lbltxtTotalShares.Text) - CDbl(lbltxtAllocatedShares.Text))
        
        
        If CDbl(lbltxtAllocatedShares.Text) > CDbl(lbltxtTotalShares.Text) Then
            lbltxtTotalShares.ForeColor = Color.Black
            lbltxtAllocatedShares.ForeColor = Color.Black
        Else
            lbltxtAllocatedShares.ForeColor = Color.Black
            lbltxtTotalShares.ForeColor = Color.Black
            lblAllocatedShares.ForeColor = Color.Black
        End If
        If ((CDbl(lbltxtRemainingShares.Text) > CDbl(lbltxtTotalShares.Text)) Or CDbl(lbltxtRemainingShares.Text) < 0) Then
            lblRemainingShares.ForeColor = Color.Red
            lbltxtRemainingShares.ForeColor = Color.Red
        Else
            lblRemainingShares.ForeColor = Color.Green
            lbltxtRemainingShares.ForeColor = Color.Green
        End If
        'lblValidateShares.Text = 
	Catch ex as Exception
		Throw ex
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
                        'Xml.Append("<EPOF_OrderInfo>" & CType((row.Cells(2).Controls.OfType(Of FinIQ_Fast_Find_Customer)().FirstOrDefault()), FinIQ_Fast_Find_Customer).HiddenCIFNo & "</EPOF_OrderInfo>") ''<Nikhil M. on 24-Sep-2016: Changed>
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



    Function chkDuplicateRecords() As Boolean
        Try
            Dim tempDt As DataTable
            tempDt = New DataTable("tempDt")
            Dim sDuplicateRec As String = ""
            Dim sDuplicateRec1 As String = ""
            Dim j As Integer = 0
            Dim x As Integer = 0
            tempDt = CType(Session("dtACCDECPreTradeAllocation"), DataTable)


            If Not tempDt Is Nothing Then
                If tempDt.Rows.Count = 0 Then
                    lblerrorPopUp.Text = "No records to insert."
                    Return False
                End If
            End If


            Dim duplicatesNo As IEnumerable(Of String)
            Dim duplicatesName As IEnumerable(Of String)

            '' '' '' chkUpfrontOverride.Visible = False
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




            'Select Case CStr(objReadConfig.ReadConfig(dsConfig, "EQC_ShowAllocationAccountNumber", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper)
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

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim tempDt As DataTable
                tempDt = CType(Session("dtACCDECPreTradeAllocation"), DataTable)


                Dim rowDataBoundIndex As Integer = e.Row.RowIndex


                Dim ddlRM_Name As RadDropDownList = TryCast(e.Row.FindControl("ddlRMName"), RadDropDownList)

                Dim lblRM_Name As Label
                If Not IsNothing(TryCast(e.Row.FindControl("txtRM_Name"), Label)) Then
                    lblRM_Name = TryCast(e.Row.FindControl("txtRM_Name"), Label)
                Else
                    lblRM_Name.Text = ""

                End If



                With ddlRM_Name
                    .DataSource = Session("ACCDEC_DTRMList")
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

    Protected Sub ddlCIFPAN_onTextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim I, cntCodes As Integer
        Dim tempDt As DataTable
        Dim dtCustomerCodes As DataTable
        tempDt = CType(Session("dtACCDECPreTradeAllocation"), DataTable)
        dtCustomerCodes = CType(Session("dtCustomerCodes"), DataTable)
        lblerrorPopUp.Text = ""

        Try
            ''<Start | Nikhil M. on 23-Sep-2016:Changed >
            If Not TypeOf sender Is TextBox Then
                I = DirectCast(CType(CType(CType(sender, RadDropDownList).Parent, DataControlFieldCell).Parent, GridViewRow), System.Web.UI.WebControls.GridViewRow).DataItemIndex

                If CType(sender, RadDropDownList).ID = "ddlCIFPAN" Then
                    If CType(sender, RadDropDownList).SelectedText.Trim = "" Then
                        lblerrorPopUp.Text = "Please select code."
                        Exit Sub
                    End If
                    tempDt.Rows(I).Item("Account_Number") = CType(sender, RadDropDownList).SelectedItem.Text
                    grdRMData.Rows(I).Cells(grdRMDataEnum.Account_Number).Controls.OfType(Of Label)().FirstOrDefault().Text = CType(sender, RadDropDownList).SelectedItem.Text



                    ''<RiddhiS. on 10-Nov-2016: Commented>
                    ''After discussion with Milind K, Sanchita: Booking center dropdown is made editable and based on the selection customer's are filtered.

                    '<RiddhiS. on 03-Oct-2016: To change Booking Branch based on CIF of first row in Allocation grid>
                    ''Check first record and if it is a Retail customer then set Retail as booking center
                    'If I = 0 Then               ''To Change only according to first row
                    '    Dim drCustSegment As DataRow = dtCustomerCodes.Select("CIF_PANId = '" + CType(sender, RadDropDownList).SelectedItem.Text + "'")(0)
                    '    Dim sFirstCustSegment As String = drCustSegment.Item("Segment").ToString

                    '    If sFirstCustSegment.ToUpper = "RETAIL" Then
                    '        Dim sAvailableBKC As String()
                    '        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
                    '            If ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
                    '                ddlBookingBranchPopUpValue.SelectedIndex = iBKC
                    '                Exit For
                    '            End If
                    '        Next
                    '    ElseIf sFirstCustSegment.ToUpper = "PVB" Then
                    '        Dim sAvailableBKC As String()
                    '        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
                    '            If Not ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
                    '                ddlBookingBranchPopUpValue.SelectedIndex = iBKC
                    '                Exit For
                    '            End If
                    '        Next
                    '    End If
                    'For cntCodes = 0 To dtCustomerCodes.Rows.Count - 1
                    '    If dtCustomerCodes.Rows(cntCodes).Item("CIF_PANId").ToString = CType(sender, RadDropDownList).SelectedItem.Text Then
                    '        If dtCustomerCodes.Rows(cntCodes).Item("Segment").ToString.ToUpper = "RETAIL" Then
                    '            ddlBookingBranchPopUpValue.SelectedValue = "Retail"
                    '            Exit For
                    '        Else
                    '            ddlBookingBranchPopUpValue.SelectedValue = "SG"
                    '        End If
                    '    End If
                    'Next
                    'End If
                    '</RiddhiS. on 03-Oct-2016>


                End If
                ''<End | Nikhil M. on 23-Sep-2016:Changed >
            ElseIf CType(sender, TextBox).ID = "txtAlloNotional" Then

                'If Qty_Validate(CType(sender, TextBox).Text) = False Then
                '    Exit Sub
                'End If
                I = DirectCast(CType(CType(CType(sender, TextBox).Parent, DataControlFieldCell).Parent, GridViewRow), System.Web.UI.WebControls.GridViewRow).DataItemIndex ''<Nikhil M. on 26-Sep-2016: Added>

                CType(sender, TextBox).Text = FinIQApp_Number.ConvertFormattedAmountToNumber(CType(sender, TextBox).Text).ToString

                tempDt.Rows(I).Item(2) = CType(sender, TextBox).Text
                grdRMData.Rows(I).Cells(grdRMDataEnum.Notional).Controls.OfType(Of Label)().FirstOrDefault().Text = CType(sender, TextBox).Text
            End If
            Session.Add("dtACCDECPreTradeAllocation", tempDt)
            Dim TotalSum As Double
            TotalSum = 0
            For Each row As GridViewRow In grdRMData.Rows
                If row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim <> "" And (row.Cells(4).Controls.OfType(Of Label)().FirstOrDefault().Text.Trim <> "" Or row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked) Then
                    TotalSum = TotalSum + CDbl(row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                End If
            Next
            chkNoShares()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
              sSelfPath, "ddlCIFPAN_onTextChanged", ErrorLevel.High)
        End Try
    End Sub


    Private Sub chkBAML_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBAML.CheckedChanged
        Try
            hdnBestStrike.Value = lblBAMLPrice.Text
            hdnBestProvider.Value = "BAML"
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
            hdnBestStrike.Value = lblOCBCPrice.Text
            hdnBestProvider.Value = "OCBC"
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
            hdnBestStrike.Value = lblLEONTEQPrice.Text
            hdnBestProvider.Value = "LEONTEQ"
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
            hdnBestStrike.Value = lblCOMMERZPrice.Text
            hdnBestProvider.Value = "COMMERZ"
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
            fill_Accum_Decum_Grid()
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
            
            Generate_ACCDEC(strBAMLID)

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

            Generate_ACCDEC(strBNPPID)

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

            Generate_ACCDEC(strCITIID)

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

            Generate_ACCDEC(strCOMMERZID)

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

            Generate_ACCDEC(strCSID)

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

            Generate_ACCDEC(strDBIBID)

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
            Generate_ACCDEC(strHSBCID)

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
            strJPMID = CStr(hashRFQID(hashPPId("JPM")))
            Generate_ACCDEC(strJPMID)

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

            Generate_ACCDEC(strLEONTEQID)

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

            Generate_ACCDEC(strOCBCID)

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

            Generate_ACCDEC(strUBSID)

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
                        lblAdvisoryFlagVal.ForeColor = Color.Red
                        lblAdvisoryFlagVal.Text = "No"
                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                        lblAdvisoryFlagVal.ForeColor = Color.Red
                        lblAdvisoryFlagVal.Text = "No"
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
        tempDt = CType(Session("dtACCDECPreTradeAllocation"), DataTable)
        lblerrorPopUp.Text = ""
        Try
            I = CInt(Customer_Info.ItemIndex)
            tempDt.Rows(I).Item("Account_Number") = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).GetCustName
            tempDt.Rows(I).Item("Cust_ID") = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCustomerId  'Customer_Info.CustomerCIFNo
            tempDt.Rows(I).Item("DocId") = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenDocId
            grdRMData.Rows(I).Cells(grdRMDataEnum.Account_Number).Controls.OfType(Of Label)().FirstOrDefault().Text = Customer_Info.CustomerName      'Customer_Info.CustomerName

            Session.Add("dtACCDECPreTradeAllocation", tempDt)
            Dim TotalSum As Double
            TotalSum = 0
            For Each row As GridViewRow In grdRMData.Rows
                If row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim <> "" And (row.Cells(4).Controls.OfType(Of Label)().FirstOrDefault().Text.Trim <> "" Or row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked) Then
                    TotalSum = TotalSum + CDbl(row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text)
                End If
            Next
            chkNoShares()


            ''<RiddhiS. on 10-Nov-2016: Commented>
            ''After discussion with Milind K, Sanchita: Booking center dropdown is made editable and based on the selection customer's are filtered.

            'If I = 0 Then               ''To Change only according to first row
            '    '<AvinashG on 09-Oct-2016: Removing temporary drop down and using customer control itself>

            '    Dim sFirstCustName As String

            '    sFirstCustName = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).GetCustName

            '    If sFirstCustName.ToUpper.Contains("TSO") Then
            '        Dim sAvailableBKC As String()
            '        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
            '            If ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
            '                ddlBookingBranchPopUpValue.SelectedIndex = iBKC
            '                Exit For
            '            End If
            '        Next
            '    Else
            '        Dim sAvailableBKC As String()
            '        For iBKC As Integer = 0 To ddlBookingBranchPopUpValue.Items.Count - 1
            '            If Not ddlBookingBranchPopUpValue.Items.Item(iBKC).Value.ToUpper.Contains("RETAIL") Then
            '                ddlBookingBranchPopUpValue.SelectedIndex = iBKC
            '                Exit For
            '            End If
            '        Next
            '    End If
            'End If

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


        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
              sSelfPath, "CustomerSelected", ErrorLevel.High)
        End Try

    End Sub

    '/Added by Mohit Lalwani on 30-sept-2016 for customer control


    Private Sub btnSaveSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveSettings.Click
        Dim o_strXMLNote_DefaultValues As String
        Dim DefaultSetting_Level As String
        Dim ApplicationID As String
        Try
            If Not IsNothing(Request.QueryString("PrdToLoad")) Then
                If UCase(Request.QueryString("PrdToLoad")) = "ACCUMULATOR" Then
                    ApplicationID = "ACC_DealEntry"
                Else
                    ApplicationID = "DEC_DealEntry"
                End If
            Else
                ApplicationID = "ACC_DealEntry"
            End If
            If Write_PersonalSettings_TOXML(o_strXMLNote_DefaultValues) = True Then
                Select Case objELNRFQ.Web_Insert_ACCDEC_DefaultSettings(CStr(o_strXMLNote_DefaultValues), CStr(LoginInfoGV.Login_Info.EntityID), LoginInfoGV.Login_Info.LoginId, ApplicationID, objReadConfig.ReadConfig(dsConfig, "EQC_DefaultSetting_Level", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "ENTITY").Trim.ToUpper, LoginInfoGV.Login_Info.LoginId)
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

