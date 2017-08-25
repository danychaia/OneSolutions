<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ParametrosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguracionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ORDENESDEVENTAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiariasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutomaticoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ParametrosToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(230, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ParametrosToolStripMenuItem
        '
        Me.ParametrosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguracionToolStripMenuItem, Me.ORDENESDEVENTAToolStripMenuItem})
        Me.ParametrosToolStripMenuItem.Name = "ParametrosToolStripMenuItem"
        Me.ParametrosToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.ParametrosToolStripMenuItem.Text = "Parametros"
        '
        'ConfiguracionToolStripMenuItem
        '
        Me.ConfiguracionToolStripMenuItem.Name = "ConfiguracionToolStripMenuItem"
        Me.ConfiguracionToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ConfiguracionToolStripMenuItem.Text = "Configuracion"
        '
        'ORDENESDEVENTAToolStripMenuItem
        '
        Me.ORDENESDEVENTAToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DiariasToolStripMenuItem, Me.AutomaticoToolStripMenuItem})
        Me.ORDENESDEVENTAToolStripMenuItem.Name = "ORDENESDEVENTAToolStripMenuItem"
        Me.ORDENESDEVENTAToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ORDENESDEVENTAToolStripMenuItem.Text = "Envio de Ordenes"
        '
        'DiariasToolStripMenuItem
        '
        Me.DiariasToolStripMenuItem.Name = "DiariasToolStripMenuItem"
        Me.DiariasToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.DiariasToolStripMenuItem.Text = "Diarias"
        '
        'AutomaticoToolStripMenuItem
        '
        Me.AutomaticoToolStripMenuItem.Name = "AutomaticoToolStripMenuItem"
        Me.AutomaticoToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.AutomaticoToolStripMenuItem.Text = "Automatico"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipText = "Generador de Ordenes"
        Me.NotifyIcon1.BalloonTipTitle = "Generador de Ordenes"
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Interfaz Cargas Ordenes de venta"
        Me.NotifyIcon1.Visible = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(230, 64)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "ICG "
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ParametrosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfiguracionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ORDENESDEVENTAToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiariasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutomaticoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon

End Class
