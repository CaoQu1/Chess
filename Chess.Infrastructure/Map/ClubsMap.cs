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
    /// 平台实体配置
    /// </summary>
    public class ClubsMap : EntityTypeConfiguration<Club>
    {
        public ClubsMap()
        {
            this.ToTable("Clubs");

            this.HasOptional(x => x.Platform)
                .WithMany(y => y.Clubs)
                .HasForeignKey(fk => fk.PlatformId);

        }
    }
}
