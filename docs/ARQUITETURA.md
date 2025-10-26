# Arquitetura do DtiAnimeManager

O projeto foi estruturado em 3 camadas simples para garantir a separa��o de responsabilidades (Separation of Concerns).

## 1. Camada de Apresenta��o (Program.cs)

* **Arquivo:** `Program.cs`
* **Responsabilidade:** � a �nica camada que interage com o usu�rio. � respons�vel por:
    * Exibir o menu principal (`Menu()`).
    * Coletar a entrada do usu�rio (`Console.ReadLine()`).
    * Chamar a camada de dados (`AnimeRepository`) para executar as a��es.
    * Formatar e exibir os resultados para o usu�rio.

## 2. Camada de Modelo (Models/Anime.cs)

* **Arquivo:** `Models/Anime.cs`
* **Responsabilidade:** � um objeto POCO (Plain Old C# Object) que representa a entidade do nosso dom�nio. Ele apenas armazena os dados (Id, Nome, Autor, etc.).

## 3. Camada de Acesso a Dados (Data/AnimeRepository.cs)

* **Arquivo:** `Data/AnimeRepository.cs`
* **Responsabilidade:** � a �nica camada que "sabe" como falar com o banco de dados.
    * Gerencia a conex�o com o SQLite (`SqliteConnection`).
    * Cont�m todas as l�gicas de SQL (INSERT, UPDATE, SELECT, DELETE).
    * Traduz os dados do banco para objetos `Anime` e vice-versa.