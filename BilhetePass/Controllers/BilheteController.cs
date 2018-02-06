using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using BilhetePass.Services;
using BilhetePass.Models;

namespace BilhetePass.Controllers
{
    [Route("api/[controller]")]
    public class BilheteController : Controller
    {

        private bilheteClass _bilheteClass { get; set; }

        public BilheteController()
        {
            _bilheteClass = new bilheteClass();
        }

        [HttpPut]
        public IActionResult Put([FromBody]requestBilheteModel param)
        {
            var bilhete = _bilheteClass.obtemBilhete();

            var retorno = _bilheteClass.recarregarBilhete(bilhete, param.valor);

            return Ok(retorno);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bilhete = _bilheteClass.obtemBilhete();

            var retorno = _bilheteClass.efetuaCobranca(bilhete);            

            return Ok(retorno);
        }

    }
}
