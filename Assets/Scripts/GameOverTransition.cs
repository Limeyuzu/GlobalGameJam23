using Assets.GenericTools.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var blackScreen = GetComponentInChildren<Image>();
        var gameOverText = GetComponentInChildren<TextMeshProUGUI>();
        blackScreen.canvasRenderer.SetAlpha(0);
        gameOverText.canvasRenderer.SetAlpha(0);

        EventManager.Subscribe(GameEvent.GameOver, _ =>
        {
            blackScreen.CrossFadeAlpha(1, 3, false);
            gameOverText.CrossFadeAlpha(1, 8, false);
        });
    }
}
