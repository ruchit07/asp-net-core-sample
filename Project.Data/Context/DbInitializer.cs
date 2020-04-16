namespace Project.Data.Context
{
    public static class DbInitializer
    {
        public static void Initialize(ProjectContext context)
        {
            var exists = new DatabaseChecker().DatabaseExists(context);

            if (exists == DatabaseExistenceState.Exists)
            {
                try
                {
                    context.Database.EnsureCreated();
                    context.SaveChanges();
                }
                catch
                {

                }
            }
            else
            {
                context.Database.EnsureCreated();
            }
        }
    }
}