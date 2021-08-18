using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{

    public StateManager player;
    public GameObject item;
    public string type;
    public Sprite[] icons;
    private Image icon;

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        item = player.heldItem;
        type = item.tag.ToString();
        if (item == null || player.itemUsed)
        {
            var temp = icon.color;
            temp.a = 0;
            icon.color = temp;
            icon.sprite = null;
        }
        else
        {
            icon.color = Color.white;
            if (item.tag.Equals("SpeedBoostItem"))
            {
                icon.sprite = icons[0];
            }
            else if (item.tag.Equals("ShieldItem"))
            {
                icon.sprite = icons[1];
            }
            else if (item.tag.Equals("StunItem"))
            {
                icon.sprite = icons[2];
            }
        }
    }
}
