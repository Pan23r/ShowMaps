using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom_Manager : MonoBehaviour
{
    float _zoomVelocity = (Screen.width/3) * 10;
    float _scaleRunTime = 0.2f;
    public static bool zoomIsActive = false;
    RectTransform _backGroundImage;

    // Start is called before the first frame update
    void Start()
    {
        _backGroundImage = GameObject.FindGameObjectWithTag("BackGroundMap").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomIsActive && Input.GetKeyDown(KeyCode.Escape) && ButtonMapping_Manager.timer < 0)
        {
            transform.localScale = new Vector2(1, 1);
            transform.localPosition = Vector2.zero;
            zoomIsActive = false;
            ButtonMapping_Manager.timer = ButtonMapping_Manager.waitForPress;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            zoomIsActive = true;
            transform.localScale = new Vector2(transform.localScale.x + _scaleRunTime, transform.localScale.y + _scaleRunTime);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            transform.localScale = new Vector2(transform.localScale.x - _scaleRunTime, transform.localScale.y - _scaleRunTime);
        }

        if (transform.localScale.x < 1)
        {
            zoomIsActive = false;
            transform.localScale = new Vector2(1, 1);
        }

        if (zoomIsActive && Input.GetMouseButton(0))
        {
            transform.position += new Vector3(Input.GetAxis("Mouse X") * (_zoomVelocity * Time.deltaTime), Input.GetAxis("Mouse Y") * (_zoomVelocity * Time.deltaTime));
        }

        //Debug.Log(transform.localScale.x);
    }

    private void LateUpdate()
    {
        //Sprite.width : 1920 = X : Screen.Width
        float _rectWidth = (ButtonMapping_Manager.isActive) ? 1920: _backGroundImage.rect.width;
        float _rectHeight = (ButtonMapping_Manager.isActive) ? 1080 : _backGroundImage.rect.height;

        float _rectImageScaleX = (_rectWidth * Screen.width) / 1920;
        float _rectImageScaleY = (_rectHeight * Screen.height) / 1080;

        if (transform.localScale.x == 1)
        {
            transform.localPosition = Vector2.zero;
        }
        else if (transform.localScale.x < 1.6 && zoomIsActive)
        {
            //CONTROLLER X MAPS
            if (!ButtonMapping_Manager.isActive)
            {
                ControllerX(_rectImageScaleX);
            }
            else //CONTROLLER X IMMAGES
            {
                ControllerXOutOfBorder(_rectImageScaleX);
            }

            ControllerYOutOfBorder(_rectImageScaleY);
        }
        else if (transform.localScale.x >= 1.6 && zoomIsActive)
        {
            //CONTROLLER X
            ControllerXOutOfBorder(_rectImageScaleX);
            //CONTROLLER Y
            ControllerYOutOfBorder(_rectImageScaleY);
        }
    }

    #region ControllerX
    void ControllerX(float _rectImageScaleX)
    {
        float _distanceBorderX = (_rectImageScaleX) / 2;
        if (transform.position.x + _distanceBorderX > Screen.width)
        {
            transform.position = new Vector2(Screen.width - _distanceBorderX, transform.position.y);
        }

        if (transform.position.x - _distanceBorderX < 0)
        {
            transform.position = new Vector2(_distanceBorderX, transform.position.y);
        }
    }
    #endregion

    #region ControllerXOutOfBorder
    void ControllerXOutOfBorder(float _rectImageScaleX)
    {
        float _distanceBorderX = (_rectImageScaleX * transform.localScale.x) / 2;
        if (transform.position.x + _distanceBorderX < Screen.width)
        {
            transform.position = new Vector2(Screen.width - _distanceBorderX, transform.position.y);
        }

        if (transform.position.x - _distanceBorderX > 0)
        {
            transform.position = new Vector2(_distanceBorderX, transform.position.y);
        }
    }
    #endregion

    #region ControllerYOutOfBorder
    void ControllerYOutOfBorder(float _rectImageScaleY)
    {
        float _distanceBorderY = (_rectImageScaleY * transform.localScale.y) / 2;
        if (transform.position.y + _distanceBorderY < Screen.height)
        {
            transform.position = new Vector2(transform.position.x, Screen.height - _distanceBorderY);
        }

        if (transform.position.y - _distanceBorderY > 0)
        {
            transform.position = new Vector2(transform.position.x, _distanceBorderY);
        }
    }
    #endregion
}
