using Autofac;
using EmergencyCenter.Core.Contracts.Engine;
using EmergencyCenter.Program.InjectionConfig;

namespace EmergencyCenter.Program
{
    public class Program
    {
        public static void Main()
        {
            var containerConfig = new AutofacConfig();
            var container = containerConfig.Build();

            var engine = container.Resolve<IEngine>();

            engine.Run();
        }
    }
}
