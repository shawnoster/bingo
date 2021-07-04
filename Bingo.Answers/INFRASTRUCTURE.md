# INFRASTRUCTURE

## Overview

This document lists all infrastructure resources needed to run this project in either a local development mode or deployed to production in the cloud.

In time these manual steps will be replaced with infrastructure building pipelines and configuration as code, but that's farther down the backlog.

## Environment - Development

Basic steps:

1. Create a local Cosmos DB
1. Create a local secret store

### 1. Create a local Cosmos DB

Local Cosmos DB development is done via the Azure Cosmos DB Emulator. It supports the same functionality and connection methods making it ideal for local development.

1. Install and run Azure Cosmos DB Emulator
1. Open Data Explorer
1. Create a new database: `Bingo`
1. Create a new container: `Answers`
   - Partition key: `/category`

### 2. Create a local secret store

Secrets are managed locally via Secret Manager, which uses a plaintext JSON file stored under the users roaming data folder.

All secrets need to be created manually before running the project. At least if you want it to work.

See [SECRET_MANAGEMENT.md](./SECRET_MANAGEMENT.md) for detailed steps.

## Environment - Cloud

Basic steps:

1. Create the resource group `bingo-answers-rg`
1. Create the Cosmos DB resource `answers-db`
1. Create the Key Vault `answers-kv`

### 1. Create the resource group `bingo-answers-rg`

1. From the Azure Portal, create a resource group
1. Name it: `bingo-answers-rg`

### 2. Create the Cosmos DB resource `answers-db`

1. From the Azure Portal, navigate to `bingo-answers-rg`
1. Name it: `answers-db`
1. Use all defaults

### 3. Create the Key Vault `answers-kv`

1. From the Azure Portal, navigate to `bingo-answers-rg`
1. Create a Key Vault
1. Name it: `answers-kv`
