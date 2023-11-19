using DevFramework.Core.CrossCuttingConcerns.Caching;
using DevFramework.Core.CrossCuttingConcerns.Logging;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.Postsharp
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
    public class LogAspect : OnMethodBoundaryAspect
    {
        private readonly Type _loggerType;
        private LoggerService _loggerService;

        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType != typeof(LoggerService))
            {
                throw new InvalidCastException("Invalid logger service type");
            }
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);
            base.RuntimeInitialize(method);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            try
            {
                if (_loggerService.IsInfoEnabled)
                {
                    var logParams = args.Method.GetParameters().Select((type, iterator) => new LogParameter
                    {
                        Name = type.Name,
                        Type = type.ParameterType.Name,
                        Value = args.Arguments.GetArgument(iterator)
                    }).ToList();

                    var logDetail = new LogDetail
                    {
                        FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                        MethodName = args.Method.Name,
                        Parameters = logParams
                    };

                    _loggerService.Info(logDetail);
                }
            }
            catch (Exception)
            {
                //Do nothing
            }

        }
    }
}
