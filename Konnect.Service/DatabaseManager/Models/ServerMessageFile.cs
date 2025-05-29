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
    [Table("ServerMessageFile")]
    [Index(nameof(Id), Name = "IX_ServerMessageFile_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class ServerMessageFile : EntityBase
    {
        [Key]
        [ForeignKey(nameof(Models.ServerMessage))]
        public string ServerMessageId { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(MAX)")]
        public byte[] FileContent { get; set; } = Array.Empty<byte>();
        
        [Column(TypeName = "nvarchar(MAX)")]
        public string FileName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(MAX)")]
        public string FileType { get; set; } = string.Empty;

        #region Relationships
        public virtual ServerMessage? ServerMessage { get; set; }
        #endregion
    }
}
