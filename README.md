# Financial Management 

REST service for managing financial accounts.

## General
- The service utilizes authentication and authorization.
- Users can modify the balance (depositing and withdrawing money) of specified accounts.
- An unlimited number of currencies can be created in the system.
- Each user can have only one account per currency.
- The exchange system should transfer money from one account to another, converting from the first currency to the second at the appropriate exchange rate if necessary. The exchange rate is always a positive number not equal to zero. When converting from one user's account to another, the entered amount in the first currency is debited, and the calculated amount in the second currency is credited, minus a commission. The commission is calculated as the amount received in the second currency multiplied by the commission percentage. The commission percentage is a positive number greater than zero (default is 0.05%). The history of transfers, deposits, and withdrawals should be saved in the database.

## Functionality
1. Register and authenticate a new user.
2. Retrieve all user accounts by their Id.
3. Retrieve information on all user's financial transactions by their Id.
4. Retrieve information on a specific user account by account Id.
5. Create an account.
6. Retrieve a list of available currencies.
7. Deposit and withdraw money to/from an account.
8. Add a new currency to the system.
9. Retrieve all commissions.
10. Transfer money from one account to another (converting if necessary).
11. Add a new commission for currency exchange.
12. Retrieve all financial transactions related to a specific account by its Id.
