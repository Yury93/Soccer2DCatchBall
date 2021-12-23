using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : SingletonBase<MenuController>
{
    [SerializeField] private PlayerMetrics playerMetrics;
    [Header("Metrick Panel")]
    [SerializeField] private GameObject metrickPanel;
    [Header("Result Panel")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Text textScore;
    [SerializeField] private int sceneIndex = 1;

    private void Start()
    {
        resultPanel.SetActive(false);
    }
    private void Update()
    {
        if(playerMetrics.HP == 0)
        {
            Time.timeScale = 0;
            metrickPanel.SetActive(false);
            resultPanel.SetActive(true);
            textScore.text = playerMetrics.textScore.text;
        }
        if(playerMetrics.HP > 0)
        {
            Time.timeScale = 1;
            metrickPanel.SetActive(true);
            resultPanel.SetActive(false);
        }
    }
    /// <summary>
    /// Перезагружаем сцену, при нажатии на кнопку
    /// </summary>
    public void OnClickLoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
