using UnityEngine;

namespace NTween.Develop {

    public class ExampleUsage : MonoBehaviour {
        private float foo = 0f;
        private int bar = 0;

        void Start() {
            
            // float 変数の補間
            Tween.To(() => foo, x => foo = x, 10f, 5f);

            // int 変数の補間
            //Tween.FromTo(x => bar = x, 0, 10, 5f);
        }

        void Update() {
            Debug.Log( $"[{Time.time}] = {foo}");
        }
    }
}
