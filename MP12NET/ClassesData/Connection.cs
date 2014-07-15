namespace MP22NET.DATA.ClassesData
{
    public static class Connection
    {
        private static DataEntities _connectionBdd;


        public static DataEntities ConnectionBdd
        {
            get
            {
                if (_connectionBdd == null)
                    _connectionBdd = new DataEntities();
                return _connectionBdd;
            }
            set { _connectionBdd = value; }
        }
    }
}
