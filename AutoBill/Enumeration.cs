namespace AutoBill
{
    public enum SalesTaxes : int
    {
        Unknown,
        IncludedInPrice,
        InAdditionToThePrice,
        NotApplicable
    }

    public enum PaymentForm : int
    {
        Unknown,
        BankDraft,
        Cash,
        InternetBanking,
        PromissoryNote,
        Other
    }
}
