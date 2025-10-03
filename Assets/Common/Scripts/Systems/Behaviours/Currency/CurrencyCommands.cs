using System.Threading.Tasks;
using UnityEngine;

namespace CurrencyCommands {
    public class DropCommand : ICommand {
        private int _value;
        private Vector2 _origin;

        public DropCommand(int value, Vector2 origin) {
            _value = value;
            _origin = origin;
        }

        public void Execute() {
            Drop();
        }

        async void Drop() {
            var coin = await CoinFactory.Instance.Pool.GetObject<int>();
            coin.Bind(_value);
            ((MonoBehaviour)coin).GetComponent<Dropable>().Drop(_origin);
        }
    }
}