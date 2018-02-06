using System;
using System.Collections.Generic;
using System.Linq;

using BilhetePass.Struct;

namespace BilhetePass.Models
{
    public class bilheteModel
    {
        public List<recargaModel> recargas { get; set; }
        public decimal saldo { get; set; }
    }

    public class recargaModel
    {
        public decimal valor { get; set; }
        public DateTime data { get; set; }
        public decimal valorPassagem { get; set; }
    }

    public class requestBilheteModel
    {
        public decimal valor { get; set;}
    }

    public class catracaModel
    {
        public decimal saldoRestante { get; set; }
        public decimal valorCobrado { get; set; }
        public string display { get; set; }
    }
}