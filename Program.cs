using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace TextBasedRPG_FirstPlayable_CalebWolthers
{
    internal class Program
    {

        //Map Variables
        static int currentRow = 0;
        static int currentCol = 0;

        static int width;
        static int height;
        
        //Player Variables
        static char player = 'P';

        static int playerPosX;
        static int playerPosY;

        static int nextPosX;
        static int nextPosY;

        static int lastPosX;
        static int lastPosY;

        //Enemy Variables
        static char goblin = 'G';

        static int goblinPosX;
        static int goblinPosY;

        static int goblinNextPosX;
        static int goblinNextPosY;

        static int goblinLastPosX;
        static int goblinLastPosY;

        static bool goblinUp = true;





        static void Main(string[] args)
        {
            width = map.GetLength(1);
            height = map.GetLength(0);

            Console.WriteLine("Current map width: " + width);
            Console.WriteLine("Current map height: " + height);

            map[height / 2, width / 2] = player;
            playerPosX = width / 2;
            playerPosY = height / 2;


            while (Console.ReadKey(true).Key != ConsoleKey.E)
            {
                

                DisplayMap();

                GetInput();

            }


            Console.ReadKey();


        }


        static void MoveGoblin()
        {

        }

        static void GetInput()
        {
            var exit = false;

            ConsoleKeyInfo keyInfo;

            do
            {
                Console.WriteLine("use wasd to move");

                keyInfo = Console.ReadKey(true);

                Console.WriteLine();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.W:
                        KeyW();
                        break;

                    case ConsoleKey.A:
                        KeyA();
                        break;

                    case ConsoleKey.S:
                        KeyS();
                        break;

                    case ConsoleKey.D:
                        KeyD();
                        break;

                    default:
                        //ExitProgram();
                        break;

                }
            }

            while (exit == false);
        }

        static void KeyW()
        {
            nextPosY = playerPosY - 1;

            lastPosY = playerPosY;
            lastPosX = playerPosX;

            if (playerPosY != 0 && map[nextPosY, playerPosX] != '#')
            {
                playerPosY--;

                map[lastPosY, lastPosX] = '~';

                map[playerPosY, playerPosX] = player;

                Console.WriteLine("W");
            }

            DisplayMap();



        }
        static void KeyA()
        {
            nextPosX = playerPosX - 1;

            lastPosY = playerPosY;
            lastPosX = playerPosX;

            if (playerPosX != 0 && map[playerPosY, nextPosX] != '#')
            {
                playerPosX--;

                map[lastPosY, lastPosX] = '~';

                map[playerPosY, playerPosX] = player;

                Console.WriteLine("A");
            }

            DisplayMap();



        }
        static void KeyS()
        {
            nextPosY = playerPosY + 1;

            lastPosY = playerPosY;
            lastPosX = playerPosX;

            if (playerPosY != height - 1 && map[nextPosY, playerPosX] != '#')
            {
                playerPosY++;

                map[lastPosY, lastPosX] = '~';

                map[playerPosY, playerPosX] = player;

                Console.WriteLine("S");
            }

            DisplayMap();

            

        }
        static void KeyD()
        {
            nextPosX = playerPosX + 1;

            lastPosY = playerPosY;
            lastPosX = playerPosX;

            if (playerPosX != width - 1 && map[playerPosY, nextPosX] != '#')
            {
                playerPosX++;

                map[lastPosY, lastPosX] = '~';

                map[playerPosY, playerPosX] = player;

                Console.WriteLine("D");
            }

            DisplayMap();


        }

        static void DrawTile()
        {
            if (map[currentRow, currentCol] == '~')
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else if (map[currentRow, currentCol] == 'P')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (map[currentRow, currentCol] == '#')
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (map[currentRow, currentCol] == 'G')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                MoveGoblin();
            }

            Console.Write(map[currentRow, currentCol]);
        }


        static void DisplayMap()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("+");
            for (int i = 0; i < width; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");

            while (currentCol < width)
            {
                if (currentCol == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("|");
                }

                DrawTile();
                currentCol++;

                if (currentCol >= width)
                {
                    currentRow++;
                    if (currentRow < height)
                    {
                        currentCol = 0;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("|");
                        Console.WriteLine();
                    }
                    else
                    {
                        currentCol = width + 2;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("|");
                    }
                }

            }
            Console.WriteLine("");
            Console.Write("+");
            for (int i = 0; i < width; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            
            currentCol = 0;
            currentRow = 0;
        }






                static char[,] map = new char[,] // dimensions defined by following data:
                {
             {'~','~','~','~','~','~','~','~','~','~','~','~','G','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','#','#','#','#','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','#','~','~','#','~','~','~','~','~','~','~','~','~','~','~','~','~','~','#','#','#','#','#','#','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','#','~','~','#','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','#','#','#','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','#','#','#','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','#','#','#','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
             {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','#','#','#','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},



                };
            }
        }
    


