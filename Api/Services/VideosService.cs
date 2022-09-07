using Models;

namespace Services;

public class VideosService : IVideosService
{
    private Repositories.IDatabaseItemRepository<Video> _videosRepo;

    public VideosService(Repositories.IDatabaseItemRepository<Video> videosRepo)
    {
        _videosRepo = videosRepo;
    }

    public void Delete(string id)
    {
        _videosRepo.Delete(id);
    }

    public Video Get(string id)
    {
        return _videosRepo.Get(id);
    }

    public List<Video> Get()
    {
        return _videosRepo.Get();
    }

    public Video Put(Video video)
    {
        return _videosRepo.Put(video);
    }
}