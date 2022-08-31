using CryTogether.Models;
using CryTogether.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryTogether.Services
{
    public class RestService : IRESTService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        public List<Breakdown> Items { get; private set; }

        public RestService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Breakdown>> RefreshDataAsync()
        {
             Items = new List<Breakdown>();

            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<Breakdown>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task SaveDataAsync(Breakdown breakdown, bool isNew)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            breakdown.suferer = LoginViewModel.Instance.getUserName();
            try
            {
                string json = JsonSerializer.Serialize<Breakdown>(breakdown, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNew)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }



        public async Task DelteDataAsync(string id)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, id));

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<Breakdown> GetItemAsync(string id)
        {
            var item = new Breakdown();

            Uri uri = new Uri(string.Format(Constants.RestUrl, id));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    item = JsonSerializer.Deserialize<Breakdown>(content, serializerOptions);
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return item;
        }
    }
}

