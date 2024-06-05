using API.Repositories;
using API.Services;
using Quartz;

namespace API.CronJob {
    public class PermanentDeletionJob : IJob
    {
        public static ArticleService ArticleService {get; set;}
        // private readonly ArticleService _articleService;

        // public PermanentDeletionJob(ArticleService articleService)
        // {
        //     _articleService = articleService;
        // }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Test scheduling");
            int id = context.JobDetail.JobDataMap.GetInt("id");
            await ArticleService.DeleteDraftArticlePermanent(id);
        }
    }
}