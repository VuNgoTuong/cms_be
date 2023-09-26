using Common;
using Common.Commons;
using Common.Params.Base;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.CustomModel;
using Repository.Model;
using Repository.Queries;
using Repository.Repositories;
using System.Data;

namespace Repository.Repositories
{
    public class BankRepository : BaseRepositorySql<QTTS01_Bank>, IBankRepository
    {
        
    }
}
