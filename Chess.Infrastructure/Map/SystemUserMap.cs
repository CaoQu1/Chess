using Chess.DtoModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain.Map
{
    /// <summary>
    /// 用户实体配置
    /// </summary>
    public class SystemUserMap : EntityTypeConfiguration<SystemUser>
    {
        public SystemUserMap()
        {
            this.ToTable("SystemUsers");

            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.HasOptional(x => x.Platform)
                .WithMany(y => y.SystemUsers)
                .HasForeignKey(fk => fk.PlatformId);
        }
    }
}
