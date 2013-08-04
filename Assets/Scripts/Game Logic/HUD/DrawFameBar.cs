using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DrawFameBar : MonoBehaviour
{
    public Texture texture;

    public Transform player;

    private float fameBarLength = 0.0f;
    private int maxBarLength = 100;

    private FameBar fameBar;

    private GUIStyle style = new GUIStyle();

    void Awake()
    {
        //get the fame bar script on the player
        fameBar = player.GetComponent<FameBar>();
    }

    void Start()
    {
        fameBarLength = Screen.width / 2;
    }

	// Update is called once per frame
	void Update ()
    {
        AdjustFameBar();
	}

    void AdjustFameBar()
    {
        if (fameBar.reelCount > maxBarLength)
        {
            fameBar.reelCount = maxBarLength;
        }

        //apply the length of the bar
        fameBarLength = (Screen.width / 2) * (fameBar.reelCount / (float)maxBarLength);
    }

    void OnGUI()
    {
        //TEMPORARY: create green text
        style.normal.textColor = Color.green;
        //TEMPORARY: create a background texture
        style.normal.background = texture as Texture2D;
        //draw the box
        GUI.Box(new Rect(10, 60, fameBarLength, 20), "Fame: " + fameBar.reelCount, style);
    }
}
