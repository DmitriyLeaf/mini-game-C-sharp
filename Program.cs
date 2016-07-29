using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


//handle mapscreen = 
// COORD name;
//name=m
//srtconsolecursorposition(cor, maps)

namespace Game_sharp_0._2
{
    static class Human
    {
        static private int player;

        static public int coordinat_x = 1;
        static public int coordinat_y = 1;
        static private int look;

        static public int coordinat_2_x = 15;
        static public int coordinat_2_y = 45;
        static private int look_2;

        static private char symbol;
        static public int show_flag = 0;

        static private int fire_quan = 0;
        static private int fire_limit = 5;
        static private int[] fire_x = new int[100];
        static private int[] fire_y = new int[100];
        static private int[] fire_look_x = new int[100];
        static private int[] fire_look_y = new int[100];

        static private int fire_2_quan = 0;
        static private int fire_2_limit = 5;
        static private int[] fire_2_x = new int[100];
        static private int[] fire_2_y = new int[100];
        static private int[] fire_2_look_x = new int[100];
        static private int[] fire_2_look_y = new int[100];


        static public char[,] a = new char[100, 100];
        static public int n = 15, m = 45;

        [DllImport("msvcrt")] static extern int _getch();

        static public void show_table()
        {
            while (true)
            {
                fire();
                if (show_flag == 1)
                {
                    Console.Clear();
                    for (int i = 0; i <= n + 1; i++)
                    {
                        for (int j = 0; j <= m + 1; j++)
                        {
                            if (i == 0) Console.Write("#");
                            else if (i == n + 1) Console.Write("#");
                            else if (j == m + 1 && (i != 0 || i != n + 1)) Console.WriteLine("#");
                            else if (j == 0 && (i != 0 || i != n + 1)) Console.Write("#");
                            else Console.Write(a[i, j]);
                            if (i == 0 && j == m + 1) Console.WriteLine();
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("(c) Kruglow 2015");
                    show_flag = 0;
                }
                Thread.Sleep(8);
            }
        }

        static private void clear_human()
        {
            a[coordinat_x, coordinat_y] = ' ';
            a[coordinat_2_x, coordinat_2_y] = ' ';
        }

        static public void show_human()
        {
            a[coordinat_x, coordinat_y] = (char)2;
            a[coordinat_2_x, coordinat_2_y] = (char)1;
        }

        static private void check_coord()
        {
            if (coordinat_x > n) coordinat_x -= 1;
            if (coordinat_x < 1) coordinat_x += 1;
            if (coordinat_y > m) coordinat_y -= 1;
            if (coordinat_y < 1) coordinat_y += 1;

            if (a[coordinat_x, coordinat_y] == '#')
            {
                switch (look)
                {
                    case 1: coordinat_x++; break;
                    case 3: coordinat_y++; break;
                    case 2: coordinat_x--; break;
                    case 4: coordinat_y--; break;
                }
            }

            if (coordinat_2_x > n) coordinat_2_x -= 1;
            if (coordinat_2_x < 1) coordinat_2_x += 1;
            if (coordinat_2_y > m) coordinat_2_y -= 1;
            if (coordinat_2_y < 1) coordinat_2_y += 1;

            if (a[coordinat_2_x, coordinat_2_y] == '#')
            {
                switch (look_2)
                {
                    case 1: coordinat_2_x++; break;
                    case 3: coordinat_2_y++; break;
                    case 2: coordinat_2_x--; break;
                    case 4: coordinat_2_y--; break;
                }
            }
        }

        static private void show_object()
        {
            if (player == 0)
            {
                switch (look)
                {
                    case 4: a[coordinat_x, coordinat_y + 1] = '#'; break;
                    case 3: a[coordinat_x, coordinat_y - 1] = '#'; break;
                    case 1: a[coordinat_x - 1, coordinat_y] = '#'; break;
                    case 2: a[coordinat_x + 1, coordinat_y] = '#'; break;
                }
            }

            if (player == 1)
            {
                switch (look_2)
                {
                    case 4: a[coordinat_2_x, coordinat_2_y + 1] = '#'; break;
                    case 3: a[coordinat_2_x, coordinat_2_y - 1] = '#'; break;
                    case 1: a[coordinat_2_x - 1, coordinat_2_y] = '#'; break;
                    case 2: a[coordinat_2_x + 1, coordinat_2_y] = '#'; break;
                }
            }
        }

        static private void clear_object()
        {
            if (player == 0)
            {
                switch (look)
                {
                    case 4: a[coordinat_x, coordinat_y + 1] = ' '; break;
                    case 3: a[coordinat_x, coordinat_y - 1] = ' '; break;
                    case 1: a[coordinat_x - 1, coordinat_y] = ' '; break;
                    case 2: a[coordinat_x + 1, coordinat_y] = ' '; break;
                }
            }

            if (player == 1)
            {
                switch (look_2)
                {
                    case 4: a[coordinat_2_x, coordinat_2_y + 1] = ' '; break;
                    case 3: a[coordinat_2_x, coordinat_2_y - 1] = ' '; break;
                    case 1: a[coordinat_2_x - 1, coordinat_2_y] = ' '; break;
                    case 2: a[coordinat_2_x + 1, coordinat_2_y] = ' '; break;
                }
            }
        }

        static public void fire()
        {
            if (fire_quan > 0)
            {
                for (int i = 1; i <= fire_quan; i++)
                {
                    a[fire_x[i], fire_y[i]] = ' ';
                    fire_x[i] = fire_x[i] + fire_look_x[i];
                    fire_y[i] = fire_y[i] + fire_look_y[i];
                    if (fire_y[i] > m || fire_x[i] > n || fire_y[i] < 1 || fire_x[i] < 1 || a[fire_x[i], fire_y[i]] == '#')
                    {
                        a[fire_x[i], fire_y[i]] = ' ';
                        //a[fire_x[i] + fire_look_x[i], fire_y[i] + fire_look_y[i]] = ' ';
                        for (int j = i; j <= fire_quan; j++)
                        {
                            fire_x[j] = fire_x[j + 1];
                            fire_y[j] = fire_y[j + 1];
                            fire_look_x[j] = fire_look_x[j + 1];
                            fire_look_y[j] = fire_look_y[j + 1];
                        }
                        i--;
                        fire_quan--;
                    }
                    if (a[fire_x[i], fire_y[i]] == (char)1)
                    {
                        player = 0;
                        game_over();
                        fire_2_quan = 0;
                        fire_quan = 0;
                        break;
                    }
                    a[fire_x[i], fire_y[i]] = (char)4;
                }
                show_flag = 1;
            }

            if (fire_2_quan > 0)
            {
                for (int i = 1; i <= fire_2_quan; i++)
                {
                    a[fire_2_x[i], fire_2_y[i]] = ' ';
                    fire_2_x[i] = fire_2_x[i] + fire_2_look_x[i];
                    fire_2_y[i] = fire_2_y[i] + fire_2_look_y[i];
                    if (fire_2_y[i] > m || fire_2_x[i] > n || fire_2_y[i] < 1 || fire_2_x[i] < 1 || a[fire_2_x[i], fire_2_y[i]] == '#')
                    {
                        a[fire_2_x[i], fire_2_y[i]] = ' ';
                        //a[fire_x[i] + fire_look_x[i], fire_y[i] + fire_look_y[i]] = ' ';
                        for (int j = i; j <= fire_2_quan; j++)
                        {
                            fire_2_x[j] = fire_2_x[j + 1];
                            fire_2_y[j] = fire_2_y[j + 1];
                            fire_2_look_x[j] = fire_2_look_x[j + 1];
                            fire_2_look_y[j] = fire_2_look_y[j + 1];
                        }
                        i--;
                        fire_2_quan--;
                    }
                    if (a[fire_2_x[i], fire_2_y[i]] == (char)2)
                    {
                        player = 1;
                        game_over();
                        fire_2_quan = 0;
                        fire_quan = 0;
                        break;
                    }
                    a[fire_2_x[i], fire_2_y[i]] = (char)4;
                }
                show_flag = 1;
            }
        }
        static private void game_over()
        {
            show_flag = 1;
            char death = (char)1;
            if (player == 0) death = (char)2;
            if (player == 1) death = (char)1;
            Console.WriteLine();
            Console.WriteLine("GAME OVER ");
            Console.WriteLine();
            Console.Write(death);
            Console.WriteLine(" <--WINNER");
            Console.ReadLine();

            for (int i = 0; i < n+1; i++)
                for (int j = 0; j < m+1; j++) a[i, j] = ' ';


            coordinat_x = 1;
            coordinat_y = 1;

            coordinat_2_x = n;
            coordinat_2_y = m;

            show_human();
            show_flag = 1;
        }

        static public void step()
        {
            symbol = (char)_getch();
            clear_human();
            switch (symbol)
            {
                case 'w': look = 1; coordinat_x--; break;
                case 'a': look = 3; coordinat_y--; break;
                case 's': look = 2; coordinat_x++; break;
                case 'd': look = 4; coordinat_y++; break;

                case 'i': look_2 = 1; coordinat_2_x--; break;
                case 'j': look_2 = 3; coordinat_2_y--; break;
                case 'k': look_2 = 2; coordinat_2_x++; break;
                case 'l': look_2 = 4; coordinat_2_y++; break;

                case 'c': player = 0; show_object(); break;
                case 'v': player = 0; clear_object(); break;
                case 'f':
                    fire_quan++;
                    if (fire_quan > fire_limit)
                    {
                        fire_quan--;
                    }
                    else
                    {
                        switch (look)
                        {
                            case 1: fire_look_x[fire_quan] = -1; fire_look_y[fire_quan] = 0; break;
                            case 3: fire_look_x[fire_quan] = 0; fire_look_y[fire_quan] = -1; break;
                            case 2: fire_look_x[fire_quan] = 1; fire_look_y[fire_quan] = 0; break;
                            case 4: fire_look_x[fire_quan] = 0; fire_look_y[fire_quan] = 1; break;
                        }
                        fire_x[fire_quan] = coordinat_x + fire_look_x[fire_quan];
                        fire_y[fire_quan] = coordinat_y + fire_look_y[fire_quan];
                    }
                    break;

                case '1': player = 1; show_object(); break;
                case '2': player = 1; clear_object(); break;
                case '5':
                    fire_2_quan++;
                    if (fire_2_quan > fire_2_limit)
                    {
                        fire_2_quan--;
                    }
                    else
                    {
                        switch (look_2)
                        {
                            case 1: fire_2_look_x[fire_2_quan] = -1; fire_2_look_y[fire_2_quan] = 0; break;
                            case 3: fire_2_look_x[fire_2_quan] = 0; fire_2_look_y[fire_2_quan] = -1; break;
                            case 2: fire_2_look_x[fire_2_quan] = 1; fire_2_look_y[fire_2_quan] = 0; break;
                            case 4: fire_2_look_x[fire_2_quan] = 0; fire_2_look_y[fire_2_quan] = 1; break;
                        }
                        fire_2_x[fire_2_quan] = coordinat_2_x + fire_2_look_x[fire_2_quan];
                        fire_2_y[fire_2_quan] = coordinat_2_y + fire_2_look_y[fire_2_quan];
                    }
                    break;
            }
            check_coord();
            show_human();
            show_flag = 1;
        }
    }

    class Program
    {
        static Thread showThread = new Thread(Human.show_table);

        static void Main()
        {
            showThread.Start();
            Human.show_human();
            Human.show_flag = 1;
            while (true)
            {
                Human.step();
                Thread.Sleep(1);
            }
        }
    }
}
