using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class DTOs
    {
        public class ExpenseTypeDTO
        {
            public int ExpenseTypeID { get; set; }
            public string ExpenseTypeName { get; set; }
        }

        public class ExpenseDTO
        {
            public int Id { get; set; }
            public int ExpenseTypeId { get; set; }
            public string ExpenseTypeName { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
            // Otras propiedades
        }

        public class ExpenseGroupDTO
        {
            public int ExpenseTypeId { get; set; }
            public decimal TotalAmount { get; set; }
        }

        public class TipoDineroDTO
        {
            public int Id { get; set; }
            public int CurrencyId { get; set; }
            public decimal TotalMoney { get; set; }
            public string CurrencyName { get; set; }
        }

        public class TipoPlataformaDTO
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }
}
