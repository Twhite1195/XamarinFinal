using ProyectoFinal.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Data
{
    public class ChoneteDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public ChoneteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Usuario>().Wait();
            database.CreateTableAsync<Producto>().Wait();
        }

        public async Task<List<Usuario>> getUsuariosAsync()
        {
            return await database.Table<Usuario>().ToListAsync();
        }

        public async Task<List<Producto>> getProductosAsync()
        {
            return await database.Table<Producto>().ToListAsync();
        }

        public Task<Usuario> getUsuarioAsync(int id)
        {
            return database.Table<Usuario>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<Producto> getProductoAsync(int id)
        {
            return database.Table<Producto>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> saveUsuarioAsync(Usuario item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> saveProductoAsync(Producto item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteUsuarioAsync(Usuario item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> DeleteProductoAsync(Producto item)
        {
            return database.DeleteAsync(item);
        }

    }
}
