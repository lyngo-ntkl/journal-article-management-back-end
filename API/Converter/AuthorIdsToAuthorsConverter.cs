using System.Collections.ObjectModel;
using API.Entities;
using API.Repositories;
using AutoMapper;

namespace API.Converter {
    public class AuthorIdsToAuthorsConverter : IValueConverter<ICollection<int>?, ICollection<User>?>
    {
        private readonly UnitOfWork _unitOfWork;

        public AuthorIdsToAuthorsConverter(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<User>? Convert(ICollection<int>? sourceMember, ResolutionContext context)
        {
            if (sourceMember == null) {
                return null;
            }
            return _unitOfWork.UserRepository.GetAll().FindAll(user => sourceMember.Contains(user.Id));
        }
    }
}