# Secrets and the Configuration System

This project uses the default .NET 5 configuration system to store secrets transparently in a specialized secret store running locally and in Azure Key Vault.

The `appSettings.json` file, as well as the environment dependent files (e.g. `appSettings.Development.json`), may still be used for configuration settings that are not deemed sensitive information.

Connection strings, passwords, and other sensitive information, should be configured via a secret store.

During development either a local secret store or Key Vault can be used. When deployed to a cloud environment a Key Vault must be used with the Web App's Managed Service Identity (MSI). This method provides the safest configuration since secrets are stored in Key Vault, and (after appropriate configuration of the Key Vault), the MSI does not require a set of credentials to access Key Vault.

To test locally using Key Vault you have to provide Key Vault client credentials. This is because MSI only runs in the context of Azure and not within the development machine. For this scenario, you can provide Key Vault client credentials via the local secret store.

To setup the configuration:

1. Configure the local Secret Store
2. Add secrets to the Secret Store
3. Add secrets to the Key Vault
4. Adjust the `appSettings.json` file to establish the secret storage mode.

## 1. Configure the local secret store

The local secret store uses a different format than `appSettings.json`. Secrets are stored in a file called `secrets.json`, which must be created under the user's profile at the following locations:

* Windows: `%APPDATA%\Microsoft\UserSecrets\<GUID>\secrets.json`
* macOS: `~/.microsoft/usersecrets/<GUID>/secrets.json`
* Linux: `~/.microsoft/usersecrets/<GUID>/secrets.json`

The GUID is project specific and defined in the .csproj file as UserSecretsId. See [Safe storage of app secrets in development in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=macos) for more information on setting up a local secret store.

## 2. Add secrets to the secret store

The `secrets.json` file uses a flat schema to store information. To simulate a hierarchy, colons `:` must be used.

For example, if `appSettings.json` looks like:

```json
{
  "Application": {
    "DefaultLanguage" : "English"
  },
  "ConnectionStrings": {
    "CosmosDbEndpoint": "https://acosmosname.documents.azure.com:443/",
    "CosmosDbName": "todo"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  ...
}
```

and we want to store a setting called AdminPassword for the Application section, then `secrets.json` should be:

```json
{
    "Application:AdminPassword" : "passwordHere"
}
```

## 3. Add Secrets to Key Vault

The same secrets that appear in `secrets.json` should exist in Key Vault. The only exceptions are `ClientId` and `ClientSecret`, which are used to connect to Key Vault, so there is little point in storing them in Key Vault itself.

Secrets in Key Vault are not stored in hierarchies, so their names must be flattened like `secrets.json` . The only difference is that colons are not allowed in Key Vault secrets because the secret name is part of its URI.

To work around this, replace colons with double dashes `--`. For example, the Admin Password shown above would be stored in Key Vault as: `Application--AdminPassword`

## 4. Adjust the appSettings.json file to establish the secret storage mode
A setting in `appSettings.json` is used to tell the application where to find secrets. The following snippet shows the setting that controls the secret storage approach:

```json
{
...
  "Secrets": {
    "Mode": "UseMsi",
    "KeyVaultUri": "https://<KeyVaultName>.vault.azure.net/"
  }
...
}
```

The Mode configuration entry can have one of three values:

* `UseLocalSecretStore`
* `UseClientSecret`
* `UseMsi`

To use Key Vault when developing locally set the Key Vault connection credentials. Set the Mode to `UseClientSecret` in `appSettings.json`, and add the following entries in `secrets.json`:

```json
{
  ...
  "Secrets:ClientId": "[AppId]",
  "Secrets:ClientSecret": "[Secret]"
  ...
}
```
