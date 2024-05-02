using App.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    class RepositoryVehiculos
    {
        string urlApi = "https://pracprof2023-af4f.restdb.io/rest/vehiculos";
        HttpClient client = new HttpClient();
        public RepositoryVehiculos()
        {
            //configuramos que trabajará con respuestas JSON
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("apikey", "11851071b7e2d5e690135e3dfd83482d68424");
        }
        public async Task<ObservableCollection<AppVehiculos>> GetAllAsync()
        {
            try
            {
                var response = await client.GetStringAsync(urlApi);
                return
                       JsonConvert.DeserializeObject<ObservableCollection<AppVehiculos>>(response);
            }
            catch (Exception error)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un error: " + error.Message, "Ok");
                return null;
            }
        }
        public async Task<bool> RemoveAsync(string id)
        {
            var response = await client.DeleteAsync($"{urlApi}/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddAsync(AppVehiculos app)
        {
            var response = await client.PostAsync(urlApi,
                new StringContent(
                    JsonConvert.SerializeObject(app),
                Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAsync(AppVehiculos app)
        {
            var response = await client.PutAsync($"{urlApi}/{app._id}",
                new StringContent(
                    JsonConvert.SerializeObject(app),
                Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
