<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPopUpArchivo
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dvgArchivo = New System.Windows.Forms.DataGridView()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtBuscar = New DevExpress.XtraEditors.TextEdit()
        CType(Me.dvgArchivo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dvgArchivo
        '
        Me.dvgArchivo.AllowUserToAddRows = False
        Me.dvgArchivo.AllowUserToDeleteRows = False
        Me.dvgArchivo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dvgArchivo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dvgArchivo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dvgArchivo.Location = New System.Drawing.Point(12, 35)
        Me.dvgArchivo.Name = "dvgArchivo"
        Me.dvgArchivo.ReadOnly = True
        Me.dvgArchivo.Size = New System.Drawing.Size(274, 275)
        Me.dvgArchivo.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(58, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Descripcion:"
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(76, 9)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(145, 20)
        Me.txtBuscar.TabIndex = 2
        '
        'frmPopUpArchivo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(295, 312)
        Me.Controls.Add(Me.txtBuscar)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.dvgArchivo)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPopUpArchivo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscar Archivo"
        CType(Me.dvgArchivo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBuscar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dvgArchivo As System.Windows.Forms.DataGridView
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtBuscar As DevExpress.XtraEditors.TextEdit
End Class
