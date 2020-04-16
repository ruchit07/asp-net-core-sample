namespace Project.Data.Infrastructure
{
    using System;
    using Project.Data.Context;

    public interface IDbFactory : IDisposable
    {
        ProjectContext Init();
    }
}