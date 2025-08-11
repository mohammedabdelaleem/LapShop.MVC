namespace LapShop.MVC.Services;

public interface ISalesInvoiceService
{
	 Task<List<VwSalesInvoice>> GetAll(CancellationToken cancellationToken = default);

	 Task<TbSalesInvoice?> Get(int id, CancellationToken cancellationToken = default);

	public Task<bool> Save(TbSalesInvoice Item, List<TbSalesInvoiceItem> lstItems, bool isNew, string userId, CancellationToken cancellationToken = default);

	 Task<bool> Delete(int id, CancellationToken cancellationToken = default);
}
