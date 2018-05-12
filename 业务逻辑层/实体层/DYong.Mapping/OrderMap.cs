using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using DYong.Entity.Entitys;

namespace DYong.Mapping
{
    /// <summary>
    /// 订单信息映射
    /// </summary>
    public class OrderMap: EntityTypeConfiguration<OrderEntity>
    {
        public OrderMap()
        {
            this.ToTable("Test_Order");
            this.HasKey(t => t.OrderID);
        }
    }
}
