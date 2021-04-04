using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EXSOM.Inventory
{
    public class TipMenu : MonoBehaviour
    {
        private ItemUI selectedItemUI;

        public void Show(ItemUI itemUI)
        {
            if (selectedItemUI == itemUI)
            {
                Close();
            }
            else
            {
                selectedItemUI = itemUI;
                gameObject.SetActive(true);
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
            Dispose();
        }

        public void Dispose()
        {
            selectedItemUI = null;
        }
    }
}
