using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP332.Models
{
    public class Invoice : ModelBase
    {
        public Lettor Lettor { get; set; }
        public float AmountOwed { get; set; }

        public DateTime PaymentDueDate { get; set; }

        public Invoice()
        {

        }

        public Invoice(Lettor lettor, float amountOwed, DateTime paymentDueDate)
        {
            Lettor = lettor;
            AmountOwed = amountOwed;
            PaymentDueDate = paymentDueDate;
        }


        public Invoice(int id, Lettor lettor, float amountOwed, DateTime paymentDueDate)
        {
            Id = id;
            Lettor = lettor;
            AmountOwed = amountOwed;
            PaymentDueDate = paymentDueDate;
        }
    }
}
