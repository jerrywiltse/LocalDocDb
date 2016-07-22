This project aims to "loosely mimic" the DocumentDB API's but instead of posting to actual DocumentDB, writing to a directory structure on Disk.  This is mostly academic, as it doesn't really deal with a number of common concerns of data storage.  This is a largely academic endeavor to help me learn C# and create a simple functionality that I know I've wanted on a few script projects in the past.

Based on Document DB Model Reference:
https://azure.microsoft.com/en-us/documentation/articles/documentdb-resources/

Dependencies:
https://www.nuget.org/packages/LanguageExt.Core
https://www.nuget.org/packages/Xunit
https://www.nuget.org/packages/NLog
https://www.nuget.org/packages/Newtonsoft.Json


This project aims to use the following principles: 
. Functional Principles where Sensible, Object Oriented where Sensible
. Frequent use of railway-oriented programming
. Frequent use of CQS
. Avoidance of primitive obsession

This project aims to use the following Features: 
. C# 6 features
. Heavy use of Linq
. Immutability
. Use of Optional<T>

. Discipline with Exceptions
. Explicit Null
. Property-based testing
