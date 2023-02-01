using System.Collections.Generic;

namespace TesttingServer.Models.VewModels
{
    public class AnswersViewModel
    {
         public HashSet<Answer> Answers { get; set; }
    }
    public class Answer 
    { 
        public int QestionId { get; set; }
        public string AnswerName { get; set; }
    }
}
