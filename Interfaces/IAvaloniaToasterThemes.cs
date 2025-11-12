using Avalonia.Layout;
using Avalonia.Media;

namespace AvaloniaToaster.Interfaces;

public interface IAvaloniaToasterThemes
{
    IBrush BackgroundColor { get; }
    IBrush ForegroundColor { get; }
    HorizontalAlignment? HorizontalAlignment { get; }
    VerticalAlignment? VerticalAlignment { get; }
    double? BorderRadius { get; }
}