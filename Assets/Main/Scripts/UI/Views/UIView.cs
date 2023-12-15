using UnityEngine;

public abstract class UIView : MonoBehaviour
{
    public abstract void Initialize();
    public abstract void SetActive(bool isActive);
    public abstract void UpdateView();
}
