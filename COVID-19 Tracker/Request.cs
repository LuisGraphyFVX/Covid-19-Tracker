using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace COVID_19_Tracker
{
    class Request
    {
        public static async Task AllCountrys(int TimeRefresh)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            Console.Clear();

            await Task.Run(async () => 
            {
                for (; ; )
                {
                    try
                    {
                        var httpclient = new HttpClient();

                        var Req = await httpclient.GetAsync("https://coronavirus-tracker-api.herokuapp.com/v2/locations");

                        var result = await Req.Content.ReadAsStringAsync();

                        var regix = Regex.Match(result, "\"country_code\":\"(.*?)\"");

                        var latitud = Regex.Match(result, "\"latitude\":\"(.*?)\",\"longitude\":\".*?}");

                        var longitude = Regex.Match(result, "\"latitude\":\".*?\",\"longitude\":\"(.*?)\"}");

                        var regix2 = Regex.Match(result, "\"confirmed\":(.*?),\"");

                        var regix3 = Regex.Match(result, "\"confirmed\":.*?,\"deaths\":(.*?),");

                        var regix4 = Regex.Match(result, "\"confirmed\":.*?,\"deaths\":.*?,\"recovered\":(.*?)}");

                        Console.ForegroundColor = ConsoleColor.Green;

                        Style.PrintLine();

                        Style.PrintRow("Country Code", "Coordinates", "Confirmated/Deaths/Recovered");

                        Style.PrintLine();

                        Console.ResetColor();

                        for (; ; )
                        {
                            if (regix.Groups[1].Value == "")
                            {
                                break;
                            }

                            if (regix.Groups[1].Value == "" && regix2.Groups[1].Value == "" && regix3.Groups[1].Value == "" && regix4.Groups[1].Value == "" && latitud.Groups[1].Value == "" && longitude.Groups[1].Value == "")
                            {
                                break;
                            }

                            Console.ForegroundColor = ConsoleColor.Green;

                            Style.PrintRow(regix.Groups[1].Value, latitud.Groups[1].Value + "," + longitude.Groups[1].Value, "C: " + regix2.Groups[1].Value + " - D: " + regix3.Groups[1].Value + " - R: " + regix4.Groups[1].Value);
                            //Console.Write("| " + regix.Groups[1].Value + " | Coordinates: " + latitud.Groups[1].Value + ", " + longitude.Groups[1].Value + " | Confirmed: " + regix2.Groups[1].Value + " | Deaths: " + regix3.Groups[1].Value + " | Recovered: " + regix4.Groups[1].Value + " |\n");

                            Console.ResetColor();

                            regix = regix.NextMatch();

                            latitud = latitud.NextMatch();

                            longitude = longitude.NextMatch();

                            regix2 = regix2.NextMatch();

                            regix3 = regix3.NextMatch();

                            regix4 = regix4.NextMatch();

                            continue;
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Style.PrintLine();
                        Console.ResetColor();

                        await Task.Delay(TimeSpan.FromSeconds(TimeRefresh));

                        Console.Clear();

                        continue;
                    }
                    catch(Exception ex)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(TimeRefresh));
                        continue;
                    }
                }
            });
        }
        public static async Task SearchCountry(int TimeRefresh, string CountryCode)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            Console.Clear();

            Console.Title = "COVID-19 Tracker · Coder: LEV_ · Search Country: " + CountryCode;

            await Task.Run(async () =>
            {
                for (; ; )
                {
                    try
                    {
                        var httpclient = new HttpClient();

                        var Req = await httpclient.GetAsync("https://coronavirus-tracker-api.herokuapp.com/v2/locations");

                        var result = await Req.Content.ReadAsStringAsync();

                        var country = Regex.Match(result, "\"id\":(\\d+),\"country\":\"[a-zA-Z]+\",\"country_code\":\"" + CountryCode.ToUpper() + "\",");

                        Console.ForegroundColor = ConsoleColor.Green;

                        Style.PrintLine();

                        Style.PrintRow("Country Code", "Coordinates", "Confirmated/Deaths/Recovered");

                        Style.PrintLine();

                        Console.ResetColor();

                        for (; ; )
                        {
                            if (country.Groups[1].Value == "")
                            {
                                break;
                            }

                            var Req2 = await httpclient.GetAsync("https://coronavirus-tracker-api.herokuapp.com/v2/locations/" + country.Groups[1].Value);

                            var result2 = await Req2.Content.ReadAsStringAsync();

                            var latitud = Regex.Match(result2, "\"latitude\":\"(.*?)\",\"longitude\":\".*?}");

                            var longitude = Regex.Match(result2, "\"latitude\":\".*?\",\"longitude\":\"(.*?)\"}");

                            var regix2 = Regex.Match(result2, "\"confirmed\":(.*?),\"");

                            var regix3 = Regex.Match(result2, "\"confirmed\":.*?,\"deaths\":(.*?),");

                            var regix4 = Regex.Match(result2, "\"confirmed\":.*?,\"deaths\":.*?,\"recovered\":(.*?)}");

                            Console.ForegroundColor = ConsoleColor.Green;


                            Style.PrintRow(CountryCode, latitud.Groups[1].Value + "," + longitude.Groups[1].Value, "C: " + regix2.Groups[1].Value + " - D: " + regix3.Groups[1].Value + " - R: " + regix4.Groups[1].Value);
                            //Console.Write("| " + regix.Groups[1].Value + " | Coordinates: " + latitud.Groups[1].Value + ", " + longitude.Groups[1].Value + " | Confirmed: " + regix2.Groups[1].Value + " | Deaths: " + regix3.Groups[1].Value + " | Recovered: " + regix4.Groups[1].Value + " |\n");

                            Style.PrintLine();
                            Console.ResetColor();

                            country = country.NextMatch();
                            continue;
                        }

                        await Task.Delay(TimeSpan.FromSeconds(TimeRefresh));

                        Console.Clear();

                        continue;
                    }
                    catch (Exception ex)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(TimeRefresh));
                        continue;
                    }
                }
            });
        }
    }
}
