using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;

    [SerializeField] private GameObject menuPanel;

    [SerializeField] private GameObject[] emptyStars;

    [SerializeField] private GameObject[] fullStars;
    
    [SerializeField] private GameObject[] levels;

    [SerializeField] private int[] starsStat = new [] {0, 0, 0, 0, 0, 0, 0, 0};
    private int levelOn = 1;
    private bool isMenuActived = false;


    // Start is called before the first frame update
    void Start()
    {
        victoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (menuPanel.activeSelf && isMenuActived)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                List<GameObject> emptyChildStars = new List<GameObject>();
                List<GameObject> fullChildStars = new List<GameObject>();

                foreach (Transform child in levels[i].GetComponentInParent<Transform>())
                {
                    if (child.gameObject.tag.Equals("EmptyStar"))
                    {
                        emptyChildStars.Add(child.gameObject);
                    }

                    if (child.gameObject.tag.Equals("FullStar"))
                    {
                        fullChildStars.Add(child.gameObject);
                    }
                }

                for (int j = 0; j < 3; j++)
                {
                    emptyChildStars[j].SetActive(j >= starsStat[i]);
                    fullChildStars[j].SetActive(j < starsStat[i]);
                }

            }

            isMenuActived = false;
        }

        OpenVictoryStage();
    }

    private void ActivateStars(int amount)
    {
        if (starsStat[levelOn - 1] < amount)
        {
            starsStat[levelOn - 1] = amount;
        }
        
        for (int i = 0; i < emptyStars.Length; i++)
        {
            emptyStars[i].SetActive(i >= amount);
            fullStars[i].SetActive(i < amount);
        }
    }

    private void OpenVictoryStage()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !victoryPanel.activeSelf && !menuPanel.activeSelf)
        {
            victoryPanel.SetActive(true);
            ActivateStars(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && !victoryPanel.activeSelf && !menuPanel.activeSelf)
        {
            victoryPanel.SetActive(true);
            ActivateStars(2);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && !victoryPanel.activeSelf && !menuPanel.activeSelf)
        {
            victoryPanel.SetActive(true);
            ActivateStars(3);
        }
    }

    public void OnOKClick()
    {
        victoryPanel.SetActive(false);
        menuPanel.SetActive(true);
        isMenuActived = true;
    }

    public void OnLevelClick(int level)
    {
        levelOn = level;
        menuPanel.SetActive(false);
    }
}
