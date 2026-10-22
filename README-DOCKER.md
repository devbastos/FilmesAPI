# Executar com Docker

Requisitos: Docker com Compose.

1. Copie `.env.example` para `.env` e escolha senhas locais (o arquivo `.env` não deve ser versionado).
2. Inicie o MySQL:
   ```bash
   docker compose up -d mysql
   ```
3. Crie o esquema no banco **novo**:
   ```bash
   docker compose --profile setup run --rm migrate
   ```
4. Suba a API:
   ```bash
   docker compose --profile app up -d --build api
   ```
5. Acesse `http://localhost:8080/swagger` (Swagger habilitado no ambiente Development).

O MySQL local é exposto apenas em `127.0.0.1:3307`, evitando conflito com um MySQL existente em 3306.

## Testes de integração

```bash
dotnet test FilmesAPI.Tests/FilmesAPI.Tests.csproj --filter FullyQualifiedName~FilmeMySqlIntegrationTests
```

Os testes usam Testcontainers e precisam de Docker ativo. O container é temporário e isolado do banco do Compose.

**Aviso:** o comando `docker compose down -v` apaga os dados locais. Não execute em bancos com informações que queira preservar.
