# Cake.Deploy.Variables
This is an extension for Cake framework. It allows using so called ReleaseVariables. Release variables are values which required during deployment and may deffer among environments e.g. server address, port number.

# How to add Cake.Deploy.Variables

In order to use it add the following line in your addin section:
```cake
#addin Cake.Deploy.Variables
```

Release variables can be simply accessed by ReleaseVariable(variableName) method. With one condition: an "env" argument has to be set. An example this can be done below:
```powershell
.\build.ps1 -ScriptArgs '-env="dev"'
```
Now when you want to indicate which environment the deployment should be run to (which set of release variables should be used) set the "env" argument with the name of the environment like dev, test, uat.

# How to define release variables
The code below shows how to define release variables for dev environment
```cake
ReleaseEnvironment("dev")
    .AddVariable("UserName", "devuser@somedomain.com")
    .AddVariable("Pass", "************");
```
A basic inheritance mechanism is implemented. It is possible to mark an environment as based on other environment. An example below:
```cake
ReleaseEnvironment("default")
    .AddVariable("Port", "8080")
    .AddVariable("address", "localhost")

ReleaseEnvironment("dev")
    .IsBasedOn("default")
    .AddVariable("address", "172.10.0.1")
    .AddVariable("UserName", "devuser@somedomain.com")
    .AddVariable("Pass", "************");
```
This way there is no need to add add common variables to each environment.
It is also possible to override a base variable value simply by adding varibale with the same name.

Additionally variables can use other variables (also when it is defined in base environment):
```cake
ReleaseEnvironment("dev")
    .AddVariable("userName", "AUser")
    .AddVariable("domainName", "aDomain.com")
    .AddVariable("fullUserName", x => x["userName"] + "." + x["domainName"]);
```

It also works if you define, in a base environment, a variable which reference other variable, and then override the referenced variable in a derived environment.

```cake
ReleaseEnvironment("default")
    .AddVariable("appName", "MyApp")
    .AddVariable("envSuffix", "default")
    .AddVariable("siteName", x => x["appName"] + x["envSuffix"]);

ReleaseEnvironment("dev")
    .IsBasedOn("default")
    .AddVariable("address", "172.10.0.1")
    .AddVariable("UserName", "devuser@somedomain.com")
    .AddVariable("Pass", "************");
```

A variable value can be set with value from an argument

```cake
ReleaseEnvironment("dev").AddVariable("Password", x => Argument<string>("Password", ""));
```
In this case Cake should be run as below:

```powershell
.\build.ps1 -ScriptArgs '-env="dev"', '-Password="yourPassword"'
```

# How to use release variables
If you want to use release variable simply call ReleaseVariable method with a variable name

```cake
var login = ReleaseVariable("UserName");
```

**Please remember that in order to use release varibles an "env" argument has to be set.**

In some cases you might want to update a variable. To do so call:

```cake
ReleaseVariable().SetVariable(variableName, variableValue).
```