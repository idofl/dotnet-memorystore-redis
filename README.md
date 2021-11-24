# Accessing a secured Google Cloud Memorystore Redis from a .NET app

This example shows how to access a [Memorystore](https://cloud.google.com/memorystore) Redis instance, that has both [AUTH](https://cloud.google.com/memorystore/docs/redis/auth-overview) and [in-transit encryption](https://cloud.google.com/memorystore/docs/redis/in-transit-encryption) enabled, from a .NET 5 app.

## Create the container image
1. Clone the repo.
1. Build and publish the application:
    ```bash
    dotnet publish -c Release Redistest/Redistest.csproj
    ```
1. Build the container image and push it to Google Cloud:
    ```bash
    export PROJECT_ID=[PROJECT_ID]
    gcloud auth configure-docker
    docker build -t gcr.io/$PROJECT_ID/redistest:latest .
    docker push gcr.io/$PROJECT_ID/redistest:latest
    ```
    Replace **[PROJECT_ID]** with your project ID.

## Create a secured Memorystore Redis instance

1. Create a Memorystore Redis instance with AUTH and in-transit encryption enabled. For instructions see:
    - https://cloud.google.com/memorystore/docs/redis/enabling-in-transit-encryption
    - https://cloud.google.com/memorystore/docs/redis/managing-auth
1. [Get the instance's host and port information](https://cloud.google.com/memorystore/docs/redis/creating-managing-instances#viewing_instance_information) and write them down.
1. [Get the AUTH string](https://cloud.google.com/memorystore/docs/redis/managing-auth#getting_the_auth_string) and write it down.
1. [Download the CA certificate](https://cloud.google.com/memorystore/docs/redis/enabling-in-transit-encryption#downloading_the_certificate_authority) and store it in the same folder as the **dockerfile** file. Name the file **server-ca.pem**.

## Test the application

SSH into a Linux VM in your VPC that has the docker CLI installed and run the following command:

```bash
sudo docker run \
-e REDIS_HOST='HOST:POST' \
-e REDIS_AUTH='AUTH' \
gcr.io/$PROJECT_ID/redistest:latest
```
Replace **HOST:PORT** and **AUTH** with the values you wrote down for the instance's host IP, port, and AUTH string.

You should see the following output:
```
Value stored in Redis: 12345678
Value fetched from Redis: 12345678
```