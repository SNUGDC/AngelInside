using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Dialogue
{
    [RequireComponent(typeof(Image))]
    public class Character : MonoBehaviour
    {
        // yarn use `Object.name` to identify the character
        public string Name => name;

        [YarnCommand("show")]
        public void Show()
        {
            GetComponent<Image>().enabled = true;
        }

        [YarnCommand("hide")]
        public void Hide()
        {
            GetComponent<Image>().enabled = false;
        }

        public void SetSprite(string spriteName)
        {
            Sprite image = GameAssetReferences.Load<Sprite>($"Characters/{Name}/{spriteName}");
            GetComponent<Image>().sprite = image;
        }
    }
}
