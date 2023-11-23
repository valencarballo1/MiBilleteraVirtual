using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Repository;
using static Repository.DTO.DTOs;

namespace Business
{
    public class GastoBusiness
    {
        private GastoRepository _GastoRepository;
        private DineroRepository _DineroRepository;
        private SalarioRepository _SalarioRepository;

        public GastoBusiness()
        {
            this._GastoRepository = new GastoRepository();
            this._DineroRepository = new DineroRepository();
            this._SalarioRepository = new SalarioRepository();
        }
        public List<Expenses> GetAll()
        {
            return _GastoRepository.GetAll();
        }

        public Expenses Get(int id)
        {
            return _GastoRepository.Get(id);
        }

        public bool Grabar(Expenses gasto)
        {
            try
            {
                bool grabo = false;
                Expenses gastoAEditar = _GastoRepository.Get(gasto.ExpenseID);
                decimal totalDineroGastos = _GastoRepository.GetTotalGastoBySalario(gasto.SalaryID.Value);
                SalaryCurrencies tipoDinero = _DineroRepository.GetTipoDineroByIdTipo(gasto.SalaryCurrenciesId.Value, gasto.SalaryID.Value);
                Salaries salario = _SalarioRepository.GetById(tipoDinero.SalaryId.Value);
                decimal totalGastoACargar = gasto.Amount.Value + totalDineroGastos;


                if (gastoAEditar != null && (gastoAEditar.Amount <= tipoDinero.TotalMoney && totalGastoACargar <= salario.Amount.Value))
                {
                    gastoAEditar.Description = gasto.Description;
                    gastoAEditar.Amount = gasto.Amount;
                    gastoAEditar.ExpenseDate = DateTime.Now;
                    gastoAEditar.ExpenseTypeID = gasto.ExpenseTypeID;
                    _GastoRepository.Grabar(gastoAEditar);
                    grabo = true;
                }
                else
                {
                    if (gasto.Amount <= tipoDinero.TotalMoney && totalGastoACargar <= salario.Amount.Value)
                    {
                        gasto.ExpenseDate = DateTime.Now;
                        gasto.IsActive = true;
                        _GastoRepository.Grabar(gasto);
                        tipoDinero.TotalMoney = tipoDinero.TotalMoney - gasto.Amount;
                        grabo = _DineroRepository.Save(tipoDinero, gasto.Amount.Value);
                    }
                }
                return grabo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GrabarTipoGasto(ExpenseTypes tipoGasto)
        {
            if (tipoGasto.ExpenseTypeID == 0)
            {
                _GastoRepository.GrabarTipoGasto(tipoGasto);
            }
            else
            {
                ExpenseTypes gastoAEditar = _GastoRepository.GetTipoGasto(tipoGasto.ExpenseTypeID);
                gastoAEditar.ExpenseTypeName = tipoGasto.ExpenseTypeName;
                gastoAEditar.IsActive = true;
                _GastoRepository.GrabarTipoGasto(tipoGasto);
            }
        }

        public List<ExpenseTypeDTO> GetAllTipoGasto()
        {
            return _GastoRepository.GetAllTipoGasto();
        }

        public List<ExpenseGroupDTO> GetExpenseTotalsByExpenseType(int idSalario)
        {
            return _GastoRepository.GetExpenseTotalsByExpenseType(idSalario);
        }

        public List<ExpenseTypeDTO> GetTipoGastoConGastos(int idSalario)
        {
            return _GastoRepository.GetTipoGastoConGastos(idSalario);
        }

        public List<ExpenseDTO> GetGastoByID(int idSalario)
        {
            return _GastoRepository.GetGastoByID(idSalario);
        }
    }
}
