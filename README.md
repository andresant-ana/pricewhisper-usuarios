# Price Whisper - OM Corp
### Integrantes
André Sant'Ana Boim - RM551575

Marcelo Hespanhol Dias - RM98251

Gustavo Imparato Chaves - RM551988

Gabriel Eringer de Oliveira - RM99632


## Arquitetura
Decidimos por utilizar a arquitetura de microsserviços devido a facilidade de escalabilidade, flexibilidade na utilização de diversas tecnologias (como por exemplo .NET e Java que estamos utilizando), 
e também pela maior velocidade e eficiência no desenvolvimento do projeto.

## Design Patterns
Utilizamos o design pattern de Singleton para o gerenciador de configurações do projeto, e também para o gerenciador de banco de dados.

## Princípios de Clean Code e SOLID Aplicados

Neste projeto, adotamos diversas práticas de Clean Code e princípios SOLID para garantir um código limpo, de fácil manutenção e escalável. A seguir, destacamos os principais conceitos aplicados:

### Clean Code

1. **Nomenclatura Significativa**
   - Os nomes das classes, métodos e variáveis são claros e descritivos, facilitando a compreensão do propósito de cada elemento no código.

2. **Responsabilidade Única**
   - Cada classe possui uma única responsabilidade, alinhando-se ao princípio de responsabilidade única do Clean Code e do SOLID.

3. **Funções Curtas e Objetivas**
   - Métodos são mantidos curtos e focados em uma única tarefa, aumentando a legibilidade e facilitando testes unitários.

4. **Comentários Justificados**
   - Comentários são utilizados apenas quando necessário para explicar decisões complexas, evitando redundância com o código.

5. **Formato Consistente**
   - A formatação do código segue padrões consistentes, utilizando indentação adequada e espaçamento para melhorar a legibilidade.

### Princípios SOLID

1. **Single Responsibility Principle (SRP)**
   - Cada classe tem uma única responsabilidade. Por exemplo, os repositórios gerenciam a persistência de dados, enquanto os serviços contêm a lógica de negócios.

2. **Open/Closed Principle (OCP)**
   - As classes estão abertas para extensão, mas fechadas para modificação. Utilizamos abstrações (interfaces) para permitir a extensão das funcionalidades sem alterar o código existente.

3. **Liskov Substitution Principle (LSP)**
   - As classes derivadas podem substituir suas classes base sem alterar a corretude do programa. As implementações das interfaces garantem compatibilidade e substituição adequada.

4. **Interface Segregation Principle (ISP)**
   - Interfaces específicas são preferidas em vez de interfaces generalistas. Isso evita que as classes implementem métodos que não utilizarão.

5. **Dependency Inversion Principle (DIP)**
   - Dependemos de abstrações (interfaces) em vez de implementações concretas. Isso é evidenciado pelo uso de injeção de dependências para repositórios e serviços, promovendo baixo acoplamento e facilitando testes unitários.

Estas práticas asseguram que o código seja robusto, eficiente e de fácil manutenção, alinhando-se às melhores práticas de desenvolvimento de software.

## Instruções
Clone o projeto em sua máquina e na pasta raiz do projeto execute o comando 'dotnet run'.

## Integração com CNPJá

Este projeto utiliza a API do CNPJá para validação de CNPJs durante o cadastro de empresas. A integração permite:

- Validação automática de CNPJs na base da Receita Federal
- Verificação da existência e situação cadastral da empresa
- Garantia de dados consistentes no cadastro de empresas

### Uso da API CNPJá

A validação acontece automaticamente ao cadastrar uma nova empresa através do endpoint POST /api/Empresa. O sistema irá:

1. Verificar se o CNPJ existe na base da Receita Federal
2. Validar se o CNPJ está ativo
3. Permitir o cadastro apenas de empresas com situação regular

Se o CNPJ for inválido ou não for encontrado, a API retornará um erro 400 (Bad Request).

## Exemplos de Testes de Requisições

### Usuário

#### GET /api/Usuario
Para obter todos os usuários, utilize a URL: `http://localhost:5036/api/Usuario`

#### GET /api/Usuario/{id}
Para obter um usuário específico pelo ID, utilize a URL: `http://localhost:5036/api/Usuario/{id}`

#### POST /api/Usuario
```
{
  "nome": "João Silva",
  "nomeUsuario": "joaosilva",
  "senha": "senha123",
  "empresaId": 1
}
```

#### PUT /api/Usuario
```
{
  "usuarioId": 1,
  "nome": "João Silva Santos",
  "nomeUsuario": "joao.silva",
  "senha": "NovaSenha@123",
  "empresaId": 1
}
```

#### DELETE /api/Usuario/{id}
Para deletar um usuário específico pelo ID, utilize a URL: `http://localhost:5036/api/Usuario/{id}`

### Empresa

#### GET /api/Empresa
Para obter todas as empresas, utilize a URL: `http://localhost:5036/api/Empresa`

#### GET /api/Empresa/{id}
Para obter uma empresa específica pelo ID, utilize a URL: `http://localhost:5036/api/Empresa/{id}`

#### POST /api/Empresa
```
{
  "cnpj": "47960950000121",
  "razaoSocial": "Magazine Luiza S/A",
  "nomeFantasia": "Magazine Luiza",
  "usuarios": []
}
```
Obs.: A empresa deve existir, pois a API do CNPJá está realizando a verificação.

#### PUT /api/Empresa
```
{
  "empresaId": 1,
  "cnpj": "47960950000121",
  "razaoSocial": "Magazine Luiza",
  "nomeFantasia": "Magazine Luiza"
  "usuarios": []
}
```

#### DELETE /api/Empresa/{id}
Para deletar uma empresa específica pelo ID, utilize a URL: `http://localhost:5036/api/Empresa/{id}`


