using Avalonia.Controls;
using System;
using System.Threading.Tasks;

// Author: Tavis MacFarlane
// Copyright (c) 2025 Tavis MacFarlane
// License: MIT

namespace AvaloniaToaster;

public class ToastNotificationService
{
    private Window? _mainWindow;

    public void RegisterMainWindow(Window window) => _mainWindow = window;

    /// <summary>
    /// Displays a toast notification overlay with the specified message on the registered main window.
    /// The notification will automatically disappear after the specified duration.
    /// </summary>
    /// <param name="message">The text to display in the toast notification.</param>
    /// <param name="durationMs">The duration in milliseconds the toast will be visible (default is 3000ms).</param>
    /// <exception cref="InvalidOperationException">Thrown if the main window has not been registered.</exception>
    public void Show(string message, int durationMs = 3000)
    {
        if (_mainWindow == null) throw new InvalidOperationException("Main window not registered.");

        // Create a simple overlay (could be a UserControl, Border, etc.)
        var toast = new Border
        {
            Background = Avalonia.Media.Brushes.Black,
            CornerRadius = new Avalonia.CornerRadius(8),
            Padding = new Avalonia.Thickness(16),
            Child = new TextBlock
            {
                Text = message,
                Foreground = Avalonia.Media.Brushes.White,
                FontSize = 16
            },
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Bottom,
            Margin = new Avalonia.Thickness(0, 40, 0, 0),
            ZIndex = 9999
        };

        if (_mainWindow.Content is Panel panel)
        {
            panel.Children.Add(toast);

            // Remove after duration
            Task.Delay(durationMs).ContinueWith(_ =>
            {
                Avalonia.Threading.Dispatcher.UIThread.Post(() => panel.Children.Remove(toast));
            });
        }
    }
}
