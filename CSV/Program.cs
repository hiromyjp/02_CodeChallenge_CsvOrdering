using System;

namespace CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            var csv = "Beth,Charles,Danielle,Adam,Eric\n17945,10091,10088,3907,10132\n2,12,13,48,11";
            Console.WriteLine(Challenge.SortCsvColumns(csv));
            Console.ReadKey();
        }
    }
}
