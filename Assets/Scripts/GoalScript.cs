using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
    public GameObject wonText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("collided with player");
            wonText.SetActive(true);
            StartCoroutine(RestartGame());
        }
    }



    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3f); // after 3 seconds, reload current scene
        // reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
