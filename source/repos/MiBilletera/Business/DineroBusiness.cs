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

        public DineroBusiness()
        {
            this._DineroRepository = new DineroRepository();
        }

        public bool Save(SalaryCurrencies tipoDinero)
        {
            return _DineroRepository.Save(tipoDinero);
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
