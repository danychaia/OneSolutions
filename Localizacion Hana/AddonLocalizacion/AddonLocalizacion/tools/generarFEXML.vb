﻿Imports System.Xml
Imports System.IO

Public Class generarFEXML
    Public Sub generarXML(DocEntry As String, objectType As String, oCompany As SAPbobsCOM.Company, SBO As SAPbouiCOM.Application)
        Try
            Dim doc As New XmlDocument
            Dim oRecord As SAPbobsCOM.Recordset
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            Dim fleteInter As String
            Dim seguroInter As String
            Dim gastosAduaneros As String
            Dim gastosTransporteOtros As String

            oRecord.DoQuery("CALL ENCABEZADO_FACTURA ('" & DocEntry & "','13E')")
            Dim writer As New XmlTextWriter("Comprobante (FE) No." & DocEntry.ToString & ".xml", System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)
            writer.Formatting = Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("factura")
            writer.WriteAttributeString("id", "comprobante")
            writer.WriteAttributeString("version", "2.0.0")
            writer.WriteStartElement("infoTributaria")
            createNode("razonSocial", oRecord.Fields.Item("razonSocial").Value.ToString, writer)
            'createNode("ambiente", oRecord.Fields.Item(0).Value.ToString, writer)
            'createNode("tipoEmision", oRecord.Fields.Item(1).Value.ToString, writer)
            createNode("ruc", oRecord.Fields.Item("ruc").Value.ToString.PadLeft(13, "0"), writer)
            'createNode("claveAcesso", claveAcceso(oRecord).PadLeft(49, "0"), writer)
            'createNode("claveAcesso", "", writer)
            createNode("codDoc", oRecord.Fields.Item("codDoc").Value.ToString.PadLeft(2, "0"), writer)
            createNode("estab", oRecord.Fields.Item("estab").Value.ToString.PadLeft(3, "0"), writer)
            createNode("ptoEmi", oRecord.Fields.Item("ptoEmi").Value.ToString.PadLeft(3, "0"), writer)
            createNode("secuencial", oRecord.Fields.Item("secuencial").Value.ToString.PadLeft(9, "0"), writer)
            createNode("dirMatriz", oRecord.Fields.Item("dirMatriz").Value.ToString, writer)
            Dim direccion = oRecord.Fields.Item("dirMatriz").Value.ToString
            Dim contribuyenteEspecial = oRecord.Fields.Item("contriespecial").Value.ToString
            Dim obliConta = oRecord.Fields.Item("contaobligado").Value.ToString
            ''Cierre info Tributaria
            writer.WriteEndElement()

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()

            writer.WriteStartElement("infoFactura")
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL SP_INFO_FACTURA ('" & DocEntry & "','13E')")
            createNode("fechaEmision", Date.Parse(oRecord.Fields.Item("DATE").Value.ToString).ToString("dd/MM/yyyy"), writer)
            If contribuyenteEspecial <> "" Then
                createNode("contribuyenteEspecial", contribuyenteEspecial, writer)
            End If

            createNode("obligadoContabilidad", obliConta, writer)
            createNode("comercioExterior", "EXPORTADOR", writer)
            createNode("incoTermFactura", oRecord.Fields.Item("U_INCO_TERM").Value, writer)
            createNode("lugarIncoTerm", oRecord.Fields.Item("U_LUGAR_INCOTERM").Value, writer)
            createNode("paisOrigen", oRecord.Fields.Item("U_PAIS_ORIGEN").Value, writer)
            createNode("puertoEmbarque", oRecord.Fields.Item("U_PUERTO_EMBARGUE").Value, writer)
            createNode("puertoDestino", oRecord.Fields.Item("U_PUERTO_DESTINO").Value, writer)
            createNode("paisDestino", oRecord.Fields.Item("U_PAIS_DESTINO").Value, writer)
            createNode("paisAdquisicion", oRecord.Fields.Item("U_PAIS_ADQUISION").Value, writer)
            createNode("tipoIdentificacionComprador", oRecord.Fields.Item("U_IDENTIFICACION").Value, writer)
            ' createNode("guiaRemision", "", writer)
            createNode("razonSocialComprador", oRecord.Fields.Item("CardName").Value.ToString, writer)
            createNode("identificacionComprador", oRecord.Fields.Item("U_DOCUMENTO").Value.ToString, writer)
            createNode("direccionComprador", oRecord.Fields.Item("DIRECCION").Value.ToString, writer)
            createNode("totalSinImpuestos", Double.Parse(oRecord.Fields.Item("sin_impuesto").Value).ToString("0.00"), writer)
            createNode("incoTermTotalSinImpuestos", oRecord.Fields.Item("U_TERM_TOT_SIN_IMPUESTO").Value.ToString, writer)
            createNode("totalDescuento", Double.Parse(oRecord.Fields.Item("totDescuento").Value).ToString("0.00"), writer)
            writer.WriteStartElement("totalConImpuestos")
            Dim importeTotal = oRecord.Fields.Item("DocTotal").Value.ToString
            Dim moneda = oRecord.Fields.Item("MONEDA").Value.ToString
            fleteInter = oRecord.Fields.Item("U_FLETE_INTERNA").Value.ToString
            seguroInter = oRecord.Fields.Item("U_SEGURO_INTERNA").Value.ToString
            gastosAduaneros = oRecord.Fields.Item("U_GASTOS_ADUANEROS").Value.ToString
            gastosTransporteOtros = oRecord.Fields.Item("U_G_TRANS_OTROS").Value.ToString
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()

            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL SP_Total_Con_Impuesto ('" & DocEntry & "','13')")
            If oRecord.RecordCount > 0 Then
                While oRecord.EoF = False
                    writer.WriteStartElement("totalImpuesto")
                    createNode("codigo", oRecord.Fields.Item(0).Value.ToString, writer)
                    createNode("codigoPorcentaje", oRecord.Fields.Item(1).Value.ToString, writer)
                    createNode("baseImponible", oRecord.Fields.Item(2).Value.ToString, writer)
                    createNode("tarifa", oRecord.Fields.Item(3).Value, writer)
                    createNode("valor", oRecord.Fields.Item(4).Value.ToString, writer)
                    writer.WriteEndElement()
                    oRecord.MoveNext()
                End While
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()

            ''Cierre TotalConImpuestos
            writer.WriteEndElement()
            createNode("propina", "0.00", writer)
            createNode("fleteInternacional", fleteInter, writer)
            createNode("seguroInternacional", seguroInter, writer)
            createNode("gastosAduaneros", gastosAduaneros, writer)
            createNode("gastosTransporteOtros", gastosTransporteOtros, writer)
            createNode("importeTotal", importeTotal, writer)
            createNode("moneda", moneda, writer)

            writer.WriteStartElement("pagos")
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL SP_Forma_Pago ('" & DocEntry & "','13')")
            If oRecord.RecordCount > 0 Then
                While oRecord.EoF = False
                    writer.WriteStartElement("pago")
                    createNode("formaPago", oRecord.Fields.Item(0).Value, writer)
                    createNode("total", oRecord.Fields.Item(1).Value, writer)
                    createNode("plazo", oRecord.Fields.Item(2).Value, writer)
                    createNode("unidadTiempo", oRecord.Fields.Item(3).Value, writer)
                    writer.WriteEndElement()
                    oRecord.MoveNext()
                End While
            End If
            ''Cierre Pagos
            writer.WriteEndElement()



            ''Cierre INFO FACTURA
            writer.WriteEndElement()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()

            writer.WriteStartElement("detalles")
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL SP_DetalleFac ('" & DocEntry & "','13')")


            If oRecord.RecordCount > 0 Then

                While oRecord.EoF = False
                    Dim oRecord2 As SAPbobsCOM.Recordset
                    oRecord2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    writer.WriteStartElement("detalle")
                    createNode("codigoPrincipal", oRecord.Fields.Item(0).Value.ToString, writer)
                    createNode("descripcion", oRecord.Fields.Item(1).Value.ToString, writer)
                    createNode("cantidad", oRecord.Fields.Item(2).Value.ToString, writer)
                    createNode("precioUnitario", oRecord.Fields.Item(3).Value.ToString, writer)
                    createNode("descuento", oRecord.Fields.Item(4).Value.ToString, writer)
                    createNode("precioTotalSinImpuesto", oRecord.Fields.Item(5).Value.ToString, writer)

                    oRecord2.DoQuery("CALL SP_DETALLEADICIONALES ('" & DocEntry & "','FE','" & oRecord.Fields.Item(0).Value.ToString & "')")
                    If oRecord2.RecordCount > 0 Then
                        writer.WriteStartElement("detallesAdicionales")
                        While oRecord2.EoF = False
                            writer.WriteStartElement("detAdicional")
                            writer.WriteAttributeString("nombre", oRecord2.Fields.Item("nombre").Value)
                            writer.WriteAttributeString("valor", oRecord2.Fields.Item("Valor").Value)
                            writer.WriteEndElement()
                            oRecord2.MoveNext()
                        End While
                        writer.WriteEndElement()
                    End If

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord2)
                    oRecord2 = Nothing
                    GC.Collect()

                    oRecord2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                    writer.WriteStartElement("impuestos")
                    oRecord2.DoQuery("CALL SP_Impuesto_Detalle ('" & DocEntry & "','" & oRecord.Fields.Item(0).Value & "','13')")
                    If oRecord2.RecordCount > 0 Then
                        While oRecord2.EoF = False
                            writer.WriteStartElement("impuesto")
                            createNode("codigo", oRecord2.Fields.Item(0).Value.ToString, writer)
                            createNode("codigoPorcentaje", oRecord2.Fields.Item(1).Value.ToString, writer)
                            createNode("tarifa", oRecord2.Fields.Item(3).Value.ToString, writer)
                            createNode("baseImponible", oRecord2.Fields.Item(2).Value.ToString, writer)
                            createNode("valor", oRecord2.Fields.Item(4).Value.ToString, writer)
                            writer.WriteEndElement()
                            oRecord2.MoveNext()
                        End While
                    End If

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord2)
                    oRecord2 = Nothing
                    GC.Collect()
                    writer.WriteEndElement()

                    writer.WriteEndElement()
                    oRecord.MoveNext()
                End While
            End If

            ''Cierre detalles
            writer.WriteEndElement()

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()
            ''Abre Campos Adicionales

            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            Dim en = "CALL SP_INFOADICIONAL ('" & DocEntry & "','FE')"
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



            ''Cierre Factura
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()
            If Directory.Exists("C:\OS_FE") = False Then
                Directory.CreateDirectory("C:\OS_FE")
            End If
            Dim esta = Application.StartupPath & "\Comprobante (FE) No." & DocEntry.ToString & ".xml"
            Dim va = "C:\OS_FE\Comprobante (FE) No." & DocEntry.ToString & ".xml"
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
                My.Computer.Network.UploadFile(va, Lista(0).ToString & "Comprobante (FE) No." & DocEntry.ToString & ".xml", Lista(1).ToString, Lista(2).ToString, True, 2500, FileIO.UICancelOption.DoNothing)
            End If
           
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub createNode(ByVal pID As String, ByVal pName As String, ByVal writer As XmlTextWriter)
        writer.WriteStartElement(pID)
        writer.WriteString(pName)
        writer.WriteEndElement()
    End Sub
End Class
