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
        fameBar = player.GetComponent<FameBar>();
    }
    	
	// Update is called once per frame
	void Update ()
    {
        AdjustFameBar();
	}

    void AdjustFameBar()
    {
        fameBarLength = (Screen.width / 2) * (fameBar.reelCount / (float)maxBarLength);
    }

    void OnGUI()
    {
        style.normal.textColor = Color.green;
        style.normal.background = texture as Texture2D;
        GUI.Box(new Rect(10, 60, fameBarLength, 20), "Fame: " + fameBar.reelCount, style);
    }
}
