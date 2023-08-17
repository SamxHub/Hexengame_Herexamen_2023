using BoardSystem;
using UnityEngine;

// A class that provides helper functions for converting between grid positions and world positions
public class PositionHelper : MonoBehaviour
{
    // A constant representing the size of a tile on the grid
    public const int TileSize = 1;

    // Function to convert a world position to a grid position
    public static Position GridPosition(Vector3 worldPosition)
    {
        // Scale the world position by dividing it by the tile size
        Vector3 scaleWorldPosition = worldPosition / TileSize;
        // Calculate the row position (gridPositionR) based on the scaled world position
        /// 0.65f is used to account for the spacing of the hexagonal cells
        /// world positions is translated into the row positions, considering the staggered layout
        int gridPostionR = Mathf.RoundToInt(scaleWorldPosition.z * 0.65f);

        // Calculate the column position based on the scaled world position
        int gridPositionColumn = Mathf.RoundToInt((scaleWorldPosition.x - (Mathf.Abs(gridPostionR % 2) * (Mathf.Sqrt(3) / 2))) / Mathf.Sqrt(3));
        // Calculate the final Q and R values of the hexagonal grid based on column and row
        int gridPositionQ = gridPositionColumn - (gridPostionR - (gridPostionR & 1)) / 2;

        // Create a new Position instance using the calculated Q and R values
        return new Position(gridPositionQ, gridPostionR);
    }

    // Function to convert a grid position to a world position
    public static Vector3 WorldPosition(Position gridPosition)
    {
        // Calculate the Y component of the world position based on the grid position's row
        float worldPositionY = (gridPosition.R / 0.65f);

        // Calculate the column part of the world position based on grid position
        float worldPositionColom = gridPosition.Q + (gridPosition.R - (gridPosition.R & 1)) / 2;

        // Calculate the X component of the world position based on column and row
        float worldPositionX = worldPositionColom * Mathf.Sqrt(3) + ((Mathf.Abs(gridPosition.R % 2) * (Mathf.Sqrt(3) / 2)));

        // Create a new Vector3 instance using the calculated X, Y, and Z (0) values
        return new Vector3(worldPositionX, 0, worldPositionY);
    }
}
