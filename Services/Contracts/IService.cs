using Models.DbModels;

namespace Services.Contracts
{
    public interface IService
    {
    }

    public interface IService<TDbEntity> : IService where TDbEntity : BaseEntity
    {
    }
}
