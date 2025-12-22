using System;
using BeyondWPF.Core.Enums;

namespace BeyondWPF.Core.Abstractions
{
    public interface IThemeService
    {
        void ApplyTheme(SystemTheme theme);
        SystemTheme GetCurrentTheme();
        /// <summary>
        /// Applies the system accent color to the application resources.
        /// </summary>
        /// <param name="isEnabled">If true, uses system/blue accent. If false, uses neutral gray.</param>
        void ApplySystemAccent(bool isEnabled);
    }
}
