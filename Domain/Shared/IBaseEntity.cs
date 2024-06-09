namespace Domain.Shared;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
}