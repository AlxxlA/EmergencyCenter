using Autofac;
using EmergencyCenter.InputOutput;
using EmergencyCenter.InputOutput.Contracts;

namespace EmergencyCenter.Program.InjectionConfig.Modules.IO
{
    public class InputOutputModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileReader>().As<IReader>()
                .WithParameter(new TypedParameter(typeof(string), @"...\...\...\input.txt")).SingleInstance();

            builder.RegisterType<ConsoleWriter>().As<IWriter>().SingleInstance();

            builder.RegisterType<FileReader>().As<IFileReader>()
                .WithParameter(new TypedParameter(typeof(string), @"...\...\...\Map.txt")).SingleInstance();
        }
    }
}
