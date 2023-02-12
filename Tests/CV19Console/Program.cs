
using System.Globalization;

namespace CV19Console
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var responce = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await responce.Content.ReadAsStreamAsync();
        }
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;

                yield return line.Replace("Korea,", "Korea -");
            }
        }
        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(5)
            .Select(line => DateTime.Parse(line,CultureInfo.InvariantCulture))
            .ToArray();

        public static IEnumerable<(string Country, string Province, int[] Count)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var line in lines)
            {
                var province = line[0].Trim();
                var country_name = line[1].Trim(' ','"');
                int [] ill_count = line.Skip(5)
                    .Select(line => Int32.Parse(line))
                    .ToArray();

                yield return (country_name, province, ill_count);
            }
          
        }
        static void Main(string[] args)
        {
            #region Рубрика - эксперррррименты
            //var client = new HttpClient();
            //var response = client.GetAsync(data_url).Result;
            //string data_str = response.Content.ReadAsStringAsync().Result;

            //foreach (string line in GetDataLines())
            //    Console.WriteLine(line); 

            //foreach (DateTime date in GetDates())
            //{
            //    Console.WriteLine($"{date.ToShortDateString()}\n"); ;
            //}
            #endregion

            var russiaCovidData = GetData()
                .First(data => data.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n",GetDates().Zip(russiaCovidData.Count, (date, count) => $"{date.ToShortDateString()} {count}")));
           
                
            Console.ReadLine();
        }
    }
}

