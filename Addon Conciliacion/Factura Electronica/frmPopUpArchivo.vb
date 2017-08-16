Imports System.Xml

Public Class frmPopUpArchivo
    Private Property _oCompany As SAPbobsCOM.Company
    Public carga As New carga_excelvb
    Public Property oCompany() As SAPbobsCOM.Company
        Get
            Return _oCompany
        End Get
        Set(ByVal value As SAPbobsCOM.Company)
            _oCompany = value
        End Set
    End Property
    Private Sub frmPopUpArchivo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
           
        Catch ex As Exception

        End Try
       
    End Sub

    Public Function MakeConnectionSAP(Lista As ArrayList) As Boolean
        Dim Connected As Boolean = False
        '' Dim cnnParam As New Settings
        Try
            Connected = -1

            oCompany = New SAPbobsCOM.Company
            oCompany.Server = Lista(0)
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

    Private Sub txtBuscar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBuscar.KeyPress
        Dim tmp As System.Windows.Forms.KeyPressEventArgs = e
        If tmp.KeyChar = ChrW(Keys.Enter) Then
            busqueda(txtBuscar.Text)
        End If
    End Sub


    Private Sub busqueda(llave As String)
        Try
            dvgArchivo.DataSource = Nothing
            Dim Lista = cargarConfiguaracion()
            If Lista.Count <> 0 Then
                If MakeConnectionSAP(Lista) Then
                    Dim oRecordSet As SAPbobsCOM.Recordset
                    oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    If txtBuscar.Text <> "" Then
                        oRecordSet.DoQuery("exec BUSCAR_EXCEL_CARGA '1','" & llave & "'")
                    Else
                        oRecordSet.DoQuery("exec BUSCAR_EXCEL_CARGA '*','" & "" & "'")
                    End If

                    Dim table1 As New DataTable
                    table1.Columns.Add("Descripción")
                    table1.Columns.Add("Plantilla")
                    table1.Columns.Add("Ruta")
                    table1.Columns.Add("Fecha")

                    Dim tabla As New carga_excelvb
                    If oRecordSet.RecordCount > 0 Then
                        While oRecordSet.EoF = False
                            Dim fila = table1.NewRow
                            fila(0) = oRecordSet.Fields.Item(4).Value
                            fila(1) = oRecordSet.Fields.Item(3).Value
                            fila(2) = oRecordSet.Fields.Item(2).Value
                            fila(3) = oRecordSet.Fields.Item(5).Value
                            table1.Rows.Add(fila)
                            oRecordSet.MoveNext()
                        End While

                    End If
                    dvgArchivo.DataSource = table1
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dvgArchivo_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dvgArchivo.RowHeaderMouseDoubleClick
        carga.descripcion = dvgArchivo.Rows(e.RowIndex).Cells(0).Value.ToString()
        carga.plantilla = dvgArchivo.Rows(e.RowIndex).Cells(1).Value.ToString()
        carga.url = dvgArchivo.Rows(e.RowIndex).Cells(2).Value.ToString()
        carga.fecha = dvgArchivo.Rows(e.RowIndex).Cells(3).Value.ToString()
        Me.Close()
    End Sub
End Class