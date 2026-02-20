# 🎓 Hackton – MVP Sistema de Matrícula para Secretarias

## 📌 Sobre o Projeto

Este projeto é um **MVP (Minimum Viable Product)** de um sistema voltado para **secretarias escolares**, com o objetivo de **otimizar e agilizar o processo de matrícula de alunos**.

A proposta é centralizar e simplificar o fluxo de cadastro, reduzindo retrabalho, organizando informações e facilitando o gerenciamento das matrículas.

> ⚠️ Atualmente o sistema está rodando **apenas em ambiente local**.

Além disso, a parte de **finalização de cadastro**, que futuramente poderá ser externa (não intranet), está integrada neste mesmo projeto para **otimização do tempo de desenvolvimento**.

Outra evolução planejada é a implementação de **OCR (Reconhecimento Óptico de Caracteres)** para leitura automática de documentos, além de **validações automáticas de matrícula**, que atualmente são realizadas manualmente pelas secretarias.

---

## 🏗️ Arquitetura do Projeto

O projeto foi estruturado seguindo os princípios de **Arquitetura Limpa (Clean Architecture)**, visando:

- Separação clara de responsabilidades  
- Baixo acoplamento  
- Alta coesão  
- Facilidade de manutenção e testes  
- Escalabilidade futura
  
- A arquitetura foi pensada para manter separação de responsabilidades e facilitar futuras evoluções.

- `Hackton.Application`
- `Hackton.Domain`
- `Hackton.Service`
- `docker-compose.yaml`



---

## 🚀 Como Executar o Projeto

### ⚠️ Requisitos

Antes de iniciar, você precisa ter instalado:

- .NET SDK
- Docker
- Docker Compose

---

### 🔥 Passos para subir o projeto

#### 1️⃣ Subir os containers com Docker

O projeto depende do **Docker** para subir os serviços definidos no `docker-compose.yaml`.

Execute:

```bash
docker-compose up --build
```

Ou, dependendo da sua versão:

```bash
docker compose up --build
```

---

#### 2️⃣ Iniciar os dois projetos simultaneamente

⚠️ **IMPORTANTE**

É necessário iniciar **os dois projetos juntos**:

- `Hackton.Application`
- `Hackton.Service`

Se apenas um for iniciado, o sistema não funcionará corretamente, pois há dependência entre eles.

Você pode:

- Configurar múltiplos projetos de inicialização na sua IDE  
ou  
- Rodar via CLI em terminais separados

Exemplo:

```bash
dotnet run --project Hackton.Application
```

```bash
dotnet run --project Hackton.Service
```

---

## 💡 Objetivo do MVP

- Validar a ideia de otimização do processo de matrícula
- Centralizar informações dos alunos
- Reduzir erros manuais
- Melhorar a organização da secretaria
- Criar base escalável para futuras integrações
- Preparar o sistema para futuras automações com OCR e validações inteligentes

---

## 🤖 Evolução Planejada – OCR e Validações Automáticas

Atualmente, a validação das matrículas é realizada manualmente pelas secretarias.

Como evolução do projeto, será implementado:

- 📄 Leitura automática de documentos utilizando OCR
- ✅ Validação automática de dados obrigatórios
- 🔎 Verificação de inconsistências cadastrais
- ⏱️ Redução do tempo de conferência manual

Essa melhoria tem potencial para reduzir significativamente o tempo operacional da secretaria e minimizar erros humanos.

---

## ✅ Pontos Positivos da Solução

✔️ Redução de retrabalho no processo de matrícula  
✔️ Organização centralizada de dados  
✔️ Estrutura preparada para escalar  
✔️ Separação por camadas (Domain, Application, Service)  
✔️ Possibilidade futura de separar a finalização de cadastro para ambiente externo  
✔️ Uso de Docker facilita padronização do ambiente  
✔️ Base preparada para implementação de OCR e automações inteligentes  
✔️ Potencial de redução significativa do tempo de validação de matrícula  

---

## 🧠 Desafios Enfrentados

Durante o desenvolvimento tivemos alguns desafios importantes:

- **Idealização do projeto:** definir escopo e funcionalidades principais dentro do tempo disponível.
- **Escolha da stack:** o tempo limitado dificultou a análise mais aprofundada das tecnologias.
- **Definição da arquitetura:** estruturar corretamente as camadas sem comprometer a entrega do MVP.
- **Otimização do tempo:** por isso, a parte de finalização de cadastro foi mantida junto ao sistema principal.

Esses desafios foram importantes para o aprendizado e amadurecimento da solução.

---

## 🔮 Próximos Passos (Possíveis Evoluções)

- Implementação de OCR para leitura automática de documentos
- Implementação de validações automáticas de matrícula
- Separar a finalização de cadastro em um projeto independente
- Publicação em ambiente de produção
- Implementação de autenticação e controle de acesso
- Melhorias de UX/UI
- Integração com sistemas acadêmicos

---

## 👥 Considerações Finais

Este projeto representa a validação inicial de uma solução que pode trazer impacto real na rotina de secretarias escolares, reduzindo tempo operacional e aumentando eficiência.

A implementação futura de OCR e validações automáticas permitirá transformar um processo atualmente manual em um fluxo inteligente e automatizado.

Por se tratar de um MVP, ainda há melhorias a serem implementadas, mas a base está estruturada para crescimento e evolução.
