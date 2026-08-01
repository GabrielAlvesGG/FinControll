# 05 — Arquitetura Inicial do FinControl

## 1. Objetivo do documento

Este documento define a arquitetura inicial do projeto FinControl, descrevendo a organização da solução, a responsabilidade de cada camada e as tecnologias que serão utilizadas na fundação do sistema.

O objetivo desta etapa é estabelecer uma base técnica clara antes do início da implementação, evitando mistura de responsabilidades entre regras de negócio, casos de uso, infraestrutura e entrada HTTP.

O FinControl será desenvolvido utilizando uma arquitetura em camadas com inspiração em Clean Architecture, priorizando separação de responsabilidades, baixo acoplamento e facilidade de evolução.

---

## 2. Arquitetura escolhida

A arquitetura escolhida para o projeto será baseada na seguinte divisão:

**API → Application → Domain → Infrastructure**

Cada camada terá uma responsabilidade específica dentro da aplicação.

A intenção é garantir que as regras de negócio fiquem protegidas de detalhes técnicos, como banco de dados, framework web, autenticação, filas, cache ou integrações externas.

---

## 3. Camadas da aplicação

## 3.1 Domain

A camada Domain representará o coração do negócio do FinControl.

Ela deverá conter os principais conceitos do domínio e as regras que não dependem de tecnologia externa.

Nesta camada ficarão as entidades e regras relacionadas a:

* Organização;
* Contrato;
* Plano;
* Usuário;
* Cliente;
* Cobrança;
* Pagamento;
* Histórico da Cobrança;
* regras de estado da cobrança;
* regras de pagamento;
* regras de estorno;
* validações de consistência financeira;
* regras de isolamento conceitual entre organizações.

A camada Domain não deverá depender da API, do banco de dados, do Entity Framework Core ou de qualquer detalhe de infraestrutura.

---

## 3.2 Application

A camada Application será responsável por organizar os casos de uso do sistema.

Ela deverá coordenar as ações da aplicação, utilizando as regras do domínio e solicitando operações externas por meio de abstrações.

Nesta camada ficarão os fluxos relacionados a:

* cadastrar organização;
* autenticar usuário;
* cadastrar colaborador;
* cadastrar cliente;
* criar cobrança;
* registrar pagamento;
* cancelar cobrança;
* estornar pagamento;
* consultar cobranças;
* validar permissões de uso conforme organização, contrato e plano.

A camada Application não deverá conter detalhes de banco de dados, controllers ou configurações de infraestrutura.

Seu papel será representar a intenção da aplicação e garantir que cada caso de uso siga as regras definidas no domínio.

---

## 3.3 Infrastructure

A camada Infrastructure será responsável pelos detalhes técnicos necessários para a aplicação funcionar.

Nesta camada ficarão as implementações relacionadas a:

* Entity Framework Core;
* SQL Server;
* persistência de dados;
* repositórios;
* configurações de mapeamento;
* acesso a serviços externos;
* integração futura com Redis;
* integração futura com RabbitMQ;
* implementação futura de envio de notificações;
* implementação futura de serviços de armazenamento, caso necessário.

A Infrastructure poderá depender da camada Domain e da camada Application, pois ela implementará os contratos necessários para que os casos de uso funcionem.

As regras de negócio não deverão ser implementadas diretamente nesta camada.

---

## 3.4 API

A camada API será a entrada HTTP do sistema.

Ela será responsável por receber requisições, validar o acesso inicial, acionar os casos de uso da camada Application e devolver respostas ao cliente da API.

Nesta camada ficarão:

* controllers;
* configuração da aplicação;
* autenticação;
* autorização;
* tratamento padronizado de erros;
* documentação com Swagger;
* configuração de logs;
* configuração de injeção de dependência;
* exposição dos endpoints REST.

A API não deverá conter regras de negócio complexas.

Seu papel será atuar como uma porta de entrada para a aplicação, delegando o comportamento principal para a camada Application.

---

## 4. Direção das dependências

A direção das dependências deverá respeitar a separação entre as camadas.

A regra principal será:

**Domain não depende de nenhuma camada.**

**Application depende de Domain.**

**Infrastructure depende de Application e Domain.**

**API depende de Application e Infrastructure.**

Essa decisão garante que o domínio permaneça independente dos detalhes técnicos da aplicação.

As regras financeiras, estados de cobrança, pagamentos e estornos não deverão depender diretamente de banco de dados, controllers, frameworks ou serviços externos.

---

## 5. Tecnologias aprovadas para a fundação

As tecnologias iniciais do projeto serão:

* ASP.NET Core Web API;
* C# moderno;
* Entity Framework Core;
* SQL Server;
* JWT para autenticação;
* Swagger para documentação da API;
* Serilog para logs estruturados.

Essas tecnologias serão suficientes para construir a base inicial do sistema, incluindo autenticação, organização, usuários, clientes, cobranças e pagamentos.

---

## 6. Tecnologias planejadas para fases futuras

Algumas tecnologias fazem parte do objetivo profissional do projeto, mas não serão implementadas no início.

Ficarão para fases posteriores:

* Docker;
* RabbitMQ;
* Redis;
* Workers;
* processamento assíncrono;
* cache;
* CI/CD;
* observabilidade avançada;
* testes de concorrência;
* integração simulada com provedor externo.

Essa separação evita que a fundação do projeto fique excessivamente complexa antes de o núcleo da aplicação estar estável.

---

## 7. Primeira área de implementação

A primeira área técnica a ser implementada será:

**Organização + Usuário + Autenticação básica**

Essa decisão foi tomada porque, antes de criar clientes, cobranças e pagamentos, o sistema precisa identificar:

* quem está acessando;
* a qual organização o usuário pertence;
* qual perfil o usuário possui;
* quais dados esse usuário pode acessar;
* se a organização possui permissão para utilizar o sistema.

Essa base será essencial para garantir o isolamento entre organizações e preparar o sistema para os próximos módulos.

---

## 8. Ordem inicial da Fase 2

A Fase 2 será iniciada com a fundação técnica do projeto.

A ordem planejada será:

* definição da arquitetura inicial;
* criação da estrutura da solution;
* criação dos projetos base;
* configuração inicial da API;
* configuração inicial do banco de dados;
* criação da base de organização;
* criação da base de usuários;
* autenticação inicial;
* autorização básica;
* isolamento por organização.

Somente depois dessa fundação serão iniciados os módulos de clientes, cobranças e pagamentos.

---

## 9. Regras arquiteturais iniciais

As principais regras arquiteturais do projeto serão:

* controllers não deverão conter regra de negócio;
* regras financeiras deverão ficar protegidas no domínio;
* casos de uso deverão ficar na camada Application;
* acesso ao banco deverá ficar na camada Infrastructure;
* a API deverá apenas receber requisições e acionar a aplicação;
* o domínio não deverá depender do Entity Framework Core;
* o domínio não deverá depender do ASP.NET Core;
* integrações externas deverão ser abstraídas;
* tecnologias futuras não deverão ser adicionadas antes da fundação estar estável.

---

## 10. Fora do escopo desta tarefa

Nesta tarefa não será criado:

* código C#;
* classes;
* controllers;
* endpoints;
* migrations;
* entidades de banco;
* Docker;
* RabbitMQ;
* Redis;
* testes automatizados;
* pipelines de CI/CD.

O objetivo deste documento é apenas registrar a decisão arquitetural inicial do projeto.

---

## 11. Resultado esperado

Ao final desta etapa, deverá estar claro como o FinControl será organizado tecnicamente.

A arquitetura definida deverá permitir que o projeto evolua de forma controlada, mantendo separação entre negócio, aplicação, infraestrutura e entrada HTTP.

Essa fundação será usada como base para a próxima etapa, que será a criação da estrutura inicial da solution.
