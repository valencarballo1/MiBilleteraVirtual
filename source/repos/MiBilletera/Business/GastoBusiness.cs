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

        public GastoBusiness()
        {
            this._GastoRepository = new GastoRepository();
        }
        public List<Expenses> GetAll()
        {
            return _GastoRepository.GetAll();
        }

        public Expenses Get(int id)
        {
            return _GastoRepository.Get(id);
        }

        public void Grabar(Expenses gasto)
        {
            try
            {
                Expenses gastoAEditar = _GastoRepository.Get(gasto.ExpenseID);

                if (gastoAEditar != null)
                {
                    gastoAEditar.Description = gasto.Description;
                    gastoAEditar.Amount = gasto.Amount;
                    gastoAEditar.ExpenseDate = DateTime.Now;
                    gastoAEditar.ExpenseTypeID = gasto.ExpenseTypeID;
                    _GastoRepository.Grabar(gastoAEditar);
                }
                else
                {
                    gasto.ExpenseDate = DateTime.Now;
                    gasto.IsActive = true;
                    _GastoRepository.Grabar(gasto);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void GrabarTipoGasto(ExpenseTypes tipoGasto)
        {
            if(tipoGasto.ExpenseTypeID == 0)
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
