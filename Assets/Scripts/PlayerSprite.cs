using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeSprite(Direction dir)
    {
        switch (dir)
        {
            case Direction.UP:
                spriteRenderer.sprite = backSprite;
                break;
            case Direction.DOWN:
                spriteRenderer.sprite = frontSprite;
                break;
            case Direction.LEFT:
                spriteRenderer.sprite = leftSprite;
                break;
            case Direction.RIGHT:
                spriteRenderer.sprite = rightSprite;
                break;
            default:
                break;
        }
    }
}
