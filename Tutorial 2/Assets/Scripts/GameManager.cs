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
            int timeLimitInt = Mathf.FloorToInt(timeLimit);
            timeLimitText.text = timeLimitInt.ToString();
        }else
        {
            timeLimitText.text = timeLimit.ToString();
        }
    }
}
