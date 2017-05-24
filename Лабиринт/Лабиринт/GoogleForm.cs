using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Collections.Specialized;

namespace Лабиринт
{
    static class GoogleForm
    {
        static Hashtable htparams = new Hashtable();
        static HttpClient client = new HttpClient();


        public static void PostToFormTest(string familiya, string imya, string otchestvo, string login, string password, string email)
        {
            WebClient client = new WebClient();
            var keyValue = new NameValueCollection();
            keyValue.Add("entry.1707525642", familiya);
            keyValue.Add("entry.841390488", imya);
            keyValue.Add("entry.360338567", otchestvo);
            keyValue.Add("entry.2142161237", login);
            keyValue.Add("entry.1795745657", password);
            keyValue.Add("entry.513202195", email);
            Uri uri = new Uri("https://docs.google.com/forms/d/e/1FAIpQLSe2Hv9gk5ZWZTh9kIqTXPGbIIeBZKDrPvuoxtodUiAByEVFGA/formResponse");
            byte[] response = client.UploadValues(uri, "POST", keyValue);
            string result = Encoding.UTF8.GetString(response);

        }

        
    }
}
