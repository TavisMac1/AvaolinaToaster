using Avalonia.Media;
using AvaloniaToaster.Interfaces;

namespace AvaloniaToaster.Themes;

internal class AvaloniaToasterDefaultTheme : IAvaloniaToasterThemes
{
    public IBrush BackgroundColor => Brushes.Black;

    public IBrush ForegroundColor => Brushes.White;
}