using System;

public static class Header
{
    public static void Show(string title)
    {
        Console.Clear();
        Console.WriteLine("┌ " + new string('─', 39) + "┐");
        Console.WriteLine("|" + CenterText("DOTNET Hospital Management System", 40) + "|");
        Console.WriteLine("|" + new string('-', 40) + "|");
        Console.WriteLine("|" + CenterText(title, 40) + "|");
        Console.WriteLine("└" + new string('─', 40) + "┘");
    }

    // Helper method to add padding for centering text
    private static string CenterText(string text, int width)
    {
        if (text.Length >= width) return text.Substring(0, width);
        int leftPadding = (width - text.Length) / 2;
        int rightPadding = width - text.Length - leftPadding;
        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }

    public static void ResizeWindow(int width, int height)
    {
        Console.SetWindowSize(width, height);
    }
}
