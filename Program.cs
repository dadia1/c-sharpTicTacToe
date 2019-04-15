using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        /// <summary>
        /// Crate a matrix.
        /// </summary>
        static string[,] matrix;

        /// <summary>
        /// if player won, int win return 1.
        /// </summary>
        static int win = 0;

        /// <summary>
        /// print 3 x 3 tic tac toe board.
        /// </summary>
        static void PrintBoard()
        {
            matrix = new string[3, 3];
            int num = 1;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = num.ToString();
                    Console.Write(" | " + matrix[i, j]);
                    num++;
                }

                Console.Write(" | ");
                Console.WriteLine();

                for (int p = 0; p < matrix.GetLength(0); p++)
                {
                    Console.Write(" ");
                    Console.Write("----");
                }
                Console.WriteLine();
            }
        }

        static void UpdateBoard(string[,] MX)
        {
            matrix = MX;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(" | " + matrix[i, j]);
                }

                Console.Write(" | ");
                Console.WriteLine();

                for (int p = 0; p < matrix.GetLength(0); p++)
                {
                    Console.Write(" ");
                    Console.Write("----");
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Code Game.
        /// </summary>
        static void PlayGame()
        {
            PrintBoard();//call the function and print.

            string[,] MATrix = matrix;//Call function with New  previous values.

            int player = 1; //By default player 1 is set 

            string choice = " "; //the choice at which position user want to mark
            int b = 0, v = 0;
            int cuonter = 0;
            Random random = new Random();
            int rand = random.Next(1, 9);
            do
            {
                if (player % 2 == 0)
                {
                    Console.WriteLine("PC turn");

                    //the pc put the "O" on empty place.
                    if (cuonter == 1)
                    {

                        for (v = 0; v < MATrix.GetLength(0); v++)
                        {
                            for (b = 0; b < MATrix.GetLength(1); b++)
                            {
                                if (MATrix[v, b] != "X" && MATrix[v, b] == rand.ToString())
                                {
                                    matrix[v, b] = "O";
                                    Thread.Sleep(1500);
                                    Console.Clear();      //whenever loop will be again start
                                    //then screen will be clear 
                                    UpdateBoard(MATrix);
                                    break;

                                }

                                if (MATrix[v, b] == "X" && MATrix[v, b] == rand.ToString())
                                {
                                    rand = random.Next(1, 9);
                                    break;
                                }
                            }
                        }



                        cuonter++;//In the next loop Go outside and never come back.
                    }

                    //the pc find the best place.
                    CheckPcToBlockPlayer(MATrix);
                    Thread.Sleep(1500);
                    Console.Clear();//whenever loop will be again start then screen will be clear 

                    UpdateBoard(MATrix);

                    Check_Who_Win(MATrix);

                    if (win == 1)
                    {
                        Console.WriteLine("The PC wins");
                        return;
                    }
                }

                else
                {
                    bool X_or_O = false;
                REch:
                    Console.WriteLine("\n your turn \n Choose where you want to " +
                       "put the X between 1 and 9 numbers");

                    choice = Console.ReadLine();


                    // if  enter eror 
                    int myid;
                    bool isString = int.TryParse(choice, out myid);

                    while (isString == false || choice.Length != 1 || int.Parse(choice) < 1
                        || choice == null)
                    {
                        if (isString == false || choice.Length != 1 || int.Parse(choice) < 1
                        || choice == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n EROR, enter only numbers between 1 to 9 thanks");
                            Console.ResetColor();
                            choice = Console.ReadLine();
                            isString = int.TryParse(choice, out myid);
                        }

                        else
                        {
                            isString = true;
                            break;
                        }
                    }

                    //start matrix

                    for (int m = 0; m < MATrix.GetLength(0); m++)
                    {
                        for (int n = 0; n < MATrix.GetLength(1); n++)
                        {
                            if (MATrix[m, n] == choice)
                            {
                                X_or_O = true;
                                break;
                            }
                        }
                        if (X_or_O == true)
                        {
                            break;
                        }
                    }

                    if (X_or_O == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n The place is already marked." +
                              " Please select a different place to mark. \n Thanks");
                        Console.ResetColor();
                        goto REch;
                    }


                    for (int p = 0; p < MATrix.GetLength(0); p++)
                    {
                        for (int t = 0; t < MATrix.GetLength(1); t++)
                        {
                            if (MATrix[p, t] == choice)
                            {
                                MATrix[p, t] = "X";
                                Thread.Sleep(1000);
                                Console.Clear();// whenever loop will be again start then screen will be clear 
                                UpdateBoard(MATrix);
                                Check_Who_Win(MATrix);

                                if (win == 1)
                                {
                                    Console.WriteLine("The player wins");
                                    return;
                                }
                            }
                        }
                    }
                }

                player++;
                cuonter++;
                if (cuonter == 10 && win != 1)
                {
                    Console.Clear();
                    UpdateBoard(MATrix);
                    Console.WriteLine("There are no winners :( :( :(");
                    return;
                }

            } while (true);
          
        }
        /// <summary>
        /// The PC Find the best place to mark "O".
        /// </summary>
        /// <param name="MiX">get matrix</param>
        static void CheckPcToBlockPlayer(string[,] MiX)
        {
            matrix = MiX;

            //Find the best place and Complete to mark "O" (3).
            if (matrix[0, 0] == matrix[0, 1] || matrix[2, 0] == matrix[1, 1]
                || matrix[2, 2] == matrix[1, 2])
            {
                if (matrix[0, 2] != "X" && matrix[0, 2] != "O")
                {
                    matrix[0, 2] = "O";
                    return;
                }
            }

            if (matrix[0, 1] == matrix[0, 2] || matrix[2, 2] == matrix[1, 1]
                || matrix[1, 0] == matrix[2, 0])
            {
                if (matrix[0, 0] != "X" && matrix[0, 0] != "O")
                {
                    matrix[0, 0] = "O";
                    return;
                }
            }

            if (matrix[1, 1] == matrix[1, 0] || matrix[2, 2] == matrix[0, 2])
            {
                if (matrix[1, 2] != "X" && matrix[1, 2] != "O")
                {
                    matrix[1, 2] = "O";
                    return;
                }
            }

            if (matrix[2, 0] == matrix[2, 1] || matrix[1, 1] == matrix[0, 0]
                || matrix[0, 2] == matrix[1, 2])
            {
                if (matrix[2, 2] != "X" && matrix[2, 2] != "O")
                {
                    matrix[2, 2] = "O";
                    return;
                }
            }

            if (matrix[1, 1] == matrix[2, 1] || matrix[0, 2] == matrix[0, 0])
            {
                if (matrix[0, 1] != "X" && matrix[0, 1] != "O")
                {
                    matrix[0, 1] = "O";
                    return;
                }
            }

            if (matrix[1, 1] == matrix[1, 2] || matrix[0, 0] == matrix[2, 0])
            {
                if (matrix[1, 0] != "X" && matrix[1, 0] != "O")
                {
                    matrix[1, 0] = "O";
                    return;
                }
            }

            if (matrix[0, 1] == matrix[2, 1] || matrix[0, 2] == matrix[2, 0]
                || matrix[0, 0] == matrix[2, 2] || matrix[1, 0] == matrix[1, 2])
            {
                if (matrix[1, 1] != "X" && matrix[1, 1] != "O")
                {
                    matrix[1, 1] = "O";
                    return;
                }
            }

            if (matrix[0, 2] == matrix[1, 1] || matrix[0, 0] == matrix[1, 0]
                || matrix[2, 1] == matrix[2, 2])
            {
                if (matrix[2, 0] != "X" && matrix[2, 0] != "O")
                {
                    matrix[2, 0] = "O";
                    return;
                }
            }

            if (matrix[0, 1] == matrix[1, 1] || matrix[2, 0] == matrix[2, 2])
            {
                if (matrix[2, 1] != "X" && matrix[2, 1] != "O")
                {
                    matrix[2, 1] = "O";
                    return;
                }
            }

            if (matrix[0, 0] == matrix[0, 2] && matrix[2, 1] == "X")
            {
                if (matrix[0, 1] == matrix[1, 1])
                {
                    matrix[1, 2] = "O";
                    return;
                }
            }

            if (matrix[0, 0] == matrix[2, 0] && matrix[1, 0] == matrix[1, 1])
            {
                if (matrix[1, 2] == "X")
                {
                    matrix[2, 1] = "O";
                    return;
                }
            }

            if (matrix[2, 0] == matrix[2, 2] && matrix[1, 1] == matrix[2, 1])
            {
                if (matrix[0, 1] == "X")
                {
                    matrix[1, 2] = "O";
                    return;
                }
            }

            if (matrix[0, 2] == matrix[2, 2] && matrix[1, 1] == matrix[1, 2])
            {
                if (matrix[1, 0] == "X")
                {
                    matrix[0, 1] = "O";
                    return;
                }
            }



            if (matrix[0, 0] == matrix[0, 2] && matrix[0, 1] == matrix[1, 1]
                && matrix[2, 0] == matrix[2, 2])
            {
                if (matrix[2, 1] == "O")
                {
                    matrix[1, 2] = "O";
                    return;
                }
            }

            // PC Block Player.

            if (matrix[0, 1] == matrix[1, 0] && matrix[1, 1] == matrix[2, 1]
                && matrix[0, 1] == matrix[1, 2])
            {
                matrix[2, 0] = "O";
                return;
            }

            if (matrix[0, 1] == matrix[2, 1] && matrix[1, 0] == matrix[1, 1]
                && matrix[0, 1] == matrix[1, 2])
            {
                matrix[2, 0] = "O";
                return;
            }

            if (matrix[0, 1] == matrix[1, 0] && matrix[1, 1] == matrix[1, 2]
                && matrix[0, 1] == matrix[2, 1])
            {
                matrix[2, 2] = "O";
                return;
            }

            if (matrix[0, 1] == matrix[1, 1] && matrix[1, 0] == matrix[1, 2]
                && matrix[1, 0] == matrix[2, 1])
            {
                matrix[0, 2] = "O";
                return;
            }


            if (matrix[0, 0] == matrix[0, 2] && matrix[1, 1] == matrix[2, 1]
                && matrix[0, 1] == matrix[2, 0])
            {
                matrix[2, 2] = "O";
                return;
            }

            if (matrix[0, 0] == matrix[1, 2] && matrix[1, 0] == matrix[1, 1]
                && matrix[0, 2] == matrix[2, 2])
            {
                matrix[2, 0] = "O";
                return;
            }

            if (matrix[0, 0] == matrix[2, 0] && matrix[1, 0] == matrix[0, 2]
                && matrix[1, 1] == matrix[1, 2])
            {
                matrix[2, 2] = "O";
                return;
            }

            if (matrix[0, 1] == matrix[1, 1] && matrix[2, 0] == matrix[2, 2]
                && matrix[2, 1] == matrix[0, 2])
            {
                matrix[0, 0] = "O";
                return;
            }

            if (matrix[0, 0] == matrix[0, 1] || matrix[1, 2] == matrix[2, 2]
                || matrix[2, 0] == matrix[1, 1])
            {
                if (matrix[0, 2] != "O" && matrix[0, 2] != "X")
                {
                    matrix[0, 2] = "O";
                    return;
                }
            }

            if (matrix[0, 1] == matrix[0, 2] || matrix[1, 0] == matrix[2, 0]
                || matrix[1, 1] == matrix[2, 2])
            {
                if (matrix[0, 0] != "O" && matrix[0, 0] != "X")
                {
                    matrix[0, 0] = "O";
                    return;
                }
            }

            if (matrix[1, 0] == matrix[1, 1] || matrix[0, 2] == matrix[2, 2])
            {
                if (matrix[1, 2] != "O" && matrix[1, 2] != "X")
                {
                    matrix[1, 2] = "O";
                    return;

                }
            }

            if (matrix[1, 1] == matrix[1, 2] || matrix[0, 0] == matrix[2, 0])
            {
                if (matrix[1, 0] != "O" && matrix[1, 0] != "X")
                {
                    matrix[1, 0] = "O";
                    return;
                }
            }

            if (matrix[2, 0] == matrix[2, 1] || matrix[0, 0] == matrix[1, 1]
                || matrix[0, 2] == matrix[1, 2])
            {
                if (matrix[2, 2] != "O" && matrix[2, 2] != "X")
                {
                    matrix[2, 2] = "O";
                    return;
                }
            }

            if (matrix[2, 1] == matrix[2, 2] || matrix[0, 0] == matrix[1, 0]
               || matrix[0, 2] == matrix[1, 1])
            {
                if (matrix[2, 0] != "O" && matrix[2, 0] != "X")
                {
                    matrix[2, 0] = "O";
                    return;
                }
            }

            if (matrix[0, 1] == matrix[1, 1] || matrix[2, 0] == matrix[2, 2])
            {
                if (matrix[2, 1] != "O" && matrix[2, 1] != "X")
                {
                    matrix[2, 1] = "O";
                    return;
                }

            }

            if (matrix[1, 1] == matrix[2, 1] || matrix[0, 0] == matrix[0, 2])
            {
                if (matrix[0, 1] != "O" && matrix[0, 1] != "X")
                {
                    matrix[0, 1] = "O";
                    return;
                }
            }

            if (matrix[0, 2] == matrix[2, 0] || matrix[0, 0] == matrix[2, 2]
               || matrix[0, 1] == matrix[2, 1] || matrix[1, 0] == matrix[1, 2])
            {
                if (matrix[1, 1] != "O" && matrix[1, 1] != "X")
                {
                    matrix[1, 1] = "O";
                    return;
                }
            }

            // Find the best place to mark "O" (2).

            // 1 == 3 && 2 == "O"  
            if (matrix[0, 0] == matrix[0, 2] && matrix[0, 1] == "O")
            {
                //5 != "O" && 5 != "X"
                if (matrix[1, 1] != "O" && matrix[1, 1] != "X")
                {
                    // 5 = "O"
                    matrix[1, 1] = "O";
                    return;
                }
            }

            // 4 == 6 && 5 == "O"  
            if (matrix[1, 0] == matrix[1, 2] && matrix[1, 1] == "O")
            {
                // 8 != "O" && 8 != "X"
                if (matrix[2, 1] != "O" && matrix[2, 1] != "X")
                {
                    // 8 = "O"
                    matrix[2, 1] = "O";
                    return;
                }
            }

            // 7 == 9 && 8 == "O"
            if (matrix[2, 0] == matrix[2, 2] && matrix[2, 1] == "O")
            {
                // 5 != "O" && 5 != "X"
                if (matrix[1, 1] != "O" && matrix[1, 1] != "X")
                {
                    // 5 = "O"
                    matrix[1, 1] = "O";
                    return;
                }
            }

            // 1 == 7 && 4 == "O" 
            if (matrix[0, 0] == matrix[2, 0] && matrix[1, 0] == "O")
            {
                //  5 != "O" && 5 != "X"
                if (matrix[1, 1] != "O" && matrix[1, 1] != "X")
                {
                    // 5 = "O"
                    matrix[1, 1] = "O";
                    return;
                }
            }

            // 2 == 8 && 5 == "O"  
            if (matrix[0, 1] == matrix[2, 1] && matrix[1, 1] == "O")
            {
                // 6 != "O" && 6 != "X"
                if (matrix[1, 2] != "O" && matrix[1, 2] != "X")
                {
                    // 6 = "O"
                    matrix[1, 2] = "O";
                    return;
                }
            }

            // 3 == 9 && 6 == "O" 
            if (matrix[0, 2] == matrix[2, 2] && matrix[1, 2] == "O")
            {
                //  5 != "O" && 5 != "x"
                if (matrix[1, 1] != "O" && matrix[1, 1] != "X")
                {
                    // 5 = "O"
                    matrix[1, 1] = "O";
                    return;
                }
            }

            // 3 == 7 && 5 == "O"
            if (matrix[0, 2] == matrix[2, 0] && matrix[1, 1] == "O")
            {
                // 9 != "O" && 9 != "X"
                if (matrix[2, 2] != "O" && matrix[2, 2] != "X")
                {
                    // 9 = "O"
                    matrix[2, 2] = "O";
                    return;
                }
            }

            // 1 == 9 && 5 == "O"
            if (matrix[0, 0] == matrix[2, 2] && matrix[1, 1] == "O")
            {
                // 7 != "O" && 7 != "X"
                if (matrix[2, 0] != "O" && matrix[2, 0] != "X")
                {
                    // 7 = "O"
                    matrix[2, 0] = "O";
                    return;
                }
            }

            // 1 == 2 && 3 == "O"
            if (matrix[0, 0] == matrix[0, 1] && matrix[0, 2] == "O")
            {
                //  7 != "x" && 7 != "O"
                if (matrix[2, 0] != "O" && matrix[2, 0] != "X")
                {
                    // 7 = "O"
                    matrix[2, 0] = "O";
                    return;
                }
            }

            // 5 == 8 && 2 == "O"
            if (matrix[1, 1] == matrix[2, 1] && matrix[0, 1] == "O")
            {
                // 3 != "X" && 3 != "O"
                if (matrix[0, 2] != "O" && matrix[0, 2] != "X")
                {
                    // 3 = "O"
                    matrix[0, 2] = "O";
                    return;
                }
            }

            // 5 == 9 && 1 == "O"
            if (matrix[1, 1] == matrix[2, 2] && matrix[0, 0] == "O")
            {
                //  3 != "X" && 3 != "O"
                if (matrix[0, 2] != "O" && matrix[0, 2] != "X")
                {
                    // 3 = "O"
                    matrix[0, 2] = "O";
                    return;
                }
            }

            // 4 == 5 && 6 == "O"
            if (matrix[1, 0] == matrix[1, 1] && matrix[1, 2] == "O")
            {
                //  9 != "X" && 9 != "O"
                if (matrix[2, 2] != "O" && matrix[2, 2] != "X")
                {
                    // 9 = "O"
                    matrix[2, 2] = "O";
                    return;
                }
            }

            // 5 == 7 && 3 == "O"
            if (matrix[1, 1] == matrix[2, 0] && matrix[0, 2] == "O")
            {
                //  9 != "X" && 9 != "O"
                if (matrix[2, 2] != "O" && matrix[2, 2] != "X")
                {
                    // 9 = "O"
                    matrix[2, 2] = "O";
                    return;
                }
            }

            // 6 == 9 && 3 == "O"
            if (matrix[1, 2] == matrix[2, 2] && matrix[0, 2] == "O")
            {
                // 7 != "X" && 7 != "O"
                if (matrix[2, 0] != "O" && matrix[2, 0] != "X")
                {
                    // 7 = "O"
                    matrix[2, 0] = "O";
                    return;
                }
            }

            // 2 == 3 && 1 == "O"
            if (matrix[0, 1] == matrix[0, 2] && matrix[0, 0] == "O")
            {
                //  5 != "X" && 5 != "O"
                if (matrix[1, 1] != "O" && matrix[1, 1] != "X")
                {
                    // 5 = "O"
                    matrix[1, 1] = "O";
                    return;
                }
            }

            // 4 == 7 && 1 == "O"
            if (matrix[1, 0] == matrix[2, 0] && matrix[0, 0] == "O")
            {
                //  9 != "X" && 9 != "O"
                if (matrix[2, 2] != "O" && matrix[2, 2] != "X")
                {
                    // 9 = "O"
                    matrix[2, 2] = "O";
                    return;
                }
            }

            // 5 == 6 && 4 == "O" 
            if (matrix[1, 1] == matrix[1, 2] && matrix[1, 0] == "O")
            {
                //  7 != "X" && 7 != "O"
                if (matrix[2, 0] != "O" && matrix[2, 0] != "X")
                {
                    // 7 = "O"
                    matrix[2, 0] = "O";
                    return;
                }
            }

            // 3 == 5 && 7 == "O"
            if (matrix[0, 2] == matrix[1, 1] && matrix[2, 0] == "O")
            {
                //  9 != "X" && 9 != "O"
                if (matrix[2, 2] != "O" && matrix[2, 2] != "X")
                {
                    // 9 = "O"
                    matrix[2, 2] = "O";
                    return;
                }
            }

            // 8 == 9 && 7 == "O"
            if (matrix[2, 1] == matrix[2, 2] && matrix[2, 0] == "O")
            {
                // 3 != "X" && 3 != "O"
                if (matrix[0, 2] != "O" && matrix[0, 2] != "X")
                {
                    // 3 = "O"
                    matrix[0, 2] = "O";
                    return;
                }
            }

            // 1 == 4 && 7 == "O"
            if (matrix[0, 0] == matrix[1, 0] && matrix[2, 0] == "O")
            {
                // 3 != "X" && 3 != "O"
                if (matrix[0, 2] != "O" && matrix[0, 2] != "X")
                {
                    // 3 = "O"
                    matrix[0, 2] = "O";
                    return;
                }
            }

            // 2 == 5 && 8 == "O"
            if (matrix[0, 1] == matrix[1, 1] && matrix[2, 1] == "O")
            {
                // 9 != "X" && 9 != "O"
                if (matrix[2, 2] != "O" && matrix[2, 2] != "X")
                {
                    // 9 = "O"
                    matrix[2, 2] = "O";
                    return;
                }
            }

            // 1 == 5 && 9 == "O" 
            if (matrix[0, 0] == matrix[1, 1] && matrix[2, 2] == "O")
            {
                // 7 != "X" && 7 != "O"
                if (matrix[2, 0] != "O" && matrix[2, 0] != "X")
                {
                    // 7 = "O"
                    matrix[2, 0] = "O";
                    return;
                }
            }

            // 3 == 6 && 9 == "O"
            if (matrix[0, 2] == matrix[1, 2] && matrix[2, 2] == "O")
            {
                // 8 != "X" && 8 != "O"
                if (matrix[2, 1] != "O" && matrix[2, 1] != "X")
                {
                    // 8 = "O"
                    matrix[2, 1] = "O";
                    return;
                }
            }

            // 7 == 8 && 9 == "O"
            if (matrix[2, 0] == matrix[2, 1] && matrix[2, 2] == "O")
            {
                //  5 != "X" && 5 != "O"
                if (matrix[1, 1] != "O" && matrix[1, 1] != "X")
                {
                    // 5 = "O"
                    matrix[1, 1] = "O";
                    return;
                }
            }


        }

        static void Check_Who_Win(string[,] Update_AND_check_Matrix)
        {

            matrix = Update_AND_check_Matrix;
            //horizontal
            if (matrix[0, 0] == matrix[0, 1] && matrix[0, 0] == matrix[0, 2])
            {
                win = 1;
                return;
            }

            if (matrix[1, 0] == matrix[1, 1] && matrix[1, 0] == matrix[1, 2])
            {
                win = 1;
                return;
            }

            if (matrix[2, 0] == matrix[2, 1] && matrix[2, 0] == matrix[2, 2])
            {
                win = 1;
                return;
            }

            //Vertical
            if (matrix[0, 0] == matrix[1, 0] && matrix[0, 0] == matrix[2, 0])
            {
                win = 1;
                return;
            }

            if (matrix[0, 1] == matrix[1, 1] && matrix[0, 1] == matrix[2, 1])
            {
                win = 1;
                return;
            }

            if (matrix[0, 2] == matrix[1, 2] && matrix[0, 2] == matrix[2, 2])
            {
                win = 1;
                return;
            }

            //diagonal
            if (matrix[0, 0] == matrix[1, 1] && matrix[0, 0] == matrix[2, 2])
            {
                win = 1;
                return;
            }

            if (matrix[0, 2] == matrix[1, 1] && matrix[0, 2] == matrix[2, 0])
            {
                win = 1;
                return;
            }


        }
        static void Main(string[] args)
        {
            //Play game (into function "PlayGame()"call the function
            //print the board and call function update and function who win )
            PlayGame();
            Console.ReadKey();
        }
    }
}


