using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtiAnimeManager.Models
{
    internal class Anime
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public string DataDeLancamento{get;set;}
        public double? Nota { get; set; }

    }
}
