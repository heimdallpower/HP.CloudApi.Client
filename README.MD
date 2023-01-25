# API Client package for Heimdall Cloud

A class library that consumes the Heimdall Cloud API distributed as a nuget package.

## Version 1.x.x

This version set features authentication using Azure Active Directory Authentication (ADAL) library. An HTTP Client for Heimdall Cloud can be set up using x509 certificates. The api client acquires access tokens using the clients credentials flow in AAD. The version set uses .NET 5.

## Version 2.x.x

This version set features authentication using Microsoft Authentication Library (MSAL), has ADAL is deprecated. The major change in this version set is a migration from using regular Azure Active Directory to Azure Active Directory B2C as basis for authentication. 

As AAD B2C does not support the use of certificates to issue access tokens, **client secrets** must be used to initialize the Heimdall Cloud HTTP Client.
