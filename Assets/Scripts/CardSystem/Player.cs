namespace CardSystem
{
    public enum Player
    {
        Player,
        Enemy
    }
    public class StateMachinePlayer
    {
        private Player currentPlayer = new();
        private Enemy enemy = new();
        public void ChangePlayer(Player newPlayer)
        {
            if (newPlayer == currentPlayer)
                return;

            currentPlayer = newPlayer;

            if (currentPlayer == Player.Enemy)
                enemy.ChangePosition();
        }
    }
}
