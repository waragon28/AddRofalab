using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using Forxap.Framework.Extensions;
using Vistony.Distribucion.Constans;
using Vistony.Distribucion.BLL;

namespace Vistony.Distribucion.Win.Formulario
{
    [FormAttribute("Vistony.Distribucion.Win.Formulario.FrmSinStockVistony", "Formulario/FrmSinStockVistony.b1f")]
    class FrmSinStockVistony : UserFormBase
    {
        EntregaBLL EntregaBLL = new EntregaBLL();
        public FrmSinStockVistony(string Key)
        {
            SAPbouiCOM.DataTable oDT = oForm.GetDataTable("DT_0");
            EntregaBLL.ObtenerOVSinStockVistony(ref oDT, AddonMessageInfo.QueryObtenerOVConsolidadoSinStockVistony, Key);
            Grid0.ReadOnlyColumns();

        }
        private SAPbouiCOM.Form oForm;

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Grid Grid0;

        private void OnCustomInitialize()
        {
            oForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.Item(this.UIAPIRawForm.UniqueID);
            oForm.ScreenCenter();
        }

    }
}
