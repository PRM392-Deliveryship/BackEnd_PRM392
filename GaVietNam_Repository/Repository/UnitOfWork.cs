using GaVietNam_Repository.Entity;

namespace GaVietNam_Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private GaVietNamContext _context = new GaVietNamContext();
        private IGenericRepository<Admin> _adminRepository;
        private IGenericRepository<CartItem> _cartItemRepository;
        private IGenericRepository<Bill> _billRepository;
        private IGenericRepository<Chicken> _chickenRepository;
        private IGenericRepository<Contact> _contactRepository;
        private IGenericRepository<Kind> _kindRepository;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<OrderItem> _orderItemRepository;
        private IGenericRepository<Role> _roleRepository;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Cart> _cartRepository;

        public UnitOfWork()
        {
        }

        public IGenericRepository<Cart> CartRepository
        {
            get
            {

                if (_cartRepository == null)
                {
                    _cartRepository = new GenericRepository<Cart>(_context);
                }
                return _cartRepository;
            }
        }
        public IGenericRepository<Admin> AdminRepository
        {
            get
            {

                if (_adminRepository == null)
                {
                    _adminRepository = new GenericRepository<Admin>(_context);
                }
                return _adminRepository;
            }
        }
        public IGenericRepository<CartItem> CartItemRepository
        {
            get
            {

                if (_cartItemRepository == null)
                {
                    _cartItemRepository = new GenericRepository<CartItem>(_context);
                }
                return _cartItemRepository;
            }
        }
        public IGenericRepository<Chicken> ChickenRepository
        {
            get
            {

                if (_chickenRepository == null)
                {
                    _chickenRepository = new GenericRepository<Chicken>(_context);
                }
                return _chickenRepository;
            }
        }
        public IGenericRepository<Contact> ContactRepository
        {
            get
            {

                if (_contactRepository == null)
                {
                    _contactRepository = new GenericRepository<Contact>(_context);
                }
                return _contactRepository;
            }
        }
        public IGenericRepository<Kind> KindRepository
        {
            get
            {

                if (_kindRepository == null)
                {
                    _kindRepository = new GenericRepository<Kind>(_context);
                }
                return _kindRepository;
            }
        }
        public IGenericRepository<OrderItem> OrderItemRepository
        {
            get
            {

                if (_orderItemRepository == null)
                {
                    _orderItemRepository = new GenericRepository<OrderItem>(_context);
                }
                return _orderItemRepository;
            }
        }
        public IGenericRepository<Role> RoleRepository
        {
            get
            {

                if (_roleRepository == null)
                {
                    _roleRepository = new GenericRepository<Role>(_context);
                }
                return _roleRepository;
            }
        }
        public IGenericRepository<User> UserRepository
        {
            get
            {

                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }
                return _userRepository;
            }
        }
        public IGenericRepository<Order> OrderRepository
        {
            get
            {

                if (_orderRepository == null)
                {
                    _orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }

        public IGenericRepository<Bill> BillRepository
        {
            get
            {

                if (_billRepository == null)
                {
                    _billRepository = new GenericRepository<Bill>(_context);
                }
                return _billRepository;
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}