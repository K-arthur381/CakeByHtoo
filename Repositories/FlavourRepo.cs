using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeByHtoo.DBContent;
using CakeByHtoo.Interfaces;
using CakeByHtoo.Models;
using Microsoft.EntityFrameworkCore;

namespace CakeByHtoo.Repositories
{
    public class FlavourRepo : IFlavour
    {
        private readonly CakeByHtooDBContent _context;

        public FlavourRepo(CakeByHtooDBContent context)
        {
            _context = context;
        }

        public async Task<List<Flavour>> GetAllFlavoursAsync()
        {
            return await _context.Flavours
                .OrderBy(f => f.Name)
                .ToListAsync();
        }

        public async Task<Flavour> GetFlavourByIdAsync(int id)
        {
            return await _context.Flavours.FindAsync(id);
        }

        public async Task AddFlavourAsync(Flavour flavour)
        {
            _context.Flavours.Add(flavour);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlavourAsync(Flavour flavour)
        {
           
            var existingData = await _context.Flavours.FindAsync(flavour.FlavourId);
            if (existingData != null)
            {
                existingData.Name = flavour.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteFlavourAsync(int id)
        {
            var flavour = await _context.Flavours.FindAsync(id);
            if (flavour != null)
            {
                _context.Flavours.Remove(flavour);
                await _context.SaveChangesAsync();
            }
        }
    }
}
