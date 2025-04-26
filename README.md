# CryptoTrader
An assignment for the Advanced Programming II. course. The topic of the assignment is a cryptocurrency trade site simulator.

# API Endpoints


## User

| Method | Endpoint                     | Description                              |
|--------|------------------------------|------------------------------------------|
| GET    | `/api/users/{UserId}`        | Get User details                         |
| PUT    | `/api/users/{UserId}`        | Update user Username,Email and password  |
| POST   | `/api/users/register`        | Register a new user                      |
| POST   | `/api/users/login`           | Login a user                             |
| DELETE | `/api/users/Delete`          | Delete a user                            |

---


## Wallet

| Method | Endpoint                     | Description                              |
|--------|------------------------------|------------------------------------------|
| GET    | `/api/wallet/{UserId}`       | Get wallet matches UserId                |
| PUT    | `/api/wallet/{UserId}`       | Top-up wallet balance                    |
| DELETE | `/api/wallet/{UserId}`       | Delete a wallet of matching UserId       |

---


## Crypto currencies

| Method | Endpoint                     | Description                              |
|--------|------------------------------|------------------------------------------|
| GET    | `/api/cryptos`               | List all available cryptos               |
| POST   | `/api/cryptos`               | Create a new Crypto                      |
| GET    | `/api/cryptos/{cryptoid}`    | Get details of a Crypto matches cryptoid |
| DELETE | `/api/cryptos/{cryptoid}`    | Delete a crypto                          |

---


