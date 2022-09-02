using Models;

namespace Services;

public interface IInvoicesService
{
    Invoice Get(string id);
    List<Invoice> Get();
    void Delete(string id);
    Invoice Put(Invoice Invoice);
}