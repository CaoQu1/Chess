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
    /// 俱乐部实体配置
    /// </summary>
    public class ClubMembersMap : EntityTypeConfiguration<ClubMember>
    {
        public ClubMembersMap()
        {
            this.ToTable("ClubMembers");

            this.HasOptional(x => x.SystemUser).WithMany().HasForeignKey(x => x.SystemUserId);

            this.HasOptional(x => x.Club).WithMany(y => y.ClubMembers).HasForeignKey(fk => fk.SystemClubId).WillCascadeOnDelete(true);
        }
    }
}
