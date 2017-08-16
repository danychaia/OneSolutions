<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCuentasPopup
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
        Me.dvgArchivo = New System.Windows.Forms.DataGridView()
        Me.txtCuenta = New System.Windows.Forms.TextBox()
        CType(Me.dvgArchivo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(39, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Cuenta:"
        '
        'dvgArchivo
        '
        Me.dvgArchivo.AllowUserToAddRows = False
        Me.dvgArchivo.AllowUserToDeleteRows = False
        Me.dvgArchivo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dvgArchivo.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dvgArchivo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dvgArchivo.Location = New System.Drawing.Point(12, 36)
        Me.dvgArchivo.MultiSelect = False
        Me.dvgArchivo.Name = "dvgArchivo"
        Me.dvgArchivo.ReadOnly = True
        Me.dvgArchivo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dvgArchivo.Size = New System.Drawing.Size(308, 345)
        Me.dvgArchivo.TabIndex = 2
        '
        'txtCuenta
        '
        Me.txtCuenta.Location = New System.Drawing.Point(57, 9)
        Me.txtCuenta.Name = "txtCuenta"
        Me.txtCuenta.Size = New System.Drawing.Size(100, 21)
        Me.txtCuenta.TabIndex = 3
        '
        'frmCuentasPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(332, 385)
        Me.Controls.Add(Me.txtCuenta)
        Me.Controls.Add(Me.dvgArchivo)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCuentasPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cuentas Contables"
        CType(Me.dvgArchivo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dvgArchivo As System.Windows.Forms.DataGridView
    Friend WithEvents txtCuenta As System.Windows.Forms.TextBox
End Class
