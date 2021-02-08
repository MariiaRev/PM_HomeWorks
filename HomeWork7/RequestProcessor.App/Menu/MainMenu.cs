using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using RequestProcessor.App.Models;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Services;
using RequestProcessor.App.Exceptions;

namespace RequestProcessor.App.Menu
{
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        private readonly IRequestPerformer _performer;
        private readonly IOptionsSource _options;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>
        public MainMenu(
            IRequestPerformer performer, 
            IOptionsSource options, 
            ILogger logger)
        {
            _performer = performer;
            _options = options;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<int> StartAsync()
        {
            try
            {
                var options = await GetValidOptionsAsync();

                if (options == null || options.Count() == 0)
                {
                    Inform("\n\nThere are no valid options.\n\n\n");
                    return 0;
                }

                Inform("\n\nOptions are received.");

                var tasks = options.Select(opt => _performer.PerformRequestAsync(opt.Item1, opt.Item2)).ToArray();
                Inform("\n\nRequests are sent.");

                var results = await Task.WhenAll(tasks);
                Inform("\n\nResults are received.\n\n\n");

                return 0;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File with options was not found.\n\n\n");
                return 0;
            }
            catch (PerformException e)
            {
                Inform("\n\nPerform exception occured.\n\n\n", e);
                return -1;
            }

        }

        private async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetValidOptionsAsync()
        {
            var allOptions = await _options.GetOptionsAsync();
            var validOptions = allOptions?.Where(opt => opt.Item1.IsValid && opt.Item2.IsValid).ToList();

            return validOptions;
        }

        private void Inform(string message, Exception exception = null)
        {
            Console.WriteLine(message);

            if (exception == null)
            {
                _logger.Log(message);
            }
            else
            {
                Console.WriteLine($"{exception.InnerException?.Message}");
                _logger.Log(exception, message);
            }
        }

    }
}
