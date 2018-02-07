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
            var dataCobranca = DateTime.Now; //new DateTime(2018, 01, 30);

            var recargas = bilhete.recargas.Where(x => x.data <= dataCobranca).OrderBy(x => x.data).ToList();

            int indx = 1;
            decimal valorPassagem = 0;

            foreach (var recarga in recargas)
            {
                if (recarga.valor >= recarga.valorPassagem || indx == recargas.Count())
                {
                    valorPassagem = recarga.valorPassagem;
                }

                if (bilhete.saldo >=  valorPassagem)
                {
                    bilhete.saldo -= recarga.valorPassagem;   

                    bilhete.recargas.Remove(recarga);  
                    recarga.valor -= recarga.valorPassagem;
                    bilhete.recargas.Add(recarga);

                    gravaBilhete(bilhete);

                    return new catracaModel()
                    {
                        display = bilheteStruct.liberadoMsg,
                        saldoRestante = bilhete.saldo,
                        valorCobrado = recarga.valorPassagem
                    };
                }
                
                bilhete.recargas.Remove(recarga);
                gravaBilhete(bilhete);

                indx += 1;                                
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