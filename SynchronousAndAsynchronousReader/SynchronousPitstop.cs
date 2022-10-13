using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousAndAsynchronousReader
{
    public static class SynchronousPitstop
    {
        public static bool stopEnable=true;

        internal class WykonajCzynnosc { }
        public static async Task Stop()
        {
            await Task.Run(() =>
            {
                while (stopEnable == true)
                {
                    Console.WriteLine("Znak stop aktywny");
                    Task.Delay(100).Wait();
                }
            });
        }
        internal static WykonajCzynnosc WymianaKola(string msg, int milisecondDelay, int iloscKol)
        {
            for(int i = 0; i < iloscKol; i++)
            {
                Console.WriteLine("Wykonuje: " + msg +" numer "+(i+1).ToString());
                Task.Delay(milisecondDelay).Wait();
                Console.WriteLine("Zakńczyłem: " + msg);
            }
            return new WykonajCzynnosc();
        }
        internal static WykonajCzynnosc Czynnosc(string msg, int milisecondDelay)
        {
            Console.WriteLine("Wykonuje: " + msg);
            Task.Delay(milisecondDelay).Wait();
            Console.WriteLine("Zakńczyłem: "+msg );
            return new WykonajCzynnosc();
        }
       
    }
    

}
