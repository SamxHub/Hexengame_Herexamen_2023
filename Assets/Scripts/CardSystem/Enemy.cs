using BoardSystem;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    class Enemy
    {
        private StateMachinePlayer player;

        public Board Board { get; set; }
        private GameObject[] _pieces;
        private GameObject[] _enemies;
        private GameObject[] _boardPieces;

        public void ChangePosition()
        {
            Move();
        }

        private void Move()
        {
            //find every enemy
            _enemies = FindGameObjectsWithLayer("Piece", "Enemies");
            // Find the coordinates of all hexagons

            _boardPieces = FindGameObjectsWithLayer("Board", "Tile");

            //change all positions
            PositionChange(_enemies);

        }
        private void PositionChange(GameObject[] enemies)
        {
            // Check if there are enough board pieces to move all enemies
            if (_boardPieces.Length < enemies.Length)
            {
                Debug.LogWarning("Not enough board pieces for all enemies.");
                return;
            }

            // Create a list of available board piece indices
            List<int> availableIndices = new List<int>();
            for (int i = 0; i < _boardPieces.Length; i++)
            {
                availableIndices.Add(i);
            }

            // Iterate through each enemy and assign a random hexagon position
            foreach (GameObject enemy in enemies)
            {
                // Check if there are any available positions left
                if (availableIndices.Count == 0)
                {
                    Debug.LogWarning("No available board pieces for enemy movement.");
                    break;
                }

                // Choose a random index from the available positions
                int randomIndex = Random.Range(0, availableIndices.Count);
                int chosenIndex = availableIndices[randomIndex];

                // Assign the position and remove the chosen index from available positions
                Transform hex = _boardPieces[chosenIndex].transform;
                enemy.transform.position = hex.position;
            }
        }

        // Function to find game objects with a specific tag and layer
        private GameObject[] FindGameObjectsWithLayer(string tag, string layer)
        {
            GameObject[] taggedGameObjects = GameObject.FindGameObjectsWithTag(tag);
            List<GameObject> result = new();

            foreach (GameObject go in taggedGameObjects)
                if (go.layer == LayerMask.NameToLayer(layer))
                    result.Add(go);

            return result.ToArray();
        }
    }
}

