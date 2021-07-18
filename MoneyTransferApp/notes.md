# Get All accounts
GET /accounts 
    {
        UserID      int
        AccountName string
    }

# Get MY balance
GET /account/:id/balance
    {
        Balance int
    }

# Send money to user
POST /account/:id/transfer
    {
        Amount      decimal
        From   id
        Reciever
        Type        int
    }

    Returns Approve /    Disapprove
            Ok      /    BadRequest

# Get All transactions for MY account
GET /account/:id/transfers
    {
        TransferID int
        OtherParty int
        Type       int
        Amount     decimal
    }

# Get specific transfer details
GET /account/:AccountId/transfers/:UserId
    {
        TransferId int
        OtherParty int
        Type       int
        Amount     decimal
        Status     int
    }


