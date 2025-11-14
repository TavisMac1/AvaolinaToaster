using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace AvaloniaToaster.Services.Extensions;

public static class VisualTreeHelperExtensions
{
    public static Grid? FindRootGrid(this Window window)
    {
        foreach (var child in window.GetVisualChildren())
        {
            if (child is Grid grid) return grid;

            if (child is Visual visualChild)
            {
                var result = (visualChild as Window)?.FindRootGrid()
                          ?? (visualChild as Control)?.FindRootGrid();
                if (result is not null) return result;
            }
        }
        return null;
    }

    public static Grid? FindRootGrid(this Control control)
    {
        foreach (var child in control.GetVisualChildren())
        {
            if (child is Grid grid) return grid;

            if (child is Visual visualChild)
            {
                var result = (visualChild as Window)?.FindRootGrid()
                          ?? (visualChild as Control)?.FindRootGrid();
                if (result is not null) return result;
            }
        }
        return null;
    }
}
