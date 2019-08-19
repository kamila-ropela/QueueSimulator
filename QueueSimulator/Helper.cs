namespace QueueSimulator
{
    public static class Helper
    {
        public static DbContext dbContext;
        public static int doctorCount;
        public static int additionalEvents;
        public static int algorytm;
        public static int highestPriority;

        public static int ReturnByte(string number)
        {
            var result = 0;
            var splitNumbers = number.ToCharArray();

            if (splitNumbers[0] == '0')
                result += 0;
            else
                result += 4;
            if (splitNumbers[1] == '0')
                result += 0;
            else
                result += 2;
            if (splitNumbers[2] == '0')
                result += 0;
            else
                result += 1;

            return result;
        }
    }
}
