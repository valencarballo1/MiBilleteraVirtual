using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.DTO.DTOs;

namespace Business
{
    public class DineroBusiness
    {
        private DineroRepository _DineroRepository;
        private SalarioRepository _SalarioRepository;

        public DineroBusiness()
        {
            this._DineroRepository = new DineroRepository();
            this._SalarioRepository = new SalarioRepository();
        }

        public bool Save(SalaryCurrencies tipoDinero)
        {
            Salaries salario = _SalarioRepository.GetById(tipoDinero.SalaryId.Value);
            SalaryCurrencies tipoDineroExistente = _SalarioRepository.GetTipoDineroCargado(tipoDinero.CurrencyId, tipoDinero.SalaryId);
            if (tipoDineroExistente != null)
            {
                tipoDineroExistente.TotalMoney += tipoDinero.TotalMoney;
                return _DineroRepository.Save(tipoDineroExistente, salario.Amount.Value);
            }
            else
            {

                return _DineroRepository.Save(tipoDinero, salario.Amount.Value);
            }

        }

        public List<TipoDineroDTO> Load()
        {
            return _DineroRepository.Load();
        }

        public List<TipoDineroDTO> LoadById(int idSalario)
        {
            return _DineroRepository.LoadById(idSalario);
        }

        public List<TipoPlataformaDTO> GetAllTipos()
        {
            return _DineroRepository.GetAllTipos();
        }
    }
}
