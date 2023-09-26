using Dapper;

namespace Repository.Queries
{
    public interface IStoreProcedureExecute
    {
        string _connectionString { get; set; }

        Task ExecuteNotReturn(string storeProcedureName, DynamicParameters parameters = null);
        Task<IEnumerable<T>> ExecuteReturnList<T>(string storeProcedureName, DynamicParameters parameters = null);
        Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query);
        Task<IEnumerable<T>> ExecuteReturnListBySql<T>(string query, DynamicParameters parameters = null);
        T ExecuteReturnScaler<T>(string storeProcedureName, DynamicParameters parameters = null);
    }
}