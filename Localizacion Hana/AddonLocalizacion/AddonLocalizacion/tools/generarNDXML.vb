Imports System.Xml
Imports System.IO

Public Class generarNDXML
    Public Sub generarXML(DocEntry As String, objectType As String, oCompany As SAPbobsCOM.Company, SBO As SAPbouiCOM.Application)
        Dim doc As New XmlDocument
        Dim oRecord As SAPbobsCOM.Recordset
        Dim oContriEspecial As String
        Dim oObliconta As String
        oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecord.DoQuery("CALL ENCABEZADO_FACTURA ('" & DocEntry & "','ND')")
        Dim writer As New XmlTextWriter("Comprobante (ND) No." & DocEntry.ToString & ".xml", System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.Formatting = Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("notaDebito")
        writer.WriteAttributeString("id", "comprobante")
        writer.WriteAttributeString("version", "1.0.0")
        writer.WriteStartElement("infoTributaria")
        createNode("razonSocial", oRecord.Fields.Item(2).Value.ToString, writer)
        'createNode("ambiente", oRecord.Fields.Item(0).Value.ToString, writer)
        'createNode("tipoEmision", oRecord.Fields.Item(1).Value.ToString, writer)
        createNode("ruc", oRecord.Fields.Item(3).Value.ToString.PadLeft(13, "0"), writer)
        'createNode("claveAcesso", claveAcceso(oRecord).PadLeft(49, "0"), writer)
        'createNode("claveAcesso", "", writer)
        createNode("codDoc", oRecord.Fields.Item("codDoc").Value.ToString.PadLeft(2, "0"), writer)
        createNode("estab", oRecord.Fields.Item("estab").Value.ToString.PadLeft(3, "0"), writer)
        createNode("ptoEmi", oRecord.Fields.Item("ptoEmi").Value.ToString.PadLeft(3, "0"), writer)
        createNode("secuencial", oRecord.Fields.Item("secuencial").Value.ToString.PadLeft(9, "0"), writer)
        createNode("dirMatriz", oRecord.Fields.Item("dirMatriz").Value.ToString, writer)
        Dim direccion = oRecord.Fields.Item("dirMatriz").Value.ToString
        oContriEspecial = oRecord.Fields.Item("contriespecial").Value
        oObliconta = oRecord.Fields.Item("contaobligado").Value
        ''Cierre info Tributaria
        writer.WriteEndElement()

        writer.WriteStartElement("infoNotaDebito")
        oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecord.DoQuery("CALL SP_INFO_FACTURA ('" & DocEntry & "','ND')")
        createNode("fechaEmision", Date.Parse(oRecord.Fields.Item("DATE").Value.ToString).ToString("dd/MM/yyyy"), writer)
        createNode("tipoIdentificacionComprador", oRecord.Fields.Item("U_IDENTIFICACION").Value.ToString, writer)
        createNode("razonSocialComprador", oRecord.Fields.Item("CardName").Value.ToString, writer)
        createNode("identificacionComprador", oRecord.Fields.Item("U_DOCUMENTO").Value.ToString, writer)
        If oContriEspecial <> "" Then
            createNode("contribuyenteEspecial", oContriEspecial, writer)
        End If

        createNode("obligadoContabilidad", oObliconta, writer)
        createNode("codDocModificado", "01", writer)
        createNode("numDocModificado", oRecord.Fields.Item("DocModifi").Value, writer)
        createNode("fechaEmisionDocSustento", oRecord.Fields.Item("FechaModifi").Value, writer)
        createNode("totalSinImpuestos", oRecord.Fields.Item("sin_impuesto").Value.ToString, writer)

        Dim importeTotal = oRecord.Fields.Item("DocTotal").Value.ToString
        Dim moneda = oRecord.Fields.Item("MONEDA").Value.ToString
        Dim motivo = oRecord.Fields.Item("Comments").Value.ToString
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
        oRecord = Nothing
        GC.Collect()
        oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecord.DoQuery("CALL SP_Impuesto_Detalle ('" & DocEntry & "','','ND')")
        If oRecord.RecordCount > 0 Then
            writer.WriteStartElement("impuestos")
            While oRecord.EoF = False
                writer.WriteStartElement("impuesto")
                createNode("codigo", oRecord.Fields.Item(0).Value.ToString, writer)
                createNode("codigoPorcentaje", oRecord.Fields.Item(1).Value.ToString, writer)
                createNode("tarifa", oRecord.Fields.Item(3).Value.ToString, writer)
                createNode("baseImponible", oRecord.Fields.Item(2).Value.ToString, writer)
                createNode("valor", oRecord.Fields.Item(4).Value.ToString, writer)
                writer.WriteEndElement()
                oRecord.MoveNext()
            End While
            ''Cierre de impuestos 
            writer.WriteEndElement()
        End If
        createNode("valorTotal", importeTotal, writer)


        ''Cierre infoNotaDebito
        writer.WriteEndElement()
        writer.WriteStartElement("motivos")
        writer.WriteStartElement("motivo")
        createNode("razon", motivo, writer)
        createNode("valor", importeTotal, writer)
        writer.WriteEndElement()
        writer.WriteEndElement()
        ''Cierre Nota de Débito

        System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
        oRecord = Nothing
        GC.Collect()
        ''Abre Campos Adicionales

        oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        Dim en = "CALL SP_INFOADICIONAL ('" & DocEntry & "','ND')"
        oRecord.DoQuery(en)
        If oRecord.RecordCount > 0 Then
            writer.WriteStartElement("infoAdicional")

            While oRecord.EoF = False
                writer.WriteStartElement("campoAdicional")
                writer.WriteAttributeString("nombre", oRecord.Fields.Item("nombre").Value)
                writer.WriteString(oRecord.Fields.Item("Valor").Value)
                writer.WriteEndElement()
                oRecord.MoveNext()
            End While
            writer.WriteEndElement()
            'Cierre Campos Adicionales

        End If
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
        oRecord = Nothing
        GC.Collect()


        writer.WriteEndElement()
        writer.Close()
        If Directory.Exists("C:\OS_FE") = False Then
            Directory.CreateDirectory("C:\OS_FE")
        End If
        Dim esta = Application.StartupPath & "\Comprobante (ND) No." & DocEntry.ToString & ".xml"
        Dim va = "C:\OS_FE\Comprobante (ND) No." & DocEntry.ToString & ".xml"
        If File.Exists(va) Then
            File.Delete(va)
            File.Move(esta, va)
        Else
            File.Move(esta, va)
        End If


        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\CONFIGURACION.xml") = True Then
            Dim Docc As New XmlDocument, ListaNodos As XmlNodeList, Nodo As XmlNode
            Dim Lista As ArrayList = New ArrayList()
            Docc.Load(Application.StartupPath & "\CONFIGURACION.xml")

            ListaNodos = Docc.SelectNodes("/CONFIGURACION/PARAMETRO")

            For Each Nodo In ListaNodos
                Lista.Add(Nodo.ChildNodes.Item(0).InnerText)
            Next
            My.Computer.Network.UploadFile(va, Lista(0).ToString & "Comprobante (ND) No." & DocEntry.ToString & ".xml", Lista(1).ToString, Lista(2).ToString, True, 2500, FileIO.UICancelOption.DoNothing)
        End If
        
    End Sub

    Private Sub createNode(ByVal pID As String, ByVal pName As String, ByVal writer As XmlTextWriter)
        writer.WriteStartElement(pID)
        writer.WriteString(pName)
        writer.WriteEndElement()
    End Sub
End Class
