using Xunit;

namespace Cake.Deploy.Variables.Test
{
    public class AddinTests
    {
        [Fact]
        public void ShouldReturnCorrectVariableValue_String()
        {
            // arrange
            var fixture = new CakeContextFixture();
            //act
            var result = fixture.CreateContext();

            var env = result.Environment("dev")
                .AddVariable("test", "testResult");
            
            //assert
            Assert.Equal("testResult", env.Variables["test"]);
        }

        [Fact]
        public void ShouldReturnCorrectVariableValue_Func()
        {
            // arrange
            var fixture = new CakeContextFixture();
            //act
            var result = fixture.CreateContext();

            var env = result.Environment("dev")
                .AddVariable("base", x => "default value")
                .AddVariable("child", x => "Not a " + x["base"]);

            //assert
            Assert.Equal("Not a default value", env.Variables["child"]);
        }

        [Fact]
        public void ShouldReturnCorrectVariableValue_default()
        {
            // arrange
            var fixture = new CakeContextFixture();
            //act
            var result = fixture.CreateContext();

            var baseEnv = result.Environment("default")
                .AddVariable("var1", "default1")
                .AddVariable("var2", "default2");

            var env = result.Environment("dev")
                .IsBasedOn("default")
                .AddVariable("var2", "differentValue");
            
            //assert
            Assert.Equal("default1", env.Variables["var1"]);
            Assert.Equal("differentValue", env.Variables["var2"]);
        }
    }
}
