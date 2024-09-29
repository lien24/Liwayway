using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private bool allEnemiesDead = false;

    // Reference to all enemy children (EnemyK, EnemyB, Fairy, Boss)
    private List<Enemy> enemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {

        // Find all the child enemies (Enemy scripts) under the AllEnemy parent
        foreach (Transform child in transform)
        {
            Enemy enemyScript = child.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemies.Add(enemyScript);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Continuously check if all enemies are dead
        if (!allEnemiesDead)
        {
            CheckAllEnemiesDead();
        }
    }

    void CheckAllEnemiesDead()
    {
        foreach (Enemy enemy in enemies)
        {
            // If at least one enemy is alive, return early
            if (enemy.currentHealth > 0)
            {
                return;
            }
        }

        // If all enemies are dead, set the flag and load the winning scene
        allEnemiesDead = true;
        StartCoroutine(LoadWinningScene());
    }

    IEnumerator LoadWinningScene()
    {
        // Optionally add a delay before showing the winning scene
        yield return new WaitForSeconds(5f);  // Adjust the delay as needed

        // Load the winning scene (replace with the appropriate scene index or name)
        SceneManager.LoadScene(3);  // Change "WinningScene" to your actual scene name or index
    }

}
