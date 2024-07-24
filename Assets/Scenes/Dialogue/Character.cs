using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Yarn.Unity;

namespace Dialogue
{
    [RequireComponent(typeof(Image))]
    public class Character : MonoBehaviour
    {
        // yarn use `Object.name` to identify the character
        public string Name => name;

        [YarnCommand("setSprite")]
        public void SetSprite(string spriteName)
        {
            Sprite image = GameAssetReferences.Load<Sprite>($"Characters/{Name}/{spriteName}");
            GetComponent<Image>().sprite = image;
        }

        /// <summary>
        /// x: 0.0 ~ 1.0
        /// </summary>
        [YarnCommand("setPosition")]
        public void SetPosition(float x)
        {
            // Position relative to parent
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(
                x * rectTransform.parent.GetComponent<RectTransform>().rect.width,
                rectTransform.anchoredPosition.y
            );
        }

        [YarnCommand("setDirection")]
        public void SetDirection(string direction)
        {
            bool left = false;
            switch (direction)
            {
                case "left":
                    left = true;
                    break;
                case "right":
                    left = false;
                    break;
                default:
                    Assert.IsTrue(false, $"Invalid direction: {direction}");
                    break;
            }

            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(left ? -1 : 1, 1, 1);
        }

        [YarnCommand("show")]
        public void Show(string spriteName = null, string x = null, string direction = null)
        {
            if (spriteName != null)
            {
                SetSprite(spriteName);
            }
            if (x != null)
            {
                SetPosition(float.Parse(x));
            }
            if (direction != null)
            {
                SetDirection(direction);
            }
            GetComponent<Image>().enabled = true;
        }

        [YarnCommand("hide")]
        public void Hide()
        {
            GetComponent<Image>().enabled = false;
        }
    }
}
