﻿using System;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace PitchDetector
{
    public class SvmVowelClassifier : VowelClassifier
    {
        private readonly MulticlassSupportVectorLearning<Linear> _teacher = new MulticlassSupportVectorLearning<Linear>();

        public override void Learn()
        {
            var inputs = new double[this.TrainingData.Count][];
            var classes = new int[this.TrainingData.Count];

            for (var i = 0; i < this.TrainingData.Count; i++)
            {
                var (x, y) = this.TrainingData[i];
                inputs[i] = x;
                classes[i] = (int)y;
            }

            this._teacher.Learn(inputs, classes);
        }

        public override VowelType Decide(ReadOnlySpan<float> samples, int sampleRate)
        {
            var input = new MfccAccord(sampleRate, samples.Length, 0, 8000, 24).ComputeMfcc12D(samples);
            return (VowelType)this._teacher.Model.Decide(input);
        }
    }
}
