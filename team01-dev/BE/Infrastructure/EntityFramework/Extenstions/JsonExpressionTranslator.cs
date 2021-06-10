using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.EntityFramework.Extensions
{
    public static class JsonExpressionTranslator
    {
        public static Expression Translate(IReadOnlyCollection<Expression> expressions)
        {
            var items = expressions.ToArray();
            return new JsonExpression(items[0], items[1]);
        }
    }
}
