using System;
using System.Windows;

namespace BeyondWPF.Common.Resources
{
    /// <summary>
    /// A specialized <see cref="ResourceDictionary"/> for managing control-specific resources.
    /// </summary>
    public class ControlResourceDictionary : ResourceDictionary
    {
        private const string DictionaryUri = "pack://application:,,,/BeyondWPF.Common;component/Resources/BeyondWPF.Common.xaml";

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlResourceDictionary"/> class.
        /// </summary>
        public ControlResourceDictionary()
        {
            Source = new Uri(DictionaryUri);
        }

        /// <summary>
        /// Adds a resource to the dictionary.
        /// </summary>
        /// <param name="key">The key of the resource.</param>
        /// <param name="value">The value of the resource.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> or <paramref name="value"/> is null.</exception>
        public void AddResource(string key, object value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));
            this[key] = value;
        }

        /// <summary>
        /// Gets a resource from the dictionary.
        /// </summary>
        /// <param name="key">The key of the resource.</param>
        /// <returns>The resource if found; otherwise, null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> is null.</exception>
        public object? GetResource(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            return Contains(key) ? this[key] : null;
        }

        /// <summary>
        /// Removes a resource from the dictionary.
        /// </summary>
        /// <param name="key">The key of the resource.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> is null.</exception>
        public void RemoveResource(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (Contains(key))
            {
                Remove(key);
            }
        }
    }
}
