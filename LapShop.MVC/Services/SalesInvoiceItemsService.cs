
using LapShop.MVC.Persistance;

namespace LapShop.MVC.Services;

public class SalesInvoiceItemsService(AppDBContext context) : ISalesInvoiceItemsService
{
	private readonly AppDBContext _context = context;

	public async Task<List<TbSalesInvoiceItem>> GetSalesInvoiceId(int id, CancellationToken	cancellationToken=default)=>await _context.TbSalesInvoiceItems
				.Where(x=>x.InvoiceId == id)
				.ToListAsync(cancellationToken);

	// dealing With 2 Lists
	public async Task<bool> Save(IList<TbSalesInvoiceItem> items, int salesInvoiceId, bool isNew, CancellationToken cancellationToken = default)
	{
		// get those which sales invoice id = comming sales invoice 
		List<TbSalesInvoiceItem> dbSalesInvoiceItems =
			await GetSalesInvoiceId(items[0].InvoiceId);



		foreach (var interfaceItems in items)
		{
			var dbObject = await _context.TbSalesInvoiceItems
						   .Where(x => x.InvoiceItemId == interfaceItems.InvoiceItemId)
						   .FirstOrDefaultAsync(cancellationToken);

			if (dbObject != null)
			{
				_context.Update(dbObject);
			}
			else
			{
				interfaceItems.InvoiceId = salesInvoiceId;
				_context.TbSalesInvoiceItems.Add(interfaceItems);
			}
		}


		foreach (var item in dbSalesInvoiceItems)
		{
			var interfaceObject = items.Where(x => x.InvoiceItemId == item.InvoiceItemId)
										.FirstOrDefault();

			if (interfaceObject == null)
			{
				_context.Remove(item);
			}
		}

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}
}
