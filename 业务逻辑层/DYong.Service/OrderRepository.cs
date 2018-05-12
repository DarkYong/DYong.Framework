
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DYong.Data.Repository;
using DYong.Entity.Entitys;
using DYong.IService;

namespace DYong.Service
{
    public class OrderRepository: RepositoryBase<OrderEntity>, IOrderRepository
    {
    }
}
