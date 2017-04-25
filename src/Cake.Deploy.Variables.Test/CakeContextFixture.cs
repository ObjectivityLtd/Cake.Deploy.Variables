using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using NSubstitute;

namespace Cake.Deploy.Variables.Test
{
    public class CakeContextFixture : IDisposable
    {
        public IFileSystem FileSystem { get; set; }
        public ICakeEnvironment Environment { get; set; }
        public IGlobber Globber { get; set; }
        public ICakeLog Log { get; set; }
        public ICakeArguments Arguments { get; set; }
        public IProcessRunner ProcessRunner { get; set; }
        public IRegistry Registry { get; set; }
        public IToolLocator Tools { get; set; }

        public ICakeContext Context { get; set; }

        public CakeContextFixture(string currentEnvironment)
        {
            FileSystem = Substitute.For<IFileSystem>();
            Environment = Substitute.For<ICakeEnvironment>();
            Globber = Substitute.For<IGlobber>();
            Log = Substitute.For<ICakeLog>();
            Arguments = Substitute.For<ICakeArguments>();
            ProcessRunner = Substitute.For<IProcessRunner>();
            Registry = Substitute.For<IRegistry>();
            Tools = Substitute.For<IToolLocator>();

            this.Environment.GetEnvironmentVariables()
                .Returns(new Dictionary<string, string>() { { "env", currentEnvironment } });

            this.Environment.GetEnvironmentVariable("env")
                .Returns(currentEnvironment);

            Context = new CakeContext(FileSystem, Environment, Globber,
                Log, Arguments, ProcessRunner, Registry, Tools);
        }

        public ICakeContext GetContext()
        {
            return Context;
        }

        public void Dispose()
        {
            VariableManager.Clear();
        }
    }
}
