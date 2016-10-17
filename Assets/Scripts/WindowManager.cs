using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WindowManager : MonoBehaviour {

    private float offset;
    private Vector3 cv;

    private Vector3 gameWindowPos;
    private Vector3 settingsWindowPos;

    [SerializeField]
    private float smoothTime = 1;

    [SerializeField]
    private RectTransform gameWindow;
    [SerializeField]
    private RectTransform settingsWindow;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button backToGameWindowSettingsButton;

    void Start()
    {
        offset = gameWindow.rect.width;


        settingsButton.onClick.AddListener(() => {
            StartCoroutine(ChangeToSettings());
        });

        backToGameWindowSettingsButton.onClick.AddListener(() =>
        {
            StartCoroutine(ChangeToGame());
        });
    }


    //Changes window with smooth effect. Nice meme.
    IEnumerator ChangeToSettings()
    {
        Vector3 target1 = new Vector3(0f, 0f);
        Vector3 target2 = new Vector3(offset, 0f);

        while (settingsWindow.localPosition != target1)
        {
            //settingsWindow.localPosition = target1;
            //gameWindow.localPosition = target2;

            settingsWindow.localPosition = Vector3.SmoothDamp(settingsWindow.localPosition, target1, ref cv, smoothTime);
            gameWindow.localPosition = Vector3.SmoothDamp(gameWindow.localPosition, target2, ref cv, smoothTime);
            yield return null;
        }
    } 
    IEnumerator ChangeToGame()
    {
        Vector3 target1 = new Vector3(0f, 0f);
        Vector3 target2 = new Vector3(-offset, 0f);

        while (gameWindow.localPosition != target1)
        {
            
            //gameWindow.localPosition = target1;
            //settingsWindow.localPosition = target2;

            gameWindow.localPosition = Vector3.SmoothDamp(gameWindow.localPosition, target1, ref cv, smoothTime);
            settingsWindow.localPosition = Vector3.SmoothDamp(settingsWindow.localPosition, target2, ref cv, smoothTime);
            yield return null;
        }
    }
}
