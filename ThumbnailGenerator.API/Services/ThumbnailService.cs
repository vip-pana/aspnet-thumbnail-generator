using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ThumbnailGenerator.API.Services
{
    public interface IThumbnailService
    {
        byte[] ResizeImageAsync(byte[] imageData);
    }

    public class ThumbnailService : IThumbnailService
    {
        public byte[] ResizeImageAsync(byte[] imageData)
        {
            using (var image = Image.Load(imageData))
            {
                image.Mutate(x => x.Resize(100, 100)); // Resize to 100x100
                using (var stream = new MemoryStream())
                {
                    return stream.ToArray();
                }
            }
        }
    }
}
