using System;
using System.Security.Authentication;
using Spectre.Console.Cli;

namespace Spectre.Console.Examples
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                DoMagic(42, null);
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine();
                AnsiConsole.Write(new Rule("Exception.ToString()").LeftAligned());
                AnsiConsole.WriteLine();
                AnsiConsole.WriteLine(ex.ToString());

                AnsiConsole.WriteLine();
                AnsiConsole.Write(new Rule("Default").LeftAligned());
                AnsiConsole.WriteLine();
                AnsiConsole.WriteException(ex);

                AnsiConsole.WriteLine();
                AnsiConsole.Write(new Rule("Compact").LeftAligned());
                AnsiConsole.WriteLine();
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks);

                AnsiConsole.WriteLine();
                AnsiConsole.Write(new Rule("Compact + Custom colors").LeftAligned());
                AnsiConsole.WriteLine();
                AnsiConsole.WriteException(ex, new ExceptionSettings
                {
                    Format = ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks,
                    Style = new ExceptionStyle
                    {
                        Exception = new Style().Foreground(Color.Grey),
                        Message = new Style().Foreground(Color.White),
                        NonEmphasized = new Style().Foreground(Color.Cornsilk1),
                        Parenthesis = new Style().Foreground(Color.Cornsilk1),
                        Method = new Style().Foreground(Color.Red),
                        ParameterName = new Style().Foreground(Color.Cornsilk1),
                        ParameterType = new Style().Foreground(Color.Red),
                        Path = new Style().Foreground(Color.Red),
                        LineNumber = new Style().Foreground(Color.Cornsilk1),
                    }
                });

                AnsiConsole.WriteLine();
                AnsiConsole.Write(new Rule("GetRenderableErrorMessage").LeftAligned());
                AnsiConsole.WriteLine();
                var pretty = CommandApp.GetRenderableErrorMessage(ex);
                foreach (var renderable in pretty)
                {
                    if (renderable != null)
                    {
                        AnsiConsole.Write(renderable);
                    }
                }
            }
        }

        private static void DoMagic(int foo, string[,] bar)
        {
            try
            {
                CheckCredentials(foo, bar);
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Whaaat?", ex);
            }
        }

        private static void CheckCredentials(int qux, string[,] corgi)
        {
            throw new InvalidCredentialException("The credentials are invalid.");
        }
    }
}
