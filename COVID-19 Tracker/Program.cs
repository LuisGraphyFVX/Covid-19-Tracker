using System;
using System.Threading.Tasks;

namespace COVID_19_Tracker
{
    class Program : Style
    {
        public static async Task Main()
        {
            Console.Title = "COVID-19 Tracker";

            await Ascii();

            WriteConsole("All Countrys\n", "1", ConsoleColor.Green);

            WriteConsole("Search Country\n\n", "2", ConsoleColor.Green);

            WriteConsole("choose the option you want: ", "!", ConsoleColor.Green);

            string Key = Console.ReadLine();

            switch (Key)
            {
                case "1":
                    Console.Clear();
                    Console.Title = "COVID-19 Tracker · All Countrys";
                    await Ascii();

                    WriteConsole("The update time is in seconds not milliseconds · Example: 1 or 2 (seconds)\n\n", "Tip", ConsoleColor.Green);

                    WriteConsole("At what time do you want the statistics to be updated: ", "!", ConsoleColor.Green);

                    int Timeout = Convert.ToInt32(Console.ReadLine());

                    await Request.AllCountrys(Timeout);

                    break;
                case "2":
                    Console.Clear();
                    Console.Title = "COVID-19 Tracker · Search Country";
                    await Ascii();

                    WriteConsole("The update time is in seconds not milliseconds · Example: 1 or 2 (seconds)\n\n", "Tip", ConsoleColor.Green);

                    WriteConsole("At what time do you want the statistics to be updated: ", "!", ConsoleColor.Green);

                    int Timeout2 = Convert.ToInt32(Console.ReadLine());

                    WriteConsole("write your country code: ", "!", ConsoleColor.Green);

                    string Country_Code = Console.ReadLine();

                    await Request.SearchCountry(Timeout2, Country_Code);
                    break;
            }

            await Task.Delay(-1);
        }
    }
}
