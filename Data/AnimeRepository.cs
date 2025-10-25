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
        #region Variaveis
        private const string DataBaseFileName = "meu_aplicativo.db";
        private static string _connectionString => $"Data Source={DataBaseFileName}";
        #endregion

        public static void IniciarDB()
        {
            string _initializationSqlScript = LerSql();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = _initializationSqlScript;
                command.ExecuteNonQuery();

                Console.WriteLine("Sucesso: Banco de dados inicializado a partir do script SQL.");
            }
        }

        public static List<Anime> ListarRecursos()
        {
        }

        public static bool CadastrarRecurso()
        {
            return true;
        }
        public static bool AtualizarRecurso()
        {
            return true;
        }

        public static string DeletarRecurso()
        {
            return "teste";
        }

        private static bool ValidarEntrada()
        {
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
