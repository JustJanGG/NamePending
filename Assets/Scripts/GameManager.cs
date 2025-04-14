using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Instance")]
    public static GameManager instance;

    [Header("Lists")]
    public GameObject[] enemyList;
    public GameObject[] circuitList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
