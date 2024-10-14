-- Question 1 --
-- Accounts table to track clients and balances
CREATE TABLE Accounts(
  ID INT NOT NULL IDENTITY,
  LastUpdateUtc DATETIME NOT NULL,
  Account INT NOT NULL UNIQUE,
  Name NVARCHAR(50) NOT NULL,
  Description NVARCHAR(200) NOT NULL,
  Balance DECIMAL(19,4) NOT NULL,                        
  CONSTRAINT PK_Accounts PRIMARY KEY(ID)
  );
GO

-- Function to return account balance, for balance available check
CREATE FUNCTION dbo.MaintainPositiveAccountBalance(@Account INT)
RETURNS int
AS BEGIN RETURN (
  SELECT Balance
  FROM Accounts acc
  WHERE acc.Account = @Account
) END
GO

-- Table to keep track of transactions
CREATE TABLE Transactions(
  ID INT NOT NULL IDENTITY PRIMARY KEY,
  EntryDateUtc DATETIME NOT NULL,
  CreditAccount INT NOT NULL,
  DebitAccount INT NOT NULL,
  Amount DECIMAL(15,2) NOT NULL,
  Description NVARCHAR(100) NOT NULL,
  CONSTRAINT UNQ_Transactions UNIQUE(EntryDateUtc, CreditAccount, DebitAccount),
  CONSTRAINT FK_Transactions_Credit FOREIGN KEY(CreditAccount) REFERENCES Accounts(Account),
  CONSTRAINT FK_Transactions_Debit FOREIGN KEY(DebitAccount) REFERENCES Accounts(Account),
  CONSTRAINT CHK_Transaction_Positive CHECK(dbo.MaintainPositiveAccountBalance(CreditAccount) > Amount)
);
GO

-- Trigger to keep balance updated after a transaction has been inserted
CREATE TRIGGER TR_Transactions_Balance
ON Transactions AFTER INSERT
AS
BEGIN
  UPDATE acc
  SET
    Balance += inserted.Amount
  FROM Accounts acc
  RIGHT JOIN INSERTED on INSERTED.DebitAccount = acc.Account

  UPDATE acc
  SET
    Balance -= inserted.Amount
  FROM Accounts acc
  RIGHT JOIN INSERTED on INSERTED.CreditAccount = acc.Account
END;
GO