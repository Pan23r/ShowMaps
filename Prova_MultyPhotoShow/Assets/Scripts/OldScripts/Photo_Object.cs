using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo_Object : MonoBehaviour
{
    private string _myName { get; set; }
    private Image _myImage { get; set; } 

    public Photo_Object(string name, Image image)
    {
        _myName = name;
        _myImage = image;
    }
}
