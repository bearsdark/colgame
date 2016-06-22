namespace ColGameServer.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ColGameDbContext : DbContext
    {
        public ColGameDbContext()
            : base("name=ColGameDbContext")
        {
        }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<ItemChar> ItemChars { get; set; }
        public virtual DbSet<SkillChar> SkillChars { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
