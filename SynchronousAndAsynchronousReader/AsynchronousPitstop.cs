using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousAndAsynchronousReader
{
    public class AsynchronousPitstop
    {
        public static bool stopEnable = true;

        internal class WykonajCzynnoscAsynchronicznie { }
        public static async Task Stop()
        {
            await Task.Run(async() =>
            {
                while (stopEnable == true)
                {
                    Console.WriteLine("Znak stop aktywny");
                    await Task.Delay(100);
                }
            });
        }
        internal static async Task<WykonajCzynnoscAsynchronicznie> WymianaKolaAsync(string msg, int milisecondDelay, int iloscKol)
        {
            for (int i = 0; i < iloscKol; i++)
            {
                Console.WriteLine("Wykonuje: " + msg + " numer " + (i + 1).ToString());
                await Task.Delay(milisecondDelay);
                Console.WriteLine("Zakńczyłem: " + msg  +" numer " + (i + 1).ToString());
            }
           

            return new WykonajCzynnoscAsynchronicznie();
        }
        internal static async Task<WykonajCzynnoscAsynchronicznie> CzynnoscAsync(string msg, int milisecondDelay)
        {

            Console.WriteLine("Wykonuje: " + msg);
            await Task.Delay(milisecondDelay);
            Console.WriteLine("Zakńczyłem: " + msg);
            
            return new WykonajCzynnoscAsynchronicznie();
        }


    }

}
