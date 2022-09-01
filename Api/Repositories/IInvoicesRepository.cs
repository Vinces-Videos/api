using Models;

namespace Repositories;

//An interface for an Invoice repository object
public interface IInvoicesRepository
{
    Invoice Get(string id);
    List<Invoice> GetInvoices();
    void Delete(string id);
    Invoice Put(Invoice invoice);
}