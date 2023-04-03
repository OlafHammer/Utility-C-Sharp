using Utility.Base.Abstraction;
using Utility.Base.Attributes.Classes;

namespace Utility.SQL_Server;

public abstract class TableNames
{
    [HideProperty] public int Length => PropertyManager.PropertyCount(this);

    [HideProperty]
    public List<string> AllElements => PropertyManager.PropertyValues<TableName>(this).Select(e => (string)e).ToList();

    public static TableName NewTable(string name)
    {
        return new TableName(name);
    }
}