# Grid Class

<sub>Namespace: [Connect4](../Connect4.md)  
Assembly: Connect4.dll</sub>

Defines a grid.

```cs
public class Grid
```

## Constructors
| Constructor | Description |
| ----------- | ----------- |
| [Grid(int, int)](Constructor/Grid(int,%20int)) | Initialises a new instance of the Grid class. |
| [Grid(Grid)](Constructor/Grid(Grid)) | Clones an instance of the Grid class. |

## Fields
| Field | Description |
| ----- | ----------- |
| [Breadth](Field/Breadth.md) | The breadth of the grid. |
| [Length](Field/Length.md) | The length of the grid. |
| [Tokens](Field/Tokens.md) | An array of [tokens](../Token/Token.md) that represent the spaces on the grid. |

## Methods
| Method | Description |
| ------ | ----------- |
| [GetColumns(int)](Method/GetColumns(int).md) | Returns a list of columns of a specified length. |
| [GetDiagonals(int)](Method/GetDiagonals(int).md) | Returns a list of diagonals of a specified length. |
| [GetRows(int)](Method/GetRows(int).md) | Returns a list of rows of a specified length. |
| [ToString()](Method/ToString().md) | Converts the grid into a string. |