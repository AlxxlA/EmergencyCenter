using Autofac;
using EmergencyCenter.Validation;

namespace EmergencyCenter.Program.InjectionConfig.Modules.Validation
{
    public class ValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Validator>().As<IValidator>().SingleInstance();
        }
    }
}
