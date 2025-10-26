Markdown

# Design do Banco de Dados

## Escolha da Tecnologia

Foi escolhido o **SQLite** por ser um banco de dados "embutido" (embedded).

* **Vantagens:** N�o requer um servidor separado, � leve e armazena o banco inteiro em um �nico arquivo (`biblioteca.db`). Isso torna a configura��o do projeto muito mais simples.
* **Driver:** O acesso � feito via ADO.NET puro, utilizando o pacote `Microsoft.Data.Sqlite`.

## Esquema (Schema)

O esquema da tabela `Animes` � definido em `init.sql`:

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
