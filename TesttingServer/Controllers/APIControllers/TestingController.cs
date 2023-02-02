using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesttingServer.Models;
using TesttingServer.Models.VewModels;
using static System.Net.Mime.MediaTypeNames;

namespace TesttingServer.Controllers.APIControllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestingController
    {
        private readonly TesttingDbContext context;
        private readonly UserManager<User> userManager;
        public TestingController(TesttingDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTestsForUsers(string userId)
        {
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var tests = await context.Tests.Where(x => x.Type == user.UserRoleType).ToListAsync();
                var remailData = JsonConvert.SerializeObject(tests, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                return new OkObjectResult(remailData);
            }
            return new NotFoundResult();
        }
        [HttpGet("{testId}")]
        public async Task<IActionResult> GetQestions(int testId)
        {
            Test test = await context.Tests.FirstOrDefaultAsync(x => x.TestId == testId);
            if(test != null)
            {
                var result = Shuffle(test.Qestions);
                var remailData = JsonConvert.SerializeObject(result, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                return new OkObjectResult(remailData);
            }
            return new NotFoundResult();
        }
        private ICollection<Qestion> Shuffle(ICollection<Qestion> list)
        {
            Random rand = new Random();
            ICollection<Qestion> randomList = list.OrderBy(_ => rand.Next()).ToList();
            foreach(var qestion in randomList)
            {
                qestion.Answers = qestion.Answers.OrderBy(_ => rand.Next()).ToList();
            }
            return randomList;
        }

        [HttpPost]
        public async Task<IActionResult> CheckAnswers(AnswersViewModel model)
        {
            Test test = await context.Tests.FirstOrDefaultAsync(x => x.TestId == model.TestId);
            int correctAnswers = 0;
            if(test != null)
            {
                foreach (KeyValuePair<int, int> item in model.Answers)
                {
                    Qestion qestion = test.Qestions.FirstOrDefault(x => x.QestionId == item.Key);
                    Answer answer = qestion.Answers.FirstOrDefault(x => x.AnswerId == item.Value);
                    if (answer.IsTrue == true)
                    {
                        correctAnswers++;
                    }
                }
                return new OkObjectResult(correctAnswers);
            }
            return new NotFoundResult();
        }
    }
}
