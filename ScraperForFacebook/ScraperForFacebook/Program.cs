using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;


namespace ScraperForFacebook
{
    class Program
    {
        static void Main(string[] args)
        {
            string accessToken = "EAACEdEose0cBAC8VPSnmfihgPmNL9AlDN5armKZCsrnihSWjteZBk8XSgz2Sm73eSqqIDsAuGGo7glNfF4576E5mTZAmKVKOboLUZCYVLWdbM2Pr6ry5o2HWFsfle5ZB85LjQlTEVRfvWGxAZCWqOzUCZCLzwV38yNiSfabkFSZBCIfFnii2BCNQxAjPui3wAZCkZD";
            var faceBookGraph = new FaceBookGraph(accessToken);

            Console.WriteLine("Hello World! " + faceBookGraph.GetObject("dd").ToString());
            Console.ReadLine();
        }


    }

    internal class FaceBookGraph
    {
        internal HttpClient httpClient { private set; get; }

        internal string AccessToken { get; private set; }

        internal FaceBookGraph(string accessToken)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://graph.facebook.com");
            this.httpClient = client;
            this.AccessToken = accessToken;
        }

        void PostMessage(string recipient, string message)
        {
            string jSonmessage = "{recipient:{id:{recipient}},message{:{text:{message}}";
            var array = System.Text.Encoding.UTF8.GetBytes(jSonmessage);
            HttpContent httpContent = new ByteArrayContent(array);
            this.httpClient.PostAsync("v2.6/me/messages?access_token ={this.AccessToken}",httpContent);
        }


        internal object GetObject(string dd)
        {
            HttpResponseMessage response = this.httpClient.GetAsync($"me?fields=name,email&access_token={this.AccessToken}").Result;

            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;

            var jsonRes = JsonConvert.DeserializeObject<dynamic>(result);

            var email = jsonRes["email"].ToString();

            return email;
        }
    }
}
