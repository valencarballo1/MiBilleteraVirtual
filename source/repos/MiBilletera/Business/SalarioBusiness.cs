using Data;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Repository.SalarioRepository;

namespace Business
{
    public class SalarioBusiness
    {
        private SalarioRepository _SalarioRepository;

        public SalarioBusiness()
        {
            this._SalarioRepository = new SalarioRepository();
        }

        public salarioDTO Get(string periodo)
        {
            return _SalarioRepository.Get(periodo);
        }

        public decimal GetSueldoTotal(int idSalario)
        {
            return _SalarioRepository.GetSueldoTotal(idSalario);
        }

        public void Grabar(Salaries salario)
        {
            string modifiedPeriod = salario.Period.Replace("-", "");
            salarioDTO salarioExistente = _SalarioRepository.Get(modifiedPeriod);

            if (salarioExistente == null)
            {
                salario.Period = modifiedPeriod;
                _SalarioRepository.Grabar(salario);
            }
            else
            {
                throw new Exception("Este periodo contiene un salario!");
            }
        }
    }
}
