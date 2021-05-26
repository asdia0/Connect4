# Game Class

<sub>Namespace: [Connect4](../Connect4.md)  
Assembly: Connect4.dll</sub>

Defines a game of Connect4.

```cs
public class Game
```

## Constructors
| Constructor | Description |
| ----------- | ----------- |
| [Game(Grid, int, int)](Constructor/Game(Grid,%20int,%20int).md) | Intialises a new instance of the Game class. |
| [Game(Game)](Constructor/Game(Game).md) | Clones an instance of the Game class. |

## Fields
| Field | Description |
| ----- | ----------- |
| [Grid](Field/Grid.md) | The [Grid](../Grid/Grid.md) where the game is taking place. |
| [MoveList](Field/MoveList.md) | A of [moves](../Move/Move.md) made in the game. |
| [Players](Field/Players.md) | The number of players in the game. |
| [ToWin](Field/ToWin.md) | The number of [tokens](../Token/Token.md) needed in a row to win the game. |
| [Turn](Field/Turn.md) | The ID of the player to play. |

## Methods
| Method | Description |
| ------ | ----------- |
| [GetWinner()](Method/GetWinner().md) | Gets the ID of player who won the game. |
| [IsDraw()](Method/IsDraw().md) | Checks if the game has ended in a draw. |
| [IsFilled(int)](Method/IsFilled(int).md) | Checks if the column specified is filled completely. |
| [IsWinningMove(int)](Method/IsWinningMove(int).md) | Checks if playing in the column specified wins the game. |
| [Play(int)](Method/Play(int).md) | Adds a [token](../Token/Token.md) to the column specified. |
| [ToString()](Method/ToString().md) | Converts the game into a string. |
| [Undo(int)](Method/Undo(int).md) | Removes a [token](../Token/Token.md) from the column specified. |
