using BoardSystem;
using UnityEngine;

// A class that provides helper functions for converting between grid positions and world positions
public class PositionHelper : MonoBehaviour
{
    public const int TileSize = 1;
    public static Position GridPosition(Vector3 worldPosition)
    {
        Vector3 scaleWorldPosition = worldPosition / TileSize;
        int gridPositionR = Mathf.RoundToInt(scaleWorldPosition.z * 0.65f);

        int gridPositionColumn = Mathf.RoundToInt((scaleWorldPosition.x - (Mathf.Abs(gridPositionR % 2) * (Mathf.Sqrt(3) / 2))) / Mathf.Sqrt(3));
        int gridPositionQ = gridPositionColumn - (gridPositionR - (gridPositionR & 1)) / 2;

        return new Position(gridPositionQ, gridPositionR);
    }
    public static Vector3 WorldPosition(Position gridPosition)
    {
        float worldPositionY = (gridPosition.R / 0.65f);
        float worldPositionColumn = gridPosition.Q + (gridPosition.R - (gridPosition.R & 1)) / 2;
        float worldPositionX = worldPositionColumn * Mathf.Sqrt(3) + ((Mathf.Abs(gridPosition.R % 2) * (Mathf.Sqrt(3) / 2)));

        return new Vector3(worldPositionX, 0, worldPositionY);
    }
}
