using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Yxl.Dal.Context
{
    public interface IDbContext
    {
        Task<IDbConnection> OpenWriteAsync();

        IDbConnection OpenWrite();

        Task<IDbConnection> OpenReadAsync();

        IDbConnection OpenRead();
    }
}
