namespace API.Models
{
    ////////////////////////////
    /// @deprecated old design problem
    /// API gets StudentId, QuestionId, and OptionId ----> Compare if the QuestionId,OptionId exists in Answer table 
    /// ----> Add the score and record to AnswerAttemptTable ---->doesn't work, can't get attempts and compile answer attempts
    ////////////////////////////
    /// 
    ////////////////////////////
    public class TakeQuestion
    {
        public int Id { get; set; }
        public int TakeAssignmentId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public TakeAssignment TakeAssignment { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}