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
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Anime"/> com detalhes específicos.
        /// </summary>
        /// <param name="id">O identificador único para o anime.</param>
        /// <param name="nome">O nome do anime.</param>
        /// <param name="autor">O autor do anime.</param>
        /// <param name="estudio">O estúdio que produziu o anime.</param>
        /// <param name="genero">O gênero do anime.</param>
        /// <param name="data">A data de lançamento do anime.</param>
        /// <param name="nota">A nota do anime, ou <see langword="null"/> se não avaliado.</param>
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

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Anime"/> com detalhes específicos.
        /// </summary>
        /// <param name="nome">O nome do anime.</param>
        /// <param name="autor">O autor do anime.</param>
        /// <param name="estudio">O estúdio que produziu o anime.</param>
        /// <param name="genero">O gênero do anime.</param>
        /// <param name="data">A data de lançamento do anime.</param>
        /// <param name="nota">A nota do anime, ou <see langword="null"/> se não avaliado.</param>
        public Anime(string nome, string autor, string estudio, string genero, DateTime data, double? nota)
        {
            this.Nome = nome;
            this.Autor = autor;
            this.Estudio = estudio;
            this.Genero = genero;
            this.DataDeLancamento = data;
            this.Nota = nota;
        }
        #endregion

        #region Herança
        /// <summary>
        /// Retorna uma string que representa o objeto atual, incluindo suas propriedades formatadas para exibição.
        /// </summary>
        /// <remarks>
        /// A string retornada inclui as propriedades Id, Nome, Autor, Estudio, Genero, Data De
        /// Lancamento e Nota, cada uma em uma nova linha com seus respectivos rótulos. A Data De Lancamento é
        /// formatada como "dd/MM/yyyy", e a Nota é exibida como "Não Avaliado" se for nula.
        /// </remarks>
        /// <returns>Uma representação em string do objeto atual com informações detalhadas das propriedades.</returns>
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
            sb.AppendLine($"\t{DataDeLancamento.ToString("dd/MM/yyyy")}");
            sb.AppendLine($"Nota:");
            sb.AppendLine($"\t{Nota?.ToString() ?? "Não Avaliado"}");


            return sb.ToString();
        }
        #endregion
    }
}
