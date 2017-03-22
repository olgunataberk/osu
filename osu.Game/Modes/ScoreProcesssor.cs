﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Configuration;
using osu.Game.Modes.Objects.Drawables;
using osu.Framework.Logging;

namespace osu.Game.Modes
{
    public abstract class ScoreProcessor
    {
        public virtual Score GetScore() => new Score()
        {
            MaxScore = MaxScore,
            TotalScore = TotalScore,
            Combo = Combo,
            MaxCombo = HighestCombo,
            Accuracy = Accuracy,
            Health = Health,
        };

        protected double scoreMultiplier = 1;

        public readonly BindableDouble MaxScore = new BindableDouble { MinValue = 0 };
        
        public readonly BindableDouble TotalScore = new BindableDouble { MinValue = 0 };

        public readonly BindableDouble Accuracy = new BindableDouble { MinValue = 0, MaxValue = 1 };

        public readonly BindableDouble Health = new BindableDouble { MinValue = 0, MaxValue = 1 };

        public readonly BindableInt Combo = new BindableInt();

        /// <summary>
        /// Are we allowed to fail?
        /// </summary>
        protected bool CanFail => true;

        protected bool HasFailed { get; private set; }

        /// <summary>
        /// Called when we reach a failing health of zero.
        /// </summary>
        public event Action Failed;

        /// <summary>
        /// Keeps track of the highest combo ever achieved in this play.
        /// This is handled automatically by ScoreProcessor.
        /// </summary>
        public readonly BindableInt HighestCombo = new BindableInt();

        public readonly List<JudgementInfo> Judgements;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreProcessor"/> class.
        /// </summary>
        /// <param name="hitObjectCount">Number of HitObjects. It is used for specifying Judgements collection Capacity</param>
        public ScoreProcessor(int hitObjectCount = 0)
        {
            Combo.ValueChanged += delegate { HighestCombo.Value = Math.Max(HighestCombo.Value, Combo.Value); };
            Judgements = new List<JudgementInfo>(hitObjectCount);
        }

        public void setScoreMultiplier(double sm)
        {
            scoreMultiplier = sm;
        }

        public void addScore(double sc)
        {
            TotalScore.Value += sc*scoreMultiplier;
        }

        public void addMaxScore(double sc)
        {
            MaxScore.Value += sc * scoreMultiplier;
        }

        //judgement has TimeOffset information
        public void AddJudgement(JudgementInfo judgement)
        {
            Judgements.Add(judgement);

            UpdateCalculations(judgement);

            judgement.ComboAtHit = (ulong)Combo.Value;
            if (Health.Value == Health.MinValue && !HasFailed)
            {
                HasFailed = true;
                Failed?.Invoke();
            }
        }

        /// <summary>
        /// Update any values that potentially need post-processing on a judgement change.
        /// </summary>
        /// <param name="newJudgement">A new JudgementInfo that triggered this calculation. May be null.</param>
        protected abstract void UpdateCalculations(JudgementInfo newJudgement);

        public abstract double getFirstMissTimeStamp();
    }
}
