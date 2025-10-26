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

        public static bool CadastrarAnime(Anime anime)
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
                    //if (command.ExecuteScalar() == null)
                    //{
                    //    return false;
                    //}

                    command.CommandText = "INSERT INTO Animes(Name, Autor, Estudio, Genero, DataDeLancamento, Nota) VALUES ($Name, $Autor, $Estudio, $Genero, $DataDeLancamento, $Nota)";

                    command.Parameters.AddWithValue("$Name", anime.Nome);
                    command.Parameters.AddWithValue("$Autor", anime.Autor);
                    command.Parameters.AddWithValue("$Estudio", anime.Estudio);
                    command.Parameters.AddWithValue("$Genero", anime.Genero);
                    command.Parameters.AddWithValue("$DataDeLancamento", anime.DataDeLancamento.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("$Nota", (object)anime.Nota ?? DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }

            return true;
        }

        public static bool AtualizarAnime(int id, Anime anime)
        {
            if (anime == null)
            {
                return false;
            }

            if (!ValidarEntrada(anime))
            {
                return false;
            }

            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Animes SET Name = $Name, Autor = $Autor, Estudio = $Estudio, Genero = $Genero, DataDeLancamento = $DataDeLancamento, Nota = $Nota WHERE ID = $Id";

                    command.Parameters.AddWithValue("$Id", id);

                    command.Parameters.AddWithValue("$Name", anime.Nome);
                    command.Parameters.AddWithValue("$Autor", anime.Autor);
                    command.Parameters.AddWithValue("$Estudio", anime.Estudio);
                    command.Parameters.AddWithValue("$Genero", anime.Genero);
                    command.Parameters.AddWithValue("$DataDeLancamento", anime.DataDeLancamento.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("$Nota", anime.Nota ?? (object)DBNull.Value);

                    if (command.ExecuteNonQuery() != 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static List<Anime>? ListarAnimes()
        {
            List<Anime> animes = new List<Anime>();
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {


                    command.CommandText = $"SELECT * FROM Animes";

                    try
                    {
                        if (command.ExecuteScalar() == null)
                        {
                            return null;
                        }
                    }
                    catch (SqliteException)
                    {
                        return null;
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            animes.Add(CriarAnime(reader));
                        }
                    }
                }
            }

            return animes;
        }

        public static Anime? ListarAnime(int id)
        {
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"SELECT * FROM Animes WHERE Id = {id}";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CriarAnime(reader);
                        }

                        return null;
                    }
                }

            }
        }

        public static bool DeletarRecurso(int id)
        {
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Animes WHERE Id = $id";

                    command.Parameters.AddWithValue("$id", id);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool DeletarTudo()
        {
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DROP TABLE IF EXISTS Animes;";

                    if (command.ExecuteNonQuery() >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        #region Private
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
            string NameArquivo = "init.sql";
            string scriptSql = string.Empty;

            try
            {
                scriptSql = File.ReadAllText(NameArquivo);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Erro: O arquivo '{NameArquivo}' não foi encontrado no diretório de execução.");
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

        private static Anime CriarAnime(SqliteDataReader reader)
        {
            return new Anime(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), DateTime.Parse(reader.GetString(5)), reader.IsDBNull(6) ? null : reader.GetDouble(6));
        }
        #endregion
    }
}
