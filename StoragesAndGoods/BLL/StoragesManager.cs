namespace BLL
{
    using DAL;
    using DML;
    using System.Collections.Generic;
    using System.Linq;

    public class StoragesManager
    {
        private static StorehouseEntities context = new StorehouseEntities();

        public static List<string> GetStoragesNames()
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Storages> repo = new GenericRepository<Storages>(context);
                return repo.Get(s => s.Name != string.Empty).Select(s => s.Name).ToList();
            }
        }

        public static List<string> GetStoragesNamesByUserId(int userId)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Storages> repo = new GenericRepository<Storages>(context);
                return repo.Get(s => s.UserID == userId).Select(s => s.Name).ToList();
            }
        }

        public static List<int> GetStoragesIdsByUserId(int userId)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Storages> repo = new GenericRepository<Storages>(context);
                return repo.Get(s => s.UserID == userId).Select(s => s.ID).ToList();
            }
        }

        public static decimal GetStorageCapacityByName(string name)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Storages> repo = new GenericRepository<Storages>(context);
                return repo.Get(s => s.Name == name).Select(s => s.Capacity).First();
            }
        }
        public static void AddStorage(Storages storage)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Storages> repo = new GenericRepository<Storages>(context);
                repo.Add(storage);
                uow.SaveChanges();
            }
        }

        public static string GetImagePath(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Storages> repo = new GenericRepository<Storages>(context);
                return repo.Get(s => s.ID == id).Select(s => s.ImagePath).First();
            }
        }

        public static decimal GetStorageCapacity(int storageId)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Storages> repo = new GenericRepository<Storages>(context);
                return repo.Get(s => s.ID == storageId).Select(s => s.Capacity).First();
            }
        }
        public static string GetStorageNameById(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Storages> repo = new GenericRepository<Storages>(context);
                return repo.Get(s => s.ID == id).Select(s => s.Name).First();
            }
        }

        public static int GetStorageIdByName(string name)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Storages> repo = new GenericRepository<Storages>(context);
                var StoragesId = repo.Get(s => s.Name == name).Select(s => s.ID).First();
                if (StoragesId > 0)
                {
                    return StoragesId;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
