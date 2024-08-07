using System.Collections;
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
        public Image backgroundImage;

        [Required]
        public GameObject characterView;

        [Required]
        public FadeUI fadeUI;

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
        public static IEnumerator Background(string backgroundName)
        {
            Sprite image = GameAssetReferences.Load<Sprite>($"Backgrounds/{backgroundName}");

            yield return Instance.fadeUI.FadeIn();

            Instance.backgroundImage.sprite = image;

            yield return Instance.fadeUI.FadeOut();
        }

        [YarnCommand("hide_all")]
        public static void HideAll()
        {
            foreach (var character in Instance.characterView.GetComponentsInChildren<Character>())
            {
                character.Hide();
            }
        }

        // Set sprite (not yarn command version)
        public static void SetSprite(string characterName, string spriteName)
        {
            Character character = Instance
                .characterView.transform.Find(characterName)
                .GetComponent<Character>();
            Assert.IsNotNull(character, $"Character {characterName} not found");
            character.SetSprite(spriteName);
        }
    }
}
