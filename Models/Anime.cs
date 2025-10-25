using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtiAnimeManager.Models
{
    public class Anime
    {
        #region Variaveis
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Estudio { get; set; }
        public string Genero { get; set; }
        public DateTime DataDeLancamento { get; set; }
        public double? Nota { get; set; }
        #endregion

        #region construtor
        public Anime(int id, string nome, string autor, string estudio, string genero,DateTime data, double? nota)
        {
            this.Id = id;
            this.Nome = nome;
            this.Autor = autor;
            this.Estudio = estudio;
            this.Genero = genero;
            this.DataDeLancamento = data;
            this.Nota = nota;
        }
        #endregion

        #region Herança
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Id:");
            sb.AppendLine($"\t{Id}");
            sb.AppendLine($"Nome:");
            sb.AppendLine($"\t{Nome}");
            sb.AppendLine($"Autor:");
            sb.AppendLine($"\t{Autor}");
            sb.AppendLine($"Estudio:");
            sb.AppendLine($"\t{Estudio}");
            sb.AppendLine($"Genero:");
            sb.AppendLine($"\t{Genero}");
            sb.AppendLine($"Data De Lancamento:");
            sb.AppendLine($"\t{DataDeLancamento}");
            sb.AppendLine($"Nota:");
            sb.AppendLine($"\t{Nota?.ToString() ?? "Não Avaliado"}");


            return sb.ToString();
        }
        #endregion
    }
}
