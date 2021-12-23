using UnityEngine;
using UnityEngine.UI;

public class PlayerMetrics : SingletonBase<PlayerMetrics>
{
    public Text textHealth;
    public Text textScore;
    public Text textRecord;
    public int Score;
    public int HP;
    public int Record;

    private void Start()
    {
        Record = PlayerPrefs.GetInt("Record");
        textRecord.text = "Record: " + Record.ToString();
    }
    public void AddScore(int score)
    {
        Record = PlayerPrefs.GetInt("Record");
        Score += score;
        textScore.text = "Score: " + Score.ToString();
        if(Record < Score)
        {
            PlayerPrefs.SetInt("Record", Score);
            Record = Score;
            Record = PlayerPrefs.GetInt("Record");
            textRecord.text = "Record: " + Record.ToString();
        }
    }
}
