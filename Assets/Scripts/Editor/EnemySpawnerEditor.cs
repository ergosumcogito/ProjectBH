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
        var spawner = (EnemySpawner)target;

        var root = new VisualElement();
        VisualTree.CloneTree(root);

        //initializing elements
        var minPropField = root.Q<IntegerField>("minPropField");
        var maxPropField = root.Q<IntegerField>("maxPropField");
        var minMaxSlider = root.Q<MinMaxSlider>("minMaxSlider");
        var maxEnemiesPropField = root.Q<PropertyField>("maxEnemiesPropField");
        var spawnIntervalPropField = root.Q<PropertyField>("spawnIntervalPropField");

        //progress bar element, and element that renders the bar of the progress bar
        var progressBar = root.Q<ProgressBar>("spawnerTotalLoad");
        var progressFill = progressBar.Q<VisualElement>(className: "unity-progress-bar__progress");

        //removing premade labels
        minPropField.label = "";
        maxPropField.label = "";

        //get values from prop fields
        var minProp = serializedObject.FindProperty("minSpawnDistance");
        var maxProp = serializedObject.FindProperty("maxSpawnDistance");
        var maxEnemiesProp = serializedObject.FindProperty("maxEnemies");
        var spawnIntervalProp = serializedObject.FindProperty("spawnInterval");

        //bind values to min and max prop fields
        minPropField.BindProperty(minProp);
        maxPropField.BindProperty(maxProp);

        //updates progress bar
        UpdateProgressBar(spawner.CurrentEnemyCount);
        spawner.OnEnemyCountChanged += UpdateProgressBar;

        //fallback for spawn distances
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

        //setting up fields
        SetupMinMaxField(minPropField);
        SetupMinMaxField(maxPropField);
        SetupMaxEnemiesField(maxEnemiesPropField, maxEnemiesProp);
        SetupSpawnInterval(spawnIntervalPropField, spawnIntervalProp);

        progressBar.lowValue = 0;
        progressBar.highValue = maxEnemiesProp.intValue;

        return root;

        //when props are not focused anymore, update their value if illegal
        void SetupMinMaxField(IntegerField field)
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

        void SetupMaxEnemiesField(PropertyField field, SerializedProperty property)
        {
            field.RegisterCallback<GeometryChangedEvent>(evt =>
            {
                var intField = field.Q<IntegerField>();

                intField.UnregisterCallback<BlurEvent>(OnBlur);
                intField.UnregisterCallback<KeyDownEvent>(OnKeyDown);

                intField.RegisterCallback<BlurEvent>(OnBlur);
                intField.RegisterCallback<KeyDownEvent>(OnKeyDown);
                return;

                void OnBlur(BlurEvent e)
                {
                    ApplyMaxEnemiesClamp(property);
                    UpdateProgressBarHighValue();
                    UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                }

                void OnKeyDown(KeyDownEvent e)
                {
                    if (e.keyCode is not (KeyCode.Return or KeyCode.KeypadEnter)) return;
                    ApplyMaxEnemiesClamp(property);
                    UpdateProgressBarHighValue();
                    UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                }
            });
        }

        void SetupSpawnInterval(PropertyField field, SerializedProperty property)
        {
            field.RegisterCallback<GeometryChangedEvent>(evt =>
            {
                var floatField = field.Q<FloatField>();

                floatField.UnregisterCallback<BlurEvent>(OnBlur);
                floatField.UnregisterCallback<KeyDownEvent>(OnKeyDown);

                floatField.RegisterCallback<BlurEvent>(OnBlur);
                floatField.RegisterCallback<KeyDownEvent>(OnKeyDown);
                return;

                void OnBlur(BlurEvent e)
                {
                    ApplySpawnIntervalClamp(property);
                    UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                }

                void OnKeyDown(KeyDownEvent e)
                {
                    ApplySpawnIntervalClamp(property);
                    UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                }
            });
        }

        void UpdateProgressBarHighValue()
        {
            serializedObject.Update();
            progressBar.highValue = maxEnemiesProp.intValue;

            UpdateProgressBar(spawner.CurrentEnemyCount);
        }

        //updates values of progress bar and color accordingly
        void UpdateProgressBar(int count)
        {
            serializedObject.Update();

            float max = maxEnemiesProp.intValue;
            var modifier = count / max;

            progressBar.value = count;
            progressBar.title = $"{count} / {max}";

            var speed = Mathf.Pow(modifier, 2f);
            var barColor = Color.Lerp(Color.green, Color.red, speed);

            progressFill.style.backgroundColor = barColor;
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

    //limits values of maxEnemies field
    private void ApplyMaxEnemiesClamp(SerializedProperty maxEnemiesProp)
    {
        serializedObject.Update();

        var maxEnemies = maxEnemiesProp.intValue;

        if (maxEnemies < 1) maxEnemies = 1;

        maxEnemiesProp.intValue = maxEnemies;

        serializedObject.ApplyModifiedProperties();
    }

    //limits values of spawnInterval field
    private void ApplySpawnIntervalClamp(SerializedProperty spawnIntervalProp)
    {
        serializedObject.Update();

        var spawnInterval = spawnIntervalProp.floatValue;

        if (spawnInterval < 0.001f) spawnInterval = 0.001f;

        spawnIntervalProp.floatValue = spawnInterval;

        serializedObject.ApplyModifiedProperties();
    }
}