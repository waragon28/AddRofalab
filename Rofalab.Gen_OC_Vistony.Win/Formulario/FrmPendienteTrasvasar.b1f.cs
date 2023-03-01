using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using Forxap.Framework.Extensions;
using SAPbouiCOM;
using Vistony.Distribucion.BLL;
using Vistony.Distribucion.Constans;

namespace Vistony.Distribucion.Win.Formulario
{
    [FormAttribute("Vistony.Distribucion.Win.Formulario.FrmPendienteTrasvasar", "Formulario/FrmPendienteTrasvasar.b1f")]
    class FrmPendienteTrasvasar : UserFormBase
    {
        public FrmPendienteTrasvasar(string Key)
        {
            SAPbouiCOM.DataTable oDT = oForm.GetDataTable("DT_0");
            EntregaBLL.ObtenerOVPedTrasvase(ref oDT, AddonMessageInfo.QueryObtenerOVConsolidadoPendTrasvase,Key);
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
        EntregaBLL EntregaBLL = new EntregaBLL();
        private void OnCustomInitialize()
        {
            oForm = SAPbouiCOM.Framework.Application.SBO_Application.Forms.Item(this.UIAPIRawForm.UniqueID);
            oForm.ScreenCenter();

        }

    }
}
