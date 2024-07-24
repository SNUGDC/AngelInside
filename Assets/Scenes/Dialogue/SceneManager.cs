using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Yarn.Unity;

namespace Dialogue
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        [Required]
        public GameObject characterView;

        [Required]
        public Image backgroundImage;

        private void OnValidate()
        {
            Required.Assert(this);
        }

        private void Awake()
        {
            Instance = this;
            GameManager.AddGameEssential();
        }

        [YarnCommand("background")]
        public static void Background(string backgroundName)
        {
            Sprite image = Resources.Load<Sprite>("Backgrounds/" + backgroundName);
            Assert.IsNotNull(image, $"Background image \"{backgroundName}\" not found");
            Instance.backgroundImage.sprite = image;
        }

        [YarnCommand("hide_all")]
        public static void HideAll()
        {
            foreach (var character in Instance.characterView.GetComponentsInChildren<Character>())
            {
                character.Hide();
            }
        }
    }
}
