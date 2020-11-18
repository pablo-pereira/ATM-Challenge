using System;
using System.Collections.Generic;

namespace ATM.Models
{
    public partial class Cards
    {
        public Cards()
        {
            Operations = new HashSet<Operations>();
        }

        public int CardId { get; set; }
        public int ErrorsCount { get; set; }
        public string Number { get; set; }
        public int Pin { get; set; }
        public long Balance { get; set; }
        public int UserId { get; set; }
        public bool IsLoked { get; set; }
        public DateTime ValidDate { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Operations> Operations { get; set; }
        
    }
}
