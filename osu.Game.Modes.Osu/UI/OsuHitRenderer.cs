// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Modes.Objects;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.Osu.Objects;
using osu.Game.Modes.Osu.Objects.Drawables;
using osu.Game.Modes.UI;

namespace osu.Game.Modes.Osu.UI
{
    public class OsuHitRenderer : HitRenderer<OsuHitObject>
    {
        protected override HitObjectConverter<OsuHitObject> Converter => new OsuHitObjectConverter();

        protected override Playfield CreatePlayfield() => new OsuPlayfield();

        protected override DrawableHitObject GetVisualRepresentation(OsuHitObject h,bool dynamic, int levelMin, int levelMax)
        {
            if (h is HitCircle)
            {
                DrawableHitCircle d = new DrawableHitCircle(h as HitCircle);
                if(dynamic )
                    d.resize(levelMin,levelMax);
                return d;
            }
            if (h is Slider)
            {

                 DrawableSlider x = new DrawableSlider(h as Slider);
                if(dynamic  )
                     x.resize(levelMin,levelMax);
                  
               return x ;
            }
            if (h is Spinner)
            {
                DrawableSpinner s = new DrawableSpinner(h as Spinner);
                if (dynamic )
                    s.resize(levelMin,levelMax);

                
                return s;
            }
            return null;
        }
    }
}
