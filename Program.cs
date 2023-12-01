
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
        static int playerSTR = 50;
        static string healthStatus;

        static int playerPosX;
        static int playerPosY;

        static int nextPosX;
        static int nextPosY;

        static int lastPosX;
        static int lastPosY;

        //Enemy Variables

        static int goblinDamage = 20;

        //Goblin 1
        static char goblin = 'G';
        static int goblinPosX;
        static int goblinPosY;
        static bool goblinUp = true;
        static int goblinHealth = 100;
        //Goblin 2
        static char goblin_2 = 'O';
        static int goblinPosX_2;
        static int goblinPosY_2;
        static bool goblinUp_2 = false;
        static int goblinHealth_2 = 100;



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
            Console.Clear();

            Console.SetCursorPosition(0, 0);

            Console.CursorVisible = !true;

            mapFile = File.ReadAllLines(@"TextFile2.txt");

            map = new char[mapFile.Length, mapFile[0].Length];

            MakeMap();

            width = map.GetLength(1);
            height = map.GetLength(0);

            Console.WriteLine("Game Begins");
            Console.WriteLine();
            Console.WriteLine("Current map width: " + width);
            Console.WriteLine("Current map height: " + height);
            Console.WriteLine();
            Console.WriteLine("Press any key....");

            Console.ReadKey();

            playerHealth = 100;
            map[13, 5] = player;
            playerPosX = 5;
            playerPosY = 13;

            goblin = 'G';
            map[11, 40] = goblin;
            goblinPosX = 40;
            goblinPosY = 11;

            goblin_2 = 'O';
            map[18, 15] = goblin_2;
            goblinPosX_2 = 15;
            goblinPosY_2 = 18;

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

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                StartGame();
            }

        }

        static void DamageEnemy(int damage, ref int health, ref char enemy)
        {
            health -= damage;
            if (health <= 0)
            {
                if (enemy == goblin_2)
                {
                    map[goblinPosY_2, goblinPosX_2] = '`';
                    enemy = '`';
                    DisplayMap();
                }
                if (enemy == goblin)
                {
                    map[goblinPosY, goblinPosX] = '`';
                    enemy = '`';
                    DisplayMap();
                }


                DisplayMap();
            }
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
                if (map[nextPosY, playerPosX] == 'G')
                {
                    DamageEnemy(playerSTR, ref goblinHealth, ref goblin);
                    TakeDamage(goblinDamage);
                }
                else if (map[nextPosY, playerPosX] == 'O')
                {
                    DamageEnemy(playerSTR, ref goblinHealth_2, ref goblin_2);
                    TakeDamage(goblinDamage);
                }
                else
                { 
                playerPosY--;

                PlayerMoved();

                Console.WriteLine("W");
                }
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
                if (map[playerPosY, nextPosX] == 'G')
                {
                    DamageEnemy(playerSTR, ref goblinHealth, ref goblin);
                    TakeDamage(goblinDamage);
                }
                else if (map[playerPosY, nextPosX] == 'O')
                {
                    DamageEnemy(playerSTR, ref goblinHealth_2, ref goblin_2);
                    TakeDamage(goblinDamage);
                }
                else
                {
                    playerPosX--;

                    PlayerMoved();

                    Console.WriteLine("A");
                }
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
                if (map[nextPosY, playerPosX] == 'G')
                {
                    DamageEnemy(playerSTR, ref goblinHealth, ref goblin);
                    TakeDamage(goblinDamage);
                }
                else if (map[nextPosY, playerPosX] == 'O')
                {
                    DamageEnemy(playerSTR, ref goblinHealth_2, ref goblin_2);
                    TakeDamage(goblinDamage);
                }
                else
                {
                    playerPosY++;

                    PlayerMoved();

                    Console.WriteLine("S");
                }
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
                if (map[playerPosY, nextPosX] == 'G')
                {
                    DamageEnemy(playerSTR, ref goblinHealth, ref goblin);
                    TakeDamage(goblinDamage);
                }
                else if (map[playerPosY, nextPosX] == 'O')
                {
                    DamageEnemy(playerSTR, ref goblinHealth_2, ref goblin_2);
                    TakeDamage(goblinDamage);
                }
                else
                {
                    playerPosX++;

                    PlayerMoved();

                    Console.WriteLine("D");
                }
            }

            DisplayMap();

        }

        static void PlayerMoved()
        {
            map[lastPosY, lastPosX] = '`';


            map[playerPosY, playerPosX] = player;

            MoveGoblin(ref goblinPosX, ref goblinPosY, ref goblinUp, goblin);
            MoveGoblin(ref goblinPosX_2, ref goblinPosY_2, ref goblinUp_2, goblin_2);
        }


        //C stands for parameter... idk
        static void MoveGoblin(ref int CgoblinPosX, ref int CgoblinPosY, ref bool CgoblinUp, char enemy)
        {
            
                if (CgoblinUp == true)
                {
                    goblinNextPosY = CgoblinPosY - 1;

                    goblinLastPosY = CgoblinPosY;
                    goblinLastPosX = CgoblinPosX;

                    if (CgoblinPosY != 0 && map[goblinNextPosY, CgoblinPosX] != '#' && map[goblinNextPosY, CgoblinPosX] != '~')
                    {
                        if (map[goblinNextPosY, CgoblinPosX] == 'P' && enemy != '`')
                        {
                            TakeDamage(goblinDamage);
                        }
                        else
                        {
                            CgoblinPosY--;

                            map[goblinLastPosY, goblinLastPosX] = '`';

                            map[CgoblinPosY, CgoblinPosX] = enemy;
                        
                        }
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
                        if (map[goblinNextPosY, CgoblinPosX] == 'P' && enemy != '`')
                        {
                            TakeDamage(goblinDamage);
                        }
                        else
                        {

                            CgoblinPosY++;

                            map[goblinLastPosY, goblinLastPosX] = '`';

                            map[CgoblinPosY, CgoblinPosX] = enemy;
                        
                        }
                    }
                    else { CgoblinUp = true; }
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
            else if (map[currentRow, currentCol] == 'O')
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

            ShowHUD();
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


            Console.SetCursorPosition(0, height + 2);
            Console.WriteLine("                                                ");
            Console.WriteLine("                                                ");
            Console.WriteLine("                                                ");
            Console.WriteLine("                                                ");
            Console.WriteLine("                                                ");
            Console.WriteLine("                                                ");
            Console.WriteLine("                                                ");
            Console.WriteLine("                                                ");
            Console.WriteLine("                                                ");
            Console.SetCursorPosition(0, height + 2
                );



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
    


