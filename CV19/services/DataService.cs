using CV19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace CV19.Services
{
    internal class DataService
    {
        private const string DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        private static async Task<Stream> GetDataStream()
        {
             var client = new HttpClient();
             HttpResponseMessage responce = await client.GetAsync(
                DataSourceAddress,
                HttpCompletionOption.ResponseHeadersRead);

            return await responce.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(false);
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

                if (line.Contains(" "))
                    line = line.Insert(line.IndexOf(',', line.IndexOf('"')) + 1, "-")
                        .Remove(line.IndexOf(',', line.IndexOf('"')), 1);
                
                yield return line;
                   
            }
            yield break;
        }
        private static DateTime[] GetDates() => GetDataLines()
        .First()
        .Split(',')
        .Skip(4)
        .Select(column => DateTime.Parse(column, CultureInfo.InvariantCulture))
        .ToArray();
        public static IEnumerable<(string province, string country, (double lat, double lon) place, int[] illCount)>GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (string [] row in lines)
            {
                var _province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var latitude = double.Parse(row[2]);
                var longitude = double.Parse(row[3]);
                int[] _illCount = row.Skip(5)
                    .Select(line => Int32.Parse(line))
                    .ToArray();

                yield return (_province, country_name, (latitude, longitude), _illCount);
            }
        }
        public IEnumerable<CountryInfo> GetData()
        {
            DateTime[] dates = GetDates();

            var data = GetCountriesData()
                .GroupBy(d => d.country);
            foreach (var dataGroup in data)
            {
                var country = new CountryInfo()
                {
                    Name = dataGroup.Key,
                    ProvinceCounts = dataGroup.Select(c => new PlaceInfo()
                    {
                        Name = c.province,
                        Location = new Point(c.place.lat, c.place.lon),
                        Confirmed = dates.Zip(c.illCount,
                        (date,illCount) => new ConfirmedCount 
                        { 
                            Count = illCount,
                            Date = date
                        })
                    }),
                };
                yield return country;
            }
            yield break;
        }
    }
}
