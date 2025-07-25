using System;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [Range(0, 0.5f)][SerializeField] private float motionSpeed; // Velocidade do background
    [Range(0, 0.09f)][SerializeField] private float increaseMotionSpeed; // Aumento de velocidade a cada ativação do evento

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        ProceduralGeneration.OnIncreaseSpeed += IncreaseBackgroundSpeed; // Inscreve o método ao evento associado
    }

    private void OnDestroy()
    {
        ProceduralGeneration.OnIncreaseSpeed -= IncreaseBackgroundSpeed; // Remove a inscrição do evento ao ser destruído
    }
    
    void Update()
    {
        MoveBackground();
    }

    private void MoveBackground()
    {
        _renderer.material.mainTextureOffset += new Vector2(motionSpeed * Time.deltaTime, 0);
    }
    
    private void IncreaseBackgroundSpeed()
    {
        motionSpeed += increaseMotionSpeed;
    }
}