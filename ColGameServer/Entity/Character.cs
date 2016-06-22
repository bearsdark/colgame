namespace ColGameServer.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Character
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Character()
        {
            ItemChars = new HashSet<ItemChar>();
            SkillChars = new HashSet<SkillChar>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CharacterID { get; set; }

        [Required]
        [StringLength(20)]
        public string CharacterName { get; set; }
        public string CharacterPosition { get; set; }

        [Required]
        [StringLength(50)]
        public string CharacterTexture { get; set; }

        public int CharacterHpTotal { get; set; }
        public int CharacterHpCurent { get; set; }

        public int CharacterManaTotal { get; set; }
        public int CharacterManaCurent { get; set; }

        [Required]
        [StringLength(20)]
        public string CharacterClass { get; set; }

        public int CharacterMoney { get; set; }

        public string CharacterQuestsComplete { get; set; }
        public string CharacterQuestsUnComplete { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
        
        public virtual ICollection<ItemChar> ItemChars { get; set; }
        public virtual ICollection<SkillChar> SkillChars { get; set; }
    }
}
