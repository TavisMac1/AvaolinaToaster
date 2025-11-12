using Avalonia.Controls;
using Avalonia.Threading;
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
    private Window? _mainWindow;

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
        IAvaloniaToasterThemes theme = null
    )
    {
        if (_mainWindow == null)
            throw new InvalidOperationException("Main window not registered.");

        if (theme is null) theme = _defaultTheme;

        var toast = new Border
        {
            Background = theme.BackgroundColor,
            CornerRadius = theme.BorderRadius is not null ? new Avalonia.CornerRadius((double)theme.BorderRadius!) : new Avalonia.CornerRadius(5),
            Padding = new Avalonia.Thickness(20),
            Width = 150,
            Opacity = 0,
            Child = new TextBlock
            {
                Text = message,
                Foreground = theme.ForegroundColor,
                FontSize = 14,
                TextAlignment = Avalonia.Media.TextAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
            },
            HorizontalAlignment = theme.HorizontalAlignment ?? Avalonia.Layout.HorizontalAlignment.Right,
            VerticalAlignment = theme.VerticalAlignment ?? Avalonia.Layout.VerticalAlignment.Bottom,
            Margin = new Avalonia.Thickness(0, 40, 0, 0),
            ZIndex = 9999
        };

        if (_mainWindow.Content is Panel panel)
        {
            panel.Children.Add(toast);

            Dispatcher.UIThread.Post(async () =>
            {
                for (double i = 0; i <= 1; i += 0.1)
                {
                    toast.Opacity = i;
                    await Task.Delay(20);
                }
            });

            Task.Run(async () =>
            {
                await Task.Delay(durationMs);

                for (double i = 1; i >= 0; i -= 0.1)
                {
                    Dispatcher.UIThread.Post(() => toast.Opacity = i);
                    await Task.Delay(20);
                }

                Dispatcher.UIThread.Post(() => panel.Children.Remove(toast));
            });
        }
    }
}