// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;

namespace osu.Game.Overlays.Options.Sections.Gameplay
{
    public class GeneralOptions : OptionsSubsection
    {
        protected override string Header => "General";

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new OptionSlider<int>
                {
                    LabelText = "Background dim",
                    Bindable = (BindableInt)config.GetBindable<int>(OsuConfig.DimLevel)
                },
                new OptionEnumDropDown<ProgressBarType>
                {
                    LabelText = "Progress display",
                    Bindable = config.GetBindable<ProgressBarType>(OsuConfig.ProgressBarType)
                },
                new OptionEnumDropDown<ScoreMeterType>
                {
                    LabelText = "Score meter type",
                    Bindable = config.GetBindable<ScoreMeterType>(OsuConfig.ScoreMeter)
                },
                new OptionSlider<double>
                {
                    LabelText = "Score meter size",
                    Bindable = (BindableDouble)config.GetBindable<double>(OsuConfig.ScoreMeterScale)
                },
                new OsuCheckbox
                {
                    LabelText = "Always show key overlay",
                    Bindable = config.GetBindable<bool>(OsuConfig.KeyOverlay)
                },
                new OsuCheckbox
                {
                    LabelText = "Show approach circle on first \"Hidden\" object",
                    Bindable = config.GetBindable<bool>(OsuConfig.HiddenShowFirstApproach)
                },
                new OsuCheckbox
                {
                    LabelText = "Scale osu!mania scroll speed with BPM",
                    Bindable = config.GetBindable<bool>(OsuConfig.ManiaSpeedBPMScale)
                },
                new OsuCheckbox
                {
                    LabelText = "Remember osu!mania scroll speed per beatmap",
                    Bindable = config.GetBindable<bool>(OsuConfig.UsePerBeatmapManiaSpeed)
                },
                new OsuCheckbox
                {
                    LabelText = "Dynamic circle size enable",
                    Bindable = config.GetBindable<bool>(OsuConfig.DynamicCircleSize)
                },
                new OptionSlider<int>
                {
                    LabelText = "Dynamic size maximum size: ",
                    Bindable = (BindableInt)config.GetBindable<int>(OsuConfig.DynamicLevelMax)
                },
                new OptionSlider<int>
                {
                    LabelText = "Dynamic size minimum size: (0 for 1, 1 for 0.75, 2 for 0.5, 3 for 0.25, 4 for 0.1)",
                    Bindable = (BindableInt)config.GetBindable<int>(OsuConfig.DynamicLevelMin)
                },
            };
        }
    }
}
