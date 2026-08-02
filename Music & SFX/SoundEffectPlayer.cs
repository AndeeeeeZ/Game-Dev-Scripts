using UnityEngine;
using UnityEditor;

/*
 * Plays a random audio clip with optional random pitch and volume
 */

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;

    [SerializeField] private bool randomPitch = true;
    [SerializeField, Range(0.5f, 3f)] private float pitch = 1f;
    [SerializeField] private Vector2 randomPitchRange = new(0.9f, 1.1f);

    [SerializeField] private bool randomVolume = true;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;
    [SerializeField] private Vector2 randomVolumeRange = new(0.8f, 1f);

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        if (audioClips == null || audioClips.Length == 0)
        {
            Debug.LogError("SoundEffectPlayer is missing audio clips.", this);
            return;
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.pitch = randomPitch
            ? Random.Range(randomPitchRange.x, randomPitchRange.y)
            : pitch;

        audioSource.volume = randomVolume
            ? Random.Range(randomVolumeRange.x, randomVolumeRange.y)
            : volume;

        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SoundEffectPlayer))]
public class SoundEffectPlayerEditor : Editor
{
    private SerializedProperty audioClips;

    private SerializedProperty randomPitch;
    private SerializedProperty pitch;
    private SerializedProperty randomPitchRange;

    private SerializedProperty randomVolume;
    private SerializedProperty volume;
    private SerializedProperty randomVolumeRange;

    private void OnEnable()
    {
        audioClips = serializedObject.FindProperty("audioClips");

        randomPitch = serializedObject.FindProperty("randomPitch");
        pitch = serializedObject.FindProperty("pitch");
        randomPitchRange = serializedObject.FindProperty("randomPitchRange");

        randomVolume = serializedObject.FindProperty("randomVolume");
        volume = serializedObject.FindProperty("volume");
        randomVolumeRange = serializedObject.FindProperty("randomVolumeRange");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(audioClips);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Pitch", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(randomPitch, new GUIContent("Random"));

        if (randomPitch.boolValue)
            EditorGUILayout.PropertyField(randomPitchRange, new GUIContent("Range"));
        else
            EditorGUILayout.PropertyField(pitch);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Volume", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(randomVolume, new GUIContent("Random"));

        if (randomVolume.boolValue)
            EditorGUILayout.PropertyField(randomVolumeRange, new GUIContent("Range"));
        else
            EditorGUILayout.PropertyField(volume);

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();

        GUI.enabled = Application.isPlaying;

        if (GUILayout.Button("Play Random Clip"))
        {
            ((SoundEffectPlayer)target).PlayClip();
        }

        GUI.enabled = true;
    }
}
#endif