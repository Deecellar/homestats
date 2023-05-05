using backend.Data;
using MediatR;

namespace backend.Middleware;

public class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            // if is UnitOfWorkSqlKata we initializee the connection
            if(_unitOfWork is UnitOfWorkSqlKata unitOfWorkSqlKata)
                unitOfWorkSqlKata.QueryFactory.Connection.Open();

            if (!await _unitOfWork.InitializeTransaction()) throw new Exception("Error initializing transaction");
            var response = await next();

            if (!await _unitOfWork.CommitTransaction()) throw new Exception("Error saving changes");
            return response;
        }
        catch
        {
            await _unitOfWork.RollbackTransaction();
            throw;
        }
    }
}