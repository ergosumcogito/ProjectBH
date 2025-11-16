using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    [SerializeField] public VisualTreeAsset VisualTree;

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        VisualTree.CloneTree(root);

        //initializing elements
        var minPropField = root.Q<PropertyField>("minPropField");
        var maxPropField = root.Q<PropertyField>("maxPropField");
        var minMaxSlider = root.Q<MinMaxSlider>("minMaxSlider");

        //removing premade labels
        minPropField.label = "";
        maxPropField.label = "";

        //get spawn distance limits
        var minProp = serializedObject.FindProperty("minSpawnDistance");
        var maxProp = serializedObject.FindProperty("maxSpawnDistance");

        //bind to prop fields
        minPropField.BindProperty(minProp);
        maxPropField.BindProperty(maxProp);

        //fallback
        var levelMax = 10;

        var levelEditor = FindFirstObjectByType<LevelEditor>();
        if (levelEditor != null)
        {
            //getting max limit
            levelMax = Mathf.Min(levelEditor.Width, levelEditor.Length);
        }

        minMaxSlider.lowLimit = 1;
        minMaxSlider.highLimit = levelMax;

        //setting min and max value for slider
        minMaxSlider.value = new Vector2(minProp.floatValue, maxProp.floatValue);

        //when slider changes, update prop fields
        minMaxSlider.RegisterValueChangedCallback(evt =>
        {
            serializedObject.Update();

            var min = Mathf.RoundToInt(evt.newValue.x);
            var max = Mathf.RoundToInt(evt.newValue.y);

            ClampMinMax(ref min, ref max, levelMax);

            minProp.floatValue = min;
            maxProp.floatValue = max;

            minMaxSlider.SetValueWithoutNotify(new Vector2(min, max));
            serializedObject.ApplyModifiedProperties();
        });

        //when min prop field changes, update slider
        minPropField.RegisterCallback<SerializedPropertyChangeEvent>(evt =>
        {
            serializedObject.Update();

            var min = Mathf.RoundToInt(minProp.floatValue);
            var max = Mathf.RoundToInt(maxProp.floatValue);

            ClampMinMax(ref min, ref max, levelMax);

            minProp.floatValue = min;
            maxProp.floatValue = max;

            minMaxSlider.SetValueWithoutNotify(new Vector2(min, max));
            serializedObject.ApplyModifiedProperties();
        });

        //when max prop field changes, update slider
        maxPropField.RegisterCallback<SerializedPropertyChangeEvent>(evt =>
        {
            serializedObject.Update();

            var min = Mathf.RoundToInt(minProp.floatValue);
            var max = Mathf.RoundToInt(maxProp.floatValue);

            ClampMinMax(ref min, ref max, levelMax);

            minProp.floatValue = min;
            maxProp.floatValue = max;

            minMaxSlider.SetValueWithoutNotify(new Vector2(min, max));
            serializedObject.ApplyModifiedProperties();
        });

        return root;
    }

    //limits for slider
    private static void ClampMinMax(ref int min, ref int max, int levelMax)
    {
        if (min < 1) min = 1;
        if (max > levelMax) max = levelMax;

        if (min > levelMax - 1)
            min = levelMax - 1;

        if (max < min + 1)
            max = min + 1;

        if (max > levelMax)
            max = levelMax;
    }
}