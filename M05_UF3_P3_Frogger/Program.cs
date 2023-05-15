using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.PortableExecutable;

namespace M05_UF3_P3_Frogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //preparar datos
            List<ConsoleColor> colorsElements = new List<ConsoleColor>
            {
            ConsoleColor.Red,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta
            };
            

            Player player = new Player();
            List<Lane> lanes = new List<Lane>()
            {
                new Lane(0, false, ConsoleColor.DarkGreen,damageElements: false,damageBackground: false, 0f , ' ' ,colorsElements),
                new Lane(1, true, ConsoleColor.Blue,damageElements: false,damageBackground:true, 0.5f, Utils.charLogs, colorsElements),
                new Lane(2, true, ConsoleColor.Blue,damageElements:false,damageBackground: true, 0.5f, Utils.charLogs, colorsElements),
                new Lane(3, true, ConsoleColor.Blue,damageElements: false,damageBackground: true, 0.5f, Utils.charLogs, colorsElements),
                new Lane(4, true, ConsoleColor.Blue, damageElements:false,damageBackground:true, 0.5f, Utils.charLogs, colorsElements),
                new Lane(5, true, ConsoleColor.Blue, damageElements:false,damageBackground: true, 0.7f, Utils.charLogs, colorsElements),
                new Lane(6, false, ConsoleColor.DarkGreen,damageElements: false,damageBackground: false, 0.0f, ' ', colorsElements),
                new Lane(7, false, ConsoleColor.Black,damageElements: false,damageBackground: false, 0f,' ', colorsElements),
                new Lane(8, false, ConsoleColor.Black,damageElements: true,damageBackground: false, 0.2f,Utils.charCars, colorsElements),
                new Lane(9, false, ConsoleColor.Black,damageElements: false,damageBackground: false, 0f, ' ', colorsElements),
                new Lane(10,false, ConsoleColor.Black,damageElements: true,damageBackground: false, 0.1f,  Utils.charCars, colorsElements),
                new Lane(11,false, ConsoleColor.Black,damageElements: false,damageBackground: false, 0f, ' ', colorsElements),
                new Lane(12, false, ConsoleColor.DarkGreen,damageElements: false,damageBackground: false, 0f, ' ', colorsElements),
                new Lane(13, false, ConsoleColor.DarkGreen,damageElements: false,damageBackground: false, 0f, ' ', colorsElements)
            };


            
            Utils.GAME_STATE gameState = Utils.GAME_STATE.RUNNING;
            //crear personaje
            while (gameState == Utils.GAME_STATE.RUNNING)
            {
                //Inputs
                Vector2Int input = Utils.Input();
                //Logica

                foreach (Lane lane in lanes)
                {
                    lane.Update();

                    // Pierdes si haces contacto con un elemento
                    foreach (DynamicElement element in lane.elements)
                    {
                        if (lane.posY >= 6 && lane.posY <= 11)
                        {
                            if (element.pos.y == player.pos.y && element.pos.x == player.pos.x)
                            {
                                // Colisión detectada, detener el juego
                                gameState = Utils.GAME_STATE.LOOSE;
                                break;
                            }
                        }
                    }

                 


                    foreach (DynamicElement element in lane.elements)
                    {
                        if (player.pos.y <=0)
                        {
                            gameState = Utils.GAME_STATE.WIN;
                            break;
                        }
                    }

                    // Si el estado de juego ya es LOSE, sal del bucle principal
                    if (gameState == Utils.GAME_STATE.LOOSE)
                    {
                        break;
                    }

                }


                player.Update(input,lanes);

                //Dibujado
                Console.Clear();
                foreach (Lane lane in lanes)
                {
                    lane.Draw();
                }
                player.Draw(lanes);

                
                TimeManager.NextFrame();

                Console.SetCursorPosition(0, Utils.MAP_HEIGHT );
                if (gameState == Utils.GAME_STATE.WIN)
                {
                    Console.WriteLine("You Win! Press any key to exit...");
                }
                else if (gameState == Utils.GAME_STATE.LOOSE)
                {
                    Console.WriteLine("Game Over! Press any key to exit...");
                }

            }

        }

    }

}
  