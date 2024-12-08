using UnityEngine;

namespace Assets.Scripts.Buildings
{
    public class Mine : Building
    {
        private int count = 0;

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

        public override void OnWorldTick()
        {
            count++;
            if (count >= 100)
            {
                Debug.Log("Stone added");
                WorldParent.AddTickResources(GameResources.ResourceStone, 1);
                count = 0;
            }
        }
    }
}
