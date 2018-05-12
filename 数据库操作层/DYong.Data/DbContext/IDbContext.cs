using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYong.Data
{
    /// <summary>
    /// 数据库连接接口
    /// </summary>
    public interface IDbContext : IDisposable, IObjectContextAdapter
    {
    }
}
