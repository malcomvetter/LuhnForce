using System;
using System.Linq;

namespace Luhn
{
    public class LuhnForce
    {
        private static int _middle = -1;
        private static int _counter;
        private static int _places;

        public static void Main(string[] args)
        {
            if (!args.Any())
            {
                PrintUsage();
                return;
            }

            var cc = args[0].Replace("-", "").Replace(" ", "").Replace("x", "X");

            if (cc.Length != 16)
            {
                Console.WriteLine("input is not correct length.");
                PrintUsage();
                return;
            }

            _places = cc.Length - cc.Replace("X", "").Length;
            var limit = Math.Pow(10, _places);

            Console.WriteLine("Places: {0}", _places);
            Console.WriteLine("Limit: {0}", limit);

            while (_middle < limit)
            {
                var s = FindNext(cc);
                if (!PassesLuhnCheck(s)) continue;
                Console.WriteLine("Valid: {0}", s);
                _counter++;
            }

            Console.WriteLine("\r\nFound {0} potential matches for {1}", _counter, args[0]);
        }

        private static void PrintUsage()
        {
            Console.Write("Usage: luhn.exe [credit card number]\r\n"
            + " in format like 1234-56xx-xxxx-1234\r\n"
            + " or like 1234-5678-xxxx-1234, etc.\r\n\r\n");
        }

        private static string FindNext(string number)
        {
            _middle++;
            var middle = _middle.ToString();
            while (middle.Length < _places)
            {
                middle = "0" + middle;
            }
            return (number.Replace(GetPlaceHolder(), middle));
        }

        private static string GetPlaceHolder()
        {
            var s = "";
            for (var i = 0; i < _places; i++)
            {
                s += "X";
            }
            return s;
        }

        private static bool PassesLuhnCheck(string number)
        {
            var deltas = new[] { 0, 1, 2, 3, 4, -4, -3, -2, -1, 0 };
            var checksum = 0;
            var chars = number.ToCharArray();
            for (var i = chars.Length - 1; i > -1; i--)
            {
                var j = chars[i] - 48;
                checksum += j;
                if (((i - chars.Length) % 2) == 0)
                    checksum += deltas[j];
            }

            return ((checksum % 10) == 0);
        }
    }
}