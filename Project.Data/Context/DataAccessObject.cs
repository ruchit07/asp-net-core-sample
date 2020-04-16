namespace Project.Data.Context
{
    using System;

    public class DataAccessObject : IDisposable
    {
        private ProjectContext _context;

        public DataAccessObject()
        {
            _context = new ProjectContext();
        }

        protected ProjectContext Context
        {
            get { return _context; }
        }

        public bool CommitImmediately { get; set; }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}