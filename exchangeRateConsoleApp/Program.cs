using System;
using System.Net.Http;
using System.Threading;
using currencyClassLibrary;

namespace exchangeRateConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CurrencyHttpRequestHandler handler = new CurrencyHttpRequestHandler(new HttpClient());
            CurrencyResults result;
            string input;
            DateTime inputDate; 

            while (true)
            {
                WriteInstruction();
                input = Console.ReadLine();

                if (input.ToLower() == "escape") { break; }

                try
                {
                    inputDate = DateTime.Parse(input);
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Nieprawidłowy format daty. Sprubój ponownie.");
                    continue;
                }


                try
                {

                    Console.WriteLine("Pobieranie danych...");
                    result = handler.Handle(inputDate).GetAwaiter().GetResult();
                    Console.Clear();
                    WriteResult(result);
                }
                catch(ApplicationException)
                {
                    Console.Clear();
                    Console.WriteLine("Aplikacja nie działa poprawnie. :(");
                }
                catch (HttpRequestException)
                {
                    Console.Clear();
                    Console.WriteLine(string.Format("Nie udało się pobrać danych z {0:yyyy-MM-dd}.", inputDate));
                }
                catch (OperationCanceledException)
                {
                    Console.Clear();
                    Console.WriteLine("Przerwano pobieranie danych.");
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Nieobsługiwany wyjątek.");
                }

            }
        }



        static void WriteInstruction()
        {
            Console.WriteLine("Aby uzyskać inforamcje dotyczące kursu walut z konkretnego dnia roboczego podaj datę w formacie yyyy-MM-dd np. '2020-08-18'.");
            Console.WriteLine("Aby opuścić applikację wpisz 'escape'.");
        }

        static void WriteResult(CurrencyResults result)
        {
            Console.WriteLine(string.Format("Kursy walut z {0:yyyy-MM-dd}.", result.EffectiveDate));
            string formatter = " | {0,-20} | {1,20} | {2,20} | ";
            Console.WriteLine();
            Console.WriteLine(string.Format(formatter, "Waluta", "Kurs sprzedaży", "Kurs kupna"));
            Console.WriteLine(string.Format(formatter, "", "", ""));
            foreach (var rate in result.Rates)
            {
                Console.WriteLine(string.Format(formatter, rate.Currency, rate.Bid.ToString()+" PLN", rate.Ask.ToString()+" PLN"));
            }
            Console.WriteLine();
        }
    }
}
