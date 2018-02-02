using Game;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Game
{
    class GameRound
    {
        public List<Card> RoundCard = new List<Card>();
        public List<int> IdPlayers = new List<int>();
        public GameElement GameElement;
        public Boolean Err { get; private set; }
        public String ErrorMessage { get; private set; } = "No error";
        public Player Player { get; private set; }
        public int IndexTurn { get; private set; } = 0;
        private Card CurrentCard;
        public int indexOfWinner { get; private set; }
        public String Trump { get; set; }
        public String Message { get; private set; }
        public int WinningTeam { get; private set; }

        public void Round(Player player)
        {
            if (player.LastInput.Equals("hand"))
            {
                player.PrintHand();
                Err = true;
                ErrorMessage = "";
            }
            else
            {
                Err = false;
                indexOfWinner = -1;
                Player = player;
                ErrorMessage = "";
                RoundManager();
            }
        }

        private void RoundManager()
        {
            RoundCheck();
            if (!Err)
            {
                RoundCard.Add(CurrentCard);
                IdPlayers.Add(Player.Id);
                Player.Hand.RemoveCard(GameElement.ColorInput, GameElement.ValueInput);
                IndexTurn += 1;
                Message = "Player " + Player.Name + " played " + Player.LastInput;
            }
            if (IndexTurn == 4)
            {
                RoundCard.Clear();
                ResolveRound();
                IndexTurn = 0;
            }
        }

        private void ResolveRound()
        {
            int tmp = 0;
            int trumptmp = 0;
            int idWinner = -1;
            for (int i = 0; i < 4; i++)
            {
                if (RoundCard[i].Color.Equals(GameElement.GetColorOfTheRound()) && trumptmp == 0)
                {
                    if (RoundCard[i].Strength > tmp)
                    {
                        tmp = RoundCard[i].Strength;
                        idWinner = IdPlayers[i];
                    }
                }
                else if (RoundCard[i].Color.Equals(Trump))
                {
                    if (RoundCard[i].Strength > trumptmp)
                    {
                        trumptmp = RoundCard[i].Strength;
                        idWinner = IdPlayers[i];
                    }
                }
            }
            Console.WriteLine("Winner of this round is " + idWinner);
            WinningTeam = PlayerHandler.GetPlayerById(idWinner).Team;
            indexOfWinner = idWinner;
        }

        private void RoundCheck()
        {
            GameElement = new GameElement(Player.LastInput, RoundCard);
            if (!GameElement.CheckInput())
            {
                Err = true;
                ErrorMessage = "Wrong input : [Color] [Value].";
                return;
            }
            CurrentCard = new Card(GameElement.ColorInput, GameElement.ValueInput);
            if (!GameElement.CheckHasCard(CurrentCard, Player) && GameElement.CheckInput())
            {
                Err = true;
                ErrorMessage = "You don't have that card.";
                return;
            }
            if (IndexTurn != 0)
                if (!GameElement.CheckColor(CurrentCard))
                {
                    if (GameElement.CheckIfPlayerHasColor(Player))
                    {
                        Err = true;
                        ErrorMessage = "You have to play some " + GameElement.GetColorOfTheRound() + "\n [Color] [Value]";
                        return;
                    }
                }
        }
    }
}
