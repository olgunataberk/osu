﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Modes.Catch.Objects;
using osu.Game.Modes.Objects;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.UI;

namespace osu.Game.Modes.Catch.UI
{
    public class CatchHitRenderer : HitRenderer<CatchBaseHit>
    {
        protected override HitObjectConverter<CatchBaseHit> Converter => new CatchConverter();

        protected override Playfield CreatePlayfield() => new CatchPlayfield();

        protected override DrawableHitObject GetVisualRepresentation(CatchBaseHit h,bool dynamic) => null;// new DrawableFruit(h);
    }
}
