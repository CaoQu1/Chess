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
    /// 操作实体配置
    /// </summary>
    public class OperationRecordsMap : EntityTypeConfiguration<OperationRecord>
    {
        public OperationRecordsMap()
        {
            this.ToTable("OperationRecords");

            this.HasRequired(x => x.SystemUser).WithMany(y => y.OperationRecords).HasForeignKey(fk => fk.SystemUserId);
        }
    }
}
