using Repository.Model;

namespace Repository.Repositories
{
    public class ChangePasswordLogRepository : BaseRepositorySql<QTTS01_ChangePasswordLog>, IChangePasswordLogRepository
    {
        public ChangePasswordLogRepository() : base() { }
    }
}
