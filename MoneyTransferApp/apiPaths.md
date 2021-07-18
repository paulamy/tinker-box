# Get All accounts
# Public Account Model 
GET /accounts 
    {
        AccountId   int
        UserId      int
        Username string
    }

# Get Account by Id
# Private Account Model
GET /account/:id
    {
        AccountId int
        UserId  int
        Balance decimal
    }

# Get All Transfers
# Transfer Model
GET /accounts/:id/transfers
{
    TransferId int
    TransferTypeId int
    TransferStatusId int
    AccountFrom int
    AccountTo int
    Amount decimal 
}

# Get Pending Transfers
# Transfer Model - statusId = 1
GET /accounts/:id/transfers/pending
{
    TransferId int
    TransferTypeId int
    TransferStatusId int
    AccountFrom int
    AccountTo int
    Amount decimal 
}

# Get Transfer by ID
# Transfer Model
GET /accounts/:id/transfers/:id
{
    TransferId int
    TransferTypeId int
    TransferStatusId int
    AccountFrom int
    AccountTo int
    Amount decimal 
}


# Send transfer to user
# Transfer Model
POST /account/:id/transfers
    {
        Amount      decimal
        To id       int
        Type        int
    }

    Returns Approve /    Disapprove
            Ok      /    BadRequest

# Get All Users
# User Model (switch to ReturnUser for security?)
GET /users
{
        UserID int
        Username String
        PasswordHash string
        Salt string
        Email string
}

# Get User By ID
# User Model (switch to ReturnUser for security?)
GET /users/id
{
        UserID int
        Username String
        PasswordHash string
        Salt string
        Email string
}

### Not Implemented Yet

# Get Type Description 
# Type Description Model
GET /description/type
{
    TransferTypeId int
    TransferTypeDescription string
}

# Get Status Description 
# Status Description Model
GET /description/status
{
    TransferStatusId int
    TransferStatusDescription string
}

# Request Transfer from user 
# Transfer Model
POST /accounts/:id/transfers/
    {
        Amount      decimal
        From  id    int
        Type        int
    }

    Returns Approve /    Disapprove
            Ok      /    BadRequest

# Approve or Reject a Requested Transfer
# Transfer Model
PUT /accounts/:id/transfers/:id
    {
        Status id   int
    }

    Returns Approve /    Disapprove
            Ok      /    BadRequest