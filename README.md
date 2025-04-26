# CryptoTrader 🚀
An assignment for the Advanced Programming II. course. The topic of the assignment is a cryptocurrency trade site simulator.

# API Endpoints 🛣️ 


## User 👤

| Method | Endpoint                     | Description                              |
|--------|------------------------------|------------------------------------------|
| GET    | `/api/users/{UserId}`        | Get User details                         |
| PUT    | `/api/users/{UserId}`        | Update user Username,Email and password  |
| POST   | `/api/users/register`        | Register a new user                      |
| POST   | `/api/users/login`           | Login a user                             |
| DELETE | `/api/users/Delete`          | Delete a user                            |

---


## Wallet 💰

| Method | Endpoint                     | Description                              |
|--------|------------------------------|------------------------------------------|
| GET    | `/api/wallet/{UserId}`       | Get wallet matches UserId                |
| PUT    | `/api/wallet/{UserId}`       | Top-up wallet balance                    |
| DELETE | `/api/wallet/{UserId}`       | Delete a wallet of matching UserId       |

---


## Crypto manageing 🛠️

| Method | Endpoint                     | Description                              |
|--------|------------------------------|------------------------------------------|
| GET    | `/api/cryptos`               | List all available cryptos               |
| POST   | `/api/cryptos`               | Create a new Crypto                      |
| GET    | `/api/cryptos/{cryptoid}`    | Get details of a Crypto matches cryptoid |
| DELETE | `/api/cryptos/{cryptoid}`    | Delete a crypto                          |

---


## Crypto Trade 💱

| Method | Endpoint                     | Description                              |
|--------|------------------------------|------------------------------------------|
| POST   | `/api/trade/buy`             | User can buy crypto                      |
| POST   | `/api/trade/sell`            | User can sell crypto                     |
| GET    | `/api/portfolio/{userid}`    | Get  a user's portfolio                  |


---


## Crypto Exchange Rate 📈

| Method | Endpoint                              | Description                              |
|--------|---------------------------------------|------------------------------------------|
| PUT    | `/api/crypto/price`                   | Update Exchange rate                     |
| GET    | `/api/crypto/price/history/{cryptoid}`| Exchange rate logs for specified crypto  |

---


## Profit/Loss Calculation 📊

| Method | Endpoint                      | Description                              |
|--------|-------------------------------|------------------------------------------|
| GET    | `/api/profit/{userid}`        | Get overall Earnings                     |
| GET    | `/api/profit/details/{userid}`| Get detailed Earnings                    |

---


## Transaction Log 📜

| Method | Endpoint                                   | Description                              |
|--------|--------------------------------------------|------------------------------------------|
| GET    | `/api/transactions/{userid}`               | Get User's transaction logs              |
| GET    | `/api/transactions/details/{transactionid}`| Get detailes of a transaction            |


---

# Notes 📚

On the first launch, the application automatically fills the database with sample data.
Among the users, the one with the email address bob@samplemail.com has Admin privileges, while all other users have only User privileges. 
The password for all users is **Almafa123**. <br>
Every 60 seconds, the application automatically fetches the latest crypto exchange rates via an API.


