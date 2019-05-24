namespace Cake.Deploy.Variables.Test
{
    using System;
    using Cake.Core;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Cake.Core.Tooling;
    using NSubstitute;

    public class CakeContextFixture : IDisposable
    {
        public string CurrentEnvironment { get; }
        public IFileSystem FileSystem { get; set; }
        public ICakeEnvironment Environment { get; set; }
        public IGlobber Globber { get; set; }
        public ICakeLog Log { get; set; }
        public ICakeArguments Arguments { get; set; }
        public IProcessRunner ProcessRunner { get; set; }
        public IRegistry Registry { get; set; }
        public IToolLocator Tools { get; set; }
        public ICakeDataService DataService { get; set; }
        public ICakeContext Context { get; set; }

        public CakeContextFixture(string currentEnvironment)
        {
            CurrentEnvironment = currentEnvironment;
            FileSystem = Substitute.For<IFileSystem>();
            Environment = Substitute.For<ICakeEnvironment>();
            Globber = Substitute.For<IGlobber>();
            Log = Substitute.For<ICakeLog>();
            Arguments = Substitute.For<ICakeArguments>();
            ProcessRunner = Substitute.For<IProcessRunner>();
            Registry = Substitute.For<IRegistry>();
            Tools = Substitute.For<IToolLocator>();
            DataService = Substitute.For<ICakeDataService>();

            this.Arguments.GetArgument("env")
                .Returns(currentEnvironment);
                
            this.Arguments.HasArgument("env")
                .Returns(true);

            Context = new CakeContext(FileSystem, Environment, Globber,
                Log, Arguments, ProcessRunner, Registry, Tools, DataService);
        }

        public ICakeContext GetContext()
        {
            return Context;
        }

        public void Dispose()
        {
            VariableManager.Clear(CurrentEnvironment);
        }
    }
}
