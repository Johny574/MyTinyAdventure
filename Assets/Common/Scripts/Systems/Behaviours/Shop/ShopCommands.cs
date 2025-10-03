using UnityEngine;

namespace ShopCommands
{

    public abstract class ShopCommand : ItemStackCommand {
        protected CurrencyComponent _buyerCurrency;
        protected InventoryComponent _buyerInventory;

        protected ShopCommand(GameObject gameObject, ItemStack stack) : base(stack) {
            _buyerCurrency = gameObject.GetComponent<CurrencyBehaviour>().Currency;
            _buyerInventory = gameObject.GetComponent<InventoryBehaviour>().Inventory;
        }
    }

    public class BuyCommand : ShopCommand {
        public BuyCommand(GameObject gameObject, ItemStack stack) : base(gameObject, stack) {
        }

        public override void Execute() {

            int price = Stack.Count * Stack.Item.CostPrice;
            
            if (_buyerCurrency.Currency.TotalCopper < price) {
                throw new CurrencyException("Not enough money");
            }

            _buyerCurrency.Remove(price);
            _buyerInventory.Add(Stack);
        }
    }

    public class SellCommand : ShopCommand {
        public SellCommand(GameObject gameObject, ItemStack stack) : base(gameObject, stack) {
        }

        public override void Execute() {
            int price = Stack.Count * Stack.Item.CostPrice;
            _buyerCurrency.Add(price);
            _buyerInventory.Remove(Stack);
        }
    }
}