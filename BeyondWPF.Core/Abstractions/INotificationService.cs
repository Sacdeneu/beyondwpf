/// <summary>
/// Specifies the type of notification.
/// </summary>
public enum NotificationType
{
    /// <summary> Information notification. </summary>
    Info,
    /// <summary> Success notification. </summary>
    Success,
    /// <summary> Warning notification. </summary>
    Warning,
    /// <summary> Error notification. </summary>
    Error
}

/// <summary>
/// Interface for a service that manages native Windows toast notifications.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Shows a native toast notification.
    /// </summary>
    /// <param name="title">The title of the notification.</param>
    /// <param name="message">The message content.</param>
    /// <param name="type">The type of notification (influences the branding/look if supported).</param>
    void Show(string title, string message, NotificationType type = NotificationType.Info);
}
