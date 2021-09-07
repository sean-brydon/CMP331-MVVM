using CMP332.Data;
using CMP332.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CMP332.Services
{
    public class InvoiceService
    {
        private IRepository<Invoice> _invoiceContext;

        public InvoiceService()
        {
           _invoiceContext = ContainerHelper.Container.Resolve<IRepository<Invoice>>();
        }

        public List<Invoice> FindAll()
        {
            return _invoiceContext.Collection().ToList();
        }

        public List<Invoice> FindAllOverdue()
        {
            DateTime dateTime = new DateTime();
            return _invoiceContext.DbSet().Where(e => e.AmountOwed > 0 && e.PaymentDueDate < dateTime).ToList();
        }
    }
}
