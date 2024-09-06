using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite itemImage;
}

public class CollectionController : MonoBehaviour
{
    public GameObject itemCard;
    public Item item;
    public int healthChange;
    public float movementSpeedChange;
    public float fireRateChange;
    public float bulletSizeChange;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {

            GameController.HealPlayer(healthChange);
            GameController.MovementSpeedChange(movementSpeedChange);
            GameController.FireRateChange(fireRateChange);
            GameController.BulletSizeChange(bulletSizeChange);

            //spawn item card
            Vector2 pos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            var card = Instantiate(itemCard, pos, Quaternion.identity);
            var itemName = card.transform.GetChild(0).GetComponent<TextMesh>();
            itemName.text = item.name;
            var itemDesc = card.transform.GetChild(1).GetComponent<TextMesh>();
            itemDesc.text = item.description;

            Destroy(gameObject);
        }
    }
}
