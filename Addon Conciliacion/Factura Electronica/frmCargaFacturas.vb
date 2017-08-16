Public Class frmCargaFacturas 

    Private Sub btnBuscarRuta_Click(sender As Object, e As EventArgs) Handles btnBuscarRuta.Click
        OpenFileDialog1.Filter = "Excel Files |*.xlsx;*.xls;*.csv"
        OpenFileDialog1.Title = "Seleccione el archivo de Excel"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.ShowDialog()
        If Not OpenFileDialog1.FileName.Equals("") Then
            txtRuta.Text = OpenFileDialog1.FileName.ToString()
        End If
    End Sub
End Class