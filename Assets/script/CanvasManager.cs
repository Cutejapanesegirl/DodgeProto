using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    private GameObject levelUpUI;
    private GameObject gameStartUI;
    private GameObject gameResultUI;
    private GameObject hudUI;
    private GameObject stageUI;

    private Canvas currentCanvas; // ���� ���� Canvas�� ������ ����

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // ������ CanvasManager�� ����
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // CanvasManager�� �� ���� �ÿ��� ����

        // ���� ����� ������ UI ������Ʈ���� ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // �� ���� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ������ UI ������Ʈ�� �ٽ� �Ҵ�
        UpdateUI();

        // �� ���� Canvas�� ã�ų� ���� �Ҵ�
        Canvas newCanvas = FindObjectOfType<Canvas>();

        if (newCanvas != null)
        {
            // ������ CanvasManager�� ���ο� Canvas�� �Ҵ�
            currentCanvas = newCanvas;

            // ���ο� Canvas�� DontDestroyOnLoad�� ����
            DontDestroyOnLoad(currentCanvas.gameObject);
        }
        else
        {
            Debug.LogWarning("���ο� ���� Canvas�� �����ϴ�.");
        }
    }

    public void UpdateUI()
    {
        // ���� ������ �ʿ��� UI ������Ʈ���� �ٽ� ã��
        levelUpUI = GameObject.Find("LevelUP");
        gameStartUI = GameObject.Find("GameStart");
        gameResultUI = GameObject.Find("GameResult");
        hudUI = GameObject.Find("HUD");
        stageUI = GameObject.Find("StageUI");

        // �� UI�� null�� �ƴ��� üũ�Ͽ� �ʿ� �� ó�� �߰�
    }
}
