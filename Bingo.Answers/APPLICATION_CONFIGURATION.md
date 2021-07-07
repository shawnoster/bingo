# Application Configuration Settings

## Overview

This file describes the application configuration options and how to configure the application for both local development and production.

## Configuration

All project settings:

```json
{
"ApplicationInsights": {
    "ConnectionString": "<APPLICATION_INSIGHTS_CONNECTIONSTRING>"
  },
  "ConnectionStrings": {
    "ConnectionMode": "<Azure | Emulator>",
    "Azure": {
      "PrimaryKey": "<AUTH_KEY>",
      "ServiceEndpoint": "<COSMOS_DB_ENDPOINT>"
    },
    "Emulator": {
      "PrimaryKey": "<AUTH_KEY>",
      "ServiceEndpoint": "<COSMOS_DB_EMULATOR_ENDPOINT>"
    }
  },
  "CosmosDb": {
    "DatabaseName": "<COSMOS_DB_NAME>",
    "Containers": [
        {
        "Name" : "<COLLECTION_NAME>",
        "PartitionKey" : "<PARTITION_KEY_NAME>"
        }
    ]
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "Secrets": {
    "Mode": "<UseMsi | UseKeyVault | UseLocalSecretStore>",
    "KeyVaultUri": "<KEY_VAULT_URI>",
    "ClientId": "<APP_CLIENT_ID>",
    "ClientSecret": "<APP_CLIENT_SECRET>"
  }
}
```

These define the complete set of configuration settings, though they are not provided by the same file.

## How Settings Are Loaded

To best structure where various settings live it's helpful to understand how the settings are loaded.

At runtime, the configuration system reads several locations to create a single, combined, in-memory representation. If there are duplicate keys then the last value loaded will be the one used. In the case of the default host, the load order is:

1. `appsettings.json`
1. `appsettings.{Environment}.json`
1. `secrets.json` (if in Development environment specifically)
1. Environment variables
1. Command line arguments

Sensitive settings are stored outside the configuration system in a separate secret store. This is documented in [SECRET_MANAGEMENT.md](./SECRET_MANAGEMENT.md).

## Configuring the Application

To successful configure the application ensure all the required infrastructure is in place for the desired environment. Infrastructure requirements are documented in [INFRASTRUCTURE.md](./INFRASTRUCTURE.md)

### Replace Configuration Placeholders with Real Values

1. Open `appSettings.json`
   - Replace the `<YOUR>` values with your values
1. Open `appSettings.Development.json`
   - No changes needed but good to know what it's doing

### Add secrets to secret store (Development)

For development, Secret Manager is used to store secrets that should be kept separate from application settings.

This project has already been enabled for Secret Manager use, which adds a `UserSecretsId` property to the project, but for the curious, to enable a project to use the Secret Manager:

```powershell
# Enable Secret Manager for project
# This is informational only, this step has already been run for this project
dotnet user-secrets init
```

Add the sensitive keys:

```powershell
dotnet user-secrets set "ConnectionStrings:Azure:PrimaryKey" "<YOUR-AZURE-PRIMARYKEY>"
dotnet user-secrets set "ConnectionStrings:Emulator:PrimaryKey" "<YOUR-EMULATOR-PRIMARYKEY>"
dotnet user-secrets set "ApplicationInsights:ConnectionString" "<YOUR-APP-INSIGHTS-CONNECTIONSTRING>"

```

### Add secrets to secret store (Deployed)

Key Vault

- `ConnectionStrings--Azure--PrimaryKey`
- `ConnectionStrings--Emulator--PrimaryKey`
- `Secrets--ClientId`
- `Secrets--ClientSecret`

---

## Default Configuration

### Global Settings - `appSettings.json`

The base settings file, configured for a production-like environment with sane logging defaults.

```json
{
  "ConnectionStrings": {
    "ConnectionMode": "Azure",
    "Azure": {
      "ServiceEndpoint": "<YOUR SERVICE ENDPOINT URI>"
    },
    "Emulator": {
      "ServiceEndpoint": "https://localhost:8081"
    }
  },
  "CosmosDb": {
    "DatabaseName": "Bingo",
    "Containers": [
      {
        "Name": "Answers",
        "PartitionKey": "/category"
      }
    ]
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "Secrets": {
    "Mode": "UseMsi",
    "KeyVaultUri": "<YOUR KEY VAULT URI>"
  }
}
```

### Developer Overrides - `appSettings.Development.json`

Developer overrides. Increased logging, use of a local secrets.json file instead of Azure Key Vault. Use of a local secrets store greatly improves read access of secrets during development.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    },
    "Console": {
      "IncludeScopes": "true"
    }
  },
  "Secrets": {
    "Mode": "UseLocalSecretStore"
  }
}
```

---

## Acknowledgements

This document is largely based on two sources:
- [Github - Azure-Samples/PartitionedRepository](https://github.com/Azure-Samples/PartitionedRepository/tree/a0146f6b28b75571cbe3448a29af28efed26a527) ([MIT License](https://github.com/Azure-Samples/PartitionedRepository/blob/master/LICENSE.md))
- [Configuration, Secrets and KeyVault with ASP .NET Core](https://www.compositional-it.com/news-blog/configuration-secrets-and-keyvault-with-asp-net-core/) ([terms of use](https://www.compositional-it.com/terms-of-use/))
- https://github.com/Azure/azure-sdk-for-net/tree/Azure.Extensions.AspNetCore.Configuration.Secrets_1.2.1/sdk/extensions/Azure.Extensions.AspNetCore.Configuration.Secrets

By "largely based", I mean I straight copied [APPLICATION_CONFIGURATION.md](/.APPLICATION_CONFIGURATION.md) and [SECRET_MANAGEMENT.md](/.SECRET_MANAGEMENT.md) from Microsoft (love that MIT), then heavily edited it down, and swapped out some of the more vague content with the much more concise details gleaned from Compositional IT's post.
