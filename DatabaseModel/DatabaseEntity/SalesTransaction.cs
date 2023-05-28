using System;
using System.Collections.Generic;

namespace DatabaseModel.DatabaseEntity;

public partial class SalesTransaction
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool? IsInvoiceGenerated { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; } 

    public virtual Product? Product { get; set; }

    public virtual ICollection<Invoice>? Invoices { get; set; } = new List<Invoice>();
    public string? CustomerName { get; set; }
    public string? ProductName { get; set; }
}
