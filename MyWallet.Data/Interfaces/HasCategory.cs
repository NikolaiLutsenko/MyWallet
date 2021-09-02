using MyWallet.Data.Entities;
using System;

namespace MyWallet.Data.Interfaces
{
    public interface HasCategory
    {
        Guid CategoryId { get; }

        CategoryEntity Category { get; }
    }
}
