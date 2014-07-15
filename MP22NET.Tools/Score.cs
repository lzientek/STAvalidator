using System.Collections.Generic;
using MP22NET.DATA.ClassesData;

namespace MP22NET.DATA.ClassesMetier
{
    public static class Score
    {

        public static Scores StringToScores(string score)
        {
            if(score=="A")
                return Scores.A;
            if(score=="B")
                return Scores.B;
            if(score=="C")
                return Scores.C;
            throw new KeyNotFoundException("Valeur d'entrée non valide, doit etre A,B ou C");
        }

        public static string ScoreToString(Scores score)
        {
            if (score == Scores.A)
                return "A";
            if (score == Scores.B)
                return "B";
            if (score == Scores.C)
                return "C";
            throw new KeyNotFoundException("Valeur d'entrée non valide, doit etre A,B ou C");
        }
    }

    /// <summary>
    /// Question mis en relatin avec son score
    /// </summary>
    public class ScoredQuestion
    {
        public ScoredQuestion(Questions question)
        {
            Question = question;
        }
        public Scores Score { get; set; }

        public Questions Question { get; set; }

    }

    public enum Scores
    {
        A,B,C
    }
}
