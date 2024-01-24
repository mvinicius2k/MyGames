# O que é

Pequeno projeto com Blazor WASM para ser usando em conjunto do Azure Functions e então trabalhar no Static Web App.

O projeto Client é o front-end em Blazor-Wasm
O projeto Api é o back-end com Functions do Azure
O projeto Shared são as estruturas comuns entre os dois projetos

# Como rodar

1. Lance o Docker Compose com `docker-compose up`;
2. Veja se a configuração da string de conexão em `local.settings.json` está alinhada;
3. Inicie o Azurite (Se estiver no vscode);
4. Escolha o launch `Attach Functions Startup`, espere até o log do `func shell (functions)` inicializar, então clique para continuar depurando;