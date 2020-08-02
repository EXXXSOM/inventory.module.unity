using UnityEngine;
using UnityEngine.EventSystems;

namespace EXSOM.Inventory
{
    public class CellInventory : MonoBehaviour, IDropHandler, IContainerCell
    {
        private Inventory inventory;
        public bool busy = false;
        private ItemUI itemUI;
        public ItemUI ItemUI { get { return itemUI; } }

        public void Initialization(Inventory inventory)
        {
            this.inventory = inventory;
        }

        //Installing a new item
        public void FirstSetItem(Item item, ItemUI itemUI)
        {
            busy = true;
            this.itemUI = itemUI;
            this.itemUI.PresentItem(inventory, item, this);
        }

        public void SetItem(ItemUI itemUI)
        {
            busy = true;
            this.itemUI = itemUI;
            itemUI.CellInventory = this;
        }

        //Clearing the interface before deleting or changing an item
        public void DisposeCell()
        {
            busy = false;

            itemUI.Dispose();
            itemUI = null;
        }

        public void OnDrop(PointerEventData eventData)
        {
            inventory.SwapItem(this);
        }
    }
}
