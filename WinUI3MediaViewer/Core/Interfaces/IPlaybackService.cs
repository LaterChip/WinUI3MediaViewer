namespace WinUI3MediaViewer.Core.Interfaces;

public interface IPlaybackService
{
    void Play();
    void Pause();
    void Stop();
    void SetSource(string filePath);
    double Position { get; set; }
    double Duration { get; }
    bool IsPlaying { get; }
}
