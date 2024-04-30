using System.Collections.ObjectModel;
using System.Net.Http.Json;
using API.Dto.Requests;

namespace UnitTesting.Article {
    // [TestFixture]
    public class ArticleCreationTest {
        private const string BaseUrl = "https://localhost:7258/v1/api/articles";

        [SetUp]
        public void SetUp() {
        }

        [Test]
        public async Task TestCreateArticleUsingTextSuccefully() {
            HttpClient client = new HttpClient();
            ArticleCreationRequestText request = new ArticleCreationRequestText() {
                Abstract = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Introduction = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?",
                Method = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure?\n" +
                    "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.\n" +
                    "On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains.",
                Results = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc consequat mauris ac elit aliquet consequat quis sed ligula. Nulla venenatis sapien quis dui bibendum blandit. Etiam efficitur mollis justo ac commodo. Etiam tincidunt leo ac risus aliquam ultricies. Nullam suscipit sodales nibh, sed sodales orci placerat vel. Donec scelerisque tristique tincidunt. Duis orci dolor, condimentum a sem id, tincidunt lacinia ex. Mauris non eleifend nisi. Nam vitae dictum leo. Aliquam erat volutpat. Nulla quis orci id libero placerat interdum in sed augue. Cras vestibulum dolor auctor nibh lobortis, nec pharetra ligula iaculis. Vestibulum eu elit ante. Fusce ultricies aliquet sem at semper. Phasellus placerat erat vel nisi tempor eleifend.\n"
                    + "Nullam vitae ex cursus, placerat quam ac, viverra urna. Ut a ipsum eu urna suscipit facilisis eu a risus. Nunc vel augue suscipit, lacinia nibh sit amet, aliquam erat. Interdum et malesuada fames ac ante ipsum primis in faucibus. Cras eget lectus quis enim efficitur lobortis. Vestibulum pellentesque efficitur porttitor. Pellentesque finibus vel risus id ullamcorper. Aliquam elementum nec augue sed fringilla.\n"
                    + "Curabitur nisl lorem, malesuada sed blandit ac, faucibus quis ante. Curabitur id tincidunt quam, eu feugiat dui. In hac habitasse platea dictumst. Aliquam in nisl erat. Nunc tempor, augue in elementum fringilla, tortor mauris egestas sapien, at gravida sapien nibh sit amet nisi. Nullam congue ante sit amet nulla fringilla ultrices. Proin viverra, diam et consequat cursus, ipsum turpis placerat lectus, a auctor erat sapien vel dolor. Fusce quis consequat diam. Nunc vel convallis velit, et venenatis lacus. Etiam congue, eros vitae vehicula finibus, sem mi gravida eros, vitae pretium felis ligula eget arcu. Nunc commodo neque eu metus gravida, ac mollis massa eleifend. Vestibulum a mauris eget quam aliquet gravida. Etiam luctus erat ut ligula scelerisque, ut elementum est venenatis. Nulla rutrum venenatis dui vel venenatis.",
                ReferenceIds = {},
                AuthorIds = new Collection<int> {1, 2, 3},
                TopicIds = new Collection<int> {1}
            };
            JsonContent body = JsonContent.Create(request);
            var response = await client.PostAsync(BaseUrl, body);
            var actual = response.IsSuccessStatusCode;
            var expect = true;
            Assert.That(actual, Is.EqualTo(expect));
        }
        
        // [Test]
        // public async Task TestCreateArticleUsingFileSuccessfully() {
        //     var requestBody = new MultipartFormDataContent();
        //     requestBody.Add(new StreamContent(File.OpenRead("testArticle.txt")), "File");
        //     requestBody.Add(JsonContent.Create(new Collection<int> {1, 3}), "Authors");
        //     requestBody.Add(JsonContent.Create(new Collection<int> {1}), "Topics");
        //     HttpClient client = new HttpClient();
        //     await client.PostAsync(BaseUrl, requestBody);
        // }
    }
}