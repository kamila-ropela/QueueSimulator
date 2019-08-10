using System.Collections.Generic;
using System.Linq;

namespace QueueSimulator.EnumAndDictionary
{
    public static class Dictionary
    {
        private static Dictionary<string, int> sexDictionary = new Dictionary<string, int>()
        {
            { "kobieta", 1 },
            { "mężczyzna", 0 }
        };

        private static Dictionary<string, int> airwaysDictionary = new Dictionary<string, int>()
        {
            { "drożne", 1 },
            { "niedrożne", 0 }
        };

        private static Dictionary<string, int> disabilityDictionary = new Dictionary<string, int>()
        {
            { "alarm", 1 },
            { "dezorientacja", 2 },
            { "upośledzenie umysłowe", 3 },
            { "nieprzytomność", 4 }
        };

        private static Dictionary<string, int> algorytmDictionary = new Dictionary<string, int>()
        {
            { "Skala Glasgow", 1 },
            { "Skala FOUR", 2 },
            { "METTS", 3 },
            { "SCON", 4 },
            { "SREN", 5 },
            { "MIXED", 6 }
        };

        public static string GetSexByValue(int value)
        {
            return sexDictionary.First(x => x.Value == value).Key;
        }

        public static string GetAirwayByValue(int value)
        {
            return airwaysDictionary.First(x => x.Value == value).Key;
        }

        public static string GetDisabilityByValue(int value)
        {
            return disabilityDictionary.First(x => x.Value == value).Key;
        }

        public static string GetAlgorytmByValue(int value)
        {
            return algorytmDictionary.First(x => x.Value == value).Key;
        }
    }
}
