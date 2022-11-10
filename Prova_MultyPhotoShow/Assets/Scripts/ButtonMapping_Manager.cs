using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMapping_Manager : MonoBehaviour
{
    [SerializeField] GameObject _prefabToInstanciate;
    [SerializeField] Sprite[] _imagesToView = new Sprite[4];
    public static bool isActive = false;
    static GameObject ObjToDesactivate;
    public const float waitForPress = 1f;
    public static float timer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject _prefabInstantiate = Instantiate(_prefabToInstanciate, transform);
        _prefabInstantiate.transform.position = new Vector2(Screen.width/2, Screen.height/2);
        _prefabInstantiate.transform.parent = transform;
        Transform firstChild = transform.GetChild(0);

        for(int i = 0; i < firstChild.childCount; i++)
        {
            firstChild.GetChild(i).GetComponent<Image>().sprite = _imagesToView[i];
        }

        firstChild.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().onClick.AddListener(OpenImages);

        PressToEsc();

        timer -= Time.deltaTime;
    }

    void OpenImages()
    {
        if (!isActive && timer < 0 && !Zoom_Manager.zoomIsActive)
        {
            isActive = true;
            ObjToDesactivate = transform.GetChild(0).gameObject;
            ObjToDesactivate.SetActive(true);
            //Debug.Log("name = " + gameObject.name + "IsActive = " + isActive);
            timer = waitForPress;
        }
        
    }

    void PressToEsc()
    {
        if (isActive && !Zoom_Manager.zoomIsActive && Input.GetKeyDown(KeyCode.Escape) && timer < 0)
        {
            isActive = false;
            ObjToDesactivate.SetActive(false);
            //Debug.Log("name = " + gameObject.name + "IsActive = " + isActive);
            timer = waitForPress;
        }
    }

    private void OnDrawGizmosSelected()
    {
        DrawLine();
    }

    void DrawLine()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.black;
        Image objImage = GetComponent<Image>();
        float width = objImage.rectTransform.rect.width / 2;
        float height = objImage.rectTransform.rect.height / 2;
        Vector3 leftUpPoint = new Vector3(transform.position.x - (width), transform.position.y + (height));
        Vector3 rightUpPoint = new Vector3(transform.position.x + (width) , transform.position.y + (height));
        Vector3 leftDownPoint = new Vector3(transform.position.x - (width), transform.position.y - (height));
        Vector3 rightDownPoint = new Vector3(transform.position.x + (width), transform.position.y - (height));

        Gizmos.DrawLine(leftUpPoint, rightUpPoint);
        Gizmos.DrawLine(leftUpPoint, leftDownPoint);
        Gizmos.DrawLine(rightUpPoint, rightDownPoint);
        Gizmos.DrawLine(leftDownPoint, rightDownPoint);
#endif
    }
}
