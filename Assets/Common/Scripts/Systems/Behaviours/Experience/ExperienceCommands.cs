using System.Threading.Tasks;
using UnityEngine;

namespace ExperienceCommands{
    public class DropCommand : ICommand {
        private float _amount;
        private Vector2 _origin;

        public DropCommand(float value, Vector2 origin) {
            _amount = value;
            _origin = origin;
        }

        public void Execute() {
            Drop();
        }

        async void Drop()
        {
            var globe = await GlobeFactory.Instance.Pool.GetObject<GlobeData>();
            GlobeData globeData = new GlobeData(GlobeComponent.Type.Experience, _amount);
            globe.Bind(globeData);
            ((MonoBehaviour)globe).GetComponent<Dropable>().Drop(_origin);
            Debug.Break();
        }
    }
}