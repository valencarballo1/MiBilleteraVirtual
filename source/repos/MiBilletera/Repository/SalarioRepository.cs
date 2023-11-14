using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SalarioRepository
    {
        public salarioDTO Get(string periodo)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                var salario = db.Salaries
                    .SingleOrDefault(s => s.Period == periodo);

                if (salario != null)
                {
                    // Mapea los datos a tu clase Salario
                    var salarioClase = new salarioDTO
                    {
                        SalaryId = salario.SalaryID,
                        Amount = salario.Amount.Value,
                        Period = salario.Period,
                        // Mapea otras propiedades
                    };

                    return salarioClase;
                }

                return null;
            }
        }

        public Salaries GetById(int salaryId)
        {
            using(BilleteraEntities db = new BilleteraEntities())
            {
                return db.Salaries.Where(s => s.SalaryID == salaryId).FirstOrDefault();
            }
        }

        public decimal GetSueldoTotal(int idSalario)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                return db.Salaries.Where(s => s.SalaryID == idSalario).FirstOrDefault().Amount.Value;
            }
        }

        public void Grabar(Salaries salario)
        {
            using(BilleteraEntities db = new BilleteraEntities())
            {
                db.Salaries.AddOrUpdate(salario);
                db.SaveChanges();
;            }
        }

        public class salarioDTO
        {
            public int SalaryId { get; set; }
            public decimal Amount { get; set; }
            public string Period { get; set; }
            // Otras propiedades
        }

    }
}
