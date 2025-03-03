using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inworld.UI
{
    /// <summary>
    /// PotionBrewed will be hooked to the OnBrew event of the CauldronContent in the scene, and this script will take care
    /// of dispatching the right prefab to the ObjectSpawner to make the potion spawn
    /// </summary>
    /// 
    public class PotionSpawner : MonoBehaviour
    {
        public GameObject PotionPrefab;
        public GameObject GhostToy;
        public GameObject BadPotionPrefab;
        public ObjectSpawner SpawnerCorrect;
        public ObjectSpawner SpawnerIncorrect;
        private void Start()
        {

        }
        public void PotionBrewed(CauldronContent.Recipe recipe)
        {
            if (recipe != null)
            {
                SpawnerCorrect.Prefab = PotionPrefab;
                if (recipe.name == "Meat Boy")
                {
                    SpawnerCorrect.Prefab = GhostToy;
                }
                SpawnerCorrect.enabled = true;
                SpawnerCorrect.Spawn();
            }
            else
            {
                SpawnerIncorrect.Prefab = BadPotionPrefab;
                SpawnerIncorrect.enabled = true;
                SpawnerIncorrect.Spawn();
            }
        }
    }
}
