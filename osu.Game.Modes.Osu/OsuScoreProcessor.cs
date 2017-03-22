// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.Osu.Objects.Drawables;

namespace osu.Game.Modes.Osu
{
    class OsuScoreProcessor : ScoreProcessor
    {

        private double firstMissTimeStamp;

        public OsuScoreProcessor(int hitObjectCount)
            : base(hitObjectCount)
        {
            Health.Value = 1;
            firstMissTimeStamp = 0;
        }

        protected override void UpdateCalculations(JudgementInfo judgement)
        {
            if (judgement != null)
            {
                switch (judgement.Result)
                {
                    case HitResult.Hit:
                        Combo.Value++;
                        Health.Value += 0.1f;
                        break;
                    case HitResult.Miss:
                        if (firstMissTimeStamp <= 0) firstMissTimeStamp = judgement.TimeStamp;
                        Combo.Value = 0;
                        Health.Value -= 0.2f;
                        break;
                }
            }

            /*
            int score = 0;
            int maxScore = 0;

            foreach (OsuJudgementInfo j in Judgements)
            {
                score += j.ScoreValue;
                maxScore += j.MaxScoreValue;
            }
            */
            int score = (judgement as OsuJudgementInfo).ScoreValue;
            int maxScore = (judgement as OsuJudgementInfo).MaxScoreValue;
            TotalScore.Value += score * scoreMultiplier;
            MaxScore.Value += maxScore * scoreMultiplier;
            Accuracy.Value = TotalScore.Value/MaxScore.Value;
        }

        public override double getFirstMissTimeStamp() => firstMissTimeStamp;
    }
}
