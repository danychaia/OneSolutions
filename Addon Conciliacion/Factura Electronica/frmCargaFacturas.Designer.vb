<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCargaFacturas
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
        Me.cboPlantilla = New System.Windows.Forms.ComboBox()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtDescripcion = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtRuta = New DevExpress.XtraEditors.TextEdit()
        Me.btnBuscarRuta = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRuta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Plantilla"
        '
        'cboPlantilla
        '
        Me.cboPlantilla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPlantilla.FormattingEnabled = True
        Me.cboPlantilla.Items.AddRange(New Object() {"BANRURAL", "BANCO INDUSTRIAL", "CITIBANK", "G&T"})
        Me.cboPlantilla.Location = New System.Drawing.Point(76, 10)
        Me.cboPlantilla.Name = "cboPlantilla"
        Me.cboPlantilla.Size = New System.Drawing.Size(121, 21)
        Me.cboPlantilla.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(13, 37)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(57, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Descripción "
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Location = New System.Drawing.Point(76, 37)
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(251, 20)
        Me.txtDescripcion.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(12, 66)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Documento"
        '
        'txtRuta
        '
        Me.txtRuta.Location = New System.Drawing.Point(76, 63)
        Me.txtRuta.Name = "txtRuta"
        Me.txtRuta.Size = New System.Drawing.Size(251, 20)
        Me.txtRuta.TabIndex = 5
        '
        'btnBuscarRuta
        '
        Me.btnBuscarRuta.Location = New System.Drawing.Point(333, 61)
        Me.btnBuscarRuta.Name = "btnBuscarRuta"
        Me.btnBuscarRuta.Size = New System.Drawing.Size(40, 23)
        Me.btnBuscarRuta.TabIndex = 6
        Me.btnBuscarRuta.Text = "....."
        Me.btnBuscarRuta.UseVisualStyleBackColor = True
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(12, 103)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.Size = New System.Drawing.Size(693, 173)
        Me.dgv.TabIndex = 7
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmCargaFacturas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(717, 288)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.btnBuscarRuta)
        Me.Controls.Add(Me.txtRuta)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.cboPlantilla)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "frmCargaFacturas"
        Me.Text = "Cargar Excel"
        CType(Me.txtDescripcion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRuta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboPlantilla As System.Windows.Forms.ComboBox
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDescripcion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtRuta As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnBuscarRuta As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class
