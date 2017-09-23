namespace Sql_Business_Functions.Business.Models
{
    public class ActionLink
    {
        private SelectAction _select;
        private UpdateAction _update;
        private InsertAction _insert;
        public ActionType Type { get; set; }

        public SelectAction Select
        {
            get { return _select ?? (_select = new SelectAction()); }
            set { _select = value; }
        }

        public UpdateAction Update
        {
            get { return _update ?? (_update = new UpdateAction()); }
            set { _update = value; }
        }

        public InsertAction Insert
        {
            get { return _insert ?? (_insert = new InsertAction()); }
            set { _insert = value; }
        }
    }
}