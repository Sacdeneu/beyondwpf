using System;

namespace BeyondWPF.Common.Models;

/// <summary>
/// Represents a single log entry in the application.
/// </summary>
public record LogEntry(DateTime Timestamp, string Message, LogLevel Level);
