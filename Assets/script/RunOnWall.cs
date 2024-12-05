using UnityEngine;

public class RunOnWall : MonoBehaviour
{
    public bool runWall;
    public bool IsLeftRun;

    public Transform RightChecker;
    public Transform LeftChecker;

    private void Update() 
    {
        if (Physics.CheckSphere(RightChecker.position, 0.1f, LayerMask.GetMask("wallrun")))
        {
            runWall = true;
            IsLeftRun = false; // Стена справа
            Debug.Log("Правая сторона");
        }
        else if (Physics.CheckSphere(LeftChecker.position, 0.1f, LayerMask.GetMask("wallrun")))
        {
            runWall = true;
            IsLeftRun = true; // Стена слева
            Debug.Log("Левая сторона");
        }
        else
        {
            runWall = false; // Нет стены
        }
    }
}
