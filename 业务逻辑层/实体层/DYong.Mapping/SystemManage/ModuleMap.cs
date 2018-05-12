using DYong.Entity.Entitys.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace DYong.Mapping.SystemManage
{
    public class ModuleMap : EntityTypeConfiguration<ModuleEntity>
    {
        public ModuleMap()
        {
            this.ToTable("Sys_Module");
            this.HasKey(t => t.F_Id);
        }
    }
}
