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
        var minPropField = root.Q<IntegerField>("minPropField");
        var maxPropField = root.Q<IntegerField>("maxPropField");
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
        minMaxSlider.value = new Vector2(minProp.intValue, maxProp.intValue);

        //sets props to value of slider
        minMaxSlider.RegisterValueChangedCallback(evt =>
        {
            serializedObject.Update();

            var min = Mathf.RoundToInt(evt.newValue.x);
            var max = Mathf.RoundToInt(evt.newValue.y);

            ClampMinMax(ref min, ref max, levelMax);

            minProp.intValue = min;
            maxProp.intValue = max;

            minMaxSlider.SetValueWithoutNotify(new Vector2(min, max));
            minPropField.SetValueWithoutNotify(min);
            maxPropField.SetValueWithoutNotify(max);

            serializedObject.ApplyModifiedProperties();
        });

        SetupField(minPropField);
        SetupField(maxPropField);

        return root;

        //when props are not focused anymore, update their value if illegal
        void SetupField(IntegerField field)
        {
            field.RegisterCallback<BlurEvent>(evt =>
            {
                ApplyFieldClamp(minPropField, maxPropField, minProp, maxProp, minMaxSlider, levelMax);
            });

            field.RegisterCallback<KeyDownEvent>(evt =>
            {
                if (evt.keyCode is KeyCode.Return or KeyCode.KeypadEnter)
                {
                    ApplyFieldClamp(minPropField, maxPropField, minProp, maxProp, minMaxSlider, levelMax);
                }
            });
        }
    }

    //limits for values
    //min >= 1
    //max <= levelMax
    //min can't reach max
    //max can't drop below min - 1
    private static void ClampMinMax(ref int min, ref int max, int levelMax)
    {
        min = Mathf.Clamp(min, 1, levelMax - 1);
        max = Mathf.Clamp(max, min + 1, levelMax);
    }

    //checks whether fields have legal values, if not, clamps
    private void ApplyFieldClamp(
        IntegerField minPropField,
        IntegerField maxPropField,
        SerializedProperty minProp,
        SerializedProperty maxProp,
        MinMaxSlider slider,
        int levelMax)
    {
        serializedObject.Update();

        var min = minPropField.value;
        var max = maxPropField.value;

        ClampMinMax(ref min, ref max, levelMax);

        minProp.intValue = min;
        maxProp.intValue = max;

        slider.SetValueWithoutNotify(new Vector2(min, max));
        minPropField.SetValueWithoutNotify(min);
        maxPropField.SetValueWithoutNotify(max);

        serializedObject.ApplyModifiedProperties();
    }
}