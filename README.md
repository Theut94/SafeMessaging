# SafeMessaging

SafeMessaging is a secure messaging application built with .NET, leveraging a Blazor frontend for a seamless and responsive user experience. The project prioritizes security, efficiency, and scalability, making it ideal for modern communication needs.

## Features

- **Secure Messaging:** Ensures end-to-end encryption for secure communication.
- **Modern Frontend:** Built using Blazor for a responsive, interactive, and high-performance user interface.
- **Scalable Backend:** Powered by .NET for robust and scalable backend services.
- **Configurable Database Connection:** Easily configurable PostgreSQL database connection string.

## Technologies Used

- **.NET**: Backend development and core application logic.
- **Blazor**: Frontend framework for building web UI with .NET.
- **PostgreSQL**: Database for storing application data.

## Getting Started

### Prerequisites

Before running the project, ensure you have the following installed:

- .NET SDK 8.0 or higher
- PostgreSQL
- A compatible browser (for running the Blazor frontend)

### Configuration

1. Clone the repository to your local machine:
   ```bash
   git clone https://github.com/theut94/safe-messaging.git
   cd safe-messaging
   ```
### Apssettings

```
"ConnectionStrings": {
  "PGConnectionString": "<CONNECTIONSTRING>"
},
  "JWT": {
    "KEY": "<KEY OF ATLEAST 128 bit>"
  }
```

### Run Dockercompose
In the root folder of SafeMessaging run the following command :
```
docker-compose up
```

it will deploy the system with the needed configurations. Make sure that all containers have started.

### Manual Testing

If you want to test the application with 2 users, you need to have one in normal browser, and the other in incognito or in another browser.