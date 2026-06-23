# FinControl — Fluxo principal e requisitos funcionais iniciais

## 1. Objetivo do sistema

O FinControl será uma plataforma SaaS para empresas controlarem seus clientes, cobranças e pagamentos.

Uma organização poderá possuir uma matriz e suas respectivas filiais. Cada empresa terá usuários responsáveis por cadastrar clientes, criar cobranças, registrar pagamentos e acompanhar valores pendentes, pagos ou vencidos.

O sistema também deverá controlar o contrato e o pacote contratado pela organização, permitindo somente o acesso às funcionalidades disponíveis naquele plano.

---

## 2. Participantes do sistema

### Organização

Empresa que contrata e utiliza o FinControl.

Uma organização poderá ser:

* uma empresa matriz;
* uma empresa sem filiais;
* uma filial vinculada a uma matriz.

### Usuário administrador

Pessoa responsável por administrar a organização dentro do FinControl.

O administrador poderá:

* cadastrar colaboradores;
* consultar dados da organização;
* administrar filiais;
* consultar contrato e pacote;
* acompanhar clientes, cobranças e pagamentos.

### Colaborador financeiro

Pessoa que trabalha no setor financeiro da organização e utiliza o sistema para:

* cadastrar clientes;
* criar cobranças;
* registrar pagamentos;
* consultar cobranças pendentes, pagas e vencidas.

### Cliente

Pessoa física ou jurídica que possui uma cobrança criada pela organização.

---

# 3. Requisitos funcionais iniciais

## RF01 — Cadastro de nova organização

**Descrição:**
O sistema deverá permitir o cadastro de uma nova organização cliente do FinControl por meio de um formulário de adesão.

**Informações necessárias:**

* CNPJ;
* razão social;
* nome fantasia;
* e-mail de contato;
* data de início do contrato;
* data de término do contrato;
* pacote contratado.

Ao concluir o cadastro da organização, deverá ser criado um primeiro usuário com perfil de administrador.

A organização não realizará login diretamente. O acesso será feito pelo usuário administrador vinculado a ela.

---

## RF02 — Cadastro e vínculo de filiais

**Descrição:**
O sistema deverá permitir que uma organização matriz cadastre e vincule suas filiais.

Cada filial deverá possuir seus próprios dados de identificação e deverá estar relacionada a uma única matriz.

A matriz poderá consultar informações consolidadas de suas filiais.

Uma filial somente poderá visualizar e administrar seus próprios clientes, cobranças, pagamentos e usuários, salvo quando existir uma permissão específica.

Uma filial não poderá acessar diretamente os dados de outra filial.

---

## RF03 — Controle de contrato e pacote

**Descrição:**
O sistema deverá registrar o contrato e o pacote adquirido pela organização.

O contrato deverá possuir:

* data de início;
* data de término;
* situação do contrato;
* pacote contratado.

Os estados iniciais do contrato serão:

* ativo;
* suspenso;
* encerrado.

O pacote deverá determinar quais funcionalidades estarão disponíveis para a organização.

Quando a organização tentar acessar uma funcionalidade que não pertence ao pacote contratado, o sistema deverá impedir a operação e informar que o recurso não está disponível.

Nesta primeira versão, o sistema não realizará cobrança real pela assinatura do FinControl.

---

## RF04 — Autenticação do usuário administrador

**Descrição:**
O sistema deverá permitir que o usuário administrador acesse o FinControl utilizando suas credenciais.

O login deverá ser realizado por uma pessoa vinculada à organização, e não pela empresa diretamente.

Após a autenticação, o sistema deverá identificar:

* o usuário autenticado;
* a organização à qual ele pertence;
* seu perfil de acesso;
* as funcionalidades disponíveis no pacote contratado.

---

## RF05 — Cadastro de colaboradores

**Descrição:**
O usuário administrador deverá conseguir cadastrar os colaboradores que terão acesso ao FinControl.

**Informações necessárias:**

* registro do colaborador;
* nome completo;
* e-mail;
* perfil de acesso;
* organização ou filial de atuação;
* situação ativa ou inativa.

O vínculo com a organização deverá ser definido pelo administrador responsável pelo cadastro.

O colaborador não deverá informar ou escolher livremente uma organização que não esteja sob responsabilidade do administrador.

---

## RF06 — Autenticação dos colaboradores

**Descrição:**
O sistema deverá permitir que colaboradores cadastrados e ativos façam login utilizando suas próprias credenciais.

Depois da autenticação, o colaborador somente poderá acessar informações pertencentes à organização ou filial à qual está vinculado.

Colaboradores inativos não poderão acessar o sistema.

---

## RF07 — Cadastro de clientes

**Descrição:**
O sistema deverá permitir que um colaborador autorizado cadastre clientes que poderão receber cobranças.

**Informações necessárias:**

* nome ou razão social;
* CPF ou CNPJ;
* e-mail;
* telefone;
* situação ativa ou inativa.

O cliente deverá ser automaticamente vinculado à organização ou filial do usuário autenticado.

O sistema deverá impedir o cadastro duplicado do mesmo CPF ou CNPJ dentro da mesma organização ou filial.

Um cliente pertencente a uma empresa não poderá ser consultado por usuários de outra empresa.

---

## RF08 — Registro de cobrança

**Descrição:**
O sistema deverá permitir que um colaborador autorizado crie uma cobrança para um cliente cadastrado.

**Informações necessárias:**

* cliente;
* descrição da cobrança;
* valor total;
* data de criação;
* data de vencimento;
* referência contratual, quando aplicável.

Ao ser criada, a cobrança deverá possuir:

* estado inicial pendente;
* saldo correspondente ao valor total;
* nenhum pagamento registrado.

O usuário não precisará informar manualmente o identificador da organização ou do colaborador responsável. O sistema deverá obter essas informações a partir do usuário autenticado.

---

## RF09 — Registro de pagamento

**Descrição:**
O sistema deverá permitir que um colaborador autorizado registre o pagamento de uma cobrança.

Cada pagamento deverá ser registrado individualmente, contendo:

* valor pago;
* data do pagamento;
* forma de pagamento;
* referência externa, quando existir;
* usuário responsável pelo registro.

O sistema não deverá controlar os pagamentos apenas por meio de um campo acumulado chamado “quanto foi pago”.

O valor total pago deverá ser determinado a partir dos pagamentos válidos registrados para a cobrança.

O sistema deverá impedir pagamentos:

* com valor igual ou inferior a zero;
* superiores ao saldo restante;
* relacionados a uma cobrança cancelada;
* duplicados quando possuírem a mesma referência externa.

---

## RF10 — Pagamento parcial

**Descrição:**
O sistema deverá permitir o registro de um pagamento inferior ao saldo da cobrança.

Quando isso acontecer, o sistema deverá:

* registrar o pagamento individualmente;
* calcular o total já pago;
* calcular o saldo restante;
* alterar automaticamente o estado da cobrança para parcialmente paga;
* registrar a data da operação.

O colaborador não deverá alterar manualmente o estado da cobrança.

### Exemplo

Uma cobrança possui valor total de R$ 1.000.

Após o registro de um pagamento de R$ 400:

* total pago: R$ 400;
* saldo restante: R$ 600;
* estado: parcialmente paga.

---

## RF11 — Pagamento integral

**Descrição:**
Quando a soma dos pagamentos válidos atingir exatamente o valor total da cobrança, o sistema deverá alterar automaticamente o estado para paga.

### Exemplo

Uma cobrança de R$ 1.000 já recebeu um pagamento de R$ 400.

Após o registro de um segundo pagamento de R$ 600:

* total pago: R$ 1.000;
* saldo restante: R$ 0;
* estado: paga.

Uma cobrança paga não poderá receber novos pagamentos enquanto não existir uma regra específica que permita essa operação.

---

## RF12 — Cobrança vencida

**Descrição:**
Uma cobrança deverá ser considerada vencida quando:

* a data de vencimento tiver sido ultrapassada;
* ainda existir saldo pendente;
* a cobrança não estiver cancelada.

A identificação do vencimento deverá ser feita automaticamente pelo sistema.

O colaborador não deverá alterar manualmente uma cobrança para vencida.

Uma cobrança vencida poderá continuar recebendo pagamentos.

Quando o valor completo for pago, ela deverá assumir automaticamente o estado paga.

---

## RF13 — Isolamento entre organizações

**Descrição:**
O sistema deverá garantir que os dados de organizações diferentes nunca sejam misturados.

Um usuário somente poderá acessar:

* sua organização;
* a filial à qual está vinculado;
* outras filiais quando possuir uma permissão específica;
* informações consolidadas quando for administrador autorizado da matriz.

O sistema deverá impedir o acesso indevido mesmo quando o usuário tentar informar diretamente o identificador de um cliente, cobrança ou pagamento pertencente a outra organização.

---

# 4. Fluxo principal do FinControl

O fluxo inicial do sistema será:

1. Uma organização contrata o FinControl.
2. A organização informa seus dados, contrato e pacote.
3. Um primeiro usuário administrador é criado.
4. O administrador acessa o sistema.
5. O administrador cadastra filiais, quando existirem.
6. O administrador cadastra colaboradores.
7. Um colaborador financeiro acessa o sistema.
8. O colaborador cadastra um cliente.
9. O colaborador cria uma cobrança para esse cliente.
10. A cobrança inicia como pendente.
11. O colaborador registra um pagamento parcial.
12. O sistema calcula o saldo e altera a cobrança para parcialmente paga.
13. O colaborador registra o valor restante.
14. O sistema calcula novamente o saldo e altera a cobrança para paga.
15. Caso o vencimento seja ultrapassado com saldo pendente, o sistema considera a cobrança vencida automaticamente.

---

# 5. Regras principais desta primeira modelagem

* Empresas não realizam login; usuários realizam login.
* Cada usuário deve estar vinculado a uma organização ou filial.
* Cada cliente pertence a uma organização ou filial.
* Cada cobrança pertence a um cliente.
* Cada pagamento pertence a uma cobrança.
* Pagamentos devem ser armazenados individualmente.
* O estado da cobrança deve ser determinado pelas regras do sistema.
* O saldo de uma cobrança nunca poderá ficar negativo.
* Uma cobrança cancelada não poderá receber pagamentos.
* Uma empresa não poderá acessar dados de outra empresa.
* A matriz poderá visualizar dados consolidados das filiais autorizadas.
* Uma filial não poderá visualizar os dados de outra filial.
* O pacote contratado deverá limitar as funcionalidades disponíveis.
* Operações financeiras não deverão ser apagadas fisicamente.

---

# 6. Fora do escopo inicial

Mesmo mantendo contratos, pacotes, matrizes e filiais, os seguintes itens não farão parte da primeira entrega:

* pagamento real da assinatura do FinControl;
* renovação automática de contrato;
* emissão de nota fiscal pelo FinControl;
* integração com gateway de assinatura;
* quantidade ilimitada de pacotes configuráveis;
* compartilhamento automático de clientes entre filiais;
* conversão de moedas;
* pagamentos superiores ao saldo;
* divisão de um pagamento entre várias cobranças.
