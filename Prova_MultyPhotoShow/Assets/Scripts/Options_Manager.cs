using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options_Manager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _resolutionText;
    [SerializeField] Button _resolutionButton, _fullScreenButton;
    [SerializeField] GameObject _checkOBJ;

    public static bool optionsIsOpen = false;

    static Vector2[] _allResolutions = new Vector2[4];
    static int _resolutionCounter = 0;
    const float waitForPress = 1f;
    static float _timer;

    public static void ResolutionNotSupport()
    {
        SetResolutions();

        Vector2 _screenSize = new Vector2(Screen.width, Screen.height);
        if (_screenSize != _allResolutions[0] && _screenSize != _allResolutions[1] && _screenSize != _allResolutions[2])
        {
            Screen.SetResolution(640, 360, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (Screen.width)
        {
            case 1920:
                _resolutionCounter = 0;
                break;

            case 1280:
                _resolutionCounter = 1;
                break;

            case 640:
                _resolutionCounter = 2;
                break;
        }

        _checkOBJ.SetActive(Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        _resolutionButton.onClick.AddListener(SetNewResolution);
        _fullScreenButton.onClick.AddListener(FullScreen);

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            optionsIsOpen = false;
            gameObject.SetActive(false);
        }

        ResolutionNotSupportInUpdate();
        _resolutionText.text = $"DIMENSIONE      {_allResolutions[_resolutionCounter].x}x{_allResolutions[_resolutionCounter].y}";
        _timer -= Time.deltaTime;
    }
    
    void ResolutionNotSupportInUpdate()
    {
        Vector2 _screenSize = new Vector2(Screen.width, Screen.height);
        if (_screenSize != _allResolutions[0] && _screenSize != _allResolutions[1] && _screenSize != _allResolutions[2])
        {
            Screen.SetResolution(640, 360, false);
            _resolutionCounter = 2;
            _checkOBJ.SetActive(false);
        }
    }

    void FullScreen()
    {
        if (!Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            _checkOBJ.SetActive(true);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            _checkOBJ.SetActive(false);
        }
    }

    void SetNewResolution()
    {
        if(_timer < 0)
        {
            switch (_resolutionCounter)
            {
                case 0:
                    _resolutionCounter = 1;
                    break;

                case 1:
                    _resolutionCounter = 2;
                    break;

                case 2:
                    _resolutionCounter = 0;
                    break;
            }

            Screen.SetResolution((int)_allResolutions[_resolutionCounter].x, (int)_allResolutions[_resolutionCounter].y, Screen.fullScreenMode);
            _timer = waitForPress;
        }        
    }

    static void SetResolutions()
    {
        _allResolutions[0] = new Vector2(1920,1080);
        _allResolutions[1] = new Vector2(1280, 720);
        _allResolutions[2] = new Vector2(640, 360);
    }
}
