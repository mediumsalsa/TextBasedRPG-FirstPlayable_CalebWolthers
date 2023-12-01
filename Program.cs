
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Reflection.Emit;

namespace TextBasedRPG_FirstPlayable_CalebWolthers
{
    internal class Program
    {
        static string breaker = "------------------------";


        //Map Variables
        static int currentRow = 0;
        static int currentCol = 0;

        static int width;
        static int height;



        static string[] mapFile;

        static char[,] map;



        //Player Variables
        static char player = 'P';

        static int playerHealth = 100;
        static string healthStatus;

        static int playerPosX;
        static int playerPosY;

        static int nextPosX;
        static int nextPosY;

        static int lastPosX;
        static int lastPosY;

        //Enemy Variables
        static char goblin = 'G';

        static int goblinDamage = 19;

        //Goblin 1
        static int goblinPosX;
        static int goblinPosY;
        static bool goblinUp = true;
        //Goblin 2
        static int goblinPosX_2;
        static int goblinPosY_2;
        static bool goblinUp_2 = false;

        static int goblinNextPosX;
        static int goblinNextPosY;

        static int goblinLastPosX;
        static int goblinLastPosY;

        






        static void Main(string[] args)
        {

            StartGame();


            while (Console.ReadKey(true).Key != ConsoleKey.E)
            {
                

                DisplayMap();

                GetInput();

            }


            Console.ReadKey();


        }

        static void StartGame()
        {
            Console.CursorVisible = !true;

            mapFile = File.ReadAllLines(@"TextFile2.txt");

            map = new char[mapFile.Length, mapFile[0].Length];

            MakeMap();

            width = map.GetLength(1);
            height = map.GetLength(0);

            Console.WriteLine("Current map width: " + width);
            Console.WriteLine("Current map height: " + height);
            Console.WriteLine();
            Console.WriteLine("Press any key....");

            map[13, 5] = player;
            playerPosX = 5;
            playerPosY = 13;

            map[29, 27] = goblin;
            goblinPosX = 27;
            goblinPosY = 29;

            map[24, 15] = goblin;
            goblinPosX_2 = 15;
            goblinPosY_2 = 24;

        }

        static void MakeMap()
        {
            for (int i = 0; i < mapFile.Length; i++)
            {
                for (int j = 0; j < mapFile[0].Length; j++)
                {
                    map[i,j] = mapFile[i][j];
                }
            }
        }

        static void TakeDamage(int damage)
        {
            playerHealth -= damage;
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

            if (playerPosY != 0 && map[nextPosY, playerPosX] != '#' && map[nextPosY, playerPosX] != '~')
            {
                
                playerPosY--;

                PlayerMoved();

                Console.WriteLine("W");
            }

            DisplayMap();



        }
        static void KeyA()
        {
            nextPosX = playerPosX - 1;

            lastPosY = playerPosY;
            lastPosX = playerPosX;

            if (playerPosX != 0 && map[playerPosY, nextPosX] != '#' && map[playerPosY, nextPosX] != '~')
            {
                playerPosX--;

                PlayerMoved();

                Console.WriteLine("A");
            }

            DisplayMap();



        }
        static void KeyS()
        {
            nextPosY = playerPosY + 1;

            lastPosY = playerPosY;
            lastPosX = playerPosX;

            if (playerPosY != height - 1 && map[nextPosY, playerPosX] != '#' && map[nextPosY, playerPosX] != '~')
            {
                playerPosY++;

                PlayerMoved();

                Console.WriteLine("S");
            }

            DisplayMap();

            

        }
        static void KeyD()
        {
            nextPosX = playerPosX + 1;

            lastPosY = playerPosY;
            lastPosX = playerPosX;

            if (playerPosX != width - 1 && map[playerPosY, nextPosX] != '#' && map[playerPosY, nextPosX] != '~')
            {
                playerPosX++;

                PlayerMoved();

                Console.WriteLine("D");
            }

            DisplayMap();

        }

        static void PlayerMoved()
        {
            map[lastPosY, lastPosX] = '`';

            if (map[playerPosY, playerPosX] == 'G')
            {
                TakeDamage(goblinDamage);
            }

            map[playerPosY, playerPosX] = player;

            MoveGoblin(ref goblinPosX, ref goblinPosY, ref goblinUp);
            MoveGoblin(ref goblinPosX_2, ref goblinPosY_2, ref goblinUp_2);
        }


        //C stands for parameter... idk
        static void MoveGoblin(ref int CgoblinPosX, ref int CgoblinPosY, ref bool CgoblinUp)
        {

            if (CgoblinUp == true)
            {
                goblinNextPosY = CgoblinPosY - 1;

                goblinLastPosY = CgoblinPosY;
                goblinLastPosX = CgoblinPosX;

                if (CgoblinPosY != 0 && map[goblinNextPosY, CgoblinPosX] != '#' && map[goblinNextPosY, CgoblinPosX] != '~')
                {

                    CgoblinPosY--;

                    map[goblinLastPosY, goblinLastPosX] = '`';

                    map[CgoblinPosY, CgoblinPosX] = goblin;
                }
                else { CgoblinUp = false; }
            }

            else if (CgoblinUp == false)
            {
                goblinNextPosY = CgoblinPosY + 1;

                goblinLastPosY = CgoblinPosY;
                goblinLastPosX = CgoblinPosX;

                if (CgoblinPosY != height - 1 && map[goblinNextPosY, CgoblinPosX] != '#' && map[goblinNextPosY, CgoblinPosX] != '~')
                {

                    CgoblinPosY++;

                    map[goblinLastPosY, goblinLastPosX] = '`';

                    map[CgoblinPosY, CgoblinPosX] = goblin;
                }
                else {  CgoblinUp = true; }
            }



        }


        static void DrawTile()
        {
            if (map[currentRow, currentCol] == '`')
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (map[currentRow, currentCol] == '~')
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (map[currentRow, currentCol] == 'P')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (map[currentRow, currentCol] == '#')
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else if (map[currentRow, currentCol] == 'G')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }

            Console.Write(map[currentRow, currentCol]);
        }


        static void DisplayMap()
        {

            Console.CursorVisible = !true;

            Console.SetCursorPosition(0, 0);
            //Console.Clear();

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

            Console.CursorVisible = !true;
        }


        static void ShowHUD()
        {
            if (playerHealth == 100)
            { healthStatus = "Perfect Health"; }

            else if (playerHealth < 99 && playerHealth >= 90)
            { healthStatus = "Healthy"; }

            else if (playerHealth < 89 && playerHealth >= 75)
            { healthStatus = "Hurt"; }

            else if (playerHealth < 74 && playerHealth >= 50)
            { healthStatus = "Badly Hurt"; }

            else if (playerHealth < 49 && playerHealth >= 20)
            { healthStatus = "Danger"; }

            else if (playerHealth < 19 && playerHealth > 0)
            { healthStatus = "ALMOST DEAD"; }

            else { healthStatus = "Dead"; }


            Console.WriteLine("");
            Console.WriteLine(breaker);
            Console.WriteLine("Health: " + playerHealth);
            Console.WriteLine("Health Status: " + healthStatus);
            Console.WriteLine(breaker);
            Console.WriteLine("");

            Console.CursorVisible = !true;
        }



    }
        }
    


