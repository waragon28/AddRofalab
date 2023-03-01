using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using Forxap.Framework.Extensions;
using Vistony.Distribucion.Constans;
using Vistony.Distribucion.BLL;
using Forxap.Framework.UI;
using Vistony.Distribucion.BO;
using Vistony.Distribucion.DAL;
using Newtonsoft.Json;
using RestSharp;

namespace Vistony.Distribucion.Win.Formulario
{
    [FormAttribute("Rofalab", "Formulario/WzAsistente.b1f")]
    class WzAsistente : UserFormBase
    {
        public WzAsistente()
        {

        }

        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.Button Button0;
        string UserName = Sb1Globals.UserName;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.Grid Grid1;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.LinkedButton LinkedButton0;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.Button Button2;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.StaticText StaticText6;

        public int PaneLevel { get; set; }
        public int PaneMax = 4;
        public string key = "";
        private SAPbouiCOM.Form oForm;
        EntregaBLL tr = new EntregaBLL();
        JSON obj = new JSON();
        public void PriorPane()
        {
            if (Button1.Item.Enabled==true)
            {
                Button0.Caption = "Siguiente >";
                if (oForm.PaneLevel + 1 >= 2)
                {
                    oForm.PaneLevel -= 1;
                }

                if (oForm.PaneLevel == 2)
                {
                }
                if (oForm.PaneLevel == 1)
                {
                    Button1.Caption = "";
                    Button1.Item.Enabled = false;
                }
            }
            
            StaticText0.Caption = oForm.PaneLevel + " de " + PaneMax;
        }
        public void NextPane()
        {
            Button1.Caption = "< Anterior";
            Button1.Item.Enabled = true;
            if (PaneLevel < PaneMax)
            {
                oForm.PaneLevel += 1;
            }

            if (oForm.PaneLevel == 1)
             {
               
            }
             if (oForm.PaneLevel == 2)
             {
                 
             }
             if (oForm.PaneLevel == 3)
             {
                Grid1.DataTable.Clear();
                key = "";
                Sb1Messages.ShowWarning("Iniciando Consolidado");
                oForm.Freeze(true);
                try
                {
                    for (int i = 0; i < Grid0.Rows.Count; i++)
                    {
                        if (Grid0.DataTable.GetValue(0, i).ToString() == "Y")
                        {
                            key += "" + Grid0.DataTable.GetValue("DocEntry", i).ToString() + "" + Grid0.DataTable.GetValue("LineNum", i).ToString() + "" + Grid0.DataTable.GetValue("ItemCode", i).ToString() + ",";
                        }
                    }
                    SAPbouiCOM.DataTable oDT = oForm.GetDataTable("DT_1");

                    SAPbouiCOM.DataTable PendTYrasv= oForm.GetDataTable("DT_2");
                    SAPbouiCOM.DataTable SinStockVistony = oForm.GetDataTable("DT_3");

                    using (EntregaBLL EntregaBLL = new EntregaBLL())
                    {
                        EntregaBLL.ObtenerOVConsolidados(ref oDT, AddonMessageInfo.QueryObtenerOVConsolidado, key);
                    }

                    using (EntregaBLL EntregaBLL = new EntregaBLL())
                    {
                        EntregaBLL.ObtenerOVPedTrasvase(ref PendTYrasv, AddonMessageInfo.QueryObtenerOVConsolidadoPendTrasvase, key);
                    }

                    using (EntregaBLL EntregaBLL = new EntregaBLL())
                    {
                        EntregaBLL.ObtenerOVSinStockVistony(ref SinStockVistony, AddonMessageInfo.QueryObtenerOVConsolidadoSinStockVistony, key);
                    }

                    StaticText10.Caption = "Sin Equivalencia : " + PendTYrasv.Rows.Count;
                    StaticText11.Caption = "Sin Stock Vistony : " + SinStockVistony.Rows.Count;


                    Grid1.Columns.Item(0).LinkedObjectType(Grid1, "Cod Rofalab", "4");

                     Grid1.ReadOnlyColumns();
                     Grid1.AutoResizeColumns();
                }
                catch (Exception)
                {

                }
                finally
                {
                    oForm.Freeze(false);
                    Sb1Messages.ShowSuccess("Consolidado Culminado");

                }


            }

            if (oForm.PaneLevel == 4)
             {
                oForm.PaneLevel = 3;
                var Message = Sb1Messages.ShowMessageBox("Se generara una orden de venta en la compañia Vistony "+"\n" + "¿Desea Continuar?");

                if (Message == 2 || Message == 3)
                {
                    oForm.PaneLevel = 3;
                    Button1.Caption = "< Anterior";
                    Button0.Caption = "Siguiente >";
                    Button1.Item.Enabled = true;
                }
                else
                {
                    Sb1Messages.ShowWarning("Iniciando Envio de O.V a Vistony");
                    //RestClient client = new RestClient("https://salesforce.vistony.pe/post.qa/api/SalesOrder");
                    RestClient client = new RestClient("https://salesforce.vistony.pe/post/api/SalesOrder");
                    RestRequest request = new RestRequest(Method.POST);
                    obj = tr.JSON_OV_VISTONY(Grid1);
                    string JsonObtenerCabezera = JsonConvert.SerializeObject(obj);
                    string dataReq = JsonObtenerCabezera;
                    dynamic result = client.Execute(request.AddJsonBody(dataReq));
                    //dynamic resultad = JsonConvert.DeserializeObject(result.Content.ToString());
                    
                    //string ErrorCode = "0";
                    if (result.ResponseStatus == RestSharp.ResponseStatus.Completed)//rEESPUESTA DE PETICION
                    {

                        dynamic m = JsonConvert.DeserializeObject(result.Content.ToString());

                        var dinamicObj = JsonConvert.DeserializeObject<dynamic>(result.Content.ToString());

                        string DocNum = m["SalesOrders"][0]["DocNum"];
                        string MensajeError = m["SalesOrders"][0]["Message"];

                        if (DocNum != null)
                        {
                           Sb1Messages.ShowSuccess("Se creo correctamente en Vistony : "+ DocNum);
                            oForm.PaneLevel = 4;

                            StaticText8.Caption = "Se genero Correctamente la O.V. en la compañia Vistony "+ DocNum;

                            StaticText8.SetBold();
                            StaticText8.SetSize(20);
                            StaticText8.Item.Height = 50;
                       }
                        else
                        {
                            oForm.PaneLevel = 3;
                            Button0.Caption = "Siguiente >";
                            Button0.Item.Enabled = true;
                            Sb1Messages.ShowError("Error al insertar  la O.V en  Vistony : "+ MensajeError);
                       }
                       
                    }
                    else
                    {
                        oForm.PaneLevel = 3;
                        //Sb1Messages.ShowError(m["error"]["message"]["value"].ToString());
                    }

                }

                Button0.Caption = "Terminar";
                Button1.Caption = "";
                Button1.Item.Enabled= false;
            }

            StaticText0.Caption = oForm.PaneLevel + " de " + PaneMax;
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button0_ClickBefore);
            this.Button0.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button0_ClickAfter);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button1.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button1_ClickAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_5").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_8").Specific));
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_9").Specific));
            this.Grid0.LinkPressedBefore += new SAPbouiCOM._IGridEvents_LinkPressedBeforeEventHandler(this.Grid0_LinkPressedBefore);
            this.Grid0.LinkPressedAfter += new SAPbouiCOM._IGridEvents_LinkPressedAfterEventHandler(this.Grid0_LinkPressedAfter);
            this.Grid0.ClickBefore += new SAPbouiCOM._IGridEvents_ClickBeforeEventHandler(this.Grid0_ClickBefore);
            this.Grid0.ClickAfter += new SAPbouiCOM._IGridEvents_ClickAfterEventHandler(this.Grid0_ClickAfter);
            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_10").Specific));
            this.Button2.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button2_ClickAfter);
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_11").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_14").Specific));
            this.Grid1 = ((SAPbouiCOM.Grid)(this.GetItem("Item_15").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_16").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_17").Specific));
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_18").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_19").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_20").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_21").Specific));
            this.Button3 = ((SAPbouiCOM.Button)(this.GetItem("Item_22").Specific));
            this.Button3.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button3_ClickAfter);
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_23").Specific));
            this.Button4 = ((SAPbouiCOM.Button)(this.GetItem("Item_24").Specific));
            this.Button4.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button4_ClickAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }
        private void OnCustomInitialize()
        {
            oForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.Item(this.UIAPIRawForm.UniqueID);
            oForm.ScreenCenter();
            EditText3.SetInt(0);
            StaticText1.SetBold();
            StaticText1.SetSize(13);
            StaticText1.Item.Height = 15;
            Button1.Item.Enabled = false;
            Grid0.AutoResizeColumns();

        }

        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (Button0.Caption=="Terminar")
            {
                oForm.Close();
            }
            NextPane();
        }
        private void Button2_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Sb1Messages.ShowWarning("Iniciando consulta");
            oForm.Freeze(true);
            OV();
            oForm.Freeze(false);
            Sb1Messages.ShowSuccess("Fin de consulta");

        }
        private void OV()
        {
            SAPbouiCOM.DataTable oDT = oForm.GetDataTable("DT_0");
            string startDate = EditText0.Value;
            string endDate = EditText1.Value;
            using (EntregaBLL EntregaBLL = new EntregaBLL())
            {
                EntregaBLL.ObtenerOV(ref oDT, startDate, endDate, AddonMessageInfo.QueryObtenerOVLiberada, UserName);
            }
         
            SetFormatGrid();
        }
        private void SetFormatGrid()
        {
            Grid0.AssignLineNro();
            Grid0.Columns.Item(0).Type = SAPbouiCOM.BoGridColumnType.gct_CheckBox;
            Grid0.Columns.Item(1).Visible = false;
            Grid0.Columns.Item(6).Visible = false;
            Grid0.Columns.Item(2).LinkedObjectType(Grid0, "DocNum", "17");
            Grid0.Columns.Item(4).LinkedObjectType(Grid0, "CardCode", "2");
            Grid0.Columns.Item(7).LinkedObjectType(Grid0, "ItemCode", "2");

            Grid0.Columns.Item(2).TitleObject.Caption = "Orden de Venta";
            Grid0.Columns.Item(3).TitleObject.Caption = "Fecha del Documento";
            Grid0.Columns.Item(4).TitleObject.Caption = "Cod. Cliente";
            Grid0.Columns.Item(5).TitleObject.Caption = "Nombre Cliente";
            Grid0.Columns.Item(7).TitleObject.Caption = "Cod. Articulo";
            Grid0.Columns.Item(8).TitleObject.Caption = "Descripcion";
            Grid0.Columns.Item(9).TitleObject.Caption = "Cantidad";
            Grid0.Columns.Item(10).TitleObject.Caption = "Stock Actual";

            Grid0.ReadOnlyColumns();
            Grid0.Columns.Item(0).Editable = true;
            Grid0.Sortable();
            Grid0.AutoResizeColumns();
            EditText3.SetInt(Grid0.Rows.Count);
        }
        private void Grid0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            int rowSelected = 0;
            int rowIndex = 0;
            string isCheck = "N";
            try
            {
                if (pVal.ColUID == "Marcar" && pVal.Row == -1)
                {
                    // obtengo los registros seleccionados
                    rowSelected = EditText3.GetInt();

                    // debo marcar o desmarcar todo
                    Utils.CheckRows(oForm, Grid0, ref rowSelected);
                    // asigno el nro de documentos seleccionados
                    EditText3.SetInt(rowSelected);
                } // si hicieron click enun registro valido dentro del Grid
                else if (pVal.ColUID == "Marcar" && pVal.Row > -1)
                {
                    // obtengo los registros seleccionados
                    rowSelected = EditText3.GetInt();
                    rowIndex = pVal.Row;
                    isCheck = Grid0.DataTable.GetString("Marcar", Grid0.GetDataTableRowIndex(rowIndex)).ToString();

                    // si hicieron check
                    if (isCheck == "Y")
                    {
                        rowSelected += 1;
                    }
                    else
                    {
                        // si descheckearon
                        if (rowSelected > 0) // para evitar negativos
                        {
                            rowSelected -= 1;
                        }
                    }
                    // asigno el nro de documentos seleccionados
                    EditText3.SetInt(rowSelected);

                }
            }
            catch (Exception ex)
            {
                Sb1Messages.ShowError(ex.ToString());
            }

        }
        private void Grid0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }
        private void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (oForm.PaneLevel == 2)
            {
                if (EditText3.GetInt() > 0)
                {
                    BubbleEvent = true;
                }
                else
                {
                    BubbleEvent = false;
                    Sb1Messages.ShowError("Seleccione 1 registro como minimo");
                }
            }
            if (oForm.PaneLevel == 4)
            {
                oForm.Close();
            }
           
        }
        private void Button1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            PriorPane();
        }
        private void Grid0_LinkPressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (pVal.ColUID == "DocNum")
            {
                SAPbouiCOM.EditTextColumn col = null;
                col = ((SAPbouiCOM.EditTextColumn)(Grid0.Columns.Item("DocEntry")));
               col.LinkedObjectType = "17";// muestra la flecha amariilla asociada al objeto pedidos  
            }
        }
        private void Grid0_LinkPressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            SAPbouiCOM.EditTextColumn col = null;
            string codigo = string.Empty;
            int rowIndex;
            int rowSelected;

            // verifico en que columna hicieron click  en el linkedbutton
            if (pVal.ColUID == "DocNum")
            {
                //int rowSelected = Grid0.Rows.SelectedRows.Item(0, SAPbouiCOM.BoOrderType.ot_RowOrder);
                rowSelected = pVal.Row;
                rowIndex = rowSelected;


                codigo = Grid0.DataTable.GetValue("DocEntry", Grid0.GetDataTableRowIndex(rowIndex)).ToString();
                EditText4.Value = codigo;
                LinkedButton0.Item.Click(SAPbouiCOM.BoCellClickType.ct_Linked);
            }

        }

        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.StaticText StaticText10;
        private SAPbouiCOM.Button Button3;
        private SAPbouiCOM.StaticText StaticText11;
        private SAPbouiCOM.Button Button4;

        private void Button3_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            FrmPendienteTrasvasar FrmPendienteTrasvasar = new FrmPendienteTrasvasar(key);
            FrmPendienteTrasvasar.Show();
        }

        private void Button4_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            FrmSinStockVistony FrmSinStockVistony = new FrmSinStockVistony(key);
            FrmSinStockVistony.Show();
        }
    }
}
