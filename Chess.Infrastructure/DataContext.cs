using Chess.Domain;
using Chess.Domain.UnitOfWork;
using Chess.DtoModel;
using Chess.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Infrastructure
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DataContext : DbContext, IDataContext
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        public DataContext() : base(ConfigConst.DATACONTEXT)
        {
            Database.SetInitializer(new CreateDataBaseInit());
        }

        /// <summary>
        /// 创建数据库实例
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var assembls = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.BaseType != null && x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var item in assembls)
            {
                modelBuilder.Configurations.Add((dynamic)Activator.CreateInstance(item));
            }
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 获取数据库实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// 附加数据库实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return base.Entry<TEntity>(entity);
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }
            var transactionalBehavior = doNotEnsureTransaction ? TransactionalBehavior.DoNotEnsureTransaction : TransactionalBehavior.EnsureTransaction;

            var result = base.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);
            if (timeout.HasValue)
            {
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }
            return result;
        }
    }

    /// <summary>
    /// 重建数据库初始化类（每次）
    /// </summary>
    public class DropDataBaseInit : DropCreateDatabaseAlways<DataContext>
    {

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(DataContext context)
        {
            var initUser = context.Set<SystemUser>();
            initUser.Add(new SystemUser
            {
                Openid = "",
                Unionid = "",
                HeadImgUrl = "",
                NickName = "测试",
                Sex = EnumSex.UnKown,
                UserIdentity = 0,
                CreateTime = DateTime.Now
            });
            base.Seed(context);
        }
    }

    /// <summary>
    /// 创建数据库初始化类（不存在）
    /// </summary>
    public class CreateDataBaseInit : CreateDatabaseIfNotExists<DataContext>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(DataContext context)
        {
            var initUser = context.Set<Platform>();
            initUser.Add(new Platform
            {
                PlatformId = "001",
                PlatformName = "麻将小强",
                CreateTime = DateTime.Now
            });
            base.Seed(context);
        }
    }

    /// <summary>
    /// 创建数据库初始化类（模型变更）
    /// </summary>
    public class ModelChangeDataBaseInit : DropCreateDatabaseIfModelChanges<DataContext>
    {

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(DataContext context)
        {
            var initUser = context.Set<SystemUser>();
            initUser.Add(new SystemUser
            {
                Openid = "",
                Unionid = "",
                HeadImgUrl = "",
                NickName = "测试",
                Sex = EnumSex.UnKown,
                UserIdentity = 0,
                CreateTime = DateTime.Now
            });
            base.Seed(context);
        }
    }
}
