using System.Threading.Tasks;

namespace Realtime.Presenter.Function.Files.Readers
{
    public interface IBlobReader
    {
        Task<byte[]> Read(string blobName);
    }
}