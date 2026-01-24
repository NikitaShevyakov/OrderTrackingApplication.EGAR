namespace EGAR.SharedKernel.Enums;
public enum ErrorType
{
    None = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Unauthorized = 4,
    Forbidden = 5,
    Internal = 6,

    // Database errors
    DbConnection = 7,
    DbTimeout = 8,
    DbConcurrency = 9,
    DbConstraint = 10
}