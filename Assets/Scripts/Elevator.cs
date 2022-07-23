using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    #region Singleton
    private static Elevator Instance;
    public static Elevator _instance { get => Instance; }
    #endregion

    private bool isInElevator = false;
    Collider2D playerCollider;

    public Collider2D door;
    public Transform spriteRenderer;
    public GameObject[] rooms;

    public Animator elevatorAnimator;

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            isInElevator = true;
            elevatorAnimator.SetTrigger("Open");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            isInElevator = false;
            elevatorAnimator.SetTrigger("Close");
        }
    }
    #endregion

    void Awake()
    {
        Instance = this; // Singleton
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeRoom("green");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeRoom("red");
        }
    }

    public void ChangeRoom(string color)
    {
        if (!isInElevator)
            return;

        switch (color)
        {
            case "green":
                StartCoroutine(LoadRoom(0));
                break;

            case "red":
                StartCoroutine(LoadRoom(1));
                break;
        }
    }

    IEnumerator LoadRoom(int room)
    {
        CloseDoor();
        foreach (var item in rooms)
        {
            item.SetActive(false);
        }

        spriteRenderer.transform.position += new Vector3(0.2f, 0, 0);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.transform.position += new Vector3(-0.4f, 0, 0);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.transform.position += new Vector3(0.4f, 0, 0);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.transform.position += new Vector3(-0.2f, 0, 0);
        yield return new WaitForSeconds(0.5f);
        OpenDoor();

        rooms[room].SetActive(true);
    }

    void OpenDoor()
    {
        door.enabled = false;
    }

    void CloseDoor()
    {
        door.enabled = true;
    }
}
