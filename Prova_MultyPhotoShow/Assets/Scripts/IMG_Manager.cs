using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IMG_Manager : MonoBehaviour
{
    bool isPress = false; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().onClick.AddListener(OpenScene);
    }

    void OpenScene()
    {
        if (!isPress && !Options_Manager.optionsIsOpen)
        {
            string _sceneName = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text;
            SceneManager.LoadScene(_sceneName);
            isPress = true;
        }
    }
}
