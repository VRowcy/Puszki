using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;
public class ScoreManager : MonoBehaviour
{
    GameObject text;
    public GameObject structure;
    bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("Text");
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        int count = 0;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Cylinder");
        foreach (GameObject gameObject in objects)
        {
            Debug.Log("1" + Vector3.Dot(gameObject.transform.up, Vector3.right));
            Debug.Log("2" + Vector3.Dot(gameObject.transform.up, Vector3.down));
            Debug.Log("0" + Vector3.Dot(gameObject.transform.up, Vector3.left));
            Debug.Log("3" + Vector3.Dot(gameObject.transform.up, Vector3.forward));
            Debug.Log("4" + Vector3.Dot(gameObject.transform.up, Vector3.back));
            if ((Vector3.Dot(gameObject.transform.up, Vector3.right) > 0.5f
                || Vector3.Dot(gameObject.transform.up, Vector3.down) > 0
                || Vector3.Dot(gameObject.transform.up, Vector3.left) > 0.5f
                || Vector3.Dot(gameObject.transform.up, Vector3.forward) > 0.5f
                || Vector3.Dot(gameObject.transform.up, Vector3.back) > 0.5f)||gameObject.transform.position.y<0.45)
            {
                count++;
                if (count == objects.Length && !win)
                {
                    Debug.Log("win");
                    StartCoroutine(waiter());
                }
            }    
        }
        
    }
    IEnumerator waiter()
    {
        Destroy(GameObject.FindGameObjectWithTag("Structure"));
        win = true;
        text.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        foreach (Hand hand in GameObject.FindObjectsOfType<Hand>())
        {
            GameObject gameObject = hand.currentAttachedObject;
            hand.DetachObject(gameObject, false);
            Destroy(gameObject);
        }
        text.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }
}
