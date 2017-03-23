// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;

namespace osu.Game.Modes.Osu.Objects
{
    public class Spinner : OsuHitObject
    {
        public double Length;

        public override double EndTime => StartTime + Length;

        public void resize()
        {
            float multiplier = (float)osu.Framework.MathUtils.RNG.NextDouble(0.1, 3);
            Scale *= multiplier;
        }
    }
}
