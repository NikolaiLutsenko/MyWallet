namespace MyWallet.Services.Dtos
{
    public record StatisticItem(string Label, decimal prevAmount, decimal currentAmount, decimal percent);
}
