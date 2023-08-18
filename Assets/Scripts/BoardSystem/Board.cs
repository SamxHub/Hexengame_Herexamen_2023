using GameSystem.Views;
using System;
using System.Collections.Generic;

namespace BoardSystem
{
    public class PieceMovedEventArgs : EventArgs
    {
        public Position FromPosition { get; }
        public Position ToPosition { get; }
        public PieceView PieceView { get; }

        public PieceMovedEventArgs(Position from, Position To, PieceView pieceView)
        {
            FromPosition = from;
            ToPosition = To;
            PieceView = pieceView;
        }
    }
    public class PieceTakenEventArgs : EventArgs
    {
        public Position HitPosition { get; }
        public PieceView PieceView { get; }
        public PieceTakenEventArgs(PieceView pieceView, Position hit)
        {
            PieceView = pieceView;
            HitPosition = hit;
        }
    }
    public class PiecePlacedEventArgs : EventArgs
    {
        public PieceView PieceView { get; }
        public Position PlacedPosition { get; }
        public PiecePlacedEventArgs(PieceView pieceView, Position position)
        {
            PieceView = pieceView;
            PlacedPosition = position;
        }
    }
    class Board
    {
        public event EventHandler<PieceMovedEventArgs> Moved;
        public event EventHandler<PieceTakenEventArgs> Taken;
        public event EventHandler<PiecePlacedEventArgs> Placed;

        private int _radius;

        public Dictionary<Position, PieceView> Pieces = new();

        public Board(int radius)
        {
            _radius = radius;
        }
        public bool Move(Position from, Position to)
        {
            if (!IsValid(from)) return false;
            if (!IsValid(to)) return false;

            if (Pieces.ContainsKey(to)) return false;
            if (!Pieces.TryGetValue(from, out var piece)) return false;

            Pieces[to] = piece;
            Pieces.Remove(from);

            OnMoveObject(new PieceMovedEventArgs(from, to, piece));
            return true;
        }
        public bool Take(Position from)
        {
            if (!IsValid(from)) return false;
            if (!Pieces.TryGetValue(from, out var piece)) return false;

            Pieces.Remove(from);

            OnTakeObject(new PieceTakenEventArgs(piece, from));

            return true;
        }
        public bool Place(Position to, PieceView pieceView)
        {
            if (!IsValid(to)) return false;
            if (Pieces.ContainsKey(to)) return false;
            if (Pieces.ContainsValue(pieceView)) return false;

            Pieces[to] = pieceView;

            OnPlaceObject(new PiecePlacedEventArgs(pieceView, to));
            return true;
        }
        public bool IsValid(Position position) => position.Distance >= -1 * _radius && position.Distance <= _radius;
        protected virtual void OnMoveObject(PieceMovedEventArgs eventArgs)
        {
            var handler = Moved;
            handler?.Invoke(this, eventArgs); 
        }
        protected virtual void OnTakeObject(PieceTakenEventArgs eventArgs)
        {
            var handler = Taken;
            handler?.Invoke(this, eventArgs);
        }
        protected virtual void OnPlaceObject(PiecePlacedEventArgs eventArgs)
        {
            var handler = Placed;
            handler?.Invoke(this, eventArgs);
        }
    }
}
