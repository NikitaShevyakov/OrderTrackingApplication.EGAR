using EGAR.SharedKernel.Enums;

namespace EGAR.Infrastructure.Data.MsSQL.Extensions;

public static class ErrorTypeMessagesExtensions
{
    public static string GetUserFriendlyMessage(this ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => "Invalid data provided.",
            ErrorType.NotFound => "Resource not found.",
            ErrorType.Conflict => "Data conflict occurred.",
            ErrorType.Unauthorized => "Not authorized.",
            ErrorType.Forbidden => "Access forbidden.",
            ErrorType.DbConnection => "Database connection failed.",
            ErrorType.DbTimeout => "Operation timed out.",
            ErrorType.DbConcurrency => "Data was modified by another user.",
            ErrorType.DbConstraint => "Database constraint violation.",
            _ => "An error occurred."
        };
    }
}
