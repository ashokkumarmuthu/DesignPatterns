using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Creational.Builder;

public class SqlQuery
{
    public string Table { get; init; } = string.Empty;
    public List<string> SelectColumns { get; init; } = new();
    public List<string> Joins { get; init; } = new();
    public List<string> WhereConditions { get; init; } = new();
    public List<string> GroupByColumns { get; init; } = new();
    public string? HavingCondition { get; init; }
    public string? OrderByColumn { get; init; }
    public bool IsDescending { get; init; }
    public int? Limit { get; init; }
    public int? Offset { get; init; }

    public string ToExecutableSql()
    {
        var sb = new StringBuilder();

        string cols = SelectColumns.Count > 0 ? string.Join(", ", SelectColumns) : "*";
        sb.AppendLine($"SELECT {cols}");
        sb.AppendLine($"FROM {Table}");

        foreach (var join in Joins)
            sb.AppendLine($"  {join}");

        if (WhereConditions.Count > 0)
            sb.AppendLine($"WHERE {string.Join(" AND ", WhereConditions)}");

        if (GroupByColumns.Count > 0)
            sb.AppendLine($"GROUP BY {string.Join(", ", GroupByColumns)}");

        if (!string.IsNullOrEmpty(HavingCondition))
            sb.AppendLine($"HAVING {HavingCondition}");

        if (!string.IsNullOrEmpty(OrderByColumn))
            sb.AppendLine($"ORDER BY {OrderByColumn} {(IsDescending ? "DESC" : "ASC")}");

        if (Limit.HasValue)
        {
            sb.Append($"LIMIT {Limit.Value}");
            if (Offset.HasValue) sb.Append($" OFFSET {Offset.Value}");
            sb.AppendLine(";");
        }

        return sb.ToString().TrimEnd();
    }
}
