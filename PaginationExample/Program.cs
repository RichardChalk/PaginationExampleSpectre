using Spectre.Console;

namespace PaginationExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Exempeldata
            var data = new List<string>();
            for (int i = 1; i <= 100; i++)
            {
                data.Add($"Rad {i}");
            }

            int pageSize = 10; // Antal rader per sida
            int currentPage = 0;
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);

            while (true)
            {
                Console.Clear();

                // Rendera tabellen för aktuell sida
                RenderTable(data, currentPage, pageSize);

                // Visa sidinformation
                AnsiConsole.MarkupLine($"\nSida [yellow]{currentPage + 1}[/] av [green]{totalPages}[/]");
                AnsiConsole.MarkupLine("[blue]◄[/] Föregående sida   [blue]►[/] Nästa sida");
                AnsiConsole.MarkupLine("[red]Esc[/]: Avsluta");

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (currentPage < totalPages - 1) currentPage++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (currentPage > 0) currentPage--;
                        break;
                    case ConsoleKey.Escape:
                        return; // Avsluta programmet
                }
            }

            static void RenderTable(List<string> data, int currentPage, int pageSize)
            {
                var table = new Table();
                table.AddColumn("[Green]Index[/]");
                table.AddColumn("[Green]Innehåll[/]");

                int start = currentPage * pageSize;
                int end = Math.Min(start + pageSize, data.Count);

                for (int i = start; i < end; i++)
                {
                    table.AddRow((i + 1).ToString(), data[i]);
                }

                AnsiConsole.Write(table);
            }
        }
    }
}
