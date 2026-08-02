using UnityEngine; 

public class FloatValue : ScriptableObject
{
    [SerializeField] private float value; 

    public float Value => value; 

    public void SetValue(float v)
    {
        value = v; 
    }
}