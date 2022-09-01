using Models;

namespace Services;

public class InvoicesService : IInvoicesService
{
    private Repositories.IInvoicesRepository _invoicesRepo;

    public InvoicesService(Repositories.IInvoicesRepository invoicesRepo)
    {
        _invoicesRepo = invoicesRepo;
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Invoice Get(string id)
    {
        throw new NotImplementedException();
    }

    public List<Invoice> GetInvoices()
    {
        throw new NotImplementedException();
    }

    public Invoice Put(Invoice invoice)
    {
        throw new NotImplementedException();
    }
}