using Android.App;
using Android.Widget;
using Android.OS;
using Android.Media;

namespace SoundDemo
{
    [Activity(Label = "SoundDemo", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected MediaPlayer player;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button playShortButton = FindViewById<Button>(Resource.Id.shortPlayButton);
            playShortButton.Click += delegate
            {
                if (player == null)
                {
                    player = MediaPlayer.Create(this, Resource.Raw.splat);
                    player.Start();
                }
                else
                {
                    player.Reset();
                    player = MediaPlayer.Create(this, Resource.Raw.splat);
                    player.Start();
                }
            };

            Button playLongButton = FindViewById<Button>(Resource.Id.longPlayButton);
            playLongButton.Click += delegate
            {
                if (player == null)
                {
                    player = MediaPlayer.Create(this, Resource.Raw.bells);
                    player.Start();
                }
                else
                {
                    player.Reset();
                    player = MediaPlayer.Create(this, Resource.Raw.bells);
                    player.Start();
                }
            };

            Button resumeButton = FindViewById<Button>(Resource.Id.resumeButton);
            resumeButton.Click += delegate
            {
                if (player != null)
                {
                    player.Start();
                }
            };

            Button pauseButton = FindViewById<Button>(Resource.Id.pauseButton);
            pauseButton.Click += delegate
            {
                if (player != null)
                {
                    player.Pause();
                }
            };

            Button stopButton = FindViewById<Button>(Resource.Id.stopButton);
            stopButton.Click += delegate
            {
                if (player != null)
                {
                    player.Stop();
                }
            };

        }
    }
}

