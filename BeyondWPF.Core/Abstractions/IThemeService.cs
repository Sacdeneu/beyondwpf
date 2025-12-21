using System;
using BeyondWPF.Core.Enums;

namespace BeyondWPF.Core.Abstractions
{
    public interface IThemeService
    {
        void ApplyTheme(SystemTheme theme);
        SystemTheme GetCurrentTheme();
        void ApplySystemAccent();
    }
}
