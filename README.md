# Azure web shell
[Demo]()

An interface for azure logs which you can consume sending queries (same queries you use/write in the azure console) and 
return a json with all the information requested 

## Endpoints:

{apiUrl}/api/AzureWebShell/apps/{appId}?appKey={appKey}&query={azureQuery}

### Examples:

{apiUrl}/api/AzureWebShell/apps/{appId}?appKey={appKey}&query=requests | where name != 'GET /' and timestamp >= ago(150d) | summarize CountByService=count() by name, url | sort by CountByService

## Other informations:

AppId: is azure unique app key (the one you're going to use is the insight app key), can found this one 
in the app information


AppKey: Like AppId is unique one but this one should generate by admin user and must be saved because azure (the platform)
doesn't save this one.

### References:

[How get AppId and create AppKey](https://dev.applicationinsights.io/documentation/Authorization/API-key-and-App-ID/)

## Updates:

The AppKey and AppId must be save in azure key vaults for security reasons.

## Versioning:

Version 0.1 beta
