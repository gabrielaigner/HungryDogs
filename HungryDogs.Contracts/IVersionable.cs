namespace HungryDogs.Contracts
{
    public interface IVersionable : IIdentifiable
    {
        byte[] RowVersion { get; set; }
    }
}
