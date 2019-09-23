using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.Framework
{
    public static class CommonExtention
    {
        public static IEnumerable<string> GetList<T, TKey>(this T item, Expression<Func<T, TKey>> selector, char separator = ';') where T : ITableModel where TKey : class
        {
            var expression = (MemberExpression)selector.Body;
            if (expression.Type != typeof(string))
            {
                throw new InvalidCastException($"Параметер {expression.Member.Name} не является строчкой");
            }
            var func = selector.Compile();

            var propertyValue = func.Invoke(item);

            return propertyValue.ToString().Split(separator).ToList();
        }
    }
}
