namespace HungryDogs.Logic.Entities
{
    internal abstract class IdentityEntity : HungryDogs.Contracts.IIdentifiable
    {
        public int Id { get; set; }
    }
}
