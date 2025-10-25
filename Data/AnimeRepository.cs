using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace DtiAnimeManager.Models
{
    public static class AnimeRepository
    {
        public static string IniciarDB()
        {
            string _initializationSqlScript = LerSql();
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = _initializationSqlScript;
                command.ExecuteNonQuery();

                return "Sucesso: Banco de dados inicializado a partir do script SQL.";
            }
        }

        public static bool CadastrarRecurso(Anime anime)
        {
            string connectionString = "Data Source=biblioteca.db";

            if (anime == null)
            {
                return false;
            }

            if (!ValidarEntrada(anime))
            {
                return false;
            }

            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Animes(Nome, Autor, Estudio, Genero, Data, Nota) VALUES ($Nome, $Autor, $Estudio, $Genero, $Data, $Nota)";

                    command.Parameters.AddWithValue("$Nome", anime.Nome);
                    command.Parameters.AddWithValue("$Autor", anime.Autor);
                    command.Parameters.AddWithValue("$Estudio", anime.Estudio);
                    command.Parameters.AddWithValue("$Genero", anime.Genero);
                    command.Parameters.AddWithValue("$DataDeLancamento", anime.DataDeLancamento.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("$Nota", anime.Nota ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
        public static bool AtualizarRecurso()
        {
            return true;
        }

        public static List<Anime> ListarRecursos()
        {
            List<Anime> animes = new List<Anime>();
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM Animes WHERE Animes";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var anime = new Anime(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDouble(7));

                            animes.Add(anime);
                        }
                    }
                }
            }

            return animes;

        }

        public static Anime ListarAnime(int id)
        {
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"SELECT ID FROM Animes WHERE ID = {id}";

                    using (var reader = command.ExecuteReader())
                    {
                        var anime = new Anime(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDouble(7));

                        return anime;
                    }
                }

            }
        }

        public static string DeletarRecurso()
        {
            return "teste";
        }

        private static bool ValidarEntrada(Anime anime)
        {
            if (anime.Nome == null || anime.Autor == null || anime.Estudio == null || anime.Genero == null || anime.DataDeLancamento.ToString() == null)
            {
                return false;
            }

            return true;
        }

        private static string LerSql()
        {
            string nomeArquivo = "init.sql";
            string scriptSql = string.Empty;

            try
            {
                scriptSql = File.ReadAllText(nomeArquivo);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Erro: O arquivo '{nomeArquivo}' não foi encontrado no diretório de execução.");
                throw;
            }
            catch (SqliteException ex)
            {
                Console.WriteLine($"Erro do SQLite: Falha ao executar o script. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao ler o arquivo: {ex.Message}");
                throw;
            }

            return scriptSql;
        }
    }
}
