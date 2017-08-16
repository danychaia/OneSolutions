Imports System.Xml

Public Class frmBaseDatos

    Private Sub frmBaseDatos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim Doc As New XmlDocument, ListaNodos As XmlNodeList, Nodo As XmlNode
            Dim Lista As ArrayList = New ArrayList()
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
            txtServer.Text = Lista(0)
            txtCompañia.Text = Lista(1)
            'cboCompany.SelectedIndex = 0
            txtSapUser.Text = Lista(2)
            txtContraseñaSap.Text = Lista(3)
            cboTipoSQl.SelectedIndex = Lista(4)
            txtuserDB.Text = Lista(5)
            txtPasswordDB.Text = Lista(6)
            txtDBServer.Text = Lista(7)
            Dim mytext2 = System.IO.File.ReadAllText(Application.StartupPath & "\CONFIGURACION.xml")
            mytext2 = encrip.EncryptKey(mytext)
            System.IO.File.WriteAllText(Application.StartupPath & "\CONFIGURACION.xml", mytext2)
        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim oCompany As SAPbobsCOM.Company
            oCompany = New SAPbobsCOM.Company
            Dim recordSet As SAPbobsCOM.Recordset
            oCompany.Server = txtDBServer.Text
            oCompany.DbPassword = txtPasswordDB.Text
            oCompany.DbUserName = txtuserDB.Text
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012
            recordSet = oCompany.GetCompanyList
            If recordSet.RecordCount > 0 Then
                While recordSet.EoF = False
                    cboCompany.Items.Add(recordSet.Fields.Item(0).Value)
                    recordSet.MoveNext()
                End While
            End If
            MessageBox.Show("Compañias Cargadas Exitosamente!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
End Class
