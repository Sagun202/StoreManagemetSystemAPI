using System;
using System.Collections.Generic;

namespace DatabaseModel.DatabaseEntity;

public partial class Customer
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Invoice>? Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<SalesTransaction>? SalesTransactions { get; set; } = new List<SalesTransaction>();
}
