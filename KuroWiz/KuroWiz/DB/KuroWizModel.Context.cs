﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace KuroWiz.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KuroWizEntities : DbContext
    {
        public KuroWizEntities()
            : base("name=KuroWizEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<M_CATEGORY> M_CATEGORY { get; set; }
        public virtual DbSet<M_DIFFICULTY> M_DIFFICULTY { get; set; }
        public virtual DbSet<T_ANSWER> T_ANSWER { get; set; }
        public virtual DbSet<T_QUIZ> T_QUIZ { get; set; }
    }
}
