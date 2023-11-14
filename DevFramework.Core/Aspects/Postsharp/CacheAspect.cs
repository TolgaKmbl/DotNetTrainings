using DevFramework.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.Postsharp
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        private Type _cacheType;
        private int _timeToLive;
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int timeToLive=5)
        {
            _cacheType = cacheType;
            _timeToLive = timeToLive;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (!typeof(ICacheManager).IsAssignableFrom(_cacheType))
            {
                throw new InvalidCastException("Invalid cache manager type");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);
            base.RuntimeInitialize(method);
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}.{2}",
                args.Method.ReflectedType.Namespace,
                args.Method.ReflectedType.Name,
                args.Method.Name);
            var arguments = args.Arguments.ToList();
            var generatedKey = string.Format("{0}({1})", 
                methodName, 
                string.Join(",", arguments.Select(a => a != null ? a.ToString() : "<Null>")));
            if (_cacheManager.isAdded(generatedKey))
            {   
              args.ReturnValue = _cacheManager.Get<object>(generatedKey);  
            }
            base.OnInvoke(args);
            _cacheManager.Add(generatedKey, args.ReturnValue, _timeToLive);
        }

    }
}
