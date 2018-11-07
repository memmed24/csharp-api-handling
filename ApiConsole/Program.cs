using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ApiConsole
{
    class Program
    {
        static public string url = "http://localhost:3000/api/artworks";
        static void Main(string[] args)
        {
            // requesting web service
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // assigning web service response to string value
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            
            // allowing null values
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            // converting string data to JSON
            var result = JsonConvert.DeserializeObject<RootObject>(content, settings);
            
            // simply writing to console the values that's been deserialized to JSON
            foreach(var res in result.Data)
            {
                Console.WriteLine("Name is {0}, Price is {1} (azn) / {2} (rubl)", res.name, res.price_azn, res.price_rubl);
            }
            Console.Read();
        }

        public class Artwork
        {

            public string name;
            public string photo;
            public int height;
            public int width;
            public int price_azn;
            public int price_rubl;

        }

        public class RootObject 
        {
            public List<Artwork> Data { get; set; } 
        }
    }
}
