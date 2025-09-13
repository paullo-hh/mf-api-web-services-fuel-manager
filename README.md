# Fuel Manager API

Uma API RESTful para gerenciamento de consumo de combustível de veículos, desenvolvida em ASP.NET Core.

## 🚀 Tecnologias Utilizadas

- .NET 9
- Entity Framework Core
- SQL Server
- BCrypt.NET (para hash de senhas)

## 📋 Descrição

O Fuel Manager é um sistema que permite o gerenciamento de veículos e seus consumos de combustível, com controle de usuários e permissões. O sistema possibilita o registro detalhado dos abastecimentos e consumos por veículo.

## 🏗️ Estrutura do Projeto

### Modelos de Dados
- **Usuario**: Gerenciamento de usuários do sistema, com autenticação e perfil (Administrador ou Usuário).
- **Veiculo**: Cadastro de veículos, contendo informações como nome, modelo, placa, ano de fabricação e ano do modelo. Cada veículo pode estar vinculado a um ou mais usuários.
- **Consumo**: Registro de consumos de combustível, associado a um veículo. Inclui descrição, data, valor, tipo de combustível e referência ao veículo.
- **VeiculoUsuario**: Relacionamento entre veículos e usuários, permitindo que um veículo seja compartilhado entre diferentes usuários.

### DTOs
- **UsuarioDTO**: Objeto de transferência de dados para usuários
- **ConsumoDetalhadoDTO**: Objeto de transferência de dados para consumos

## 🔐 Perfis de Acesso
- **Administrador**: Acesso total ao sistema
- **Usuario**: Acesso limitado aos seus veículos vinculados

## 📌 Endpoints da API

### Usuários (`/api/usuarios`)
- `GET /`: Lista todos os usuários
- `GET /{id}`: Obtém um usuário específico
- `POST /`: Cria um novo usuário
- `PUT /{id}`: Atualiza um usuário existente
- `DELETE /{id}`: Remove um usuário

### Veículos (`/api/veiculos`)
- `GET /`: Lista todos os veículos
- `GET /{id}`: Obtém um veículo específico
- `POST /`: Cadastra um novo veículo
- `PUT /{id}`: Atualiza um veículo existente
- `DELETE /{id}`: Remove um veículo

### Consumos (`/api/consumos`)
- `GET /`: Lista todos os consumos
- `GET /{id}`: Obtém um consumo específico
- `POST /`: Registra um novo consumo
- `PUT /{id}`: Atualiza um consumo existente
- `DELETE /{id}`: Remove um consumo

## 📝 Exemplos de Requisições

### Criar Usuário
- POST /api/usuarios `{ "nome": "João Silva", "password": "senha123", "perfil": "Usuario" }`

### Criar Veículo
- POST /api/veiculos `{ "nome": "Fiat Uno", "modelo": "Mille Fire", "placa": "ABC-1234", "anoFabricacao": 2010, "anoModelo": 2011 }`

### Registrar Consumo
- POST /api/consumos `{ "descricao": "Abastecimento Posto Shell", "data": "2025-09-13", "valor": 250.00, "tipo": "Gasolina", "veiculoId": 1 }`

## 🔒 Segurança

- Senhas são armazenadas utilizando hash BCrypt
- Validações implementadas para todos os campos obrigatórios
- Verificações de integridade referencial entre veículos e consumos

## 🗃️ Banco de Dados

O sistema utiliza SQL Server com as seguintes tabelas principais:
- Usuarios
- Veiculos
- Consumos
- VeiculosUsuarios (tabela de relacionamento)

## ⚙️ Validações Implementadas

### Usuários
- Nome obrigatório
- Senha obrigatória
- Perfil obrigatório

### Veículos
- Nome, modelo, placa, ano de fabricação e ano do modelo obrigatórios

### Consumos
- Descrição obrigatória
- Data não pode ser futura
- Valor deve ser maior que zero
- Tipo de combustível deve ser válido
- Veículo deve existir no sistema

## 🚦 Status de Respostas

- 200 (OK): Requisição bem-sucedida
- 201 (Created): Recurso criado com sucesso
- 400 (Bad Request): Erro de validação
- 404 (Not Found): Recurso não encontrado
