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
        /// <summary>
        /// Inicializa o banco de dados SQLite ("biblioteca.db") executando um script SQL.
        /// </summary>
        /// <remarks>
        /// Este método utiliza um script SQL, obtido através do método <see cref="LerSql"/>,
        /// para configurar a estrutura do banco de dados.
        /// Ele abre uma conexão com o arquivo "biblioteca.db" e executa o script.
        /// </remarks>
        /// <returns>
        /// Uma string de confirmação ("Sucesso: Banco de dados inicializado...") após a
        /// execução bem-sucedida do script.
        /// </returns>
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

        /// <summary>
        /// Tenta cadastrar um novo anime no banco de dados SQLite.
        /// </summary>
        /// <remarks>
        /// Este método primeiro verifica se o objeto anime é nulo ou inválido (usando <see cref="ValidarEntrada"/>).
        /// Em seguida, ele abre uma conexão, adiciona os parâmetros do objeto anime
        /// e executa um comando INSERT. Ele trata a 'Nota' nula convertendo-a para DBNull.Value.
        /// </remarks>
        /// <param name="anime">O objeto <see cref="Anime"/> contendo os dados a serem inseridos.</param>
        /// <returns>
        /// Uma <see cref="string"/> indicando o resultado da operação:
        /// - "Anime cadastrado com sucesso!" (em caso de sucesso).
        /// - Uma mensagem de erro específica (em caso de dados nulos, inválidos ou erro no banco).
        /// </returns>
        public static string CadastrarAnime(Anime anime)
        {
            string connectionString = "Data Source=biblioteca.db";

            if (anime == null)
            {
                return "Erro: Os dados do anime estão nulos.";
            }

            if (!ValidarEntrada(anime))
            {
                return "Erro: Dados inválidos. Por favor, verifique os campos.";
            }

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Animes(Name, Autor, Estudio, Genero, DataDeLancamento, Nota) VALUES ($Name, $Autor, $Estudio, $Genero, $DataDeLancamento, $Nota)";

                    command.Parameters.AddWithValue("$Name", anime.Nome);
                    command.Parameters.AddWithValue("$Autor", anime.Autor);
                    command.Parameters.AddWithValue("$Estudio", anime.Estudio);
                    command.Parameters.AddWithValue("$Genero", anime.Genero);
                    command.Parameters.AddWithValue("$DataDeLancamento", anime.DataDeLancamento.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("$Nota", (object)anime.Nota ?? DBNull.Value);

                    try
                    {
                        int linhasAfetadas = command.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            return "Anime cadastrado com sucesso!";
                        }
                        else
                        {
                            return "Aviso: O comando foi executado, mas nenhuma linha foi alterada.";
                        }
                    }
                    catch (SqliteException ex)
                    {
                        return $"Erro de banco de dados: {ex.Message}";
                    }
                }
            }
        }

        /// <summary>
        /// Tenta atualizar um anime existente no banco de dados SQLite com base em seu ID.
        /// </summary>
        /// <remarks>
        /// Este método primeiro verifica se o objeto <paramref name="anime"/> é nulo ou inválido (usando <see cref="ValidarEntrada"/>).
        /// Em seguida, ele abre uma conexão e executa um comando SQL UPDATE para o <paramref name="id"/> fornecido.
        /// O método utiliza parâmetros para prevenir SQL Injection e trata corretamente a 'Nota' nula, convertendo-a para DBNull.Value.
        /// A data é formatada como "yyyy-MM-dd HH:mm:ss".
        /// </remarks>
        /// <param name="id">O ID do anime a ser atualizado no banco de dados.</param>
        /// <param name="anime">O objeto <see cref="Anime"/> contendo os novos dados para o registro.</param>
        /// <returns>
        /// <see langword="true"/> se exatamente um registro foi atualizado com sucesso;
        /// caso contrário, <see langword="false"/>.
        /// </returns>
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

        /// <summary>
        /// Tenta atualizar um anime existente no banco de dados com base em seu ID.
        /// </summary>
        /// <remarks>
        /// Este método primeiro valida o objeto anime (usando <see cref="ValidarEntrada"/>).
        /// Em seguida, abre uma conexão e executa um comando UPDATE, usando o ID para a
        /// cláusula WHERE e os dados do objeto anime para os valores SET.
        /// Ele verifica se exatamente uma linha foi afetada.
        /// </remarks>
        /// <param name="id">O ID único do anime a ser atualizado.</param>
        /// <param name="anime">O objeto <see cref="Anime"/> contendo os novos dados para a atualização.</param>
        /// <returns>
        /// <see langword="true"/> se exatamente uma linha foi atualizada com sucesso;
        /// <see langword="false"/> se o ID não foi encontrado ou se os
        /// dados de entrada eram inválidos.
        /// </returns>
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

        /// <summary>
        /// Busca e retorna um único anime do banco de dados pelo seu ID.
        /// </summary>
        /// <remarks>
        /// Este método executa uma consulta parametrizada
        /// e usa o <c>ExecuteReader</c>. Se um registro for encontrado,
        /// ele o mapeia para um objeto <see cref="Anime"/> (através do método <see cref="CriarAnime(SqliteDataReader)"/>).
        /// </remarks>
        /// <param name="id">O ID único do anime a ser procurado.</param>
        /// <returns>
        /// Um objeto <see cref="Anime"/> preenchido se o ID for encontrado;
        /// caso contrário, <see langword="null"/>.
        /// </returns>
        public static Anime? ListarAnime(int id)
        {
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"SELECT * FROM Animes WHERE Id = $id";

                    command.Parameters.AddWithValue("$id", id);

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

        /// <summary>
        /// Tenta deletar um anime do banco de dados com base em seu ID.
        /// </summary>
        /// <remarks>
        /// Este método executa um comando DELETE parametrizado
        /// usando o ID fornecido na cláusula WHERE. Ele verifica se exatamente uma
        /// linha foi afetada pela operação.
        /// </remarks>
        /// <param name="id">O ID único do anime a ser deletado.</param>
        /// <returns>
        /// <see langword="true"/> se exatamente uma linha foi deletada com sucesso;
        /// <see langword="false"/> se o ID não foi encontrado ou
        /// se ocorreu um erro.
        /// </returns>
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

        /// <summary>
        /// Tenta excluir todos os registros da tabela 'Animes'.
        /// </summary>
        /// <remarks>
        /// Executa um comando 'DELETE FROM Animes'. A lógica atual deste método retorna
        /// 'true' apenas se uma ou mais linhas forem realmente excluídas. Se a tabela já
        /// estiver vazia (0 linhas afetadas), este método retornará 'false',
        /// o que pode não ser o comportamento esperado.
        /// </remarks>
        /// <returns>
        /// <see langword="true"/> se 1 ou mais linhas foram excluídas;
        /// <see langword="false"/> se 0 linhas foram excluídas (tabela já estava vazia).
        /// </returns>
        public static bool DeletarTudo()
        {
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Animes;";

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

        /// <summary>
        /// Exclui a tabela "Animes" do banco de dados "biblioteca.db".
        /// </summary>
        /// <returns>
        /// Retorna <c>true</c> se a tabela for excluída com sucesso (ou se ela já não existia, 
        /// pois o comando usa "IF EXISTS"). Retorna <c>false</c> se ocorrer uma exceção 
        /// durante a execução do comando.
        /// </returns>
        /// <remarks>
        /// Este método executa o comando SQL "DROP TABLE IF EXISTS Animes;".
        /// </remarks>
        public static bool DeletarTabelaAnimes()
        {
            string connectionString = "Data Source=biblioteca.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DROP TABLE IF EXISTS Animes;";

                    if (command.ExecuteNonQuery() != -1 || command.ExecuteNonQuery() != 0)
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
        /// <summary>
        /// Valida se as propriedades essenciais de um objeto Anime não são nulas.
        /// </summary>
        /// <remarks>
        /// Este método verifica especificamente se as propriedades Nome, Autor, Estudio, Genero
        /// e o resultado de DataDeLancamento.ToString() não são nulos.
        /// </remarks>
        /// <param name="anime">O objeto <see cref="Anime"/> a ser validado.</param>
        /// <returns>
        /// <see langword="true"/> se todas as propriedades verificadas não forem nulas;
        /// caso contrário, <see langword="false"/>.
        /// </returns>
        private static bool ValidarEntrada(Anime anime)
        {
            if (anime.Nome == null || anime.Autor == null || anime.Estudio == null || anime.Genero == null || anime.DataDeLancamento.ToString() == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Lê o conteúdo de um arquivo de script SQL ("init.sql") do disco.
        /// </summary>
        /// <remarks>
        /// Este método tenta ler todo o texto do arquivo "init.sql". Ele inclui tratamento de
        /// exceções para casos como arquivo não encontrado (<see cref="FileNotFoundException"/>)
        /// e outros erros de leitura (<see cref="Exception"/>) ou banco de dados (<see cref="SqliteException"/>).
        /// </remarks>
        /// <returns>
        /// O conteúdo do script SQL lido do arquivo como uma única string.
        /// </returns>
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

        /// <summary>
        /// Cria e retorna uma nova instância da classe <see cref="Anime"/> a partir de um registro do SqliteDataReader.
        /// </summary>
        /// <remarks>
        /// Este método mapeia os dados do reader para um objeto Anime usando índices ordinais
        /// (baseados em número, ex: GetInt32(0), GetString(1)). Ele também trata corretamente
        /// valores <see cref="DBNull"/> para a propriedade 'Nota' (índice 6).
        /// </remarks>
        /// <param name="reader">O <see cref="SqliteDataReader"/> ativo, já posicionado na linha que contém os dados do anime.</param>
        /// <returns>Um novo objeto <see cref="Anime"/> preenchido.</returns>
        private static Anime CriarAnime(SqliteDataReader reader)
        {
            return new Anime(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), DateTime.Parse(reader.GetString(5)), reader.IsDBNull(6) ? null : reader.GetDouble(6));
        }
        #endregion
    }
}
