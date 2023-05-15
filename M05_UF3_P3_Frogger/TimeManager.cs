using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace M05_UF3_P3_Frogger
{
    public static class TimeManager
    {
        public static uint frameCount { get; private set; }//el numero de frames renderizados
        public static double time { get; private set; }// solo lectura que representa el tiempo transcurrido
        public static double deltaTime { get; private set; }//tiempo en segundos que ha pasado desde el último cuadro renderizado
        public static Stopwatch timer { get; private set; } = new Stopwatch();//controlar el tiempo

        public static void NextFrame()
        {
            timer.Stop();//detiene el temporizador
            deltaTime = timer.Elapsed.TotalMilliseconds / 1000.0;//calcula el tiempo transcurridos en segundos desde el ultimo cuadro
            time += deltaTime; //le suma a time el deltatime
            frameCount++;//suma la cuentade frames
            timer.Restart();//reinicia el temporizador
            Thread.Sleep(200);
        }
    }
}
