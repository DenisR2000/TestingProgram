using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TesttingServer.Models
{
    public class Qestion
    {
        private readonly ILazyLoader _lazyLoader;
        public Qestion(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        public Qestion()
        {
            Answers = new HashSet<Answer>();
        }

        [Key]
        public int QestionId { get; set; }
        [MaxLength(200)]
        public string QestionName { get; set; }
        private ICollection<Answer> _answers;
        public int TestId { get; set; }
        public Test Test { get; set; }
        public virtual ICollection<Answer> Answers 
        {
            get => _lazyLoader.Load(this, ref _answers);
            set => _answers = value;
        }
    }
}
