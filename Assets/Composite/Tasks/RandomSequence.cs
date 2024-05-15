/**
 * Do what sequence do only on start it shuffles the composite collection.
 */

using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Wave.Engine.Composite.Enums;
using URandom = UnityEngine.Random;

namespace Wave.Engine.Composite.Tasks
{
    public class RandomSequence : SequenceSelector
    {
        public int Seed;
        public bool UseSeed;
        public bool ShuffleInEachRun;

        public override void Awake()
        {
            base.Awake();

            ShuffleCollection(Seed);
        }

        public override async UniTask<CompositeResult> Run(CancellationToken cancellationToken = default)
        {
            if (ShuffleInEachRun == true)
                ShuffleCollection(Seed);

            return await base.Run(cancellationToken);
        }


        private void ShuffleCollection(int seed)
        {
            URandom.State lastState = URandom.state;

            if (UseSeed)
                URandom.InitState(seed);
            else
                URandom.InitState((int)DateTime.Now.Ticks);

            List<CompositeObject> collection = GetCompositeCollection()
                .OrderBy(x => URandom.value)
                .ToList();
            
            URandom.state = lastState;

            LoadCompositeCollection(collection);
        }
    }
}
