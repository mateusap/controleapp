using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleApp.Class
{
    internal class Empresas
    {
        public class EmpNome
        {
            public int cod { get; set; }
            public string nome { get; set; }
            public string uf { get; set; }
            public string mes { get; set; }
        }
        public class EmpDados
        {
            public int cod { get; set; }
            public string regime { get; set; }
            public string uf { get; set; }
            public string cidade { get; set; }
            public string ie { get; set; }
            public string cnpj { get; set; }
        }
    }
}

