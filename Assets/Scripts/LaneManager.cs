using UnityEngine;

public class LaneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] lanes;


    private void Start()
    {

    }



    public void SetLanes(params int[] inputLanes)
    {
        for (int i = 0; i < inputLanes.Length; i++)
        {
            if (inputLanes[i] == 1)
            {
                lanes[i].gameObject.SetActive(true);
            }
            else
            {
                lanes[i].gameObject.SetActive(false);
            }

        }
    }

}
