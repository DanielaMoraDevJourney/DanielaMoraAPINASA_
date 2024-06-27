using DanielaMoraAPINASA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace DanielaMoraAPINASA.Services
{
    public class DMApodService
    {
        public async Task<DMApod> GetImage(DateTime dt)
        {
            DMApod dto = null;
            HttpResponseMessage response;
            string formattedDate = dt.ToString("yyyy-MM-dd");
            string apiKey = "32BWPNoJaot78acVHQaPdlrls1exHduz7pSKwAZB"; // Asegúrate de usar tu API Key correcta
            string requestUrl = $"https://api.nasa.gov/planetary/apod?date={formattedDate}&api_key={apiKey}";

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                HttpClient client = new HttpClient();
                response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dto = JsonConvert.DeserializeObject<DMApod>(json);
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException($"Error fetching data from NASA API: {response.StatusCode}, {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching data from NASA API", ex);
            }

            return dto;
        }
    }
}
