using System;
using System.Collections;
using UnityEngine;

namespace Towers
{
    public class ReloadableTower : TowerController
    {
        protected bool shootAvailable;
        protected bool reloading;

        public float attackFrequency = 1f;
        [Header("Upgrade")]
        [SerializeField]
        public float attackFrequencyUpgradeScale = .08f;
        

        protected override void Start()
        {
            base.Start();
            OnAdditionalUpgradeTextFormat += AdditionalUpgradeTextFormat;
            OnUpgrade += Upgrade;
        }

        private new void Upgrade()
        {
            attackFrequency -= attackFrequency * attackFrequencyUpgradeScale;
        }

        private string AdditionalUpgradeTextFormat()
        {
            var upgraded = attackFrequency * attackFrequencyUpgradeScale;
            var w = $"Скорострельность: {attackFrequency:F1}с - {upgraded:F1}с\n";
            return w;
        }

        protected override void PerformShoot()
        {
            if (shootAvailable && Rotated)
            {
                PerformShootWithReload();
                shootAvailable = false;
                Rotated = false;
            }
            else if (!reloading && !shootAvailable)
            {
                StartCoroutine(Reload());
            }
        }

        protected virtual void PerformShootWithReload()
        {
            throw new NotImplementedException();
        }

        private IEnumerator Reload()
        {
            reloading = true;
            yield return new WaitForSeconds(attackFrequency);
            reloading = false;
            shootAvailable = true;
        }
    }
}