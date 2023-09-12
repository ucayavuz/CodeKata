using System.Collections.Generic;
using System.Text;

namespace CodeKata.Case1
{
    public class Case1
    {

        public void Execute()
        {
            var dt1 = DateTime.Now;

            // Verilen küme

            // Algoritma verilen kümeyi kulanarak ve DateTime (Yıl ,Ay,Gün,Saat,Dakika,Saniye) paternini kullanarak unique üretiyor.

            // Yıl --> 1 bit  Ay --> 1 bit Gün --> 1 bit Saat --> 1 bit Dakika --> 2 bit Saniye --> 2 bit  = 8 bitlik Code

            char[] arr = { 'A', 'C', 'D', 'E', 'F', 'G', 'H', 'K', 'L', 'M', 'N', 'P', 'R', 'T', 'X', 'Y', 'Z', '2', '3', '4', '5', '7', '9' };

            var dateTimeNow = DateTime.Now;
            List<string> list = new List<string>();
            for (int j = 0; j < 23; j++)
            {
                for (int i = 0; i < 70000; i++)
                {
                    dateTimeNow = dateTimeNow.AddMinutes(1).AddSeconds(1);
                    string res = GenerateCode(dateTimeNow, arr);

                    //Console.WriteLine($"Üretilen Code: {res} Uzunluk: {res.Length}");
                    list.Add(res);
                }
                dateTimeNow = dateTimeNow.AddYears(1);
            }

            // Sonuçlar

            GetResults(list);

            Console.WriteLine($"Program {(DateTime.Now - dt1).TotalSeconds} saniye kadar sürdü");
        }
        private string CheckCodeForMin(string a, char[] arr)
        {
            // index lerinin herhagi bir önemi yok rastgele seçilmiştir çeşitliliği sağlamak amaçlıdır
            var sb = new StringBuilder(a);
            sb.Replace('1', arr[1]);
            sb.Replace('6', arr[3]);
            sb.Replace('8', arr[5]);
            sb.Replace('0', arr[7]);


            return sb.ToString();
        }
        private string CheckCodeForSecond(string a, char[] arr)
        {
            // index lerinin herhagi bir önemi yok rastgele seçilmiştir çeşitliliği sağlamak amaçlıdır

            var sb = new StringBuilder(a);
            sb.Replace('1', arr[2]);
            sb.Replace('6', arr[4]);
            sb.Replace('8', arr[6]);
            sb.Replace('0', arr[8]);

            return sb.ToString();
        }
        private string GenerateCode(DateTime dt, char[] arr)
        {
            string result = string.Empty;

            // Yıl -> mod 23 e göre diziden eleman seçilecek  1 

            result += arr[dt.Year % 23].ToString();
            // Ay ->  1-6-8 ve 2 haneli aylar bir algoritmaya göre seçilecek  1 

            result += dt.Day > 23 ? arr[22 - (31 - dt.Day)] : arr[dt.Month];

            // Gün -> 0 1 6 8 i harfle ifade edilecek.  1 

            result += arr[dt.Day % 23].ToString(); // normalde mode 23 e göre hareket edilir eğer gün 23 ten büyükse ay değerinin modu 23-ay olarak alınacak gün yine mod 23 alınacak

            // Saat --> Mod 23  1 

            result += arr[dt.Hour % 23].ToString();

            // Dakika -->0 1 6 8 

            result += CheckCodeForMin(dt.Minute.ToString("D2"), arr);

            // Saniye --> 0 1 6 8 duruma göre harflerle ifade edilecek 2 

            result += CheckCodeForSecond(dt.Second.ToString("D2"), arr);

            //Console.WriteLine($"Üretilen Unique: {result}  - {result.Length}");

            return result;
        }

        private void GetResults(List<string> list)
        {
            Console.WriteLine("\n" + "**Program Çıktı Sonucu**" + "\n");
            Console.WriteLine($"Üretilen Unique Adedi: {list.Count}");
            Console.WriteLine($"Benzersiz olan Unique Adedi: {list.Distinct().Count()}");
        }
    }
}