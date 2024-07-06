using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaVietNam_Repository.Entity
{
    [Table("Token")]
    public class Token
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    
        public long UserId { get; set; }

        public string TokenValue { get; set; }

        public DateTime Time { get; set; }

        public bool Revoked { get; set; }

        public bool IsExpired { get; set; }
    
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}