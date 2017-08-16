Imports System.Data.OleDb

Public Class frmConciliacion
    Private Sub txtArchivo_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles txtArchivo.MouseDoubleClick
        Dim popUp As New frmPopUpArchivo
        popUp.ShowDialog()
        Dim objetos = popUp.carga
        txtArchivo.Text = objetos.descripcion
        txtplantilla.Text = objetos.plantilla
        Try
            If txtplantilla.Text = "BANRURAL" Then
                Dim dataTable As New DataTable
                Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + objetos.url + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'")
                Dim myDataAdapter As New OleDbDataAdapter("Select * from [" + "Table 1" + "$]", conn)
                myDataAdapter.Fill(dataTable)
                dvgPlantilla.DataSource = dataTable
            End If
            If txtplantilla.Text = "BANCO INDUSTRIAL" Then
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

                For Each line As String In System.IO.File.ReadAllLines(objetos.url)
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
                dvgPlantilla.DataSource = dataNueva
            End If

            If txtplantilla.Text = "CITIBANK" Then
                Dim dataTable As New DataTable
                Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + objetos.url + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'")
                Dim myDataAdapter As New OleDbDataAdapter("Select * from [" + "First Sheet" + "$]", conn)
                myDataAdapter.Fill(dataTable)
                dvgPlantilla.DataSource = dataTable
            End If
            If txtplantilla.Text = "G&T" Then
                Dim dataTable As New DataTable
                Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + objetos.url + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'")
                Dim myDataAdapter As New OleDbDataAdapter("Select * from [" + "Estado de Cuenta" + "$]", conn)
                myDataAdapter.Fill(dataTable)
                Dim dataNueva As New DataTable
                dataNueva.Columns.Add("#")
                dataNueva.Columns.Add("FECHA")
                dataNueva.Columns.Add("REFERENCIA")
                dataNueva.Columns.Add("DESCRIPCION")
                dataNueva.Columns.Add("DEBITO")
                dataNueva.Columns.Add("CREDITO")
                dataNueva.Columns.Add("SALDO")
                Dim agregar As Boolean = False
                For Each fila As DataRow In dataTable.Rows
                    Dim s = fila(0)
                    If s.ToString = "#" And fila(1).ToString = "Fecha" Then
                        'MessageBox.Show("encontró la fecha")
                        agregar = True
                    Else
                        If s.ToString = "No Débitos:" Then
                            agregar = False
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
                       
                    End If
                Next

                dvgPlantilla.DataSource = dataNueva
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub

    Private Sub txtCuentaContable_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        Dim jj As New frmCuentasPopup
        jj.ShowDialog()
    End Sub

    Private Sub TextBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseDoubleClick
        Dim jj As New frmCuentasPopup
        jj.ShowDialog()
        TextBox1.Text = jj.formCode
        txtidCuenta.Text = jj.IdCuenta
    End Sub
End Class