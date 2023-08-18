using BoardSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Views
{
    public class PositionEventArgs : EventArgs
    {
        public Position Position { get; }
        public CardView Card { get; }
        public PositionEventArgs(Position pos, CardView card)
        {
            Position = pos;
            Card = card;
        }
    }
    class BoardView
    {
        public event EventHandler<PositionEventArgs> PositionDrop;
        public event EventHandler<PositionEventArgs> HoverEnter;
        public event EventHandler<PositionEventArgs> HoverExit;

        public CardView DroppedCard;

        private Dictionary<Position, TileView> _tileViewsCached = new();
        private List<Position> _activatedPositions = new();

        public List<Position> ActivatedPositions
        {
            set
            {
                foreach(var position in _activatedPositions)
                {
                    if (_tileViewsCached.ContainsKey(position))
                        _tileViewsCached[position].Deactivate();
                }
                if (value == null)
                    _activatedPositions = new(0);
                else
                    _activatedPositions = value;

                foreach (var position in _activatedPositions)
                    if (_tileViewsCached.ContainsKey(position))
                        _tileViewsCached[position].Activate();
            }            
        }
    }
}
