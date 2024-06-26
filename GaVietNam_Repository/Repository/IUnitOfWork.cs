using GaVietNam_Repository.Entity;

namespace GaVietNam_Repository.Repository
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Admin> AdminRepository { get;  }
        public IGenericRepository<Bill> BillRepository { get; }
        public IGenericRepository<Contact> ContactRepository { get; }
        public IGenericRepository<Kind> KindRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<OrderItem> OrderItemRepository { get; }
        public IGenericRepository<Chicken> ChickenRepository { get; }
        public IGenericRepository<Role> RoleRepository { get; }
        public IGenericRepository<User> UserRepository { get; }
        void Save();
        void Dispose();
    }
}
