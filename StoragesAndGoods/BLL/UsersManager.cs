namespace BLL
{
    using DAL;
    using DML;
    using System.Linq;

    public class UsersManager
    {
        private static StorehouseEntities context = new StorehouseEntities();


        public static void SaveUser(Users user)
        {

            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Users> repo = new GenericRepository<Users>(context);
                repo.Add(user);
                uow.SaveChanges();
            }
        }

        public static bool IsEmailExist(string email)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Users> repo = new GenericRepository<Users>(context);
                string mail = repo.Get(e => e.Email == email).Select(e => e.Email).FirstOrDefault();
               
                if (email == mail)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static int GetUserIdByEmail(string email)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Users> repo = new GenericRepository<Users>(context);
                var id = repo.Get(u => u.Email == email).Select(u => u.ID).First();
                return id;
            }
        }

        public static string GetUsernamById(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Users> repo = new GenericRepository<Users>(context);
                var username = repo.Get(u => u.ID == id).Select(u => u.Username).First();
                return username;
            }
        }

        public static string LoadHashedPassword(string email)
        {
            using (UnitOfWork uow = new UnitOfWork(context))
            {
                GenericRepository<Users> repo = new GenericRepository<Users>(context);
                var id = GetUserIdByEmail(email);
                return repo.Get(u => u.ID == id).Select(p => p.Password).First();
            }
        }
    }
}
