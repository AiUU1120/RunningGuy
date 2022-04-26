using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterFirst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Enterfirst", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Enterfirst()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2 );
    }
}
