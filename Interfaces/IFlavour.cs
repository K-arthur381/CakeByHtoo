using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeByHtoo.Models;

namespace CakeByHtoo.Interfaces
{
    public interface IFlavour
    {
        Task<List<Flavour>> GetAllFlavoursAsync();
        Task<Flavour> GetFlavourByIdAsync(int id);
        Task AddFlavourAsync(Flavour flavour);
        Task UpdateFlavourAsync(Flavour flavour);
        Task DeleteFlavourAsync(int id);
    }
}
