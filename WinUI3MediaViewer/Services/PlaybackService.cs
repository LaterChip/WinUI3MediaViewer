using System;
using Microsoft.UI.Xaml.Controls;
using WinUI3MediaViewer.Core.Interfaces;

namespace WinUI3MediaViewer.Services;

public class PlaybackService : IPlaybackService
{
    private MediaPlayerElement? _mediaPlayerElement;

    public void Attach(MediaPlayerElement mediaPlayerElement)
    {
        _mediaPlayerElement = mediaPlayerElement;
        if (_mediaPlayerElement?.MediaPlayer != null)
        {
            _mediaPlayerElement.MediaPlayer.MediaEnded += (s, e) => { /* 循环处理 */ };
        }
    }

    public void SetSource(string filePath)
    {
        if (_mediaPlayerElement?.MediaPlayer != null)
        {
            _mediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri(filePath));
        }
    }

    public void Play() => _mediaPlayerElement?.MediaPlayer?.Play();
    public void Pause() => _mediaPlayerElement?.MediaPlayer?.Pause();
    public void Stop() => _mediaPlayerElement?.MediaPlayer?.Stop();

    public double Position
    {
        get => _mediaPlayerElement?.MediaPlayer?.Position.TotalSeconds ?? 0;
        set
        {
            if (_mediaPlayerElement?.MediaPlayer != null)
                _mediaPlayerElement.MediaPlayer.Position = TimeSpan.FromSeconds(value);
        }
    }

    public double Duration => _mediaPlayerElement?.MediaPlayer?.NaturalDuration.TotalSeconds ?? 0;
    public bool IsPlaying => _mediaPlayerElement?.MediaPlayer?.PlaybackSession.PlaybackState == Windows.Media.Playback.MediaPlaybackState.Playing;
}
