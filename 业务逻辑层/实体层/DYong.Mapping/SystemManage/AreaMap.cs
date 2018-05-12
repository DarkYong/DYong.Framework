
using DYong.Entity.Entitys.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace DYong.Mapping.SystemManage
{
    public class AreaMap : EntityTypeConfiguration<AreaEntity>
    {
        public AreaMap()
        {
            this.ToTable("Sys_Area");
            this.HasKey(t => t.F_Id);
        }
    }
}
