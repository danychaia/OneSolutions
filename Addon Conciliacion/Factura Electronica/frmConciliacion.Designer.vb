<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConciliacion
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtplantilla = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.dvgPlantilla = New System.Windows.Forms.DataGridView()
        Me.txtArchivo = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txtidCuenta = New System.Windows.Forms.TextBox()
        CType(Me.txtplantilla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dvgPlantilla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Archivo"
        '
        'txtplantilla
        '
        Me.txtplantilla.Enabled = False
        Me.txtplantilla.Location = New System.Drawing.Point(103, 36)
        Me.txtplantilla.Name = "txtplantilla"
        Me.txtplantilla.Size = New System.Drawing.Size(100, 20)
        Me.txtplantilla.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(13, 39)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Plantilla"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 68)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(84, 13)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Cuenta Contable "
        '
        'dvgPlantilla
        '
        Me.dvgPlantilla.AllowUserToAddRows = False
        Me.dvgPlantilla.AllowUserToDeleteRows = False
        Me.dvgPlantilla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dvgPlantilla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders
        Me.dvgPlantilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dvgPlantilla.Location = New System.Drawing.Point(12, 91)
        Me.dvgPlantilla.MultiSelect = False
        Me.dvgPlantilla.Name = "dvgPlantilla"
        Me.dvgPlantilla.ReadOnly = True
        Me.dvgPlantilla.Size = New System.Drawing.Size(588, 184)
        Me.dvgPlantilla.TabIndex = 6
        '
        'txtArchivo
        '
        Me.txtArchivo.Location = New System.Drawing.Point(103, 10)
        Me.txtArchivo.Name = "txtArchivo"
        Me.txtArchivo.Size = New System.Drawing.Size(100, 21)
        Me.txtArchivo.TabIndex = 1
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(103, 65)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 21)
        Me.TextBox1.TabIndex = 3
        '
        'txtidCuenta
        '
        Me.txtidCuenta.Location = New System.Drawing.Point(209, 65)
        Me.txtidCuenta.Name = "txtidCuenta"
        Me.txtidCuenta.Size = New System.Drawing.Size(100, 21)
        Me.txtidCuenta.TabIndex = 7
        Me.txtidCuenta.Visible = False
        '
        'frmConciliacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 287)
        Me.Controls.Add(Me.txtidCuenta)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.txtArchivo)
        Me.Controls.Add(Me.dvgPlantilla)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.txtplantilla)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "frmConciliacion"
        Me.Text = "Conciliacion"
        CType(Me.txtplantilla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dvgPlantilla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtplantilla As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dvgPlantilla As System.Windows.Forms.DataGridView
    Friend WithEvents txtArchivo As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtidCuenta As System.Windows.Forms.TextBox
End Class
