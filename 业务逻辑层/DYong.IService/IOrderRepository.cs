using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DYong.Data.Repository;
using DYong.Entity.Entitys;

namespace DYong.IService
{
    public interface IOrderRepository: IRepositoryBase<OrderEntity>
    {
    }
}
