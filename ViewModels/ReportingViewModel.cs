using System.Collections.Generic;
using CMP332.Models;
using CMP332.Services;

namespace CMP332.ViewModels
{
    public class ReportingViewModel:ViewModelBase
    {

        private List<Lettor> _lettors;

        public List<Lettor> AllLettors
        {
            get => _lettors;
            set
            {
                _lettors= value;
                OnPropertyChanged(nameof(AllLettors));
            }
        }

        private List<Invoice> _invoices;

        public List<Invoice> AllInvoices
        {
            get => _invoices;
            set
            {
                _invoices = value;
                OnPropertyChanged(nameof(AllInvoices));
            }
        }

        public ReportingViewModel()
        {
            AllLettors = new LettorService().GetAllLettors();
            AllInvoices = new InvoiceService().FindAll();
        }
    }
}