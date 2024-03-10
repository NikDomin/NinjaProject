using DataPersistence;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



    [CustomEditor(typeof(DataPersistenceManager))]
    public class DataPersistenceManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            DataPersistenceManager manager = (DataPersistenceManager)target;
            if (GUILayout.Button("DeleteSaveFile"))
            {
                manager.CreateFileDataHandler();
                manager.dataHandler.DeleteSaveFile();
            }
        }
    }
