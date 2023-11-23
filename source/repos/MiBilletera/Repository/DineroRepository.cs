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
        public bool Save(SalaryCurrencies tipoDinero, decimal amount)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                decimal saldoDinero = this.GetSaldoTotalSalario(tipoDinero.SalaryId.Value);
                decimal saldoPlataforma = this.GetIdTipoBySalario(tipoDinero.SalaryId.Value);
                amount += saldoPlataforma;
                
                bool seGuardo = false;
                if(tipoDinero != null && amount <= saldoDinero)
                {
                    db.SalaryCurrencies.AddOrUpdate(tipoDinero);
                    db.SaveChanges();
                    seGuardo = true;
                }

                return seGuardo;
            }
        }

        public decimal GetSaldoTotalSalario(int idSalario)
        {
            using(BilleteraEntities db = new BilleteraEntities())
            {
                return db.Salaries.Where(s => s.SalaryID == idSalario).FirstOrDefault().Amount.Value;
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
                return db.SalaryCurrencies.Include("CurrencyTypes").Where(s => s.Id == salaryCurrenciesId).FirstOrDefault();
            }
        }

        public decimal GetIdTipoBySalario(int idSalario)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                return db.SalaryCurrencies.Where(s => s.SalaryId == idSalario).ToList().Sum(s => s.TotalMoney.Value);
            }
        }

        public SalaryCurrencies GetTipoDineroByIdTipo(int idTipoDinero, int idSalario)
        {
            using(BilleteraEntities db = new BilleteraEntities())
            {
                return db.SalaryCurrencies.Where(s => s.CurrencyId == idTipoDinero && s.SalaryId == idSalario).FirstOrDefault();
            }
        }

        public SalaryCurrencies GetByIdAndSalario(int idTipoDineroAAumentar, int? salaryId)
        {
            using(BilleteraEntities db = new BilleteraEntities())
            {
                return db.SalaryCurrencies.Where(s => s.CurrencyId == idTipoDineroAAumentar && s.SalaryId ==  salaryId).FirstOrDefault();
            }
        }
    }
}
