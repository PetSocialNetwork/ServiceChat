namespace ServiceChat.Domain.Interfaces
{
    public interface IUserPhotoService
    {
        Task AddAndSetPersonalPhotoAsync(Guid userProfileId, byte[] photo, string originalFileName, CancellationToken cancellationToken);
    }
}
