using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PreEntregaProjecto1SoftwareDesign {
    public class TraceEvent {
        public static int spanId;
        public string eventName;
        public string eventData;

        static HttpClient client = new HttpClient();

        public static string productionURL = "https://gentle-coast-69723.herokuapp.com/api/apps/9246afda645abbb1c22849aa42a08d82/traces/";
        public static string developmentURL = "https://gentle-coast-69723.herokuapp.com/api/apps/92f2371cc3602e5724bc7f6823555736/traces/";

        public TraceEvent(string eventName, string eventData, bool production) {
            this.eventName = eventName;
            this.eventData = eventData;
            if(eventName== "block_model_loaded") spanId += 1;

            InitializeClient(production);
        }

        public async Task InitializeClient(bool production) {
            // Check if production or development
            if(production) client.BaseAddress = new Uri(productionURL);
            else client.BaseAddress = new Uri(developmentURL);

            // Accept Json type for post
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //Try to send Json
            try {
                var url = await SendJson(ConvertToJson());
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        static async Task<Uri> SendJson(string json) {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/products", json);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public string ConvertToJson() {
            Dictionary<string, Dictionary<string, string>> trace = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> traceData = new Dictionary<string, string>();

            traceData.Add("span_id", spanId.ToString());
            traceData.Add("event_name", eventName);
            traceData.Add("eventData", eventData);
            trace.Add("trace", traceData);

            string jsonA = JsonConvert.SerializeObject(trace);
            return jsonA;
        }
    }
}
