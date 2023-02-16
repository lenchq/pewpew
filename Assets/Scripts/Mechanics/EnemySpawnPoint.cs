using Models;
using UnityEngine;


public class EnemySpawnPoint : MonoBehaviour
{
    // [SerializeField]
    // private GameObject enemyRecruit;
    // [SerializeField]
    // private GameObject enemyArmed;
    // [SerializeField]
    // private GameObject enemyFast;

    [SerializeField]
    public CreepPathController SelectedPath;
    
    

    void Start()
    {

    }

    // public void SpawnEnemy(Models.EnemyType enemyType)
    // {
    //     if (enemyType == EnemyType.Armed)
    //     {
    //         this.CreateEnemy(enemyArmed);
    //     }
    // }

    // private void CreateEnemy(GameObject enemy)
    // {
    //     var e = (GameObject)Instantiate(enemy, this.transform);
    //     var enemyController = e.GetComponent<CreepController>();
    //     enemyController.creepPathController = SelectedPath;
    // }

    // Update is called once per frame
    void Update()
    {

    }
}