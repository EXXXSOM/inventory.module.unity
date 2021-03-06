﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EXSOM.Inventory
{
    public class Inventory : MonoBehaviour
    {
        //For the test
        public List<Item> itemsInWorld;
        private void Start()
        {
            for (int i = 0; i < itemsInWorld.Count; i++)
            {
                AddItem(itemsInWorld[i]);
            }
        }

        //Inventory сustomization options
        [Header("Inventory settings")]
        public bool UseWeight = true;
        public bool UseActiveSlots = true;
        public int TotalWeightMax = 100;
        public const int InventoryCellsCount = 3;
        public const int InventoryActiveCellsCount = 4;
        public GameObject ItemUITemplate; //Set prefab "Cell"

        //Class working fields
        [Header("Inventory stats")]
        [SerializeField] private int totalWeight = 0;
        [SerializeField] private int totalItemsCount = 0;
        public bool dragItem = false;

        private List<Item> items = new List<Item>(InventoryCellsCount);
        [SerializeField] private List<CellInventory> cells = new List<CellInventory>(InventoryCellsCount);
        [SerializeField] private List<CellUsedItem> activeCells = new List<CellUsedItem>();

        [HideInInspector] public bool swapingItem = false;
        private ItemUI selectedItemUI;
        private CellInventory selectedCell;

        //References gameobjects
        [Header("Inventory stats")]
        public Text nameItem;
        public Text discriptionItem;
        public TipMenu tipMenu;

        private void Awake()
        {
#if UNITY_EDITOR
            if (ItemUITemplate == null)
            {
                Debug.LogWarning("Not installed prefab!");
            }
            if (tipMenu == null)
            {
                Debug.LogWarning("Not installed TipMenu!");
            }
#endif


            CellInventory cell = cells[0]; 
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].Initialization(this);
            }

            if (UseActiveSlots)
            {
                for (int i = 0; i < activeCells.Count; i++)
                {
                    activeCells[i].Initialization(this);
                }
            }
        }

        public void AddItem(Item item)
        {
            if (InventoryCellsCount >= totalItemsCount)
            {
                if (UseWeight == true)
                {
                    if (totalWeight + item.Weight <= TotalWeightMax)
                    {
                        totalWeight += item.Weight;
                    }
                }

                for (int i = 0; i < cells.Count; i++)
                {
                    if (cells[i].busy == false)
                    {
                        cells[i].busy = true;
                        totalItemsCount++;
                        items.Add(item);

                        ItemUI itemUI = Instantiate(ItemUITemplate, cells[i].transform).GetComponent<ItemUI>();
                        itemUI.GetComponent<UnityEngine.UI.Image>().color = item.testingColor;

                        cells[i].FirstSetItem(item, itemUI);

                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Inventory full!");
            }
        }

        public void RemoveItem(Item item)
        {
            if (UseWeight == true)
            {
                totalWeight -= item.Weight;
            }
            items.Remove(item);
        }

        //Place an item in the quick access slot (can be by index)
        public void SetActiveItem(CellUsedItem cellUsedItem)
        {
            cellUsedItem.SetItem(selectedItemUI.Item);
        }
        public void SetActiveItem(CellUsedItem cellUsedItem, int cellNumber)
        {
            activeCells[cellNumber].SetItem(selectedItemUI.Item);
        }

        public void SelectedItem(ItemUI itemUI)
        {
            selectedItemUI = itemUI;
            selectedCell = itemUI.CellInventory;

            nameItem.text = itemUI.Item.keyName;
            discriptionItem.text = itemUI.Item.keyDiscription;
        }

        //Moves an item to a slot
        public void SwapItem(CellInventory newCellInventory)
        {
            if (newCellInventory is IContainerCell)
            {
                swapingItem = true;

                if (newCellInventory.busy == true)
                {
                    SwapItems(selectedCell, newCellInventory);
                }
                else
                {
                    selectedItemUI.CellInventory.DisposeCell();
                    selectedItemUI.transform.SetParent(newCellInventory.transform);
                    newCellInventory.SetItem(selectedItemUI);
                }
                return;
            }

            if (newCellInventory is IContainerCell)
            {
            }
        }

        //Swaps items in slots
        public void SwapItems(CellInventory firstCell, CellInventory secondCell)
        {
            firstCell.ItemUI.transform.SetParent(secondCell.transform);
            secondCell.ItemUI.transform.SetParent(firstCell.transform);

            ItemUI saveItemUI = firstCell.ItemUI;
            firstCell.SetItem(secondCell.ItemUI);
            secondCell.SetItem(saveItemUI);
        }

        public void ShowTipMenu(ItemUI itemUI)
        {
            tipMenu.Show(itemUI);
        }

        public void CloseTipMenu()
        {
            tipMenu.Close();
        }
    }
}
