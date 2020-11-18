using System;
using System.Collections.Generic;

namespace ATM.Models
{
    public partial class Operations
    {
        public int OperationId { get; set; }
        public OperationType OperationType { get; set; }
        public int CardId { get; set; }
        public DateTime DateOperation { get; set; }
        public int? Amount { get; set; }

        public virtual Cards Card { get; set; }

        public bool ValidateWithdrawal(long amount) 
        {
            return this.Card.Balance < amount;            
        }
    }

    public enum OperationType { 
        Balance,
        Withdrawal,
        Deposit
    }
}
