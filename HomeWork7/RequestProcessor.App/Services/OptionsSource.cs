using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RequestProcessor.App.Models;
using System.Linq;

namespace RequestProcessor.App.Services
{
    /// <inheritdoc/>
    internal class OptionsSource : IOptionsSource
    {
        string Path { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">Path to json file with source options.</param>
        public OptionsSource(string path)
        {
            Path = path;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync()
        {
            if (Path == null)
                throw new ArgumentNullException(nameof(Path), "Source path cannot be null.");

            var json = await File.ReadAllTextAsync(Path);
            var deserialisedOptions = JsonConvert.DeserializeObject<List<RequestOptions>>(json);
            var options = deserialisedOptions?.Select(opt => ((IRequestOptions)opt, (IResponseOptions)opt)).ToList();
            
            return options;
        }
    }
}
