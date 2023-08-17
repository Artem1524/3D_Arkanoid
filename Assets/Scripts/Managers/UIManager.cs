using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Managers
{
    public class UIManager : MonoBehaviour
    {
        private static readonly string LIFE_TEXT_OBJ_NAME = "LifeText";
        private static readonly string LIFE_FORMAT_TEXT = "<color=#B3FF00><b>{0}</b></color>";

        [SerializeField]
        [Tooltip("Список элементов UI типа 'LifeInfo', на которых необходимо обновить число жизней")]
        private LifeInfo[] _lifeBoards;

        private static UIManager _self;

        private void Start()
        {
            _self = this;
        }

        public void UpdateLifeInfo(int currentLives)
        {
            List<Text> uiLivesTexts = GetUILivesTextComponents();

            foreach (Text obj in uiLivesTexts)
            {
                obj.text = string.Format(LIFE_FORMAT_TEXT, currentLives);
            }
        }

        private List<Text> GetUILivesTextComponents()
        {
            List<Text> uiLivesTexts = new List<Text>();

            foreach (LifeInfo info in _lifeBoards)
            {
                Transform textObj = info.transform.Find(LIFE_TEXT_OBJ_NAME);

                if (textObj != null && textObj.TryGetComponent<Text>(out Text text))
                    uiLivesTexts.Add(text);
            }

            return uiLivesTexts;
        }

        private void DrawMenuPauseScreen()
        {

        }

        public static UIManager GetInstance()
        {
            return _self;
        }
    }
}