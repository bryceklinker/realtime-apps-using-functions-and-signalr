using System;
using FluentAssertions;
using FluentAssertions.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace Realtime.Presenter.Function.Tests.Common
{
    public static class ActionResultExtensions
    {
        public static ActionResultAssertions Should(this IActionResult result)
        {
            return new ActionResultAssertions(result);
        }
    }
    
    public class ActionResultAssertions : ReferenceTypeAssertions<IActionResult, ActionResultAssertions>
    {
        protected override string Identifier { get; } = typeof(IActionResult).Name;

        public ActionResultAssertions(IActionResult subject)
        {
            Subject = subject;
        }

        public AndConstraint<ActionResultAssertions> BeOk()
        {
            Subject.Should().Match(r => r.GetType() == typeof(OkResult) || r.GetType() == typeof(OkObjectResult));
            return new AndConstraint<ActionResultAssertions>(this);
        }

        public AndConstraint<ActionResultAssertions> HaveContent<T>(Action<T> assertion)
        {
            var objectResult = (OkObjectResult) Subject;
            var content = (T) objectResult.Value;
            assertion(content);
            return new AndConstraint<ActionResultAssertions>(this);
        }
    }
}