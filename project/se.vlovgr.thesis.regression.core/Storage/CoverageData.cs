using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using se.vlovgr.thesis.regression.core.Extensions;
using se.vlovgr.thesis.regression.core.Models.Methods;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;
using se.vlovgr.thesis.regression.core.Storage.Interfaces;

namespace se.vlovgr.thesis.regression.core.Storage
{
    public class CoverageData : ICoverageData
    {
        public string BackingFileName { get; private set; }

        private readonly Stack<IMethod> _nextTestStack = new Stack<IMethod>(); 
        private readonly IList<IMethodInvocation> _nextTestInvocations = new List<IMethodInvocation>();

        private ITestMethod _currentTest;
        private readonly Stack<IMethod> _currentTestStack = new Stack<IMethod>();
        private IDictionary<ITestMethod, IList<IMethodInvocation>> _coverage =
            new Dictionary<ITestMethod, IList<IMethodInvocation>>();

        public CoverageData(string backingFileName)
        {
            BackingFileName = backingFileName;
        }

        public void OnMethodEntered(MethodBase m)
        {
            Method method;
            if (m.IsConstructor)
            {
                var mString = m.ToString();
                method = new Method(mString.Substring(mString.LastIndexOf(' ') + 1), m.DeclaringType.FullName);
            } else method = new Method(m.Name, m.DeclaringType.FullName);

            if (_currentTest != null)
            {
                if(_currentTestStack.Any())
                    _coverage[_currentTest].Add(new MethodInvocation(_currentTestStack.Peek(), method));

                _currentTestStack.Push(method);
            }
            else
            {
                if(_nextTestStack.Any())
                    _nextTestInvocations.Add(new MethodInvocation(_nextTestStack.Peek(), method));

                _nextTestStack.Push(method);
            }
        }

        public void OnMethodExited(MethodBase m)
        {
            if (_currentTest != null)
                _currentTestStack.Pop();
            else
            {
                
                _nextTestStack.Pop();
            }
        }

        public void OnTestMethodEntered(MethodBase m)
        {
            OnMethodEntered(m);
        }

        public void OnTestMethodExited(MethodBase m)
        {
            OnMethodExited(m);
        }

        public void OnTestStarted(ITestMethod test)
        {
            if (!_coverage.ContainsKey(test))
            {
                var methodInvocations = new List<IMethodInvocation>(_nextTestInvocations);
                _coverage.Add(test, methodInvocations);
                _nextTestInvocations.Clear();
                _nextTestStack.Clear();
            }

            _currentTest = test;
            _currentTestStack.Clear();
            _currentTestStack.Push(test);
        }

        public void OnTestFinished(bool successful)
        {
            if (_coverage[_currentTest].None())
                _coverage.Remove(_currentTest);
            else _currentTest.WasSuccessful = successful;

            _currentTest = null;
            _currentTestStack.Clear();
            _nextTestStack.Clear();
            _nextTestInvocations.Clear();
        }

        public IDictionary<ITestMethod, IList<IMethodInvocation>> GetStored()
        {
            var serializer = new DataContractJsonSerializer(_coverage.GetType(),
                new List<Type> { typeof(TestMethod), typeof(Method), typeof(MethodInvocation) });

            using (var stream = new FileStream(BackingFileName, FileMode.Open, FileAccess.Read))
            {
                return (IDictionary<ITestMethod, IList<IMethodInvocation>>)serializer.ReadObject(stream);
            }
        }

        public IDictionary<ITestMethod, IList<IMethodInvocation>> Get()
        {
            return _coverage;
        }

        public void Store()
        {
            if (File.Exists(BackingFileName))
            {
                var storedCoverage = GetStored();
                foreach (var entry in _coverage.Where(entry => entry.Value.Any()))
                {
                    storedCoverage.Remove(entry.Key);
                    storedCoverage.Add(entry.Key, entry.Value);
                }

                _coverage = storedCoverage;
            }

            File.WriteAllText(BackingFileName, GetSerializedCoverage());
        }

        private String GetSerializedCoverage()
        {
            var stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(_coverage.GetType());

            serializer.WriteObject(stream, _coverage);
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}