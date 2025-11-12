using Avalonia.Controls;
using AvaloniaToaster.Interfaces;
using AvaloniaToaster.Themes;
using System;
using System.Threading.Tasks;

// Author: Tavis MacFarlane
// Copyright (c) 2025 Tavis MacFarlane
// License: MIT

namespace AvaloniaToaster;

public class ToastNotificationService
{
    private Window _mainWindow;

    private IAvaloniaToasterThemes _defaultTheme = new AvaloniaToasterDefaultTheme();
    
    public void RegisterMainWindow(Window window) => _mainWindow = window;

    /// <summary>
    /// Displays a toast notification overlay with the specified message on the registered main window.
    /// The notification will automatically disappear after the specified duration.
    /// </summary>
    /// <param name="message">The text to display in the toast notification.</param>
    /// <param name="durationMs">The duration in milliseconds the toast will be visible (default is 3000ms).</param>
    /// <param name="theme">The theme for the toast to implement. Must implement IAvaloniaToasterThemes</param>
    /// <exception cref="InvalidOperationException">Thrown if the main window has not been registered.</exception>
    public void Show
    (
        string message,
        int durationMs = 3000,
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        IAvaloniaToasterThemes theme = null
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    )
    {
        if (_mainWindow == null)
            throw new InvalidOperationException("Main window not registered.");

        if (theme is null) theme = _defaultTheme;

        // Create a simple overlay (could be a UserControl, Border, etc.)
        var toast = new Border
        {
            Background = theme.BackgroundColor,
            CornerRadius = new Avalonia.CornerRadius(5),
            Padding = new Avalonia.Thickness(20),
            Child = new TextBlock
            {
                Text = message,
                Foreground = theme.ForegroundColor,
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