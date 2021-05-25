using UnityEngine;
using UnityEditor;

public class AddPlayerPrefsKey : EditorWindow
{
    [MenuItem("Saves/Manage keys")]
    public static void OpenWindow()
    {
        GetWindow<AddPlayerPrefsKey>("Key manager");
    }

    string stringKey;
    string intKey;
    string floatKey;

    string stringValue;
    int intValue;
    float floatValue;

    bool stringValueKey;
    bool intValueKey;
    bool floatValueKey;

    bool stringReadKey;
    bool intReadKey;
    bool floatReadKey;

    string readKeyString;
    string readKeyInt;
    string readKeyFloat;

    public void OnGUI()
    {
        GUILayout.Space(30f);
        
        GUILayout.Label("Set/Create key", EditorStyles.boldLabel);


        #region SetKey
        GUILayout.Space(20f);

        stringValueKey = GUILayout.Toggle(stringValueKey, "String");

        GUILayout.Space(5f);

        if (stringValueKey)
        {
            GUILayout.Label("string key", EditorStyles.boldLabel);
            stringKey = EditorGUILayout.TextField("Key name", stringKey);
            stringValue = EditorGUILayout.TextField("Key value", stringValue);
        }

        GUILayout.Space(20f);

        intValueKey = GUILayout.Toggle(intValueKey, "Int");

        GUILayout.Space(5f);

        if (intValueKey)
        {
            GUILayout.Label("string key", EditorStyles.boldLabel);
            intKey = EditorGUILayout.TextField("Key name", intKey);
            intValue = EditorGUILayout.IntField("Key value", intValue);
        }

        GUILayout.Space(20f);

        floatValueKey = GUILayout.Toggle(floatValueKey, "Float");

        GUILayout.Space(5f);

        if (floatValueKey)
        {
            GUILayout.Label("string key", EditorStyles.boldLabel);
            floatKey = EditorGUILayout.TextField("Key name", floatKey);
            floatValue = EditorGUILayout.FloatField("Key value", floatValue);
        }


        GUILayout.Space(20f);

        if(GUILayout.Button("Set/Create key"))
        {
            if (stringValueKey)
            {
                if(stringKey != "")
                {
                    PlayerPrefs.SetString(stringKey, stringValue);
                }
            }

            if (intValueKey)
            {
                if(intKey != "")
                {
                    PlayerPrefs.SetInt(intKey, intValue);
                }
            }

            if (floatValueKey)
            {
                if(floatKey != "")
                {
                    PlayerPrefs.SetFloat(floatKey, floatValue);
                }
            }
        }
        #endregion

        GUILayout.Space(30f);

        #region ReadKey
        GUILayout.Label("Read key", EditorStyles.boldLabel);
        GUILayout.Space(20f);

        stringReadKey = GUILayout.Toggle(stringReadKey, "String");
        if (stringReadKey)
        {
            readKeyString = EditorGUILayout.TextField("Key name", readKeyString);
        }

        GUILayout.Space(20f);

        intReadKey = GUILayout.Toggle(intReadKey, "Int");
        if (intReadKey)
        {
            readKeyInt = EditorGUILayout.TextField("Key name", readKeyInt);
        }

        GUILayout.Space(20f);

        floatReadKey = GUILayout.Toggle(floatReadKey, "float");
        if (floatReadKey)
        {
            readKeyFloat = EditorGUILayout.TextField("Key name", readKeyFloat);
        }

        GUILayout.Space(20f);

        if (GUILayout.Button("Read"))
        {
            if (stringReadKey)
            {
                string temp = PlayerPrefs.GetString(readKeyString);
                Debug.Log(temp.ToString()); 
            }

            if (intReadKey)
            {
                int temp = PlayerPrefs.GetInt(readKeyInt);
                Debug.Log(temp.ToString());
            }

            if (floatReadKey)
            {
                float temp = PlayerPrefs.GetFloat(readKeyFloat);
                Debug.Log(temp.ToString());
            }
        }

        #endregion
    }
}
