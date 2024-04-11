using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int maxItem;
    public int stage;
    public double timeLimit;
    public Text stageText;
    public Text timeLimitText;
    public Text gotItemText;
    public Text maxItemText;
    
    int timeLimitOver5;
    float timeLimitUnder5;
    
    private void Awake()
    {
        maxItemText.text = "/ "+maxItem;
    }
    void StageTextUpdate()
    {
        stageText.text = "Stage"+stage.ToString();
    }
    public void GotItemTextUpdate(int gotItem)
    {
        gotItemText.text = gotItem.ToString();
    }
    private void Update()
    {
        StageTextUpdate();
        TimeLimitTextUpdate();
        
    }
    void TimeLimitTextUpdate()
    {
        timeLimit -= Time.deltaTime;
        if (timeLimit >= 5f)
        {
            timeLimitOver5 = Mathf.FloorToInt(timeLimit);
            timeLimitText.text = timeLimitOver5.ToString();
        }else if(timeLimit < 5f)
        {
            timeLimitUnder5 = Mathf.FLoor(timeLimit*100f)/100f;
            timeLimitText.text = timeLimitUnder5.ToString();
        }
    }
}
