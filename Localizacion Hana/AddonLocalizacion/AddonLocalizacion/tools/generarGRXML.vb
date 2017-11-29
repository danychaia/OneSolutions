Imports System.Xml
Imports System.IO

Public Class generarGRXML
    Public Sub generarXML(DocEntry As String, objectType As String, oCompany As SAPbobsCOM.Company, SBO As SAPbouiCOM.Application)
        Try
            Dim doc As New XmlDocument
            Dim oRecord As SAPbobsCOM.Recordset
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            oRecord.DoQuery("CALL ENCABEZADO_FACTURA ('" & DocEntry & "','" & objectType & "')")
            Dim writer As New XmlTextWriter("Comprobante (GR) No." & DocEntry.ToString & ".xml", System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)
            writer.Formatting = Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("guiaRemision")
            writer.WriteAttributeString("id", "comprobante")
            writer.WriteAttributeString("version", "1.1.0")
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
            Dim oContriEspecial = oRecord.Fields.Item("contriespecial").Value
            Dim oObliconta = oRecord.Fields.Item("contaobligado").Value
            ''Cierre info Tributaria
            writer.WriteEndElement()

            writer.WriteStartElement("infoGuiaRemision")
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL SP_INFO_FACTURA ('" & DocEntry & "','" & objectType & "')")
            createNode("dirEstablecimiento", oRecord.Fields.Item("DIRECCION").Value.ToString, writer)
            createNode("dirPartida", oRecord.Fields.Item("PARTIDA").Value.ToString, writer)
            createNode("razonSocialTransportista", oRecord.Fields.Item(0).Value, writer)
            createNode("tipoIdentificacionTransportista", oRecord.Fields.Item(1).Value.ToString, writer)
            createNode("rucTransportista", oRecord.Fields.Item(2).Value.ToString, writer)
            createNode("obligadoContabilidad", oObliconta, writer)
            If oContriEspecial <> "" Then
                createNode("contribuyenteEspecial", oContriEspecial, writer)
            End If

            'createNode("guiaRemision", "", writer)
            createNode("fechaIniTransporte", oRecord.Fields.Item(3).Value.ToString, writer)
            createNode("fechaFinTransporte", oRecord.Fields.Item(4).Value.ToString, writer)
            createNode("placa", oRecord.Fields.Item(5).Value.ToString, writer)
            'cierre infoGuiaRemision
            writer.WriteEndElement()

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()

            writer.WriteStartElement("destinatarios")
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL SP_DetalleFac ('" & DocEntry & "','" & objectType & "')")
            If oRecord.RecordCount > 0 Then
                While oRecord.EoF = False
                    Dim inicial = oRecord.Fields.Item(0).Value
                    Dim final = oRecord.Fields.Item(1).Value
                    Dim tipoDoc = oRecord.Fields.Item(2).Value
                    Dim identifi = oRecord.Fields.Item(3).Value
                    Dim motivo = oRecord.Fields.Item(4).Value
                    Dim direguia = oRecord.Fields.Item(5).Value
                    Dim razon = oRecord.Fields.Item(6).Value
                    Dim oRecord2 As SAPbobsCOM.Recordset
                    oRecord2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    Dim sssj = "CALL  FACTURAS_GUIA_REMISION (" & DocEntry & ",'" & inicial & "','" & final & "','" & oRecord.Fields.Item(3).Value & "',2)"
                    oRecord2.DoQuery("CALL  FACTURAS_GUIA_REMISION (" & DocEntry & ",'" & inicial & "','" & final & "','" & oRecord.Fields.Item(3).Value & "',2)")
                    If oRecord2.RecordCount > 0 Then
                        While oRecord2.EoF = False
                            writer.WriteStartElement("destinatario")
                            createNode("identificacionDestinatario", Trim(Right(identifi, Len(identifi) - 2)).ToString(), writer)
                            createNode("razonSocialDestinatario", razon, writer)
                            createNode("dirDestinatario", direguia, writer)
                            createNode("motivoTraslado", motivo, writer)
                            createNode("codDocSustento", tipoDoc, writer)
                            Dim oRecord3 As SAPbobsCOM.Recordset
                            oRecord3 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                            oRecord3.DoQuery("CALL  FACTURAS_GUIA_REMISION (" & DocEntry & ",'" & oRecord2.Fields.Item(0).Value & "','" & final & "','" & identifi & "',1)")
                            If oRecord3.RecordCount > 0 Then
                                writer.WriteStartElement("detalles")
                                While oRecord3.EoF = False
                                    writer.WriteStartElement("detalle")
                                    createNode("codigoInterno", oRecord3.Fields.Item(1).Value, writer)
                                    createNode("codigoAdicional", oRecord3.Fields.Item("auxiliar").Value, writer)
                                    createNode("descripcion", oRecord3.Fields.Item(2).Value, writer)
                                    createNode("cantidad", oRecord3.Fields.Item(3).Value, writer)

                                    'Adicionales a detalle
                                    Dim oRecord4 As SAPbobsCOM.Recordset
                                    oRecord4 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                                    oRecord4.DoQuery("CALL SP_DETALLEADICIONALES ('" & DocEntry & "','GR','" & oRecord3.Fields.Item(1).Value.ToString & "')")
                                    If oRecord4.RecordCount > 0 Then
                                        writer.WriteStartElement("detallesAdicionales")
                                        While oRecord4.EoF = False
                                            writer.WriteStartElement("detAdicional")
                                            writer.WriteAttributeString("nombre", oRecord4.Fields.Item("nombre").Value)
                                            writer.WriteAttributeString("valor", oRecord4.Fields.Item("Valor").Value)
                                            writer.WriteEndElement()
                                            oRecord4.MoveNext()
                                        End While
                                        writer.WriteEndElement()
                                    End If

                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord4)
                                    oRecord4 = Nothing
                                    GC.Collect()


                                    writer.WriteEndElement()
                                    oRecord3.MoveNext()
                                End While
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord3)
                                oRecord3 = Nothing
                                GC.Collect()
                                'fin detalles
                                writer.WriteEndElement()
                            End If
                            'fin destinatarios
                            writer.WriteEndElement()
                            oRecord2.MoveNext()
                        End While
                    End If
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord2)
                    oRecord2 = Nothing
                    GC.Collect()

                    oRecord.MoveNext()
                End While
            End If
            'Cierre destinatario
            writer.WriteEndElement()

            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()
            ''Abre Campos Adicionales

            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            Dim en = "CALL SP_INFOADICIONAL ('" & DocEntry & "','GR')"
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
            Dim esta = Application.StartupPath & "\Comprobante (GR) No." & DocEntry.ToString & ".xml"
            Dim va = "C:\OS_FE\Comprobante (GR) No." & DocEntry.ToString & ".xml"
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
                My.Computer.Network.UploadFile(va, Lista(0).ToString & "Comprobante (GR) No." & DocEntry.ToString & ".xml", Lista(1).ToString, Lista(2).ToString, True, 2500, FileIO.UICancelOption.DoNothing)
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
