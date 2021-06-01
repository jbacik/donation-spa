# Donations SPA
A sample SPA that is a Donation Form for my favorite Non-profit [Claire's Crusade]("http://clairescrusade.org/" "http://clairescrusade.org/").
The application is built using ASP.Net 5.0 Razor Pages and stores the donation information to a Local SQL Database.  

This stack with the easiest for me to get up and running right away with a built-in code building, web server and data storage.


## Claire's Crusade
This is a very personal non-profit run by friends who have a daughter with Rett Syndrome.   Our family has been supporting them however we can for over 10 years.

### Prerequisites
- Visual Studio 2019 updated version 16.8 or later
- [.NET SDK 5.0]("https://dotnet.microsoft.com/download/dotnet/5.0") or Later
- [SQL Server Express LocalDB]("https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15")
- Create a `Donations_JBacik` database on your LocalDB and run the `01_InitialSchema.sql`_script in `App_Data\runAfterCreateDatabase` to create the donation table


### Donation Form Functionality
- Enter Donor Information like First Name, Last Name, Email Address
and Home Address
- Enter Donation amount
- Enter (Fake) credit card information
- Option to "Cover Costs" where Donor pays the credit card fees. Donor
should be shown a new Donation amount Example: 5 donation with cover costs is 5.45 with a
rate of 2.9% + flat fee 30 cents
- Donation Thank you message
- Capture Donation data, without credit card information, in a database

#### Form Validation 
- All fields except Address 2 are required
- Email Address type validation
- Donation Amount > 0
- Length validation to avoid database overflow length errors

#### Covered Costs
- The service method is commented to describe why the calculation is doing what is it
- An WebAPI endpoint was added to call the same calculator logic

### What's Left
I believe my solution still needs:
- Unit Testing on the Covered Cost Caculator to verify results and edge cases
- Client side input masking for Credit Card information
- Clean the guid encoder for the additional confirmation number
- Update the client-side fetch and server side OnPost with more error-handling responses to the user

#### What would have been nice to have
- On successful donation hide the donation form with a button to donate again
- On successful donation present share buttons
- Think about a stepped approach to give the user a small number of inputs at once
- Donation Amount options like $50, $100, etc to get the user to donate quicker (and possibly more if they thinking under the lowest amount)

## Dependencies
| **Tech** | **Description / Why I used it** |**Area**|
|----------|-------|---|
|  [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/get-started/)  |  lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology, serving as our O/R mapper |  Server Side, Database  |
|  [Bootstrap 5](https://getbootstrap.com/docs/5.0/getting-started/introduction/)  |  The world’s most popular framework for building responsive, mobile-first sites. |  Client Side  |
|  [MediatR](https://github.com/jbogard/MediatR)  | Simple mediator implementation in .NET.  I like to reach for this pattern and package to keep my controller actions slim and create more [integration] testable handlers.  |  Server Side  |
|  [Fluent Validation](https://fluentvalidation.net/)  | A popular .NET library for building strongly-typed validation rules.  MVC will use FluentValidation to validate objects that are passed in to controller actions by the model binding infrastructure.  Unobtrusive jQuery support to show validation errors with asp tags. |  Server & Client Side  |
|  [Currency.js](https://currency.js.org/)  |  A small, lightweight javascript library for working with currency values.  |  Client Side  |
