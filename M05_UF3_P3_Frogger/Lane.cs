using System;
using System.Collections.Generic;
using System.Text;

namespace M05_UF3_P3_Frogger
{
    public class Lane
    {
        //Definicion en public pero de solo lectura
        public readonly int posY;//posicion eje y
        public readonly int speedElements;//Velocidad elementos
        public readonly bool speedPlayer;//indica si el jugador tiene una velocidad específica habilitada. Puede usarse para permitir que el jugador se mueva más rápido o más lento según una condición determinada.
        public readonly ConsoleColor background;//color de fondo
        public readonly bool damageElements;//si los elementos hacen daño
        public readonly bool damageBackground;//si el fondo hace daño 
        public List<DynamicElement> elements { get; protected set; } = new List<DynamicElement>();//En resumen, esta propiedad elements proporciona acceso a una lista de objetos del tipo DynamicElement y permite agregar, eliminar y acceder a esos elementos desde la clase actual o sus clases derivadas.


        public Lane(int posY, bool speedPlayer, ConsoleColor background, bool damageElements, bool damageBackground, float elementsPercent, char elementsChar, List<ConsoleColor> colorsElements)
        {
            this.posY = posY;
            this.speedElements = Utils.rnd.Next(-10, 10) < 0 ? 1 : -1;
            this.speedPlayer = speedPlayer;
            this.background = background;
            this.damageElements = damageElements;
            this.damageBackground = damageBackground;

            for (int i = 0; i < Utils.MAP_WIDTH; i++)//mientras el ancho el mapa sea menor que i
            {
                if(Utils.rnd.NextDouble() < elementsPercent)
                {
                    this.elements.Add(
                        new DynamicElement(
                            new Vector2Int(speedElements, 0), 
                            new Vector2Int(i, posY),
                            elementsChar, 
                            colorsElements[Utils.rnd.Next(colorsElements.Count)]
                            ));
                }
            }
            this.elements.TrimExcess();
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, posY);//posciciona el cursor en el 0, posy
            Console.BackgroundColor = background;//color de fondo
            for (int i = 0; i < Utils.MAP_WIDTH; i++)//ancho del mapa
            {
                DynamicElement element = ElementAtPosition(new Vector2Int(i, posY));//si hay un elemento lo posiciona
                if (element == null)
                {
                    Console.Write(' ');
                }
                else
                {
                    Console.ForegroundColor = element.foreground;
                    Console.Write(element.character);
                }
            }
        }
        public void Update()
        {
            foreach (DynamicElement element in elements)//actualiza el elemento 
            {
                element.Update();
            }
        }

        public DynamicElement ElementAtPosition(Vector2Int position)//busca un elemento de la clase Lane en una posición específica y devolverlo si se encuentra
        {
            foreach (DynamicElement element in elements)
            {
                if(element.pos == position)
                    return element;
            }
            return null;
        }
    }
}
