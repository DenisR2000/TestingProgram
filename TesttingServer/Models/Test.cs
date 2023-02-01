using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TesttingServer.Models
{
    public class Test
    {
        private readonly ILazyLoader _lazyLoader;
        public Test(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        public Test()
        {
            Qestions = new List<Qestion>();
        }
        [Key]
        public int TestId { get; set; }
        [MaxLength(200)]
        public string TestTitle { get; set; }
        public int Type { get; set; }
        [MaxLength(200)]
        public string TestDescription { get; set; }
        private ICollection<Qestion> _qestions;
        public virtual ICollection<Qestion> Qestions
        {
            get => _lazyLoader.Load(this, ref _qestions);
            set => _qestions = value;
        }
    }
}
