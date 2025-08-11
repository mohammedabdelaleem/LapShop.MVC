
using LapShop.MVC.Persistance;
using System.Threading.Tasks;

namespace LapShop.MVC.Services;

public class SalesInvoiceService(AppDBContext context , 
	ISalesInvoiceItemsService salesInvoiceItemsService
	) : ISalesInvoiceService
{
	private readonly AppDBContext _context = context;
	private readonly ISalesInvoiceItemsService _salesInvoiceItemsService = salesInvoiceItemsService;

	public  async Task<List<VwSalesInvoice>> GetAll(CancellationToken cancellationToken=default)=>await _context.VwSalesInvoices.ToListAsync(cancellationToken);

	public async Task<TbSalesInvoice?> Get(int id, CancellationToken cancellationToken = default)=> await _context.TbSalesInvoices.Where(x => x.InvoiceId == id).SingleOrDefaultAsync(cancellationToken);

	public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
	{
		var invoice = await Get(id,cancellationToken);
		if (invoice != null) 
			{ 
			    _context.Remove(invoice);
				await _context.SaveChangesAsync(cancellationToken);
				return true;
			}

			return false;

	}

	public async Task<bool> Save(
		TbSalesInvoice item,
		List<TbSalesInvoiceItem> lstItems,
		bool isNew,
		string userId,
		CancellationToken cancellationToken = default)
	{
		if (lstItems == null || lstItems.Count == 0)
			throw new ArgumentException("Invoice must have at least one item.", nameof(lstItems));

		if (string.IsNullOrWhiteSpace(userId))
			throw new ArgumentException("User ID is required.", nameof(userId));

		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			item.CurrentState = 1;

			if (isNew)
			{
				item.CreatedBy = userId; // Or Guid.Parse(userId) if property type is Guid
				item.CreatedDate = DateTime.UtcNow;
				await _context.TbSalesInvoices.AddAsync(item, cancellationToken);
			}
			else
			{
				item.UpdatedBy = userId;
				item.UpdatedDate = DateTime.UtcNow;
				_context.TbSalesInvoices.Update(item);
			}

			await _context.SaveChangesAsync(cancellationToken);

			await _salesInvoiceItemsService.Save(lstItems, item.InvoiceId, true, cancellationToken);

			await transaction.CommitAsync(cancellationToken);

			return true;
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync(cancellationToken);
			throw new InvalidOperationException("Failed to save sales invoice.", ex);
		}
	}

}
