using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Rotator1 : MonoBehaviour
{
    public static Rotator1 instance;
    public float rotationSpeed = 60f;

    [Header("Scene Setting")]
    public bool DirectionChange = false;
    public float changeInterval = 5f;

    private float direction = 1f;
    private float timer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //  �� �̵� �Ŀ��� ����

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�� �� �̺�Ʈ �߰�
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //  ���� ������ �� �̺�Ʈ ���� (�޸� ���� ����)
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ȸ�� �ʱ�ȭ
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (DirectionChange)
                Boss();
            else
                Rotate();
        }
    }

    void Rotate()
    {
        //Rotate(float xAngle, float yAngle, float zAngle)
        transform.Rotate(xAngle: 0f, yAngle: Time.deltaTime * rotationSpeed, zAngle: 0f);
    }

    void Boss()
    {
        // ��ü�� y ���� �������� ȸ��
        transform.Rotate(0f, Time.deltaTime * rotationSpeed * direction, 0f);

        // Ÿ�̸� ����
        timer += Time.deltaTime;

        // 5�ʸ��� ���� ����
        if (timer >= changeInterval)
        {
            direction *= -1; // ������ �ݴ�� ��ȯ
            timer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }

}