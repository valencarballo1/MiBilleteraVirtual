//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class SalaryCurrencies
    {
        public int Id { get; set; }
        public Nullable<int> SalaryId { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<decimal> TotalMoney { get; set; }
    
        public virtual CurrencyTypes CurrencyTypes { get; set; }
        public virtual Salaries Salaries { get; set; }
    }
}
