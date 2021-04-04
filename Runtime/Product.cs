using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EXSOM.Inventory;

public class Product : EXSOM.Inventory.Item
{
    public Inventory tel;

    public void Use()
    {
        tel.AddItem(this);
    }
}
