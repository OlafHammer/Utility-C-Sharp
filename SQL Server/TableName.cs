namespace Utility.SQL_Server;

public class TableName : IComparable<TableName?>
{
    internal TableName(string name)
    {
        Name = name;
    }

    private string Name { get; }

    public int CompareTo(TableName? obj)
    {
        if (obj == null) return -1;
        return obj.Name == Name ? 0 : 1;
    }

    public static implicit operator string(TableName d)
    {
        return d.Name;
    }

    public override string ToString()
    {
        return Name;
    }
}