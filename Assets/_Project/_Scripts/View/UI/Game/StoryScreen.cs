using System.Collections;
using UnityEngine;
using TMPro;
using View.Button;

namespace View.UI.Game
{
    public class StoryScreen : UIScreen
    {
        [SerializeField] private TMP_Text _storyText;
        [SerializeField] private CustomButton _okButton;
        [SerializeField] private float _typeSpeed = 0.04f;

        private Coroutine _typingCoroutine;
        private string[] _stories =
        {
            "The fox swears one cage clucked in its sleep and demanded grain.",
            "Yesterday, a chicken inside a cage learned to imitate the fox’s laugh. Unnervingly well.",
            "The fox tried to push two cages at once… and is still looking for the third.",
            "One chicken refused to leave its cage without ‘compensation’: exactly three grains.",
            "Sometimes a cage creaks so sadly it feels like you’re pushing it the wrong way.",
            "The fox overheard the chickens arguing about which one is ‘today’s main quest.’"
        };

        private System.Action _onComplete;

        private void Awake()
        {
            if (_okButton != null)
            {
                _okButton.AddListener(OnOkPressed);
            }
        }

        public void ShowStory(System.Action onComplete)
        {
            _onComplete = onComplete;

            gameObject.SetActive(true);
            _okButton.Interactable = false;
            _okButton.gameObject.SetActive(false);

            _storyText.text = "";
            string randomStory = _stories[Random.Range(0, _stories.Length)];

            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);

            _typingCoroutine = StartCoroutine(TypeRoutine(randomStory));
        }

        private IEnumerator TypeRoutine(string text)
        {
            _storyText.text = "";

            foreach (char c in text)
            {
                _storyText.text += c;
                yield return new WaitForSeconds(_typeSpeed);
            }

            _okButton.gameObject.SetActive(true);
            _okButton.Interactable = true;

            _typingCoroutine = null;
        }

        private void OnOkPressed()
        {
            _okButton.Interactable = false;
            gameObject.SetActive(false);

            _onComplete?.Invoke();
        }
    }
}