# 📡 Radar Meteorológico Estilizado

> Projeto do 2º Bimestre — Disciplina: Introdução à Computação Gráfica  
> Colégio Técnico Antônio Teixeira Fernandes (Univap) — Curso Técnico em Informática  
> Turma: 3°J

---

## 📋 Sobre o Projeto

Aplicação desenvolvida em **C# Windows Forms** que simula o módulo de visualização de um **sistema de radar meteorológico estilizado**. O programa renderiza diferentes tipos de retas para representar frentes de vento e limites territoriais, além de um feixe rotativo animado com gradiente de cor.

---

## ✨ Funcionalidades

### 🔄 Feixe Rotativo Animado
- Reta que gira **360° continuamente** a partir do centro da tela, simulando a varredura de um radar real
- Apresenta **gradiente linear** entre **Verde** (centro) e **Preto** (ponta), alternando a direção do gradiente a cada volta completa
- Espessura **4px**
- Animado via `Timer` com ~50 FPS

### 🖱️ Desenho de Frentes de Vento
O usuário clica em **dois pontos** dentro do radar para definir a trajetória de uma nova frente de vento. O estilo da reta depende da tecla pressionada:

| Tecla | Cor | Estilo | Espessura | Representa |
|-------|-----|--------|-----------|------------|
| `1`   | 🔴 Vermelho | Tracejada (`DashPattern {5,2}`) | 2px | Frente de baixa pressão |
| `2`   | 🔵 Azul | Traço-ponto (`DashPattern {5,2,1,2}`) | 3px | Limite de zona de segurança |

### 🗑️ Limpeza
Botão **"LIMPAR FRENTES"** remove todas as frentes de vento desenhadas.

---

## 🧮 Conceitos Aplicados

Todos os algoritmos foram implementados **manualmente** usando apenas as primitivas ensinadas em aula, sem bibliotecas externas.

```
// Fórmula de rotação do feixe (dada pelo professor)
Xc = x + raio * cos(angle * 3.15 / 180)
Yc = y + raio * sin(angle * 3.15 / 180)
```

| Conceito | Implementação |
|----------|---------------|
| Primitiva de ponto | `DrawLine(caneta, x, y, x+1, y)` |
| Criação de cor | `Color.FromArgb(r, g, b)` |
| Espessura de linha | `new Pen(cor, espessura)` |
| Linha tracejada | `caneta.DashPattern = new float[]{ 5, 2 }` |
| Linha traço-ponto | `caneta.DashPattern = new float[]{ 5, 2, 1, 2 }` |
| Gradiente linear | Interpolação RGB ponto a ponto ao longo do raio |
| Captura do mouse | `MouseEventArgs e.X / e.Y` |
| Animação | `Timer.Tick` + `pictureBox1.Invalidate()` |
| Rotação trigonométrica | `Math.Cos` / `Math.Sin` |

---

## 🗂️ Estrutura do Projeto

```
ProjetoRadar2Bim/
│
├── Form1.cs              # Lógica principal + todas as primitivas gráficas
├── Form1.Designer.cs     # Definição da interface (PictureBox, Labels, Botão)
└── README.md
```

### Métodos principais em `Form1.cs`

```
criaCor()          → cria uma cor RGB
criaCaneta()       → cria uma Pen com cor e espessura
pintaPonto()       → desenha um pixel via DrawLine de 1px
interpolaCor()     → gradiente entre duas cores (usado no feixe)
desenhafeixe()     → feixe rotativo com gradiente ponto a ponto
retaTracejada()    → reta vermelha tracejada (Ação A)
retaTracoPonto()   → reta azul traço-ponto   (Ação B)
desenhaRadar()     → círculos e eixos do radar
desenhaFrentes()   → redesenha todas as frentes salvas
```

---

## 🚀 Como Executar

**Pré-requisitos:** Visual Studio com suporte a Windows Forms (.NET Framework)

```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/ProjetoRadar2Bim.git

# 2. Abra o arquivo .sln no Visual Studio

# 3. Compile e execute
F5
```

---

## 🎮 Como Usar

```
1. O feixe verde começa a girar automaticamente ao abrir o programa.

2. Pressione [1] para selecionar o modo Vermelho Tracejado.
   Pressione [2] para selecionar o modo Azul Traço-ponto.

3. Clique uma vez no radar → define o Ponto A.
   Clique novamente       → define o Ponto B.
   A frente de vento é desenhada entre os dois pontos.

4. Repita quantas vezes quiser, alternando os modos.

5. Clique em "LIMPAR FRENTES" para apagar tudo e recomeçar.
```

---

## 🛠️ Tecnologias

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows_Forms-0078D6?style=for-the-badge&logo=windows&logoColor=white)

---

## 👨‍💻 Autores

| Nome | Turma |
|------|-------|
| João Vinicius dos Santos  | 3°J |
| Mateus Ricardo dos Santos | 3°J |

---

## 👨‍🏫 Professor

**Wagner Santos C. de Jesus** — wagner@univap.br

---

*Fundação Valeparaibana de Ensino — Colégios Univap*
