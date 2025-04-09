using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour
{

    public float rotationSpeed = 60f;

    [Header("Scene Setting")]
    public bool DirectionChange = false;
    public float changeInterval = 5f;
    public int gametimer;

    private float direction = 1f;
    private float timer;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�� �� �̺�Ʈ �߰�
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //  ���� ������ �� �̺�Ʈ ���� (�޸� ���� ����)
    }

    // Ư�� ���� �ε�� �� DirectionChange ����
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1) // Ư�� ��(��: 1�� ��)���� Ȱ��ȭ
        {
            this.enabled = true;
        }
        else
        {
            this.enabled = false;
        }

        Debug.Log($"�� �����: {scene.name} (Index: {scene.buildIndex})");
    }

    void Update()
    {
        if (DirectionChange)
        {
            Boss();
        }
        else
        {
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
