using System;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Mappers
{
    public class BookModelMapper : IBookModelMapper
    {
        public BookInfoModel GetBookInfoModel(BookProfile bookProfile)
        {
            if (bookProfile == null)
            {
                throw new ArgumentNullException(nameof(bookProfile));
            }

            return new BookInfoModel
            {
                Id = bookProfile.Id,
                Title = bookProfile.Title,
                Authors = bookProfile.Authors,
                Accessibility = bookProfile.Accessibility
            };
        }

        public BookFullInfoModel GetBookFullInfoModel(BookProfile bookProfile)
        {
            if (bookProfile == null)
            {
                throw new ArgumentNullException(nameof(bookProfile));
            }

            return new BookFullInfoModel
            {
                Id = bookProfile.Id,
                Title = bookProfile.Title,
                CreationYear = bookProfile.CreationYear,
                PagesCount = bookProfile.PagesCount,
                Accessibility = bookProfile.Accessibility,
                Authors = bookProfile.Authors,
                Genres = bookProfile.Genres,
                UserName = null
            };
        }
    }
}