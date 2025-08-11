namespace LapShop.MVC.Services;

public interface ISalesInvoiceItemsService
{
	public Task<List<TbSalesInvoiceItem>> GetSalesInvoiceId(int id, CancellationToken cancellationToken = default);

	public Task<bool> Save(IList<TbSalesInvoiceItem> Items, int salesInvoiceId, bool isNew, CancellationToken cancellationToken = default);
}
