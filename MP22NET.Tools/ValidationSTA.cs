using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MP22NET.DATA.ClassesData;
using MP22NET.Tools;

namespace MP22NET.DATA.ClassesMetier
{
    public class ValidationSTA
    {
        private List<Scores> listNote;
        public List<ScoredQuestion> Questions { get; private set; }
        public Teacher Teacher { get; private set; }

        public Mail Mail { get; set; }
        public String ResultatQuestion
        {
            get
            {
                return string.Format("A: {0} \tB: {1} \tC: {2}", Questions.Count(s => s.Score == Scores.A),
                    Questions.Count(s => s.Score == Scores.B), Questions.Count(s => s.Score == Scores.C));
            }
        }

        public ObservableCollection<string> CampusList { get; set; }
        public bool IsValid { get; set; }
        public Courses Courses { get; set; }
        public Scores ScoreExperiencePro { get; set; }
        public Scores ScoreCertif { get; private set; }
        public Scores ScoreEcts { get; set; }
        public int NoteEcts { get; set; }
        /// <summary>
        /// Objet de validation
        /// </summary>
        /// <param name="t">Teacher a faire valider</param>
        public ValidationSTA(Teacher t)
        {
            CampusList = new ObservableCollection<string>();
            IsValid = false;
            Teacher = t;
            Questions = new List<ScoredQuestion>();
            CalculScoreCertification();
            NoteEcts = 10;
        }


        /// <summary>
        /// On donne la note de certification
        /// </summary>
        private void CalculScoreCertification()
        {
            if (Teacher.Certification.Count > 0)
            {
                foreach (var certification in Teacher.Certification)
                {
                    if (certification.Note == "A")
                    {
                        ScoreCertif = Scores.A;
                        break; //On peut pas avoir mieux
                    }
                    ScoreCertif = Scores.B;
                }
            }
            else
            {
                ScoreCertif = Scores.C;
            }
        }

        /// <summary>
        /// On verifie la note eu a la matiere auparavant
        /// </summary>
        public void CalculScoreEcts()
        {
            if (NoteEcts < 10)
                ScoreEcts = Scores.C;
            else if (NoteEcts < 15)
                ScoreEcts = Scores.B;
            else
                ScoreEcts = Scores.A;
        }



        public void CheckFinalScore()
        {
            listNote = new List<Scores> { ScoreEcts, ScoreCertif, ScoreExperiencePro };
            listNote.AddRange(CalculateQuestionNoteList());

            if (!listNote.Contains(Scores.C) && listNote.Contains(Scores.A))
                IsValid = true; //si il n'y a pas de C et au moins un A on valide
            if (listNote.Count(n => n == Scores.C) == 1 && listNote.Count(n => n == Scores.A) >= 2)
                IsValid = true; //si il a un C mais au moins 2 A on valide

            //si il valide,creation du mail
            if (IsValid)
                Mail = new Mail(Teacher, Courses);

        }



        /// <summary>
        /// Calcule les différence de note entre les question bonus et importante
        /// </summary>
        /// <returns>une liste de note</returns>
        private IEnumerable<Scores> CalculateQuestionNoteList()
        {
            var list = new List<Scores>();

            foreach (var question in Questions)
                if (question.Question.Type == 0) //si bonus
                {
                    if (question.Score == Scores.A)
                        list.Add(Scores.A);
                    if (question.Score != Scores.C) //si c'est B on ajoute un A et si c'est A on en ajoute bien 2
                        list.Add(Scores.A);
                }
                else //si important
                {
                    if (question.Score == Scores.C)
                        list.AddRange(new[] { Scores.C, Scores.C }); //si on a un C il compte double
                    else
                        list.Add(question.Score);
                }

            return list;
        }

    }
}
