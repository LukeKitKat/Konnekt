using Azure.Messaging;
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
    [Table("ServerMessage")]
    [Index(nameof(Id), Name = "IX_ServerMessage_Id", IsUnique = true)]
    [PrimaryKey(nameof(Id))]
    public class ServerMessage : EntityBase
    {
        [ForeignKey(nameof(Models.ServerChannel))]
        public string ChannelId { get; set; } = string.Empty;

        [ForeignKey(nameof(Models.User))]
        public string SenderId { get; set; } = string.Empty;

        public string? MessageBody { get; set; }

        public DateTime TimeSent { get; set; }

        public DateTime? TimeEdited { get; set; }

        #region Relationships
        public virtual ServerChannel? Channel { get; set; }
        public virtual User? Sender { get; set; }
        public virtual ICollection<ServerMessageFile> ServerMessageFiles { get; set; } = [];
        #endregion
    }
}
