using System;
using System.Collections.Generic;

namespace DatabaseModel.DatabaseEntity;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<SalesTransaction> SalesTransactions { get; set; } = new List<SalesTransaction>();
}
