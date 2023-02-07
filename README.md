[[_TOC_]]

# Introduction

This project establishes a Proof of Concept (POC) for centralized contextual logging for Microservice in regards to VSA 2.0 architecture. 

As part of this project several options were evaluated, New Relic, ELK stack, Snare, Custom Implementations, etc. New Relic was chosen due to the ease of transition (since the organization already has the infrastructure in place), Single Point of Responsibility (since the software is already used for APM purposes), flexibility and provides all the necessary features in order to support centralized logging for Microservices.

# Getting Started

1.	Installation process
   1.	Microsoft Visual Studio
      1.	Clone DevOps Project - ~/flvs/VSA/Repos/Files/NewRelicLoggingPOC
   2.	MS SQL Server Database
      1.	Connect to Host Database
      1.	Run /NewRelicLoggingPOC/Release/Create Database.SQL
         1.	LoggingPOC Database name will be created with appropriate schema
         2.	Dummy data will be created in appropriate table
   3.	TODO: Postman
      1.	Import Collections - ~/flvs/VSA/Repos/Files/MongoDBPOC/Release/Postman/xxx_collection.json
      2.	Import Environments - ~/flvs/VSA/Repos/Files/MongoDBPOC/Release/Postman/xxx_environment.json
         1.	Edit api_url variable to match your local debugging environment URL
2.	Software dependencies
   - .NET Core 5.0
   - [Correlation ID](https://www.nuget.org/packages/CorrelationId/3.0.1?_src=template)
   - [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/5.0.14?_src=template)
   - [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/5.0.12?_src=template)
   - [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/5.0.12?_src=template)
   - [NewRelic.Agent.API](https://www.nuget.org/packages/NewRelic.Agent.Api/9.6.0?_src=template)
   - [NewRelic.LogEnrichers.Serilog](https://www.nuget.org/packages/NewRelic.LogEnrichers.Serilog/1.0.1?_src=template)
   - [Serilog](https://www.nuget.org/packages/Serilog/2.10.0?_src=template)
   - [Serilog.AspNetCore](https://www.nuget.org/packages/Serilog.AspNetCore/5.0.0?_src=template)
   - [Serilog.Enrichers.Thread](https://www.nuget.org/packages/Serilog.Enrichers.Thread/3.1.0?_src=template)
   - [Serilog.Sinks.File](https://www.nuget.org/packages/Serilog.Sinks.File/5.0.0?_src=template)
   - [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/5.6.3?_src=template)
   - [NSwag.ApiDescription.Client](https://www.nuget.org/packages/NSwag.ApiDescription.Client/13.0.5?_src=template)
   - [Microsoft.Extensions.ApiDescription.Client](https://www.nuget.org/packages/Microsoft.Extensions.ApiDescription.Client/3.0.0?_src=template)
   - [Microsoft.VisualStudio.Web.CodeGene](https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Design/5.0.2?_src=template)
   - [Microsoft.VisualStudio.Web.CodeGeneration.Utils](https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Utils/5.0.2?_src=template)
3.	Latest releases
   - 1.x.x
4.	API references
   - Swagger & OpenAPI via Visual Sudio Connected Services
     - [Generating HTTP API clients using dotnet tools and Visual Studio Connected Services](https://dotnetthoughts.net/generating-http-api-clients-using-dotnet-tools/)
       - Microsoft.com does not provide a page describing the process as accurately as this blog by a Microsoft MVP.
       - The blog post was validated based on my personal experience in implementing it.
   - Tracing Requests between Microservices via Correlation IDs
     - [Correlation ID Git](https://github.com/stevejgordon/CorrelationId)
     - [ASP.NET CORE Correlation IDs](https://www.stevejgordon.co.uk/asp-net-core-correlation-ids)
     - [Correlation IDs](https://stevejgordon.github.io/CorrelationId/)
   - Serilog
     - [Serilog Home](https://serilog.net/)
       - Includes Configuration, Examples, Explanations, Architecture, among many other collection of references
     - [Serilog Git][https://github.com/serilog/serilog]
   - New Relic Centralized Logging
     - [Docs New Relic for .NET](https://docs.newrelic.com/docs/apm/agents/net-agent/getting-started/introduction-new-relic-net/)
     - [.NET: Configure logs in context with Serilog](https://docs.newrelic.com/docs/logs/logs-context/net-configure-logs-context-all/#serilog)
   - 
5.	Tools
   - [SQL Server Management Studio (SSMS])(https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)

# Build and Test
NewRelicLoggingPOC main project is just an empty template that houses Microservices, LoggingDataLayerWEBAPI and LoggingBusinessDomainWEBAPI.

- Building
  - You can build the project as normal, and it will be build all referenced projects gracefully.
- Running
  - There is no project to set as "Setup as Startup Project"
  - Each project should be run as a new instance
    - Right click on each project name and under "Setup as Startup Project", you will find "Debug", and then select "Start New Instance".
- Testing
  - LoggingDataLayerWEBAPI
    - This project is a stand alone project that exposes an API and connects directly to the Database via EF.
  - LoggingBuisnessDomainWEBAPI
    - This project requires LoggingDataLayerWEBAPI to be running as it consumes the exposed API of this service in order to execute.

# Related Documents

# Related Projects

# Contribute