using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Machine.Specifications;
using NSubstitute;

namespace StockTracker.Specifications
{
    public static class OrderedContextExtensions
    {
        public static void CallsShouldBeLike(this OrderedContext ctx, Action expected)
        {
            ctx.Playback(expected);
            ctx.Verify();
        }

        public static void CallsShouldBeLike(this OrderedContext ctx, Action expected, Func<string, bool> predicate)
        {
            ctx.Playback(expected);
            ctx.ActualCalls = ctx.ActualCalls.Where(predicate).ToList();
            ctx.Verify();
        }
    }

    public class OrderedContext
    {
        private readonly List<string> _temp = new List<string>();
        private bool _played = false;

        public List<string> ActualCalls { get; set; }
        public List<string> ExpectedCalls { get; set; } 

        public OrderedContext()
        {
            ActualCalls = new List<string>();
            ExpectedCalls = new List<string>();
        }

        public void Verify()
        {
            if(!_played)
                throw new InvalidOperationException("This context has not been played back yet.");

            if (ActualCalls.Count == 0 && ExpectedCalls.Count == 0)
                throw new InvalidOperationException("There were no recorded or expected calls received.");

            string.Join(Environment.NewLine, ActualCalls).ShouldEqual(string.Join(Environment.NewLine, ExpectedCalls));
        }

        public void Playback(Action expected)
        {
            if(_played)
                return;

            ActualCalls = _temp.Select(x => x).ToList();
            _temp.Clear();
            expected();
            ExpectedCalls = _temp.Select(x => x).ToList();

            _played = true;
        }

        public void Record<T>(T target)
            where T : class
        {
            if(_played)
                throw new InvalidOperationException("This context has already been verified, you cannot record any more calls.");

            var methods = typeof(T).GetMethods().ToList();


            var queue = new Queue<Type>();
            queue.Enqueue(typeof(T));
            var set = new HashSet<Type>();

            while(queue.Count > 0)
            {
                var type = queue.Dequeue();
                methods.AddRange(type.GetMethods());

                var interfaces = type.GetInterfaces();

                foreach(var i in interfaces)
                {
                    if(set.Contains(i)) return;
                    queue.Enqueue(i);
                    set.Add(i);
                }
            }

            foreach (var method in methods)
            {
                var parameters = method.GetParameters();
                var paramList = parameters.Select(param => Expression.Parameter(param.ParameterType)).ToList();
                var expression = Expression.Call(Expression.Constant(target), method, paramList);
                var compiledExpression = Expression.Lambda(expression, paramList).Compile();
                var defaultParams = parameters.Select(x => x.ParameterType.IsValueType ? Activator.CreateInstance(x.ParameterType) : null).ToArray();
                var action = new Action<T>(x => compiledExpression.DynamicInvoke(defaultParams));

                target.WhenForAnyArgs(action).Do(x =>
                {
                    _temp.Add(string.Format("{0}.{1}({2})",
                        typeof(T).Name,
                        method.Name,
                        string.Join(",", x.Args().Select(y => (y != null) ? y.ToString().Replace("\r", @"\r") : "null"))));
                });
            }
        }
    }
}
