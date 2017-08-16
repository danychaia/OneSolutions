Imports System.Xml
Imports System.Data
Imports System.Data.OleDb
Imports DevExpress.Skins
Imports System.Globalization

Public Class MenuPrincipal
    Private Property _oCompany As SAPbobsCOM.Company
    Dim formularioBaseDatos = Nothing
    Dim formularioGface = Nothing
    Dim formActivo = String.Empty
    Dim formularioCarga = Nothing
    Dim formularioConciliacion = Nothing

    Public Property oCompany() As SAPbobsCOM.Company
        Get
            Return _oCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _oCompany = value
        End Set
    End Property
    Private Sub btnBaseDatos_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnBaseDatos.ItemClick
        If formularioBaseDatos Is Nothing Then
            limpiarPanel()
            formularioBaseDatos = New frmBaseDatos
            formularioBaseDatos.MdiParent = Me
            formularioBaseDatos.Show()
            activarFormulario()
        End If
    End Sub

    Private Sub btnGface_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGface.ItemClick
        If formularioGface Is Nothing Then
            limpiarPanel()
            formularioGface = New frmGface
            formularioGface.MdiParent = Me
            formularioGface.Show()
            activarFormulario()
        End If
    End Sub


    Private Sub RibbonControl_SelectedPageChanged(sender As Object, e As EventArgs) Handles RibbonControl.SelectedPageChanged
        Dim indexPage = RibbonControl.SelectedPage.PageIndex
        limpiarPanel()
        If indexPage = 1 Then
            formularioCarga = New frmCargaFacturas
            formularioCarga.MdiParent = Me
            formularioCarga.Show()
            activarFormulario()
        End If

    End Sub

    Private Sub btnCancelar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCancelar.ItemClick
        Try

            If formActivo = "frmBaseDatos" Then
                Dim frm2 As frmBaseDatos = CType(Me.ActiveMdiChild, frmBaseDatos)
                frm2.txtCompañia.Text = ""
                frm2.txtSapUser.Text = ""
                frm2.txtContraseñaSap.Text = ""
                frm2.txtuserDB.Text = ""
                frm2.txtPasswordDB.Text = ""
            Else
                If formActivo = "frmGface" Then

                End If
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub XtraTabbedMdiManager1_PageRemoved(sender As Object, e As DevExpress.XtraTabbedMdi.MdiTabPageEventArgs) Handles XtraTabbedMdiManager1.PageRemoved
        formularioBaseDatos = Nothing
        formularioGface = Nothing
        formularioCarga = Nothing
        formularioConciliacion = Nothing
    End Sub

    Private Sub limpiarPanel()
        For Each formre As Form In Me.MdiChildren
            formre.Hide()
            formre.Close()
        Next
        formularioBaseDatos = Nothing
        formularioGface = Nothing
        formularioCarga = Nothing
        formularioConciliacion = Nothing
    End Sub

    Private Sub activarFormulario()
        Dim form = Me.ActiveMdiChild
        formActivo = form.Name.ToString()
    End Sub

    Private Sub btnAgregarUDT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAgregarUDT.ItemClick

        Dim Lista = cargarConfiguaracion()
        If Lista.Count > 0 Then
            Try
                If MakeConnectionSAP(Lista) Then
                    Dim lretcode As Integer = -1
                    Dim lerrcode As Integer = -1
                    Dim serrmsg As String = ""
                    Dim ousertable = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)
                    ousertable.TableName = "CARGA_EXCEL"
                    ousertable.TableDescription = "Cargar Excel"
                    lretcode = ousertable.Add
                    If lretcode <> 0 Then
                        oCompany.GetLastError(lerrcode, serrmsg)
                        MessageBox.Show(lerrcode.ToString & "  " & serrmsg.ToString)
                    End If
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ousertable)

                    ' Dim ouserfield = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                    Dim ouserfield = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                    ouserfield.TableName = "@CARGA_EXCEL"
                    ouserfield.Name = "url_excel"
                    ouserfield.EditSize = "60"
                    ouserfield.Type = SAPbobsCOM.BoFieldTypes.db_Alpha
                    ouserfield.Description = "direccion del Excel"
                    lretcode = ouserfield.Add
                    If lretcode <> 0 Then
                        oCompany.GetLastError(lerrcode, serrmsg)
                        MessageBox.Show(lerrcode.ToString & "  " & serrmsg.ToString)

                    End If

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ouserfield)

                    ouserfield = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                    ouserfield.TableName = "@CARGA_EXCEL"
                    ouserfield.Name = "tipo_plantilla"
                    ouserfield.EditSize = "60"
                    ouserfield.Type = SAPbobsCOM.BoFieldTypes.db_Alpha
                    ouserfield.Description = "plantilla a usar"
                    lretcode = ouserfield.Add
                    If lretcode <> 0 Then
                        oCompany.GetLastError(lerrcode, serrmsg)
                        MessageBox.Show(lerrcode.ToString & "  " & serrmsg.ToString)

                    End If
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ouserfield)

                    ouserfield = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                    ouserfield.TableName = "@CARGA_EXCEL"
                    ouserfield.Name = "descripcion"
                    ouserfield.EditSize = "100"
                    ouserfield.Type = SAPbobsCOM.BoFieldTypes.db_Alpha
                    ouserfield.Description = "Descripcion de la plantilla"
                    lretcode = ouserfield.Add
                    If lretcode <> 0 Then
                        oCompany.GetLastError(lerrcode, serrmsg)
                        MessageBox.Show(lerrcode.ToString & "  " & serrmsg.ToString)

                    End If
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ouserfield)

                    ouserfield = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                    ouserfield.TableName = "@CARGA_EXCEL"
                    ouserfield.Name = "fecha"
                    ouserfield.Type = SAPbobsCOM.BoFieldTypes.db_Date
                    ouserfield.Description = "fecha de la carga"
                    lretcode = ouserfield.Add
                    If lretcode <> 0 Then
                        oCompany.GetLastError(lerrcode, serrmsg)
                        MessageBox.Show(lerrcode.ToString & "  " & serrmsg.ToString)
                    End If

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ouserfield)

                    ouserfield = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
                    ouserfield.TableName = "@CARGA_EXCEL"
                    ouserfield.EditSize = "2"
                    ouserfield.Name = "status"
                    ouserfield.Type = SAPbobsCOM.BoFieldTypes.db_Alpha
                    ouserfield.Description = "estado de conciliacion"
                    lretcode = ouserfield.Add
                    If lretcode <> 0 Then
                        oCompany.GetLastError(lerrcode, serrmsg)
                        MessageBox.Show(lerrcode.ToString & "  " & serrmsg.ToString)
                    End If
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ouserfield)
                    oCompany.Disconnect()
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany)
                    MessageBox.Show("La Base de Datos se ha restructurado correctamente!")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString)
            End Try

        End If

    End Sub

    Public Function MakeConnectionSAP(Lista As ArrayList) As Boolean
        Dim Connected As Boolean = False
        '' Dim cnnParam As New Settings
        Try
            Connected = -1

            oCompany = New SAPbobsCOM.Company
            oCompany.Server = Lista(0).ToString
            
            Select Case Lista(4).ToString
                Case "0"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005
                Case "1"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008
                Case "2"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012
                Case "3"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014
                Case "4"
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB
            End Select

            oCompany.CompanyDB = Lista(1) '"FYA"
            oCompany.UserName = Lista(2) '"manager"
            oCompany.Password = Lista(3) ' "alegria"

            Connected = oCompany.Connect()

            If Connected <> 0 Then
                ' oCompany.GetLastError(ErrorCode, ErrorMessage)
                ' MsgBox(ErrorCode & " " & ErrorMessage)
                Connected = False
                MsgBox(oCompany.GetLastErrorDescription)
                'conectado = False
            Else
                'MsgBox("Conexión con SAP exitosa")
               
                Connected = True
            End If
            Return Connected
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Private Sub btnGuardar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGuardar.ItemClick
        If formActivo.Equals("frmBaseDatos") Then
            crearArchivoConfig()
        End If
    End Sub

    Private Sub crearArchivoConfig()
        Dim ecrip As New EncryptComp.Library.Encrypt()

        Dim Doc As New XmlDocument, Nodo As XmlNode
        Dim Lista As ArrayList = New ArrayList()
        Try


            Dim frm2 As frmBaseDatos = CType(Me.ActiveMdiChild, frmBaseDatos)
            If frm2.txtCompañia.Text = "" Or frm2.txtSapUser.Text.Equals("") Or frm2.txtContraseñaSap.Text.Equals("") Or frm2.txtuserDB.Text.Equals("") Or frm2.txtPasswordDB.Text.Equals("") Or frm2.txtServer.Text.Equals("") And frm2.cboTipoSQl.SelectedIndex = -1 Then
                MessageBox.Show("Debe de Llenar todos los campos")
                Return
            End If
            Lista.Add(frm2.txtServer.Text)
            Lista.Add(frm2.txtCompañia .Text)
            Lista.Add(frm2.txtSapUser.Text)
            Lista.Add(frm2.txtContraseñaSap.Text)
            Lista.Add(frm2.cboTipoSQl.SelectedIndex.ToString)
            Lista.Add(frm2.txtuserDB.Text)
            Lista.Add(frm2.txtPasswordDB.Text)
            Lista.Add(frm2.txtDBServer.Text)
            Nodo = Doc.CreateElement("CONFIGURACION")
            Doc.AppendChild(Nodo)


            For Each Elemento As String In Lista
                Nodo = Doc.CreateElement("PARAMETRO")
                Nodo.InnerText = Elemento
                Doc.DocumentElement.AppendChild(Nodo)
            Next
            Doc.Save(Application.StartupPath & "\CONFIGURACION.xml")
            Dim mytext = System.IO.File.ReadAllText(Application.StartupPath & "\CONFIGURACION.xml")
            mytext = ecrip.EncryptKey(mytext)
            System.IO.File.WriteAllText(Application.StartupPath & "\CONFIGURACION.xml", mytext)
            MessageBox.Show("Guardado Exitosamente!")
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Function cargarConfiguaracion() As ArrayList

        Dim Lista As ArrayList = New ArrayList()
        Try
            Dim Doc As New XmlDocument, ListaNodos As XmlNodeList, Nodo As XmlNode
            Dim encrip As New EncryptComp.Library.Encrypt
            Dim mytext = System.IO.File.ReadAllText(Application.StartupPath & "\CONFIGURACION.xml")
            mytext = encrip.DecryptKey(mytext)
            Dim xmldoc = XDocument.Parse(mytext)
            xmldoc.Save(Application.StartupPath & "\CONFIGURACION.xml")

            Doc.Load(Application.StartupPath & "\CONFIGURACION.xml")

            ListaNodos = Doc.SelectNodes("/CONFIGURACION/PARAMETRO")

            For Each Nodo In ListaNodos
                Lista.Add(Nodo.ChildNodes.Item(0).InnerText)
            Next
            Dim mytext2 = System.IO.File.ReadAllText(Application.StartupPath & "\CONFIGURACION.xml")
            mytext2 = encrip.EncryptKey(mytext)
            System.IO.File.WriteAllText(Application.StartupPath & "\CONFIGURACION.xml", mytext2)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
            Lista = New ArrayList
        End Try
        Return Lista
    End Function

    Private Sub btnEnviar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEnviar.ItemClick
        Try
            If formActivo = "frmCargaFacturas" Then
                Dim Lista = cargarConfiguaracion()
                If Lista.Count <> 0 Then
                    Dim frm2 As frmCargaFacturas = CType(Me.ActiveMdiChild, frmCargaFacturas)
                    If frm2 IsNot Nothing Then
                        If frm2.cboPlantilla.SelectedIndex = -1 Or frm2.txtDescripcion.Text.Equals("") Or frm2.txtRuta.Text.Equals("") Then
                            MessageBox.Show("Debe de LLenar todos los Campos")
                            Return
                        End If
                        If MakeConnectionSAP(Lista) Then
                            Dim oRecordSet As SAPbobsCOM.Recordset
                            oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                            Dim sql = "exec INSERTAR_EXCEL_CARGA '" & frm2.txtRuta.Text & "','" & frm2.cboPlantilla.SelectedItem.ToString & "','" & frm2.txtDescripcion.Text & "'"
                            oRecordSet.DoQuery(sql)
                            oCompany.Disconnect()
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany)
                        End If
                        If frm2.cboPlantilla.SelectedItem.ToString = "BANRURAL" Then
                            Dim dataTable As New DataTable
                            Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + frm2.txtRuta.Text + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'")
                            Dim myDataAdapter As New OleDbDataAdapter("Select * from [" + "Table 1" + "$]", conn)
                            myDataAdapter.Fill(dataTable)
                            frm2.dgv.DataSource = dataTable
                            MessageBox.Show("Carga Realizada Con Exito!")
                            formularioCarga = Nothing
                            formActivo = ""
                        Else
                            If frm2.cboPlantilla.SelectedItem.ToString = "BANCO INDUSTRIAL" Then
                                Dim dataTable1 As New DataTable
                                dataTable1.Columns.Add("FECHA")
                                dataTable1.Columns.Add("TT")
                                dataTable1.Columns.Add("DESCRIPCION")
                                dataTable1.Columns.Add("No. DOC")
                                dataTable1.Columns.Add("DEBE")
                                dataTable1.Columns.Add("HABER")
                                dataTable1.Columns.Add("SALDO")

                                Dim dataNueva As New DataTable
                                dataNueva.Columns.Add("FECHA")
                                dataNueva.Columns.Add("TT")
                                dataNueva.Columns.Add("DESCRIPCION")
                                dataNueva.Columns.Add("No. DOC")
                                dataNueva.Columns.Add("DEBE")
                                dataNueva.Columns.Add("HABER")
                                dataNueva.Columns.Add("SALDO")
                                Dim agregar As Boolean = False

                                For Each line As String In System.IO.File.ReadAllLines(frm2.txtRuta.Text)
                                    dataTable1.Rows.Add(line.Split(","))
                                Next

                                For Each fila As DataRow In dataTable1.Rows
                                    Dim s = fila(0)
                                    If s.ToString = "Fecha" And fila(1).ToString = "TT" Then

                                        'MessageBox.Show("encontró la fecha")
                                        agregar = True
                                    Else
                                        If agregar = True Then
                                            Dim rownew = dataNueva.NewRow
                                            rownew(0) = fila(0).ToString
                                            rownew(1) = fila(1).ToString
                                            rownew(2) = fila(2).ToString
                                            rownew(3) = fila(3).ToString
                                            rownew(4) = fila(4).ToString
                                            rownew(5) = fila(5).ToString
                                            rownew(6) = fila(6).ToString
                                            dataNueva.Rows.Add(rownew)
                                        End If
                                    End If

                                Next
                                frm2.dgv.DataSource = dataNueva
                                MessageBox.Show("Carga Realizada Con Exito!")
                                formularioCarga = Nothing
                                formActivo = ""
                            End If
                            ''''''Si es City bank
                            If frm2.cboPlantilla.SelectedItem.ToString = "CITIBANK" Then
                                Dim dataTable As New DataTable
                                Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + frm2.txtRuta.Text + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'")
                                Dim myDataAdapter As New OleDbDataAdapter("Select * from [" + "First Sheet" + "$]", conn)
                                myDataAdapter.Fill(dataTable)
                                frm2.dgv.DataSource = dataTable
                                MessageBox.Show("Carga Realizada Con Exito!")
                                formularioCarga = Nothing
                                formActivo = ""
                            Else
                                If frm2.cboPlantilla.SelectedItem.ToString = "G&T" Then
                                    Dim dataTable As New DataTable
                                    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + frm2.txtRuta.Text + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'")
                                    Dim myDataAdapter As New OleDbDataAdapter("Select * from [" + "Estado de Cuenta" + "$]", conn)
                                    myDataAdapter.Fill(dataTable)
                                    frm2.dgv.DataSource = dataTable
                                    MessageBox.Show("Carga Realizada Con Exito!")
                                    formularioCarga = Nothing
                                    formActivo = ""
                                End If
                            End If

                           
                        End If

                    Else
                        RibbonControl_SelectedPageChanged(Nothing, Nothing)
                    End If

                End If
            Else
                RibbonControl_SelectedPageChanged(Nothing, Nothing)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try

    End Sub
    Private Sub btnConsolidar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnConsolidar.ItemClick
        limpiarPanel()
        If formularioConciliacion Is Nothing Then
            formularioConciliacion = New frmConciliacion
            formularioConciliacion.MdiParent = Me
            formularioConciliacion.Show()
            activarFormulario()
        End If

    End Sub

    Private Sub btnEjecutar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEjecutar.ItemClick
        Try
            If formActivo = "frmConciliacion" Then
                Dim frm As frmConciliacion = CType(Me.ActiveMdiChild, frmConciliacion)
                If frm.txtArchivo.Text = "" Or frm.txtplantilla.Text = "" Or frm.TextBox1.Text = "" Or frm.txtidCuenta.Text = "" Then
                    MessageBox.Show("Debe de Llenar todos los campos")
                    Return
                End If
                If frm.txtplantilla.Text = "BANRURAL" Then
                    ejecutarBanrural(frm)
                Else
                    If frm.txtplantilla.Text = "BANCO INDUSTRIAL" Then
                        ejecutarIndustrial(frm)
                    Else
                        If frm.txtplantilla.Text = "CITIBANK" Then
                            ejecutarCiti(frm)
                        Else
                            If frm.txtplantilla.Text = "G&T" Then
                                ejecutarGT(frm)
                            End If
                        End If

                    End If

                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ejecutarBanrural(frm As frmConciliacion)
        Try
            Dim oReturn As Integer = -1
            Dim oError As Integer = 0
            Dim errMsg As String = ""
            Dim conteo As Integer = 0
            Dim Lista = cargarConfiguaracion()
            If Lista.Count <> 0 Then
                If MakeConnectionSAP(Lista) Then
                    oCompany.StartTransaction()
                    If frm.dvgPlantilla.Rows.Count <> 0 Then
                        For i As Integer = 0 To frm.dvgPlantilla.Rows.Count - 1
                            Dim banrural As New banrural
                            banrural.fecha = frm.dvgPlantilla.Rows(i).Cells(0).Value.ToString
                            banrural.oficina = frm.dvgPlantilla.Rows(i).Cells(1).Value.ToString
                            banrural.descripcionOperacion = frm.dvgPlantilla.Rows(i).Cells(2).Value.ToString
                            banrural.docto = frm.dvgPlantilla.Rows(i).Cells(3).Value.ToString
                            banrural.credito = Double.Parse(frm.dvgPlantilla.Rows(i).Cells(4).Value)
                            banrural.debito = Double.Parse(frm.dvgPlantilla.Rows(i).Cells(5).Value)
                            banrural.saldoDisponible = Double.Parse(frm.dvgPlantilla.Rows(i).Cells(6).Value)

                            Dim oBankPages As SAPbobsCOM.BankPages
                            oBankPages = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBankPages)
                            oBankPages.AccountCode = frm.txtidCuenta.Text
                            oBankPages.DueDate = DateTime.Parse(banrural.fecha)
                            oBankPages.Reference = banrural.docto
                            oBankPages.Memo = banrural.descripcionOperacion
                            If banrural.credito > 0 Then
                                oBankPages.CreditAmount = banrural.credito
                            End If
                            If banrural.debito > 0 Then
                                oBankPages.DebitAmount = banrural.debito
                            End If

                            oReturn = oBankPages.Add()
                            If oReturn <> 0 Then
                                oCompany.GetLastError(oError, errMsg)
                                MsgBox(errMsg)
                                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                            Else
                                conteo = conteo + 1
                            End If

                        Next
                        If conteo = frm.dvgPlantilla.Rows.Count Then
                            Dim oRecordSet As SAPbobsCOM.Recordset
                            oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                            oRecordSet.DoQuery("update [@CARGA_EXCEL] set U_status='N'  where U_descripcion = '" & frm.txtArchivo.Text & "' and U_tipo_plantilla = '" & frm.txtplantilla.Text & "'")
                            formularioConciliacion = Nothing
                            btnConsolidar_ItemClick(Nothing, Nothing)
                            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                            MessageBox.Show("Proceso Finalizado Exitosamente!")
                        Else
                            formularioConciliacion = Nothing
                            btnConsolidar_ItemClick(Nothing, Nothing)
                            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                        End If
                    Else
                        MessageBox.Show("Error al Cargar conciliacion por falta de Datos en el Gridview")
                    End If
                End If
            Else
                MessageBox.Show("Revisar Configuracion de Parametros")
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub

    Private Sub btnTest_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTest.ItemClick
        Dim Lista = cargarConfiguaracion()
        If Lista.Count <> 0 Then
            If MakeConnectionSAP(Lista) Then
                MessageBox.Show("Conexion Satisfactoria!")
            End If
        Else
            MessageBox.Show("Debe de Ingresar una configuración")
        End If

    End Sub

    Private Sub MenuPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim skin As Skin = RibbonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Dim elem As SkinElement = skin(RibbonSkins.SkinFormApplicationButton)
        elem.Image.SetImage(CType(Nothing, Image), Color.Empty)
        elem.Size.MinSize = New System.Drawing.Size(0, 0)
    End Sub

    Private Sub ejecutarIndustrial(frm As frmConciliacion)
        Try
            Dim oReturn As Integer = -1
            Dim oError As Integer = 0
            Dim errMsg As String = ""
            Dim conteo As Integer = 0
            Dim Lista = cargarConfiguaracion()
            If Lista.Count <> 0 Then
                If MakeConnectionSAP(Lista) Then
                    oCompany.StartTransaction()
                    If frm.dvgPlantilla.Rows.Count <> 0 Then
                        For i As Integer = 0 To frm.dvgPlantilla.Rows.Count - 1
                            Dim bi As New BI

                            bi.fecha = frm.dvgPlantilla.Rows(i).Cells(0).Value
                            bi.TT = frm.dvgPlantilla.Rows(i).Cells(1).Value
                            bi.descripcion = frm.dvgPlantilla.Rows(i).Cells(2).Value.ToString
                            bi.NoDoc = frm.dvgPlantilla.Rows(i).Cells(3).Value.ToString
                            bi.debe = Double.Parse(IIf(frm.dvgPlantilla.Rows(i).Cells(4).Value.ToString = "", 0, frm.dvgPlantilla.Rows(i).Cells(4).Value.ToString))
                            bi.haber = Double.Parse(IIf(frm.dvgPlantilla.Rows(i).Cells(5).Value.ToString = "", 0, frm.dvgPlantilla.Rows(i).Cells(5).Value.ToString))
                            bi.saldo = Double.Parse(IIf(frm.dvgPlantilla.Rows(i).Cells(6).Value.ToString = "", 0, frm.dvgPlantilla.Rows(i).Cells(6).Value.ToString))
                            Dim oBankPages As SAPbobsCOM.BankPages
                            oBankPages = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBankPages)
                            oBankPages.AccountCode = frm.txtidCuenta.Text
                            oBankPages.DueDate = DateTime.Parse(bi.fecha)
                            oBankPages.Reference = bi.NoDoc
                            oBankPages.Memo = bi.descripcion
                            If bi.debe > 0 Then
                                oBankPages.DebitAmount = bi.debe
                            End If
                            If bi.haber > 0 Then
                                oBankPages.CreditAmount = bi.haber
                            End If
                            oReturn = oBankPages.Add()
                            If oReturn <> 0 Then
                                oCompany.GetLastError(oError, errMsg)
                                MsgBox(errMsg)
                            Else
                                conteo = conteo + 1
                            End If
                        Next
                        If conteo = frm.dvgPlantilla.Rows.Count Then


                            Try

                                Dim oRecordSet As SAPbobsCOM.Recordset
                                oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                                oRecordSet.DoQuery("update [@CARGA_EXCEL] set U_status='N'  where U_descripcion = '" & frm.txtArchivo.Text & "' and U_tipo_plantilla = '" & frm.txtplantilla.Text & "'")
                                formularioConciliacion = Nothing
                                btnConsolidar_ItemClick(Nothing, Nothing)
                                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                                MessageBox.Show("Proceso Finalizado Exitosamente!")
                            Catch ex As Exception
                                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                                MessageBox.Show(ex.Message)
                            End Try
                           
                        End If
                    Else
                        MessageBox.Show("Error al Cargar conciliacion por falta de Datos en el Gridview")
                        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                    End If
                End If
            Else
                MessageBox.Show("Revisar Configuracion de Parametros")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ejecutarCiti(frm As frmConciliacion)
        Try
            Dim oReturn As Integer = -1
            Dim oError As Integer = 0
            Dim errMsg As String = ""
            Dim conteo As Integer = 0
            Dim Lista = cargarConfiguaracion()
            If Lista.Count <> 0 Then
                If MakeConnectionSAP(Lista) Then
                    oCompany.StartTransaction()
                    If frm.dvgPlantilla.Rows.Count <> 0 Then
                        For i As Integer = 0 To frm.dvgPlantilla.Rows.Count - 1
                            Dim citi As New citiBank
                            citi.fecha = frm.dvgPlantilla.Rows(i).Cells(0).Value.ToString
                            citi.oficina = frm.dvgPlantilla.Rows(i).Cells(1).Value.ToString
                            citi.referencia = frm.dvgPlantilla.Rows(i).Cells(2).Value.ToString
                            citi.transaccion = frm.dvgPlantilla.Rows(i).Cells(3).Value.ToString
                            citi.debiOrCredit = frm.dvgPlantilla.Rows(i).Cells(4).Value.ToString
                            citi.valor = Double.Parse(frm.dvgPlantilla.Rows(i).Cells(5).Value.ToString)
                            citi.saldoreal = Double.Parse(frm.dvgPlantilla.Rows(i).Cells(6).Value.ToString)
                            citi.saldoDisponible = Double.Parse(frm.dvgPlantilla.Rows(i).Cells(7).Value.ToString)

                            Dim oBankPages As SAPbobsCOM.BankPages
                            oBankPages = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBankPages)
                            oBankPages.AccountCode = frm.txtidCuenta.Text
                            oBankPages.DueDate = DateTime.Parse(citi.fecha)
                            oBankPages.Reference = citi.referencia
                            oBankPages.Memo = citi.transaccion
                            If citi.debiOrCredit = "+" Then
                                oBankPages.CreditAmount = citi.valor
                            End If
                            If citi.debiOrCredit = "-" Then
                                oBankPages.DebitAmount = citi.valor
                            End If

                            oReturn = oBankPages.Add()
                            If oReturn <> 0 Then
                                oCompany.GetLastError(oError, errMsg)
                                MsgBox(errMsg)
                                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                            Else
                                conteo = conteo + 1
                            End If
                        Next
                        If conteo = frm.dvgPlantilla.Rows.Count Then
                            Try
                                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                                MessageBox.Show("Proceso Finalizado Exitosamente!")
                                Dim oRecordSet As SAPbobsCOM.Recordset
                                oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                                oRecordSet.DoQuery("update [@CARGA_EXCEL] set U_status='N'  where U_descripcion = '" & frm.txtArchivo.Text & "' and U_tipo_plantilla = '" & frm.txtplantilla.Text & "'")
                                formularioConciliacion = Nothing
                                btnConsolidar_ItemClick(Nothing, Nothing)
                            Catch ex As Exception
                                formularioConciliacion = Nothing
                                btnConsolidar_ItemClick(Nothing, Nothing)
                                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                            End Try
                            
                        End If
                    Else
                        MessageBox.Show("Error al Cargar conciliacion por falta de Datos en el Gridview")
                    End If
                End If
            Else
                MessageBox.Show("Revisar Configuracion de Parametros")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ejecutarGT(frm As frmConciliacion)
        Try
            Dim oReturn As Integer = -1
            Dim oError As Integer = 0
            Dim errMsg As String = ""
            Dim conteo As Integer = 0
            Dim Lista = cargarConfiguaracion()
            If Lista.Count <> 0 Then
                If MakeConnectionSAP(Lista) Then
                    oCompany.StartTransaction()
                    For i As Integer = 0 To frm.dvgPlantilla.Rows.Count - 1
                        Dim gt As New bancoGT
                        gt.numero = frm.dvgPlantilla.Rows(i).Cells(0).Value.ToString
                        gt.fecha = frm.dvgPlantilla.Rows(i).Cells(1).Value.ToString
                        gt.referencia = frm.dvgPlantilla.Rows(i).Cells(2).Value.ToString
                        gt.descripcion = frm.dvgPlantilla.Rows(i).Cells(3).Value.ToString
                        gt.debito = Double.Parse(IIf(frm.dvgPlantilla.Rows(i).Cells(4).Value.ToString = "", 0, frm.dvgPlantilla.Rows(i).Cells(4).Value.ToString))
                        gt.credito = Double.Parse(IIf(frm.dvgPlantilla.Rows(i).Cells(5).Value.ToString = "", 0, frm.dvgPlantilla.Rows(i).Cells(5).Value.ToString))
                        gt.saldo = IIf(frm.dvgPlantilla.Rows(i).Cells(6).Value.ToString = "", 0, frm.dvgPlantilla.Rows(i).Cells(6).Value.ToString = "")
                        If gt.numero <> "" Then
                            Dim oBankPages As SAPbobsCOM.BankPages
                            oBankPages = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBankPages)
                            oBankPages.AccountCode = frm.txtidCuenta.Text
                            oBankPages.DueDate = DateTime.Parse(gt.fecha)
                            oBankPages.Reference = gt.referencia
                            oBankPages.Memo = gt.descripcion
                            If gt.credito > 0 Then
                                oBankPages.CreditAmount = gt.credito
                            End If
                            If gt.debito > 0 Then
                                oBankPages.DebitAmount = gt.debito
                            End If

                            oReturn = oBankPages.Add()
                            If oReturn <> 0 Then
                                oCompany.GetLastError(oError, errMsg)
                                MsgBox(errMsg)
                            Else
                                conteo = conteo + 1
                            End If
                        Else
                            conteo = conteo + 1
                        End If

                    Next
                    If conteo = frm.dvgPlantilla.Rows.Count Then
                        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit)
                        MessageBox.Show("Proceso Finalizado Exitosamente!")
                        Dim oRecordSet As SAPbobsCOM.Recordset
                        oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        oRecordSet.DoQuery("update [@CARGA_EXCEL] set U_status='N'  where U_descripcion = '" & frm.txtArchivo.Text & "' and U_tipo_plantilla = '" & frm.txtplantilla.Text & "'")
                        formularioConciliacion = Nothing
                        btnConsolidar_ItemClick(Nothing, Nothing)
                    Else
                        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
                        formularioConciliacion = Nothing
                        btnConsolidar_ItemClick(Nothing, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack)
        End Try
    End Sub

End Class