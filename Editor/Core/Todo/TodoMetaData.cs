using System.Collections.Generic;
using System.Data.SqlTypes;

namespace BaseTool.Editor.Todo
{
    public struct TodoMetaData : INullable
    {
        public readonly List<string> Names;
        public readonly List<string> Tags;

        public TodoMetaData(List<string> names, List<string> tags)
        {
            Names = names;
            Tags = tags;
        }

        public readonly bool IsNull => (Names == null || Names.Count == 0) && (Tags == null || Tags.Count == 0);
    }
}
