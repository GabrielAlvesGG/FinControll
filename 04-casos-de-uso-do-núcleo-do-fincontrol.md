# 04 — Casos de uso do núcleo do FinControl

## UC01 — Cadastrar organização

**Ator principal:**
Administrador inicial ou responsável pelo cadastro da organização.

**Objetivo:**
Cadastrar uma nova empresa que utilizará o FinControl.

**Pré-condições:**
A organização ainda não deve existir no sistema com o mesmo CNPJ.

**Fluxo principal:**
O responsável informa os dados básicos da organização, como nome da empresa, CNPJ e e-mail de contato.

O sistema valida se o CNPJ ainda não está cadastrado.

Após a validação, o sistema registra a organização como cliente do FinControl.

Em seguida, o sistema permite a criação do primeiro usuário administrador vinculado a essa organização.

**Regras de negócio:**
Uma organização não realiza login diretamente.

Toda organização precisa possuir pelo menos um usuário administrador.

O CNPJ não pode ser duplicado dentro do cadastro de organizações do FinControl.

**Resultado esperado:**
A organização fica cadastrada e pronta para possuir usuários, contrato, plano, clientes, cobranças e pagamentos.

**Cenários de erro:**
Se o CNPJ já existir, o cadastro deve ser impedido.

Se os dados obrigatórios não forem informados, o cadastro não deve ser concluído.

---

## UC02 — Criar usuário administrador da organização

**Ator principal:**
Responsável pelo cadastro inicial da organização.

**Objetivo:**
Criar o primeiro usuário com permissão administrativa dentro da organização.

**Pré-condições:**
A organização deve estar cadastrada.

**Fluxo principal:**
O responsável informa nome, e-mail e senha do primeiro usuário administrador.

O sistema vincula esse usuário à organização cadastrada.

O sistema define esse usuário como administrador da organização.

**Regras de negócio:**
O login pertence ao usuário, não à organização.

O usuário administrador será responsável por gerenciar colaboradores, filiais e configurações permitidas pelo plano.

Uma organização não deve ficar sem administrador.

**Resultado esperado:**
O administrador inicial é criado e poderá acessar o FinControl.

**Cenários de erro:**
Se o e-mail já estiver sendo utilizado, o cadastro deve ser impedido.

Se a organização não existir, o usuário não pode ser criado.

---

## UC03 — Autenticar usuário

**Ator principal:**
Usuário administrador ou colaborador financeiro.

**Objetivo:**
Permitir que uma pessoa autorizada acesse o FinControl.

**Pré-condições:**
O usuário deve estar cadastrado e ativo.

A organização vinculada ao usuário deve estar ativa e possuir contrato válido.

**Fluxo principal:**
O usuário informa e-mail e senha.

O sistema valida as credenciais.

O sistema identifica a organização ou filial vinculada ao usuário.

O sistema identifica o perfil do usuário e as funcionalidades disponíveis conforme o plano contratado.

**Regras de negócio:**
Usuários inativos não podem acessar o sistema.

Usuários não podem acessar dados de organizações às quais não pertencem.

Uma organização com contrato suspenso ou encerrado pode ter operações bloqueadas.

**Resultado esperado:**
O usuário acessa o sistema com as permissões corretas.

**Cenários de erro:**
Credenciais inválidas devem impedir o acesso.

Usuário inativo deve ser bloqueado.

Contrato suspenso ou encerrado deve limitar ou impedir operações.

---

## UC04 — Selecionar ou validar plano da organização

**Ator principal:**
Usuário administrador da organização.

**Objetivo:**
Definir quais funcionalidades estarão disponíveis para a organização.

**Pré-condições:**
A organização deve estar cadastrada.

O usuário deve ser administrador.

**Fluxo principal:**
O administrador seleciona um plano disponível.

O sistema associa o plano ao contrato ativo da organização.

O sistema passa a liberar ou bloquear funcionalidades conforme o plano contratado.

**Regras de negócio:**
Cada organização deve possuir um plano válido por meio de um contrato.

Filiais poderão herdar o plano contratado pela matriz.

Funcionalidades fora do plano devem ser bloqueadas.

**Resultado esperado:**
A organização passa a operar com as funcionalidades permitidas pelo plano escolhido.

**Cenários de erro:**
Se o plano não existir ou estiver inativo, ele não poderá ser selecionado.

Se o usuário não for administrador, a operação deve ser impedida.

---

## UC05 — Cadastrar filial

**Ator principal:**
Usuário administrador da matriz.

**Objetivo:**
Cadastrar uma filial vinculada à organização matriz.

**Pré-condições:**
A matriz deve estar cadastrada.

O usuário deve ser administrador da matriz.

O plano contratado deve permitir uso de filiais.

**Fluxo principal:**
O administrador informa os dados da filial.

O sistema valida se o CNPJ da filial ainda não está cadastrado.

O sistema vincula a filial à matriz.

A filial passa a ter seus próprios usuários, clientes, cobranças e pagamentos.

**Regras de negócio:**
Uma filial pertence a uma única matriz.

Uma filial não pode acessar dados de outra filial.

A matriz pode visualizar dados consolidados das filiais.

O cadastro de filial só deve ser permitido se o plano contratado permitir essa funcionalidade.

**Resultado esperado:**
A filial fica cadastrada e vinculada à matriz.

**Cenários de erro:**
Se o plano não permitir filial, o cadastro deve ser bloqueado.

Se o CNPJ já estiver cadastrado, o cadastro deve ser impedido.

---

## UC06 — Cadastrar colaborador

**Ator principal:**
Usuário administrador da organização ou filial.

**Objetivo:**
Cadastrar um colaborador financeiro para utilizar o FinControl.

**Pré-condições:**
O administrador deve estar autenticado.

A organização ou filial deve estar ativa.

**Fluxo principal:**
O administrador informa os dados do colaborador, como nome, e-mail e perfil.

O sistema vincula o colaborador à organização ou filial correta.

O colaborador passa a poder acessar o sistema conforme seu perfil.

**Regras de negócio:**
O colaborador só pode acessar dados da organização ou filial à qual está vinculado.

O administrador não pode cadastrar colaborador em uma organização que não administra.

Usuários inativos não podem acessar o sistema.

**Resultado esperado:**
O colaborador é cadastrado e fica disponível para acessar o FinControl.

**Cenários de erro:**
Se o e-mail já estiver em uso, o cadastro deve ser impedido.

Se o administrador não tiver permissão, a operação deve ser bloqueada.

---

## UC07 — Cadastrar cliente

**Ator principal:**
Colaborador financeiro ou administrador.

**Objetivo:**
Cadastrar um cliente que poderá receber cobranças.

**Pré-condições:**
O usuário deve estar autenticado.

O usuário deve pertencer à organização ou filial onde o cliente será cadastrado.

**Fluxo principal:**
O usuário informa os dados do cliente, como nome, CPF ou CNPJ e contato.

O sistema valida se o documento já existe dentro da mesma organização ou filial.

O sistema vincula o cliente à organização ou filial do usuário autenticado.

**Regras de negócio:**
O cliente não realiza login no sistema nesta primeira versão.

O mesmo CPF ou CNPJ não pode ser duplicado dentro da mesma organização ou filial.

Um cliente de uma organização não pode ser acessado por outra organização.

**Resultado esperado:**
O cliente é cadastrado e fica disponível para receber cobranças.

**Cenários de erro:**
Documento duplicado deve impedir o cadastro.

Usuário sem permissão não pode cadastrar cliente.

---

## UC08 — Criar cobrança

**Ator principal:**
Colaborador financeiro ou administrador.

**Objetivo:**
Criar uma cobrança para um cliente cadastrado.

**Pré-condições:**
O cliente deve existir.

O cliente deve pertencer à mesma organização ou filial do usuário.

A organização deve possuir contrato ativo.

**Fluxo principal:**
O usuário seleciona o cliente.

O usuário informa descrição, valor e data de vencimento.

O sistema cria a cobrança com estado pendente.

O sistema registra o acontecimento no histórico da cobrança.

**Regras de negócio:**
Toda cobrança nasce como pendente.

O valor da cobrança deve ser maior que zero.

A cobrança pertence à organização ou filial do usuário autenticado.

O usuário não define manualmente estados como paga ou vencida.

**Resultado esperado:**
A cobrança é criada e fica disponível para receber pagamentos.

**Cenários de erro:**
Cliente inexistente deve impedir a criação.

Cliente de outra organização deve impedir a criação.

Valor inválido deve impedir a criação.

---

## UC09 — Registrar pagamento

**Ator principal:**
Colaborador financeiro ou administrador.

**Objetivo:**
Registrar um pagamento recebido para uma cobrança.

**Pré-condições:**
A cobrança deve existir.

A cobrança não pode estar cancelada.

A cobrança não pode estar paga.

O usuário deve ter permissão sobre a organização ou filial da cobrança.

**Fluxo principal:**
O usuário seleciona a cobrança.

O usuário informa o valor pago, a data do pagamento, a forma de pagamento e uma referência externa, se existir.

O sistema registra o pagamento individualmente.

O sistema recalcula o saldo da cobrança.

O sistema altera automaticamente o estado da cobrança para parcialmente paga ou paga.

O sistema registra o acontecimento no histórico.

**Regras de negócio:**
O pagamento deve ter valor maior que zero.

O pagamento não pode ultrapassar o saldo restante.

Cada pagamento deve ser registrado individualmente.

O estado da cobrança é alterado pelo sistema, não pelo usuário.

**Resultado esperado:**
O pagamento é registrado e a cobrança tem saldo e estado atualizados.

**Cenários de erro:**
Pagamento acima do saldo deve ser impedido.

Pagamento em cobrança cancelada deve ser impedido.

Pagamento em cobrança paga deve ser impedido.

Pagamento duplicado por referência externa deve ser impedido.

---

## UC10 — Identificar cobrança vencida

**Ator principal:**
Sistema.

**Objetivo:**
Identificar cobranças que ultrapassaram a data de vencimento e ainda possuem saldo pendente.

**Pré-condições:**
A cobrança deve possuir saldo pendente.

A data atual deve ser maior que a data de vencimento.

A cobrança não pode estar cancelada.

**Fluxo principal:**
O sistema verifica cobranças em aberto.

O sistema identifica cobranças com vencimento ultrapassado.

O sistema altera o estado para vencida.

O sistema registra a mudança no histórico.

**Regras de negócio:**
Uma cobrança vencida ainda pode receber pagamento.

Uma cobrança paga não deve virar vencida.

Uma cobrança cancelada não deve virar vencida.

O histórico deve ser registrado apenas quando houver mudança real de estado.

**Resultado esperado:**
Cobranças em atraso passam a ser identificadas como vencidas.

**Cenários de erro:**
Se a cobrança já estiver paga ou cancelada, nenhuma alteração deve ser feita.

---

## UC11 — Cancelar cobrança

**Ator principal:**
Administrador ou colaborador com permissão.

**Objetivo:**
Cancelar uma cobrança que ainda não possui pagamento.

**Pré-condições:**
A cobrança deve existir.

A cobrança deve estar pendente.

A cobrança não deve possuir pagamentos registrados.

**Fluxo principal:**
O usuário solicita o cancelamento.

O usuário informa uma justificativa.

O sistema valida se a cobrança pode ser cancelada.

O sistema altera o estado para cancelada.

O sistema registra o cancelamento no histórico.

**Regras de negócio:**
Cobrança paga não pode ser cancelada.

Cobrança com pagamento parcial não pode ser cancelada diretamente.

Cobrança cancelada não pode receber pagamentos.

Cancelamento deve exigir justificativa.

**Resultado esperado:**
A cobrança fica cancelada e não pode mais receber pagamento.

**Cenários de erro:**
Cobrança paga deve impedir cancelamento.

Cobrança com pagamento deve impedir cancelamento.

Falta de justificativa deve impedir cancelamento.

---

## UC12 — Estornar pagamento

**Ator principal:**
Administrador ou colaborador com permissão.

**Objetivo:**
Estornar um pagamento registrado indevidamente.

**Pré-condições:**
O pagamento deve existir.

O pagamento ainda não pode estar estornado.

O usuário deve possuir permissão.

**Fluxo principal:**
O usuário seleciona o pagamento.

O usuário informa uma justificativa.

O sistema marca o pagamento como estornado.

O sistema recalcula o saldo da cobrança.

O sistema recalcula o estado da cobrança.

O sistema registra o estorno no histórico.

**Regras de negócio:**
Pagamento estornado não deve ser apagado.

Pagamento estornado não conta mais para o total pago da cobrança.

O estorno deve exigir justificativa.

O estado da cobrança deve ser recalculado após o estorno.

**Resultado esperado:**
O pagamento permanece registrado, mas deixa de compor o total pago da cobrança.

**Cenários de erro:**
Pagamento inexistente deve impedir estorno.

Pagamento já estornado deve impedir novo estorno.

Falta de justificativa deve impedir estorno.

---

## UC13 — Consultar cobranças

**Ator principal:**
Administrador ou colaborador financeiro.

**Objetivo:**
Consultar cobranças da organização ou filial.

**Pré-condições:**
O usuário deve estar autenticado.

O usuário deve possuir permissão para consultar cobranças.

**Fluxo principal:**
O usuário acessa a consulta de cobranças.

O sistema retorna apenas cobranças da organização ou filial permitida.

O usuário poderá filtrar por cliente, situação, vencimento e período.

**Regras de negócio:**
Usuários de uma filial não podem visualizar cobranças de outra filial.

Administradores da matriz podem visualizar dados consolidados das filiais.

Cobranças de outra organização não podem ser retornadas.

**Resultado esperado:**
O usuário visualiza apenas as cobranças que tem permissão para acessar.

**Cenários de erro:**
Tentativa de acessar cobrança de outra organização deve ser impedida.

Usuário sem permissão deve ter acesso negado.
