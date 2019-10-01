using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HappyOrSad.Models
{
    public class Question : IComparable<Question>
    {
        public Question() { }
        [Key]
        public int QuestionID { get; set; }
        [Required]
        [DisplayName("Question Content")]
        public string Text { get; set; }
        [DisplayName("Date Time")]
        [DataType(DataType.DateTime)]
        public DateTime? DateDisplay { get; set; }
        public int? NextQuestionID { get; set; }
        public int? PreviousQuestionID { get; set; }

        public int CompareTo(Question question)
        {
            return this.DateDisplay.Value.ToLocalTime().CompareTo(question.DateDisplay.Value.ToLocalTime());
        }
    }
}