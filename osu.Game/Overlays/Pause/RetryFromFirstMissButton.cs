using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Game.Graphics;

namespace osu.Game.Overlays.Pause
{
    public class RetryFromFirstMissButton : RetryButton
    {
        //load function of RetryButton
        [BackgroundDependencyLoader]
        private void load(AudioManager audio, OsuColour colours)
        {
            ButtonColour = colours.YellowDark;
            SampleHover = audio.Sample.Get(@"Menu/menuclick");
            SampleClick = audio.Sample.Get(@"Menu/menu-play-click");
        }

        public RetryFromFirstMissButton()
        {
            Text = @"Start from first miss";
        }
    }
}
