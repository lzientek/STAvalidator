
using MP22NET.DATA.ClassesData;
using Exception = System.Exception;

namespace MP22NET.Tools
{
    public class Mail
    {
        private Microsoft.Office.Interop.Outlook.MailItem eMail;

        public string Text
        {
            get { return eMail.Body; }
            set { eMail.Body = value; }
        }

        private Microsoft.Office.Interop.Outlook.Application outlookApplication;
        public Teacher Teacher { get; set; }

        public Courses Cours { get; set; }

        public Mail(Teacher teacher,Courses cours)
        {
            Teacher = teacher;
            Cours = cours;
             outlookApplication = new Microsoft.Office.Interop.Outlook.Application();
                eMail = (Microsoft.Office.Interop.Outlook.MailItem) outlookApplication.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                eMail.Body = string.Format("Bonjour {0} {1}, vous venez de valider la matiere {2}!!{3}"
                        , Teacher.Firstname, Teacher.Name, Cours.Name
                        , eMail.Body); //Permet d'avoir la signature par default
                eMail.Subject = string.Format("[{0}]:Validation obtained", Cours.Id);
        }

        /// <summary>
        /// Fonctionne uniquement avec outlook installer sur la machine
        /// </summary>
        public void SendMail()
        {
            try
            {
                

                

                var envoyerA = eMail.Recipients;
                var contact = envoyerA.Add(Teacher.Email);
                contact.Resolve();
                eMail.Send();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
