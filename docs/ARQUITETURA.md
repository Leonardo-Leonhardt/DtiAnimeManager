# Arquitetura do DtiAnimeManager

O projeto foi estruturado em 3 camadas simples para garantir a separação de responsabilidades (Separation of Concerns).

## 1. Camada de Apresentação (Program.cs)

* **Arquivo:** `Program.cs`
* **Responsabilidade:** É a única camada que interage com o usuário. É responsável por:
    * Exibir o menu principal (`Menu()`).
    * Coletar a entrada do usuário (`Console.ReadLine()`).
    * Chamar a camada de dados (`AnimeRepository`) para executar as ações.
    * Formatar e exibir os resultados para o usuário.

## 2. Camada de Modelo (Models/Anime.cs)

* **Arquivo:** `Models/Anime.cs`
* **Responsabilidade:** É um objeto POCO (Plain Old C# Object) que representa a entidade do nosso domínio. Ele apenas armazena os dados (Id, Nome, Autor, etc.).

## 3. Camada de Acesso a Dados (Data/AnimeRepository.cs)

* **Arquivo:** `Data/AnimeRepository.cs`
* **Responsabilidade:** É a única camada que "sabe" como falar com o banco de dados.
    * Gerencia a conexão com o SQLite (`SqliteConnection`).
    * Contém todas as lógicas de SQL (INSERT, UPDATE, SELECT, DELETE).
    * Traduz os dados do banco para objetos `Anime` e vice-versa.