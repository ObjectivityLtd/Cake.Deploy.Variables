namespace Cake.Deploy.Variables.Test
{
    using System;
    using Cake.Core;
    using Cake.Core.Configuration;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Cake.Core.Tooling;
    using NSubstitute;

    public class CakeContextFixture : IDisposable
    {
        public CakeContextFixture(string currentEnvironment)
        {
            this.CurrentEnvironment = currentEnvironment;
            this.FileSystem = Substitute.For<IFileSystem>();
            this.Environment = Substitute.For<ICakeEnvironment>();
            this.Globber = Substitute.For<IGlobber>();
            this.Log = Substitute.For<ICakeLog>();
            this.Arguments = Substitute.For<ICakeArguments>();
            this.ProcessRunner = Substitute.For<IProcessRunner>();
            this.Registry = Substitute.For<IRegistry>();
            this.Tools = Substitute.For<IToolLocator>();
            this.DataService = Substitute.For<ICakeDataService>();
            this.Configuration = Substitute.For<ICakeConfiguration>();

            this.Arguments.GetArgument("env")
                .Returns(currentEnvironment);

            this.Arguments.HasArgument("env")
                .Returns(true);

            this.Context = new CakeContext(this.FileSystem, this.Environment, this.Globber, this.Log, this.Arguments, this.ProcessRunner, this.Registry, this.Tools, this.DataService, this.Configuration);
        }

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

        public ICakeConfiguration Configuration { get; set; }

        public ICakeContext GetContext()
        {
            return this.Context;
        }

        public void Dispose()
        {
            VariableManager.Clear(this.CurrentEnvironment);
        }
    }
}