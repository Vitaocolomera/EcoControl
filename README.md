# Projeto - Cidades ESG Inteligentes

## EcoControl

O EcoControl é uma API criada para acompanhar o consumo de energia de equipamentos industriais. O sistema mantém o cadastro dos equipamentos, seus horários de funcionamento, leituras de sensores, registros de desligamento e informações de consumo e economia de energia.

Nesta entrega o projeto foi adaptado para trabalhar com um fluxo de DevOps, usando Docker, testes automatizados e GitHub Actions para as etapas de build, validação e deploy.

**Integrante:** Victor Hugo Bastos Colomera - RM 566171

## Tecnologias utilizadas

- C# e .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Oracle Database
- xUnit
- Swagger / OpenAPI
- Docker
- Docker Compose
- GitHub Actions
- Docker Hub
- Azure App Service
- Postman

## Estrutura do projeto

```text
EcoControl/
├── .github/
│   └── workflows/
│       ├── ci.yml
│       ├── deploy-staging.yml
│       └── deploy-production.yml
├── docs/
│   └── evidencias/
├── postman/
├── scripts/
├── src/
│   └── EcoControl.API/
├── tests/
│   └── EcoControl.Tests/
├── .dockerignore
├── .env.example
├── .gitignore
├── Dockerfile
├── docker-compose.yml
├── EcoControl.sln
├── README.md
├── CHECKLIST.md
└── Documentacao_EcoControl.pdf
```

## Como executar localmente com Docker

### Pré-requisitos

É necessário ter o Docker Desktop com Docker Compose disponível no Windows.

Na raiz do projeto, crie o arquivo local de variáveis a partir do exemplo:

```powershell
Copy-Item .env.example .env
```

Depois suba a API e o banco:

```powershell
docker compose up -d --build
```

Para verificar os containers:

```powershell
docker compose ps
```

Endereços usados no ambiente local:

- Swagger: `http://localhost:8080/swagger`
- Healthcheck: `http://localhost:8080/health`
- Equipamentos: `http://localhost:8080/api/Equipamentos`

Para parar o ambiente:

```powershell
docker compose down
```

O volume do Oracle é mantido. Caso seja necessário apagar também os dados locais:

```powershell
docker compose down -v
```

## Containerização

O Dockerfile usa duas etapas. Na primeira, a imagem do SDK .NET 8 faz o restore e publica a aplicação. Na segunda, a API é executada em uma imagem menor com o runtime ASP.NET Core 8.

O `docker-compose.yml` sobe dois serviços:

- `oracle-db`: banco Oracle usado pela aplicação;
- `ecocontrol-api`: API do EcoControl na porta 8080.

Também são usados um volume persistente para o banco e uma rede própria para comunicação entre os containers. A API só inicia depois do healthcheck do Oracle.

As credenciais locais ficam no arquivo `.env`, que não é versionado. O repositório mantém apenas o `.env.example`.

## Testes automatizados

Os testes estão em `tests/EcoControl.Tests` e usam xUnit. Durante a execução dos testes, o Oracle é substituído por um banco InMemory do Entity Framework Core. Dessa forma, os testes do pipeline não dependem de um banco externo.

Para executar localmente:

```powershell
dotnet restore EcoControl.sln
dotnet build EcoControl.sln --configuration Release --no-restore
dotnet test EcoControl.sln --configuration Release --no-build
```

Também existe um script de validação:

```powershell
.\scripts\validar.ps1
```

## Pipeline CI/CD

Os workflows ficam em `.github/workflows/`.

### CI - `ci.yml`

O CI roda em Pull Requests para `develop` e `main` e também em pushes para `develop`.

Ordem das etapas:

1. checkout do código;
2. configuração do .NET 8;
3. restore das dependências;
4. build em Release;
5. execução dos testes;
6. upload dos resultados de teste;
7. validação do build da imagem Docker.

Se o build ou os testes falharem, a validação do Docker não avança.

### Staging - `deploy-staging.yml`

O workflow de staging é executado quando há push na branch `develop`. Ele executa build, testes, cria a imagem Docker, publica no Docker Hub e faz o deploy no ambiente de staging.

Tags usadas:

```text
staging
staging-<commit-sha>
```

### Produção - `deploy-production.yml`

O workflow de produção roda em pushes para `main`. Antes do deploy, build e testes são executados novamente.

Tags usadas:

```text
latest
prod-<commit-sha>
```

A tag com o SHA do commit permite identificar a versão exata do código usada em cada imagem.

## Fluxo de branches

```text
feature/*
   |
   v
Pull Request
   |
   v
develop ----> staging
   |
   v
Pull Request
   |
   v
main -------> produção
```

A branch `develop` representa a versão que vai para homologação. Depois da validação, as alterações são promovidas para `main`, que representa a versão de produção.

## Variáveis e secrets

Credenciais não ficam gravadas no código-fonte. No ambiente local são usadas variáveis do `.env`. Nos workflows, os dados de acesso ao Docker Hub e ao Azure são lidos a partir dos GitHub Secrets.

Os principais secrets previstos são:

```text
DOCKERHUB_USERNAME
DOCKERHUB_TOKEN
AZURE_WEBAPP_NAME_STAGING
AZURE_WEBAPP_PUBLISH_PROFILE_STAGING
AZURE_WEBAPP_NAME_PRODUCTION
AZURE_WEBAPP_PUBLISH_PROFILE_PRODUCTION
```

## Prints do funcionamento

As evidências devem ficar em `docs/evidencias/`. Os principais prints esperados são:

- build e testes;
- `docker compose ps`;
- Swagger e `/health` local;
- pipeline de CI;
- deploy de staging;
- aplicação em staging;
- deploy de produção;
- aplicação em produção.

## Desafios encontrados

Durante a adaptação foram tratados alguns pontos do projeto original:

- as credenciais que estavam no arquivo de configuração foram movidas para variáveis de ambiente;
- os testes deixaram de depender do Oracle externo e passaram a usar InMemory;
- foi criado healthcheck para controlar a ordem de inicialização dos containers;
- as imagens de staging e produção passaram a ser identificadas também pelo SHA do commit.

## Checklist

O checklist da entrega está no arquivo `CHECKLIST.md` e também no final da documentação em PDF.
