using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using Task1_FindingPrimes.Utils;

namespace Task1_FindingPrimes
{
    class Program
    {
        static void Main()
        {
            var result = new Result();
            Stopwatch watch = new Stopwatch();

            try
            {
                //read settings
                var json = File.ReadAllText("settings.json");
                var settings = JsonSerializer.Deserialize<Settings>(json);

                //find primes with counting time
                watch.Start();
                result.Primes = Primes.FindPrimesInRange(settings.PrimesFrom, settings.PrimesTo);
                watch.Stop();


                result.Success = true;
                result.Error = null;
            }
            catch (FileNotFoundException)
            {
                result.Success = false;
                result.Error = "File settings.json was not found.";
            }
            catch (SerializationException)
            {
                result.Success = false;
                result.Error = "Deserialization error.";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.Message;
            }
            finally
            {
                //set duration
                result.Duration = $"{watch.Elapsed.Hours:00}:{watch.Elapsed.Minutes:00}:{watch.Elapsed.Seconds:00}";

                //set options for serialization
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    //PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                //save result to json file
                var json = JsonSerializer.Serialize(result, options);
                File.WriteAllText("result.json", json);
            }

        }
    }
}
