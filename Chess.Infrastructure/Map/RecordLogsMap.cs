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
    /// 战绩记录实体配置
    /// </summary>
    public class RecordLogMap : EntityTypeConfiguration<RecordLog>
    {
        public RecordLogMap()
        {
            this.ToTable("RecordLogs");

            this.HasOptional(x => x.SystemUser).WithMany(y => y.RecordLogs).HasForeignKey(fk => fk.SystemUserId);

        }
    }
}
