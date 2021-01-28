using System;
using System.Threading;

namespace UniqueLoginsIssuingService.Services
{
    public static class LoginClient
    {
        //returns a random authorization token or null if login failed
        //with random delay and random success
        public static string Login(string login, string password)
        {
            var rand = new Random();

            //random delay
            Thread.Sleep((int)Math.Round(rand.NextDouble() * 1000));

            //random 50/50 success
            if (rand.NextDouble() < 0.5)
                return Guid.NewGuid().ToString();
            else
                return null;
        }
    }
}
