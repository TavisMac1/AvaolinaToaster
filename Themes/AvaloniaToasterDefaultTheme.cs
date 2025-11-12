using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaToaster.Interfaces;

namespace AvaloniaToaster.Themes;

internal class AvaloniaToasterDefaultTheme : IAvaloniaToasterThemes
{
    public IBrush BackgroundColor => Avalonia.Media.Brushes.Black;

    public IBrush ForegroundColor => Avalonia.Media.Brushes.White;

    HorizontalAlignment? IAvaloniaToasterThemes.HorizontalAlignment => null;

    VerticalAlignment? IAvaloniaToasterThemes.VerticalAlignment => null;

    double? IAvaloniaToasterThemes.BorderRadius => null;
}