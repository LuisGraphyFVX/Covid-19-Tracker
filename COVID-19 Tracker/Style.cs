using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace COVID_19_Tracker
{
    class Style
    {
        public static void WriteConsole(string text, string prefix, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(" (");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(prefix);
            Console.ResetColor();
            Console.ForegroundColor = color;
            Console.Write(") " + text);
            Console.ResetColor();
        }

        public static async Task Ascii()
        {
            Console.WriteLine(" ");
            List<string> listAscii = new List<string> 
            {
                " ██████╗ ██████╗ ██╗   ██╗██╗██████╗        ██╗ █████╗ ",
                "██╔════╝██╔═══██╗██║   ██║██║██╔══██╗      ███║██╔══██╗",
                "██║     ██║   ██║██║   ██║██║██║  ██║█████╗╚██║╚██████║",
                "██║     ██║   ██║╚██╗ ██╔╝██║██║  ██║╚════╝ ██║ ╚═══██║",
                "╚██████╗╚██████╔╝ ╚████╔╝ ██║██████╔╝       ██║ █████╔╝",
                " ╚═════╝ ╚═════╝   ╚═══╝  ╚═╝╚═════╝        ╚═╝ ╚════╝ "
            };
            await Task.Run(() => 
            {
                foreach (string text in listAscii)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(text);
                    Console.ResetColor();
                }
            });
            Console.WriteLine(" ");
        }

        static int tableWidth = 150;
        public static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        public static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        public static string Parse(string source, string left, string right)
        {
            string pattern = ((left == "") ? "^" : Regex.Escape(left)) + "(.+?)" + ((right == "") ? "$" : Regex.Escape(right));

            string text = Regex.Match(source, pattern).Groups[1].Value;

            return text;
        }
    }
}
