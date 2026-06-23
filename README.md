Azure DevOps CI/CD scaffold

This repository contains a sample .NET app, Dockerfile, Kubernetes manifests, and reusable Azure DevOps pipeline templates.

Quick setup:

1. Create a variable group in Azure DevOps:
   - Go to Pipelines → Library → Variable groups
   - Create a new group named `sample-app-variables`
   - Add these variables:
     - `containerRegistry`: your ACR URL (e.g., `myregistry.azurecr.io`)
     - `imageName`: `sample-app`
     - `kubernetesServiceConnection`: your K8s service connection name
     - `containerRegistryServiceConnection`: your ACR service connection name
   - (Optional) link secrets like ACR credentials here

2. Create two service connections in your Azure DevOps project:
   - ACR service connection for your container registry (use the name from the variable group)
   - Kubernetes service connection (use the name from the variable group)

3. Push this repo to Azure DevOps and create a pipeline pointing to `azure-pipelines/azure-pipelines.yml`.

4. Configure environment approvals in the Azure DevOps project for `stage` and `prod` environments (Environments → create environment → set approvals & checks).

Notes:
- The pipeline builds the .NET app, runs unit tests, builds and pushes a Docker image, scans the image for vulnerabilities, publishes pipeline artifacts, and deploys to Kubernetes using the `kubectl` task.
- A sample test project exists in `sample-app.Tests` and is executed via `dotnet test` during the build.
- Image tags use a simple versioning template `$(versionPrefix)-$(Build.BuildId)` set in `versioning.yml`.
- Variables from `azure-pipelines/templates/variables.yml` act as defaults; override them in the `sample-app-variables` variable group.
- For production-grade deployments, replace `kubectl apply` with a proper deployment strategy (Helm, Kustomize, ArgoCD) and secure service connections.
