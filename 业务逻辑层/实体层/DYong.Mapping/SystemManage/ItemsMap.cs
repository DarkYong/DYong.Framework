using DYong.Entity.Entitys.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace DYong.Mapping.SystemManage
{
    public class ItemsMap : EntityTypeConfiguration<ItemsEntity>
    {
        public ItemsMap()
        {
            this.ToTable("Sys_Items");
            this.HasKey(t => t.F_Id);
        }
    }
}
