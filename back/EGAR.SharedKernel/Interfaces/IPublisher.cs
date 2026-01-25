namespace EGAR.SharedKernel.Publishers;

public interface IPublisher
{
    Task<bool> PublishAsync(object @event, CancellationToken cancellationToken = default);
}