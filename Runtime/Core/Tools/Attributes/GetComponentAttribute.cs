using System;

namespace BaseTool
{
    public abstract class ComponentRetrieverAttribute : Attribute { }

    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentAttribute : ComponentRetrieverAttribute { }

    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentsAttribute : ComponentRetrieverAttribute { }

    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentInChildrenAttribute : ComponentRetrieverAttribute { }

    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentsInChildrenAttribute : ComponentRetrieverAttribute { }

    [System.AttributeUsage(System.AttributeTargets.Field | AttributeTargets.Property)]
    public class GetComponentInParentAttribute : ComponentRetrieverAttribute { }
}
