﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using OpenTK;
using osu.Framework.Graphics.Transformations;
using osu.Game.Beatmaps;
using osu.Game.Graphics.Backgrounds;

namespace osu.Game.Screens.Backgrounds
{
    public class BackgroundScreenBeatmap : BackgroundScreen
    {
        private Background background;

        private WorkingBeatmap beatmap;
        private Vector2 blurTarget;

        public WorkingBeatmap Beatmap
        {
            get
            {
                return beatmap;
            }
            set
            {
                if (beatmap == value && beatmap != null)
                    return;
                beatmap = value;

                Schedule(() =>
                {
                    Background newBackground;
                    if (beatmap == null)
                        newBackground = new Background(@"Backgrounds/bg1");
                    else
                        newBackground = new BeatmapBackground(beatmap);

                    newBackground.Preload(Game, delegate
                    {
                        float newDepth = 0;
                        if (background != null)
                        {
                            newDepth = background.Depth + 1;
                            background.Flush();
                            background.FadeOut(250);
                            background.Expire();
                        }

                        newBackground.Depth = newDepth;
                        Add(background = newBackground);
                        background.BlurSigma = blurTarget;
                    });
                });
            }
        }

        public BackgroundScreenBeatmap(WorkingBeatmap beatmap)
        {
            Beatmap = beatmap;
        }

        public void BlurTo(Vector2 sigma, double duration, EasingTypes easing = EasingTypes.None)
        {
            background?.BlurTo(sigma, duration, easing);
            blurTarget = sigma;
        }

        public override bool Equals(BackgroundScreen other)
        {
            return base.Equals(other) && beatmap == ((BackgroundScreenBeatmap)other).Beatmap;
        }

        class BeatmapBackground : Background
        {
            private WorkingBeatmap beatmap;

            public BeatmapBackground(WorkingBeatmap beatmap)
            {
                this.beatmap = beatmap;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                Sprite.Texture = beatmap?.Background;
            }
        }
    }
}
