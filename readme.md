# Numbers
### How to start
Run Visual Studio as administrator (to allow self hosting). Set up startup projects: Numbers.Host, Numbers.UI.

### Overview
Solution contains 5 app projects and 3 tests projects.

Numbers - domain logic to convert user input from numbers into word presentation.
Numbers.Contracts - WCF contracts shared between UI and Host.
Numbers.Host - console application to host services.
Numbers.Services - implementation of services.
Number.UI - WPF application.

During development I was mainly focusing on readability of solution. Some code duplication was left, because I believe that it reduce complexity. It can be easily replaced with generic solution if requirements change. Example: CalculateMillions etc. methods in EnglishNumbersConverter contain similar logic, which can be replaced by some class if supported range changed, DollarsWithCentsConverter can be generalized and get "dollars" and "cents" converters as constructor parameters to support other currencies (like euros, pounds).  
I belive that current solution is reasonable compromise between easy, readable and extensible solution.