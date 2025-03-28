using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.DatabaseManager.Models
{
    [Table("Posts")]
    [Index(nameof(Id), Name = "IX_Posts_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class Post : EntityBase
    {
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string? PostTitle { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string? PostContent { get; set; }

        [Key]
        [ForeignKey(nameof(User))]
        public string? PostAuthorId { get; set; }
        public virtual User? PostAuthor { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
