using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;
    Vector3 startScale;
    Coroutine hintAnimation;
    public GameObject InteractionHint;

    [SerializeField]
    PlayerAnimation playerAnim;

    float horizontal;
    float vertical;

    Dictionary<string, bool> inventory = new Dictionary<string, bool>();

    public  float runSpeed = 5.0f;

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "item")
        {
            InteractionHint.GetComponent<SpriteRenderer>().enabled = true;
            hintAnimation = StartCoroutine(InteractionHintAnimation());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "item")
        {
            InteractionHint.GetComponent<SpriteRenderer>().enabled = false;
            StopCoroutine(hintAnimation);
        }
    }
    #endregion

    void Awake()
    {
        PopulateInventory();
        startScale = InteractionHint.transform.localScale;
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        playerAnim.SetParams(horizontal, vertical);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void PopulateInventory()
    {
        inventory.Add("shovel", false);
        inventory.Add("chest", false);
        inventory.Add("sapling", false);
        inventory.Add("watering can", false);
        inventory.Add("fruit", false);
        inventory.Add("rat", false);
        inventory.Add("Chest key", false);
    }

    IEnumerator InteractionHintAnimation()
    {
        while (true)
        {

            for (float i = 0; i < 1; i += 0.01f)
            {
                Debug.Log("<color=purple>" + i + "</color>");
                InteractionHint.transform.localScale = Vector3.Lerp(startScale, startScale * 1.5f, Mathf.SmoothStep(0, 1f, i));
                yield return new WaitForSeconds(0.01f);
            }


            for (float i = 0; i < 1; i += 0.01f)
            {
                Debug.Log("<color=purple>" + i + "</color>");
                InteractionHint.transform.localScale = Vector3.Lerp(startScale * 1.5f, startScale, Mathf.SmoothStep(0, 1f, i));
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}