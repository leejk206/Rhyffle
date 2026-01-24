using UnityEngine;

public class FilterCheck : MonoBehaviour
{
    private GameObject checkObject;

    private void Awake()
    {
        // "Check"라는 이름을 가진 자식 오브젝트 찾기
        checkObject = transform.Find("Check")?.gameObject;
    }

    public void ToggleCheck()
    {
        if (checkObject != null)
        {
            checkObject.SetActive(!checkObject.activeSelf);
        }
    }
}