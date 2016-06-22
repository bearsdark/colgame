namespace ColGameServer.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SkillChar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SkillCharacterID { get; set; }

        public int SC_CharID { get; set; }

        public int SC_SkillID { get; set; }

        public int SC_Level { get; set; }

        public virtual Character Character { get; set; }
    }
}
