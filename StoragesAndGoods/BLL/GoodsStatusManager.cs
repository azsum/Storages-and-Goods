namespace BLL
{
    using DAL;
    using DML;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GoodsStatusManager
    {
        private static StorehouseEntities context = new StorehouseEntities();

        public static List<GoodsStatusHelper> LoadGoodsStatusByStorageId(int storageId)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork(context))
                {
                    GenericRepository<GoodsStatus> repo = new GenericRepository<GoodsStatus>(context);

                    var list = repo.Get(gs => gs.Goods.StorageID == storageId)
                        .Select(gs => new GoodsStatusHelper { ID = gs.ID, Name = gs.Goods.Name, Amount = gs.GoodAmount, OperationType = gs.GoodOperationType.TypeOfOperation, DateFrom = gs.StatusDataFrom })
                        .OrderByDescending(gs => gs.DateFrom)
                        .ToList();
                    if (list != null)
                    {
                        return list;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return LoadGoodsStatusByStorageId(storageId);
            }
        }

        public static GoodsStatus GetGoodStatusById(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<GoodsStatus> repo = new GenericRepository<GoodsStatus>(context);
                return repo.GetByID(id);
            }
        }

        public static void Save(GoodsStatus goodStatus)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<GoodsStatus> repo = new GenericRepository<GoodsStatus>(context);
                repo.Add(goodStatus);
                uow.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<GoodsStatus> repo = new GenericRepository<GoodsStatus>(context);
                GoodsStatus goodStatus = GetGoodStatusById(id);
                repo.Remove(goodStatus);
                uow.SaveChanges();
            }
        }
    }
}
