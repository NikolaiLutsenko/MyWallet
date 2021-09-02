using System;

namespace MyWallet.Data.Interfaces
{
    public interface HasCreatedAt
    {
        DateTime Date { get; set; }
    }
}
