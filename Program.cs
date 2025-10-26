using DtiAnimeManager.Models;
using System.ComponentModel.Design;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DtiAnimeManager
{
    internal class Program
    {
        /// <summary>
        /// Ponto de entrada principal para a aplicação de console.
        /// </summary>
        /// <remarks>
        /// Este método gerencia o loop principal do programa, exibindo o <see cref="Menu"/> e
        /// aguardando a entrada do usuário. Ele usa uma estrutura switch para direcionar o fluxo da
        /// aplicação para diferentes métodos (como <see cref="CadastraAnime"/>, <see cref="AtualizarAnime"/>,
        /// <see cref="ListaTodos"/>, etc.) com base na opção escolhida. O loop continua até que o
        /// usuário digite '0' para sair.
        /// </remarks>
        /// <param name="args">Argumentos da linha de comando (não utilizados nesta aplicação).</param>
        static void Main(string[] args)
        {
            int opcao = 0;

            do
            {
                Menu();
                opcao = Convert.ToInt32(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Cabecalho();
                        Console.WriteLine(AnimeRepository.IniciarDB());
                        MensagemDeTecleEnter();
                        break;
                    case 2:
                        Cabecalho();
                        CadastraAnime();
                        MensagemDeTecleEnter();
                        break;
                    case 3:
                        Cabecalho();
                        PopularBancoComDadosTeste();
                        MensagemDeTecleEnter();
                        break;
                    case 4:
                        Cabecalho();
                        AtualizarAnime();
                        MensagemDeTecleEnter();
                        break;
                    case 5:
                        Cabecalho();
                        ListaTodos();
                        MensagemDeTecleEnter();
                        break;
                    case 6:
                        Cabecalho();
                        ListaUm();
                        MensagemDeTecleEnter();
                        break;
                    case 7:
                        Cabecalho();
                        DeletarUm();
                        MensagemDeTecleEnter();
                        break;
                    case 8:
                        Cabecalho();
                        DeletarTudo();
                        MensagemDeTecleEnter();
                        break;
                    case 0:
                        Cabecalho();
                        Console.WriteLine("Saindo...");
                        Thread.Sleep(2000);
                        break;
                }

            } while (opcao != 0);
        }

        /// <summary>
        /// Exibe o menu principal da aplicação no console.
        /// </summary>
        /// <remarks>
        /// Este método primeiro chama <see cref="Cabecalho"/> para limpar a tela e mostrar um título.
        /// Em seguida, lista todas as opções de navegação disponíveis para o usuário, como
        /// cadastrar, atualizar, listar e deletar animes.
        /// </remarks>
        static void Menu()
        {
            Cabecalho();

            Console.WriteLine($"1 - Iniciar Banco de dados.");
            Console.WriteLine($"2 - Cadastra anime.");
            Console.WriteLine($"3 - Cadastra 10 animes.");
            Console.WriteLine($"4 - Atualizar anime.");
            Console.WriteLine($"5 - Lista todos os anime.");
            Console.WriteLine($"6 - Lista um anime expesifico.");
            Console.WriteLine($"7 - Deletar um anime.");
            Console.WriteLine($"8 - Deletar tudo.");
            Console.WriteLine($"0 - Sair.\n");
        }

        /// <summary>
        /// Limpa o console e exibe o cabeçalho padronizado da aplicação.
        /// </summary>
        /// <remarks>
        /// Este método limpa a tela, define a cor do texto do console para verde, imprime o título
        /// "Biblioteca de animes" formatado e, em seguida, reverte a cor para o padrão.
        /// </remarks>
        static void Cabecalho()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("===========================================================");
            Console.WriteLine("Biblioteca de animes");
            Console.WriteLine("===========================================================\n\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Tenta cadastrar um novo anime no repositório com base na entrada do usuário.
        /// </summary>
        /// <remarks>
        /// Este método chama <see cref="CriarAnime"/> para coletar os detalhes do anime.
        /// Em seguida, tenta passar o objeto anime para o <c>AnimeRepository.CadastrarAnime</c>.
        /// Ele verifica o valor de retorno booleano do repositório e exibe uma mensagem
        /// de sucesso ou de falha apropriada no console.
        /// </remarks>
        static void CadastraAnime()
        {
            Anime anime = CriarAnime();

            if (!AnimeRepository.CadastrarAnime(anime))
            {
                Console.WriteLine("\nNão foi possível cadastrar o anime. Verifique os dados e tente novamente.");
            }
            else
            {
                Console.WriteLine("\nAnime cadastrado com sucesso!");

            }
        }

        /// <summary>
        /// Popula o repositório com um conjunto de dados de teste (Seed).
        /// </summary>
        /// <remarks>
        /// Adiciona uma lista pré-definida de 10 animes ao repositório.
        /// Útil para facilitar testes de listagem, atualização e exclusão.
        /// </remarks>
        static void PopularBancoComDadosTeste()
        {
            int contador = 0;
            Console.WriteLine("Iniciando carga de dados de teste...");

            Anime[] animes = new Anime[] {
                             new Anime("Attack on Titan", "Hajime Isayama", "Wit Studio", "Ação / Fantasia / Drama", new DateTime(2013, 4, 7), 9.3),
                             new Anime("Fullmetal Alchemist: Brotherhood", "Hiromu Arakawa", "Bones", "Ação / Aventura / Fantasia", new DateTime(2009, 4, 5), 9.5),
                             new Anime("Death Note", "Tsugumi Ohba", "Madhouse", "Suspense / Mistério / Sobrenatural", new DateTime(2006, 10, 4), 9.0),
                             new Anime("Naruto", "Masashi Kishimoto", "Pierrot", "Ação / Aventura / Shonen", new DateTime(2002, 10, 3), 8.2),
                             new Anime("Demon Slayer", "Koyoharu Gotouge", "Ufotable", "Ação / Fantasia / Shonen", new DateTime(2019, 4, 6), 8.9),
                             new Anime("One Piece", "Eiichiro Oda", "Toei Animation", "Aventura / Comédia / Shonen", new DateTime(1999, 10, 20), 8.8),
                             new Anime("My Hero Academia", "Kohei Horikoshi", "Bones", "Ação / Superpoderes / Shonen", new DateTime(2016, 4, 3), 8.5),
                             new Anime("Sword Art Online", "Reki Kawahara", "A-1 Pictures", "Ação / Aventura / Fantasia", new DateTime(2012, 7, 8), 7.8),
                             new Anime("Tokyo Ghoul", "Sui Ishida", "Pierrot", "Ação / Horror / Sobrenatural", new DateTime(2014, 7, 4), 8.0),
                             new Anime("Anime Fantasma", "12 ep.", null, null, new DateTime(2025, 1, 10), 7.2) };

            foreach (var anime in animes)
            {
                if (!AnimeRepository.CadastrarAnime(anime))
                {
                    Console.WriteLine($"\nNão foi possível cadastrar o anime: {anime.Nome}. Verifique os dados e tente novamente.");
                }
            }
        }

        /// <summary>
        /// Tenta atualizar um anime existente no repositório com novas informações.
        /// </summary>
        /// <remarks>
        /// Este método primeiro solicita ao usuário um ID (usando <see cref="PegarId"/>) para
        /// identificar qual anime deve ser atualizado. Em seguida, solicita todos os novos dados
        /// (usando <see cref="CriarAnime"/>).
        /// <para>
        /// Ele passa o ID e o novo objeto anime para o repositório e verifica o retorno booleano
        /// para informar ao usuário se a atualização foi bem-sucedida ou falhou.
        /// </para>
        /// </remarks>
        static void AtualizarAnime()
        {
            Anime anime = CriarAnime();

            if (!AnimeRepository.AtualizarAnime(PegarId(), anime))
            {
                Console.WriteLine("\nNão foi possível atualizar o anime. Verifique os dados e tente novamente.");
            }
            else
            {
                Console.WriteLine("\nAnime atualizado com sucesso!");
            }
        }

        /// <summary>
        /// Recupera e exibe todos os animes cadastrados no console.
        /// </summary>
        /// <remarks>
        /// Este método busca a lista de animes do repositório. Se a lista for nula, exibe uma mensagem
        /// de erro. Se a lista estiver vazia, informa ao usuário que não há animes
        /// cadastrados. Caso contrário, itera sobre cada anime na lista e imprime seus detalhes
        /// formatados no console.
        /// </remarks>
        static void ListaTodos()
        {
            List<Anime> animes = AnimeRepository.ListarAnimes();
            if (animes == null)
            {
                Console.WriteLine("Erro ao recuperar animes.");
            }
            else if (animes.Count == 0)
            {
                Console.WriteLine("Nenhum anime cadastrado.");
            }
            else
            {
                foreach (Anime anime in animes)
                {
                    Console.WriteLine($"{anime.ToString()}\n");
                }
            }
        }

        /// <summary>
        /// Recupera e exibe informações sobre um anime específico pelo seu identificador.
        /// </summary>
        /// <remarks>
        /// Este método busca (ou recupera) um anime do repositório usando um identificador específico e
        /// imprime seus detalhes no console. Se o anime não for encontrado, uma mensagem indicando a ausência
        /// é exibida.
        /// </remarks>
        static void ListaUm()
        {
            Anime anime = AnimeRepository.ListarAnime(PegarId());

            Console.WriteLine(anime != null ? anime.ToString() : "\nAnime não foi encontrado");
        }

        /// <summary>
        /// Exclui um recurso de anime identificado pelo ID especificado após a confirmação do usuário.
        /// </summary>
        /// <remarks>
        /// Solicita a confirmação do usuário antes de prosseguir com a exclusão. Se o usuário
        /// não confirmar, a exclusão é cancelada e uma mensagem é exibida. Caso contrário, o recurso de anime
        /// especificado é excluído do repositório.
        /// </remarks>
        static void DeletarUm()
        {
            int id = PegarId();

            if (!ComfirmarAcao("\nTem certesa que deseja deleta?"))
            {
                Console.WriteLine("O anime não foi deletado.");
            }
            else
            {
                if (!AnimeRepository.DeletarRecurso(id))
                {
                    Console.WriteLine("\nAnime não encontrado.");
                }
                else
                {
                    Console.WriteLine("Anime deletado.");
                }
            }
        }

        /// <summary>
        /// Exclui todos os itens do repositório após a confirmação do usuário.
        /// </summary>
        /// <remarks>
        /// Solicita a confirmação do usuário antes de prosseguir com a exclusão. Se o usuário
        /// confirmar, todos os itens serão excluídos; caso contrário, nenhuma ação será tomada.
        /// </remarks>
        static void DeletarTudo()
        {
            if (!ComfirmarAcao("Tem certesa que deseja deleta tudo?"))
            {
                Console.WriteLine("\nOs animes não foram deletados.");
            }
            else
            {
                if (!AnimeRepository.DeletarTudo())
                {
                    Console.WriteLine("\nNenhum anime cadastrado.");
                }
                else
                {
                    Console.WriteLine("\nTodos os animes foram deletados.");
                }

            }
        }

        #region Helpers
        /// <summary>
        /// Exibe a mensagem "Pressione qualquer tecla para continuar..." e pausa a execução do console.
        /// </summary>
        /// <remarks>
        /// Este método utilitário usa <c>Console.ReadKey()</c> para aguardar que o usuário pressione
        /// qualquer tecla. É tipicamente usado para impedir que o console feche ou limpe a tela
        /// imediatamente, permitindo que o usuário leia as informações exibidas antes de prosseguir.
        /// </remarks>
        static void MensagemDeTecleEnter()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        /// <summary>
        /// Exibe uma mensagem ao usuário e aguarda uma resposta de confirmação.
        /// </summary>
        /// <remarks>
        /// O método lê a entrada do console e espera que o usuário digite 'S' para
        /// confirmação. Qualquer outra entrada resultará em um valor de retorno <see langword="false"/>.
        /// </remarks>
        /// <param name="mensagem">A mensagem a ser exibida ao usuário, pedindo confirmação.</param>
        /// <returns>
        /// <see langword="true"/> se o usuário confirmar a ação digitando 'S'; caso contrário, <see langword="false"/>.
        /// </returns>
        static bool ComfirmarAcao(string mensagem)
        {
            Console.WriteLine(mensagem + " (S/N)");
            string opcao = Convert.ToString(Console.ReadLine());
            opcao = opcao.ToUpper();
            return opcao == "S";
        }

        /// <summary>
        /// Solicita ao usuário que insira um ID de anime e retorna o valor inserido.
        /// </summary>
        /// <remarks>
        /// O método lê a entrada do console e a converte para um inteiro. Garanta que
        /// a entrada seja um inteiro válido para evitar exceções.
        /// </remarks>
        /// <returns>O ID do anime conforme inserido pelo usuário.</returns>
        static int PegarId()
        {
            Console.WriteLine("Digite o ID do anime.");
            int id = Convert.ToInt32(Console.ReadLine());

            return id;
        }

        /// <summary>
        /// Cria uma nova instância da classe <see cref="Anime"/> com detalhes fornecidos pelo usuário.
        /// </summary>
        /// <remarks>
        /// Este método solicita ao usuário que insira vários detalhes sobre um anime, incluindo seu
        /// nome, autor, estúdio, gênero, data de lançamento e nota. A data de lançamento deve ser inserida no formato
        /// "dd/MM/yyyy".
        /// </remarks>
        /// <returns>Um objeto <see cref="Anime"/> inicializado com os detalhes especificados.</returns>
        static Anime CriarAnime()
        {
            string nome;
            string autor;
            double? nota;
            string genero;
            string estudio;
            bool formatoValido = false;
            DateTime dataDeLancamento = default;

            Console.WriteLine("Nome do anime.");
            nome = Convert.ToString(Console.ReadLine());
            Console.WriteLine("\nAutor do anime.");
            autor = Convert.ToString(Console.ReadLine());
            Console.WriteLine("\nEstudior do anime.");
            estudio = Convert.ToString(Console.ReadLine());
            Console.WriteLine("\nGenero do anime.");
            genero = Convert.ToString(Console.ReadLine());
            Console.WriteLine("\nData do lançamento do anime (dd/MM/yyyy).");

            while (!formatoValido)
            {
                string dataInput = Convert.ToString(Console.ReadLine());

                formatoValido = DateTime.TryParseExact(dataInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataDeLancamento);

                if (!formatoValido)
                {
                    Console.WriteLine("Formato inválido. Por favor, insira a data no formato dd/MM/yyyy.");
                }
            }
            Console.WriteLine("\nNota do anime.");
            nota = Convert.ToSingle(Console.ReadLine());

            return new Anime(nome, autor, estudio, genero, dataDeLancamento, nota);

        }
        #endregion
    }
}
