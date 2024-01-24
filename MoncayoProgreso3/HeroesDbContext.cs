using MoncayoProgreso3.Models;
using SQLite;

namespace MoncayoProgreso3.Data
{
    public class HeroesDbContext
    {
        private readonly SQLiteAsyncConnection _database;

        public HeroesDbContext(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<HeroeGuardado>().Wait();
        }

        public async Task<List<HeroeGuardado>> GetHeroesGuardadosAsync()
        {
            return await _database.Table<HeroeGuardado>().OrderByDescending(h => h.Id).ToListAsync();
        }

        public async Task SaveHeroeGuardadoAsync(HeroeGuardado heroeGuardado)
        {
            await _database.InsertAsync(heroeGuardado);
        }

        public async Task DeleteHeroeGuardadoAsync(int heroeId)
        {
            await _database.DeleteAsync<HeroeGuardado>(heroeId);
        }

    }
}


