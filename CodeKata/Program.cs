using CodeKata.Case1;
using CodeKata.Case2;

string? input = string.Empty;
Console.WriteLine("Case1 için --> 1  Case2 için 2 ye basınız ");

do
{

    input = Console.ReadLine();

    switch (input)
    {
        case "1":
            
            var case1 = new Case1();
            case1.Execute();

            Console.WriteLine("\n"+"Case 2 için 2 ye  Çıkış yapmak için e ye basınız");
            break;

        case "2":

            var case2 = new Case2();
            case2.Execute();
            Console.WriteLine("\n"+"Case 1 için 1 e  Çıkış yapmak için e ye basınız");

            break;

        default:
            break;
    }

} while (!(input.ToLower().Equals("e")));




