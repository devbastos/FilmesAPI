# FilmesAPI

API REST para gerenciamento de filmes, desenvolvida com ASP.NET Core, Entity Framework Core, MySQL e AutoMapper.

> **Estado do projeto:** refatoração incremental. O projeto original utiliza .NET 5, que está fora de suporte. A migração para uma versão atual do .NET e a inclusão de testes automatizados estão planejadas; ainda não foram concluídas.

## Endpoints

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/filmes` | Lista filmes |
| GET | `/api/filmes/{id}` | Consulta por ID |
| POST | `/api/filmes` | Cadastra filme |
| PUT | `/api/filmes/{id}` | Atualiza filme |
| DELETE | `/api/filmes/{id}` | Remove filme |

Exemplo de corpo para POST/PUT:

```json
{
  "titulo": "Interestelar",
  "diretor": "Christopher Nolan",
  "genero": "Ficção científica",
  "duracao": 169
}
```

## Melhorias desta branch

- Operações de banco assíncronas com suporte a `CancellationToken`.
- `AsNoTracking()` para consultas somente leitura.
- DTOs nas respostas em vez de expor diretamente as entidades.
- Campos de dependência somente leitura.
- Rotas explícitas sob `/api/filmes`.

**Atenção:** a mudança de rota de `/Filme` para `/api/filmes` altera o contrato HTTP anterior. Clientes existentes precisarão ser ajustados.

## Execução local

1. Instale o SDK do .NET 5 (legado) e disponibilize uma instância MySQL compatível.
2. Configure a connection string `FilmeConnection` por variáveis de ambiente ou user secrets, evitando credenciais no repositório.
3. Configure `ConnectionStrings__FilmeConnection` com a string de conexão MySQL no ambiente (ou use `dotnet user-secrets`).
4. Execute `dotnet restore` e `dotnet run --project FilmesAPI/FilmesAPI.csproj`.

> **Segurança:** o histórico original do repositório contém uma credencial de desenvolvimento. Se ela ainda estiver ativa, revogue-a ou faça a rotação. A remoção do `appsettings.json` atual não apaga o histórico Git.

A configuração de migrações e compatibilidade de dependências precisa ser validada no ambiente local.

## Próximos passos

- Migrar para .NET 10 e atualizar pacotes.
- Adicionar testes unitários e de integração.
- Extrair regras de negócio e acesso a dados do controller conforme a complexidade evoluir.
- Configurar CI para build e testes.
- Documentar decisões arquiteturais.
