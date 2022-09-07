using Models;

namespace Services;

public interface IVideosService
{
    Video Get(string id);
    List<Video> Get();
    void Delete(string id);
    Video Put(Video video);
}