using HungryDogs.Contracts;

namespace HungryDogs.Logic.Entities
{
    internal abstract class VersionEntity : IdentityEntity, HungryDogs.Contracts.IVersionable
    {
        public byte[] RowVersion { get; set; }
    }
}
