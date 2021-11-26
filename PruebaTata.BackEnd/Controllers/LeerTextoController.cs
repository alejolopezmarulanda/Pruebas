using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;



namespace PruebaTata.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LeerTextoController : ControllerBase
    {
            
 

        [HttpGet("{Palabras}")]

        public Dictionary<string, int> Get(string Palabras)
        {
                        
            //Limpiar Texto
            string Textolimpio= Palabras.ToLower().Replace(",", "").Replace(".", "").Replace(";", "").Replace("¿", "").Replace("?", "").Replace("“"," ").Replace("º"," ").Replace("º", " ").Replace("”", " ").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
            //Crear un nuevo diccionario para contar 
            Dictionary<string, int> palabraCount = new Dictionary<string, int>();
            //Separamos el texto por espacios, saltos de linea. El StringSplitOptions.RemoveEmptyEntries sirve para que no se guarden entradas vacias en el array
            var arrText = Textolimpio.Split(" ",StringSplitOptions.RemoveEmptyEntries);

            //Metodo para recorrer la lista de tipo Diccionario
            foreach (var palabra in arrText)
            {
                string palabraLower = palabra.ToLower();
                //Comprobamos si la palabra ya existe en el diccionario.
                if (palabraCount.ContainsKey(palabraLower))
                {
                    //Si existe le sumamos una repetición más.
                    palabraCount[palabraLower] = palabraCount[palabraLower] + 1;
                }
                else
                {
                    //Si no, la agregamos al diccionario con valor 1. 
                    palabraCount.Add(palabraLower, 1);
                }
            }
            //Ordenamos el diccionario de mayor a menor
            var OrdenarDicc = palabraCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
           
            //Retornamos el arreglo ya ordenado de mayor a menor
            return OrdenarDicc;
        }

        [HttpPost]
       public Dictionary<string, int> Post(Diccionarioresultado OrdenarDicc)
        {
            return OrdenarDicc.Texto;
        }



        public class Diccionarioresultado
        {
            public Dictionary<string, int> Texto { get; set; }
        }
    }


}
