using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserRepositoryTests
{
    public class DbContextTester<T> where T : DbContext
    {
        private readonly DbContextOptions<T> _options;

        private T CreateContextInstance()
            => (T) Activator.CreateInstance(typeof(T), _options);

        public DbContextTester(DbContextOptions<T> options)
        {
            _options = options;
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> usersToInsert) where TEntity : class
            => Using(CreateContextInstance(), context =>
            {
                context.AddRange(usersToInsert);
                context.SaveChanges();
            });

        public void Assert(Action<T> assert)
            => Using(CreateContextInstance(), assert);

        private void Using<TDisposable>(TDisposable disposable, Action<TDisposable> action)
            where TDisposable : IDisposable
        {
            using (disposable)
            {
                action(disposable);
            }
        }
    }
}