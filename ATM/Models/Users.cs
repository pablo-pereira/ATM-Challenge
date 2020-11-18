using System;
using System.Collections.Generic;

namespace ATM.Models
{
    public partial class Users
    {
        public Users()
        {
            Cards = new HashSet<Cards>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Cards> Cards { get; set; }
    }
}
