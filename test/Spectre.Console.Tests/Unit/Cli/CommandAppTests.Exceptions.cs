using System;
using Shouldly;
using Spectre.Console.Cli;
using Spectre.Console.Tests.Data;
using Spectre.Console.Testing;
using Xunit;

namespace Spectre.Console.Tests.Unit.Cli
{
    public sealed partial class CommandAppTests
    {
        public sealed class Exception_Handling
        {
            [Fact]
            public void Should_Not_Propagate_Runtime_Exceptions_If_Not_Explicitly_Told_To_Do_So()
            {
                // Given
                var app = new CommandAppTester();
                app.Configure(config =>
                {
                    config.AddBranch<AnimalSettings>("animal", animal =>
                    {
                        animal.AddCommand<DogCommand>("dog");
                        animal.AddCommand<HorseCommand>("horse");
                    });
                });

                // When
                var result = app.Run(new[] { "animal", "4", "dog", "101", "--name", "Rufus" });

                // Then
                result.ExitCode.ShouldBe(-1);
            }

            [Fact]
            public void Should_Not_Propagate_Exceptions_If_Not_Explicitly_Told_To_Do_So()
            {
                // Given
                var app = new CommandAppTester();
                app.Configure(config =>
                {
                    config.AddCommand<ThrowingCommand>("throw");
                });

                // When
                var result = app.Run(new[] { "throw" });

                // Then
                result.ExitCode.ShouldBe(-1);
            }
            
            [Fact]
            public void Should_Handle_Exceptions_If_ExceptionHandler_Is_Set_Using_Action()
            {
                // Given
                var exceptionHandled = false;
                var app = new CommandAppTester();
                app.Configure(config =>
                {
                    config.AddCommand<ThrowingCommand>("throw");
                    config.SetExceptionHandler(_ =>
                    {
                        exceptionHandled = true;
                    });
                });

                // When
                var result = app.Run(new[] { "throw" });

                // Then
                result.ExitCode.ShouldBe(-1);
                exceptionHandled.ShouldBeTrue();
            }
            
            [Fact]
            public void Should_Handle_Exceptions_If_ExceptionHandler_Is_Set_Using_Function()
            {
                // Given
                var exceptionHandled = false;
                var app = new CommandAppTester();
                app.Configure(config =>
                {
                    config.AddCommand<ThrowingCommand>("throw");
                    config.SetExceptionHandler(_ =>
                    {
                        exceptionHandled = true;
                        return -99;
                    });
                });

                // When
                var result = app.Run(new[] { "throw" });

                // Then
                result.ExitCode.ShouldBe(-99);
                exceptionHandled.ShouldBeTrue();
            }
        }
    }
}
