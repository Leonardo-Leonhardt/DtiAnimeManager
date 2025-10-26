Markdown

# Design do Banco de Dados

## Escolha da Tecnologia

Foi escolhido o **SQLite** por ser um banco de dados "embutido" (embedded).

* **Vantagens:** Não requer um servidor separado, é leve e armazena o banco inteiro em um único arquivo (`biblioteca.db`). Isso torna a configuração do projeto muito mais simples.
* **Driver:** O acesso é feito via ADO.NET puro, utilizando o pacote `Microsoft.Data.Sqlite`.

## Esquema (Schema)

O esquema da tabela `Animes` é definido em `init.sql`:

```sql
CREATE TABLE IF NOT EXISTS Animes (
	"Id"	INTEGER PRIMARY KEY AUTOINCREMENT,
	"Name"	TEXT NOT NULL,
	"Autor"	TEXT NOT NULL,
	"Estudio"	TEXT NOT NULL,
	"Genero"	TEXT NOT NULL,
	"DataDeLancamento"	TEXT NOT NULL,
	"Nota"	REAL
);
