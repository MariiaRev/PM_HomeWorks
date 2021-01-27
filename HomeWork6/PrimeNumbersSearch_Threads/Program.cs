using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Library;
using System.Threading;

namespace PrimeNumbersSearch_Threads
{
    class Program
    {
        static CountdownEvent cde;
        static void Main(string[] args)
        {
            var result = new Result();
            var watch = new Stopwatch();

            try
            {
                //read settings
                var json = File.ReadAllText("settings.json");
                var settings = JsonConvert.DeserializeObject<List<Settings>>(json);
                settings = settings.Where(obj => obj != null).ToList();                 //delete null objects from settings


                //find primes in threads with counting time
                watch.Start();
                SearchPrimesByThreads(settings);
                watch.Stop();

                result.Primes = ThreadSafeSortedSet.GetData().ToList();
                result.Success = true;
                result.Error = null;
            }
            catch (FileNotFoundException)
            {
                result.Success = false;
                result.Error = "File settings.json was not found.";
            }
            catch (JsonSerializationException)
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
                result.Duration = watch.Elapsed.ToString();

                //save result to json file
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText("result.json", json);
            }
        }

        static void SearchPrimesByThreads(List<Settings> settings)
        {
            cde = new CountdownEvent(settings.Count);

            foreach (var s in settings)
            {
                var numbers = Primes.GenerateListForPrimes(s.PrimesFrom, s.PrimesTo);
                ThreadPool.QueueUserWorkItem(SearchPrimes, numbers);
                //new Thread(() => SearchPrimes(numbers)).Start();
            }

            cde.Wait();
        }

        static void SearchPrimes(object numbers)
        {
            try
            {
                var primes = Primes.SearchByLinq((List<int>)numbers);
                ThreadSafeSortedSet.Add(primes);
                cde.Signal();
            }
            catch { }
        }
    }
}
