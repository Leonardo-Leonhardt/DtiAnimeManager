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
                        Console.Clear();
                        Console.WriteLine(AnimeRepository.IniciarDB());
                        Thread.Sleep(2000);
                        break;
                    case 2:
                        CadastraAnime();
                        break;
                    case 3:
                        AtualizarAnime();
                        break;
                    case 4:
                        ListaTodos();
                        Console.ReadKey();
                        break;
                    case 5:
                        ListaUm();
                        break;
                    case 6:
                        DeletarUm();
                        break;
                    case 7:
                        DeletarTudo();
                        break;
                }

            } while (opcao != 0);
        }


        static void Menu()
        {
            Cabecalho();

            Console.WriteLine($"1 - Iniciar Banco de dados.");
            Console.WriteLine($"2 - Cadastra anime.");
            Console.WriteLine($"3 - Atualizar anime.");
            Console.WriteLine($"4 - Lista todos os anime.");
            Console.WriteLine($"5 - Lista um anime expesifico.");
            Console.WriteLine($"6 - Deletar um anime.");
            Console.WriteLine($"7 - Deletar tudo.");
            Console.WriteLine($"0 - Sair.");
        }

        static void Cabecalho()
        {
            Console.Clear();
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
            Console.WriteLine("Data do lançamento do anime (yyyy/mm/dd).");
            dataDeLancamento = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Nota do anime.");
            nota = Convert.ToSingle(Console.ReadLine());

            return new Anime(nome, autor, estudio, genero, dataDeLancamento, nota);

        }

        static int PegarId()
        {
            Console.WriteLine("Digite o ID do anime.");
            int id = Convert.ToInt32(Console.ReadLine());

            return id;
        }
        static void CadastraAnime()
        {
            //Anime anime = CriarAnime();
            Anime[] animes = new Anime[]
        {
            new Anime("Attack on Titan", "Hajime Isayama", "Wit Studio", "Ação / Fantasia / Drama", new DateTime(2013, 4, 7), 9.3),
            new Anime("Fullmetal Alchemist: Brotherhood", "Hiromu Arakawa", "Bones", "Ação / Aventura / Fantasia", new DateTime(2009, 4, 5), 9.5),
            new Anime("Death Note", "Tsugumi Ohba", "Madhouse", "Suspense / Mistério / Sobrenatural", new DateTime(2006, 10, 4), 9.0),
            new Anime("Naruto", "Masashi Kishimoto", "Pierrot", "Ação / Aventura / Shonen", new DateTime(2002, 10, 3), 8.2),
            new Anime("Demon Slayer", "Koyoharu Gotouge", "Ufotable", "Ação / Fantasia / Shonen", new DateTime(2019, 4, 6), 8.9),
            new Anime("One Piece", "Eiichiro Oda", "Toei Animation", "Aventura / Comédia / Shonen", new DateTime(1999, 10, 20), 8.8),
            new Anime("My Hero Academia", "Kohei Horikoshi", "Bones", "Ação / Superpoderes / Shonen", new DateTime(2016, 4, 3), 8.5),
            new Anime("Sword Art Online", "Reki Kawahara", "A-1 Pictures", "Ação / Aventura / Fantasia", new DateTime(2012, 7, 8), 7.8),
            new Anime("Tokyo Ghoul", "Sui Ishida", "Pierrot", "Ação / Horror / Sobrenatural", new DateTime(2014, 7, 4), 8.0),
            new Anime("Steins;Gate", "5bp.", "White Fox", "Sci-Fi / Thriller / Drama", new DateTime(2011, 4, 6), 9.1)
        };

            foreach (var anime in animes)
            {
               AnimeRepository.CadastrarAnime(anime);
            }


            
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

        static void DeletarTudo()
        {
           Console.WriteLine(AnimeRepository.DeletarTudo());
        }
    }
}
