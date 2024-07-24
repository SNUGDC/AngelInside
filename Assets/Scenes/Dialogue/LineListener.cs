using System;
using UnityEngine.Assertions;
using Yarn.Unity;

namespace Dialogue
{
    public class LineListener : DialogueViewBase
    {
        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            // Listen for dialogueLine, check tag, and set character sprite
            string characterName = dialogueLine.CharacterName;
            if (!string.IsNullOrWhiteSpace(characterName) && dialogueLine.Metadata is not null)
            {
                Assert.AreEqual(1, dialogueLine.Metadata.Length);
                string spriteName = dialogueLine.Metadata[0];
                SceneManager.SetSprite(characterName, spriteName);
            }

            // Our presentation is complete; call the completion handler.
            onDialogueLineFinished();
        }
    }
}
