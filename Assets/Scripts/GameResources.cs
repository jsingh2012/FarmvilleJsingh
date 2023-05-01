using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameResources : MonoBehaviour
{
    public float wood;
    public float stone;
    public float food;

    [Header("UI Refrence")]
    public TMP_Text ResourceText;


    private void FixedUpdate()
    {
        ResourceText.text = "Wood : " + ((int)wood).ToString() + " Stone " + ((int)stone).ToString() + " food " +((int) food).ToString();
    }

}
