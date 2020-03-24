namespace Model.DbItems.Base
{
    public interface IDbModel<T>
    {
        T Id { get; set; }
    }
}
