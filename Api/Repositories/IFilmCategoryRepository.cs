using Models;
using Microsoft.Extensions.Caching.Memory;
using Database;

namespace Repositories;

public class FilmCategoryRepository : DatabaseItemRepository<FilmCategory>
{
    public FilmCategoryRepository(IDatabaseContext dbController, MemoryCacheOptions options) : base(dbController, options)
    {
    }

    //Check whether the cache already has a FilmCategory with a matching name, if it does return that. Otherwise go to the database to find one with a matching name.
    public FilmCategory GetByName(string name)
    {
        if (_cache.TryGetValue(name, out FilmCategory result) && result.Name == name)
            return result;

        return _cache.GetOrCreate(name, cacheEntry => {
            cacheEntry.Size = 1;
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

            var result = _queryable.FirstOrDefault(x => x.Name == name);

            if (result == null)
            {
                throw new KeyNotFoundException($"Failed to find {typeof(FilmCategory)} with id: {name}");
            }

            return result;
        });
    }
}
