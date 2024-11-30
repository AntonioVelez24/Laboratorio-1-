using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask gameLayer;

    private float DirectionX;
    private float DirectionY;

    private GameObject lastHitObject = null;
    private SpriteRenderer spriteRenderer;   

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    void Update()
    {
        Vector3 movement = new Vector3(DirectionX, DirectionY, 0) * speed * Time.deltaTime;

        float clampedX = Mathf.Clamp(transform.position.x, -8.15f, 8.15f);
        float clampedY = Mathf.Clamp(transform.position.y, -4.3f, 4.3f);

        transform.position = new Vector3(clampedX, clampedY);
        transform.position = transform.position + movement;

        Debug.DrawRay(transform.position, movement.normalized * 0.5f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.normalized, 0.5f, gameLayer);
        if (hit.collider != null && hit.collider.gameObject != lastHitObject)
        {
            Debug.Log("Colisionando con: " + hit.collider.gameObject.name);
            Debug.Log("Posición: " + hit.collider.transform.position);
            Debug.Log("Tag: " + hit.collider.tag);
            if (hit.collider.tag == "Color")
            {
                Debug.Log("Cambiando el Color del jugador");
                ChangeColor(hit.collider.gameObject);
            }
            else if(hit.collider.tag == "Shape")
            {
                Debug.Log("Cambiando el Sprite del jugador");
                ChangeSprite(hit.collider.gameObject);
            }
            lastHitObject = hit.collider.gameObject;
        }
    }
    void ChangeSprite(GameObject hitObject)
    {
        SpriteRenderer hitSpriteRenderer = hitObject.GetComponent<SpriteRenderer>();
        if (hitSpriteRenderer != null)
        {
            spriteRenderer.sprite = hitSpriteRenderer.sprite;
        }
    }
    void ChangeColor(GameObject hitObject)
    {
        SpriteRenderer hitRenderer = hitObject.GetComponent<SpriteRenderer>();
        if (hitRenderer != null)
        {
            spriteRenderer.color = hitRenderer.color;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        lastHitObject = null;
    }
    public void ReadMovementX(InputAction.CallbackContext context)
    {
        DirectionX = context.ReadValue<float>();
    }
    public void ReadMovementY(InputAction.CallbackContext context)
    {
        DirectionY = context.ReadValue<float>();
    }
}
