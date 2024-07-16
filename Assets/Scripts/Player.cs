using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] float paddingLeft = 1f;
    [SerializeField] float paddingRight = 1f;
    [SerializeField] float paddingTop = 1f;
    [SerializeField] float paddingBottom = 1f;

    Vector2 moveInput;

    Vector2 minBounds; //stores the bottom left of viewport
    Vector2 maxBounds; //stores the top right of viewport

    Shooter shooter;

    void Awake() {
      shooter = GetComponent<Shooter>();
    }

    void Start()
    {
      InitBounds();
    }

    void Update()
    {
      Move();
    }

    void InitBounds() {
      Camera mainCamera = Camera.main;
      minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
      maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void Move() {
      Vector2 delta = moveInput * moveSpeed * Time.deltaTime; //multiplying by delta time makes the framerate independent
      Vector2 newPos = new Vector2();
      newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
      newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
      transform.position = newPos;
    }

    void OnFire(InputValue value) {
        if (shooter != null) {
          shooter.isFiring = value.isPressed;
        }
    }
}
