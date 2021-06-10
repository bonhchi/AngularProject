using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.EntityFramework.Extensions
{
    public class JsonExpression : SqlExpression
    {
        public JsonExpression(Expression column, Expression path) : base(typeof(Expression), RelationalTypeMapping.NullMapping)
        {
            Column = column;
            Path = path;
        }

        public Expression Column { get; private set; }

        public Expression Path { get; private set; }

        protected override Expression Accept(ExpressionVisitor visitor)
        {
            if (visitor is SqlExpressionVisitor specificVisitor)
            {
                string sql = $"JSON_VALUE([{Column.ToString().Trim('"')}],";
                specificVisitor.Visit(new SqlFragmentExpression(sql));
                visitor.Visit(Path);
                sql = ")";
                return specificVisitor.Visit(new SqlFragmentExpression(sql));
            }
            else
                return base.Accept(visitor);
        }

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            return new JsonExpression(visitor.Visit(Column), visitor.Visit(Path));
        }

        public override Type Type => typeof(string);

        public override string ToString() => $"JSON_VALUE([{Column.ToString().Trim('"')}], '{Path.ToString().Trim('"')}')";

        protected override void Print([NotNullAttribute] ExpressionPrinter expressionPrinter)
        {
            throw new NotImplementedException();
        }
    }
}
