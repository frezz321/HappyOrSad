using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HappyOrSad.Models
{
    public class QuestionResponse
    {
        public QuestionResponse() { }
        [Key]
        public int QuestionResponseID { get; set; }
        public int QuestionID { get; set; }
        public string UserId { get; set; }

        [Required]
        public int Score { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateSubmitted { get; set; }
        public virtual Question Question { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
    
}