using CryTogether.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryTogether.Services
{
    public interface IRESTService
    {
        Task<List<Breakdown>> RefreshDataAsync();

        Task SaveDataAsync(Breakdown breakdown, bool isNew);

        Task DelteDataAsync(string id);

        Task<Breakdown> GetItemAsync(string id);
    }
}
