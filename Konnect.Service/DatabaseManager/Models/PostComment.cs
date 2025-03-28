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
    [Table("PostComments")]
    [Index(nameof(Id), Name = "IX_PostComments_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class PostComment : EntityBase
    {
        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string? CommentContent { get; set; }

        [Key]
        [ForeignKey(nameof(User))]
        public string? CommentAuthorId { get; set; }
        public virtual User? CommentAuthor { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
