Imports System.Xml
Imports System.IO

Public Class generarATS
    Public Sub generarXML(mes As String, ano As String, objectType As String, oCompany As SAPbobsCOM.Company, SBO As SAPbouiCOM.Application)
        Try
            Dim doc As New XmlDocument
            Dim oRecord As SAPbobsCOM.Recordset
            Dim oProgressBar As SAPbouiCOM.ProgressBar        
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            oRecord.DoQuery("CALL ATS_Encabezado (" & ano & "," & mes & ")")

            Dim writer As New XmlTextWriter("Comprobante (ATS) No." & mes & "-" & ano & ".xml", System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)
            writer.Formatting = Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("iva")
            ' writer.WriteAttributeString("version", "1.0")
            createNode("TipoIDInformante", oRecord.Fields.Item("TipoIDInformante").Value, writer)
            createNode("IdInformante", oRecord.Fields.Item("IdInformante").Value, writer)
            createNode("razonSocial", oRecord.Fields.Item("razonSocial").Value, writer)
            createNode("Anio", ano, writer)
            createNode("Mes", mes, writer)
            createNode("numEstabRuc", oRecord.Fields.Item("numEstabRuc").Value.ToString.PadLeft(3, "0"), writer)
            createNode("totalVentas", Double.Parse(oRecord.Fields.Item("totalVentas").Value).ToString("N2"), writer)
            createNode("codigoOperativo", oRecord.Fields.Item("codigoOperativo").Value, writer)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
            oRecord = Nothing
            GC.Collect()


            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL ATS_DetalleCompras (" & ano & "," & mes & ")")

            If oRecord.RecordCount > 0 Then
                writer.WriteStartElement("compras")
                oProgressBar = SBOApplication.StatusBar.CreateProgressBar("Generando Compras", oRecord.RecordCount, True)

                While oRecord.EoF = False
                    writer.WriteStartElement("detalleCompras")
                    DetalleCompras(oRecord, oCompany, SBOApplication, writer, ano, mes)
                    'Detalle Compras 
                    oRecord.MoveNext()
                    writer.WriteEndElement()
                    oProgressBar.Value = oProgressBar.Value + 1
                End While
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
                oProgressBar.Stop()
                oProgressBar = Nothing
                'Compras notas de credito 
                oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oProgressBar = SBOApplication.StatusBar.CreateProgressBar("Generando Compras", oRecord.RecordCount, True)
                oRecord.DoQuery("CALL ATS_NCDetalleCompras (" & ano & "," & mes & ")")
                While oRecord.EoF = False
                    writer.WriteStartElement("detalleCompras")
                    DetalleCompras(oRecord, oCompany, SBOApplication, writer, ano, mes)
                    writer.WriteEndElement()
                    oRecord.MoveNext()
                    oProgressBar.Value = oProgressBar.Value + 1
                End While
                'Compras 
                writer.WriteEndElement()
                oProgressBar.Stop()
                oProgressBar = Nothing
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
            Else
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
            End If

           

            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL ATS_detalleVentas (" & ano & "," & mes & ")")

            If oRecord.RecordCount > 0 Then
                oProgressBar = SBOApplication.StatusBar.CreateProgressBar("Generando Ventas", oRecord.RecordCount, True)
                writer.WriteStartElement("ventas")
                While oRecord.EoF = False
                    writer.WriteStartElement("detalleVentas")
                    createNode("tpIdCliente", oRecord.Fields.Item("tpIdCliente").Value, writer)
                    createNode("idCliente", oRecord.Fields.Item("idCliente").Value, writer)

                    Dim oRecord2 As SAPbobsCOM.Recordset
                    oRecord2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oRecord2.DoQuery("CALL ATS_denoCli ('" & oRecord.Fields.Item("idCliente").Value & "')")
                    If oRecord.Fields.Item("tpIdCliente").Value <> "04" Then
                        createNode("denoCli", oRecord2.Fields.Item("denoCli").Value, writer)
                    End If
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord2)
                    oRecord2 = Nothing
                    GC.Collect()
                    createNode("parteRelVtas", oRecord.Fields.Item("parteRelVtas").Value, writer)
                    createNode("tipoComprobante", oRecord.Fields.Item("tipoComprobante").Value, writer)
                    createNode("tipoEmision", oRecord.Fields.Item("tipoEm").Value, writer)
                    createNode("numeroComprobantes", oRecord.Fields.Item("numeroComprobantes").Value, writer)
                    createNode("baseNoGraIva", Double.Parse(oRecord.Fields.Item("baseNoGraIva").Value).ToString("N2"), writer)
                    createNode("baseImponible", Double.Parse(oRecord.Fields.Item("baseImponible").Value).ToString("N2"), writer)
                    createNode("baseImpGrav", Double.Parse(oRecord.Fields.Item("baseImpGrav").Value).ToString("N2"), writer)
                    createNode("montoIva", Double.Parse(oRecord.Fields.Item("montoIva").Value).ToString("N2"), writer)

                    oRecord2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oRecord2.DoQuery("CALL ATS_Compensacion (" & ano & "," & mes & ",'" & oRecord.Fields.Item("tipoComprobante").Value & "','" & oRecord.Fields.Item("idCliente").Value & "')")
                    If oRecord2.RecordCount > 0 Then
                        writer.WriteStartElement("compensaciones")
                        While oRecord2.EoF = False
                            writer.WriteStartElement("compensacion")
                            createNode("tipoCompe", oRecord2.Fields.Item("tipoCompe").Value, writer)
                            createNode("monto", oRecord2.Fields.Item("monto").Value, writer)
                            writer.WriteEndElement()
                            oRecord2.MoveNext()
                        End While
                        'Cierre Compensaciones
                        writer.WriteEndElement()
                    End If

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord2)
                    oRecord2 = Nothing
                    GC.Collect()
                    createNode("montoIce", Double.Parse(oRecord.Fields.Item("montoIce").Value).ToString("N2"), writer)
                    createNode("valorRetIva", Double.Parse(oRecord.Fields.Item("valorRetIva").Value).ToString("N2"), writer)
                    createNode("valorRetRenta", Double.Parse(oRecord.Fields.Item("valorRetRenta").Value).ToString("N2"), writer)
                    If oRecord.Fields.Item("tipoComprobante").Value <> "04" Then
                        oRecord2 = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        Dim sql = "CALL ATS_formasDePago (" & ano & "," & mes & ",'" & oRecord.Fields.Item("tipoComprobante").Value & "','" & oRecord.Fields.Item("idCliente").Value & "')"
                        oRecord2.DoQuery(sql)
                        writer.WriteStartElement("formasDePago")
                        While oRecord2.EoF = False
                            createNode("formaPago", oRecord2.Fields.Item("formaPago").Value, writer)
                            oRecord2.MoveNext()
                        End While
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord2)
                        oRecord2 = Nothing
                        GC.Collect()
                        writer.WriteEndElement()
                    End If
                                      
                    'Fin detalle ventas 
                    writer.WriteEndElement()
                    oRecord.MoveNext()
                    oProgressBar.Value = oProgressBar.Value + 1
                End While
                'Ciere Ventas 
                writer.WriteEndElement()
                oProgressBar.Stop()
                oProgressBar = Nothing
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
            Else
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
            End If
             
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL ATS_ventasEstablecimiento (" & ano & "," & mes & ")")

            If oRecord.RecordCount > 0 Then
                writer.WriteStartElement("ventasEstablecimiento")
                While oRecord.EoF = False
                    writer.WriteStartElement("ventaEst")
                    createNode("codEstab", oRecord.Fields.Item("codEstab").Value, writer)
                    createNode("ventasEstab", Double.Parse(oRecord.Fields.Item("ventasEstab").Value).ToString("N2"), writer)
                    createNode("ivaComp", Double.Parse(oRecord.Fields.Item("ivaCompe").Value).ToString("N2"), writer)
                    'Cierre ventasEstablecimiento
                    writer.WriteEndElement()
                    oRecord.MoveNext()
                End While

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
                'Cierre ventasEstablecimiento
                writer.WriteEndElement()
            Else
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
            End If

           
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL ATS_detalleExportaciones (" & ano & "," & mes & ")")
            If oRecord.RecordCount > 0 Then
                'Inicio exportaciones.
                writer.WriteStartElement("exportaciones")
                While oRecord.EoF = False
                    writer.WriteStartElement("detalleExportaciones")
                    detalleExportaciones(oRecord, oCompany, SBOApplication, writer, ano, mes)
                    writer.WriteEndElement()
                    oRecord.MoveNext()
                End While
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
                oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRecord.DoQuery("CALL ATS_detalleExportacionesNC (" & ano & "," & mes & ")")
                While oRecord.EoF = False
                    writer.WriteStartElement("detalleExportaciones")
                    detalleExportaciones(oRecord, oCompany, SBOApplication, writer, ano, mes)
                    writer.WriteEndElement()
                    oRecord.MoveNext()
                End While
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
                'Fin exportaciones
                writer.WriteEndElement()
            Else
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
            End If
           
          
            
            'INICIO ANULADOS
            oRecord = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecord.DoQuery("CALL ATS_Anulados (" & ano & "," & mes & ")")
            If oRecord.RecordCount > 0 Then
                writer.WriteStartElement("anulados")
                While oRecord.EoF = False
                    writer.WriteStartElement("detalleAnulados")
                    createNode("tipoComprobante", oRecord.Fields.Item("tipoComprobante").Value, writer)
                    createNode("establecimiento", oRecord.Fields.Item("establecimiento").Value, writer)
                    createNode("puntoEmision", oRecord.Fields.Item("puntoEmision").Value, writer)
                    createNode("secuencialInicio", oRecord.Fields.Item("secuencialInicio").Value, writer)
                    createNode("secuencialFin", oRecord.Fields.Item("secuencialFin").Value, writer)
                    createNode("autorizacion", oRecord.Fields.Item("autorizacion").Value, writer)
                    writer.WriteEndElement()
                    oRecord.MoveNext()
                End While

                'FINAL ANULADOS
                writer.WriteEndElement()
            Else
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecord)
                oRecord = Nothing
                GC.Collect()
            End If

            'While oRecord.EoF = False

            'End While

            ''Cierre Factura
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()

            If Directory.Exists("C:\OS_ATS\") = False Then
                Directory.CreateDirectory("C:\OS_ATS\")
            End If
            Dim esta = Application.StartupPath & "\Comprobante (ATS) No." & mes & "-" & ano & ".xml"
            Dim va = "C:\OS_ATS\Comprobante (ATS) No." & mes & "-" & ano & ".xml"
            If File.Exists(va) Then
                File.Delete(va)
                File.Move(esta, va)
            Else
                File.Move(esta, va)
            End If
            SBOApplication.MessageBox("ATS Generado en la ruta C:/OS_ATS", 1, "Ok")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub createNode(ByVal pID As String, ByVal pName As String, ByVal writer As XmlTextWriter)
        writer.WriteStartElement(pID)
        writer.WriteString(pName)
        writer.WriteEndElement()
    End Sub

    Private Sub DetalleCompras(oRecord As SAPbobsCOM.Recordset, oCompany As SAPbobsCOM.Company, application As SAPbouiCOM.Application, writer As XmlTextWriter, ano As String, mes As String)
        Try
            createNode("codSustento", oRecord.Fields.Item("CodSustento").Value, writer)
            createNode("tpIdProv", oRecord.Fields.Item("tpIdProv").Value, writer)
            createNode("idProv", oRecord.Fields.Item("idProv").Value, writer)
            createNode("tipoComprobante", oRecord.Fields.Item("tipoComprobante").Value, writer)
            createNode("parteRel", oRecord.Fields.Item("parteRel").Value, writer)
            createNode("fechaRegistro", oRecord.Fields.Item("fechaRegistro").Value, writer)
            createNode("establecimiento", oRecord.Fields.Item("establecimiento").Value, writer)
            createNode("puntoEmision", oRecord.Fields.Item("puntoEmision").Value, writer)
            createNode("secuencial", oRecord.Fields.Item("secuencial").Value, writer)
            createNode("fechaEmision", oRecord.Fields.Item("fechaEmision").Value, writer)
            createNode("autorizacion", oRecord.Fields.Item("autorizacion").Value, writer)
            createNode("baseNoGraIva", Double.Parse(oRecord.Fields.Item("baseNoGraIva").Value).ToString("N2"), writer)
            createNode("baseImponible", Double.Parse(oRecord.Fields.Item("baseImponible").Value).ToString("N2"), writer)
            createNode("baseImpGrav", Double.Parse(oRecord.Fields.Item("baseImpGrav").Value).ToString("N2"), writer)
            createNode("baseImpExe", Double.Parse(oRecord.Fields.Item("baseImpExe").Value).ToString("N2"), writer)
            createNode("montoIce", Double.Parse(oRecord.Fields.Item("montoIce").Value).ToString("N2"), writer)
            createNode("montoIva", Double.Parse(oRecord.Fields.Item("montoIva").Value.ToString).ToString("N2"), writer)
            createNode("valRetBien10", Double.Parse(oRecord.Fields.Item("valRetBien10").Value).ToString("N2"), writer)
            createNode("valRetServ20", Double.Parse(oRecord.Fields.Item("valRetServ20").Value).ToString("N2"), writer)
            createNode("valorRetBienes", Double.Parse(oRecord.Fields.Item("valRetServ20").Value).ToString("N2"), writer)
            createNode("valRetServ50", Double.Parse(oRecord.Fields.Item("ValorRetServ50").Value).ToString("N2"), writer)
            createNode("valorRetServicios", Double.Parse(oRecord.Fields.Item("valorRetServicios").Value).ToString("N2"), writer)
            createNode("valRetServ100", Double.Parse(oRecord.Fields.Item("ValorRetServ100").Value).ToString("N2"), writer)
            createNode("totbasesImpReemb", Double.Parse(oRecord.Fields.Item("totbasesImpReemb").Value).ToString("N2"), writer)

            writer.WriteStartElement("pagoExterior")
            createNode("pagoLocExt", oRecord.Fields.Item("pagoLocExt").Value, writer)
            If oRecord.Fields.Item("pagoLocExt").Value = "02" Then
                createNode("tipoRegi", oRecord.Fields.Item("tipoRegi").Value, writer)
                createNode("paisEfecPagoGen", oRecord.Fields.Item("paisEfecPagoGen").Value, writer)
            End If
            createNode("paisEfecPago", oRecord.Fields.Item("paisEfecPago").Value, writer)
            createNode("aplicConvDobTrib", oRecord.Fields.Item("aplicConvDobTrib").Value, writer)
            createNode("pagExtSujRetNorLeg", oRecord.Fields.Item("pagExtSujRetNorLeg").Value, writer)
            'Fin pago exterior
            writer.WriteEndElement()
            'EMPIEZA FORMAS DE PAGO PARA COMPRAS
            Dim oRecordP As SAPbobsCOM.Recordset


            If oRecord.Fields.Item("tipoComprobante").Value = "04" Or oRecord.Fields.Item("tipoComprobante").Value = "05" Then
                createNode("docModificado", oRecord.Fields.Item("docModificado").Value, writer)
                createNode("estabModificado", oRecord.Fields.Item("estabModificado").Value, writer)
                createNode("ptoEmiModificado", oRecord.Fields.Item("ptoEmiModificado").Value, writer)
                createNode("secModificado", oRecord.Fields.Item("secModificado").Value, writer)
                createNode("autModificado", oRecord.Fields.Item("autModificado").Value, writer)
            Else
                'DETALLE AIR PARA COMPRAS 
                oRecordP = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRecordP.DoQuery("CALL ATS_Air (" & oRecord.Fields.Item("DocEntry").Value & ")")
                If oRecordP.RecordCount > 0 Then
                    writer.WriteStartElement("air")
                    While oRecordP.EoF = False
                        writer.WriteStartElement("detalleAir")
                        createNode("codRetAir", oRecordP.Fields.Item("codRetAir").Value, writer)
                        createNode("baseImpAir", Double.Parse(oRecordP.Fields.Item("baseImpAir").Value).ToString("N2"), writer)
                        createNode("porcentajeAir", Double.Parse(oRecordP.Fields.Item("porcentajeAir").Value).ToString("N2"), writer)
                        createNode("valRetAir", Double.Parse(oRecordP.Fields.Item("valRetAir").Value).ToString("N2"), writer)
                        'Fin detalle Air
                        writer.WriteEndElement()
                        oRecordP.MoveNext()
                    End While
                    writer.WriteEndElement()
                End If

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordP)
                oRecordP = Nothing
                GC.Collect()
                If oRecord.Fields.Item("tipoComprobante").Value <> "41" Then
                    createNode("estabRetencion1", oRecord.Fields.Item("estabRetencion1").Value, writer)
                    createNode("ptoEmiRetencion1", oRecord.Fields.Item("ptoEmiRetencion1").Value, writer)
                    createNode("secRetencion1", oRecord.Fields.Item("secRetencion1").Value, writer)
                    createNode("autRetencion1", oRecord.Fields.Item("autRetencion1").Value, writer)
                    createNode("fechaEmiRet1", oRecord.Fields.Item("fechaEmiRet1").Value, writer)
                Else
                    oRecordP = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    oRecordP.DoQuery("CALL ATS_Reembolso (" & oRecord.Fields.Item("DocEntry").Value & ")")
                    If oRecordP.RecordCount > 0 Then
                        writer.WriteStartElement("reembolsos")
                        While oRecordP.EoF = False
                            writer.WriteStartElement("reembolso")
                            createNode("tipoComprobanteReemb", oRecordP.Fields.Item("tipoComprobanteReemb").Value, writer)
                            createNode("tpIdProvReemb", oRecordP.Fields.Item("tpIdProvReemb").Value, writer)
                            createNode("idProvReemb", oRecordP.Fields.Item("idProvReemb").Value, writer)
                            createNode("establecimientoReemb", oRecordP.Fields.Item("establecimientoReemb").Value, writer)
                            createNode("puntoEmisionReemb", oRecordP.Fields.Item("puntoEmisionReemb").Value, writer)
                            createNode("secuencialReemb", oRecordP.Fields.Item("secuencialReemb").Value, writer)
                            createNode("fechaEmisionReemb", oRecordP.Fields.Item("fechaEmisionReemb").Value, writer)
                            createNode("autorizacionReemb", oRecordP.Fields.Item("autorizacionReemb").Value, writer)
                            createNode("baseImponibleReemb", Double.Parse(oRecordP.Fields.Item("baseImponibleReemb").Value).ToString("N2"), writer)
                            createNode("baseImpGravReemb", Double.Parse(oRecordP.Fields.Item("baseImpGravReemb").Value).ToString("N2"), writer)
                            createNode("baseNoGraIvaReemb", Double.Parse(oRecordP.Fields.Item("baseNoGraIvaReemb").Value).ToString("N2"), writer)
                            createNode("baseImpExeReemb", Double.Parse(oRecordP.Fields.Item("baseImpExeReemb").Value).ToString("N2"), writer)
                            createNode("montoIceRemb", Double.Parse(oRecordP.Fields.Item("montoIceRemb").Value).ToString("N2"), writer)
                            createNode("montoIvaRemb", Double.Parse(oRecordP.Fields.Item("montoIvaRemb").Value).ToString("N2"), writer)
                            'FIN REEMBOLSO
                            writer.WriteEndElement()
                            oRecordP.MoveNext()
                        End While
                        'FIN REEMBOLSOS
                        writer.WriteEndElement()
                    End If
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordP)
                    oRecordP = Nothing
                    GC.Collect()
                End If

            End If

        Catch ex As Exception
            SBOApplication.SetStatusBarMessage(ex.Message & "  " & oRecord.Fields.Item("DocEntry").Value, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try
       
    End Sub

    Private Sub detalleExportaciones(oRecord As SAPbobsCOM.Recordset, oCompany As SAPbobsCOM.Company, application As SAPbouiCOM.Application, writer As XmlTextWriter, ano As String, mes As String)
        Try
            createNode("tpIdClienteEx", oRecord.Fields.Item("tpIdClienteEx").Value, writer)
            createNode("idClienteEx", oRecord.Fields.Item("idClienteEx").Value, writer)
            createNode("parteRelExp", oRecord.Fields.Item("parteRel").Value, writer)
            createNode("tipoRegi", oRecord.Fields.Item("tipoRegi").Value, writer)
            createNode("paisEfecPagoGen", oRecord.Fields.Item("paisEfecPagoGen").Value, writer)
            createNode("paisEfecExp", oRecord.Fields.Item("paisEfecExp").Value, writer)
            createNode("exportacionDe", oRecord.Fields.Item("exportacionDe").Value, writer)
            createNode("tipoComprobante", oRecord.Fields.Item("tipoComprobante").Value, writer)
            createNode("distAduanero", oRecord.Fields.Item("distAduanero").Value, writer)
            createNode("anio", oRecord.Fields.Item("anio").Value, writer)
            createNode("regimen", oRecord.Fields.Item("regimen").Value, writer)
            createNode("correlativo", oRecord.Fields.Item("correlativo").Value, writer)
            createNode("docTransp", oRecord.Fields.Item("docTransp").Value, writer)
            createNode("fechaEmbarque", oRecord.Fields.Item("fechaEmbarque").Value, writer)
            createNode("valorFOB", oRecord.Fields.Item("valorFOB").Value, writer)
            createNode("valorFOBComprobante", oRecord.Fields.Item("valorFOBComprobante").Value, writer)
            createNode("establecimiento", oRecord.Fields.Item("establecimiento").Value, writer)
            createNode("puntoEmision", oRecord.Fields.Item("puntoEmision").Value, writer)
            createNode("secuencial", oRecord.Fields.Item("secuencial").Value.ToString.PadLeft(8, "0"), writer)
            createNode("autorizacion", oRecord.Fields.Item("autorizacion").Value, writer)
            createNode("fechaEmision", oRecord.Fields.Item("fechaEmision").Value, writer)
        Catch ex As Exception
            SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try
       
    End Sub

End Class
