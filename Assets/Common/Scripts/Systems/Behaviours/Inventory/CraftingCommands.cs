using System.Collections.Generic;
using System.Linq;

namespace CraftingCommands
{
    public class CraftCommand : ICommand {
        Recipe _recipe;
        InventoryComponent _inventory;
        int _craftCount;

        public CraftCommand(Recipe recipe, InventoryComponent inventory, int craftCount) {
            _recipe = recipe;
            _inventory = inventory;   
            _craftCount = craftCount;
        }

        public void Execute() {
            for (int i = 0; i < _recipe.Material.Count; i++) {
                if (_inventory.Inventory.Where(x => x.Item == _recipe.Material[i].Item).Count() == 0) {
                    throw new System.Exception("Insufficient material");
                }
            }

            for (int i = 0; i < _recipe.Material.Count; i++) {
                _inventory.Remove(_recipe.Material[i]);
            }

            // add result
            _inventory.Add(_recipe.Result);
        }
    }
}