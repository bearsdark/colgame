namespace ColGameServer.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ItemChar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemCharID { get; set; }

        public int IC_CharID { get; set; }

        public int IC_ItemID { get; set; }

        [Required]
        [StringLength(20)]
        public string IC_Position { get; set; }

        public virtual Character Character { get; set; }
    }
}
