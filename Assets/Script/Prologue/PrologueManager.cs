using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PrologueManager : MonoBehaviour
{
    public Image backgroundImage;
    public TextMeshProUGUI subtitleText;
    public AudioSource audioSource;
    public AudioClip typingSound;
    public AudioClip rainSound;
    private AudioSource rainAudioSource;
    public Sprite[] images;
    public string[] subtitles = {
        "로봇과 아이는 서로에게 가장 소중한 친구였다...",
        "평소와 같이 아이를 데리러 가는길에...",
        "그날, 모든 것이 산산조각 났다...",
        "아이를 찾으러 가야하는데..."
    };
    private int currentIndex = 0;
    private bool isTransitioning = false;
    private Coroutine _typingCoroutine;

    void Start()
    {
        rainAudioSource = gameObject.AddComponent<AudioSource>();
        rainAudioSource.clip = rainSound;
        rainAudioSource.loop = true;
        rainAudioSource.volume = 0.5f;
        rainAudioSource.playOnAwake = false;

        ShowSlide(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            NextSlide();
        }
    }

    void ShowSlide(int index)
    {
        backgroundImage.sprite = images[index];
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);

        if (index == images.Length - 1)
            rainAudioSource.Play();
        else
            rainAudioSource.Stop();

        bool useSound = index != subtitles.Length - 1;
        _typingCoroutine = StartCoroutine(TypeText(subtitles[index], useSound));
    }

    IEnumerator TypeText(string fullText, bool useSound = true)
    {
        subtitleText.text = "";
        foreach (char c in fullText)
        {
            subtitleText.text += c;
            if (useSound && c != ' ' && audioSource != null && typingSound != null)
                audioSource.PlayOneShot(typingSound, 0.5f);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.4f);
            subtitleText.text += ".";
        }
    }

    public void NextSlide()
    {
        if (isTransitioning) return;
        currentIndex++;
        if (currentIndex >= images.Length)
        {
            SceneManager.LoadScene("JunkYardScene");
        }
        else
        {
            StartCoroutine(FadeTransition());
        }
    }

    IEnumerator FadeTransition()
    {
        isTransitioning = true;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            backgroundImage.color = new Color(1, 1, 1, 1f - t);
            yield return null;
        }
        ShowSlide(currentIndex);
        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            backgroundImage.color = new Color(1, 1, 1, t);
            yield return null;
        }
        isTransitioning = false;
    }
}