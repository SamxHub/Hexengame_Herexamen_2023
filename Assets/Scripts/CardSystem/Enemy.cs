//using BoardSystem;
//using System;
//using UnityEngine;

//namespace CardSystem
//{
//    class Enemy
//    {
//        private StateMachinePlayer player = new();
//        public Board Board { get; set; }
//        private GameObject[] _pieces;
//        private GameObject[] _enemies;
//        private GameObject[] _boardPieces;

//        public void ChangePosition()
//        {
//            Move();
//            player.ChangePlayer(Player.Player);
//        }

//        private void Move()
//        {
//            //find every enemy
//            _enemies = FindGameObjectsWithLayer("Player", "Enemies");
//            //change all positions
//            PositionChange(_enemies);
            
//        }

//        private void PositionChange(GameObject[] enemies)
//        {
//            /// find the coordinates of all hexagons
//            _boardPieces = FindGameObjectsWithLayer("Untagged", "")
//            /// place the pieces on the board
//            /// make sure the place isn't already occupied
//            /// Check if the position is on the board.
//        }

//            public GameObject[] FindGameObjectsWithLayer(string tag, string layer)
//        {
//            //Find all Gameobjects with tag Player
//            _pieces = GameObject.FindGameObjectsWithTag(tag);

//            // Find in those gameobjects the "players" with the layer enemies
//            int index = 0;
//            GameObject[] layerPieces = new GameObject[int.MaxValue];

//            foreach (GameObject go in _pieces)
//                if (go.layer.ToString() == layer)
//                {
//                    layerPieces[index] = go;
//                    index++;
//                }
//            // return those
//            return layerPieces;
//        }
//    }
//}

