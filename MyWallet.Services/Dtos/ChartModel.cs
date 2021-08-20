namespace MyWallet.Services.Dtos
{
    public record ChartModel(string DataSetLabel, string[] Labels, decimal[] Amounts, string[] Colors);
}
