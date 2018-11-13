using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace RESTExample
{
    [Activity(Label = "RESTExample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        string webservice_url = "http://services.groupkt.com/country/get/iso3code/";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //get the instance of the submit button
            Button submitButton = FindViewById<Button>(Resource.Id.submitButton);

            //setup the click event for the submit button
            submitButton.Click += SubmitButton_Click;
            
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            //get the info from the box that we're searching for
            EditText countryCode = FindViewById<EditText>(Resource.Id.countryCodeBox);

            //this is where we're going to put the result
            TextView result = FindViewById<TextView>(Resource.Id.countryName);

            try
            {
                webservice_url = webservice_url + countryCode.Text;

                var webRequest = WebRequest.Create(webservice_url);

                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.ContentType = "application/json";

                    //Get the response 
                    WebResponse wr = webRequest.GetResponseAsync().Result;
                    Stream receiveStream = wr.GetResponseStream();
                    StreamReader reader = new StreamReader(receiveStream);

                    Country currentCountry = JsonConvert.DeserializeObject<Country>(reader.ReadToEnd());

                    if(currentCountry.RestResponse.result == null)
                    {
                        result.Text = "No country found";
                    }
                    else
                    {
                        result.Text = currentCountry.RestResponse.result.name;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        //classes created using http://json2csharp.com/

        public class Result
        {
            public string name { get; set; }
            public string alpha2_code { get; set; }
            public string alpha3_code { get; set; }
        }

        public class RestResponse
        {
            public List<string> messages { get; set; }
            public Result result { get; set; }
        }

        public class Country
        {
            public RestResponse RestResponse { get; set; }
        }
    }
}

