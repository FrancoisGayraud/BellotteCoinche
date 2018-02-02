using Protocol;
using server.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Game
    {
        enum GameState
        {
            announce,
            game
        };
        private GameState CurrentGameState;
        private AnnounceRound AnnounceRound;
        private GameRound GameRound;
        public int Id { get; }
        private List<Player> GamePlayers;
        private int currentTurnIndex;
        private Deck deck = new Deck();
        public bool NeedEndGame { get; private set; }
        private int NbOfTurn { get; set; }

        public Game(int gameId, List<Player> gamePlayers)
        {
            Console.WriteLine("Game Begins");
            deck.PrintDeck();
            CurrentGameState = GameState.announce;
            AnnounceRound = new AnnounceRound();
            GameRound = new GameRound();
            Id = gameId;
            currentTurnIndex = 0;
            NeedEndGame = false;
            NbOfTurn = 0;
            GamePlayers = gamePlayers;
            gamePlayers[0].Team = 1;
            gamePlayers[1].Team = 1;
            gamePlayers[2].Team = 2;
            gamePlayers[3].Team = 2;
            for (int i = 0; i < 4; i++)
            {
                gamePlayers[i].Hand = deck.Distribute();
                gamePlayers[i].PrintHand();
            }
            WriteToPlayers(new Standard("A new belote coinchee game is starting !"));
            GamePlayers[0].Connection.SendObject("Standard", Serialization.Serialize(new Standard("It is your turn to make an announce.")).Data);
        }

        public void EndGame()
        {
            WriteToPlayers(new Standard("Game is shutting down."));
            GamePlayers.ForEach(x => x.ResetPlayer());
        }

        public void HandleTurn(Player player)
        {
            if (player.Id.Equals(GamePlayers[currentTurnIndex].Id))
            {
                if (CurrentGameState == GameState.announce)
                {
                    if (AnnounceRound.BiggestAnnounceElement == null && AnnounceRound.PassCount == 4)
                    {
                        NeedEndGame = true;
                    }
                    else if (AnnounceRound.BiggestAnnounceElement != null && (AnnounceRound.PassCount == 3 || AnnounceRound.BiggestAnnounceElement.Value == 160))
                    {
                        CurrentGameState = GameState.game;
                        WriteToPlayers(new Standard("--NEW ROUND--"));
                        GameRound.Trump = AnnounceRound.BiggestAnnounceElement.Type;
                    }
                    else
                    {
                        AnnounceRound.Round(player);
                        if (player.AnnounceElement != null)
                        {
                            player.Connection.SendObject("Standard", Serialization.Serialize(new Standard("Your announce was registered.")).Data);
                            WriteToPlayers(new Standard("Player " + player.Name + " announced " + player.AnnounceElement.Type + " " + player.AnnounceElement.Value));
                            ChangeTurnIndex();
                        }
                        else
                        {
                            player.Connection.SendObject("Standard", Serialization.Serialize(new Standard(AnnounceElement.AnnounceUsage)).Data);
                        }
                        if (AnnounceRound.BiggestAnnounceElement == null && AnnounceRound.PassCount == 4)
                        {
                            WriteToPlayers(new Protocol.Standard("4 players passed without making an announce."));
                            GameHandler.GetGameById(Id).EndGame();
                            GameHandler.DestroyGameById(Id);
                        }
                    }
                }
                else
                {
                    GameRound.Round(player);
                    if (GameRound.Err != true)
                    {
                        WriteToPlayers(new Standard(GameRound.Message));
                        ChangeTurnIndexWinnerPlayer();
                        if (NbOfTurn == 8)
                            GameOver();
                    }
                    else
                    {
                        Standard tosend = new Standard(GameRound.ErrorMessage);
                        player.Connection.SendObject("Standard", Serialization.Serialize(tosend).Data);
                    }
                        
                }
                GamePlayers[currentTurnIndex].Connection.SendObject("Standard", Serialization.Serialize(new Standard("It is your turn to " + (CurrentGameState == GameState.announce ? "announce" : "play a card"))).Data);
            }
            else
            {
                player.Connection.SendObject("Standard", Serialization.Serialize(new Standard("It is not your turn.")).Data);
            }
        }

        private void GameOver()
        {
            var big = GamePlayers.OrderByDescending(Player => Player.Score).First();
            WriteToPlayers(new Standard("GAME OVER, " + big.Team + " win with a score of " + big.Score));
        }

        internal void RemovePlayer(Player player)
        {
            GamePlayers.Remove(player);
        }

        private void ChangeTurnIndexWinnerPlayer()
        {
            Console.WriteLine("id winner is " + GameRound.indexOfWinner);
            if (GameRound.indexOfWinner == -1)
            {
                currentTurnIndex += 1;
                if (currentTurnIndex > 3)
                    currentTurnIndex = 0;
            }
            else
            {
                currentTurnIndex = GameRound.indexOfWinner;
                NbOfTurn += 1;
                List<Player> tmp = GamePlayers.FindAll(x => x.Team == GameRound.WinningTeam);
                tmp.ForEach(x => x.Score += 10);
                WriteToPlayers(new Standard("Player " + PlayerHandler.GetPlayerById(GameRound.indexOfWinner).Name + " has win the round.\n--NEW ROUND--"));
            }
                
        }

        private void ChangeTurnIndex()
        {
            currentTurnIndex += 1;
            if (currentTurnIndex > 3)
                currentTurnIndex = 0;
        }

        public void WriteToPlayers(Standard standard)
        {
            GamePlayers.ForEach(player => player.Connection.SendObject("Standard", Serialization.Serialize(standard).Data));
        }
    }
}