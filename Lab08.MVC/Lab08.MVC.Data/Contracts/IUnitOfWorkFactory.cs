namespace Lab08.MVC.Data
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}