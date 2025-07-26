# Connect Four – WPF Game 🎮

A classic **Connect Four** game built using **C# and WPF (Windows Presentation Foundation)**. This project includes a fully interactive grid UI, customizable player names, and logic for determining the winner!

## 🧩 Features

- 🔴🔵 Two-player mode (Red vs. Blue)
- 🧠 Automatic win detection (horizontal, vertical, diagonal)
- 🌟 Highlights the winning 4 tiles with a `*`
- 🎨 Simple, clean WPF grid-based layout
- 🖱️ Clickable buttons for column selection
- 🆕 "New Game" button to reset the board

## 📸 Screenshot

<img width="787" height="915" alt="image" src="https://github.com/user-attachments/assets/e26499ed-87b4-4aa8-a1d9-f64a8b28ecbd" />

## 🚀 Getting Started

### Requirements

- Windows OS
- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later recommended)
- Visual Studio 2022+ (or any IDE that supports WPF)

## 🛠️ How it Works
- The game board is a 6x7 grid created dynamically at runtime.
- Player turns are indicated by button background color (Red/Blue).
- Each move drops a piece into the selected column.
- When four connected pieces are found, the game ends, and winning pieces are visually marked with a *.
