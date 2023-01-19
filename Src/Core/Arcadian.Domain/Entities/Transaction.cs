using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arcadian.Domain.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }

        [Required]
        public string TransactionName { get; set; } = default!;

        public decimal Cost { get; set; }

        public DateTime Date { get; set; }
    }
}
