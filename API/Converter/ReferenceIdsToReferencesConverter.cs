using API.Entities;
using API.Repositories;
using AutoMapper;

namespace API.Converter {
    public class ReferenceIdsToReferencesConverter : IValueConverter<ICollection<int>?, ICollection<Reference>?>
    {
        private readonly UnitOfWork _unitOfWork;

        public ReferenceIdsToReferencesConverter(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<Reference>? Convert(ICollection<int>? sourceMember, ResolutionContext context)
        {
            if (sourceMember == null) return null;
            return _unitOfWork.ReferenceRepository.GetAll().FindAll(reference => sourceMember.Contains(reference.ReferenceArticleId));
        }
    }
}