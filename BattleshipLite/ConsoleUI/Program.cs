using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicLibrary;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel opponent = CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            do
            {
                //Display grid from activePlayer on where they fired
                DisplayShotGrid(activePlayer);

                RecordPlayerShot(activePlayer, opponent);

                //Determine if the game should continue
                bool doesGameContinue = GameLogic.PlayerStillActive(opponent);
               
                if (doesGameContinue == true)
                {
                    // swap using a temp variable
                    //PlayerInfoModel tempHolder = opponent;
                    //opponent = activePlayer;
                    //activePlayer = tempHolder;

                    //use Tuple -> Swap positions
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    winner = activePlayer;
                }

            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations to { winner.Username} for winning!");
            Console.WriteLine($"{winner.Username} took {GameLogic.GetShotCount(winner)} shots.");
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            bool isValidShot = false;
            string row = "";
            int column = 0;

            do
            {
                string shot = AskForShot(activePlayer); ;
                try
                {
                    (row, column) = GameLogic.SplitShotIntoRowAndCollumn(shot);
                    isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
                }
                catch (Exception ex)
                {
                    isValidShot=false;
                }

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot location. Please try again");
                }

            } while (isValidShot == false);

            //Determine shot results
            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

            //Record results
            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);

            DisplayShotResults(row, column, isAHit);
        }

        private static void DisplayShotResults(string row, int column, bool isAHit)
        {
            if (isAHit)
            {
                Console.WriteLine($"{row}{column} is a Hit"); 
            }
            else
            {
                Console.WriteLine($"{row}{column} is a Miss");
            }

            Console.WriteLine();
        }

        private static string AskForShot(PlayerInfoModel player)
        {
            Console.Write($" {player.Username}, please enter your shot selection: ");
            string output = Console.ReadLine();

            return output;
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {   //displays to console, so method is not in a library
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            Console.WriteLine($"Shot Display for {activePlayer.Username}: ");

            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }    

                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }
                else if(gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X  ");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O  ");
                }
                else
                {
                    Console.WriteLine(" ?  ");
                }

               
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("created by Rebecca Göbel");
            Console.WriteLine();
        }
        public static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();

            Console.WriteLine($"Player information for {playerTitle}");
            //Ask user for their name
            output.Username = AskForUsersName();
            //Load up the shot grid
            GameLogic.InitializeGrid(output);

            //Ask the user for their 5 ship placements
            PlaceShips(output);

            //Clear 
            Console.Clear();

            return output;

        }

        private static string AskForUsersName()
        {
            Console.Write("What is your name: ");
            string output = Console.ReadLine();

            return output;
        }

        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where do you want to place ship number {model.ShipLocations.Count + 1}: ");
                string location = Console.ReadLine();

                bool isValidLocation = false;
                try
                {
                    isValidLocation = GameLogic.PlaceShip(model, location);
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error: " + ex.Message);
                }

                if (isValidLocation == false)
                {
                    Console.WriteLine("That was not a valid location. Please try again.");
                }
                                
            } while (model.ShipLocations.Count < 5);
        }
                
    }
}
