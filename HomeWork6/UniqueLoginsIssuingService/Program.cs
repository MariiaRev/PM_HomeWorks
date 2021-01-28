using System;
using System.IO;
using System.Threading;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Library;
using UniqueLoginsIssuingService.Services;

namespace UniqueLoginsIssuingService
{
    class Program
    {
        static int errorsCount = 0;
        static int successCount = 0;
        static CountdownEvent cde;
        static void Main(string[] args)
        {
            //print greetings
            Console.WriteLine($"{"",22}This is Homework 6 Task 3\n{"",20}Unique Logins Issuing Service");
            Console.WriteLine($"\n{"",19}### Made by Mariia Revenko ###\n\n");

            //accept threads number from user
            var threadsNumber = UserInput.ReadPositiveInt("\nEnter threads number, please:", false);
            
            //read logins from file
            var dataStorage = ReadLogins("logins.csv");

            if (dataStorage == null)
                return;

            //login in {threadsNumber} threads
            RunInThreads(threadsNumber, dataStorage);

            //save results to the file
            var result = new Result(successCount, errorsCount);
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            File.WriteAllText("result.json", json);

            Console.WriteLine("\n\n\nThe result was saved.\n\n\n");
        }

        static ConcurrentQueue<LoginPasswordPair> ReadLogins(string path)
        {
            var dataStorage = new ConcurrentQueue<LoginPasswordPair>();

            try
            {
                var data = File.ReadAllText(path);
                var pairs = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                foreach (var p in pairs)
                {
                    var pair = p.Split(';');
                    dataStorage.Enqueue(new LoginPasswordPair(pair[0], pair[1]));
                }

                return dataStorage;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"\n\n\nFile {path} was not found.\n\n\n");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n\n\nThe error occurred.\n{e.Message}\n\n\n");
                return null;
            }
        }

        static void RunInThreads(int threadsNumber, ConcurrentQueue<LoginPasswordPair> dataStorage)
        {
            cde = new CountdownEvent(threadsNumber);

            for (int i = 1; i <= threadsNumber; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadWork, dataStorage);
            }

            cde.Wait();
        }

        static void ThreadWork(object dataStorage)
        {
            try
            {
                while (((ConcurrentQueue<LoginPasswordPair>)dataStorage).TryDequeue(out var pair))
                {
                    var loginToken = LoginClient.Login(pair.GetLogin(), pair.GetPassword());

                    if (loginToken == null)
                        Interlocked.Increment(ref errorsCount);
                    else
                        Interlocked.Increment(ref successCount);
                }

                cde.Signal();
            }
            catch { }
        }
    }
}
