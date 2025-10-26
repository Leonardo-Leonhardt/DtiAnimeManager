# DtiAnimeManager

`DtiAnimeManager` é um projeto de console (C#) para gerenciamento de uma coleção de animes. Ele foi desenvolvido como um case técnico para demonstrar conceitos de arquitetura em camadas e acesso a dados.

## ✨ Funcionalidades

* Listar todos os animes cadastrados.
* Adicionar um novo anime ao banco de dados.
* Atualizar as informações de um anime existente.
* Remover um anime pelo seu ID.
* Buscar um anime específico pelo nome.

*(Observação: Adapte a lista de funcionalidades acima para o que seu projeto realmente faz.)*

## 🚀 Tecnologias Utilizadas

* **C# (.NET):** Linguagem e plataforma principal do projeto.
* **SQLite:** Banco de dados leve e local (suposição baseada no arquivo `init.sql`).
* **Arquitetura em Camadas:** O projeto está estruturado com uma separação clara de responsabilidades.

## ⚙️ Configuração e Instalação

Siga os passos abaixo para configurar e executar o projeto em sua máquina local.

### Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/download) (Verifique a versão no seu arquivo `.csproj`)
* [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/) (Recomendado) ou [VS Code](https://code.visualstudio.com/)
* Uma ferramenta para gerenciar SQLite (como [DB Browser for SQLite](https://sqlitebrowser.org/))
* Não é necessário instalar o SQLite: O projeto usa o pacote Microsoft.Data.Sqlite, que já inclui o motor do banco de dados. Ele será baixado automaticamente pelo .NET durante a compilação.

### Passos

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/Leonardo-Leonhardt/DtiAnimeManager.git
    cd DtiAnimeManager
    ```

2.  **Abra o projeto:**
    * Abra o arquivo `DtiAnimeManager.sln` com o Visual Studio.

3.  **Configure o Banco de Dados:**
    * O projeto utiliza um banco de dados SQLite.
    * O arquivo `init.sql` contém os comandos `CREATE TABLE` necessários.
    * **Importante:** Verifique no código (provavelmente em `AnimeRepository.cs`) como o banco de dados (`.db`) é criado e onde ele espera que o arquivo `init.sql` seja executado.
    * *Se o projeto não criar o banco automaticamente*, use o DB Browser for SQLite para:
        1.  Criar um novo banco de dados (ex: `anime.db`) na pasta de execução do projeto (geralmente `bin/Debug/...`).
        2.  Ir para a aba "Executar SQL" e colar o conteúdo do `init.sql` para criar as tabelas.

4.  **Compile e Execute:**
    * Pelo Visual Studio, pressione `F5` ou o botão "Start" para compilar e executar o projeto.

## 📂 Estrutura do Projeto

O projeto está organizado da seguinte forma para manter uma arquitetura limpa: