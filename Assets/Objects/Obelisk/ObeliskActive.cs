using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObeliskActive : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Animator>().SetBool("Active", true);
            StartCoroutine(MoveToNextLevel());
        }
    }
    private IEnumerator MoveToNextLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
}
