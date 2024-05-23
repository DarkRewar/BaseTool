using System;

namespace BaseTool
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    //[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter | AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class ButtonAttribute : Attribute { }
}
