using System.Linq;
using System.Threading.Tasks;
using Bread.ExamSystem.Project.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WalkingTec.Mvvm.Core;

namespace Bread.ExamSystem.Project.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DbSet<FrameworkUser> FrameworkUsers { get; set; }


        public DataContext(CS cs)
             : base(cs)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype)
            : base(cs, dbtype)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype, string version = null)
            : base(cs, dbtype, version)
        {
        }

        /// <summary>
        /// 题型
        /// </summary>
        public DbSet<QuestionType> QuestionTypes { get; set; }
        /// <summary>
        /// 题库
        /// </summary>
        public DbSet<Question> Questions { get; set; }
        /// <summary>
        /// 考试设置
        /// </summary>
        public DbSet<ExaminationSetup> ExaminationSetups { get; set; }
        /// <summary>
        /// 印章
        /// </summary>
        public DbSet<Seal> Seals { get; set; }
        /// <summary>
        /// 免账号参与考试记录
        /// </summary>
        public DbSet<RecordNoAccount> RecordNoAccounts { get; set; }
        /// <summary>
        /// 登录账号参与考试记录
        /// </summary>
        public DbSet<RecordWithAccount> RecordWithAccounts { get; set; }
        /// <summary>
        /// 登录用户
        /// </summary>
        public DbSet<WebUser> WebUsers { get; set; }
        /// <summary>
        /// 轮播图设置
        /// </summary>
        public DbSet<RotationChart> RotationCharts { get; set; }
        /// <summary>
        /// 试题收藏夹
        /// </summary>
        public DbSet<Collection> Collections { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public DbSet<UnitWork> UnitWorks { get; set; }
        /// <summary>
        /// 错题库
        /// </summary>
        public DbSet<WrongQuestionBank> WrongQuestionBanks { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public override async Task<bool> DataInit(object allModules, bool IsSpa)
        {
            var state = await base.DataInit(allModules, IsSpa);
            bool emptydb = false;
            try
            {
                emptydb = Set<FrameworkUser>().Count() == 0 && Set<FrameworkUserRole>().Count() == 0;
            }
            catch { }
            if (state == true || emptydb == true)
            {
                //when state is true, means it's the first time EF create database, do data init here
                //当state是true的时候，表示这是第一次创建数据库，可以在这里进行数据初始化
                var user = new FrameworkUser
                {
                    ITCode = "admin",
                    Password = Utils.GetMD5String("000000"),
                    IsValid = true,
                    Name = "Admin"
                };

                var userrole = new FrameworkUserRole
                {
                    UserCode = user.ITCode,
                    RoleCode = "001"
                };
                
                Set<FrameworkUser>().Add(user);
                Set<FrameworkUserRole>().Add(userrole);
                await SaveChangesAsync();
            }
            return state;
        }

    }

    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("your full connection string", DBTypeEnum.SqlServer);
        }
    }

}
