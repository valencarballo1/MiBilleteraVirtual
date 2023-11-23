using Data;
using Repository;
using System;
using System.Collections.Generic;
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
                return _DineroRepository.Save(tipoDineroExistente, tipoDinero.TotalMoney.Value);
            }
            else
            {

                return _DineroRepository.Save(tipoDinero, tipoDinero.TotalMoney.Value);
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

        public bool Transferir(int idTipoDineroADescontar, int idTipoDineroAAumentar, decimal montoATransferir)
        {
            bool primerTransfe = false;
            bool segundaTransfe = false;
            bool transferido = false;
            SalaryCurrencies tipoDineroDescontar = _DineroRepository.GetIdTipo(idTipoDineroADescontar);
            SalaryCurrencies tipoDineroAumentar = _DineroRepository.GetByIdAndSalario(idTipoDineroAAumentar, tipoDineroDescontar.SalaryId);


            tipoDineroDescontar.TotalMoney -= montoATransferir;
            primerTransfe = _DineroRepository.Save(tipoDineroDescontar, 0);

            if (primerTransfe)
            {
                tipoDineroAumentar.TotalMoney += montoATransferir;
                segundaTransfe = _DineroRepository.Save(tipoDineroAumentar, montoATransferir);
            }

            if (primerTransfe == true && segundaTransfe == true)
            {
                transferido = true;
                return transferido;
            }
            else
            {
                return transferido;
            }


        }

        public SalaryCurrencies GetTipoDineroByIdTipo(int idTipoDinero, int idSalario)
        {
            return _DineroRepository.GetTipoDineroByIdTipo(idTipoDinero, idSalario);
        }
    }
}
