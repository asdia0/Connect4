# FindBestColumn(int, int, int) Method

<sub>Class: [Solve](../Solve.md)  
Namespace: [Connect4](../../Connect4.md)  
Assembly: Connect4.dll</sub>

Finds the best column to play in.

```cs
public static int FindBestColumn(int length, int breadth, int toWin)
```

## Parameters
`length`: int  
The length of the [grid](../../Grid/Grid.md).

`breadth`: int  
The breadth of the [grid](../../Grid/Grid.md).

`toWin`: int  
The number of [tokens](../../Token/Token.md) needed in a row to win the [game](../../Game/Game.md).

## Returns
int  
The ID of the column to play in. 0 represents the left most column, 1 represents the second column to the left, and so on.