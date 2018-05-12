using DYong.Entity.Entitys.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace DYong.Mapping.SystemManage
{
    public class RoleMap : EntityTypeConfiguration<RoleEntity>
    {
        public RoleMap()
        {
            this.ToTable("Sys_Role");
            this.HasKey(t => t.F_Id);
        }
    }
}
