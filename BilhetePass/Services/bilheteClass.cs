using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using BilhetePass.Models;
using BilhetePass.Struct;
using Newtonsoft.Json;

namespace BilhetePass.Services
{    
    public class bilheteClass
    {
        public bilheteModel obtemBilhete()
        {
            string valor = System.IO.File.ReadAllText(@"bilhete.txt");
            
            return JsonConvert.DeserializeObject<bilheteModel>(valor);
        }
        public decimal recarregarBilhete(bilheteModel bilhete, decimal valorRecarga)
        {
            bilhete.recargas.Add(novaRecarga(valorRecarga));

            bilhete.saldo += valorRecarga;

            gravaBilhete(bilhete);

            return bilhete.saldo;
        }

        private void gravaBilhete(bilheteModel bilhete)
        {
            new writeDocClass().writeInDoc("bilhete.txt", JsonConvert.SerializeObject(bilhete));
        }

        private recargaModel novaRecarga(decimal valorRecarga)
        {
            return new recargaModel()
            {
                data = DateTime.Now,
                valor = valorRecarga,
                valorPassagem = bilheteStruct.precoPassagem.FirstOrDefault().Value
            };
        }

        public catracaModel efetuaCobranca(bilheteModel bilhete)
        {
            var dataCobranca = new DateTime(2018, 01, 30); //DateTime.Now;

            var recargas = bilhete.recargas.Where(x => x.data <= dataCobranca).OrderByDescending(x => x.data).ToList();

            foreach (var recarga in recargas)
            {
                if (bilhete.saldo >= recarga.valorPassagem)
                {
                    bilhete.saldo -= recarga.valorPassagem;     

                    gravaBilhete(bilhete);

                    return new catracaModel()
                    {
                        display = bilheteStruct.liberadoMsg,
                        saldoRestante = bilhete.saldo,
                        valorCobrado = recarga.valorPassagem
                    };
                }
            }         

            return new catracaModel()   
            {
                display = bilheteStruct.saldoInsufMsg,
                saldoRestante = bilhete.saldo,
                valorCobrado = 0,
            };

        }
    }
}