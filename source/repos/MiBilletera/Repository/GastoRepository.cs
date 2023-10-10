using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.DTO.DTOs;

namespace Repository
{
    public class GastoRepository
    {
        public Expenses Get(int id)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                return db.Expenses.Include("ExpenseTypes").Where(g => g.ExpenseID == id).FirstOrDefault();
            }
        }

        public List<Expenses> GetAll()
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                return db.Expenses.Include("ExpenseTypes").ToList();
            }
        }

        public List<ExpenseTypeDTO> GetAllTipoGasto()
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                var expenseTypes = db.ExpenseTypes.ToList();

                // Mapea los ExpenseTypes a ExpenseTypeDTO
                var expenseTypeDTOs = expenseTypes.Select(et => new ExpenseTypeDTO
                {
                    ExpenseTypeID = et.ExpenseTypeID,
                    ExpenseTypeName = et.ExpenseTypeName
                }).ToList();

                return expenseTypeDTOs;
            }
        }

        public List<ExpenseTypeDTO> GetTipoGastoConGastos(int idSalario)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                var expenseTypes = db.ExpenseTypes.Include("Expenses").Where(t => t.Expenses.Count > 0 && t.IsActive == true && t.Expenses.Where(s => s.SalaryID == idSalario).FirstOrDefault().SalaryID == idSalario).ToList();

                // Mapea los ExpenseTypes a ExpenseTypeDTO
                var expenseTypeDTOs = expenseTypes.Select(et => new ExpenseTypeDTO
                {
                    ExpenseTypeID = et.ExpenseTypeID,
                    ExpenseTypeName = et.ExpenseTypeName
                }).ToList();

                return expenseTypeDTOs;
            }
        }

        public ExpenseTypes GetTipoGasto(int expenseTypeID)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                return db.ExpenseTypes.Where(t => t.ExpenseTypeID == expenseTypeID).FirstOrDefault();
            }
        }

        public void Grabar(Expenses gasto)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                db.Expenses.AddOrUpdate(gasto);
                db.SaveChanges();
            }
        }

        public void GrabarTipoGasto(ExpenseTypes tipoGasto)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                db.ExpenseTypes.AddOrUpdate(tipoGasto);
                db.SaveChanges();
            }
        }

        public List<ExpenseGroupDTO> GetExpenseTotalsByExpenseType(int idSalario)
        {
            using (BilleteraEntities db = new BilleteraEntities())
            {
                List<ExpenseGroupDTO> expenseGroups = db.Expenses
                    .Where(g => g.SalaryID == idSalario)
                    .GroupBy(ExpenseDTO => ExpenseDTO.ExpenseTypeID)
                    .Select(group => new ExpenseGroupDTO
                    {
                        ExpenseTypeId = group.Key.Value,
                        TotalAmount = group.Sum(expense => expense.Amount.Value)
                    })
                    .ToList();

                return expenseGroups;

            }
        }

        public List<ExpenseDTO> GetGastoByID(int idSalario)
        {
            using(BilleteraEntities db = new BilleteraEntities())
            {
                List<ExpenseDTO> lista = db.Expenses.Include("ExpenseTypes").Where(g => g.SalaryID == idSalario).Select(g => new ExpenseDTO
                {
                    Id = g.ExpenseID,
                    ExpenseTypeName = g.ExpenseTypes.ExpenseTypeName,
                    ExpenseTypeId = g.ExpenseTypeID.Value,
                    Amount = g.Amount.Value,
                    Description = g.Description
                }).ToList();

                return lista;
            }
        }

        


    }
}
