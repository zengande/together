using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Together.Activity.BackgroundTasks.Tasks.Base
{
    public abstract class BackgroundTask
        : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public void Dispose()
        {
            _tokenSource.Cancel();
        }

        /// <summary>
        /// 在 <see cref="IHostedService"/> 启动时调用
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_tokenSource.Token);

            if (_executingTask.IsCanceled)
            {
                return _executingTask;
            }
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }
            try
            {
                _tokenSource.Cancel();
            }
            catch (Exception)
            {
            }
            finally
            {
                await Task.WhenAll(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }
    }
}
