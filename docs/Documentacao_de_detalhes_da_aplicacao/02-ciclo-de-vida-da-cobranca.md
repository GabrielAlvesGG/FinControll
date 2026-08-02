# Ciclo de vida da cobrança — FinControl

## 1. Regras gerais

O estado de uma cobrança deverá ser controlado automaticamente pelo sistema com base nos pagamentos, no saldo restante, na data de vencimento e no cancelamento.

O colaborador não poderá escolher livremente o estado financeiro da cobrança.

Cada pagamento deverá ser registrado individualmente. O total pago será representado pela soma de todos os pagamentos válidos vinculados à cobrança.

O histórico da cobrança deverá registrar os principais acontecimentos, como:

* criação da cobrança;
* registro de pagamento;
* mudança de estado;
* aplicação de juros;
* cancelamento;
* estorno de pagamento.

O pagamento e o histórico são registros diferentes. O pagamento representa uma movimentação financeira, enquanto o histórico informa o que aconteceu com a cobrança.

---

## 2. Criação da cobrança

Quando o usuário criar uma cobrança, deverá selecionar um cliente e informar:

* descrição;
* valor total;
* data de vencimento.

A cobrança será criada com:

* estado pendente;
* saldo igual ao valor total;
* nenhum pagamento registrado.

Também deverá ser criado um registro no histórico informando:

* quem criou a cobrança;
* quando ela foi criada;
* para qual cliente;
* qual era o valor inicial;
* qual era a data de vencimento.

### Transição

**Estado anterior:** inexistente.
**Acontecimento:** criação da cobrança.
**Novo estado:** pendente.
**Responsável:** usuário autorizado.

---

## 3. Cobrança pendente

Uma cobrança estará pendente quando:

* ainda possuir todo o saldo em aberto;
* não tiver ultrapassado a data de vencimento;
* não estiver cancelada.

Enquanto estiver pendente, será permitido:

* registrar um pagamento;
* cancelar a cobrança;
* consultar seu histórico.

Não será permitido alterar manualmente seu estado para paga ou vencida.

### Possíveis transições

* pendente para parcialmente paga;
* pendente para paga;
* pendente para vencida;
* pendente para cancelada.

---

## 4. Pagamento parcial

Quando o cliente pagar um valor maior que zero e menor que o saldo atual, o sistema deverá:

* registrar o pagamento individualmente;
* recalcular o total pago;
* recalcular o saldo restante;
* alterar o estado para parcialmente paga;
* registrar a operação no histórico.

### Exemplo

Uma cobrança possui valor de R$ 1.000.

Após um pagamento de R$ 400:

* total pago: R$ 400;
* saldo restante: R$ 600;
* estado: parcialmente paga.

### Transição

**Estado anterior:** pendente.
**Acontecimento:** pagamento inferior ao saldo.
**Novo estado:** parcialmente paga.
**Responsável pela mudança:** sistema.

Uma cobrança parcialmente paga não poderá ser cancelada diretamente. Primeiro, os pagamentos existentes deverão ser estornados.

---

## 5. Pagamento integral

Quando a soma dos pagamentos válidos atingir exatamente o valor devido, o sistema deverá:

* registrar o novo pagamento;
* definir o saldo como zero;
* alterar o estado para paga;
* registrar a quitação no histórico.

### Transição

**Estado anterior:** pendente, parcialmente paga ou vencida.
**Acontecimento:** pagamentos atingiram o valor total devido.
**Novo estado:** paga.
**Responsável pela mudança:** sistema.

Uma cobrança paga não poderá:

* receber novos pagamentos;
* ser cancelada;
* ter seu valor alterado;
* ter seu vencimento alterado.

Caso seja necessário desfazer um pagamento, deverá ser executado um estorno.

---

## 6. Cobrança vencida

Uma cobrança será considerada vencida quando:

* a data atual ultrapassar a data de vencimento;
* ainda existir saldo pendente;
* ela não estiver cancelada.

Um processamento periódico deverá identificar cobranças que ultrapassaram o vencimento.

O histórico deverá ser registrado somente no momento em que a cobrança mudar para vencida. Não deverá ser criado um novo registro diariamente para a mesma cobrança.

### Transições possíveis

* pendente para vencida;
* parcialmente paga para vencida.

Uma cobrança que recebeu pagamento parcial, mas ainda possui saldo depois do vencimento, deverá ser considerada vencida.

A cobrança vencida poderá continuar recebendo pagamentos.

Depois de um pagamento:

* se ainda existir saldo, continuará vencida;
* se o saldo chegar a zero, passará para paga.

---

## 7. Juros por atraso

A organização poderá configurar uma regra de juros para cobranças vencidas.

Quando existir uma configuração válida, o sistema deverá calcular os juros conforme:

* percentual configurado;
* quantidade de dias de atraso;
* data em que o pagamento estiver sendo registrado.

A aplicação dos juros deverá ser registrada no histórico, informando:

* regra utilizada;
* quantidade de dias;
* valor original;
* valor dos juros;
* valor final devido.

Quando a organização não possuir uma regra de juros configurada, a cobrança continuará vencida com seu valor original.

O sistema poderá enviar uma notificação ao responsável financeiro informando que existe uma cobrança vencida sem configuração de juros. Essa notificação não deverá impedir o registro de um pagamento.

Falhas no envio da notificação também não deverão impedir pagamentos ou mudanças de estado.

---

## 8. Cancelamento

Uma cobrança poderá ser cancelada quando:

* estiver pendente;
* não possuir pagamentos registrados;
* o usuário possuir permissão;
* uma justificativa for informada.

Ao cancelar, o sistema deverá:

* alterar o estado para cancelada;
* registrar o usuário responsável;
* registrar a data;
* registrar a justificativa no histórico.

Uma cobrança cancelada não poderá:

* receber pagamentos;
* tornar-se vencida;
* ter valores alterados;
* ser reativada.

Caso seja necessário realizar uma nova cobrança, deverá ser criada outra cobrança.

---

## 9. Estorno de pagamento

O estorno deverá ser aplicado sobre um pagamento existente, e não diretamente sobre a cobrança.

O pagamento estornado deverá permanecer no histórico, mas não deverá continuar compondo o total pago.

Depois do estorno, o sistema deverá recalcular o saldo e o estado da cobrança.

O novo estado dependerá da situação:

* se nenhum pagamento válido permanecer e a cobrança não estiver vencida, ficará pendente;
* se ainda existirem pagamentos válidos e não estiver vencida, ficará parcialmente paga;
* se existir saldo e a data de vencimento tiver passado, ficará vencida;
* se o saldo continuar igual a zero, permanecerá paga.

Todo estorno deverá exigir:

* usuário responsável;
* data;
* justificativa.

---

## 10. Regras proibidas

O sistema deverá impedir:

* pagamento com valor igual ou inferior a zero;
* pagamento superior ao saldo;
* pagamento em cobrança cancelada;
* pagamento adicional em cobrança totalmente paga;
* cancelamento de cobrança que possui pagamentos;
* alteração manual do estado financeiro;
* exclusão física de pagamentos;
* exclusão física do histórico;
* acesso à cobrança por usuários de outra organização ou filial sem autorização.

---

## 11. Resumo das transições

### Pendente

Pode mudar para:

* parcialmente paga;
* paga;
* vencida;
* cancelada.

### Parcialmente paga

Pode mudar para:

* paga;
* vencida;
* pendente após estorno de todos os pagamentos.

### Vencida

Pode mudar para:

* paga;
* continuar vencida após pagamento insuficiente;
* pendente ou parcialmente paga após alteração válida do vencimento, caso essa alteração seja permitida futuramente.

### Paga

Pode mudar após estorno para:

* pendente;
* parcialmente paga;
* vencida.

### Cancelada

É um estado final e não permite novas transições.
