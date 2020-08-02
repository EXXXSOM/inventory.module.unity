using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EXSOM.Inventory
{
    public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CellInventory CellInventory;
        private Inventory inventory;
        private Item item;

        public Item Item { get { return item; } }

        public void PresentItem(Inventory inventory, Item item, CellInventory cellInventory)
        {
            this.inventory = inventory;
            this.item = item;
            CellInventory = cellInventory;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            inventory.SelectedItem(this);

            GetComponent<Image>().raycastTarget = false;
            transform.SetParent(inventory.transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (inventory.swapingItem == true)
            {
                inventory.swapingItem = false;
                GetComponent<Image>().raycastTarget = true;
            }
            else
            {
                //Return in this cell
                transform.SetParent(CellInventory.transform);
                GetComponent<Image>().raycastTarget = true;
            }
        }

        //Clearing the interface before removing it from the cell
        public void Dispose()
        {
            CellInventory = null;
        }
    }
}
