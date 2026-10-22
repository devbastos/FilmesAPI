# FilmesAPI — .NET 10

API REST de filmes com ASP.NET Core, Entity Framework Core, MySQL (Pomelo), serviços, DTOs e operações assíncronas.

## Arquitetura

- **Controller:** HTTP e status codes.
- **Service:** operações de aplicação e mapeamento para DTOs.
- **DbContext:** persistência EF Core com MySQL.
- **Tests:** testes de comportamento com EF InMemory (não substituem testes de integração MySQL).

## Rotas

| Método | Rota | Ação |
|---|---|---|
| GET | /api/filmes | Listar |
| GET | /api/filmes/{id} | Consultar |
| POST | /api/filmes | Criar |
| PUT | /api/filmes/{id} | Atualizar |
| DELETE | /api/filmes/{id} | Excluir |

Exemplo POST:
```json
{"titulo":"Interestelar","diretor":"Christopher Nolan","genero":"Ficção científica","duracao":169}
```

## Executar

Requer .NET SDK 10 e MySQL. Configure a connection string fora do código:
```bash
export ConnectionStrings__FilmeConnection='server=localhost;database=filmeDb;user=app;password=SUA_SENHA'
dotnet restore FilmesAPI.sln
dotnet run --project FilmesAPI/FilmesAPI.csproj
```

### Banco de dados e migrações

A migração inicial Pomelo desta versão foi criada para **bancos novos**. Se já existir banco criado com as migrações do provider anterior, **não execute a migração diretamente em produção**. Faça backup e planeje a reconciliação do histórico `__EFMigrationsHistory` e do esquema antes de aplicar.

```bash
dotnet tool install --global dotnet-ef --version 9.0.10
dotnet ef database update --project FilmesAPI/FilmesAPI.csproj
```

## Testes

```bash
dotnet test FilmesAPI.Tests/FilmesAPI.Tests.csproj
```

## Docker

```bash
docker build -t filmes-api .
docker run -p 8080:8080 -e ConnectionStrings__FilmeConnection='server=host.docker.internal;database=filmeDb;user=app;password=SUA_SENHA' filmes-api
```

## Segurança e limitações

- Nunca inclua credenciais reais no repositório. Uma credencial existia no histórico anterior; faça rotação se ainda estiver válida.
- Migração para .NET 10 e provedor Pomelo precisa de validação de build, testes e banco real antes de merge.
- A rota antiga `/Filme` foi substituída por `/api/filmes`.
- O projeto demonstra separação básica Controller/Service/Persistência, não uma implementação completa de DDD ou microserviços.
