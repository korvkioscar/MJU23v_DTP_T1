using System.Globalization;

namespace MJU23v_DTP_T1
{
    public class Language
    {
        public string family, group;
        public string language, area, link;
        public int pop;
        public Language(string line)
        {
            string[] field = line.Split("|");
            family = field[0];
            group = field[1];
            language = field[2];
            pop = (int)double.Parse(field[3], CultureInfo.InvariantCulture);
            area = field[4];
            link = field[5];
        }
        public void Print()
        {
            Console.WriteLine($"Language {language}:");
            Console.Write($"  family: {family}");
            if (group != "")
                Console.Write($">{group}");
            Console.WriteLine($"\n  population: {pop}");
            Console.WriteLine($"  area: {area}");
        }
    }
    public class Program
    {
        static string dir = @"..\..\..";
        static List<Language> eulangs = new List<Language>();
        static void Main(string[] arg)
        {
            using (StreamReader sr = new StreamReader($"{dir}\\lang.txt"))
            {
                Language lang;
                string line = sr.ReadLine();
                while (line != null)
                {
                    // Console.WriteLine(line);
                    lang = new Language(line);
                    eulangs.Add(lang);
                    line = sr.ReadLine();
                }
                // Steg 1 - skapa kommandoloop
                while (true)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine();
                    if (input == null) continue;

                    string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0) continue;

                    string command = parts[0].ToLower();

                    // Steg 2: help och quit
                    if (command == "quit")
                        break;
                    else if (command == "help")
                    {
                        Console.WriteLine("Tillgängliga kommandon:");
                        Console.WriteLine("  help");
                        Console.WriteLine("  quit");
                        Console.WriteLine("  list group <groupname>");
                        Console.WriteLine("  list country <countryname>");
                        Console.WriteLine("  show <language>");
                        Console.WriteLine("  population group <groupname>");
                    }
                    // Steg 3: Grupper 
                    else if (command == "list" && parts.Length >= 3 && parts[1].ToLower() == "group")
                    {
                        string groupname = string.Join(' ', parts.Skip(2));
                        foreach (Language L in eulangs)
                        {
                            if (L.group.Contains(groupname, StringComparison.OrdinalIgnoreCase))
                                Console.WriteLine(L.language);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Okänt kommando. Skriv 'help' för hjälp.");
                    }
                }

            }
        }
    }
}
