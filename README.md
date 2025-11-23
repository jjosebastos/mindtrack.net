# 🧠 MindTrack API

MindTrack é uma solução de backend desenvolvida em **.NET** para
monitoramento e promoção do bem-estar corporativo.\
O sistema permite registro de check-ins de humor, gerenciamento de
planos de bem-estar, notificações, recomendações via IA e dashboard
integrado ao **Oracle**.

------------------------------------------------------------------------

## 📋 Índice

-   Objetivo da Solução
-   Tecnologias Utilizadas
-   Arquitetura e Padrões
-   Versionamento da API
-   Configuração e Execução
-   Documentação das Rotas (Endpoints)
-   Testes
-   Dependências Principais

------------------------------------------------------------------------

## 🎯 Objetivo da Solução

O **MindTrack** permite às empresas acompanhar indicadores emocionais de
seus colaboradores, identificando:

-   Tendências de humor\
-   Níveis de estresse\
-   Recomendações proativas (cursos e competências)\
-   Planos personalizados de bem-estar\
-   Informações consolidadas via dashboard

------------------------------------------------------------------------

## 🚀 Tecnologias Utilizadas

-   .NET 8/9\
-   ASP.NET Core Web API\
-   Entity Framework Core\
-   Oracle Database (PL/SQL)\
-   ASP.NET API Versioning\
-   Swagger/OpenAPI\
-   xUnit & Moq

------------------------------------------------------------------------

## 🏗 Arquitetura e Padrões

-   **Controller** --- Entrada de requisições HTTP\
-   **Service** --- Regras de negócio\
-   **Repository** --- Acesso a dados (EF + ADO.NET)\
-   **Models/DTO** --- Estruturas de dados\
-   **HATEOAS** --- Navegação assistida via links

------------------------------------------------------------------------

## 📌 Versionamento da API

Versões via URL:

    /api/v{version}/[controller]

Exemplos:

    GET /api/v1/Notificacao
    GET /api/v2/Notificacao

Controladores:

``` csharp
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
```

------------------------------------------------------------------------

## ⚙ Configuração e Execução

### Pré-requisitos

-   .NET SDK\
-   Banco Oracle com package **mindtrack_pkg**

### Passos

``` bash
git clone https://github.com/seu-usuario/mindtrack.git
cd mindtrack
dotnet restore
dotnet run --project mindtrack
```

Acesse o Swagger:

    https://localhost:7182/swagger

------------------------------------------------------------------------

## 📚 Documentação das Rotas (Endpoints)

A seguir os recursos da versão **v1**.

------------------------------------------------------------------------

# 🔔 Notificações --- `/api/v1/Notificacao`

Gerenciamento de alertas e mensagens.

  Método   Rota    Descrição
  -------- ------- ---------------------------
  POST     /       Cria uma nova notificação
  PUT      /{id}   Atualiza uma notificação
  GET      /{id}   Busca uma notificação
  DELETE   /{id}   Remove uma notificação

------------------------------------------------------------------------

# 🧘 Planos de Bem-Estar --- `/api/v1/PlanoBemEstar`

Gestão de metas e planos de saúde mental.

  Método   Rota    Descrição
  -------- ------- -------------------------
  POST     /       Registra novo plano
  PUT      /{id}   Atualiza dados do plano
  GET      /{id}   Consulta um plano
  DELETE   /{id}   Exclui o plano

------------------------------------------------------------------------

# 🤖 Recomendações de IA --- `/api/v1/RecomendacaoIA`

Insights automáticos baseados nos check-ins do usuário.

  Método   Rota    Descrição
  -------- ------- ------------------------
  POST     /       Gera nova recomendação
  PUT      /{id}   Atualiza recomendação
  GET      /{id}   Consulta recomendação
  DELETE   /{id}   Remove recomendação

------------------------------------------------------------------------

# 📊 Dashboard --- `/api/v1/Dashboard`

Integra PL/SQL Oracle de alta performance.

  -----------------------------------------------------------------------
  Método            Rota                         Descrição
  ----------------- ---------------------------- ------------------------
  GET               /{idUser}                    Retorna JSON consolidado
                                                 com perfil,
                                                 competências, cursos e
                                                 índice de humor

  -----------------------------------------------------------------------

------------------------------------------------------------------------

## 🧪 Testes

O projeto possui testes unitários com **xUnit** e **Moq**.

### Executar testes:

``` bash
dotnet test
```

### Cobertura:

-   Services (mocks de repositório)\
-   Controllers (mocks de service)\
-   Casos: sucesso, NotFound, BadRequest

------------------------------------------------------------------------

## 📦 Dependências Principais

-   Asp.Versioning.Mvc\
-   Microsoft.EntityFrameworkCore\
-   Oracle.EntityFrameworkCore\
-   Swashbuckle.AspNetCore\
-   Moq\
-   xUnit

------------------------------------------------------------------------

Desenvolvido para o projeto **MindTrack** 🧠
