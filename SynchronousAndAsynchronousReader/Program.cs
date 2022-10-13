using SynchronousAndAsynchronousReader;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static SynchronousAndAsynchronousReader.AsynchronousPitstop;
using static SynchronousAndAsynchronousReader.SynchronousPitstop;

public delegate void DelegataWykonujaca(string msg);

class Program
{
    public delegate WykonajCzynnosc Wykonaj(string msg, int czasWykonywania);
    public delegate WykonajCzynnosc WykonajWymiane(string msg, int czasWykonywania, int ilosc);
    static void  Main()
    {
        //wykonajPitstopSynchronicznie();
        wykonajPitstopAsynchronicznie();

    }
    static void wykonajPitstopSynchronicznie()
    {
        SynchronousPitstop.Stop();
        Wykonaj wykonaj = SynchronousPitstop.Czynnosc;
        WykonajWymiane wykonajWymiane = SynchronousPitstop.WymianaKola;
        wykonajWymiane("wymiana kola", 2000, 4);
        wykonaj("tankowanie", 5000);
        wykonaj("ustawianie skrzydła", 1000);
        wykonaj("czyszczenie kasku", 500);
        SynchronousPitstop.stopEnable = false;
    }
    static async Task wykonajPitstopAsynchronicznie()
    {
        AsynchronousPitstop.Stop();

        var wymianaKolaTask = WymianaKolaAsync("wymiana kola", 2000, 4);
        var tankowanieTask = CzynnoscAsync("tankowanie", 5000);
        var ustawianieSkrzydlaTask = CzynnoscAsync("ustawianie skrzydła", 1000);
        var czyszczenieKaskuTask = CzynnoscAsync("czyszczenie kasku", 500);

        var pitstopTasks = new List<Task> { wymianaKolaTask, tankowanieTask, ustawianieSkrzydlaTask, czyszczenieKaskuTask };
        while (pitstopTasks.Count > 0)
        {
            Task finishedTask = await Task.WhenAny(pitstopTasks);
            pitstopTasks.Remove(finishedTask);
        }
        SynchronousPitstop.stopEnable = false;
    }
}