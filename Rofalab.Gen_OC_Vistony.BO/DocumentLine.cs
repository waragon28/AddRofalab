using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistony.Distribucion.BO
{
    public class DocumentLine
    {
        public string AcctCode { get; set; }
        public string COGSAccountCode { get; set; }
        public string CostingCode { get; set; }
        public string CostingCode2 { get; set; }
        public string CostingCode3 { get; set; }
        public string DiscountPercent { get; set; }
        public string Dscription { get; set; }
        public string ItemCode { get; set; }
        public double LineTotal { get; set; }
        public double Price { get; set; }
        public string Quantity { get; set; }
        public string TaxCode { get; set; }
        public string TaxOnly { get; set; }
        public string U_SYP_FECAT07 { get; set; }
        public string U_VIST_CTAINGDCTO { get; set; }
        public string U_VIS_CommentText { get; set; }
        public string U_VIS_PromID { get; set; }
        public string U_VIS_PromLineID { get; set; }
        public string WarehouseCode { get; set; }

    }
}
