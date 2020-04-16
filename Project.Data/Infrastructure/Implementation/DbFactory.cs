namespace Project.Data.Infrastructure
{
    using Project.Data.Context;

    public class DbFactory : Disposable, IDbFactory
    {
        ProjectContext dbContext;

        public ProjectContext Init(){ 
            return dbContext ?? (dbContext = new ProjectContext());
        }

        protected override void DisposeCore(){
            if(dbContext != null)
                dbContext.Dispose();
        }
    }
}