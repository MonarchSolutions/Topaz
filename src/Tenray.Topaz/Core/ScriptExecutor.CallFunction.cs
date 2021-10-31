﻿using Esprima.Ast;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tenray.Topaz.ErrorHandling;
using Tenray.Topaz.Interop;
using Tenray.Topaz.Utility;

namespace Tenray.Topaz.Core
{
    internal partial class ScriptExecutor
    {
        internal object CallFunction(object callee, IReadOnlyList<object> args, bool optional)
        {
            var value = GetValue(callee);
            if (value == null)
            {
                if (optional)
                    return GetNullOrUndefined();
                Exceptions.ThrowFunctionIsNotDefined(callee, this);
            }

            if (value is TopazFunction topazFunction)
            {
                return topazFunction.ScriptExecutor.GetValue(topazFunction.Execute(args));
            }

            if (value is IInvokable invokable)
            {
                return invokable.Invoke(args);
            }

            return TopazEngine.DelegateInvoker.Invoke(value, args);
        }

        internal async ValueTask<object> CallFunctionAsync(object callee, IReadOnlyList<object> args, bool optional)
        {
            var value = GetValue(callee);
            if (value == null)
            {
                if (optional)
                    return GetNullOrUndefined();
                Exceptions.ThrowFunctionIsNotDefined(callee, this);
            }

            if (value is TopazFunction topazFunction)
            {
                return topazFunction.ScriptExecutor.GetValue(
                    await topazFunction.ExecuteAsync(args));
            }

            if (value is IInvokable invokable)
            {
                return invokable.Invoke(args);
            }

            return TopazEngine.DelegateInvoker.Invoke(value, args);
        }

        internal object GetNullOrUndefined()
        {
            return Options.NoUndefined ? null : Undefined.Value;
        }
    }
}