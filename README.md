### TP24 Technical Test

# Domain Overview
The technical team at TP24 build technology to support the wider organisation in its B2B lending
business. A key part in how we do this is by ingesting and analysing receivables from
prospective clients to help our organisation decide if we should lend to a business, and how
much we are comfortable providing.
Receivables are debts owed to a company for goods or services which have been provided but
not yet paid for. Invoices and credit notes can be considered types of receivables.

# Brief
Write a basic set of HTTP APIs which follows best practices to provide the following capabilities:
- Accept a payload containing receivables data (see example payload below) and store it
- Return summary statistics about the stored receivables data; specifically the value of
open and closed invoices

The test is open ended and candidates should make and document assumptions about their
solution. Candidates are free to interpret the data and apply business logic as they see fit,
however the format of the payload should not differ from the example below.

# Guidance and Additional Notes
- The solution should be easily runnable and well tested
- The completed test can be submitted either as a GitHub repository or as a zip via email
- We encourage candidates to note how much time they spend on the test in the readme
- Your solution will be reviewed and then discussed during the technical interview

# Example Payload
[
{
"reference": "string",
"currencyCode": "string",
"issueDate": "string",
"openingValue": 1234.56,
"paidValue": 1234.56,
"dueDate": "string",
"closedDate": "string", //optional
"cancelled": true|false, // optional
"debtorName": "string",
"debtorReference": "string",
"debtorAddress1": "string", //optional
"debtorAddress2": "string", //optional
"debtorTown": "string", //optional
"debtorState": "string", //optional
"debtorZip": "string", //optional
"debtorCountryCode": "string",
"debtorRegistrationNumber": "string" //optional
}
]

# Assumptions
I assumed that only receivables with a valid closing date will be counted towards the closed statistics and only receivables without a closing date will be counted towards the open statistics.
For the database model, I considered the receivables reference field as the primary key.
Monetary values ​​returned in statistical endpoints are always converted to USD. 
I created a CurrencyConverterService, but to simplify the GetRate() function it always returns a static rate = 1, because otherwise I would need to fetch the rates from some external API.