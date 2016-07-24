This project aims to "loosely mimic" the DocumentDB API's but instead of posting to actual DocumentDB, writing to a directory structure on Disk.  This is mostly academic, as it doesn't really deal with a number of common concerns of data storage (yet). However, once completed, it's possible that it could be useful for Mocking or Unit testing without a live document db account. It is not designed to run as a service or actually perform network requests, it is designed to be called from the included client classes. 

Based on Document DB Model Reference:
https://azure.microsoft.com/en-us/documentation/articles/documentdb-resources/

Dependencies:
https://www.nuget.org/packages/LanguageExt.Core
https://www.nuget.org/packages/Xunit
https://www.nuget.org/packages/NLog
https://www.nuget.org/packages/Newtonsoft.Json
https://www.nuget.org/packages/AutoFixture


This project aims to use the following principles: 
. Functional Principles where Sensible, Object Oriented where Sensible
. Command Query Separation (CQS)
. Avoidance of primitive obsession
. Discipline with Exceptions
. Unit tests with given/when/then 
. Partial Application

This project aims to use the following features: 
. C# 6 features
. Heavy use of Linq
. Immutability
. Explicit Null
. Property-based testing
. Monads from LanguageExt
	.Optional
	.Either
	.Try
	etc
	
References: 
http://fsharpforfunandprofit.com/rop/
https://app.pluralsight.com/courses/csharp-applying-functional-principles

