using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TilingTest : MonoBehaviour
{
    public GameObject tilingObject;

    [SerializeField]
    Vector2Int dimensions;

    [Range(0,15)]
    public int position;
    
    float xJump, yJump;

    Renderer r;

    private void Awake()
    {
        r = tilingObject.GetComponent<Renderer>();
        xJump = 1f / dimensions.x;
        yJump = 1f / dimensions.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            position = (position + 15) % 16;
            UpdateTexture();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            position = (position + 17) % 16;
            UpdateTexture();
        }
    }

    void UpdateTexture()
    {
        if (position > dimensions.x * dimensions.y)
        {
            Debug.LogError($"{position} is out of range!");
            return;
        }
        int xPos = Mathf.FloorToInt(position / dimensions.x);
        int yPos = position % dimensions.y;
        print($"{position} maps to: {xPos}, {yPos}");

        r.material.SetTextureOffset("_MainTex", new Vector2(xPos * xJump, yPos * yJump));
    }
}
