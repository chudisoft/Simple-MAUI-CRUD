using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMAUICRUD.Classes
{
    public class AppDb
    {
        SQLiteAsyncConnection db;

        public AppDb()
        {
        }


        async Task Init()
        {
            if (db is not null)
                return;

            db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await db.CreateTableAsync<Item>();
        }

        // CRUD Operations
        public async Task<List<Item>> GetItemsAsync()
        {
            await Init();
            return await db.Table<Item>().ToListAsync();
        }

        public async Task<List<Item>> GetItemsWithOver12NameCharsAsync()
        {
            await Init();
            return await db.Table<Item>().Where(t => t.Name.Length > 12).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<Item> GetItemAsync(int id)
        {
            await Init();
            return await db.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Item item)
        {
            await Init();
            int r = 0;
            if (item.Id != 0)
                r = await db.UpdateAsync(item);
            else
                r = await db.InsertAsync(item);

            return r;
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            await Init();
            var item = await db.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
            if (item == null) throw new Exception($"Item with id {id} was not found!");
            return await db.DeleteAsync(item);
        }
    }

}
