using UnityEngine;
using UnityEngine.EventSystems;

namespace EXSOM.Inventory
{
    public class CellUsedItem : MonoBehaviour, IDropHandler, IContainerActive
    {
        private Inventory inventory;
        private Item usedItem;

        public void Initialization(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public void SetItem(Item item)
        {
            usedItem = item;
            GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Use Item");
            inventory.SetActiveItem(this);
        }
    }
}
