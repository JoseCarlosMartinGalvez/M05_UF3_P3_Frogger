using System;
using System.Collections.Generic;

namespace M05_UF3_P3_Frogger
{
    public abstract class Element
    {
        public Vector2Int pos { get; protected set; }
        public char character { get; protected set; }
        public readonly ConsoleColor foreground;

        public Element(Vector2Int pos, char character = ' ', ConsoleColor foreground = ConsoleColor.White)//Elementos
        {
            this.pos = pos;
            this.character = character;
            this.foreground = foreground;
        }

        //Escribe el personaje
        public virtual void Draw()//draw del personaje
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = foreground;
            Console.Write(character);
        }
        //Fondo
        public virtual void Draw(ConsoleColor background)//draw del fondo
        {
            Console.BackgroundColor = background;
            Draw();
        }
        public abstract void Update();
    }

    public class DynamicElement : Element
    {
        //velocidad
        public Vector2Int speed { get; protected set; }//definir la velocidad
        //posicion personaje
        public DynamicElement(Vector2Int speed, Vector2Int pos, char character = ' ', ConsoleColor foreground = ConsoleColor.White) : base(pos, character, foreground)//DynamicElement constructor
        {
            this.speed = speed;
        }

        //actualizar mapa
        public override void Update()
        {
            pos += speed;//actualiza la posicion actual
            if(pos.x >= Utils.MAP_WIDTH)//verifica que no supere a el mapa
            {
                pos.x = 0;
            }
            else if (pos.x < 0)//si posicion x es menor a 0
            {
                pos.x = Utils.MAP_WIDTH - 1;//apareced por la derecha
            }
            if(pos.y >= Utils.MAP_HEIGHT)//se asegura qu entre en el mapa
            {
                pos.y = 0;
            }
            else if (pos.y < 0)
            {
                pos.y = Utils.MAP_HEIGHT - 1;
            }
        }
        public virtual void Update(Vector2Int dir)//actualiza la velocidad del objeto
        {
            speed = dir;
            Update();
        }
    }

    public class Player : DynamicElement
    {
        //definicion  de el personaje
        public const char characterForward = '╧';//personaje arriba
        public const char characterBackwards = '╤';//personaje abajo
        public const char characterLeft = '╢';//personaje izquierda
        public const char characterRight = '╟';//personaje derecha

        public Player() : base(Vector2Int.zero, new Vector2Int(Utils.MAP_WIDTH / 2, Utils.MAP_HEIGHT - 1), characterForward, ConsoleColor.Green)
        {
        }
        //constructor Player


        public Utils.GAME_STATE Update(Vector2Int dir, List<Lane> lanes)//verifican la dirección del jugador (dir) y actualizan el carácter (character) del jugador en función de la dirección.
        {
            speed = dir;

            if(dir.y < 0)
            { character = characterForward; }
            else if (dir.y > 0)
            { character = characterBackwards;}
            else if (dir.x > 0)
            { character = characterRight; }
            else if (dir.x < 0)
            { character = characterLeft; }

            pos += speed;
            if (pos.y <= 0)
            {
                return Utils.GAME_STATE.WIN;
            }
            else if (pos.y >= Utils.MAP_HEIGHT)
            {
                pos.y = Utils.MAP_HEIGHT - 1;
            }
            foreach (Lane lane in lanes)
            {
                if (lane.posY == pos.y)
                {
                    if (lane.speedPlayer) {
                        pos.x += lane.speedElements;//se ve afectado por la velocidad
                    }
                    if (pos.x >= Utils.MAP_WIDTH)//si supera el ancho del mapa
                    {
                        pos.x = 0;//aparece en el otro extremo
                    }
                    else if (pos.x < 0)//igual que antes pero por el otro lado
                    {
                        pos.x = Utils.MAP_WIDTH - 1;
                    }
                    if (lane.ElementAtPosition(pos) == null)//si hay algun elemento dinamico
                    {
                        if (lane.damageBackground)//si el background le hace daño pierde
                        {
                            return Utils.GAME_STATE.LOOSE;
                        }
                        else
                        {
                            return Utils.GAME_STATE.RUNNING;
                        }
                    }
                    else
                    {
                        if (lane.damageElements)//si el elemento le hace daño pierde
                        {
                            return Utils.GAME_STATE.LOOSE;
                        }
                        else
                        {
                            return Utils.GAME_STATE.RUNNING;
                        }
                    }
                }
            }
            return Utils.GAME_STATE.RUNNING;
        }

        public void Draw(List<Lane> lanes)
        {
            foreach (Lane lane in lanes)
            {
                if (lane.posY == pos.y)//determina la fila del jugador
                {
                    Console.BackgroundColor = lane.background;//color fondo
                }
            }
            base.Draw();//dibuja
        }
    }
}
