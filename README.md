# SafeMessaging

SafeMessaging is a secure messaging application built with .NET Web API back, an angular 19 frontend and a postgres 17 database. The project prioritizes security, efficiency, and scalability, making it ideal for modern communication needs.

## Features

- **Secure Messaging:** Ensures end-to-end encryption for secure communication.
- **Modern Frontend:** Built using Angular for a responsive, interactive, and high-performance user interface.
- **Scalable Backend:** Powered by .NET for robust and scalable backend services.
- **Configurable Database Connection:** Easily configurable PostgreSQL database connection string.

## Technologies Used

- **.NET**: Backend development and core application logic.
- **Angular**: Frontend framework for building backend agnostic web UIs.
- **PostgreSQL**: Database for storing application data.

## Getting Started

### Prerequisites

Before running the project, ensure you have the following installed:

- The Docker CLI and/ or Docker Desktop.

### Installation

Clone the repository to your local machine
   ```bash
   git clone https://github.com/theut94/safe-messaging.git
   cd safe-messaging
   ```


### Run Dockercompose

In the root folder of SafeMessaging run the following command
```
docker-compose up
```

it will deploy the system with the needed configurations. Make sure that all containers have started. Docker will map the frontend to localhost port 4200.
