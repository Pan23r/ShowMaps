using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MappingGeneral_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ButtonMapping_Manager.isActive && !Zoom_Manager.zoomIsActive && ButtonMapping_Manager.timer < 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("0.Menu");
        }
    }
}
