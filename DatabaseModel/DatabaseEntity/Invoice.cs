using System;
using System.Collections.Generic;

namespace DatabaseModel.DatabaseEntity;

public partial class Invoice
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<SalesTransaction> SalesTransactions { get; set; } = new List<SalesTransaction>();
}
