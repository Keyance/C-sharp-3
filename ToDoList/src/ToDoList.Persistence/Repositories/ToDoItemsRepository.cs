using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Persistence.Repositories
{
    public class ToDoItemsRepository : IRepository<ToDoItem>
    {
        private readonly ToDoItemsContext context;
        public ToDoItemsRepository(ToDoItemsContext context)
        {
            this.context = context;
        }
        public void Create(ToDoItem item)
        {
            context.ToDoItems.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            return context.ToDoItems
                .AsNoTracking() // jen pro čtení
                .ToList();
        }

        public ToDoItem? GetById(int id)
        {
            var itemFromDb = context.ToDoItems
                .AsNoTracking() //definuje, že nebudeme zasahovat do databáze, readonly
                .FirstOrDefault(i => i.ToDoItemId == id);
            return itemFromDb; //místo předchozího vkládání teď musíme mít return
        }

        public void Update(int id, ToDoItem item)
        {
            var itemIndexToUpdate = context.ToDoItems.FirstOrDefault(i => i.ToDoItemId == id);
            if (itemIndexToUpdate is null)
            {
                throw new ArgumentOutOfRangeException($"ToDo item with ID {id} not found.");
            }

            // zajistí konzistenci klíče
            item.ToDoItemId = id;

            // zkopíruje hodnoty z updated do tracked entity
            context.Entry(itemIndexToUpdate).CurrentValues.SetValues(item);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var itemToDelete = context.ToDoItems.FirstOrDefault(i => i.ToDoItemId == id);
            if (itemToDelete is null)
            {
                throw new ArgumentOutOfRangeException($"ToDo item with ID {id} not found.");
            }
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }

    }
}
