using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int maxItem;
    public int stage;
    public Text gotItemText;
    public Text maxItemText;
    private void Awake()
    {
        maxItemText.text = "/ "+maxItem;
    }
    public void GotItemTextUpdate(int gotItem)
    {
        gotItemText.text = gotItem.ToString();
    }
}
