using DtiAnimeManager.Models;
using System.ComponentModel.Design;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DtiAnimeManager
{
    internal class Program
    {
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
                        AnimeRepository.IniciarDB();
                        break;
                    case 2:
                        CadastraAnime();
                        break;
                    case 3:
                        AtualizarAnime();
                        break;
                    case 4:
                        ListaTodos();
                        break;
                    case 5:
                        ListaUm();
                        break;
                    case 6:
                        DeletarUm();
                        break;
                }

            } while (opcao != 0);

            Console.ReadKey();
        }


        static void Menu()
        {
            Cabecalho();

            Console.WriteLine($"1 - Criar Banco de dados.");
            Console.WriteLine($"2 - Cadastra anime.");
            Console.WriteLine($"3 - Atualizar anime.");
            Console.WriteLine($"4 - Lista todos os anime.");
            Console.WriteLine($"5 - Lista um anime expesifico.");
            Console.WriteLine($"6 - Deletar um anime.");
            Console.WriteLine($"0 - Sair.");
        }

        static void Cabecalho()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("===========================================================");
            Console.WriteLine("Biblioteca de animes");
            Console.WriteLine("===========================================================\n\n");
            Console.ResetColor();
        }

        static Anime CriarAnime()
        {
            string nome;
            string autor;
            string estudio;
            string genero;
            DateTime dataDeLancamento;
            double? nota;

            Console.WriteLine("Nome do anime.");
            nome = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Autor do anime.");
            autor = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Estudior do anime.");
            estudio = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Genero do anime.");
            genero = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Data do lançamento do anime (dd/MM/yyyy).");
            dataDeLancamento = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Nota do anime.");
            nota = Convert.ToSingle(Console.ReadLine());

            return new Anime(nome, autor, estudio, genero, dataDeLancamento, nota);

        }

        static int PegarId()
        {
            Console.WriteLine("Digite o ID do anime para modificação.");
            int id = Convert.ToInt32(Console.ReadLine());

            return id;
        }
        static void CadastraAnime()
        {
            Anime anime = CriarAnime();

            AnimeRepository.CadastrarAnime(anime);
        }
        static void AtualizarAnime()
        {
            Anime anime = CriarAnime();

            AnimeRepository.AtualizarAnime(PegarId(), anime);
        }
        static void ListaTodos()
        {
            List<Anime> animes = AnimeRepository.ListarAnimes();

            foreach (Anime anime in animes)
            {
                Console.WriteLine($"{anime.ToString()}\n");
            }
        }
        static void ListaUm()
        {
            Console.WriteLine(AnimeRepository.ListarAnime(PegarId()));
        }
        static void DeletarUm()
        {
            int id = PegarId();

            Console.WriteLine(AnimeRepository.ListarAnime(id));
            Console.WriteLine($"Tem certesa que deseja deleta? (S/N)");
            string opcao = Convert.ToString(Console.ReadLine());

            opcao = opcao.ToUpper();

            if (opcao == "S")
            {
                AnimeRepository.DeletarRecurso(id);

                Console.WriteLine("Anime deletado.");
            }
            else
            {
                Console.WriteLine("O anime não foi deletado.");
            }
        }
    }
}
