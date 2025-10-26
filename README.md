# DtiAnimeManager

`DtiAnimeManager` é um projeto de console (C#) para gerenciamento de uma coleção de animes. Ele foi desenvolvido como um case técnico para demonstrar conceitos de arquitetura simples e acesso a dados com ADO.NET.

## ✨ Funcionalidades

O aplicativo fornece um menu interativo com as seguintes operações:

* Iniciar Banco de dados: Cria o arquivo biblioteca.db e a tabela Animes (baseado no script init.sql).
* Cadastrar anime: Adiciona um novo anime ao banco.
* Cadastrar 10 animes: Popula o banco com dados de teste (Seed).
* Atualizar anime: Modifica um anime existente (requer ID).
* Listar todos os animes: Exibe todos os animes do banco.
* Listar um anime específico: Busca e exibe um anime pelo seu ID.
* Deletar um anime: Remove um anime pelo seu ID.
* Deletar tudo: Limpa todos os registros da tabela Animes.

## 🚀 Tecnologias Utilizadas

* **C# (.NET)**: Linguagem e plataforma principal do projeto.
* **SQLite**: Banco de dados leve e local.
* **Microsoft.Data.Sqlite**: Driver ADO.NET para comunicação com o SQLite.
* **Arquitetura Simples**: O projeto está estruturado com uma separação de responsabilidades (View/Controller em Program.cs, Repositório em Data/, Modelo em Models/)..
* **Arquitetura em Camadas:** O projeto está estruturado com uma separação clara de responsabilidades.

## 📂 Estrutura do Projeto

O projeto está organizado da seguinte forma para manter uma arquitetura limpa:


```
DtiAnimeManager/
│
├── Data/
│   └── AnimeRepository.cs      # Lógica de acesso ao banco de dados (SQL).
│
├── Models/
│   └── Anime.cs                # Classe POCO que representa a entidade Anime.
│
├── .gitignore
├── DtiAnimeManager.csproj
├── DtiAnimeManager.sln
├── init.sql                     # Script de criação da tabela Animes.
└── Program.cs                   # Camada de apresentação: menu e interação com o usuário.
```

## ⚙️ Configuração e Instalação

Siga os passos abaixo para configurar e executar o projeto em sua máquina local.

### Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/download) (Verifique a versão no seu arquivo `.csproj`)
* [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/) (Recomendado) ou [VS Code](https://code.visualstudio.com/)
* Não é necessário instalar o SQLite: O projeto usa o pacote Microsoft.Data.Sqlite, que já inclui o motor do banco de dados. Ele será baixado automaticamente pelo .NET durante a compilação.

### Passos

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/Leonardo-Leonhardt/DtiAnimeManager.git
    cd DtiAnimeManager
    ```

2.  **Abra o projeto:**
    * Abra o arquivo `DtiAnimeManager.sln` com o Visual Studio.

3.  Compile e Execute:

* Pelo Visual Studio, pressione F5 ou o botão "Start" para compilar e executar o projeto.
* Alternativamente, pelo terminal na pasta do projeto:

```
bash

dotnet run
```

4. Inicialize o Banco de Dados (Passo Importante!):

* Ao executar o programa pela primeira vez, o menu será exibido.
* Escolha a Opção 1 - Iniciar Banco de dados.
* Isso irá ler o arquivo ``init.sql`` (que deve estar na pasta de execução, ex: ``bin/Debug/...``) e criar o arquivo ``biblioteca.db`` no mesmo local.

5. Pronto!

* Após o banco ser criado, você pode usar as outras funções, como a Opção 3 - Cadastra 10 animes para popular o banco e testar as listagens.