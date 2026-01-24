using EGAR.SharedKernel.Enums;
using Microsoft.EntityFrameworkCore;

namespace EGAR.Infrastructure.Data.MsSQL.Repositories;

public static class DatabaseErrorResolver
{
    public static ErrorType ResolveFromException(Exception ex)
    {
        if (ex is DbUpdateException dbEx) return ResolveDbUpdateException(dbEx);

        if (ex is TimeoutException 
            || ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase))        
            return ErrorType.DbTimeout;        

        if (ex.Message.Contains("connection", StringComparison.OrdinalIgnoreCase) 
            || ex.Message.Contains("connect", StringComparison.OrdinalIgnoreCase))        
            return ErrorType.DbConnection;        

        return ErrorType.Internal;
    }

    private static ErrorType ResolveDbUpdateException(DbUpdateException ex)
    {
        var message = ex.InnerException?.Message ?? ex.Message;
        message = message.ToLowerInvariant();

        if (message.Contains("unique") || message.Contains("duplicate")) return ErrorType.Conflict;
        if (message.Contains("foreign") || message.Contains("reference")) return ErrorType.DbConstraint;
        if (message.Contains("concurrency")) return ErrorType.DbConcurrency;

        return ErrorType.DbConstraint;
    }
}
