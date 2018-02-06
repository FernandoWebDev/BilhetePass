using System;
using System.Collections.Generic;

namespace BilhetePass.Struct
{
    public struct bilheteStruct
    {
        public static Dictionary<DateTime, decimal> precoPassagem = new Dictionary<DateTime, decimal>()
        {
            { new DateTime(2018, 02, 01), 4.00M },
            { new DateTime(2016, 02, 01), 3.80M },
        };

        public static string liberadoMsg = "Passagem Liberada!";
        public static string saldoInsufMsg = "Saldo Insuficiente";
    }
}