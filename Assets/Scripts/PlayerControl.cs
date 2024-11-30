using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask gameLayer;

    private float DirectionX;
    private float DirectionY;

    private GameObject lastHitObject = null;
    void Update()
    {
        Vector3 movement = new Vector3(DirectionX, DirectionY, 0) * speed * Time.deltaTime;

        float clampedX = Mathf.Clamp(transform.position.x, -8.15f, 8.15f);
        float clampedY = Mathf.Clamp(transform.position.y, -4.3f, 4.3f);

        transform.position = new Vector3(clampedX, clampedY);
        transform.position = transform.position + movement;

        Debug.DrawRay(transform.position, movement.normalized * 1f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.normalized, 1f, gameLayer);
        if (hit.collider != null && hit.collider.gameObject != lastHitObject)
        {
            Debug.Log("Colisionando con: " + hit.collider.gameObject.name);
            Debug.Log("Posición: " + hit.collider.transform.position);
            Debug.Log("Tag: " + hit.collider.tag);
            if (hit.collider.tag == "Color")
            {
                Debug.Log("Cambiando el Color del jugador");
            }
            else if(hit.collider.tag == "Shape")
            {
                Debug.Log("Cambiando el Sprite del jugador");
            }
            lastHitObject = hit.collider.gameObject;
        }
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
