using API.Entities;

namespace Constant {
    public class MockData {
        public static User[] Readers = [
            new User {
                Id = 1,
                Email = "user@example.com",
                Name = "User",
                Role = Role.READER,
                PasswordHash = "CEKeMeLiKoqYgaaQcRGPLiEiFlRZNUuj9mte7N1FLh4=",
                PasswordSalt = "3wxJSQvxsdLCs4E37G3Igg=="
            }
        ];
        public static User[] Authors = [
            new User {
                Id = 2,
                Email = "author2@example.com",
                Name = "Author 2",
                Role = Role.AUTHOR,
                PasswordHash = "CEKeMeLiKoqYgaaQcRGPLiEiFlRZNUuj9mte7N1FLh4=",
                PasswordSalt = "3wxJSQvxsdLCs4E37G3Igg=="
            },
            new User {
                Id = 3,
                Email = "author3@example.com",
                Name = "Author 3",
                Role = Role.AUTHOR,
                PasswordHash = "CEKeMeLiKoqYgaaQcRGPLiEiFlRZNUuj9mte7N1FLh4=",
                PasswordSalt = "3wxJSQvxsdLCs4E37G3Igg=="
            },
            new User {
                Id = 4,
                Email = "author4@example.com",
                Name = "Author 4",
                Role = Role.AUTHOR,
                PasswordHash = "CEKeMeLiKoqYgaaQcRGPLiEiFlRZNUuj9mte7N1FLh4=",
                PasswordSalt = "3wxJSQvxsdLCs4E37G3Igg=="
            },
            new User {
                Id = 5,
                Email = "author5@example.com",
                Name = "Author 5",
                Role = Role.AUTHOR,
                PasswordHash = "CEKeMeLiKoqYgaaQcRGPLiEiFlRZNUuj9mte7N1FLh4=",
                PasswordSalt = "3wxJSQvxsdLCs4E37G3Igg=="
            }
        ];
        public static Topic[] Topics = [
            new Topic() {Id = 1, TopicName = "Mental health"},
            new Topic {Id = 2, TopicName = "Physics"},
            new Topic {Id = 3, TopicName = "Mathematics"},
            new Topic {Id = 4, TopicName = "Computer Science"},
            new Topic {Id = 5, TopicName = "Chemistry"},
            new Topic {Id = 6, TopicName = "Engineering"},
            new Topic {Id = 7, TopicName = "Agriculture"},
            new Topic {Id = 8, TopicName = "Medicine"},
            new Topic {Id = 9, TopicName = "Pharmacology"},
            new Topic {Id = 10, TopicName = "Biology"},
            new Topic {Id = 11, TopicName = "Biotechnology"},
            new Topic {Id = 12, TopicName = "Biomedical Application"},
            new Topic {Id = 13, TopicName = "Geography"},
            new Topic {Id = 14, TopicName = "Geology"},
            new Topic {Id = 15, TopicName = "Geophysics"},
            new Topic {Id = 16, TopicName = "Oceanography"},
            new Topic {Id = 17, TopicName = "Ecology"},
            new Topic {Id = 18, TopicName = "Climatology"},
            new Topic {Id = 19, TopicName = "Nutrition"}
        ];
        public static Article[] PublishedArticle = [
            new Article {
                    Id = 2,
                    Abstract = "Mock abstract",
                    Introduction = "Mock introduction",
                    Method = "Mock method",
                    Results = "Mock result",
                    Conclusion = "Mock conclusion",
                    Authors = [
                        Authors[0],
                        Authors[1],
                        Authors[2]
                    ],
                    Topics = [
                        Topics[4],
                        Topics[9]
                    ],
                    References = [],
                    Status = ArticleStatus.APPROVED
            }
        ];
        public static Article[] DraftArticles = [
            new Article {
                    Id = 1,
                    Abstract = "Mock abstract",
                    Introduction = "Mock introduction",
                    Method = "Mock method",
                    Results = "Mock result",
                    Conclusion = "Mock conclusion",
                    Authors = [
                        Authors[0],
                        Authors[1],
                        Authors[2]
                    ],
                    Topics = [
                        Topics[4],
                        Topics[9]
                    ],
                    References = [
                        new Reference {
                            ArticleId = 1,
                            ReferenceArticleId = 2
                        }
                    ],
                    Status = ArticleStatus.DRAFTED
            }
        ];
    }
}