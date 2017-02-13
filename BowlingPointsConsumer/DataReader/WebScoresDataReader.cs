using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace BowlingPointsApplication
{
    class WebScoresDataReader : IScoresDataReader
    {
        private string getUrl;
 
        public WebScoresDataReader(string url){
            this.getUrl = url;
        }

        public string GetScores()
        {
            Uri uri = new Uri(getUrl);
            WebRequest webRequest = WebRequest.Create(uri);
            WebResponse response = webRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                string responseData = streamReader.ReadToEnd();
                return responseData;
            }
        }
    }
}
