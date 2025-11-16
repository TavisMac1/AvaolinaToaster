using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace AvaloniaToaster.Services.Extensions;

public static class VisualTreeHelperExtensions
{
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public static Grid? FindRootGrid(this Window window)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
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

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public static Grid? FindRootGrid(this Control control)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
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
