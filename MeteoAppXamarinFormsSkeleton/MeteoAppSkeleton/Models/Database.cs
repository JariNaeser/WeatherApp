using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using MeteoAppSkeleton.Models;
using System.Collections.ObjectModel;

namespace MeteoAppSkeleton
{ 
    public class Database
    {
        private readonly SQLiteAsyncConnection database;


        public Database()
        {
            // collegamento al database
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "weather.db3");
            database = new SQLiteAsyncConnection(dbPath);

            // crea la tabella se non esiste
            database.CreateTableAsync<Location>().Wait();
        }

        
        /*
         * Ritorna una lista con tutti gli items.
         */

        
        public Task<List<Location>> GetItemsAsync()
        {
            var locationType = typeof(Location);
            var locationTypeName = locationType.Name;

            // Log the name of the Location class
            Console.WriteLine($"Location class name: {locationTypeName}");

            Task<List<Location>>  list =database.Table<Location>().ToListAsync();

            

            return list;
        }

        /*
         * Query con statement SQL.
         */
        
        public Task<List<Location>> GetItemsWithWhere(int id)
        {
            return database.QueryAsync<Location>("SELECT * FROM [Location] WHERE [ID] =?", id);
        }

        /*
         * Query con LINQ.
         */
        
        public Task<Location> GetItemAsync(int id)
        {
            return database.Table<Location>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        /*
         * Salvataggio o update.
         */
        public Task<int> SaveItemAsync(Location item)
        {
            if (item.Id == 0) // esempio
                return database.UpdateAsync(item);

            return database.InsertAsync(item);
        }

        /*
         * Cancellazione di un elemento.
         */
        public async Task<int> DeleteItemAsync(Location item)
        {
            return await database.DeleteAsync(item);
        }
    }
}