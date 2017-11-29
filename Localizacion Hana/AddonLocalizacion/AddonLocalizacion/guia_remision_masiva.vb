﻿Imports System.Globalization

Public Class guia_remision_masiva


    Private XmlForm As String = Replace(Application.StartupPath & "\guia_remision_masiva.srf", "\\", "\")
    Private WithEvents SBO_Application As SAPbouiCOM.Application
    Private oForm As SAPbouiCOM.Form
    Private oCompany As SAPbobsCOM.Company
    Private oFilters As SAPbouiCOM.EventFilters
    Private oFilter As SAPbouiCOM.EventFilter
    Private selected As Boolean = False
    Public Sub New()
        MyBase.New()
        Try
            Me.SBO_Application = UDT_UF.SBOApplication
            Me.oCompany = UDT_UF.Company

            If UDT_UF.ActivateFormIsOpen(SBO_Application, "GREMISION_M") = False Then
                LoadFromXML(XmlForm)
                oForm = SBO_Application.Forms.Item("GREMISION_M")
                oForm.Left = 400



                Dim txtFechaini As SAPbouiCOM.EditText = oForm.Items.Item("Item_6").Specific
                Dim txtFechallega As SAPbouiCOM.EditText = oForm.Items.Item("Item_8").Specific

                oForm.Visible = True
                oForm.PaneLevel = 1
                oForm.DataSources.DataTables.Add("MyDataTable")
                oForm.DataSources.UserDataSources.Add("Date", SAPbouiCOM.BoDataType.dt_DATE)
                oForm.DataSources.UserDataSources.Add("Date2", SAPbouiCOM.BoDataType.dt_DATE)
                txtFechaini.DataBind.SetBound(True, "", "Date")
                txtFechallega.DataBind.SetBound(True, "", "Date2")
                ' Dim oTipoDocumento As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_1").Specific
                'Dim oBuscar As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_3").Specific
                'oTipoDocumento.ExpandType = SAPbouiCOM.BoExpandType.et_ValueDescription
                'oBuscar.ExpandType = SAPbouiCOM.BoExpandType.et_ValueDescription
            Else
                oForm = Me.SBO_Application.Forms.Item("GREMISION_M")
            End If
            Dim oTipoDocumento As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_1").Specific
            Dim oBuscar As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_3").Specific
            oTipoDocumento.ExpandType = SAPbouiCOM.BoExpandType.et_ValueDescription
            oBuscar.ExpandType = SAPbouiCOM.BoExpandType.et_ValueDescription
            llenarSeries()
        Catch ex As Exception
            SBOApplication.SetStatusBarMessage(ex.Message)
        End Try
    End Sub


    Private Sub LoadFromXML(ByRef FileName As String)
        Try
            Dim oXmlDoc As Xml.XmlDocument

            oXmlDoc = New Xml.XmlDocument

            ' ''// load the content of the XML File
            ''Dim sPath As String

            ''sPath = IO.Directory.GetParent(Application.StartupPath).ToString

            'oXmlDoc.Load(sPath & "\" & FileName)
            oXmlDoc.Load(FileName)

            '// load the form to the SBO application in one batch
            SBO_Application.LoadBatchActions(oXmlDoc.InnerXml)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SBO_Application_ItemEvent(ByVal FormUID As String, ByRef pVal As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean) Handles SBO_Application.ItemEvent
        Try

            If oForm Is Nothing Then
                Exit Sub
            End If

            If pVal.EventType = SAPbouiCOM.BoEventTypes.et_FORM_CLOSE And pVal.BeforeAction = True And pVal.FormUID = "GREMISION_M" Then
                oForm = Nothing
                oCompany = Nothing
                SBO_Application = Nothing
            End If

            If FormUID = "GREMISION_M" Then
                If FormUID = "GREMISION_M" And pVal.ItemUID = "Item_4" And pVal.Before_Action = True Then
                    If validar() = False Then
                        BubbleEvent = False
                        Return
                    End If

                    Dim oTipoDocumento As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_1").Specific
                    Dim oBusca As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_3").Specific
                    llenarGridView(oTipoDocumento.Value.Trim, oBusca.Value.Trim)
                    BubbleEvent = False
                    Return
                End If
                If FormUID = "GREMISION_M" And pVal.ItemUID = "Item_18" And pVal.Before_Action = True And pVal.EventType = SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED Then
                    Dim gridView As SAPbouiCOM.Grid
                    gridView = oForm.Items.Item("Item_17").Specific
                    Dim oTipoDocumento As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_1").Specific
                    Dim oBusca As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_3").Specific
                    If oTipoDocumento.Value.Trim <> "05" Then
                        If validaringreso() = False Then
                            BubbleEvent = False
                            Return
                        End If
                    End If



                    If gridView.Rows.SelectedRows.Count = 0 Then
                        SBO_Application.SetStatusBarMessage("Debe de Seleccionar al menos una Fila", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        BubbleEvent = False
                        Return
                    End If
                    If oTipoDocumento.Value.Trim <> "05" Then
                        Dim Progress = SBOApplication.StatusBar.CreateProgressBar("Generando Guia de remision para " & gridView.Rows.SelectedRows.Count & " Líneas seleccionadas", gridView.Rows.SelectedRows.Count, True)
                        'Progress.Value = 0
                        For i As Integer = 0 To gridView.Rows.Count - 1
                            If gridView.Rows.IsSelected(i) = True Then
                                GenerarGuiaRemision(oTipoDocumento.Value.Trim, oBusca.Value.Trim, gridView.DataTable.GetValue(0, i), gridView.DataTable.GetValue(1, i))
                                Progress.Value = Progress.Value + 1
                            End If

                        Next
                        gridView.Rows.SelectedRows.Clear()
                        gridView.DataTable.Clear()
                        Progress.Stop()
                        Progress = Nothing
                    Else
                        Dim Progress = SBOApplication.StatusBar.CreateProgressBar("Generando XML de Facturas " & gridView.Rows.SelectedRows.Count & " Líneas seleccionadas", gridView.Rows.SelectedRows.Count, True)
                        'Progress.Value = 0
                        Dim facXML As New generarFXML
                        For i As Integer = 0 To gridView.Rows.Count - 1
                            If gridView.Rows.IsSelected(i) = True Then
                                facXML.generarXML(gridView.DataTable.GetValue(0, i).ToString, "13", oCompany, SBO_Application)
                                Progress.Value = Progress.Value + 1
                            End If

                        Next
                        gridView.Rows.SelectedRows.Clear()
                        gridView.DataTable.Clear()
                        Progress.Stop()
                        Progress = Nothing
                    End If

                End If
                End If


            If FormUID = "GREMISION_M" And pVal.Before_Action = False Then
                If pVal.EventType = SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST Then
                    Dim oCFLEvento As SAPbouiCOM.IChooseFromListEvent
                    oCFLEvento = pVal
                    Dim sCFL_ID As String
                    sCFL_ID = oCFLEvento.ChooseFromListUID
                    Dim oForm As SAPbouiCOM.Form
                    oForm = SBO_Application.Forms.Item(FormUID)
                    Dim oCFL As SAPbouiCOM.ChooseFromList
                    oCFL = oForm.ChooseFromLists.Item(sCFL_ID)
                    If oCFLEvento.BeforeAction = False Then
                        Dim oDataTable As SAPbouiCOM.DataTable
                        oDataTable = oCFLEvento.SelectedObjects
                        Dim val As String
                        If (pVal.ItemUID = "Item_14") Then
                            Try
                                Dim txtTrans As SAPbouiCOM.EditText = oForm.Items.Item("Item_14").Specific
                                Dim txtNom As SAPbouiCOM.EditText = oForm.Items.Item("Item_16").Specific
                                ' Dim txtNomEmp As SAPbouiCOM.EditText = oForm.Items.Item("1000008").Specific                                
                                val = oDataTable.GetValue("Code", 0)
                                txtNom.Value = getNombre(val)
                                txtTrans.Value = val
                            Catch ex As Exception

                            End Try
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try
    End Sub

    Function validar() As Boolean
        Try
            Dim oTipoDocumento As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_1").Specific
            Dim oBusca As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_3").Specific

            If oTipoDocumento.Value = "" Then
                SBOApplication.SetStatusBarMessage("Debe de seleccionar un tipo de Documento para la búsqueda", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                Return False
            End If
            If oBusca.Value = "" Then
                SBOApplication.SetStatusBarMessage("Debe de seleccionar el tipo de busqueda", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                Return False
            End If

        Catch ex As Exception
            SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try
        Return True
    End Function

    Private Sub llenarGridView(p1 As String, p2 As String)
        Try
            Dim gridView As SAPbouiCOM.Grid
            gridView = oForm.Items.Item("Item_17").Specific
            Dim sql As String = "CALL BUSCAR_DOC_GUIA ('" & p1 & "','" & p2 & "')"
            oForm.DataSources.DataTables.Item(0).ExecuteQuery(sql)
            gridView.DataTable = oForm.DataSources.DataTables.Item("MyDataTable")
            gridView.AutoResizeColumns()
            gridView.Columns.Item(0).Editable = False
            gridView.Columns.Item(1).Editable = False
            gridView.Columns.Item(2).Editable = False
            gridView.Columns.Item(3).Editable = False
            gridView.Columns.Item(4).Editable = False

            gridView.SelectionMode = SAPbouiCOM.BoMatrixSelect.ms_Auto
            System.Runtime.InteropServices.Marshal.ReleaseComObject(gridView)
            gridView = Nothing
            GC.Collect()
        Catch ex As Exception
            SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try
    End Sub

    Private Sub GenerarGuiaRemision(p1 As String, p2 As String, DOC As Object, RUC As Object)
        Try

            Dim oGeneralService As SAPbobsCOM.GeneralService
            Dim oGeneralData As SAPbobsCOM.GeneralData
            Dim oChild As SAPbobsCOM.GeneralData
            Dim oChildren As SAPbobsCOM.GeneralDataCollection
            Dim oCompService As SAPbobsCOM.CompanyService = oCompany.GetCompanyService()
            oGeneralService = oCompService.GetGeneralService("GREMISION")
            oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)

            Dim oTipoDocumento As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_1").Specific
            Dim oBusca As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_3").Specific
            Dim oSeries As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_25").Specific
            Dim oFechaI As SAPbouiCOM.EditText = oForm.Items.Item("Item_6").Specific
            Dim oFechaLL As SAPbouiCOM.EditText = oForm.Items.Item("Item_8").Specific
            Dim oMotivo As SAPbouiCOM.EditText = oForm.Items.Item("Item_10").Specific
            Dim oComentario As SAPbouiCOM.EditText = oForm.Items.Item("Item_12").Specific
            Dim oTransport As SAPbouiCOM.EditText = oForm.Items.Item("Item_14").Specific
            Dim oNombreT As SAPbouiCOM.EditText = oForm.Items.Item("Item_16").Specific
            Dim oFechaEn As SAPbouiCOM.EditText = oForm.Items.Item("Item_20").Specific
            Dim oRuta As SAPbouiCOM.EditText = oForm.Items.Item("Item_21").Specific
            Dim oPlaca As SAPbouiCOM.EditText = oForm.Items.Item("Item_26").Specific
            Dim Docentry As String
            Dim oRecord As SAPbobsCOM.Recordset
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            'oGeneralData.SetProperty("CreateDate", Date.Now)
            oGeneralData.SetProperty("Remark", oComentario.Value)
            'oGeneralData.SetProperty("Canceled", "N")
            oGeneralData.SetProperty("Series", oSeries.Value.Trim)
            oGeneralData.SetProperty("U_RUC_DESTI", RUC.ToString)
            oRecord.DoQuery("CALL SP_CONSULTAS ('1','" & DOC.ToString & "','" & oTipoDocumento.Value.Trim & "')")
            oGeneralData.SetProperty("U_PTO_PARTIDA", oRecord.Fields.Item(0).Value)
            oGeneralData.SetProperty("U_PTO_LLEGADA", oRecord.Fields.Item(1).Value)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()
            oGeneralData.SetProperty("U_F_ITRASLADO", Date.Parse(DateTime.ParseExact(oFechaI.Value.ToString, "yyyyMMdd", CultureInfo.InvariantCulture)).ToString("yyyy/MM/dd"))
            oGeneralData.SetProperty("U_F_ITRASLADO", Date.Parse(DateTime.ParseExact((oFechaI.Value.ToString), "yyyyMMdd", CultureInfo.InvariantCulture)).ToString("yyyy/MM/dd"))
            oGeneralData.SetProperty("U_F_FTRASLADO", Date.Parse(DateTime.ParseExact(oFechaLL.Value.ToString, "yyyyMMdd", CultureInfo.InvariantCulture)).ToString("yyyy/MM/dd"))
            oGeneralData.SetProperty("U_G_TRANSPOR", oTransport.Value)
            oGeneralData.SetProperty("U_TRANPORTISTA", oNombreT.Value)
            oGeneralData.SetProperty("U_PLACA", oPlaca.Value)
            oGeneralData.SetProperty("U_B_TRANS", oMotivo.Value)
            oGeneralData.SetProperty("U_F_ENVIO", oFechaEn.Value)
            oGeneralData.SetProperty("U_G_RUTA", oRuta.Value)
            oChildren = oGeneralData.Child("DGREMISION")
            oChild = oChildren.Add
            oChild.SetProperty("U_TIPO_DOC", oTipoDocumento.Value)
            oChild.SetProperty("U_DOC_INI", DOC.ToString)
            oChild.SetProperty("U_FINAL", DOC.ToString)
            oChild.SetProperty("U_N_EMPAQUE", "")
            oGeneralService.Add(oGeneralData)
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("SELECT MAX(A.""DocEntry"") FROM ""@GREMISION"" A")
            If oRecord.RecordCount > 0 Then
                Docentry = oRecord.Fields.Item(0).Value
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
                oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRecord.DoQuery("INSERT INTO ""@G_ULTIMO"" VALUES ('" & Docentry & "','GUIA'," & Docentry & ")")
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
                oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRecord.DoQuery("CALL ACTUALIZAR_DOC_GUIA()")
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
                Dim generaXml As New generarGRXML
                generaXml.generarXML(Docentry, "GR", oCompany, SBOApplication)
            End If


        Catch ex As Exception
            SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try
    End Sub

    Private Sub llenarSeries()
        Try
            Dim oSeries As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_25").Specific
            oSeries.ValidValues.LoadSeries("GREMISION", SAPbouiCOM.BoSeriesMode.sf_View)
        Catch ex As Exception
            SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try

    End Sub

    Private Function validaringreso() As Boolean
        Dim control As Boolean = True
        Try
            Dim oTipoDocumento As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_1").Specific
            Dim oBusca As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_3").Specific
            Dim oSeries As SAPbouiCOM.ComboBox = oForm.Items.Item("Item_25").Specific
            Dim oFechaI As SAPbouiCOM.EditText = oForm.Items.Item("Item_6").Specific
            Dim oFechaLL As SAPbouiCOM.EditText = oForm.Items.Item("Item_8").Specific
            Dim oMotivo As SAPbouiCOM.EditText = oForm.Items.Item("Item_10").Specific
            Dim oComentario As SAPbouiCOM.EditText = oForm.Items.Item("Item_12").Specific
            Dim oTransport As SAPbouiCOM.EditText = oForm.Items.Item("Item_14").Specific
            Dim oNombreT As SAPbouiCOM.EditText = oForm.Items.Item("Item_16").Specific
            Dim oFechaEn As SAPbouiCOM.EditText = oForm.Items.Item("Item_20").Specific
            Dim oRuta As SAPbouiCOM.EditText = oForm.Items.Item("Item_21").Specific

            If oSeries.Value = "" Then
                SBO_Application.SetStatusBarMessage("Debe de seleccionar una Serie", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                Return False
            Else
                If oFechaI.Value = "" Then
                    SBO_Application.SetStatusBarMessage("Debe de seleccionar una fecha inicial", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                    Return False
                Else
                    If oFechaLL.Value = "" Then
                        SBO_Application.SetStatusBarMessage("Debe de seleccionar una fecha de llegada", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                        Return False
                    Else
                        If oMotivo.Value = "" Then
                            SBO_Application.SetStatusBarMessage("Debe de ingresar un motivo ", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                            Return False
                        Else
                            If oComentario.Value = "" Then
                                SBO_Application.SetStatusBarMessage("Debe de ingresar un comentario ", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                                Return False
                            Else
                                If oTransport.Value = "" Then
                                    SBO_Application.SetStatusBarMessage("Debe de ingresar un transportista ", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                                    Return False
                                Else
                                    If oFechaEn.Value = "" Then
                                        SBO_Application.SetStatusBarMessage("Debe de ingresar fecha de Envio ", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                                        Return False
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try

        Return control
    End Function

    Private Function getNombre(val As String) As String
        Dim nombre As String = ""
        Try
            Dim oRecord As SAPbobsCOM.Recordset
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("SELECT ""Name"" FROM ""@T_GTRANSPORTISTA"" A WHERE A.""Code""='" & val & "'")
            If oRecord.RecordCount > 0 Then
                nombre = oRecord.Fields.Item(0).Value
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()
        Catch ex As Exception
            SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try
        Return nombre
    End Function

End Class
