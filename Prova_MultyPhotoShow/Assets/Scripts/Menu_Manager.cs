using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField] GameObject _prefab, _optionsObj;
    [SerializeField] Transform _firstPrefab, _parent, _scrollObj; //POSITION FIRST PREF: Vector3(-681,81,0)

    string _scenesPath = "Assets/Scenes";
    string _imagesPath = "Images/Mapping/";
    float _distanceOfPrefabs = 343f; //Distanza tra i prefab (scale base = 1920)
    float _yPosOfPrefabs;
    int _imgCounter = 1; //Counter per verificare quando aumentare le Y invece delle X (Posizione Prefab)
    float _scrollVelocity = 200;

    private void Awake()
    {
        Options_Manager.ResolutionNotSupport();
    }

    // Start is called before the first frame update
    void Start()
    {
        //SET DISTANCE OF PREFAB FROM SCALE (343:1920 = x: screenWidth)
        _distanceOfPrefabs = (Screen.width * 343) / 1920;

        _yPosOfPrefabs = _firstPrefab.position.y;
        int _mappingNumber = SceneManager.sceneCountInBuildSettings;

        for(int i = 1; i < _mappingNumber; i++)
        {
            if(i != 1)
            {
                GameObject _newPrefabObj = Instantiate(_prefab, SetPrefabPosition(), Quaternion.identity, _parent);
                _newPrefabObj.name = $"IMG{i}";
                _newPrefabObj.transform.GetChild(0).name = $"IMG{i}_Text";
                //Debug.Log(_newPrefabObj);
            }

            string _path = SceneUtility.GetScenePathByBuildIndex(i);
            GameObject.Find($"IMG{i}_Text").GetComponent<TextMeshProUGUI>().text = _path.Substring(_scenesPath.Length + 1, _path.Length - 7 - _scenesPath.Length);

            GameObject.Find($"IMG{i}").GetComponent<Image>().sprite = Resources.Load($"{_imagesPath}Mapping_{i}", typeof(Sprite)) as Sprite;
        }
    }

    Vector2 SetPrefabPosition()
    {
        if (_imgCounter == 5)
        {
            _yPosOfPrefabs -= _distanceOfPrefabs; 
            _imgCounter = 1;
            return new Vector2(_firstPrefab.position.x, _yPosOfPrefabs);
        }

        Vector2 newPos = new Vector2(_firstPrefab.position.x + (_distanceOfPrefabs * _imgCounter), _yPosOfPrefabs);
        _imgCounter++;

        /*if (_imgCounter == 6)
            _imgCounter = 1;*/

        return newPos;
    }

    // Update is called once per frame
    void Update()
    {
        ControllerManager();
    }

    void ControllerManager()
    {
        if (_scrollObj.childCount > 11 && Input.GetAxis("Mouse ScrollWheel") != 0f && !Options_Manager.optionsIsOpen) //TODO AGGIUNGERE OPTIONS NON APERTO
        {
            _scrollObj.position = (Input.GetAxis("Mouse ScrollWheel") > 0f && _scrollObj.localPosition.y > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f && _scrollObj.GetChild(_scrollObj.childCount - 1).position.y < Screen.height / 5) ? new Vector3(_scrollObj.position.x, _scrollObj.position.y - Input.GetAxis("Mouse ScrollWheel")* _scrollVelocity) : _scrollObj.position;
        }
    }

    public void OpenOptions()
    {
        Options_Manager.optionsIsOpen = true;
        _optionsObj.SetActive(true);
    }

    public void Escape()
    {
        Application.Quit();
    }
}
