using System;

namespace BaseTool.Tools.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentAttribute : Attribute { }

    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentsAttribute : Attribute { }

    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentInChildrenAttribute : Attribute { }

    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentsInChildrenAttribute : Attribute { }

    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentInParentAttribute : Attribute { }
}
