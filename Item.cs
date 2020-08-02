using UnityEngine;
using UnityEngine.UI;

namespace EXSOM.Inventory
{
    public class Item : MonoBehaviour
    {
        /// <summary>
        /// Base class of the item
        /// </summary>

        private int id = 0;
        private int weight = 0;
        private Image icon;
        public Color testingColor;

        public int Weight { get { return weight; } }
        public int ID { get { return id; } }
        public Image Icon { get { return icon; } }

        public Item(int id, int weight)
        {
            this.id = id;
            this.weight = weight;
        }
    }
}
