# FilmesAPI — API REST .NET 10

[![CI](https://github.com/devbastos/FilmesAPI/actions/workflows/ci.yml/badge.svg)](https://github.com/devbastos/FilmesAPI/actions/workflows/ci.yml)
[![MySQL integration](https://github.com/devbastos/FilmesAPI/actions/workflows/mysql-integration.yml/badge.svg)](https://github.com/devbastos/FilmesAPI/actions/workflows/mysql-integration.yml)
[![Build](https://github.com/devbastos/FilmesAPI/actions/workflows/build.yml/badge.svg)](https://github.com/devbastos/FilmesAPI/actions/workflows/build.yml)

Projeto de portfólio com ASP.NET Core 10, EF Core, MySQL, Docker Compose, JWT, paginação, observabilidade e testes de integração reais.

## Arquitetura

Veja [diagrama e decisões técnicas](docs/ARCHITECTURE.md).

## Executar com Docker

```bash
cp .env.example .env
# Defina MYSQL_PASSWORD e JWT_KEY com valores fortes no .env
docker compose up -d mysql
docker compose --profile setup run --rm migrate
docker compose --profile app up -d --build api
```

Swagger: http://localhost:8080/swagger — Health check: http://localhost:8080/health.

## Endpoints

| Método | Rota | Autenticação | Resultado |
|---|---|---|---|
| GET | /api/filmes?page=1&pageSize=20 | Pública | `items, page, pageSize, totalItems, totalPages` |
| GET | /api/filmes/{id} | Pública | Filme ou 404 |
| POST | /api/filmes | Bearer JWT | 201 + filme |
| PUT | /api/filmes/{id} | Bearer JWT | 204 ou 404 |
| DELETE | /api/filmes/{id} | Bearer JWT | 204 ou 404 |
| GET | /health | Pública | Saúde da aplicação/banco |

Exemplo de leitura:
```bash
curl "http://localhost:8080/api/filmes?page=1&pageSize=10"
```

Exemplo de escrita com **token emitido externamente**:
```bash
curl -X POST "http://localhost:8080/api/filmes" \
  -H "Authorization: Bearer SEU_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"titulo":"Interestelar","diretor":"Christopher Nolan","genero":"Ficção científica","duracao":169}'
```

A API **não emite tokens**. Valida assinatura HMAC, issuer, audience e expiração. Configure `Jwt__Key` (mínimo 32 bytes), `Jwt__Issuer` e `Jwt__Audience` no ambiente. Nunca comite chaves reais.

## Testes

```bash
dotnet test FilmesAPI.Tests/FilmesAPI.Tests.csproj
```

Testes MySQL usam Testcontainers e exigem Docker em execução. GitHub Actions executa build, testes unitários e integração.

## Observabilidade

Logs estruturados com duração, método, caminho e status HTTP; cabeçalho `X-Correlation-ID` para rastrear requisições e health check em `/health`.

## Segurança e evolução

Este é um projeto de estudo e portfólio, não uma plataforma de identidade. Para produção: OIDC/JWKS, autorização por escopo, métricas e traces OpenTelemetry, rate limiting e gestão segura de segredos. As migrations iniciais destinam-se a **banco novo**; não execute diretamente sobre banco legado.
