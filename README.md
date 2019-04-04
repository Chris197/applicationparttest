# Application Part Test
Simple test project to try dynamic loading of application parts.

The purpose of this sample is to reproduce an issue regarding application parts. The sample uses McMaster.NETCore.Plugins to load an 
assembly at runtime and load its application parts.

When running the sample, an api call will be made to a GET call defined in a controller in a separate project called Module.csproj.
There is no direct reference to this project.

Expected: loading the dll at runtime adds the Controller to the current application domain and exposes the call contained in it.
Actual Result: A 404 is returned when trying to execute the call at the expected address.
