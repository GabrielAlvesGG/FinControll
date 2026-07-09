# 03 — Modelagem Conceitual das Entidades do FinControl

## 1. Objetivo da modelagem

Este documento tem como objetivo definir as principais entidades do FinControl, explicando o que cada uma representa no negócio, qual é sua responsabilidade e como elas se relacionam.

Nesta etapa, o foco ainda não é banco de dados, código, endpoints ou estrutura técnica. O foco é entender os conceitos principais do sistema antes da implementação.

O FinControl será um sistema SaaS utilizado por organizações para controlar clientes, cobranças e pagamentos. Como várias organizações poderão usar a mesma aplicação, os dados de uma empresa não poderão ser misturados com os dados de outra.

---

# 2. Entidades principais

## 2.1 Organização

A organização representa a empresa que contrata o FinControl para controlar seus clientes, cobranças e pagamentos.

Ela é a principal entidade do sistema, pois todos os dados financeiros devem estar vinculados a uma organização. Sem uma organização cadastrada, não existe cliente, cobrança, pagamento ou usuário dentro do FinControl.

A organização poderá representar uma matriz ou uma filial. Quando for matriz, poderá possuir filiais vinculadas. Quando for filial, deverá estar vinculada a uma matriz.

A principal responsabilidade da organização é separar os dados dentro do sistema. Usuários, clientes, cobranças e pagamentos de uma organização não podem ser acessados por outra organização sem permissão.

A organização também estará relacionada a um contrato e a um plano, que definirão se ela pode utilizar o sistema e quais funcionalidades estarão disponíveis.

### Relacionamentos

Uma organização pode possuir vários usuários.

Uma organização pode possuir vários clientes.

Uma organização pode possuir várias cobranças.

Uma organização matriz pode possuir várias filiais.

Uma filial pertence a uma única matriz.

Uma organização pode possuir contratos ao longo do tempo.

### Regras importantes

Uma organização não realiza login diretamente. Quem realiza login é um usuário vinculado a ela.

Os dados de uma organização não podem ser acessados por usuários de outra organização.

A matriz poderá visualizar dados consolidados das filiais.

Uma filial não poderá visualizar dados de outra filial.

Uma filial não poderá visualizar dados da matriz, exceto quando existir uma permissão específica.

---

## 2.2 Contrato

O contrato representa o vínculo entre a organização e o FinControl.

Ele define se a organização possui permissão para utilizar o sistema, considerando a data de início, a data de término e a situação atual do contrato.

O contrato poderá estar ativo, suspenso ou encerrado.

Quando o contrato estiver ativo, a organização poderá utilizar as funcionalidades disponíveis no plano contratado.

Quando o contrato estiver suspenso ou encerrado, o sistema poderá bloquear operações como cadastro de clientes, criação de cobranças e registro de pagamentos.

A principal responsabilidade do contrato é controlar a validade do uso do FinControl pela organização.

### Relacionamentos

Uma organização pode possuir vários contratos ao longo do tempo.

Um contrato pertence a uma única organização.

Um contrato está vinculado a um único plano.

Um plano pode estar vinculado a vários contratos de organizações diferentes.

### Regras importantes

Uma organização não deverá possuir mais de um contrato ativo ao mesmo tempo.

Contratos antigos deverão permanecer registrados para histórico.

Um contrato encerrado não deve ser apagado fisicamente.

Contrato suspenso ou encerrado poderá bloquear operações financeiras.

---

## 2.3 Plano

O plano representa o conjunto de funcionalidades disponíveis para uma organização.

Cada plano poderá definir limites e permissões diferentes. Por exemplo, um plano básico poderá permitir clientes, cobranças e pagamentos. Um plano mais avançado poderá permitir filiais, dashboard, relatórios ou importação de extrato.

O plano não representa uma cobrança feita ao cliente final da organização. Ele representa o pacote contratado pela própria organização que usa o FinControl.

A principal responsabilidade do plano é limitar ou liberar funcionalidades conforme o pacote contratado.

### Relacionamentos

Um plano pode estar relacionado a vários contratos.

Um contrato possui um único plano.

Uma organização utiliza o plano por meio do contrato ativo.

### Regras importantes

O sistema deverá impedir o uso de funcionalidades que não fazem parte do plano contratado.

As filiais herdarão o plano contratado pela matriz.

O plano poderá definir limites, como quantidade de clientes, quantidade de usuários ou acesso a módulos específicos.

---

## 2.4 Usuário

O usuário representa uma pessoa autorizada a acessar o FinControl em nome de uma organização.

Nesta primeira versão, existirão dois tipos principais de usuário: administrador da organização e colaborador financeiro.

O administrador poderá gerenciar usuários, acompanhar dados da organização, visualizar informações das filiais permitidas e acessar configurações disponíveis no plano contratado.

O colaborador financeiro poderá cadastrar clientes, criar cobranças, registrar pagamentos e consultar informações financeiras, conforme suas permissões.

O usuário sempre deverá estar vinculado a uma organização ou filial. Ele não poderá acessar dados de organizações às quais não pertence.

Nesta primeira versão, o cliente que recebe cobranças não será considerado usuário do sistema.

### Relacionamentos

Um usuário pertence a uma organização ou filial.

Uma organização pode possuir vários usuários.

Um usuário pode criar cobranças.

Um usuário pode registrar pagamentos.

Um usuário pode gerar registros de histórico ao executar ações importantes.

### Regras importantes

Usuários inativos não poderão acessar o sistema.

Usuários só poderão visualizar dados da organização ou filial à qual pertencem.

Administradores da matriz poderão visualizar dados consolidados das filiais.

Colaboradores de uma filial não poderão visualizar dados de outra filial.

O cliente cobrado não realiza login nesta primeira versão.

---

## 2.5 Cliente

O cliente representa uma pessoa física ou jurídica atendida pela organização e que poderá receber cobranças.

Ele é o cliente da empresa que usa o FinControl, não o cliente direto do FinControl.

Por exemplo, se a empresa TechFix usa o FinControl e presta serviço para a Loja Central, a TechFix é a organização e a Loja Central é o cliente.

O cliente não realiza login no sistema nesta primeira versão. Ele apenas representa a pessoa ou empresa que possui cobranças em aberto, pagas, vencidas ou canceladas.

A principal responsabilidade do cliente é servir como referência para as cobranças criadas pela organização.

### Relacionamentos

Um cliente pertence a uma organização ou filial.

Uma organização pode possuir vários clientes.

Um cliente pode possuir várias cobranças.

Uma cobrança pertence a um único cliente.

### Regras importantes

O mesmo CPF ou CNPJ não poderá ser cadastrado duas vezes dentro da mesma organização ou filial.

Um cliente de uma organização não poderá ser acessado por usuários de outra organização.

Um cliente poderá ter várias cobranças ao longo do tempo.

Um cliente com cobranças vinculadas não deverá ser excluído fisicamente.

---

## 2.6 Cobrança

A cobrança representa um valor que a organização tem para receber de um cliente.

Ela é uma das entidades centrais do FinControl, pois concentra a regra principal do sistema: controlar valores pendentes, pagos, vencidos e cancelados.

Uma cobrança nasce quando um usuário autorizado registra que determinado cliente precisa pagar um valor por algum serviço, venda ou obrigação financeira.

A cobrança deverá possuir um estado atual. Os estados definidos inicialmente são: pendente, parcialmente paga, paga, vencida e cancelada.

A principal responsabilidade da cobrança é controlar o valor devido, o vencimento, o estado atual e sua relação com os pagamentos recebidos.

### Relacionamentos

Uma cobrança pertence a uma organização ou filial.

Uma cobrança pertence a um único cliente.

Uma cobrança pode possuir vários pagamentos.

Uma cobrança pode possuir vários registros de histórico.

Uma cobrança pode ser criada por um usuário autorizado.

### Regras importantes

Toda cobrança nasce como pendente.

Uma cobrança pode receber pagamentos parciais.

Uma cobrança paga não pode receber novos pagamentos.

Uma cobrança cancelada não pode receber pagamentos.

Uma cobrança vencida ainda pode receber pagamentos.

O usuário não altera manualmente o estado financeiro da cobrança.

O sistema calcula o estado com base nos pagamentos, no vencimento, no saldo e no cancelamento.

O saldo da cobrança será calculado com base nos pagamentos válidos.

---

## 2.7 Pagamento

O pagamento representa um valor recebido para uma cobrança.

Cada pagamento deverá ser registrado individualmente. O sistema não deverá controlar pagamento apenas por um campo acumulado de “quanto foi pago”.

O pagamento informa quanto foi recebido, quando foi recebido, de qual forma foi pago e a qual cobrança ele pertence.

A principal responsabilidade do pagamento é registrar uma movimentação financeira relacionada a uma cobrança.

### Relacionamentos

Um pagamento pertence a uma única cobrança.

Uma cobrança pode possuir vários pagamentos.

Um pagamento pode ser registrado por um usuário autorizado.

Um pagamento pode gerar registros no histórico da cobrança.

### Regras importantes

O pagamento não pode ter valor zero ou negativo.

O pagamento não pode ultrapassar o saldo restante da cobrança.

Um pagamento em cobrança cancelada deve ser impedido.

Um pagamento em cobrança paga deve ser impedido.

Um pagamento poderá ser estornado, mas não apagado fisicamente.

Pagamentos estornados não contam mais para o total pago da cobrança.

Cada pagamento deverá permanecer registrado para histórico e rastreabilidade.

---

## 2.8 Histórico da Cobrança

O histórico da cobrança representa os acontecimentos importantes durante o ciclo de vida de uma cobrança.

Ele não substitui o pagamento. O pagamento é a movimentação financeira. O histórico apenas registra que algo aconteceu.

A principal responsabilidade do histórico é permitir rastreabilidade sobre as mudanças da cobrança.

Exemplos de acontecimentos registrados:

* cobrança criada;
* pagamento registrado;
* cobrança alterada para parcialmente paga;
* cobrança alterada para paga;
* cobrança vencida;
* cobrança cancelada;
* pagamento estornado;
* juros aplicados;
* tentativa de operação inválida relevante.

### Relacionamentos

Uma cobrança pode possuir vários registros de histórico.

Cada registro de histórico pertence a uma única cobrança.

Um histórico pode estar relacionado a um usuário responsável pela ação.

Um histórico pode estar relacionado a um pagamento quando o evento envolver pagamento ou estorno.

### Regras importantes

O histórico não poderá ser apagado por usuários comuns.

O histórico deverá registrar operações relevantes.

O histórico deverá ajudar a entender o que aconteceu com a cobrança ao longo do tempo.

O histórico não deve guardar informações sensíveis em texto aberto.

---

# 3. Relacionamentos gerais do domínio

Uma organização pode possuir várias filiais.

Uma filial pertence a uma única matriz.

Uma organização pode possuir vários usuários.

Um usuário pertence a uma organização ou filial.

Uma organização pode possuir vários clientes.

Um cliente pertence a uma organização ou filial.

Um cliente pode possuir várias cobranças.

Uma cobrança pertence a um único cliente.

Uma cobrança pertence a uma organização ou filial.

Uma cobrança pode possuir vários pagamentos.

Um pagamento pertence a uma única cobrança.

Uma cobrança pode possuir vários registros de histórico.

Um histórico pertence a uma única cobrança.

Uma organização pode possuir vários contratos ao longo do tempo.

Um contrato pertence a uma organização.

Um contrato está vinculado a um plano.

Um plano pode ser utilizado por vários contratos.

---

# 4. Decisões tomadas

A filial será tratada como uma organização vinculada a uma matriz. Dessa forma, matriz e filial usam a mesma estrutura conceitual, evitando duplicação de regras.

A matriz poderá visualizar dados consolidados das filiais.

Uma filial não poderá visualizar dados da matriz ou de outras filiais, exceto quando existir permissão específica.

O cliente pertencerá à organização ou filial que realizou o cadastro.

O mesmo CPF ou CNPJ de cliente não poderá ser duplicado dentro da mesma organização ou filial. Porém, poderá existir em organizações diferentes, pois empresas diferentes podem atender o mesmo cliente.

O plano será contratado pela matriz e herdado pelas filiais.

Uma organização poderá possuir vários contratos ao longo do tempo, mas somente um contrato ativo por vez.

O saldo da cobrança será calculado com base nos pagamentos válidos.

O estado da cobrança será armazenado, mas sempre alterado pelas regras do sistema.

O usuário não poderá alterar manualmente o estado financeiro da cobrança.

Um pagamento pertencerá a uma única cobrança.

Nesta primeira versão, o histórico será focado na cobrança.

O cliente cobrado não será usuário do sistema nesta primeira versão.

---

# 5. Fora do escopo inicial

Nesta primeira versão, não será implementado:

* portal do cliente para consulta de débitos;
* pagamento real online;
* assinatura real do FinControl;
* emissão de nota fiscal;
* integração com gateway de pagamento;
* renovação automática de contrato;
* cobrança automática da mensalidade do plano;
* cliente acessando o sistema com login próprio;
* divisão de um pagamento entre várias cobranças;
* pagamento acima do saldo;
* estorno parcial;
* múltiplos contratos ativos ao mesmo tempo.
