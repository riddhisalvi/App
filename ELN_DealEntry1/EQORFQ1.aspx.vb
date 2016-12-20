Imports System.Windows
Imports System.Drawing
Imports System.Web.UI.DataVisualization.Charting
Imports System.Threading
Imports System.ComponentModel
Imports System.Xml
Imports Telerik.Web.UI
Imports System.IO

Partial Public Class EQORFQ1
    Inherits FinIQAppMain
    Private Const sSelfPath As String = "FinIQWebApp/ELN_DealEntry1/EQORFQ1.aspx"
    Dim oWEBMarketData As WEB_FINIQ_MarketData.FINIQ_WEBSRV_MarketData
    Private oWebCustomerProfile As Web_CustomerProfile.CustomerProfile
    Dim oWEBADMIN As WEB_ADMINISTRATOR.LSSAdministrator
    Dim ObjCommanData As Web_CommonFunction.CommonFunction ''Added By Nikhil M On 08Aug2016 EQSCB-16
    Dim objELNRFQ As Web_ELNRFQ.ELN_RFQ
    Dim generateDocument As Web_DocumentGeneration.GenerateDocumentsForWeb
    '<AvinashG. on 14-Aug-2014: Not used for now>Dim objDocGen As Web_DocGen.DocGen
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
    Dim strBNPPId As String
    Dim strQuantoCcy As String
    Dim strAmount As String
    Dim strXMLNote_RFQ As String
    Dim strEntityId As String
    Dim flag As String
    Public flag1 As Boolean = False
    Dim strcount As String
    Dim StrnoteRFQXML As String
    Dim SchemeName As String
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
    Dim BNPP_ID As String
    Public Structure ChartColors
        Dim BNPP As Color
    End Structure
    Public Shared structChartColors As ChartColors
    Public Shared dtShareEQO As DataTable


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
    '    Public dblRMMargin As Double ''KBM on 29-April-2014
    '    Public dblUpfront As Double
    '    '<Rutuja S. on 26-Feb-2015: Added for EQO>
    '    Public strSecuritySubType As String
    '    Public strExerciseType As String
    '    Public strSettlementType As String
    '    Public strUnderlyingAltCode As String
    '    Public strUnderlyingAltCodeType As String
    '    Public strStrikeType As String
    '    Public strQuoteType As String
    '    Public strQuanto_Currency As String
    '    Public strBuySell As String
    '    Public dblPremium As Double
    '    Public strBarrierType As String
    '    Public strBarriermode As String
    '    Public strUnderlyingProduct As String
    '    Public strNoOfShares As String   'Added by Imran P 12-Nov-2015

    'End Structure
    ''--
    Public hitCount As Integer = 0
    Dim orderValidityTimer As Integer = 0 'In Seconds
#End Region

#Region "Enum"
    Private Enum prdTab
        ELN
        FCN
        DRA
        Acc
        Dec
        EQO
    End Enum
    Enum grdOrderEnum
        RFQ_ID
        Ext_Order_ID            'Mohit on 30-06-2016
        'Ext_RFQ_ID_DuoteId 'Mohit on 30-06-2016
        Order_ID_DatarID
        Order_Details
        ER_RMName
        Provider
        Type
        ER_EQO_Quantity_Type
        Order_Status
        Order_Type
        Limit_Prc1
        Limit_Prc2
        Limit_Prc3
        Exec_Prc1
        Exec_Prc2
        Exec_Prc3
        Avg_Exec_Prc
        Ordered_Qty
        Filled_Qty
        Tenormths
        Underlying
        Ccy_DataField
        Quanto_Currency
        SettlementType
        ExerciseType
        Premium
        Barrier
        BarrType
        Option_Type
        Strike
        Upfront
        Client_Price
        Margin
        Notional_Amount
        Order_Remark
        Booking_Branch
        Order_Requested_At
        Trade_Date
        Settlement_Date
        Expiry_Date
        Maturity_Date
        Tenor
        SolveFor
        Value
        created_by
        Client_side
        StrikeType
        EP_OrderComment
    End Enum
    '<Rutuja S. on 03-Mar-2015: Added for EQO>
    Enum grdEQORFQEnum
        RFQ_ID
        RFQ_Details '<Mohit Lalwani on 20-Jan-2015: Added for BoS>
        Generate_Doc
        Solve_For
        Client_side
        Provider
        Type
        Underlying
        Product
        Strike_Type
        Strike
        Upfront
        Premium
        Barrier
        BarrType
        Tenormths
        Tenor
        Ccy
        Underlying_Ccy
        Settl_Ccy
        Settl_Type
        Option_Type
        Security_Sub_Type
        ''Nominal_Amount
        Order_Qty
        EQO_Quantity_Type
        Trade_Date
        Settlement_Date
        Expiry_Date
        Maturity_Date
        Exchange
        Value
        Remark
        Ext_RFQ_ID
        Quote_Requested_At
        ClubbingRFQId
        Created_By

    End Enum
    '</Rutuja S. on 03-Mar-2015: Added for EQO

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
        Dim dtSupported_Option_Type As DataTable
        Try
            '<RiddhiS. on 28-Oct-2016: Clear session>
            Session.Remove("Scheme_EQO")
            Session.Remove("Template_Code_EQO")
            '</RiddhiS.>
            dtSupported_Option_Type = New DataTable("Option_Type")
            WebServicePath = String.Empty
            WebServicePath = ConfigurationManager.AppSettings("EQSP_WebServiceLocation").ToString
            WebServicePath = Request.Url.Scheme & Uri.SchemeDelimiter & WebServicePath
            objELNRFQ = New Web_ELNRFQ.ELN_RFQ
            objELNRFQ.Url = WebServicePath & "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx"
            oWEBMarketData = New Web_FinIQ_MarketData.FINIQ_WEBSRV_MarketData()
            oWEBMarketData.Url = LoginInfoGV.Login_Info.WebServicePath & "/WebELN_DealEntry/FINIQ_WEBSRV_MarketData.asmx"

            ''<Email Doc gen Services>
            oWEBADMIN = New WEB_ADMINISTRATOR.LSSAdministrator()
            oWEBADMIN.Url = LoginInfoGV.Login_Info.WebServicePath & "/LSSAdministrator/LSSAdministrator.asmx"

            generateDocument = New Web_DocumentGeneration.GenerateDocumentsForWeb
            generateDocument.Url = LoginInfoGV.Login_Info.WebServicePath & "/DocumentGeneration/DocumentGeneration.asmx"
            generateDocument.Credentials = System.Net.CredentialCache.DefaultCredentials
            ''</Email Doc gen Services>
            'Mohit Lalwani on 01-sept-2016 
            oWebCustomerProfile = New Web_CustomerProfile.CustomerProfile()
            oWebCustomerProfile.Url = LoginInfoGV.Login_Info.WebServicePath & "/Customer_Profile/CustomerProfile.asmx"
            '/Mohit Lalwani on 01-sept-2016 
            ''<Dilkhush/Rushikesh 24Oct2016: Added url for commondata object>
            ObjCommanData = New Web_CommonFunction.CommonFunction
            ObjCommanData.Url = LoginInfoGV.Login_Info.WebServicePath & "/CommonFunction/CommonFunction.asmx"

            '<AvinashG. on 21-Oct-2016:EQSCB-79 - URL schema binding on Share search>
            ddlShareEQO.WebServiceSettings.Path = WebServicePath & "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            ddlShareEQO.WebServiceSettings.Method = "GetSharesNames"
            ddlShareEQO2.WebServiceSettings.Path = WebServicePath & "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            ddlShareEQO2.WebServiceSettings.Method = "GetSharesNames"
            ddlShareEQO3.WebServiceSettings.Path = WebServicePath & "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            ddlShareEQO3.WebServiceSettings.Method = "GetSharesNames"
            'ddlShareEQO.WebServiceSettings.Path = "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            'ddlShareEQO.WebServiceSettings.Method = "GetSharesNames"
            'ddlShareEQO2.WebServiceSettings.Path = "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            'ddlShareEQO2.WebServiceSettings.Method = "GetSharesNames"
            'ddlShareEQO3.WebServiceSettings.Path = "/FinIQWeb_WebService/WebELN_DealEntry/ELN_RFQ.asmx" ''objELNRFQ.Url
            'ddlShareEQO3.WebServiceSettings.Method = "GetSharesNames"
            '</AvinashG. on 21-Oct-2016:EQSCB-79 - URL schema binding on Share search>
            Select Case tabContainer.ActiveTabIndex
                Case prdTab.EQO
                    hitCount = CInt(objReadConfig.ReadConfig(dsConfig, "FINIQ_ELN_RFQ", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "30"))
            End Select
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

                '<RiddhiS. on 10-Nov-2016: Pass booking center as filter to customer find control>
                FindCustomer.CustomerBookingCenter = ddlBookingBranchPopUpValue.SelectedValue

            Next
            '/Mohit Lalwani on 27-Sept-2016


            If Page.IsPostBack = False AndAlso Request.HttpMethod = "GET" Then
                Session.Remove("dtEQOPreTradeAllocation")




                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                    Case "BOTH"
                        grdRMData.Columns(3).HeaderText = "No. of shares/Notional"

                    Case "NOTIONAL"
                        grdRMData.Columns(3).HeaderText = "Notional"
                    Case "NOOFSHARE"
                        grdRMData.Columns(3).HeaderText = "No. of shares"
                End Select


                'Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options


                'Mohit Lalwani on 7-Sept-2016 for Personal Settings
                dtPersonalSettings = New DataTable("Personal Settings")
                Session.Remove("dtCustomerCodes")
                'ObjCommanData = New Web_CommonFunction.CommonFunction
                Select Case ObjCommanData.Web_Get_DefaultPersonalSettingsInfo(LoginInfoGV.Login_Info.EntityID, LoginInfoGV.Login_Info.LoginId, "EQO_DealEntry", dtPersonalSettings)
                    Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                        Session.Add("dtPersonalSettings", dtPersonalSettings)
                    Case Web_CommonFunction.Database_Transaction_Response.Db_No_Data
                        Session.Add("dtPersonalSettings", dtPersonalSettings)
                    Case Web_CommonFunction.Database_Transaction_Response.DB_Unsuccessful
                        Session.Add("dtPersonalSettings", dtPersonalSettings)
                End Select
                '/Mohit Lalwani on 7-Sept-2016 for Personal Settings

                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                    Case "BOTH"
			  rdbQuantity.Checked = True
                        rdbNotional.Checked = False

                        rdbQuantity.Visible = True
                        txtOrderqtyEQO.Visible = True
                        lblOrderqtyEQO.Visible = True
                        lblTitleNotional.Visible = True
                        rdbNotional.Visible = True
                        txtNotional.Visible = True
                        chkrdbQuantity()
                        txtOrderqtyEQO.Enabled = True
                        txtOrderqtyEQO.BackColor = Color.White
                        txtNotional.Enabled = False
                        'txtOrderqtyEQO_TextChanged(Nothing, Nothing)
                        grdEQORFQ.Columns(grdEQORFQEnum.EQO_Quantity_Type).Visible = True
                        grdOrder.Columns(grdOrderEnum.ER_EQO_Quantity_Type).Visible = True

                    Case "NOTIONAL"
                        
                        rdbQuantity.Checked = False
                        rdbNotional.Checked = True
                        rdbQuantity.Enabled = False
                        rdbQuantity.Visible = False
                        txtOrderqtyEQO.Visible = False
                        lblOrderqtyEQO.Visible = False
                        lblTitleNotional.Visible = True
                        rdbNotional.Visible = True
                        txtNotional.Visible = True

                        chkrdbQuantity()
                        txtOrderqtyEQO.Enabled = False
                        txtNotional.BackColor = Color.White
                        txtNotional.Enabled = True
                        'txtNotional_TextChanged(Nothing, Nothing)
                        grdEQORFQ.Columns(grdEQORFQEnum.EQO_Quantity_Type).Visible = False
                        grdOrder.Columns(grdOrderEnum.ER_EQO_Quantity_Type).Visible = False
                       
                    Case "NOOFSHARE"
                        
                        rdbQuantity.Checked = True
                        rdbNotional.Checked = False
                        rdbNotional.Enabled = False
                        rdbQuantity.Visible = True
                        txtOrderqtyEQO.Visible = True
                        lblOrderqtyEQO.Visible = True
                        lblTitleNotional.Visible = False
                        rdbNotional.Visible = False
                        txtNotional.Visible = False

                        chkrdbQuantity()
                        txtOrderqtyEQO.Enabled = True
                        txtOrderqtyEQO.BackColor = Color.White
                        txtNotional.Enabled = False
                        'txtOrderqtyEQO_TextChanged(Nothing, Nothing)
                        grdEQORFQ.Columns(grdEQORFQEnum.EQO_Quantity_Type).Visible = False
                        grdOrder.Columns(grdOrderEnum.ER_EQO_Quantity_Type).Visible = False
                End Select
'/Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options
                If ((objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper = "NO" Or _
                                     objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper = "N") And _
                                     (objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "NO" Or _
                                     objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "N")) Then

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
                    If count = 2 Then
                        rblShareData.Visible = True
                        tdrblShareData.Visible = True '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
                    Else
                        rblShareData.Visible = False
                        tdrblShareData.Visible = False '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
                    End If

                End If




'Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options
                'Mohit and Rushi on 24-Apr-2016
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAQDQ_Estimated_Notional", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                            Case "BOTH"
                                fsEstimates.Visible = True
                                lblEstimatedNotional.Visible = True
                                lblNotionalWithCcy.Visible = True
                            Case "NOTIONAL"
                                fsEstimates.Visible = False
                                lblEstimatedNotional.Visible = False
                                lblNotionalWithCcy.Visible = False
                            Case "NOOFSHARE"
                                fsEstimates.Visible = True
                                lblEstimatedNotional.Visible = True
                                lblNotionalWithCcy.Visible = True
                        End Select
                    Case "N", "NO"
                        fsEstimates.Visible = False
                        lblEstimatedNotional.Visible = False
                        lblNotionalWithCcy.Visible = False
                End Select

                If rdbNotional.Checked Then
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                        Case "BOTH"
                            lblNotionalWithCcy1.Visible = True
                        Case "NOTIONAL"
                            lblNotionalWithCcy1.Visible = True
                        Case "NOOFSHARE"
                            lblNotionalWithCcy1.Visible = True
                    End Select
                Else
                    lblNotionalWithCcy1.Visible = False
                End If
'/Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options

                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableStrikeType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        ddlStrikeTypeEQO.Enabled = True
                        ddlStrikeTypeEQO.BackColor = Color.White
                    Case "N", "NO"
                        ddlStrikeTypeEQO.Enabled = False
                        ddlStrikeTypeEQO.BackColor = Color.FromArgb(242, 242, 243)
                End Select

                'This Config is added by Mohit Lalwani on 7-Jul-2016 as told by Sanchita 
                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableClientSide", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        ddlSideEQO.Enabled = True
                        ddlSideEQO.BackColor = Color.White
                    Case "N", "NO"
                        ddlSideEQO.Enabled = False
                        ddlSideEQO.BackColor = Color.FromArgb(242, 242, 243)
                End Select
                '/This Config is added by Mohit Lalwani on 7-Jul-2016 as told by Sanchita


                '<MohitL. on 15-Apr-2016:Set Default notional size and daily no. of shares to 0 on main pricer screen JIRA ID: EQBOSDEV-321>
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SetZeroNotional_MainPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        txtOrderqtyEQO.Text = "0"
                    Case "N", "NO"
                        ' txtOrderqtyEQO.Text = "1,000"
                End Select
                '</MohitL. on 15-Apr-2016:Set Default notional size and daily no. of shares to 0 on main pricer screen JIRA ID: EQBOSDEV-321>


                'Commented by Mohit Lalwani on 7-Jul-2016 as told by Sanchita 
                'ddlSideEQO.Enabled = True
                'ddlSideEQO.BackColor = Color.White
                '/Commented by Mohit Lalwani on 7-Jul-2016 as told by Sanchita 



                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ColorGrayedOut", " $('#ctl00_MainContent_tabContainer_tabPanelEQO_txtTradeDate_txtDate').css('background-color', '#D3D3D3');", True)
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        lblSelectionExchangeEQOHeader.Visible = False
                        lblDisplayExchangeEQOHeader.Visible = True
                        lblExchangeEQO.Visible = True
                        lblExchangeEQO2.Visible = True
                        lblExchangeEQO3.Visible = True
                        ddlExchangeEQO.Visible = False
                        ddlExchangeEQO2.Visible = False
                        ddlExchangeEQO3.Visible = False
                    Case "N", "NO"
                        lblSelectionExchangeEQOHeader.Visible = True
                        lblDisplayExchangeEQOHeader.Visible = False
                        lblExchangeEQO.Visible = False
                        lblExchangeEQO2.Visible = False
                        lblExchangeEQO3.Visible = False
                End Select

                'Added by Mohit Lalwani on 20-Jan-2015
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowRFQandOrderDetails", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper()
                    Case "Y", "YES"
                        grdEQORFQ.Columns(grdEQORFQEnum.RFQ_Details).Visible = True
                    Case "N", "NO"
                        grdEQORFQ.Columns(grdEQORFQEnum.RFQ_Details).Visible = False
                End Select

                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowRFQandOrderDetails", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper()
                    Case "Y", "YES"
                        grdOrder.Columns(grdOrderEnum.Order_Details).Visible = True
                    Case "N", "NO"
                        grdOrder.Columns(grdOrderEnum.Order_Details).Visible = False
                End Select
                '/Added by Mohit Lalwani on 20-Jan-2015

                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_CaptureOrderComment", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        grdOrder.Columns(grdOrderEnum.EP_OrderComment).Visible = True
                    Case "N", "NO"
                        grdOrder.Columns(grdOrderEnum.EP_OrderComment).Visible = False
                End Select

                tabContainer.ActiveTabIndex = prdTab.EQO
                tabPanelEQO.Visible = True
                txtLimitPricePopUpValue.Text = "0"
                txtLimitPricePopUpValue.Enabled = False
                lblMsgPriceProvider.Text = ""
                lblerror.Text = ""
                lblBNPPPrice.Text = ""
                '  lblComentry1.Visible = False     'Mohit Lalwani on 5-Apr-2016
                '  lblComentry2.Visible = False      'Mohit Lalwani on 5-Apr-2016
                txttrade.Value = FinIQApp_Date.FinIQDate(Now.ToShortDateString)
                btnCancelReq.Text = objReadConfig.ReadConfig(dsConfig, "EQC_ResetButtonText", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "Reset").Trim
                Fill_Entity()
                fill_RMList()
                fill_RFQRMList()
                fill_All_EntityBooks()
                chk_Login_For_PP()

                Get_Price_Provider()

                setKeyPressValidations()

                SetTemplateDetails(SchemeName)  ''use to get active tab index
                '<AvinashG. on 07-Apr-2016: SHare 3 is controlled by checkbox of share 2 hence not required to have it in config case>
                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_AllowBasket", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        tblShareEQO_2.Visible = True
                        tdAddShare1.Visible = True
                        tdHeadAddShare1.Visible = True
                        ddlInvestCcy.Visible = True
                        txtOrderqtyEQO.Width = 70
                    Case "N", "NO"
                        tblShareEQO_2.Visible = False
                        tdAddShare1.Visible = False
                        tdHeadAddShare1.Visible = False
                        ddlInvestCcy.Visible = False
                        txtOrderqtyEQO.Width = 125
                End Select
                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_AllowUnderlyingType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        trUType.Visible = True
                    Case "N", "NO"
                        trUType.Visible = False
                End Select
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowOrderOnMainPricerPageLoad", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        rbHistory.SelectedValue = "Order History"
                        fill_OrderGrid()
                        '<MohitL. on 04-Nov-2015: FA-1174>
                        ColumnVisibility()
                        '<MohitL. on 04-Nov-2015: FA-1174>
                    Case "N", "NO"
                        rbHistory.SelectedValue = "Quote History"
                        fill_EQO_Grid()
                End Select
                SetChartColors()
                btnBNPPDeal.Visible = False

                '<AvinashG. on 05-Apr-2016: >
                fill_All_Exchange()
                'fillEQOExchanges()
                '<AvinashG. on 05-Apr-2016: >
                'fillEQOShare()'<AvinashG. on 05-Apr-2016: As per Load-on-demand> 
                fillOptionType()
                fillQuantoForEQO()
                If ddlShareEQO.SelectedValue.Trim <> "" Then
                    lblEQOBaseCcy.Text = getBaseCurrency(ddlShareEQO.SelectedValue)
                End If

                ddlSettlCcyEQO.SelectedValue = lblEQOBaseCcy.Text
                ddlInvestCcy.SelectedValue = lblEQOBaseCcy.Text
                GetDatesForEQO()
                txtOrderqtyEQO_TextChanged(sender, e)
                ''
                ddlExchangeEQO_SelectedIndexChanged(sender, e)
                ddlOptionType_SelectedIndexChanged(sender, e)
                lblClientPriceCaption.Text = "Client Premuim(%)"
                lblClientYieldCaption.Text = ""
                DisplayEstimatedNotional()  'Added by Imran  12-Nov-2015
                If rbHistory.SelectedValue = "Quote History" Then
                    Call fill_EQO_Grid()
                    upnlGrid.Update()
                    makeThisGridVisible(grdEQORFQ)
                End If
                '<AvinashG. on 07-Apr-2016: >getRange()  '<AvinashG. on 04-Sep-2014: Commented as ddlExchnage SelectedIndexChange will get limit for first load.>


                txtStrikeEQO.Text = "100.00"
                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_DisableQuanto", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        ddlSettlCcyEQO.Enabled = False
                        ddlSettlCcyEQO.BackColor = Color.FromArgb(242, 242, 243)
                    Case "N", "NO"
                        If ddlsettlmethod.SelectedValue = "Cash" Then               'Mohit Lalwani on 13-Apr-2016
                            ddlSettlCcyEQO.Enabled = True
                            ddlSettlCcyEQO.BackColor = Color.White
                        Else
                            ddlSettlCcyEQO.Enabled = False
                            ddlSettlCcyEQO.BackColor = Color.FromArgb(242, 242, 243)
                        End If
                End Select
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

                    tdCommentry.Visible = False
                    tdgrphShareData.Visible = False
                    trSaveSetting.Visible = True
                    tdShareGraphData.Visible = False

                Else
                    trSaveSetting.Visible = False
                End If
            Else
                trSaveSetting.Visible = False
            End If
            '/Mohit Lalwani on 12-Oct-2016





            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Fill_All_Charts()
            Else
                Call manageShareReportShowHide()
            End If
            hideShowRBLShareData()
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "setResolution", "setResolution();", True)
            '<Mohit 28-Apr-2016>
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "CP_Dragable", "CP_Dragable();", True)
            '</Mohit 28-Apr-2016>
        Catch ex As Exception
            lblerror.Text = "Error occurred on Page Load event."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Page_Load", ErrorLevel.High)
        End Try
    End Sub


    'Added by Mohit Lalwani on 16-Sept-2016
    Public Sub SetPersonalSetting()
        Dim dtROrderDetails As DataTable
        Dim strNewTenorEQO As String = String.Empty
        Dim strNewTenorEQOType As String = String.Empty
        Dim dtBaseCCY As DataTable
        Try

            'Dim strExchngforEQO As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.Exchange).ToString
            'Dim strProductForEQO As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.ELNType).ToString
            'Dim strTypeForEQO As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.Option_Type).ToString
            'Dim strSecuritySubType As String = dtROrderDetails.Rows(0)(RedirectOrderDetails.Security_Sub_Type).ToString

            ddlOptionType.SelectedValue = getControlPersonalSetting("Option Type", "European Call")

            ddlOptionType_SelectedIndexChanged(Nothing, Nothing)
            Dim arrExchanges As String()
            Dim arrShares As String()
            '         Dim dtBaseCCY As DataTable

            '     arrExchanges = dtROrderDetails.Rows(0)(RedirectOrderDetails.Exchange).ToString.Split(CChar(","))
            arrShares = getControlPersonalSetting("Share", "").Split(CChar(","))
            '   lblEmail.Text = dtROrderDetails.Rows(0)(RedirectOrderDetails.ER_EmailId).ToString


            If arrShares.Length > 0 Then
                chkAddShare1.Checked = True
                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_AllowBasket", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        chkAddShare1_CheckedChanged(Nothing, Nothing)
                    Case "N", "NO"

                End Select
                ddlExchangeEQO.Enabled = True
                ddlShareEQO.Enabled = True
                FillDRAddl_exchange(ddlExchangeEQO)
                Dim dtExchange As DataTable
                dtExchange = New DataTable("Dummy")
                Select Case objELNRFQ.DB_GetExchange(arrShares(0), dtExchange)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        arrExchanges = dtExchange.Rows(0)(0).ToString.Split(CChar(","))  'Added by Mohit Lalwani on 17-Oct-2016
                        '   ddlExchangeDRAFCN_SelectedIndexChanged(Nothing, Nothing)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                End Select

                If arrShares(0).Equals("") Then
                Else
                    setShare1(arrExchanges(0), arrShares(0))   'Added by Mohit Lalwani on 17-Oct-2016
                End If


                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        lblExchangeEQO.Text = setExchangeByShare(ddlShareEQO)
                    Case "N", "NO"
                        Try
                            ddlExchangeEQO.SelectedValue = arrExchanges(0)
                        Catch ex As Exception
                            lblerror.Text = "Exchange missing from setup."
                        End Try
                        lblExchangeEQO.Text = setExchangeByShare(ddlShareEQO)
                End Select
                If ddlShareEQO.SelectedItem IsNot Nothing Then
                    ddlShareEQO.Text = ddlShareEQO.SelectedItem.Text
                End If
                dtBaseCCY = New DataTable("BaseCcy")
                ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                Select Case objELNRFQ.DB_GetBASECCY(ddlShareEQO.SelectedValue, dtBaseCCY)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        lblEQOBaseCcy.Text = dtBaseCCY.Rows(0)(0).ToString
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                End Select
                'If arrExchanges.Length >= 2 Then
                '    chkAddShare2.Checked = True
                '    chkAddShare2_CheckedChanged(Nothing, Nothing)
                '    ddlExchangeEQO2.Enabled = True
                '    ddlShareEQO2.Enabled = True
                '    FillDRAddl_exchange(ddlExchangeEQO2)
                '    setShare2(arrExchanges(1), arrShares(1))
                '    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                '        Case "Y", "YES"
                '            ''ddlShareDRA2.SelectedIndex = ddlShareDRA2.Items.IndexOf(ddlShareDRA2.Items.FindItemByValue(arrShares(1)))
                '            ''   ddlShareDRA2.Text = ddlShareDRA2.Text
                '            lblExchangeEQO2.Text = setExchangeByShare(ddlShareEQO2)
                '        Case "N", "NO"
                '            Try
                '                ddlExchangeEQO2.SelectedValue = arrExchanges(1)
                '            Catch ex As Exception
                '                lblerror.Text = "Exchange missing from setup."
                '            End Try
                '            lblExchangeEQO2.Text = setExchangeByShare(ddlShareEQO2)
                '    End Select
                '    If ddlShareEQO2.SelectedItem IsNot Nothing Then
                '        ddlShareEQO2.Text = ddlShareEQO2.SelectedItem.Text
                '    End If
                '    dtBaseCCY = New DataTable("BaseCcy")
                '    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                '    Select Case objELNRFQ.DB_GetBASECCY(ddlShareEQO2.SelectedValue, dtBaseCCY)
                '        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                '            lblBaseCurrency2.Text = dtBaseCCY.Rows(0)(0).ToString
                '        Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                '        Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                '    End Select
                'End If

                'If arrExchanges.Length = 3 Then
                '    chkAddShare3.Checked = True
                '    ddlExchangeEQO3.Enabled = True
                '    ddlShareEQO3.Enabled = True
                '    FillDRAddl_exchange(ddlExchangeEQO3)
                '    setShare3(arrExchanges(2), arrShares(2))
                '    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                '        Case "Y", "YES"
                '            lblExchangeEQO3.Text = setExchangeByShare(ddlShareEQO3)
                '        Case "N", "NO"
                '            Try
                '                ddlExchangeEQO3.SelectedValue = arrExchanges(2)
                '            Catch ex As Exception
                '                lblerror.Text = "Exchange missing from setup."
                '            End Try
                '            lblExchangeEQO3.Text = setExchangeByShare(ddlShareEQO3)
                '            dtBaseCCY = New DataTable("BaseCcy")
                '    End Select
                '    If ddlShareEQO3.SelectedItem IsNot Nothing Then
                '        ddlShareEQO3.Text = ddlShareEQO3.SelectedItem.Text
                '    End If
                '    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                '    Select Case objELNRFQ.DB_GetBASECCY(ddlShareEQO3.SelectedValue, dtBaseCCY)
                '        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                '            lblBaseCurrency3.Text = dtBaseCCY.Rows(0)(0).ToString
                '        Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                '        Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                '    End Select
                'End If
            End If
            txtTenorEQO.Text = getControlPersonalSetting("Tenor", "3")

            ddlTenorEQO.SelectedValue = getControlPersonalSetting("Tenor Type", "MONTH")

            Dim strTypeStrike As String = getControlPersonalSetting("Strike Type", "Percentage")
            ddlStrikeTypeEQO.SelectedValue = strTypeStrike

            txtStrikeEQO.Text = getControlPersonalSetting("Strike", "100.00")
            txtPremium.Text = getControlPersonalSetting("Premium", "")
            txtUpfrontEQO.Text = getControlPersonalSetting("Upfront", "0.5")
            txtBarrierLevelEQO.Text = getControlPersonalSetting("Barrier", "90.00")
            ddlBarrierEQO.SelectedValue = getControlPersonalSetting("Barrier Type", "Absolute")

            ddlBarrierMonitoringType.SelectedValue = getControlPersonalSetting("Barrier Monitoring Type", "Continuous")

            GetDatesForEQO()

            Dim strSolveForEQO As String = getControlPersonalSetting("Solve for", "PREMIUM")
            ddlSolveforEQO.SelectedValue = strSolveForEQO
            ddlSolveforEQO_SelectedIndexChanged(Nothing, Nothing)
            Dim strSideForEQO As String = getControlPersonalSetting("Client side", "Sell")
            ddlSideEQO.SelectedValue = strSideForEQO

            fillddlInvestCcy()

            ddlInvestCcy.SelectedValue = getControlPersonalSetting("Investment Ccy", "AUD")

            ddlsettlmethod.SelectedValue = getControlPersonalSetting("Settlement", "Physical")
            ddlSettlCcyEQO.Enabled = False
            ddlSettlCcyEQO.BackColor = Color.LightGray


            Dim strSettCcyForEQO As String = getControlPersonalSetting("Settlement Ccy", "AUD")
            ddlSettlCcyEQO.SelectedValue = strSettCcyForEQO
            chkrdbQuantity()

            Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                Case "BOTH"
                    rdbQuantity.Visible = True
                    txtOrderqtyEQO.Visible = True
                    lblOrderqtyEQO.Visible = True
                    lblTitleNotional.Visible = True
                    rdbNotional.Visible = True
                    txtNotional.Visible = True
                    If getControlPersonalSetting("Share or Notional", "SHARE") = "SHARE" Then
                        rdbQuantity.Checked = True
                        rdbNotional.Checked = False
                        chkrdbQuantity()
                        txtOrderqtyEQO.Enabled = True
                        txtOrderqtyEQO.BackColor = Color.White
                        txtNotional.Enabled = False
                        txtOrderqtyEQO.Text = getControlPersonalSetting("No. of shares", "2,000")
                        txtOrderqtyEQO_TextChanged(Nothing, Nothing)
                    ElseIf getControlPersonalSetting("Share or Notional", "SHARE") = "NOTIONAL" Then
                        rdbQuantity.Checked = False
                        rdbNotional.Checked = True
                        chkrdbQuantity()
                        txtOrderqtyEQO.Enabled = False
                        txtNotional.BackColor = Color.White
                        txtNotional.Enabled = True
                        txtOrderqtyEQO.Text = ""
                        txtNotional.Text = getControlPersonalSetting("Notional", "1,000,000")
                        txtNotional_TextChanged(Nothing, Nothing)
                    End If
                    'rdbQuantity.Checked = True
                    'rdbNotional.Checked = False
                Case "NOTIONAL"
                    rdbQuantity.Checked = False
                    rdbQuantity.Enabled = False
                    rdbNotional.Checked = True
                    rdbQuantity.Visible = False
                    txtOrderqtyEQO.Visible = False
                    lblOrderqtyEQO.Visible = False
                    lblTitleNotional.Visible = True
                    rdbNotional.Visible = True
                    txtNotional.Visible = True
                    chkrdbQuantity()
                    txtOrderqtyEQO.Enabled = False
                    txtNotional.BackColor = Color.White
                    txtNotional.Enabled = True
                    txtOrderqtyEQO.Text = ""
                    txtNotional.Text = getControlPersonalSetting("Notional", "1,000,000")
                    txtNotional_TextChanged(Nothing, Nothing)
                    '     lblerror.Text = "Order quantity can not be in number of shares."
                Case "NOOFSHARE"
                    rdbQuantity.Checked = True
                    rdbNotional.Checked = False
                    rdbNotional.Enabled = False
                    rdbQuantity.Visible = True
                    txtOrderqtyEQO.Visible = True
                    lblOrderqtyEQO.Visible = True
                    lblTitleNotional.Visible = False
                    rdbNotional.Visible = False
                    txtNotional.Visible = False
                    chkrdbQuantity()
                    txtOrderqtyEQO.Enabled = True
                    txtOrderqtyEQO.BackColor = Color.White
                    txtNotional.Enabled = False
                    txtOrderqtyEQO.Text = getControlPersonalSetting("No. of shares", "2,000")
                    txtOrderqtyEQO_TextChanged(Nothing, Nothing)
            End Select




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
            strXMLRFQ.Append("<Option_Type>" & ddlOptionType.SelectedValue & "</Option_Type>")
            strXMLRFQ.Append("<Share>" & ddlShareEQO.SelectedValue & "</Share>")
            strXMLRFQ.Append("<Tenor>" & txtTenorEQO.Text & "</Tenor>")
            strXMLRFQ.Append("<Tenor_Type>" & ddlTenorEQO.SelectedValue & "</Tenor_Type>")
            strXMLRFQ.Append("<Strike_Type>" & ddlStrikeTypeEQO.SelectedValue & "</Strike_Type>")
            strXMLRFQ.Append("<Strike>" & txtStrikeEQO.Text & "</Strike>")
            strXMLRFQ.Append("<Premium>" & txtPremium.Text & "</Premium>")
            strXMLRFQ.Append("<Upfront>" & txtUpfrontEQO.Text & "</Upfront>")
            strXMLRFQ.Append("<Barrier>" & txtBarrierLevelEQO.Text & "</Barrier>")
            strXMLRFQ.Append("<Barrier_Type>" & ddlBarrierEQO.SelectedValue & "</Barrier_Type>")
            strXMLRFQ.Append("<Barrier_Monitoring_Type>" & ddlBarrierMonitoringType.SelectedValue & "</Barrier_Monitoring_Type>")
            strXMLRFQ.Append("<Solve_for>" & ddlSolveforEQO.SelectedValue & "</Solve_for>")
            strXMLRFQ.Append("<Client_side>" & ddlSideEQO.SelectedValue & "</Client_side>")
            strXMLRFQ.Append("<Investment_Ccy>" & ddlInvestCcy.SelectedValue & "</Investment_Ccy>")
            strXMLRFQ.Append("<Settlement>" & ddlsettlmethod.SelectedValue & "</Settlement>")
            strXMLRFQ.Append("<Settlement_Ccy>" & ddlSettlCcyEQO.SelectedValue & "</Settlement_Ccy>")
            If rdbQuantity.Checked = True Then
                strXMLRFQ.Append("<Share_or_Notional>SHARE</Share_or_Notional>")
            Else
                strXMLRFQ.Append("<Share_or_Notional>NOTIONAL</Share_or_Notional>")
            End If

            strXMLRFQ.Append("<Notional>" & txtNotional.Text & "</Notional>")

            strXMLRFQ.Append("<No_of_shares>" & txtOrderqtyEQO.Text & "</No_of_shares>")
            
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



    Sub fillOptionType()
        Dim dtSupported_Option_Type As DataTable
        dtSupported_Option_Type = New DataTable("Option_Type")
        Select Case WebCommonFunction.DB_Get_Common_Data("EQO_Option_Type", dtSupported_Option_Type)
            Case Web_CommonFunction.Database_Transaction_Response.Db_Successful
                For Each dr As DataRow In dtSupported_Option_Type.Select("", "Data_Value")
                    ddlOptionType.Items.Add(New DropDownListItem(dr("Data_Value").ToString, dr("Data_Value").ToString))
                    ''ddlOptionType.Items.Add(New ListItem(dr("Misc1").ToString, dr("Data_Value").ToString))
                    ''Dilkhush/Avinash: 01jul2016 Commented above line to load datavalue in label as well as in value instead of two diff value case 79
                Next
            Case Else
                With ddlOptionType
                    ddlOptionType.Items.Add(New DropDownListItem("European Call", "European Call"))
                    ddlOptionType.Items.Add(New DropDownListItem("European Put", "European Put"))
                    ddlOptionType.Items.Add(New DropDownListItem("KnockIn Put", "KnockIn Put"))
                    .SelectedValue = "European Call"
                End With
        End Select



        'Select Case objReadConfig.ReadConfig(dsConfig, "Display_AmericanCallPut", "EQO", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
        '    Case "Y", "YES"
        '        ddlOptionType.Items.Add(New ListItem("European Call", "European Call"))
        '        ddlOptionType.Items.Add(New ListItem("European Put", "European Put"))
        '        ddlOptionType.Items.Add(New ListItem("American Call", "American Call"))
        '        ddlOptionType.Items.Add(New ListItem("American Put", "American Put"))
        '        ddlOptionType.Items.Add(New ListItem("KnockIn Put", "KnockIn Put"))
        '    Case "N", "NO"
        '        ddlOptionType.Items.Add(New ListItem("European Call", "European Call"))
        '        ddlOptionType.Items.Add(New ListItem("European Put", "European Put"))
        '        ddlOptionType.Items.Add(New ListItem("KnockIn Put", "KnockIn Put"))
        'End Select

    End Sub


    Public Sub SetChartColors()
        Try
            structChartColors = New ChartColors
            With Me.structChartColors
                .BNPP = System.Drawing.Color.FromArgb(37, 53, 131)
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


            ''<Added on 8July for upfront validation>
            txtUpfrontPopUpValue.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtUpfrontPopUpValue.ClientID & "');")
            ''<Added on 8July for upfront validation>
            txtNotional.Attributes.Add("onkeypress", "KeysAllowedForNotional()")
            txtUpfrontEQO.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtUpfrontEQO.ClientID & "');")
            ddlExchangeEQO.Attributes.Add("onchange", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            ddlShareEQO.Attributes.Add("onchange", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            ddlShareEQO2.Attributes.Add("onchange", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            ddlShareEQO3.Attributes.Add("onchange", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            txtNotional.Attributes.Add("onchange", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            txtOrderqtyEQO.Attributes.Add("onchange", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            txtUpfrontPopUpValue.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtUpfrontPopUpValue.ClientID & "');")

            txtLimitPricePopUpValue.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtLimitPricePopUpValue.ClientID & "');")

            '<Rutuja S. on 03-Mar-2015: Added for EQO tab controls>
            txtStrikeEQO.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtStrikeEQO.ClientID & "');")
            txtPremium.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtPremium.ClientID & "');")
            txtBarrierLevelEQO.Attributes.Add("onkeypress", "AllowNumericWithdecimal('" & txtBarrierLevelEQO.ClientID & "');")
            txtTenorEQO.Attributes.Add("onkeypress", "AllowOnlyNumeric()")
            '  txtOrderqtyEQO.Attributes.Add("onkeypress", "KeysAllowedForNotional()") 
            txtOrderqtyEQO.Attributes.Add("onkeypress", "AllowOnlyNumeric()")

            'PriceButton  StopTimer 
            btnBNPPPrice.Attributes.Add("onclick", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")

            'SolveAll StopTimer
            btnSolveAll.Attributes.Add("onclick", "StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
        Catch ex As Exception
            lblerror.Text = "setKeyPressValidations:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setKeyPressValidations", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
#Region "ShareReport"
    '<Rutuja S. on 12-Jan-2015: FA-775 Download and display TR DSS data on pricer page>
    ''' <summary>
    ''' Gets Data and fills the first set of share report
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setTDSSData(ByVal strShare As String)
        Dim dtDssData As DataTable
        Try
            '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
            Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
            LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": In setTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData", ErrorLevel.None)
            '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
            '<AvinashG. on 08-Sep-2015: Config controlled service call>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    dtDssData = New DataTable("Share_data")
                    If (strShare.Trim <> "") Then
                        '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                        Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                        LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": EQC_ShowTRShareInfo found YES, calling webservice method Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData", ErrorLevel.None)
                        '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                        Select Case objELNRFQ.Web_GetTDSSData(strShare, dtDssData)
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": Db_Successful from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock.Text = dtDssData.Rows(0)("RICCode").ToString
                                lblStockDesc.Text = dtDssData.Rows(0)("Description").ToString.ToUpper
                                lblSpotDate.Text = If(dtDssData.Rows(0)("SpotLastUpdateDate").ToString.Trim = "", "", CDate(dtDssData.Rows(0)("SpotLastUpdateDate").ToString).ToString("dd-MMM-yy HH:mm"))
                                lblSpotValue.Text = SetNumberFormat(dtDssData.Rows(0)("Spot").ToString, 2)
                                lbl52WkHighDate.Text = If(dtDssData.Rows(0)("Last1YearHighDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("Last1YearHighDate").ToString))
                                lbl52WkHighValue.Text = SetNumberFormat(dtDssData.Rows(0)("Last1YearHighValue").ToString, 2)
                                lbl52WkLowDate.Text = If(dtDssData.Rows(0)("Last1YearLowDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("Last1YearLowDate").ToString))
                                lbl52WkLowValue.Text = SetNumberFormat(dtDssData.Rows(0)("Last1YearLowValue").ToString, 2)
                                'lblYTDChngValue.Text = SetNumberFormat(dtDssData.Rows(0)("YTDchngperc").ToString, 2) & "%"
                                'lblMTDChngValue.Text = SetNumberFormat(dtDssData.Rows(0)("MTDchngperc").ToString, 2) & "%"
                                'lbl1YearChngValue.Text = SetNumberFormat(dtDssData.Rows(0)("1yearchngperc").ToString, 2) & "%"
                                lblYTDChngValue.Text = SetNumberFormat(dtDssData.Rows(0)("YTDchngperc").ToString, 2)
                                lblMTDChngValue.Text = SetNumberFormat(dtDssData.Rows(0)("MTDchngperc").ToString, 2)
                                lbl1YearChngValue.Text = SetNumberFormat(dtDssData.Rows(0)("1yearchngperc").ToString, 2)
                                lbl20DHistVolCurr.Text = SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaCurr").ToString, 2)
                                'lbl20DHistVolHi.Text = SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaHi").ToString, 2)
                                'lbl20DHistVolLo.Text = SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaLo").ToString, 2)
                                lbl60DHistVolCurr.Text = SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaCurr").ToString, 2)
                                'lbl60DHistVolHi.Text = SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaHi").ToString, 2)
                                'lbl60DHistVolLo.Text = SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaLo").ToString, 2)
                                lbl250DHistVolCurr.Text = SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaCurr").ToString, 2)
                                'lbl250DHistVolHi.Text = SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaHi").ToString, 2)
                                'lbl250DHistVolLo.Text = SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaLo").ToString, 2)
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
                                '<AvinashG. on 29-May-2015:     FA-885 EQC Share info: show the 'As Of dd/mmm/yyyy' based on the oldest information used in the data displayed >
                                lblAsOfValue.Text = If(dtDssData.Rows(0)("AsOfDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("AsOfDate").ToString))
                                '</AvinashG. on 29-May-2015:     FA-885 EQC Share info: show the 'As Of dd/mmm/yyyy' based on the oldest information used in the data displayed >
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": Db_No_Data from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock.Text = strShare
                                Call clearTDSSData()
                            Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": DB_Unsuccessful from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock.Text = strShare
                                Call clearTDSSData()
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": Db_Error from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock.Text = strShare
                                Call clearTDSSData()
                        End Select
                    Else
                        lblStock.Text = strShare
                        Call clearTDSSData()
                    End If
                Case "N", "NO"
                    ''Do Nothing
            End Select
        Catch ex As Exception
            lblerror.Text = "setTDSSData:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setTDSSData", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    '</Rutuja S. on 12-Jan-2015: FA-775 Download and display TR DSS data on pricer page>
    ''' <summary>
    ''' Clears the third set of share report
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clearTDSSData()
        Try
            '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
            Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
            LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": In clearTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "clearTDSSData", ErrorLevel.None)
            '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>


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
            'lbl20DHistVolHi.Text = ""
            'lbl20DHistVolLo.Text = ""
            lbl60DHistVolCurr.Text = ""
            'lbl60DHistVolHi.Text = ""
            'lbl60DHistVolLo.Text = ""
            lbl250DHistVolCurr.Text = ""
            'lbl250DHistVolHi.Text = ""
            'lbl250DHistVolLo.Text = ""
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

    ''' <summary>
    ''' Gets Data and fills the second set of share report
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setTDSSData2(ByVal strShare As String)
        Dim dtDssData As DataTable
        Try
            '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
            Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
            LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": In setTDSSData2", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData2", ErrorLevel.None)
            '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>

            '<AvinashG. on 08-Sep-2015: Config controlled service call>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    dtDssData = New DataTable("Share_data")
                    If strShare.Trim <> "" Then
                        '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                        Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                        LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": EQC_ShowTRShareInfo found YES, calling webservice method Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData2", ErrorLevel.None)
                        '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>

                        Select Case objELNRFQ.Web_GetTDSSData(strShare, dtDssData)
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": Db_Successful from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData2", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock2.Text = dtDssData.Rows(0)("RICCode").ToString
                                lblStockDesc2.Text = dtDssData.Rows(0)("Description").ToString.ToUpper
                                lblSpotDate2.Text = If(dtDssData.Rows(0)("SpotLastUpdateDate").ToString.Trim = "", "", CDate(dtDssData.Rows(0)("SpotLastUpdateDate").ToString).ToString("dd-MMM-yy HH:mm"))
                                lblSpotValue2.Text = SetNumberFormat(dtDssData.Rows(0)("Spot").ToString, 2)
                                lbl52WkHighDate2.Text = If(dtDssData.Rows(0)("Last1YearHighDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("Last1YearHighDate").ToString))
                                lbl52WkHighValue2.Text = SetNumberFormat(dtDssData.Rows(0)("Last1YearHighValue").ToString, 2)
                                lbl52WkLowDate2.Text = If(dtDssData.Rows(0)("Last1YearLowDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("Last1YearLowDate").ToString))
                                lbl52WkLowValue2.Text = SetNumberFormat(dtDssData.Rows(0)("Last1YearLowValue").ToString, 2)
                                'lblYTDChngValue2.Text 		= SetNumberFormat(dtDssData.Rows(0)("YTDchngperc").ToString, 2) & "%"
                                'lblMTDChngValue2.Text 		= SetNumberFormat(dtDssData.Rows(0)("MTDchngperc").ToString, 2) & "%"
                                'lbl1YearChngValue2.Text 	= SetNumberFormat(dtDssData.Rows(0)("1yearchngperc").ToString, 2) & "%"
                                lblYTDChngValue2.Text = SetNumberFormat(dtDssData.Rows(0)("YTDchngperc").ToString, 2)
                                lblMTDChngValue2.Text = SetNumberFormat(dtDssData.Rows(0)("MTDchngperc").ToString, 2)
                                lbl1YearChngValue2.Text = SetNumberFormat(dtDssData.Rows(0)("1yearchngperc").ToString, 2)
                                lbl20DHistVolCurr2.Text = SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaCurr").ToString, 2)
                                'lbl20DHistVolHi2.Text 		= SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaHi").ToString, 2)
                                'lbl20DHistVolLo2.Text 		= SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaLo").ToString, 2)
                                lbl60DHistVolCurr2.Text = SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaCurr").ToString, 2)
                                'lbl60DHistVolHi2.Text 		= SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaHi").ToString, 2)
                                'lbl60DHistVolLo2.Text 		= SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaLo").ToString, 2)
                                lbl250DHistVolCurr2.Text = SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaCurr").ToString, 2)
                                'lbl250DHistVolHi2.Text 	= SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaHi").ToString, 2)
                                'lbl250DHistVolLo2.Text 	= SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaLo").ToString, 2)
                                lblTrailing12MPEValue2.Text = SetNumberFormat(dtDssData.Rows(0)("Trailing12MPE").ToString, 2)
                                lblTrailing12MPBValue2.Text = SetNumberFormat(dtDssData.Rows(0)("Trailing12MPB").ToString, 2)
                                lblMarketCapCcy2.Text = dtDssData.Rows(0)("MarketCapCcy").ToString
                                lblMarketCapValue2.Text = SetNumberFormat(CDbl(CDbl(Val(dtDssData.Rows(0)("MarketCapValue").ToString))) / (1000000), 2) & "M"
                                lblCashEquivCcy2.Text = dtDssData.Rows(0)("CashAndEquivCcy").ToString
                                lblCashEquivValue2.Text = SetNumberFormat(CDbl(CDbl(Val(dtDssData.Rows(0)("CashAndEquivValue").ToString))) / (1000000), 2) & "M"
                                lblTotalDebtCcy2.Text = dtDssData.Rows(0)("TotalDebtCcy").ToString
                                lblTotalDebtValue2.Text = SetNumberFormat(CDbl(CDbl(Val(dtDssData.Rows(0)("TotalDebtValue").ToString))) / (1000000), 2) & "M"
                                lbl12TDivValuefreq2.Text = If(dtDssData.Rows(0)("DividendYield12MonthFreq").ToString.Trim = "", "", dtDssData.Rows(0)("DividendYield12MonthFreq").ToString.Substring(0, 1))
                                lbl12TDivYieldValue2.Text = If(dtDssData.Rows(0)("DividendYield12MonthValue").ToString = "", "", SetNumberFormat(dtDssData.Rows(0)("DividendYield12MonthValue").ToString, 2) & "%")
                                ''lbl12TDivYieldValue2.Text = SetNumberFormat(dtDssData.Rows(0)("DividendYield12MonthValue").ToString, 2) & "%"   ''''<14May2015 By Dilkhush:-Commented as per Avinash For No yeild there should be no % sign/>
                                lblNextDivDateValue2.Text = If(dtDssData.Rows(0)("NextDividendDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("NextDividendDate").ToString))
                                lblPrevEarnFreq2.Text = If(dtDssData.Rows(0)("PreviousEarnFreq").ToString.Trim = "", "", dtDssData.Rows(0)("PreviousEarnFreq").ToString.Substring(0, 1))
                                lblPrevEarnDateValue2.Text = If(dtDssData.Rows(0)("PreviousEarnDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("PreviousEarnDate").ToString))
                                lblPrevEPSValue2.Text = SetNumberFormat(dtDssData.Rows(0)("PreviousEPS").ToString, 4)
                                lblNextEarnFreq2.Text = If(dtDssData.Rows(0)("NextEarnFreq").ToString.Trim = "", "", dtDssData.Rows(0)("NextEarnFreq").ToString.Substring(0, 1))
                                lblNextEarnDateValue2.Text = If(dtDssData.Rows(0)("NextEarnDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("NextEarnDate").ToString))
                                '<AvinashG. on 29-May-2015:     FA-885 EQC Share info: show the 'As Of dd/mmm/yyyy' based on the oldest information used in the data displayed >
                                lblAsOfValue2.Text = If(dtDssData.Rows(0)("AsOfDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("AsOfDate").ToString))
                                '</AvinashG. on 29-May-2015:     FA-885 EQC Share info: show the 'As Of dd/mmm/yyyy' based on the oldest information used in the data displayed >
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": Db_No_Data from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData2", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock2.Text = strShare
                                Call clearTDSSData2()
                            Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": DB_Unsuccessful from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData2", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock2.Text = strShare
                                Call clearTDSSData2()
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": Db_Error from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData2", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock2.Text = strShare
                                Call clearTDSSData2()
                        End Select
                    Else
                        lblStock2.Text = strShare
                        Call clearTDSSData2()
                    End If
                Case "N", "NO"
                    ''Do Nothing
            End Select

        Catch ex As Exception
            lblerror.Text = "setTDSSData2:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setTDSSData2", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Clears the second set of share report
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clearTDSSData2()
        Try
            '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
            Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
            LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": In clearTDSSData2", LogType.FnqDebug, Nothing, sSelfPath, "clearTDSSData2", ErrorLevel.None)
            '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>



            lblStockDesc2.Text = ""
            lblSpotDate2.Text = ""
            lblSpotValue2.Text = ""
            lbl52WkHighDate2.Text = ""
            lbl52WkHighValue2.Text = ""
            lbl52WkLowDate2.Text = ""
            lbl52WkLowValue2.Text = ""
            lblYTDChngValue2.Text = ""
            lblMTDChngValue2.Text = ""
            lbl1YearChngValue2.Text = ""
            lbl20DHistVolCurr2.Text = ""
            'lbl20DHistVolHi2.Text = ""
            'lbl20DHistVolLo2.Text = ""
            lbl60DHistVolCurr2.Text = ""
            'lbl60DHistVolHi2.Text = ""
            'lbl60DHistVolLo2.Text = ""
            lbl250DHistVolCurr2.Text = ""
            'lbl250DHistVolHi2.Text = ""
            'lbl250DHistVolLo2.Text = ""
            lblTrailing12MPEValue2.Text = ""
            lblTrailing12MPBValue2.Text = ""
            lblMarketCapCcy2.Text = ""
            lblMarketCapValue2.Text = ""
            lblCashEquivCcy2.Text = ""
            lblCashEquivValue2.Text = ""
            lblTotalDebtCcy2.Text = ""
            lblTotalDebtValue2.Text = ""
            lbl12TDivValuefreq2.Text = ""
            lbl12TDivYieldValue2.Text = ""
            lblNextDivDateValue2.Text = ""
            lblPrevEarnFreq2.Text = ""
            lblPrevEarnDateValue2.Text = ""
            lblPrevEPSValue2.Text = ""
            lblNextEarnFreq2.Text = ""
            lblNextEarnDateValue2.Text = ""
            lblPrevEarnDateValue2.Text = ""
            lblPrevEPSValue2.Text = ""
            lblNextEarnFreq2.Text = ""
            lblNextEarnDateValue2.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Gets Data and fills the third set of share report
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setTDSSData3(ByVal strShare As String)
        Dim dtDssData As DataTable
        Try
            '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
            Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
            LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": In setTDSSData3", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData3", ErrorLevel.None)
            '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>

            '<AvinashG. on 08-Sep-2015: Config controlled service call>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    dtDssData = New DataTable("Share_data")
                    If (strShare.Trim <> "") Then
                        '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                        Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                        LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": EQC_ShowTRShareInfo found YES, calling webservice method Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData3", ErrorLevel.None)
                        '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                        Select Case objELNRFQ.Web_GetTDSSData(strShare, dtDssData)
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": Db_Successful from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData3", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock3.Text = dtDssData.Rows(0)("RICCode").ToString
                                lblStockDesc3.Text = dtDssData.Rows(0)("Description").ToString.ToUpper
                                lblSpotDate3.Text = If(dtDssData.Rows(0)("SpotLastUpdateDate").ToString.Trim = "", "", CDate(dtDssData.Rows(0)("SpotLastUpdateDate").ToString).ToString("dd-MMM-yy HH:mm"))
                                lblSpotValue3.Text = SetNumberFormat(dtDssData.Rows(0)("Spot").ToString, 2)
                                lbl52WkHighDate3.Text = If(dtDssData.Rows(0)("Last1YearHighDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("Last1YearHighDate").ToString))
                                lbl52WkHighValue3.Text = SetNumberFormat(dtDssData.Rows(0)("Last1YearHighValue").ToString, 2)
                                lbl52WkLowDate3.Text = If(dtDssData.Rows(0)("Last1YearLowDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("Last1YearLowDate").ToString))
                                lbl52WkLowValue3.Text = SetNumberFormat(dtDssData.Rows(0)("Last1YearLowValue").ToString, 2)
                                'lblYTDChngValue3.Text 		= SetNumberFormat(dtDssData.Rows(0)("YTDchngperc").ToString, 2) & "%"
                                'lblMTDChngValue3.Text 		= SetNumberFormat(dtDssData.Rows(0)("MTDchngperc").ToString, 2) & "%"
                                'lbl1YearChngValue3.Text 	= SetNumberFormat(dtDssData.Rows(0)("1yearchngperc").ToString, 2) & "%"
                                lblYTDChngValue3.Text = SetNumberFormat(dtDssData.Rows(0)("YTDchngperc").ToString, 2)
                                lblMTDChngValue3.Text = SetNumberFormat(dtDssData.Rows(0)("MTDchngperc").ToString, 2)
                                lbl1YearChngValue3.Text = SetNumberFormat(dtDssData.Rows(0)("1yearchngperc").ToString, 2)
                                lbl20DHistVolCurr3.Text = SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaCurr").ToString, 2)
                                'lbl20DHistVolHi3.Text 		= SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaHi").ToString, 2)
                                'lbl20DHistVolLo3.Text 		= SetNumberFormat(dtDssData.Rows(0)("Last20DHistVolaLo").ToString, 2)
                                lbl60DHistVolCurr3.Text = SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaCurr").ToString, 2)
                                'lbl60DHistVolHi3.Text 		= SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaHi").ToString, 2)
                                'lbl60DHistVolLo3.Text 		= SetNumberFormat(dtDssData.Rows(0)("Last60DHistVolaLo").ToString, 2)
                                lbl250DHistVolCurr3.Text = SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaCurr").ToString, 2)
                                'lbl250DHistVolHi3.Text 	= SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaHi").ToString, 2)
                                'lbl250DHistVolLo3.Text 	= SetNumberFormat(dtDssData.Rows(0)("Last250DHistVolaLo").ToString, 2)
                                lblTrailing12MPEValue3.Text = SetNumberFormat(dtDssData.Rows(0)("Trailing12MPE").ToString, 2)
                                lblTrailing12MPBValue3.Text = SetNumberFormat(dtDssData.Rows(0)("Trailing12MPB").ToString, 2)
                                lblMarketCapCcy3.Text = dtDssData.Rows(0)("MarketCapCcy").ToString
                                lblMarketCapValue3.Text = SetNumberFormat(CDbl(CDbl(Val(dtDssData.Rows(0)("MarketCapValue").ToString))) / (1000000), 2) & "M"
                                lblCashEquivCcy3.Text = dtDssData.Rows(0)("CashAndEquivCcy").ToString
                                lblCashEquivValue3.Text = SetNumberFormat(CDbl(CDbl(Val(dtDssData.Rows(0)("CashAndEquivValue").ToString))) / (1000000), 2) & "M"
                                lblTotalDebtCcy3.Text = dtDssData.Rows(0)("TotalDebtCcy").ToString
                                lblTotalDebtValue3.Text = SetNumberFormat(CDbl(CDbl(Val(dtDssData.Rows(0)("TotalDebtValue").ToString))) / (1000000), 2) & "M"
                                lbl12TDivValuefreq3.Text = If(dtDssData.Rows(0)("DividendYield12MonthFreq").ToString.Trim = "", "", dtDssData.Rows(0)("DividendYield12MonthFreq").ToString.Substring(0, 1))
                                lbl12TDivYieldValue3.Text = If(dtDssData.Rows(0)("DividendYield12MonthValue").ToString = "", "", SetNumberFormat(dtDssData.Rows(0)("DividendYield12MonthValue").ToString, 2) & "%")
                                ''lbl12TDivYieldValue3.Text = SetNumberFormat(dtDssData.Rows(0)("DividendYield12MonthValue").ToString, 2) & "%" ''''<14May2015 By Dilkhush:-Commented as per Avinash For No yeild there should be no % sign/>
                                lblNextDivDateValue3.Text = If(dtDssData.Rows(0)("NextDividendDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("NextDividendDate").ToString))
                                lblPrevEarnFreq3.Text = If(dtDssData.Rows(0)("PreviousEarnFreq").ToString.Trim = "", "", dtDssData.Rows(0)("PreviousEarnFreq").ToString.Substring(0, 1))
                                lblPrevEarnDateValue3.Text = If(dtDssData.Rows(0)("PreviousEarnDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("PreviousEarnDate").ToString))
                                lblPrevEPSValue3.Text = SetNumberFormat(dtDssData.Rows(0)("PreviousEPS").ToString, 4)
                                lblNextEarnFreq3.Text = If(dtDssData.Rows(0)("NextEarnFreq").ToString.Trim = "", "", dtDssData.Rows(0)("NextEarnFreq").ToString.Substring(0, 1))
                                lblNextEarnDateValue3.Text = If(dtDssData.Rows(0)("NextEarnDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("NextEarnDate").ToString))
                                '<AvinashG. on 29-May-2015:     FA-885 EQC Share info: show the 'As Of dd/mmm/yyyy' based on the oldest information used in the data displayed >
                                lblAsOfValue3.Text = If(dtDssData.Rows(0)("AsOfDate").ToString.Trim = "", "", FinIQApp_Date.FinIQDate(dtDssData.Rows(0)("AsOfDate").ToString))
                                '</AvinashG. on 29-May-2015:     FA-885 EQC Share info: show the 'As Of dd/mmm/yyyy' based on the oldest information used in the data displayed >
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": Db_No_Data from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData3", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock3.Text = strShare
                                Call clearTDSSData3()
                            Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": DB_Unsuccessful from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData3", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock3.Text = strShare
                                Call clearTDSSData3()
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                                '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                                LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": Db_Error from Web_GetTDSSData", LogType.FnqDebug, Nothing, sSelfPath, "setTDSSData3", ErrorLevel.None)
                                '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
                                lblStock3.Text = strShare
                                Call clearTDSSData3()
                        End Select
                    Else
                        lblStock3.Text = strShare
                        Call clearTDSSData3()
                    End If
                Case "N", "NO"
                    ''Do Nothing
            End Select


        Catch ex As Exception
            lblerror.Text = "setTDSSData3:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "setTDSSData3", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Clears the third set of share report
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clearTDSSData3()
        Try
            '<AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>
            Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
            LogException(LoginInfoGV.Login_Info.LoginId, Session("Debug_Counter").ToString.PadLeft(5, CChar("0")) + ": In clearTDSSData3", LogType.FnqDebug, Nothing, sSelfPath, "clearTDSSData3", ErrorLevel.None)
            '</AvinashG. on 20-Oct-2015: FA-1157 - Add Debug Info to web application code>


            lblStockDesc3.Text = ""
            lblSpotDate3.Text = ""
            lblSpotValue3.Text = ""
            lbl52WkHighDate3.Text = ""
            lbl52WkHighValue3.Text = ""
            lbl52WkLowDate3.Text = ""
            lbl52WkLowValue3.Text = ""
            lblYTDChngValue3.Text = ""
            lblMTDChngValue3.Text = ""
            lbl1YearChngValue3.Text = ""
            lbl20DHistVolCurr3.Text = ""
            'lbl20DHistVolHi3.Text = ""
            'lbl20DHistVolLo3.Text = ""
            lbl60DHistVolCurr3.Text = ""
            'lbl60DHistVolHi3.Text = ""
            'lbl60DHistVolLo3.Text = ""
            lbl250DHistVolCurr3.Text = ""
            'lbl250DHistVolHi3.Text = ""
            'lbl250DHistVolLo3.Text = ""
            lblTrailing12MPEValue3.Text = ""
            lblTrailing12MPBValue3.Text = ""
            lblMarketCapCcy3.Text = ""
            lblMarketCapValue3.Text = ""
            lblCashEquivCcy3.Text = ""
            lblCashEquivValue3.Text = ""
            lblTotalDebtCcy3.Text = ""
            lblTotalDebtValue3.Text = ""
            lbl12TDivValuefreq3.Text = ""
            lbl12TDivYieldValue3.Text = ""
            lblNextDivDateValue3.Text = ""
            lblPrevEarnFreq3.Text = ""
            lblPrevEarnDateValue3.Text = ""
            lblPrevEPSValue3.Text = ""
            lblNextEarnFreq3.Text = ""
            lblNextEarnDateValue3.Text = ""
            lblPrevEarnDateValue3.Text = ""
            lblPrevEPSValue3.Text = ""
            lblNextEarnFreq3.Text = ""
            lblNextEarnDateValue3.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub manageShareReportShowHide()
        Try
            clearTDSSData()
            clearTDSSData2()
            clearTDSSData3()

            If ddlShareEQO.SelectedValue <> "" Then
                setTDSSData(ddlShareEQO.SelectedValue)
            End If

            Prepare_EQO_Basket()
            If ((objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper = "YES" Or _
                   objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper = "Y") And _
                   (objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "YES" Or _
                   objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper = "Y")) Then
                rblShareData.Visible = True
            Else
                rblShareData.Visible = False
            End If

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
            dr1 = dtLoginPP.Select("PP_CODE ='BNPP'")
            If dr1.Length > 0 Then
                showIssuerControls("BNPP")
            Else
                hideIssuerControls("BNPP")
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

            Select Case tabContainer.ActiveTabIndex
                Case prdTab.EQO
                    SchemeName = "EQO" ''Scheme name to decide
            End Select

            Select Case objELNRFQ.Db_Get_Avail_Login_For_PP(strLoginName, SchemeName, dtLoginPP)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtLoginPP.Rows.Count > 0 Then

                        '<Rutuja S. on 27-Feb-2015:Added to check available providers for EQO tab:need to discuss scheme name >
                        If tabContainer.ActiveTabIndex = prdTab.EQO Then
                            dr = dtLoginPP.Select("Product_Code='EQO'")
                            If dr.Length > 0 Then
                                chk_PriceProviderStatus(dtLoginPP)
                            End If
                        End If
                        '</Rutuja S. on 27-Feb-2015:Added to check available providers for EQO tab:need to discuss scheme name >

                    End If  ''dtLoginPP.rows.count loop end

                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    hideIssuerControls("BNPP")

            End Select


            Select Case tabContainer.ActiveTabIndex
                Case prdTab.EQO
                    If dtLoginPP.Rows.Count > 1 Then
                        btnSolveAll.Visible = True
                        lblRangeCcy.Visible = True
                    Else
                        btnSolveAll.Visible = False
                        lblRangeCcy.Visible = False
                    End If
                    '</Rutuja S. on 27-Feb-2015:Added to check available providers for EQO tab>
            End Select
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
            dr1 = dtGetLoginPP.Select("PP_CODE ='BNPP' ")
            If dr1.Length > 0 Then
                dr = dtChkServer.Select("Link_Provider_Name ='" & "BNPP" & "'  ")
                If dr.Length > 0 Then
                    If dr(0).Item("Link_Provider_Status").ToString = "UP" Then
                        blnLPIs_Up = True
                        btnBNPPPrice.Enabled = True
                        lblBNPPPrice.Visible = True
                        chkBNPP.Enabled = True ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    Else
                         ''<Nikhil M. on 16-Oct-2016: For Endable /disable | DK | Rohit>
                    End If
                Else
                    '<AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                    btnBNPPPrice.Enabled = False
                    btnBNPPPrice.CssClass = "btnDisabled"
                    lblBNPPPrice.Visible = False
                    chkBNPP.Enabled = False
                    '</AvinashG. on 20-Oct-2016:EQSCB-77 - Pricer button remains enabled if no record is present in Link_Provider_Status >
                End If ''end of dr
            Else
                ''Condition already handled in chk_PriceProviderStatus()
            End If  ''end of dr1
            ''--
            ''--

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
                    If tabContainer.ActiveTabIndex = prdTab.EQO Then
                        dr2 = dtGetLoginPP.Select("Product_Code='EQO'")
                        If dr2.Length > 0 Then
                            chk_LinkUPDownStatus(dtGetLoginPP, dtChkServer)
                        End If
                    End If
                    '</Rutuja S. on 03-Mar-2015: Added for EQO tab>
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

    ''---Added on 13Nov
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
    ''for Order RM Save
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
                        '<Mohit  to bind login name as text :JIRA-ID:FA-1158>
                        '.DataTextField = "Rel_Manager_Name"
                        .DataTextField = "Host"
                        '</Mohit to bind login name as text :JIRA-ID:FA-1158>
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
    '<AvinashG. on 17-Dec-2014: FA-768 	Move RM dropdown from pricer page to order popup ,for RFQ RM Save>
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
                        '<Mohit  to bind login name as text :JIRA-ID:FA-1158>
                        '.DataTextField = "Rel_Manager_Name"
                        .DataTextField = "Host"
                        '</Mohit to bind login name as text :JIRA-ID:FA-1158>
                        .DataValueField = "Rel_Manager_Name"
                        .DataBind()
                        .Items.Insert(0, "")
                        '<Avinash G/Sarun s/Rutuja S. on 19-Sep-2014:Added to set default login RM Jira id:FA-602 >
                        If dtRMList.Rows.Count = 1 Then
                            .SelectedIndex = 1
                        Else
                            .SelectedIndex = 0
                        End If
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
    '</AvinashG. on 17-Dec-2014: FA-768 	Move RM dropdown from pricer page to order popup >
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

    '<Rutuja S. on 02-Jan-2015: for Order Use selected RM's Email id,Changed method fill_Branch_Email to fill_Email>
    'Public Sub fill_Branch_Email()
    Public Sub fill_Email()
        Dim dtBranchEmail As DataTable
        Try
            dtBranchEmail = New DataTable("Email-Branch")
            ''fill Emailid and branch
            Select Case objELNRFQ.get_EmailId_Branch(ddlRM.SelectedValue, dtBranchEmail)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    'lblbranch.Text = dtBranchEmail.Rows(0).Item("BookName").ToString
                    lblEmail.Text = dtBranchEmail.Rows(0).Item("EmailId").ToString

                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    ' lblbranch.Text = ""
                    lblEmail.Text = ""
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    ' lblbranch.Text = ""
                    lblEmail.Text = ""
            End Select
        Catch ex As Exception
            lblerror.Text = "fill_Email:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_Email", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    '</Rutuja S. on 02-Jan-2015: for Order Use selected RM's Email id,Changed method fill_Branch_Email to fill_Email>

    '<Rutuja S. on 02-Jan-2015: for Order Use selected RM's Email id,Changed method fill_Branch_Email to fill_Branch>
    Public Sub fill_Branch()
        Dim dtBranchEmail As DataTable
        Try
            dtBranchEmail = New DataTable("Email-Branch")
            ''fill Emailid and branch
            Select Case objELNRFQ.get_EmailId_Branch(ddlRFQRM.SelectedValue, dtBranchEmail)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    lblbranch.Text = dtBranchEmail.Rows(0).Item("BookName").ToString
                    'lblEmail.Text = dtBranchEmail.Rows(0).Item("EmailId").ToString

                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    lblbranch.Text = ""
                    ' lblEmail.Text = ""
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    lblbranch.Text = ""
                    'lblEmail.Text = ""
            End Select
        Catch ex As Exception
            lblerror.Text = "fill_Branch:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_Branch", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    '</Rutuja S. on 02-Jan-2015: for Order Use selected RM's Email id,Changed method fill_Branch_Email to fill_Branch>
    ''---
    Private Sub SetTemplateDetails(ByVal SchemeName As String)
        Dim dtTemplate As DataTable = Nothing
        Dim dtexchange As DataTable = Nothing
        Dim TemplateId As String = ""
        strDefaultExchange = ""

        Try
            schemeCode = tabContainer.ActiveTabIndex.ToString
            dtTemplate = New DataTable("Template Code")
            Dim strSchemeName As String = String.Empty
            Select Case tabContainer.ActiveTabIndex
                Case prdTab.EQO
                    ' SchemeName = "OTCOPTION"  ''To decide
                    strSchemeName = "EQO"  '<RiddhiS. on 28-Oct-2016: As Template code is EQO>
            End Select


            Session.Add("Scheme_EQO", strSchemeName)
            Select Case objELNRFQ.DB_Get_TemplateDetails(strSchemeName, dtTemplate)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful

                    If dtTemplate.Rows.Count > 0 Then
                        strDefaultExchange = dtTemplate.Rows(0).Item("ST_Exchange_Name").ToString
                        TemplateId = dtTemplate.Rows(0).Item("ST_Template_ID").ToString
                        Session.Add("Template_Code_EQO", TemplateId)
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

    Public Sub fill_EQO_Grid()
        Dim dtEQO As DataTable
        Try
            EnableTimerTick()
            dtAccum = New DataTable("DUMMY")
            '<MohitL. on 22-Apr-2016: FA-1163 Display of 'Self' , 'Group' and 'All' drop down on order blotter on main pricer page >
            Dim strMode As String = If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "SELF", "SELF", If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "GROUP", "GRP", "ALL"))
            '</MohitL. on 22-Apr-2016: FA-1163 Display of 'Self' , 'Group' and 'All' drop down on order blotter on main pricer page >

            Select Case objELNRFQ.web_fill_EQO_Grid("EQO", LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, dtEQO)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    grdEQORFQ.CurrentPageIndex = 0

                    'For Each row As DataRow In dtEQO.Rows
                    '    If row.Item("ER_BarrierPercentage").ToString <> "&nbsp;" And row.Item("ER_BarrierPercentage").ToString <> "" Then
                    '        'row.Item("ER_BarrierPercentage") = CDbl(row.Item("ER_BarrierPercentage")) / 100

                    '    End If

                    'Next row


                    grdEQORFQ.DataSource = dtEQO
                    grdEQORFQ.DataBind()

                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    grdEQORFQ.CurrentPageIndex = 0
                    dtEQO = dtEQO.Clone
                    grdEQORFQ.DataSource = dtEQO
                    grdEQORFQ.DataBind()
            End Select
            Session.Add("EQOGrid", dtEQO)

            'Select Case objReadConfig.ReadConfig(dsConfig, "EQO_DisableQuanto", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
            '    Case "Y", "YES"
            '        grdEQORFQ.Columns(grdEQORFQEnum.Settl_Ccy).Visible = False
            '    Case "N", "NO"
            '        grdEQORFQ.Columns(grdEQORFQEnum.Settl_Ccy).Visible = True
            'End Select

        Catch ex As Exception
            lblerror.Text = "fill_EQO_Grid:Error occurred in filling grid."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_EQO_Grid", ErrorLevel.High)
            Throw ex
        End Try
    End Sub


    Public Sub fill_OrderGrid()
        Dim dt As DataTable
        Try
            EnableTimerTick()
            ''Dim strSchemeName As String = Convert.ToString(Session("Scheme"))  ''commented on 3July 
            Dim strSchemeName As String = "" ''Convert.ToString(Session("Scheme"))
            ''<Added on 3July to get current tabindex>

            '<MohitL. on 26-Oct-2015: FA-1163 Display of 'Self' , 'Group' and 'All' drop down on order blotter on main pricer page >
            Dim strMode As String = If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "SELF", "SELF", If(ddlSelfGrp.SelectedItem.Text.Trim.ToUpper = "GROUP", "GRP", "ALL"))
            '</MohitL. on 26-Oct-2015: FA-1163 Display of 'Self' , 'Group' and 'All' drop down on order blotter on main pricer page >

            Select Case tabContainer.ActiveTabIndex

                Case prdTab.EQO
                    strSchemeName = "OTCOPTION" ''To decide
            End Select
            ''</Added on 3July to get current tabindex>
            dt = New DataTable("DUMMY")
            '<AvinashG. on 26-Oct-2015: FA-1163, adding SELF Backward compatibility>
            Select Case objELNRFQ.GetEQOptionsOrderHistory("ELN", strSchemeName, LoginInfoGV.Login_Info.LoginId, txttrade.Value, txtTotalRows.Text, strMode, dt)
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

            'Select Case objReadConfig.ReadConfig(dsConfig, "EQO_DisableQuanto", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
            '    Case "Y", "YES"
            '        grdOrder.Columns(grdOrderEnum.Quanto_Currency).Visible = False
            '    Case "N", "NO"
            '        grdOrder.Columns(grdOrderEnum.Quanto_Currency).Visible = True
            'End Select
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
    Public Sub fill_All_Exchange()
        Dim dtExchange As DataTable
        Try
            dtExchange = New DataTable("Exchange")
            Select Case objELNRFQ.DB_Fill_Exchange_Combo(dtExchange)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlExchangeEQO
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
                    With ddlExchangeEQO
                        .DataSource = dtExchange
                        .DataBind()
                    End With
                    With ddlExchangeEQO2
                        .DataSource = dtExchange
                        .DataBind()
                    End With
                    With ddlExchangeEQO3
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
    ''REPLACED BY fill_All_Exchange
    'Public Sub fillEQOExchanges()
    '    Dim dtEQOExchanges As DataTable
    '    Try
    '        dtEQOExchanges = New DataTable("EQO_Exchange")
    '        Select Case objELNRFQ.web_Fill_EQO_Exchange(ddlProductEQO.SelectedItem.Text, dtEQOExchanges)
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
    '                With ddlExchangeEQO
    '                    .DataSource = dtEQOExchanges
    '                    .DataTextField = "ExchangeLongName"
    '                    .DataValueField = "ExchangeCode"
    '                    .DataBind()
    '                End With
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
    '                lblerror.Text = "No data found."
    '                ddlExchangeEQO.DataSource = dtEQOExchanges
    '                ddlExchangeEQO.DataBind()
    '            Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful

    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_Error

    '        End Select
    '        Session.Add("EQOExchange", dtEQOExchanges)
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub FillDRAddl_exchange(ByVal ddl As DropDownList)
        Dim dtExchange As DataTable
        Try
            dtExchange = New DataTable("Exchange")
            Select Case objELNRFQ.DB_Fill_Exchange_Combo(dtExchange)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddl
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
                    ddl.DataSource = dtExchange
                    ddl.DataBind()
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            End Select
        Catch ex As Exception
            lblerror.Text = "FillDRAddl_exchange:Error occurred in filling exchange."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "FillDRAddl_exchange", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    ''REPLACED BY FillDRAddl_exchange
    'Public Sub fillEQOShare()
    '    Dim stroptiontype As String = ""

    '    Try
    '        dtShareEQO = New DataTable("DUMMY")

    '        If ddlOptionType.SelectedValue.ToUpper.Contains("KNOCKIN PUT") Then
    '            stroptiontype = "WOKIP" ''"Barrier"
    '        Else
    '            stroptiontype = "VANI"
    '        End If
    '        'stroptiontype = "VANI,WOKIP"

    '        Select Case objELNRFQ.web_Get_EQO_Share(ddlExchangeEQO.SelectedValue, ddlProductEQO.SelectedItem.Text, stroptiontype, dtShareEQO)
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
    '                With ddlShareEQO
    '                    .DataSource = dtShareEQO
    '                    .DataTextField = "UnderlyingSecurityDesc"
    '                    ''.DataValueField = "AltCode"  ''-- Changed to Short Description instead of code
    '                    .DataValueField = "AltCode"
    '                    .DataBind()

    '                End With

    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
    '                With ddlShareEQO
    '                    .DataSource = dtShareEQO
    '                    .DataBind()
    '                End With
    '        End Select
    '        Session.Add("Share_ValueEQO1", dtShareEQO)
    '        '  GetCommentary()
    '    Catch ex As Exception
    '        lblerror.Text = "fillEQOShare:Error occurred in filling Share."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "fillEQOShare", ErrorLevel.High)
    '        Throw ex
    '    End Try
    'End Sub


#End Region


#Region "Fill Chart"



    Public Sub Get_RFQ_orderperformanceChart()

        Dim dtOrderP As DataTable
        Chart1.Visible = True
        Chart2.Visible = True
        Dim ProductType As String
        Dim ActiveTabIndex As Integer = tabContainer.ActiveTabIndex
        If (ActiveTabIndex = 0) Then
            ProductType = "ELN"
        ElseIf (ActiveTabIndex = 1) Then
            ProductType = "DRA/FCN"
        ElseIf (ActiveTabIndex = 2) Then
            ProductType = "Accum/Decum"
        Else
            ProductType = "ELN"
        End If
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
        Dim ActiveTabIndex As Integer = tabContainer.ActiveTabIndex
        If (ActiveTabIndex = 0) Then
            ProductType = "OTCOPTION"
        ElseIf (ActiveTabIndex = 1) Then
            ProductType = "DRA/FCN"
        ElseIf (ActiveTabIndex = 2) Then
            ProductType = "Accum/Decum"
        Else
            ProductType = "OTCOPTION" ''Type OTCOPTION added by Rushikesh D. on 24-July-15 for graph.
        End If
        Try
            dtOrderPrice = New DataTable("Order To PriceRatio")

            Select Case objELNRFQ.DB_Get_ColumnChart_Details(ProductType, LoginInfoGV.Login_Info.LoginId, dtOrderPrice)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtOrderPrice.Rows.Count > 0 Then
                        DrawColumnChart(dtOrderPrice, "PP_CODE", "OrderToPriceRatio", Chart2, "Default", False, "", SeriesChartType.Bar)
                        Chart2.Visible = True
                        'tabChart1.Visible = True
                    Else
                        Chart2.Visible = False
                        'tabChart1.Visible = False
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    Chart2.Visible = False
                    'tabChart1.Visible = False

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
        Dim ActiveTabIndex As Integer = tabContainer.ActiveTabIndex
        If (ActiveTabIndex = 0) Then
            strProductType = "OTCOPTION"
        Else
            strProductType = "OTCOPTION" ''OTCOPTION Added by Rushikesh D. on 24-July-15 for pi chart.
        End If
        Try
            dtMaxCount = New DataTable("Max Count")

            Select Case objELNRFQ.DB_Get_RFQ_Details_Chart(strProductType, LoginInfoGV.Login_Info.LoginId, dtMaxCount)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtMaxCount.Rows.Count > 0 Then
                        DrawPieChart(dtMaxCount, "PP_CODE", "MaxCount", "Default", False, "", Chart1)
                        Chart1.Visible = True
                        ''tabChart1.Visible = True
                    Else
                        Chart1.Visible = False
                        ''tabChart1.Visible = False
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    Chart1.Visible = False
                    ''tabChart1.Visible = False

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

            ''Set Chart
            Chart.BackColor = colorBackGround

            Chart.BackGradientStyle = _ChartBackGradientStyle
            Chart.BorderlineDashStyle = _ChartBorderlineDashStyle
            Chart.BorderSkin.SkinStyle = _ChartBorderSkinStyle

            Chart.ChartAreas("Default").BackColor = colorBackGround
            Chart.ChartAreas("Default").BackGradientStyle = GradientStyle.None
            ' setInnerPlotPosition(_ChartPositionProperties)
            Chart.ChartAreas("Default").InnerPlotPosition.X = 12
            Chart.ChartAreas("Default").InnerPlotPosition.Y = 10
            ''---
            Chart.ChartAreas("Default").AxisX.TitleFont = New Font("arial", 1.0F, FontStyle.Regular)
            Chart.ChartAreas("Default").AxisY.TitleFont = New Font("arial", 1.0F, FontStyle.Regular)
            ''--
            Chart.ChartAreas("Default").InnerPlotPosition.Height = 80
            Chart.ChartAreas("Default").InnerPlotPosition.Width = 80 ''55
            Chart.ChartAreas("Default").InnerPlotPosition.Auto = False

            Chart.Legends("Default").Enabled = True
            Chart.Legends(0).Docking = _DockingLocation

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
                    Case "BNPP"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.BNPP
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

            Dim MSChartType As System.Web.UI.DataVisualization.Charting.SeriesChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Doughnut

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

            Chart.Series(0).ChartType = MSChartType
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
                    Case "BNPP"
                        Chart.Series(0).Points(i).Color = Me.structChartColors.BNPP
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
            If tabContainer.ActiveTabIndex = prdTab.EQO Then
                If ddlSolveforEQO.SelectedValue.ToUpper.Trim = "BARRIER" Then
                    txtBarrierLevelEQO.Text = ""
                    txtBarrierLevelEQO.Enabled = False
                    ddlBarrierEQO.Enabled = False
                    ddlBarrierEQO.BackColor = Color.FromArgb(242, 242, 243)
                    txtPremium.Enabled = True
                    txtStrikeEQO.Enabled = True
                ElseIf ddlSolveforEQO.SelectedValue.ToUpper.Trim = "PREMIUM" Then
                    txtPremium.Text = ""
                    txtPremium.Enabled = False
                    txtBarrierLevelEQO.Enabled = True
                    txtStrikeEQO.Enabled = True

                    'If chkAddShareEQO2.Checked Then
                    '    ddlBarrierEQO.Enabled = False
                    '    ddlBarrierEQO.BackColor = Color.FromArgb(242, 242, 243)
                    '    ddlBarrierEQO.SelectedIndex = 0
                    'Else
                    '    ddlBarrierEQO.Enabled = True
                    '    ddlBarrierEQO.BackColor = Color.White
                    'End If
                    '<Comment this code in Case for multiple Shares>
                    ddlBarrierEQO.Enabled = True
                    ddlBarrierEQO.BackColor = Color.White
                    '</Comment this code in Case for multiple Shares>
                Else
                    ''Strike
                    txtStrikeEQO.Text = ""
                    txtStrikeEQO.Enabled = False
                    txtPremium.Enabled = True
                    txtBarrierLevelEQO.Enabled = True
                    'If chkAddShareEQO2.Checked Then
                    '    ddlBarrierEQO.Enabled = False
                    '    ddlBarrierEQO.BackColor = Color.FromArgb(242, 242, 243)
                    'Else
                    '    ddlBarrierEQO.Enabled = True
                    '    ddlBarrierEQO.BackColor = Color.White
                    'End If
                    '<Comment this code in Case for multiple Shares>
                    ddlBarrierEQO.Enabled = True
                    ddlBarrierEQO.BackColor = Color.White
                    '</Comment this code in Case for multiple Shares>
                End If
                upnl4.Update()
            End If

        Catch ex As Exception
            lblerror.Text = "clearFields:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "clearFields", ErrorLevel.High)
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
                    If tabContainer.ActiveTabIndex = 0 Then
                    ElseIf tabContainer.ActiveTabIndex = 2 Then
                    End If
                    sCcy = sShareCcy
                Else
                    dtBaseCCY = New DataTable("Dummy")

                    Select Case objELNRFQ.DB_GetBASECCY(Share, dtBaseCCY)
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
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
                    If tabContainer.ActiveTabIndex = 0 Then
                    ElseIf tabContainer.ActiveTabIndex = 2 Then
                    End If
                    sPRR = sSharePRR
                Else
                    dt = New DataTable("Dummy")
                    Select Case objELNRFQ.DB_UnderlyingRiskRatingShare(Share, dt)
                        Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                            sPRR = dt.Rows(0)(0).ToString
                            ''If sPRR = "NA" Then
                            ''    sPRR = Color.Red
                            ''Else
                            ''    sPRR = Color.Green
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
    '' REPLACED by getCurrency(ByVal Share As String, Optional ByRef sCcy As String = "")
    'Public Sub getCurrency(ByVal Share As String)
    '    Try
    '        Dim dtBaseCCY As DataTable
    '        dtBaseCCY = New DataTable("Dummy")

    '        Select Case objELNRFQ.DB_GetBASECCY(Share, dtBaseCCY)
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
    '                If tabContainer.ActiveTabIndex = 0 Then
    '                    lblEQOBaseCcy.Text = dtBaseCCY.Rows(0)(0).ToString
    '                    '</Rutuja S. on 16-Feb-2015: Added to fill EQO exchange>
    '                End If

    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

    '            Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
    '        End Select
    '    Catch ex As Exception
    '        lblerror.Text = "getCurrency:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "getCurrency", ErrorLevel.High)
    '        Throw ex
    '    End Try
    'End Sub

    '<Ajay P. on 24-Apr-2015>
    Public Function getBaseCurrency(ByVal Share As String) As String
        Try
            Dim dtBaseCCY As DataTable

            dtBaseCCY = New DataTable("Dummy")

            Select Case objELNRFQ.web_GetBASEcurrencyforEQO(Share, dtBaseCCY)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    Return dtBaseCCY.Rows(0)(0).ToString
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    Return ""
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    Return ""
            End Select
        Catch ex As Exception
            lblerror.Text = "getCurrency:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "getCurrency", ErrorLevel.High)
            Throw ex
        End Try
    End Function
    '</Ajay P. on 24-Apr-2015>


    Function chkMultiple(ByVal Number As Integer) As Boolean
        Try
            If Number Mod 3 = 0 Then
                chkMultiple = True
            Else
                lblerror.Text = "Guarantee should be multiple of 3,6.."
                chkMultiple = False
            End If
        Catch ex As Exception
            '' lblerror.Text = "chkMultiple:Error while converting to months"
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



    Private Sub tabContainer_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabContainer.ActiveTabChanged
        Try
            Select Case tabContainer.ActiveTabIndex
                Case prdTab.ELN
                    Response.Redirect("../ELN_DealEntry1/ELN_RFQL1.aspx?menustr=EQ%20Sales%20-%20EQ%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=", False)
                Case prdTab.DRA
                    Response.Redirect("../ELN_DealEntry1/DRAFCNRFQ1.aspx?menustr=EQ%20Sales%20-%20DRA%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=&PrdToLoad=DRA", False)
                Case prdTab.FCN
                    Response.Redirect("../ELN_DealEntry1/DRAFCNRFQ1.aspx?menustr=EQ%20Sales%20-%20FCN%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=&PrdToLoad=FCN", False)
                Case prdTab.Acc
                    Response.Redirect("../ELN_DealEntry1/AccDecRFQ1.aspx?menustr=EQ%20Sales%20-%20Acc%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=&PrdToLoad=ACCUMULATOR", False)
                Case prdTab.Dec
                    Response.Redirect("../ELN_DealEntry1/AccDecRFQ1.aspx?menustr=EQ%20Sales%20-%20Dec%20RFQ%20And%20Order%20Entry&Mode=" + UCase(Request.QueryString("Mode")) + "&token=&PrdToLoad=DECUMULATOR", False) '<AvinashG. on 24-Jan-2016: >
            End Select
        Catch ex As Exception
            lblerror.Text = "tabContainer_ActiveTabChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "tabContainer_ActiveTabChanged", ErrorLevel.High)
        End Try
    End Sub

    Protected Sub rbHistory_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbHistory.SelectedIndexChanged
        Try
            RestoreSolveAll()
            RestoreAll() 'Added by Imran to regain  client state in server
            If rbHistory.SelectedValue.Trim = "Quote History" Then
                Select Case tabContainer.ActiveTabIndex
                    Case prdTab.EQO
                        '<Rutuja S. on 03-Mar-2015: Added for filling grid of EQO tab>
                        makeThisGridVisible(grdEQORFQ)
                        fill_EQO_Grid()
                        '</Rutuja S. on 03-Mar-2015: Added for filling grid of EQO tab>
                End Select
            ElseIf rbHistory.SelectedValue.Trim = "Order History" Then
                makeThisGridVisible(grdOrder)
                fill_OrderGrid()
                'ColumnVisibility()
            End If
            upnlGrid.Update()
        Catch ex As Exception
            lblerror.Text = "rbHistory_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "rbHistory_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txttrade_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttrade.TextChanged
        Try
            'stop_timer()   ''Commented by Imran /Rutuja 25-June-14
            clearFields()
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "txttrade_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txttrade_TextChanged", ErrorLevel.High)
        End Try
    End Sub


    ''ADDED BY SNEHA ON 27-mAR-2014
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

            '<AvinashG. on 29-Sep-2014:FA-639 	Acc/Dec: Tenor - default to 12 Months: Wrongly coded, moving logic within Select Case>Return True
        Catch ex As Exception
            lblerror.Text = "SetFrequencytype:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "SetFrequencyType", ErrorLevel.High)
            Throw ex
        End Try
    End Function



    Public Sub stop_timer()
        Try
            lblMsgPriceProvider.Text = ""
            lblerror.Text = ""
            lblBNPPPrice.Text = ""
            btnBNPPPrice.Text = "Price"
            btnBNPPDeal.CssClass = "btnDisabled"
            Dim strJavaScriptStopTimer As New StringBuilder
            If Val(lblTimerBNPP.Text) > 0 Then
                'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "js2+4", "StopTimer('" + lblTimer.ClientID + "','" + btnBNPPDeal.ClientID + "');", True)
                strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
                btnBNPPPrice.Enabled = True
                btnBNPPPrice.CssClass = "btn"
            End If

            If strJavaScriptStopTimer.Length > 0 Then
                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptStopTimer", strJavaScriptStopTimer.ToString, True)
            End If

            ResetAll()
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = ex.Message.ToString
            lblerror.Text = "Stop_timer:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Stop_timer", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub Stop_timer_Only()
        Try
            Dim strJavaScriptStopTimerOnly As New StringBuilder

            If Val(lblTimerBNPP.Text) > 0 Then
                'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "js2+4", "StopTimer('" + lblTimer.ClientID + "','" + btnBNPPDeal.ClientID + "');", True)
                strJavaScriptStopTimerOnly.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
                btnBNPPPrice.Enabled = True
                btnBNPPPrice.CssClass = "btn"
            End If

            If strJavaScriptStopTimerOnly.Length > 0 Then
                System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptStopTimerOnly", strJavaScriptStopTimerOnly.ToString, True)
            End If
        Catch ex As Exception
            ''lblerror.Text = "Stop_timer_Only:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Stop_timer_Only", ErrorLevel.High)
        End Try
        'ResetAll()

    End Sub


    Public Sub Enable_Disable_Deal_Buttons()
        Try
            btnBNPPPrice.Text = "Price"

            '<AvinashG. on 15-Mar-2014: Css of Price buttons to enabled mode when any pricing parameter is changed>
            '<AvinashG. on 15-Mar-2014: TO check which one was being priced and enable those only>
            btnBNPPPrice.Enabled = True

            btnBNPPPrice.CssClass = "btn"


            btnBNPPDeal.CssClass = "btnDisabled"

            btnBNPPDeal.Visible = False


            DealConfirmPopup.Visible = False
            UPanle11111.Update()
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = ex.Message.ToString
            lblerror.Text = "Enable_Disable_Deal_Buttons:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Enable_Disable_Deal_Buttons", ErrorLevel.High)
            Throw ex

        End Try
    End Sub


#End Region

#Region "Populate XML"



    'Public Sub Get_EQORFQData_TOXML(ByVal PP_ID As String)
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

    '        If Write_EQO_RFQData_TOXML(PP_ID, ER_QuoteRequestId, LoginInfoGV.Login_Info.LoginId, strEntityId, ddlSolveforEQO.SelectedValue, udtStructured_Product_Tranche, StrnoteRFQXML) = True Then
    '            Select Case objELNRFQ.web_Insert_IntoEQO_RFQ(CStr(StrnoteRFQXML))
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


    'Public Function Write_EQO_RFQData_TOXML(ByVal PP_ID As String, _
    '                                    ByVal strQuoteId As String, _
    '                                    ByVal strEntityName As String, _
    '                                    ByVal strEntityId As String, _
    '                                    ByVal strDRAType As String, _
    '                                    ByRef udtStructured_Product_Tranche As Structured_Product_Tranche_ELN, _
    '                                    ByRef o_strXMLNote_RFQ As String) As Boolean

    '    Dim strXMLRFQ As StringBuilder
    '    Dim dtQuote As New DataTable
    '    Try
    '        strXMLRFQ = New StringBuilder
    '        With udtStructured_Product_Tranche

    '            Dim sProductDefinition As String = "<EQO xmlns=""http://www.abcdz.com/StructuredProducts/1.0""><Type>" & udtStructured_Product_Tranche.strELNType & "</Type><SettlementDate>" & udtStructured_Product_Tranche.strValueDate & "</SettlementDate><ExpiryDate>" & udtStructured_Product_Tranche.strFxingdate & "</ExpiryDate><MaturityDate>" & udtStructured_Product_Tranche.strMaturityDate & " </MaturityDate><StrikePercentage>" & udtStructured_Product_Tranche.dblStrike1 & "</StrikePercentage><InterBankPrice Solve=""true"">0.0</InterBankPrice><Underlyings><Underlying><UnderlyingCode Type=""RIC"">" & udtStructured_Product_Tranche.strAsset & "</UnderlyingCode></Underlying></Underlyings></EQO>"

    '            Dim strAsset() = txtBasketShares.Text.Split(","c)

    '            ''<Commented by Rushikesh on 6-April-16 need to remove assistdeff table hence strAssetclass hard coded to "RIC" temp>
    '            'Dim dtShareEQO1 As DataTable = CType(Session.Item("Share_ValueEQO1"), DataTable)
    '            'Dim drShareEQ1 As DataRow() = dtShareEQO1.Select(" AltCode = '" & strAsset(0) & "' ")

    '            'Dim dtShareEQO2 As DataTable
    '            'Dim drShareEQ2 As DataRow()

    '            'If Not Session.Item("Share_ValueEQOddlShareEQO2") Is Nothing And strAsset.Length > 1 Then
    '            '    dtShareEQO2 = CType(Session.Item("Share_ValueEQOddlShareEQO2"), DataTable)
    '            '    drShareEQ2 = dtShareEQO2.Select(" AltCode = '" & strAsset(1) & "' ")
    '            'End If

    '            'Dim dtShareEQO3 As DataTable
    '            'Dim drShareEQ3 As DataRow()

    '            'If Not Session.Item("Share_ValueEQOddlShareEQO3") Is Nothing And strAsset.Length > 2 Then
    '            '    dtShareEQO3 = CType(Session.Item("Share_ValueEQOddlShareEQO3"), DataTable)
    '            '    drShareEQ3 = dtShareEQO3.Select(" AltCode = '" & strAsset(2) & "' ")
    '            'End If
    '            ''</Commented by Rushikesh on 6-April-16 need to remove assistdeff table hence strAssetclass hard coded to "RIC" temp>

    '            strXMLRFQ.Append("<tradeDetails>")
    '            strXMLRFQ.Append("<quoteDetails>")

    '            strXMLRFQ.Append("<ER_PP_ID>" & PP_ID & "</ER_PP_ID>")
    '            strXMLRFQ.Append("<ER_Type>" & udtStructured_Product_Tranche.strELNType & "</ER_Type>")
    '            strXMLRFQ.Append("<ER_SettlmentDate>" & udtStructured_Product_Tranche.strValueDate & "</ER_SettlmentDate>")
    '            strXMLRFQ.Append("<ER_ExpiryDate>" & udtStructured_Product_Tranche.strFxingdate & "</ER_ExpiryDate>")
    '            strXMLRFQ.Append("<ER_MaturityDate>" & udtStructured_Product_Tranche.strMaturityDate & "</ER_MaturityDate>")
    '            strXMLRFQ.Append("<ER_StrikePercentage>" & udtStructured_Product_Tranche.dblStrike1 & "</ER_StrikePercentage>")
    '            strXMLRFQ.Append("<ER_BarrierPercentage>" & udtStructured_Product_Tranche.dblBarrier & "</ER_BarrierPercentage>")
    '            If ddlSolveforEQO.Text <> "Premium" Then
    '                strXMLRFQ.Append("<ER_OfferPrice>" & udtStructured_Product_Tranche.dblPremium & "</ER_OfferPrice>")
    '            End If
    '            strXMLRFQ.Append("<ER_BarrierMonitoringMode>" & udtStructured_Product_Tranche.strBarriermode & "</ER_BarrierMonitoringMode>")
    '            strXMLRFQ.Append("<ER_Barrier_Type>" & udtStructured_Product_Tranche.strBarrierType & "</ER_Barrier_Type>")

    '            ''<Commented by Rushikesh on 6-April-16 need to remove assistdeff table hence strAssetclass hard coded to "RIC" temp>
    '            'strXMLRFQ.Append("<ER_UnderlyingCode_Type>" & drShareEQ1(0).Item("AltCodeType").ToString.ToUpper & _
    '            '                 If(drShareEQ2 Is Nothing, "", "," & drShareEQ2(0).Item("AltCodeType").ToString.ToUpper) & _
    '            '                 If(drShareEQ3 Is Nothing, "", "," & drShareEQ3(0).Item("AltCodeType").ToString.ToUpper) & _
    '            '                 "</ER_UnderlyingCode_Type>")
    '            ''</Commented by Rushikesh on 6-April-16 need to remove assistdeff table hence strAssetclass hard coded to "RIC" temp>

    '            strXMLRFQ.Append("<ER_UnderlyingCode_Type>" & "RIC" & _
    '                             If(ddlShareEQO2.SelectedValue Is Nothing, "", "," & "RIC") & _
    '                             If(ddlShareEQO2.SelectedValue Is Nothing, "", "," & "RIC") & _
    '                             "</ER_UnderlyingCode_Type>")

    '            ''<Commented by RushikeshD.>
    '            'strXMLRFQ.Append("<ER_UnderlyingCode>" & drShareEQ1(0).Item("AltCode").ToString & _
    '            '                 If(drShareEQ2 Is Nothing, "", "," & drShareEQ2(0).Item("AltCode").ToString) & _
    '            '                 If(drShareEQ3 Is Nothing, "", "," & drShareEQ3(0).Item("AltCode").ToString) & _
    '            '                  "</ER_UnderlyingCode>")

    '            strXMLRFQ.Append("<ER_UnderlyingCode>" & ddlShareEQO.SelectedValue & _
    '                            If(ddlShareEQO2.SelectedValue Is Nothing Or ddlShareEQO2.SelectedValue = "", "", "," & ddlShareEQO2.SelectedValue) & _
    '                            If(ddlShareEQO3.SelectedValue Is Nothing Or ddlShareEQO3.SelectedValue = "", "", "," & ddlShareEQO3.SelectedValue) & _
    '                             "</ER_UnderlyingCode>")


    '            '</Neha M. on 12-May-2015: Added as to show share column value and underlying same told by Ajay>
    '            strXMLRFQ.Append("<ER_TenorType>" & udtStructured_Product_Tranche.strTenorType & "</ER_TenorType>")
    '            strXMLRFQ.Append("<ER_Tenor>" & udtStructured_Product_Tranche.inttenor & "</ER_Tenor>")
    '            strXMLRFQ.Append("<ER_TradeDate>" & udtStructured_Product_Tranche.strTradeDate & "</ER_TradeDate>")
    '            strXMLRFQ.Append("<ER_QuoteRequestId>" & strQuoteId & "</ER_QuoteRequestId>")
    '            strXMLRFQ.Append("<ER_SecurityDescription>" & udtStructured_Product_Tranche.strSecurityDesc & "</ER_SecurityDescription>")
    '            strXMLRFQ.Append("<ER_SecuritySubType>" & udtStructured_Product_Tranche.strSecuritySubType & "</ER_SecuritySubType>")
    '            strXMLRFQ.Append("<ER_ExerciseType>" & udtStructured_Product_Tranche.strExerciseType & "</ER_ExerciseType>")
    '            strXMLRFQ.Append("<ER_SettlementType>" & udtStructured_Product_Tranche.strSettlementType & "</ER_SettlementType>")
    '            'strXMLRFQ.Append("<ER_UnderlyingAltCode>" & udtStructured_Product_Tranche.strUnderlyingAltCode & "</ER_UnderlyingAltCode>")

    '            '<Commented by Rushikesh D. on 6-April-16 Set ER_Alt_Exchange in Sp>
    '            'strXMLRFQ.Append("<ER_UnderlyingAltCode>" & drShareEQ1(0).Item("Code").ToString & _
    '            '                 If(drShareEQ2 Is Nothing, "", "," & drShareEQ2(0).Item("Code").ToString) & _
    '            '                 If(drShareEQ3 Is Nothing, "", "," & drShareEQ3(0).Item("Code").ToString) & _
    '            '                 "</ER_UnderlyingAltCode>")
    '            '</Commented by Rushikesh D. on 6-April-16 Set ER_Alt_Exchange in Sp>

    '            strXMLRFQ.Append("<ER_UnderlyingAltCodeType>" & udtStructured_Product_Tranche.strUnderlyingAltCodeType.ToUpper & "</ER_UnderlyingAltCodeType>")

    '            strXMLRFQ.Append("<ER_StrikeType>" & udtStructured_Product_Tranche.strStrikeType & "</ER_StrikeType>")
    '            strXMLRFQ.Append("<ER_UnderlyingProduct>" & udtStructured_Product_Tranche.strUnderlyingProduct & "</ER_UnderlyingProduct>")
    '            strXMLRFQ.Append("<ER_BuySell>" & udtStructured_Product_Tranche.strBuySell & "</ER_BuySell>")
    '            strXMLRFQ.Append("<ER_CashOrderQuantity>" & udtStructured_Product_Tranche.strOrderQty & "</ER_CashOrderQuantity>")
    '            strXMLRFQ.Append("<ER_Nominal_Amount>" & udtStructured_Product_Tranche.strNoOfShares & "</ER_Nominal_Amount>")  'Added by Imran P 12-Nov-2015
    '            strXMLRFQ.Append("<ER_CashCurrency>" & ddlInvestCcy.SelectedValue & "</ER_CashCurrency>")

    '            'Commented by Rushikesh D. on 6-April-16
    '            'strXMLRFQ.Append("<ER_UnderlyingCcy>" & drShareEQ1(0).Item("Currency").ToString & _
    '            '                 If(drShareEQ2 Is Nothing, "", "," & drShareEQ2(0).Item("Currency").ToString) & _
    '            '                 If(drShareEQ3 Is Nothing, "", "," & drShareEQ3(0).Item("Currency").ToString) & _
    '            '                 "</ER_UnderlyingCcy>")
    '            strXMLRFQ.Append("<ER_UnderlyingCcy>" & lblEQOBaseCcy.Text & _
    '                             If(lblBaseCurrency2.Text Is Nothing Or lblBaseCurrency2.Text = "", "", "," & lblBaseCurrency2.Text) & _
    '                             If(lblBaseCurrency3.Text Is Nothing Or lblBaseCurrency3.Text = "", "", "," & lblBaseCurrency3.Text) & _
    '                            "</ER_UnderlyingCcy>")

    '            strXMLRFQ.Append("<ER_TransactionTime>" & DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff tt") & "</ER_TransactionTime>")
    '            strXMLRFQ.Append("<ER_Quanto_Currency>" & udtStructured_Product_Tranche.strQuanto_Currency & "</ER_Quanto_Currency>")
    '            'strXMLRFQ.Append("<ER_Created_At>""</ER_Created_At>")  inserted at SP level
    '            strXMLRFQ.Append("<ER_Created_By>" & strEntityName & "</ER_Created_By>")
    '            strXMLRFQ.Append("<ER_Remark1></ER_Remark1>")
    '            strXMLRFQ.Append("<ER_Remark2></ER_Remark2>")
    '            strXMLRFQ.Append("<ER_Misc1></ER_Misc1>")
    '            strXMLRFQ.Append("<ER_Misc2></ER_Misc2>")
    '            strXMLRFQ.Append("<ER_Active_YN>Y</ER_Active_YN>")
    '            strXMLRFQ.Append("<ER_SubScheme>" & udtStructured_Product_Tranche.strTemplateName & "</ER_SubScheme>")
    '            'Commented by Rushikesh D. on 6-April-16
    '            'strXMLRFQ.Append("<ER_Exchange>" & drShareEQ1(0).Item("FinIQ_Exchange_Code").ToString & _
    '            '                 If(drShareEQ2 Is Nothing, "", "," & drShareEQ2(0).Item("FinIQ_Exchange_Code").ToString) & _
    '            '                 If(drShareEQ3 Is Nothing, "", "," & drShareEQ3(0).Item("FinIQ_Exchange_Code").ToString) & _
    '            '                  "</ER_Exchange>")
    '            strXMLRFQ.Append("<ER_Exchange>" & udtStructured_Product_Tranche.strExchange & "</ER_Exchange>")

    '            '<Commented by Rushikesh D. on 6-April-16 Set ER_Alt_Exchange in Sp>
    '            'strXMLRFQ.Append("<ER_Alt_Exchange>" & drShareEQ1(0).Item("ExchangeCode").ToString & _
    '            '                 If(drShareEQ2 Is Nothing, "", "," & drShareEQ2(0).Item("ExchangeCode").ToString) & _
    '            '                 If(drShareEQ3 Is Nothing, "", "," & drShareEQ3(0).Item("ExchangeCode").ToString) & _
    '            '                  "</ER_Alt_Exchange>")
    '            '</Commented by Rushikesh D. on 6-April-16 Set ER_Alt_Exchange in Sp>

    '            strXMLRFQ.Append("<ER_Quote_Request_YN>Y</ER_Quote_Request_YN>")
    '            strXMLRFQ.Append("<ER_Entity_ID>" & strEntityId & "</ER_Entity_ID>")
    '            strXMLRFQ.Append("<ER_Issuer_Date_Offset>" & udtStructured_Product_Tranche.strIssuer_Date_Offset & "</ER_Issuer_Date_Offset>")
    '            strXMLRFQ.Append("<ER_Template_ID>" & udtStructured_Product_Tranche.lngTemplateId & "</ER_Template_ID>")
    '            'strXMLRFQ.Append("<ER_Frequency>""</ER_Frequency>")    
    '            'strXMLRFQ.Append("<ER_Nominal_Amount>""</ER_Nominal_Amount>") not required for now
    '            strXMLRFQ.Append("<ER_SolveFor>" & udtStructured_Product_Tranche.strSolveFor & "</ER_SolveFor>")
    '            strXMLRFQ.Append("<ER_EntityName>" & udtStructured_Product_Tranche.strEntityName & "</ER_EntityName>")
    '            strXMLRFQ.Append("<ER_RFQ_RMName>" & udtStructured_Product_Tranche.strRFQRMName & "</ER_RFQ_RMName>")
    '            strXMLRFQ.Append("<ER_EmailId>" & udtStructured_Product_Tranche.strEmailId & "</ER_EmailId>")
    '            strXMLRFQ.Append("<ER_Branch>" & udtStructured_Product_Tranche.strBranch & "</ER_Branch>")
    '            strXMLRFQ.Append("<EP_RM_Margin>" & udtStructured_Product_Tranche.dblRMMargin & "</EP_RM_Margin>")  ''Branch 
    '            strXMLRFQ.Append("<ER_Upfront>" & udtStructured_Product_Tranche.dblUpfront.ToString & "</ER_Upfront>")
    '            strXMLRFQ.Append("</quoteDetails>")
    '            strXMLRFQ.Append("</tradeDetails>")
    '        End With
    '        o_strXMLNote_RFQ = strXMLRFQ.ToString
    '        Write_EQO_RFQData_TOXML = True

    '    Catch ex As Exception
    '        lblerror.Text = "Write_EQO_RFQData_TOXML:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "Write_EQO_RFQData_TOXML", ErrorLevel.High)

    '        Throw ex
    '    End Try
    'End Function
    'Private Sub setUDTValuesFromForm(ByRef udtStructured_Product_Tranche As Structured_Product_Tranche_ELN)
    '    Dim strFixingDate As String = String.Empty
    '    Try
    '        If tabContainer.ActiveTabIndex = prdTab.EQO Then
    '            udtStructured_Product_Tranche.dblUpfront = Val(txtUpfrontEQO.Text) * 100
    '            udtStructured_Product_Tranche.dblBarrier = txtBarrierLevelEQO.Text  ''TO DO
    '            udtStructured_Product_Tranche.dblStrike1 = Val(txtStrikeEQO.Text.Replace(",", ""))
    '            udtStructured_Product_Tranche.dblPremium = Val(txtPremium.Text) / 100
    '            udtStructured_Product_Tranche.inttenor = CInt(txtTenorEQO.Text)
    '            udtStructured_Product_Tranche.strAsset = ddlShareEQO.SelectedValue
    '            udtStructured_Product_Tranche.lngTemplateId = Convert.ToString(Session("Template_Code"))
    '            udtStructured_Product_Tranche.strIssuer_Date_Offset = "2" ''txtValueDays.Text   need to discuss for value days for now using T+2

    '            ''udtStructured_Product_Tranche.strExchange = ddlExchangeEQO.SelectedValue ''Commented by Rushikesh D. for All exchange case.
    '            If ddlExchangeEQO.SelectedValue.ToUpper = "ALL" Then
    '                Dim sTemp As String
    '                udtStructured_Product_Tranche.strExchange = objELNRFQ.GetShareExchange(ddlShareEQO.SelectedValue.ToString, sTemp) & _
    '                           If(ddlExchangeEQO2.SelectedValue Is Nothing Or ddlExchangeEQO2.SelectedValue = "", "", "," & objELNRFQ.GetShareExchange(ddlShareEQO2.SelectedValue.ToString, sTemp)) & _
    '                           If(ddlExchangeEQO3.SelectedValue Is Nothing Or ddlExchangeEQO3.SelectedValue = "", "", "," & objELNRFQ.GetShareExchange(ddlShareEQO3.SelectedValue.ToString, sTemp))
    '            Else
    '                udtStructured_Product_Tranche.strExchange = ddlExchangeEQO.SelectedValue & _
    '                           If(ddlExchangeEQO2.SelectedValue Is Nothing Or ddlExchangeEQO2.SelectedValue = "", "", "," & ddlExchangeEQO2.SelectedValue) & _
    '                           If(ddlExchangeEQO3.SelectedValue Is Nothing Or ddlExchangeEQO3.SelectedValue = "", "", "," & ddlExchangeEQO3.SelectedValue)
    '            End If
    '            ''<Commented by Rushikesh on 6-April-16 need to remove assistdeff table hence strAssetclass hard coded to "RIC" temp>
    '            'Dim dtShareEQO As DataTable = CType(Session.Item("Share_ValueEQO1"), DataTable)
    '            'Dim dr As DataRow() = dtShareEQO.Select("Code = '" & ddlShareEQO.SelectedValue.Trim & "' ")
    '            'If dr.Length > 0 Then
    '            '    udtStructured_Product_Tranche.strAssetclass = dr(0).Item("AltCodeType").ToString()
    '            'End If
    '            udtStructured_Product_Tranche.strAssetclass = "RIC"
    '            ''</Commented by Rushikesh on 6-April-16 need to remove assistdeff table hence strAssetclass hard coded to "RIC" temp>
    '            udtStructured_Product_Tranche.strNoOfShares = Replace(txtOrderqtyEQO.Text, ",", "") 'Changed by Imran P 12-Nov-2015
    '            udtStructured_Product_Tranche.strOrderQty = Replace(lblEstimatedNotional.Text, ",", "")  'Added by Imran P 12-Nov-2015
    '            udtStructured_Product_Tranche.strValueDate = txtSettlDateEQO.Value  ''Convert.ToString(Session("Settlementdate"))
    '            udtStructured_Product_Tranche.strFxingdate = txtExpiryDateEQO.Value  ''Convert.ToString(Session("expiryDAte"))
    '            udtStructured_Product_Tranche.strMaturityDate = txtMaturityDateEQO.Value ''Convert.ToString(Session("MaturityDAte"))
    '            udtStructured_Product_Tranche.strTradeDate = txtTradeDateEQO.Value  ''Convert.ToString(Session("TradeDAte"))
    '            'udtStructured_Product_Tranche.strPrice = Val(txtELNPrice.Text)
    '            udtStructured_Product_Tranche.strTenorType = ddlTenorEQO.SelectedValue
    '            udtStructured_Product_Tranche.strSecurityDesc = "OTCOPTION" ''Convert.ToString(Session("Scheme")) ''To Do for now it is hardcoded
    '            udtStructured_Product_Tranche.strSolveFor = ddlSolveforEQO.SelectedValue
    '            If ddlOptionType.SelectedValue.ToUpper.Contains("CALL") Then
    '                udtStructured_Product_Tranche.strSecuritySubType = "Call"
    '            Else
    '                udtStructured_Product_Tranche.strSecuritySubType = "Put"
    '            End If

    '            If ddlOptionType.SelectedValue.ToUpper.Contains("EUROPEAN") Then
    '                udtStructured_Product_Tranche.strExerciseType = "European"
    '            ElseIf ddlOptionType.SelectedValue.ToUpper.Contains("AMERICAN") Then
    '                udtStructured_Product_Tranche.strExerciseType = "American"
    '            Else
    '                udtStructured_Product_Tranche.strExerciseType = "Barrier"  ''Need to decide exercise type for KNOCKIN PUT  ''TO DO
    '            End If

    '            If ddlsettlmethod.SelectedValue.ToUpper.Contains("PHYSICAL") Then
    '                udtStructured_Product_Tranche.strSettlementType = "P"
    '            Else
    '                udtStructured_Product_Tranche.strSettlementType = "C"
    '            End If

    '            If txtBasketShares.Text = "" Then
    '                udtStructured_Product_Tranche.strUnderlyingAltCode = ddlShareEQO.SelectedValue    ''For now use current underlying later will use  Bloomberg code
    '            Else
    '                udtStructured_Product_Tranche.strUnderlyingAltCode = txtBasketShares.Text    ''For now use current underlying later will use  Bloomberg code
    '            End If

    '            udtStructured_Product_Tranche.strUnderlyingAltCodeType = "Bloomberg"
    '            udtStructured_Product_Tranche.strStrikeType = ddlStrikeTypeEQO.SelectedValue

    '            If ddlProductEQO.SelectedValue.ToUpper.Contains("EQUITY") Then
    '                udtStructured_Product_Tranche.strUnderlyingProduct = "EQUITY"
    '            Else
    '                udtStructured_Product_Tranche.strUnderlyingProduct = "INDEX"
    '            End If
    '            udtStructured_Product_Tranche.strCurrency = lblEQOBaseCcy.Text
    '            udtStructured_Product_Tranche.strQuanto_Currency = ddlSettlCcyEQO.SelectedValue
    '            udtStructured_Product_Tranche.strBuySell = ddlSideEQO.SelectedValue
    '            If ddlOptionType.SelectedValue.ToUpper.Contains("KNOCKIN PUT") Then
    '                If ddlSolveforEQO.SelectedItem.Text.Trim.ToUpper <> "BARRIER" Then
    '                    udtStructured_Product_Tranche.strELNType = "WOKIP"
    '                    udtStructured_Product_Tranche.dblBarrier = txtBarrierLevelEQO.Text
    '                    udtStructured_Product_Tranche.strBarriermode = ddlBarrierMonitoringType.SelectedValue
    '                    udtStructured_Product_Tranche.strBarrierType = ddlBarrierEQO.SelectedValue    ''ABSOLUTE, PERCENTAGE
    '                Else
    '                    udtStructured_Product_Tranche.strELNType = "WOKIP"
    '                    udtStructured_Product_Tranche.dblBarrier = ""
    '                    udtStructured_Product_Tranche.strBarriermode = ddlBarrierMonitoringType.SelectedValue
    '                    udtStructured_Product_Tranche.strBarrierType = ""
    '                End If

    '            Else
    '                udtStructured_Product_Tranche.strELNType = "VANI"
    '                udtStructured_Product_Tranche.dblBarrier = ""
    '                udtStructured_Product_Tranche.strBarriermode = ""
    '                udtStructured_Product_Tranche.strBarrierType = ""
    '            End If
    '        End If
    '        ''set Dealer detail records in XML''
    '        udtStructured_Product_Tranche.strEntityName = ddlentity.SelectedItem.Text
    '        '<AvinashG. on 17-Dec-2014: FA-768 	Move RM dropdown from pricer page to order popup >
    '        'udtStructured_Product_Tranche.strRMName = ddlRM.SelectedValue
    '        udtStructured_Product_Tranche.strRFQRMName = ddlRFQRM.SelectedItem.Text

    '        Dim strLoginUserEmailID As String
    '        strLoginUserEmailID = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)

    '        udtStructured_Product_Tranche.strEmailId = strLoginUserEmailID
    '        '<Rutuja S. on 16-Dec-2014:Commented code as we are saving only login email for RFQ>
    '        '</Sarun/Rutuja S. on 19-Sep-2014:Added for Jira id : FA-602,taking primary email id of logged in user and selected RM>
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

    ''KBM on 17-Feb-2014
    Public Function set_ELN_ClientYield_Price(ByVal dblIssuerPrice As Double, ByVal issuer As String) As Boolean
        Try
            Dim dblClientPrice As Double = 0
            'dblClientPrice = dblIssuerPrice + CDbl(txtUpfrontELN.Text)

            Select Case UCase(issuer)
                Case "BNPP"
                    lblBNPPClientPrice.Text = SetNumberFormat(dblClientPrice, 2)

            End Select

            Return True
        Catch ex As Exception
            lblerror.Text = "set_ELN_ClientYield_Price:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "set_ELN_ClientYield_Price", ErrorLevel.High)
            Throw ex
        End Try
    End Function



    ''KBM on 17-Feb-2014 for solve for strike casse
    Public Function set_ELN_AllIssuerPrice_ClientYield_ClientPrice(ByVal dblIssuerPrice As Double) As Boolean

        Try
            Dim dblClientPrice As Double = 0
            'dblClientPrice = dblIssuerPrice + CDbl(txtUpfrontELN.Text)

            Dim strClientYield As String = ""
            'strClientYield = get_ELN_ClientYield(dblClientPrice)


            'lblBNPPPrice.Text = dblIssuerPrice.ToString
            lblBNPPClientPrice.Text = SetNumberFormat(dblClientPrice, 2)
            lblBNPPClientYield.Text = strClientYield

            pnlReprice.Update()

            Return True
        Catch ex As Exception
            lblerror.Text = "set_ELN_AllIssuerPrice_ClientYield_ClientPrice:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "set_ELN_AllIssuerPrice_ClientYield_ClientPrice", ErrorLevel.High)
            Throw ex
        End Try
    End Function


    ''KBM on 17-Feb-2014 for solve for strike casse
    Public Function setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice(Optional ByVal activeTabIndex As Integer = 0) As Boolean
        Try

            lblBNPPPrice.Text = "" ''"0.0"
            lblBNPPClientPrice.Text = "" ''"0.0"
            lblBNPPClientYield.Text = "" ''"0.0"


            pnlReprice.Update()

            Return True
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = "Error in setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice:" & ex.Message
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
        Dim sPrdCcy As String
        Dim sRangeCcy As String
        Dim sPrd As String
        Dim sEQC_DealerRedirection_OnPricer As String
        Dim dblUserLimit As Double
        Dim sExchange1, sExchange2, sExchange3 As String
        Dim sStock1, sStock2, sStock3 As String
        Try
            '<AvinashG. on 04-Sep-2014: FA543>
            dtRangeLimit = New DataTable("LimitData")
            sEQC_DealerRedirection_OnPricer = objReadConfig.ReadConfig(dsConfig, "EQC_DealerRedirection_OnPricer_NO", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO")               'Config Name changed by Mohit Lalwani on 27-Apr-2016 to diable RM from Redirection to use Redirection remove _no from config Name 
            'sPrdCcy = CType(Session("RangePrd"), String)'<AvinashG. on 22-Sep-2014: FA-613 - Min/Max Range not following sub-product ranges>
            '<Rutuja S. on 17-Dec-2014: FA-768 	Move RM dropdown from pricer page to order popup >
            If ddlRM.Items.Count = 0 OrElse ddlRM.SelectedItem.Text = "" Then
                lblerrorPopUp.Text = "Cannot proceed with order. Please select an RM."  '<Rutuja S. on 18-Dec-2014:Added fullstop>
                chk_DealValidations = False
                Exit Function
            Else
                chk_DealValidations = True
            End If

            ''<AshwiniP on 19-Sept-16 START>
            dt = New DataTable("PRRating")
            Select Case objELNRFQ.DB_UnderlyingRiskRatingShare(ddlShareEQO.SelectedValue.ToString, dt)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dt.Rows(0).Item(0).ToString = "NA" Then
                        lblerrorPopUp.ForeColor = Color.Red
                        lblerrorPopUp.Text = "Order cannot be placed as PRR is not available for " + ddlShareEQO.SelectedValue.ToString + "."
                        'chkConfirmDeal.Visible = False
                        chk_DealValidations = False
                        Exit Function
                    Else
                        chk_DealValidations = True
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            End Select
            Select Case objELNRFQ.DB_UnderlyingRiskRatingShare(ddlShareEQO2.SelectedValue.ToString, dt)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dt.Rows(0).Item(0).ToString = "NA" Then
                        lblerrorPopUp.ForeColor = Color.Red
                        lblerrorPopUp.Text = "Order cannot be placed as PRR is not available for " + ddlShareEQO2.SelectedValue.ToString + "."
                        'chkConfirmDeal.Visible = False
                        chk_DealValidations = False
                        Exit Function
                    Else
                        chk_DealValidations = True
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            End Select
            Select Case objELNRFQ.DB_UnderlyingRiskRatingShare(ddlShareEQO3.SelectedValue.ToString, dt)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dt.Rows(0).Item(0).ToString = "NA" Then
                        lblerrorPopUp.ForeColor = Color.Red
                        lblerrorPopUp.Text = "Order cannot be placed as PRR is not available for " + ddlShareEQO3.SelectedValue.ToString + "."
                        ' chkConfirmDeal.Visible = False
                        chk_DealValidations = False
                        Exit Function
                    Else
                        chk_DealValidations = True
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
            End Select
            ''<END>

            If CDbl(lblClientPricePopUpValue.Text) <= 0 Then            'Mohit Lalwani on 21-Apr-2016
                '<Price is Replaced by Premium in message by  Mohit Lalwani on 23-Nov-2015>
                lblerrorPopUp.Text = "Cannot proceed with order. Client Premium can not be zero or negative."  '<Rutuja S. on 18-Dec-2014:Added fullstop>
                '</Price is Replaced by Premium in message by  Mohit Lalwani on 23-Nov-2015>
                chk_DealValidations = False
                Exit Function
            Else
                chk_DealValidations = True
            End If
            '</Rutuja S. on 17-Dec-2014: FA-768 	Move RM dropdown from pricer page to order popup >
            sPPCode = lblIssuerPopUpValue.Text

            If ddlOrderTypePopUpValue.SelectedValue.Trim.ToUpper = "LIMIT" Then
                If txtLimitPricePopUpValue.Text = "" OrElse Val(txtLimitPricePopUpValue.Text) = 0 Then
                    lblerrorPopUp.Text = "Please enter limit price."
                    chk_DealValidations = False
                    Exit Function
                Else
                    chk_DealValidations = True
                End If
                '<AvinashG. on 15-Oct-2014: FA-681 - Limit Price precision while saving>
                If (txtLimitPricePopUpValue.Text.Length - (txtLimitPricePopUpValue.Text.LastIndexOf(".") + 1)) > 4 And CDbl(txtLimitPricePopUpValue.Text) <> Math.Floor(CDbl(txtLimitPricePopUpValue.Text)) Then
                    lblerrorPopUp.Text = "Precision of " + lblLimitPricePopUpCaption.Text + " cannot exceed four digits after decimal point."
                    chk_DealValidations = False
                    Exit Function
                Else
                    chk_DealValidations = True
                End If
                '</AvinashG. on 15-Oct-2014: FA-681 - Limit Price precision while saving>
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
                        lblerrorPopUp.Text = "Please select Customer."
                        Return False
                    End If
                    If isChecked AndAlso Qty_Validate(row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim) = False Then
                        chkUpfrontOverride.Visible = False
                        lblerrorPopUp.Text = "Please enter valid Notional."
                        Return False
                    ElseIf isChecked AndAlso (row.Cells(3).Controls.OfType(Of TextBox)().FirstOrDefault().Text.Trim) = "0" Then ''<Nikhil M. on 17-Oct-2016: Added to check the no zero notional RM  >
                        chkUpfrontOverride.Visible = False
                        lblerrorPopUp.Text = "Please enter valid Notional."
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
                ''<Start | Nikhil M. on 18-Oct-2016: Added for Order type >
                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                    Case "BOTH"
                        If rdbQuantity.Checked Then
                            If TotalSum <> CDbl(lblNotionalPopUpValue.Text) Then
                                chkUpfrontOverride.Visible = False
                                lblerrorPopUp.Text = "Sum of Notional does not equal Order Quantity."
                                Return False
                            End If
                        Else
                            If TotalSum <> CDbl(lblNoOfSharePopUpValue.Text) Then
                                chkUpfrontOverride.Visible = False
                                lblerrorPopUp.Text = "Sum of Shares does not equal Order Quantity."
                                Return False
                            End If
                        End If
                    Case "NOTIONAL"
                        If TotalSum <> CDbl(lblNotionalPopUpValue.Text) Then
                            chkUpfrontOverride.Visible = False
                            lblerrorPopUp.Text = "Sum of Notional does not equal Order Quantity."
                            Return False
                        End If

                    Case "NOOFSHARE"
                        If TotalSum <> CDbl(lblNoOfSharePopUpValue.Text) Then
                            chkUpfrontOverride.Visible = False
                            lblerrorPopUp.Text = "Sum of Shares does not equal Order Quantity."
                            Return False
                        End If
                End Select
                ''<Start | Nikhil M. on 18-Oct-2016: Added for Order type >           
               

            Else
                chkUpfrontOverride.Visible = False
                lblerrorPopUp.Text = "Please add Allocation."
            End If
            ''</Changed by ashwiniP on 22Sept2016
	    ''<Added by Rushikesh As told by Sanchita on 5Nov16>
            If chkDuplicateRecords() Then     ''Removed by AshwiniP on 01-Oct-2016
            Else
                Return False
            End If
	    ''</Added by Rushikesh As told by Sanchita on 5Nov16>
            ''</ashwiniP on 22Sept2016

            '''''''Dilkhush Avinash upfront check 
            'EQBOSDEV-435 - Set Min/Max upfront for HK and non HK underlyings for notes and options
            Dim arrStocks() As String
            Dim iBasketCnt As Integer
            arrStocks = lblUnderlyingPopUpValue.Text.Split(CChar(","))
            iBasketCnt = arrStocks.Length

            sStock1 = arrStocks(0)
            sStock2 = If(iBasketCnt > 1, arrStocks(1), "")
            sStock3 = If(iBasketCnt > 2, arrStocks(2), "")

            If ddlExchangeEQO.SelectedValue.ToUpper = "ALL" Then
                Dim sTemp As String
                sExchange1 = objELNRFQ.GetShareExchange(sStock1, sTemp)
            Else
                sExchange1 = ddlExchangeEQO.SelectedValue
            End If

            If (iBasketCnt > 1) Then
                If ddlExchangeEQO2.SelectedValue.ToUpper = "ALL" Then
                    Dim sTemp As String
                    sExchange2 = objELNRFQ.GetShareExchange(sStock2, sTemp)
                Else
                    sExchange2 = ddlExchangeEQO2.SelectedValue
                End If

                If (iBasketCnt > 2) Then
                    If ddlExchangeEQO3.SelectedValue.ToUpper = "ALL" Then
                        Dim sTemp As String
                        sExchange3 = objELNRFQ.GetShareExchange(sStock3, sTemp)
                    Else
                        sExchange3 = ddlExchangeEQO3.SelectedValue
                    End If

                End If
            End If

            Dim objdeal As Web_FinIQ_MarketData.QECapture
            objdeal = New Web_FinIQ_MarketData.QECapture
            objdeal.ProductCode = "OPTIONS"
            objdeal.sStock = sStock1
            objdeal.sStockCcy = lblEQOBaseCcy.Text
            objdeal.sQuantoCcy = lblCurrencyPopUpValue.Text

            If ddlOrderTypePopUpValue.SelectedValue.ToUpper = "MARKET" Then
                If iBasketCnt = 1 Then
                    objdeal.ExchangeName = sExchange1
                ElseIf iBasketCnt = 2 Then
                    objdeal.ExchangeName = If(sExchange1 = sExchange2, sExchange1, sExchange1 + "," + sExchange2)
                ElseIf iBasketCnt = 3 Then
                    If (sExchange1 = sExchange2 And sExchange2 = sExchange3) Then
                        objdeal.ExchangeName = sExchange1
                    Else
                        objdeal.ExchangeName = sExchange1 + "," + sExchange2 + "," + sExchange3
                    End If
                End If
            Else
                ''As discussed on call with Sanchita
                Dim sLimitStock As String = ddlBasketSharesPopValue.SelectedValue
                Select Case sLimitStock
                    Case sStock1
                        objdeal.ExchangeName = sExchange1
                    Case sStock2
                        objdeal.ExchangeName = sExchange2
                    Case sStock3
                        objdeal.ExchangeName = sExchange3
                    Case Else
                        objdeal.ExchangeName = sExchange1 + "," + sExchange2 + "," + sExchange3
                End Select
            End If


            objdeal.Notional = CDbl(If(lblNotionalPopUpValue.Text = "", lblNoOfSharePopUpValue.Text, lblNotionalPopUpValue.Text))
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




            '<Added by Mohit Lalwani on 18_Nov-2015>
            sRangeCcy = lblEQOBaseCcy.Text
            sPrd = "OTC Option"

            Dim Notional As Double
            Dim EqPair As String = ddlShareEQO.SelectedValue.ToString & " - " & lblEQOBaseCcy.Text

            Dim BidRate As Double = objELNRFQ.GetShareRate(EqPair, BidRate)

            'Dim iEQODays As Integer

            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowIssuerLimitCheckForAccDec", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    If rdbQuantity.Checked Then
                        If BidRate = 0 Or BidRate = Nothing Then
                            lblerrorPopUp.Text = "Cannot proceed. Share rate not specified."
                            chk_DealValidations = False
                            Exit Function ''Added by Rushikesh
                        Else
                            '<AvinashG. on 01-Jul-2016: EQO doesn't have no of sahres/day>
                            ' Notional Validations
                            'Select Case lblIssuerPopUpValue.Text.Trim.ToUpper
                            '    Case "BNPP"
                            '        iEQODays = CInt(lblBNPPClientPrice.Text.Replace(",", ""))
                            'End Select
                            'Notional = (Convert.ToDouble(txtOrderqtyEQO.Text) * iEQODays * BidRate)
                            Notional = (Convert.ToDouble(txtOrderqtyEQO.Text) * BidRate)
                        End If
                    Else
                        Notional = Convert.ToDouble(txtNotional.Text)
                    End If

                    Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                        Case "Y", "YES"
                            'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                            'Dim sLoginGrp As String
                            'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
                            'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                            If UCase(Request.QueryString("Mode")) = "ALL" Then
                                ''User is Dealer
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
                                '<\Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
                                Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, sRangeCcy, sPrd, dblUserLimit)
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                        If dblUserLimit > 0 Then
                                            If Notional > dblUserLimit Then ''Condition for notional greater than issuer limit will get checked in previous if
                                                ''Dilkhush/AVinash 03Feb2016 Added limit in message
                                                lblerrorPopUp.Text = "Notional Size is larger than your permitted limit (" + FormatNumber(dblUserLimit.ToString, 0) + " " + sRangeCcy + "). Do you want to redirect this order to dealer?"
                                                ''lblerrorPopUp.Text = "Notional Size is larger than your permitted limit. Do you want to redirect this order to dealer?"
                                                '''''''''''''''btnRedirect.Visible = True
                                                btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                                chk_DealValidations = False
                                            Else
                                                chk_DealValidations = True
                                            End If
                                            '<Added by Mohit Lalwani on 2‐Nov‐2015 for DRA FA‐1169 >
                                        Else
                                            ''In‐aligment with the JIRA description allowing redirection if user limit is zero
                                            If dblUserLimit = 0 Then
                                                lblerrorPopUp.Text = "Limit not found or Zero limit found. Do you want to redirect this order to dealer?"
                                                ''''''''''''''btnRedirect.Visible = True
                                                btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                                chk_DealValidations = False
                                                LogException(LoginInfoGV.Login_Info.LoginId, "User/User Group Limit found 0(Zero) for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                            Else
                                                LogException(LoginInfoGV.Login_Info.LoginId, "Invalid value(" + dblUserLimit.ToString + ") found for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
                                                sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                                chk_DealValidations = False
                                            End If
                                            '</Added by Mohit Lalwani on 2‐Nov‐2015 for DRA FA‐1169 >
                                        End If
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                        lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
                                        chk_DealValidations = False
                                End Select
                                'End If '<Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
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
                    End Select


                    '</Added by Mohit Lalwani on 18_Nov-2015>


                Case "N", "NO"
                    Select Case sEQC_DealerRedirection_OnPricer.ToUpper
                        Case "Y", "YES"
                            'sEQC_DealerLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_DealerLoginGroups", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
                            'Dim sLoginGrp As String
                            'sLoginGrp = LoginInfoGV.Login_Info.LoginGroup
                            'If sEQC_DealerLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                            If UCase(Request.QueryString("Mode")) = "ALL" Then
                                ''User is Dealer
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
                                chk_DealValidations = True

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
                                '<\Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
                                If rdbQuantity.Checked Then
                                    If BidRate = 0 Or BidRate = Nothing Then
                                        lblerrorPopUp.Text = "Cannot proceed. Share rate not specified."
                                        chk_DealValidations = False
                                        Exit Function ''Added by Rushikesh
                                    Else
                                        '<AvinashG. on 01-Jul-2016: EQO doesn't have no of sahres/day>
                                        ' Notional Validations
                                        'Select Case lblIssuerPopUpValue.Text.Trim.ToUpper
                                        '    Case "BNPP"
                                        '        iEQODays = CInt(lblBNPPClientPrice.Text.Replace(",", ""))
                                        'End Select
                                        ' Notional = (Convert.ToDouble(txtOrderqtyEQO.Text) * iEQODays * BidRate)
                                        Notional = (Convert.ToDouble(txtOrderqtyEQO.Text) * BidRate)
                                    End If
                                Else
                                    Notional = Convert.ToDouble(txtNotional.Text)
                                End If

                                Select Case objELNRFQ.GetUserLimit(LoginInfoGV.Login_Info.LoginId, sRangeCcy, sPrd, dblUserLimit)
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                        If dblUserLimit > 0 Then
                                            If Notional > dblUserLimit Then ''Condition for notional greater than issuer limit will get checked in previous if
                                                ''Dilkhush/AVinash 03Feb2016 Added limit in message
                                                lblerrorPopUp.Text = "Notional Size is larger than your permitted limit (" + FormatNumber(dblUserLimit.ToString, 0) + " " + sRangeCcy + "). Do you want to redirect this order to dealer?"
                                                ''lblerrorPopUp.Text = "Notional Size is larger than your permitted limit. Do you want to redirect this order to dealer?"
                                                '''''''''''''''btnRedirect.Visible = True
                                                btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                                chk_DealValidations = False
                                            Else
                                                chk_DealValidations = True
                                            End If
                                            '<Added by Mohit Lalwani on 2‐Nov‐2015 for DRA FA‐1169 >
                                        Else
                                            ''In‐aligment with the JIRA description allowing redirection if user limit is zero
                                            If dblUserLimit = 0 Then
                                                lblerrorPopUp.Text = "Limit not found or Zero limit found. Do you want to redirect this order to dealer?"
                                                ''''''''''''''btnRedirect.Visible = True
                                                btnDealConfirm.Visible = False '<AvinashG. on 26-Feb-2016: FA-1327 - Hide confirm button and show redirect for redirection>
                                                chk_DealValidations = False
                                                LogException(LoginInfoGV.Login_Info.LoginId, "User/User Group Limit found 0(Zero) for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                            Else
                                                LogException(LoginInfoGV.Login_Info.LoginId, "Invalid value(" + dblUserLimit.ToString + ") found for " + LoginInfoGV.Login_Info.LoginId + " for " + sRangeCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
                                                sSelfPath, "chk_DealValidations", ErrorLevel.Medium)
                                                chk_DealValidations = False
                                            End If
                                            '</Added by Mohit Lalwani on 2‐Nov‐2015 for DRA FA‐1169 >
                                        End If
                                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data, Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                                        lblerrorPopUp.Text = "Cannot proceed. User/User Group limit not found."
                                        chk_DealValidations = False
                                End Select
                                'End If '<Commented By Imran on 19‐Dec‐2015 FA‐1236 ‐ Allow user to Quote without Notional>
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
                            chk_DealValidations = True
                    End Select





            End Select

            ''<Start | Nikhil M. on 21-Sep-2016:  Added For Softblock>
            If ddlSideEQO.SelectedText.Trim = "Sell" And ddlOptionType.SelectedText.Contains("Call") _
                                     Or ddlSideEQO.SelectedText.Trim = "Buy" And ddlOptionType.SelectedText.Contains("Put") Then
                If chkConfirmDeal.Checked = True Then
                    chk_DealValidations = True
                    ResetCommetryElement() ''<Nikhil M. on 21-Sep-2016:Added>
                Else
                    chkConfirmDeal.Visible = True
		    chkConfirmDeal.Text = "Sell Call and Buy Put are not allowed to trade. Please select to continue."
                    lblerrorPopUp.Text = ""
                    chkUpfrontOverride.Visible = False
                    chk_DealValidations = False
                End If
            End If

            ''<End | Nikhil M. on 21-Sep-2016:  Added For Softblock>
            chk_DealValidations = True             ''AshwiniP on 05-Oct-2016
        Catch ex As Exception
            lblerror.Text = "chk_DealValidations:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chk_DealValidations", ErrorLevel.High)

            Throw ex
        End Try
    End Function

    Public Sub btnBNPPDeal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBNPPDeal.Click
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
            Quote_ID = Convert.ToString(Session("BNPPQuote"))
            Session.Remove("BNPPQuote")
            ''</Imran 7April:Added for getting pp's latest generated Id>
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

            '<AvinashG. on 18-Mar-2014: TO CLose timer if deal done>
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


    Private Sub DealClicked(ByVal strId As String)
        Dim orderQuantity As String = ""
        Dim strType As String = ""
        Dim strLimitPrice1 As String = ""
        Dim strLimitPrice2 As String = ""
        Dim strLimitPrice3 As String = ""
        Dim strMargin As String = "" ''KBM/Rutuja for saving on order confirm
        Dim strClientPrice As String = ""
        Dim strClientYield As String = ""
        Dim strBookingBranch As String = "" ''end
        Dim strJavaScriptDealClicked As New StringBuilder
        '<Rutuja S. on 16-Dec-2014: Added variables for RMname and RM emailid>
        Dim strRMNameforOrderConfirm As String = ""
        Dim strRMEmailIdforOrderConfirm As String = ""
        Dim strLoginUserEmail As String = ""
        '</Rutuja S. on 16-Dec-2014: Added variables for RMname and RM emailid>
        Try

            ''Stop_timer_Only()


            'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "js2", "StopTimer('" + lblTimer.ClientID + "','" + btnBNPPDeal.ClientID + "');", True)
            strJavaScriptDealClicked.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")


            lblerror.Text = ""

            ' lblMsg.Text = "Deal for RFQ " & strId & " is placed."
            'lblerror.Text = "Order requested for RFQ " & strId
            'lblerror.ForeColor = Color.Blue
            Select Case tabContainer.ActiveTabIndex

                Case prdTab.EQO
                    strMargin = txtUpfrontPopUpValue.Text
                    strClientPrice = lblClientPricePopUpValue.Text
                    'strClientYield = lblClientYieldPopUpValue.Text
                    strBookingBranch = ddlBookingBranchPopUpValue.SelectedValue

                    'If rdbQuantity.Checked Then
                    '    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAQDQ_Estimated_Notional", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    '        Case "Y", "YES"
                    '            orderQuantity = lblEstimatedNotional.Text
                    '        Case "N", "NO"
                    '            orderQuantity = "0"
                    '    End Select
                    'Else
                    '    orderQuantity = lblNotionalPopUpValue.Text
                    'End If


                    If rdbQuantity.Checked Then
                        orderQuantity = Replace(txtOrderqtyEQO.Text, ",", "")
                    Else
                        orderQuantity = Replace(txtNotional.Text, ",", "")
                    End If

                    strRMNameforOrderConfirm = ddlRM.SelectedValue
                    'ddlOrderTypePopUpValue.SelectedIndex = 0



                    If ddlOrderTypePopUpValue.SelectedItem.Text.ToUpper.Contains("LIMIT") Then
                        ddlBasketSharesPopValue.Visible = True
                        txtLimitPricePopUpValue.Width = New WebControls.Unit(75)
                        ddlBasketSharesPopValue.Width = New WebControls.Unit(100)
                        If ddlBasketSharesPopValue.SelectedIndex = 0 Then
                            strLimitPrice1 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", ""))) '<Avinash G. on 2-May-2014: Val returns wrong value becuase of formatted text value>
                            strLimitPrice2 = ""
                            strLimitPrice3 = ""
                        ElseIf ddlBasketSharesPopValue.SelectedIndex = 1 Then
                            strLimitPrice1 = ""
                            strLimitPrice2 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", ""))) '<Avinash G. on 2-May-2014: Val returns wrong value becuase of formatted text value>
                            strLimitPrice3 = ""
                        ElseIf ddlBasketSharesPopValue.SelectedIndex = 2 Then
                            strLimitPrice1 = ""
                            strLimitPrice2 = ""
                            strLimitPrice3 = CStr(Val(txtLimitPricePopUpValue.Text.Replace(",", ""))) '<Avinash G. on 2-May-2014: Val returns wrong value becuase of formatted text value>
                        End If
                        strType = "Limit"
                    Else
                        ddlBasketSharesPopValue.Visible = False
                        '<AvinashG. on 29-Jan-2015: FA-827, handle long RM Names >
                        txtLimitPricePopUpValue.Width = New WebControls.Unit(175)

                        strLimitPrice1 = "" ''set blank limit for market order
                        strLimitPrice2 = ""
                        strLimitPrice3 = ""
                        'txtLimitPriceELN.Enabled = False
                        strType = "Market"
                    End If
            End Select

            '<Rutuja S. on 16-Dec-2014:Added code for saving login Email id and RM email id on order confirm>
            strLoginUserEmail = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)
            If (lblEmail.Text.Trim <> strLoginUserEmail.Trim) And (lblEmail.Text.Trim <> "") Then
                strRMEmailIdforOrderConfirm = strLoginUserEmail & "," & lblEmail.Text
            Else
                strRMEmailIdforOrderConfirm = strLoginUserEmail
            End If
            '</Rutuja S. on 16-Dec-2014:Added code for saving login Email id and RM email id on order confirm>
            ''<Added by Rushikesh on 17-Sep-16 SCB requirement Pretrade Allocation>
            Dim strPreTradeXml As StringBuilder
            strPreTradeXml = savePretradeAllocation(strId)
            ''If savePretradeAllocation(strId) Then
            ''Else
            ''    Exit Sub
            ''End If
            ''<Added by Rushikesh on 17-Sep-16 SCB requirement Pretrade Allocation>


            Dim count As Integer = 0
            Dim sOrderComment As String = ""
            sOrderComment = txtOrderCmt.Text.Trim
            Select Case objELNRFQ.web_Get_orderPlaced_with_Margin_Price_Yield(orderQuantity.Replace(",", ""), strType, strLimitPrice1, strLimitPrice2, strLimitPrice3, _
                                                                              strId, "0", "0", LoginInfoGV.Login_Info.LoginId, sOrderComment, strMargin, strClientPrice, _
                                                                              strClientYield, strBookingBranch, _
                                                                              strRMNameforOrderConfirm, strRMEmailIdforOrderConfirm, "", strPreTradeXml.ToString) ''<Nikhil M. on 16-Sep-2016:Added Comment for Deal Confirmation Reason  >
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful

                    ''KBM on 19-Feb-2014 message moved here to show after trade proceed YN update
                    ' lblMsg.Text = "Deal for RFQ " & strId & " is placed."
                    lblerror.Text = "Order requested for RFQ " & strId
                    lblerror.ForeColor = Color.Blue

                    ''KBM on 19-Feb-2014 to do interim visible stop
                    '<AvinashG. on 27-Mar-2014: >
                    '<AvinashG. on 01-Apr-2014: Disable other controls>
                    ShowHideConfirmationPopup(False)
                    ''DealConfirmPopup.Visible = False
                    ''UPanle11111.Update()
                    '</AvinashG. on 01-Apr-2014: Disable other controls>

                    ''temp.added for filling order grid.
                    If rbHistory.SelectedValue = "Order History" Then
                        fill_OrderGrid()
                        makeThisGridVisible(grdOrder)
                        'ColumnVisibility()
                        upnlGrid.Update()
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
            End Select
            btnBNPPPrice.Text = "Price"
            btnBNPPDeal.CssClass = "btnDisabled"
            btnBNPPDeal.Visible = False
            '<ashwiniP Start 21Sept16>
            lblTotalAmt.Visible = False
            lblTotalAmtVal.Visible = False
            lblAlloAmt.Visible = False
            lblAlloAmtVal.Visible = False
            lblRemainNotional.Visible = False
            lblRemainNotionalVal.Visible = False
            '<ashwiniP End>


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
            ''lblerror.Text = "isWeekendDay:Error occurred in processing."
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
            '' lblerror.Text = "addDaysToDate:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "addDaysToDate", ErrorLevel.High)
        End Try
    End Function


#End Region

#Region "Timer,Button Visibility"



#End Region


#Region "Grid Data Bound,PageIndex,sort"

    Private Sub grdOrder_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdOrder.ItemCommand
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                If e.CommandName.ToUpper = "GETORDERDETAILS" Then
                    ShowHideDeatils(True)
                    lblDetails.Text = "Order Details"
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
                    lblAlloSettlCcy.Text = ""
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
                    'lblAlloYield.Text = ""
                    lblValQuoteStatus.Text = ""
                    lblAlloOrderStatus.Text = ""
                    lblAlloExePrc1.Text = ""
                    lblAlloAvgExePrc.Text = ""
                    lblAlloSpot.Text = ""
                    lblValAlloSolvefor.Text = ""
                    lblClientSide.Text = ""
                    lblFixingType.Text = ""
                    lblOptionType.Text = ""
                    lblAlloKOType.Text = ""
                    lblSettlementMethod.Text = ""
                    ''</added by Rushikesh on 29-Dec-15 to flush old value>
                    lblAlloRFQID.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.RFQ_ID).Text
                    lclAlloCP.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Provider).Text
                    trClientPremium.Visible = True
                    lblAlloClientPrice.Text = SetNumberFormat(grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Client_Price).Text, 2)
                    lblAlloExpiDt.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Expiry_Date).Text
                    'Mohit 28-Dec-2015
                    If grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Barrier).Text.Trim <> "" And grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Barrier).Text.Trim <> "&nbsp;" And grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Barrier).Text.Trim <> "0" Then
                        lblAlloKO.Text = SetNumberFormat(grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Barrier).Text, 2)
                        lblAlloKOType.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.BarrType).Text 'Mohit Lalwani on 13-4-2016 
                    Else

                    End If
                    lblAlloMatuDt.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Maturity_Date).Text
                    lblAlloNoteCcy.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Ccy_DataField).Text
                    lblAlloSettlCcy.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Quanto_Currency).Text
                    If grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ER_EQO_Quantity_Type).Text = "SHARES" Then
                        Label21.Text = "No. of Shares"
                    Else
                        Label21.Text = "Notional"
                    End If
                    Label24.Text = "Issuer Order Remark"
                    lblAlloOrderSize.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Ordered_Qty).Text
                    lblalloOrderType.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Order_Type).Text
                    lblAlloPrice.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Premium).Text
                    lblAlloRemark.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Order_Remark).Text
                    lblAlloSettDt.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Settlement_Date).Text
                    lblAlloSettWk.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Value).Text            'Mohit Lalwani on 13-Apr-2016
                    ''lblAlloSpot.Text = item("").Text '' Need to discuss.
                    lblAlloStrike.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Strike).Text
                    lblAlloSubmitteddBy.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.created_by).Text     'Mohit Lalwnai on 11-Jul-2016
                    lblAlloTenor.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Tenor).Text
                    lblAlloTradeDt.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Trade_Date).Text
                    lblAlloUnderlying.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Underlying).Text
                    lblAlloUpfront.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Margin).Text          'Mohit Lalwani on 13-Apr-2016
                    '  lblAlloYield.Text = SetNumberFormat(grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.EP_Client_Yield).Text, 2)
                    lblAlloOrderStatus.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Order_Status).Text
                    'lblAlloOrderStatus.Text = item("Order_Status").Text
                    lblAlloExePrc1.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Exec_Prc1).Text
                    lblAlloAvgExePrc.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Avg_Exec_Prc).Text
                    lblValAlloSolvefor.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.SolveFor).Text
                    lblClientSide.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Client_side).Text
                    lblFixingType.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.ExerciseType).Text
                    lblOptionType.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Option_Type).Text
                    If grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Order_Type).Text.ToUpper = "LIMIT" Then
                        lblAlloSpot.Visible = True
                        lblAlloSpot.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.Limit_Prc1).Text
                    Else
                        lblAlloSpot.Visible = False
                    End If
                    lblAlloStrikeType.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.StrikeType).Text
                    lblSettlementMethod.Text = grdOrder.Items(e.Item.ItemIndex).Cells(grdOrderEnum.SettlementType).Text
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    upnlDetails.Update()
                End If
            End If
        Catch ex As Exception
            lblerror.Text = "grdOrder_ItemCommand:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdOrder_ItemCommand", ErrorLevel.High)
        End Try
    End Sub


    Private Sub grdOrder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdOrder.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.EditItem Then
                '' If e.Item.Cells(5).Text <> "&nbsp;" Then
                If e.Item.Cells(grdOrderEnum.Ordered_Qty).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Ordered_Qty).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Ordered_Qty).Text, 2).Replace(".00", "") 'Mohit Lalwani on 19-Apr-2016
                End If

                If e.Item.Cells(grdOrderEnum.Premium).Text <> "&nbsp;" And e.Item.Cells(grdOrderEnum.Premium).Text <> "" Then
                    'e.Item.Cells(grdOrderEnum.Premium).Text = SetNumberFormat(CDbl(e.Item.Cells(grdOrderEnum.Premium).Text) * 100, 4) ''Commented by rushikesh D. on 26-April-16
                    e.Item.Cells(grdOrderEnum.Premium).Text = SetNumberFormat(CDbl(e.Item.Cells(grdOrderEnum.Premium).Text), 4)
                End If

                ''If e.Item.Cells(6).Text <> "&nbsp;" Then
                If e.Item.Cells(grdOrderEnum.Filled_Qty).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Filled_Qty).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Filled_Qty).Text, 2).Replace(".00", "") 'Mohit Lalwani on 19-Apr-2016
                End If

                If e.Item.Cells(grdOrderEnum.Avg_Exec_Prc).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Avg_Exec_Prc).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Avg_Exec_Prc).Text, 4)
                End If

                ''If e.Item.Cells(10).Text <> "&nbsp;" Then
                If e.Item.Cells(grdOrderEnum.Premium).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Premium).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Premium).Text, 4)
                End If

                '' If e.Item.Cells(11).Text <> "&nbsp;" Then
                If e.Item.Cells(grdOrderEnum.Strike).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Strike).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Strike).Text, 2)
                End If


                If e.Item.Cells(grdOrderEnum.Exec_Prc1).Text = "0" Then
                    e.Item.Cells(grdOrderEnum.Exec_Prc1).Text = ""
                End If
                If e.Item.Cells(grdOrderEnum.Exec_Prc2).Text = "0" Then
                    e.Item.Cells(grdOrderEnum.Exec_Prc2).Text = ""
                End If
                If e.Item.Cells(grdOrderEnum.Exec_Prc3).Text = "0" Then
                    e.Item.Cells(grdOrderEnum.Exec_Prc3).Text = ""
                End If
                '<Added by Mohit Lalwnai on 23-Nov-215>
                If e.Item.Cells(grdOrderEnum.Client_Price).Text <> "&nbsp;" Then
                    e.Item.Cells(grdOrderEnum.Client_Price).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Client_Price).Text, 4)
                End If
                '<Added by Mohit Lalwnai on 23-Nov-215>
                Select Case tabContainer.ActiveTabIndex
                    Case prdTab.EQO
                        'If schemeCode = "0" Then
                        'If e.Item.Cells(grdOrderEnum.Notional_Amount).Text <> "&nbsp;" Then
                        '    e.Item.Cells(grdOrderEnum.Notional_Amount).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Notional_Amount).Text, 2)
                        'End If

                        If e.Item.Cells(grdOrderEnum.Limit_Prc1).Text <> "&nbsp;" Then
                            e.Item.Cells(grdOrderEnum.Limit_Prc1).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Limit_Prc1).Text, 4) ''16April:Changed from 2 digit to 4 digit decimal,told by Kalyan
                        End If

                        'If e.Item.Cells(grdOrderEnum.Exec_Prc1).Text <> "&nbsp;" Then
                        '    e.Item.Cells(grdOrderEnum.Exec_Prc1).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Exec_Prc1).Text, 4) ''16April:Changed from 2 digit to 4 digit decimal,told by Kalyan
                        'End If
                        'If e.Item.Cells(grdOrderEnum.Client_Price).Text <> "&nbsp;" Then
                        '    e.Item.Cells(grdOrderEnum.Client_Price).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Client_Price).Text, 2)
                        'End If
                        'If e.Item.Cells(grdOrderEnum.EP_Client_Yield).Text <> "&nbsp;" Then
                        '    e.Item.Cells(grdOrderEnum.EP_Client_Yield).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_Client_Yield).Text, 2)
                        'End If
                        If e.Item.Cells(grdOrderEnum.Margin).Text <> "&nbsp;" Then
                            e.Item.Cells(grdOrderEnum.Margin).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Margin).Text, 2)
                        End If

                End Select
                ''<Rutuja 16April:Added for column formatting>
                If e.Item.Cells(grdOrderEnum.Avg_Exec_Prc).Text <> "&nbsp;" Then
                    '<AvinashG. on 15-Oct-2014: FA680 Precision of Execution price, Average execution and Limit price need to be changed on Order log, Blotter, Order cancel monitor and on combined log >
                    e.Item.Cells(grdOrderEnum.Avg_Exec_Prc).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.Avg_Exec_Prc).Text, 4)
                    'e.Item.Cells(grdOrderEnum.EP_AveragePrice).Text = SetNumberFormat(e.Item.Cells(grdOrderEnum.EP_AveragePrice).Text, 2)
                    '</AvinashG. on 15-Oct-2014: FA679 Precision of Execution price, Average execution and Limit price need to be changed on Order log, Blotter, Order cancel monitor and on combined log >
                End If
                ''</Rutuja 16April:Added for column formatting>

                If e.Item.Cells(grdOrderEnum.Order_Requested_At).Text <> "&nbsp;" Or e.Item.Cells(grdOrderEnum.Order_Requested_At).Text <> "" Then
                    e.Item.Cells(grdOrderEnum.Order_Requested_At).Text = Format(CDate(e.Item.Cells(grdOrderEnum.Order_Requested_At).Text), "dd-MMM-yyyy hh:mm:ss tt")
                End If


            End If


            '
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = Exception(ex)
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
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = Exception(ex)
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
                '<AvinashG. on 12-Mar-2015:     FA-775 Download and display TR DSS data on pricer page >
                If rblShareData.SelectedValue = "GRAPHDATA" Then
                    Call Fill_All_Charts()
                End If
                'Call Fill_All_Charts()
                '</AvinashG. on 12-Mar-2015:     FA-775 Download and display TR DSS data on pricer page >
            End If

            upnlGrid.Update()
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
            lblerror.ForeColor = Color.Red
            Select Case tabContainer.ActiveTabIndex

                Case prdTab.EQO
                    '' EQO tab
                    ''TODO

                    Dim dtShareEQO1 As DataTable = CType(Session.Item("Share_ValueEQO1"), DataTable)
                    'If checkCodefromAltcode(ddlShareEQO.SelectedItem.Value, dtShareEQO1) Then
                    '    Chk_validation = True
                    'Else
                    '    lblerror.Text = "Underlying not supported."
                    '    Chk_validation = False
                    '    Exit Function
                    'End If

                    If chkAddShare1.Checked Then
                        ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                        'If ddlShareDRA.SelectedItem Is Nothing Then
                        If ddlShareEQO.SelectedValue Is Nothing Then
                            lblerror.Text = "Please select valid share. "
                            Chk_validation = False
                            Exit Function
                        ElseIf ddlShareEQO.SelectedValue = "" Then
                            lblerror.Text = "Please select valid share."
                            Chk_validation = False
                            Exit Function
                        Else
                            Chk_validation = True
                        End If
                    End If
                    If chkAddShare2.Checked Then
                        ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                        'If ddlShareDRA2.SelectedItem Is Nothing Then
                        If ddlShareEQO2.SelectedValue Is Nothing Then
                            lblerror.Text = "Please select valid share. "
                            Chk_validation = False
                            Exit Function
                        ElseIf ddlShareEQO2.SelectedValue = "" Then
                            lblerror.Text = "Please select valid share."
                            Chk_validation = False
                            Exit Function
                        Else
                            Chk_validation = True
                        End If
                    End If

                    If chkAddShare3.Checked Then
                        ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                        'If ddlShareDRA3.SelectedItem Is Nothing Then
                        If ddlShareEQO3.SelectedValue Is Nothing Then
                            lblerror.Text = "Please select valid share. "
                            Chk_validation = False
                            Exit Function
                        ElseIf ddlShareEQO3.SelectedValue = "" Then
                            lblerror.Text = "Please select valid share."
                            Chk_validation = False
                            Exit Function
                        Else
                            Chk_validation = True
                        End If
                    End If

                    '  If Val(txtUpfrontEQO.Text) = 0 OrElse Val(txtUpfrontEQO.Text.Replace(",", "")) >= 100 Then
                    If Val(txtUpfrontEQO.Text) <= 0 OrElse Val(txtUpfrontEQO.Text.Replace(",", "")) >= 100 Then 'Changed by Mohit Lalwani on 29_Jun-2016
                        lblerror.Text = "Please enter valid upfront."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If
                    If txtSettlDateEQO.Value.Trim = "" Then
                        lblerror.Text = "Please enter valid Settlement date."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If

                    If txtExpiryDateEQO.Value.Trim = "" Then
                        lblerror.Text = "Please enter valid Expiry date."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                        '' Throw ErrorCodeProvider.GetErrorDictionary(ErrorCodeProvider.ExceptionName.InvalidExpirySettlementDate)
                    End If
                    If txtMaturityDateEQO.Value.Trim = "" Then
                        lblerror.Text = "Please enter valid Maturity date."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If
                    If txtSettlDateEQO.Value.Trim = "" Or CDate(txtSettlDateEQO.Value) <= CDate(txtTradeDateEQO.Value) Then
                        lblerror.Text = "Please enter valid Settlement date."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If

                    If txtExpiryDateEQO.Value.Trim = "" Or CDate(txtExpiryDateEQO.Value) <= CDate(txtSettlDateEQO.Value) Then
                        lblerror.Text = "Please enter valid Expiry date."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                        '' Throw ErrorCodeProvider.GetErrorDictionary(ErrorCodeProvider.ExceptionName.InvalidExpirySettlementDate)
                    End If
                    If txtMaturityDateEQO.Value.Trim = "" Or CDate(txtMaturityDateEQO.Value) <= CDate(txtExpiryDateEQO.Value) Then
                        lblerror.Text = "Please enter valid Maturity date."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If


                    'If CDate(txtSettlementDate.Value) <= CDate(txtTradeDate.Value) Then
                    '    lblerror.Text = "Please enter valid Settlement date."
                    '    Chk_validation = False
                    '    Exit Function
                    'Else
                    '    Chk_validation = True
                    'End If

                    'If CDate(txtExpiryDate.Value) <= CDate(txtSettlementDate.Value) Then
                    '    lblerror.Text = "Please enter valid Expiry date."
                    '    Chk_validation = False
                    '    Exit Function
                    'Else
                    '    Chk_validation = True
                    '    '' Throw ErrorCodeProvider.GetErrorDictionary(ErrorCodeProvider.ExceptionName.InvalidExpirySettlementDate)
                    'End If
                    'If CDate(txtMaturityDate.Value) <= CDate(txtExpiryDate.Value) Then
                    '    lblerror.Text = "Please enter valid Maturity date."
                    '    Chk_validation = False
                    '    Exit Function
                    'Else
                    '    Chk_validation = True
                    'End If




                    If Val(txtUpfrontEQO.Text) > 0 Or Val(txtUpfrontEQO.Text) < 100 Then
                        Chk_validation = True
                    Else
                        lblerror.Text = "Please enter a valid Upfront."
                        Chk_validation = False
                        Exit Function
                    End If

                    If Val(txtTenorEQO.Text) > 0 Then
                        Chk_validation = True
                    Else
                        lblerror.Text = "Please enter a valid Tenor."
                        Chk_validation = False
                        Exit Function
                    End If

                    ''AshwiniP on 09-Nov-2016
                    If ValidateTenor() = False Then
                        lblerror.Text = "Please enter valid tenor."
                        Chk_validation = False
                        Exit Function
                    Else
                        Chk_validation = True
                    End If

                    If Val(txtOrderqtyEQO.Text) >= 0 Then ''<Added by Rushikesh on 7-April-16 for zero notional >
                        Chk_validation = True
                    Else
                        lblerror.Text = "Please enter valid No. Of Shares."
                        '<added by Mohit Lalwani on 16-Nov-2015>
                        txtNotional.Text = ""
                        '</added by Mohit Lalwani on 16-Nov-2015>
                        Chk_validation = False
                        Exit Function
                    End If


                    '' Value of Strike - 1 not empty 2 RANGE WITHIN ?
                    If ddlOptionType.SelectedValue.ToUpper = "KNOCKIN PUT" Then

                        If ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM" Then
                            If Val(txtStrikeEQO.Text) > 0 Then
                                Chk_validation = True
                            Else
                                lblerror.Text = "Please enter a valid Strike."
                                Chk_validation = False
                                Exit Function
                            End If

                            If Val(txtBarrierLevelEQO.Text) > 0 Then
                                Chk_validation = True
                            Else
                                lblerror.Text = "Please enter a valid Barrier Level."
                                Chk_validation = False
                                Exit Function
                            End If

                        ElseIf ddlSolveforEQO.SelectedValue.ToUpper = "STRIKE" Then
                            If Val(txtBarrierLevelEQO.Text) > 0 Then
                                Chk_validation = True
                            Else
                                lblerror.Text = "Please enter a valid Barrier Level."
                                Chk_validation = False
                                Exit Function
                            End If

                            If Val(txtPremium.Text) > 0 Then
                                Chk_validation = True
                            Else
                                lblerror.Text = "Please enter a valid Premium."
                                Chk_validation = False
                                Exit Function
                            End If

                        ElseIf ddlSolveforEQO.SelectedValue.ToUpper = "BARRIER" Then
                            If Val(txtStrikeEQO.Text) > 0 Then
                                Chk_validation = True
                            Else
                                lblerror.Text = "Please enter a valid Strike."
                                Chk_validation = False
                                Exit Function
                            End If

                            If Val(txtPremium.Text) > 0 Then
                                Chk_validation = True
                            Else
                                lblerror.Text = "Please enter a valid Premium."
                                Chk_validation = False
                                Exit Function
                            End If

                        End If

                    Else '' OptionType = European Call/Put or American Call/Put
                        If ddlSolveforEQO.SelectedValue.ToUpper = "STRIKE" Then
                            If txtPremium.Text <> "" And Val(txtPremium.Text) > 0 Then
                                Chk_validation = True
                            Else
                                lblerror.Text = "Please enter a valid Premium."
                                Chk_validation = False
                                Exit Function
                            End If
                        ElseIf ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM" Then
                            If txtStrikeEQO.Text <> "" And Val(txtStrikeEQO.Text) > 0 Then
                                Chk_validation = True
                            Else
                                lblerror.Text = "Please enter a valid Strike."
                                Chk_validation = False
                                Exit Function
                            End If
                        End If
                    End If


                    Dim dblITM_TOLERANCE_SOLD_OPTIONS = CDbl(objReadConfig.ReadConfig(dsConfig, "EQO_ITM_TOLERANCE_SOLD_OPTIONS", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "-1"))

                    If dblITM_TOLERANCE_SOLD_OPTIONS < 0 Then
                        Chk_validation = True
                    Else
                        If ddlOptionType.SelectedValue.ToUpper.Contains("CALL") And ddlSideEQO.SelectedValue.Trim.ToUpper = "SELL" Then
                            If ddlStrikeTypeEQO.SelectedValue.ToUpper = "ABSOLUTE" Then
                                Dim dblCalcAllowedStrikeCalcSpot As Double
                                dblCalcAllowedStrikeCalcSpot = CDbl(lblSpotValue.Text) * (1 - (dblITM_TOLERANCE_SOLD_OPTIONS / 100))
                                If CDbl(txtStrikeEQO.Text) >= dblCalcAllowedStrikeCalcSpot Then
                                    Chk_validation = True
                                Else
                                    lblerror.Text = "Strike should be greater than or equal to " + FormatNumber(dblCalcAllowedStrikeCalcSpot.ToString, 2)
                                    Chk_validation = False
                                    Exit Function
                                End If
                            ElseIf ddlStrikeTypeEQO.SelectedValue.ToUpper = "PERCENTAGE" Then
                                Dim dblCalcAllowedStrikeVal As Double
                                dblCalcAllowedStrikeVal = 100 - dblITM_TOLERANCE_SOLD_OPTIONS
                                If CDbl(txtStrikeEQO.Text.Replace(",", "")) >= dblCalcAllowedStrikeVal Then
                                    Chk_validation = True
                                Else
                                    lblerror.Text = "Strike(%) should be greater than or equal to " + FormatNumber(dblCalcAllowedStrikeVal.ToString, 2)
                                    Chk_validation = False
                                    Exit Function
                                End If
                            End If
                        ElseIf ddlOptionType.SelectedValue.ToUpper.Contains("PUT") And ddlSideEQO.SelectedValue.Trim.ToUpper = "SELL" Then
                            If ddlStrikeTypeEQO.SelectedValue.ToUpper = "ABSOLUTE" Then
                                Dim dblCalcAllowedStrikeCalcSpot As Double
                                dblCalcAllowedStrikeCalcSpot = CDbl(lblSpotValue.Text) * (1 + (dblITM_TOLERANCE_SOLD_OPTIONS / 100))
                                If CDbl(txtStrikeEQO.Text) <= dblCalcAllowedStrikeCalcSpot Then
                                    Chk_validation = True
                                Else
                                    lblerror.Text = "Strike should be less than or equal to " + FormatNumber(dblCalcAllowedStrikeCalcSpot.ToString, 2)
                                    Chk_validation = False
                                    Exit Function
                                End If
                            ElseIf ddlStrikeTypeEQO.SelectedValue.ToUpper = "PERCENTAGE" Then
                                Dim dblCalcAllowedStrikeVal As Double
                                dblCalcAllowedStrikeVal = 100 - dblITM_TOLERANCE_SOLD_OPTIONS
                                If CDbl(txtStrikeEQO.Text.Replace(",", "")) <= dblCalcAllowedStrikeVal Then
                                    Chk_validation = True
                                Else
                                    lblerror.Text = "Strike(%) should be less than or equal to " + FormatNumber(dblCalcAllowedStrikeVal.ToString, 2)
                                    Chk_validation = False
                                    Exit Function
                                End If
                            Else
                                Chk_validation = False
                                Exit Function
                            End If
                        End If
                    End If
                    '''02Nov2016 Dilkhush Added validation before price to check if notional or share is valid or not
                    If rdbQuantity.Checked Then
                        If Qty_Validate(txtOrderqtyEQO.Text) = False Then
                            Chk_validation = False
                            lblerror.ForeColor = Color.Red
                            lblerror.Text = "Please enter valid No. Of Shares."
                            Exit Function
                        End If
                    Else
                        If Qty_Validate(txtNotional.Text) = False Then
                            Chk_validation = False
                            lblerror.ForeColor = Color.Red
                            lblerror.Text = "Please enter valid notional."
                            Exit Function
                        End If
                    End If

            End Select
            'Chk_validation = True
        Catch ex As Exception
            lblerror.Text = "Chk_validation:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Chk_validation", ErrorLevel.High)
            Throw ex

        End Try
    End Function


    'Added by sneha on 27-Mar-2014


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
    ''Single Solve RFQ 
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

            '<RushikeshD. on 07-April-2016: Optimization of rfq saving>
            If tabContainer.ActiveTabIndex = prdTab.EQO Then
                ''Get_EQORFQData_TOXML(PP_ID)
                FillRFQDataTable(PP_ID)
            End If
            '<RushikeshD. on 07-April-2016: Optimization of rfq saving>


            Quote_ID = Convert.ToString(Session("Quote_ID"))
            lblerror.Text = "RFQ " & Quote_ID & "  generated."
            lblerror.ForeColor = Color.Blue

            lblMsgPriceProvider.Text = "" ''KBM on 13-Feb-2014, cleared text message

            ''<Imran 7April:Added for Addeding pp's latest generated Id>
            If (PP_CODE = "BNPP") Then
                strJavaScripthdnSolveSingleRequest.AppendLine("document.getElementById('BNPPwait').style.visibility = 'visible';")
                Session.Add("BNPPQuote", Quote_ID)
            End If
            ''</Imran 7April:Added for Addeding pp's latest generated Id>

            Select Case tabContainer.ActiveTabIndex

                Case prdTab.EQO
                    If ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM" Then
                        'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "JSPrice", "getPrice('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');", True)
                        strJavaScripthdnSolveSingleRequest.AppendLine("getPremium('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');")

                    Else
                        'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "JSStrike", "getStrike('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');", True)
                        strJavaScripthdnSolveSingleRequest.AppendLine("getBarrier('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');")
                    End If
            End Select
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScripthdnSolveSingleRequest", strJavaScripthdnSolveSingleRequest.ToString, True)

        Catch ex As Exception
            lblerror.Text = "solveRFQ:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "solveRFQ", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub btnhdnSolveSingleRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnhdnSolveSingleRequest.Click
        Try

            ''''TmRefresh.Enabled = True 'KBM on 15-Feb-2014
            Session.Remove("flag")
            Session("flag") = ""

            ''KBM on 24-Feb-2014 On click clear all fields
            setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice()
            rbHistory.SelectedValue = "Quote History"
            fill_EQO_Grid()
            makeThisGridVisible(grdEQORFQ)                            '<RiddhiS. on 16-Oct-2016: To shift to Quote History on Single Price>


            'Dim strTradeDate As String = txtTradeDate.Value
            Session.Add("TradeDAte", strTradeDate)
            If Chk_validation() = False Then
                Exit Sub
            End If
            If Session("hdnPP").ToString = "BNPP" Then
                AllHiddenPrice.Value = "Disable;Disable"
                btnBNPPPrice.Enabled = False
                ' btnBNPPDeal.Enabled = False
                btnBNPPPrice.CssClass = "btnDisabled"
                ' btnBNPPDeal.CssClass = "btnDisabled"
                btnSolveAll.Enabled = False
                btnSolveAll.CssClass = "btnDisabled"
                lblBNPPPrice.Text = ""
                lblBNPPlimit.Text = "" 'Mohit Lalwani on 1-Feb-2016
                lblerror.Text = ""
                BNPPHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setBNPPLoader", "document.getElementById('BNPPwait').style.visibility = 'visible';", True)

                solveRFQ("BNPP", lblTimerBNPP, btnBNPPDeal, btnBNPPPrice, lblBNPPPrice)
                'ElseIf Session("hdnPP").ToString = "HSBC" Then
                '    ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setHSBCLoader", "document.getElementById('HSBCwait').style.visibility = 'visible';", True)

                '    AllHiddenPrice.Value = "Disable;Disable"
                '    btnHSBCPrice.Enabled = False
                '    'btnHSBCDeal.Enabled = False
                '    btnHSBCPrice.CssClass = "btnDisabled"
                '    'btnHSBCDeal.CssClass = "btnDisabled"
                '    btnSolveAll.Enabled = False
                '    btnSolveAll.CssClass = "btnDisabled"
                '    lblHSBCPrice.Text = ""
                '    lblerror.Text = ""
                '    HsbcHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                '    solveRFQ("HSBC", lblTimerHSBC, btnHSBCDeal, btnHSBCPrice, lblHSBCPrice)

            End If
            btnLoad_Click(sender, e)
            Session.Remove("hdnPP")
            '<AvinashG. on 12-Mar-2015:     FA-775 Download and display TR DSS data on pricer page >
            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Fill_All_Charts()
            End If
            'Fill_All_Charts()
            '<AvinashG. on 12-Mar-2015:     FA-775 Download and display TR DSS data on pricer page >
        Catch ex As Exception
            lblerror.Text = "btnhdnSolveSingleRequest_Click:Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnhdnSolveSingleRequest_Click", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub btnBNPPprice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBNPPPrice.Click
        Try
            '' RestoreSolveAll()
            '' RestoreAll()
            chkConfirmDeal.Visible = False ''<Nikhil M. on 09-Sep-2016:Added >
            chkConfirmDeal.Checked = False ''<Nikhil M. on 09-Sep-2016:Added >
            If btnBNPPPrice.Text <> "Order" Then

                'If tabContainer.ActiveTabIndex = 0 Then
                '    ' ReCalcDate() '<AvinashG. on 30-Apr-2014: To manage Tenor Tabout issue> 
                'End If
                If tabContainer.ActiveTabIndex = prdTab.EQO Then
                    Session.Add("hdnPP", "BNPP")
                Else
                    'Session.Add("hdnPP", "BNPP")
                End If

                btnhdnSolveSingleRequest_Click(sender, e)
                ''End shailesh Pore 22-Jan-2014
            Else
                ''btnBNPPDeal_Click(sender, e)
                ''<Commented by Rushikesh on 7-April-16 for zero notional >

                If rdbQuantity.Checked Then
                    If txtOrderqtyEQO.Text = "" Or txtOrderqtyEQO.Text = "&nbsp;" Or Val(txtOrderqtyEQO.Text) = 0 Then
                        lblerror.ForeColor = Color.Red
                        lblerror.Text = "Can not place Order. Daily no. of shares can not be 0."
                        Exit Sub
                    End If
                Else
                    If txtNotional.Text = "" Or txtNotional.Text = "&nbsp;" Or Val(txtNotional.Text) = 0 Then
                        lblerror.ForeColor = Color.Red
                        lblerror.Text = "Can not place Order. Notional Size can not be 0."
                        Exit Sub
                    End If
                End If


                ''</Commented by Rushikesh on 7-April-16 for zero notional >



                If checkIssuerLimit("BNPP") Then
                    If 1 = 1 Then
                        Set_Order_Pop_Up_Items("BNPP")
                        ShowHideConfirmationPopup(True)
                        fill_RMList()
                    Else
                        btnBNPPDeal_Click(sender, e)
                    End If
                End If
                ''Need to change/confirm
                btnBNPPPrice.Text = "Order"
            End If
            RestoreSolveAll()
            RestoreAll()

        Catch ex As Exception
            ''<err_msg changed to BNPP for quick fix : by Rushikesh D. on 27-July-2015>
            lblerror.Text = "btnBNPPprice_Click:Error occurred in BNPP reprice."
            ''</err_msg changed to BNPP for quick fix : by Rushikesh D. on 27-July-2015>
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnBNPPprice_Click", ErrorLevel.High)
        End Try
    End Sub

    Public Function Set_Order_Pop_Up_Items(ByVal Issuer As String) As Boolean
        Try
            'AvinashG. on 09-Jul-2016
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            lblPopupSCBPPValue.Visible = True ''<Added by Rushikesh on 16-Sep-16 for SCB JPM issuere>


            ''AshwiniP on 23Sept16 
            Session.Remove("dtEQOPreTradeAllocation")
            Dim tempDt As DataTable
            tempDt = New DataTable("tempDt")
            tempDt.Columns.Add("RM_Name", GetType(String))
            'tempDt.Columns.Add("RM_Name", GetType(String))
            tempDt.Columns.Add("Account_Number", GetType(String))
            tempDt.Columns.Add("AlloNotional", GetType(String))
            tempDt.Columns.Add("Cust_ID", GetType(String))
            tempDt.Columns.Add("DocId", GetType(String))
            tempDt.Columns.Add("EPOF_OrderId", GetType(String))
            tempDt.Rows.InsertAt(tempDt.NewRow(), 0)
            Session.Add("dtEQOPreTradeAllocation", tempDt)
            grdRMData.DataSource = tempDt
            grdRMData.DataBind()
            For Each row As GridViewRow In grdRMData.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    row.Cells(0).Controls.OfType(Of CheckBox)().FirstOrDefault().Checked = True
                End If
                OnCheckedChanged(CType(grdRMData.Rows((0)).Cells(0).FindControl("CheckBox1"), CheckBox), Nothing)
'0:
            Next


            lblIssuerPopUpValue.Text = Issuer
            'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowHongKongForOrderPlacement", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            '    Case "Y", "YES"
            '        ddlBookingBranchPopUpValue.Enabled = True
            '    Case "N", "NO"
            '        ddlBookingBranchPopUpValue.Enabled = False
            'End Select
            'Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options

            Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                Case "BOTH"
                    lblNoOfSharePopUpCaption.Visible = True
                    lblNoOfSharePopUpValue.Visible = True
                    lblNotionalPopUpCaption.Visible = True
                    lblNotionalPopUpValue.Visible = True
                    lblCurrencyPopUpValue.Visible = True
                    ''<Nikhil M. on 18-Oct-2016: Added tp set the popup Test>
                    If rdbQuantity.Checked Then
                        lblTotalAmt.Text = "Total No. of shares"
                        lblAlloAmt.Text = "Allocated No. of shares"
                        lblRemainNotional.Text = "Remaining No. of shares"
                    Else
                        lblTotalAmt.Text = "Total Notional"
                        lblAlloAmt.Text = "Allocated Notional"
                        lblRemainNotional.Text = "Remaining Notional"
                    End If


                Case "NOTIONAL"
                    lblNoOfSharePopUpCaption.Visible = False
                    lblNoOfSharePopUpValue.Visible = False
                    lblNotionalPopUpCaption.Visible = True
                    lblNotionalPopUpValue.Visible = True
                    lblCurrencyPopUpValue.Visible = True
                    tdPopUpShares.Style.Add("display", "")
                    tdPopUpSharesValue.Style.Add("display", "")
                    ''<Nikhil M. on 18-Oct-2016: Added tp set the popup Test>
                    lblTotalAmt.Text = "Total Notional"
                    lblAlloAmt.Text = "Allocated Notional"
                    lblRemainNotional.Text = "Remaining Notional"


                Case "NOOFSHARE"
                    lblNoOfSharePopUpCaption.Visible = True
                    lblNoOfSharePopUpValue.Visible = True
                    lblNotionalPopUpCaption.Visible = False
                    lblNotionalPopUpValue.Visible = False
                    lblCurrencyPopUpValue.Visible = False

                    tdPopUpNotional.Style.Add("display", "none")
                    tdPopUpNotionalValue.Style.Add("display", "none")
                    ''<Nikhil M. on 18-Oct-2016: Added tp set the popup Test>
                    lblTotalAmt.Text = "Total No. of shares"
                    lblAlloAmt.Text = "Allocated No. of shares"
                    lblRemainNotional.Text = "Remaining No. of shares"

                    lblTotalAmtVal.Text = lblNoOfSharePopUpValue.Text '' lblNoOfSharePopUpValue.Text ''Nikhil 28Sep2016
                    lblRemainNotionalVal.Text = lblTotalAmtVal.Text  ''Nikhil 28Sep2016
            End Select
            '/Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options
            Select Case tabContainer.ActiveTabIndex

                Case prdTab.EQO
                    ddlBasketSharesPopValue.Visible = False
                    txtLimitPricePopUpValue.Width = New WebControls.Unit(175)
                    lblNotionalPopUpCaption.Text = "Notional"
                    lblUpfrontPopUpCaption.Text = "Upfront (%)"

                    txtUpfrontPopUpValue.Enabled = True

                    lblIssuerPricePopUpCaption.Text = lblSolveForType.Text.ToString
                    lblClientPricePopUpCaption.Text = "Client Premium (%)"
                    'lblClientYieldPopUpCaption.Visible = False
                    ddlOrderTypePopUpValue.SelectedIndex = 0
                    ddlOrderTypePopUpValue.Enabled = True
                    'txtLimitPricePopUpValue.Visible = True  ''Ajay-21-Apr-2015-limit orders are not in EQO 
                    'txtLimitPricePopUpValue.Enabled = False ''Ajay-21-Apr-2015-limit orders are not in EQO 

                    ddlBasketSharesPopValue.Items.Clear()
                    Dim strbasketforddl() As String
                    strbasketforddl = txtBasketShares.Text.Split(CChar(","))
                    For Each share In strbasketforddl
                        ddlBasketSharesPopValue.Items.Add(New DropDownListItem(share, share))
                    Next


                    txtLimitPricePopUpValue.Text = "0"
                    txtLimitPricePopUpValue.Enabled = False '<AvinashG. on 27-Mar-2014: >

                    lblUnderlyingPopUpValue.Text = If(txtBasketShares.Text = "", ddlShareEQO.SelectedValue, txtBasketShares.Text).ToString
                    lblNoOfSharePopUpValue.Text = If(txtOrderqtyEQO.Text = "0", "", txtOrderqtyEQO.Text)
                    If rdbQuantity.Checked Then
                        lblNotionalPopUpValue.Text = ""
                        lblCurrencyPopUpValue.Text = ""
                    Else
                        lblCurrencyPopUpValue.Text = "(" + ddlInvestCcy.SelectedValue + ")" '' IIf(chkQuantoCcy.Checked, ddlQuantoCcy.SelectedItem.Text, lblELNBaseCcy.Text).ToString
                        lblNotionalPopUpValue.Text = txtNotional.Text
                    End If

                    'Taking caption as in EQO strike can be in percentage or absolute
                    lblStrikePopUpCaption.Text = ddlStrikeTypeEQO.SelectedItem.Text

                    lblSettlementPopup.Text = ddlsettlmethod.SelectedItem.Text
                    lblStrikePopUpValue.Text = txtStrikeEQO.Text
                    lblTenorPopUpValue.Text = txtTenorEQO.Text
                    lblTenorTypePopUpValue.Text = ddlTenorEQO.SelectedItem.Text

                    txtUpfrontPopUpValue.Text = txtUpfrontEQO.Text
                    lblIssuerPricePopUpValue.Text = lblBNPPPrice.Text
                    lblClientPricePopUpValue.Text = lblBNPPClientPrice.Text
                    lblOptionPopUpValue.Text = ddlSideEQO.SelectedItem.Text + "&nbsp;" + ddlOptionType.SelectedItem.Text 'Rushikesh
                    'lblClientYieldPopUpValue.Text = lblBNPPClientYield.Text
                    'lblLimitPricePopUpCaption.Visible = False ''Ajay-21-Apr-2015-limit orders are not in EQO 

                    ddlBookingBranchPopUpValue.Enabled = False '<RiddhiS. on 03-Oct-2016: Booking Branch should be disabled and filled on selected CIF/PAN>

                    lblRFQDateValue.Text = txtMaturityDateEQO.Value

                    Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                        Case "BOTH"
                            If rdbQuantity.Checked Then
                                lblTotalAmtVal.Text = lblNoOfSharePopUpValue.Text ''Nikhil 28Sep2016
                                lblRemainNotionalVal.Text = lblTotalAmtVal.Text  ''Nikhil 28Sep2016
                            Else
                                lblTotalAmtVal.Text = lblNotionalPopUpValue.Text ''lblNotionalPopUpValue.Text ''Nikhil 28Sep2016
                                lblRemainNotionalVal.Text = lblTotalAmtVal.Text  ''Nikhil 28Sep2016
                            End If
                        Case "NOTIONAL"
                            lblTotalAmtVal.Text = lblNotionalPopUpValue.Text ''lblNotionalPopUpValue.Text ''Nikhil 28Sep2016
                            lblRemainNotionalVal.Text = lblTotalAmtVal.Text  ''Nikhil 28Sep2016

                        Case "NOOFSHARE"
                            lblTotalAmtVal.Text = lblNoOfSharePopUpValue.Text ''Nikhil 28Sep2016
                            lblRemainNotionalVal.Text = lblTotalAmtVal.Text  ''Nikhil 28Sep2016
                    End Select
                 
                    'AvinashG on 5Nov2015, setting RFQ Date, COde for more issuers to be done
                    Select Case lblIssuerPopUpValue.Text.ToUpper
                        Case "BNPP"
                            lblOrderDateValue.Text = FinIQApp_Date.FinIQDate(CDate(BNPPHiddenMatDate.Value))

                    End Select
                    lblRFQOrderDateMismatchMsg.Text = ""
                    If CDate(lblRFQDateValue.Text) <> CDate(lblOrderDateValue.Text) Then
                        '   lblRFQOrderDateMismatchMsg.Text = "FinIQ's calculated date(" + lblRFQDateValue.Text + ") does not match with LP date(" + lblOrderDateValue.Text + "), do you want to proceed with order using LP date?"
                        lblRFQOrderDateMismatchMsg.Text = "This order to " + lblIssuerPopUpValue.Text.ToUpper + " will be based on Maturity Date " + lblOrderDateValue.Text + ". Click confirm to continue, Cancel to stop."
                        lblRFQOrderDateMismatchMsg.Visible = True
                    Else
                        lblRFQOrderDateMismatchMsg.Visible = False
                    End If
                    'AvinashG on 5Nov2015, setting RFQ Date, COde for more issuers to be done

            End Select

            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False '<AvinashG. on 27-Mar-2014: >

            Return True
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = "Error while setting order pop items:" & ex.Message
            lblerror.Text = "set_Order_Pop_Up_Items:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "set_Order_Pop_UP_Items", ErrorLevel.High)
            Throw ex
            Return False
        End Try

    End Function


    Public Sub Solve_All_Requests(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnhdnSolveAllRequests.Click
        Dim all_RFQ_IDs As String = String.Empty
        Dim dtUpdate As DataTable
        Dim strQuoteId1 As String = String.Empty
        Dim dtPriceProvider As DataTable
        Dim strJavaScriptAllRequest As New StringBuilder
        Try
            'Imran comment all RegisterStartupScript and create single java script for all. 6-June-14
            If Chk_validation() = False Then
                Exit Sub
            End If

            ResetMinMaxNotional()

            'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setPriceAllWaitLoader", "document.getElementById('PriceAllWait').style.visibility = 'visible';", True)
            strJavaScriptAllRequest.AppendLine("document.getElementById('PriceAllWait').style.visibility = 'visible';")


            getAllId = New Hashtable
            getPPId = New Hashtable

            'stop_timer() ''Commented by Imran /Rutuja 25-June-14
            clearFields()

            flag = "I"
            Session.Add("flag", flag)
            dtUpdate = New DataTable("Dummy")
            rbHistory.SelectedValue = "Quote History"
            'Dim strTradeDate As String = txtTradeDate.Value
            'Session.Add("TradeDAte", strTradeDate)

            Dim dtLogin As DataTable
            Dim dr As DataRow()
            Dim dr1 As DataRow()
            dtLogin = CType(Session("PP_Login"), DataTable)
            dtPriceProvider = New DataTable("Price Provider")
            dtPriceProvider = CType(Session("Provide_Id"), DataTable) ''contain info.about all available PP_CODE and PP_ID
            '<AvinashG. on 25-Aug-2014: FA-516 SolveAll gives error is single LP is present on screen >

            If dtLogin.Rows.Count > 0 Then
                'If dtLogin.Rows.Count > 1 Then
                '</AvinashG. on 25-Aug-2014: FA-516 SolveAll gives error is single LP is present on screen >
                ''--BNPP price
                dr = dtLogin.Select("PP_CODE = '" & "BNPP" & "' ")
                If dr.Length > 0 Then
                    If btnBNPPPrice.Visible = True Then
                        If btnBNPPPrice.Enabled = True Then
                            ' If chkBNPP.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Commented for CheckBox>


                                dr1 = dtPriceProvider.Select("PP_CODE = '" & "BNPP" & "' ")
                                BNPP_ID = dr1(0).Item("PP_ID").ToString
                                '<AvinashG. on 16-Feb-2016: Optimization>
                                FillRFQDataTable(BNPP_ID)
                                'Get_RFQData_TOXML(BNPP_ID)
                                ''</AvinashG. on 16-Feb-2016: Optimization>
                                Quote_ID = Convert.ToString(Session("Quote_ID"))
                                If ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM" Then
                                    'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "JSPrice", "getPrice('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');", True)
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('BNPPwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getPremium('" + Quote_ID + "','" + lblBNPPPrice.ClientID + "','" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "','" + btnBNPPPrice.ClientID + "');")
                                    BNPPHiddenPrice.Value = ";Disable;Disable;Enable;Price"
                                Else
                                    'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "JSStrike", "getStrike('" + Quote_ID + "','" + lblPrice.ClientID + "','" + lblTimer.ClientID + "','" + btnDeal.ClientID + "','" + btnPrice.ClientID + "');", True)
                                    strJavaScriptAllRequest.AppendLine("document.getElementById('BNPPwait').style.visibility = 'visible';")
                                    strJavaScriptAllRequest.AppendLine("getBarrier('" + Quote_ID + "','" + lblBNPPPrice.ClientID + "','" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "','" + btnBNPPPrice.ClientID + "');")
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
                ''--


            End If

            Session.Add("All_IDs", all_RFQ_IDs)
            Session.Add("Hash_Values", getAllId)  ''putting RFQ id's in hash table
            Session.Add("PP_IdTable", getPPId)     ''putting all pp id's in hash table

            lblerror.Text = "RFQs " & all_RFQ_IDs & " are generated."
            lblerror.ForeColor = Color.Blue
            lblMsgPriceProvider.Text = "" ''KBM on 13-Feb-2014, cleared text message
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "strJavaScriptAllRequest", "try { " + strJavaScriptAllRequest.ToString + "} catch(e) {}", True) 'Added by Mohit Lalwani on 17-Oct-2016
            '<AvinashG. on 25-Aug-2014: FA-516 SolveAll gives error is single LP is present on screen , ADD ON Validation of If Condition>
            If all_RFQ_IDs <> "" Then
                objELNRFQ.DB_update_quoteID(all_RFQ_IDs, strQuoteId1, dtUpdate)  ''update a group of quote id's with one id in db
            End If
            'objELNRFQ.DB_update_quoteID(all_RFQ_IDs, strQuoteId1, dtUpdate)  ''update a group of quote id's with one id in db
            '</AvinashG. on 25-Aug-2014: FA-516 SolveAll gives error is single LP is present on screen , ADD ON Validation of If Condition>
            '<AvinashG. on 12-Mar-2015:     FA-775 Download and display TR DSS data on pricer page >
            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Call Fill_All_Charts()
            End If
            '</AvinashG. on 12-Mar-2015:     FA-775 Download and display TR DSS data on pricer page >
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = "Solve_All_Requests:Error occurred in processing:" & ex.Message
            lblerror.Text = "Solve_All_Requests:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Solve_All_Requests", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub btnSolveAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSolveAll.Click

        Try

            If txtTradeDateEQO.Value = "" Or txtMaturityDateEQO.Value = "" Or txtExpiryDateEQO.Value = "" Or txtSettlDateEQO.Value = "" Then
                lblerror.Text = "Please enter valid date."
                Exit Sub
            End If
            'If chkBNPP.Checked = True Then''<Nikhil M. on 17-Sep-2016: Commented >
                TRBNPP1.Attributes.Remove("class")
            'End If

            hideShowRBLShareData()
            Session.Remove("TradeDAte")
            Session.Remove("MaturityDAte")
            Session.Remove("expiryDAte")
            Session.Remove("Settlementdate")
            Session.Add("TradeDAte", txtTradeDateEQO.Value)
            Session.Add("MaturityDAte", txtMaturityDateEQO.Value)
            Session.Add("expiryDAte", txtExpiryDateEQO.Value)
            Session.Add("Settlementdate", txtSettlDateEQO.Value)
            If (btnBNPPPrice.Visible = False Or btnBNPPPrice.Enabled = False) Then
                lblerror.Text = "All price providers are Off. Please try again later."
                Exit Sub
            End If

            AllHiddenPrice.Value = "Disable;Enable"
            RestoreSolveAll()
            RestoreAll()

            'If chkBNPP.Checked = True Then ''<Nikhil M. on 17-Sep-2016: Commented>
                BNPPHiddenPrice.Value = ";Disable;Disable;Disable;Price"
            'End If

            lblerror.Text = ""

            Solve_All_Requests(sender, e)
            rbHistory.SelectedValue = "Quote History"

            fill_EQO_Grid()
            makeThisGridVisible(grdEQORFQ) 'FA-1174
            If rblShareData.SelectedValue = "GRAPHDATA" Then
                Call Fill_All_Charts()
            End If
            lblBNPPPrice.Text = ""

            lblMsgPriceProvider.Text = ""

            setToZero_ELN_AllIssuerPrice_ClientYield_ClientPrice()
            'If chkBNPP.Checked = True Then ''<Nikhil M. on 17-Sep-2016: COmmented >
                btnBNPPPrice.Text = "Price"
                btnBNPPDeal.Visible = False
            'End If

            btnBNPPPrice.Enabled = False
            btnBNPPPrice.CssClass = "btnDisabled"

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
                ''lblerror.Text = "Please enter valid No. Of Shares." ''<Commented by Rushikesh on 7-April-16 for zero notional >
                txtOrderqtyEQO.Text = "0" ''<Added by Rushikesh on 7-April-16 for zero notional >
                '<added by Mohit Lalwani on 16-Nov-2015>
                If rdbNotional.Checked Then

                Else
                    txtNotional.Text = ""
                End If


                '</added by Mohit Lalwani on 16-Nov-2015>
                Return False
                Exit Function
            Else
                Dim reg As Regex = New Regex("^[0-9kKmMbBtTlLpP.,]*$")
                If reg.IsMatch(strQyt) = False Then
                    lblerror.Text = "Please enter valid No. Of Shares."
                    '<added by Mohit Lalwani on 16-Nov-2015>
                    txtNotional.Text = ""
                    '</added by Mohit Lalwani on 16-Nov-2015>
                    Return False
                    Exit Function
                End If
            End If

            Return True
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = "Qty_Validate:Error occurred in processing. "
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
            fill_All_EntityBooks()

        Catch ex As Exception
            lblerror.Text = "ddlentity_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlentity_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    '<AvinashG. on 17-Dec-2014: FA-768 	Move RM dropdown from pricer page to order popup >
    Private Sub ddlRM_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRM.SelectedIndexChanged
        Try
            'Added by Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            '/Added by Mohit Lalwani on 1-Aug-2016
            lblerrorPopUp.Text = ""
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False '<Rutuja S. on 09-Dec-2014: As per JIRA(FA-693 - RFQ label message clear on tab change)>
            fill_Email()
            '<AvinashG. on 12-Mar-2015:     FA-775 Download and display TR DSS data on pricer page >
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
            '<Rutuja S. on 02-Jan-2015: Added to set Branch on  RM change for RFQ>
            fill_Branch()
            '</Rutuja S. on 02-Jan-2015: Added to set Branch on  RM change for RFQ>
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

            Get_RFQ_PieChart()
            upnlChart.Update()

            Get_RFQ_ColumnChart()
            upnlColumnChart.Update()


            upnlGrid.Update()
        Catch ex As Exception
            lblerror.Text = "Fill_All_Charts:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
            sSelfPath, "Fill_All_Charts", ErrorLevel.High)
        End Try
    End Sub

    Private Sub btnDealConfirm_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDealConfirm.ServerClick
        Try
            ' Call txtUpfrontPopUpValue_TextChanged(Nothing, Nothing) 'To surely calculate new upfront based yield
            Stop_timer_Only()
            '''<Dilkhush 04Nov2016 >EQSCB-102 Block TSO users from placing orders for Options 
            Dim sEQC_TSOLoginGroups As String
            sEQC_TSOLoginGroups = objReadConfig.ReadConfig(dsConfig, "EQC_BlockLoginGroupsFromOrder", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NONE")
            Dim sLoginGrp As String
            sLoginGrp = LoginInfoGV.Login_Info.LoginGroup

            If sEQC_TSOLoginGroups.Split(CChar(",")).ToList().IndexOf(sLoginGrp) > -1 Then
                lblerrorPopUp.Text = "Cannot place order please contact desk."
                Exit Sub
            End If
            '''</Dilkhush 04Nov2016 >EQSCB-102 Block TSO users from placing orders for Options 

            'Upfront condition for popup, 28-Apr-2014
            If Val(txtUpfrontPopUpValue.Text) > 0 And Val(txtUpfrontPopUpValue.Text.Replace(",", "")) < 100 Then
                Select Case UCase(lblIssuerPopUpValue.Text)
                    Case "BNPP"
                        ' ''<Start : Nikhil M. on 09-Sep-2016:Added >
                        'If ddlSideEQO.SelectedText.Trim = "Sell" And ddlOptionType.SelectedText.Contains("Call") _
                        '    Or ddlSideEQO.SelectedText.Trim = "Buy" And ddlOptionType.SelectedText.Contains("Put") Then
                        '    If chkConfirmDeal.Checked = True Then
                        '        btnBNPPDeal_Click(sender, e)
                        '    Else
                        '        chkConfirmDeal.Visible = True
                        '    End If
                        'Else
                        '    btnBNPPDeal_Click(sender, e)
                        'End If
                        ' ''<End : Nikhil M. on 09-Sep-2016:Added >
                        btnBNPPDeal_Click(sender, e)
                End Select
                ''KBM/Avi on 05-May-2014 to get updated order status
                btnLoad_Click(sender, e)
            ElseIf Val(txtUpfrontPopUpValue.Text.Replace(",", "")) >= 100 Then
                lblerrorPopUp.Text = "Upfront should be less than 100."
            Else
                lblerrorPopUp.Text = "Upfront should be greater than zero."
            End If
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = "Error in btnDealConfirm_ServerClick" & ex.Message
            lblerror.Text = "btnDealConfirm_ServerClick:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnDealConfirm_ServerClick", ErrorLevel.High)

        Finally
            '' DealConfirmPopup.Visible = False
            UPanle11111.Update()
        End Try

    End Sub
    Private Sub btnDealCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDealCancel.ServerClick
        Try
            '<AvinashG. on 01-Apr-2014: Disable other controls>
            ShowHideConfirmationPopup(False, "NO")
            ''DealConfirmPopup.Visible = False
            ''UPanle11111.Update()
            '</AvinashG. on 01-Apr-2014: Disable other controls>

            'Commented by Imran to create single string of java script 6-June-14
            'added below logic 

            'Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()


            'Dim strJavaScriptStopTimer As New StringBuilder
            ''System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "js2", "StopTimer('" + lblTimer.ClientID + "','" + btnBNPPDeal.ClientID + "');", True)
            'strJavaScriptStopTimer.AppendLine("StopTimer('" + lblTimerBNPP.ClientID + "','" + btnBNPPDeal.ClientID + "');")
            'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "StopAllTimers", strJavaScriptStopTimer.ToString, True)
            'ResetAll()

            '/Mohit Lalwani on 1-Aug-2016


            lblerror.Text = ""
            '<ashwiniP Start>
            lblTotalAmt.Visible = False
            lblTotalAmtVal.Visible = False
            lblAlloAmt.Visible = False
            lblAlloAmtVal.Visible = False
            lblRemainNotional.Visible = False
            lblRemainNotionalVal.Visible = False
            '<End>


        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = "Error in btnDealCancel_ServerClick" & ex.Message
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
                ''<Rutuja S.20March:Added for displaying the basket values in popup for DRA/FCN>

                txtLimitPricePopUpValue.Visible = True
                txtLimitPricePopUpValue.Enabled = True

                ddlBasketSharesPopValue.Visible = True
                txtLimitPricePopUpValue.Style.Add("width", "65px !important")
                ddlBasketSharesPopValue.Style.Add("width", "80px !important")

                Select Case tabContainer.ActiveTabIndex
                    Case prdTab.EQO
                        ddlBasketSharesPopValue.Visible = True
                        '<AvinashG. on 29-Jan-2015: FA-827, handle long RM Names >
                        txtLimitPricePopUpValue.Width = New WebControls.Unit(175)
                        'txtLimitPricePopUpValue.Width = New WebControls.Unit(115)
                        '</AvinashG. on 29-Jan-2015: FA-827, handle long RM Names >


                End Select

                ''</Rutuja S.20March:Added for displaying the basket values in popup for DRA/FCN>
            Else
                txtLimitPricePopUpValue.Enabled = False
                ddlBasketSharesPopValue.Visible = False
                txtLimitPricePopUpValue.Style.Add("width", "115px !important")
            End If
            ''need to change/cross check
            ''        UPanle11111.Update()
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = "Error in ddlOrderTypePopUpValue_SelectedIndexChanged" & ex.Message
            lblerror.Text = "ddlOrderTypePopUpValue_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlOrderTypePopUpValue_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub Prepare_EQO_Basket()
        Dim arrSelectedShare() As String
        Try
            txtBasketShares.Text = ""
            Session.Remove("BasketExchange")
            Session.Add("BasketExchange", "")
            tabShare2.Visible = False
            setTDSSData2("")
            tabShare3.Visible = False
            setTDSSData3("")
            If chkAddShare1.Checked = True Then
                ddlShareEQO.Enabled = True
                ddlExchangeEQO.Enabled = True
                ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                'If ddlShareEQO.SelectedItem Is Nothing Then
                If ddlShareEQO.SelectedValue Is Nothing Then
                    txtBasketShares.Text = ""
                    setTDSSData("")
                Else
                    If ddlExchangeEQO.SelectedValue.ToUpper = "ALL" Then
                        Dim sTemp As String
                        sTemp = lblExchangeEQO.Text.Split(CChar("-"))(0).Trim
                        Session.Add("BasketExchange", sTemp)
                    Else
                        Session.Add("BasketExchange", ddlExchangeEQO.SelectedValue)
                    End If
                    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    txtBasketShares.Text = ddlShareEQO.SelectedValue
                    setTDSSData(ddlShareEQO.SelectedValue)
                End If
                tabShare1.Visible = True
            Else
                ddlShareEQO.Enabled = False
                ddlExchangeEQO.Enabled = False
                setTDSSData("")
                tabShare1.Visible = False
            End If
            If chkAddShare2.Checked = True Then
                Dim blnAddSelectedShare As Boolean = True
                ddlShareEQO2.Enabled = True
                ddlExchangeEQO2.Enabled = True
                arrSelectedShare = txtBasketShares.Text.Split(CChar(","))
                For i = 0 To arrSelectedShare.Length - 1
                    'If ddlShareEQO2.Items.Count = 0 Then
                    'Else
                    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    'If ddlShareEQO2.SelectedItem Is Nothing Then
                    If ddlShareEQO2.SelectedValue Is Nothing Then
                    Else
                        If arrSelectedShare(i) = ddlShareEQO2.SelectedValue Then
                            blnAddSelectedShare = False
                            Exit For
                        End If

                        'End If
                    End If
                Next
                If blnAddSelectedShare = True Then
                    'If ddlShareEQO2.Items.Count = 0 Then
                    'Else
                    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    'If ddlShareEQO2.SelectedItem Is Nothing Then
                    If ddlShareEQO2.SelectedValue Is Nothing Then
                        txtBasketShares.Text = ""
                        setTDSSData2("")
                    Else
                        If ddlExchangeEQO2.SelectedValue.ToUpper = "ALL" Then
                            Dim sTemp As String
                            sTemp = lblExchangeEQO2.Text.Split(CChar("-"))(0).Trim
                            Session.Add("BasketExchange", CType(Session("BasketExchange"), String) + "," + sTemp)
                        Else
                            Session.Add("BasketExchange", CType(Session("BasketExchange"), String) + "," + ddlExchangeEQO2.SelectedValue)
                        End If
                        ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                        txtBasketShares.Text = If(txtBasketShares.Text = "", ddlShareEQO2.SelectedValue, If(ddlShareEQO2.SelectedValue <> "", txtBasketShares.Text & "," & ddlShareEQO2.SelectedValue, txtBasketShares.Text))
                        setTDSSData2(ddlShareEQO2.SelectedValue)

                    End If

                    tabShare2.Visible = True
                    'End If
                End If

            Else
                ddlShareEQO2.Enabled = False
                ddlExchangeEQO2.Enabled = False
                setTDSSData2("")
                tabShare2.Visible = False
            End If

            If chkAddShare3.Checked = True Then

                Dim blnAddSelectedShare As Boolean = True
                ddlShareEQO3.Enabled = True
                ddlExchangeEQO3.Enabled = True
                arrSelectedShare = txtBasketShares.Text.Split(CChar(","))
                For i = 0 To arrSelectedShare.Length - 1
                    ''If ddlShareEQO3.Items.Count = 0 Then
                    'Else
                    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    'If ddlShareEQO3.SelectedItem Is Nothing Then
                    If ddlShareEQO3.SelectedValue Is Nothing Then
                    Else
                        If arrSelectedShare(i) = ddlShareEQO3.SelectedValue Then
                            blnAddSelectedShare = False
                            Exit For

                        End If
                    End If
                    '' End If
                Next
                If blnAddSelectedShare = True Then
                    ''If ddlShareEQO3.Items.Count = 0 Then
                    ''Else
                    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    'If ddlShareEQO3.SelectedItem Is Nothing Then
                    If ddlShareEQO3.SelectedValue Is Nothing Then
                        txtBasketShares.Text = ""
                        setTDSSData3("")
                    Else
                        If ddlExchangeEQO3.SelectedValue.ToUpper = "ALL" Then
                            Dim sTemp As String
                            sTemp = lblExchangeEQO3.Text.Split(CChar("-"))(0).Trim
                            Session.Add("BasketExchange", CType(Session("BasketExchange"), String) + "," + sTemp)
                        Else
                            Session.Add("BasketExchange", CType(Session("BasketExchange"), String) + "," + ddlExchangeEQO3.SelectedValue)
                        End If
                        ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                        txtBasketShares.Text = If(txtBasketShares.Text = "", ddlShareEQO3.SelectedValue, If(ddlShareEQO3.SelectedValue <> "", txtBasketShares.Text & "," & ddlShareEQO3.SelectedValue, txtBasketShares.Text))
                        setTDSSData3(ddlShareEQO3.SelectedValue)

                    End If
                    tabShare3.Visible = True
                    ''End If

                End If
            Else
                ddlShareEQO3.Enabled = False
                ddlExchangeEQO3.Enabled = False
                setTDSSData3("")
                tabShare3.Visible = False
            End If

            If txtBasketShares.Text.StartsWith(",") Then
                txtBasketShares.Text = txtBasketShares.Text.Substring(1)
            End If
            If CType(Session("BasketExchange"), String).StartsWith(",") Then
                Session("BasketExchange") = CType(Session("BasketExchange"), String).Substring(1)
            End If
            GetCommentary_EQO()
            ''chkQuantoFlag_EQO()


        Catch ex As Exception
            lblerror.Text = "Prepare_EQO_Basket:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "Prepare_EQO_Basket", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub ResetAll()
        Try
            Enable_Disable_Deal_Buttons()   ''Added by Imran/Rutuja 26-June-14
            btnSolveAll.Enabled = True
            btnSolveAll.CssClass = "btn"
            chk_Login_For_PP()  ''Imran/Rutuja Added on 24June

            ''  Fill_All_Charts()
            Dim strJavaScript As New StringBuilder

            AllHiddenPrice.Value = "Enable;Disable"

            btnSolveAll.Enabled = True
            strJavaScript.AppendLine("document.getElementById('PriceAllWait').style.visibility = 'hidden';")

            If btnBNPPPrice.Visible Then
                strJavaScript.AppendLine("StopPPTimerValue('" + btnBNPPDeal.ClientID + "');")
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




            ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "StopPolling", "StopPolling();", True)
            strJavaScript.AppendLine("StopPolling();")
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "StopPolling", "try {" + strJavaScript.ToString + "} catch(e){}", True) 'Added by Mohit Lalwani on 17-Oct-2016
            '<AvinashG. on 27-Mar-2014: >
            DealConfirmPopup.Visible = False
            UPanle11111.Update()
            TRBNPP1.Attributes.Remove("class")
            ResetMinMaxNotional()
        Catch ex As Exception
            lblerror.Text = "ResetAll:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ResetAll", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub RestoreAll()
        Try
            Dim iTabIdx As Integer = tabContainer.ActiveTabIndex
            Dim ddlPrdSolveFor As DropDownList
            'Select Case iTabIdx
            '    Case 0
            '        ddlPrdSolveFor = ddlSolveFor
            '    Case 1
            '        ddlPrdSolveFor = ddlSolveForDRA
            '    Case 2
            '        ddlPrdSolveFor = ddlSolveForAccumDecum
            'End Select
            Dim strJavaScriptRestoreAll As New StringBuilder
            'RestoreSolveAll()
            strJavaScriptRestoreAll.AppendLine(Restore(BNPPHiddenPrice, lblBNPPPrice, lblBNPPClientPrice, lblBNPPClientYield, iTabIdx, ddlPrdSolveFor, btnBNPPPrice, BNPPHiddenMatDate, lblBNPPlimit, BNPPHiddenLimit))
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
            ResetCommetryElement() ''<Nikhil M. on 20-Sep-2016: Added to reset COmmetry element>
            lblerror.Text = ""
        Catch ex As Exception
            '<AvinashG. on 13-Aug-2014: Not used>lblerror.Text = ""
            lblerror.Text = "btnCancelReq_Click:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "btnCancelReq_Click", ErrorLevel.High)

        End Try
    End Sub
    '<AvinashG. on 28-Mar-2014: To Update basket if JS search results in unique share >
    Public Sub UpdateDRABasket(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHdnUpdateDRABasket.Click
        Try
            'Call Prepare_DRAFCN_Basket()
        Catch ex As Exception
            lblerror.Text = "UpdateDRABasket:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "UpdateDRABasket", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Sub EnablePage(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHdnEnablePage.Click, btnHdnEnablePage2.Click
        Try
            'ShowHideConfirmationPopup(False)
            ShowHideConfirmationPopup(False)
        Catch ex As Exception
            lblerror.Text = "EnablePage:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "EnablePage", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub ShowHideConfirmationPopup(ByVal blnShowPopup As Boolean, Optional ByVal isResetAll As String = "YES")
        Try
            'masterPanel.Enabled = Not blnShowPopup
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
            ObjCommanData = New Web_CommonFunction.CommonFunction
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

            Select Case tabContainer.ActiveTabIndex
                Case prdTab.EQO
                    panelEQO.Enabled = Not blnShowPopup
                    upnl4.Update()
            End Select
            '<AvinashG. on 07-Aug-2014: Let the PP control be diabled when popup is on>
            If TRBNPP1.Visible Then
                TRBNPP1.Disabled = blnShowPopup
            End If

            '</AvinashG. on 07-Aug-2014: Let the PP control be diabled when popup is on>
            PanelReprice.Enabled = Not blnShowPopup

            'Added by Mohit Lalwani on 1-Aug-2016
            If isResetAll = "YES" Then
                If blnShowPopup = False Then
                    ResetAll()
                End If
            End If
            '/Added by Mohit Lalwani on 1-Aug-2016


        Catch ex As Exception
            lblerror.Text = "ShowHideConfirmationPopup:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ShowHideConfirmationPopup", ErrorLevel.High)
            Throw ex
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

            If txtLimitPricePopUpValue.Text.Trim <> "" Then  'Changed by Mohit Lalwani on 8-Jul-2016
                '<AvinashG. on 15-Oct-2014: FA-681 - Limit Price precision while saving>
                If (txtLimitPricePopUpValue.Text.Length - (txtLimitPricePopUpValue.Text.LastIndexOf(".") + 1)) > 4 And CDbl(txtLimitPricePopUpValue.Text) <> Math.Floor(CDbl(txtLimitPricePopUpValue.Text)) Then
                    lblerrorPopUp.Text = "Precision of " + lblLimitPricePopUpCaption.Text + " cannot exceed four digits after decimal point."
                End If
            End If
            'txtLimitPricePopUpValue.Text = SetNumberFormat(txtLimitPricePopUpValue.Text, 4)
            '</AvinashG. on 15-Oct-2014: FA-681 - Limit Price precision while saving>
        Catch ex As Exception
            lblerror.Text = "txtLimitPricePopUpValue_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtLimitPricePopUpValue_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    ''</Rutuja:16April:added for formatting 4 number decimal,told by Kalyan>
    Private Sub makeThisGridVisible(ByVal grdToBeShown As DataGrid)
        Try

            grdOrder.Visible = False
            '<Rutuja S. on 02-Mar-2015: Added for EQO grid visibility>
            grdEQORFQ.Visible = False
            '</Rutuja S. on 02-Mar-2015: Added for EQO grid visibility>
            'Make parameter grid Visible True
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
            ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setPriceAllEnableDisable", "setPriceAllEnableDisable();", True)
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
                    'System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setPriceAllWaitLoader", "document.getElementById('PriceAllWait').style.visibility = 'visible';", True)
                Else
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "stopPriceAllWaitLoader", "try { document.getElementById('PriceAllWait').style.visibility = 'hidden'; } catch(e){ }", True) 		'Mohit Lalwani on 26-Oct-2016
                End If
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "RestoreSolveAll", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Function Restore(ByVal hiddenPriceCsv As HiddenField, ByVal lblIssuerPrice As Label, ByVal lblIssuerClientPrice As Label, _
                        ByVal lblIssuerClientYield As Label, ByVal tabIndex As Integer, ByRef ddlPrdSolveFor As DropDownList, _
                        ByVal btnPrice As Button, Optional ByVal HiddenMatDateCsv As HiddenField = Nothing, Optional ByVal lblMinMaxSize As Label = Nothing, _
                        Optional ByVal HiddenLimitCSV As HiddenField = Nothing) As String
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
                    lblIssuerPrice.Text = PriceArray(0)
                    '<AvinashG. on 18-May-2016:  FA-1430 - Easily changable color for Rejected and Timeout cases(remove red)>
                    'lblIssuerPrice.ForeColor = System.Drawing.Color.Green
                    lblIssuerPrice.CssClass = lblIssuerPrice.CssClass.Replace("lblPrice", "").Replace("lblRejected", "").Replace("lblTimeout", "") + " lblPrice"
                    '</AvinashG. on 18-May-2016:  FA-1430 - Easily changable color for Rejected and Timeout cases(remove red)>
                    If (lblIssuerPrice.Text <> "") Then
                        Select Case tabIndex
                            Case prdTab.EQO
                                If ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM" Then
                                    If ddlSideEQO.SelectedValue = "Buy" Then
                                        'lblBNPPClientPrice.Text = FormatNumber(((CDbl(lblBNPPPrice.Text) + (CDbl(txtUpfrontEQO.Text)) / 100)), 5)
                                        lblBNPPClientPrice.Text = FormatNumber(((CDbl(lblBNPPPrice.Text) + (CDbl(txtUpfrontEQO.Text)))), 4)
                                    ElseIf ddlSideEQO.SelectedValue = "Sell" Then
                                        'lblBNPPClientPrice.Text = FormatNumber(((CDbl(lblBNPPPrice.Text) - CDbl(txtUpfrontEQO.Text)) / 100), 5)
                                        lblBNPPClientPrice.Text = FormatNumber(((CDbl(lblBNPPPrice.Text) - CDbl(txtUpfrontEQO.Text))), 4)
                                    End If
                                    'lblBNPPPrice.Text = PriceArray(0)
                                    'lblBNPPPrice.Text = lblIssuerPrice.Text
                                Else
                                    If ddlSideEQO.SelectedValue = "Buy" Then
                                        'lblBNPPClientPrice.Text = FormatNumber(((CDbl(lblBNPPPrice.Text) + (CDbl(txtUpfrontEQO.Text)) / 100)), 5)
                                        lblBNPPClientPrice.Text = FormatNumber(((CDbl(txtPremium.Text) + (CDbl(txtUpfrontEQO.Text)))), 4)
                                    ElseIf ddlSideEQO.SelectedValue = "Sell" Then
                                        'lblBNPPClientPrice.Text = FormatNumber(((CDbl(lblBNPPPrice.Text) - CDbl(txtUpfrontEQO.Text)) / 100), 5)
                                        lblBNPPClientPrice.Text = FormatNumber(((CDbl(txtPremium.Text) - CDbl(txtUpfrontEQO.Text))), 4)
                                    End If

                                End If
                                '<AvinashG. on 11-Apr-2016: Setting Limit from UI hidden fields>
                                lblMinMaxSize.Text = convertNotionalintoShort((If(Split(BNPPHiddenLimit.Value, ",")(0).ToString = "", 0, CDbl(Split(BNPPHiddenLimit.Value, ",")(0)))), "MIN") + _
                                " / " + convertNotionalintoShort((If(Split(BNPPHiddenLimit.Value, ",")(1).ToString = "", 0, CDbl(Split(BNPPHiddenLimit.Value, ",")(1)))), "MAX")
                                lblMinMaxSize.ToolTip = Split(BNPPHiddenLimit.Value, ",")(0) + " / " + Split(BNPPHiddenLimit.Value, ",")(1)
                                lblMinMaxSize.Visible = True
                                setMinMaxCurrency()
                                '<AvinashG. on 11-Apr-2016: >

                        End Select
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

                '<AvinashG. on 15-Mar-2014: Run script only if PP is On>
                Dim waitCtrlId As String = btnPrice.ID.ToUpper.Replace("BTN", "").Replace("PRICE", "") & "wait"
                If btnPrice.Visible Then
                    If (PriceArray(3) = "Disable") Then
                        ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setPPLoaderInVisible", "document.getElementById('" & waitCtrlId & "').style.visibility = 'hidden';", True)
                        Restore = "try { document.getElementById('" & waitCtrlId & "').style.visibility = 'hidden'; } catch (e) { }" 		'Mohit Lalwani on 26-Oct-2016
                    Else
                        ' System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "setPPLoaderVisible", "document.getElementById('" & waitCtrlId & "').style.visibility = 'visible';", True)
                        Restore = "try { document.getElementById('" & waitCtrlId & "').style.visibility = 'visible'; } catch(e){ }"		'Mohit Lalwani on 26-Oct-2016
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
    Public Sub getRange()
        Dim Product_name As String
        Dim ExchangeCcy As String
        Dim dtRange As DataTable
        Try
            dtRange = New DataTable("RangeLimit")
            Product_name = "OTC Option"

            ExchangeCcy = ddlSettlCcyEQO.SelectedValue
            lblRangeCcy.Text = ""
            Select Case objELNRFQ.Get_EQCPRD_Limit(Product_name, ExchangeCcy, dtRange)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    If dtRange.Rows.Count > 0 Then
                        Dim limit As String
                        Dim minDoub As Double
                        Dim maxDoub As Double
                        getLimit("BNPP", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
                        lblBNPPlimit.Text = If(minDoub >= 1000000, FormatNumber((minDoub / 1000000), 2).ToString.Replace(".00", "") + "M/", FormatNumber((minDoub / 1000), 2).ToString.Replace(".00", "") + "K/") _
                                                + If(maxDoub >= 1000000, FormatNumber((maxDoub / 1000000), 2).ToString.Replace(".00", "") + "M", FormatNumber((maxDoub / 1000), 2).ToString.Replace(".00", "") + "K")
                        lblBNPPlimit.ToolTip = FormatNumber(minDoub.ToString, 2).Replace(".00", "") + "/" + FormatNumber(maxDoub.ToString, 2).Replace(".00", "")




                    Else
                        setLimitsAsNA()
                    End If
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    setLimitsAsNA()

                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                    setLimitsAsNA()
            End Select

            clearShareData()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "getRange", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    ''REPLACE BY Newer function 
    'Public Sub getRange()
    '    Dim Product_name As String
    '    Dim ExchangeCcy As String
    '    Dim dtRange As DataTable
    '    Try
    '        dtRange = New DataTable("RangeLimit")
    '        Select Case tabContainer.ActiveTabIndex

    '            Case 0
    '                Product_name = "OTC Option"
    '                ExchangeCcy = ddlSettlCcyEQO.SelectedValue
    '        End Select
    '        'Session.Add("RangePrd", Product_name) '<AvinashG. on 22-Sep-2014: Not required now, FA613>
    '        ''<Rutuja 16-Sept-2014:Added for changing label value from Range to Min/Max Jira id:FA-584>
    '        ''lblRangeCcy.Text = "Range(<B>" + ExchangeCcy + "</B>)"
    '        lblRangeCcy.Text = ""
    '        ''</Rutuja 16-Sept-2014:Added for changing label value from Range to Min/Max Jira id:FA-584>
    '        Select Case objELNRFQ.Get_EQCPRD_Limit(Product_name, ExchangeCcy, dtRange)  ' SchemeName(2nd Para ) to be discussed yet
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
    '                '<AvinashG. on 04-Sep-2014: FA-543 	Add issuer specific support to Limit ranges >

    '                If dtRange.Rows.Count > 0 Then

    '                    Dim limit As String
    '                    Dim minDoub As Double
    '                    Dim maxDoub As Double

    '                    '<AvinashG. on 04-Sep-2014: FA543>
    '                    '<AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    getLimit("BAML", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
    '                    'getLimit("BAML", dtRange, minDoub, maxDoub)
    '                    '</AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    lblBAMLlimit.Text = FormatNumber((minDoub / 1000000), 2).ToString + "M/" + FormatNumber((maxDoub / 1000000), 2).ToString & "M"
    '                    lblBAMLlimit.ToolTip = FormatNumber(minDoub.ToString, 2) + "/" + FormatNumber(maxDoub.ToString, 2)

    '                    '<AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    'getLimit("UBS", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
    '                    ''getLimit("UBS", dtRange, minDoub, maxDoub)
    '                    ''</AvinashG. on 23-Sep-2014: FA-597 ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    'lblUBSlimit.Text = FormatNumber((minDoub / 1000000), 2).ToString + "M/" + FormatNumber((maxDoub / 1000000), 2).ToString & "M"
    '                    'lblUBSlimit.ToolTip = FormatNumber(minDoub.ToString, 2) + "/" + FormatNumber(maxDoub.ToString, 2)

    '                    ''<AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    'getLimit("CS", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
    '                    ''getLimit("CS", dtRange, minDoub, maxDoub)
    '                    ''</AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    'lblCSLimit.Text = FormatNumber((minDoub / 1000000), 2).ToString + "M/" + FormatNumber((maxDoub / 1000000), 2).ToString & "M"
    '                    'lblCSLimit.ToolTip = FormatNumber(minDoub.ToString, 2) + "/" + FormatNumber(maxDoub.ToString, 2)

    '                    ''<AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    'getLimit("GS", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
    '                    ''getLimit("GS", dtRange, minDoub, maxDoub)
    '                    ''</AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    'lblGSlimit.Text = FormatNumber((minDoub / 1000000), 2).ToString + "M/" + FormatNumber((maxDoub / 1000000), 2).ToString & "M"
    '                    'lblGSlimit.ToolTip = FormatNumber(minDoub.ToString, 2) + "/" + FormatNumber(maxDoub.ToString, 2)

    '                    ''<AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    'getLimit("HSBC", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
    '                    ''getLimit("HSBC", dtRange, minDoub, maxDoub)
    '                    ''</AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    'lblHSBClimit.Text = FormatNumber((minDoub / 1000000), 2).ToString + "M/" + FormatNumber((maxDoub / 1000000), 2).ToString & "M"
    '                    'lblHSBClimit.ToolTip = FormatNumber(minDoub.ToString, 2) + "/" + FormatNumber(maxDoub.ToString, 2)

    '                    'getLimit("MS", dtRange, minDoub, maxDoub, ExchangeCcy, Product_name)
    '                    ''getLimit("CS", dtRange, minDoub, maxDoub)
    '                    ''</AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '                    'lblMSlimit.Text = FormatNumber((minDoub / 1000000), 2).ToString + "M/" + FormatNumber((maxDoub / 1000000), 2).ToString & "M"
    '                    'lblMSlimit.ToolTip = FormatNumber(minDoub.ToString, 2) + "/" + FormatNumber(maxDoub.ToString, 2)

    '                Else
    '                    lblBAMLlimit.Text = "N.A."
    '                    lblGSlimit.Text = "N.A."
    '                    lblUBSlimit.Text = "N.A."
    '                    lblGSlimit.Text = "N.A."
    '                    lblHSBClimit.Text = "N.A."
    '                    lblCSLimit.Text = "N.A."
    '                    lblMSlimit.Text = "N.A."
    '                    lblBAMLlimit.ToolTip = ""
    '                    lblGSlimit.ToolTip = ""
    '                    lblUBSlimit.ToolTip = ""
    '                    lblGSlimit.ToolTip = ""
    '                    lblHSBClimit.ToolTip = ""
    '                    lblCSLimit.ToolTip = ""
    '                    lblMSlimit.ToolTip = ""
    '                    'Session("MinLimit") = 0
    '                    'Session("MaxLimit") = 0
    '                End If
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
    '                lblBAMLlimit.Text = "N.A."
    '                lblGSlimit.Text = "N.A."
    '                lblUBSlimit.Text = "N.A."
    '                lblGSlimit.Text = "N.A."
    '                lblHSBClimit.Text = "N.A."
    '                lblCSLimit.Text = "N.A."
    '                lblMSlimit.Text = "N.A."
    '                lblBAMLlimit.ToolTip = ""
    '                lblGSlimit.ToolTip = ""
    '                lblUBSlimit.ToolTip = ""
    '                lblGSlimit.ToolTip = ""
    '                lblHSBClimit.ToolTip = ""
    '                lblCSLimit.ToolTip = ""
    '                lblMSlimit.ToolTip = ""
    '                'Session("MinLimit") = 0
    '                'Session("MaxLimit") = 0
    '            Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
    '                lblBAMLlimit.Text = "N.A."
    '                lblGSlimit.Text = "N.A."
    '                lblUBSlimit.Text = "N.A."
    '                lblGSlimit.Text = "N.A."
    '                lblHSBClimit.Text = "N.A."
    '                lblCSLimit.Text = "N.A."
    '                lblMSlimit.Text = "N.A."
    '                lblBAMLlimit.ToolTip = ""
    '                lblGSlimit.ToolTip = ""
    '                lblUBSlimit.ToolTip = ""
    '                lblGSlimit.ToolTip = ""
    '                lblHSBClimit.ToolTip = ""
    '                lblCSLimit.ToolTip = ""
    '                lblMSlimit.ToolTip = ""

    '                'Session("MinLimit") = 0
    '                'Session("MaxLimit") = 0
    '        End Select
    '    Catch ex As Exception
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "getRange", ErrorLevel.High)
    '        Throw ex
    '    End Try
    'End Sub

    '<AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array, Change of function signature(added Ccy)>
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
    ''RECIEVED by newer getLimit
    'Private Sub getLimit(ByVal sPPCode As String, ByVal dt As DataTable, ByRef dblMinLim As Double, ByRef dblMaxLim As Double, ByVal sCcy As String, ByVal sPrd As String)
    '    Try
    '        dblMinLim = 0
    '        dblMaxLim = 0
    '        Dim result() As DataRow = dt.Select("EQCPPL_PPCode = '" + sPPCode + "'")
    '        '<AvinashG. on 18-Sep-2014: FA-591 	Message if limit not found for a currency & FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array >
    '        If result.Length < 1 Then
    '            dblMinLim = 0
    '            dblMaxLim = 0
    '            '<AvinashG. on 23-Sep-2014: FA-597 	ELN_RFQ: Exception:Index was outside the bounds of the array>
    '            LogException(LoginInfoGV.Login_Info.LoginId, "No limit setup found for " + sPPCode + " for " + sCcy + " for " + sPrd, LogType.FnqInfo, Nothing, _
    '                                      sSelfPath, "getLimit", ErrorLevel.Medium)
    '        Else
    '            Dim lmtRow As DataRow = result(0)
    '            dblMinLim = CDbl(If(IsDBNull(lmtRow("EQCPPL_Minm")), 0, CDbl(lmtRow("EQCPPL_Minm"))))
    '            dblMaxLim = CDbl(If(IsDBNull(lmtRow("EQCPPL_Maxm")), 0, CDbl(lmtRow("EQCPPL_Maxm"))))
    '        End If

    '    Catch ex As Exception
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "getLimit", ErrorLevel.High)
    '    End Try
    'End Sub

    '<Rutuja S. on 16-Feb-2015: Added to fill EQO exchange>
    Private Sub ddlExchangeEQO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExchangeEQO.SelectedIndexChanged

        Dim dtBaseCCY As DataTable
        Try
            dtBaseCCY = New DataTable("Dummy")
            lblerror.Text = ""
            clearFields()

            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            With ddlShareEQO
                .Items.Clear()
                .Text = ""
            End With

            ''fill_EQO_FCN_Share()  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            If ddlShareEQO.SelectedValue IsNot Nothing And ddlShareEQO.SelectedItem IsNot Nothing Then
                Select Case objELNRFQ.DB_GetBASECCY(ddlShareEQO.SelectedValue, dtBaseCCY)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        lblEQOBaseCcy.Text = dtBaseCCY.Rows(0)(0).ToString
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                        lblEQOBaseCcy.Text = ""
                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                        lblEQOBaseCcy.Text = ""
                End Select
                fillddlInvestCcy()  ''<Added by Rushikesh on 6-April-16 to fill investment ccy in case if basket>
                GetDatesForEQO()
                GetCommentary_EQO()
                ddlShareEQO.Text = ddlShareEQO.Text  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                If lblEQOBaseCcy.Text <> "" Then
                    ddlInvestCcy.SelectedValue = lblEQOBaseCcy.Text
                    ddlSettlCcyEQO.SelectedValue = lblEQOBaseCcy.Text
                End If

                Prepare_EQO_Basket()
            End If
            pnlReprice.Update()
            upnlCommentry.Update()
            ResetAll()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub getCurrencyforEQO(ByVal strShare As String)
        Try
            Dim dtBaseCurrencyEQO As DataTable
            dtBaseCurrencyEQO = New DataTable("Dummy")

            Select Case objELNRFQ.web_GetBASEcurrencyforEQO(strShare, dtBaseCurrencyEQO)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    lblEQOBaseCcy.Text = dtBaseCurrencyEQO.Rows(0)(0).ToString
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    lblEQOBaseCcy.Text = ""
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    lblEQOBaseCcy.Text = ""
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtBarrierLevelEQO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBarrierLevelEQO.TextChanged
        Try
            lblerror.Text = ""
            clearFields()
            ''GetCommentary_DRA()  TO DO
            txtBarrierLevelEQO.Text = SetNumberFormat(txtBarrierLevelEQO.Text, 2)
            ResetAll()

        Catch ex As Exception
            lblerror.Text = "txtBarrierLevelEQO_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtBarrierLevelEQO_TextChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txtPremium_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPremium.TextChanged
        Try
            lblerror.Text = ""
            clearFields()
            txtPremium.Text = SetNumberFormat(txtPremium.Text, 4)
            ''GetCommentary_DRA()  TO DO
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtPremium_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtPremium_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    Public Sub fillQuantoForEQO()
        Dim dtQuantoCcy As DataTable
        Dim I_Marketype As String = String.Empty
        Try

            dtQuantoCcy = New DataTable("DUMMY")
            I_Marketype = "EQ"
            '<AvinashG. on 21-Apr-2014: Managing QuantoCcy filling as per product, logic to be finalised with Kalyan/Jitu>
            '<Rutuja S. on 24-Feb-2015: Need to discuss about Product type for filling settlement CCy for method :IMP>
            Select Case objELNRFQ.Get_EQO_StockBased_Currency(dtQuantoCcy)
                '</Rutuja S. on 04-Nov-2014:Use share currency instead of exchange currency ,JIRA ID-FA:706 >
                'Select Case objELNRFQ.Get_ProdBased_QuantoCcy("ELN", ddlExchange.SelectedItem.Text, dtQuantoCcy)
                '</AvinashG. on 09-Oct-2014: FA-668 DRA/FCN: Look and feel for Exchnage on DRA/FCN >
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlSettlCcyEQO
                        .DataSource = dtQuantoCcy
                        .DataTextField = "CCY"
                        .DataValueField = "CCY"
                        .DataBind()
                    End With



                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlSettlCcyEQO
                        .DataSource = dtQuantoCcy
                        .DataBind()
                    End With

                    With ddlInvestCcy
                        .DataSource = dtQuantoCcy
                        .DataBind()
                    End With


            End Select

        Catch ex As Exception
            lblerror.Text = "fillQuantoForEQO:Error occurred in filling Quanto Ccy."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fillQuantoForEQO", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Public Sub GetDatesForEQO()
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
        ''<Rutuja 3April>
        Dim dtExchangeInfo As DataTable
        ''<Rutuja 3April>
        Try
            ''----
            Dim Trade_Date As String = Today.Date.ToString("dd-MMM-yyyy") '' start date/settlement date

            Ccy = lblEQOBaseCcy.Text

            'If chkQuantoCcy.Checked = False Then
            '    Ccy1 = lblELNBaseCcy.Text
            'Else
            '    Ccy1 = ddlQuantoCcy.SelectedValue
            'End If

            Ccy1 = ddlSettlCcyEQO.SelectedValue

            Dim interval As String = ddlTenorEQO.SelectedValue
            If interval = "DAY" Then
                interval = "D"
            ElseIf interval = "WEEK" Then
                interval = "W"
            ElseIf interval = "MONTH" Then
                interval = "M"
            ElseIf interval = "YEAR" Then
                interval = "Y"
            End If
            SoftTenor = txtTenorEQO.Text & interval
            '<AvinashG. on 11-Jul-2016: AS per Deutsche(Aditya) mail> Settlement_Days = txtSettlDays.Text '<AvinashG. on 21-Apr-2016: as per Mahesh's mail>
            ''Commented to make it common function
            If ddlExchangeEQO.SelectedValue.ToUpper = "ALL" Then
                Maturity_Days = getMaturityDays(ddlShareEQO.SelectedValue.ToString)
            Else
                Maturity_Days = getMaturityDays(ddlExchangeEQO.SelectedValue.Trim.ToUpper)
            End If


            '' ''Dim dr As DataRow()
            '' ''dtExchangeInfo = New DataTable("Dummy")
            '' ''dtExchangeInfo = CType(Session("Exchange_Details"), DataTable)

            '' ''If ddlExchangeEQO.SelectedValue.ToUpper = "ALL" Then
            '' ''    Dim sTemp As String
            '' ''    'dr = dtExchangeInfo.Select("ExchangeCode = '" & ddlExchangeEQO.SelectedValue.Trim.ToUpper & "' ")
            '' ''    dr = dtExchangeInfo.Select("Exchange_Name = '" & objELNRFQ.GetShareExchange(ddlShareEQO.SelectedValue.ToString, sTemp) & "' ")
            '' ''Else
            '' ''    dr = dtExchangeInfo.Select("Exchange_Name = '" & ddlExchangeEQO.SelectedValue.Trim.ToUpper & "' ")
            '' ''End If

            '' ''If dr.Length > 0 Then
            '' ''    Maturity_Days = dr(0).Item("SettlementDays").ToString
            '' ''    If Val(Maturity_Days) = 0 Then
            '' ''        Maturity_Days = "2"   ''if settlement is 0 in db then take it 2 by default,told by Kalyan M.
            '' ''    Else
            '' ''        Maturity_Days = Maturity_Days
            '' ''    End If
            '' ''End If
            '<AvinashG. on 11-Jul-2016: AS per Deutsche(Aditya) mail for date calc>
            Settlement_Days = Maturity_Days
            txtSettlDays.Text = Maturity_Days

            Friday_Preferred_YN = objReadConfig.ReadConfig(dsConfig, "EQO_MaturityDate_FridayPreferred_YN", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO")
            '<AvinashG. on 16-Nov-2015: FA-1178 - EQO: Use Settlemnet days for Mat date and settlement date calculation>
            Select Case objELNRFQ.getUSDBasedEQODates(CStr(LoginInfoGV.Login_Info.EntityID), Ccy, Ccy1, Ccy, _
                                                  SoftTenor, Trade_Date, Settlement_Days, _
                                                  Maturity_Days, Friday_Preferred_YN, Value_Date, Fixing_Date, Maturity_Date)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    Session.Add("SettlementDate", Value_Date)
                    Session.Add("ExpiryDate", Fixing_Date)
                    Session.Add("MaturityDate", Maturity_Date)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
                    lblerror.Text = "GetDatesForEQO:Error occurred in processing."
            End Select
            'Select Case objELNRFQ.getUSDBasedDates(CStr(LoginInfoGV.Login_Info.EntityID), Ccy, Ccy1, Ccy, _
            '                                       SoftTenor, Trade_Date, Settlement_Days, _
            '                                       Maturity_Days, Friday_Preferred_YN, Value_Date, Fixing_Date, Maturity_Date)
            '    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
            '        Session.Add("SettlementDate", Value_Date)
            '        Session.Add("ExpiryDate", Fixing_Date)
            '        Session.Add("MaturityDate", Maturity_Date)
            '    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data

            '    Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            '        lblerror.Text = "GetDatesForEQO:Error occurred in processing."
            'End Select
            '</AvinashG. on 16-Nov-2015: FA-1178 - EQO: Use Settlemnet days for Mat date and settlement date calculation>
            txtTradeDateEQO.Value = FinIQApp_Date.FinIQDate(Trade_Date)
            txtMaturityDateEQO.Value = FinIQApp_Date.FinIQDate(Maturity_Date)
            txtSettlDateEQO.Value = FinIQApp_Date.FinIQDate(Value_Date)
            txtExpiryDateEQO.Value = FinIQApp_Date.FinIQDate(Fixing_Date)
            'upnl1.Update() '<AvinashG. on 29-Apr-2014: Update the panel>
            Session("IsManualDateEditYN") = "N" '<AvinashG. on 19-Aug-2014: Reset Manual Date Edit Flag>
        Catch ex As Exception
            lblerror.Text = "GetDatesForEQO:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "GetDatesForEQO", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub ddlProductEQO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProductEQO.SelectedIndexChanged
        Try
            ''on product change fill shares
            If ddlProductEQO.SelectedValue.ToUpper = "EQUITY" Then
                '<AvinashG. on 05-Apr-2016: >
                fill_All_Exchange()
                'fillEQOExchanges()
                '<AvinashG. on 05-Apr-2016: >
                'fillEQOShare()''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''TODO
                'fill_EQO_Shares(ddlExchangeEQO2.SelectedValue, ddlShareEQO2)
                'fill_EQO_Shares(ddlExchangeEQO3.SelectedValue, ddlShareEQO3)
                If lblEQOBaseCcy.Text <> "" Then
                    ddlSettlCcyEQO.SelectedValue = lblEQOBaseCcy.Text
                End If

                ''getRange()
                'chkAddShareEQO2_CheckedChanged(sender, e)
                'chkAddShareEQO3_CheckedChanged(sender, e)

                If ddlShareEQO.SelectedValue Is Nothing Then
                    lblEQOBaseCcy.Text = ""
                    'txtsearchEQO1.Text = ddlShareEQO.SelectedValue
                Else
                    'txtsearchEQO1.Text = ddlShareEQO.SelectedValue
                    lblEQOBaseCcy.Text = getBaseCurrency(ddlShareEQO.SelectedValue)
                    If lblEQOBaseCcy.Text <> "" Then
                        ddlSettlCcyEQO.SelectedValue = lblEQOBaseCcy.Text
                    End If
                End If
                Prepare_EQO_Basket()

            Else
                txtBasketShares.Text = ""
                'txtsearchEQO1.Text = ""
                'txtSearchShareEQO2.Text = ""
                'txtSearchShareEQO3.Text = ""
                '<AvinashG. on 05-Apr-2016: >
                fill_All_Exchange()
                'fillEQOExchanges()
                '<AvinashG. on 05-Apr-2016: >
                'txtsearchEQO1.Text = ddlShareEQO.SelectedValue
                'ddlShareEQO2.
                Prepare_EQO_Basket()
            End If
            'If chkAddShareEQO2.Checked = True Then
            '    FillEQOddl_exchange(ddlExchangeEQO2)
            '    fill_EQO_Shares(ddlExchangeEQO2.SelectedValue, ddlShareEQO2)
            'End If

            'If chkAddShareEQO3.Checked = True Then
            '    FillEQOddl_exchange(ddlExchangeEQO3)
            '    fill_EQO_Shares(ddlExchangeEQO3.SelectedValue, ddlShareEQO3)
            'End If

            lblerror.Text = ""
            clearFields()
            ResetAll()
            'getRange()

        Catch ex As Exception
            lblerror.Text = "ddlProductEQO_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlProductEQO_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub ddlSolveforEQO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSolveforEQO.SelectedIndexChanged
        Try
            lblerror.Text = ""
            clearFields()
            '' GetCommentary_EQO()
            HideShowControlsforEQO()
            ''ResetAll()
            If ddlSolveforEQO.SelectedText.ToUpper = "PREMIUM" Then
                lblSolveForType.Text = "Premium (%)"
            ElseIf ddlSolveforEQO.SelectedText.ToUpper = "STRIKE" Then
                lblSolveForType.Text = "Strike (%)"
            ElseIf ddlSolveforEQO.SelectedText.ToUpper = "BARRIER" Then
                lblSolveForType.Text = "Barrier (%)"
            End If

            If ddlOptionType.SelectedValue.Trim.ToUpper = "KNOCKIN PUT" Then
                ddlSideEQO.SelectedIndex = 1
                ddlSideEQO.Enabled = False
                ddlSideEQO.BackColor = Color.FromArgb(242, 242, 243)
            Else

                'This Config is added by Mohit Lalwani on 7-Jul-2016 as told by Sanchita 
                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableClientSide", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        ddlSideEQO.Enabled = True
                        ddlSideEQO.BackColor = Color.White
                    Case "N", "NO"
                        ddlSideEQO.Enabled = False
                        ddlSideEQO.BackColor = Color.FromArgb(242, 242, 243)
                End Select
             

                'ddlSideEQO.Enabled = True
                'ddlSideEQO.BackColor = Color.White


                '/This Config is added by Mohit Lalwani on 7-Jul-2016 as told by Sanchita

            End If
            ''Sequence changed by AshwiniP on 24-Oct-2016 to reset Commentry 
            ResetAll()
            GetCommentary_EQO()
            ' getRange()
        Catch ex As Exception
            lblerror.Text = "ddlSolveforEQO_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlSolveforEQO_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Public Sub HideShowControlsforEQO()
        Try
            If ddlOptionType.SelectedValue.Trim.ToUpper <> "KNOCKIN PUT" Then
                Select Case ddlSolveforEQO.SelectedValue.ToUpper.Trim
                    Case "PREMIUM"
                        txtPremium.Text = ""
                        txtPremium.Enabled = False
                        txtStrikeEQO.Enabled = True

                    Case "STRIKE"
                        txtPremium.Enabled = True
                        txtStrikeEQO.Text = ""
                        txtStrikeEQO.Enabled = False
                        '' Case "KnockIn Put"
                        '' barrier not for ddloption types other than barrier
                End Select
                txtBarrierLevelEQO.Visible = False
                ddlBarrierEQO.Visible = False
                ddlBarrierMonitoringType.Visible = False
                lblBarrierMonitoringType.Visible = False
            Else
                txtBarrierLevelEQO.Visible = True
                ddlBarrierEQO.Visible = True
                ddlBarrierMonitoringType.Visible = True
                lblBarrierMonitoringType.Visible = True
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "HideShowControlsforEQO", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub ddlOptionType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOptionType.SelectedIndexChanged
        Try
            lblerror.Text = ""
            ddlSolveforEQO.Items.Clear()
            If ddlOptionType.SelectedValue.Trim.ToUpper = "KNOCKIN PUT" Then
                'chkAddShareEQO1.Visible = True   '<Neha M. on 12-May-2015: told by Ajay>
                ddlSideEQO.SelectedIndex = 1
                ddlSideEQO.Enabled = False
                ddlSideEQO.BackColor = Color.FromArgb(242, 242, 243)
                With ddlSolveforEQO
                    .Items.Add(New DropDownListItem("Premium", "Premium"))
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableSolveForStrike", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper 'Mohit Lalwani on 8-Jul-2016 as Told by Roshani on 8-Jul-2016
                        Case "Y", "YES"
                            .Items.Add(New DropDownListItem("Strike", "Strike"))
                        Case "N", "NO"
                    End Select
                    .Items.Add(New DropDownListItem("Barrier", "Barrier"))
                    .SelectedIndex = 0
                End With
                txtBarrierLevelEQO.Visible = True
                ddlBarrierEQO.Visible = True
                ddlBarrierMonitoringType.Visible = True
                lblBarrierMonitoringType.Visible = True

                txtBarrierLevelEQO.Text = "90"
                ddlBarrierEQO.SelectedValue = "Percentage"
                ddlBarrierMonitoringType.SelectedValue = "Continuous"

                'tblEQOShare2.Visible = True
                'If chkAddShareEQO2.Checked Then
                '    ddlStrikeTypeEQO.Enabled = False
                '    ddlStrikeTypeEQO.BackColor = Color.FromArgb(242, 242, 243)
                '    ddlStrikeTypeEQO.SelectedIndex = 0
                '    ddlBarrierEQO.Enabled = False
                '    ddlBarrierEQO.BackColor = Color.FromArgb(242, 242, 243)
                '    ddlBarrierEQO.SelectedIndex = 0
                'Else
                '    ddlStrikeTypeEQO.Enabled = True
                '    ddlStrikeTypeEQO.BackColor = Color.White
                '    ddlBarrierEQO.Enabled = True
                '    ddlBarrierEQO.BackColor = Color.White
                'End If
            Else
                txtBarrierLevelEQO.Text = "0"

                'This Config is added by Mohit Lalwani on 7-Jul-2016 as told by Sanchita 
                Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableClientSide", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        ddlSideEQO.Enabled = True
                        ddlSideEQO.BackColor = Color.White
                    Case "N", "NO"
                        ddlSideEQO.Enabled = False
                        ddlSideEQO.BackColor = Color.FromArgb(242, 242, 243)
                End Select







                'ddlSideEQO.Enabled = True
                'ddlSideEQO.BackColor = Color.White


                '/This Config is added by Mohit Lalwani on 7-Jul-2016 as told by Sanchita 
                'chkAddShareEQO1.Visible = False  '<Neha M. on 12-May-2015: told by Ajay>
                With ddlSolveforEQO
                    .Items.Add(New DropDownListItem("Premium", "Premium"))
                    Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableSolveForStrike", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper 'Mohit Lalwani on 8-Jul-2016 as Told by Roshani on 8-Jul-2016
                        Case "Y", "YES"
                            .Items.Add(New DropDownListItem("Strike", "Strike"))
                        Case "N", "NO"
                    End Select
                    .SelectedIndex = 0
                End With
                txtBarrierLevelEQO.Visible = False
                ddlBarrierEQO.Visible = False
                ddlBarrierMonitoringType.Visible = False
                lblBarrierMonitoringType.Visible = False
                'tblEQOShare2.Visible = False
                'tblEQOShare3.Visible = False
                'chkAddShareEQO2.Checked = False
                'chkAddShareEQO2_CheckedChanged(sender, e)
                'chkAddShareEQO3.Checked = False
                'chkAddShareEQO3_CheckedChanged(sender, e)
                'ddlStrikeTypeEQO.Enabled = True
                'ddlStrikeTypeEQO.BackColor = Color.White
            End If
            ddlSolveforEQO_SelectedIndexChanged(sender, e)
            ResetAll()
        Catch ex As Exception
            lblerror.Text = "ddlOptionType_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlOptionType_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub ddlShareEQO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShareEQO.SelectedIndexChanged
        Dim dtBaseCCY As DataTable
        Try
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            'If ddlShareEQO.SelectedItem Is Nothing Then
            If ddlShareEQO.SelectedValue Is Nothing Then
                clearShareData()
                ddlShareEQO.Text = "Please select valid share."
                lblExchangeEQO.Text = ""
                txtNotional.Text = ""
                lblNotionalWithCcy1.Text = ""
                lblNotionalWithCcy.Text = ""
                ResetAll()                       ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                ''ResetAll()
                Exit Sub
            ElseIf ddlShareEQO.SelectedValue = "" Then
                clearShareData()
                ddlShareEQO.Text = "Please select valid share."
                lblExchangeEQO.Text = ""
                txtNotional.Text = ""
                lblNotionalWithCcy1.Text = ""
                lblNotionalWithCcy.Text = ""
                ResetAll()                  ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                ''ResetAll()
                Exit Sub
            Else
                lblExchangeEQO.Text = setExchangeByShare(ddlShareEQO)
                dtBaseCCY = New DataTable("Dummy")
                lblerror.Text = ""
                clearFields()
                Dim sShareCcy1 As String = ""
                getCurrency(ddlShareEQO.SelectedValue, sShareCcy1)
                lblEQOBaseCcy.Text = sShareCcy1
                Dim sSharePRR1 As String = ""
                getPRR(ddlShareEQO.SelectedValue.ToString, sSharePRR1)
                getFlag(ddlShareEQO.SelectedValue.ToString)
                lblPRRVal.Text = sSharePRR1
                ''If lblPRRVal.Text = "NA" Then
                ''    lblPRRVal.ForeColor = Color.Red
                ''Else
                ''    lblPRRVal.ForeColor = Color.Green
                ''End If
                fillddlInvestCcy()  ''<Added by Rushikesh on 6-April-16 to fill investment ccy in case if basket>
                ddlInvestCcy.SelectedValue = lblEQOBaseCcy.Text
                ddlSettlCcyEQO.SelectedValue = lblEQOBaseCcy.Text
                Prepare_EQO_Basket()
                GetCommentary_EQO()
                If ddlShareEQO.SelectedItem IsNot Nothing Then
                    ddlShareEQO.Text = ddlShareEQO.SelectedItem.Text
                End If
                GetDatesForEQO()
                ResetAll()                      ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                ''ResetAll()
                DisplayEstimatedNotional()
            End If

        Catch ex As Exception
            lblerror.Text = "ddlShareEQO_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlShareEQO_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub ddlShareEQO2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShareEQO2.SelectedIndexChanged
        Dim dtBaseCCY As DataTable
        Try
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            'If ddlShareEQO2.SelectedItem Is Nothing Then
            If ddlShareEQO2.SelectedValue Is Nothing Then
                clearShareData()
                ddlShareEQO2.Text = "Please select valid share."
                lblExchangeEQO2.Text = ""
                ResetAll()                  ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                ''ResetAll()
                Exit Sub
            ElseIf ddlShareEQO2.SelectedValue = "" Then
                clearShareData()
                ddlShareEQO2.Text = "Please select valid share."
                lblExchangeEQO2.Text = ""
                ResetAll()              ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                ''ResetAll()
                Exit Sub
            Else
                lblExchangeEQO2.Text = setExchangeByShare(ddlShareEQO2)
                dtBaseCCY = New DataTable("Dummy")
                lblerror.Text = ""
                clearFields()
                Dim sShareCcy1 As String = ""
                getCurrency(ddlShareEQO2.SelectedValue, sShareCcy1)
                lblBaseCurrency2.Text = sShareCcy1
                Dim sSharePRR1 As String = ""
                getPRR(ddlShareEQO2.SelectedValue.ToString, sSharePRR1)
                getFlag(ddlShareEQO2.SelectedValue.ToString)
                lblPRRVal2.Text = sSharePRR1
                ''If lblPRRVal2.Text = "NA" Then
                ''    lblPRRVal2.ForeColor = Color.Red
                ''Else
                ''    lblPRRVal2.ForeColor = Color.Green
                ''End If
                fillddlInvestCcy()  ''<Added by Rushikesh on 6-April-16 to fill investment ccy in case if basket>
                ddlInvestCcy.SelectedValue = lblBaseCurrency2.Text
                ddlSettlCcyEQO.SelectedValue = lblBaseCurrency2.Text
                Prepare_EQO_Basket()

                If ddlShareEQO2.SelectedItem IsNot Nothing Then
                    ddlShareEQO2.Text = ddlShareEQO2.SelectedItem.Text
                End If
                ResetAll()                  ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                ''ResetAll()
                DisplayEstimatedNotional()
            End If

        Catch ex As Exception
            lblerror.Text = "ddlShareEQO2_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlShareEQO2_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    Private Sub ddlShareEQO3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShareEQO3.SelectedIndexChanged
        Dim dtBaseCCY As DataTable
        Try
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            'If ddlShareEQO3.SelectedItem Is Nothing Then
            If ddlShareEQO3.SelectedValue Is Nothing Then
                clearShareData()
                ddlShareEQO3.Text = "Please select valid share."
                lblExchangeEQO3.Text = ""
                ResetAll()                          ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                ''ResetAll()
                Exit Sub
            ElseIf ddlShareEQO3.SelectedValue = "" Then
                clearShareData()
                ddlShareEQO3.Text = "Please select valid share."
                lblExchangeEQO3.Text = ""
                ResetAll()          ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                ''ResetAll()
                Exit Sub
            Else
                lblExchangeEQO3.Text = setExchangeByShare(ddlShareEQO3)
                dtBaseCCY = New DataTable("Dummy")
                lblerror.Text = ""
                clearFields()
                Dim sShareCcy1 As String = ""
                getCurrency(ddlShareEQO3.SelectedValue, sShareCcy1)
                lblBaseCurrency3.Text = sShareCcy1
                Dim sSharePRR1 As String = ""
                getPRR(ddlShareEQO3.SelectedValue.ToString, sSharePRR1)
                lblPRRVal.Text = sSharePRR1
                ''If lblPRRVal3.Text = "NA" Then
                ''    lblPRRVal3.ForeColor = Color.Red
                ''Else
                ''    lblPRRVal3.ForeColor = Color.Green
                ''End If
                fillddlInvestCcy()  ''<Added by Rushikesh on 6-April-16 to fill investment ccy in case if basket>
                ddlInvestCcy.SelectedValue = lblBaseCurrency3.Text
                ddlSettlCcyEQO.SelectedValue = lblBaseCurrency3.Text
                Prepare_EQO_Basket()
                ResetAll()                                  ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                'If ddlShareEQO3.SelectedItem IsNot Nothing Then
                '    ddlShareEQO3.Text = ddlShareEQO3.SelectedItem.Text
                'End If

                ''ResetAll()
                DisplayEstimatedNotional()
            End If

        Catch ex As Exception
            lblerror.Text = "ddlShareEQO3_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlShareEQO3_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub
    ''COMMENTED Cant have CPTY dependent stocks
    'Public Function checkCodefromAltcode(ByVal strShareEQO As String, ByVal dtShareEQO1 As DataTable) As Boolean
    '    Dim drShareInfo As DataRow() = dtShareEQO1.Select(" AltCode = '" & strShareEQO & "' ")
    '    If drShareInfo(0).Item("Code").ToString <> "" Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Private Sub ddlSideEQO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSideEQO.SelectedIndexChanged
        Try
            lblerror.Text = ""
            clearFields()
            ResetAll()                  ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
            ''ResetAll()
            ' getRange()

        Catch ex As Exception
            lblerror.Text = "ddlSideEQO_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlSideEQO_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txtTenorEQO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTenorEQO.TextChanged
        Try
            lblerror.Text = ""
            If ValidateTenor() = False Then   ''AshwiniP on 09-Nov-2016
                Exit Sub
            Else
                GetDatesForEQO()
                ResetAll()                          ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                Enable_Disable_Deal_Buttons()
                ''ResetAll()
                ''Added by Imran P 12-Nov-2015
                'If txtTenorEQO.Text = "0" Or txtTenorEQO.Text = "" Or Val(txtOrderqtyEQO.Text) = 0 Then
                '    lblEstimatedNotional.Text = ""
                '    lblEstimatedNoOfDays.Text = ""
                'Else
                '    DisplayEstimatedNotional()
                'End If
                ''Added by Imran P 12-Nov-2015
            End If
        Catch ex As Exception
            lblerror.Text = "txtTenorEQO_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtTenorEQO_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    ''AshwiniP on 09-Nov-2016
    Public Sub Disablebuttons()
        Try
            btnSolveAll.Enabled = False
            btnSolveAll.CssClass = "btnDisabled"
            btnBNPPPrice.Enabled = False
            btnBNPPPrice.CssClass = "btnDisabled"
        Catch ex As Exception
            lblerror.Text = "Disablebuttons:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtTenor_TextChanged", ErrorLevel.High)
        End Try
    End Sub
    ''AshwiniP on 09-Nov-2016: Config to allow tenor in months <START>
    Public Function ValidateTenor() As Boolean
        Try


            Dim interval1 As String = ddlTenorEQO.SelectedValue
            If interval1 = "MONTH" Then
                interval1 = "M"
            ElseIf interval1 = "YEAR" Then
                interval1 = "Y"
            End If
            Dim monthcount As Integer = 0
            If interval1 = "Y" And CDbl(txtTenorEQO.Text) = 1 Then
                monthcount = 12
            ElseIf interval1 = "Y" And CDbl(txtTenorEQO.Text) <> 1 Then
                lblerror.Text = "Please enter valid tenor."
                Disablebuttons()
                Exit Function
            Else
                monthcount = CInt(txtTenorEQO.Text)
            End If
            Dim max_months As Integer = 0

            max_months = CInt(objReadConfig.ReadConfig(dsConfig, "EQO_AllowedTenorInMonths", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "12").Trim.ToUpper())
           
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

    Private Sub ddlTenorEQO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTenorEQO.SelectedIndexChanged
        Try
            lblerror.Text = ""
            If ValidateTenor() = False Then   ''AshwiniP on 09-Nov-2016
                Exit Sub
            Else
                GetDatesForEQO()
                ResetAll()                              ''Sequence changed by AshwiniP on 24-Oct-2016
                GetCommentary_EQO()
                Enable_Disable_Deal_Buttons()
                ''ResetAll()
                ''Added by Imran P 12-Nov-2015
                'If txtTenorEQO.Text = "0" Or txtTenorEQO.Text = "" Or Val(txtOrderqtyEQO.Text) = 0 Then
                '    lblEstimatedNotional.Text = ""
                '    lblEstimatedNoOfDays.Text = ""
                'Else
                '    DisplayEstimatedNotional()
                'End If
                ''Added by Imran P 12-Nov-2015
            End If
        Catch ex As Exception
            lblerror.Text = "ddlTenorEQO_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlTenorEQO_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub ddlStrikeTypeEQO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStrikeTypeEQO.SelectedIndexChanged
        Try
            lblerror.Text = ""
            ResetAll()                      ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
            Enable_Disable_Deal_Buttons()
            ''ResetAll()
        Catch ex As Exception
            lblerror.Text = "ddlStrikeTypeEQO_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlStrikeTypeEQO_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txtStrikeEQO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStrikeEQO.TextChanged
        Try
            lblerror.Text = ""
            ResetAll()                          ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
            txtStrikeEQO.Text = SetNumberFormat(txtStrikeEQO.Text, 2)
           '' ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtStrikeEQO_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtStrikeEQO_TextChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub ddlSettlCcyEQO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSettlCcyEQO.SelectedIndexChanged
        Try
            lblerror.Text = ""
            ResetAll()                  ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
            ''ResetAll()
        Catch ex As Exception
            lblerror.Text = "ddlSettlCcyEQO_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlSettlCcyEQO_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub ddlsettlmethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlsettlmethod.SelectedIndexChanged
        Try
            'Added by Mohit Lalwani on 21-Apr-2016
            Select Case objReadConfig.ReadConfig(dsConfig, "EQO_DisableQuanto", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                Case "Y", "YES"
                    ddlSettlCcyEQO.Enabled = False
                    ddlSettlCcyEQO.BackColor = Color.FromArgb(242, 242, 243)
                    ddlSettlCcyEQO.SelectedValue = lblEQOBaseCcy.Text
                    If ddlsettlmethod.SelectedValue = "Cash" Then 
                    Else
                        ddlSettlCcyEQO.SelectedValue = lblEQOBaseCcy.Text
                    End If
                Case "N", "NO"
                    If ddlsettlmethod.SelectedValue = "Cash" Then               'Mohit Lalwani on 13-Apr-2016
                        ddlSettlCcyEQO.Enabled = True
                        ddlSettlCcyEQO.BackColor = Color.White
                    Else
                        ddlSettlCcyEQO.Enabled = False
                        ddlSettlCcyEQO.BackColor = Color.FromArgb(242, 242, 243)
                        If lblEQOBaseCcy.Text <> "" Then
                            ddlSettlCcyEQO.SelectedValue = lblEQOBaseCcy.Text
                        End If
                    End If
            End Select
            '/Added by Mohit Lalwani on 21-Apr-2016
            lblerror.Text = ""
            ResetAll()                      ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
           '' ResetAll()
        Catch ex As Exception
            lblerror.Text = "ddlsettlmethod_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlsettlmethod_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txtSettlDateEQO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSettlDateEQO.TextChanged
        Try
            lblerror.Text = ""
            ResetAll()                              ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
           '' ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtSettlDateEQO_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtSettlDateEQO_TextChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txtMaturityDateEQO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMaturityDateEQO.TextChanged
        Try
            lblerror.Text = ""
            ResetAll()              ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
           '' ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtMaturityDateEQO_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtMaturityDateEQO_TextChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txtExpiryDateEQO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExpiryDateEQO.TextChanged
        Try
            lblerror.Text = ""
            ResetAll()          ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
            ''ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtExpiryDateEQO_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtExpiryDateEQO_TextChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub txtOrderqtyEQO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrderqtyEQO.TextChanged
        Try
            lblerror.Text = ""
            ''GetCommentary_EQO()
            If Qty_Validate(txtOrderqtyEQO.Text) = False Then
                Exit Sub
            End If
            Try
                txtOrderqtyEQO.Text = FinIQApp_Number.ConvertFormattedAmountToNumber(txtOrderqtyEQO.Text).ToString
                txtOrderqtyEQO.Text = SetNumberFormatZeroDecim(txtOrderqtyEQO.Text, 0)
            Catch ex As Exception
                lblerror.Text = "Please enter valid No. Of Shares."
            End Try
            'Added by Imran P 12-Nov-2015
            If txtTenorEQO.Text = "0" Or txtTenorEQO.Text = "" Or Val(txtOrderqtyEQO.Text) = 0 Then
                txtNotional.Text = "0"
                lblEstimatedNoOfDays.Text = ""
                lblEstimatedNotional.Text = "0"
                upnl4.Update()

            Else
                DisplayEstimatedNotional()
            End If
            'Added by Imran P 12-Nov-2015
            ResetAll()                                    ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
        Catch ex As Exception
            lblerror.Text = "txtOrderqtyEQO_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtOrderqtyEQO_TextChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub ddlBarrierEQO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBarrierEQO.SelectedIndexChanged
        Try
            lblerror.Text = ""
            ResetAll()                  ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
            ''ResetAll()
        Catch ex As Exception
            lblerror.Text = "ddlBarrierEQO_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlBarrierEQO_SelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub grdEQORFQ_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdEQORFQ.ItemCommand
        Dim strNewTenorEQO As String = String.Empty
        Dim strNewTenorEQOType As String = String.Empty
        Dim dtBaseCCY As DataTable
        Try
            lblMsgPriceProvider.Text = ""
            lblerror.Text = ""
            dtBaseCCY = New DataTable("BaseCcy")


            If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                If e.CommandName.ToUpper = "SELECT" Then
                    ShowHideConfirmationPopup(False)
                    ResetAll()
                    'grdELNRFQ.SelectedItemStyle.BackColor = Color.FromArgb(56, 99, 148)
                    Dim strExchngforEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Exchange).Text
                    Dim strProductForEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Product).Text
                    '<Added by Mohit Lalwani on 24/aug/2015 >
                    Dim strTypeForEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Option_Type).Text
                    Dim strSecuritySubType As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Security_Sub_Type).Text



                    If strTypeForEQO = "Barrier" Then
                        ddlOptionType.SelectedText = "KnockIn Put"
                    ElseIf strTypeForEQO = "European" And strSecuritySubType = "Call" Then
                        ddlOptionType.SelectedText = "European Call"

                    ElseIf strTypeForEQO = "European" And strSecuritySubType = "Put" Then
                        ddlOptionType.SelectedText = "European Put"

                    ElseIf strTypeForEQO = "American" And strSecuritySubType = "Call" Then
                        ddlOptionType.SelectedText = "American Call" 'Mohit Lalwani on 30-Aug-2016

                    ElseIf strTypeForEQO = "American" And strSecuritySubType = "Put" Then
                        ddlOptionType.SelectedText = "American Put"
                    End If
                    ddlOptionType_SelectedIndexChanged(Nothing, Nothing)
                    Dim arrExchanges As String()
                    Dim arrShares As String()
                    '         Dim dtBaseCCY As DataTable

                    arrExchanges = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Exchange).Text.Split(CChar(","))
                    arrShares = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Underlying).Text.Split(CChar(","))


                    '<Added by Mohit Lalwani on 24/aug/2015 >
                    ddlProductEQO.SelectedIndex = ddlProductEQO.Items.IndexOf(ddlProductEQO.Items.FindByText(grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Product).Text))

                    ''''''''If arrExchanges.Length > 0 Then
                    '' '' '' ''ddlExchangeEQO.SelectedIndex = ddlExchangeEQO.Items.IndexOf(ddlExchangeEQO.Items.FindByValue(arrExchanges(0)))
                    '' '' '' ''ddlExchangeEQO_SelectedIndexChanged(Nothing, Nothing)
                    '' '' '' ''ddlShareEQO.SelectedIndex = ddlShareEQO.Items.IndexOf(ddlShareEQO.Items.FindItemByValue(arrShares(0)))
                    '' '' '' ''ddlShareEQO_SelectedIndexChanged(Nothing, Nothing)
                    ' '' '' '' ''If arrExchanges.Length = 1 Then
                    ' '' '' '' ''    chkAddShareEQO3.Checked = False
                    ' '' '' '' ''    chkAddShareEQO3_CheckedChanged(Nothing, Nothing)
                    ' '' '' '' ''    chkAddShareEQO2.Checked = False
                    ' '' '' '' ''    chkAddShareEQO2_CheckedChanged(Nothing, Nothing)
                    ' '' '' '' ''End If
                    ' '' '' '' ''If arrExchanges.Length >= 2 Then
                    ' '' '' '' ''    chkAddShareEQO2.Checked = True
                    ' '' '' '' ''    chkAddShareEQO2_CheckedChanged(Nothing, Nothing)

                    ' '' '' '' ''    ddlExchangeEQO2.SelectedIndex = ddlExchangeEQO2.Items.IndexOf(ddlExchangeEQO2.Items.FindByValue(arrExchanges(1)))
                    ' '' '' '' ''    ddlExchangeEQO2_SelectedIndexChanged(Nothing, Nothing)
                    ' '' '' '' ''    ddlShareEQO2.SelectedIndex = ddlShareEQO2.Items.IndexOf(ddlShareEQO2.Items.FindItemByValue(arrShares(1)))
                    ' '' '' '' ''    ddlShareEQO2_SelectedIndexChanged(Nothing, Nothing)
                    ' '' '' '' ''    If arrExchanges.Length = 2 Then
                    ' '' '' '' ''        chkAddShareEQO3.Checked = False
                    ' '' '' '' ''        chkAddShareEQO3_CheckedChanged(Nothing, Nothing)
                    ' '' '' '' ''    End If
                    ' '' '' '' ''    If arrExchanges.Length = 3 Then
                    ' '' '' '' ''        chkAddShareEQO3.Checked = True
                    ' '' '' '' ''        chkAddShareEQO3_CheckedChanged(Nothing, Nothing)
                    ' '' '' '' ''        ddlExchangeEQO3.SelectedIndex = ddlExchangeEQO3.Items.IndexOf(ddlExchangeEQO3.Items.FindByValue(arrExchanges(2)))
                    ' '' '' '' ''        ddlExchangeEQO3_SelectedIndexChanged(Nothing, Nothing)
                    ' '' '' '' ''        ddlShareEQO3.SelectedIndex = ddlShareEQO3.Items.IndexOf(ddlShareEQO3.Items.FindItemByValue(arrShares(2)))
                    ' '' '' '' ''        ddlShareEQO3_SelectedIndexChanged(Nothing, Nothing)
                    ' '' '' '' ''    End If
                    ' '' '' '' ''End If


                    ''''''''End If

                    ''-----------------------------Go-------------------------
                    If arrExchanges.Length > 0 Then
                        chkAddShare1.Checked = True
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQO_AllowBasket", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                            Case "Y", "YES"
                                chkAddShare1_CheckedChanged(Nothing, Nothing)
                            Case "N", "NO"

                        End Select
                        ddlExchangeEQO.Enabled = True
                        ddlShareEQO.Enabled = True
                        FillDRAddl_exchange(ddlExchangeEQO)
                        setShare1(arrExchanges(0), arrShares(0))
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            Case "Y", "YES"
                                '' ddlShareDRA.SelectedIndex = ddlShareDRA.Items.IndexOf(ddlShareDRA.Items.FindItemByValue(arrShares(0)))
                                ''   ddlShareDRA.Text = ddlShareDRA.Text
                                lblExchangeEQO.Text = setExchangeByShare(ddlShareEQO)
                            Case "N", "NO"
                                Try
                                    ddlExchangeEQO.SelectedValue = arrExchanges(0)
                                Catch ex As Exception
                                    lblerror.Text = "Exchange missing from setup."
                                End Try
                                ''fill_DRA_FCN_Share()
                                ''ddlShareDRA.SelectedIndex = ddlShareDRA.Items.IndexOf(ddlShareDRA.Items.FindItemByValue(arrShares(0)))
                                ''   ddlShareDRA.Text = ddlShareDRA.Text
                                lblExchangeEQO.Text = setExchangeByShare(ddlShareEQO)
                        End Select
                        If ddlShareEQO.SelectedItem IsNot Nothing Then
                            ddlShareEQO.Text = ddlShareEQO.SelectedItem.Text
                        End If
                        dtBaseCCY = New DataTable("BaseCcy")
                        ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                        Select Case objELNRFQ.DB_GetBASECCY(ddlShareEQO.SelectedValue, dtBaseCCY)
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                lblEQOBaseCcy.Text = dtBaseCCY.Rows(0)(0).ToString
                            Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                            Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                        End Select
                        If arrExchanges.Length >= 2 Then
                            chkAddShare2.Checked = True
                            chkAddShare2_CheckedChanged(Nothing, Nothing)
                            ddlExchangeEQO2.Enabled = True
                            ddlShareEQO2.Enabled = True
                            FillDRAddl_exchange(ddlExchangeEQO2)
                            setShare2(arrExchanges(1), arrShares(1))
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                    ''ddlShareDRA2.SelectedIndex = ddlShareDRA2.Items.IndexOf(ddlShareDRA2.Items.FindItemByValue(arrShares(1)))
                                    ''   ddlShareDRA2.Text = ddlShareDRA2.Text
                                    lblExchangeEQO2.Text = setExchangeByShare(ddlShareEQO2)
                                Case "N", "NO"
                                    Try
                                        ddlExchangeEQO2.SelectedValue = arrExchanges(1)
                                    Catch ex As Exception
                                        lblerror.Text = "Exchange missing from setup."
                                    End Try
                                    'fill_DRA_FCN_Share2()  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                    ''ddlShareDRA2.SelectedIndex = ddlShareDRA2.Items.IndexOf(ddlShareDRA2.Items.FindItemByValue(arrShares(1)))
                                    ''    ddlShareDRA2.Text = ddlShareDRA2.Text
                                    lblExchangeEQO2.Text = setExchangeByShare(ddlShareEQO2)
                            End Select
                            If ddlShareEQO2.SelectedItem IsNot Nothing Then
                                ddlShareEQO2.Text = ddlShareEQO2.SelectedItem.Text
                            End If
                            dtBaseCCY = New DataTable("BaseCcy")
                            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            Select Case objELNRFQ.DB_GetBASECCY(ddlShareEQO2.SelectedValue, dtBaseCCY)
                                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                    lblBaseCurrency2.Text = dtBaseCCY.Rows(0)(0).ToString
                                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                            End Select
                        End If

                        If arrExchanges.Length = 3 Then
                            chkAddShare3.Checked = True
                            ddlExchangeEQO3.Enabled = True
                            ddlShareEQO3.Enabled = True
                            FillDRAddl_exchange(ddlExchangeEQO3)
                            setShare3(arrExchanges(2), arrShares(2))
                            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                                Case "Y", "YES"
                                    'fill_DRA_FCN_Share3()  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                    ''ddlShareDRA3.SelectedIndex = ddlShareDRA3.Items.IndexOf(ddlShareDRA3.Items.FindItemByValue(arrShares(2)))
                                    ''    ddlShareDRA3.Text = ddlShareDRA3.Text
                                    lblExchangeEQO3.Text = setExchangeByShare(ddlShareEQO3)
                                Case "N", "NO"
                                    Try
                                        ddlExchangeEQO3.SelectedValue = arrExchanges(2)
                                    Catch ex As Exception
                                        lblerror.Text = "Exchange missing from setup."
                                    End Try
                                    'fill_DRA_FCN_Share3()  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                    ''ddlShareDRA3.SelectedIndex = ddlShareDRA3.Items.IndexOf(ddlShareDRA3.Items.FindItemByValue(arrShares(2)))
                                    ''   ddlShareDRA3.Text = ddlShareDRA3.Text
                                    lblExchangeEQO3.Text = setExchangeByShare(ddlShareEQO3)
                                    dtBaseCCY = New DataTable("BaseCcy")
                            End Select
                            If ddlShareEQO3.SelectedItem IsNot Nothing Then
                                ddlShareEQO3.Text = ddlShareEQO3.SelectedItem.Text
                            End If
                            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            Select Case objELNRFQ.DB_GetBASECCY(ddlShareEQO3.SelectedValue, dtBaseCCY)
                                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                                    lblBaseCurrency3.Text = dtBaseCCY.Rows(0)(0).ToString
                                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                            End Select
                        End If
                    End If
                    ''-----------------------------End------------------------

                    ''getCurrency(grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Underlying).Text)

                    Dim strTenorforEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Tenor).Text
                    txtTenorEQO.Text = strTenorforEQO
                    For i = 0 To strTenorforEQO.Length - 1
                        If IsNumeric(strTenorforEQO.Substring(i, 1)) = True Then
                            strNewTenorEQO = strNewTenorEQO + strTenorforEQO.Substring(i, 1)
                        End If
                    Next
                    txtTenorEQO.Text = strNewTenorEQO
                    For i = 0 To strTenorforEQO.Length - 1
                        If IsNumeric(strTenorforEQO.Substring(i, 1)) = False Then
                            strNewTenorEQOType = strNewTenorEQOType + strTenorforEQO.Substring(i, 1)
                        End If
                    Next
                    '<Changed by Mohit Lalwani on 17-Nov-2015>
                    'ddlTenorEQO.SelectedItem.Text = strNewTenorEQOType
                    ddlTenorEQO.SelectedValue = strNewTenorEQOType.ToUpper.Trim

                    'ddlTenorEQO.SelectedIndex = ddlTenorEQO.Items.IndexOf(ddlTenorEQO.Items.FindByText(strNewTenorEQOType))
                    '</Changed by Mohit Lalwani on 17-Nov-2015>
                    ''''''''''''''''''
                    Dim strTypeStrike As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Strike_Type).Text


                    If strTypeStrike = "Percentage" Then
                        ddlStrikeTypeEQO.SelectedIndex = ddlStrikeTypeEQO.FindItemByText("Strike(%)").Index
                    Else
                        ddlStrikeTypeEQO.SelectedIndex = ddlStrikeTypeEQO.FindItemByText("Strike").Index
                    End If

                    txtStrikeEQO.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Strike).Text
                    txtPremium.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Premium).Text
                    txtUpfrontEQO.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Upfront).Text

                    If grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Barrier).Text <> "&nbsp;" Then
                        If grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Barrier).Text.Contains("%") Then
                            txtBarrierLevelEQO.Text = (grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Barrier).Text).Replace("%", "").Trim
                            ddlBarrierEQO.SelectedValue = "Percentage"
                        Else
                            txtBarrierLevelEQO.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Barrier).Text
                            ddlBarrierEQO.SelectedValue = "Absolute"
                        End If
                    End If

                    Dim strBarrierType As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.BarrType).Text
                    ddlBarrierMonitoringType.SelectedIndex = ddlBarrierMonitoringType.Items.IndexOf(ddlBarrierMonitoringType.Items.FindByText(strBarrierType))

                    ''  ddlBarrierEQO.SelectedValue = strSolveForEQO
                    '''''''''''''''''''''''''''''''''
                    Dim strTradeDateEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Trade_Date).Text

                    If strTradeDateEQO = Today.ToString("dd-MMM-yy") Then
                        txtTradeDateEQO.Value = Convert.ToDateTime(strTradeDateEQO).ToString("dd-MMM-yyyy")

                        Dim strSettDateEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Settlement_Date).Text
                        txtSettlDateEQO.Value = Convert.ToDateTime(strSettDateEQO).ToString("dd-MMM-yyyy")

                        Dim strExpDateEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Expiry_Date).Text
                        txtExpiryDateEQO.Value = Convert.ToDateTime(strExpDateEQO).ToString("dd-MMM-yyyy")

                        Dim strMatDateEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Maturity_Date).Text
                        txtMaturityDateEQO.Value = Convert.ToDateTime(strMatDateEQO).ToString("dd-MMM-yyyy")
                        '''Added to get maturity days as per the exchange from grid selection
                        '''When EQO go for basket need to check on which exchange we need to set settlement days
                        Dim strMaturityDays As String
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allow_ALL_AsExchangeOption", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                            Case "Y", "YES"
                                strMaturityDays = getMaturityDays(arrShares(0))
                            Case "N", "NO"
                                Try
                                    strMaturityDays = getMaturityDays(arrExchanges(0))
                                Catch ex As Exception
                                    lblerror.Text = "Exchange missing from setup."
                                End Try
                        End Select
                        txtSettlDays.Text = strMaturityDays

                    Else
                        GetDatesForEQO()
                    End If

                    Dim strSolveForEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Solve_For).Text
                    ddlSolveforEQO.SelectedValue = strSolveForEQO
                    ddlSolveforEQO_SelectedIndexChanged(Nothing, Nothing)
                    Dim strSideForEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Client_side).Text
                    ddlSideEQO.SelectedValue = strSideForEQO




                    Dim strSettCcyForEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Settl_Ccy).Text
                    ddlSettlCcyEQO.SelectedValue = strSettCcyForEQO
                    fillddlInvestCcy()
                    If ddlInvestCcy.Visible = True Then
                        ddlInvestCcy.SelectedValue = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Underlying_Ccy).Text         'Mohit Lalwani on 22-Apr-2016
                    Else
                        ddlInvestCcy.SelectedValue = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Underlying_Ccy).Text
                    End If
                    Dim strSettlTypeForEQO As String = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Settl_Type).Text
                    ddlsettlmethod.SelectedValue = strSettlTypeForEQO

                    If ddlsettlmethod.SelectedValue = "Physical" Then
                        ddlSettlCcyEQO.Enabled = False
                        ddlSettlCcyEQO.BackColor = Color.FromArgb(242, 242, 243)
                    Else
                        ddlSettlCcyEQO.Enabled = True
                        ddlSettlCcyEQO.BackColor = Color.White
                    End If

                    chkrdbQuantity()
                    Dim strOrderqtyEQO As String = SetNumberFormat(grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Order_Qty).Text, 2)
                    'Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options
                    If grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.EQO_Quantity_Type).Text.ToUpper = "SHARES" Then
                        'rdbQuantity.Checked = True
                        'rdbNotional.Checked = False
                        'chkrdbQuantity()
                        'txtOrderqtyEQO.Enabled = True
                        'txtOrderqtyEQO.BackColor = Color.White
                        'txtNotional.Enabled = False
                        'txtOrderqtyEQO.Text = strOrderqtyEQO
                        'txtOrderqtyEQO_TextChanged(Nothing, Nothing)
                        Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                            Case "BOTH"
                                'tdTitleNotional.Visible = True
                                'tdControlNotional.Visible = True
                                'tdControlNumberOfShares.Visible = True
                                'tdTitleNumberOfShares.Visible = True


                                rdbQuantity.Checked = True
                                rdbNotional.Checked = False

                                rdbQuantity.Visible = True
                                txtOrderqtyEQO.Visible = True
                                lblOrderqtyEQO.Visible = True
                                lblTitleNotional.Visible = True
                                rdbNotional.Visible = True
                                txtNotional.Visible = True
                                chkrdbQuantity()
                                txtOrderqtyEQO.Enabled = True
                                txtOrderqtyEQO.BackColor = Color.White
                                txtNotional.Enabled = False
                                txtOrderqtyEQO.Text = strOrderqtyEQO
                                txtOrderqtyEQO_TextChanged(Nothing, Nothing)

                            Case "NOTIONAL"
                                'tdTitleNotional.Visible = True
                                'tdControlNotional.Visible = True
                                'tdControlNumberOfShares.Visible = False
                                'tdTitleNumberOfShares.Visible = False
                                'txtOrderqtyEQO.Text = ""
                                'txtNotional.Text = "1,000,000"
                                'lblerror.Text = "Order quantity can not be in number of shares."

                                rdbQuantity.Checked = False
                                rdbQuantity.Enabled = False
                                rdbNotional.Checked = True

                                rdbQuantity.Visible = False
                                txtOrderqtyEQO.Visible = False
                                lblOrderqtyEQO.Visible = False
                                lblTitleNotional.Visible = True
                                rdbNotional.Visible = True
                                txtNotional.Visible = True
                                chkrdbQuantity()
                                txtOrderqtyEQO.Enabled = False
                                txtNotional.BackColor = Color.White
                                txtNotional.Enabled = True
                                txtOrderqtyEQO.Text = ""
                                txtNotional.Text = getControlPersonalSetting("Notional", "1,000,000")
                                txtNotional_TextChanged(Nothing, Nothing)
                                lblerror.Text = "Order quantity can not be in number of shares."
                            Case "NOOFSHARE"
                                'tdTitleNotional.Visible = False
                                'tdControlNotional.Visible = False
                                'tdControlNumberOfShares.Visible = True
                                'tdTitleNumberOfShares.Visible = True
                                'txtOrderqtyEQO.Text = strOrderqtyEQO

                                rdbQuantity.Checked = True
                                rdbNotional.Checked = False
                                rdbNotional.Enabled = False

                                rdbQuantity.Visible = True
                                txtOrderqtyEQO.Visible = True
                                lblOrderqtyEQO.Visible = True
                                lblTitleNotional.Visible = False
                                rdbNotional.Visible = False
                                txtNotional.Visible = False
                                chkrdbQuantity()
                                txtOrderqtyEQO.Enabled = True
                                txtOrderqtyEQO.BackColor = Color.White
                                txtNotional.Enabled = False
                                txtOrderqtyEQO.Text = strOrderqtyEQO
                                txtOrderqtyEQO_TextChanged(Nothing, Nothing)

                        End Select


                    Else
                        'rdbNotional.Checked = True
                        'rdbQuantity.Checked = False
                        'chkrdbQuantity()
                        'lblEstimatedNotional.Text = ""
                        'txtNotional.Text = strOrderqtyEQO
                        'txtOrderqtyEQO.Enabled = False
                        'txtNotional.Enabled = True
                        'txtNotional.BackColor = Color.White

                        Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                            Case "BOTH"
                                'tdTitleNotional.Visible = True
                                'tdControlNotional.Visible = True
                                'tdControlNumberOfShares.Visible = True
                                'tdTitleNumberOfShares.Visible = True

                                rdbNotional.Checked = True
                                rdbQuantity.Checked = False
                                chkrdbQuantity()
                                lblEstimatedNotional.Text = ""
                                txtNotional.Text = strOrderqtyEQO
                                txtOrderqtyEQO.Enabled = False
                                txtNotional.Enabled = True
                                txtNotional.BackColor = Color.White
                            Case "NOTIONAL"
                                'tdTitleNotional.Visible = True
                                'tdControlNotional.Visible = True
                                'tdControlNumberOfShares.Visible = False
                                'tdTitleNumberOfShares.Visible = False
                                'txtNotional.Text = strOrderqtyEQO


                                rdbQuantity.Checked = False
                                rdbQuantity.Enabled = False
                                rdbNotional.Checked = True

                                chkrdbQuantity()
                                txtOrderqtyEQO.Enabled = False
                                txtNotional.BackColor = Color.White
                                txtNotional.Enabled = True
                                txtOrderqtyEQO.Text = ""
                                txtNotional.Text = strOrderqtyEQO
                                txtNotional_TextChanged(Nothing, Nothing)
                            Case "NOOFSHARE"
                                'tdTitleNotional.Visible = False
                                'tdControlNotional.Visible = False
                                'tdControlNumberOfShares.Visible = True
                                'tdTitleNumberOfShares.Visible = True
                                'txtOrderqtyEQO.Text = "2,000"
                                'txtNotional.Text = ""
                                'lblerror.Text = "Order quantity can not be in notional."

                                rdbQuantity.Checked = True
                                rdbNotional.Checked = False
                                rdbNotional.Enabled = False
                                chkrdbQuantity()
                                txtOrderqtyEQO.Enabled = True
                                txtOrderqtyEQO.BackColor = Color.White
                                txtNotional.Enabled = False
                                txtOrderqtyEQO.Text = getControlPersonalSetting("No. of shares", "2,000")
                                txtOrderqtyEQO_TextChanged(Nothing, Nothing)
                                lblerror.Text = "Order quantity can not be in notional."

                        End Select
                    End If
                    '/Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options
                    '  rdbQuantity_CheckedChanged(Nothing, Nothing)
                ElseIf e.CommandName.ToUpper = "GETRFQDETAILS" Then
                    ShowHideDeatils(True)
                    lblDetails.Text = "RFQ Details"
                    pnlDetailsPopup.Visible = True
                    trStatus.Visible = False
                    trOrderType.Visible = False
                    trSpot.Visible = False
                    trExePrc1.Visible = False
                    trAvgExePrc.Visible = False
                    trQuoteStatus.Visible = True

                    '''''''''''''''''''''''''''''''''''
                    lblAlloRFQID.Text = ""
                    lclAlloCP.Text = ""
                    lblAlloClientPrice.Text = ""
                    lblAlloExpiDt.Text = ""
                    lblAlloKO.Text = ""
                    lblAlloMatuDt.Text = ""
                    lblAlloSettlCcy.Text = ""       'Mohit Lalwani added on 14-Apr-2016
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
                    'lblAlloYield.Text = ""
                    lblAlloOrderStatus.Text = ""
                    lblAlloOrderStatus.Text = ""
                    lblAlloExePrc1.Text = ""
                    lblAlloAvgExePrc.Text = ""
                    lblAlloSpot.Text = ""
                    lblValAlloSolvefor.Text = ""
                    lblValQuoteStatus.Text = ""
                    lblClientSide.Text = ""
                    lblFixingType.Text = ""
                    lblOptionType.Text = ""
                    lblAlloStrikeType.Text = ""
                    lblAlloKOType.Text = ""
                    ''</added by Rushikesh on 29-Dec-15 to flush old value>
                    '  grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Trade_Date)
                    lblAlloRFQID.Text = DirectCast(DirectCast(grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.RFQ_ID).Controls(1), System.Web.UI.Control), System.Web.UI.WebControls.LinkButton).Text
                    lclAlloCP.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Provider).Text
                    trClientPremium.Visible = False
                    lblAlloClientPrice.Text = ""
                    lblAlloExpiDt.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Expiry_Date).Text
                    ' lblAlloYield.Text = SetNumberFormat(grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Expiry_Date).Text, 2)
                    'Mohit 28-Dec-2015
                    If grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Barrier).Text.Trim <> "" And grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Barrier).Text.Trim <> "&nbsp;" And grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Barrier).Text.Trim <> "0" Then
                        lblAlloKO.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Barrier).Text
                    Else
                    End If
                    lblAlloMatuDt.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Maturity_Date).Text

                    lblAlloSettlCcy.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Settl_Ccy).Text

                    lblAlloNoteCcy.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Underlying_Ccy).Text            'Mohit Lalwani on 14-Apr-2016 

                    If grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.EQO_Quantity_Type).Text = "SHARES" Then
                        Label21.Text = "No. of Shares"
                    Else
                        Label21.Text = "Notional"
                    End If

                    Label24.Text = "Issuer Remark"


                    lblAlloOrderSize.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Order_Qty).Text
                    'lblalloOrderType.Text = item("ELN_Order_Type").Text
                    lblAlloPrice.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Premium).Text
                    lblAlloRemark.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Remark).Text
                    lblAlloSettDt.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Settlement_Date).Text
                    lblAlloSettWk.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Value).Text
                    ''lblAlloSpot.Text = item("").Text '' Need to discuss.
                    lblAlloStrike.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Strike).Text
                    lblAlloSubmitteddBy.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Created_By).Text
                    lblAlloTenor.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Tenor).Text '+ "&nbsp;Month(s)"
                    lblAlloTradeDt.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Trade_Date).Text
                    lblAlloUnderlying.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Underlying).Text
                    lblValAlloSolvefor.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Solve_For).Text
                    lblAlloUpfront.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Upfront).Text
                    lblClientSide.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Client_side).Text
                    lblFixingType.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Option_Type).Text
                    lblOptionType.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Security_Sub_Type).Text
                    lblAlloStrikeType.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Strike_Type).Text
                    lblAlloKOType.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.BarrType).Text
                    lblSettlementMethod.Text = grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.Settl_Type).Text
                    upnlDetails.Update()
                    RestoreAll()
                    RestoreSolveAll()
                    upnlGrid.Update()
                    '<RiddhiS. on 19-Oct-2016: To generate Pre-trade termsheet>
                ElseIf e.CommandName.ToUpper = "GENERATEDOCUMENT" Then
                    Dim RFQID As String = DirectCast(DirectCast(grdEQORFQ.Items(e.Item.ItemIndex).Cells(grdEQORFQEnum.RFQ_ID).Controls(1), System.Web.UI.Control), System.Web.UI.WebControls.LinkButton).Text
                    Generate_EQO(RFQID)
                End If
            End If
            upnl4.Update()
            Prepare_EQO_Basket()

            ''commentry

        Catch ex As Exception
            lblerror.Text = "grdEQORFQ_ItemCommand:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdEQORFQ_ItemCommand", ErrorLevel.High)
        End Try
    End Sub

    Private Sub grdEQORFQ_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdEQORFQ.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.EditItem Or e.Item.ItemType = ListItemType.SelectedItem Then

                'If e.Item.Cells(grdEQORFQEnum.Barrier).Text <> "&nbsp;" And e.Item.Cells(grdEQORFQEnum.Barrier).Text <> "" Then
                '    e.Item.Cells(grdEQORFQEnum.Barrier).Text = SetNumberFormat(CDbl(e.Item.Cells(grdEQORFQEnum.Barrier).Text) * 100, 2)
                'End If
                If e.Item.Cells(grdEQORFQEnum.Trade_Date).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Trade_Date).Text = Format(CDate(e.Item.Cells(grdEQORFQEnum.Trade_Date).Text), "dd-MMM-yy")
                End If
                If e.Item.Cells(grdEQORFQEnum.Settlement_Date).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Settlement_Date).Text = Format(CDate(e.Item.Cells(grdEQORFQEnum.Settlement_Date).Text), "dd-MMM-yy")
                End If

                If e.Item.Cells(grdEQORFQEnum.Expiry_Date).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Expiry_Date).Text = Format(CDate(e.Item.Cells(grdEQORFQEnum.Expiry_Date).Text), "dd-MMM-yy")
                End If

                If e.Item.Cells(grdEQORFQEnum.Maturity_Date).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Maturity_Date).Text = Format(CDate(e.Item.Cells(grdEQORFQEnum.Maturity_Date).Text), "dd-MMM-yy")
                End If

                If e.Item.Cells(grdEQORFQEnum.Quote_Requested_At).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Quote_Requested_At).Text = Format(CDate(e.Item.Cells(grdEQORFQEnum.Quote_Requested_At).Text), "dd-MMM-yyyy hh:mm:ss tt")
                End If
                If e.Item.Cells(grdEQORFQEnum.Premium).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Premium).Text = SetNumberFormat(e.Item.Cells(grdEQORFQEnum.Premium).Text, 4)
                End If
                If e.Item.Cells(grdEQORFQEnum.Strike).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Strike).Text = SetNumberFormat(e.Item.Cells(grdEQORFQEnum.Strike).Text, 2)
                End If
                If e.Item.Cells(grdEQORFQEnum.Order_Qty).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Order_Qty).Text = SetNumberFormat(e.Item.Cells(grdEQORFQEnum.Order_Qty).Text, 2)
                End If
                'If e.Item.Cells(grdEQORFQEnum.Nominal_Amount).Text <> "&nbsp;" Then
                '    e.Item.Cells(grdEQORFQEnum.Nominal_Amount).Text = SetNumberFormatZeroDecim(e.Item.Cells(grdEQORFQEnum.Nominal_Amount).Text, 0)
                'End If
                If e.Item.Cells(grdEQORFQEnum.Premium).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Premium).Text = SetNumberFormat(e.Item.Cells(grdEQORFQEnum.Premium).Text, 4)
                End If
                If e.Item.Cells(grdEQORFQEnum.Order_Qty).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Order_Qty).Text = SetNumberFormat(e.Item.Cells(grdEQORFQEnum.Order_Qty).Text, 0)
                End If
                If e.Item.Cells(grdEQORFQEnum.Upfront).Text <> "&nbsp;" Then
                    e.Item.Cells(grdEQORFQEnum.Upfront).Text = (CDbl(e.Item.Cells(grdEQORFQEnum.Upfront).Text) / 100).ToString
                End If
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowRFQByClubbing_OnPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                    Case "Y", "YES"
                        If e.Item.ItemIndex = 0 Then
                            e.Item.CssClass = "Grp1"
                        Else
                            If (e.Item.Cells(grdEQORFQEnum.ClubbingRFQId).Text.ToString = "" Or e.Item.Cells(grdEQORFQEnum.ClubbingRFQId).Text.ToString = "&nbsp;") And (grdEQORFQ.Items(e.Item.ItemIndex - 1).Cells(grdEQORFQEnum.ClubbingRFQId).Text.ToString = "" Or grdEQORFQ.Items(e.Item.ItemIndex - 1).Cells(grdEQORFQEnum.ClubbingRFQId).Text.ToString = "&nbsp;") Then
                                e.Item.CssClass = If(grdEQORFQ.Items(e.Item.ItemIndex - 1).CssClass = "Grp2", "Grp1", "Grp2")
                            Else
                                If (e.Item.Cells(grdEQORFQEnum.ClubbingRFQId).Text.ToString = grdEQORFQ.Items(e.Item.ItemIndex - 1).Cells(grdEQORFQEnum.ClubbingRFQId).Text.ToString) Then
                                    e.Item.CssClass = grdEQORFQ.Items(e.Item.ItemIndex - 1).CssClass
                                Else
                                    e.Item.CssClass = If(grdEQORFQ.Items(e.Item.ItemIndex - 1).CssClass = "Grp2", "Grp1", "Grp2")
                                End If
                            End If

                        End If
                    Case "N", "NO"
                End Select
            End If

        Catch ex As Exception
            lblerror.Text = "grdEQORFQ_ItemDataBound:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdEQORFQ_ItemDataBound", ErrorLevel.High)
        End Try
    End Sub


    Private Sub rblShareData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblShareData.SelectedIndexChanged
        If rblShareData.SelectedValue = "SHAREDATA" Then
            rowGraphData.Visible = False
            rowShareData.Visible = True
            Fill_All_Charts()
        Else
            rowGraphData.Visible = True
            rowShareData.Visible = False
        End If
    End Sub


    'Private Sub chkAddShareEQO2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAddShareEQO2.CheckedChanged
    '    Try
    '        lblerror.Text = ""
    '        If chkAddShareEQO2.Checked Then
    '            FillEQOddl_exchange(ddlExchangeEQO2)
    '            ddlExchangeEQO2.SelectedValue = ddlExchangeEQO.SelectedValue

    '            fill_EQO_Shares(ddlExchangeEQO2.SelectedValue, ddlShareEQO2)
    '            If ddlShareEQO2.SelectedItem Is Nothing Then
    '                lblBaseCurrencyEQO2.Text = ""
    '                'txtSearchShareEQO2.Text = ""
    '                txtUnderlyingBasketEQO.Text = ""
    '            Else
    '                lblBaseCurrencyEQO2.Text = getBaseCurrency(ddlShareEQO2.SelectedItem.Value)
    '                'txtSearchShareEQO2.Text = ddlShareEQO2.SelectedValue
    '            End If
    '            ddlExchangeEQO2_SelectedIndexChanged(sender, e)
    '            ddlShareEQO2.Enabled = True
    '            ddlExchangeEQO2.Enabled = True
    '            tblEQOShare3.Visible = True
    '            chkAddShareEQO3.Enabled = True
    '            'chkAddShareEQO3_CheckedChanged(Nothing, Nothing)
    '            ddlStrikeTypeEQO.Enabled = False
    '            ddlStrikeTypeEQO.BackColor = Color.FromArgb(242, 242, 243)
    '            ddlStrikeTypeEQO.SelectedIndex = 0
    '            ddlBarrierEQO.Enabled = False
    '            ddlBarrierEQO.BackColor = Color.FromArgb(242, 242, 243)
    '            ddlBarrierEQO.SelectedIndex = 0
    '        Else
    '            ddlExchangeEQO2.Items.Clear()
    '            ddlExchangeEQO2.DataSource = Nothing
    '            ddlExchangeEQO2.DataBind()
    '            ddlExchangeEQO2.Enabled = False
    '            ddlShareEQO2.Items.Clear()
    '            ddlShareEQO2.DataSource = Nothing
    '            ddlShareEQO2.DataBind()
    '            ddlShareEQO2.Text = ""
    '            ddlShareEQO2.Enabled = False
    '            'txtSearchShareEQO2.Text = ""
    '            lblBaseCurrencyEQO2.Text = ""
    '            'txtSearchShareEQO2.Enabled = False
    '            chkAddShareEQO3.Checked = False
    '            chkAddShareEQO3.Enabled = False
    '            tblEQOShare3.Visible = False
    '            'tblEQOShare3.Style.Add("opacity", "0")
    '            ddlShareEQO3.Text = ""
    '            chkAddShareEQO3_CheckedChanged(sender, e)
    '            ddlStrikeTypeEQO.Enabled = True
    '            ddlStrikeTypeEQO.BackColor = Color.White
    '            ddlBarrierEQO.Enabled = True
    '            ddlBarrierEQO.BackColor = Color.White
    '        End If


    '        Prepare_EQO_Basket()
    '        ResetAll()
    '        getRange()
    '        fillddlInvestCcy()
    '    Catch ex As Exception
    '        lblerror.Text = "chkAddShareEQO2_CheckedChanged:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "chkAddShareEQO2_CheckedChanged", ErrorLevel.High)

    '    End Try
    'End Sub

    Public Sub fill_EQO_Shares(ByRef exchange As String, ByVal ddl As RadComboBox)
        Dim dtShare As DataTable
        Dim stroptiontype As String = ""
        Try
            dtShare = New DataTable("DUMMY")
            If ddlOptionType.SelectedValue.ToUpper.Contains("KNOCKIN PUT") Then
                stroptiontype = "WOKIP"
            Else
                stroptiontype = "VANI"
            End If


            Select Case objELNRFQ.web_Get_EQO_Share(exchange, ddlProductEQO.SelectedItem.Text, stroptiontype, dtShare)

                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddl
                        .DataSource = dtShare
                        .DataTextField = "UnderlyingSecurityDesc"
                        ''.DataValueField = "AltCode"  -- Changed to Short Description instead of code
                        .DataValueField = "AltCode"
                        .DataBind()
                    End With
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddl
                        .DataSource = dtShare
                        .DataBind()
                    End With
            End Select
            Session.Add("Share_ValueEQO" & ddl.ID, dtShare)
            GetCommentary_EQO()
        Catch ex As Exception
            lblerror.Text = "fill_EQO_Shares:Error occurred in filling Share."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "fill_EQO_Shares", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    'Private Sub chkAddShareEQO3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAddShareEQO3.CheckedChanged
    '    Try
    '        lblerror.Text = ""

    '        If chkAddShareEQO3.Checked Then
    '            FillEQOddl_exchange(ddlExchangeEQO3)
    '            ddlExchangeEQO3.SelectedValue = ddlExchangeEQO2.SelectedValue

    '            fill_EQO_Shares(ddlExchangeEQO3.SelectedValue, ddlShareEQO3)
    '            If ddlShareEQO3.SelectedItem Is Nothing Then
    '                lblBaseCurrencyEQO3.Text = ""
    '                'txtSearchShareEQO3.Text = ""
    '                txtUnderlyingBasketEQO.Text = ""
    '            Else
    '                lblBaseCurrencyEQO3.Text = getBaseCurrency(ddlShareEQO3.SelectedItem.Value)
    '                'txtSearchShareEQO3.Text = ddlShareEQO3.SelectedValue
    '            End If
    '            ddlExchangeEQO3_SelectedIndexChanged(Nothing, Nothing)
    '            ddlExchangeEQO3.Enabled = True
    '            ddlShareEQO3.Enabled = True
    '            'txtSearchShareEQO3.Enabled = True
    '        Else
    '            ddlExchangeEQO3.Items.Clear()
    '            ddlExchangeEQO3.DataSource = Nothing
    '            ddlExchangeEQO3.DataBind()

    '            ddlShareEQO3.Items.Clear()
    '            ddlShareEQO3.DataSource = Nothing
    '            ddlShareEQO3.DataBind()
    '            ddlShareEQO3.Text = ""
    '            'txtSearchShareEQO3.Text = ""
    '            lblBaseCurrencyEQO3.Text = ""
    '            ddlExchangeEQO3.Enabled = False
    '            ddlShareEQO3.Enabled = False
    '        End If

    '        Prepare_EQO_Basket()
    '        ResetAll()
    '        getRange()
    '        fillddlInvestCcy()
    '    Catch ex As Exception
    '        lblerror.Text = "chkAddShareEQO3_CheckedChanged:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "chkAddShareEQO3_CheckedChanged", ErrorLevel.High)

    '    End Try
    'End Sub

    'Private Sub ddlExchangeEQO2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExchangeEQO2.SelectedIndexChanged
    '    Try
    '        lblerror.Text = ""
    '        clearFields()
    '        fill_EQO_Shares(ddlExchangeEQO2.SelectedValue, ddlShareEQO2)
    '        If ddlShareEQO2.SelectedItem Is Nothing Then
    '            lblBaseCurrencyEQO2.Text = ""
    '            'txtSearchShareEQO2.Text = ""
    '            txtUnderlyingBasketEQO.Text = ""
    '        Else
    '            lblBaseCurrencyEQO2.Text = getBaseCurrency(ddlShareEQO2.SelectedItem.Value)
    '            'txtSearchShareEQO2.Text = ddlShareEQO2.SelectedValue
    '            Prepare_EQO_Basket()
    '        End If
    '        ResetAll()
    '        getRange()
    '        fillddlInvestCcy()

    '    Catch ex As Exception
    '        lblerror.Text = "ddlExchangeEQO2_SelectedIndexChanged:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "ddlExchangeEQO2_SelectedIndexChanged", ErrorLevel.High)
    '    End Try
    'End Sub
    Private Sub fillddlInvestCcy()
        Dim ccyList As String = ""
        ddlInvestCcy.Items.Clear()
        Dim strbasketforddl() As String
        If chkAddShare1.Checked Then
            ccyList = lblEQOBaseCcy.Text
        End If
        If chkAddShare2.Checked Then
            If Not ccyList.Contains(lblBaseCurrency2.Text) Then
                ccyList = ccyList + "," + lblBaseCurrency2.Text
            End If
        End If
        If chkAddShare3.Checked Then
            If Not ccyList.Contains(lblBaseCurrency3.Text) Then
                ccyList = ccyList + "," + lblBaseCurrency3.Text
            End If
        End If

        strbasketforddl = ccyList.Split(CChar(","))
        For Each share In strbasketforddl
            ddlInvestCcy.Items.Add(New DropDownListItem(share, share))

        Next

    End Sub

    'Private Sub ddlExchangeEQO3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExchangeEQO3.SelectedIndexChanged
    '    Try
    '        lblerror.Text = ""
    '        clearFields()
    '        fill_EQO_Shares(ddlExchangeEQO3.SelectedValue, ddlShareEQO3)
    '        If ddlShareEQO3.SelectedItem Is Nothing Then
    '            lblBaseCurrencyEQO3.Text = ""
    '            'txtSearchShareEQO3.Text = ""
    '            txtUnderlyingBasketEQO.Text = ""
    '        Else
    '            lblBaseCurrencyEQO3.Text = getBaseCurrency(ddlShareEQO3.SelectedItem.Value)
    '            'txtSearchShareEQO3.Text = ddlShareEQO3.SelectedValue
    '            Prepare_EQO_Basket()
    '        End If
    '        ResetAll()
    '        getRange()
    '        fillddlInvestCcy()
    '    Catch ex As Exception
    '        lblerror.Text = "ddlExchangeEQO3_SelectedIndexChanged:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "ddlExchangeEQO3_SelectedIndexChanged", ErrorLevel.High)
    '    End Try
    'End Sub

    'Private Sub ddlShareEQO2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShareEQO2.SelectedIndexChanged

    '    Try
    '        lblerror.Text = ""
    '        clearFields()
    '        If ddlShareEQO2.SelectedItem Is Nothing Then
    '            lblBaseCurrencyEQO2.Text = ""
    '            'txtSearchShareEQO2.Text = ""
    '            txtUnderlyingBasketEQO.Text = ""
    '        Else
    '            lblBaseCurrencyEQO2.Text = getBaseCurrency(ddlShareEQO2.SelectedItem.Value)
    '            'txtSearchShareEQO2.Text = ddlShareEQO2.SelectedValue
    '            Prepare_EQO_Basket()
    '        End If
    '        ''GetCommentary_Accum()   ''TO DO
    '        ResetAll()
    '        ' getRange()
    '        ' fillddlInvestCcy()
    '    Catch ex As Exception
    '        lblerror.Text = "ddlShareEQO2_SelectedIndexChanged:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "ddlShareEQO2_SelectedIndexChanged", ErrorLevel.High)
    '    End Try
    'End Sub

    'Private Sub ddlShareEQO3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShareEQO3.SelectedIndexChanged
    '    Try
    '        lblerror.Text = ""
    '        clearFields()
    '        If ddlShareEQO3.SelectedItem Is Nothing Then
    '            lblBaseCurrencyEQO3.Text = ""
    '            'txtSearchShareEQO3.Text = ""
    '            txtUnderlyingBasketEQO.Text = ""
    '        Else
    '            lblBaseCurrencyEQO3.Text = getBaseCurrency(ddlShareEQO3.SelectedItem.Value)
    '            'txtSearchShareEQO3.Text = ddlShareEQO3.SelectedValue
    '            Prepare_EQO_Basket()
    '        End If
    '        ''GetCommentary_Accum()   ''TO DO
    '        ResetAll()
    '        ' getRange()
    '        ' fillddlInvestCcy()
    '    Catch ex As Exception
    '        lblerror.Text = "ddlShareEQO3_SelectedIndexChanged:Error occurred in processing."
    '        LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
    '                     sSelfPath, "ddlShareEQO3_SelectedIndexChanged", ErrorLevel.High)
    '    End Try
    'End Sub

    Private Sub grdEQORFQ_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdEQORFQ.PageIndexChanged
        Dim dtGrid As New DataTable("Dummy")
        Try
            grdEQORFQ.EditItemIndex = -1
            'fill_grid()
            grdEQORFQ.CurrentPageIndex = e.NewPageIndex
            dtGrid = CType(Session("EQOGrid"), DataTable)
            grdEQORFQ.DataSource = dtGrid
            grdEQORFQ.DataBind()
            'Get_RFQ_PieChart()
        Catch ex As Exception
            lblerror.Text = "grdEQORFQ_PageIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdEQORFQ_PageIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub grdEQORFQ_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdEQORFQ.SortCommand
        Try
            numberdiv = CType(ViewState("NumberForSort_" + e.SortExpression), Int32)
            numberdiv = numberdiv + 1
            ViewState("NumberForSort_" + e.SortExpression) = numberdiv

            If CType(Session("EQOGrid"), DataTable) Is Nothing Then Exit Sub

            Dim dtSortRevData As DataTable = CType(Session("EQOGrid"), DataTable)
            Dim dvRevData As DataView
            dvRevData = dtSortRevData.DefaultView

            If (numberdiv Mod 2) = 0 Then
                dvRevData.Sort = e.SortExpression & " DESC"
            Else
                dvRevData.Sort = e.SortExpression & " ASC"
            End If

            grdEQORFQ.DataSource = dvRevData
            grdEQORFQ.DataBind()

        Catch ex As Exception
            lblerror.Text = "grdEQORFQ_SortCommand:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "grdEQORFQ_SortCommand", ErrorLevel.High)

        End Try
    End Sub

    Private Sub txtUpfrontEQO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUpfrontEQO.TextChanged
        Try
            lblerror.Text = ""
            clearFields()
            ResetAll()                          ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
            txtUpfrontEQO.Text = SetNumberFormat(txtUpfrontEQO.Text, 2)
           '' ResetAll()
            If Val(txtUpfrontEQO.Text) > 0 Or Val(txtUpfrontEQO.Text) < 100 Then

            Else
                lblerror.Text = "Please enter a valid Upfront."
                Exit Sub
            End If
        Catch ex As Exception
            lblerror.Text = "txtUpfrontEQO_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtUpfrontEQO_TextChanged", ErrorLevel.High)
        End Try
    End Sub

    ''Added by Imran P 12-Nov-2015 
    Private Sub DisplayEstimatedNotional()
        Try
            If ddlShareEQO.SelectedValue.ToString <> "" Then
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAQDQ_Estimated_Notional", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        Dim EqPair As String = ddlShareEQO.SelectedValue.ToString & " - " & lblEQOBaseCcy.Text
                        Session.Add("Debug_Counter", CInt(Session("Debug_Counter")) + 1)
                        Dim BidRate As Double = objELNRFQ.GetShareRate(EqPair, BidRate)
                        Dim iMatDate As Integer
                        If BidRate = 0 Or BidRate = Nothing Then
                            lblerror.Text = "Cannot calculate estimated notional. Rate for selected share not found."
                            txtNotional.Text = ""
                            lblEstimatedNoOfDays.Text = ""
                            lblNotionalWithCcy1.Text = ""
                            lblNotionalWithCcy.Text = ""
                            lblEstimatedNotional.Text = ""
                        Else
                            Dim EstimatedNotional As Double
                            Dim NoOfDays As Long
                            Dim sExchange As String = ""
                            If rdbQuantity.Checked Then
                                EstimatedNotional = CLng(txtOrderqtyEQO.Text) * BidRate
                                lblEstimatedNoOfDays.Text = CStr(NoOfDays)
                                lblEstimatedNotional.Text = SetNumberFormatZeroDecim(CStr(EstimatedNotional), 0)
                                lblNotionalWithCcy.Text = "(" + lblEQOBaseCcy.Text + ")"
                            Else

                            End If
                            '   txtNotional.Text = SetNumberFormatZeroDecim(CStr(EstimatedNotional), 0)

                            lblNotionalWithCcy1.Text = "(" + lblEQOBaseCcy.Text + ")"

                            lblerror.Text = ""
                        End If
                    Case "N", "NO"
                        lblEstimatedNotional.Text = ""
                        txtNotional.Text = "0"
                        lblEstimatedNoOfDays.Text = "0"
                End Select
                upnl4.Update()

            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "DisplayEstimatedNotional", ErrorLevel.High)
        End Try
    End Sub

    Private Sub ddlBarrierMonitoringType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBarrierMonitoringType.SelectedIndexChanged
        ResetAll()
    End Sub
    Private Sub GetCommentary_EQO()
        Dim strELNCommentary As StringBuilder
        Dim sbComm As StringBuilder
        Dim sbMailComm As StringBuilder
        Dim sbMailCommScb As StringBuilder
        Dim strTempJS As StringBuilder

        Dim MRating, SPRating, FRating, bestLP As String
        Dim PriceOrStrikeMail As String
        Dim IssuerMail As String
        Dim IssuePriceMail As String
        Dim lst As ListItem

        Try
            '<AvinashG. on 05-Jan-2016: Initialization should be in Try Block>
            sbComm = New StringBuilder()
            sbMailComm = New StringBuilder()
            sbMailCommScb = New StringBuilder()
            strTempJS = New StringBuilder()
            '</AvinashG. on 05-Jan-2016: Initialization should be in Try Block>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allowed_QuoteMailing", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    If tabContainer.ActiveTabIndex = prdTab.EQO Then
                        lblMailComentry.Text = ""
                        If ddlShareEQO.SelectedValue.ToString.Trim <> "" Then
                            sbComm.AppendLine("<TABLE cellpadding='0' style=' margin-top:-2px;'><TR><TD COLSPAN=""1"" style=""font-size:12px;""><span class='fieldInfo'>!</span></TD><TD COLSPAN=""2""  style=""font-size:12px;"">RFQ Details (EQO)</TD></TR>")

                            Dim tempCounterRFQDetails As Integer
                            tempCounterRFQDetails = 1

                            sbComm.AppendLine("<TR><TD Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD  Style='color:#919191' >Tenor</TD><TD  style='padding-left: 23px;'>  " + txtTenorEQO.Text + ddlTenorEQO.SelectedItem.Text.Substring(0, 1) + "(TD = " + FinIQApp_Date.FinIQDate(txtTradeDateEQO.Value) + "; VD =  " + _
                                              FinIQApp_Date.FinIQDate(txtSettlDateEQO.Value) + "; ED = " + FinIQApp_Date.FinIQDate(txtExpiryDateEQO.Value) + "; MD = " + FinIQApp_Date.FinIQDate(txtMaturityDateEQO.Value) + ")</TD></TR>")
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            If ddlShareEQO.SelectedValue IsNot Nothing Then
                                sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Underlying</TD><TD  style='padding-left: 23px;'>  " + ddlShareEQO.Text)  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                                tempCounterRFQDetails = tempCounterRFQDetails + 1
                            End If
                            sbComm.Append(If(chkAddShare2.Checked AndAlso ddlShareEQO2.Text <> "" AndAlso ddlShareEQO.SelectedValue <> ddlShareEQO2.SelectedValue, ", " + ddlShareEQO2.Text, ""))
                            sbComm.Append(If(chkAddShare3.Checked AndAlso ddlShareEQO3.Text <> "" AndAlso ddlShareEQO3.SelectedValue <> ddlShareEQO2.SelectedValue AndAlso _
                                                 ddlShareEQO3.SelectedValue <> ddlShareEQO.SelectedValue, ", " + ddlShareEQO3.Text, "")) ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            sbComm.AppendLine("</TD></TR>")
                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Client Side</TD><TD  style='padding-left: 23px;'>  " + ddlSideEQO.SelectedItem.Text + "</TD></TR>")
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            'Added by Mohit Lalwani on 12-Apr-2016
                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Option Type</TD><TD  style='padding-left: 23px;'>  " + ddlOptionType.SelectedItem.Text + "</TD></TR>")
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            '/Added by Mohit Lalwani on 12-Apr-2016

                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Strike</TD><TD  style='padding-left: 23px;'>  " + If(ddlStrikeTypeEQO.SelectedValue.ToUpper = "PERCENTAGE", FormatNumber(Val(txtStrikeEQO.Text.Replace(",", "")), 2) + "% of Spot", FormatNumber(Val(txtStrikeEQO.Text.Replace(",", "")), 2)) + "</TD></TR>")
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Premium</TD><TD  style='padding-left: 23px;'>  " + If(ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM", " " + hdnBestPremium.Value + " ", FormatNumber(Val(txtPremium.Text.Replace(",", "")), 2)) + "% of Spot" + "</TD></TR>") ''<Nikhil M. on 20-Sep-2016: Added hdnBestPremium.value>
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            If ddlOptionType.SelectedValue = "KnockIn Put" Then
                                If ddlBarrierEQO.SelectedValue = "Percentage" Then
                                    sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Barrier</TD><TD  style='padding-left: 23px;'>  " + If(ddlSolveforEQO.SelectedValue.ToUpper = "BARRIER", "[  ]", FormatNumber(Val(txtBarrierLevelEQO.Text.Replace(",", "")), 2)) + "% of Spot" + "</TD></TR>")
                                    tempCounterRFQDetails = tempCounterRFQDetails + 1
                                    sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Barrier Type</TD><TD  style='padding-left: 23px;'>  " + ddlBarrierMonitoringType.SelectedItem.Text + "</TD></TR>")
                                    tempCounterRFQDetails = tempCounterRFQDetails + 1
                                Else
                                    sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Barrier</TD><TD  style='padding-left: 23px;'>  " + If(ddlSolveforEQO.SelectedValue.ToUpper = "BARRIER", "[  ]", FormatNumber(Val(txtBarrierLevelEQO.Text.Replace(",", "")), 2)) + "</TD></TR>")
                                    tempCounterRFQDetails = tempCounterRFQDetails + 1
                                End If
                            End If


                            '<Changed by Mohit Lalwani on 30-Nov-2015>
                            'sbComm.AppendLine("<TR><TD  >Upfront</TD><TD>  " + FormatNumber(Val(txtUpfrontELN.Text.Replace(",", "")), 2) + "</TD></TR>")
                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Upfront</TD><TD  style='padding-left: 23px;'>  " + FormatNumber(Val(txtUpfrontEQO.Text.Replace(",", "")), 2) + "%</TD></TR>")
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            '</Changed by Mohit Lalwani on 30-Nov-2015>
                            'sbComm.AppendLine("<TR><TD  >Client Yield</TD><TD>  " + "[  ]% p.a." + "</TD></TR>")
                            ' ''sbComm.AppendLine("<TR><TD  >Notional</TD><TD>  " + ddlQuantoCcy.SelectedItem.Text + FormatNumber(Val(txtQuantity.Text.Replace(",", "")), 2) + "</TD></TR>")
                            If rdbQuantity.Checked Then
                                sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>No. Of Shares</TD><TD  style='padding-left: 23px;'>  " + SetNumberFormat(Val(txtOrderqtyEQO.Text.Replace(",", "")), 0) + "</TD></TR>")  '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                                tempCounterRFQDetails = tempCounterRFQDetails + 1
                            Else
                                sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Notional</TD><TD  style='padding-left: 23px;'>  " + SetNumberFormat(Val(txtNotional.Text.Replace(",", "")), 0) + "</TD></TR>")  '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                                tempCounterRFQDetails = tempCounterRFQDetails + 1
                            End If

                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Settl. Ccy</TD><TD  style='padding-left: 23px;'>  " + ddlSettlCcyEQO.SelectedItem.Text + "</TD></TR>")
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            If ddlInvestCcy.Visible = True And ddlInvestCcy.SelectedItem IsNot Nothing Then
                                sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Notional Ccy.</TD><TD  style='padding-left: 23px;'>  " + ddlInvestCcy.SelectedItem.Text + "</TD></TR>")
                                tempCounterRFQDetails = tempCounterRFQDetails + 1
                            End If
                            sbComm.AppendLine("<TR><TD   Style='color:#919191'><span class='fieldInfo'>" + tempCounterRFQDetails.ToString + "</span></td><TD Style='color:#919191'>Settl. Type</TD><TD  style='padding-left: 23px;'>  " + ddlsettlmethod.SelectedItem.Text + "</TD></TR>")
                            tempCounterRFQDetails = tempCounterRFQDetails + 1
                            sbComm.AppendLine("</TABLE>")
                            ''If ddlShare.SelectedItem IsNot Nothing Then
                            'ddlShare.Text = ddlShare.SelectedItem.Text
                            ''sbMailComm.AppendLine(txtTenor.Text + ddlTenorTypeELN.SelectedItem.Text.Substring(0, 1) + " " + ddlQuantoCcy.SelectedItem.Text + If(chkQuantoCcy.Checked, "(q)", "") + " denominated " + If(chkELNType.Checked, "KO ELN on ", "ELN on ") + ddlShare.SelectedItem.Text + "#")
                            ' sbMailComm.AppendLine(txtTenorEQO.Text + ddlTenorEQO.SelectedItem.Text.Substring(0, 1) + " " + ddlSettlCcyEQO.SelectedItem.Text + " denominated " + ddlShareEQO.Text + "#")
                            ''Else
                            ''End If


                            '<Rushikesh on 30-Aug-16 for new email format to scb>
                            'sbMailComm.Append(txtTenorEQO.Text + ddlTenorEQO.SelectedItem.Text.Substring(0, 1) + " " + ddlSettlCcyEQO.SelectedItem.Text)
                            'sbMailComm.Append(" linked to " + ddlShareEQO.Text)  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            ' ''If (txtBasketShares.Text.Split(CChar(","))).Length > 1 Then
                            ' ''If (txtBasketShares.Text.Split(CChar(","))).Length = 2 Then
                            'sbMailComm.Append(If(chkAddShare2.Checked AndAlso ddlShareEQO2.Text <> "" AndAlso ddlShareEQO.SelectedValue <> ddlShareEQO2.SelectedValue, ", " + ddlShareEQO2.Text, ""))
                            'sbMailComm.Append(If(chkAddShare3.Checked AndAlso ddlShareEQO3.Text <> "" AndAlso ddlShareEQO3.SelectedValue <> ddlShareEQO2.SelectedValue AndAlso _
                            '                     ddlShareEQO3.SelectedValue <> ddlShareEQO.SelectedValue, ", " + ddlShareEQO3.Text, ""))  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            ''ElseIf (txtBasketShares.Text.Split(CChar(","))).Length = 3 Then
                            ''    sbMailComm.Append(If(chkAddShare2.Checked AndAlso ddlShareDRA2.SelectedItem.Text <> "" AndAlso ddlShareDRA.SelectedItem.Value <> ddlShareDRA2.SelectedItem.Value, ", " + ddlShareDRA2.SelectedItem.Text, ""))
                            ''    sbMailComm.Append(If(chkAddShare3.Checked AndAlso ddlShareDRA3.SelectedItem.Text <> "" AndAlso ddlShareDRA3.SelectedItem.Value <> ddlShareDRA2.SelectedItem.Value AndAlso _
                            ''                         ddlShareDRA3.SelectedItem.Value <> ddlShareDRA.SelectedItem.Value, ", " + ddlShareDRA3.SelectedItem.Text, ""))
                            ''End If
                            ' ''End If

                            'sbMailComm.AppendLine("#").AppendLine("#")

                            'sbMailComm.AppendLine("#")
                            'sbMailComm.AppendLine("TD              = " + FinIQApp_Date.FinIQDate(txtTradeDateEQO.Value) + "     ; VD     = " + FinIQApp_Date.FinIQDate(txtSettlDateEQO.Value) + "#")
                            'sbMailComm.AppendLine("ED              = " + FinIQApp_Date.FinIQDate(txtExpiryDateEQO.Value) + "     ; MD     = " + FinIQApp_Date.FinIQDate(txtMaturityDateEQO.Value) + "#")
                            'sbMailComm.AppendLine("#")
                            'sbMailComm.Append("Strike          = " + If(ddlStrikeTypeEQO.SelectedValue.ToUpper = "PERCENTAGE", FormatNumber(Val(txtStrikeEQO.Text.Replace(",", "")), 2) + "% of Spot", FormatNumber(Val(txtStrikeEQO.Text.Replace(",", "")), 2)))

                            'If ddlOptionType.SelectedValue = "KnockIn Put" Then
                            '    sbMailComm.AppendLine("#")
                            '    If ddlBarrierEQO.SelectedValue = "Percentage" Then
                            '        sbMailComm.Append("Barrier          = " + If(ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM", "[  ]", FormatNumber(Val(txtPremium.Text.Replace(",", "")), 2)) + "% of Spot" + "#")
                            '    Else
                            '        sbMailComm.Append("Barrier          = " + If(ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM", "[  ]", FormatNumber(Val(txtPremium.Text.Replace(",", "")), 2)) + "#")
                            '    End If
                            'End If

                            ''sbMailComm.Append(If(chkELNType.Checked, " ; KO      =" + FormatNumber(Val(txtBarrier.Text.Replace(",", "")), 2) + "% of Spot", "")).AppendLine("#")



                            ''  sbMailComm.Append(If(chkELNType.Checked, " ; KO      =" + FormatNumber(Val(txtBarrier.Text.Replace(",", "")), 2) + "% of Spot", "")).AppendLine("#")
                            'sbMailComm.AppendLine("Premium     = " + If(ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM", "[  ]", FormatNumber(Val(txtPremium.Text.Replace(",", "")), 2)) + "% of Spot" + "#")
                            'sbMailComm.AppendLine("#")
                            ' ''                     sbMailComm.AppendLine("Notional     = " + ddlQuantoCcy.SelectedItem.Text + " " + FormatNumber(Val(txtQuantity.Text.Replace(",", "")), 2) + "#")
                            'sbMailComm.AppendLine("No. Of Shares     =  " + SetNumberFormat(Val(txtOrderqtyEQO.Text.Replace(",", "")), 0) + "#")  '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
                            'sbMailComm.AppendLine("Currency      =  " + ddlSettlCcyEQO.SelectedItem.Text + "#")
                            'sbMailComm.AppendLine("Settlement    =  " + ddlsettlmethod.SelectedItem.Text + "#")
                            'lblMailComentry.Text = sbMailComm.ToString()


                            '<Rushikesh on 30-Aug-16 for new email format to scb>
                            ''bestLP = CheckBestPriceForEmail()
                            ''GetIssuerRatingForMail(bestLP, MRating, SPRating, FRating)


                            ''Select Case bestLP
                            ''    Case "BNPP"
                            ''        PriceOrStrikeMail = lblBNPPClientPrice.Text
                            ''        IssuerMail = "BNPP"
                            ''        IssuePriceMail = lblBNPPPrice.Text
                            ''    Case Nothing, ""
                            ''        PriceOrStrikeMail = ""
                            ''        IssuerMail = ""
                            ''        IssuePriceMail = ""
                            ''End Select


                            ''sbMailCommScb.Append(txtTenorEQO.Text + ddlTenorEQO.SelectedItem.Text.Substring(0, 1) + " " + ddlSettlCcyEQO.SelectedItem.Text)
                            ''sbMailCommScb.Append(" linked to " + ddlShareEQO.Text)  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            ''sbMailCommScb.AppendLine("#").AppendLine("#")
                            ''sbMailCommScb.Append("Product : Options")
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Reference Stock : " + ddlShareEQO.Text)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Stock Code : " + ddlShareEQO.SelectedValue)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Stock Ccy : " + ddlSettlCcyEQO.SelectedItem.Text)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Spot : " + lblSpotValue.Text)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Trade Date : " + txtTradeDateEQO.Value)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Settl. Date : " + txtSettlDateEQO.Value)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Expiry Date : " + txtExpiryDateEQO.Value)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Mat. Date : " + txtMaturityDateEQO.Value)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Tenor : " + txtTenorEQO.Text + ddlTenorEQO.SelectedItem.Text.Substring(0, 1))
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append(ddlStrikeTypeEQO.SelectedItem.Text + " : " + If(ddlStrikeTypeEQO.SelectedValue.ToUpper = "PERCENTAGE", IssuePriceMail, FormatNumber(Val(txtStrikeEQO.Text.Replace(",", "")), 2)))
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Premium : " + If(ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM", IssuePriceMail, FormatNumber(Val(txtPremium.Text.Replace(",", "")), 2)))
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Settlement : " + ddlsettlmethod.SelectedItem.Text)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Upfront : " + FormatNumber(Val(txtUpfrontEQO.Text.Replace(",", "")), 2))
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Option Type : " + ddlOptionType.SelectedItem.Text)
                            ''sbMailCommScb.AppendLine("#")
                            ''sbMailCommScb.Append("Client Side : " + ddlSideEQO.SelectedItem.Text)
                            ''sbMailCommScb.AppendLine("#")
                            ''If rdbQuantity.Checked Then
                            ''    sbMailCommScb.Append("No. Of Shares      : " + SetNumberFormat(Val(txtOrderqtyEQO.Text.Replace(",", "")), 0))
                            ''End If
                            ''If rdbNotional.Checked Then
                            ''    sbMailCommScb.Append("Notional : " + SetNumberFormat(Val(txtNotional.Text.Replace(",", "")), 0))
                            ''End If
                            ''lblMailComentry.Text = sbMailCommScb.ToString()

                            '</Rushikesh on 30-Aug-16 for new email format to scb>

                            '<RiddhiS. on 06-Sep-2016: for sending mail to selected Price providers>

                            sbMailCommScb.Append(txtTenorEQO.Text + ddlTenorEQO.SelectedItem.Text.Substring(0, 1) + " " + ddlSettlCcyEQO.SelectedItem.Text)
                            sbMailCommScb.Append(" linked to " + ddlShareEQO.Text)  ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                            sbMailCommScb.AppendLine("#").AppendLine("#")
                            sbMailCommScb.Append("Product : Options")
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Reference Stock : " + ddlShareEQO.Text)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Stock Code : " + ddlShareEQO.SelectedValue)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Stock Ccy : " + ddlSettlCcyEQO.SelectedItem.Text)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Spot : " + lblSpotValue.Text)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Trade Date : " + txtTradeDateEQO.Value)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Settl. Date : " + txtSettlDateEQO.Value)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Expiry Date : " + txtExpiryDateEQO.Value)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Mat. Date : " + txtMaturityDateEQO.Value)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Tenor : " + txtTenorEQO.Text + ddlTenorEQO.SelectedItem.Text.Substring(0, 1))
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append(ddlStrikeTypeEQO.SelectedItem.Text + " : " + If(ddlStrikeTypeEQO.SelectedValue.ToUpper = "PERCENTAGE", IssuePriceMail, FormatNumber(Val(txtStrikeEQO.Text.Replace(",", "")), 2)))
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Settlement : " + ddlsettlmethod.SelectedItem.Text)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Upfront : " + FormatNumber(Val(txtUpfrontEQO.Text.Replace(",", "")), 2))
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Option Type : " + ddlOptionType.SelectedItem.Text)
                            sbMailCommScb.AppendLine("#")
                            sbMailCommScb.Append("Client Side : " + ddlSideEQO.SelectedItem.Text)
                            sbMailCommScb.AppendLine("#")
                            If rdbQuantity.Checked Then
                                sbMailCommScb.Append("No. Of Shares      : " + SetNumberFormat(Val(txtOrderqtyEQO.Text.Replace(",", "")), 0))
                            End If
                            If rdbNotional.Checked Then
                                sbMailCommScb.Append("Notional : " + SetNumberFormat(Val(txtNotional.Text.Replace(",", "")), 0))
                            End If

                            For Each lst In chkPPmaillist.Items
                                If lst.Selected = True Then
                                    Select Case lst.Value
                                      

                                        Case "BNPP"
                                            PriceOrStrikeMail = lblBNPPClientPrice.Text
                                            IssuerMail = "BNPP"
                                            IssuePriceMail = lblBNPPPrice.Text

                                      

                                        Case Nothing, ""
                                            PriceOrStrikeMail = ""
                                            IssuerMail = ""
                                            IssuePriceMail = ""

                                    End Select
                                    GetIssuerRatingForMail(lst.Value, MRating, SPRating, FRating)
                                    sbMailCommScb.AppendLine("#")
                                    sbMailCommScb.Append("Issuer : " + lst.Text)
                                    sbMailCommScb.AppendLine("#")
                                    sbMailCommScb.Append("Premium : " + If(ddlSolveforEQO.SelectedValue.ToUpper = "PREMIUM", IssuePriceMail, FormatNumber(Val(txtPremium.Text.Replace(",", "")), 2)))
                                    sbMailCommScb.AppendLine("#")
                                    sbMailCommScb.Append("Client Premium : " + PriceOrStrikeMail)
                                    sbMailCommScb.AppendLine("#")
                                    sbMailCommScb.AppendLine("Moody's Rating : " + MRating) ''Moody's Rating
                                    sbMailCommScb.AppendLine("Standard and Poor's Rating : " + SPRating) ''S&P Rating
                                    sbMailCommScb.AppendLine("Fitch Rating : " + FRating) ''Fitch Rating
                                End If
                            Next
                            lblMailComentry.Text = sbMailCommScb.ToString()

                            '</RiddhiS. on 06-Sep-2016>



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
            If tabContainer.ActiveTabIndex = prdTab.EQO And ddlShareEQO.SelectedValue = "" Then
                lblComentry.Text = ""
                strTempJS.AppendLine("try{ hideEmail(); } catch(e){ }") 		'Mohit Lalwani on 26-Oct-2016
            Else
                '<AvinashG. on 24-Aug-2015: FA-1014 - Main Pricer: Quote Mailing Option >
                Select Case objReadConfig.ReadConfig(dsConfig, "EQC_Allowed_QuoteMailing", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                    Case "Y", "YES"
                        'btnMail.Visible = True
                        'Avinash/Shekar:-Hide/Show mailing button
                        strTempJS.AppendLine("try{ showEmail(); } catch(e){ }")  		'Mohit Lalwani on 26-Oct-2016
                        ''System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "hideEmail12", "try{ showEmail(); } catch(e){ }", True) 			'Mohit Lalwani on 26-Oct-2016
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
                        SchemeName = Convert.ToString(Session("Scheme_EQO"))

                        If ddlSolveforEQO.SelectedValue = "Premium" Then

                            '   If chkQuantoCcy.Checked = True Then

                            If ddlSideEQO.SelectedValue = "Sell" Then
                                strELNCommentary.Append("You are selling " & ddlSettlCcyEQO.SelectedValue.Trim & " " & txtOrderqtyEQO.Text)
                            Else
                                strELNCommentary.Append("You are buying " & ddlSettlCcyEQO.SelectedValue.Trim & " " & txtOrderqtyEQO.Text)
                            End If

                            strELNCommentary.Append(" quantity of OTC OPTION on " & txtBasketShares.Text & " share starting from " & txtSettlDateEQO.Value & _
                            " for tenor of " & txtTenorEQO.Text & " " & ddlTenorEQO.SelectedValue.Substring(0, 1).ToUpper + ddlTenorEQO.SelectedValue.Substring(1).ToLower _
                            & " with strike of  " & txtStrikeEQO.Text)
                            If ddlStrikeTypeEQO.SelectedValue = "Percentage" Then
                                strELNCommentary.Append("% of Spot.")
                            Else
                                strELNCommentary.Append(".")
                            End If

                            lblComentry.Text = strELNCommentary.ToString
                        Else
                            ' If chkQuantoCcy.Checked = True Then
                            strELNCommentary.Append("You are buying " & ddlSettlCcyEQO.SelectedValue.Trim & " " & txtNotional.Text _
                           & " quantity of " & SchemeName & " on " & txtBasketShares.Text & " share starting from " & txtSettlDateEQO.Value & _
                           " for tenor of " & txtTenorEQO.Text & " " & ddlTenorEQO.SelectedValue.Substring(0, 1).ToUpper + ddlTenorEQO.SelectedValue.Substring(1).ToLower _
                            & " with price of  " & txtPremium.Text & " % ")
                            strELNCommentary.Append(" and strike of  " & txtStrikeEQO.Text)
                            If ddlStrikeTypeEQO.SelectedValue = "Percentage" Then
                                strELNCommentary.Append("% of Spot.")
                            Else
                                strELNCommentary.Append(".")
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
    Private Sub ddlExchangeEQO2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExchangeEQO2.SelectedIndexChanged
        Dim dtBaseCCY As DataTable
        Try
            dtBaseCCY = New DataTable("Dummy")
            lblerror.Text = ""
            clearFields()
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            With ddlShareEQO2
                .Items.Clear()
                .Text = ""
            End With
            If ddlShareEQO2.SelectedValue IsNot Nothing And ddlShareEQO2.SelectedItem IsNot Nothing Then
                Select Case objELNRFQ.DB_GetBASECCY(ddlShareEQO2.SelectedValue, dtBaseCCY)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        lblBaseCurrency2.Text = dtBaseCCY.Rows(0)(0).ToString
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                        lblBaseCurrency2.Text = ""
                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                        lblBaseCurrency2.Text = ""
                End Select
                GetCommentary_EQO()
                ddlShareEQO2.Text = ddlShareEQO2.Text ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                Prepare_EQO_Basket()
            End If
            pnlReprice.Update()
            upnlCommentry.Update()
            ResetAll()
            fillddlInvestCcy()  ''<Added by Rushikesh on 6-April-16 to fill investment ccy in case if basket>
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
        Catch ex As Exception
            lblerror.Text = "ddlExchangeEQO2_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlExchangeEQO2_SelectedIndexChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub ddlExchangeEQO3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExchangeEQO3.SelectedIndexChanged
        Dim dtBaseCCY As DataTable
        Try
            dtBaseCCY = New DataTable("Dummy")
            lblerror.Text = ""
            clearFields()
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            With ddlShareEQO3
                .Items.Clear()
                .Text = ""
            End With
            If ddlShareEQO3.SelectedValue IsNot Nothing And ddlShareEQO3.SelectedItem IsNot Nothing Then
                Select Case objELNRFQ.DB_GetBASECCY(ddlShareEQO3.SelectedValue, dtBaseCCY)
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                        lblBaseCurrency3.Text = dtBaseCCY.Rows(0)(0).ToString
                    Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                        lblBaseCurrency3.Text = ""
                    Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                        lblBaseCurrency3.Text = ""
                End Select
                GetCommentary_EQO()
                ddlShareEQO3.Text = ddlShareEQO3.Text ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                Prepare_EQO_Basket()
            End If
            pnlReprice.Update()
            upnlCommentry.Update()
            ResetAll()
            fillddlInvestCcy()  ''<Added by Rushikesh on 6-April-16 to fill investment ccy in case if basket>
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
        Catch ex As Exception
            lblerror.Text = "ddlExchangeEQO2_SelectedIndexChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ddlExchangeEQO2_SelectedIndexChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub

    Private Sub chkAddShare1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAddShare1.CheckedChanged
        Try
            lblerror.Text = ""
            If chkAddShare1.Checked Then
                FillEQOddl_exchange(ddlExchangeEQO)
                ddlExchangeEQO_SelectedIndexChanged(Nothing, Nothing)
                chkAddShare2.Enabled = True
                tblShareEQO_2.Visible = True
                chkAddShare2_CheckedChanged(Nothing, Nothing)
            Else
                ddlExchangeEQO.Items.Clear()
                ddlExchangeEQO.DataSource = Nothing
                ddlExchangeEQO.DataBind()
                ddlShareEQO.Items.Clear()
                lblEQOBaseCcy.Text = ""
                chkAddShare2.Checked = False
                chkAddShare2.Enabled = False
                tblShareEQO_2.Visible = False
                tblShareEQO_2.Style.Add("opacity", "0")
                chkAddShare2_CheckedChanged(Nothing, Nothing)
            End If
            Prepare_EQO_Basket()
            ResetAll()
            fillddlInvestCcy()  ''<Added by Rushikesh on 6-April-16 to fill investment ccy in case if basket>
        Catch ex As Exception
            lblerror.Text = "chkAddShare1_CheckedChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chkAddShare1_CheckedChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub chkAddShare2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAddShare2.CheckedChanged
        Try
            lblerror.Text = ""
            lblExchangeEQO2.Text = ""
            If chkAddShare2.Checked Then
                FillEQOddl_exchange(ddlExchangeEQO2)
                ddlExchangeEQO2_SelectedIndexChanged(Nothing, Nothing)
                chkAddShare3.Enabled = True
                tblShareEQO_3.Visible = True
                tblShareEQO_3.Style.Add("opacity", "100")
                chkAddShare3_CheckedChanged(Nothing, Nothing)
            Else
                ddlExchangeEQO2.Items.Clear()
                ddlExchangeEQO2.DataSource = Nothing
                ddlExchangeEQO2.DataBind()
                ddlShareEQO2.Items.Clear()
                ddlShareEQO2.SelectedValue = ""
                'ddlShareEQO2.DataSource = Nothing
                'ddlShareEQO2.DataBind()
                ddlShareEQO2.Text = ""
                lblBaseCurrency2.Text = ""
                chkAddShare3.Checked = False
                chkAddShare3.Enabled = False
                tblShareEQO_3.Visible = False
                tblShareEQO_3.Style.Add("opacity", "0")
                chkAddShare3_CheckedChanged(Nothing, Nothing)
            End If
            Prepare_EQO_Basket()
            ResetAll()
            fillddlInvestCcy()  ''<Added by Rushikesh on 6-April-16 to fill investment ccy in case if basket>
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
        Catch ex As Exception
            lblerror.Text = "chkAddShare2_CheckedChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chkAddShare2_CheckedChanged", ErrorLevel.High)

        End Try
    End Sub
    Private Sub chkAddShare3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAddShare3.CheckedChanged
        Try
            lblerror.Text = ""
            lblExchangeEQO3.Text = ""
            If chkAddShare3.Checked Then
                FillEQOddl_exchange(ddlExchangeEQO3)
                ddlExchangeEQO3_SelectedIndexChanged(Nothing, Nothing)
            Else
                ddlExchangeEQO3.Items.Clear()
                ddlExchangeEQO3.DataSource = Nothing
                ddlExchangeEQO3.DataBind()
                ddlShareEQO3.Items.Clear()
                ddlShareEQO3.SelectedValue = ""
                'ddlShareEQO3.DataSource = Nothing
                'ddlShareEQO3.DataBind()
                ddlShareEQO3.Text = ""
                lblBaseCurrency3.Text = ""
            End If
            Prepare_EQO_Basket()
            ResetAll()
            fillddlInvestCcy()  ''<Added by Rushikesh on 6-April-16 to fill investment ccy in case if basket>
            '<AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
            'getRange()
            '</AvinashG. on 05-Jan-2016: Update Solo pricer as main pricer>
        Catch ex As Exception
            lblerror.Text = "chkAddShare3_CheckedChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "chkAddShare3_CheckedChanged", ErrorLevel.High)
        End Try
    End Sub
    Public Sub FillEQOddl_exchange(ByVal ddl As DropDownList)
        Dim dtExchange As DataTable
        Try
            dtExchange = New DataTable("Exchange")
            Select Case objELNRFQ.DB_Fill_Exchange_Combo(dtExchange)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddl
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
                    ddl.DataSource = dtExchange
                    ddl.DataBind()
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Error
            End Select
        Catch ex As Exception
            lblerror.Text = "FillEQOddl_exchange:Error occurred in filling exchange."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "FillEQOddl_exchange", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Public Sub clearShareData()
        ''btnMail.Visible = True
        If chkAddShare1.Checked Then
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            'If ddlShareEQO.SelectedItem Is Nothing OrElse ddlShareEQO.SelectedValue = "" Then
            If ddlShareEQO.SelectedValue Is Nothing OrElse ddlShareEQO.SelectedValue = "" Then
                lblEQOBaseCcy.Text = ""
                lblExchangeEQO.Text = ""
                lblPRRVal.Text = ""
                setLimitsAsNA()


                lblRangeCcy.Text = "Min/Max()"
                txtBasketShares.Text = ""
                If chkAddShare2.Checked Then
                    If ddlShareEQO2.SelectedValue <> "" Then
                        txtBasketShares.Text = ddlShareEQO2.SelectedValue
                    End If
                End If
                If chkAddShare3.Checked Then
                    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    If ddlShareEQO3.SelectedValue <> "" Then
                        If txtBasketShares.Text <> "" Then
                            txtBasketShares.Text += "," + ddlShareEQO3.SelectedValue
                        Else
                            txtBasketShares.Text += ddlShareEQO3.SelectedValue
                        End If
                    End If
                End If
                pnlReprice.Update()
                upnlCommentry.Update()
            End If
        End If
        If chkAddShare2.Checked Then
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            'If ddlShareEQO2.SelectedItem Is Nothing OrElse ddlShareEQO2.SelectedValue = "" Then
            If ddlShareEQO2.SelectedValue Is Nothing OrElse ddlShareEQO2.SelectedValue = "" Then
                lblBaseCurrency2.Text = ""
                lblExchangeEQO2.Text = ""
                lblPRRVal2.Text = ""
                setLimitsAsNA()

                lblRangeCcy.Text = "Min/Max()"
                txtBasketShares.Text = ""
                If chkAddShare1.Checked Then
                    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    If ddlShareEQO.SelectedValue <> "" Then
                        txtBasketShares.Text = ddlShareEQO.SelectedValue
                    End If
                End If
                If chkAddShare3.Checked Then
                    If ddlShareEQO3.SelectedValue <> "" Then
                        If txtBasketShares.Text <> "" Then
                            txtBasketShares.Text += "," + ddlShareEQO3.SelectedValue
                        Else
                            txtBasketShares.Text += ddlShareEQO3.SelectedValue
                        End If
                    End If
                End If
                pnlReprice.Update()
                upnlCommentry.Update()
            End If
        End If
        If chkAddShare3.Checked Then
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
            'If ddlShareEQO3.SelectedItem Is Nothing OrElse ddlShareEQO3.SelectedValue = "" Then
            If ddlShareEQO3.SelectedValue Is Nothing OrElse ddlShareEQO3.SelectedValue = "" Then
                lblBaseCurrency3.Text = ""
                lblExchangeEQO3.Text = ""
                lblPRRVal3.Text = ""
                setLimitsAsNA()

                lblRangeCcy.Text = "Min/Max()"
                txtBasketShares.Text = ""
                If chkAddShare1.Checked Then
                    ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
                    If ddlShareEQO.SelectedValue <> "" Then
                        txtBasketShares.Text = ddlShareEQO.SelectedValue
                    End If
                End If
                If chkAddShare2.Checked Then
                    If ddlShareEQO2.SelectedValue <> "" Then
                        If txtBasketShares.Text <> "" Then
                            txtBasketShares.Text += "," + ddlShareEQO2.SelectedValue
                        Else
                            txtBasketShares.Text += ddlShareEQO2.SelectedValue
                        End If


                    End If

                End If
                pnlReprice.Update()
                upnlCommentry.Update()
            End If
        End If
        If txtBasketShares.Text.Trim = "" Then
            ''btnMail.Visible = False
        End If

    End Sub
    Private Function setExchangeByShare(ByVal _ddlShare As RadComboBox) As String
        Dim sExchangeName As String
        Try
            sExchangeName = ""
            ''Rushikesh 14Jan2016:- For Share load on lazyload change item to value
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
    Public Sub ColumnVisibility()
        '''''''''''''''''TODO
    End Sub
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

            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_MailText_Narration_AS", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "TEXT").Trim.ToUpper
                Case "TEXT"
                    System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "hideEmail13", "mailtoMail();", True)
                Case "HTML"

                    hashRFQID = CType(Session("Hash_Values"), Hashtable)
                    hashPPId = CType(Session("PP_IdTable"), Hashtable)
                    bestPP = CheckBestPriceForEmail()
                    'bestPP = "BNPP" 'Hardcoded by Mohit Lalwani because only one LP Available yet


                    EmailtemplateFilePath = System.Web.HttpContext.Current.Server.MapPath("BasicMailTemplate.eml")    ''mangesh wakode 26 nov 2015  Relative path  
                    '' sMailBodyTemplate = System.IO.File.ReadAllText("C:\inetpub\wwwroot\FinIQWebApp\ELN_DealEntry_Layout10\BasicMailTemplate.eml")
                    sMailBodyTemplate = System.IO.File.ReadAllText(EmailtemplateFilePath)
                    ''mangesh wakode 25 nov 2015 
                    Dim strMailHTML As String = ""


                    Dim mailSubject As New StringBuilder     ''mangesh wakode 26 nov 2015  for mail subject
                    ''mangesh wakode 26 nov 2015 for quote mail narration tab wise 
                    Select Case tabContainer.ActiveTabIndex
                        Case prdTab.EQO
                            Select Case bestPP
                                Case "BNPP"
                                    PriceOrStrike = lblBNPPPrice.Text
                                    Issuer = "BNPP"
                                    Yield = lblBNPPClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("BNPP")))
                                    Else
                                        RFQID = Convert.ToString(Session("BNPPQuote"))
                                    End If
                                Case Nothing, ""
                                    PriceOrStrike = ""
                                    Issuer = ""
                                    Yield = ""
                            End Select

                            '  If RFQID Is Nothing Or RFQID = "" Or Not IsNumeric(PriceOrStrike) Or PriceOrStrike.Trim = "" Then           'Mohit Lalwani on 12-Apr-2016
                            If RFQID Is Nothing Or RFQID = "" Or PriceOrStrike = "" Then           'Mohit Lalwani on 12-Apr-2016
                                lblerror.ForeColor = Color.Red
                                lblerror.Text = "No price available for mailing!"
                                Exit Sub
                            End If
                            sUnderlyingTicker = getUnderlyingTicker(ddlShareEQO.Text)
                            strMailHTML = getQuoteMailHTMLString_EQO(sUnderlyingTicker, sUnderlyingTicker2, sUnderlyingTicker3, PriceOrStrike, bestPP, Issuer, Yield, RFQID)   ''for ELN
                            mailSubject.Append(objReadConfig.ReadConfig(dsConfig, "EQC_QuoteMailSubjectBankName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), " <EQ_Connect RFQ>").Replace("&lt;", "<").Replace("&gt;", ">"))
                            mailSubject.Append(" " + txtTenorEQO.Text.ToString.Trim + " " + ddlTenorEQO.SelectedItem.Text)
                            mailSubject.Append(" " + ddlSettlCcyEQO.SelectedItem.Text.ToString.Trim)           ''append CCy to subject
                            mailSubject.Append(" EQO on ")                                    ''append product to subject
                            mailSubject.Append(sUnderlyingTicker)       ''append Underlying to subject
                            mailFileName = "EQ_Connect_ELN_" + txtTenorEQO.Text + "_" + ddlTenorEQO.SelectedItem.Text + "_" + sUnderlyingTicker + ".eml"
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


                    Dim mailSubject As New StringBuilder     ''mangesh wakode 26 nov 2015  for mail subject
                     Select Case tabContainer.ActiveTabIndex
                        Case prdTab.EQO
                            Select Case hdnBestProvider.Value
                                Case "BNPP"
                                    PriceOrStrike = lblBNPPPrice.Text
                                    Issuer = "BNPP"
                                    Yield = lblBNPPClientYield.Text
                                    If Convert.ToString(Session("flag")) = "I" Then
                                        RFQID = CStr(hashRFQID(hashPPId("BNPP")))
                                    Else
                                        RFQID = Convert.ToString(Session("BNPPQuote"))
                                    End If
                                Case Nothing, ""
                                    PriceOrStrike = ""
                                    Issuer = ""
                                    Yield = ""
                            End Select
                            If RFQID Is Nothing Or RFQID = "" Or PriceOrStrike.ToUpper = "" Or PriceOrStrike.ToUpper = "REJECTED" Then           'Mohit Lalwani on 12-Apr-2016
                                lblerror.ForeColor = Color.Red
                                lblerror.Text = "No price available for mailing!"
                                Exit Sub
                            End If
                            sUnderlyingTicker = getUnderlyingTicker(ddlShareEQO.Text)
                            mailSubject.Append(objReadConfig.ReadConfig(dsConfig, "EQC_QuoteMailSubjectBankName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), " <EQ_Connect RFQ>").Replace("&lt;", "<").Replace("&gt;", ">"))
                            mailSubject.Append(" " + txtTenorEQO.Text.ToString.Trim + " " + ddlTenorEQO.SelectedItem.Text)
                            mailSubject.Append(" " + ddlSettlCcyEQO.SelectedItem.Text.ToString.Trim)           ''append CCy to subject
                            mailSubject.Append(" EQO on ")                                    ''append product to subject
                            mailSubject.Append(sUnderlyingTicker)       ''append Underlying to subject
                            mailFileName = "EQ_Connect_ELN_" + txtTenorEQO.Text + "_" + ddlTenorEQO.SelectedItem.Text + "_" + sUnderlyingTicker + ".eml"


                            Dim strFileName As String
                            strFileName = generateDocument.StartDocumentGeneration(LoginInfoGV.Login_Info.LoginId, "EQO", "SEND_QUOTE_EMAIL", RFQID, "ELN_DEAL", LoginInfoGV.Login_Info.EntityID.ToString, LoginInfoGV.Login_Info.GlobalServerDateTime, 1)
                            Dim FileText As String = File.ReadAllText(strFileName)
                            replaceClassToStyle(FileText)

                            Dim dtImageDetails As DataTable
                            dtImageDetails = New DataTable("dtImageDetails")
                            dtImageDetails.Columns.Add("imageID", GetType(String))
                            dtImageDetails.Columns.Add("imagePath", GetType(String))

                            Dim strLoginUserEmail As String = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)


                            If oWEBADMIN.Notify_ToDealerDeskGroupEmailID_imageContent(LoginInfoGV.Login_Info.EntityID.ToString(), _
                                                                                               LoginInfoGV.Login_Info.LoginId, strLoginUserEmail, FileText, mailSubject.ToString(), "", dtImageDetails, "") Then
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

    Sub replaceClassToStyle(ByRef strHtmlText As String)

        Dim strblueHeader As String = "background-color :#003466;color :White ;text-align :left ;height:20px !important;"
        Dim strfontCss As String = "font-family :verdana,tahoma,helvetica;font-size :10pt;color:#03316C;"
        Dim strouterTR As String = "outline: 2px solid #41ABD1;"
        Dim strboldCss As String = "color: #004d99;font-weight:bold;"

        Dim strtableizertable As String = "font-size: 12px;border: 1px solid #CCC;font-family: Arial, Helvetica, sans-serif;"
        Dim strtableizer_table_th As String = "background-color: #104E8B;color: #FFF;font-weight: bold;"
        Dim strredClass As String = "color: red;font-weight:bold;"
        Dim strblueClass As String = "color: #004d99;font-weight:bold;"

        strHtmlText = strHtmlText.Replace("blueHeader", strblueHeader)
        strHtmlText = strHtmlText.Replace("fontCss", strfontCss)
        strHtmlText = strHtmlText.Replace("outerTR", strouterTR)

        strHtmlText = strHtmlText.Replace("tableizer-table", strtableizertable)
        strHtmlText = strHtmlText.Replace("tableizer-table_th", strtableizer_table_th)
        strHtmlText = strHtmlText.Replace("redClass", strredClass)
        strHtmlText = strHtmlText.Replace("blueClass", strblueClass)
        strHtmlText = strHtmlText.Replace("boldClass", strboldCss)

    End Sub

    Public Function getQuoteMailHTMLString_EQO(ByVal sUnderlyingTicker As String, ByVal sUnderlyingTicker2 As String, _
                                               ByVal sUnderlyingTicker3 As String, ByVal PriceOrStrike As String, ByVal bestPP As String, ByVal Issuer As String, _
                                               ByVal Yield As String, ByRef RFQID As String) As String
        Try

            ''Ashwini P 1-Aug-2016
            Dim BestPPMoodys As String
            Dim MoodysRating As String
            Dim SnPRating As String
            Dim FitchRating As String
            Dim sbEQOMailColumnHeader As StringBuilder
            sbEQOMailColumnHeader = New StringBuilder()
            sbEQOMailColumnHeader.Append("<br/>")
            sbEQOMailColumnHeader.Append("<HTML><HEAD></HEAD>")
            sbEQOMailColumnHeader.Append("<BODY dir=3Dltr>")
            sbEQOMailColumnHeader.Append("<DIV dir=3Dltr>")
            sbEQOMailColumnHeader.Append("<DIV style=3D""FONT-SIZE: 12pt; FONT-FAMILY: 'Calibri'; COLOR: #000000"">")
            sbEQOMailColumnHeader.Append("<TABLE style=3D""BORDER-COLLAPSE: collapse; COLOR: #000000; ""=20")
            sbEQOMailColumnHeader.Append("cellSpacing=3D0 cellPadding=3D0  border=3D0>")
            sbEQOMailColumnHeader.Append("  <TBODY>")
            sbEQOMailColumnHeader.Append(" <TR style=3D""HEIGHT: 25pt; BACKGROUND: #336699; color:#ffffff"">")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>RFQ ID</TD>")  ''Mangesh wakode 16 dec 2015
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Product</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Issue Date</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Valuation Date</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Maturity Date</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Tenor (d)</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Underlying</TD>")
            If chkAddShare2.Checked = True Then
                sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Underlying</TD>")
            End If
            If chkAddShare3.Checked = True Then
                sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Underlying</TD>")
            End If
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Option Type</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Settl. Ccy</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Strike</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Barrier</TD>")      ''Added as per new requirement 25 nov 2015
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Premium</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Issuer</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Settl. Type</TD>")

            'Barrier Type removed by Mohit as told by Avinash Gollecha on 29-Jun-2016
            'sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; padding:4pt;"" align=3Dcenter vAlign=3Dtop noWrap>Barrier Type</TD>")
            '/Barrier Type removed by Mohit as told by Avinash Gollecha on 29-Jun-2016
            sbEQOMailColumnHeader.Append(" </TR><TR > ")
            sbEQOMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + RFQID + "</TD>")  ''mangesh wakode 16 dec 2015
            sbEQOMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">EQO</TD>")
            sbEQOMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + CDate(txtSettlDateEQO.Value.ToString).ToString("dd-MMM-yy") + _
                                    "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" _
                                    + CDate(txtExpiryDateEQO.Value.ToString).ToString("dd-MMM-yy") + "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
            '<AvinashG. on 19-Jul-2016: Take BNPP maturity date if mail is for BNP>
            If Issuer = "BNPP" Then
                sbEQOMailColumnHeader.Append(CDate(FinIQApp_Date.FinIQDate(CDate(BNPPHiddenMatDate.Value))).ToString("dd-MMM-yy") + "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
                sbEQOMailColumnHeader.Append(DateDiff(DateInterval.Day, CDate(txtSettlDateEQO.Value), CDate(FinIQApp_Date.FinIQDate(CDate(BNPPHiddenMatDate.Value)))).ToString + "</TD>")
            Else
                sbEQOMailColumnHeader.Append(CDate(txtMaturityDateEQO.Value.ToString).ToString("dd-MMM-yy") + "</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
                sbEQOMailColumnHeader.Append(DateDiff(DateInterval.Day, CDate(txtSettlDateEQO.Value), CDate(txtMaturityDateEQO.Value)).ToString + "</TD>")

            End If
            '</AvinashG. on 19-Jul-2016: Take BNPP maturity date if mail is for BNP>

            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + sUnderlyingTicker + "</TD>")

            If chkAddShare2.Checked = True Then
                sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + sUnderlyingTicker2 + " </TD>") ''Underlying    
            End If

            If chkAddShare3.Checked = True Then
                sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + sUnderlyingTicker3 + " </TD>") ''Underlying    
            End If

            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + ddlOptionType.SelectedItem.Text + "</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + ddlSettlCcyEQO.SelectedItem.Text + "</TD>")


            If ddlSolveforEQO.SelectedValue = "Strike" Then
                sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center; BACKGROUND: #f5fb99;"">")
            Else
                sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center; "">")
            End If

            If ddlStrikeTypeEQO.SelectedValue = "Percentage" Then
                sbEQOMailColumnHeader.Append(txtStrikeEQO.Text.Trim + "%")

            Else
                sbEQOMailColumnHeader.Append(txtStrikeEQO.Text.Trim)
            End If

            sbEQOMailColumnHeader.Append(" </TD>")

            If ddlOptionType.SelectedValue = "KnockIn Put" Then
                If ddlSolveforEQO.SelectedValue = "Barrier" Then
                    If ddlBarrierEQO.SelectedValue = "Percentage" Then
                        sbEQOMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + PriceOrStrike.Trim + "%</TD>")
                    Else
                        sbEQOMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + PriceOrStrike.ToString.Trim + "</TD>")
                    End If
                Else
                    If ddlBarrierEQO.SelectedValue = "Percentage" Then
                        sbEQOMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + txtBarrierLevelEQO.Text.ToString.Trim + "%</TD>")
                    Else
                        sbEQOMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + txtBarrierLevelEQO.Text.ToString.Trim + "</TD>")
                    End If
                End If
            Else
                sbEQOMailColumnHeader.Append(" <TD style=3D""BORDER: windowtext 1pt solid;PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + "N.A." + "</TD>")
            End If
            If ddlSolveforEQO.SelectedValue = "Barrier" Then
            sbEQOMailColumnHeader.Append("</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
                If ddlSideEQO.SelectedValue = "Buy" Then
                    sbEQOMailColumnHeader.Append((Double.Parse(txtPremium.Text.Trim) + Double.Parse(txtUpfrontEQO.Text.Trim)).ToString + "%")
                Else
                    sbEQOMailColumnHeader.Append((Double.Parse(txtPremium.Text.Trim) - Double.Parse(txtUpfrontEQO.Text.Trim)).ToString + "%")
                End If

            Else

                If ddlSolveforEQO.SelectedValue = "Premium" Then
                    sbEQOMailColumnHeader.Append("</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;BACKGROUND: #f5fb99;"">")
                Else
                    sbEQOMailColumnHeader.Append("</TD><TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
                End If

                If Not PriceOrStrike Is Nothing Then
                    If PriceOrStrike.Trim <> "" Then
                        If ddlSideEQO.SelectedValue = "Buy" Then
                            sbEQOMailColumnHeader.Append((Double.Parse(PriceOrStrike) + Double.Parse(txtUpfrontEQO.Text.Trim)).ToString + "%")
                        Else
                            sbEQOMailColumnHeader.Append((Double.Parse(PriceOrStrike) - Double.Parse(txtUpfrontEQO.Text.Trim)).ToString + "%")

                        End If

                    Else
                        sbEQOMailColumnHeader.Append("")
                    End If
                    Else
                        sbEQOMailColumnHeader.Append("")
                    End If
            End If
            sbEQOMailColumnHeader.Append("</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">")
            sbEQOMailColumnHeader.Append(Issuer)
            sbEQOMailColumnHeader.Append("</TD>")
            sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + ddlsettlmethod.SelectedItem.Text + "</TD>")

            'Barrier Type removed by Mohit as told by Avinash Gollecha on 29-Jun-2016
            'If ddlOptionType.SelectedValue = "KnockIn Put" Then
            '    sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + ddlBarrierMonitoringType.SelectedItem.Text + "</TD>")

            'Else
            '    sbEQOMailColumnHeader.Append("<TD style=3D""BORDER: windowtext 1pt solid; PADDING-LEFT: 0.05in; PADDING-RIGHT: 0.05in; text-align: center;"">" + ddlBarrierMonitoringType.SelectedItem.Text + "</TD>")

            'End If
            '/Barrier Type removed by Mohit as told by Avinash Gollecha on 29-Jun-2016

            '' <Ashwini P> 01-Aug-2016 START
            BestPPMoodys = CheckBestPriceForEmail()
            GetIssuerRatingForMail(BestPPMoodys, MoodysRating, SnPRating, FitchRating)
            sbEQOMailColumnHeader.Append("<TR><TD>* Issuer Rating: Moody's Rating:  " + MoodysRating + ", S&P Rating:  " + SnPRating + ", Fitch Rating:  " + FitchRating + "</TD></TR>")
            ''</Ashwini P> END

            sbEQOMailColumnHeader.Append("</TR></TBODY></TABLE></DIV></DIV></BODY>")

            sbEQOMailColumnHeader.Append("</HTML>")
            Return sbEQOMailColumnHeader.ToString
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                  sSelfPath, "getQuoteMailHTMLString_EQO", ErrorLevel.High)
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
    Public Function convertNotionalintoShort(ByVal minDoub As Double, ByVal MinOrMax As String) As String
        If (MinOrMax = "MIN") Then
            ''Avinash 28Dec2015 For leng roundup and roundDown       
            Return If(minDoub >= 1000000, FormatNumber(RoundUp(minDoub / 1000000.0, 1), 1) + "M", FormatNumber(RoundUp(minDoub / 1000.0, 1), 1) + "K")
        Else
            ''Avinash 28Dec2015 For leng roundup and roundDown       
            Return If(minDoub >= 1000000, FormatNumber(RoundDown(minDoub / 1000000.0, 1), 1) + "M", FormatNumber(RoundDown(minDoub / 1000.0, 1), 1) + "K")
        End If
    End Function
    Private Function setMinMaxCurrency() As String
        Dim ExchangeCcy As String

        '<AvinashG. on 11-Apr-2016: TODO>
        'If chkQuantoCcy.Checked Then
        '    ExchangeCcy = ddlQuantoCcy.SelectedValue
        'Else
        '    ExchangeCcy = lblELNBaseCcy.Text
        'End If
        ExchangeCcy = ddlInvestCcy.SelectedValue 'Mohit / Rushi on 27-Jul-2016 told by Shilpa

        lblRangeCcy.Text = "Min/Max(<B>" + ExchangeCcy + "</B>)"
    End Function
    Private Sub ResetMinMaxNotional()
        lblBNPPlimit.Text = ""
        lblBNPPlimit.ToolTip = ""
        lblBNPPlimit.Visible = False
        lblRangeCcy.Text = ""  'Added by Mohit lalwani on 12-Apr-2016
    End Sub
    Private Function FillRFQDataTable(ByVal sPP_ID As String) As Boolean
        Dim sRFQId As String
        Try
            Dim dtRFQInsertData As DataTable
            Dim sProductDefinition As String = ""
            objELNRFQ.GetUDTSchema("udtEQORFQ", dtRFQInsertData)
            dtRFQInsertData.Rows.Add()
            dtRFQInsertData.Rows(0)("ER_PP_ID") = sPP_ID
            dtRFQInsertData.Rows(0)("ER_SettlmentDate") = txtSettlDateEQO.Value
            dtRFQInsertData.Rows(0)("ER_ExpiryDate") = txtExpiryDateEQO.Value
            dtRFQInsertData.Rows(0)("ER_MaturityDate") = txtMaturityDateEQO.Value
            dtRFQInsertData.Rows(0)("ER_StrikePercentage") = Val(txtStrikeEQO.Text.Replace(",", ""))
            dtRFQInsertData.Rows(0)("ER_BarrierPercentage") = Val(txtBarrierLevelEQO.Text)
            If ddlSolveforEQO.SelectedText <> "Premium" Then
                dtRFQInsertData.Rows(0)("ER_OfferPrice") = Val(txtPremium.Text) / 100
            End If
            If ddlOptionType.SelectedValue.ToUpper.Contains("KNOCKIN PUT") Then
                If ddlSolveforEQO.SelectedItem.Text.Trim.ToUpper <> "BARRIER" Then
                    dtRFQInsertData.Rows(0)("ER_Type") = "WOKIP"
                    dtRFQInsertData.Rows(0)("ER_BarrierPercentage") = Val(txtBarrierLevelEQO.Text)
                    dtRFQInsertData.Rows(0)("ER_BarrierMonitoringMode") = ddlBarrierMonitoringType.SelectedValue
                    dtRFQInsertData.Rows(0)("ER_Barrier_Type") = ddlBarrierEQO.SelectedValue    ''ABSOLUTE, PERCENTAGE
                Else
                    dtRFQInsertData.Rows(0)("ER_Type") = "WOKIP"
                    dtRFQInsertData.Rows(0)("ER_BarrierPercentage") = 0.0
                    dtRFQInsertData.Rows(0)("ER_BarrierMonitoringMode") = ddlBarrierMonitoringType.SelectedValue
                    dtRFQInsertData.Rows(0)("ER_Barrier_Type") = ""
                End If
                sProductDefinition += "WOKIP"
            Else
                dtRFQInsertData.Rows(0)("ER_Type") = "VANI"
                dtRFQInsertData.Rows(0)("ER_BarrierPercentage") = 0.0
                dtRFQInsertData.Rows(0)("ER_BarrierMonitoringMode") = ""
                dtRFQInsertData.Rows(0)("ER_Barrier_Type") = ""
                sProductDefinition += "VANI"
            End If

            sProductDefinition += txtSettlDateEQO.Value
            sProductDefinition += txtMaturityDateEQO.Value
            sProductDefinition += Val(txtStrikeEQO.Text.Replace(",", "")).ToString
            sProductDefinition += ddlShareEQO.SelectedValue.ToString
            dtRFQInsertData.Rows(0)("ER_ProductDefinition") = sProductDefinition
            dtRFQInsertData.Rows(0)("ER_Text") = "2012_QR12"


            dtRFQInsertData.Rows(0)("ER_UnderlyingCode_Type") = ("RIC" & _
                 If(ddlShareEQO2.SelectedValue Is Nothing Or ddlShareEQO2.SelectedValue = "", "", "," & "RIC") & _
                 If(ddlShareEQO3.SelectedValue Is Nothing Or ddlShareEQO3.SelectedValue = "", "", "," & "RIC"))
            dtRFQInsertData.Rows(0)("ER_UnderlyingCode") = (ddlShareEQO.SelectedValue & _
                If(ddlShareEQO2.SelectedValue Is Nothing Or ddlShareEQO2.SelectedValue = "", "", "," & ddlShareEQO2.SelectedValue) & _
                If(ddlShareEQO3.SelectedValue Is Nothing Or ddlShareEQO3.SelectedValue = "", "", "," & ddlShareEQO3.SelectedValue))
            dtRFQInsertData.Rows(0)("ER_TenorType") = ddlTenorEQO.SelectedValue
            dtRFQInsertData.Rows(0)("ER_Tenor") = CInt(txtTenorEQO.Text)
            dtRFQInsertData.Rows(0)("ER_TradeDate") = txtTradeDateEQO.Value
            ''dtRFQInsertData.Rows(0)("ER_QuoteRequestId") = strQuoteId '''' To be returned after insertion
            dtRFQInsertData.Rows(0)("ER_SecurityDescription") = "OTCOPTION"
            If ddlOptionType.SelectedValue.ToUpper.Contains("CALL") Then
                dtRFQInsertData.Rows(0)("ER_SecuritySubType") = "Call"
            Else
                dtRFQInsertData.Rows(0)("ER_SecuritySubType") = "Put"
            End If
            If ddlOptionType.SelectedValue.ToUpper.Contains("EUROPEAN") Then
                dtRFQInsertData.Rows(0)("ER_ExerciseType") = "European"
            ElseIf ddlOptionType.SelectedValue.ToUpper.Contains("AMERICAN") Then
                dtRFQInsertData.Rows(0)("ER_ExerciseType") = "American"
            Else
                dtRFQInsertData.Rows(0)("ER_ExerciseType") = "Barrier"
            End If
            If ddlsettlmethod.SelectedValue.ToUpper.Contains("PHYSICAL") Then
                dtRFQInsertData.Rows(0)("ER_SettlementType") = "P"
            Else
                dtRFQInsertData.Rows(0)("ER_SettlementType") = "C"
            End If

            dtRFQInsertData.Rows(0)("ER_StrikeType") = ddlStrikeTypeEQO.SelectedValue

            If ddlProductEQO.SelectedValue.ToUpper.Contains("EQUITY") Then
                dtRFQInsertData.Rows(0)("ER_UnderlyingProduct") = "EQUITY"
            Else
                dtRFQInsertData.Rows(0)("ER_UnderlyingProduct") = "INDEX"
            End If

            dtRFQInsertData.Rows(0)("ER_BuySell") = ddlSideEQO.SelectedValue

            ''Commented by Rushikesh D.
            'If rdbQuantity.Checked Then

            '    Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAQDQ_Estimated_Notional", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            '        Case "Y", "YES"
            '            If lblEstimatedNotional.Text.Trim = "" Or lblEstimatedNotional.Text.Trim = "&nbsp;" Then
            '                'dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = 0
            '            Else
            '                dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(lblEstimatedNotional.Text, ",", "")

            '            End If

            '            dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(lblEstimatedNotional.Text, ",", "")

            '        Case "N", "NO"

            '            'dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace("0", ",", "")

            '    End Select

            'Else

            '    dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(txtNotional.Text, ",", "")
            'End If
            'If rdbQuantity.Checked Then
            '    dtRFQInsertData.Rows(0)("ER_Nominal_Amount") = Replace(txtOrderqtyEQO.Text, ",", "")
            'Else
            '    'dtRFQInsertData.Rows(0)("ER_Nominal_Amount") = "0"
            'End If

'Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options
            Select Case objReadConfig.ReadConfig(dsConfig, "EQO_EnableOrderQuantityType", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "BOTH").Trim.ToUpper
                Case "BOTH"
                    If rdbQuantity.Checked Then
                        dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(txtOrderqtyEQO.Text, ",", "")
                    Else
                        dtRFQInsertData.Rows(0)("ER_Nominal_Amount") = Replace(txtNotional.Text, ",", "")
                        dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(txtNotional.Text, ",", "")
                    End If
                Case "NOTIONAL"
                    dtRFQInsertData.Rows(0)("ER_Nominal_Amount") = Replace(txtNotional.Text, ",", "")
                    dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(txtNotional.Text, ",", "")
                Case "NOOFSHARE"
                    dtRFQInsertData.Rows(0)("ER_CashOrderQuantity") = Replace(txtOrderqtyEQO.Text, ",", "")
            End Select
'/Mohit Lalwani on 23-Jul-2016 FA-1458 - Config based Order Quantity input in Options
            dtRFQInsertData.Rows(0)("ER_CashCurrency") = ddlInvestCcy.SelectedValue
            dtRFQInsertData.Rows(0)("ER_UnderlyingCcy") = (lblEQOBaseCcy.Text & _
                 If(lblBaseCurrency2.Text Is Nothing Or lblBaseCurrency2.Text = "", "", "," & lblBaseCurrency2.Text) & _
                 If(lblBaseCurrency3.Text Is Nothing Or lblBaseCurrency3.Text = "", "", "," & lblBaseCurrency3.Text))

            dtRFQInsertData.Rows(0)("ER_TransactionTime") = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff tt")
            dtRFQInsertData.Rows(0)("ER_Quanto_Currency") = ddlSettlCcyEQO.SelectedValue
            dtRFQInsertData.Rows(0)("ER_Created_By") = LoginInfoGV.Login_Info.LoginId
            dtRFQInsertData.Rows(0)("ER_Remark1") = ""
            dtRFQInsertData.Rows(0)("ER_Remark2") = ""
            dtRFQInsertData.Rows(0)("ER_Misc1") = ""
            dtRFQInsertData.Rows(0)("ER_Misc2") = ""
            dtRFQInsertData.Rows(0)("ER_Active_YN") = "Y"
            dtRFQInsertData.Rows(0)("ER_SubScheme") = ""

            If ddlExchangeEQO.SelectedValue.ToUpper = "ALL" Then
                Dim sTemp As String
                dtRFQInsertData.Rows(0)("ER_Exchange") = objELNRFQ.GetShareExchange(ddlShareEQO.SelectedValue.ToString, sTemp) & _
                           If(ddlExchangeEQO2.SelectedValue Is Nothing Or ddlExchangeEQO2.SelectedValue = "", "", "," & objELNRFQ.GetShareExchange(ddlShareEQO2.SelectedValue.ToString, sTemp)) & _
                           If(ddlExchangeEQO3.SelectedValue Is Nothing Or ddlExchangeEQO3.SelectedValue = "", "", "," & objELNRFQ.GetShareExchange(ddlShareEQO3.SelectedValue.ToString, sTemp))
            Else
                dtRFQInsertData.Rows(0)("ER_Exchange") = ddlExchangeEQO.SelectedValue & _
                           If(ddlExchangeEQO2.SelectedValue Is Nothing Or ddlExchangeEQO2.SelectedValue = "", "", "," & ddlExchangeEQO2.SelectedValue) & _
                           If(ddlExchangeEQO3.SelectedValue Is Nothing Or ddlExchangeEQO3.SelectedValue = "", "", "," & ddlExchangeEQO3.SelectedValue)
            End If


            dtRFQInsertData.Rows(0)("ER_Quote_Request_YN") = "Y"
            dtRFQInsertData.Rows(0)("ER_Entity_ID") = LoginInfoGV.Login_Info.EntityID.ToString

            '<AvinashG. on 19-Jul-2016: ALERT!!!!! Check what to be saved when baskets go-live>

            dtRFQInsertData.Rows(0)("ER_Issuer_Date_Offset") = txtSettlDays.Text

            'dtRFQInsertData.Rows(0)("ER_Issuer_Date_Offset") = "2"

            '<AvinashG. on 19-Jul-2016: To be saved as per exchange>
            SetTemplateDetails("EQO")  'Rushikesh on 06-Nov
            dtRFQInsertData.Rows(0)("ER_Template_ID") = Convert.ToString(Session("Template_Code_EQO"))
            dtRFQInsertData.Rows(0)("ER_SolveFor") = ddlSolveforEQO.SelectedValue
            dtRFQInsertData.Rows(0)("ER_EntityName") = ddlentity.SelectedItem.Text
            dtRFQInsertData.Rows(0)("ER_RFQ_RMName") = ddlRFQRM.SelectedItem.Value
            Dim strLoginUserEmailID As String
            strLoginUserEmailID = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)
            dtRFQInsertData.Rows(0)("ER_EmailId") = strLoginUserEmailID
            dtRFQInsertData.Rows(0)("ER_Branch") = lblbranch.Text
            dtRFQInsertData.Rows(0)("ER_Upfront") = Val(txtUpfrontEQO.Text) * 100
            If rdbQuantity.Checked Then
                dtRFQInsertData.Rows(0)("ER_EQO_Quantity_Type") = "SHARES"
            Else
                dtRFQInsertData.Rows(0)("ER_EQO_Quantity_Type") = "NOTIONAL"
            End If
            ''<Rushikesh on 21-Oct-16 for quote email day count field>
            dtRFQInsertData.Rows(0)("ER_LongDays") = DateDiff(DateInterval.Day, CDate(txtTradeDateEQO.Value), CDate(txtExpiryDateEQO.Value))
            ''</Rushikesh on 21-Oct-16 for quote email day count field>
            If flag = "I" Then
                '' To decide logic for Individual or Group RFQ in type
                ''dtRFQInsertData.Rows(0)("EP_Remark2") = Convert.ToString(Session("Quote_ID"))
            End If
            Select Case objELNRFQ.Insert_Dt_EQO_RFQ(dtRFQInsertData, sRFQId)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    Session.Add("Quote_ID", sRFQId)
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''Rushikesh to set share value for grid selection and for other operations
    Private Sub setShare1(ByVal strExchng As String, ByVal strShareVal As String)
        Try
            Dim dtSelectedShare As DataTable
            dtSelectedShare = Nothing
            Select Case objELNRFQ.Db_Get_Selected_Share("EQ", strExchng, "", strShareVal, dtSelectedShare)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlShareEQO
                        .Items.Clear()
                        .DataSource = dtSelectedShare
                        .DataTextField = "LongName"
                        .DataValueField = "Code"
                        .DataBind()
                        .SelectedIndex = 0
                    End With
                    ddlShareEQO.SelectedIndex = ddlShareEQO.Items.IndexOf(ddlShareEQO.Items.FindItemByValue(strShareVal))
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlShareEQO
                        .DataSource = dtShare
                        .DataBind()
                    End With
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ShowHideDeatils(ByVal blnShowPopup As Boolean)
        Try
            panelEQO.Enabled = Not blnShowPopup
            upnl4.Update()
            If TRBNPP1.Visible Then
                TRBNPP1.Disabled = blnShowPopup
            End If
            PanelReprice.Enabled = Not blnShowPopup
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableTabs", " document.getElementById('ctl00_MainContent_tabContainer_header').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableELNTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelELN').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableFCNTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelFCN').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableDRATab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelDRA').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableAQTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelAQDQ').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableAQDQTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelDQ').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            System.Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "DisableEQOTab", " document.getElementById('__tab_ctl00_MainContent_tabContainer_tabPanelEQO').disabled = " + blnShowPopup.ToString.ToLower + ";", True)
            '</Changed by Mohit Lalwani on 1-Feb-2016 for splitting of TAB>
        Catch ex As Exception
            lblerror.Text = "ShowHideDeatils:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "ShowHideDeatils", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    Private Sub setShare2(ByVal strExchng As String, ByVal strShareVal As String)
        Try
            Dim dtSelectedShare As DataTable
            dtSelectedShare = Nothing
            Select Case objELNRFQ.Db_Get_Selected_Share("EQ", strExchng, "", strShareVal, dtSelectedShare)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlShareEQO2
                        .Items.Clear()
                        .DataSource = dtSelectedShare
                        .DataTextField = "LongName"
                        .DataValueField = "Code"
                        .DataBind()
                        .SelectedIndex = 0
                    End With
                    ddlShareEQO2.SelectedIndex = ddlShareEQO2.Items.IndexOf(ddlShareEQO2.Items.FindItemByValue(strShareVal))
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlShareEQO2
                        .DataSource = dtShare
                        .DataBind()
                    End With
            End Select
        Catch ex As Exception

        End Try
    End Sub
    'Mohit Lalwani on 20-Jan-2015
    Private Sub btnDetailsCancle_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetailsCancle.ServerClick
        pnlDetailsPopup.Visible = False
        upnlDetails.Update()
        ShowHideDeatils(False)
    End Sub
    '/Mohit Lalwani on 20-Jan-2015
    Private Sub setShare3(ByVal strExchng As String, ByVal strShareVal As String)
        Try
            Dim dtSelectedShare As DataTable
            dtSelectedShare = Nothing
            Select Case objELNRFQ.Db_Get_Selected_Share("EQ", strExchng, "", strShareVal, dtSelectedShare)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    With ddlShareEQO3
                        .Items.Clear()
                        .DataSource = dtSelectedShare
                        .DataTextField = "LongName"
                        .DataValueField = "Code"
                        .DataBind()
                        .SelectedIndex = 0
                    End With
                    ddlShareEQO3.SelectedIndex = ddlShareEQO3.Items.IndexOf(ddlShareEQO3.Items.FindItemByValue(strShareVal))
                Case Web_ELNRFQ.Database_Transaction_Response.Db_No_Data
                    With ddlShareEQO3
                        .DataSource = dtShare
                        .DataBind()
                    End With
            End Select
        Catch ex As Exception

        End Try
    End Sub
    ''</Rushikesh to set share value for grid selection and for other operations>
    Private Function checkIssuerLimit(ByVal PPName As String) As Boolean
        Dim min As Double
        Dim max As Double
        Select Case PPName.ToUpper
            Case "BNPP"      
                min = CDbl(Split(lblBNPPlimit.ToolTip, " / ")(0).Trim)
                max = CDbl(Split(lblBNPPlimit.ToolTip, " / ")(1).Trim)
        End Select





        If rdbQuantity.Checked Then

            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_AllowIssuerLimitCheckForAccDec", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
                Case "Y", "YES"
                    Dim EqPair As String = ddlShareEQO.SelectedValue.ToString & " - " & lblEQOBaseCcy.Text
                    Dim BidRate As Double = objELNRFQ.GetShareRate(EqPair, BidRate)
                    ''Dim iEQODays As Integer '' Daily no of shares not required in EQO.
                    If BidRate = 0 Or BidRate = Nothing Then
                        lblerror.Text = "Cannot proceed. Share rate not specified."
                        Return False
                    Else
                        '<'' Daily no of shares not required in EQO.>
                        'Select Case PPName.ToUpper
                        '    Case "BNPP"
                        '        iEQODays = CInt(lblBNPPClientPrice.Text.Replace(",", ""))
                        'End Select
                        '</ Daily no of shares not required in EQO.>

                        ''Dim Notional As Double = (Convert.ToDouble(txtOrderqtyEQO.Text) * iEQODays * BidRate) '<'' Daily no of shares not required in EQO.>
                        Dim Notional As Double = (Convert.ToDouble(txtOrderqtyEQO.Text) * BidRate)


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
        Else
            Dim Notional As Double = Convert.ToDouble(txtNotional.Text)
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



    End Function

    Private Sub txtUpfrontPopUpValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUpfrontPopUpValue.TextChanged
        Try
            'Added by Mohit Lalwani on 1-Aug-2016
            RestoreSolveAll()
            RestoreAll()
            '/Added by Mohit Lalwani on 1-Aug-2016
            txtUpfrontPopUpValue.Text = SetNumberFormat(txtUpfrontPopUpValue.Text, 2)
            lblerrorPopUp.Text = "" '<AvinashG. on 26-Feb-2016: FA-1232 Client yield validation message on order confirmation pop should get clear when upfront changes >
            chkUpfrontOverride.Checked = False
            chkUpfrontOverride.Visible = False
            If txtUpfrontPopUpValue.Text.Trim <> "" Then
                If ddlSideEQO.SelectedValue = "Buy" Then
                    lblClientPricePopUpValue.Text = FormatNumber((Double.Parse(lblIssuerPricePopUpValue.Text.Trim) + Double.Parse(txtUpfrontPopUpValue.Text.Trim)).ToString, 2)
                Else
                    lblClientPricePopUpValue.Text = FormatNumber((Double.Parse(lblIssuerPricePopUpValue.Text.Trim) - Double.Parse(txtUpfrontPopUpValue.Text.Trim)).ToString, 2)
                End If
            Else
                lblClientPricePopUpValue.Text = FormatNumber(Double.Parse(lblIssuerPricePopUpValue.Text.Trim).ToString, 2)
            End If
        Catch ex As Exception
            lblerror.Text = "txtUpfrontPopUpValue_TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtUpfrontPopUpValue_TextChanged", ErrorLevel.High)
            Throw ex
        End Try
    End Sub
    
    Public Sub txtNotional_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNotional.TextChanged
        Dim strcount As Integer = 0
        Dim sQty As Integer = 0
        Try
            lblerror.Text = ""
            If txtNotional.Text = "" Then
                txtNotional.Text = "0"
            End If

            If Qty_Validate(txtNotional.Text) = False Then
                Exit Sub
            End If
            Try
                txtNotional.Text = FinIQApp_Number.ConvertFormattedAmountToNumber(txtNotional.Text).ToString
                txtNotional.Text = SetNumberFormat(txtNotional.Text, 0)  '' EQBOSDEV-228 Replaced '2' by '0' on 21-jan-16 for removing decimals from notional by chaitali F
            Catch ex As Exception
                lblerror.Text = "Please enter valid Notional"
            End Try
            ResetAll()          ''Sequence changed by AshwiniP on 24-Oct-2016
            GetCommentary_EQO()
           '' ResetAll()
        Catch ex As Exception
            lblerror.Text = "txtNotional _TextChanged:Error occurred in processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                         sSelfPath, "txtNotional _TextChanged", ErrorLevel.High)
            Throw ex
        End Try

    End Sub

    Public Function CheckBestPriceForEmail() As String
        Try
            Dim temp As HiddenField
            temp = getBestPrice(BNPPHiddenPrice, BNPPHiddenPrice) ''******* IMP to do Self Comparing due to single counter party need to discuss when more than one CP ********************* 

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

            ''****** IMP -For client BUY, lowest premium is best. For client SELL, highest premium is best 
            If ddlSideEQO.SelectedValue.ToUpper = "BUY" Then
                If Value1 = 0.0 And Value2 = 0.0 Then
                    Return New HiddenField()
                End If

                If Value1 = 0.0 And Value2 < 0.0 Then
                    Return hiddenPriceCsv2
                ElseIf Value2 = 0.0 And Value1 < 0.0 Then
                    Return hiddenPriceCsv1
                End If

                If Value2 < Value1 Then
                    Return hiddenPriceCsv2
                Else
                    Return hiddenPriceCsv1
                End If

            ElseIf ddlSideEQO.SelectedValue.ToUpper = "SELL" Then
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

            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "getBestPrice", ErrorLevel.High)
            Throw ex
        End Try
    End Function
    Public Function CheckBestPrice() As Boolean
        Try
            Dim temp As HiddenField
            temp = getBestPrice(BNPPHiddenPrice, BNPPHiddenPrice) ''******* IMP to do Self Comparing due to single counter party need to discuss when more than one CP ********************* 
            If temp.ID Is Nothing Then
            Else
                If (temp.ID.ToUpper.Contains("BNPP")) Then
                    ''TRJPM1.Attributes.Remove("class") ''********* Added when new LP
                    TRBNPP1.Attributes.Add("class", "lblBestPrice")
                    hdnBestPremium.Value = lblBNPPPrice.Text
                    hdnBestProvider.Value = "BNPP"
                End If
                GetCommentary_EQO()
                checkDuplicate(temp)
            End If
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "CheckBestPrice", ErrorLevel.High)
            Throw ex
        End Try
    End Function

    Public Function checkDuplicate(ByVal bestPriceCSV As HiddenField) As String
        Try
            Dim PriceArray1() As String
            Dim Value1 As Double
            PriceArray1 = Split(bestPriceCSV.Value, ";")
            Value1 = CDbl(Split(PriceArray1(0), ",")(0).ToString) '<Changed By Imran on 19-Dec-2015 FA-1236 - Allow user to Quote without Notional>
            compareDuplicate(Value1, BNPPHiddenPrice)

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
       If (temp.ID.ToUpper.Contains("BNPP")) Then
            TRBNPP1.Attributes.Add("class", "lblBestPrice")
        End If
    End Function
    
     ''' <summary>
    ''' Sets all Cpty limit labels to NA and their tooltips to blank
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setLimitsAsNA()
        Try
            'lblJPMlimit.Text = "N.A."
            lblBNPPlimit.Text = "N.A."
            'lblUBSlimit.Text = "N.A."
            'lblHSBClimit.Text = "N.A."
            'lblCSLimit.Text = "N.A."
            'lblBAMLlimit.Text = "N.A."
            'lblDBIBLimit.Text = "N.A."
            'lblOCBClimit.Text = "N.A."
            'lblCITIlimit.Text = "N.A."

            'lblJPMlimit.ToolTip = ""
            lblBNPPlimit.ToolTip = ""
            'lblUBSlimit.ToolTip = ""
            'lblHSBClimit.ToolTip = ""
            'lblCSLimit.ToolTip = ""
            'lblBAMLlimit.ToolTip = ""
            'lblDBIBLimit.ToolTip = ""
            'lblOCBClimit.ToolTip = ""
            'lblCITIlimit.ToolTip = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Hide all Issuer controls like labels, price deal buttons, TRs, TDs
    ''' </summary>
    ''' <param name="strIssuerCode"></param>
    ''' <remarks></remarks>
    Private Sub hideIssuerControls(ByVal strIssuerCode As String)
        Try
            Select Case strIssuerCode
                'Case "JPM"
                '    btnJPMprice.Visible = False
                '    btnJPMDeal.Visible = False
                '    tdJPM1.Style.Remove("display")
                '    tdJPM1.Style.Add("display", "none !important;")
                '    lblJPM.Visible = False
                '    lblJPMPrice.Visible = False
                '    lblTimerJPM.Visible = False
                '    TRJPM1.Visible = False
                'Case "HSBC"
                '    btnHSBCPrice.Visible = False
                '    btnHSBCDeal.Visible = False
                '    tdHSBC1.Style.Remove("display")
                '    tdHSBC1.Style.Add("display", "none !important;")
                '    lblHSBC.Visible = False
                '    lblHSBCPrice.Visible = False
                '    lblTimerHSBC.Visible = False
                '    TRHSBC1.Visible = False
                'Case "CITI"
                '    btnCITIprice.Visible = False
                '    btnCITIDeal.Visible = False
                '    tdCITI1.Style.Remove("display")
                '    tdCITI1.Style.Add("display", "none !important;")
                '    lblCITI.Visible = False
                '    lblCITIPrice.Visible = False
                '    lblTimerCITI.Visible = False
                '    TRCITI1.Visible = False
                'Case "OCBC"
                '    btnOCBCprice.Visible = False
                '    btnOCBCDeal.Visible = False
                '    tdOCBC1.Style.Remove("display")
                '    tdOCBC1.Style.Add("display", "none !important;")
                '    lblOCBC.Visible = False
                '    lblOCBCPrice.Visible = False
                '    lblTimerOCBC.Visible = False
                '    TROCBC1.Visible = False
                'Case "CS"
                '    btnCSPrice.Visible = False
                '    btnCSDeal.Visible = False
                '    tdCS1.Style.Remove("display")
                '    tdCS1.Style.Add("display", "none !important;")
                '    lblCS.Visible = False
                '    lblCSPrice.Visible = False
                '    lblTimerCS.Visible = False
                '    TRCS1.Visible = False
                Case "BNPP"
                    btnBNPPPrice.Visible = False
                    btnBNPPDeal.Visible = False
                    tdBNPP1.Style.Remove("display")
                    tdBNPP1.Style.Add("display", "none !important;")
                    lblBNPP.Visible = False
                    lblBNPPPrice.Visible = False
                    lblTimerBNPP.Visible = False
                    TRBNPP1.Visible = False
                    'Case "UBS"
                    '    btnUBSPrice.Visible = False
                    '    btnUBSDeal.Visible = False
                    '    tdUBS1.Style.Remove("display")
                    '    tdUBS1.Style.Add("display", "none !important;")
                    '    lblUBS.Visible = False
                    '    lblUBSPrice.Visible = False
                    '    lblTimerUBS.Visible = False
                    '    TRUBS1.Visible = False
                    'Case "BAML"
                    '    btnBAMLPrice.Visible = False
                    '    btnBAMLDeal.Visible = False
                    '    tdBAML1.Style.Remove("display")
                    '    tdBAML1.Style.Add("display", "none !important;")
                    '    lblBAML.Visible = False
                    '    lblBAMLPrice.Visible = False
                    '    lblTimerBAML.Visible = False
                    '    TRBAML1.Visible = False
                    'Case "DBIB"
                    '    btnDBIBPrice.Visible = False
                    '    btnDBIBDeal.Visible = False
                    '    tdDBIB.Style.Remove("display")
                    '    tdDBIB.Style.Add("display", "none !important;")
                    '    lblDBIB.Visible = False
                    '    lblDBIBPrice.Visible = False
                    '    lblTimerDBIB.Visible = False
                    '    TRDBIB.Visible = False
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Shows all Issuer controls like labels, price deal buttons, TRs, TDs
    ''' </summary>
    ''' <param name="strIssuerCode"></param>
    ''' <remarks></remarks>
    Private Sub showIssuerControls(ByVal strIssuerCode As String)
        Try
            Select Case strIssuerCode
                'Case "JPM"
                '    btnJPMprice.Visible = True
                '    tdJPM1.Style.Remove("display")
                '    lblJPM.Visible = True
                '    lblJPMPrice.Visible = True
                '    lblTimerJPM.Visible = True
                '    TRJPM1.Visible = True
                'Case "HSBC"
                '    btnHSBCPrice.Visible = True
                '    tdHSBC1.Style.Remove("display")
                '    lblHSBC.Visible = True
                '    lblHSBCPrice.Visible = True
                '    lblTimerHSBC.Visible = True
                '    TRHSBC1.Visible = True
                'Case "CITI"
                '    btnCITIprice.Visible = True
                '    tdCITI1.Style.Remove("display")
                '    lblCITI.Visible = True
                '    lblCITIPrice.Visible = True
                '    lblTimerCITI.Visible = True
                '    TRCITI1.Visible = True
                'Case "OCBC"
                '    btnOCBCprice.Visible = True
                '    tdOCBC1.Style.Remove("display")
                '    lblOCBC.Visible = True
                '    lblOCBCPrice.Visible = True
                '    lblTimerOCBC.Visible = True
                '    TROCBC1.Visible = True
                'Case "CS"
                '    btnCSPrice.Visible = True
                '    tdCS1.Style.Remove("display")
                '    lblCS.Visible = True
                '    lblCSPrice.Visible = True
                '    lblTimerCS.Visible = True
                '    TRCS1.Visible = True
                Case "BNPP"
                    btnBNPPPrice.Visible = True
                    tdBNPP1.Style.Remove("display")
                    lblBNPP.Visible = True
                    lblBNPPPrice.Visible = True
                    lblTimerBNPP.Visible = True
                    TRBNPP1.Visible = True
                    'Case "UBS"
                    '    btnUBSPrice.Visible = True
                    '    tdUBS1.Style.Remove("display")
                    '    lblUBS.Visible = True
                    '    lblUBSPrice.Visible = True
                    '    lblTimerUBS.Visible = True
                    '    TRUBS1.Visible = True
                    'Case "BAML"
                    '    btnBAMLPrice.Visible = True
                    '    tdBAML1.Style.Remove("display")
                    '    lblBAML.Visible = True
                    '    lblBAMLPrice.Visible = True
                    '    lblTimerBAML.Visible = True
                    '    TRBAML1.Visible = True
                    'Case "DBIB"
                    '    btnDBIBPrice.Visible = True
                    '    tdDBIB.Style.Remove("display")
                    '    lblDBIB.Visible = True
                    '    lblDBIBPrice.Visible = True
                    '    lblTimerDBIB.Visible = True
                    '    TRDBIB.Visible = True
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    
     Private Sub rdbNotional_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbNotional.CheckedChanged
        Try
            chkrdbQuantity()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "rdbNotional_CheckedChanged", ErrorLevel.High)
        End Try
    End Sub

    Private Sub rdbQuantity_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbQuantity.CheckedChanged
        Try
            chkrdbQuantity()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                        sSelfPath, "rdbQuantity_CheckedChanged", ErrorLevel.High)
        End Try
    End Sub


    Private Sub chkrdbQuantity()
        Try
            If rdbNotional.Checked Then
                lblNotionalWithCcy1.Visible = True
            Else
                lblNotionalWithCcy1.Visible = False
            End If

        If rdbNotional.Checked Then
            lblEstimatedNoOfDays.Text = ""
            lblNotionalWithCcy.Text = ""
            lblEstimatedNotional.Text = ""
            txtOrderqtyEQO.Enabled = False
            txtOrderqtyEQO.Text = ""
                txtOrderqtyEQO.BackColor = Color.FromArgb(242, 242, 243)
            ddlInvestCcy.Enabled = False
                ddlInvestCcy.BackColor = Color.FromArgb(242, 242, 243)
            txtNotional.Enabled = True
            txtNotional.BackColor = Color.White
            '<MohitL. on 15-Apr-2016:Set Default notional size and daily no. of shares to 0 on main pricer screen JIRA ID: EQBOSDEV-321>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SetZeroNotional_MainPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                Case "Y", "YES"
                    txtNotional.Text = "0"
                Case "N", "NO"
                        txtNotional.Text = getControlPersonalSetting("Notional", "1,000,000")
            End Select
            '</MohitL. on 15-Apr-2016:Set Default notional size and daily no. of shares to 0 on main pricer screen JIRA ID: EQBOSDEV-321>
        Else
            txtOrderqtyEQO.Enabled = True
            txtOrderqtyEQO.BackColor = Color.White
            ddlInvestCcy.Enabled = True
            ddlInvestCcy.BackColor = Color.White
            txtNotional.Enabled = False
                txtNotional.BackColor = Color.FromArgb(242, 242, 243)
            txtNotional.Text = ""

            '<MohitL. on 15-Apr-2016:Set Default notional size and daily no. of shares to 0 on main pricer screen JIRA ID: EQBOSDEV-321>
            Select Case objReadConfig.ReadConfig(dsConfig, "EQC_SetZeroNotional_MainPricer", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
                Case "Y", "YES"
                    txtOrderqtyEQO.Text = "0"
                Case "N", "NO"
                        txtOrderqtyEQO.Text = getControlPersonalSetting("No. of shares", "2,000")
            End Select
            '</MohitL. on 15-Apr-2016:Set Default notional size and daily no. of shares to 0 on main pricer screen JIRA ID: EQBOSDEV-321>
            DisplayEstimatedNotional()
            End If
            GetCommentary_EQO()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    
    
      Private Sub hideShowRBLShareData()
        Dim count As Integer = 0

        Dim tabIndex As Integer = tabContainer.ActiveTabIndex

        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowTRShareInfo", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "YES").Trim.ToUpper
            Case "Y", "YES"
                count = count + 1
            Case "N", "NO"
                rblShareData.Items.FindByValue("SHAREDATA").Attributes.Add("style", "display:none")
                rowShareData.Visible = False
        End Select
        Select Case objReadConfig.ReadConfig(dsConfig, "EQC_DisplayGraph", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper
            Case "Y", "YES"
                count = count + 1
            Case "N", "NO"
                rblShareData.Items.FindByValue("GRAPHDATA").Attributes.Add("style", "display:none")
                rowGraphData.Visible = False
        End Select
        
        'If tabIndex = 5 Then
        '    rblShareData.Items.FindByValue("calc").Attributes.Add("style", "display:none")
        'End If

        If count >= 2 Then
            rblShareData.Visible = True
            tdrblShareData.Visible = True '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
        Else
            rblShareData.Visible = False
            tdrblShareData.Visible = False '<Added By Mohit Lalwnai on 1-Apr-2016 FA-1384>
        End If

    End Sub

    ''' <summary>
    ''' Made common function to get Maturity days at the time of share selection and from grid selection
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function getMaturityDays(ByVal strExchange As String) As String
        Try
            '<AvinashG. on 11-Jul-2016: AS per Deutsche(Aditya) mail> Settlement_Days = txtSettlDays.Text '<AvinashG. on 21-Apr-2016: as per Mahesh's mail>
            Dim dtExchangeInfo As DataTable
            Dim Maturity_Days As String
            Dim dr As DataRow()
            dtExchangeInfo = New DataTable("Dummy")
            dtExchangeInfo = CType(Session("Exchange_Details"), DataTable)

            If ddlExchangeEQO.SelectedValue.ToUpper = "ALL" Then
                Dim sTemp As String
                'dr = dtExchangeInfo.Select("ExchangeCode = '" & ddlExchangeEQO.SelectedValue.Trim.ToUpper & "' ")
                dr = dtExchangeInfo.Select("Exchange_Name = '" & objELNRFQ.GetShareExchange(strExchange, sTemp) & "' ")
            Else
                dr = dtExchangeInfo.Select("Exchange_Name = '" & strExchange.ToUpper & "' ")
            End If

            If dr.Length > 0 Then
                Maturity_Days = dr(0).Item("SettlementDays").ToString
                If Val(Maturity_Days) = 0 Then
                    Maturity_Days = "2"   ''if settlement is 0 in db then take it 2 by default,told by Kalyan M.
                Else
                    Maturity_Days = Maturity_Days
                End If
            Else
                Maturity_Days = "2"
            End If
            Return Maturity_Days
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Added by Mohit Lalwani on 1-Aug-2016
    Private Sub ddlBookingBranchPopUpValue_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBookingBranchPopUpValue.SelectedIndexChanged
        RestoreSolveAll()
        RestoreAll()

        '<RiddhiS. on 10-Nov-2016: On change of book center, reset customer grid>
        Session.Remove("dtEQOPreTradeAllocation")
        Dim tempDt As DataTable
        tempDt = New DataTable("tempDt")
        tempDt.Columns.Add("RM_Name", GetType(String))
        tempDt.Columns.Add("Account_Number", GetType(String))
        tempDt.Columns.Add("AlloNotional", GetType(String))
        tempDt.Columns.Add("Cust_ID", GetType(String))
        tempDt.Columns.Add("DocId", GetType(String))
        tempDt.Columns.Add("EPOF_OrderId", GetType(String))
        tempDt.Rows.InsertAt(tempDt.NewRow(), 0)
        Session.Add("dtEQOPreTradeAllocation", tempDt)
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

    Private Sub ddlBasketSharesPopValue_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBasketSharesPopValue.SelectedIndexChanged
        RestoreSolveAll()
        RestoreAll()
    End Sub


    ''Added By nikhil M on  08Aug16  EQSCB-16
    Public Function SetDllBookingBranch() As Boolean
        ddlBookingBranchPopUpValue.Items.Add(New ListItem("Hong Kong", "HK").ToString)
        ddlBookingBranchPopUpValue.Items.Add(New ListItem("Singapore", "SG").ToString)
        ddlBookingBranchPopUpValue.SelectedIndex = 1
    End Function


    Private Sub addPPimg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles addPPimg.Click
        chkPPmaillist.Visible = True
    End Sub


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

            lblerror.Text = "TmRefresh_Tick: Error occurred in Processing."

            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "TmRefresh_Tick", ErrorLevel.High)
        End Try
    End Sub
    'Ended by Chitralekha on 12-sept-16
    Private Sub chkBNPP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBNPP.CheckedChanged
        Try
            hdnBestPremium.Value = lblBNPPPrice.Text
            hdnBestProvider.Value = "BNPP"
            GetCommentary_EQO()
        Catch ex As Exception
            lblerror.Text = "chkBNPP_CheckedChanged: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "chkBNPP_CheckedChanged", ErrorLevel.High)
        End Try
    End Sub
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


    '<Rushikesh Start>
    Private Sub funAdd()
        Try

            Dim tempDt As DataTable
            tempDt = New DataTable("tempDt")
            If CType(Session("dtEQOPreTradeAllocation"), DataTable) Is Nothing Then
            Else
                tempDt = CType(Session("dtEQOPreTradeAllocation"), DataTable)
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
                tempDt.Columns.Add("EPOF_OrderId", GetType(String))
                tempDt.Rows.InsertAt(tempDt.NewRow(), 0)
            Else
                tempDt.Rows.InsertAt(tempDt.NewRow(), tempDt.Rows.Count)
            End If

            Session.Add("dtEQOPreTradeAllocation", tempDt)

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

                    ddl_OnSelectedIndexChanged(CType(grdRMData.Rows(k).Cells(0).FindControl("ddlRMName"), RadDropDownList), Nothing)        'Mohit Lalwani on 04-Nov-2016

                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).setCustName = tempDt.Rows(k).Item("Account_Number").ToString
                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCustomerName = tempDt.Rows(k).Item("Account_Number").ToString
                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCustomerId = tempDt.Rows(k).Item("Cust_ID").ToString
                    CType(grdRMData.Rows((k)).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenDocId = tempDt.Rows(k).Item("DocId").ToString
                    'Select Case objReadConfig.ReadConfig(dsConfig, "EQC_ShowAllocationRMName", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "NO").Trim.ToUpper   ''Commented by AshwiniP on 13-Oct-2016
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
            '' Dim maxAcclength As String = objReadConfig.ReadConfig(dsConfig, "EQC_PostTrade_AccountNumLength", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "10").Trim.ToUpper   ''Commented by AshwiniP on 13-Oct-2016
            tempDt = CType(Session("dtEQOPreTradeAllocation"), DataTable)
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
            lblerror.Text = "Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                          sSelfPath, "OnCheckedChanged", ErrorLevel.High)
        End Try
    End Sub


    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim tempDt As DataTable
                tempDt = CType(Session("dtEQOPreTradeAllocation"), DataTable)
                Dim rowDataBoundIndex As Integer = e.Row.RowIndex
                Dim ddlRM_Name As RadDropDownList = TryCast(e.Row.FindControl("ddlRMName"), RadDropDownList)

                Dim lblRM_Name As Label
                If Not IsNothing(TryCast(e.Row.FindControl("txtRM_Name"), Label)) Then
                    lblRM_Name = TryCast(e.Row.FindControl("txtRM_Name"), Label)
                Else
                    lblRM_Name.Text = ""
                End If
                With ddlRM_Name
                    .DataSource = Session("DTRMList")
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
                If lblCIFPAN.Text <> "" Then
                    ddlCIFPAN.setCustName = lblCIFPAN.Text
                    ddlCIFPAN.HiddenCustomerId = tempDt.Rows(rowDataBoundIndex).Item("Cust_ID").ToString
                    ddlCIFPAN.HiddenDocId = tempDt.Rows(rowDataBoundIndex).Item("DocId").ToString
                End If
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
            tempDt = CType(Session("dtEQOPreTradeAllocation"), DataTable)
            dtCIFPANTemp = New DataTable("dtCIFPANTemp")
            If CType(sender, RadDropDownList).ID = "ddlRMName" Then
                tempDt.Rows(I).Item("RM_Name") = CType(sender, RadDropDownList).SelectedItem.Text
                grdRMData.Rows(I).Cells(grdRMDataEnum.RM_Name).Controls.OfType(Of Label)().FirstOrDefault().Text = CType(sender, RadDropDownList).SelectedItem.Text
                ' SetDrpCIFPAN(LoginInfoGV.Login_Info.EntityID.ToString, CType(sender, RadDropDownList).SelectedItem.Text, dtCIFPANTemp)
                'With grdRMData.Rows(I).Cells(grdRMDataEnum.Account_Number).Controls.OfType(Of RadDropDownList)().FirstOrDefault()
                '    .DataTextField = "Name"
                '    .DataValueField = "CIF_PANId"
                '    .DataSource = dtCIFPANTemp
                '    .DataBind()
                '    .Items.Insert(0, "")
                'End With
                '   CType(grdRMData.Rows(I).Cells(grdRMDataEnum.Account_Number).FindControl("ddlCIFPAN"), RadDropDownList).SelectedIndex = 0
                ' CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).Customer_Filter.ENTITY_LRC_SELECTED_RM

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
            Session.Add("dtEQOPreTradeAllocation", tempDt)
            Session.Add("dtCustomerCodes", dtCIFPANTemp)   '<RiddhiS. on 03-Oct-2016: To get Customer Segment>
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
              sSelfPath, "ddl_OnSelectedIndexChanged", ErrorLevel.High)
        End Try
    End Sub

    Protected Sub ddlCIFPAN_onTextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim I, cntCodes As Integer
        Dim tempDt As DataTable
        Dim dtCustomerCodes As DataTable
        tempDt = CType(Session("dtEQOPreTradeAllocation"), DataTable)
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

                    '<RiddhiS. on 10-Nov-2016: Commented>
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

                    '    For cntCodes = 0 To dtCustomerCodes.Rows.Count - 1
                    '    If dtCustomerCodes.Rows(cntCodes).Item("CIF_PANId").ToString = CType(sender, RadDropDownList).SelectedItem.Text Then
                    '        If dtCustomerCodes.Rows(cntCodes).Item("Segment").ToString.ToUpper = "RETAIL" Then
                    '            ddlBookingBranchPopUpValue.SelectedValue = "Retail"
                    '            ddlBookingBranchPopUpValue.SelectedText = "Singapore Retail"
                    '            Exit For
                    '        Else
                    '            ddlBookingBranchPopUpValue.SelectedValue = "SG"
                    '            ddlBookingBranchPopUpValue.SelectedText = "Singapore"
                    '        End If
                    '    End If
                    'Next
                    'End If
                    '</RiddhiS. on 03-Oct-2016>



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
            Session.Add("dtEQOPreTradeAllocation", tempDt)

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

            If rdbQuantity.Checked Then
                lblTotalAmtVal.Text = lblNoOfSharePopUpValue.Text
            Else
                lblTotalAmtVal.Text = lblNotionalPopUpValue.Text
            End If
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

    Function chkDuplicateRecords() As Boolean ''Add this validation to end of function only
        Try
            Dim tempDt As DataTable
            tempDt = New DataTable("tempDt")
            Dim sDuplicateRec As String = ""
            Dim sDuplicateRec1 As String = ""
            Dim j As Integer = 0
            Dim x As Integer = 0
            tempDt = CType(Session("dtEQOPreTradeAllocation"), DataTable)
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

            'chkHSBC.Checked = False
            chkBNPP.Checked = False
           
        Catch ex As Exception
				Throw ex
        End Try

    End Function

    ''<Nikhil M. on 20-Sep-2016: Added to reset COmmetry element>
    Public Function ResetCommetryElement() As Boolean
        Try
            hdnBestPremium.Value = ""
            hdnBestProvider.Value = ""
            'chkHSBC.Checked = False
            chkBNPP.Checked = False
            GetCommentary_EQO()
        Catch ex As Exception
            lblerror.Text = "ResetAllChkBox: Error occurred in Processing."
            Throw ex
        End Try

    End Function


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
        tempDt = CType(Session("dtEQOPreTradeAllocation"), DataTable)
        lblerrorPopUp.Text = ""
        Try
            I = CInt(Customer_Info.ItemIndex)
            tempDt.Rows(I).Item("Account_Number") = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).GetCustName
            tempDt.Rows(I).Item("Cust_ID") = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenCustomerId   'Customer_Info.CustomerCIFNo
            tempDt.Rows(I).Item("DocId") = CType(grdRMData.Rows(I).FindControl("FindCustomer"), FinIQ_Fast_Find_Customer).HiddenDocId
            grdRMData.Rows(I).Cells(grdRMDataEnum.Account_Number).Controls.OfType(Of Label)().FirstOrDefault().Text = Customer_Info.CustomerName      'Customer_Info.CustomerName

            Session.Add("dtEQOPreTradeAllocation", tempDt)

            '<RiddhiS. on 10-Nov-2016: Commented>
            ''After discussion with Milind K, Sanchita: Booking center dropdown is made editable and based on the selection customer's are filtered.
            '<AvinashG on 09-Oct-2016: Removing temporary drop down and using customer control itself>
            'If I = 0 Then               ''To Change only according to first row
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


            GetTotalRemainAlloLables()
        Catch ex As Exception
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
              sSelfPath, "CustomerSelected", ErrorLevel.High)
        End Try

    End Sub

    '/Added by Mohit Lalwani on 30-sept-2016 for customer control





'Added by Mohit Lalwani on 17-Oct-2016
    Private Sub btnSaveSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveSettings.Click
        Dim o_strXMLNote_DefaultValues As String
        Dim DefaultSetting_Level As String

        Try

            If Write_PersonalSettings_TOXML(o_strXMLNote_DefaultValues) = True Then
                Select Case objELNRFQ.Web_Insert_EQO_DefaultSettings(CStr(o_strXMLNote_DefaultValues), CStr(LoginInfoGV.Login_Info.EntityID), LoginInfoGV.Login_Info.LoginId, "EQO_DealEntry", objReadConfig.ReadConfig(dsConfig, "EQC_DefaultSetting_Level", "ELN", CStr(LoginInfoGV.Login_Info.EntityID), "ENTITY").Trim.ToUpper, LoginInfoGV.Login_Info.LoginId)
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
'/Added by Mohit Lalwani on 17-Oct-2016



   '<RiddhiS. on 19-Oct-2016: For generating pre trade termsheet>
    Private Sub Generate_EQO(ByVal DealNO As String)

        Try
            Dim objDocGen As Web_DocGenOpenXML.DocumentGenerationOpenXml
            objDocGen = New Web_DocGenOpenXML.DocumentGenerationOpenXml
            objDocGen.Url = LoginInfoGV.Login_Info.WebServicePath & "/DocumentGenerationOpenXml/DocumentGenerationOpenXml.asmx"
            Dim O_Outputfile As String
            Dim strDocGenVirtualPath As String = String.Empty
            Dim strURL As String = String.Empty

            strDocGenVirtualPath = objReadConfig.ReadConfig(New DataSet, "WebDocGen_VirtualPath", "DocGen", CStr(LoginInfoGV.Login_Info.EntityID), "")
            O_Outputfile = objDocGen.StartDocumentGeneration(LoginInfoGV.Login_Info.LoginId, "EQO", "PUBLISH_TERMSHEET", DealNO, "ELN_RFQ", LoginInfoGV.Login_Info.EntityID.ToString, LoginInfoGV.Login_Info.GlobalServerDateTime, 1)


            '<RiddhiS. on 08-Nov-2016: For creating proper URL>
            strURL = LoginInfoGV.Login_Info.WebServicePath.ToString.Substring(0, LoginInfoGV.Login_Info.WebServicePath.ToString.IndexOf("FinIQWeb_WebService"))

            If Not IsNothing(O_Outputfile) Then
                If O_Outputfile.Length > 0 Then
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSgetDescription5446", " window.open('" & LoginInfoGV.Login_Info.WebServicePath & "/../" & strDocGenVirtualPath & "/" & System.IO.Path.GetFileName(O_Outputfile) & "','CUSTOMER_PROFILE','scrollbars=yes,resizable=yes,menubar=0,status=0,width=1280,height=650,top=0,left=0');", True)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSgetDescription5446", " window.open('" & strURL & "/../" & strDocGenVirtualPath & "/" & System.IO.Path.GetFileName(O_Outputfile) & "','CUSTOMER_PROFILE','scrollbars=yes,resizable=yes,menubar=0,status=0,width=1280,height=650,top=0,left=0');", True)
                    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "JSSpineer", "setSpineer();", True)
                    '   AttachtoEmail(O_Outputfile, DealNO)
                Else
                    lblerror.Text = "Document generation failed"
                End If
            Else
            End If
        Catch ex As Exception
            lblerror.Text = "Generate_EQO: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "Generate_EQO", ErrorLevel.High)
        End Try
    End Sub


    Private Sub AttachtoEmail(ByVal strFileName As String, ByVal DealNO As String)
        Try
            'oWEBADMIN = New WEB_ADMINISTRATOR.LSSAdministrator
            'Dim strLoginUserEmail As String = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)
            'Dim FileText As String = File.ReadAllText(strFileName)
            'Dim mailSubject As String = "EQO"  'Temporary
            'Dim dtImageDetails As DataTable
            'Dim filePath As String = "C:\BULK\" & strFileName
            'dtImageDetails = New DataTable("dtImageDetails")

            'If oWEBADMIN.Notify_ToDealerDeskGroupEmailID_imageContent(LoginInfoGV.Login_Info.EntityID.ToString(), _
            '                                                                          LoginInfoGV.Login_Info.LoginId, strLoginUserEmail, FileText, mailSubject.ToString(), filePath, dtImageDetails, "") Then
            '    lblerror.ForeColor = Color.Blue
            '    lblerror.Text = "Email sent successfully."
            'Else
            '    lblerror.ForeColor = Color.Blue
            '    lblerror.Text = "Email sending failed."
            'End If

            Dim strLoginUserEmail As String = objELNRFQ.web_Get_EmailOf_Login_User(LoginInfoGV.Login_Info.LoginId)
            Dim FileText As String = File.ReadAllText(strFileName)
            Dim mailSubject As String = "EQO Termsheet for " & DealNO  'Temporary
            Dim dtImageDetails As DataTable
            Dim filePath As String = strFileName
            Dim errMsg As String = String.Empty
            Select Case objELNRFQ.Web_AttachDoctoEmail(LoginInfoGV.Login_Info.EntityID.ToString(), strLoginUserEmail, mailSubject, "PFA", True, "", "", "", True, filePath, errMsg)
                Case Web_ELNRFQ.Database_Transaction_Response.Db_Successful
                    lblerror.ForeColor = Color.Blue
                    lblerror.Text = "Email sent successfully."
                Case Web_ELNRFQ.Database_Transaction_Response.DB_Unsuccessful
                    lblerror.ForeColor = Color.Blue
                    lblerror.Text = "Email sending failed."
                Case Else
                    lblerror.ForeColor = Color.Blue
                    lblerror.Text = "Email sending failed."
            End Select



        Catch ex As Exception
            lblerror.Text = "AttachtoEmail: Error occurred in Processing."
            LogException(LoginInfoGV.Login_Info.LoginId, "Exception:" + ex.Message.ToString, LogType.FnqError, ex, _
                                sSelfPath, "AttachtoEmail", ErrorLevel.High)
        End Try
    End Sub


End Class
