using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.DTO.DTOs;

namespace Repository
{
    public class DineroRepository
    {
        public bool Save(SalaryCurrencies tipoDinero)
        {
            using(BilleteraEntities db = new BilleteraEntities())
            {
                bool seGuardo = false;
                if(tipoDinero != null)
                {
                    db.SalaryCurrencies.AddOrUpdate(tipoDinero);
                    db.SaveChanges();
                    seGuardo = true;
                }

                return seGuardo;
            }
        }

        public List<TipoDineroDTO> Load()
        {
            using(BilleteraEntities db = new BilleteraEntities())
            {
                List<TipoDineroDTO> tipoDinero = db.SalaryCurrencies
                    .Include("CurrencyTypes")
                    .Select(t => new TipoDineroDTO
                    {
                        Id = t.SalaryId.Value,
                        CurrencyId = t.CurrencyId.Value,
                        TotalMoney = t.TotalMoney.Value,
                        CurrencyName = t.CurrencyTypes.CurrencyName
                    })
                    .ToList();

                return tipoDinero;
            }
        }

        public List<TipoDineroDTO> LoadById(int idSalario)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                List<TipoDineroDTO> tipoDinero = db.SalaryCurrencies
                    .Include("CurrencyTypes")
                    .Where(t => t.SalaryId == idSalario)
                    .Select(t => new TipoDineroDTO
                    {
                        Id = t.SalaryId.Value,
                        CurrencyId = t.CurrencyId.Value,
                        TotalMoney = t.TotalMoney.Value,
                        CurrencyName = t.CurrencyTypes.CurrencyName
                    })
                    .ToList();

                return tipoDinero;
            }
        }

        public List<TipoPlataformaDTO> GetAllTipos()
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                List<TipoPlataformaDTO> tipoPlataforma = db.CurrencyTypes
                    .Select(t => new TipoPlataformaDTO
                    {
                        id = t.CurrencyID,
                        name = t.CurrencyName
                    })
                    .ToList();

                return tipoPlataforma;
            }
        }

        public SalaryCurrencies GetIdTipo(int? salaryCurrenciesId)
        {
            using(BilleteraEntities db = new BilleteraEntities())
            {
                return db.SalaryCurrencies.Include("CurrencyTypes").Where(s => s.CurrencyId == salaryCurrenciesId).FirstOrDefault();
            }
        }
    }
}
