   Portal de Vagas – Desafio Técnico

Este projeto foi desenvolvido como parte de um teste técnico e tem como objetivo simular
 um portal de vagas simples. A aplicação foi construída com ASP.NET Core MVC e permite o 
 cadastro e gerenciamento de:

Vagas disponíveis

Pessoas (candidatos)

Candidaturas realizadas

Para simplificar o ambiente, toda a persistência de dados é feita via arquivos .txt na pasta
 Data/, funcionando como um "banco de dados" baseado em arquivos — o suficiente para atender os
 requisitos do desafio sem depender de um banco real.

 Tecnologias e Boas Práticas que UtilizeI
ASP.NET Core 9.0
C#
Arquitetura MVC, com separação clara entre controllers, serviços e repositórios
Injeção de dependência aplicada em todas as camadas
AutoMapper para conversão entre Models e ViewModels
Persistência usando StreamReader e StreamWriter com serialização em JSON

Testes unitários com xUnit e Moq

 Funcionalidades Implementadas
Requisitos principais
Cadastro, edição, listagem e exclusão de Vagas

Cadastro, edição, listagem e exclusão de Pessoas (candidatos)

Sistema de candidatura (relacionando pessoas a vagas)

Detalhamento de vaga com lista de candidatos

Aprovação ou reprovação de candidatos

Listagem de pessoas com histórico de candidaturas

Extras que resolvi implementar
Filtro de busca por título na listagem de vagas
Paginação nas telas de vagas e pessoas
Exportação de candidatos aprovados para CSV
Teste unitário cobrindo a regra de negócio que impede candidaturas duplicadas

 Testes
Incluí um teste unitário com xUnit para garantir que a lógica de bloqueio de candidaturas duplicadas
 funciona corretamente. Também deixei a estrutura preparada para facilitar a expansão dos testes.

 Como rodar o projeto
Pré-requisitos
.NET SDK 9.0 ou superior

Passos
Clone o repositório:

git clone https://github.com/DanielDiogo/Portal_de_Vagas.git
Vá até a pasta do projeto:

cd PortalVagas
Rode a aplicação:

Abra o navegador e acesse a URL exibida no terminal http://localhost:5104/
