using System;
using System.Collections.Generic;

namespace DesignPatterns.Creational.Builder;

public class SqlQueryBuilder
{
    private string _table = string.Empty;
    private readonly List<string> _selectColumns = new();
    private readonly List<string> _joins = new();
    private readonly List<string> _whereConditions = new();
    private readonly List<string> _groupByColumns = new();
    private string? _havingCondition;
    private string? _orderByColumn;
    private bool _isDescending;
    private int? _limit;
    private int? _offset;

    public SqlQueryBuilder From(string table)
    {
        _table = table;
        return this;
    }

    public SqlQueryBuilder Select(params string[] columns)
    {
        _selectColumns.AddRange(columns);
        return this;
    }

    public SqlQueryBuilder InnerJoin(string targetTable, string onCondition)
    {
        _joins.Add($"INNER JOIN {targetTable} ON {onCondition}");
        return this;
    }

    public SqlQueryBuilder LeftJoin(string targetTable, string onCondition)
    {
        _joins.Add($"LEFT JOIN {targetTable} ON {onCondition}");
        return this;
    }

    public SqlQueryBuilder Where(string condition)
    {
        _whereConditions.Add(condition);
        return this;
    }

    public SqlQueryBuilder GroupBy(params string[] columns)
    {
        _groupByColumns.AddRange(columns);
        return this;
    }

    public SqlQueryBuilder Having(string condition)
    {
        _havingCondition = condition;
        return this;
    }

    public SqlQueryBuilder OrderBy(string column, bool descending = false)
    {
        _orderByColumn = column;
        _isDescending = descending;
        return this;
    }

    public SqlQueryBuilder Paginate(int pageNumber, int pageSize)
    {
        _limit = pageSize;
        _offset = (pageNumber - 1) * pageSize;
        return this;
    }

    public SqlQuery Build()
    {
        if (string.IsNullOrWhiteSpace(_table))
            throw new InvalidOperationException("SQL Query cannot be built without specifying a target FROM table.");

        return new SqlQuery
        {
            Table = _table,
            SelectColumns = new List<string>(_selectColumns),
            Joins = new List<string>(_joins),
            WhereConditions = new List<string>(_whereConditions),
            GroupByColumns = new List<string>(_groupByColumns),
            HavingCondition = _havingCondition,
            OrderByColumn = _orderByColumn,
            IsDescending = _isDescending,
            Limit = _limit,
            Offset = _offset
        };
    }
}
