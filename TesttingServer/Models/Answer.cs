using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TesttingServer.Models
{
    public class Answer
    {
        public Answer() { }

        [Key]
        public int AnswerId { get; set; }
        [Required]
        public string AnswerToQuestion { get; set; }
        public int QestionId { get; set; }
        public bool IsTrue { get; set; } = false;
        public virtual Qestion Qestion { get; set; }
        /*private ICollection<Qestion> _qestions;
          public virtual ICollection<Qestion> Qestions
                {
                    get => _lazyLoader.Load(this, ref _qestions);
                 set => _qestions = value;
        }*/
    }
}
