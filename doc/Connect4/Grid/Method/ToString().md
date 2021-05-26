# ToString() Method

<sub>Class: [Grid](../Grid.md)  
Namespace: [Connect4](../../Connect4.md)  
Assembly: Connect4.dll</sub>

Converts the grid into a string.

```cs
public override string ToString()
```

## Returns
string  
A string representation of a grid. Each space is represented by `[ ]`. If the space has a [token](../../Token/Token.md), the player ID of said token is written in the brackets.

### Example
```
[1][ ][ ]
[1][ ][ ]
[1][2][2]
```