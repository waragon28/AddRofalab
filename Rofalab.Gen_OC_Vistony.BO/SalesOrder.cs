using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistony.Distribucion.BO
{
    public class SalesOrder
    {
        public string ApCredit { get; set; }
        public string ApDues { get; set; }
        public string ApPrcnt { get; set; }
        public string ApTPag { get; set; }
        public string AppVersion { get; set; }
        public string Branch { get; set; }
        public string Brand { get; set; }
        public string CardCode { get; set; }
        public string Comments { get; set; }
        public string DocCurrency { get; set; }
        public string DocDate { get; set; }
        public string DocDueDate { get; set; }
        public string DocRate { get; set; }
        public string DocType { get; set; }
        public List<DocumentLine> DocumentLines { get; set; }
        public string DocumentsOwner { get; set; }
        public string FederalTaxID { get; set; }
        public string Intent { get; set; }
        public string Model { get; set; }
        public string OSVersion { get; set; }
        public string PayToCode { get; set; }
        public string PaymentGroupCode { get; set; }
        public string SalesPersonCode { get; set; }
        public string ShipToCode { get; set; }
        public string TaxDate { get; set; }
        public string U_SYP_DOCEXPORT { get; set; }
        public string U_SYP_FEEST { get; set; }
        public string U_SYP_FEMEX { get; set; }
        public string U_SYP_FETO { get; set; }
        public string U_SYP_MDMT { get; set; }
        public string U_SYP_TVENTA { get; set; }
        public string U_SYP_VIST_TG { get; set; }
        public string U_VIS_AgencyCode { get; set; }
        public string U_VIS_AgencyDir { get; set; }
        public string U_VIS_AgencyName { get; set; }
        public string U_VIS_AgencyRUC { get; set; }
        public string U_VIS_SalesOrderID { get; set; }
        public string quotation { get; set; }
    }
}
