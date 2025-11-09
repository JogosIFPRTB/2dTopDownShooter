# Projeto Top-Down 2D Modular (Unity)

Este projeto é um jogo Top-Down 2D (estilo *shooter*) desenvolvido em Unity. O objetivo principal não é criar um jogo completo, mas sim servir como um **exemplo robusto de Arquitetura Modular em C#**, seguindo o **Princípio da Responsabilidade Única (SRP)**.

O resultado é um código limpo, desacoplado, fácil de manter e de expandir.

![[INSIRA AQUI UM GIF DO SEU JOGO EM AÇÃO]](https://i.imgur.com/link-para-seu-gif.gif)

## 🚀 Principais Características

* **Arquitetura Modular (SRP):** Cada script tem uma única responsabilidade (Input, Movimento, Combate, Animação, etc.).
* **Novo Input System:** Uso do pacote moderno da Unity para gerenciar inputs de forma flexível (com WASD para movimento e mira independente com o mouse).
* **IA Inimiga (Máquina de Estados):** Uma máquina de estados simples que alterna entre os scripts `EnemyPatrol` (patrulha) e `EnemyChase` (perseguição) habilitando e desabilitando-os.
* **Movimentação Baseada em Física:** Personagens (jogador e inimigos) são movidos através de `Rigidbody2D.velocity`, permitindo que o motor de física gerencie as colisões e que a animação seja lida corretamente.
* **Sistema de Animação Avançado:** Uso de `Blend Trees 2D Freeform Directional` para gerenciar animações de 4 direções (Idle/Walk) com base na velocidade do Rigidbody.
* **Gerenciamento de Estado (Event-Driven):** O script `PlayerStats` (vidas, moedas) usa `static events` (ex: `OnVidasChanged`) para notificar a UI, sem que a UI precise de uma referência direta ao jogador.
* **Combate Balanceado:** Lógica de disparo com *cooldown* (cadência de tiro) e projéteis que se autodestroem após um tempo ou ao colidir.
* **Mundo com Tilemaps:** Cenário construído com o sistema de `Tilemap` da Unity, com colisões otimizadas usando `Tilemap Collider 2D` e `Composite Collider 2D`.
* **Otimização de Renderização:** Uso de `Sprite Atlas` para empacotar os *sprites* do jogador e inimigos em uma única textura, reduzindo *draw calls*.

## 🧱 Arquitetura e Módulos

O projeto é dividido em scripts que representam "módulos" de comportamento:

### 🧑 Player
* `PlayerInputHandler.cs`: **(Ouvinte)** A única fonte de input. Lê os dados do Input System e os armazena.
* `PlayerMovement.cs`: **(Executor)** Lê os dados do `InputHandler` e aplica velocidade ao `Rigidbody2D`.
* `PlayerCombat.cs`: **(Executor)** Lê o input de tiro, gerencia o *cooldown* e instancia os projéteis.
* `PlayerAnimation.cs`: **(Executor)** Lê o `InputHandler` e atualiza os parâmetros `MoveX`, `MoveY` e `isMoving` do Animator.
* `PlayerStats.cs`: **(Banco de Dados)** Armazena dados (vidas, moedas) e dispara eventos estáticos quando eles mudam.
* `PlayerInteraction.cs`: **(Detector)** Gerencia colisões *trigger* (coletar moedas, tomar dano ao tocar inimigo) e notifica o `PlayerStats`.

### 👾 Inimigo
* `EnemyAI.cs`: **(Cérebro)** Decide qual estado está ativo (Patrulha ou Perseguição) e habilita/desabilita os scripts de comportamento.
* `EnemyPatrol.cs`: **(Comportamento)** Move o `Rigidbody2D` entre os pontos A e B.
* `EnemyChase.cs`: **(Comportamento)** Move o `Rigidbody2D` em direção ao jogador.
* `EnemyAnimation.cs`: **(Executor)** Lê a velocidade do `Rigidbody2D` para atualizar a Blend Tree de animação.
* `EnemyHealth.cs`: **(Banco de Dados)** Armazena a vida do inimigo e o destrói quando a vida chega a zero.

### 🌎 Mundo e Sistema
* `ProjectileController.cs`: Lógica do projétil (mover para frente e detectar colisão `OnTriggerEnter2D`).
* `UIController.cs`: **(Ouvinte)** Se inscreve nos eventos estáticos do `PlayerStats` para atualizar os textos de vida e moedas, sem acoplamento direto.

## 💻 Tecnologias Utilizadas

* **Engine:** Unity 2022.3 (ou superior)
* **Linguagem:** C#
* **Pacotes Unity:**
    * Input System
    * 2D Tilemap Editor
    * 2D Sprite (Sprite Atlas)

## 🏁 Como Executar

1.  Clone este repositório: `git clone [URL-DO-SEU-REPOSITÓRIO]`
2.  Abra o projeto com o **Unity Hub** (use a versão do Unity especificada acima).
3.  A Unity pode pedir para reiniciar para habilitar o **Novo Input System**. Aceite.
4.  Abra a cena principal em `Assets/Scenes/`.
5.  Pressione **Play**.

## 🚀 Deploy

O projeto está configurado para build em **WebGL**.
1.  Vá em `File > Build Settings...`.
2.  Selecione `WebGL` e clique em `Switch Platform`.
3.  Em `Player Settings > Publishing Settings`, mude `Compression Format` para **Disabled**.
4.  Clique em `Build` e faça o upload do conteúdo da pasta para o [itch.io](https://itch.io/) (configurado como um projeto HTML).

---
