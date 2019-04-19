using Lab08.MVC.Domain;

namespace Lab08.MVC.Mappers
{
    public interface IBookModelMapper
    {
        BookInfoModel GetBookInfoModel(BookProfile bookProfile);

        BookFullInfoModel GetBookFullInfoModel(BookProfile bookProfile);
    }
}