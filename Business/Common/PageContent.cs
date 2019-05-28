using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Common
{
    public static class PageContent
    {
        public static string Read(string path)
        {
            // We will store the html response of the request here
            string retValue = string.Empty;

            // The url you want to grab
            string url = path;

            // Here we're creating our request, we haven't actually sent the request to the site yet...
            // we're simply building our HTTP request to shoot off to google...
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            // Right now... this is what our HTTP Request has been built in to...
            /*
                GET http://google.com/ HTTP/1.1
                Host: google.com
                Accept-Encoding: gzip
                Connection: Keep-Alive
            */


            // Wrap everything that can be disposed in using blocks... 
            // They dispose of objects and prevent them from lying around in memory...
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())  // Go query google
            using (Stream responseStream = response.GetResponseStream())               // Load the response stream
            using (StreamReader streamReader = new StreamReader(responseStream))       // Load the stream reader to read the response
            {
                retValue = streamReader.ReadToEnd(); // Read the entire response and store it in the siteContent variable
            }
            return retValue;
        }
    }
}
