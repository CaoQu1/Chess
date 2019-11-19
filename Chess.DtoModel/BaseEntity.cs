using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.DtoModel
{
    /// <summary>
    /// 数据库实体基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 编号（自增）
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 获取hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <summary>
        /// 比较实体是否相等（hashcode相同）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!Object.ReferenceEquals(this, obj))
            {
                var entity = obj as BaseEntity;
                return entity.Id.Equals(this.Id);
            }
            return true;
        }
    }
}
