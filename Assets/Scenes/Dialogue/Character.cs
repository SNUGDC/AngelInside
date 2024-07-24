using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Dialogue
{
    [RequireComponent(typeof(Image))]
    public class Character : MonoBehaviour
    {
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
    }
}
