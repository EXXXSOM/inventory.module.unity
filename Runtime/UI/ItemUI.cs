using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EXSOM.Inventory
{
    public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
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

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Выбран элемент: " + name);
                inventory.SelectedItem(this);
            }

            if (Input.GetMouseButton(1))
            {
                Debug.Log("Вызов меню у элемента: " + name);

                inventory.ShowTipMenu(this);
                inventory.SelectedItem(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Перетаскивание начато");

                inventory.dragItem = true;
                GetComponent<Image>().raycastTarget = false;
                transform.SetParent(inventory.transform);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (inventory.dragItem)
            {
                Debug.Log("Перетягиваем!");
                transform.position = Input.mousePosition;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (inventory.dragItem)
            {
                Debug.Log("Перетаскивание законченно!");
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
                inventory.dragItem = false;
            }
        }

        //Clearing the interface before removing it from the cell
        public void Dispose()
        {
            CellInventory = null;
        }
    }
}
