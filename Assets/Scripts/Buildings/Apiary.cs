using UnityEngine;

namespace Assets.Scripts.Buildings
{
    public class Apiary : Building
    {
        new private void Start()
        {
            base.Start();
            WorldParent.OnTick += OnWorldTick;
        }

        private void OnEnable()
        {
            if (started) WorldParent.OnTick += OnWorldTick;
        }

        private void OnDisable()
        {
            if (started) WorldParent.OnTick -= OnWorldTick;
        }

        private int collectedHoney = 0;

        public override void OnWorldTick()
        {
            collectedHoney++;
            if (collectedHoney >= 100)
            {
                Debug.Log("honey added");
                WorldParent.AddTickResources(GameResources.ResourceHoney, 1);
                collectedHoney = 0;
            }
        }
    }
}