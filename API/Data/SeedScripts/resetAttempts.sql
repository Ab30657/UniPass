delete from TakeAssignmentPIScores where TakeAssignmentId>0;
delete from TakeAssignments where Id>0;
delete from TakeQuestions where Id>0;
update Takes set Grade = 0 where Id>0;
delete from TakesCoursePIs where TakesId>0;
