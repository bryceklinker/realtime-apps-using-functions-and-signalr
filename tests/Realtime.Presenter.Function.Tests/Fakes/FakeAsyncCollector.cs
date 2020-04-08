using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Realtime.Presenter.Function.Tests.Fakes
{
    public class FakeAsyncCollector<T> : IAsyncCollector<T>
    {
        private readonly List<T> _items = new List<T>();

        public IEnumerable<T> AddedItems => _items.AsEnumerable();
        
        public Task AddAsync(T item, CancellationToken cancellationToken = new CancellationToken())
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task FlushAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }
    }
}