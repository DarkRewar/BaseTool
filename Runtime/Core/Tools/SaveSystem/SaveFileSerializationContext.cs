using System.Collections.Generic;

namespace BaseTool
{
    public class SaveFileSerializationContext
    {
        public readonly Dictionary<string, object> Values;
        
        public SaveFileSerializationContext() => Values = new Dictionary<string, object>();
        
        public SaveFileSerializationContext(Dictionary<string, object> values) => Values = values;
    }
}