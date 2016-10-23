nuget install xunit.runner.console -Version 2.1.0 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.7.0 -OutputDirectory tools

.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:.\tools\xunit.runner.console.2.1.0\tools\xunit.console.exe -targetargs:".\NArchitecture.Tests\bin\Debug\NArchitecture.Tests.dll -noshadow" -filter:"+[NArchitecture*]* -[NArchitecture.Tests*]* -[NArchitecture*]NArchitecture.Properties.*" -output:NArchitecture.Tests.xml -coverbytest:*.Tests.dll
.\tools\coveralls.net.0.7.0\tools\csmacnz.Coveralls.exe --opencover -i .\NArchitecture.Tests.xml 