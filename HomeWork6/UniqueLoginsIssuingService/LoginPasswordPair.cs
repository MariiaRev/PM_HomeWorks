using System;
using System.Collections.Generic;
using System.Text;

namespace UniqueLoginsIssuingService
{
    public class LoginPasswordPair
    {
        string _login;
        string _password;

        public LoginPasswordPair(string login, string password)
        {
            _login = login;
            _password = password;
        }

        public string GetLogin()
        {
            return _login;
        }

        public string GetPassword()
        {
            return _password;
        }
    }
}
