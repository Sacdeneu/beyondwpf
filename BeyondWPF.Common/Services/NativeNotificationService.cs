using System.Diagnostics;
using Microsoft.Toolkit.Uwp.Notifications;
using BeyondWPF.Core.Abstractions;

namespace BeyondWPF.Common.Services;

/// <summary>
/// Service that implements native Windows toast notifications using Community Toolkit.
/// </summary>
public class NativeNotificationService : INotificationService
{
    private bool _isRegistered;

    /// <inheritdoc />
    public void Show(string title, string message, NotificationType type = NotificationType.Info)
    {
        try
        {
            // The toolkit handles the registration (shortcut creation) automatically
            // when we call ToastContentBuilder.Show().
            // However, it's good practice to ensure it's initialized if needed.
            
            new ToastContentBuilder()
                .AddText(title)
                .AddText(message)
                .AddAttributionText(type.ToString())
                .Show();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[BeyondWPF] Error showing toolkit toast: {ex.Message}");
        }
    }
}
