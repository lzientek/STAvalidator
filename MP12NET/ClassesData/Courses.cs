//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MP22NET.DATA.ClassesData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Courses
    {
        public Courses()
        {
            this.Questions = new HashSet<Questions>();
            this.Repartition = new HashSet<Repartition>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Questions> Questions { get; set; }
        public virtual ICollection<Repartition> Repartition { get; set; }
    }
}
