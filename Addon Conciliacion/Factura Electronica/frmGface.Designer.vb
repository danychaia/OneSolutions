<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGface
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
        Me.lblTipoGface = New DevExpress.XtraEditors.LabelControl()
        Me.cboTipoGface = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtPassGface = New DevExpress.XtraEditors.TextEdit()
        Me.lblPassGface = New DevExpress.XtraEditors.LabelControl()
        Me.lblUserGface = New DevExpress.XtraEditors.LabelControl()
        Me.txtUserGface = New DevExpress.XtraEditors.TextEdit()
        Me.lblUrlGface = New DevExpress.XtraEditors.LabelControl()
        Me.UrlGface = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.lblEmpresa = New DevExpress.XtraEditors.LabelControl()
        Me.txtCodEmpresa = New DevExpress.XtraEditors.TextEdit()
        Me.lblCiudad = New DevExpress.XtraEditors.LabelControl()
        Me.txtCiudad = New DevExpress.XtraEditors.TextEdit()
        Me.lblDireccion = New DevExpress.XtraEditors.LabelControl()
        Me.txtDireccion = New DevExpress.XtraEditors.TextEdit()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.column1 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.cboTipoGface.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassGface.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserGface.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UrlGface.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCodEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiudad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTipoGface
        '
        Me.lblTipoGface.Location = New System.Drawing.Point(12, 12)
        Me.lblTipoGface.Name = "lblTipoGface"
        Me.lblTipoGface.Size = New System.Drawing.Size(53, 13)
        Me.lblTipoGface.TabIndex = 0
        Me.lblTipoGface.Text = "Tipo GFace"
        '
        'cboTipoGface
        '
        Me.cboTipoGface.Location = New System.Drawing.Point(74, 9)
        Me.cboTipoGface.Name = "cboTipoGface"
        Me.cboTipoGface.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTipoGface.Properties.Items.AddRange(New Object() {"-------------", "MegaPrinter", "G&T", "INFILE"})
        Me.cboTipoGface.Size = New System.Drawing.Size(133, 20)
        Me.cboTipoGface.TabIndex = 1
        '
        'txtPassGface
        '
        Me.txtPassGface.Location = New System.Drawing.Point(74, 61)
        Me.txtPassGface.Name = "txtPassGface"
        Me.txtPassGface.Size = New System.Drawing.Size(133, 20)
        Me.txtPassGface.TabIndex = 3
        '
        'lblPassGface
        '
        Me.lblPassGface.Location = New System.Drawing.Point(12, 64)
        Me.lblPassGface.Name = "lblPassGface"
        Me.lblPassGface.Size = New System.Drawing.Size(56, 13)
        Me.lblPassGface.TabIndex = 3
        Me.lblPassGface.Text = "Contraseña"
        '
        'lblUserGface
        '
        Me.lblUserGface.Location = New System.Drawing.Point(12, 38)
        Me.lblUserGface.Name = "lblUserGface"
        Me.lblUserGface.Size = New System.Drawing.Size(36, 13)
        Me.lblUserGface.TabIndex = 5
        Me.lblUserGface.Text = "Usuario"
        '
        'txtUserGface
        '
        Me.txtUserGface.Location = New System.Drawing.Point(74, 35)
        Me.txtUserGface.Name = "txtUserGface"
        Me.txtUserGface.Size = New System.Drawing.Size(133, 20)
        Me.txtUserGface.TabIndex = 2
        '
        'lblUrlGface
        '
        Me.lblUrlGface.Location = New System.Drawing.Point(12, 90)
        Me.lblUrlGface.Name = "lblUrlGface"
        Me.lblUrlGface.Size = New System.Drawing.Size(19, 13)
        Me.lblUrlGface.TabIndex = 7
        Me.lblUrlGface.Text = "URL"
        '
        'UrlGface
        '
        Me.UrlGface.Location = New System.Drawing.Point(74, 87)
        Me.UrlGface.Name = "UrlGface"
        Me.UrlGface.Size = New System.Drawing.Size(133, 20)
        Me.UrlGface.TabIndex = 4
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 116)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl1.TabIndex = 9
        Me.LabelControl1.Text = "Sucursal"
        '
        'TextEdit1
        '
        Me.TextEdit1.Location = New System.Drawing.Point(74, 113)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(133, 20)
        Me.TextEdit1.TabIndex = 5
        '
        'lblEmpresa
        '
        Me.lblEmpresa.Location = New System.Drawing.Point(262, 12)
        Me.lblEmpresa.Name = "lblEmpresa"
        Me.lblEmpresa.Size = New System.Drawing.Size(41, 13)
        Me.lblEmpresa.TabIndex = 12
        Me.lblEmpresa.Text = "Empresa"
        '
        'txtCodEmpresa
        '
        Me.txtCodEmpresa.Location = New System.Drawing.Point(324, 9)
        Me.txtCodEmpresa.Name = "txtCodEmpresa"
        Me.txtCodEmpresa.Size = New System.Drawing.Size(133, 20)
        Me.txtCodEmpresa.TabIndex = 6
        '
        'lblCiudad
        '
        Me.lblCiudad.Location = New System.Drawing.Point(262, 38)
        Me.lblCiudad.Name = "lblCiudad"
        Me.lblCiudad.Size = New System.Drawing.Size(33, 13)
        Me.lblCiudad.TabIndex = 14
        Me.lblCiudad.Text = "Ciudad"
        '
        'txtCiudad
        '
        Me.txtCiudad.Location = New System.Drawing.Point(324, 35)
        Me.txtCiudad.Name = "txtCiudad"
        Me.txtCiudad.Size = New System.Drawing.Size(133, 20)
        Me.txtCiudad.TabIndex = 7
        '
        'lblDireccion
        '
        Me.lblDireccion.Location = New System.Drawing.Point(262, 64)
        Me.lblDireccion.Name = "lblDireccion"
        Me.lblDireccion.Size = New System.Drawing.Size(43, 13)
        Me.lblDireccion.TabIndex = 16
        Me.lblDireccion.Text = "Dirección"
        '
        'txtDireccion
        '
        Me.txtDireccion.Location = New System.Drawing.Point(324, 61)
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.Size = New System.Drawing.Size(133, 20)
        Me.txtDireccion.TabIndex = 8
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(13, 136)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(585, 200)
        Me.GridControl1.TabIndex = 17
        Me.GridControl1.UseEmbeddedNavigator = True
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.column1})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace
        Me.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click
        Me.GridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top
        '
        'column1
        '
        Me.column1.Caption = "Serie"
        Me.column1.Name = "column1"
        Me.column1.Visible = True
        Me.column1.VisibleIndex = 0
        '
        'frmGface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 359)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.lblDireccion)
        Me.Controls.Add(Me.txtDireccion)
        Me.Controls.Add(Me.lblCiudad)
        Me.Controls.Add(Me.txtCiudad)
        Me.Controls.Add(Me.lblEmpresa)
        Me.Controls.Add(Me.txtCodEmpresa)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TextEdit1)
        Me.Controls.Add(Me.lblUrlGface)
        Me.Controls.Add(Me.UrlGface)
        Me.Controls.Add(Me.lblUserGface)
        Me.Controls.Add(Me.txtUserGface)
        Me.Controls.Add(Me.lblPassGface)
        Me.Controls.Add(Me.txtPassGface)
        Me.Controls.Add(Me.cboTipoGface)
        Me.Controls.Add(Me.lblTipoGface)
        Me.Name = "frmGface"
        Me.Text = "Configuracion GFace"
        CType(Me.cboTipoGface.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassGface.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserGface.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UrlGface.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCodEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiudad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDireccion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTipoGface As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboTipoGface As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtPassGface As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblPassGface As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblUserGface As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtUserGface As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblUrlGface As DevExpress.XtraEditors.LabelControl
    Friend WithEvents UrlGface As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblEmpresa As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCodEmpresa As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblCiudad As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCiudad As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblDireccion As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDireccion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents column1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
