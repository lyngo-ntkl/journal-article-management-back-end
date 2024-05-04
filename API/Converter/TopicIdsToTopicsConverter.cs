using API.Entities;
using API.Repositories;
using AutoMapper;

namespace API.Converter {
    public class TopicIdsToTopicsConverter : IValueConverter<ICollection<int>?, ICollection<Topic>?>
    {
        private readonly UnitOfWork _unitOfWork;

        public TopicIdsToTopicsConverter(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<Topic>? Convert(ICollection<int>? sourceMember, ResolutionContext context)
        {
            if (sourceMember == null) return null;
            return _unitOfWork.TopicRepository.GetAll().FindAll((topic) => sourceMember.Contains(topic.Id));
        }
    }
}