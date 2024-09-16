

# Price Whisper - OM Corp

### Integrantes
- **André Sant'Ana Boim** - RM551575
- **Marcelo Hespanhol Dias** - RM98251
- **Gustavo Imparato Chaves** - RM551988
- **Gabriel Eringer de Oliveira** - RM99632

## Arquitetura
Decidimos por utilizar a arquitetura de microsserviços devido à facilidade de escalabilidade, flexibilidade na utilização de diversas tecnologias (como por exemplo .NET e Java que estamos utilizando), e também pela maior velocidade e eficiência no desenvolvimento do projeto.

## Design Patterns
Utilizamos o design pattern de Singleton para o gerenciador de configurações do projeto, e também para o gerenciador de banco de dados.

## Instruções de execução do deploy no Azure App Services + Github Actions

### Passo a passo

1. **Faça um fork do projeto**  
   - Clique no botão "Fork" no canto superior direito deste repositório
   ![PRINT](https://i.imgur.com/GoT12DF.png)
   - Após fazer o fork, clone o repositório para a sua máquina local com o seguinte comando:
     ```
     git clone https://github.com/<SEU_USER>/pricewhisper-usuarios.git
     ```
     
    - Obs.: Troque `<SEU_USER>` pelo seu usuário do GitHub após ter realizado o fork do projeto.

2. **Configuração do Projeto**
   - Navegue até o diretório do projeto:
     ```
     cd pricewhisper-usuarios.git
     ```
   - Abra o projeto no seu editor de código preferido (VSCode, Visual Studio, etc.).

3. **Configuração do Banco de Dados Oracle**
   - No arquivo `appsettings.json`, atualize a string de conexão para o banco de dados Oracle da FIAP:
     ```
     "ConnectionStrings": {
        "OracleConnection": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=<SEU_USER>;Password=<SUA_SENHA>;"
      }
     ```
   - Troque `<SEU-USER>` pelo seu RM da FIAP e `<SUA_SENHA>` pela sua senha do banco de dados Oracle da FIAP.

5. **Configuração do Pipeline de CI/CD com GitHub Actions**
   - Certifique-se de que o arquivo `main_pricewhisperdotnet.yml` está configurado corretamente para o deploy automático no Azure App Services. Ele se encontra no diretório `.github/workflows/`.

6. **Deploy no Azure App Services**
   - Para realizar o deploy manualmente:
     - Acesse o portal do Azure, crie um **App Service** para o backend com as seguintes opções na aba "Basics":
![PRINT](https://i.imgur.com/ho7HHAB.png)
![PRINT](https://i.imgur.com/4SSWdWj.png)
     - Na aba "Deployment", habilite a opção "Continuous deployment" e vincule o **App Service** ao repositório GitHub do seu fork.
![PRINT](https://i.imgur.com/5l2I0yI.png)
     - Ative também a opção "Basic authentication" no fim da aba "Deployment"

     - O deploy será gerado automaticamente a partir do pipeline configurado.
     - Na aba "Monitor + secure", certifique-se de que a opção "Application insights" está ativada.
![PRINT](https://i.imgur.com/DDV53ku.png)
     - Por fim, clique em "Create" para finalizar o processo de deploy via Github Actions.

7. **Testando o backend**
   - Após o deploy, acesse a URL gerada pelo Azure App Services para verificar se o backend está ativo.
   - Utilize ferramentas como Postman ou Swagger para fazer requisições e testar os endpoints da API.

## Endpoints da API

### GET /api/Usuario
- **Descrição**: Busca por todos os usuários.
- **Resposta**: Lista de Usuários.

### GET /api/Usuario/{id}
- **Descrição**: Busca por um usuário por id.
- **Resposta**: Usuário buscado.

### PUT /api/Usuario
- **Descrição**: Atualiza um usuário existente.
- **Request Body**:
  ```
  {
    "Id": 1,
    "Nome": "Marcelo",
    "NomeUsuario": "marcelodias",
    "Senha": "12345",
    "EmpresaId": 1
  }
  ```
- **Resposta**: Usuário atualizado com sucesso.

### POST /api/Usuario
- **Descrição**: Cria um novo usuário.
- **Request Body**:
  ```
  {
    "Nome": "Marcelo",
    "NomeUsuario": "marcelodias",
    "Senha": "12345",
    "EmpresaId": 1
  }
  ```
- **Resposta**: Usuário criado com sucesso.

### DELETE /api/Usuario/{id}
- **Descrição**: Deleta um usuário por id.
- **Resposta**: Usuário deletado.

### GET /api/Empresa
- **Descrição**: Busca por todas as empresas.
- **Resposta**: Lista de Empresas.

### GET /api/Empresa/{id}
- **Descrição**: Busca por uma empresa por id.
- **Resposta**: Empresa buscada.

### PUT /api/Empresa
- **Descrição**: Atualiza uma empresa existente.
- **Request Body**:
  ```
  {
    "Id": 1,
    "CNPJ": "123456789",
    "RazaoSocial": "Teste",
    "NomeFantasia": "Teste"
  }
  ```
- **Resposta**: Empresa atualizada com sucesso.

### POST /api/Empresa
- **Descrição**: Cria uma nova empresa.
- **Request Body**:
  ```
  {
    "CNPJ": "123456789",
    "RazaoSocial": "Teste",
    "NomeFantasia": "Teste"
  }
  ```
- **Resposta**: Empresa criada com sucesso.

### DELETE /api/Empresa/{id}
- **Descrição**: Deleta uma empresa por id.
- **Resposta**: Empresa deletada.

Esse código em Markdown está formatado de maneira que ficará bem estruturado e legível no arquivo README ou em qualquer outra plataforma que suporte Markdown.

## Considerações Finais
Essa API foi construída utilizando o framework .NET e está integrada ao banco de dados Oracle, utilizando uma arquitetura de microsserviços, facilitando a escalabilidade e integração com outros componentes do sistema Price Whisper. Agradecemos por acompanhar e estamos abertos a contribuições via Pull Request.

Após finalizar o processo de testes da API, exclua o resource group para evitar possíveis gastos.
![PRINT](https://i.imgur.com/Y55DrrX.png)
