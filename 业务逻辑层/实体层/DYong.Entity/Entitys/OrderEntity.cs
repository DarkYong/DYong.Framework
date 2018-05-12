using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace DYong.Entity.Entitys
{
    /// <summary>
    /// 订单信息
    /// </summary>
    [TableClassInfo(TableName = "Test_Order", PhysicalID = "OrderID", TableDescribtion = "订单信息表")]
    public class OrderEntity
    {
        private string _OrderID;
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderID
        {
            get{return _OrderID;}
            set{ _OrderID = value; }
        }

        private string _OrderNo;
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo
        {
            get{ return _OrderNo;}
            set{ _OrderNo = value;}
        }   
  
        private string _OrderName;
        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName
        {
            get { return _OrderName;  }
            set {  _OrderName = value; }
        }

        private decimal? _TotalSum;
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? TotalSum
        {
            get{return _TotalSum; }
            set{ _TotalSum = value; }
        }

        private DateTime? _CreateTime;
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime? CreateTime
        {
            get{return _CreateTime;}
            set{ _CreateTime = value;}
        }

        private int? _State;
        /// <summary>
        /// 状态
        /// </summary>
        public int? State
        {
            get{ return _State;}
            set{ _State = value;}
        }

        private string _Rem;
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Rem
        {
            get{ return _Rem;}
            set{ _Rem = value; }
        }

        private string _Ext;
        /// <summary>
        /// 扩展字段
        /// </summary>
        [NotMapped]
        public string Ext
        {
            get{return _Ext; }
            set{ _Ext = value; }
        }

    }
}
