using System;
using System.Collections.Generic;
using System.Text;

namespace M05_UF3_P3_Frogger
{
    public class Vector2Int
    {
        public int x;
        public int y;
        public Vector2Int(int x = 0, int y = 0) { this.x = x; this.y = y; }//constructor

        public static Vector2Int zero { get { return new Vector2Int(); } }//estatico
        public static Vector2Int right { get { return new Vector2Int(x: 1); } }//derecho
        public static Vector2Int left { get { return new Vector2Int(x: -1); } }//izquierda
        public static Vector2Int up { get { return new Vector2Int(y: -1); } }//arriba
        public static Vector2Int down { get { return new Vector2Int(y: 1); } }//abajo

        public override bool Equals(object obj)//se usa para comparar 2 objetos vector2Int por si son iguales
        {
            if(obj is Vector2Int)//si el boj es un vector2int
            {
                return this == (Vector2Int)obj;//lo iguala
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new Vector2Int(a.x + b.x, a.y + b.y); //SUMA
        public static bool operator ==(Vector2Int a, Vector2Int b) => a.x == b.x && a.y == b.y;//IGUAL
        public static bool operator !=(Vector2Int a, Vector2Int b) => a.x != b.x || a.y != b.y;//SI SON DIFERENTES
    }
    public static class Utils
    {
        public static Random rnd = new Random();//numeros aleatorios
        public const int MAP_WIDTH = 20;//alto mapa
        public static int MAP_HEIGHT = 13;//ancho mpaa

        public const char charCars = '╫';//definir los caracteres de los coches
        public static readonly ConsoleColor[] colorsCars = { ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.Red };
        //pinta los coches

        public const char charLogs = '=';//definir los caracteres de los troncos
        public static readonly ConsoleColor[] colorsLogs = { ConsoleColor.DarkYellow, ConsoleColor.Yellow};
        //Pinta los troncos
        public enum GAME_STATE { RUNNING, WIN, LOOSE };//define el gamestate

        public static Vector2Int Input()//los controles del juego
        {
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        return Vector2Int.left;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        return Vector2Int.up;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        return Vector2Int.right;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        return Vector2Int.down;
                }
            }
            return Vector2Int.zero;
        }
    }
} 
