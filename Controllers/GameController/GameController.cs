using UnityEngine;
using System.Runtime.InteropServices;
using Photon.Pun;

public class GameWindowManager : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(System.IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();

    void Start()
    {
        // Garante que o jogo abre no monitor principal
        DontDestroyOnLoad(this.gameObject);
        Screen.fullScreen = true;
        var display = Display.displays[0];
        Screen.SetResolution(display.systemWidth, display.systemHeight, false);
        SetForegroundWindow(GetActiveWindow());
    }

    void Update()
    {
        // Alterna entre tela cheia e modo janela ao pressionar F11
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Screen.fullScreen = !Screen.fullScreen; //Invesor de valor booleano XOR
        }
    }

    void OnApplicationQuit()
    {
        // Garante que o processo seja encerrado completamente
        Application.Quit();
    }
}