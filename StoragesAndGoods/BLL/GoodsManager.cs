namespace BLL
{
    using System.Linq;
    using DML;
    using System.Collections.Generic;
    using DAL;

    public class GoodsManager
    {
        private static StorehouseEntities context = new StorehouseEntities();

        public static void AddGoodAmountById(int id, decimal amount)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                Goods goodAmount = repo.GetByID(id);
                goodAmount.TotalAmount += amount;
                repo.Update(goodAmount);
                uow.SaveChanges();
            }
        }

        public static void RemoveGoodAmountById(int id, decimal amount)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                Goods goodAmount = repo.GetByID(id);
                goodAmount.TotalAmount -= amount;
                repo.Update(goodAmount);
                uow.SaveChanges();
            }
        }

        public static Goods GetGoodById(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                return repo.Get(s => s.ID == id).First();
            }
        }

        public static int GetGoodIdByNameAndStorageId(string name, int storageId)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                return repo.Get(g => g.Name == name && g.StorageID == storageId).Select(id => id.ID).First();
            }
        }

        public static decimal GetGoodAmountByName(string name)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                return repo.Get(s => s.Name == name).Select(amount => amount.TotalAmount).First();
            }
        }

        public static List<decimal> GetGoodAmountByStorageId(int storageId)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                return repo.Get(s => s.StorageID == storageId).Select(a => a.TotalAmount).ToList();
            }
        }

        public static List<string> GetGoodsNamesByStorageId(int storageId)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                return repo.Get(s => s.StorageID == storageId).Select(n => n.Name).ToList();
            }
        }

        public static Dictionary<int, string> LoadGoodsByStorageId(int storageId)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                return repo.Get(s => s.StorageID == storageId)
                     .Select(g => new { g.ID, g.Name })
                     .ToDictionary(g => g.ID, g => g.Name);
            }
        }

        public static List<Goods>LoadAllGoodsByStorageId(int storageId)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                return repo.Get(g=>g.StorageID==storageId).ToList();
            }
        }
        public static void AddGoodToStorage(Goods good)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Goods> repo = new GenericRepository<Goods>(context);
                repo.Add(good);
                uow.SaveChanges();
            }
        }
    }
}
