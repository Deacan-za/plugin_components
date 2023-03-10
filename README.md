# Plug-in Architecture Example

C# Console application demostrating a simple implementation for loading dll's as plug-in's on start up. Although this is a console application, dependency injection is implemented using the ServiceCollection to illustrate how this would be wired up in a normal API.

This implementation is config driven. On start up, registration of plug-in modes occurs by reading the supported modes in the cofiguration and looking for the dlls that support that mode. Once the plug-in has been found, the handler belonging to this mode is created and stored in an in memory dictionary.

Requests are processed by getting the handler from the dictionary for a requested mode and calling the ProcessRequest method. 

This repository serves as example code for my blog post: 
[Our Journey to a Segmented Monolith with Plug-ins and why?](https://www.linkedin.com/pulse/our-journey-segmented-monolith-plug-ins-andwhy-brandon-shaw)
